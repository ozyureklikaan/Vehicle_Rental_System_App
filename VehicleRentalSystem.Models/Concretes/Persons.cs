using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSystem.Models.Concretes
{
    public class Persons : IDisposable
    {
        public int PersonID { get; set; }

        public string PersonName { get; set; }

        public string PersonLastName { get; set; }

        public int Age { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int? RentalID { get; set; }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Persons()
        {

        }
    }
}
