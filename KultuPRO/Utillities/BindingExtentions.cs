using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Data;

namespace KulturPRO.Utillities
{
    /// <summary>
    /// rozszenie bindowania dla wczytywania i zmiany ustawień
    /// </summary>
    public class SettingBindingExtension : Binding
    {
        public SettingBindingExtension()
        {
            Initialize();
        }

        public SettingBindingExtension(string path)
            : base(path)
        {
            Initialize();
        }

        /// <summary>
        /// pobiera ustawienia z Properties.Settings.Default
        /// bindowanie działa w obie strony, tzn jeżeli zmienimy coś w ustawieniach, zmieni się to w interfejsie, jeżeli zmienimy coś w interfejsie, zmieni się to w ustawieniach
        /// </summary>
        private void Initialize()
        {
            this.Source = Properties.Settings.Default;
            this.Mode = BindingMode.TwoWay;
        }
    }
   
}
