using PDVnet.ControleCaixa.UI.Behaviors;
using PDVnet.ControleCaixa.UI.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            NumericTextBoxHelper.SomenteNumeros(sender, e);
        }
    }
}
