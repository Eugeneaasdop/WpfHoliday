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
    /// Логика взаимодействия для PageOrganiz.xaml
    /// </summary>
    public partial class PageOrganiz : Page
    {
        public PageOrganiz()
        {
            InitializeComponent();
            dtgOrganiz.ItemsSource = HolidayEntities.GetContext().Organizer.ToList();
        }

        private void MenuAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageAddOrgniz(null));
        }

        private void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageAddOrgniz((Organizer)dtgOrganiz.SelectedItem));
        }

        private void MenuUpdate_Click(object sender, RoutedEventArgs e)
        {
            dtgOrganiz.ItemsSource = HolidayEntities.GetContext().Person.ToList();
        }

        private void MenuDel_Click(object sender, RoutedEventArgs e)
        {
            var salesForRemoving = dtgOrganiz.SelectedItems.Cast<Organizer>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {salesForRemoving.Count()} записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    HolidayEntities.GetContext().Organizer.RemoveRange(salesForRemoving);
                    HolidayEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    dtgOrganiz.ItemsSource = HolidayEntities.GetContext().Organizer.ToList();
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
        