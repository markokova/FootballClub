using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampDayOne
{
    internal class Medic : Employee
    {
        public Medic(string firstName, string lastName, string email, DateTime startedWorkingDate, Address address,
            double salary) : base(firstName, lastName, email, startedWorkingDate, address, salary) {

        }

        public override int GetWorkExperience()
        {
            //medic team workers have benefited work experience because of stressful situations
            return (int)Math.Ceiling((double)base.GetWorkExperience() * 1.2);
        }
    }
}
