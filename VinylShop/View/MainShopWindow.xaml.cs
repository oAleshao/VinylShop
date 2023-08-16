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
    /// Interaction logic for MainShopWindow.xaml
    /// </summary>
    public partial class MainShopWindow : Window
    {
        VinylShopContext db = null;
        public Users users {  get; set; }
        
        
        int BorderBackHelp = 0; 
        /// 
        /// BorderBackHelp бордек как кнопка которая возвращает на предыдущую панель
        /// 0 - главное окно
        /// 1 - характеристики пластинок
        /// 2 - найсторки пользователя
        /// 

        public MainShopWindow()
        {
            InitializeComponent();
            using(db = new VinylShopContext())
            {
                AllMusicRecordsInShopListBox.ItemsSource = db.GetListMusic_Records();
            }
           
        }

        public void SetUser(Users user)
        {
            using (db = new VinylShopContext())
            {
                MessageBox.Show("WORK");
                users = db.users.Find(user.Id);
                UsersLoginTextBlock.Text = users.Login;
                FullNameTextBox.Text = users.FullName;
                EmailTextBox.Text = users.Email;
                PhoneTextBox.Text = users.PhoneNumber;
                AdressTextBox.Text = users.Adres;
                LoginTextBox.Text = users.Login;
                UserPasswordBox.Password = users.Password;
            }
        }

        int indxHelpRecord = 0;
        private void AllMusicRecordsInShopListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BorderBackHelp = 1;
            using (db = new VinylShopContext())
            {
                Music_Records music_Records = (Music_Records)AllMusicRecordsInShopListBox.SelectedItem;
                if(music_Records != null)
                {
                    AllMusicRecordsStackPanel.Visibility = Visibility.Hidden;
                    UserChoosedMusicRecordsStackPanel.Visibility = Visibility.Visible;
                    indxHelpRecord = music_Records.Id;
                    music_Records = db.music_Records.Find(music_Records.Id);
                    NameRecordTextBox.Text = music_Records.Name;
                    PublishHouseNameTextBox.Text = music_Records.pubishHouse.Name;
                    CountSongsTextBox.Text = "Кол-во треков: " + music_Records.CountSongs.ToString();
                    PriceTextBox.Text = "Цена: " + music_Records.Price.ToString();
                    YearTextBox.Text =  "Год: " + music_Records.Year.ToString();
                    DescriptionTextBox.Text = music_Records.Description.ToString();
                    music_Records.songs = db.GetListSongs(music_Records.songs.ToList());
                    ListSongs.ItemsSource = music_Records.songs;
                }

            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (BorderBackHelp == 0)
                return;

            IsAllStackPanelVisibilityFalse();
            if (BorderBackHelp == 1)
            {
                BorderBackHelp = 0;
                AllMusicRecordsStackPanel.Visibility= Visibility.Visible;
                NameRecordTextBox.Text = string.Empty;
                PublishHouseNameTextBox.Text = string.Empty;
                CountSongsTextBox.Text = string.Empty;
                PriceTextBox.Text = string.Empty; 
                YearTextBox.Text = string.Empty;
                DescriptionTextBox.Text = string.Empty;
            }
            else if(BorderBackHelp == 2)
            {
                AllMusicRecordsStackPanel.Visibility = Visibility.Visible;
            }
        }

        public void IsAllStackPanelVisibilityFalse()
        {
            UserChoosedMusicRecordsStackPanel.Visibility = Visibility.Hidden;
            AllMusicRecordsStackPanel.Visibility = Visibility.Hidden;
            UserSettingsStackPanel.Visibility = Visibility.Hidden;
        }


        private void BorderCanNotSee_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CanNotSeeStackPanel.Visibility = Visibility.Hidden;
            CanSeeStackPanel.Visibility = Visibility.Visible;
            UserPassword.Text = UserPasswordBox.Password;
        }
        private void BorderCanSee_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CanSeeStackPanel.Visibility = Visibility.Hidden;
            CanNotSeeStackPanel.Visibility = Visibility.Visible;
            if (UserPasswordBox.Password != users.Password && UserPassword.Text == users.Password)
            {
                UserPassword.Text = UserPasswordBox.Password;
            }
            else
            {
                UserPasswordBox.Password = UserPassword.Text;
            }

        }


        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Settings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IsAllStackPanelVisibilityFalse();
            BorderBackHelp = 2;
            UserSettingsStackPanel.Visibility = Visibility.Visible;

        }
    }
}
