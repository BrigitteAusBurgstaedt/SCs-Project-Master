using System.Globalization;

namespace SCsProjectMaster.Source.Logic.Converter;

internal class DateOnlyToDateTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateOnly date)
            return date.ToDateTime(TimeOnly.MinValue);

        return DateTime.Now;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime dateTime)
            return DateOnly.FromDateTime(dateTime);

        return DateOnly.FromDateTime(DateTime.Now);
    }
}
