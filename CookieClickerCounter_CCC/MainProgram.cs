using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Console = Colorful.Console;

namespace CookieClickerCounter_CCC
{
    class MainProgram
    {
        string metaLocation = AppDomain.CurrentDomain.BaseDirectory;
        string databaseLocation = AppDomain.CurrentDomain.BaseDirectory + "database.json";

        static void Main(string[] args)
        {
            MainProgram program = new MainProgram();
            program.Start();

        }

        public void Start()
        {
            // Start the server
            WebServer server = new WebServer();
            server.StartServer();

            //AddDatabaseUser("Sombra", "23");
            //UpdateDatabaseUser("Ethan Green", "866980", "89213980891208901238903129801238982390089213890", "20008239801239080000", "wad48d48w6a468adw1564adw1356adw689+dsa456csz46545da456w6a54d456s546ad456wa456");
            //SetUserStatus("99", "password", "true");
            //DeleteUser("Ethan Green", "228822", "password");
            //doesUserExist("Ethan Green", "866980");
        }

        public void AddDatabaseUser(string _studentName, string _studentID)
        {
            User user;

            string jsonData = File.ReadAllText(databaseLocation);
            string studentName = _studentName;
            string studentID = _studentID;
            string jsonString;

            dynamic jsonDocument = null;

            if (new FileInfo(databaseLocation).Length > 0)
            {
                jsonDocument = JsonConvert.DeserializeObject<List<User>>(jsonData);

                Console.WriteLine("Checking data");

                // Check to make sure no dupes exist in the database. If there are no matches, continue on to writing the new User object to file
                foreach (dynamic userObject in jsonDocument)
                {
                    if ((userObject.StudentID == studentID) && (userObject.Name == studentName))
                    {
                        Console.WriteLine("Found match");

                        return;
                    }
                }

                // If the system finds no matches, continue on with the script
                user = new User();
                user.Initialize(studentName, studentID);

                jsonDocument.Add(user);

                jsonString = JsonConvert.SerializeObject(jsonDocument, Formatting.Indented);

                File.WriteAllText(databaseLocation, jsonString);
            }

            // So it doesn't crash when the database is empty
            else if (new FileInfo(databaseLocation).Length == 0)
            {
                jsonDocument = new List<User>();

                user = new User();
                user.Initialize(studentName, studentID);

                jsonDocument.Add(user);

                jsonString = JsonConvert.SerializeObject(jsonDocument, Formatting.Indented);
                File.WriteAllText(databaseLocation, jsonString);
            }
        }

        public void UpdateDatabaseUser(string _studentName, string _studentID, string _cookieCount, string _cookiesPerSecond, string _saveString)
        {
            User user = new User();

            string jsonData = File.ReadAllText(databaseLocation);
            string studentName = _studentName;
            string studentID = _studentID;
            string cookieCount = _cookieCount;
            string cookiesPerSecond = _cookiesPerSecond;
            string saveString = _saveString;
            string jsonString;

            dynamic jsonDocument = JsonConvert.DeserializeObject<List<User>>(jsonData);

            foreach (dynamic userObject in jsonDocument)
            {
                if ((userObject.StudentID == studentID) && (userObject.Name == studentName))
                {
                    userObject.Update(cookieCount, cookiesPerSecond, saveString);

                    jsonString = JsonConvert.SerializeObject(jsonDocument, Formatting.Indented);
                    File.WriteAllText(databaseLocation, jsonString);

                    break;
                }
            }
        }

        public void SetUserStatus(string _studentID, string _password, string _activeValue)
        {
            // THIS IS USED FOR REMOVING OR BANNING A USER FROM THE DATABASE. USE THIS IF YOU WANT TO BLOCK SOMEONE FROM USING THE PROGRAM, 
            // AND USE DELETEUSER TO REMOVE A STUDENT FROM THE DATABASE

            int counter = 0;

            string jsonData = File.ReadAllText(databaseLocation);
            string studentID = _studentID;
            string password = _password;
            string activeValue = _activeValue;

            bool isRunning = true;

            dynamic jsonDocument = JsonConvert.DeserializeObject<List<User>>(jsonData);

            while (isRunning)
            {
                Console.WriteLine(counter);

                if ((jsonDocument[counter].StudentID) == studentID)
                    isRunning = false;

                else
                    counter++;
            }

            jsonDocument[counter].Active = activeValue;

            string jsonOutput = JsonConvert.SerializeObject(jsonDocument, Formatting.Indented);
            File.WriteAllText(databaseLocation, jsonOutput);
        }

        public void DeleteUser(string _studentName, string _studentID, string _password)
        {
            User user;

            int counter = 0;

            string jsonData = File.ReadAllText(databaseLocation);
            string studentName = _studentName;
            string studentID = _studentID;
            string jsonString;

            bool isRunning = true;
            bool foundMatch = false;

            dynamic jsonDocument = JsonConvert.DeserializeObject<List<User>>(jsonData);

            for (int i = 0; i < jsonDocument.Count; i++)
            {
                if (jsonDocument[i].StudentID == studentID && jsonDocument[i].Name == studentName)
                {
                    jsonDocument.RemoveAt(i);
                }
            }

            string jsonOutput = JsonConvert.SerializeObject(jsonDocument, Formatting.Indented);
            File.WriteAllText(databaseLocation, jsonOutput);
        }

        public bool doesUserExist(string _studentName, string _studentID)
        {
            string jsonData = File.ReadAllText(databaseLocation);
            string studentName = _studentName;
            string studentID = _studentID;

            dynamic jsonDocument = null;

            if (new FileInfo(databaseLocation).Length > 0)
            {
                jsonDocument = JsonConvert.DeserializeObject<List<User>>(jsonData);

                Console.WriteLine("Checking data");

                // Check to make sure no dupes exist in the database. If there are no matches, continue on to writing the new User object to file
                foreach (dynamic userObject in jsonDocument)
                {
                    if ((userObject.StudentID == studentID) && (userObject.Name == studentName))
                    {
                        Console.WriteLine("Match found");

                        return true;
                    }
                }
            }

            Console.WriteLine("Match not found");
            return false;
        }
    }
}
