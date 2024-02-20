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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
            login.Text = "";
            password.Password = "";
        }

        private void btnenter_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text == "") MessageBox.Show("Заполните поле логин", "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
            if (password.Password == "") MessageBox.Show("Заполните поле пароль", "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                Organizer organizers = ClassFrame.db.Organizer.FirstOrDefault(x => x.Email == login.Text && x.Password == password.Password);
                if (organizers != null)
                {
                    Users _user = new Users(organizers);
                    ClassFrame.frmObj.Navigate(new PageMainMenu());
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
