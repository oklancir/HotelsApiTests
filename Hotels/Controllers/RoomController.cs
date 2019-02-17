using Hotels.Models;
using Hotels.ViewModels;
using NLog;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Hotels.Controllers
{
    public class RoomController : Controller
    {
        private readonly HotelsContext Context;
        private readonly Logger Logger = LogManager.GetLogger("logfile");

        public RoomController()
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

        public ActionResult RoomList()
        {
            return View(Context.Rooms.ToList());
        }

        public ActionResult AddRoom(RoomViewModel model)
        {
            if (model.RoomTypes == null)
            {
                model.RoomTypes = Context.RoomTypes.ToList();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var room = new Room()
            {
                Name = model.Name,
                RoomTypeId = model.RoomTypeId,
                IsAvailable = true
            };

            Context.Rooms.Add(room);

            try
            {
                Context.SaveChanges();
                return RedirectToAction("RoomList", "Room");
            }
            catch (DbEntityValidationException e)
            {
                Logger.Error(e, e.Message);
                return View("Error", new HandleErrorInfo(e, "Room", "AddRoom"));
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = Context.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,RoomType,IsAvailable")] Room room)
        {
            if (ModelState.IsValid)
            {
                Context.Entry(room).State = EntityState.Modified;
                Context.SaveChanges();
                return RedirectToAction("RoomList");
            }
            return View(room);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = Context.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = Context.Rooms.Find(id);
            if (room != null)
            {
                Context.Rooms.Remove(room);
            }

            try
            {
                Context.SaveChanges();
                return RedirectToAction("RoomList");
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return View("Error", new HandleErrorInfo(e, "Room", "Delete"));
            }
        }
    }
}