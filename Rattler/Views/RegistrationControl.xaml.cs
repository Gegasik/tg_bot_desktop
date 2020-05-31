using Rattler.Controller;
using Rattler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для RegistrationControl.xaml
    /// </summary>
    public partial class RegistrationControl : UserControl
    {
        public RegistrationControl()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            RegistrationController controller = new RegistrationController();
            if(txtPasswordRepeat.Password != txtPassword.Password)
            {
                MessageBox.Show("пароли не совпадают");
                return;
            }
            Client user = new Client(txtUsername.Text, txtPassword.Password, txtUserEmail.Text);
            user.maxCount = 1;
            user.role = 0;
            user.ChatId = 0;
            user.isActive = false;
            user.key = GetPhoto.GenRandomString(10);
            user.LastUpdate = DateTime.Parse("01 01 2019");
            controller.Register(user);
            if (new LoginControl().login(user))
            {
                EntryWindow parentWindow = Window.GetWindow(this) as EntryWindow;
                parentWindow.Close();
            }
        }
    }
}
