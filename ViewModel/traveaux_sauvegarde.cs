using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Windows;
using System.Reflection;
using EasySaveG6.Model;
using System.Threading;

namespace EasySaveG6.ViewModel
{
    // ViewModel class for handling backup tasks
    class travaux_sauvegarde : EasySaveG6.Model.File
    {
        // Property to store the current backup
        private string sauvegarde { get; set; }

        // Default constructor
        public travaux_sauvegarde()
        {

        }

        // Constructor to initialize sourcePath based on index
        public travaux_sauvegarde(int sourcePath)
        {
            // Read the saved backup tasks from file
            string fRead = System.IO.File.ReadAllText(@"..\..\..\Save\travaux_sauvegarde.txt");
            var workList = JsonConvert.DeserializeObject<List<Status>>(fRead);

            // Set the sourcePath based on the provided index
            this.sourcePath = workList[sourcePath - 1].sourcePath;
        }

        // Constructor to initialize backupName and type
        public travaux_sauvegarde(string backupName, string type)
        {
            this.backupName = backupName;
            this.type = type;
        }

        // Constructor to initialize all properties
        public travaux_sauvegarde(string backupName, string type, string sourcePath, string destinationPath)
        {
            this.backupName = backupName;
            this.sourcePath = sourcePath;
            this.timestamp = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            this.destinationPath = destinationPath;
            this.type = type;
        }

        // Convert the current backup to JSON format
        public string travaux_sauvegardeToJSON()
        {
            var f = System.IO.File.Exists(@"..\..\..\Save\travaux_sauvegarde.txt");
            if (f)
            {
                var toJSONN = new List<travaux_sauvegarde> {
                    new travaux_sauvegarde{
                        backupName = backupName,
                        type = type,
                        sourcePath = sourcePath,
                        destinationPath = destinationPath,
                        timestamp = timestamp,
                    }
                };
                // Serialize the backup to JSON format
                sauvegarde = JsonConvert.SerializeObject(toJSONN);
            }

            return sauvegarde;
        }

        // Display the list of backup tasks
        public List<travaux_sauvegarde> displayBackups()
        {
            // Create the file if it doesn't exist
            if (!System.IO.File.Exists(@"..\..\..\Save\travaux_sauvegarde.txt"))
            {
                System.IO.File.Create(@"..\..\..\Save\travaux_sauvegarde.txt");
            }
            // Read the saved backup tasks from file
            string fRead = System.IO.File.ReadAllText(@"..\..\..\Save\travaux_sauvegarde.txt");
            var workList = JsonConvert.DeserializeObject<List<travaux_sauvegarde>>(fRead);

            return workList;
        }


        // Execute a single backup task based on the provided name
        public void executeSave(int name)
        {
            // Initialize fileT and status objects
            EasySaveG6.Model.File fileT = new Status(@"..\..\..\Save\traveaux_sauvegarde.txt");
            Status status = new Status(backupName, sourcePath, destinationPath, type);

            // Check if the file containing backup tasks exists
            var f = System.IO.File.Exists(@"..\..\..\Save\travaux_sauvegarde.txt");
            if (f)
            {
                // Read the saved backup tasks and status from file
                string fRead = System.IO.File.ReadAllText(@"..\..\..\Save\travaux_sauvegarde.txt");
                var workList = JsonConvert.DeserializeObject<List<travaux_sauvegarde>>(fRead);

                string fReadStatus = System.IO.File.ReadAllText(@"..\..\..\Save\Status.txt");
                var workListStatus = JsonConvert.DeserializeObject<List<Status>>(fReadStatus);

                // Create a Backup object based on the specified backup task
                Backup b = new Backup(workList[name - 1].backupName, workList[name - 1].type, workList[name - 1].sourcePath, workList[name - 1].destinationPath);
                b.backupUserChoice();

                // Iterate through the status list to find and remove the executed backup task
                for (var i = 0; i < workListStatus.Count; i++)
                {
                    if (workList[name - 1].backupName == workListStatus[i].backupName)
                    {
                        workList.RemoveAt(name - 1);
                        var workListToString = JsonConvert.SerializeObject(workList);
                        fileT.save(workListToString, @"..\..\..\Save\travaux_sauvegarde.txt");
                        break;
                    }
                }
            }
            else
            {
                // Create the file if it doesn't exist
                System.IO.File.Create(@"..\..\..\Save\travaux_sauvegarde.txt");
            }
        }

        // Execute multiple backup tasks based on their status
        public void executeSaveMultiple()
        {
            // Initialize fileT and status objects
            EasySaveG6.Model.File fileT = new Status(@"..\..\..\Save\traveaux_sauvegarde.txt");
            Status status = new Status();

            // Check if the file containing backup tasks exists
            var f = System.IO.File.Exists(@"..\..\..\Save\travaux_sauvegarde.txt");

            // Read the saved backup tasks and status from file
            string fReadStatus = System.IO.File.ReadAllText(@"..\..\..\Save\Status.txt");
            var workListStatus = JsonConvert.DeserializeObject<List<Status>>(fReadStatus);

            string fRead = System.IO.File.ReadAllText(@"..\..\..\Save\travaux_sauvegarde.txt");
            var workList = JsonConvert.DeserializeObject<List<travaux_sauvegarde>>(fRead);

            // Iterate through the backup tasks and execute them based on their status
            for (var i = 0; i < workListStatus.Count; i++)
            {
                Backup b = new Backup(workList[i].backupName, workList[i].type, workList[i].sourcePath, workList[i].destinationPath);
                b.backupUserChoice();
                if (workList[i].backupName == workListStatus[i].backupName && workListStatus[i].stateSave != "END")
                {
                    // Update the status of the backup task
                    var workListToString = JsonConvert.SerializeObject(workList);
                    fileT.save(workListToString, @"..\..\..\Save\travaux_sauvegarde.txt");
                }
            }
        }

        // Save the status of the current backup task
        public void SaveStatus()
        {
            // Create a Status object and save its JSON representation to a file
            Status status = new Status(backupName, sourcePath, destinationPath, type);
            status.save(status.convertStatusToJSON(""), @"..\..\..\Save\Status.txt");
        }

        // Get the number of backup tasks
        public int workNumber()
        {
            // Read the saved backup tasks from file and count them
            string fRead = System.IO.File.ReadAllText(@"..\..\..\Save\travaux_sauvegarde.txt");
            var ccount = fRead.Split("backupName").Length;
            return ccount;
        }
    }

}



