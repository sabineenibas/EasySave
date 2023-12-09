using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using EasySaveG6.Model;

namespace EasySaveG6.ViewModel
{
    class travaux_sauvegarde : EasySaveG6.Model.File
    {
        private string sauvegarde { get; set; }


        public travaux_sauvegarde()
        {

        }

        public travaux_sauvegarde(int sourcePath)
        {
            string fRead = System.IO.File.ReadAllText(@"..\..\..\Save\travaux_sauvegarde.txt");
            var workList = JsonConvert.DeserializeObject<List<Status>>(fRead);
            this.sourcePath = workList[sourcePath - 1].sourcePath;
        }

        public travaux_sauvegarde(string backupName, string type)
        {
            this.backupName = backupName;
            this.type = type;
        }

        public travaux_sauvegarde(string backupName, string type, string sourcePath, string destinationPath, string logFileType)
        {
            this.backupName = backupName;
            this.sourcePath = sourcePath;
            this.timestamp = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            this.destinationPath = destinationPath;
            this.type = type;
            this.logFileType = logFileType;
        }

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
                        logFileType = logFileType,
                    }
                };
                sauvegarde = JsonConvert.SerializeObject(toJSONN);
            }

            return sauvegarde;
        }

        public List<travaux_sauvegarde> displayBackups()
        {
            if (!System.IO.File.Exists(@"..\..\..\Save\travaux_sauvegarde.txt"))
            {
                System.IO.File.Create(@"..\..\..\Save\travaux_sauvegarde.txt");
            }

            string fRead = System.IO.File.ReadAllText(@"..\..\..\Save\travaux_sauvegarde.txt");
            var workList = JsonConvert.DeserializeObject<List<travaux_sauvegarde>>(fRead);

            return workList;
        }

        public void executeSave(int name, string logFormat)
        {
            EasySaveG6.Model.File fileT = new Status(@"..\..\..\Save\traveaux_sauvegarde.txt");
            Status status = new Status(backupName, sourcePath, destinationPath, type);

            var f = System.IO.File.Exists(@"..\..\..\Save\travaux_sauvegarde.txt");
            if (f)
            {
                string fRead = System.IO.File.ReadAllText(@"..\..\..\Save\travaux_sauvegarde.txt");
                var workList = JsonConvert.DeserializeObject<List<travaux_sauvegarde>>(fRead);

                string fReadStatus = System.IO.File.ReadAllText(@"..\..\..\Save\Status.txt");
                var workListStatus = JsonConvert.DeserializeObject<List<Status>>(fReadStatus);

                Backup b = new Backup(workList[name - 1].backupName, workList[name - 1].type, workList[name - 1].sourcePath, workList[name - 1].destinationPath, workList[name - 1].logFileType);
                b.backupUserChoice();

                for (var i = 0; i < workListStatus.Count; i++)
                {
                    if (workList[name - 1].backupName == workListStatus[i].backupName)
                    {
                        LogBackupDetails(logFormat);

                        workList.RemoveAt(name - 1);
                        var workListToString = JsonConvert.SerializeObject(workList);
                        fileT.save(workListToString, @"..\..\..\Save\travaux_sauvegarde.txt");
                        break;
                    }
                }
            }
            else
            {
                System.IO.File.Create(@"..\..\..\Save\travaux_sauvegarde.txt");
            }
        }

        public void executeSaveMultiple(string logFormat)
        {
            EasySaveG6.Model.File fileT = new Status(@"..\..\..\Save\traveaux_sauvegarde.txt");
            Status status = new Status();

            var f = System.IO.File.Exists(@"..\..\..\Save\travaux_sauvegarde.txt");

            string fReadStatus = System.IO.File.ReadAllText(@"..\..\..\Save\Status.txt");
            var workListStatus = JsonConvert.DeserializeObject<List<Status>>(fReadStatus);

            string fRead = System.IO.File.ReadAllText(@"..\..\..\Save\travaux_sauvegarde.txt");
            var workList = JsonConvert.DeserializeObject<List<travaux_sauvegarde>>(fRead);

            for (var i = 0; i < workListStatus.Count; i++)
            {
                Backup b = new Backup(workList[i].backupName, workList[i].type, workList[i].sourcePath, workList[i].destinationPath, workList[i].logFileType);
                b.backupUserChoice();
                if (workList[i].backupName == workListStatus[i].backupName && workListStatus[i].stateSave != "END")
                {
                    LogBackupDetails(logFormat);

                    var workListToString = JsonConvert.SerializeObject(workList);
                    fileT.save(workListToString, @"..\..\..\Save\travaux_sauvegarde.txt");
                }
            }
        }

        public void SaveStatus()
        {
            Status status = new Status(backupName, sourcePath, destinationPath, type);
            status.save(status.convertStatusToJSON(""), @"..\..\..\Save\Status.txt");
        }

        public int workNumber()
        {
            string fRead = System.IO.File.ReadAllText(@"..\..\..\Save\travaux_sauvegarde.txt");
            var ccount = fRead.Split("backupName").Length;
            return ccount;
        }

        // Log the backup details
        private void LogBackupDetails(string logFormat)
        {
            Log log = Log.Instance;

            if (logFormat.ToLower() == "json")
            {
                string jsonLog = log.ChooseLogFormat("json");
                log.LogMessage(jsonLog);
            }
            else if (logFormat.ToLower() == "xml")
            {
                string xmlLog = log.ChooseLogFormat("xml");
                log.LogMessage(xmlLog);
            }
            else
            {
                log.LogMessage("Invalid log format. Please choose either XML or JSON.");
            }
        }
    }
}
