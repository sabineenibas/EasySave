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
            Console.WriteLine(" _________________________________________________________________________ ");
            Console.WriteLine("| ____|     /   | /  ___/ | |  / /      /  ___/     /   | | |   / / | ____|");
            Console.WriteLine("| |__      / /| | | |___   | |/ /       | |___     / /| | | |  / /  | |__  ");
            Console.WriteLine("|  __|    / / | | |___  |   |  /         ____     / / | | | | / /   |  __| ");
            Console.WriteLine("| |___   / /  | |  ___| |   / /          ___| |  / /  | | | |/ /    | |___ ");
            Console.WriteLine("|_____| /_/   |_| /_____/  /_/          /_____/ /_/   |_| |___/     |_____|");
            Console.WriteLine("   _____    ");
            Console.WriteLine("  / ____|  / ");
            Console.WriteLine(" | |  __  /__/ _");
            Console.WriteLine(" | | |_ |/ _` | ");
            Console.WriteLine(" | |__| | (_| | ");
            Console.WriteLine(" |______|\\___|");

            travaux_sauvegarde travaux_sauvegarde = new travaux_sauvegarde("cc", "0", @"C:\Users\Pcnet\Desktop\cc", @"C:\Users\Pcnet\Desktop\cc2", "1");
            List<travaux_sauvegarde> s = travaux_sauvegarde.displayBackups();
            int tCount = s.Count;
            if (tCount > 0)
            {

                s.ForEach(travaux => Console.WriteLine(" |*| " + travaux.backupName + " || " + travaux.sourcePath + " || " + travaux.destinationPath + "\n"));
            }
            else
            {
                Console.WriteLine("Pas de travail de sauvegarde pour le moment ! \n");
            }
            Console.WriteLine("------------------------- Choose a language || sélectionnez une langue ---------------------------");
            Console.WriteLine("|                    1. Français                                                                  |");
            Console.WriteLine("|                    2. English                                                                   | ");
            Console.WriteLine("|_________________________________________________________________________________________________|");
            int L = Convert.ToInt32(Console.ReadLine());
            // get informations from user 
            if (L == 1) // language in french
            {
                Console.WriteLine("------------------------- Menu ---------------------------");
                Console.WriteLine("|                    1. Créer une sauvegarde a backup job |");
                Console.WriteLine("|                    2. Executer une sauvegarde           | ");
                Console.WriteLine("|_________________________________________________________|");
                Console.WriteLine("entrez le numero 1 ou 2:");
            }
            else if (L == 2) // language in english
            {

                Console.WriteLine("------------------------- Menu ------------------------");
                Console.WriteLine("|                    1. Execute a backup job           |");
                Console.WriteLine("|                    2. Create a backup job            |");
                Console.WriteLine("|______________________________________________________|");
                Console.WriteLine("Please enter the number  1 or 2:");
            }
            string user = Console.ReadLine();

            if (user == "2")
            {
                if (L == 1)
                {
                    Console.WriteLine("------------------------- Menu -------------------------------");
                    Console.WriteLine("|                    1. Executer une sauvegarde unitaire      |");
                    Console.WriteLine("|                    2. executer une sauvegarde séquentiel    |");
                    Console.WriteLine("|_____________________________________________________________|");
                    Console.WriteLine("entrez le numero 1 ou 2:");
                }
                else if (L == 2)
                {

                    Console.WriteLine("------------------------- Menu -----------------------");
                    Console.WriteLine("|                    1. Execute a single backup       |");
                    Console.WriteLine("|                    2. Execute a sequential backup   |");
                    Console.WriteLine("|_____________________________________________________|");
                }

                int user2 = Convert.ToInt32(Console.ReadLine());
                if (user2 == 1)
                {
                    if (L == 1)
                    {
                        // ask the user to choose from 1 to 5 backups
                        Console.WriteLine("------------------------- Menu -----------------------");
                        Console.WriteLine("|                    1-Choisissez une sauvegarde (1-5)|");
                        Console.WriteLine("|_____________________________________________________|");
                    }
                    else if (L == 2)
                    {
                        // select a backup from the backups
                        Console.WriteLine("------------------------- Menu -----------------------");
                        Console.WriteLine("|                    1-Select a backup (1-5)          |");
                        Console.WriteLine("|_____________________________________________________|");
                    }
                    // execute a single backup from the class travaux_sauvegarde
                    int sauvegardeIndex = Convert.ToInt32(Console.ReadLine());
                    travaux_sauvegarde.executeSave(sauvegardeIndex, "json");
                }
                else if (user2 == 2)
                {
                    // execute a sequential backup from the class travaux_sauvegarde
                    travaux_sauvegarde.executeSaveMultiple("json");

                }

            }
            // creation of a backup
            else if (user == "1" && tCount < 5)
            {
                if (L == 1)
                {

                    Console.WriteLine("------------------------- Menu -----------------------");
                    Console.WriteLine("|       Veuillez entrer le nom de la sauvegarde :     |");
                    Console.WriteLine("|_____________________________________________________|");
                }
                else if (L == 2)
                {
                    Console.WriteLine("------------------------- Menu -----------------------");
                    Console.WriteLine("|      Please enter the name of the backup :          |");
                    Console.WriteLine("|_____________________________________________________|");

                }
                string backupName = Console.ReadLine();
                if (L == 1)
                {

                    Console.WriteLine("------------------------- Menu -----------------------");
                    Console.WriteLine("|       Veuillez selectionner un repertoire source :  |");
                    Console.WriteLine("|_____________________________________________________|");
                }
                else if (L == 2)
                {

                    Console.WriteLine("------------------------- Menu -----------------------");
                    Console.WriteLine("|      Please select a source directory :             |");
                    Console.WriteLine("|_____________________________________________________|");
                }
                string source = Console.ReadLine();
                if (L == 1)
                {
                    Console.WriteLine("------------------------- Menu -----------------------");
                    Console.WriteLine("|    Veuillez selectionner un repertoire cible :      |");
                    Console.WriteLine("|_____________________________________________________|");
                }
                else if (L == 2)
                {
                    Console.WriteLine("------------------------- Menu -----------------------");
                    Console.WriteLine("|    Please select a target directory :               |");
                    Console.WriteLine("|_____________________________________________________|");
                }

                string destination = Console.ReadLine();
                if (L == 1)
                {

                    Console.WriteLine("------------------------- Menu -------------------------------");
                    Console.WriteLine("|             Veuillez selectionner un type de sauvegarde :   |");
                    Console.WriteLine("|                    1. Complete                              |");
                    Console.WriteLine("|                    2. differentielle                        |");
                    Console.WriteLine("|_____________________________________________________________|");
                    Console.WriteLine("entrez le numero 1 ou 2:");
                }
                else if (L == 2)
                {
                    Console.WriteLine("------------------------- Menu -------------------------------");
                    Console.WriteLine("|             Veuillez selectionner un type de sauvegarde :   |");
                    Console.WriteLine("|                    1. Complete                              |");
                    Console.WriteLine("|                    2. differential                          |");
                    Console.WriteLine("|_____________________________________________________________|");
                }
                string type = Console.ReadLine();

                if (L == 1)
                {
                    Console.WriteLine("Veuillez selectionner le type du fichier log :");
                    Console.WriteLine(" 1- JSON                   2- XML");
                }
                else if (L == 2)
                {
                    Console.WriteLine("Please select the type of the log file :");
                    Console.WriteLine(" 1- JSON                   2- XML");
                }
                string logFileType = Console.ReadLine();

                // execute a sequential backup from the class travaux_sauvegarde
                travaux_sauvegarde k = new travaux_sauvegarde(backupName, type, source, destination, logFileType);
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
