using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using EasySaveG6.Model;
using System.ComponentModel;

namespace EasySaveG6.ViewModel
{
    class travaux_sauvegarde : EasySaveG6.Model.File, INotifyPropertyChanged
    {
        private string sauvegarde { get; set; }

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        public travaux_sauvegarde()
        {
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public travaux_sauvegarde(int sourcePath)
        {
            string fRead = System.IO.File.ReadAllText(@"..\..\..\Save\travaux_sauvegarde.json");
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
            var f = System.IO.File.Exists(@"..\..\..\Save\travaux_sauvegarde.json");
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

            if (!System.IO.File.Exists(@"..\..\..\Save\travaux_sauvegarde.json"))
            {
                System.IO.File.Create(@"..\..\..\Save\travaux_sauvegarde.json");
            }

            string fRead = System.IO.File.ReadAllText(@"..\..\..\Save\travaux_sauvegarde.json");
            var workList = JsonConvert.DeserializeObject<List<travaux_sauvegarde>>(fRead);

            return workList;
        }

        public List<travaux_sauvegarde> displayOneBackup(int index)
        {
            if (index < 0)
            {
                index = 0;
            }
            if (!System.IO.File.Exists(@"..\..\..\Save\travaux_sauvegarde.json"))
            {
                System.IO.File.Create(@"..\..\..\Save\travaux_sauvegarde.json");
            }

            string fRead = System.IO.File.ReadAllText(@"..\..\..\Save\travaux_sauvegarde.json");
            var workList = JsonConvert.DeserializeObject<List<travaux_sauvegarde>>(fRead);

            return new List<travaux_sauvegarde> { workList[index] };
        }

        public List<travaux_sauvegarde> displaybackupByLeriem()
        {

            if (!System.IO.File.Exists(@"..\..\..\Save\travaux_sauvegarde.json"))
            {
                System.IO.File.Create(@"..\..\..\Save\travaux_sauvegarde.json");
            }

            string fRead = System.IO.File.ReadAllText(@"..\..\..\Save\travaux_sauvegarde.json");
            var workList = JsonConvert.DeserializeObject<List<travaux_sauvegarde>>(fRead);

            return workList;
        }

        public void executeSave(int name)
        {
            EasySaveG6.Model.File fileT = new Status(@"..\..\..\Save\traveaux_sauvegarde.json");
            Status status = new Status(backupName, sourcePath, destinationPath, type);

            var f = System.IO.File.Exists(@"..\..\..\Save\travaux_sauvegarde.json");
            if (f)
            {
                string fRead = System.IO.File.ReadAllText(@"..\..\..\Save\travaux_sauvegarde.json");
                var workList = JsonConvert.DeserializeObject<List<travaux_sauvegarde>>(fRead);

                string fReadStatus = System.IO.File.ReadAllText(@"..\..\..\Save\Status.json");
                var workListStatus = JsonConvert.DeserializeObject<List<Status>>(fReadStatus);

                Backup b = new Backup(workList[name].backupName, workList[name].type, workList[name].sourcePath, workList[name].destinationPath, workList[name].logFileType);
                b.backupUserChoice();
                string logFormat = workList[name].logFileType;
                for (var i = 0; i < workListStatus.Count; i++)
                {
                    if (workList[name].backupName == workListStatus[i].backupName)
                    {
                        LogBackupDetails(logFormat);

                        workList.RemoveAt(name);
                        var workListToString = JsonConvert.SerializeObject(workList);
                        fileT.save(workListToString, @"..\..\..\Save\travaux_sauvegarde.json");
                        break;
                    }
                }
            }
            else
            {
                System.IO.File.Create(@"..\..\..\Save\travaux_sauvegarde.json");
            }
        }

        public void SaveStatus()
        {
            Status status = new Status(backupName, sourcePath, destinationPath, type);
            status.save(status.convertStatusToJSON(""), @"..\..\..\Save\Status.json");
        }

        public int workNumber()
        {
            string fRead = System.IO.File.ReadAllText(@"..\..\..\Save\travaux_sauvegarde.json");
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
