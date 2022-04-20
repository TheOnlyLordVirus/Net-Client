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
            if(AdminApi.redeemKey(redeemKeyTextBox.Text, redeemKeyUserTextBox.Text))
            {
                MessageBox.Show("Key Succeeded", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                int seconds = AdminApi.getTimeLeft(redeemKeyUserTextBox.Text);
                if (!seconds.Equals(0))
                {
                    MessageBox.Show
                    (
                        $"Years Left: {TimeSpan.FromSeconds(Convert.ToDouble(seconds)).Days / 365}" +
                        $"\nDays Left: {TimeSpan.FromSeconds(Convert.ToDouble(seconds)).Days}" +
                        $"\nHours Left: {TimeSpan.FromSeconds(Convert.ToDouble(seconds)).Hours}" +
                        $"\nMinutes Left: {TimeSpan.FromSeconds(Convert.ToDouble(seconds)).Minutes}" +
                        $"\nSeconds Left: {seconds}",
                        "Time Left",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                }
            }

            else
            {
                MessageBox.Show("Key Failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {

            if(int.TryParse(keyDayValueTextBox.Text, out int days))
            {
                string key = AdminApi.generateKey(days);
                if (!key.Equals(string.Empty))
                {
                    generateKeyTextBox.Text = key;
                }

                else
                {
                    generateKeyTextBox.Text = "Key gen error.";
                }
            }

            else
            {
                MessageBox.Show("You must enter the amount of days as an int!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            if(AdminApi.addUser(createEmailTextBox.Text, createUserTextBox.Text, createPassTextBox.Text, adminCheckBox.IsChecked.Equals(true) ? true : false ))
            {
                MessageBox.Show("Account created successufuly!", "Account Created!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            else
            {
                MessageBox.Show("Account creation failed!", "Oh snap!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdminApi.deleteUser(deleteUserTextBox.Text))
            {
                MessageBox.Show("Account deleted successufuly!", "Account Created!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            else
            {
                MessageBox.Show("Account deletion failed!", "Oh snap!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void getUserTimeButton_Click(object sender, RoutedEventArgs e)
        {
            int seconds = AdminApi.getTimeLeft(getUserTimeTextBox.Text);
            if (!seconds.Equals(0))
            {
                MessageBox.Show
                (
                    $"Years Left: {TimeSpan.FromSeconds(Convert.ToDouble(seconds)).Days / 365}" +
                    $"\nDays Left: {TimeSpan.FromSeconds(Convert.ToDouble(seconds)).Days}" +
                    $"\nHours Left: {TimeSpan.FromSeconds(Convert.ToDouble(seconds)).Hours}" +
                    $"\nMinutes Left: {TimeSpan.FromSeconds(Convert.ToDouble(seconds)).Minutes}" +
                    $"\nSeconds Left: {seconds}",
                    "Time Left",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }

            else
            {
                MessageBox.Show
                (
                    $"Years Left: 0" +
                    $"\nDays Left: 0" +
                    $"\nHours Left: 0" +
                    $"\nMinutes Left: 0" +
                    $"\nSeconds Left: 0",
                    "Time Left",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }
        }

        private void GenerateBulkButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(keyDayValueBulkTextBox.Text, out int days) &&
                int.TryParse(keyBulkAmountTextBox.Text, out int amount))
            {
                string keys = AdminApi.generateKey(days, amount);
                if (!keys.Equals(string.Empty))
                {
                    string[] keyArray = keys.Split('|');
                    for (int iKey = 0;iKey < keyArray.Length;iKey++)
                    {
                        bulkKeyGenRichTextBox.AppendText($"{keyArray[iKey]}\n");
                    }
                    System.IO.File.WriteAllText($"{AppDomain.CurrentDomain.BaseDirectory}\\bulk_keys.txt", bulkKeyGenRichTextBox.Text);
                }

                else
                {
                    bulkKeyGenRichTextBox.AppendText("Key gen error.");
                }
            }

            else
            {
                MessageBox.Show("You must enter the amount of days as an int!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
