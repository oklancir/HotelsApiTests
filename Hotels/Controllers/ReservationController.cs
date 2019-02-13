using Hotels.Models;
using Hotels.ViewModels;
using Itenso.TimePeriod;
using NLog;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Hotels.Controllers
{
    public class ReservationController : Controller
    {
        private readonly HotelsContext Context;
        private readonly Logger Logger = LogManager.GetLogger("logfile");

        public ReservationController()
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

        // GET: List of Reservations
        public ActionResult ReservationList()
        {
            var reservations = Context.Reservations.ToList();
            return View(reservations);
        }

        public ActionResult New()
        {
            var guests = Context.Guests.ToList();
            var viewModel = new ReservationFormViewModel()
            {
                Guests = guests,
            };

            return View("SelectGuestDate", viewModel);
        }

        public ActionResult SaveGuestDate(ReservationFormViewModel selectDateGuestViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SelectGuestDate", selectDateGuestViewModel);
            }

            var rangeFromSelect = new TimeRange(selectDateGuestViewModel.StartDate, selectDateGuestViewModel.EndDate);

            var reservations = Context.Reservations.ToList();

            var unavailableRoomsId = reservations
                .Where(r => rangeFromSelect.IntersectsWith(new TimeRange(r.StartDate, r.EndDate, true)))
                .Select(r => r.RoomId).ToList();

            var availableRooms = Context.Rooms.Where(ar => !unavailableRoomsId.Contains(ar.Id));

            var viewModel = new ReservationFormViewModel()
            {
                StartDate = selectDateGuestViewModel.StartDate,
                EndDate = selectDateGuestViewModel.EndDate,
                GuestId = selectDateGuestViewModel.GuestId,
                Rooms = availableRooms
            };

            return View("FinalizeReservation", viewModel);
        }

        public ActionResult FinalizeReservation(ReservationFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("FinalizeReservation", viewModel);
            }
            var reservationFormViewModel = new ReservationFormViewModel()
            {
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate,
                RoomId = viewModel.RoomId,
                Discount = viewModel.Discount,
                GuestId = viewModel.GuestId
            };
            return RedirectToAction("Save", reservationFormViewModel);
        }

        [HttpPost]
        public ActionResult Save(ReservationFormViewModel reservationFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("FinalizeReservation", reservationFormViewModel);
            }

            var reservation = new Reservation()
            {
                StartDate = reservationFormViewModel.StartDate,
                EndDate = reservationFormViewModel.EndDate,
                GuestId = reservationFormViewModel.GuestId,
                RoomId = reservationFormViewModel.RoomId,
                Discount = reservationFormViewModel.Discount,
                ReservationStatusId = 1
            };

            var invoice = new Invoice()
            {
                Reservation = reservation
            };

            try
            {
                Context.Reservations.Add(reservation);
                Context.Invoices.Add(invoice);
                Context.SaveChanges();
                return RedirectToAction("ReservationList", "Reservation");
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return View("Error", new HandleErrorInfo(e, "Reservation", "Save"));
            }
        }

        public ActionResult Checkout(int id)
        {
            var reservation = Context.Reservations.SingleOrDefault(r => r.Id == id);

            if (reservation == null)
                return HttpNotFound();

            var numberOfDays = (reservation.EndDate - reservation.StartDate).TotalDays;

            var invoice = Context.Invoices.SingleOrDefault(i => i.ReservationId == reservation.Id);

            var items = Context.Items.Where(i => i.InvoiceId == invoice.Id).ToList();

            double totalItemsAmount = 0;

            foreach (var item in items)
            {
                totalItemsAmount += item.ServiceProduct.Price * item.Quantity;
            }

            var totalAmount = totalItemsAmount + reservation.Room.RoomType.Price * numberOfDays;
            totalAmount = totalAmount * (1 - (reservation.Discount * 0.01));

            if (invoice != null)
            {
                invoice.TotalAmount = totalAmount;
            }

            var viewModel = new CheckoutViewModel
            {
                Reservation = reservation,
                Invoice = invoice,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                Items = items,
                TotalAmount = totalAmount,
                Discount = reservation.Discount
            };

            try
            {
                Context.SaveChanges();
                return View(viewModel);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return View("Error", new HandleErrorInfo(e, "Reservation", "Checkout"));
            }
        }

        public ActionResult ConfirmCheckout(int id)
        {
            var reservation = Context.Reservations.SingleOrDefault(r => r.Id == id);
            var invoice = Context.Invoices.SingleOrDefault(i => i.ReservationId == id);


            if (reservation != null && invoice != null)
            {
                invoice.IsPaid = true;
                reservation.ReservationStatusId = 3;
            }

            try
            {
                Context.SaveChanges();
                return RedirectToAction("ReservationList");
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return View("Error", new HandleErrorInfo(e, "Reservation", "ConfirmCheckout"));
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = Context.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartDate,EndDate,Guest,Room")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                Context.Entry(reservation).State = EntityState.Modified;
                Context.SaveChanges();
                return RedirectToAction("ReservationList");
            }
            return View(reservation);
        }

        public ActionResult Details(int id)
        {
            var reservation = Context.Reservations.SingleOrDefault(r => r.Id == id);

            if (reservation == null)
                return HttpNotFound();

            return View(reservation);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reservation = Context.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var reservation = Context.Reservations.Find(id);

            try
            {
                Context.Reservations.Remove(reservation);
                Context.SaveChanges();
                return RedirectToAction("ReservationList");
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return View("Error", new HandleErrorInfo(e, "Reservation", "Delete"));
            }
        }
    }
}
