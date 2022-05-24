using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using KeyAuthorization;
using FileConfig;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Windows.Threading;

namespace CheatClientWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Keep running background time check?
        /// </summary>
        private bool timeUpdates = false;

        /// <summary>
        /// C:\Users\{USER}\AppData\Local\cheatconfig\userconfig.ini
        /// </summary>
        private ProjectConfigFile ConfigFile;

        /// <summary>
        /// Cheat Authentication Api
        /// </summary>
        private AdminApi ClientAuthenticator;

        /// <summary>
        /// Our current login state.
        /// </summary>
        private ClientAuth.LoginState LoginState = ClientAuth.LoginState.Not_logged_In;

        /// <summary>
        /// Create singleton.
        /// </summary>
        private static MainWindow instance;

        /// <summary>
        /// Show the client an error if we fail auth.
        /// </summary>
        /// <returns></returns>
        private Task checkAuthentication()
        {
            while (ClientAuthenticator.Authorized) Thread.Sleep(1000);

            instance.Dispatcher.Invoke
            (
                DispatcherPriority.Normal,
                (ThreadStart)delegate
                {
                    TimeTab.IsEnabled = false;
                    RedeemKeyTab.IsEnabled = false;
                    KeyGenTab.IsEnabled = false;
                    CreateUserTab.IsEnabled = false;
                    LoginButton.IsEnabled = false;
                }
            );

            new Task(new Action(() =>
            {
                Thread.Sleep(5000);
                Process.GetCurrentProcess().Kill();
            })).Start();

            MessageBox.Show("Authentication to server failed!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Process.GetCurrentProcess().Kill();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Main Window Constructor
        /// </summary>
        public MainWindow()
        {
            if (Directory.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\dnSpy\\") || Directory.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\dnSpy\\"))
            {
                Process.GetCurrentProcess().Kill();
            }

            MainWindow.instance = this;
            InitializeComponent();

            ConfigFile = new ProjectConfigFile("cheatconfig", "userconfig", new string[] { "auth", "user", "pass" });

            if (ConfigFile["auth"].Equals("1"))
            {
                LoginUser.Text = ConfigFile["user"];
                LoginPassword.Password = ConfigFile["pass"];
            }

            Client_Version_Label.Content = $"Admin Client v{Assembly.GetExecutingAssembly().GetName().Version}";
        }

        /// <summary>
        /// Move window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                instance.DragMove();
        }

        /// <summary>
        /// Close form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseForm_Click(object sender, RoutedEventArgs e)
        {
            instance.Close();
        }


        /// <summary>
        /// Minimize form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinimizeForm_Click(object sender, RoutedEventArgs e)
        {
            instance.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ClientAuthenticator = new AdminApi();
            LoginState = ClientAuthenticator.Login(LoginUser.Text, LoginPassword.Password, "x64");

            if (LoginState.Equals(ClientAuth.LoginState.Logged_In))
            {
                ConfigFile["auth"] = "1";
                ConfigFile["user"] = LoginUser.Text;
                ConfigFile["pass"] = LoginPassword.Password;

                TimeTab.IsEnabled = true;
                RedeemKeyTab.IsEnabled = true;
                KeyGenTab.IsEnabled = true;
                LoginButton.IsEnabled = false;
                CreateUserTab.IsEnabled = true;
                MainTab.SelectedItem = KeyGenTab;

                Task.Run(() => checkAuthentication()); // General Auth Check.

                MessageBox.Show($"Logged in!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            else if (LoginState.Equals(ClientAuth.LoginState.Logged_In_Without_Time))
            {
                ConfigFile["auth"] = "1";
                ConfigFile["user"] = LoginUser.Text;
                ConfigFile["pass"] = LoginPassword.Password;

                TimeTab.IsEnabled = true;
                RedeemKeyTab.IsEnabled = true;
                KeyGenTab.IsEnabled = true;
                LoginButton.IsEnabled = false;
                CreateUserTab.IsEnabled = true;
                MainTab.SelectedItem = RedeemKeyTab;

                Task.Run(() => checkAuthentication());

                MessageBox.Show($"Logged in!\nNotice: You're out of time!", "Notice!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            else if (LoginState.Equals(ClientAuth.LoginState.Password_Failure))
            {
                MessageBox.Show("Password Mismatch failure!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (LoginState.Equals(ClientAuth.LoginState.User_doesnt_Exist))
            {
                MessageBox.Show("User doesnt exist!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (LoginState.Equals(ClientAuth.LoginState.IP_Mismatch))
            {
                new Task(new Action(() =>
                {
                    Thread.Sleep(5000);
                    Process.GetCurrentProcess().Kill();
                })).Start();

                MessageBox.Show("User IP Address Mismatch failure!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Process.GetCurrentProcess().Kill();
            }

            else if (LoginState.Equals(ClientAuth.LoginState.Response_Error))
            {
                new Task(new Action(() =>
                {
                    Thread.Sleep(5000);
                    Process.GetCurrentProcess().Kill();
                })).Start();

                MessageBox.Show("Server Response failure!\n(You're Probably not and Administrator)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Process.GetCurrentProcess().Kill();
            }

            else if (LoginState.Equals(ClientAuth.LoginState.User_Banned))
            {
                new Task(new Action(() =>
                {
                    Thread.Sleep(5000);
                    Process.GetCurrentProcess().Kill();
                })).Start();

                MessageBox.Show("We don't like you, go away.", "Banned", MessageBoxButton.OK, MessageBoxImage.Error);
                Process.GetCurrentProcess().Kill();
            }
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientAuthenticator.AddUser(RegisterEmail.Text, RegisterUsername.Text, RegisterPassword.Password, (bool)adminCheckBox.IsChecked))
            {
                MessageBox.Show("User Registered!", "Register User", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            else
            {
                MessageBox.Show("User Registy failed!", "Register User", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Detects if we are in the time tab, runs a thread to update times in the background.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainTab.SelectedIndex == 3)
            {
                timeUpdates = true;
                Task.Run(() => updateTimesEveryHalfMinute());
            }

            else
            {
                if (!TimeCounterLabel.Text.Equals("Loading Times..."))
                {
                    TimeCounterLabel.Text = "Loading Times...";
                }

                timeUpdates = false;
            }
        }

        /// <summary>
        /// Update the time every minute.
        /// </summary>
        /// <returns></returns>
        private Task updateTimesEveryHalfMinute()
        {
            while (ClientAuthenticator.Authorized && timeUpdates)
            {
                EndDateLabel.Dispatcher.Invoke
                (
                    DispatcherPriority.Normal,
                    (ThreadStart)delegate
                    {
                        EndDateLabel.Text = "End Date Time: " + ClientAuthenticator.TimeLeft.ToLocalTime().ToString();
                    }
                );

                int iYears = ClientAuthenticator.YearsLeft;
                int iMonths = ClientAuthenticator.MonthsLeft;
                int iDays = ClientAuthenticator.DaysLeft;
                int iHours = ClientAuthenticator.HoursLeft;
                int iMinutes = ClientAuthenticator.MinutesLeft;

                string Years = !iYears.Equals(0) ? ($"{ClientAuthenticator.YearsLeft} " + (iYears.Equals(1) ? "Year, " : "Years, ")) : string.Empty;
                string Months = !iMonths.Equals(0) ? ($"{ClientAuthenticator.MonthsLeft} " + (iMonths.Equals(1) ? "Month, " : "Months, ")) : string.Empty;
                string Days = !iDays.Equals(0) ? ($"{ClientAuthenticator.DaysLeft} " + (iDays.Equals(1) ? "Day, " : "Days, ")) : string.Empty;
                string Hours = !iHours.Equals(0) ? ($"{ClientAuthenticator.HoursLeft} " + (iHours.Equals(1) ? "Hour, " : "Hours, ")) : string.Empty;
                string Minutes = !iMinutes.Equals(0) ? ($"{ClientAuthenticator.MinutesLeft} " + (iMinutes.Equals(1) ? "Minute" : "Minutes")) : string.Empty;

                TimeCounterLabel.Dispatcher.Invoke
                (
                    DispatcherPriority.Normal,
                    (ThreadStart)delegate
                    {
                        TimeCounterLabel.Text = Years + Months + Days + Hours + Minutes;
                    }
                );

                Thread.Sleep(30000);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Redeem a key.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RedeemKeyButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientAuthenticator.Authorized)
            {
                bool KeyServerFlag = false;

                #pragma warning disable CS8629 // Nullable value type may be null.
                if ((bool)KeyCheckBox.IsChecked)
                {
                    KeyServerFlag = ClientAuthenticator.RedeemKey($"{KeyInputBox1.Text}-{KeyInputBox2.Text}-{KeyInputBox3.Text}-{KeyInputBox4.Text}", redeemKeyUserTextBox.Text);
                }

                else
                {
                    KeyServerFlag = ClientAuthenticator.RedeemKey($"{KeyInputFullBox.Text}", redeemKeyUserTextBox.Text);
                }
                #pragma warning restore CS8629 // Nullable value type may be null.

                if (KeyServerFlag)
                {
                    LoginState = ClientAuth.LoginState.Logged_In;

                    MessageBox.Show("Key Redeemed Sucessfully!", "Redeem Key", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                else
                {
                    MessageBox.Show("Failed to Redeem key.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            else
            {
                MessageBox.Show("User isn't authorized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// <summary>
        /// Change key input type.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            KeyInputFullBox.Visibility = Visibility.Hidden;

            KeyInputBox1.Visibility = Visibility.Visible;
            KeyInputBox2.Visibility = Visibility.Visible;
            KeyInputBox3.Visibility = Visibility.Visible;
            KeyInputBox4.Visibility = Visibility.Visible;

            Hyphen1.Visibility = Visibility.Visible;
            Hyphen2.Visibility = Visibility.Visible;
            Hyphen3.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Change key input type.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            KeyInputFullBox.Visibility = Visibility.Visible;

            KeyInputBox1.Visibility = Visibility.Hidden;
            KeyInputBox2.Visibility = Visibility.Hidden;
            KeyInputBox3.Visibility = Visibility.Hidden;
            KeyInputBox4.Visibility = Visibility.Hidden;

            Hyphen1.Visibility = Visibility.Hidden;
            Hyphen2.Visibility = Visibility.Hidden;
            Hyphen3.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Generates keys.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateBulkButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(keyDayValueBulkTextBox.Text, out int days) &&
                int.TryParse(keyBulkAmountTextBox.Text, out int amount))
            {
                string keys = ClientAuthenticator.GenerateKey(days, amount);
                if (!keys.Equals(string.Empty))
                {
                    string[] keyArray = keys.Split('|');
                    for (int iKey = 0; iKey < keyArray.Length; iKey++)
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


        /// <summary>
        /// Gets the time for a user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkTimeButton_Click(object sender, RoutedEventArgs e)
        {
            int seconds = ClientAuthenticator.GetTimeLeft(userTimeInput.Text);
            DateTime UTC = DateTime.UtcNow.AddSeconds(seconds);
            DateTime LocalTime = UTC.ToLocalTime();

            if (!seconds.Equals(0))
            {
                MessageBox.Show
                (
                    $"Years Left: {(seconds / 31556952)}" +
                    $"\nMonths Left: {(seconds % 31556952) / 2592000}" +
                    $"\nDays Left: {(seconds % 2592000) / 86400}" +
                    $"\nHours Left: {(seconds % 86400) / 3600}" +
                    $"\nMinutes Left: {(seconds % 3600) / 60}" +
                    $"\nSeconds Left: {seconds}" +
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

        /// <summary>
        /// Ban a user by name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BanUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientAuthenticator.BanUser(UserOverrideInput.Text))
            {
                MessageBox.Show("Account banned successufuly!", "Banned!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            else
            {
                MessageBox.Show("Account ban failed!", "Oh snap!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void resetUserIpButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientAuthenticator.ResetIpUser(UserOverrideInput.Text))
            {
                MessageBox.Show("Account ip reset successufuly!", "Ip Reset!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            else
            {
                MessageBox.Show("Account ip reset failed!", "Oh snap!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
