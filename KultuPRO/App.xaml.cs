using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Database.Services;
using log4net;

namespace KulturPRO
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected override void OnStartup(StartupEventArgs e)
        {
            UserService service = new UserService();
            log4net.Config.XmlConfigurator.Configure();
            var user = service.GetUserById(1).Result;

            base.OnStartup(e);
        }
        public App() : base()
        {
            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;

        }

        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

            string errorMessage = string.Format("", e.Exception.Message);
            log.Error("", e.Exception);

        }
    }
}
