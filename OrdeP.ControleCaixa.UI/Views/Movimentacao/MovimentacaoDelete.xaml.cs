using OrdeP.ControleCaixa.UI.ViewModels;
using System.Windows;

namespace OrdeP.ControleCaixa.UI.Views;

/// <summary>
/// Lógica interna para MovimentacaoDelete.xaml
/// </summary>
public partial class MovimentacaoDelete : Window
{
    public MovimentacaoDelete(MovimentacaoDeleteViewModel vm)
    {
        InitializeComponent();

        DataContext = vm;

        vm.FecharJanela += resultado =>
        {
            DialogResult = resultado;
        };
    }
}
