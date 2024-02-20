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
    /// Логика взаимодействия для PageAddPerson.xaml
    /// </summary>
    public partial class PageAddPerson : Page
    {
        private Person _currentItem = new Person();
        public PageAddPerson(Person selectedItem)
        {
            InitializeComponent();
            if (selectedItem != null)
            {
                _currentItem = selectedItem;
                Titletxt.Text = "Изменение участника";
                BtnAdd.Content = "Изменить";
            }
            DataContext = _currentItem;
            CMBCountry.ItemsSource = HolidayEntities.GetContext().Country.ToList();
            CMBCountry.SelectedValuePath = "IdCountry";
            CMBCountry.DisplayMemberPath = "NameCountry";
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.IdCountry))) error.AppendLine("Укажите страну");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.Fio))) error.AppendLine("Укажите ФИО");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.Email))) error.AppendLine("Укажите почту");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.DateBirthday))) error.AppendLine("Укажите день рождения");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.Password))) error.AppendLine("Укажите пароль");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentItem.Pol))) error.AppendLine("Укажите пол");
            if (HolidayEntities.GetContext().Person.Where(x => x.Country.NameCountry.ToString() == CMBCountry.Text && x.Fio == Txtfio.Text && x.Email == TxtEmail.Text && x.DateBirthday.ToString() == datePicker.Text && x.Password == TxtPassword.Text && x.Pol.ToString() == TxtPol.Text).Count() > 0)
            {
                MessageBox.Show("Такой участник уже есть");
                return;
            }
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            if (_currentItem.IdPerson == 0)
            {
                HolidayEntities.GetContext().Person.Add(_currentItem);
                try
                {
                    HolidayEntities.GetContext().SaveChanges();
                    ClassFrame.frmObj.Navigate(new PagePerson());
                    MessageBox.Show("Новый участник успешно добавлен!");
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
                    ClassFrame.frmObj.Navigate(new PagePerson());
                    MessageBox.Show("Участник успешно изменён!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new PagePerson());
        }
    }
}

   
