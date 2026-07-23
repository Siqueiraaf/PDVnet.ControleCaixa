using System.Globalization;
using System.Windows.Data;

namespace PDVnet.ControleCaixa.UI.Converters;

public class StatusConverter : IValueConverter
{
    public object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
    {
        if (value is bool status)
            return status ? "Ativo" : "Inativo";

        return "Indefinido";
    }

    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
