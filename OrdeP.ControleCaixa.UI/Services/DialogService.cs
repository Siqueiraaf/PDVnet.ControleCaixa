using Microsoft.Extensions.DependencyInjection;
using OrdeP.ControleCaixa.Business.Interfaces;
using OrdeP.ControleCaixa.Model;
using OrdeP.ControleCaixa.UI.Interfaces;
using OrdeP.ControleCaixa.UI.ViewModels;
using OrdeP.ControleCaixa.UI.Views;
using OrdeP.ControleCaixa.UI.Views.Movimentacao;
using System.Windows;

namespace OrdeP.ControleCaixa.UI.Services;

public class DialogService : IDialogService
{
    private readonly IMovimentacaoService _service;

    public DialogService(IMovimentacaoService service)
    {
        _service = service;
    }

    public MovimentacaoCaixa? ShowCriarMovimentacao()
    {
        var view = App.Services.GetRequiredService<MovimentacaoCreate>();

        view.Owner = Application.Current.MainWindow;

        if (view.ShowDialog() == true)
        {
            var vm = (MovimentacaoCreateViewModel)view.DataContext;
            return vm.MovimentacaoCriada;
        }

        return null;
    }

    public MovimentacaoCaixa? ShowEditarMovimentacao(MovimentacaoCaixa movimentacao)
    {
        var vm = new MovimentacaoEditViewModel(_service, movimentacao);

        var view = new MovimentacaoEdit(vm)
        {
            Owner = Application.Current.MainWindow
        };

        if (view.ShowDialog() == true)
        {
            return vm.MovimentacaoEditada;
        }

        return null;
    }

    public bool? ShowExcluirMovimentacao(MovimentacaoCaixa movimentacao)
    {
        var vm = new MovimentacaoDeleteViewModel(_service, movimentacao);

        var view = new MovimentacaoDelete(vm)
        {
            Owner = Application.Current.MainWindow
        };

        return view.ShowDialog();
    }
}