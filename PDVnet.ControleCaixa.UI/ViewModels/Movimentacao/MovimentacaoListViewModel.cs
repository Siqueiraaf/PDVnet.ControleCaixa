using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PDVnet.ControleCaixa.Business.Interfaces;
using PDVnet.ControleCaixa.Model;
using PDVnet.ControleCaixa.Model.Enums;
using PDVnet.ControleCaixa.UI.Helpers;
using System.Collections.ObjectModel;

namespace PDVnet.ControleCaixa.UI.ViewModels;

public partial class MovimentacaoListViewModel : ObservableObject
{
    private readonly IMovimentacaoService _service;

    public ObservableCollection<MovimentacaoCaixa> Movimentacoes { get; } = [];

    [ObservableProperty]
    private MovimentacaoCaixa? movimentacaoSelecionada;

    [ObservableProperty]
    private string? categoriaSelecionada;

    [ObservableProperty]
    private string? tipoSelecionado;

    [ObservableProperty]
    private string? periodoSelecionado;

    [ObservableProperty]
    private int totalMovimentacoes;

    [ObservableProperty]
    private decimal saldoTotal;

    [ObservableProperty]
    private bool saldoBaixo;

    public List<string> Categorias => MovimentacaoOptionsHelper.CategoriasFiltro;

    public List<string> PeriodosFiltro => MovimentacaoOptionsHelper.PeriodosFiltro;

    public List<string> TiposFiltro => MovimentacaoOptionsHelper.TiposFiltro;

    public MovimentacaoListViewModel(IMovimentacaoService service)
    {
        _service = service;
    }

    private const decimal SaldoMinimo = 100m;

    [RelayCommand]
    public async Task CarregarMovimentacoesAsync()
    {
        Movimentacoes.Clear();

        var movimentacoes = await _service.ListarTodasMovimentacao();

        foreach (var movimentacao in movimentacoes)
        {
            Movimentacoes.Add(movimentacao);
        }

        AtualizarResumo();
    }

    private void AtualizarResumo()
    {
        TotalMovimentacoes = Movimentacoes.Count;

        var totalEntradas = Movimentacoes
            .Where(movimentacaoCaixa => movimentacaoCaixa.Tipo == TipoMovimentacao.Entrada)
            .Sum(movimentacaoCaixa => movimentacaoCaixa.Valor);

        var totalSaidas = Movimentacoes
            .Where(movimentacaoCaixa => movimentacaoCaixa.Tipo == TipoMovimentacao.Saida)
            .Sum(movimentacaoCaixa => movimentacaoCaixa.Valor);

        SaldoTotal = totalEntradas - totalSaidas;

        SaldoBaixo = SaldoTotal < SaldoMinimo;
    }

    [RelayCommand]
    public async Task FiltrarPorCategoriaAsync()
    {
        Movimentacoes.Clear();

        var resultado = await _service.FiltrarMovimentacoesCategoria(CategoriaSelecionada);

        foreach (var movimentacao in resultado)
        {
            Movimentacoes.Add(movimentacao);
        }
    }

    [RelayCommand]
    public async Task FiltrarPorTipoAsync()
    {
        Movimentacoes.Clear();

        if (string.IsNullOrEmpty(TipoSelecionado) || TipoSelecionado == "Todos")
        {
            await CarregarMovimentacoesAsync();
            return;
        }

        var tipo = Enum.Parse<TipoMovimentacao>(TipoSelecionado);

        var resultado = await _service.FiltrarMovimentacoesTipo(tipo);

        foreach (var movimentacao in resultado)
        {
            Movimentacoes.Add(movimentacao);
        }
    }

    [RelayCommand]
    public async Task FiltrarPorPeriodoAsync()
    {
        Movimentacoes.Clear();

        var resultado = await _service.FiltrarMovimentacoesPeriodo(PeriodoSelecionado);

        foreach (var movimentacao in resultado)
        {
            Movimentacoes.Add(movimentacao);
        }
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
        IEnumerable<MovimentacaoCaixa> resultado;

        if (!string.IsNullOrEmpty(CategoriaSelecionada) && CategoriaSelecionada != "Todos")
        {
            resultado = await _service.FiltrarMovimentacoesCategoria(CategoriaSelecionada);
        }
        else if (!string.IsNullOrEmpty(TipoSelecionado) && TipoSelecionado != "Todos")
        {
            var tipo = Enum.Parse<TipoMovimentacao>(TipoSelecionado);
            resultado = await _service.FiltrarMovimentacoesTipo(tipo);
        }
        else if (!string.IsNullOrEmpty(PeriodoSelecionado) && PeriodoSelecionado != "Todos")
        {
            resultado = await _service.FiltrarMovimentacoesPeriodo(PeriodoSelecionado);
        }
        else
        {
            resultado = await _service.ListarTodasMovimentacao();
        }

        Movimentacoes.Clear();

        foreach (var movimentacao in resultado)
        {
            Movimentacoes.Add(movimentacao);
        }
    }
}
