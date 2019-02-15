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
    [TestClass]
    public class InvoicesControllerTests
    {
        [TestMethod]
        public async Task GetInvoicesTest_WhenCalled_ReturnsListOfInvoiceObjects()
        {
            var client = GetHttpClient();
            var response = await client.GetAsync("api/invoices");
            IEnumerable<InvoiceDto> invoices = null;

            if (response.IsSuccessStatusCode)
            {
                invoices = await response.Content.ReadAsAsync<IEnumerable<InvoiceDto>>();
            }

            Assert.IsInstanceOfType(invoices, typeof(IEnumerable<InvoiceDto>), "Request success");
        }

        [TestMethod]
        public async Task GetInvoice_WhenIdIsValid_ReturnsInvoiceDto()
        {
            var id = 25;
            var client = GetHttpClient();
            InvoiceDto invoiceDto = null;

            var response = await client.GetAsync("api/invoices/" + id);
            if (response.IsSuccessStatusCode)
            {
                invoiceDto = await response.Content.ReadAsAsync<InvoiceDto>();
            }

            Assert.IsNotNull(invoiceDto, "Request success.");
            Assert.AreEqual(invoiceDto.GetType(), typeof(InvoiceDto), "Invoice returned successfully.");
        }
        [TestMethod]
        public async Task GetInvoice_WhenIdNotValid_ReturnsFailedRequest()
        {
            var id = -1;
            var client = GetHttpClient();
            InvoiceDto invoiceDto = null;

            var response = await client.GetAsync("api/invoices/" + id);
            if (response.IsSuccessStatusCode)
            {
                invoiceDto = await response.Content.ReadAsAsync<InvoiceDto>();
            }

            Assert.IsNull(invoiceDto, "Failed request.");
        }

        [TestMethod]
        public async Task CreateInvoice_WhenInvoiceIsValid_ReturnsInvoiceDto()
        {
            var client = GetHttpClient();
            InvoiceDto invoiceDto = null;
            var invoiceToCreate = MockInvoice();
            var response = await client.PostAsJsonAsync("api/invoices", Mapper.Map<Invoice, InvoiceDto>(invoiceToCreate));

            if (response.IsSuccessStatusCode)
            {
                invoiceDto = await response.Content.ReadAsAsync<InvoiceDto>();
            }

            Assert.IsNotNull(invoiceDto);
            Assert.IsInstanceOfType(invoiceDto, typeof(InvoiceDto), "Invoice successfully added");
        }

        [TestMethod]
        public async Task CreateInvoice_WhenInvoiceIsNull_ReturnsNullInvoiceDto()
        {
            var client = GetHttpClient();
            InvoiceDto invoiceDto = null;
            var response = await client.PostAsJsonAsync("api/invoices", Mapper.Map<Invoice, InvoiceDto>(null));

            if (response.IsSuccessStatusCode)
            {
                invoiceDto = await response.Content.ReadAsAsync<InvoiceDto>();
            }

            Assert.IsNull(invoiceDto, "Trying to post empty Invoice");
        }

        [TestMethod]
        public async Task EditInvoice_WithVaildInvoiceId_ReturnsUpdatedInvoiceObject()
        {
            var client = GetHttpClient();
            InvoiceDto invoiceDto = null;
            var invoiceToUpdate = MockInvoiceToDb();
            invoiceToUpdate.IsPaid = true;
            var response = await client.PutAsJsonAsync("api/invoices/" + invoiceToUpdate.Id, Mapper.Map<Invoice, InvoiceDto>(invoiceToUpdate));

            if (response.IsSuccessStatusCode)
            {
                invoiceDto = await response.Content.ReadAsAsync<InvoiceDto>();
            }

            Assert.IsNotNull(invoiceDto);
            Assert.AreEqual(true, invoiceDto.IsPaid, "IsPaid updated succesfully");
            Assert.IsInstanceOfType(invoiceDto, typeof(InvoiceDto), "Returned InvoiceDto object");
        }

        [TestMethod]
        public async Task DeleteInvoice_WhenCalledWithValidId_ReturnsDeletedObject()
        {
            var client = GetHttpClient();
            InvoiceDto invoiceDto = null;
            var invoiceToDelete = GetInvoiceToDelete().GetAwaiter().GetResult();
            var response = await client.DeleteAsync($"api/invoices/{invoiceToDelete.Id}");

            if (response.IsSuccessStatusCode)
            {
                invoiceDto = await response.Content.ReadAsAsync<InvoiceDto>();
            }

            Assert.IsNotNull(invoiceDto);
            Assert.AreEqual(invoiceToDelete.Id, invoiceDto.Id, "Invoice Id is valid");
            Assert.IsInstanceOfType(invoiceDto, typeof(InvoiceDto), "Object Deleted successfully");
        }



        private HttpClient GetHttpClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private Invoice MockInvoice()
        {
            return new Invoice
            {
                Id = 1,
                ReservationId = 145,
                ItemId = 1,
                TotalAmount = 10000,
                IsPaid = false
            };
        }

        private Invoice MockInvoiceToDb()
        {
            var Context = new HotelsContext();

            var testInvoice = new Invoice
            {
                Id = 1,
                ReservationId = 145,
                ItemId = 1,
                TotalAmount = 10000,
                IsPaid = false
            };

            if (Context.Invoices.Find(testInvoice.Id) == null)
            {
                Context.Invoices.Add(testInvoice);
                Context.SaveChanges();
            }

            return testInvoice;
        }

        private async Task<InvoiceDto> GetInvoiceToDelete()
        {
            var client = GetHttpClient();
            var invoiceToCreate = MockInvoice();
            var response = await client.PostAsJsonAsync("api/invoices", Mapper.Map<Invoice, InvoiceDto>(invoiceToCreate));

            if (!response.IsSuccessStatusCode)
                return null;

            var invoiceDto = await response.Content.ReadAsAsync<InvoiceDto>();
            return invoiceDto;
        }
    }
}
