using ApiClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
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

namespace ApiClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient client;
        private User? user;
        public MainWindow()
        {
            InitializeComponent();
            client = new HttpClient();
            Task.Run(() => Load());
        }

        private async Task Load()
        {
            List<User>? list = await client.GetFromJsonAsync<List<User>>("http://localhost:5201/api/users");
            Dispatcher.Invoke(() =>
            {
                ListUsers.ItemsSource = null; ListUsers.Items.Clear();
                ListUsers.ItemsSource = list;
            });
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Save();
        }
        private async Task Save()
        {
            User user = new User(NameUser.Text, int.Parse(AgeUser.Text));
            JsonContent content=JsonContent.Create(user);
            using var response = await client.PostAsync("http://localhost:5201/api/users",content);
            string responseText=await response.Content.ReadAsStringAsync();
            await Load();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await Edit();
        }

        private async Task Edit()
        {
            user!.Name= NameUser.Text;
            user!.Age=int.Parse(AgeUser.Text);
            JsonContent content=JsonContent.Create(user);
            using var response = await client.PutAsync("http://localhost:5201/api/users", content);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
        private void ListUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            user = ListUsers.SelectedItem as User;
            NameUser.Text = user?.Name;
            AgeUser.Text=user?.Age.ToString();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            await Delete();
        }
        private async Task Delete()
        {
            using var response = await client.DeleteAsync("http://localhost:5201/api/users/"+user?.Id);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
    }
}
