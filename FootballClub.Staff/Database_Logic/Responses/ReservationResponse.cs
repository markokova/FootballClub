using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FootballClub.Staff.Models;

namespace FootballClub.Staff.Database_Logic.Responses
{
    public class ReservationResponse
    {
        public Reservation reservation { get; set; }

        public Car car { get; set; }

        public Person person { get; set; }
    }
}