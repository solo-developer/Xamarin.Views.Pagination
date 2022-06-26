using System;
using System.Globalization;
using Xamarin.Forms;

namespace Xam.Plugins.Pagination.Converters
{
    public class BooleanToBackgroundColorConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts boolean value to color based its truth value
        /// </summary>
        /// <param name="values">
        /// first param: boolean value that indicates whether button is enabled or not
        /// second param : color to use when button is enabled
        /// third param : color to use when button is disabled
        /// </param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
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
