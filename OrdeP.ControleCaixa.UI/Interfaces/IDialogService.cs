using OrdeP.ControleCaixa.Model;

namespace OrdeP.ControleCaixa.UI.Interfaces;

public interface IDialogService
{
    MovimentacaoCaixa? ShowCriarMovimentacao();
    MovimentacaoCaixa? ShowEditarMovimentacao(MovimentacaoCaixa movimentacao);
    bool? ShowExcluirMovimentacao(MovimentacaoCaixa movimentacao);
}
