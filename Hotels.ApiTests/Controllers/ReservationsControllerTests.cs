using AutoMapper;
using Hotels.Dtos;
using Hotels.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Hotels.ApiTests.Controllers
{
    /// <summary>
    /// Summary description for ReservationsControllerTests
    /// </summary>
    [TestClass]
    public class ReservationsControllerTests
    {
        [TestMethod]
        public async Task GetReservationsTest()
        {
            var client = GetHttpClient();
            var response = await client.GetAsync("api/reservations");
            IEnumerable<ReservationDto> reservations = null;

            if (response.IsSuccessStatusCode)
            {
                reservations = await response.Content.ReadAsAsync<IEnumerable<ReservationDto>>();
            }

            Assert.IsInstanceOfType(reservations, typeof(IEnumerable<ReservationDto>));
        }

        [TestMethod]
        public async Task GetReservation_WhenIdIsValid_ReturnsReservationDto()
        {
            var id = 8;
            var client = GetHttpClient();
            ReservationDto reservationDto = null;

            var response = await client.GetAsync("api/reservations/" + id);
            if (response.IsSuccessStatusCode)
            {
                reservationDto = await response.Content.ReadAsAsync<ReservationDto>();
            }

            Assert.IsNotNull(reservationDto, "Request success.");
            Assert.AreEqual(reservationDto.GetType(), typeof(ReservationDto), "ReservationDto returned.");
        }
        [TestMethod]
        public async Task GetReservation_WhenIdNotValid_ReturnsReservationDto()
        {
            var id = -1;
            var client = GetHttpClient();
            ReservationDto reservationDto = null;

            var response = await client.GetAsync("api/guests/" + id);
            if (response.IsSuccessStatusCode)
            {
                reservationDto = await response.Content.ReadAsAsync<ReservationDto>();
            }

            Assert.IsNull(reservationDto, "Failed request.");
        }

        [TestMethod]
        public async Task CreateReservation_WhenReservationDtoIsValid_ReturnsReservationDto()
        {
            var client = GetHttpClient();
            ReservationDto reservationDto = null;
            var reservationToCreate = new Reservation {  RoomId = 4, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(3) };
            var response = await client.PostAsJsonAsync("api/reservations", Mapper.Map<Reservation, ReservationDto>(reservationToCreate));

            if (response.IsSuccessStatusCode)
            {
                reservationDto = await response.Content.ReadAsAsync<ReservationDto>();
            }

            Assert.IsNotNull(reservationDto);
            Assert.IsInstanceOfType(reservationDto, typeof(ReservationDto), "Reservation successfully added");
        }

        [TestMethod]
        public async Task CreateReservation_WhenReservationDtoIsNull_ReturnsNullGuestDto()
        {
            var client = GetHttpClient();
            ReservationDto reservationDto = null;
            var response = await client.PostAsJsonAsync("api/reservations", Mapper.Map<Reservation, ReservationDto>(null));

            if (response.IsSuccessStatusCode)
            {
                reservationDto = await response.Content.ReadAsAsync<ReservationDto>();
            }

            Assert.IsNull(reservationDto, "Trying to post empty Reservation");
        }

        //[TestMethod]
        //public async Task EditReservation_WithVaildGuestId_ReturnsUpdatedGuestObject()
        //{
        //    var client = GetHttpClient();
        //    ReservationDto reservationDto = null;
        //    var reservationToUpdate = MockReservationToDb();
        //    reservationToUpdate.ReservationStatusId = 3;
        //    var response = await client.PutAsJsonAsync("api/reservations/" + reservationToUpdate.Id, Mapper.Map<Reservation, ReservationDto>(reservationToUpdate));

        //    if (response.IsSuccessStatusCode)
        //    {
        //        reservationDto = await response.Content.ReadAsAsync<ReservationDto>();
        //    }

        //    Assert.IsNotNull(reservationDto);
        //    Assert.AreEqual(3, reservationDto.ReservationStatusId, "Reservation Status updated succesfully");
        //    Assert.IsInstanceOfType(reservationDto, typeof(GuestDto), "Returned ReservationDto object");
        //}

        //[TestMethod]
        //public async Task DeleteReservation_WhenCalledWithValidId_ReturnsDeletedObject()
        //{
        //    var client = GetHttpClient();
        //    ReservationDto reservationDto = null;
        //    var reservationToDelete = GetReservationToDelete().GetAwaiter().GetResult();
        //    var response = await client.DeleteAsync($"api/reservations/{reservationToDelete.Id}");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        reservationDto = await response.Content.ReadAsAsync<ReservationDto>();
        //    }

        //    Assert.IsNotNull(reservationDto);
        //    Assert.AreEqual(reservationToDelete.Id, reservationDto.Id, "Reservation Id is valid");
        //    Assert.IsInstanceOfType(reservationDto, typeof(ReservationDto), "Reservation object Deleted successfully");
        //}



        private HttpClient GetHttpClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private Reservation MockReservation()
        {
            return new Reservation
            {
                Id = 145,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(7),
                GuestId = 1,
                ReservationStatusId = 1
            };
        }

        private Reservation MockReservationToDb()
        {
            var Context = new HotelsContext();

            var testReservation = new Reservation
            {
                Id = 221,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(7),
                GuestId = 11,
                RoomId = 1,
                ReservationStatusId = 1
            };

            if (Context.Reservations.Find(testReservation.Id) == null)
            {
                Context.Reservations.Add(testReservation);
                Context.SaveChanges();
            }

            return testReservation;
        }

        private async Task<ReservationDto> GetReservationToDelete()
        {
            var client = GetHttpClient();
            var reservationToCreate = MockReservation();
            var response = await client.PostAsJsonAsync("api/reservations", Mapper.Map<Reservation, ReservationDto>(reservationToCreate));

            if (!response.IsSuccessStatusCode)
                return null;

            var reservationDto = await response.Content.ReadAsAsync<ReservationDto>();
            return reservationDto;
        }
    }
}
