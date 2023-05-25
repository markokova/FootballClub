using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FootballClub.Staff.Controllers
{
    public class Address
    {
        public Address(string street, string city, string state, int zipCode)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int ZipCode { get; set; }

    }
}
