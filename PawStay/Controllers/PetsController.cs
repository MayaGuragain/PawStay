using System.Web.Mvc;

namespace PawStay.Controllers
{
    public class PetsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}