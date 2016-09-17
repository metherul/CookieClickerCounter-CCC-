using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nancy;
using Nancy.Hosting.Self;
using Nancy.Conventions;
using Nancy.Linker;
using System.Drawing;
using Console = Colorful.Console;

namespace CookieClickerCounter_CCC
{
    class WebServer
    {
        public NancyHost host;

        public int port = 5000;

        public string url = "http://localhost";

        public WebServer()
        {
            var uri = new Uri($"{url}:{port}/");
            host = new NancyHost(uri);
        }

        public void StartServer()
        {
            host.Start();

            Console.WriteLine("Listening on port: " + port);

            while (true)
            {

            }
        }
    }

    class Modules : NancyModule
    {
        public Modules()
        {
            Get[""] = _ =>
            {
                return Response.AsRedirect("/home/");
            };

            Get["/home/"] = _ =>
            {
                return Response.AsFile("Content/index.html", "text/html");
            };
        }
    }

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/", "Content"));
            base.ConfigureConventions(nancyConventions);
        }
    }
}
