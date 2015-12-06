using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Database.Models;
using TrackerEnabledDbContext.Common.Models;


namespace KulturPRO.Utillities.AuditLogConverters
{
    public class EventTypeConverter : IValueConverter
    {
        private string GetEventTypePolishString(EventType type)
        {
            switch (type)
            {
                case EventType.Added: return "Dodano";
                case EventType.Deleted: return "Usunięto";
                case EventType.Modified: return "Zmodyfikowano";
                default: return null;
            }
        }

        public object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return GetEventTypePolishString((EventType)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
