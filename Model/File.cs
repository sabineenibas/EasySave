using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace EasySaveG6.Model
{
    public class File
    {
        // initialization of the varaibles
        public string backupName { get; set; }
        public string destinationPath { get; set; }
        public string sourcePath { get; set; }
        public string timestamp { get; set; }
        public string type { get; set; }

        public File() // file class
        {

        }

        public void save(string serializedJSON, string fileName) // method save 
        {
            string fRead = System.IO.File.ReadAllText(fileName);
            if (fRead == "")
            {
                fRead = fRead.Remove(fRead.Length - 1, 1);
                fRead = serializedJSON;
            }
            else if (fRead != "")
            {
                fRead = fRead.Remove(fRead.Length - 1, 1);
                fRead = fRead + ", " + serializedJSON.Remove(0, 1);
            }

            else
            {
                fRead = serializedJSON;
            }
           

            try
            {
                System.IO.File.WriteAllText(fileName, fRead);

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()); // automatic error message
            }

        }

    }
}