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
using VinylShop.Model;
using VinylShop.View;

namespace VinylShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VinylShopContext db = null;
        public MainWindow()
        {
            InitializeComponent();
            AdminWindow adminWindow = new AdminWindow();
            var temp = adminWindow.ShowDialog();
        }


        private void labelTest_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RegistrationWindow registration = new RegistrationWindow();
            this.Visibility = Visibility.Hidden;
            var temp = registration.ShowDialog();
            if (temp == false)
            {
                this.Visibility = Visibility.Visible;
            }
        }

        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (loginBox.Text == string.Empty || passwordBox.Password == string.Empty)
                    return;
                using (db = new VinylShopContext())
                {
                    Users users = db.CheckUserLogAndEmail(loginBox.Text);
                    if (users == null)
                    {
                        loginBox.Foreground = Brushes.Red;
                        loginBox.ToolTip = "Такого пользователя не найдено";
                        return;
                    }
                    else
                    {
                        loginBox.Foreground = Brushes.Black;
                        loginBox.ToolTip = null;
                    }
                    if (users.Password != passwordBox.Password)
                    {
                        passwordBox.Foreground = Brushes.Red;
                        passwordBox.ToolTip = "Пароль введен не верно";
                        return;
                    }
                    else
                    {
                        passwordBox.Foreground = Brushes.Black;
                        passwordBox.ToolTip = null;
                    }

                    MainShopWindow mainShop = new MainShopWindow();
                    mainShop.ShowDialog();

                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            this.Visibility = Visibility.Hidden;
            var temp = adminWindow.ShowDialog();
            if (temp == false)
            {
                this.Visibility = Visibility.Visible;
            }
        }
    }
}
