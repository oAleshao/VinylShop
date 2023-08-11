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
using System.Windows.Shapes;
using VinylShop.Model;

namespace VinylShop.View
{
    /// <summary>
    /// Interaction logic for AdminEnterWindow.xaml
    /// </summary>
    public partial class AdminEnterWindow : Window
    {
        VinylShopContext db = null;
        public Admins admins { get; set; }
        public AdminEnterWindow()
        {
            InitializeComponent();
            
        }

        private void LogAndPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (loginBox.Text == string.Empty || passwordBox.Password == string.Empty)
                    return;
                using (db = new VinylShopContext())
                {
                    admins = db.CheckAdminLogin(loginBox.Text);
                    if (admins == null)
                    {
                        loginBox.Foreground = Brushes.Red;
                        loginBox.ToolTip = "Такого админа не найдено";
                        return;
                    }
                    else
                    {
                        loginBox.Foreground = Brushes.Black;
                        loginBox.ToolTip = null;
                    }
                    if (admins.password != passwordBox.Password)
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
                    this.Close();
                }
            }
        }
    }
}
