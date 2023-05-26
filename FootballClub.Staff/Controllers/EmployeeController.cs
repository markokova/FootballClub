using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Npgsql;

namespace FootballClub.Staff.Controllers
{
    public class EmployeeController : ApiController
    {
        private static List<Employee> employees = new List<Employee>();


        [HttpGet]
        public HttpResponseMessage GetEmployees()
        {
            
            if(employees.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, employees);
        }

        [HttpGet]
        public HttpResponseMessage GetEmployee(int id)
        {
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
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }

        [HttpPost]
        public HttpResponseMessage SaveNewEmployee([FromBody] Employee employee)
        {
            int id = employees.Max(e => e.Id);
            if (employee != null) {
                employee.Id = id + 1;
                employees.Add(employee);
                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, id);
        }

        [HttpPut]
        public HttpResponseMessage UpdateEmployee(int id, double salary)
        {
            Employee employee = employees.Find(e => e.Id == id);
            
            if(employee != null)
            {
                employee.Salary = salary;
                return Request.CreateResponse(HttpStatusCode.OK, $"employee {employee.FirstName} new salary is {salary}");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, id);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteEmployee(int id)
        {
            Employee employee = employees.Find(e => e.Id == id);
            
            if(employee != null)
            {
                employees.Remove(employee);
                return Request.CreateResponse(HttpStatusCode.OK, $"Employee with {employee.Id} deleted.");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, id);
        }
    }
}