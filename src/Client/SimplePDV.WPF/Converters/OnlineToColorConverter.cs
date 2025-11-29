using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SimplePDV.WPF.Converters;

public class OnlineToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var status = value as string;
        return status == "Online" 
            ? new SolidColorBrush(Color.FromRgb(40, 167, 69))   // Verde
            : new SolidColorBrush(Color.FromRgb(220, 53, 69));  // Vermelho
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
