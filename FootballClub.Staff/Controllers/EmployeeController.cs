using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace FootballClub.Staff.Controllers
{
    public class EmployeeController : ApiController
    {
        private static List<Employee> employees = new List<Employee>();


        [HttpGet]
        public IHttpActionResult GetEmployees()
        {
            
            if(employees.Count == 0)
            {
                return NotFound();
            }
            return Ok(employees);
        }

        [HttpGet]
        public IHttpActionResult GetEmployee(int id)
        {
            //this.CreateEmployees(5);
            Employee employee = null;
            foreach(Employee empl in employees)
            {
                if(empl.Id == id)
                {
                    employee = empl;
                    break;
                }
            }
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public IHttpActionResult SaveNewEmployee([FromBody] Employee employee)
        {
            //this.CreateEmployees(5);
            int id = employees.Max(e => e.Id);
            if (employee != null) {
                employee.Id = id + 1;
                employees.Add(employee);
                return Ok(employee);
            }
            return NotFound();
        }

        [HttpPut]
        public IHttpActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            int index = -1;
            //this.CreateEmployees(5);
            foreach(Employee emp in employees)
            {
                if(emp.Id == id)
                {
                    break; 
                }
                index++;
            }
            employees[index] = employee;
            if (index < 0)
            {
                return NotFound();
            }
            return Ok(employees);
            
        }

        [HttpDelete]
        public IHttpActionResult DeleteEmployee(int id)
        {
            //this.CreateEmployees(3);
            foreach (Employee empl in employees)
            {
                if(empl.Id == id)
                {
                    employees.Remove(empl);
                    return Ok(employees);
                }
            }
            return NotFound();
        }



    }
}