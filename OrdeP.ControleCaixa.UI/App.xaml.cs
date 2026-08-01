using Microsoft.Extensions.DependencyInjection;
using OrdeP.ControleCaixa.Business.Interfaces;
using OrdeP.ControleCaixa.Business.Services;
using OrdeP.ControleCaixa.Data.Interfaces;
using OrdeP.ControleCaixa.Data.Repository;
using OrdeP.ControleCaixa.UI.Components;
using OrdeP.ControleCaixa.UI.Interfaces;
using OrdeP.ControleCaixa.UI.Services;
using OrdeP.ControleCaixa.UI.ViewModels;
using OrdeP.ControleCaixa.UI.Views;
using OrdeP.ControleCaixa.UI.Views.Movimentacao;
using System.Globalization;
using System.Windows;

namespace OrdeP.ControleCaixa.UI;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        var cultura = new CultureInfo("pt-BR");

        CultureInfo.DefaultThreadCurrentCulture = cultura;
        CultureInfo.DefaultThreadCurrentUICulture = cultura;

        base.OnStartup(e);

        var services = new ServiceCollection();

        ConfigureServices(services);

        Services = services.BuildServiceProvider();

        var mainWindow = Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Repository
        services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();

        // Services
        services.AddScoped<IMovimentacaoService, MovimentacaoService>();
        services.AddScoped<IDialogService, DialogService>();

        // ViewModels
        services.AddTransient<MainViewModel>();
        services.AddTransient<MovimentacaoListViewModel>();
        services.AddTransient<MovimentacaoCreateViewModel>();
        services.AddTransient<MovimentacaoEditViewModel>();
        services.AddTransient<MovimentacaoDeleteViewModel>();

        // Views
        services.AddTransient<MainWindow>();
        services.AddTransient<MovimentacaoList>();
        services.AddTransient<MovimentacaoCreate>();
        services.AddTransient<MovimentacaoEdit>();
        services.AddTransient<MovimentacaoDelete>();
        services.AddTransient<MovimentacaoForm>();
    }
}