using PDVnet.ControleCaixa.Business.Interfaces;
using PDVnet.ControleCaixa.Data.Interfaces;
using PDVnet.ControleCaixa.Model;

namespace PDVnet.ControleCaixa.Business.Services;

public class MovimentacaoService : IMovimentacaoService
{
    private readonly IMovimentacaoRepository _repository;

    public MovimentacaoService(IMovimentacaoRepository repository)
    {
        _repository = repository;
    }

    public async Task<MovimentacaoCaixa> CadastrarMovimentacao(MovimentacaoCaixa movimentacao)
    {
        return await _repository.AdicionarMovimentacao(movimentacao);
    }

    public async Task<IEnumerable<MovimentacaoCaixa>> ListarTodasMovimentacao()
    {
        return await _repository.ListarTodasMovimentacoes();
    }

    public async Task<bool> ExcluirMovimentacao(int id)
    {
        return await _repository.ExcluirMovimentacao(id);
    }

    public async Task<MovimentacaoCaixa?> EditarMovimentacao(MovimentacaoCaixa movimentacao)
    {
        return await _repository.AtualizarMovimentacao(movimentacao);
    }
}