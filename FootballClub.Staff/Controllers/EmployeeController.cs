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
            
            if(employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }

        [HttpGet]
        public IHttpActionResult GetEmployee(int id)
        {
            this.CreateEmployees(5);
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
            this.CreateEmployees(5);

            if (employee != null) {
                employees.Add(employee);
                return Ok(employee);
            }
            return NotFound();
        }

        [HttpPut]
        public IHttpActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            int index = -1;
            this.CreateEmployees(5);
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
            this.CreateEmployees(3);
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

        private void CreateEmployees(int employeesNumber)
        {
            for(int i = 0; i < employeesNumber; i++)
            {
                Address address = new Address("ads", "afds", "sa", 1);
                Medic medic = new Medic("pero", "peric", "pero@gmail.com", i, DateTime.Now, address, 10000);
                employees.Add(medic);
            }
        }

    }
}