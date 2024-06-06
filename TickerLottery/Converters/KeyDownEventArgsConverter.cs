using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace TicketLottery.Converters
{
    public class KeyDownEventArgsConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is KeyEventArgs args)
            {
                return args.Key;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
