# EasySave
Repository of the System Programming block of group 6 

This file will be updated during the development of our application. Our team has just integrated the software editor ProSoft. Under the responsibility of the CIO, our goal is to manage and design the "EasySave" project. This project consists in developing a backup software. As any software of the ProSoft Suite, our software "EasySave" will be integrated to the following pricing policy:

- Unit price: 200 € HT


Tools :

In this project we used The following tools :

- Visual Studio 2019 16.3 or higher
- GIT
- UML editor: Visual-Paradigm Languages and FrameWork :
- C# language
- Net.Core 3.X library

# Release Note V1.0: 3/12/2023

in this first version our software is realized in console line without graphical interface
Features:

- Create a backup job defined by a name, a source path, a destination path and a backup type (full or differential)
- Create up to 5 backup jobs
- Display the created backups
- Execute one of the backup jobs or run all the jobs sequentially
- Generate log files and status files
- Use of our program by English and French speaking users

# User guide : 
1. When launching our software a display of the name of our software will appear.

2. The user has the choice of language between French and English.

3. The user has the choice to create or execute a backup job.
![1](https://github.com/sabineenibas/EasySave/assets/114827482/c34aeacb-6213-4cab-bd37-192ed3c242c6)
![2](https://github.com/sabineenibas/EasySave/assets/114827482/cf9c4427-a088-4efe-a587-f41b5b4cd79e)

- Create a backup job: Entering the number 1 means that you are going to add a backup job, you will enter the name of your backup job, the source path from where you want to retrieve the folder to be backed up and also specify the path of the target folder. After entering this information, you will have to choose whether you want to have a full or differential backup.  ! A reminder !: 

- Full backup: Copies the entire file and folder each time a full backup is performed.

- Differential backup: Only the files modified since the last full backup are backed up. The backup job is now created.
![3](https://github.com/sabineenibas/EasySave/assets/114827482/43eaef72-9b09-4744-a869-b22b2a720c60)

After answering to all this question, you can quit the application by clicking esc

# Release Note V1.1: 7/12/2023
EasySave 1.0 has been distributed to many customers, now we have updated the V1.1
ProSoft customers who do not want to install V2 wish to have Log files in XML format. so version 1.1 allows users to choose the format of the log file (XML or JSON).
# Release Note V2.0: 09/12/2021

1. Graphical Interface:
Console mode is discontinued. The application is now be developed in WPF under . Net Core

2. Unlimited number of jobs:
The number of backup jobs is now unlimited.

3. Cryping via CryptoSoft software:
The software is able to encrypt the files using the CryptoSoft software . Only files whose extensions have been defined by the user in the general settings will be encrypted.

4. Evolution of the Log Daily file

The daily log file must contain additional information : 

- Time required to encrypt the file (in ms)

0: no encryption

0: encryption time (in ms)
<0: error code

5. Business Software

The presence of business software is detected, the software must prohibit the launch of a backup job. In the case of sequential work, the software will complete the work in progress and stop before starting the next work. The user will be able to define the business software in the general parameters of the software. (Note: the calculator application is the default business software)

