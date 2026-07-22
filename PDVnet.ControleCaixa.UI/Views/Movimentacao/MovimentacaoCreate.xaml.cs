using PDVnet.ControleCaixa.UI.ViewModels;
using System.Windows;

namespace PDVnet.ControleCaixa.UI.Views
{
    /// <summary>
    /// Lógica interna para MovimentacaoCreate.xaml
    /// </summary>
    public partial class MovimentacaoCreate : Window
    {
        public MovimentacaoCreate(MovimentacaoCreateViewModel vm)
        {
            InitializeComponent();

            DataContext = vm;

            vm.FecharJanela += resultado =>
            {
                DialogResult = resultado;
            };
        }
    }
}
