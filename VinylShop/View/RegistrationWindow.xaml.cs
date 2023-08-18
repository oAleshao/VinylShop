using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using VinylShop.Model;

namespace VinylShop.View
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        VinylShopContext db;

        DispatcherTimer timer = new DispatcherTimer();
        public RegistrationWindow()
        {
            InitializeComponent();
            ButReg.IsEnabled = false;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 2);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                Users users = new Users();
                users.FullName = NameTBox.Text;
                users.Email = EmailTBox.Text;
                users.Login = LoginTBox.Text;
                users.Password = PasswordTBox.Password;
                users.PhoneNumber = NumberTBox.Text;
                TextBlockRegSuc.Text = "Вы успешно зарегестрированы!";
                db.AddUser(users);
                ButReg.IsEnabled = false;
            }
            timer.Start();
        }


        private void TBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                if (db.CheckUserEmail(EmailTBox.Text))
                {
                    EmailTBox.Foreground = Brushes.Red;
                    EmailTBox.ToolTip = "Пользователь с такой почтой уже зарегистрирован";
                    return;
                }
                else
                {
                    EmailTBox.Foreground = Brushes.Black;
                    EmailTBox.ToolTip = null;
                }

                if (db.CheckUserLogin(LoginTBox.Text))
                {
                    LoginTBox.Foreground = Brushes.Red;
                    LoginTBox.ToolTip = "Пользователь с таким логином уже зарегистрирован";
                    return;
                }
                else
                {
                    LoginTBox.Foreground = Brushes.Black;
                    LoginTBox.ToolTip = null;
                }

                long help;
                if (db.CheckUserPhone(NumberTBox.Text))
                {
                    NumberTBox.Foreground = Brushes.Red;
                    NumberTBox.ToolTip = "Пользователь с таким номером уже зарегистрирован";
                    return;
                }
                else if (!Int64.TryParse(NumberTBox.Text.Trim('+'), out help) && NumberTBox.Text!=string.Empty)
                {
                    NumberTBox.Foreground = Brushes.Red;
                    NumberTBox.ToolTip = "Введен неверный формат номера";
                    return;
                }
                else
                {
                    NumberTBox.Foreground = Brushes.Black;
                    NumberTBox.ToolTip = null;
                }

                if (NameTBox.Text == string.Empty || EmailTBox.Text == string.Empty || LoginTBox.Text == string.Empty || NumberTBox.Text == string.Empty
                  || PasswordTBox.Password == string.Empty || RePasswordTBox.Password == string.Empty)
                {
                    ButReg.IsEnabled = false;
                    return;
                }
                else
                {
                    ButReg.IsEnabled = true;
                }
            }

        }
        

        private void PasswordTBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            RePasswordTBox.Foreground = Brushes.Black;
            if (PasswordTBox.Password == string.Empty || RePasswordTBox.Password == string.Empty)
                return;
                if (PasswordTBox.Password != RePasswordTBox.Password && RePasswordTBox.Password!= string.Empty)
            {
                ButReg.IsEnabled = false;
                RePasswordTBox.Foreground = Brushes.Red;

            }
            else
            {
                RePasswordTBox.Foreground = Brushes.Black;
                ButReg.IsEnabled = true;
            }
        }
    }
}
