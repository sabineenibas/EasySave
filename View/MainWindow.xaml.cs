using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EasySaveG6.ViewModel;
using Ookii.Dialogs.Wpf;



namespace EasySAVEG6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Menu : Window
    {
        travaux_sauvegarde saveInstance = new travaux_sauvegarde();
        private int saveIndex { get; set; }
        private int[] checkedItemIndices { get; set; }


        public Menu()
        {
            InitializeComponent();
            LoadSaveList();
        }

        public void LoadSaveList()
        {
            List<travaux_sauvegarde> saveListItems = new List<travaux_sauvegarde>();

            Parallel.ForEach(saveInstance.displaybackupByLeriem(), saveItem =>
            {
                lock (saveListItems)
                {
                    saveListItems.Add(saveItem);
                }
            });

            saveList.ItemsSource = saveListItems;
        }


        private string langue;



        private void MaxBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                if (WindowState == WindowState.Maximized)
                {
                    WindowState = WindowState.Normal;
                }
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_fr(object sender, RoutedEventArgs e)//Function to translate the software into French
        {

            langue = "fr";
            Nom.Content = "Nom";
            Source.Content = "Source";
            Lancer.Content = "Lancer";
            AJOUTER.Content = "AJOUTER";
            Destination.Content = "Destination";
            Francais.Name = "Francais";
            Anglais.Name = "Anglais";
            Sauvegardes.Content = "Sauvegardes";
            Commentaire.Content = "Commentaire";

        }

        private void Button_Click_en(object sender, RoutedEventArgs e)//Function to translate the software into English
        {
            langue = "en";
            Nom.Content = "Name";
            Source.Content = "Source";
            Lancer.Content = "start";
            AJOUTER.Content = "Add";
            Destination.Content = "Destination";
            Francais.Name = "French";
            Anglais.Name = "English";
            Sauvegardes.Content = "backup";
            Commentaire.Content = "Comments";

        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }




        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_4(object sender, TextChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MainContentFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }


        private void addSave_Click(object sender, RoutedEventArgs e)
        {
            string NameBackup = nameBackup.Text;
            string Sourcepath = sourcePath.Text;
            string DetsinationPath = destinationPath.Text;
            int selectedIndexFileType = fileType.SelectedIndex + 1;
            int selectedIndexBackupType = fileType.SelectedIndex + 1;
            string type;
            string logTypeFile;
            if (selectedIndexFileType.ToString() == "1")
            {
                type = "2";
            }
            else
            {
                type = "1";
            }
            if (selectedIndexBackupType.ToString() == "1")
            {
                logTypeFile = "1";
            }
            else
            {
                logTypeFile = "2";
            }

            travaux_sauvegarde k = new travaux_sauvegarde(NameBackup, type, Sourcepath, DetsinationPath, logTypeFile);
            k.save(k.travaux_sauvegardeToJSON(), @"..\..\..\Save\travaux_sauvegarde.json");
            LoadSaveList();




        }

        private void Lancer_Click(object sender, RoutedEventArgs e)
        {

            travaux_sauvegarde display = new travaux_sauvegarde();
            List<travaux_sauvegarde> save = display.displayOneBackup(saveIndex);
            nameBackup.Text = save[0].backupName;
            sourcePath.Text = save[0].sourcePath;
            destinationPath.Text = save[0].destinationPath;

            BackUpType.Text = save[0].type;
            fileType.Text = save[0].logFileType;
            List<int> indices = new List<int>();

            for (int i = 0; i < saveList.Items.Count; i++)
            {
                if (saveList.Items[i] is travaux_sauvegarde backupItem && backupItem.IsSelected)
                {
                    indices.Add(i);
                }
            }
            checkedItemIndices = indices.ToArray();
            travaux_sauvegarde copySave = new travaux_sauvegarde(save[0].backupName, save[0].type, save[0].sourcePath, save[0].destinationPath, save[0].logFileType);
            Parallel.For(0, checkedItemIndices.Length, i =>
            {
                copySave.executeSave(checkedItemIndices[i]);
            });

        }

        private void Parcourir_click(object sender, RoutedEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog();
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dialog.SelectedPath = desktopPath;

            if (dialog.ShowDialog() == true)
            {
                string selectedFolderPath = dialog.SelectedPath;
                Button button = sender as Button;
                if (button != null)
                {
                    string buttonTag = button.Tag.ToString();
                    TextBox associatedTextBox = this.FindName(buttonTag) as TextBox;
                    if (associatedTextBox != null)
                    {
                        associatedTextBox.Text = selectedFolderPath;
                    };
                }
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {

        }


        private void backupType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void saveList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.saveIndex = saveList.SelectedIndex;
            travaux_sauvegarde display = new travaux_sauvegarde();
            List<travaux_sauvegarde> save = display.displayOneBackup(this.saveIndex);
            nameBackup.Text = save[0].backupName;
            sourcePath.Text = save[0].sourcePath;
            destinationPath.Text = save[0].destinationPath;

            BackUpType.SelectedIndex = int.Parse(save[0].type) - 1;
            fileType.SelectedIndex = int.Parse(save[0].logFileType) - 1;
        }
    }


}