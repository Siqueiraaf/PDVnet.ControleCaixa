using CommunityToolkit.Mvvm.Input;
using PDVnet.ControleCaixa.Business.Exceptions;
using PDVnet.ControleCaixa.Business.Interfaces;
using PDVnet.ControleCaixa.Model;
using PDVnet.ControleCaixa.Model.Enums;
using PDVnet.ControleCaixa.UI.ViewModels.Movimentacao;
using System.Windows;

namespace PDVnet.ControleCaixa.UI.ViewModels;

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
        Categoria = movimentacao.Categoria;
        Valor = movimentacao.Valor;
        Status = movimentacao.Status;

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
                Status = Status
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