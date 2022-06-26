using System;
using System.Globalization;
using Xamarin.Forms;

namespace Xam.Plugins.Pagination.Converters
{
    public class SelectedItemColorConverter : IMultiValueConverter
    {
        /// <summary>
        ///  compares two values and returns color(third param in values array) if both values are equal
        /// </summary>
        /// <param name="values">
        /// first param is number to check
        /// second param is selected page number
        /// third param is the selected color value</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return Color.Default;
            if (values[0] == null || values[1] == null)
                return Color.Default;

            if ((int)values[0] == (int)values[1])
            {
                return (Color)values[2];
            }
            return Color.Default;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
