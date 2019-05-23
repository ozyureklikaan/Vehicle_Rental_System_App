using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSystem.Models.Concretes
{
    public class Vehicles : IDisposable
    {
        public int VehicleID { get; set; }

        public string VehicleBrand { get; set; }

        public string VehicleModel { get; set; }

        public DateTime InputDate { get; set; }

        public DateTime OutputDate { get; set; }

        public int RequiredAgeOfDrivingLicense { get; set; }

        public int MinimumAgeLimit { get; set; }

        public int DailyKilometerLimit { get; set; }

        public int InstantKilometerOfTheVehicle { get; set; }

        public bool Airbag { get; set; }

        public int BaggageVolume { get; set; }

        public int NumberOfSeats { get; set; }

        public decimal DailyRentPrice { get; set; }

        public bool LeasingStatus { get; set; }

        public int CompanyID { get; set; }

        public Companies Company { get; set; }

        public List<RentalTransactions> RentalTransaction { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Vehicles()
        {
            Company = new Companies();
            RentalTransaction = new List<RentalTransactions>();
        }
    }
}
