using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Database.Services;

namespace KulturPRO
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //UserService service = new UserService();
            //var user = service.GetUserById(1).Result;

            base.OnStartup(e);
        }
    }
}
