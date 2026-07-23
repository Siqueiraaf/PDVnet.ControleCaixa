using PDVnet.ControleCaixa.Model;
using PDVnet.ControleCaixa.Model.Enums;

namespace PDVnet.ControleCaixa.Data.Interfaces;

public interface IMovimentacaoRepository
{
    Task<MovimentacaoCaixa> AdicionarMovimentacao(MovimentacaoCaixa movimentacao);
    Task<IEnumerable<MovimentacaoCaixa>> ListarTodasMovimentacoes();
    Task<MovimentacaoCaixa?> BuscarMovimentacaoPorId(int id);
    Task<MovimentacaoCaixa> AtualizarMovimentacao(MovimentacaoCaixa movimentacao);
    Task<bool> ExcluirMovimentacao(int id);

    Task<IEnumerable<MovimentacaoCaixa>> FiltrarMovimentacoesCategoria(string categoria);
    Task<IEnumerable<MovimentacaoCaixa>> FiltrarMovimentacoesTipo(TipoMovimentacao tipo);
    Task<IEnumerable<MovimentacaoCaixa>> FiltrarMovimentacoesPeriodo(string periodo);
}