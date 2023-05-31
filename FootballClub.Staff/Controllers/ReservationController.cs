using FootballClub.Staff.Database_Logic.DBHandlers;
using FootballClub.Staff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using FootballClub.Staff.Database_Logic.Responses;

namespace FootballClub.Staff.Controllers
{
    public class ReservationController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetReservations()
        {
            ReservationDBHandler handler = new ReservationDBHandler();
            List<ReservationResponse> responses = handler.GetReservations();
            if (responses.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There is no Reservations in the database.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, responses);
        }

        [HttpGet]
        public HttpResponseMessage GetReservation(Guid id)
        {
            ReservationDBHandler handler = new ReservationDBHandler();

            ReservationResponse response = handler.GetReservation(id);
            
            if (response.reservation.Id == Guid.Empty)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"There is no Reservation with Id:{id} in the database.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        public HttpResponseMessage SaveNewReservation([FromBody] Reservation Reservation)
        {
            ReservationDBHandler handler = new ReservationDBHandler();
            int affectedRows = handler.InsertReservation(Reservation);

            if (affectedRows > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, Reservation);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Object isn't inserted.");
        }

        [HttpPut]
        public HttpResponseMessage UpdateReservation(Guid id, [FromBody] Reservation Reservation)
        {
            ReservationDBHandler handler = new ReservationDBHandler();
            int affectedRows = handler.UpdateReservation(id, Reservation);
            if (affectedRows > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, id);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteReservation(Guid id)
        {
            ReservationDBHandler handler = new ReservationDBHandler();
            int affectedRows = handler.DeleteReservation(id);

            if (affectedRows > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Reservation with Id: {id} deleted.");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, id);
        }
    }
}