using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PDVnet.ControleCaixa.Business.Interfaces;
using PDVnet.ControleCaixa.Model;
using System.Collections.ObjectModel;

namespace PDVnet.ControleCaixa.UI.ViewModels;

public partial class MovimentacaoListViewModel : ObservableObject
{
    private readonly IMovimentacaoService _service;

    public ObservableCollection<MovimentacaoCaixa> Movimentacoes { get; } = [];

    [ObservableProperty]
    private MovimentacaoCaixa? movimentacaoSelecionada;

    public MovimentacaoListViewModel(IMovimentacaoService service)
    {
        _service = service;
    }

    [RelayCommand]
    public async Task CarregarMovimentacoesAsync()
    {
        Movimentacoes.Clear();

        var listarMovimentacoes = await _service.ListarTodasMovimentacao();

        foreach (var movimentacao in listarMovimentacoes)
        {
            Movimentacoes.Add(movimentacao);
        }
    }
}
