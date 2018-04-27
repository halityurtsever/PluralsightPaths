using System.Diagnostics;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;

using Owin;

namespace OwinPipelineSecurityDemo
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            //################################################################################
            #region Katana Based Authentication

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "AppCookie",
                LoginPath = new PathString("/Auth/Login")
            });

            #endregion

            app.Use(async (context, next) =>
            {
                if (context.Authentication.User.Identity.IsAuthenticated)
                {
                    var username = context.Authentication.User.Identity.Name;
                    Debug.WriteLine($"Authenticated user: {username}");
                }
                else
                {
                    Debug.WriteLine("User not authenticated.");
                }

                await next();
            });

            //################################################################################
            #region Nancy Module Authentication

            app.Map("/nancy", mappedApp => { mappedApp.UseNancy(); });

            #endregion
        }
    }
}