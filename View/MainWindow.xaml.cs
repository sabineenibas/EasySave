using System;
using System.Collections.Generic;
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

using EasySAVEG6.ViewModel;

namespace EasySAVEG6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Menu : Window
    {


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
    }


}