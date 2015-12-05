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
using System.Windows.Media.Imaging;

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

    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;

            return (date.Day.ToString() + "/" + date.Month.ToString() + "/" + date.Year.ToString());

        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            return DateTime.Parse(value.ToString());
        }
    }


    /// <summary>
    /// metoda sprawdzająca, czy jakaś wartość istnieje (tutaj głównie chodzi o to, czy wybrano jakiś item z listy), jeżeli tak, zwraca true
    /// </summary>
    public class HasItemSelectedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return true;
            else return false;

        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DateToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return Brushes.Black;

        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
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


    /// <summary>
    /// konwertuje ścieżkę do formatu rozumianego przez aplikację
    /// </summary>
    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(ImageSource))
            {
                if (value is string)
                {
                    string str = (string)value;
                    return new BitmapImage(new Uri(str, UriKind.RelativeOrAbsolute));
                }
            }
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
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
