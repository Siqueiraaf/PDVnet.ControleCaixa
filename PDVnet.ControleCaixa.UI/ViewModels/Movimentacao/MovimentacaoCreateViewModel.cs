using CommunityToolkit.Mvvm.Input;
using PDVnet.ControleCaixa.Business.Exceptions;
using PDVnet.ControleCaixa.Business.Interfaces;
using PDVnet.ControleCaixa.Model;
using PDVnet.ControleCaixa.Model.Enums;
using PDVnet.ControleCaixa.UI.ViewModels.Movimentacao;
using System.Windows;

namespace PDVnet.ControleCaixa.UI.ViewModels;

public partial class MovimentacaoCreateViewModel : MovimentacaoFormViewModel
{
    private readonly IMovimentacaoService _service;

    public MovimentacaoCaixa? MovimentacaoCriada { get; private set; }

    public MovimentacaoCreateViewModel(IMovimentacaoService service)
    {
        _service = service;
    }

    [RelayCommand]
    private async Task CadastrarMovimentacao()
    {
        var Status = true;

        try 
        {
            MovimentacaoCriada = new MovimentacaoCaixa
            {
                Descricao = Descricao,
                Categoria = Categoria,
                Valor = Valor,
                Tipo = IsEntrada ? TipoMovimentacao.Entrada : TipoMovimentacao.Saida,
                Status = Status,
                DataMovimento = DateTime.Now
            };

            await _service.CadastrarMovimentacao(MovimentacaoCriada);

            MessageBox.Show($"{Tipo} cadastrado com sucesso.");

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