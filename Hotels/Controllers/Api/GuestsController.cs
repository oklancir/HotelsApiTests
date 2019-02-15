using AutoMapper;
using Hotels.Dtos;
using Hotels.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Hotels.Controllers.Api
{
    public class GuestsController : ApiController
    {
        private readonly IHotelsContext Context;
        private readonly Logger Logger = LogManager.GetLogger("logfile");

        public GuestsController()
        {
            Context = new HotelsContext();
        }

        public GuestsController(IHotelsContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IEnumerable<GuestDto> GetGuests()
        {
            return Context.Guests.ToList().Select(Mapper.Map<Guest, GuestDto>);
        }

        [HttpGet]
        public IHttpActionResult GetGuest(int id)
        {
            var guest = Context.Guests.SingleOrDefault(g => g.Id == id);

            if (guest == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(Mapper.Map<Guest, GuestDto>(guest));
        }

        [HttpPost]
        public IHttpActionResult CreateGuest(GuestDto guestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var guest = Mapper.Map<GuestDto, Guest>(guestDto);
            Context.Guests.Add(guest);

            try
            {
                Context.SaveChanges();
                guestDto.Id = guest.Id;
                return Created(new Uri(Request.RequestUri + "/" + guest.Id), guestDto);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return BadRequest($"Error{e.Message}");
            }
        }

        [HttpPut]
        public IHttpActionResult EditGuest(int id, [FromBody] GuestDto guestDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var guestInDb = Context.Guests.SingleOrDefault(g => g.Id == id);

            if (guestInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(guestDto, guestInDb);

            try
            {
                Context.SaveChanges();
                return Ok(guestInDb);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return BadRequest($"Error{e.Message}");
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteGuest(int id)
        {
            var guestInDb = Context.Guests.SingleOrDefault(g => g.Id == id);

            if (guestInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Context.Guests.Remove(guestInDb);

            try
            {
                Context.SaveChanges();
                return Ok(guestInDb);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return BadRequest($"Error{e.Message}");
            }
        }
    }
}
