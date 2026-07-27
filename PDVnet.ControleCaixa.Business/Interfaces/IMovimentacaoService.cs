using PDVnet.ControleCaixa.Model;
using PDVnet.ControleCaixa.Model.DTOs;

namespace PDVnet.ControleCaixa.Business.Interfaces;

public interface IMovimentacaoService
{
    Task<MovimentacaoCaixa> CadastrarMovimentacao(MovimentacaoCaixa movimentacao);
    Task<IEnumerable<MovimentacaoCaixa>> ListarTodasMovimentacao();
    Task<MovimentacaoCaixa?> EditarMovimentacao(MovimentacaoCaixa movimentacao);
    Task<bool> ExcluirMovimentacao(int id);
    Task<IEnumerable<MovimentacaoCaixa>> FiltrarMovimentacoes(string? categoria, string? tipo, string? periodo);
    Task<PaginacaoDto<MovimentacaoCaixa>> ListarComPaginacao(int pagina, string? categoria, string? tipo, string? periodo);
}