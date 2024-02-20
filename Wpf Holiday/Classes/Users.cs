using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Holiday.Classes
{
    internal class Users
    {
        public static Users GetUsers { get; set; }
        public int IdOrganizer { get; set; }
        public string Fio { get; set; }
        public string Email { get; set; }
        public int IdCountry { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Pol { get; set; }

        public Users(Organizer organizers)
        {
            IdOrganizer = organizers.IdOrganizer;
            Fio = organizers.Fio;
            Email = organizers.Email;
            IdCountry = (int)organizers.IdCountry;
            Phone = organizers.Phone;
            Password = organizers.Password;
            Pol = organizers.Pol;
            GetUsers = this;

        }
    }
}
