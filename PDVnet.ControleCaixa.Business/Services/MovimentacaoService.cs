using PDVnet.ControleCaixa.Business.Interfaces;
using PDVnet.ControleCaixa.Business.Validators;
using PDVnet.ControleCaixa.Data.Interfaces;
using PDVnet.ControleCaixa.Model;
using PDVnet.ControleCaixa.Model.Enums;

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
        MovimentacaoValidator.Validar(movimentacao);
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
        MovimentacaoValidator.Validar(movimentacao);
        return await _repository.AtualizarMovimentacao(movimentacao);
    }

    public async Task<IEnumerable<MovimentacaoCaixa>> FiltrarMovimentacoesCategoria(string categoria)
    {
        return await _repository.FiltrarMovimentacoesCategoria(categoria);
    }

    public async Task<IEnumerable<MovimentacaoCaixa>> FiltrarMovimentacoesTipo(TipoMovimentacao tipo)
    {
        return await _repository.FiltrarMovimentacoesTipo(tipo);
    }

    public async Task<IEnumerable<MovimentacaoCaixa>> FiltrarMovimentacoesPeriodo(string periodo)
    {
        return await _repository.FiltrarMovimentacoesPeriodo(periodo);
    }
}