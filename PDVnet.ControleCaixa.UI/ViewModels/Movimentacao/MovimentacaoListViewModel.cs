using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PDVnet.ControleCaixa.Business.Interfaces;
using PDVnet.ControleCaixa.Model;
using PDVnet.ControleCaixa.Model.Enums;
using PDVnet.ControleCaixa.UI.Resources;
using System.Collections.ObjectModel;

namespace PDVnet.ControleCaixa.UI.ViewModels;

public partial class MovimentacaoListViewModel : ObservableObject
{
    private readonly IMovimentacaoService _service;

    private readonly ObservableCollection<MovimentacaoCaixa> _todasMovimentacoes = [];

    public ObservableCollection<MovimentacaoCaixa> Movimentacoes { get; } = [];

    [ObservableProperty]
    private MovimentacaoCaixa? movimentacaoSelecionada;

    [ObservableProperty]
    private string categoriaSelecionada = "Todos";

    [ObservableProperty]
    private string tipoSelecionado = "Todos";

    [ObservableProperty]
    private string periodoSelecionado = "Todos";

    [ObservableProperty]
    private int totalMovimentacoes;

    [ObservableProperty]
    private decimal saldoTotal;

    [ObservableProperty]
    private bool saldoBaixo;

    public List<string> Categorias => MovimentacaoOptions.CategoriasFiltro;

    public List<string> PeriodosFiltro => MovimentacaoOptions.PeriodosFiltro;

    public List<string> TiposFiltro => MovimentacaoOptions.TiposFiltro;

    public MovimentacaoListViewModel(IMovimentacaoService service)
    {
        _service = service;
    }

    private const decimal SaldoMinimo = 100m;

    private void AtualizarResumo(IEnumerable<MovimentacaoCaixa> movimentacoes)
    {
        TotalMovimentacoes = movimentacoes.Count();

        var totalEntradas = movimentacoes
            .Where(movimentacaoCaixa => movimentacaoCaixa.Tipo == TipoMovimentacao.Entrada)
            .Sum(movimentacaoCaixa => movimentacaoCaixa.Valor);

        var totalSaidas = movimentacoes
            .Where(movimentacaoCaixa => movimentacaoCaixa.Tipo == TipoMovimentacao.Saida)
            .Sum(movimentacaoCaixa => movimentacaoCaixa.Valor);

        SaldoTotal = totalEntradas - totalSaidas;

        SaldoBaixo = SaldoTotal < SaldoMinimo;
    }

    private void AtualizarLista(IEnumerable<MovimentacaoCaixa> movimentacoes)
    {
        Movimentacoes.Clear();

        foreach (var movimentacao in movimentacoes)
        {
            Movimentacoes.Add(movimentacao);
        }
    }

    [RelayCommand]
    public async Task CarregarMovimentacoesAsync()
    {
        var movimentacoes = await _service.ListarTodasMovimentacao();

        _todasMovimentacoes.Clear();

        foreach (var movimentacao in movimentacoes)
        {
            _todasMovimentacoes.Add(movimentacao);
        }

        AtualizarLista(_todasMovimentacoes);

        AtualizarResumo(_todasMovimentacoes);
    }

    [RelayCommand]
    public async Task LimparFiltrosAsync()
    {
        CategoriaSelecionada = "Todos";
        TipoSelecionado = "Todos";
        PeriodoSelecionado = "Todos";

        await CarregarMovimentacoesAsync();
    }

    [RelayCommand]
    public async Task FiltrarMovimentacoesAsync()
    {
        var resultado = await _service.FiltrarMovimentacoes(
            CategoriaSelecionada,
            TipoSelecionado,
            PeriodoSelecionado);

        AtualizarLista(resultado);
    }
}
