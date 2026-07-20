using PDVnet.ControleCaixa.Model;

namespace PDVnet.ControleCaixa.Data.Interfaces;

public interface IMovimentacaoRepository
{
    Task<MovimentacaoCaixa> AdicionarMovimentacao(MovimentacaoCaixa movimentacao);
    Task<IEnumerable<MovimentacaoCaixa>> ListarTodasMovimentacoes();
    Task<MovimentacaoCaixa?> BuscarMovimentacaoPorId(int id);
    Task<MovimentacaoCaixa> AtualizarMovimentacao(MovimentacaoCaixa movimentacao);
    Task<bool> ExcluirMovimentacao(int id);
}