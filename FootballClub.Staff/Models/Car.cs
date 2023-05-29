using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballClub.Staff.Models
{
    public class Car
    {
        public Guid Id { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public int NumberOfSeats { get; set; }

        public double Price { get; set; }
    }
}