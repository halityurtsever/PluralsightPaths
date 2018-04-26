﻿using Nancy;
using Nancy.Owin;

namespace OwinPipelineDemo.Modules
{
    public class NancyDemoModule : NancyModule
    {
        public NancyDemoModule()
        {
            Get["/nancy"] = x =>
            {
                var environment = Context.GetOwinEnvironment();
                return $"Hello from Nancy! You requested: {environment["owin.RequestPath"]}";
            };
        }
    }
}