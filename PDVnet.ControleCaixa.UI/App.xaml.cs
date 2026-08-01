using Microsoft.Extensions.DependencyInjection;
using PDVnet.ControleCaixa.Business.Interfaces;
using PDVnet.ControleCaixa.Business.Services;
using PDVnet.ControleCaixa.Data.Interfaces;
using PDVnet.ControleCaixa.Data.Repository;
using PDVnet.ControleCaixa.UI.Components;
using PDVnet.ControleCaixa.UI.Interfaces;
using PDVnet.ControleCaixa.UI.Services;
using PDVnet.ControleCaixa.UI.ViewModels;
using PDVnet.ControleCaixa.UI.Views;
using PDVnet.ControleCaixa.UI.Views.Movimentacao;
using System.Globalization;
using System.Windows;

namespace PDVnet.ControleCaixa.UI;

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