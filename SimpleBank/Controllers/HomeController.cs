using System.Web.Mvc;

namespace SimpleBank.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Foo()
        {
            return View("Index");
        }
   }
}