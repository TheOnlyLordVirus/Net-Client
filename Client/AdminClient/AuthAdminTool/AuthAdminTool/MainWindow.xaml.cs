using System;
using System.Threading.Tasks;
using System.Windows;
using KeyAuthorization;
using FileConfig;

namespace AuthAdminTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProjectConfig ConfigFile;
        private AdminApi AdminApi = null;
        public MainWindow()
        {
            InitializeComponent();
            AdminApi = new AdminApi();
            ConfigFile = new ProjectConfig("cheatconfig", "userconfig", new string[] { "auth", "user", "pass" });

            if (ConfigFile["auth"].Equals("1"))
            {
                userTextBox.Text = ConfigFile["user"];
                passTextBox.Text = ConfigFile["pass"];
            }
        }

        private Task checkAuthentication()
        {
            while (AdminApi.Authorized) ;

            MessageBox.Show("Auth Failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            return Task.CompletedTask;
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ClientAuth.LoginState LoginState = AdminApi.Login(userTextBox.Text, passTextBox.Text);

            if (LoginState.Equals(ClientAuth.LoginState.Logged_In) || LoginState.Equals(ClientAuth.LoginState.Logged_In_Without_Time))
            {
                MessageBox.Show("Login Succeeded", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                loginResultText.Content = "Login: Success";

                ConfigFile["auth"] = "1";
                ConfigFile["user"] = userTextBox.Text;
                ConfigFile["pass"] = passTextBox.Text;

                Task.Run(() => checkAuthentication());

                userTextBox.IsEnabled = false;
                passTextBox.IsEnabled = false;
                loginButton.IsEnabled = false;
            }

            else
            {
                MessageBox.Show(LoginState.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                loginResultText.Content = "Login: Failure";
            }
        }

        private void RedeemButton_Click(object sender, RoutedEventArgs e)
        {
            if(AdminApi.RedeemKey(redeemKeyTextBox.Text, redeemKeyUserTextBox.Text))
            {
                MessageBox.Show("Key Succeeded", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                int seconds = AdminApi.GetTimeLeft(redeemKeyUserTextBox.Text);
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
                string key = AdminApi.GenerateKey(days);
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
            if(AdminApi.AddUser(createEmailTextBox.Text, createUserTextBox.Text, createPassTextBox.Text, adminCheckBox.IsChecked.Equals(true) ? true : false ))
            {
                MessageBox.Show("Account created successufuly!", "Account Created!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            else
            {
                MessageBox.Show("Account creation failed!", "Oh snap!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BanUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdminApi.BanUser(deleteUserTextBox.Text))
            {
                MessageBox.Show("Account banned successufuly!", "Account Created!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            else
            {
                MessageBox.Show("Account ban failed!", "Oh snap!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void getUserTimeButton_Click(object sender, RoutedEventArgs e)
        {
            int seconds = AdminApi.GetTimeLeft(getUserTimeTextBox.Text);
            DateTime UTC = DateTime.UtcNow.AddSeconds(seconds);
            DateTime LocalTime = UTC.ToLocalTime();

            if (!seconds.Equals(0))
            {
                MessageBox.Show
                (
                    $"Years Left: {(int)Math.Floor((decimal)((seconds / 31556952)))}" +
                    $"\nMonths Left: {(int)Math.Floor((decimal)((seconds % 31556952) / 2592000))}" +
                    $"\nDays Left: {(int)Math.Floor((decimal)((seconds % 31556952) / 86400))}" +
                    $"\nHours Left: {(int)Math.Floor((decimal)((seconds % 31556952) / 3600))}" +
                    $"\nMinutes Left: {(int)Math.Floor((decimal)((seconds % 31556952) / 60))}" +
                    $"\nEnd date (UTC): {UTC}" +
                    $"\nEnd date (LocalTime): {LocalTime}",
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
                string keys = AdminApi.GenerateKey(days, amount);
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
