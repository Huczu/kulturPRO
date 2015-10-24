using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using kulturPRO.Utils;
using log4net;

namespace kulturPRO
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        

        protected override void OnStartup(StartupEventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            base.OnStartup(e);
            
            MapperConfig.Config();
        }
        public App() : base()
        {
            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;

        }

        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            
            string errorMessage = string.Format("An unhandled exception occurred: {0}", e.Exception.Message);
            log.Error("**exception**", e.Exception);
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
            
        }
    }
}
        

    

