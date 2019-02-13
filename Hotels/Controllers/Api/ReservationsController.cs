using AutoMapper;
using Hotels.Dtos;
using Hotels.Models;
using Itenso.TimePeriod;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Hotels.Controllers.Api
{
    public class ReservationsController : ApiController
    {
        private readonly HotelsContext Context;
        private readonly Logger Logger = LogManager.GetLogger("logfile");

        public ReservationsController()
        {
            Context = new HotelsContext();
        }

        [HttpGet]
        public IEnumerable<ReservationDto> GetReservations()
        {
            return Context.Reservations.ToList().Select(Mapper.Map<Reservation, ReservationDto>);
        }

        [HttpGet]
        public IHttpActionResult GetReservation(int id)
        {
            var reservation = Context.Reservations.SingleOrDefault(g => g.Id == id);

            if (reservation == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(Mapper.Map<Reservation, ReservationDto>(reservation));
        }

        [HttpPost]
        public IHttpActionResult CreateReservation(ReservationDto reservationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The data you have entered is not valid.");
            }

            var rangeFromSelect = new TimeRange(reservationDto.StartDate, reservationDto.EndDate);
            var reservations = Context.Reservations.ToList();

            var unavailableRoomsId = reservations
                .Where(r => rangeFromSelect.IntersectsWith(new TimeRange(r.StartDate, r.EndDate, true)))
                .Select(r => r.RoomId).ToList();

            if (unavailableRoomsId.Contains(reservationDto.RoomId))
                return BadRequest("The room is already reserved for that period");

            var invoice = new Invoice
            {
                ReservationId = reservationDto.Id,
                IsPaid = false
            };

            Context.Reservations.Add(Mapper.Map<ReservationDto, Reservation>(reservationDto));
            Context.Invoices.Add(invoice);

            try
            {
                Context.SaveChanges();
                return Ok("Reservation added successfully.");
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return BadRequest($"Error{e.Message}");
            }
        }

        [HttpPut]
        public IHttpActionResult EditReservation(int id, ReservationDto reservationDto)
        {
            var reservationInDb = Context.Reservations.SingleOrDefault(r => r.Id == id);

            if (reservationInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var rangeFromSelect = new TimeRange(reservationDto.StartDate, reservationDto.EndDate);
            var reservations = Context.Reservations.ToList();
            var unavailableRoomsId = reservations
                .Where(r => rangeFromSelect.IntersectsWith(new TimeRange(r.StartDate, r.EndDate, true)))
                .Select(r => r.RoomId).ToList();

            if (unavailableRoomsId.Contains(reservationDto.RoomId))
                return BadRequest("The room is already reserved for that period");

            Mapper.Map(reservationDto, reservationInDb);

            try
            {
                Context.SaveChanges();
                return Ok($"Reservation {id} updated successfully");
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return BadRequest($"Error{e.Message}");
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteReservation(int id)
        {
            var reservationInDb = Context.Reservations.SingleOrDefault(g => g.Id == id);

            if (reservationInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Context.Reservations.Remove(reservationInDb);

            try
            {
                Context.SaveChanges();
                return Ok($"Reservation {id} successfully removed.");
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return BadRequest($"Error{e.Message}");
            }
        }
    }
}
