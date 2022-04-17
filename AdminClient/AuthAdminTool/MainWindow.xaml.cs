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


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdminApi.login(userTextBox.Text, passTextBox.Text))
            {
                loginResultText.Content = "Login: Success";
            }

            else
            {
                loginResultText.Content = "Login: Failure";
            }
        }

        private Task checkAuthentication()
        {
            while (AdminApi.Authorized && AdminApi.HeartRate);

            MessageBox.Show("Auth Failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            return Task.CompletedTask;
        }
    }
}
