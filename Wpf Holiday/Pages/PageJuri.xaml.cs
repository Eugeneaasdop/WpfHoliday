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
    /// Логика взаимодействия для PageJuri.xaml
    /// </summary>
    public partial class PageJuri : Page
    {
        public PageJuri()
        {
            InitializeComponent();
            dtgJuri.ItemsSource = HolidayEntities.GetContext().Juri.ToList();
        }

        private void MenuAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageAddJuri(null));
        }

        private void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PageAddJuri((Juri)dtgJuri.SelectedItem));
        }

        private void MenuUpdate_Click(object sender, RoutedEventArgs e)
        {
            dtgJuri.ItemsSource = HolidayEntities.GetContext().Juri.ToList();
        }

        private void MenuDel_Click(object sender, RoutedEventArgs e)
        {
            var salesForRemoving = dtgJuri.SelectedItems.Cast<Juri>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {salesForRemoving.Count()} записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    HolidayEntities.GetContext().Juri.RemoveRange(salesForRemoving);
                    HolidayEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    dtgJuri.ItemsSource = HolidayEntities.GetContext().Juri.ToList();
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
