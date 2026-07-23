using PDVnet.ControleCaixa.Model;
using PDVnet.ControleCaixa.Model.Enums;

namespace PDVnet.ControleCaixa.Business.Interfaces;

public interface IMovimentacaoService
{
    Task<MovimentacaoCaixa> CadastrarMovimentacao(MovimentacaoCaixa movimentacao);
    Task<IEnumerable<MovimentacaoCaixa>> ListarTodasMovimentacao();
    Task<MovimentacaoCaixa?> EditarMovimentacao(MovimentacaoCaixa movimentacao);
    Task<bool> ExcluirMovimentacao(int id);
    Task<IEnumerable<MovimentacaoCaixa>> FiltrarMovimentacoesCategoria(string? categoria);
    Task<IEnumerable<MovimentacaoCaixa>> FiltrarMovimentacoesTipo(TipoMovimentacao tipo);
    Task<IEnumerable<MovimentacaoCaixa>> FiltrarMovimentacoesPeriodo(string? periodo);
}