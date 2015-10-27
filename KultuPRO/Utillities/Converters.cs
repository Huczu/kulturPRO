using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;

namespace KulturPRO.Utillities
{
    public class FontConverter : IMultiValueConverter
    {
        private static double mainWindowFontMultiplier = 1;
        public static double MainWindowFontMultiplier
        {
            get
            {
                return mainWindowFontMultiplier;
            }
            set
            {
                mainWindowFontMultiplier = value;
                OnStaticPropertyChanged("MainWindowFontMultiplier");
            }
        }

        private static double loginWindowFontMultiplier = 1;
        public static double LoginWindowFontMultiplier
        {
            get
            {
                return loginWindowFontMultiplier;
            }
            set
            {
                loginWindowFontMultiplier = value;
                OnStaticPropertyChanged("LoginWindowFontMultiplier");
            }
        }

        // method to notify when value of static property has changed
        public static event PropertyChangedEventHandler StaticPropertyChanged;
        public static void OnStaticPropertyChanged(string propertyName)
        {
            var handler = StaticPropertyChanged;
            if (handler != null)
                StaticPropertyChanged(
                    typeof(FontConverter),
                    new PropertyChangedEventArgs(propertyName));
        }
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //value[0] - FontSize, value[1] - FontMultiplier
            if (values[0] != null && values[1] != null)
                return (double.Parse(values[0].ToString()) * (double)values[1]);
            else throw new Exception("FontConverter error");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LoginAndPasswordConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //value[0] - FontSize, value[1] - FontMultiplier
            if (values[0] != null && values[1] != null)
                return values.Clone();

            else throw new Exception("LoginAndPasswordConverter error");
            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StringToBrushConverter
    {
        public static Brush Convert(string brushName)
        {
            return (Brush)TypeDescriptor.GetConverter(typeof(Brush)).ConvertFromString(brushName);
        }
    }
}
