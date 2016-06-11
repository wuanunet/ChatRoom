using System.Web.Mvc;

namespace Chat.Web.Controllers
{
    public class ChatController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}