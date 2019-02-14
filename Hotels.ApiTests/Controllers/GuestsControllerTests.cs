using AutoMapper;
using Hotels.App_Start;
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
    [TestClass]
    public class GuestsControllerTests
    {
        [TestMethod]
        public async Task GetGuestsTest()
        {
            var client = GetHttpClient();
            var response = await client.GetAsync("api/guests");
            IEnumerable<GuestDto> guests = null;

            if (response.IsSuccessStatusCode)
            {
                guests = await response.Content.ReadAsAsync<IEnumerable<GuestDto>>();
            }

            Assert.IsInstanceOfType(guests, typeof(IEnumerable<GuestDto>));
        }

        [TestMethod]
        public async Task GetGuest_WhenIdIsValid_ReturnsGuestDto()
        {
            var id = 12;
            var client = GetHttpClient();
            GuestDto guestDto = null;

            var response = await client.GetAsync("api/guests/" + id);
            if (response.IsSuccessStatusCode)
            {
                guestDto = await response.Content.ReadAsAsync<GuestDto>();
            }

            Assert.IsNotNull(guestDto, "Request success.");
            Assert.AreEqual(guestDto.GetType(), typeof(GuestDto), "GuestDto returned.");
        }
        [TestMethod]
        public async Task GetGuest_WhenIdNotValid_ReturnsGuestDto()
        {
            var id = -1;
            var client = GetHttpClient();
            GuestDto guestDto = null;

            var response = await client.GetAsync("api/guests/" + id);
            if (response.IsSuccessStatusCode)
            {
                guestDto = await response.Content.ReadAsAsync<GuestDto>();
            }

            Assert.IsNull(guestDto, "Failed request.");
        }

        [TestMethod]
        public async Task CreateGuest_WhenGuestDtoIsValid_ReturnsGuestDto()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<MappingProfile>();
            });

            var client = GetHttpClient();
            GuestDto guestDto = null;
            var guestToCreate = MockGuest();
            var response = await client.PostAsJsonAsync("api/guests", Mapper.Map<Guest, GuestDto>(guestToCreate));

            if (response.IsSuccessStatusCode)
            {
                guestDto = await response.Content.ReadAsAsync<GuestDto>();
            }

            Assert.IsNotNull(guestDto);
            Assert.IsInstanceOfType(guestDto, typeof(GuestDto), "Guest successfully added");
        }

        [TestMethod]
        public async Task CreateGuest_WhenGuestDtoIsNull_ReturnsNullGuestDto()
        {
            var client = GetHttpClient();
            GuestDto guestDto = null;
            var response = await client.PostAsJsonAsync("api/guests", Mapper.Map<Guest, GuestDto>(null));

            if (response.IsSuccessStatusCode)
            {
                guestDto = await response.Content.ReadAsAsync<GuestDto>();
            }

            Assert.IsNull(guestDto, "Trying to post empty Guest");
        }

        [TestMethod]
        public async Task EditGuest_WithVaildGuestId_Returns()
        {
            var client = GetHttpClient();
            GuestDto guestDto = null;
            var guestToUpdate = MockGuest();
            guestToUpdate.FirstName = "UpdatedTestName";
            var response = await client.PutAsJsonAsync("api/guests", Mapper.Map<Guest, GuestDto>(guestToUpdate));

            if (response.IsSuccessStatusCode)
            {
                guestDto = await response.Content.ReadAsAsync<GuestDto>();
            }

            Assert.IsNotNull(guestDto);
            Assert.AreEqual("UpdatedTestName", guestDto.FirstName);
            Assert.IsInstanceOfType(guestDto, typeof(GuestDto));
        }



        private HttpClient GetHttpClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private Guest MockGuest()
        {
            return new Guest
            {
                Id = 8,
                FirstName = "TestingRusk",
                LastName = "TestRussian",
                Address = "TestAddress",
                Email = "test@test.it",
                PhoneNumber = "123456789"
            };
        }

        private async Task<int> GetGuestIdToDelete()
        {
            var client = GetHttpClient();
            var guestToCreate = MockGuest();
            var response = await client.PostAsJsonAsync("api/guests", Mapper.Map<Guest, GuestDto>(guestToCreate));

            if (!response.IsSuccessStatusCode)
                return -1;

            var guestDto = await response.Content.ReadAsAsync<GuestDto>();
            return guestDto.Id;
        }
    }
}
