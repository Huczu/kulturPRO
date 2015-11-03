using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Database.Models;

namespace KulturPRO.Utillities.AuditLogConverters
{
    public class ClassTypeConverter : IValueConverter
    {
        private string GetDescriptionValue(string type)
        {

            var attr = Type.GetType(type + ", Database, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null").GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault() as DescriptionAttribute;

            if (attr != null)
            {
                return attr.Description;
            }

            return null;
        }

        public object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return GetDescriptionValue((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
