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
    /// Логика взаимодействия для PageAddActiv.xaml
    /// </summary>
    public partial class PageAddActiv : Page
    {
        private Activity _currentItem = new Activity();
        public PageAddActiv(Activity selectedItem)
        {
            InitializeComponent();
            if (selectedItem != null)
            {
                _currentItem = selectedItem;
                Titletxt.Text = "Изменение событьия";
                BtnAdd.Content = "Изменить";
            }
            DataContext = _currentItem;
            CMBEvent.ItemsSource = HolidayEntities.GetContext().Event.ToList();
            CMBEvent.SelectedValuePath = "IdEvent";
            CMBEvent.DisplayMemberPath = "NameEvent";
            CMBJuri.ItemsSource = HolidayEntities.GetContext().Juri.ToList();
            CMBJuri.SelectedValuePath = "IdJuri";
            CMBJuri.DisplayMemberPath = "Email";
            CMBModer.ItemsSource = HolidayEntities.GetContext().Moderator.ToList();
            CMBModer.SelectedValuePath = "IdModerator";
            CMBModer.DisplayMemberPath = "FIO";
            CMBPerson.ItemsSource = HolidayEntities.GetContext().Person.ToList();
            CMBPerson.SelectedValuePath = "IdPerson";
            CMBPerson.DisplayMemberPath = "FFioIO";
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder error = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.IdModerator))) error.AppendLine("Укажите модератора");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.IdJuri))) error.AppendLine("Укажите жюри");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.Days))) error.AppendLine("Укажите дни");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.ActivName))) error.AppendLine("Укажите доп. событие");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.Time))) error.AppendLine("Укажите время");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.IdPerson))) error.AppendLine("Укажите победителя");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.IdEvent))) error.AppendLine("Укажите событьие");

            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            if (_currentItem.IdActivity == 0)
            {
                HolidayEntities.GetContext().Activity.Add(_currentItem);
                try
                {
                    HolidayEntities.GetContext().SaveChanges();
                    ClassFrame.frmObj.Navigate(new PageActiv());
                    MessageBox.Show("Новое событие успешно добавлен!");
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
                    ClassFrame.frmObj.Navigate(new PageActiv());
                    MessageBox.Show("Событие успешно изменён!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageActiv());
        }
    }
}
