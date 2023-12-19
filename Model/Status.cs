using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Linq;


namespace EasySaveG6.Model
{
    // Class representing the status of a backup job
    class Status : EasySaveG6.Model.File
    {
        // Properties to store various status information
        public string stateSave { get; set; }
        public long totalFileSize { get; set; }
        public long fileLeftSize { get; set; }
        public int totalFile { get; set; }

        // Default constructor to set the timestamp when the object is created
        public Status()
        {
            timestamp = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

        // Constructor to set the source path when the object is created
        public Status(string sourcePath)
        {
            this.sourcePath = sourcePath;
        }

        // Constructor to set various properties when the object is created
        public Status(string backupName, string sourcePath, string destinationPath, string type)
        {
            this.type = type;
            this.backupName = backupName;
            this.sourcePath = sourcePath;
            this.destinationPath = destinationPath;
            timestamp = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            // Calculate total file size, total number of files, and initialize fileLeftSize
            DirectoryInfo di = new DirectoryInfo(sourcePath);
            totalFileSize = di.EnumerateFiles("*.*", SearchOption.AllDirectories).Sum(fi => fi.Length);
            totalFile = Directory.EnumerateFiles(sourcePath).Count();
            fileLeftSize = totalFileSize; // Initialize fileLeftSize to totalFileSize
        }

        // Method to convert the status object to JSON format
        public string convertStatusToJSON(string a)
        {
            if (a == "END")
            {
                stateSave = a;
            }
            else
            {
                stateSave = "ACTIVE";
            }

            var toJSON = new List<Status> {
                new Status{
                    backupName = backupName,
                    destinationPath = destinationPath,
                    sourcePath = sourcePath,
                    stateSave = stateSave,
                    timestamp = timestamp,
                    totalFileSize = totalFileSize,
                    fileLeftSize = fileLeftSize,
                    totalFile = totalFile,
                    type = type
                }
            };
            
            // Serialize the status object to JSON format
            var serializedJSON = JsonConvert.SerializeObject(toJSON, Formatting.Indented);
            return serializedJSON;
        }

        // Method to update the total file size based on the source path
        public void fileSizeStatus(string source)
        {
            DirectoryInfo di = new DirectoryInfo(source);
            totalFileSize = di.EnumerateFiles("*.*", SearchOption.AllDirectories).Sum(fi => fi.Length);
        }

        // Method to update the fileLeftToSave property based on the number of files left to save
        public void fileLeftToSavee(int left)
        {
            //this.fileLeftToSave = this.totalFile - left;
        }

        // Method to set the source and destination paths
        public void path(string source, string destination)
        {
            sourcePath = source;
            destinationPath = destination;
        }

        // Method to convert the final status to JSON format after the backup job is completed
        public string finalConvertStatusToJSON(string workList, int i)
        {
            // Deserialize the JSON string to a list of Status objects
            var workListStatus = JsonConvert.DeserializeObject<List<Status>>(workList);

            // Update the state and timestamp of the specified status object
            workListStatus[i].stateSave = "END";
            workListStatus[i].timestamp = timestamp;

            // Serialize the updated list of Status objects to JSON format
            var serializedJSON = JsonConvert.SerializeObject(workListStatus, Formatting.Indented);
            return serializedJSON;
        }
    }
}
