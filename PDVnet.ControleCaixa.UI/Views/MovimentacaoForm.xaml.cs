using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PDVnet.ControleCaixa.UI.Components;

public partial class MovimentacaoForm : UserControl
{
    public MovimentacaoForm()
    {
        InitializeComponent();
    }

    private static readonly Regex NumberRegex = new("[^0-9,]+");

    private static readonly Regex DescriptionRegex =
        new(@"^[\p{L}\p{N}\p{P}\p{Zs}]+$");

    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        e.Handled = NumberRegex.IsMatch(e.Text);
    }

    private void DescriptionValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !DescriptionRegex.IsMatch(e.Text);
    }
}