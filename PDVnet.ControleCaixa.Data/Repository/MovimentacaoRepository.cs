using PDVnet.ControleCaixa.Data.Interfaces;
using PDVnet.ControleCaixa.Model;

namespace PDVnet.ControleCaixa.Data.Repository;

class MovimentacaoRepository : IMovimentacaoRepository
{
    public Task<MovimentacaoCaixa> AdicionarMovimentacao(MovimentacaoCaixa movimentacao)
    {
        throw new NotImplementedException();
    }

    public Task<MovimentacaoCaixa> AtualizarMovimentacao(MovimentacaoCaixa movimentacao)
    {
        throw new NotImplementedException();
    }

    public Task<MovimentacaoCaixa?> BuscarMovimentacaoPorId(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExcluirMovimentacao(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MovimentacaoCaixa>> ListarTodasMovimentacoes()
    {
        throw new NotImplementedException();
    }
}
