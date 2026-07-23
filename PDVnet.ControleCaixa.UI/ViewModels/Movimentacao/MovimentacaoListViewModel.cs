using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PDVnet.ControleCaixa.Business.Interfaces;
using PDVnet.ControleCaixa.Model;
using PDVnet.ControleCaixa.Model.Enums;
using System.Collections.ObjectModel;

namespace PDVnet.ControleCaixa.UI.ViewModels;

public partial class MovimentacaoListViewModel : ObservableObject
{
    private readonly IMovimentacaoService _service;

    public ObservableCollection<MovimentacaoCaixa> Movimentacoes { get; } = [];

    [ObservableProperty]
    private MovimentacaoCaixa? movimentacaoSelecionada;

    [ObservableProperty]
    private int totalMovimentacoes;

    [ObservableProperty]
    private decimal saldoTotal;

    [ObservableProperty]
    private bool saldoBaixo;

    private const decimal SaldoMinimo = 100m;

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

        TotalMovimentacoes = Movimentacoes.Count;

        SaldoTotal = Movimentacoes
            .Where(m => m.Tipo == TipoMovimentacao.Entrada)
            .Sum(m => m.Valor)
            -
            Movimentacoes
            .Where(m => m.Tipo == TipoMovimentacao.Saida)
            .Sum(m => m.Valor);

        SaldoBaixo = SaldoTotal < SaldoMinimo;
    }
}
