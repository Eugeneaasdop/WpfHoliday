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
    /// Логика взаимодействия для PageAddJuri.xaml
    /// </summary>
    public partial class PageAddJuri : Page
    {
        private Juri _currentItem = new Juri();
        public PageAddJuri(Juri selectedItem)
        {
            InitializeComponent();
            if (selectedItem != null)
            {
                _currentItem = selectedItem;
                Titletxt.Text = "Изменение жюри";
                BtnAdd.Content = "Изменить";
            }
            DataContext = _currentItem;
            CMBCountry.ItemsSource = HolidayEntities.GetContext().Country.ToList();
            CMBCountry.SelectedValuePath = "IdCountry";
            CMBCountry.DisplayMemberPath = "NameCountry";
            CMBDiric.ItemsSource = HolidayEntities.GetContext().Dirictory.ToList();
            CMBDiric.SelectedValuePath = "IdDirictory";
            CMBDiric.DisplayMemberPath = "NameDir";
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.IdCountry))) error.AppendLine("Укажите страну");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.IdDirictory))) error.AppendLine("Укажите направление");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.Email))) error.AppendLine("Укажите почту");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.DateBirthday))) error.AppendLine("Укажите день рождения");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.Password))) error.AppendLine("Укажите пароль");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.Phone))) error.AppendLine("Укажите телефон");
            if (HolidayEntities.GetContext().Juri.Where(x => x.Country.NameCountry.ToString() == CMBDiric.Text && x.Dirictory.NameDir == CMBDiric.Text && x.Email == TxtEmail.Text && x.DateBirthday.ToString() == datePicker.Text && x.Password == TxtPassword.Text && x.Phone.ToString() == Txtphone.Text).Count() > 0)
            {
                MessageBox.Show("Такой жюри уже есть");
                return;
            }
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            if (_currentItem.IdJuri == 0)
            {
                HolidayEntities.GetContext().Juri.Add(_currentItem);
                try
                {
                    HolidayEntities.GetContext().SaveChanges();
                    ClassFrame.frmObj.Navigate(new PageJuri());
                    MessageBox.Show("Новый жюри успешно добавлен!");
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
                    ClassFrame.frmObj.Navigate(new PageJuri());
                    MessageBox.Show("Жюри успешно изменён!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageJuri());
        }
    }
}
