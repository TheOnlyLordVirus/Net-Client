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
        private ClientAuth ClientAuthenticator;

        /// <summary>
        /// Our current login state.
        /// </summary>
        private ClientAuth.LoginState LoginState = ClientAuth.LoginState.Not_logged_In;

        /// <summary>
        /// Games we have cheats for.
        /// </summary>
        private List<ClientAuth.CheatItems> cheats;

        /// <summary>
        /// The current cheat form opened up.
        /// </summary>
        private Window cheatForm;

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
                    GameCheatTab.IsEnabled = false;
                    RegisterTab.IsEnabled = true;
                    LoginButton.IsEnabled = true;
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
        /// If the client is not authorized with time left, disable the Time Tab and Game Tab
        /// </summary>
        /// <returns></returns>
        private Task checkAuthTime()
        {
            while (ClientAuthenticator.Authorized)
            {
                if (ClientAuthenticator.Authorized && !ClientAuthenticator.AuthorizedWithTimeLeft)
                {
                    instance.Dispatcher.Invoke
                    (
                        DispatcherPriority.Normal,
                        (ThreadStart)delegate
                        {
                            MainTab.SelectedItem = LoginTab;
                            TimeTab.IsEnabled = false;
                            GameCheatTab.IsEnabled = false;
                            //cheatForm.Close();
                        }
                    );


                    MessageBox.Show("Your out of time!", "Notice!", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                }

                Thread.Sleep(1000);
            }
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

            else
            {
                MainTab.SelectedItem = RegisterTab;
            }


            Client_Version_Label.Content = $"Cheat Client v{Assembly.GetExecutingAssembly().GetName().Version}";
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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ClientAuthenticator = new ClientAuth();
            LoginState = ClientAuthenticator.Login(LoginUser.Text, LoginPassword.Password, "x64");

            if (LoginState.Equals(ClientAuth.LoginState.Logged_In))
            {
                ConfigFile["auth"] = "1";
                ConfigFile["user"] = LoginUser.Text;
                ConfigFile["pass"] = LoginPassword.Password;

                TimeTab.IsEnabled = true;
                RedeemKeyTab.IsEnabled = true;
                GameCheatTab.IsEnabled = true;
                LoginButton.IsEnabled = false;
                RegisterTab.IsEnabled = false;
                MainTab.SelectedItem = GameCheatTab;

                Task.Run(() => checkAuthentication()); // General Auth Check.
                Task.Run(() => checkAuthTime()); // Check that the user is authorized with time left.

                LoadCheats();

                MessageBox.Show($"Logged in!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            else if (LoginState.Equals(ClientAuth.LoginState.Logged_In_Without_Time))
            {
                ConfigFile["auth"] = "1";
                ConfigFile["user"] = LoginUser.Text;
                ConfigFile["pass"] = LoginPassword.Password;

                RedeemKeyTab.IsEnabled = true;
                RegisterTab.IsEnabled = false;
                LoginButton.IsEnabled = false;
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

                MessageBox.Show("Server Response failure!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            ClientAuthenticator = new ClientAuth();

            if (ClientAuthenticator.RegisterUser(RegisterEmail.Text, RegisterUsername.Text, RegisterPassword.Password))
            {
                ConfigFile["auth"] = "1";
                ConfigFile["user"] = RegisterUsername.Text;
                ConfigFile["pass"] = RegisterPassword.Password;

                MainTab.SelectedItem = RedeemKeyTab;

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
                    KeyServerFlag = ClientAuthenticator.RedeemKey($"{KeyInputBox1.Text}-{KeyInputBox2.Text}-{KeyInputBox3.Text}-{KeyInputBox4.Text}");
                }

                else
                {
                    KeyServerFlag = ClientAuthenticator.RedeemKey($"{KeyInputFullBox.Text}");
                }
                #pragma warning restore CS8629 // Nullable value type may be null.

                if (KeyServerFlag)
                {
                    LoginState = ClientAuth.LoginState.Logged_In;
                    Task.Run(() => checkAuthTime());

                    TimeTab.IsEnabled = true;
                    RedeemKeyTab.IsEnabled = true;
                    GameCheatTab.IsEnabled = true;
                    LoginButton.IsEnabled = false;
                    MainTab.SelectedItem = GameCheatTab;

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
        /// Loads the cheats to the tile controll
        /// </summary>
        private void LoadCheats()
        {
            if (ClientAuthenticator.GameCheats != null)
            {
                cheats = ClientAuthenticator.GameCheats;

                for (int iItem = 0; iItem < cheats.Count; iItem++)
                {
                    Button button = new Button()
                    {
                        Content = cheats[iItem].cheatname,
                        Width = 100,
                        Height = 50
                    };

                    button.Click += new RoutedEventHandler(CheatButton_Click);

                    CheatPanel.Children.Add(button);
                }
            }
        }

        private void CheatButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string gameName = button.Content.ToString();
            ClientAuth.ToolConfig cheatFiles = ClientAuthenticator.DownloadCheat($"{ResolveName(gameName)}");

            try
            {
                if(cheatForm != null)
                {
                    cheatForm.Close();
                }

                Assembly asm = Assembly.Load(cheatFiles.dll);
                cheatForm = Activator.CreateInstance(asm.GetType($"{ResolveClass(gameName)}")) as Window;
                cheatForm.Show();
            }

            catch (Exception Ex)
            {
                MessageBox.Show("Failed to load cheat!\n" + Ex.Message, gameName, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Get the game cheat uri name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string ResolveName(string name)
        {
            for (int iCheat = 0; iCheat < cheats.Count; iCheat++)
            {
                if (name == cheats[iCheat].cheatname)
                {
                    return cheats[iCheat].shortname;
                }
            }
            return string.Empty;
        }


        /// <summary>
        /// Get the game cheat class name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string ResolveClass(string name)
        {
            for (int iCheat = 0; iCheat < cheats.Count; iCheat++)
            {
                if (name == cheats[iCheat].cheatname)
                {
                    return cheats[iCheat].classname;
                }
            }
            return string.Empty;
        }
    }
}
