using OrdeP.ControleCaixa.UI.ViewModels;
using System.Windows;

namespace OrdeP.ControleCaixa.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _vm;

        public MainWindow(MainViewModel vm)
        {
            InitializeComponent();

            _vm = vm;
            DataContext = vm;

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _vm.InicializarAsync();
        }
    }
}