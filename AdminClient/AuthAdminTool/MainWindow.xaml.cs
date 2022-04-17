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
using AdminAuth;

namespace AuthAdminTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AdminApi AdminApi = null;
        public MainWindow()
        {
            InitializeComponent();
            AdminApi = new AdminApi();
        }

        private Task checkAuthentication()
        {
            while (AdminApi.Authorized && AdminApi.HeartRate) ;

            MessageBox.Show("Auth Failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            return Task.CompletedTask;
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdminApi.login(userTextBox.Text, passTextBox.Text))
            {
                MessageBox.Show("Login Succeeded", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                loginResultText.Content = "Login: Success";
                Task.Run(() => checkAuthentication());
            }

            else
            {
                MessageBox.Show("Login Failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                loginResultText.Content = "Login: Failure";
            }
        }

        private void RedeemButton_Click(object sender, RoutedEventArgs e)
        {
            if(AdminApi.redeemKey(redeemKeyTextBox.Text))
            {
                MessageBox.Show("Key Succeeded", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                MessageBox.Show
                (
                    $"Years Left: {AdminApi.YearsLeft}" +
                    $"\nDays Left: {AdminApi.DaysLeft}" +
                    $"\nHours Left: {AdminApi.HoursLeft}" +
                    $"\nMinutes Left: {AdminApi.MinutesLeft}" +
                    $"\nSeconds Left: {AdminApi.SecondsLeft}",
                    "Time Left",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }

            else
            {
                MessageBox.Show("Key Failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            generateKeyTextBox.Text = "Debug";
        }
    }
}
