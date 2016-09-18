using Nancy;
using Nancy.Conventions;
using Nancy.Extensions;
using Nancy.Hosting.Self;
using System;
using Console = Colorful.Console;
using System.Threading;

namespace CookieClickerCounter_CCC
{
    class WebServer
    {
        public NancyHost host;
        public MainProgram program;

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
            Console.ReadLine();

            host.Stop();
        }
    }

    public class Modules : NancyModule
    {
        MainProgram program = new MainProgram();

        public Modules()
        {
            Get[""] = _ =>
            {
                return Response.AsRedirect("/home");
            };

            Get["/home"] = _ =>
            {
                return Response.AsFile("Content/index.html", "text/html");
            };

            Get["/user-not-found"] = _ =>
            {
                return Response.AsFile("Content/user-not-found.html");
            };

            Get["/admin-console"] = _ =>
            {
                return Response.AsFile("Content/admin-console.html");
            };
            
            Post["/home"] = parameters =>
            {
                var request = Request.Body;
                string[] array = request.AsString().Split('&');

                string studentName = array[0].Replace("studentName=", "").Replace("+", " ");
                string studentID = array[1].Replace("studentID=", "").Replace("+", " ");
                string cookieCount = array[2].Replace("cookieCount=", "").Replace("+", " ");
                string cookiesPerSecond = array[3].Replace("cookiesPerSecond=", "").Replace("+", " ");
                string saveString = array[4].Replace("saveString=", "").Replace("+", " ");


                Console.WriteLine("Captured parameters");

                Console.WriteLine(studentName);
                Console.WriteLine(studentID);
                Console.WriteLine(cookieCount);
                Console.WriteLine(cookiesPerSecond);
                Console.WriteLine(saveString);

                if (program.doesUserExist(studentName, studentID))
                {
                    // Update the user's info in the database
                    program.UpdateDatabaseUser(studentName, studentID, cookieCount, cookiesPerSecond, saveString);

                    Console.WriteLine("Data pass");
                    Console.WriteLine("\n\n------------------------------------------------ \n\n");

                    Thread.Sleep(5000);

                    return Response.AsRedirect("/home");
                }

                Console.WriteLine("User not found, redirecting");
                Console.WriteLine("\n\n------------------------------------------------ \n\n");

                return Response.AsRedirect("/user-not-found");
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
