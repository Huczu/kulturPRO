using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;

namespace KulturPRO.Utillities
{
    public class WindowAccessMethods
    {
        //Checks if any instance of window exists
        public static bool IsWindowExists<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }

        //Check if any instance of UserControlExists
        public static bool IsControlExists<T>(ContentControl contentControl) where T : UserControl
        {
            if (contentControl.Content != null)
            {
                if (contentControl.Content.GetType() == typeof(T))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        //Finding VisualChilden of DependencyObject (wpf object)
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }


        //Change this property to modify ccChangeableContent in MainWindow
        static ContentControl mainContent = new ContentControl();
        public static ContentControl MainContent
        {
            get
            {
                return mainContent;
            }
            set
            {
                mainContent = value;
                OnStaticPropertyChanged("MainContent");
            }
        }


        //Changes main view
        public static void SwitchView(UserControl control)
        {
            MainContent = control;
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;
        public static void OnStaticPropertyChanged(string propertyName)
        {
            var handler = StaticPropertyChanged;
            if (handler != null)
                StaticPropertyChanged(
                    typeof(WindowAccessMethods),
                    new PropertyChangedEventArgs(propertyName));
        }
    }

}
