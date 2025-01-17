using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace FetchDataApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Fetch Button Click Event
        private async void FetchButton_Click(object sender, RoutedEventArgs e)
        {
            string url = UrlTextBox.Text;

            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Please enter a valid URL.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    ResultTextBox.Text = "Fetching data...";
                    string content = await FetchDataAsync(client, url);
                    ResultTextBox.Text = content;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ResultTextBox.Text = "Error fetching data.";
            }
        }

        // Clear Button Click Event
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            UrlTextBox.Text = string.Empty;
            ResultTextBox.Text = string.Empty;
        }

        // Close Button Click Event
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Fetch Data Method
        private async Task<string> FetchDataAsync(HttpClient client, string url)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
