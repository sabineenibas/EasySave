using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Net.NetworkInformation;
using EasySaveG6.Model;// 

namespace EasySaveG6.ViewModel
{

    class Backup : EasySaveG6.Model.File
    {
        public string tmp { get; set; }

        private string source { get; set; }
        private string destination { get; set; }

        public Backup(string backupName)
        {
            this.backupName = backupName;
        }
        public Backup(string source, string destination)
        {
            this.source = source;
            this.destination = destination;
        }
        public Backup(string backupName, string type, string sourcePath, string destinationPath)
        {
            this.backupName = backupName;
            this.sourcePath = sourcePath;
            this.destinationPath = destinationPath;
            this.type = type;


        }
        public void backupUserChoice()
        {




            if (type == "Full")
            {
                Full();
            }
            else
            {
                differential();
            }
        }
        public void Full()
        {
            Log str = new Log(backupName, type);
            Status status = new Status(backupName, sourcePath, destinationPath, type);
            EasySaveG6.Model.File fileS = new Status(@"");
            EasySaveG6.Model.File fileC = new Log(@"..\..\..\Save\Log.txt");


            try
            {
                var i = 0;
                foreach (var file in Directory.GetFiles(sourcePath))
                {
                    System.IO.File.Copy(file, Path.Combine(destinationPath, Path.GetFileName(file)), true);

                    str.path(Path.Combine(sourcePath, Path.GetFileName(file)), Path.Combine(destinationPath, Path.GetFileName(file)));
                    str.fileSizeLog(Path.Combine(sourcePath, Path.GetFileName(file)));
                    status.fileSizeStatus(sourcePath);
                    status.fileLeftToSavee(i);

                    fileC.save(str.convertLogToJSON(), @"..\..\..\Save\Log.txt");
                    i++;

                }

            }

            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }

        }
        public void differential()
        {
            DateTime lastModified;
            DateTime lastModified2;
            Log str = new Log(backupName, type);
            Status status = new Status(backupName, sourcePath, destinationPath, type);
            EasySaveG6.Model.File fileS = new Status(@"..\..\..\Save\Status.txt");
            EasySaveG6.Model.File fileC = new Log(@"..\..\..\Save\Log.txt");

            var i = 0;

            try
            {
                string[] txtList = Directory.GetFiles(sourcePath);

                // Copy text files.
                foreach (string file in txtList)
                {
                    str.fileSizeLog(file);
                    // Remove path from the file name.
                    string fName = file.Substring(sourcePath.Length + 1);

                    try
                    {

                        // Will not overwrite if the destination file already exists.
                        System.IO.File.Copy(Path.Combine(sourcePath, fName), Path.Combine(destinationPath, fName));

                        str.path(Path.Combine(sourcePath, Path.GetFileName(file)), Path.Combine(destinationPath, Path.GetFileName(file)));
                        status.path(Path.Combine(sourcePath, Path.GetFileName(file)), Path.Combine(destinationPath, Path.GetFileName(file)));
                        str.fileSizeLog(Path.Combine(sourcePath, Path.GetFileName(file)));
                        status.fileSizeStatus(sourcePath);
                        status.fileLeftToSavee(i);
                        fileC.save(str.convertLogToJSON(), @"..\..\..\Save\Log.txt");
                    }

                    // Catch exception if the file was already copied.
                    catch (IOException copyError)
                    {
                        lastModified = System.IO.File.GetLastWriteTime(Path.Combine(sourcePath, Path.GetFileName(file)));
                        lastModified2 = System.IO.File.GetLastWriteTime(Path.Combine(destinationPath, Path.GetFileName(file)));

                        Console.WriteLine(copyError.Message);
                        if (lastModified != lastModified2)
                        {
                            System.IO.File.Copy(Path.Combine(sourcePath, fName), Path.Combine(destinationPath, fName), true);
                        }
                        str.path(Path.Combine(sourcePath, Path.GetFileName(file)), Path.Combine(destinationPath, Path.GetFileName(file)));
                        str.fileSizeLog(Path.Combine(sourcePath, Path.GetFileName(file)));
                        status.fileSizeStatus(sourcePath);
                        status.fileLeftToSavee(i);

                        fileC.save(str.convertLogToJSON(), @"..\..\..\Save\Log.txt");
                    }
                    i++;

                }



            }

            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);
            }

        }
    }
}