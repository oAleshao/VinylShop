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

        }


        //Окно регистрации
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

        //Вход пользователя
        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (CanSeeStackPanel.Visibility == Visibility.Visible && e.Key == Key.Enter)
                BorderCanSee_MouseDown(sender, helpE);
            if (e.Key == Key.Enter)
            {
                if (loginBox.Text == string.Empty || UserPasswordBox.Password == string.Empty)
                    return;
                using (db = new VinylShopContext())
                {
                    Admins admin = db.CheckAdminLogin(loginBox.Text);
                    if (admin != null)
                    {
                        if (admin.password != UserPasswordBox.Password)
                        {
                            UserPasswordBox.Foreground = Brushes.Red;
                            UserPasswordBox.ToolTip = "Пароль введен не верно";
                            return;
                        }
                        else
                        {
                            UserPasswordBox.Foreground = Brushes.Black;
                            UserPasswordBox.ToolTip = null;
                        }
                        OpenAdminWindow(admin);
                    }
                    else
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
                        if (users.Password != UserPasswordBox.Password)
                        {
                            UserPasswordBox.Foreground = Brushes.Red;
                            UserPasswordBox.ToolTip = "Пароль введен не верно";
                            return;
                        }
                        else
                        {
                            UserPasswordBox.Foreground = Brushes.Black;
                            UserPasswordBox.ToolTip = null;
                        }

                        users = db.users.Find(users.Id);

                        MainShopWindow mainShop = new MainShopWindow();
                        mainShop.SetUser(users);
                        this.Visibility = Visibility.Hidden;
                        var temp = mainShop.ShowDialog();
                        if (temp == false)
                        {
                            this.Visibility = Visibility.Visible;
                        }
                    }
                    loginBox.Text = string.Empty;
                    UserPasswordBox.Password = string.Empty;

                }
            }
        }




        private void OpenAdminWindow(Admins admin)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.SetAdmin(admin);
            this.Visibility = Visibility.Hidden;
            var temp = adminWindow.ShowDialog();
            if (temp == false)
            {
                this.Visibility = Visibility.Visible;
            }
        }

        MouseButtonEventArgs helpE;
        private void UserPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UserPassword.Text = UserPasswordBox.Password;
        }

        private void BorderCanNotSee_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CanNotSeeStackPanel.Visibility = Visibility.Hidden;
            CanSeeStackPanel.Visibility = Visibility.Visible;
            helpE = e;
        }
        private void BorderCanSee_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CanSeeStackPanel.Visibility = Visibility.Hidden;
            CanNotSeeStackPanel.Visibility = Visibility.Visible;
            UserPasswordBox.Password = UserPassword.Text;
        }

    }
}
