using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace WpfClient.Converters
    {
    public class DateConverter : IValueConverter
        {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
             var Date = (DateTime) value;
             return Date.ToShortDateString();
            }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
             var message = value as string;
             if ( message != null )
              {
                   DateTime buf;
                   var result = DateTime.TryParse(message, out buf);
                   if (result) return buf; else return null;
               } else return null;
            }
        }
    }
