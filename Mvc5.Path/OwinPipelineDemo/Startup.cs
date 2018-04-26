using System.Diagnostics;
using System.Web.Http;

using Nancy;
using Nancy.Owin;

using Owin;

using OwinPipelineDemo.Middleware;

namespace OwinPipelineDemo
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            #region Delegate Implementation

            //app.Use(async (context, next) =>
            //{
            //    Debug.WriteLine($"Incoming request:{context.Request.Path}");
            //    await next();
            //    Debug.WriteLine($"Outgoing request:{context.Request.Path}");
            //});

            #endregion

            #region Generic Method Implementation

            //app.Use<DebugMiddleware>(new DebugMiddlewareOptions
            //{
            //    OnIncomingRequest = context =>
            //    {
            //        var stopwatch = new Stopwatch();
            //        stopwatch.Start();
            //        context.Environment["DebuggerStopwatch"] = stopwatch;
            //    },

            //    OnOutgoingRequest = context =>
            //    {
            //        var stopwatch = context.Environment["DebuggerStopwatch"] as Stopwatch;
            //        stopwatch.Stop();
            //        Debug.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms.");
            //    }
            //});

            #endregion

            #region Extension Method Implementation

            app.UseDebugMiddleware(new DebugMiddlewareOptions
            {
                OnIncomingRequest = context =>
                {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    context.Environment["DebuggerStopwatch"] = stopwatch;
                },

                OnOutgoingRequest = context =>
                {
                    var stopwatch = context.Environment["DebuggerStopwatch"] as Stopwatch;
                    stopwatch.Stop();
                    context.Response.WriteAsync($"\nElapsed time: {stopwatch.ElapsedMilliseconds} ms.");
                }
            });

            #endregion

            #region Nancy Module Implementation

            app.UseNancy(config =>
            {
                config.PassThroughWhenStatusCodesAre(HttpStatusCode.NotFound);
            });

            #endregion

            #region WebApi Controller Implementation

            var httpConfig = new HttpConfiguration();
            httpConfig.MapHttpAttributeRoutes();
            app.UseWebApi(httpConfig);

            #endregion

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello World...");
            //});
        }
    }
}