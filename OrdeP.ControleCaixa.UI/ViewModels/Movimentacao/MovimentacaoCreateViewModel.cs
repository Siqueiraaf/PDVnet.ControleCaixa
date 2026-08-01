using CommunityToolkit.Mvvm.Input;
using OrdeP.ControleCaixa.Business.Exceptions;
using OrdeP.ControleCaixa.Business.Interfaces;
using OrdeP.ControleCaixa.Model;
using OrdeP.ControleCaixa.Model.Enums;
using OrdeP.ControleCaixa.UI.ViewModels.Movimentacao;
using System.Windows;

namespace OrdeP.ControleCaixa.UI.ViewModels;

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

        try 
        {
            MovimentacaoCriada = new MovimentacaoCaixa
            {
                Descricao = Descricao,
                Categoria = Categoria,
                Valor = Valor,
                Tipo = IsEntrada ? TipoMovimentacao.Entrada : TipoMovimentacao.Saida,
                Status = true,
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