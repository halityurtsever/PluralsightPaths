using Nancy;
using Nancy.Owin;
using Nancy.Security;

namespace OwinPipelineSecurityDemo.Modules
{
    public class NancyDemoModule : NancyModule
    {
        public NancyDemoModule()
        {
            /* This will make sure that the user is authenticated before
             * trying to use anything inside of this module.
             */
            this.RequiresMSOwinAuthentication();

            Get["/nancy"] = x =>
            {
                var environment = Context.GetOwinEnvironment();
                var user = Context.GetMSOwinUser();

                return "Hello from Nancy! " +
                       $"You requested: {environment["owin.RequestPath"]}<br><br>" +
                       $"User:{user.Identity.Name}";
            };
        }
    }
}