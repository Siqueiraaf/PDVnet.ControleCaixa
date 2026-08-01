using OrdeP.ControleCaixa.UI.ViewModels.Movimentacao;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace OrdeP.ControleCaixa.UI.Components;

public partial class MovimentacaoForm : UserControl
{
    private static readonly Regex DescriptionRegex =
        new(@"^[\p{L}\p{N}\p{P}\p{Zs}]+$");

    public MovimentacaoForm()
    {
        InitializeComponent();
    }

    private void DescriptionValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !DescriptionRegex.IsMatch(e.Text);
    }

    private void ValorTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        if (!char.IsDigit(e.Text[0]))
        {
            e.Handled = true;
            return;
        }

        AtualizarValor((TextBox)sender, e.Text[0], removerUltimo: false);
        e.Handled = true;
    }

    private void ValorTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Back)
            return;

        AtualizarValor((TextBox)sender, null, removerUltimo: true);
        e.Handled = true;
    }

    private void AtualizarValor(TextBox textBox, char? novoDigito, bool removerUltimo)
    {
        string numeros = Regex.Replace(textBox.Text, @"\D", "");

        if (removerUltimo)
        {
            if (numeros.Length > 0)
                numeros = numeros[..^1];
        }
        else if (novoDigito.HasValue)
        {
            numeros += novoDigito.Value;
        }

        if (string.IsNullOrEmpty(numeros))
            numeros = "0";

        decimal valor = decimal.Parse(numeros, NumberStyles.None, CultureInfo.InvariantCulture) / 100m;

        if (DataContext is MovimentacaoFormViewModel vm)
            vm.Valor = valor;

        textBox.Text = valor.ToString("N2");
        textBox.CaretIndex = textBox.Text.Length;
    }
}