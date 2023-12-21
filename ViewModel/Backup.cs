using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using EasySaveG6.Model;

namespace EasySaveG6.ViewModel
{
    class Backup : EasySaveG6.Model.File
    {
        public Backup() { }
        public string tmp { get; set; }
        private readonly object lockObject = new object();
        private string source;
        private string destination;

        public Backup(string backupName)
        {
            this.backupName = backupName;
        }

        public Backup(string source, string destination)
        {
            this.source = source;
            this.destination = destination;
        }

        public Backup(string backupName, string type, string sourcePath, string destinationPath, string logFileType)
        {
            this.backupName = backupName;
            this.sourcePath = sourcePath;
            this.destinationPath = destinationPath;
            this.type = type;
            this.logFileType = logFileType;
        }

        public void BackupByLeryem(string backupName, string type, string sourcePath, string destinationPath, string logFileType)
        {
            this.backupName = backupName;
            this.sourcePath = sourcePath;
            this.destinationPath = destinationPath;
            this.type = type;
            this.logFileType = logFileType;
        }

        public void backupUserChoice()
        {
            if (this.type == "1")
            {
                Full();
            }
            else
            {
                Differential();
            }
        }
        static bool IsCalculatorRunning()
        {
            Process[] processes = Process.GetProcessesByName("CalculatorApp");
            return processes.Length > 0;
        }

        public void Full()
        {
            Log str = Log.Instance;
            Status status = new Status(backupName, sourcePath, destinationPath, type);
            EasySaveG6.Model.File fileC = Log.Instance;
            fileC.backupName = backupName;
            fileC.sourcePath = sourcePath;
            fileC.destinationPath = destination;
            fileC.type = type;
            fileC.logFileType = logFileType;

            try
            {
                var files = Directory.GetFiles(sourcePath);

                Parallel.ForEach(files, file =>
                {
                    if (IsCalculatorRunning())
                    {
                        while (IsCalculatorRunning())
                        {
                            MessageBox.Show("[PAUSE] La calculatrice est en cours d'exécution.");
                        }
                    }
                    // Process each file in parallel
                    lock (lockObject)
                    {
                        if (new FileInfo(file).Length < 10 * 1024 * 1024)
                        {
                            Encrypt(sourcePath, destinationPath);
                            System.IO.File.Copy(file, Path.Combine(destinationPath, Path.GetFileName(file)), true);
                        }
                    }

                    lock (lockObject)
                    {
                        str.path(Path.Combine(sourcePath, Path.GetFileName(file)), Path.Combine(destinationPath, Path.GetFileName(file)));
                        str.fileSizeLog(Path.Combine(sourcePath, Path.GetFileName(file)));
                        status.fileSizeStatus(sourcePath);
                        status.fileLeftToSavee(files.Length);
                    }
                });


                if (logFileType == "1")
                {
                    lock (lockObject)
                    {
                        fileC.save(str.convertLogToJSON(), @"..\..\..\Save\Log.txt");
                    }
                }
                else
                {
                    lock (lockObject)
                    {
                        fileC.save2(str.ConvertLogToXML(@"..\..\..\Save\Log.xml"), @"..\..\..\Save\Log.xml");
                    }
                }
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
        }

        public void Differential()
        {
            DateTime lastModified;
            DateTime lastModified2;
            Log str = Log.Instance;
            Status status = new Status(backupName, sourcePath, destinationPath, type);
            EasySaveG6.Model.File fileC = Log.Instance;
            fileC.backupName = backupName;
            fileC.sourcePath = sourcePath;
            fileC.destinationPath = destination;
            fileC.type = type;
            fileC.logFileType = logFileType;
            EasySaveG6.Model.File fileS = new Status(@"..\..\..\Save\Status.txt");

            try
            {
                string[] files = Directory.GetFiles(sourcePath);

                Parallel.ForEach(files, file =>
                {
                    if (IsCalculatorRunning())
                    {
                        while (IsCalculatorRunning())
                        {
                            MessageBox.Show("[PAUSE] La calculatrice est en cours d'exécution.");
                        }
                    }
                    // Process each file in parallel
                    lock (lockObject)
                    {
                        str.fileSizeLog(file);
                        string fName = file.Substring(sourcePath.Length + 1);

                        try
                        {
                            if (new FileInfo(file).Length < 10 * 1024 * 1024)
                            {
                                Encrypt(sourcePath, destinationPath);
                                System.IO.File.Copy(Path.Combine(sourcePath, fName), Path.Combine(destinationPath, fName));
                            }

                            lock (lockObject)
                            {
                                str.path(Path.Combine(sourcePath, Path.GetFileName(file)), Path.Combine(destinationPath, Path.GetFileName(file)));
                                status.path(Path.Combine(sourcePath, Path.GetFileName(file)), Path.Combine(destinationPath, Path.GetFileName(file)));
                                str.fileSizeLog(Path.Combine(sourcePath, Path.GetFileName(file)));
                                status.fileSizeStatus(sourcePath);
                                status.fileLeftToSavee(files.Length);
                            }
                        }
                        catch (IOException copyError)
                        {
                            lastModified = System.IO.File.GetLastWriteTime(Path.Combine(sourcePath, Path.GetFileName(file)));
                            lastModified2 = System.IO.File.GetLastWriteTime(Path.Combine(destinationPath, Path.GetFileName(file)));

                            Console.WriteLine(copyError.Message);
                            if (lastModified != lastModified2)
                            {
                                if (new FileInfo(file).Length < 10 * 1024 * 1024)
                                {
                                    Encrypt(sourcePath, destinationPath);
                                    System.IO.File.Copy(Path.Combine(sourcePath, fName), Path.Combine(destinationPath, fName), true);
                                }
                            }

                            lock (lockObject)
                            {
                                str.path(Path.Combine(sourcePath, Path.GetFileName(file)), Path.Combine(destinationPath, Path.GetFileName(file)));
                                str.fileSizeLog(Path.Combine(sourcePath, Path.GetFileName(file)));
                                status.fileSizeStatus(sourcePath);
                                status.fileLeftToSavee(files.Length);
                                if (logFileType == "1")
                                {
                                    fileC.save(str.convertLogToJSON(), @"..\..\..\Save\Log.txt");
                                }
                                else
                                {
                                    fileC.save2(str.ConvertLogToXML(@"..\..\..\Save\Log.xml"), @"..\..\..\Save\Log.xml");
                                }
                            }
                        }
                    }
                });
            }
            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);
            }
        }



        public void DeleteBackup()
        {
            try
            {
                // Ajoutez la logique de suppression ici
                if (Directory.Exists(destinationPath))
                {
                    Directory.Delete(destinationPath, true);
                    MessageBox.Show($"La sauvegarde '{backupName}' a été supprimée avec succès.");
                }
                else
                {
                    MessageBox.Show($"La sauvegarde '{backupName}' n'existe pas.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression de la sauvegarde '{backupName}': {ex.Message}");
            }
        }

        public void Encrypt(string source, string destination)
        {
            using Process processus = new Process();
            ProcessStartInfo infoProcessus = new ProcessStartInfo
            {
                FileName = @"..\..\..\Cryptosoft\Cryptosoft.exe",
                Arguments = $"{source} {destination}",
                // Specify other parameters if necessary
            };
            processus.StartInfo = infoProcessus;
            processus.Start();
        }
    }
}
