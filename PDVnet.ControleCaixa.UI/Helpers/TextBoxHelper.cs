using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace PDVnet.ControleCaixa.UI.Helpers;

public static class TextBoxHelper
{
    private static readonly Regex Regex = new(@"^\d*([,.]?\d{0,2})?$");

    public static void SomenteNumeros(object sender, TextCompositionEventArgs e)
    {
        TextBox textBox = (TextBox)sender;

        string novoTexto = textBox.Text.Insert(textBox.SelectionStart, e.Text);

        e.Handled = !Regex.IsMatch(novoTexto);
    }
}