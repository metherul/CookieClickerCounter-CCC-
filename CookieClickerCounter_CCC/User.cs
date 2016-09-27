using System;
using System.Collections.Generic;

namespace CookieClickerCounter_CCC
{
    public class User
    {
        public string StudentName { get; set; }
        public string StudentID { get; set; }
        public string BakeryName { get; set; }
        public string GameVersion { get; set; }
        public string CookieCount { get; set; }
        public string CookieCountAllTime { get; set; }
        public string StartTime { get; set; }
        public string SaveTime { get; set; }
        public string AccessTimeStamp { get; set; }
        public string Active { get; set; }
        public int AccessCount { get; set; }
        public string SaveCode { get; set; }

        // Archives
        public List<object> CookieCountArchive { get; set; }
        public List<object> CookieCountAllTimeArchive { get; set; }
        public List<object> SaveTimeArchive { get; set; }
        public List<object> AccessTimeStampArchive { get; set; }
        public List<object> SaveCodeArchive { get; set; }

        public User()
        {
            SaveCodeArchive = new List<object>();
            SaveTimeArchive = new List<object>();
            CookieCountArchive = new List<object>();
            CookieCountAllTimeArchive = new List<object>();
            AccessTimeStampArchive = new List<object>();
        }

        public void Initialize(string _studentName, string _studentID, string _bakeryName, string _gameVersion, string _startTime, string _saveCode)
        {
            StudentName = _studentName;
            StudentID = _studentID;
            BakeryName = _bakeryName;
            GameVersion = _gameVersion;
            StartTime = _startTime;
            // SaveCode = _saveCode;
            Active = "true";
            AccessCount = 0;
        }

        public void Update(string _saveTime, string _cookieCount, string _cookieCountAllTime, string _saveCode)
        {
            if (SaveTime != null)
                SaveTimeArchive.Add(SaveTime);

            if (CookieCount != null)
                CookieCountArchive.Add(CookieCount);

            if (CookieCountAllTime != null)
                CookieCountAllTimeArchive.Add(CookieCountAllTime);

            if (SaveCode != null)
                SaveCodeArchive.Add(SaveCode);

            if (AccessTimeStamp != null)
                AccessTimeStampArchive.Add(AccessTimeStamp);

            SaveTime = _saveTime;
            CookieCount = _cookieCount;
            CookieCountAllTime = _cookieCountAllTime;
            AccessTimeStamp = DateTime.Now.ToString();
            AccessCount++;
        }

        public void Disable()
        {
            Active = "false";
        }
    }
}


