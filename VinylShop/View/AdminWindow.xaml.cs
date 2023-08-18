using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            AddDateToComboBox();
            IsVisibilityFalseAllStackPanel();
            BackgroundFone.Visibility = Visibility.Visible;

        }


        //============== ОСНОВНЫЕ СОБЫТИЯ И МЕТОДЫ  И РАБОТА С АДМИНОМ ==================

        //Прячет все стек панели и не нужные элементы
        private void IsVisibilityFalseAllStackPanel()
        {
            BackgroundFone.Visibility = Visibility.Hidden;
            AddMusicRecordStackPanel.Visibility = Visibility.Hidden;
            AddSongStackPanel.Visibility = Visibility.Hidden;
            AddGanreStackPanel.Visibility = Visibility.Hidden;
            AddExecutorStackPanel.Visibility = Visibility.Hidden;
            AddPublishHouseStackPanel.Visibility = Visibility.Hidden;
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
            OneExecutorAddStackPanel.Visibility = Visibility.Hidden;
            FewExecutorsAddStackPanel.Visibility = Visibility.Hidden;
            OneGanreAddStackPanel.Visibility = Visibility.Hidden;
            FewGanresAddStackPanel.Visibility = Visibility.Hidden;
            ComboBoxPublishHouseEdit.Visibility = Visibility.Hidden;
            AddSellDayStackPanel.Visibility = Visibility.Hidden;
            DelSellDayStackPanel.Visibility = Visibility.Hidden;
        }


        string[] SearchRecord = { "Пластинку", "Пластинку по издательству" }; // Строка для поиска пластинок
        string[] SearchSong = { "Песню", "Песню по жанру", "Песню по исполнителю" }; // Строка для поиска песен
        string[] SearchGanreAndExecutor = { "Исполнителя", "Жанр" };
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
            string[] strings = { "Пластинку", "Песню", "Жанр", "Издательство", "Исполнителя", "Акцию"};
            ComboBoxAdd.ItemsSource = strings;
            ComboBoxDel.ItemsSource = strings;
            ComboBoxEdit.ItemsSource = strings;
            ComboBoxAdd.IsEnabled = false;
            ComboBoxDel.IsEnabled = false;
            ComboBoxEdit.IsEnabled = false;
        }

        private void AddDateToComboBox()
        {
            for(int i = 1;i <= 31;i++)
            {
                ddStart.Items.Add(i);
                ddEnd.Items.Add(i);
                if (i < 13)
                {
                    mmStart.Items.Add(i);
                    mmEnd.Items.Add(i);
                }
            }

            for(int i = DateTime.Now.Year; i < 2100; i++)
            {
                yyStart.Items.Add(i);
                yyEnd.Items.Add(i);
            }


        }

        //РадиоБатоны для (Редактирования/Добавления/Удаления)
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            BackgroundFone.Visibility = Visibility.Visible;

            ComboBoxAdd.Text = string.Empty;
            ComboBoxDel.Text = string.Empty;
            ComboBoxEdit.Text = string.Empty;

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
            if (AdminPasswordBox.Password != admin.password && AdminPassword.Text == admin.password)
            {
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
                MessageBox.Show("Данные изменены");
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
                MessageBox.Show("Админ добавлен");
                NameNewAdmin.Text = string.Empty;
                PassNewAdmin.Text = string.Empty;
            }

        }



        //=================  ДОБАВЛЕНИЕ В БД =========================

        //Выборка стек панелей через комбо бокс (Добавление)
        private void ComboBoxAdd_Selected(object sender, SelectionChangedEventArgs e)
        {

            IsVisibilityFalseAllStackPanel();

            using (db = new VinylShopContext())
            {
                ComboBoxSearchHelp.ItemsSource = null;
                switch (ComboBoxAdd.SelectedIndex)
                {
                    case -1:
                        BackgroundFone.Visibility = Visibility.Visible;
                        break;
                    case 0:
                        AddMusicRecordStackPanel.Visibility = Visibility.Visible;
                        SongsListBoxForRecords.ItemsSource = db.GetListSongs().Take(15);
                        SongsListBoxForRecords.ToolTip = "Введите в поиске песню чтобы добваить";
                        ComboBoxPublishHouse.ItemsSource = db.pubishHouses.ToList();
                        ComboBoxSearchHelp.Items.Clear();
                        ComboBoxSearchHelp.ItemsSource = SearchSong;
                        break;
                    case 1:
                        AddSongStackPanel.Visibility = Visibility.Visible;
                        AddComboBoxExecutorFromSong.ItemsSource = db.executorMusics.ToList();
                        AddComboBoxGanreFromSong.ItemsSource = db.ganreMusics.ToList();
                        OneExecutorAddStackPanel.Visibility = Visibility.Visible;
                        OneGanreAddStackPanel.Visibility = Visibility.Visible;
                        ComboBoxSearchHelp.ItemsSource = SearchGanreAndExecutor;
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
                    case 5:
                        AddSellDayStackPanel.Visibility = Visibility.Visible;
                        GanreComboBoxForSell.ItemsSource = db.ganreMusics.ToList();
                        NameSellDayTextBlock.Text = "Добавления акционных дней";
                        AddOrSaveTextBlockSellsDay.Text = "Добавить";
                        HelperForSell = 0;
                        break;
                }
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

        List<ExecutorMusic> ListExecutors { get; set; } = new List<ExecutorMusic>();
        List<GanreMusic> ListGanres { get; set; } = new List<GanreMusic>();
        private void AddSong_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                Songs song = new Songs();
                song.Name = NameSong.Text;
                if (FewGanresCheckBox.IsChecked != true)
                    song.ganreMusics = db.GetListGanre((GanreMusic)AddComboBoxGanreFromSong.SelectedItem);
                else
                {
                    foreach (var ganre in ListBoxAddGanresToSongs.SelectedItems)
                    {
                        if (!CheckGanreIsList(db.GetGanre((GanreMusic)ganre).Id, song.ganreMusics.ToList()))
                        {
                            song.ganreMusics.Add(db.ganreMusics.Find(db.GetGanre((GanreMusic)ganre).Id));
                        }
                    }
                }
                if (FewExecutorsCheckBox.IsChecked != true)
                    song.executorMusics = db.GetListExecutors((ExecutorMusic)AddComboBoxExecutorFromSong.SelectedItem);
                else
                {
                    foreach (var executor in ListBoxAddExecutorsToSongs.SelectedItems)
                    {
                        if (!CheckExecutorIsList(db.GetExecutors((ExecutorMusic)executor).Id, song.executorMusics.ToList()))
                        {
                            song.executorMusics.Add(db.executorMusics.Find(db.GetExecutors((ExecutorMusic)executor).Id));
                        }
                    }
                }
                db.songs.Add(song);
                db.SaveChanges();
                ListGanres.Clear();
                ListExecutors.Clear();
                NameSong.Text = string.Empty;
                AddComboBoxExecutorFromSong.Text = string.Empty;
                AddComboBoxGanreFromSong.Text = string.Empty;
                FewGanresCheckBox.IsChecked = false;
                FewExecutorsCheckBox.IsChecked = false;
            }
        }


        //Чек боксы для добавления нескольких исполнителей песен (Добавление песен)
        private void FewExecutorsAddCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                OneExecutorAddStackPanel.Visibility = Visibility.Hidden;
                FewExecutorsAddStackPanel.Visibility = Visibility.Visible;
                ListBoxAddExecutorsToSongs.Width = 300;
                ListBoxAddExecutorsToSongs.Height = 150;
                ListBoxAddExecutorsToSongs.ItemsSource = db.executorMusics.ToList();
                AddFewExecutorsTextBlock.Margin = new Thickness(0, 60, 100, 0);
                AddFewExecutorsTextBlock.Visibility = Visibility.Visible;

            }
        }
        private void FewExecutorsAddCheckBox_Unhecked(object sender, RoutedEventArgs e)
        {
            OneExecutorAddStackPanel.Visibility = Visibility.Visible;
            FewExecutorsAddStackPanel.Visibility = Visibility.Hidden;
            ListBoxAddExecutorsToSongs.Width = 0;
            ListBoxAddExecutorsToSongs.Height = 0;
            AddFewExecutorsTextBlock.Margin = new Thickness(0, 0, 0, 0);
            AddFewExecutorsTextBlock.Visibility = Visibility.Hidden;
        }
        //Чек боксы для добавления нескольких жанров песен (Добавление песен)
        private void FewGanresAddCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                OneGanreAddStackPanel.Visibility = Visibility.Hidden;
                FewGanresAddStackPanel.Visibility = Visibility.Visible;
                ListBoxAddGanresToSongs.Width = 300;
                ListBoxAddGanresToSongs.Height = 150;
                ListBoxAddGanresToSongs.ItemsSource = db.ganreMusics.ToList();
                AddFewGanresTextBlock.Margin = new Thickness(0, 60, 100, 0);
                AddFewGanresTextBlock.Visibility = Visibility.Visible;
            }
        }
        private void FewGanresAddCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            OneGanreAddStackPanel.Visibility = Visibility.Visible;
            FewGanresAddStackPanel.Visibility = Visibility.Hidden;
            ListBoxAddGanresToSongs.Width = 0;
            ListBoxAddGanresToSongs.Height = 0;
            AddFewGanresTextBlock.Margin = new Thickness(0, 0, 0, 0);
            AddFewGanresTextBlock.Visibility = Visibility.Hidden;
        }


        // Добавление в лист исполнителей
        private void AddFewExecutorsForSongs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
               
                MessageBox.Show("Все исполнители успешно добавлены");
            }
        }
        //Наличие исполнителя в листе
        private bool CheckExecutorIsList(int id, List<ExecutorMusic> list)
        {
            foreach (var item in list)
            {
                if (item.Id == id)
                {
                    return true;
                }
            }
            return false;
        }


        //Добавление в лист жанров
        private void AddFewGanresForSongs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
               
                MessageBox.Show("Все жанры успешно добавлены");
            }
        }
        //Наличие жанров в листе
        private bool CheckGanreIsList(int id, List<GanreMusic> list)
        {
            foreach (var item in list)
            {
                if (item.Id == id)
                {
                    return true;
                }
            }
            return false;
        }


        //Добавление песни в пластинку
        public Music_Records records { get; set; } = new Music_Records();
        private void AddSongToMusicRecord_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                foreach (var item in SongsListBoxForRecords.SelectedItems)
                {
                    records.songs.Add(db.GetSong((Songs)item));
                }
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
                if (double.TryParse(PriceMusicRecord.Text, out tempPrice))
                    records.Price = tempPrice;

                int tempQuantity;
                if(int.TryParse(QuantityRecords.Text, out tempQuantity))
                    records.Quantity = tempQuantity;

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
                QuantityRecords.Text = string.Empty;
                SongsListBoxForRecords.ItemsSource = db.GetListSongs().Take(15);
                MessageBox.Show("Добавлено");
            }
        }





        //=================  РЕДАКТИРОВАНИЕ ДАННЫХ В БД =========================


        //Выборка стек панелей через комбо бокс (Редактирование)
        private void ComboBoxEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsVisibilityFalseAllStackPanel();
            using (db = new VinylShopContext())
            {
                ComboBoxSearchHelp.ItemsSource = null;
                ComboBoxSearchHelp.Items.Clear();
                switch (ComboBoxEdit.SelectedIndex)
                {
                    case -1:
                        BackgroundFone.Visibility = Visibility.Visible;
                        break;
                    case 0:
                        EditMusicRecordStackPanel.Visibility = Visibility.Visible;
                        TextBoxPublishHouseEdit.Visibility = Visibility.Visible;
                        AllMusicRecords.ItemsSource = db.GetListMusic_Records();
                        foreach (var item in SearchRecord)
                            ComboBoxSearchHelp.Items.Add(item);
                        foreach (var item in SearchSong)
                            ComboBoxSearchHelp.Items.Add(item);
                        break;
                    case 1:
                        EditSongsStackPanel.Visibility = Visibility.Visible;
                        AllSongsListBoxEdit.ItemsSource = db.GetSongs();
                        foreach (var item in SearchSong)
                            ComboBoxSearchHelp.Items.Add(item);
                        foreach (var item in SearchGanreAndExecutor)
                            ComboBoxSearchHelp.Items.Add(item);
                        break;
                    case 2:
                        EditGanreStackPanel.Visibility = Visibility.Visible;
                        GareListBox.ItemsSource = db.ganreMusics.ToList();
                        break;
                    case 3:
                        EditPublishHouesStackPanel.Visibility = Visibility.Visible;
                        PublishHousesListBox.ItemsSource = db.pubishHouses.ToList();
                        break;
                    case 4:
                        EditExecutorStackPanel.Visibility = Visibility.Visible;
                        ExecutorListBox.ItemsSource = db.executorMusics.ToList();
                        break;
                    case 5:
                        AddSellDayStackPanel.Visibility = Visibility.Visible;
                        NameSellDayTextBlock.Text = "Редактирование акционных дней";
                        AddOrSaveTextBlockSellsDay.Text = "Сохранить изменения";
                        GanreComboBoxForSell.ItemsSource = db.ganreMusics.ToList();
                        ListAllSellsDay.ItemsSource = db.GetListSellsDay();
                        HelperForSell = 1;
                        break;

                }
            }
        }

        // ====== Редактирование издательства

        public PubishHouse publish { get; set; }
        int indxPublishHouseHelp = 0;

        //Запись названия издательства в текст бокс (Редактирование издательства)
        private void EditPublishHouseListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            publish = (PubishHouse)PublishHousesListBox.SelectedItem;
            if (publish != null)
            {
                EditNamePublishHouse.Text = publish.Name;
                indxPublishHouseHelp = publish.Id;
            }
        }

        //Редактирование Издательства (Сохранение в БД)
        private void SaveEditPublishHouse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                publish = db.pubishHouses.Find(indxPublishHouseHelp);
                publish.Name = EditNamePublishHouse.Text;
                db.SaveChanges();
                PublishHousesListBox.ItemsSource = db.pubishHouses.ToList();
            }
        }


        // ====== Редактирование исполнителя

        public ExecutorMusic executor { get; set; }
        int indxExecutorHelp = 0;

        //Запись названия исполнителя в текст бокс (Редактирование исполнителя)
        private void EditExecutorListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            executor = (ExecutorMusic)ExecutorListBox.SelectedItem;
            if (executor != null)
            {
                EditNameExecutor.Text = executor.Name;
                indxExecutorHelp = executor.Id;
            }
        }

        //Редактирование исполнителя (Сохранение в БД)
        private void SaveEditExecutor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                executor = db.executorMusics.Find(indxExecutorHelp);
                executor.Name = EditNameExecutor.Text;
                db.SaveChanges();
                ExecutorListBox.ItemsSource = db.executorMusics.ToList();
            }
        }


        // ====== Редактирование жанра

        public GanreMusic ganre { get; set; }
        int indxGanreHelp = 0;

        //Запись названия жанра в текст бокс (Редактирование жанра)
        private void EditGanreListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ganre = (GanreMusic)GareListBox.SelectedItem;
            if (ganre != null)
            {
                EditNameGanre.Text = ganre.Name;
                indxGanreHelp = ganre.Id;
            }
        }
        
        //Редактирование жанра (Сохранение в БД)
        private void SaveEditGanre_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                ganre = db.ganreMusics.Find(indxGanreHelp);
                ganre.Name = EditNameGanre.Text;
                db.SaveChanges();
                GareListBox.ItemsSource = db.ganreMusics.ToList();
            }

        }



        // ====== Редактирование песни

        //Включение КомбоБоксов для исполнителей (Редактирование песни)
        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                if (CheckBoxExecutor.IsChecked == true)
                {
                    CheckBoxDelExecutor.IsChecked = false;
                    ComboBoxExecutorStackPanel.Visibility = Visibility.Visible;
                    AddNewExecutorToListTextBlockEdit.Visibility = Visibility.Visible;
                    AddNewExecutorToListTextBlockEdit.Margin = new Thickness(150, 10, 0, 0);
                    DelOldExecutorToListTextBlockEdit.Visibility = Visibility.Hidden;
                    FewExecutorsComboBoxEdit.ItemsSource = db.executorMusics.ToList();
                }
            }
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CheckBoxExecutor.IsChecked == false)
            {
                DelOldExecutorToListTextBlockEdit.Visibility = Visibility.Visible;
                AddNewExecutorToListTextBlockEdit.Margin = new Thickness(90, 10, 0, 0);
                ComboBoxExecutorStackPanel.Visibility = Visibility.Hidden;
            }
        }

        //Включение КомбоБоксов для жанра (Редактирование песни)
        private void CheckBoxGanre_Checked_1(object sender, RoutedEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                if (CheckBoxGanre.IsChecked == true)
                {
                    CheckBoxDelGanre.IsChecked = false;
                    ComboBoxGanreStackPanel.Visibility = Visibility.Visible;
                    DelOldGanreToListTextBlockEdit.Visibility = Visibility.Hidden;
                    AddNewGanreToListTextBlockEdit.Visibility = Visibility.Visible;
                    AddNewGanreToListTextBlockEdit.Margin = new Thickness(150, 10, 0, 0);
                    FewGanresComboBoxEdit.ItemsSource = db.ganreMusics.ToList();
                }
            }
        }
        private void CheckBoxGanre_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CheckBoxGanre.IsChecked == false)
            {
                DelOldGanreToListTextBlockEdit.Visibility = Visibility.Visible;
                AddNewGanreToListTextBlockEdit.Margin = new Thickness(90, 10, 0, 0);
                ComboBoxGanreStackPanel.Visibility = Visibility.Hidden;
            }
        }


        //Включение КомбоБоксов в режим удаление данных для исполнителя (Редактирование Песни)
        private void CheckBoxDel_Checked_1(object sender, RoutedEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                song = db.songs.Find(indxSongsHelp);
                if (song != null)
                {
                    if (CheckBoxDelExecutor.IsChecked == true)
                    {
                        CheckBoxExecutor.IsChecked = false;
                        ComboBoxExecutorStackPanel.Visibility = Visibility.Visible;
                        AddNewExecutorToListTextBlockEdit.Visibility = Visibility.Hidden;
                        FewExecutorsComboBoxEdit.ItemsSource = song.executorMusics;
                    }
                }
            }
        }
        private void CheckBoxDel_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CheckBoxExecutor.IsChecked == false)
            {
                ComboBoxExecutorStackPanel.Visibility = Visibility.Hidden;

            }
        }


        //Включение КомбоБоксов в режим удаление данных для жанра (Редактирование Песни)
        private void CheckBoxDelGanre_Checked_1(object sender, RoutedEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                song = db.songs.Find(indxSongsHelp);
                if (song != null)
                {
                    if (CheckBoxDelGanre.IsChecked == true)
                    {
                        CheckBoxGanre.IsChecked = false;
                        ComboBoxGanreStackPanel.Visibility = Visibility.Visible;
                        AddNewGanreToListTextBlockEdit.Visibility = Visibility.Hidden;
                        FewGanresComboBoxEdit.ItemsSource = song.ganreMusics;
                    }
                }
            }
        }
        private void CheckBoxDelGanre_Unchecked(object sender, RoutedEventArgs e)
        {

            if (CheckBoxGanre.IsChecked == false)
            {
                ComboBoxGanreStackPanel.Visibility = Visibility.Hidden;

            }
        }


        public Songs song { get; set; }
        int indxSongsHelp = 0;

        private void EditSongsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                song = (Songs)AllSongsListBoxEdit.SelectedItem;
                if (song != null)
                {
                    song = db.songs.Find(song.Id);
                    NameSongEdit.Text = song.Name;
                    EditExecutorForSongsListBox.ItemsSource = song.executorMusics;
                    EditGanreForSongsListBox.ItemsSource = song.ganreMusics;
                    listForDelExecutors = song.executorMusics.ToList();
                    listForDelGanres = song.ganreMusics.ToList();
                    indxSongsHelp = song.Id;
                }
            }
        }

        //Добавление исполнителей (Редактирование Песни)
        private void SaveEditExecutorFromSongs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                if (song != null)
                {
                    ExecutorMusic executor = (ExecutorMusic)FewExecutorsComboBoxEdit.SelectedItem;
                    if (executor != null)
                    {
                        song = db.songs.Find(indxSongsHelp);
                        if (!CheckExecutorIsList(executor.Id, song.executorMusics.ToList()))
                        {
                            song.executorMusics.Add(db.executorMusics.Find(executor.Id));
                            db.SaveChanges();
                            listForDelExecutors = db.GetListExecutors(song.executorMusics.ToList());
                            EditExecutorForSongsListBox.ItemsSource = song.executorMusics.ToList();
                        }
                    }
                }
            }
        }

        List<ExecutorMusic> listForDelExecutors; // Список не удаленных исполнителей для вывода в лист бокс
        //Удаление исполнителей (Редактирование Песни)
        private void DelEditExecutorFromSongs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                song = db.songs.Find(indxSongsHelp);
                ExecutorMusic executor = (ExecutorMusic)FewExecutorsComboBoxEdit.SelectedItem;
                DeleteExecutorsFromListForListBox(ref listForDelExecutors, db.executorMusics.Find(executor.Id));
                EditExecutorForSongsListBox.ItemsSource = listForDelExecutors;
                FewExecutorsComboBoxEdit.ItemsSource = listForDelExecutors;
            }
        }
        private void DeleteExecutorsFromListForListBox(ref List<ExecutorMusic> FullList, ExecutorMusic deletedExecutors)
        {
            List<ExecutorMusic> list = new List<ExecutorMusic>();
            foreach (var executor in FullList)
            {
                if (deletedExecutors.Id != executor.Id)
                    list.Add(executor);
            }
            FullList = list;
        }



        //Добавление жанров (Редактирование Песни)
        private void SaveEditGanreFromSongs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                if (song != null)
                {
                    GanreMusic ganre = (GanreMusic)FewGanresComboBoxEdit.SelectedItem;
                    if (ganre != null)
                    {
                        song = db.songs.Find(indxSongsHelp);
                        if (!CheckGanreIsList(ganre.Id, song.ganreMusics.ToList()))
                        {
                            song.ganreMusics.Add(db.ganreMusics.Find(ganre.Id));
                            db.SaveChanges();
                            listForDelGanres = db.GetListGanre(song.ganreMusics.ToList());
                            EditGanreForSongsListBox.ItemsSource = song.ganreMusics.ToList();
                        }
                    }
                }
            }
        }

        List<GanreMusic> listForDelGanres; // Список не удаленных жанров для вывода в лист бокс
        //Удаление жанров (Редактирование Песни)
        private void DelEditGanreFromSongs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                song = db.songs.Find(indxSongsHelp);
                GanreMusic ganre = (GanreMusic)FewGanresComboBoxEdit.SelectedItem;
                DeleteGanreFromListForListBox(ref listForDelGanres, db.ganreMusics.Find(ganre.Id));
                EditGanreForSongsListBox.ItemsSource = listForDelGanres;
                FewGanresComboBoxEdit.ItemsSource = listForDelGanres;
            }
        }
        private void DeleteGanreFromListForListBox(ref List<GanreMusic> FullList, GanreMusic deletedGanres)
        {
            List<GanreMusic> list = new List<GanreMusic>();
            foreach (var ganre in FullList)
            {
                if (deletedGanres.Id != ganre.Id)
                    list.Add(ganre);
            }
            FullList = list;
        }


        //Редактирование песни (Сохранение в БД)
        private void SaveEditSongs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                song = db.songs.Find(indxSongsHelp);
                song.Name = NameSongEdit.Text;
                if (listForDelExecutors.Count < song.executorMusics.Count)
                    db.DelListExecutorsFromSong(listForDelExecutors, song.Id);
                if (listForDelGanres.Count < song.ganreMusics.Count)
                    db.DelListGanresFromSong(listForDelGanres, song.Id);
                db.SaveChanges();
                ListGanres.Clear();
                ListExecutors.Clear();
                AllSongsListBoxEdit.ItemsSource = db.GetSongs();
            }
        }



        // ====== Редактирование пластинки

        // ЧекБоксы для добавления треков в пластинку (Редакторирование пластинок)
        private void CheckBoxEditSongs_Checked(object sender, RoutedEventArgs e)
        {
            using (db = new VinylShopContext()) {
                if (CheckBoxEditSongs.IsChecked == true)
                {
                    AddSongToListForMusicRecordTextBlock.Visibility = Visibility.Visible;
                    DelSongToListForMusicRecordTextBlock.Visibility = Visibility.Hidden;
                    CheckBoxEditSongsDel.IsChecked = false;
                    AddSongToListForMusicRecordTextBlock.Margin = new Thickness(150, 20, 0, 0);
                    SongsListBoxForRecordsEdit.ItemsSource = db.GetListSongs(db.songs.ToList());
                }
            }
        }
        private void CheckBoxEditSongs_Unchecked(object sender, RoutedEventArgs e)
        {
            AddSongToListForMusicRecordTextBlock.Visibility = Visibility.Hidden;
            AddSongToListForMusicRecordTextBlock.Margin = new Thickness(50, 20, 0, 0);
            if (Records != null)
                SongsListBoxForRecordsEdit.ItemsSource = Records.songs.ToList();
        }

        // ЧекБоксы для удаления треков из пластинки (Редакторирование пластинок)
        private void CheckBoxEditSongsDel_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckBoxEditSongsDel.IsChecked == true)
            {
                AddSongToListForMusicRecordTextBlock.Visibility = Visibility.Hidden;
                DelSongToListForMusicRecordTextBlock.Visibility = Visibility.Visible;
                CheckBoxEditSongs.IsChecked = false;
            }
        }
        private void CheckBoxEditSongsDel_Unchecked(object sender, RoutedEventArgs e)
        {
            DelSongToListForMusicRecordTextBlock.Visibility = Visibility.Hidden;
        }

        //Включение комбоБокса с Издательствами (Редактирование Пластинки)
        private void EditPublishHouseForMusicRecords_Checked(object sender, RoutedEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                TextBoxPublishHouseEdit.Visibility = Visibility.Hidden;
                ComboBoxPublishHouseEdit.Visibility = Visibility.Visible;
                ComboBoxPublishHouseEdit.ItemsSource = db.pubishHouses.ToList();
            }
        }
        private void EditPublishHouseForMusicRecords_Unchecked(object sender, RoutedEventArgs e)
        {
            TextBoxPublishHouseEdit.Visibility = Visibility.Visible;
            ComboBoxPublishHouseEdit.Visibility = Visibility.Hidden;
        }


        Music_Records Records { get; set; } = new Music_Records();
        int indxMusicRecordHelp = 0;
        private void EditMusicRecordsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                Records = (Music_Records)AllMusicRecords.SelectedItem;
                if (Records != null)
                {
                    Records = db.music_Records.Find(Records.Id);
                    indxMusicRecordHelp = Records.Id;
                    NameRecordEdit.Text = Records.Name;
                    YearRecordEdit.Text = Records.Year.ToString();
                    DescriptionRecordEdit.Text = Records.Description;
                    TextBoxPublishHouseEdit.Text = Records.pubishHouse.Name;
                    QuantityEdit.Text = Records.Quantity.ToString();
                    PriceMusicRecordsEditTextBox.Text = Records.Price.ToString();
                    Records.songs = db.GetListSongs(Records.songs.ToList());
                    SongsListBoxForRecordsEdit.ItemsSource = Records.songs.ToList();
                    listSongsForRecord = db.GetListSongs(Records.songs.ToList());
                    recordHelpForReturnList.songs = Records.songs.ToList();
                    CheckBoxEditSongs.IsChecked = false;
                    CheckBoxEditSongsDel.IsChecked = false;


                }
            }
        }


        private void AddNewSongToListForMusicRecord_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                Records = db.music_Records.Find(indxMusicRecordHelp);
                Records.songs = db.GetListSongs(Records.songs.ToList());
                foreach (var song in SongsListBoxForRecordsEdit.SelectedItems)
                {
                    Records.songs.Add(db.songs.Find((db.GetSong((Songs)song).Id)));
                }
                Records.CountSongs = Records.songs.Count;
                db.SaveChanges();
                listSongsForRecord = db.GetListSongs(Records.songs.ToList());
                SongsListBoxForRecordsEdit.ItemsSource = Records.songs.ToList();
                AllMusicRecords.ItemsSource = db.GetListMusic_Records();
                CheckBoxEditSongs.IsChecked = false;

            }
        }


        List<Songs> listSongsForRecord = new List<Songs>(); //Список для удаления треков в пластинке
        //Удаление трека из пластинки
        private void DelOldSongToListForMusicRecord_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                Records = db.music_Records.Find(indxMusicRecordHelp);
                listSongsForRecord = Records.songs.ToList();
                foreach (var song in SongsListBoxForRecordsEdit.SelectedItems)
                {
                    DeleteSongFromListForListBox(ref listSongsForRecord, db.songs.Find(db.GetSong((Songs)song).Id));
                }
                SongsListBoxForRecordsEdit.ItemsSource = db.GetListSongs(listSongsForRecord);
            }
        }
        private void DeleteSongFromListForListBox(ref List<Songs> FullList, Songs deletedSongs)
        {
            List<Songs> list = new List<Songs>();
            foreach (var song in FullList)
            {
                if (deletedSongs.Id != song.Id)
                    list.Add(song);
            }
            FullList = list;
        }
        
        //Редактирование Пластинки (Сохранение в БД)
        private void SaveEditMusicRecord_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using(db = new VinylShopContext())
            {
                Records = db.music_Records.Find(indxMusicRecordHelp);
                Records.Name = NameRecordEdit.Text;
                int tempYear;
                if (int.TryParse(YearRecord.Text, out tempYear))
                    Records.Year = tempYear;
                Records.Description = DescriptionRecordEdit.Text;
                if (CheckBoxChangeTextBoxToComboBoxEditRecord.IsChecked == true)
                {
                    PubishHouse pubish = (PubishHouse)ComboBoxPublishHouseEdit.SelectedItem;
                    Records.pubishHouse = db.pubishHouses.Find(pubish.Id);
                }

                if (listSongsForRecord.Count < Records.songs.Count && listSongsForRecord.Count != 0)
                {
                    db.DelListSongsFromRecord(listSongsForRecord, Records.Id);
                }

                double tempPrice;
                if(double.TryParse(PriceMusicRecordsEditTextBox.Text, out tempPrice))
                    Records.Price = tempPrice;

                int tempQuantity;
                if (int.TryParse(QuantityEdit.Text, out tempQuantity))
                    Records.Quantity = tempQuantity;

                Records.CountSongs = Records.songs.Count;
                db.SaveChanges();
                listSongsForRecord.Clear();
                CheckBoxChangeTextBoxToComboBoxEditRecord.IsChecked = false;
                AllMusicRecords.ItemsSource = db.GetListMusic_Records();
                MessageBox.Show("Изменения сохранены");
            }
        }

        //=================  УДАЛЕНИЕ ДАННЫХ В БД =========================

        //Выборка стек панелей через комбо бокс (Удаление)
        private void ComboBoxDel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsVisibilityFalseAllStackPanel();
            using (db = new VinylShopContext()) 
            {
                ComboBoxSearchHelp.ItemsSource = null;
                switch (ComboBoxDel.SelectedIndex)
                {
                    case -1:
                        BackgroundFone.Visibility = Visibility.Visible;
                        break;
                    case 0:
                        DelMusicRecordStackPanel.Visibility = Visibility.Visible;
                        DelListBoxMusicRecord.ItemsSource = db.GetListMusic_Records();
                        ComboBoxSearchHelp.ItemsSource = SearchRecord;
                        break;
                    case 1:
                        DelSongsStackPanel.Visibility = Visibility.Visible;
                        DelListBoxSongs.ItemsSource = db.GetListSongs(db.songs.ToList());
                        ComboBoxSearchHelp.ItemsSource = SearchSong;
                        break;
                    case 2:
                        DelGanreStackPanel.Visibility = Visibility.Visible;
                        DelListBoxGanre.ItemsSource = db.ganreMusics.ToList();
                        break;
                    case 3:
                        DelPublishHouseStackPanel.Visibility = Visibility.Visible;
                        DelListBoxPublishHoues.ItemsSource = db.pubishHouses.ToList();
                        break;
                    case 4:
                        DelExecutorStackPanel.Visibility = Visibility.Visible;
                        DelListBoxExecutor.ItemsSource = db.executorMusics.ToList();
                        break;
                    case 5:
                        DelSellDayStackPanel.Visibility = Visibility.Visible;
                        DelListBoxSellsDay.ItemsSource = db.GetListSellsDay();
                        break;

                }
            } 
        }


        // ====== Удаление исполнителя
        private void DelTextBoxExecutors_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                ExecutorMusic executor = (ExecutorMusic)DelListBoxExecutor.SelectedItem;
                if (executor != null)
                {
                    executor = db.executorMusics.Find(executor.Id);
                    db.executorMusics.Remove(executor);
                    db.SaveChanges();
                    DelListBoxExecutor.ItemsSource = db.executorMusics.ToList();
                }
            }
        }

        // ====== Удаление Издательства
        private void DelTextBoxPublishHouse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using(db = new VinylShopContext())
            {
                PubishHouse pubish = (PubishHouse)DelListBoxPublishHoues.SelectedItem;
                if(pubish!= null)
                {
                    pubish = db.pubishHouses.Find(pubish.Id);
                    db.pubishHouses.Remove(pubish);
                    db.SaveChanges();
                    DelListBoxPublishHoues.ItemsSource = db.pubishHouses.ToList();
                }
            }
        }
        
        // ====== Удаление Жанра
        private void DelTextBoxGanres_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using(db = new VinylShopContext())
            {
                GanreMusic ganre = (GanreMusic)DelListBoxGanre.SelectedItem;
                if (ganre != null)
                {
                    ganre = db.ganreMusics.Find(ganre.Id);
                    db.ganreMusics.Remove(ganre);
                    db.SaveChanges();
                    DelListBoxGanre.ItemsSource = db.ganreMusics.ToList();
                }
            }
        }

        // ====== Удаление Трека
        private void DelTextBoxSongs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using( db = new VinylShopContext())
            {
                Songs song = (Songs)DelListBoxSongs.SelectedItem;
                if(song != null)
                {
                    song = db.songs.Find(song.Id);
                    db.songs.Remove(song);
                    db.SaveChanges();
                    DelListBoxSongs.ItemsSource = db.GetListSongs(db.songs.ToList());
                }
            }
        }

        // ====== Удаление Пластинки
        private void DelTextBoxMusicRecords_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                Music_Records records = (Music_Records)DelListBoxMusicRecord.SelectedItem;
                if(records != null)
                {
                    records = db.music_Records.Find(records.Id);
                    db.music_Records.Remove(records);
                    db.SaveChanges();
                    DelListBoxMusicRecord.ItemsSource = db.GetListMusic_Records();
                }
            }
        }


        private void SearchString_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                using (db = new VinylShopContext())
                {
                    // Поиск Песен для пластинки(По жанру, по исполнителю, по названию); (Панель для добавления пластинки)
                    if (AddMusicRecordStackPanel.Visibility == Visibility.Visible) // Поиск песни по названию, по жанру, по исполнителю
                    {
                        if (ComboBoxSearchHelp.SelectedIndex == 0)
                        {
                            SongsListBoxForRecords.ItemsSource = db.GetListSongs(db.SearchSongsByName(SearchString.Text));
                        }
                        else if (ComboBoxSearchHelp.SelectedIndex == 1)
                        {
                            SongsListBoxForRecords.ItemsSource = db.GetListSongs(db.SearchSongsByGanre(SearchString.Text));
                        }
                        else if (ComboBoxSearchHelp.SelectedIndex == 2)
                        {
                            SongsListBoxForRecords.ItemsSource = db.GetListSongs(db.SearchSongsByExecutors(SearchString.Text));
                        }
                    }
                    // Поиск Исполнителей и Жанров для песни (Панель для добавления песни)
                    else if (AddSongStackPanel.Visibility == Visibility.Visible)
                    {
                        if (ComboBoxSearchHelp.SelectedIndex == 0)
                        {
                            AddComboBoxExecutorFromSong.ItemsSource = db.SearchByExecutors(SearchString.Text);
                            ListBoxAddExecutorsToSongs.ItemsSource = db.SearchByExecutors(SearchString.Text);
                        }
                        else if (ComboBoxSearchHelp.SelectedIndex == 1)
                        {
                            AddComboBoxGanreFromSong.ItemsSource = db.SearchByGanres(SearchString.Text);
                            ListBoxAddGanresToSongs.ItemsSource = db.SearchByGanres(SearchString.Text);
                        }
                    }
                    // Поиск Жанров (Панель для добавления жанра)
                    else if (AddGanreStackPanel.Visibility == Visibility.Visible)
                    {
                        GanreListBox.ItemsSource = db.SearchByGanres(SearchString.Text);
                    }
                    // Поиск Исполнителей (Панель для добавления исполнителя)
                    else if (AddExecutorStackPanel.Visibility == Visibility.Visible)
                    {
                        ExecutorsListBox.ItemsSource = db.SearchByExecutors(SearchString.Text);
                    }
                    // Поиск Издательств (Панель для добавления издательства)
                    else if (AddPublishHouseStackPanel.Visibility == Visibility.Visible)
                    {
                        PublishHouseListBox.ItemsSource = db.SearchByPublishHouse(SearchString.Text);
                    }
                    // Поиск Пластинок(По названию, по издательству); Поиск песен(По жанру, по исполнителю, по названию); (Панель для редактирования Пластинки)
                    else if (EditMusicRecordStackPanel.Visibility == Visibility.Visible)
                    {

                        if (CheckBoxEditSongs.IsChecked == true)
                        {
                            if (ComboBoxSearchHelp.SelectedIndex == 2)
                            {
                                SongsListBoxForRecordsEdit.ItemsSource = db.SearchSongsByName(SearchString.Text);
                            }
                            else if (ComboBoxSearchHelp.SelectedIndex == 3)
                            {
                                SongsListBoxForRecordsEdit.ItemsSource = db.SearchSongsByGanre(SearchString.Text);
                            }
                            else if (ComboBoxSearchHelp.SelectedIndex == 4)
                            {
                                SongsListBoxForRecordsEdit.ItemsSource = db.SearchSongsByExecutors(SearchString.Text);
                            }
                        }
                        else if (ComboBoxSearchHelp.SelectedIndex == 0)
                        {
                            AllMusicRecords.ItemsSource = db.SearchMusicRecordsByName(SearchString.Text);
                        }
                        else if (ComboBoxSearchHelp.SelectedIndex == 1)
                        {
                            AllMusicRecords.ItemsSource = db.SearchMusicRecordsByPublish(SearchString.Text);
                        }
                        else if (ComboBoxSearchHelp.SelectedIndex == 2)
                        {
                            SongsListBoxForRecordsEdit.ItemsSource = db.GetListSongs(db.SearchMusicRecordsSongByName(indxMusicRecordHelp, SearchString.Text));
                        }
                        else if (ComboBoxSearchHelp.SelectedIndex == 3)
                        {
                            SongsListBoxForRecordsEdit.ItemsSource = db.GetListSongs(db.SearchMusicRecordsSongByGanre(indxMusicRecordHelp, SearchString.Text));
                        }
                        else if (ComboBoxSearchHelp.SelectedIndex == 4)
                        {
                            SongsListBoxForRecordsEdit.ItemsSource = db.GetListSongs(db.SearchMusicRecordsSongByExecutor(indxMusicRecordHelp, SearchString.Text));
                        }
                    }
                    // Поиск Песен, Жанра и Исполнителей (Панель для редактирования песен)
                    else if(EditSongsStackPanel.Visibility == Visibility.Visible) 
                    {
                        if(ComboBoxSearchHelp.SelectedIndex == 0)
                        {
                            AllSongsListBoxEdit.ItemsSource = db.GetListSongs(db.SearchSongsByName(SearchString.Text));
                        }
                        else if(ComboBoxSearchHelp.SelectedIndex == 1)
                        {
                            AllSongsListBoxEdit.ItemsSource = db.GetListSongs(db.SearchSongsByGanre(SearchString.Text));
                        }
                        else if(ComboBoxSearchHelp.SelectedIndex == 2)
                        {
                            AllSongsListBoxEdit.ItemsSource = db.GetListSongs(db.SearchSongsByExecutors(SearchString.Text));
                        }
                        else if (ComboBoxSearchHelp.SelectedIndex == 3)
                        {
                            FewExecutorsComboBoxEdit.ItemsSource = db.SearchByExecutors(SearchString.Text);
                        }
                        else if (ComboBoxSearchHelp.SelectedIndex == 4)
                        {
                            FewGanresComboBoxEdit.ItemsSource = db.SearchByGanres(SearchString.Text);
                        }
                    }
                    // Поиск Жанра (Панель для редактирования жанра)
                    else if(EditGanreStackPanel.Visibility == Visibility.Visible)
                    {
                        GareListBox.ItemsSource = db.SearchByGanres(SearchString.Text);
                    }
                    // Поиск Исполнителя (Панель для редактирования исполнителя)
                    else if (EditExecutorStackPanel.Visibility == Visibility.Visible)
                    {
                        ExecutorListBox.ItemsSource = db.SearchByExecutors(SearchString.Text);
                    }
                    // Поиск Издательства (Панель для редактирования издательства)
                    else if (EditPublishHouesStackPanel.Visibility == Visibility.Visible)
                    {
                        PublishHousesListBox.ItemsSource = db.SearchByPublishHouse(SearchString.Text);
                    }
                    // Поиск Пластинок (Панель для удаления пластинки)
                    else if(DelMusicRecordStackPanel.Visibility == Visibility.Visible)
                    {
                        if(ComboBoxSearchHelp.SelectedIndex == 0)
                        {
                            DelListBoxMusicRecord.ItemsSource = db.SearchMusicRecordsByName(SearchString.Text);
                        }
                        else if(ComboBoxSearchHelp.SelectedIndex == 1)
                        {
                            DelListBoxMusicRecord.ItemsSource = db.SearchMusicRecordsByPublish(SearchString.Text);
                        }
                    }
                    // Поиск Песни (Панель для удаления песни)
                    else if(DelSongsStackPanel.Visibility == Visibility.Visible)
                    {
                        if (ComboBoxSearchHelp.SelectedIndex == 0)
                        {
                            DelListBoxSongs.ItemsSource = db.GetListSongs(db.SearchSongsByName(SearchString.Text));
                        }
                        else if (ComboBoxSearchHelp.SelectedIndex == 1)
                        {
                            DelListBoxSongs.ItemsSource = db.GetListSongs(db.SearchSongsByGanre(SearchString.Text));
                        }
                        else if (ComboBoxSearchHelp.SelectedIndex == 2)
                        {
                            DelListBoxSongs.ItemsSource = db.GetListSongs(db.SearchSongsByExecutors(SearchString.Text));
                        }
                    }
                    // Поиск Жанра (Панель для удаления жанра)
                    else if(DelGanreStackPanel.Visibility == Visibility.Visible)
                    {
                        DelListBoxGanre.ItemsSource = db.SearchByGanres(SearchString.Text);
                    }
                    // Поиск Издательства (Панель для удаления издательства)
                    else if (DelPublishHouseStackPanel.Visibility == Visibility.Visible)
                    {
                        DelListBoxPublishHoues.ItemsSource = db.SearchByPublishHouse(SearchString.Text);
                    }
                    // Поиск исполнителя (Панель для удаления исполнителя)
                    else if (DelExecutorStackPanel.Visibility == Visibility.Visible)
                    {
                        DelListBoxExecutor.ItemsSource = db.SearchByExecutors(SearchString.Text);
                    }
                }
            }
        }


        Music_Records recordHelpForReturnList = new Music_Records(); // Вспомогательное поле для работы с песнями
        // Возвращает обычное состояние до поиска
        private void SearchString_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                if (SearchString.Text == string.Empty)
                {
                    if (AddMusicRecordStackPanel.Visibility == Visibility.Visible)
                    {
                        SongsListBoxForRecords.ItemsSource = db.GetListSongs(db.songs.ToList());
                    }
                    else if (AddSongStackPanel.Visibility == Visibility.Visible)
                    {
                        AddComboBoxExecutorFromSong.ItemsSource = db.executorMusics.ToList();
                        ListBoxAddExecutorsToSongs.ItemsSource = db.executorMusics.ToList();
                        AddComboBoxGanreFromSong.ItemsSource = db.ganreMusics.ToList();
                        ListBoxAddGanresToSongs.ItemsSource = db.ganreMusics.ToList();
                    }
                    else if (AddGanreStackPanel.Visibility == Visibility.Visible)
                    {
                        GanreListBox.ItemsSource = db.ganreMusics.ToList();
                    }
                    else if (AddExecutorStackPanel.Visibility == Visibility.Visible)
                    {
                        ExecutorsListBox.ItemsSource = db.executorMusics.ToList();
                    }
                    else if (AddPublishHouseStackPanel.Visibility == Visibility.Visible)
                    {
                        PublishHouseListBox.ItemsSource = db.pubishHouses.ToList();
                    }
                    else if (EditMusicRecordStackPanel.Visibility == Visibility.Visible)
                    {
                        if (CheckBoxEditSongs.IsChecked == true)
                            SongsListBoxForRecordsEdit.ItemsSource = db.GetListSongs();
                        else
                            SongsListBoxForRecordsEdit.ItemsSource = recordHelpForReturnList.songs.ToList();

                        AllMusicRecords.ItemsSource = db.GetListMusic_Records();
                    }
                    else if (EditSongsStackPanel.Visibility == Visibility.Visible)
                    {
                        AllSongsListBoxEdit.ItemsSource = db.GetListSongs(db.songs.ToList());
                        FewExecutorsComboBoxEdit.ItemsSource = db.executorMusics.ToList();
                        FewGanresComboBoxEdit.ItemsSource = db.ganreMusics.ToList();
                    }
                    else if (EditGanreStackPanel.Visibility == Visibility.Visible)
                    {
                        GareListBox.ItemsSource = db.ganreMusics.ToList();
                    }
                    else if (EditExecutorStackPanel.Visibility == Visibility.Visible)
                    {
                        ExecutorListBox.ItemsSource = db.executorMusics.ToList();
                    }
                    else if (EditPublishHouesStackPanel.Visibility == Visibility.Visible)
                    {
                        PublishHousesListBox.ItemsSource = db.pubishHouses.ToList();
                    }
                    else if (DelMusicRecordStackPanel.Visibility == Visibility.Visible)
                    {
                        DelListBoxMusicRecord.ItemsSource = db.GetListMusic_Records();
                    }
                    else if (DelSongsStackPanel.Visibility == Visibility.Visible)
                    {
                        DelListBoxSongs.ItemsSource = db.GetListSongs(db.songs.ToList());
                    }
                    else if (DelGanreStackPanel.Visibility == Visibility.Visible)
                    {
                        DelListBoxGanre.ItemsSource = db.ganreMusics.ToList();
                    }
                    else if (DelPublishHouseStackPanel.Visibility == Visibility.Visible)
                    {
                        DelListBoxPublishHoues.ItemsSource = db.pubishHouses.ToList();
                    }
                    else if (DelExecutorStackPanel.Visibility == Visibility.Visible)
                    {
                        DelListBoxExecutor.ItemsSource = db.executorMusics.ToList();
                    }
                }
            }
        }

        int HelperForSell = 0;
        private void AddSellDay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                double temp = 0;
                DateTime SDate = new DateTime(int.Parse(yyStart.Text), int.Parse(mmStart.Text), int.Parse(ddStart.Text));
                DateTime EDate = new DateTime(int.Parse(yyEnd.Text), int.Parse(mmEnd.Text), int.Parse(ddEnd.Text));
                if (SDate > EDate)
                {
                    MessageBox.Show("Неправильно введена дата");
                    return;
                }
                if (!double.TryParse(AmountSellTextBox.Text, out temp))
                {
                    MessageBox.Show("Неверно введена скидка");
                    return;
                }
                if (HelperForSell == 0)
                {
                    SellsDay day = new SellsDay();
                    day.startDayForSell = SDate;
                    day.endDayForSell = EDate;
                    GanreMusic ganre = (GanreMusic)GanreComboBoxForSell.SelectedItem;
                    day.ganre = db.ganreMusics.Find(ganre.Id);
                    day.Sell = temp;
                    db.sellsDay.Add(day);
                    db.SaveChanges();
                    MessageBox.Show("Успешно добавлено");
                    ddEnd.Text = string.Empty; ddStart.Text = string.Empty;
                    mmEnd.Text = string.Empty; mmStart.Text = string.Empty;
                    yyEnd.Text = string.Empty; yyStart.Text = string.Empty;
                    GanreComboBoxForSell.Text = string.Empty;
                    AmountSellTextBox.Text = string.Empty;
                }
                else if(HelperForSell == 1)
                {
                    SellsDay day = db.sellsDay.Find(indxHelpSellsDay);
                    day.startDayForSell = SDate;
                    day.endDayForSell = EDate;
                    GanreMusic ganre = (GanreMusic)GanreComboBoxForSell.SelectedItem;
                    day.ganre = db.ganreMusics.Find(ganre.Id);
                    day.Sell = temp;
                    db.SaveChanges();
                    MessageBox.Show("Успешно сохранено");
                    ListAllSellsDay.ItemsSource = db.GetListSellsDay();
                }
            }
        }
        
        private void DelSellsDay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                SellsDay day = (SellsDay)DelListBoxSellsDay.SelectedItem;
                db.sellsDay.Remove(db.sellsDay.Find(day.Id));
                db.SaveChanges();
                DelListBoxSellsDay.ItemsSource = db.GetListSellsDay();
            }
        }

        int indxHelpSellsDay = 0;
        private void ListAllSellsDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                SellsDay day = (SellsDay)ListAllSellsDay.SelectedItem;
                if (day != null)
                {
                    day = db.sellsDay.Find(day.Id);
                    indxHelpSellsDay = day.Id;
                    MessageBox.Show(day.ToString());
                    ddStart.Text = day.startDayForSell.Day.ToString();
                    mmStart.Text = day.startDayForSell.Month.ToString();
                    yyStart.Text = day.startDayForSell.Year.ToString();
                    ddEnd.Text = day.endDayForSell.Day.ToString();
                    mmEnd.Text = day.endDayForSell.Month.ToString();
                    yyEnd.Text = day.endDayForSell.Year.ToString();
                    GanreComboBoxForSell.Text = day.ganre.Name.ToString();
                    AmountSellTextBox.Text = day.Sell.ToString();
                    
                }
            }
        }
    }
}