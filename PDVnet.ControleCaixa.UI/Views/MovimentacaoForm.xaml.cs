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


    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new("[^0-9,]+");

        e.Handled = regex.IsMatch(e.Text);
    }
}