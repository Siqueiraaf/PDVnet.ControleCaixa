using PDVnet.ControleCaixa.Model;

namespace PDVnet.ControleCaixa.UI.Interfaces;

public interface IDialogService
{
    MovimentacaoCaixa? ShowCriarMovimentacao();
    MovimentacaoCaixa? ShowEditarMovimentacao(MovimentacaoCaixa movimentacao);
    bool? ShowExcluirMovimentacao(MovimentacaoCaixa movimentacao);
}
