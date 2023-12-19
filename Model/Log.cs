using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization; // Used for XML serialization
using Newtonsoft.Json;

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
        public string ConvertLogToJSON()
        {
            var toJSON = new List<Log> {
                new Log {
                    backupName = backupName,
                    type = type,
                    sourcePath = sourcePath,
                    destinationPath = destinationPath,
                    timestamp = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                    fileSize = fileSize,
                    timeTrensfertFile = timeTrensfertFile
                }
            };

            var serializedJSON = JsonConvert.SerializeObject(toJSON, Formatting.Indented);
            return serializedJSON;
        }

        // Convert the log file to XML format
        public string ConvertLogToXML(string fileName)
        {
            // Read existing XML content from the file
            string xml = System.IO.File.ReadAllText(fileName);

            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Log";
            xRoot.IsNullable = true;

            // Deserialize existing XML content into a list of Log objects
            XmlSerializer serializer = new XmlSerializer(typeof(List<Log>), xRoot);
            TextReader textReader = new StringReader(xml);
            List<Log> worklist = (List<Log>)serializer.Deserialize(textReader);

            // Add a new log entry to the list
            worklist.Add(new Log()
            {
                backupName = backupName,
                type = type,
                sourcePath = sourcePath,
                destinationPath = destinationPath,
                timestamp = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                fileSize = fileSize,
                timeTrensfertFile = timeTrensfertFile,
            });

            // Serialize the updated list back to XML
            var writer = new StringWriter();
            serializer.Serialize(writer, worklist);
            var updatedXml = writer.ToString();

            try
            {
                // Write the updated XML content back to the file
                System.IO.File.WriteAllText(fileName, updatedXml);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return updatedXml;
        }


        // Method to calculate file size for logging
        public void FileSizeLog(string fileName)
        {
            fileSize = fileName.Length;
        }

        // Method to set source and destination paths
        public void SetPath(string source, string destination)
        {
            sourcePath = source;
            destinationPath = destination;
        }

        // Method to store transfer time with TimeSpan that tracks the execution time
        public void SetTransferTime(TimeSpan transferTime)
        {
            timeTrensfertFile = transferTime;
        }

        // Method to choose log format (XML or JSON)
        public string ChooseLogFormat(string format)
        {
            if (format.ToLower() == "json")
            {
                return ConvertLogToJSON();
            }
            else if (format.ToLower() == "xml")
            {
                return ConvertLogToXML(@"..\..\..\Save\Log.xml");
            }
            else
            {
                return "Invalid log format. Please choose either XML or JSON.";
            }
        }
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
