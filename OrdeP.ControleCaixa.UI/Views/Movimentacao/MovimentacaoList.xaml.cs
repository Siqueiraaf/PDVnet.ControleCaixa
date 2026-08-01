using OrdeP.ControleCaixa.UI.ViewModels;
using System.Windows.Controls;

namespace OrdeP.ControleCaixa.UI.Views.Movimentacao
{
    /// <summary>
    /// Interação lógica para MovimentacaoList.xam
    /// </summary>
    public partial class MovimentacaoList : UserControl
    {
        public MovimentacaoList()
        {
            InitializeComponent();

            Loaded += (_, _) =>
            {
                if (DataContext is MovimentacaoListViewModel vm)
                {
                    vm.ScrollTopoLista += ResetarScroll;
                }
            };
        }

        private void ResetarScroll()
        {
            if (ListaMovimentacoes.Items.Count > 0)
            {
                ListaMovimentacoes.ScrollIntoView(
                    ListaMovimentacoes.Items[0]
                );
            }
        }
    }
}
