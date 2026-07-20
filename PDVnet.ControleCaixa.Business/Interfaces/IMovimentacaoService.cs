using PDVnet.ControleCaixa.Model;

namespace PDVnet.ControleCaixa.Business.Interfaces;

public interface IMovimentacaoService
{
    Task<MovimentacaoCaixa> CadastrarMovimentacao(MovimentacaoCaixa movimentacao);
    Task<IEnumerable<MovimentacaoCaixa>> ListarTodasMovimentacao();
    Task<MovimentacaoCaixa?> EditarMovimentacao(int id, MovimentacaoCaixa movimentacao);
    Task<bool> ExcluirMovimentacao(int id);
}