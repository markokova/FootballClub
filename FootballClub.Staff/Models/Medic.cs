using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace FootballClub.Staff.Controllers
{
    public class Medic : Employee
    {
        public Medic(string firstName, string lastName, string email, int id, DateTime startedWorkingDate, Address address,
            double salary) : base(firstName, lastName, email, id, startedWorkingDate, address, salary) {

        }

        public override int GetWorkExperience()
        {
            //medic team workers have benefited work experience because of stressful situations
            return (int)Math.Ceiling((double)base.GetWorkExperience() * 1.2);
        }
    }
}
