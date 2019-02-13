using System.Web.Mvc;

namespace Hotels.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(HandleErrorInfo handleErrorInfo)
        {
            return View(handleErrorInfo);
        }

        public ActionResult Http404()
        {
            return View();
        }
    }
}