using Rattler.Controller;
using Rattler.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rattler.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        public LoginControl()
        {
            InitializeComponent();
        }
        public bool login(Client user)
        {
            try
            {
                LoginController controller = new LoginController();
                Client loginUser = controller.Login(user);
                if (loginUser != null)
                {
                    if(loginUser.role>0)
                    {   
                    AdminWindow window = new AdminWindow(loginUser);
                    window.Show();
                    return true;
                    }
                    else
                    {
                        MainWindow main = new MainWindow(loginUser);
                        main.Show();
                        return true;
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Client user = new Client();
            user.Login = txtUsername.Text;
            user.Password = UnHash(txtPassword.Password);
            if (login(user))
            {
                EntryWindow parentWindow = Window.GetWindow(this) as EntryWindow;
                parentWindow.Close();
            }
            else
            {
                MessageBox.Show("Логин или пароль введены неверно");
            }
        }
        private string UnHash(string pass)
        {
            byte[] data = Encoding.Default.GetBytes(pass);
            var result = new SHA256Managed().ComputeHash(data);
            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }

    }
}
