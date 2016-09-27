using Nancy;
using Nancy.Conventions;
using Nancy.Extensions;
using Nancy.Hosting.Self;
using System;
using Console = Colorful.Console;
using System.IO;

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
            Console.WriteLine("\n\n------------------------------------------------ \n\n");
            while (true)
            {

            }
        }
    }

    public class Modules : NancyModule
    {
        MainProgram program = new MainProgram();

        string databaseLocation = AppDomain.CurrentDomain.BaseDirectory + "database.json";

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

            Get["/database"] = _ =>
            {
                return Response.AsText(File.ReadAllText(databaseLocation));
            };
            
            Post["/home"] = parameters =>
            {
                var request = Request.Body;
                string[] array = request.AsString().Split('&');
                string[] decodedArray = program.DecodeSaveCode(array[2].Replace("saveString=", "").Replace("+", " "));

                string studentName = array[0].Replace("studentName=", "").Replace("+", " ");
                string studentID = array[1].Replace("studentID=", "").Replace("+", " ");
                string saveCode = array[2].Replace("saveString=", "").Replace("+", " ");
                string gameVersion = decodedArray[0];
                string startTime = decodedArray[1];
                string saveTime = decodedArray[2];
                string bakeryName = decodedArray[3];
                string cookieCount = decodedArray[4];
                string cookieCountAllTime = decodedArray[5];

                Console.WriteLine("Captured parameters");

                Console.WriteLine(studentName);
                Console.WriteLine(studentID);
                Console.WriteLine(saveCode);

                if (!program.doesUserExist(studentName, studentID))
                {
                    //Add a new user into the database
                    program.AddDatabaseUser(studentName, studentID, bakeryName, gameVersion, startTime, saveCode);

                    Console.WriteLine("New user added to the database");
                    
                }

                // Update the user's info in the database
                program.UpdateDatabaseUser(studentName, studentID, bakeryName, gameVersion, saveCode, startTime, saveTime, cookieCount, cookieCountAllTime);

                Console.WriteLine("Data pass");
                Console.WriteLine("\n\n------------------------------------------------ \n\n");

                return Response.AsRedirect("/home");
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
