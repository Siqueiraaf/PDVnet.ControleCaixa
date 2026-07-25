using PDVnet.ControleCaixa.UI.ViewModels;
using System.Windows;

namespace PDVnet.ControleCaixa.UI.Views.Movimentacao;

public partial class MovimentacaoEdit : Window
{
    public MovimentacaoEdit(MovimentacaoEditViewModel vm)
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