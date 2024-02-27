using Lab3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace labFrontEnd
{
    /// <summary>
    /// Логика взаимодействия для autorise.xaml
    /// </summary>
    public partial class autorise : Window
    {
        private HttpClient client;
        public autorise()
        {
            InitializeComponent();
            client = new HttpClient();

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

                User user = new User { EMail = Login.Text, Password = Password.Text };
                JsonContent content = JsonContent.Create(user);
                using var response = await client.PostAsync("http://localhost:24734/login", content);
                string responseText = await response.Content.ReadAsStringAsync();
                model.Response? resp = JsonSerializer.Deserialize < model.Response>(responseText);
                if (resp != null)
                {
                    main main = new main(resp,this);
                    main.Show();
                    this.Hide();
                }
            
        }
    }
}
