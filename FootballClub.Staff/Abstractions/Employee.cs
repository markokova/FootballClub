using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampDayOne
{
    internal abstract class Employee: IEmployee
    {
        public Employee()
        {
            
        }
        public Employee(string firstName, string lastName, string email, DateTime startedWorkingDate, Address address, double salary) {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Id = Guid.NewGuid();
            StartedWorkingDate = startedWorkingDate;
            Address = address;
            Salary = salary;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public Guid Id { get; set; }

        public DateTime StartedWorkingDate { get; set; }

        public Address Address { get; set; }

        public double Salary { get; set; }

        public virtual int GetWorkExperience()
        {
            if (StartedWorkingDate != null)
            {
                int workingTime = DateTime.Now.Year - StartedWorkingDate.Year;
                return workingTime;
            }
            return 0;
        }

        public void IncreaseSalary(double bonus)
        {
            Salary += bonus;
        }
    }
}
