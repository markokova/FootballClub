using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampDayOne
{
    public interface IEmployee
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string Email { get; set; }
        
        Guid Id { get; set; }
    
        DateTime StartedWorkingDate { get; set; }

        Address Address { get; set; }

        double Salary { get; set; }
    }
}
