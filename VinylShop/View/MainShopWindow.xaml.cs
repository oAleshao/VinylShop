﻿using System;
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
        public Users users { get; set; }



        public MainShopWindow()
        {
            InitializeComponent();
            ChangeTextAboutSell();
            using (db = new VinylShopContext())
            {
                AllMusicRecordsInShopListBox.ItemsSource = db.GetListMusic_Records();
            }

        }

        //Инициализация пользователя
        public void SetUser(Users user)
        {
            using (db = new VinylShopContext())
            {
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
            if (BorderBackHelp == 1)
                BorderBackHelp = 3;
            else
                BorderBackHelp = 1;
            using (db = new VinylShopContext())
            {
                Music_Records music_Records = (Music_Records)AllMusicRecordsInShopListBox.SelectedItem;
                if (music_Records != null)
                {
                    IsAllStackPanelVisibilityFalse();
                    UserChoosedMusicRecordsStackPanel.Visibility = Visibility.Visible;
                    AddToShopBagTextBlock.Visibility = Visibility.Visible;
                    music_Records = db.music_Records.Find(music_Records.Id);
                    SetAllFealdsForMusicRecords(music_Records);
                }

            }
        }

        int BorderBackHelp = 0;
        /// 
        /// BorderBackHelp бордек как кнопка которая возвращает на предыдущую панель
        /// 0 - главное окно
        /// 1 - характеристики пластинок, найсторки пользователя, выход в основное меню
        /// 2 - корзина 
        /// 3 - Топ исполнителей
        /// 4 - Топ жанров
        /// 
        //Кнопка назад
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                if (BorderBackHelp == 0)
                    return;

                IsAllStackPanelVisibilityFalse();
                if (BorderBackHelp == 1)
                {
                    BorderBackHelp = 0;
                    AllMusicRecordsInShopListBox.ItemsSource = db.GetListMusic_Records();
                    AllMusicRecordsStackPanel.Visibility = Visibility.Visible;
                }
                else if (BorderBackHelp == 2)
                {
                    BorderBackHelp = 1;
                    BagShopStackPanel.Visibility = Visibility.Visible;
                }
                else if (BorderBackHelp == 3)
                {
                    BorderBackHelp = 1;
                    AllMusicRecordsStackPanel.Visibility = Visibility.Visible;

                }
            }
        }

        //Скрытие всех стек панелей
        public void IsAllStackPanelVisibilityFalse()
        {
            UserChoosedMusicRecordsStackPanel.Visibility = Visibility.Hidden;
            AllMusicRecordsStackPanel.Visibility = Visibility.Hidden;
            UserSettingsStackPanel.Visibility = Visibility.Hidden;
            BagShopStackPanel.Visibility = Visibility.Hidden;
            AddToShopBagTextBlock.Visibility = Visibility.Hidden;
            DelFromBagAndAddToOrderStackPanel.Visibility = Visibility.Hidden;
            MakeOrderStackPanel.Visibility = Visibility.Hidden;
            HistoryOrderStackPanel.Visibility = Visibility.Hidden;

        }

        //Видимость пароля
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

        //Закрытие формы
        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        //Настройки аккаунта
        private void Settings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IsAllStackPanelVisibilityFalse();
            BorderBackHelp = 1;
            UserSettingsStackPanel.Visibility = Visibility.Visible;

        }

        //Открытие вкладки корзины
        private void ShopBag_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                IsAllStackPanelVisibilityFalse();
                BorderBackHelp = 1;
                BagShopStackPanel.Visibility = Visibility.Visible;
                ListRecordsForShop.ItemsSource = db.GetListMusicRecordsForShopBag(users.Id);
            }

        }

        //Добавление пластинки в корзину
        private void AddToShopBag_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                ShopBag bag = new ShopBag();
                bag.IdUser = db.users.Find(users.Id);
                bag.music_Records = db.music_Records.Find(indxHelpRecord);

                if (db.AddMusicRecordsToShopBag(bag, users))
                    MessageBox.Show("Добавлено в корзину");
                else
                    MessageBox.Show("Этот товар уже есть в корзине");

            }

        }



        private void ListRecordsForShopShowRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                Music_Records records = (Music_Records)ListRecordsForShop.SelectedItem;
                if (records != null)
                {
                    BorderBackHelp = 2;
                    records = db.music_Records.Find(records.Id);
                    IsAllStackPanelVisibilityFalse();
                    UserChoosedMusicRecordsStackPanel.Visibility = Visibility.Visible;
                    DelFromBagAndAddToOrderStackPanel.Visibility = Visibility;
                    if (records.Quantity > 0)
                        CountTextBlock.Text = "1";
                    else
                        CountTextBlock.Text = "0";
                    SetAllFealdsForMusicRecords(records);
                }
            }
        }

        //Заполнение всех полей для характеристики пластинки
        private void SetAllFealdsForMusicRecords(Music_Records records)
        {
            indxHelpRecord = records.Id;
            NameRecordTextBox.Text = records.Name;
            PublishHouseNameTextBox.Text = records.pubishHouse.Name;
            CountSongsTextBox.Text = "Кол-во треков: " + records.CountSongs.ToString();
            PriceTextBox.Text = "Цена: " + records.Price.ToString();
            YearTextBox.Text = "Год: " + records.Year.ToString();
            DescriptionTextBox.Text = records.Description.ToString();
            records.songs = db.GetListSongs(records.songs.ToList());
            ListSongs.ItemsSource = records.songs;
        }

        //Удаление пластинки из корзины
        private void DelFromShopBag_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                db.DelMusicRecordFromShopBag(db.music_Records.Find(indxHelpRecord), users);
                BorderBackHelp = 2;
                ListRecordsForShop.ItemsSource = db.GetListMusicRecordsForShopBag(users.Id);
                Border_MouseDown(sender, e);
            }
        }

        List<HelpOrders> HelpOrders = new List<HelpOrders>();
        private void AddToOrder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                if (db.music_Records.Find(indxHelpRecord).Quantity == 0)
                {
                    MessageBox.Show("К сожалению данного товара больше нету");
                    return;
                }
                Music_Records records = db.music_Records.Find(indxHelpRecord);
                HelpOrders helper = new HelpOrders();
                bool CheckIsList = true;
                helper.Quantity = int.Parse(CountTextBlock.Text);
                helper.music_Records = records;

                foreach (var item in HelpOrders)
                {
                    if (db.CompareToMusicRecords(db.music_Records.Find(item.music_Records.Id), records))
                    {
                        item.Quantity += helper.Quantity;
                        CheckIsList = false;
                    }
                }
                if (CheckIsList)
                {
                    HelpOrders.Add(helper);
                }
                MessageBox.Show("Добавлено в заказы");
            }
        }

        //Сохранение данных пользователя
        private void SaveUserData_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BorderCanSee_MouseDown(sender, e);
            using (db = new VinylShopContext())
            {
                users = db.users.Find(users.Id);
                users.Login = LoginTextBox.Text;
                users.Email = EmailTextBox.Text;
                users.Adres = AdressTextBox.Text;
                users.Password = UserPassword.Text;
                users.PhoneNumber = PhoneTextBox.Text;
                users.FullName = FullNameTextBox.Text;
                db.SaveChanges();
                SetUser(users);

            }

        }

        private void PlusCountRecords_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                int count = int.Parse(CountTextBlock.Text);
                Music_Records records = db.music_Records.Find(indxHelpRecord);
                if (count < records.Quantity)
                {
                    count++;
                    CountTextBlock.Text = count.ToString();
                }
            }
        }
        private void MinusCountRecords_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                int count = int.Parse(CountTextBlock.Text);
                Music_Records records = db.music_Records.Find(indxHelpRecord);
                if (count > 1)
                {
                    count--;
                    CountTextBlock.Text = count.ToString();
                }
            }
        }

        //Оформление заказа
        private void MakeOrder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                double TotalPriceAllOrders = 0;
                List<Music_Records> knowAboutSell = db.SearchMusicRecordsByGanre(db.ganreMusics.Find(IdGanreForSell).Name);
                List<Orders> list = db.orders.Where(x => x.Iduser.Id == users.Id).ToList();
                if (list.Count != 0)
                    TotalPriceAllOrders = db.orders.Where(x => x.Iduser.Id == users.Id).Sum(x => x.TotalPrice);

                IsAllStackPanelVisibilityFalse();
                MakeOrderStackPanel.Visibility = Visibility.Visible;
                BorderBackHelp = 2;
                users = db.users.Find(users.Id);
                FullNameOrderTextBox.Text = users.FullName;
                PhoneOrderTextBox.Text = users.PhoneNumber;
                AdressOrderTextBox.Text = users.Adres;
                EmailOrderTextBox.Text = users.Email;
                double temp = 0;
                bool helper = true;
                bool helperForPersent = false;
                if (TotalPriceAllOrders > 15000)
                {
                    PriceSellTextBox.Text = "Скидка 10%";
                    helperForPersent = true;
                }
                foreach (var price in HelpOrders)
                {
                    foreach (var Mrecord in knowAboutSell)
                    {
                        if (price.music_Records.Id == Mrecord.Id)
                        {
                            MessageBox.Show(Mrecord.ToString());
                            temp += price.music_Records.Price * price.Quantity - ((price.music_Records.Price * price.Quantity) * (AmountSellToday / 100));
                            helper = false;
                            if (helperForPersent)
                            {
                                PriceSellTextBox.Text = $"Скидка 10% + {AmountSellToday}% на жанр {db.ganreMusics.Find(IdGanreForSell).Name}";
                            }
                            else
                            {
                                PriceSellTextBox.Text = $"Скидка {AmountSellToday}% на жанр {db.ganreMusics.Find(IdGanreForSell).Name}";
                            }
                            break;
                        }
                    }
                    if (helper)
                    {
                        temp += price.music_Records.Price * price.Quantity;
                        MessageBox.Show(temp.ToString());
                        continue;
                    }
                    MessageBox.Show(temp.ToString());
                    helper = true;
                }
                if (TotalPriceAllOrders > 15000)
                    temp = temp - temp * 0.10;

                TotalPriceTextBox.Text = "Общая стоимость: " + temp.ToString() + " грн";
                ListOrders.ItemsSource = null;
                ListOrders.ItemsSource = HelpOrders;
            }

        }

        //Сделанный заказ
        private void MadeOrder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                if (AdressOrderTextBox.Text == string.Empty)
                {
                    AdressOrderTextBox.BorderBrush = Brushes.Red;
                    AdressOrderTextBox.ToolTip = "Обязательное поле";
                    return;
                }
                Orders newOrder = new Orders();
                newOrder.dateOrder = DateTime.Now;
                foreach (var records in HelpOrders)
                {
                    records.music_Records = db.music_Records.Find(records.music_Records.Id);
                    newOrder.TotalPrice += records.music_Records.Price * records.Quantity;
                    records.music_Records.Quantity -= records.Quantity;
                    newOrder.helpOrders_.Add(records);
                }
                newOrder.Iduser = db.users.Find(users.Id);
                db.orders.Add(newOrder);
                db.SaveChanges();
                HelpOrders.Clear();
                ListOrders.ItemsSource = null;
                MessageBox.Show("Заказ успешно оформлен");
                BorderBackHelp = 1;
                PriceSellTextBox.Text = string.Empty;
                Border_MouseDown(sender, e);
            }
        }

        private void ListBoxHistoryOrdered_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                Orders order = (Orders)ListBoxHistoryOrdered.SelectedItem;
                if (order != null)
                {
                    order = db.orders.Find(order.Id);
                    DateOrder.Text = order.dateOrder.ToString();
                    TotalPrice.Text = order.TotalPrice.ToString();
                    ListRecordsInHistory.ItemsSource = db.GetListHelpOrders(order);
                }
            }
        }

        private void HistoryOrdered_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                IsAllStackPanelVisibilityFalse();
                HistoryOrderStackPanel.Visibility = Visibility.Visible;
                BorderBackHelp = 1;
                ListBoxHistoryOrdered.ItemsSource = db.GetListOrders(users);
            }
        }

        private void NewRecordsInShop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                BorderBackHelp = 1;
                AllMusicRecordsInShopListBox.ItemsSource = db.Get5NewMusicRecords();
            }
        }

        private void TopSell_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                BorderBackHelp = 1;
                AllMusicRecordsInShopListBox.ItemsSource = db.GetTopSellMusicRecords();
            }
        }

        private void TopExecutors_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                BorderBackHelp = 1;
                AllMusicRecordsInShopListBox.ItemsSource = db.GetTopExecutors();
            }
        }

        private void TopGanre_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                BorderBackHelp = 1;
                AllMusicRecordsInShopListBox.ItemsSource = db.GetTopGanres();
            }
        }




        private void SearchGlass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Search();
        }

        private void SearchString_KeyDown(object sender, KeyEventArgs e)
        {
            if(Key.Enter == e.Key)
            {
                Search();
            }
        }

        private void Search()
        {
            if (SearchString.Text == string.Empty)
                return;
            using (db = new VinylShopContext())
            {
                BorderBackHelp = 1;
                if (SearchName.IsChecked == true)
                {
                    AllMusicRecordsInShopListBox.ItemsSource = db.SearchMusicRecordsByName(SearchString.Text);
                }
                else if(SearchPublish.IsChecked == true)
                {
                    AllMusicRecordsInShopListBox.ItemsSource = db.SearchMusicRecordsByPublish(SearchString.Text);
                }
                else if(SearchExecutor.IsChecked == true)
                {
                    AllMusicRecordsInShopListBox.ItemsSource = db.SearchMusicRecordsByExecutor(SearchString.Text);
                }
                else if(SearchGanre.IsChecked == true)
                {
                    AllMusicRecordsInShopListBox.ItemsSource = db.SearchMusicRecordsByGanre(SearchString.Text);
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                users = db.users.Find(users.Id);
                users.Adres = AdressOrderTextBox.Text;
                db.SaveChanges();
                SetUser(users);
            }
        }

        private void UserPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UserPassword.Text = UserPasswordBox.Password;
        }

        int IdGanreForSell = 0;
        double AmountSellToday = 0;
        private void ChangeTextAboutSell()
        {
            using (db = new VinylShopContext())
            {
                var temp = db.sellsDay.Where(x => x.startDayForSell.Day == DateTime.Now.Day &&
                x.startDayForSell.Month == DateTime.Now.Month && x.startDayForSell.Year == DateTime.Now.Year).Take(1);
                SellsDay day = temp.FirstOrDefault();
                if (day != null)
                {
                    IdGanreForSell = day.ganre.Id;
                    AmountSellToday = day.Sell;
                    TextAboutSells.Text = $"С {day.startDayForSell.ToShortDateString()} по {day.endDayForSell.ToShortDateString()} акция {day.Sell}% на жанр {day.ganre.Name}!!!";
                }
            }
        }

        private void DelFromOrder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (db = new VinylShopContext())
            {
                foreach(var helpOrder in ListOrders.SelectedItems)
                {
                    DelHelpOrdersFromList(ref HelpOrders, (HelpOrders)helpOrder);
                }
                ListOrders.ItemsSource = null;
                MakeOrder_MouseDown(sender, e);
            }

        }

        private void DelHelpOrdersFromList(ref List<HelpOrders> listHelps, HelpOrders help)
        {
            using (db = new VinylShopContext())
            {
                foreach (var helpOrder in listHelps)
                {
                    if(db.CompareToHelpOrders(helpOrder, help))
                    {
                        listHelps.Remove(helpOrder);
                        break;
                    }
                }
            }
        }
    }
}
