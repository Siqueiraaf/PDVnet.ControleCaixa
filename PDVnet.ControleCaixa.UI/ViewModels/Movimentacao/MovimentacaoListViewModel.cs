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
    private int paginaAtual = 1;

    [ObservableProperty]
    private int totalPaginas;

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
    
    public event Action? ScrollTopoLista;

    public List<string> Categorias => MovimentacaoOptions.CategoriasFiltro;

    public List<string> PeriodosFiltro => MovimentacaoOptions.PeriodosFiltro;

    public List<string> TiposFiltro => MovimentacaoOptions.TiposFiltro;

    public MovimentacaoListViewModel(IMovimentacaoService service)
    {
        _service = service;
    }

    private const decimal SaldoMinimo = 100m;

    private void AtualizarIndicadores(IEnumerable<MovimentacaoCaixa> movimentacoes)
    {
        var movimentacoesAtivas = movimentacoes.Where(movimentacaoCaixa => movimentacaoCaixa.Status);

        TotalMovimentacoes = movimentacoesAtivas.Count();

        var totalEntradas = movimentacoesAtivas
            .Where(movimentacaoCaixa => movimentacaoCaixa.Tipo == TipoMovimentacao.Entrada)
            .Sum(movimentacaoCaixa => movimentacaoCaixa.Valor);

        var totalSaidas = movimentacoesAtivas
            .Where(movimentacaoCaixa => movimentacaoCaixa.Tipo == TipoMovimentacao.Saida)
            .Sum(movimentacaoCaixa => movimentacaoCaixa.Valor);

        SaldoTotal = totalEntradas - totalSaidas;

        SaldoBaixo = SaldoTotal < SaldoMinimo;
    }

    private void AtualizarListagem(IEnumerable<MovimentacaoCaixa> movimentacoes)
    {
        Movimentacoes.Clear();

        foreach (var movimentacao in movimentacoes)
        {
            Movimentacoes.Add(movimentacao);
        }

        ScrollTopoLista?.Invoke();
    }

    [RelayCommand]
    public async Task LimparFiltrosAsync()
    {
        CategoriaSelecionada = "Todos";
        TipoSelecionado = "Todos";
        PeriodoSelecionado = "Todos";

        PaginaAtual = 1;

        await CarregarMovimentacoesAsync();
    }

    [RelayCommand]
    public async Task FiltrarMovimentacoesAsync()
    {
        PaginaAtual = 1;
        await CarregarMovimentacoesAsync();
    }

    [RelayCommand]
    public async Task CarregarMovimentacoesAsync()
    {
        var todasMovimentacoes = await _service.ListarTodasMovimentacao();

        _todasMovimentacoes.Clear();

        foreach (var movimentacao in todasMovimentacoes)
        {
            _todasMovimentacoes.Add(movimentacao);
        }

        AtualizarIndicadores(_todasMovimentacoes);

        var resultado = await _service.ListarComPaginacao(
            PaginaAtual,
            CategoriaSelecionada,
            TipoSelecionado,
            PeriodoSelecionado);

        AtualizarListagem(resultado.Itens);

        TotalPaginas = resultado.TotalPaginas;
    }

    [RelayCommand]
    private async Task ProximaPagina()
    {
        if (PaginaAtual >= TotalPaginas)
            return;

        PaginaAtual++;

        await CarregarMovimentacoesAsync();
    }

    [RelayCommand]
    private async Task PaginaAnterior()
    {
        if (PaginaAtual <= 1)
            return;

        PaginaAtual--;

        await CarregarMovimentacoesAsync();
    }
}
