using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieClickerCounter_CCC
{
    public class User
    {
        public string Active { get; set; }
        public string Name { get; set; }
        public string StudentID { get; set; }
        public string CookieCount { get; set; }
        public string CookiesPerSecond { get; set; }
        public string SaveString { get; set; }
        public string TimeStamp { get; set; }
        public List<object> CookieCountArchive { get; set; }
        public List<object> CookiesPerSecondArchive { get; set; }
        public List<object> SaveStringArchive { get; set; }
        public List<object> TimeStampArchive { get; set; }

        public User()
        {
            CookieCountArchive = new List<object>();
            CookiesPerSecondArchive = new List<object>();
            SaveStringArchive = new List<object>();
            TimeStampArchive = new List<object>();
        }

        public void Initialize(string _name, string _studentID)
        {
            Name = _name;
            StudentID = _studentID;
            Active = "true";
        }

        public void Update(string _cookieCount, string _cookiesPerSecond, string _saveString)
        {
            if (CookieCount != null)
                CookieCountArchive.Add(CookieCount);

            if (CookiesPerSecond != null)
                CookiesPerSecondArchive.Add(CookiesPerSecond);

            if (SaveString != null)
                SaveStringArchive.Add(SaveString);

            if (TimeStamp != null)
                TimeStampArchive.Add(TimeStamp);

            if (CookieCount != null)
                CookieCountArchive.Add(CookieCount);


            CookieCount = _cookieCount;
            CookiesPerSecond = _cookiesPerSecond;
            SaveString = _saveString;
            TimeStamp = DateTime.Now.ToString();
        }

        public void Disable()
        {
            Active = "false";
        }
    }
}


