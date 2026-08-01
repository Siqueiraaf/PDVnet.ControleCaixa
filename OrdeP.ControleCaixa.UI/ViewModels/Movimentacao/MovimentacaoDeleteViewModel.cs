using CommunityToolkit.Mvvm.Input;
using OrdeP.ControleCaixa.Business.Interfaces;
using OrdeP.ControleCaixa.Model;
using OrdeP.ControleCaixa.UI.ViewModels.Movimentacao;
using System.Windows;

namespace OrdeP.ControleCaixa.UI.ViewModels;

public partial class MovimentacaoDeleteViewModel : MovimentacaoFormViewModel
{
    private readonly IMovimentacaoService _service;

    public MovimentacaoDeleteViewModel(IMovimentacaoService service, MovimentacaoCaixa movimentacao)
    {
        _service = service;

        Id = movimentacao.Id;
        Descricao = movimentacao.Descricao;
        Categoria = movimentacao.Categoria;
        Valor = movimentacao.Valor;
        Tipo = movimentacao.Tipo.ToString();
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