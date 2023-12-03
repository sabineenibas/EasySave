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

        


    }

}


