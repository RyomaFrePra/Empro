using System;
using System.Globalization;
using System.Windows.Data;

namespace Empro.Converters
{
    public class BooleanToJoinedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? "Joined" : "NotJoined";
            }
            return "NotJoined";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
