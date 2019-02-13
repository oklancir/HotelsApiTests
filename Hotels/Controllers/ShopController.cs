using Hotels.Models;
using Hotels.ViewModels;
using NLog;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Hotels.Controllers
{
    public class ShopController : Controller
    {
        private readonly HotelsContext Context;
        private readonly Logger Logger = LogManager.GetLogger("logfile");

        public ShopController()
        {
            Context = new HotelsContext();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ShopList()
        {
            var items = Context.Items.ToList();
            return View(items);
        }

        public ActionResult Buy()
        {
            var reservations = Context.Reservations.ToList();
            var serviceProducts = Context.ServiceProducts.ToList();

            var viewModel = new BuyViewModel()
            {
                Reservations = reservations,
                ServiceProducts = serviceProducts
            };

            return View("SelectItems", viewModel);
        }

        public ActionResult SelectItems(BuyViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SelectItems", viewModel);
            }

            var buyViewModel = new BuyViewModel()
            {
                ReservationId = viewModel.ReservationId,
                ServiceProductId = viewModel.ServiceProductId,
                Quantity = viewModel.Quantity
            };

            return RedirectToAction("CompletePurchase", buyViewModel);
        }

        [HttpPost]
        public ActionResult CompletePurchase(BuyViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SelectItems");
            }

            var invoice = Context.Invoices.SingleOrDefault(i => i.ReservationId == viewModel.ReservationId);
            var reservation = Context.Reservations.SingleOrDefault(r => r.Id == viewModel.ReservationId);

            var invoiceId = invoice.Id;

            var item = new Item
            {
                InvoiceId = invoiceId,
                ServiceProductId = viewModel.ServiceProductId,
                Quantity = viewModel.Quantity
            };

            invoice.IsPaid = false;
            reservation.ReservationStatusId = 2;
            Context.Items.Add(item);

            try
            {
                Context.SaveChanges();
                return RedirectToAction("ShopList");
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return View("Error", new HandleErrorInfo(e, "Shop", "CompletePurchase"));
            }
        }
    }
}