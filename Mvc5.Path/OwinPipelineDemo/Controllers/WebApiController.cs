using System.Net;
using System.Web.Http;

namespace OwinPipelineDemo.Controllers
{
    [RoutePrefix("api")]
    public class WebApiController : ApiController
    {
        [Route("hello")]
        [HttpGet]
        public IHttpActionResult HelloWebApi()
        {
            return Content(HttpStatusCode.OK, "Hello from Web Api Controller.");
        }
    }
}