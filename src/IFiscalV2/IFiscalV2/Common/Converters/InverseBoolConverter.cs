namespace IFiscalV2.Common.Converters
{
    using System;
    using System.Globalization;
    using Xamarin.Forms;


    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
