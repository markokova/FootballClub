using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballClub.Staff.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }

        public DateTime ReservationDate { get; set; }

        public Guid CarId { get; set; }

        public Guid PersonId { get; set; }
    }
}