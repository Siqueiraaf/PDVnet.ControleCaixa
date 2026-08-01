using CommunityToolkit.Mvvm.Input;
using OrdeP.ControleCaixa.UI.Interfaces;

namespace OrdeP.ControleCaixa.UI.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly IDialogService _dialogService;
    public MovimentacaoListViewModel Lista { get; }

    public MainViewModel(IDialogService dialogService, MovimentacaoListViewModel lista)
    {
        _dialogService = dialogService;
        Lista = lista;
    }

    [RelayCommand]
    private async Task Cadastrar()
    {
        var movimentacao = _dialogService.ShowCriarMovimentacao();

        if (movimentacao != null)
            await Lista.CarregarMovimentacoesAsync();
    }

    [RelayCommand]
    private async Task Editar()
    {
        if (Lista.MovimentacaoSelecionada is null)
            return;

        var movimentacao = _dialogService.ShowEditarMovimentacao(Lista.MovimentacaoSelecionada);

        if (movimentacao != null)
            await Lista.CarregarMovimentacoesAsync();
    }

    [RelayCommand]
    private async Task Excluir()
    {
        if (Lista.MovimentacaoSelecionada is null)
            return;

        var resultado = _dialogService.ShowExcluirMovimentacao(Lista.MovimentacaoSelecionada);

        if (resultado == true)
            await Lista.CarregarMovimentacoesAsync();
        
    }

    public async Task InicializarAsync()
    {
        await Lista.CarregarMovimentacoesAsync();
    }
}