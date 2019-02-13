using Hotels.Models;
using NLog;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Hotels.Controllers
{
    public class ServiceProductController : Controller
    {
        private readonly HotelsContext Context;
        private readonly Logger Logger = LogManager.GetLogger("logfile");

        public ServiceProductController()
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

        public ActionResult ServiceProductList()
        {
            var serviceProducts = Context.ServiceProducts.ToList();
            return View(serviceProducts);
        }

        public ActionResult AddServiceProduct(ServiceProduct modelServiceProduct)
        {
            if (!ModelState.IsValid)
            {
                return View(modelServiceProduct);
            }
            var serviceProduct = new ServiceProduct()
            {
                Name = modelServiceProduct.Name,
                Price = modelServiceProduct.Price
            };

            Context.ServiceProducts.Add(serviceProduct);
            try
            {
                Context.SaveChanges();
                return RedirectToAction("ServiceProductList", "ServiceProduct");
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return View("Error", new HandleErrorInfo(e, "ServiceProduct", "AddServiceProduct"));
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var serviceProduct = Context.ServiceProducts.Find(id);
            if (serviceProduct == null)
            {
                return HttpNotFound();
            }
            return View(serviceProduct);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var serviceProduct = Context.ServiceProducts.Find(id);
            if (serviceProduct != null)
            {
                Context.ServiceProducts.Remove(serviceProduct);
            }

            try
            {
                Context.SaveChanges();
                return RedirectToAction("ServiceProductList");
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return View("Error", new HandleErrorInfo(e, "ServiceProduct", "Delete"));
            }

        }
    }
}