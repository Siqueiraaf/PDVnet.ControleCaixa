using CommunityToolkit.Mvvm.Input;
using PDVnet.ControleCaixa.Business.Interfaces;
using PDVnet.ControleCaixa.Model;
using PDVnet.ControleCaixa.UI.ViewModels.Movimentacao;
using System.Runtime.ConstrainedExecution;
using System.Windows;

namespace PDVnet.ControleCaixa.UI.ViewModels;

public partial class MovimentacaoDeleteViewModel : MovimentacaoBaseViewModel
{
    private readonly IMovimentacaoService _service;

    public MovimentacaoDeleteViewModel(
        IMovimentacaoService service,
        MovimentacaoCaixa movimentacao)
    {
        _service = service;

        Id = movimentacao.Id;
        Descricao = movimentacao.Descricao;
        Categoria = movimentacao.Categoria;
        Valor = movimentacao.Valor;
        Tipo = movimentacao.Tipo.ToString();
        Status = movimentacao.Status;
    }

    [RelayCommand]
    private async Task Excluir()
    {
        MessageBox.Show($"A exclusão da {Tipo} efetuada com sucesso ação não poderá ser desfeita.");

        await _service.ExcluirMovimentacao(Id);

        SolicitarFechamento(true);
    }

    [RelayCommand]
    private void Cancelar()
    {
        SolicitarFechamento(false);
    }
}