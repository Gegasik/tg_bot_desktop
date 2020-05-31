using Newtonsoft.Json;
using Rattler.Controller;
using Rattler.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Rattler
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private VkApi vk;
        private UserSearchParams searchParam;
        private VkNet.Enums.Sex sex;
        private string country = "";
        private string city = "";
        BindingList<User> searchUsers;
        MyDbContext context = new MyDbContext();
        int clientId;


        public AddWindow(Client user)
        {
            clientId = user.Id;
            InitializeComponent();
            var api = new VkApi();
            context = new MyDbContext();

            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 123456,
                Login = "375298495779",
                Password = "prostobarni2000",
                Settings = Settings.All
            });
            searchParam = new UserSearchParams();
            vk = api;
            searchUsers = new BindingList<User>();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int cityId = 0;
            var pairs = JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(@"C:\Users\prost\source\repos\Curs_wpf\city.json"));
            if (pairs.ContainsKey(city))
            {
                pairs.TryGetValue(city, out cityId);
                searchParam.City = cityId;
            }
            else searchParam.City = 0;

            var pairs2 = JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(@"C:\Users\prost\source\repos\Curs_wpf\country.json"));
            int keyCountry = 0;
            if (pairs2.ContainsKey(country))
            {
                pairs.TryGetValue(country, out keyCountry);
                searchParam.Country = keyCountry;
            }
            else searchParam.Country = 0;
            searchParam.Count = 20;
            searchUsers.Clear();
            var users = vk.Users.Search(searchParam);
            foreach (var user in users)
            {
                user.About = GetPhoto.PhotoById(user.Id);
                searchUsers.Add(user);

            }
            if (searchUsers.Count == 0) { MessageBox.Show("нет результатов поиска, проверьте данные"); }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            if (pressed.Content.ToString().ToLower().Contains("муж"))
            {
                sex = VkNet.Enums.Sex.Male;
            }
            if (pressed.Content.ToString().ToLower().Contains("жен"))
            {
                sex = VkNet.Enums.Sex.Female;
            }
            searchParam.Sex = sex;
        }

        private void NameUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            searchParam.Query = textBox.Text;
        }

        private void Country_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            country = textBox.Text;

        }

        private void City_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            city = textBox.Text;

        }



        private void useres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void useres_Loaded(object sender, RoutedEventArgs e)
        {
            Useres.ItemsSource = searchUsers;
        }

        private void Button_Click_AddAcc(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button button = (System.Windows.Controls.Button)sender;
            Accaunts accaunts = new Accaunts();
            accaunts.Clients.Add(context.Clients.Find(clientId));
            accaunts.link = button.Tag.ToString();
            foreach (var it in context.Clients.Find(clientId).accaunts)
            {
                if (it.link.Equals(accaunts.link))
                {
                    MessageBox.Show("пользователь уже отслеживается"); return;
                }
            }
            if (context.Clients.Find(clientId).accaunts.Count() > context.Clients.Find(clientId).maxCount) { MessageBox.Show("превышен лемит подписок"); return; }
            context.Accaunts.Add(accaunts);
            context.SaveChanges();
        }
    }
}
