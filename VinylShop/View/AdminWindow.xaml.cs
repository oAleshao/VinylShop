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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        VinylShopContext db = null;
        public AdminWindow()
        {
            InitializeComponent();
            AddComboBoxItems();
            IsVisibilityFalseAllStackPanel();
            BackgroundFone.Visibility = Visibility.Visible;
            using (db = new VinylShopContext())
            {
                AllMusicRecords.ItemsSource = db.users.ToList();
               // TEst.ItemsSource = db.users.ToList();

            }
        }

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

        private void ListMy_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

        }

        private void CloseWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        //Выборка стек панелей через комбо бокс (Добавление)
        private void ComboBoxAdd_Selected(object sender, SelectionChangedEventArgs e)
        {
            IsVisibilityFalseAllStackPanel();
            switch (ComboBoxAdd.SelectedIndex)
            {
                case 0:
                    AddMusicRecordStackPanel.Visibility = Visibility.Visible;
                    break;
                case 1:
                    AddSongStackPanel.Visibility = Visibility.Visible;
                    break;
                case 2:
                    AddGanreStackPanel.Visibility = Visibility.Visible;
                    break;
                case 3:
                    AddPublishHouseStackPanel.Visibility = Visibility.Visible;
                    break;
                case 4:
                    AddExecutorStackPanel.Visibility = Visibility.Visible;
                    break;

            }
            

        }

        //Все стек панели прячет
        private void IsVisibilityFalseAllStackPanel()
        {
            BackgroundFone.Visibility = Visibility.Hidden;
            AddMusicRecordStackPanel.Visibility = Visibility.Hidden;
            AddSongStackPanel.Visibility= Visibility.Hidden;
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



        // Когда чек бокс включен
        private void CheckBoxEditSongs_IsEnabledChanged(object sender, RoutedEventArgs e)
        {
            AddOrDelStackPanel.Visibility = Visibility.Visible;
        }

        // Когда чек бокс выключен
        private void CheckBoxEditSongs_IsEnabledChanged_1(object sender, RoutedEventArgs e)
        {
            AddOrDelStackPanel.Visibility = Visibility.Hidden;
        }

        //Выборка стек панелей через комбо бокс (Редактирование)
        private void ComboBoxEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsVisibilityFalseAllStackPanel();
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
                    break;
                case 4:
                    EditExecutorStackPanel.Visibility = Visibility.Visible;
                    break;
            }
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


        private void SettingsAdmin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IsVisibilityFalseAllStackPanel();
            AdminsStackPanel.Visibility = Visibility.Visible;
        }

        private void BorderCanNotSee_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CanNotSeeStackPanel.Visibility = Visibility.Hidden;
            CanSeeStackPanel.Visibility = Visibility.Visible;
        }

        private void BorderCanSee_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CanSeeStackPanel.Visibility = Visibility.Hidden;
            CanNotSeeStackPanel.Visibility = Visibility.Visible;
        }
    }
}
