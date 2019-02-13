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
    public class InvoicesController : ApiController
    {
        private readonly HotelsContext Context;
        private readonly Logger Logger = LogManager.GetLogger("logfile");

        public InvoicesController()
        {
            Context = new HotelsContext();
        }

        [HttpGet]
        public IEnumerable<InvoiceDto> GetInvoices()
        {
            return Context.Invoices.ToList().Select(Mapper.Map<Invoice, InvoiceDto>);
        }

        [HttpGet]
        public IHttpActionResult GetInvoice(int id)
        {
            var invoice = Context.Invoices.SingleOrDefault(i => i.Id == id);

            if (invoice == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(Mapper.Map<Invoice, InvoiceDto>(invoice));
        }

        [HttpPost]
        public IHttpActionResult CreateInvoice(InvoiceDto invoiceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var reservations = Context.Reservations.Select(r => r.Id).ToList();

            if (!reservations.Contains(invoiceDto.ReservationId))
                return BadRequest("You need to create an invoice for an existing reservation.");

            var invoice = Mapper.Map<InvoiceDto, Invoice>(invoiceDto);
            Context.Invoices.Add(invoice);

            try
            {
                Context.SaveChanges();
                invoiceDto.Id = invoice.Id;
                return Created(new Uri(Request.RequestUri + "/" + invoice.Id), invoiceDto);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return BadRequest($"Error{e.Message}");
            }
        }

        [HttpPut]
        public IHttpActionResult EditInvoice(int id, InvoiceDto invoiceDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var invoiceInDb = Context.Invoices.SingleOrDefault(i => i.Id == id);

            if (invoiceInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            try
            {
                Mapper.Map(invoiceDto, invoiceInDb);
                Context.SaveChanges();
                return Ok(invoiceInDb);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return BadRequest($"Error{e.Message}");
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteInvoice(int id)
        {
            var invoiceInDb = Context.Invoices.SingleOrDefault(i => i.Id == id);

            if (invoiceInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Context.Invoices.Remove(invoiceInDb);

            try
            {
                Context.SaveChanges();
                return Ok(invoiceInDb);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return BadRequest($"Error{e.Message}");
            }
        }
    }
}
