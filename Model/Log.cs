using System;
using System.Collections.Generic; // Used for data types
using System.Text;
using System.IO; // Used to manage files and directories
using System.Threading; // Used for progression tests 
using System.Diagnostics; // Used to calculate time
using Newtonsoft.Json; // Used to parse Json data 
using System.Linq;


using System;

namespace EasySaveG6.Model
{
    public class Log : File
    {
        private static Log instance;
        private static readonly object lockObject = new object();
        public double fileSize { get; set; }
        public TimeSpan timeTrensfertFile { get; set; }

        private Log()
        {
            // Private constructor to prevent instantiation outside of this class.
        }

        public static Log Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new Log();
                        }
                    }
                }
                return instance;
            }
        }

        public void LogMessage(string message)
        {
            Console.WriteLine(message);
        }

        // Convert the log file to JSON format
        public string convertLogToJSON()
        {
            var toJSON = new List<Log> {
                new Log {
                    backupName = backupName,
                    type = type,
                    sourcePath = sourcePath,
                    destinationPath = destinationPath,
                    timestamp = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), // Convert current time to string 
                    fileSize = fileSize,
                    timeTrensfertFile = timeTrensfertFile
                }
            };

            // Convert a list to JSON
            var serializedJSON = JsonConvert.SerializeObject(toJSON, Formatting.Indented);
            return serializedJSON; // Return the converted list
        }

        // Method to calculate file size for logging
        public void fileSizeLog(string fileName)
        {
            fileSize = fileName.Length; // Calculate the length of the fileName string 
        }

        // Method to set source and destination paths
        public void path(string source, string destination)
        {
            sourcePath = source;
            destinationPath = destination;
        }

        // Method to store transfer time with TimeSpan that tracks the execution time
        public void timeTrensfert(TimeSpan timeTrensfertFile)
        {
            this.timeTrensfertFile = timeTrensfertFile;
        }
    }

}