using OrdeP.ControleCaixa.UI.ViewModels;
using System.Windows;

namespace OrdeP.ControleCaixa.UI.Views.Movimentacao;

public partial class MovimentacaoCreate : Window
{
    public MovimentacaoCreate(MovimentacaoCreateViewModel vm)
    {
        InitializeComponent();

        DataContext = vm;

        vm.FecharJanela += resultado =>
        {
            DialogResult = resultado;
            Close();
        };
    }
}