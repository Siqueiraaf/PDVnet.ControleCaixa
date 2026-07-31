using PDVnet.ControleCaixa.UI.ViewModels.Movimentacao;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace PDVnet.ControleCaixa.UI.Components;

public partial class MovimentacaoForm : UserControl
{
    public MovimentacaoForm()
    {
        InitializeComponent();
    }

    private static readonly Regex DescriptionRegex =
        new(@"^[\p{L}\p{N}\p{P}\p{Zs}]+$");

    private void DescriptionValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !DescriptionRegex.IsMatch(e.Text);
    }

    private void ValorTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        var textBox = (TextBox)sender;

        if (!char.IsDigit(e.Text[0]))
        {
            e.Handled = true;
            return;
        }

        string numeros = Regex.Replace(textBox.Text, @"\D", "");
        numeros += e.Text;

        if (string.IsNullOrEmpty(numeros))
            numeros = "0";

        decimal valor = decimal.Parse(numeros) / 100m;

        if (DataContext is MovimentacaoFormViewModel vm)
        {
            vm.Valor = valor;
        }

        textBox.Text = valor.ToString("N2", new CultureInfo("pt-BR"));
        textBox.CaretIndex = textBox.Text.Length;

        e.Handled = true;
    }

    private void ValorTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        var textBox = (TextBox)sender;

        if (e.Key != Key.Back)
            return;

        string numeros = Regex.Replace(textBox.Text, @"\D", "");

        if (numeros.Length > 0)
            numeros = numeros[..^1];

        if (string.IsNullOrEmpty(numeros))
            numeros = "0";

        decimal valor = decimal.Parse(numeros) / 100m;

        if (DataContext is MovimentacaoFormViewModel vm)
        {
            vm.Valor = valor;
        }

        textBox.Text = valor.ToString("N2", new CultureInfo("pt-BR"));
        textBox.CaretIndex = textBox.Text.Length;

        e.Handled = true;
    }
}