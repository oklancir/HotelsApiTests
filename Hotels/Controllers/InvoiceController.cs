using Hotels.Models;
using NLog;
using System.Linq;
using System.Web.Mvc;

namespace Hotels.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly HotelsContext Context;
        private readonly Logger Logger = LogManager.GetLogger("logfile");

        public InvoiceController()
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

        public ActionResult InvoiceList()
        {
            var invoices = Context.Invoices.ToList();
            return View(invoices);
        }
    }
}