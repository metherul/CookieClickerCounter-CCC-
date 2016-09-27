using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

        public void AddDatabaseUser(string _studentName, string _studentID, string _bakeryName, string _gameVersion, string _startTime, string _saveCode)
        {
            User user;

            string jsonData = File.ReadAllText(databaseLocation);
            string studentName = _studentName;
            string studentID = _studentID;
            string bakeryName = _bakeryName;
            string gameVersion = _gameVersion;
            string startTime = _startTime;
            string saveCode = _saveCode;
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
                user.Initialize(studentName, studentID, bakeryName, gameVersion, startTime, saveCode);

                jsonDocument.Add(user);

                jsonString = JsonConvert.SerializeObject(jsonDocument, Formatting.Indented);

                File.WriteAllText(databaseLocation, jsonString);
            }

            // So it doesn't crash when the database is empty
            else if (new FileInfo(databaseLocation).Length == 0)
            {
                jsonDocument = new List<User>();

                user = new User();
                user.Initialize(studentName, studentID, bakeryName, gameVersion, startTime, saveCode);

                jsonDocument.Add(user);

                jsonString = JsonConvert.SerializeObject(jsonDocument, Formatting.Indented);
                File.WriteAllText(databaseLocation, jsonString);
            }
        }

        public void UpdateDatabaseUser(string _studentName, string _studentID, string _bakeryName, string _gameVersion, string _saveCode, string _startTime, string _saveTime, string _cookieCount, string _cookieCountAllTime)
        {
            User user = new User();

            string jsonData = File.ReadAllText(databaseLocation);
            string studentName = _studentName;
            string studentID = _studentID;
            string bakeryName = _bakeryName;
            string gameVersion = _gameVersion;
            string saveCode = _saveCode;
            string startTime = _startTime;
            string saveTime = _saveTime;
            string cookieCount = _cookieCount;
            string cookieCountAllTime = _cookieCountAllTime;
            string jsonString;

            dynamic jsonDocument = JsonConvert.DeserializeObject<List<User>>(jsonData);

            foreach (dynamic userObject in jsonDocument)
            {
                if ((userObject.StudentID == studentID) && (userObject.StudentName == studentName))
                {
                    userObject.Update(saveTime, cookieCount, cookieCountAllTime, saveCode);

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
            string jsonData = File.ReadAllText(databaseLocation);
            string studentName = _studentName;
            string studentID = _studentID;

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
                    if ((userObject.StudentID == studentID) && (userObject.StudentName == studentName))
                    {
                        Console.WriteLine("Match found");

                        return true;
                    }
                }
            }

            Console.WriteLine("Match not found");
            return false;
        }

        public string[] DecodeSaveCode(string saveCode)
        {
            string gameVersion;
            string startTime;
            string saveTime;
            string bakeryName;
            string cookieCount;
            string cookieCountAllTime;

            string temp;

            string[] decodedArray = DecodeBase64(saveCode);
            string[] decodedInformation = new string[5];

            foreach (string i in decodedArray)
            {
                Console.WriteLine(i.ToString());
            }

            Console.WriteLine("\n");

            // First off we're going to get the gameVersion
            temp = decodedArray[0];
            gameVersion = temp.Substring(0, temp.IndexOf("|"));
            Console.WriteLine("GAME VERSION: " + gameVersion);

            startTime = temp.Substring(temp.IndexOf("|") + 2);
            startTime = FromUnixTime(Convert.ToDouble(startTime));
            Console.WriteLine("START TIME: " + startTime);

            temp = decodedArray[2];
            saveTime = FromUnixTime(Convert.ToDouble(temp));
            Console.WriteLine("SAVE TIME: " + saveTime);

            temp = decodedArray[3];
            bakeryName = temp.Substring(0, temp.IndexOf("|")) + "'s bakery";
            Console.WriteLine("BAKERY NAME: " + bakeryName + "'s bakery");

            temp = decodedArray[3];
            cookieCount = temp.Substring(temp.IndexOf("|") + 1);
            cookieCount = cookieCount.Substring(cookieCount.IndexOf("|") + 1);
            
            if (!cookieCount.Contains("+"))
            {
                cookieCount = Math.Round(Convert.ToDecimal(cookieCount)).ToString();
            }

            Console.WriteLine("COOKIE COUNT: " + cookieCount);

            temp = decodedArray[4];
            cookieCountAllTime = temp.Substring(temp.IndexOf("|") + 1);

            if (!cookieCountAllTime.Contains("+"))
            {
                cookieCountAllTime = Math.Round(Convert.ToDecimal(cookieCountAllTime)).ToString();
            }

            Console.WriteLine("COOKIES COUNT ALL TIME: " + cookieCountAllTime);

            // Stick this all into an array
            decodedArray[0] = gameVersion;
            decodedArray[1] = startTime;
            decodedArray[2] = saveTime;
            decodedArray[3] = bakeryName;
            decodedArray[4] = cookieCount;
            decodedArray[5] = cookieCountAllTime;

            return decodedArray;
        }

        public string[] DecodeBase64(string base64)
        {
            // First we have to sanitize the string to make sure that all padding and certain characters are removed
            string base64String = base64.Replace("%2521END%2521", "").Replace("%253D", "=").Replace("%2F", @"/");
            string decodedString;

            Console.WriteLine(base64String);

            string[] array;

            byte[] data;

            data = Convert.FromBase64String(base64String);
            decodedString = Encoding.UTF8.GetString(data);

            array = decodedString.Split(';');

            return array;
        }

        public string FromUnixTime(double unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dtDateTime.ToString();
        }
    }
}
  