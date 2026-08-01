using CommunityToolkit.Mvvm.Input;
using OrdeP.ControleCaixa.Business.Exceptions;
using OrdeP.ControleCaixa.Business.Interfaces;
using OrdeP.ControleCaixa.Model;
using OrdeP.ControleCaixa.Model.Enums;
using OrdeP.ControleCaixa.UI.ViewModels.Movimentacao;
using System.Windows;

namespace OrdeP.ControleCaixa.UI.ViewModels;

public partial class MovimentacaoEditViewModel : MovimentacaoFormViewModel
{
    private readonly IMovimentacaoService _service;

    public MovimentacaoCaixa? MovimentacaoEditada { get; private set; }

    public MovimentacaoEditViewModel(IMovimentacaoService service)
    {
        _service = service;
    }

    public MovimentacaoEditViewModel(IMovimentacaoService service, MovimentacaoCaixa movimentacao)
    {
        _service = service;

        Id = movimentacao.Id;
        Descricao = movimentacao.Descricao;
        Categoria = movimentacao.Categoria ?? string.Empty;
        Valor = movimentacao.Valor;

        IsEntrada = movimentacao.Tipo == TipoMovimentacao.Entrada;
        IsSaida = movimentacao.Tipo == TipoMovimentacao.Saida;
    }

    [RelayCommand]
    private async Task SalvarAlteracoes()
    {
        try
        {
            MovimentacaoEditada = new MovimentacaoCaixa
            {
                Id = Id,
                Descricao = Descricao,
                Categoria = Categoria,
                Valor = Valor,
                Tipo = IsEntrada ? TipoMovimentacao.Entrada : TipoMovimentacao.Saida,
            };

            await _service.EditarMovimentacao(MovimentacaoEditada);

            MessageBox.Show($"{Tipo} editado com sucesso.");

            SolicitarFechamento();
        }
        catch (BusinessException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    [RelayCommand]
    private void CancelarMovimentacao()
    {
        SolicitarFechamento(false);
    }
}