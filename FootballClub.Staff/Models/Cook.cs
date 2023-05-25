using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampDayOne
{
    internal class Cook : Employee
    {
        public int YearlyOvertimeHours { get; set; }
        public Cook(string firstName, string lastName, string email, DateTime startedWorkingDate, Address address,
            double salary) : base(firstName, lastName, email, startedWorkingDate, address, salary)
        {

        }

        public override int GetWorkExperience()
        {
            //if yearly a cook works more than 150 hours he gets one more year of work experience in his resume
            ResetOvertimeHours();
            if(YearlyOvertimeHours > 150)
            {
                return base.GetWorkExperience() + 1;
            }
            return base.GetWorkExperience();
        }

        private void ResetOvertimeHours()
        {
            //reset overtime hours every year
            if (DateTime.Now.DayOfYear - base.StartedWorkingDate.DayOfYear == 0)
            {
                YearlyOvertimeHours = 0;
            }
        }

        public void AddOvertimeHours(int hours) {
            YearlyOvertimeHours += hours;
        }
    }
}
