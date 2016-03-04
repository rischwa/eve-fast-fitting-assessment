using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace EVE_Fast_Fitting_Assessment.Themes
{
    public class ToolTipOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var num = (double) value;
            //return -num;

            var x = value as ToolTip;
            var str = x?.Content as string;

            return -42 - str?.Count(c => c == '\n') * 16;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var num = (double)value;
            return -num;
        }
    }
}
