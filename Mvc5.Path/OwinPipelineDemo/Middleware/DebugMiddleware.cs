using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.Owin;

using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace OwinPipelineDemo.Middleware
{
    public class DebugMiddleware
    {
        private readonly AppFunc m_Next;
        private readonly DebugMiddlewareOptions m_Options;

        public DebugMiddleware(AppFunc next, DebugMiddlewareOptions options)
        {
            m_Next = next;
            m_Options = options;

            if (options.OnIncomingRequest == null)
            {
                m_Options.OnIncomingRequest = context =>
                {
                    Debug.WriteLine($"Incoming Request:{context.Request.Path}");
                };
            }

            if (options.OnOutgoingRequest == null)
            {
                m_Options.OnOutgoingRequest = context =>
                {
                    Debug.WriteLine($"Outgoing Request:{context.Request.Path}");
                };
            }
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var context = new OwinContext(environment);

            m_Options.OnIncomingRequest(context);
            await m_Next(environment);
            m_Options.OnOutgoingRequest(context);
        }
    }
}