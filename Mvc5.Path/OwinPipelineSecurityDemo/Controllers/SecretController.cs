using System.Web.Mvc;

namespace OwinPipelineSecurityDemo.Controllers
{
    public class SecretController : Controller
    {
        // GET: Secret
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}