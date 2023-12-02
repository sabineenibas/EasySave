using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using EasySaveG6.ViewModel;

namespace EasySaveG6.View
{
        internal class Program
    {
        static void Main(string[] args)
        {
            //start point 
            Console.WriteLine(" ______                                    ");
            Console.WriteLine("|  ____|                                   ");
            Console.WriteLine("| |__   __ _ ___ _   _ ___  __ ___   _____ ");
            Console.WriteLine("|  __| / _` / __| | | / __|/ _` \\ \\ / / _ \\");
            Console.WriteLine("| |___| (_| \\__ \\ |_| \\__ \\ (_| |\\ V /  __/");
            Console.WriteLine("|______\\__,_|___/\\__, |___/\\__,_| \\_/ \\___|");
            Console.WriteLine("                  __/ |                    ");
            Console.WriteLine("                 |___/                   GROUPE 6\n");


            travaux_sauvegarde travaux_sauvegarde = new travaux_sauvegarde("cc", "0", @"C:\Users\Pcnet\Desktop\cc", @"C:\Users\Pcnet\Desktop\cc2");
            List<travaux_sauvegarde> s = travaux_sauvegarde.displayBackups();
            int tCount = s.Count;
            if (s.Count > 0)
            {
            s.ForEach(travaux => Console.WriteLine(" |*| " + travaux.backupName + " || " + travaux.sourcePath + " || " + travaux.destinationPath + "\n"));
            }
            else
            {
                Console.WriteLine("Pas de travail de sauvegarde pour le moment ! \n");
            }
            int L = 1;
            // get informations from user 
            if (L == 1) // language in french
            {
                Console.WriteLine("1-Creer un travail de sauvegarde             2-Executer un travail de sauvegarde ");
            }
            else if (L == 2) // language in english
            {

                Console.WriteLine("1-Create a new backup             2-Execute a backup ");
            }
            string user = Console.ReadLine();

            if (user == "2")
            {
                if (L == 1)
                {
                    Console.WriteLine("1-Executer une sauvegarde unitaire             2-Executer une sauvegarde Sequentielle");
                }
                else if (L == 2)
                {
                    Console.WriteLine("1-Execute a single backup             2-Execute a sequential backup");
                }

                int user2 = Convert.ToInt32(Console.ReadLine());
                if (user2 == 1)
                {
                    if (L == 1)
                    {
                        Console.WriteLine("1-Choisissez une sauvegarde (1-5)"); // ask the user to choose from 1 to 5 backups 
                    }
                    else if (L == 2)
                    {
                        Console.WriteLine("1-Select a backup (1-5)"); // select a backup from the backups
                    }
                    // execute a single backup from the class travaux_sauvegarde
                    int sauvegardeIndex = Convert.ToInt32(Console.ReadLine());
                    travaux_sauvegarde.executeSave(sauvegardeIndex);
                }
                else if (user2 == 2)
                {
                    // execute a sequential backup from the class travaux_sauvegarde
                    travaux_sauvegarde.executeSaveMultiple();

                }

            }
            // creation of a backup
            else if (user == "1" && tCount < 5)
            {
                if (L == 1)
                {
                    Console.WriteLine("Veuillez entrer le nom de la sauvegarde :");
                }
                else if (L == 2)
                {
                    Console.WriteLine("Please enter the name of the backup :");
                }
                string backupName = Console.ReadLine();
                if (L == 1)
                {
                    Console.WriteLine("Veuillez selectionner un repertoire source :");
                }
                else if (L == 2)
                {
                    Console.WriteLine("Please select a source directory :");
                }
                string source = Console.ReadLine();
                if (L == 1)
                {
                    Console.WriteLine("Veuillez selectionner un repertoire destination :");
                }
                else if (L == 2)
                {
                    Console.WriteLine("Please select a destination directory :");
                }

                string destination = Console.ReadLine();
                if (L == 1)
                {
                    Console.WriteLine("Veuillez selectionner un type de sauvegarde :");
                    Console.WriteLine(" 1- Complete                   2- differentielle");
                }
                else if (L == 2)
                {
                    Console.WriteLine("Please select a backup type :");
                    Console.WriteLine(" 1- Complete                   2- Differential");
                }
                string type = Console.ReadLine();

                // execute a sequential backup from the class travaux_sauvegarde
                travaux_sauvegarde k = new travaux_sauvegarde(backupName, type, source, destination);
                if (tCount < 5)
                {
                    k.save(k.travaux_sauvegardeToJSON(), @"..\..\..\Save\travaux_sauvegarde.txt");
                }

            }
            else
            {
                Console.WriteLine("Deja 5 travaux de sauvegarde, veuillez en supprimer ou lancer aumoin 1");
            }
        }
    }
}