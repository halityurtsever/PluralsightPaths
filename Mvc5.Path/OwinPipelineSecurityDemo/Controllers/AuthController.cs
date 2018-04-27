using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

using OwinPipelineSecurityDemo.Models;

namespace OwinPipelineSecurityDemo.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            var model = new User();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(User modelUser)
        {
            if (modelUser.Username == "halit" && modelUser.Password == "yurtsever")
            {
                var identity = new ClaimsIdentity("AppCookie");

                identity.AddClaims(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, modelUser.Username),
                    new Claim(ClaimTypes.Name, modelUser.Username)
                });

                HttpContext.GetOwinContext().Authentication.SignIn(identity);
            }

            //if credentials are incorrect then return same view for retry.
            return View(modelUser);
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}