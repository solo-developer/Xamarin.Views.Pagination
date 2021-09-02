using System;
using System.Globalization;
using Xamarin.Forms;

namespace Xam.Plugins.Pagination.Converters
{
    public class BooleanToBackgroundColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return Color.Default;
            if (values[0] == null || values[1] == null || values[2] == null)
                return Color.Default;

            if ((bool)values[0])
            {
                return (Color)values[1];
            }
            return (Color)values[2];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
