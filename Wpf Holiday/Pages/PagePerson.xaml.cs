using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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
    /// Логика взаимодействия для PagePerson.xaml
    /// </summary>
    public partial class PagePerson : Page
    {
        public PagePerson()
        {
            InitializeComponent();
            dtgPerson.ItemsSource = HolidayEntities.GetContext().Person.ToList();
        }

        private void MenuAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageAddPerson(null));
        }

        private void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageAddPerson((Person)dtgPerson.SelectedItem));
        }

        private void MenuUpdate_Click(object sender, RoutedEventArgs e)
        {
            dtgPerson.ItemsSource = HolidayEntities.GetContext().Person.ToList();
        }

        private void MenuDel_Click(object sender, RoutedEventArgs e)
        {
            var salesForRemoving = dtgPerson.SelectedItems.Cast<Person>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {salesForRemoving.Count()} записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    HolidayEntities.GetContext().Person.RemoveRange(salesForRemoving);
                    HolidayEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    dtgPerson.ItemsSource = HolidayEntities.GetContext().Person.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void MenuGlav_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageMainMenu());
        }
    }
}
