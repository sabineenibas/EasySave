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


#Release Note V3.0: 23/12/2023

The requested evolutions for this new version EasySave 3.0 are :

Improvements:

- Parallel backup

- Backup jobs will be done in parallel (no more sequential mode).

- Priority files management

- No backup of a non-priority file can be done as long as there are priority files pending on at least one job. Files whose extensions are declared by the user in a predefined list (present in the general parameters) are considered as priority files.

- Prohibition of simultaneous transfer of files of more than n KB

- In order not to saturate the bandwidth, it is forbidden to transfer at the same time two files whose size is greater than n Kb. (n Kb can be set)

Remark: during the transfer of a file larger than n Kb, the other jobs can transfer files whose sizes are smaller (subject to the respect of the priority files rule)

- Real-time interaction with each job or all jobs ( functinnality in progress )

- For each backup job (or set of jobs), the user must be able to

1. Pause (effective pause after the current file transfer)

2. Play (start or resume a pause)

3. Stop (immediate stop of the work and the task in progress)

- The user must be able to follow the progress of each job in real time (at least a percentage of progress).

- Temporary pause if a business software is detected

- If the software detects the operation of a business software, it must pause the transfer of files

Example: if the calculator application is launched, all the tasks must be paused.

- Remote console ( funtionnality in progress)

- In order to follow in real time the progress of the backups on a remote workstation, you must develop a GUI allowing a user to follow the progress of the backups on a remote workstation but also to act on them

- The minimum specifications of this console are :

1. Design mode: WPF and FrameWork .NetCore

2. Communication via Sockets.

- The application is Mono-instance.

- The application cannot be launched more than once on the same computer

