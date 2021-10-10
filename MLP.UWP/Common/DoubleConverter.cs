using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Source: user @jayden on StackOverflow
// https://stackoverflow.com/questions/42613826/uwp-xbind-and-data-validation-for-numeric-field


namespace MLP.UWP.Common
{
    public class DoubleConverter : Windows.UI.Xaml.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                return value.ToString();
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            double n;
            bool isDouble = double.TryParse(value.ToString(), out n);
            if (isDouble)
            {
                return n;
            }
            else
            {
                return 0.0;
            }
        }
    }
}
