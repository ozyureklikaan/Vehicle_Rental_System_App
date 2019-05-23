using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSystem.Models.Concretes
{
    public class RentalTransactions : IDisposable
    {
        public int RentalID { get; set; }

        public DateTime RentingDate { get; set; }

        //public DateTime RentingTime { get; set; }

        //public bool isSuccess { get; set; }

        public int PersonID { get; set; }

        public int VehicleID { get; set; }

        public List<Vehicles> Vehicles { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public RentalTransactions()
        {
            Vehicles = new List<Vehicles>();
        }
    }
}
