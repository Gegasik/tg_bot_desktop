using Rattler.Controller;
using Rattler.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Rattler
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private MyDbContext context;
        private BindingList<Client> searchUsers;
        private Client currentUser;
        private Client admin;
        public AdminWindow(Client adm)
        {
            admin = adm;
            this.DataContext = currentUser;
            searchUsers = new BindingList<Client>();
            InitializeComponent();
            context = new MyDbContext();
            Title = "Окно Админа";
            List<Client> clients = context.Clients.ToList();
            foreach (var it in clients)
            {
                if (it.role == 0) { searchUsers.Add(it); }
            }
        }
        private void useres_Loaded(object sender, RoutedEventArgs e)
        {
            Useres.ItemsSource = searchUsers;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            currentUser = context.Clients.Find(Int32.Parse(button.Tag.ToString()));
            this.DataContext = currentUser;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (currentUser == null) { MessageBox.Show("выберите пользователя"); return; }
            try
            {
                int MaxCounts = Int32.Parse(textBox1.Text);
                if (MaxCounts < 1 || MaxCounts > 5)
                { MessageBox.Show("знаения должны быть от 1 до 5"); }
                while (currentUser.accaunts.Count > MaxCounts)
                {
                    currentUser.accaunts.Remove(currentUser.accaunts.FirstOrDefault());
                }
                currentUser.maxCount = MaxCounts;
                context.SaveChanges();
                MessageBox.Show("данные сохранены");
                DataContext = null;
                DataContext = currentUser;
            }
            catch (System.FormatException ex)
            {
                MessageBox.Show("проверьте вводимые данные, должны быть только цифры");
                return;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (currentUser == null) { MessageBox.Show("выберите пользователя"); return; }
            try
            {
                int role = int.Parse(textBox2.Text);
                if (role != 0 || role != 1)
                { MessageBox.Show("неверные значения, установите 1 если Админ или 0 если пользователь"); }
                currentUser.role = role;
                context.SaveChanges();
                MessageBox.Show("данные сохранены");
                currentUser = context.Clients.Find(currentUser.Id);
                DataContext = currentUser;
            }
            catch (System.FormatException ex)
            {
                MessageBox.Show("проверьте вводимые данные, должны быть только цифры");
                return;
            }
        }

        private void Button_Click_Admin(object sender, RoutedEventArgs e)
        {
            try
            {
                int MaxCounts = Int32.Parse(textAdmin.Text);
                if (MaxCounts < 1 || MaxCounts > 5)
                { MessageBox.Show("знаения должны быть от 1 до 5");
                    return;
                }
                Client client= context.Clients.Find(admin.Id);
                while (client.accaunts.Count > MaxCounts)
                {
                    client.accaunts.Remove(client.accaunts.FirstOrDefault());
                }
                client.maxCount = MaxCounts;
                context.SaveChanges();
                MessageBox.Show("данные сохранены");
            }
            catch (System.FormatException ex)
            {
                MessageBox.Show("проверьте вводимые данные, должны быть только цифры");
                return;
            }

        }

        private void Button_Click_mainW(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(context.Clients.Find(admin.Id));
            main.Show();
        }
    }
}
