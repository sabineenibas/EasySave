using EasySAVEG6.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace EasySAVEG6
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex _mutex = null;
        private server server = new server();
        protected override void OnStartup(StartupEventArgs e)
        {


            const string AppName = "EasySave";
            bool OpenApp;

            _mutex = new Mutex(true, AppName, out OpenApp);

            if (!OpenApp)
            {
                //app is already running! Exiting the application  
                MessageBox.Show($"{AppName} is already running !");
                Application.Current.Shutdown();
            }
            base.OnStartup(e);

            server.StartServer();
        }
    }
}