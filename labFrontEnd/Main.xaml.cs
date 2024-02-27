using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
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
using labFrontEnd.model;
using Lab3;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace labFrontEnd
{
    /// <summary>
    /// Логика взаимодействия для main.xaml
    /// </summary>
    public partial class main : Window
    {
        private HttpClient httpClient;
        //private MainWindow mainWindow;
        private string? token;
        private Response resp;
        private autorise autorise;

        public main(Response response, autorise window)
        {
            InitializeComponent();
            this.autorise = autorise;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + response.access_token);
            token = response.access_token;
            Task.Run(() => Load());
        }

        private async Task Load()
        {
            List<PRODUCT>? list = await httpClient.GetFromJsonAsync<List<PRODUCT>>("http://localhost:24734/PRODUCT");
            foreach (PRODUCT i in list!)
            {
                i.PriceListss = await httpClient.GetFromJsonAsync<model.PRICELIST>("http://localhost:24734/PRICELIST" + i.PriceListId);
            }
            //Dispatcher.Invoke(() =>
            //{
            //    ListStudents.ItemsSource = null;
            //    ListStudents.Items.Clear();
            //    ListStudents.ItemsSource = list;
            //});
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.autorise.Close();
        }
        private async Task Button_Click_2Async(object sender, RoutedEventArgs e)//save
        {
            StudentWindow studentWindow = new StudentWindow(token!);
            if (studentWindow.ShowDialog() == true)
            {
                PRODUCT student = new PRODUCT
                {
                    Id = studentWindow.IdProperty,
                    SaleDate = studentWindow.SaleDateProperty,
                    ProductSales = studentWindow.ProductSalesProperty,
                    Quantity = studentWindow.QuantityProperty,
                };
                JsonContent content = JsonContent.Create(student);
                using var response = await httpClient.PostAsync("http://localhost:5079/api/student", content);
                string responseText = await response.Content.ReadAsStringAsync();
                await Load();
            }
        }

        private async Task Button_Click_3Async(object sender, RoutedEventArgs e)//change
        {
            PRODUCT? st = ListStudents.SelectedItem as PRODUCT;
            StudentWindow studentWindow = new StudentWindow(token!, st!);
            if (studentWindow.ShowDialog() == true)
            {
                st!.Id = studentWindow.IdProperty;
                st!.SaleDate = studentWindow.SaleDateProperty;
                st!.ProductSales = studentWindow.ProductSalesProperty;
                st!.Quantity = studentWindow.QuantityhProperty;
                JsonContent content = JsonContent.Create(st);
                using var response = await httpClient.PutAsync("http://localhost:5079/api/student", content);
                string responseText = await response.Content.ReadAsStringAsync();
                await Load();
            }
        }

        private async Task Button_Click_4Async(object sender, RoutedEventArgs e)//delete
        {
            PRODUCT? st = ListStudents.SelectedItem as PRODUCT;
            JsonContent content = JsonContent.Create(st);
            using var response = await httpClient.DeleteAsync("http://localhost:5079/api/student/" + st!.Id);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
    }
}
