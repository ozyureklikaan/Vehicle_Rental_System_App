using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleRentalSystem.Models.Concretes
{
    public class Companies : IDisposable
    {
        public int CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string Password { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public int VehicleNumber { get; set; }

        public int CompanyScore { get; set; }

        public List<Vehicles> Vehicles { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Companies()
        {
            Vehicles = new List<Vehicles>();
        }
    }
}
