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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Client currentUser;
        BindingList<Accaunts> accaunts;
        MyDbContext context;
        public MainWindow(Client user)
        {
            context = new MyDbContext();
            currentUser = user;
            DataContext = user;
            InitializeComponent();
            Title = user.Login;
            accaunts = new BindingList<Accaunts>();
            foreach (var acc in user.accaunts)
            {
                Accaunts viewAcc = new Accaunts();
                viewAcc.link = GetPhoto.PhotoById(int.Parse(acc.link));
                viewAcc.Id = acc.Id;
                accaunts.Add(viewAcc);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddWindow add = new AddWindow(currentUser);
            add.ShowDialog();
        }
        private void Button_Click_update(object sender, RoutedEventArgs e)
        {
            accaunts.Clear();
            foreach (var ac in context.Clients.Find(currentUser.Id).accaunts)
            {
                Accaunts viewAcc = new Accaunts();
                viewAcc.link = GetPhoto.PhotoById(int.Parse(ac.link));
                viewAcc.Id = ac.Id;
                accaunts.Add(viewAcc);
            }
        }
        private void useres_Loaded(object sender, RoutedEventArgs e)
        {
            Useres.ItemsSource = accaunts;
        }

        private void Button_Click_delete_acc(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = int.Parse(button.Tag.ToString());
            foreach (var acc in currentUser.accaunts)
            {
                if (acc.Id == id)
                {
                    context.Clients.Find(currentUser.Id).accaunts.Remove(context.Accaunts.Find(acc.Id));
                    context.SaveChanges();
                    Button_Click_update(null, null);
                    Useres.ItemsSource = accaunts;

                    return;
                }
            }
        }

        private void Button_Click_СhangeToken(object sender, RoutedEventArgs e)
        {
            currentUser.key = GetPhoto.GenRandomString(10);
            context.Clients.Find(currentUser.Id).key = currentUser.key;
            context.Clients.Find(currentUser.Id).isActive = false;
            context.SaveChanges();
            token.Text = currentUser.key;
        }
      
    }
}
