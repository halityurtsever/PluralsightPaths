using System;

using Microsoft.Owin;

namespace OwinPipelineDemo.Middleware
{
    public class DebugMiddlewareOptions
    {
        public Action<OwinContext> OnIncomingRequest { get; set; }

        public Action<OwinContext> OnOutgoingRequest { get; set; }
    }
}