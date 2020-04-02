using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PowerBiometrics
{
    public static class Utility
    {
        private static string vEmployeeID = "";
        private static string vEmployeePassword = "";
        private static bool VisSyncStillOn;

        public static string EmployeeID
        {
            get { return vEmployeeID; }
            set { vEmployeeID = value; }
        }
        public static string EmployeePassword
        {
            get { return vEmployeePassword; }
            set { vEmployeePassword = value; }
        }
        public static bool isSyncStillOn
        {
            get { return VisSyncStillOn; }
            set { VisSyncStillOn = value; }
        }
        // get name of connection config file
        public static string FILE_NAME = Directory.GetCurrentDirectory() + "\\" + "ConfigFile.txt";
        public static System.IO.StreamReader objReader = new System.IO.StreamReader(FILE_NAME);

        //get read file for connection string
        public static string CONNECTION_STRING = objReader.ReadLine();
        public static string connectionString = CONNECTION_STRING;

        // get Token From Text File
        static string FileContentRead = Directory.GetCurrentDirectory() + "\\" + "Token.txt";
        public static System.IO.StreamReader objReader2 = new System.IO.StreamReader(FileContentRead);
        public static string FileContent = objReader2.ReadLine();
        public static string content = FileContentRead;

        static char[] delimiter = new char[] { '|' };

        //extract company attributes
        static string[] FileContentArray = FileContent.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
        public static string Token = FileContentArray[0];
        //public static string Token = FileContent;

        public static string APIBaseUrl = FileContentArray[1];
    }
}