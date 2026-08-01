using OrdeP.ControleCaixa.Model;
using OrdeP.ControleCaixa.Model.DTOs;

namespace OrdeP.ControleCaixa.Data.Interfaces;

public interface IMovimentacaoRepository
{
    Task<MovimentacaoCaixa> AdicionarMovimentacao(MovimentacaoCaixa movimentacao);
    Task<IEnumerable<MovimentacaoCaixa>> ListarTodasMovimentacoes();
    Task<MovimentacaoCaixa?> BuscarMovimentacaoPorId(int id);
    Task<MovimentacaoCaixa> AtualizarMovimentacao(MovimentacaoCaixa movimentacao);
    Task<bool> ExcluirMovimentacao(int id);
    Task<IEnumerable<MovimentacaoCaixa>> FiltrarMovimentacoes(string? categoria, string? tipo, string? periodo);
    Task<PaginacaoDto<MovimentacaoCaixa>> ListarComPaginacao(int pagina, int tamanhoPagina, string? categoria, string? tipo, string? periodo);
}