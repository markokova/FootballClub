using FootballClub.Staff.Database_Logic.DBHandlers;
using FootballClub.Staff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace FootballClub.Staff.Controllers
{
    public class PersonController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetPersons()
        {
            PersonDBHandler handler = new PersonDBHandler();
            List<Person> Persons = new List<Person>();
            Persons = handler.GetPersons();
            if (Persons.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no Persons in the database.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, Persons);
        }

        [HttpGet]
        public HttpResponseMessage GetPerson(Guid id)
        {
            PersonDBHandler handler = new PersonDBHandler();

            Person Person = handler.GetPerson(id);

            if (Person == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"There is no Person with Id:{id} in the database.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, Person);
        }

        [HttpPost]
        public HttpResponseMessage SaveNewPerson([FromBody] Person Person)
        {
            PersonDBHandler handler = new PersonDBHandler();
            int affectedRows = handler.InsertPerson(Person);

            if (affectedRows > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, Person);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Object isn't inserted.");
        }

        [HttpPut]
        public HttpResponseMessage UpdatePerson(Guid id, [FromBody] Person Person)
        {
            PersonDBHandler handler = new PersonDBHandler();
            int affectedRows = handler.UpdatePerson(id, Person);
            if (affectedRows > 0)
            { 
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, id);
        }

        [HttpDelete]
        public HttpResponseMessage DeletePerson(Guid id)
        {
            PersonDBHandler handler = new PersonDBHandler();
            int affectedRows = handler.DeletePerson(id);

            if (affectedRows > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Person with Id: {id} deleted.");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, id);
        }
    }
}