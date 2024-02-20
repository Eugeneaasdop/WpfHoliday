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
using Wpf_Holiday.Classes;

namespace Wpf_Holiday.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddModer.xaml
    /// </summary>
    public partial class PageAddModer : Page

    {
        private Moderator _currentItem = new Moderator();
        public PageAddModer(Moderator selectedItem)
        
        {
            InitializeComponent();
            if (selectedItem != null)
            {
                _currentItem = selectedItem;
                Titletxt.Text = "Изменение организатора";
                BtnAdd.Content = "Изменить";
            }
            DataContext = _currentItem;
            CMBCountry.ItemsSource = HolidayEntities.GetContext().Country.ToList();
            CMBCountry.SelectedValuePath = "IdCountry";
            CMBCountry.DisplayMemberPath = "NameCountry";
            CMBCountry.ItemsSource = HolidayEntities.GetContext().Dirictory.ToList();
            CMBCountry.SelectedValuePath = "IdDirictory";
            CMBCountry.DisplayMemberPath = "NameDir";
            CMBCountry.ItemsSource = HolidayEntities.GetContext().Eventmoder.ToList();
            CMBCountry.SelectedValuePath = "IdEventmoder";
            CMBCountry.DisplayMemberPath = "NameMod";


        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.IdCountry))) error.AppendLine("Укажите страну");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.FIO))) error.AppendLine("Укажите ФИО");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.Email))) error.AppendLine("Укажите почту");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.DateBirthday))) error.AppendLine("Укажите день рождения");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.Password))) error.AppendLine("Укажите пароль");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.Phone))) error.AppendLine("Укажите телефон");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.Pol))) error.AppendLine("Укажите пол");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.IdDirictory))) error.AppendLine("Укажите направление");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.IdEventmoder))) error.AppendLine("Укажите событие модератора");
            if (HolidayEntities.GetContext().Organizer.Where(x => x.Country.NameCountry.ToString() == CMBCountry.Text && x.Fio == Txtfio.Text && x.Email == TxtEmail.Text && x.DateBirthday.ToString() == datePicker.Text && x.Password == TxtPassword.Text && x.Pol.ToString() == TxtPol.Text).Count() > 0)
            {
                MessageBox.Show("Такой модератор уже есть");
                return;
            }
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            if (_currentItem.IdModerator == 0)
            {
                HolidayEntities.GetContext().Moderator.Add(_currentItem);
                try
                {
                    HolidayEntities.GetContext().SaveChanges();
                    ClassFrame.frmObj.Navigate(new PageModer());
                    MessageBox.Show("Новый модератор успешно добавлен!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                try
                {
                    HolidayEntities.GetContext().SaveChanges();
                    ClassFrame.frmObj.Navigate(new PageModer());
                    MessageBox.Show("Модератор успешно изменён!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageModer());
        }
    }
}

