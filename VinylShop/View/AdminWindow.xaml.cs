using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        VinylShopContext db = null;
        public Admins admin { get; set; }
        public object PublishHouse { get; private set; }

        public AdminWindow()
        {
            InitializeComponent();
            AddComboBoxItems();
            IsVisibilityFalseAllStackPanel();
            BackgroundFone.Visibility = Visibility.Visible;
        }

        //Прячет все стек панели 
        private void IsVisibilityFalseAllStackPanel()
        {
            BackgroundFone.Visibility = Visibility.Hidden;
            AddMusicRecordStackPanel.Visibility = Visibility.Hidden;
            AddSongStackPanel.Visibility = Visibility.Hidden;
            AddGanreStackPanel.Visibility = Visibility.Hidden;
            AddExecutorStackPanel.Visibility = Visibility.Hidden;
            AddPublishHouseStackPanel.Visibility = Visibility.Hidden;
            AddOrDelStackPanel.Visibility = Visibility.Hidden;
            EditMusicRecordStackPanel.Visibility = Visibility.Hidden;
            ComboBoxExecutorStackPanel.Visibility = Visibility.Hidden;
            ComboBoxGanreStackPanel.Visibility = Visibility.Hidden;
            EditSongsStackPanel.Visibility = Visibility.Hidden;
            EditGanreStackPanel.Visibility = Visibility.Hidden;
            EditPublishHouesStackPanel.Visibility = Visibility.Hidden;
            EditExecutorStackPanel.Visibility = Visibility.Hidden;
            DelMusicRecordStackPanel.Visibility = Visibility.Hidden;
            DelGanreStackPanel.Visibility = Visibility.Hidden;
            DelSongsStackPanel.Visibility = Visibility.Hidden;
            DelPublishHouseStackPanel.Visibility = Visibility.Hidden;
            DelExecutorStackPanel.Visibility = Visibility.Hidden;
            AdminsStackPanel.Visibility = Visibility.Hidden;
        }




        //Иннициализация админа
        public void SetAdmin(Admins admins)
        {
            using (db = new VinylShopContext())
            {
                admin = db.admins.Find(admins.Id);
                AdminNameTextBox.Text = admin.login;
                admin.IsEnabled = true;
                db.SaveChanges();
            }
            return;
        }


        //Заполнение всех основных комбоБоксов
        private void AddComboBoxItems()
        {
            string[] strings = { "Пластинку", "Песню", "Жанр", "Издательство", "Исполнителя" };
            ComboBoxAdd.ItemsSource = strings;
            ComboBoxDel.ItemsSource = strings;
            ComboBoxEdit.ItemsSource = strings;
            ComboBoxAdd.IsEnabled = false;
            ComboBoxDel.IsEnabled = false;
            ComboBoxEdit.IsEnabled = false;
        }

        //ЧекБоксы для (Редактирования/Добавления/Удаления)
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (AdderCheckBox.IsChecked == true)
            {
                ComboBoxAdd.IsEnabled = true;
                ComboBoxDel.IsEnabled = false;
                ComboBoxEdit.IsEnabled = false;

            }
            else if (EditMusicRecord.IsChecked == true)
            {
                ComboBoxEdit.IsEnabled = true;
                ComboBoxDel.IsEnabled = false;
                ComboBoxAdd.IsEnabled = false;
            }
            else 
            {
                ComboBoxDel.IsEnabled = true;
                ComboBoxAdd.IsEnabled = false;
                ComboBoxEdit.IsEnabled = false;
            }
        }
        

        //Правильное закрытие окна и перевод админа в отключеный режим
        private void CloseWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                admin = db.admins.Find(admin.Id);
                admin.IsEnabled = false;
                db.SaveChanges();
            }
            this.Close();
        }


        //Выборка стек панелей через комбо бокс (Добавление)
        private void ComboBoxAdd_Selected(object sender, SelectionChangedEventArgs e)
        {
            IsVisibilityFalseAllStackPanel();
            using (db = new VinylShopContext()) {
                switch (ComboBoxAdd.SelectedIndex)
                {
                    case 0:
                        AddMusicRecordStackPanel.Visibility = Visibility.Visible;
                        SongsListBoxForRecords.ItemsSource = db.GetSongs();
                        ComboBoxPublishHouse.ItemsSource = db.pubishHouses.ToList();
                        break;
                    case 1:
                        AddSongStackPanel.Visibility = Visibility.Visible;
                        AddComboBoxExecutorFromSong.ItemsSource = db.executorMusics.ToList();
                        AddComboBoxGanreFromSong.ItemsSource = db.ganreMusics.ToList();
                        SongListBox.ItemsSource = db.GetSongs();
                        break;
                    case 2:
                        AddGanreStackPanel.Visibility = Visibility.Visible;
                        GanreListBox.ItemsSource = db.ganreMusics.ToList();
                        break;
                    case 3:
                        AddPublishHouseStackPanel.Visibility = Visibility.Visible;
                        PublishHouseListBox.ItemsSource = db.pubishHouses.ToList();
                        break;
                    case 4:
                        AddExecutorStackPanel.Visibility = Visibility.Visible;
                        ExecutorsListBox.ItemsSource = db.executorMusics.ToList();
                        break;

                }
            }
            

        }



        //Выборка стек панелей через комбо бокс (Редактирование)
        private void ComboBoxEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsVisibilityFalseAllStackPanel();
            using (db = new VinylShopContext()) {
                switch (ComboBoxEdit.SelectedIndex)
                {
                    case 0:
                        EditMusicRecordStackPanel.Visibility = Visibility.Visible;
                        break;
                    case 1:
                        EditSongsStackPanel.Visibility = Visibility.Visible;
                        break;
                    case 2:
                        EditGanreStackPanel.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        EditPublishHouesStackPanel.Visibility = Visibility.Visible;
                        PublishHousesListBox.ItemsSource = db.pubishHouses.ToList();
                        break;
                    case 4:
                        EditExecutorStackPanel.Visibility = Visibility.Visible;
                        ExecutorListBox.ItemsSource = db.executorMusics.ToList();
                        break;
                }
            }
        }


        // ЧекБоксы для редактирования песен (СтекПанель редактор пластинок)
        private void CheckBoxEditSongs_IsEnabledChanged(object sender, RoutedEventArgs e)
        {
            AddOrDelStackPanel.Visibility = Visibility.Visible;
        }
        private void CheckBoxEditSongs_IsEnabledChanged_1(object sender, RoutedEventArgs e)
        {
            AddOrDelStackPanel.Visibility = Visibility.Hidden;
        }




        //Изменение ТекстБокса на КомбоБоксы Редактирование песни
        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            if(CheckBoxExecutor.IsChecked == true)
            {
                TextBoxExecutorStackPanel.Visibility= Visibility.Hidden;
                ComboBoxExecutorStackPanel.Visibility = Visibility.Visible;
            }
            if(CheckBoxGanre.IsChecked == true)
            {
                TextBoxGanreStackPanel.Visibility = Visibility.Hidden;
                ComboBoxGanreStackPanel.Visibility = Visibility.Visible;
            }
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CheckBoxExecutor.IsChecked == false)
            {
                TextBoxExecutorStackPanel.Visibility = Visibility.Visible;
                ComboBoxExecutorStackPanel.Visibility = Visibility.Hidden;
            }
            if (CheckBoxGanre.IsChecked == false)
            {
                TextBoxGanreStackPanel.Visibility = Visibility.Visible;
                ComboBoxGanreStackPanel.Visibility = Visibility.Hidden;
            }
        }


        //Выборка стек панелей через комбо бокс (Удаление)
        private void ComboBoxDel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsVisibilityFalseAllStackPanel();
            switch (ComboBoxDel.SelectedIndex)
            {
                case 0:
                    DelMusicRecordStackPanel.Visibility = Visibility.Visible;
                    break;
                case 1:
                    DelSongsStackPanel.Visibility = Visibility.Visible;
                    break;
                case 2:
                    DelGanreStackPanel.Visibility = Visibility.Visible;
                    break;
                case 3:
                    DelPublishHouseStackPanel.Visibility = Visibility.Visible;
                    break;
                case 4:
                    DelExecutorStackPanel.Visibility = Visibility.Visible;
                    break;
            }
        }


        //Настройки Админа и заполнение полей админа
        private void SettingsAdmin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IsVisibilityFalseAllStackPanel();
            AdminsStackPanel.Visibility = Visibility.Visible;
            AdminLogin.Text = admin.login;
            AdminPasswordBox.Password = admin.password;
            AdminPassword.Text = admin.password;
        }
        //Видимость Пароля (ДА/НЕТ)
        private void BorderCanNotSee_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CanNotSeeStackPanel.Visibility = Visibility.Hidden;
            CanSeeStackPanel.Visibility = Visibility.Visible;
            AdminPassword.Text = AdminPasswordBox.Password;
        }
        private void BorderCanSee_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CanSeeStackPanel.Visibility = Visibility.Hidden;
            CanNotSeeStackPanel.Visibility = Visibility.Visible;
            if (AdminPasswordBox.Password != admin.password &&  AdminPassword.Text == admin.password){
                AdminPassword.Text = AdminPasswordBox.Password;
            }
            else
            {
                AdminPasswordBox.Password = AdminPassword.Text;
            }
           
        }
        //Сохрранение данных админа
        private void SaveAdminData_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BorderCanSee_MouseDown(sender, e);
            using (db = new VinylShopContext())
            {
                admin = db.admins.Find(admin.Id);
                admin.login = AdminLogin.Text;
                admin.password = AdminPasswordBox.Password;
                admin.IsEnabled = true;
                AdminNameTextBox.Text = admin.login;
                db.SaveChanges();
            }
        }
        //Добавление ноаого админа
        private void AddNewAdmin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Admins newAdmin = new Admins();
            newAdmin.login = NameNewAdmin.Text;
            newAdmin.password = PassNewAdmin.Text;
            newAdmin.IsEnabled = false;
            using (db = new VinylShopContext())
            {
                db.admins.Add(newAdmin);
                db.SaveChanges();
            }
        }



        //Добавление нового Жанра
        private void AddNewGanre_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GanreMusic newGanre = new GanreMusic();
            newGanre.Name = NameGanre.Text;
            using (db = new VinylShopContext())
            {
                db.ganreMusics.Add(newGanre);
                db.SaveChanges();
                GanreListBox.ItemsSource = db.ganreMusics.ToList();
                NameGanre.Text = string.Empty;
            }
        }

        //Добавление нового Исполнителя
        private void AddExecutor_Admin(object sender, MouseButtonEventArgs e)
        {
            ExecutorMusic executor = new ExecutorMusic();
            executor.Name = NameExecutor.Text;
            using (db = new VinylShopContext())
            {
                db.executorMusics.Add(executor);
                db.SaveChanges();
                ExecutorsListBox.ItemsSource = db.executorMusics.ToList();
                NameExecutor.Text = string.Empty;
            }
        }

        // Добавление нового Издательства
        private void AddPublishHoues_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PubishHouse house = new PubishHouse();
            house.Name = NamePublishHouse.Text;
            using (db = new VinylShopContext())
            {
                db.pubishHouses.Add(house);
                db.SaveChanges();
                PublishHouseListBox.ItemsSource = db.pubishHouses.ToList();
                NamePublishHouse.Text = string.Empty;
            }
        }

        //Добавление нового трека 
        private void AddSong_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                Songs song = new Songs();
                song.Name = NameSong.Text;
                song.ganreMusics = db.GetListGanre((GanreMusic)AddComboBoxGanreFromSong.SelectedItem);
                song.executorMusics = db.GetListExecutors((ExecutorMusic)AddComboBoxExecutorFromSong.SelectedItem);

                db.songs.Add(song);
                db.SaveChanges();
                SongListBox.ItemsSource = db.GetSongs();
                NameSong.Text = string.Empty;
                AddComboBoxExecutorFromSong.Text = string.Empty;
                AddComboBoxGanreFromSong.Text = string.Empty;
            }
        }


        public Music_Records records { get; set; } = new Music_Records();

        //Добавление песни в пластинку
        private void AddSongToMusicRecord_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                records.songs.Add(db.GetSong((Songs)SongsListBoxForRecords.SelectedItem));
                CountSongsInRecord.Text = records.songs.Count.ToString();
            }
        }

        //Добавление новой пластинки
        private void AddMusicRecord_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                records.Name = NameRecord.Text;

                int tempYear;
                if (int.TryParse(YearRecord.Text, out tempYear))
                    records.Year = tempYear;

                records.Description = DescriptionRecord.Text;
                records.pubishHouse = db.GetPubishHouse((PubishHouse)ComboBoxPublishHouse.SelectedItem);
                double tempPrice;
                if(double.TryParse(PriceMusicRecord.Text, out tempPrice))
                    records.Price = tempPrice;

                records.songs = db.GetSongsForRecord(records.songs.ToList());
                records.CountSongs = records.songs.Count;

                db.music_Records.Add(records);
                db.SaveChanges();
                records = new Music_Records();

                NameRecord.Text = string.Empty;
                YearRecord.Text = string.Empty;
                DescriptionRecord.Text = string.Empty;
                ComboBoxPublishHouse.Text = string.Empty;
                CountSongsInRecord.Text = string.Empty;
                PriceMusicRecord.Text = string.Empty;
            }
        }



        public PubishHouse publish { get; set; }

       //Запись названия издательства в текст бокс (Редактирование издательства)
        private void EditPublishHouseListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            publish = (PubishHouse)PublishHousesListBox.SelectedItem;
            if (publish != null)
                EditNamePublishHouse.Text = publish.Name;
        }

        //Редактирование Издательства
        private void SaveEditPublishHouse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                publish = db.pubishHouses.Find(publish.Id);
                publish.Name = EditNamePublishHouse.Text;
                db.SaveChanges();
                PublishHousesListBox.ItemsSource = db.pubishHouses.ToList();
            }
        }


        public ExecutorMusic executor { get; set; }

        private void EditExecutorListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            executor = (ExecutorMusic)ExecutorListBox.SelectedItem;
            if(executor != null)
                EditNameExecutor.Text = executor.Name;
        }

        private void SaveEditExecutor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                executor = db.executorMusics.Find(executor.Id);
                executor.Name = EditNameExecutor.Text;
                db.SaveChanges();
                ExecutorListBox.ItemsSource = db.executorMusics.ToList();
            }
        }
    }
}
