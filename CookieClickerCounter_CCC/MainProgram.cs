using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            //SetupDatabase();
            UpdateDatabase("866980", "9500", "20", "2139013290901");
        }

        public void SetupDatabase()
        {
            List<User> userList = new List<User>();
            User user = new User();
            bool isRunning = true;

            user = new User();
            user.Initialize("Ethan Green", "866980");

            userList.Add(user);

            string jsonString = JsonConvert.SerializeObject(userList, Formatting.Indented);

            File.WriteAllText(databaseLocation, jsonString);
        }

        public void UpdateDatabase(string _studentID, string _cookieCount, string _cookiesPerSecond, string _saveString)
        {
            int counter = 0;

            string jsonData = File.ReadAllText(databaseLocation);
            string studentName;
            string studentID = _studentID;
            string cookieCount = _cookieCount;
            string cookiesPerSecond = _cookiesPerSecond;
            string saveString = _saveString;

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

            studentName = jsonDocument[counter].Name;

            // Oh god
            // Cookie count

            if (jsonDocument[counter].CookieCount != null)
            {
                jsonDocument[counter].CookieCountArchive.Add(jsonDocument[counter].CookieCount);
                jsonDocument[counter].CookieCount = cookieCount;
            }

            else
                jsonDocument[counter].CookieCount = cookieCount;

            // Cookies per second

            if (jsonDocument[counter].CookiesPerSecond != null)
            {
                jsonDocument[counter].CookiesPerSecondArchive.Add(jsonDocument[counter].CookieCount);
                jsonDocument[counter].CookiesPerSecond = cookiesPerSecond;
            }

            else
                jsonDocument[counter].CookiesPerSecond = cookiesPerSecond;

            // SaveString

            if (jsonDocument[counter].SaveString != null)
            {
                jsonDocument[counter].SaveStringArchive.Add(jsonDocument[counter].SaveString);
                jsonDocument[counter].SaveString = saveString;
            }

            else
                jsonDocument[counter].SaveString = saveString;

            // TimeStamp

            if (jsonDocument[counter].TimeStamp != null)
            {
                jsonDocument[counter].TimeStampArchive.Add(jsonDocument[counter].TimeStamp);
                jsonDocument[counter].TimeStamp = DateTime.Now.ToString();
            }

            else
                jsonDocument[counter].TimeStamp = DateTime.Now.ToString();

            string jsonString = JsonConvert.SerializeObject(jsonDocument, Formatting.Indented);

            File.WriteAllText(databaseLocation, jsonString);

        }

        public void ClearDatabase()
        {
            bool run = false;

            if (run)
            {
                File.Delete(databaseLocation);
            }
        }
    }
}
