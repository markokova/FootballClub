using FootballClub.Staff.Database_Logic.DBHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using FootballClub.Staff.Models;
using Npgsql;

namespace FootballClub.Staff.Controllers
{
    public class CarController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage GetCars()
        {
            CarDBHandler handler = new CarDBHandler();
            List<Car> cars = new List<Car>();
            cars = handler.GetCars();
            if (cars.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no cars in the database.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, cars);
        }

        [HttpGet]
        public HttpResponseMessage GetCar(Guid id)
        {
            CarDBHandler handler = new CarDBHandler();

            Car car = handler.GetCar(id);
            
            if (car == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"There is no car with Id:{id} in the database.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, car);
        }

        [HttpPost]
        public HttpResponseMessage SaveNewCar([FromBody] Car car)
        {
            CarDBHandler handler = new CarDBHandler();
            int affectedRows = handler.InsertCar(car);

            if (affectedRows > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, car);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Object isn't inserted.");
        }

        [HttpPut]
        public HttpResponseMessage UpdateCar(Guid id, [FromBody] Car car)
        {
            CarDBHandler handler = new CarDBHandler();
            int affectedRows = handler.UpdateCar(id, car);
            if (affectedRows > 0)
            {
                
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, id);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteCar(Guid id)
        {
            CarDBHandler handler = new CarDBHandler();
            int affectedRows = handler.DeleteCar(id);

            if (affectedRows > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Car with Id: {id} deleted.");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, id);
        }
    }
}