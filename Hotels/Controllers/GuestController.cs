using Hotels.Models;
using NLog;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Hotels.Controllers
{
    public class GuestController : Controller
    {
        private readonly HotelsContext Context;
        private readonly Logger Logger = LogManager.GetLogger("logfile");

        public GuestController()
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

        // GET: Guest
        public ActionResult GuestList()
        {
            return View(Context.Guests.ToList());
        }

        // POST
        public ActionResult AddGuest(Guest model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var guest = new Guest()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            Context.Guests.Add(guest);
            try
            {
                Context.SaveChanges();
                return RedirectToAction("GuestList", "Guest");
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return View("Error", new HandleErrorInfo(e, "Guest", "AddGuest"));
            }
        }



        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guest guest = Context.Guests.Find(id);
            if (guest == null)
            {
                return HttpNotFound();
            }
            return View(guest);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guest guest = Context.Guests.Find(id);
            if (guest == null)
            {
                return HttpNotFound();
            }
            return View(guest);
        }

        // POST: GuestList/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Address,Email,PhoneNumber")] Guest guest)
        {
            if (ModelState.IsValid)
            {
                Context.Entry(guest).State = EntityState.Modified;
                Context.SaveChanges();
                return RedirectToAction("GuestList");
            }
            return View(guest);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guest guest = Context.Guests.Find(id);
            if (guest == null)
            {
                return HttpNotFound();
            }
            return View(guest);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guest guest = Context.Guests.Find(id);
            Context.Guests.Remove(guest);
            Context.SaveChanges();
            return RedirectToAction("GuestList");
        }
    }
}