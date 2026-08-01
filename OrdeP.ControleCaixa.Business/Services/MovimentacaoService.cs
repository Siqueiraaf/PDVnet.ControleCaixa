using OrdeP.ControleCaixa.Business.Interfaces;
using OrdeP.ControleCaixa.Business.Validators;
using OrdeP.ControleCaixa.Data.Interfaces;
using OrdeP.ControleCaixa.Model;
using OrdeP.ControleCaixa.Model.DTOs;

namespace OrdeP.ControleCaixa.Business.Services;

public class MovimentacaoService : IMovimentacaoService
{
    private readonly IMovimentacaoRepository _repository;
    private const int TamanhoPaginacao = 50;

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

    public async Task<IEnumerable<MovimentacaoCaixa>> FiltrarMovimentacoes(string? categoria, string? tipo, string? periodo)
    {
        return await _repository.FiltrarMovimentacoes(categoria, tipo, periodo);
    }

    public async Task<PaginacaoDto<MovimentacaoCaixa>> ListarComPaginacao(int pagina, string? categoria, string? tipo, string? periodo)
    {
        return await _repository.ListarComPaginacao(pagina, TamanhoPaginacao, categoria, tipo, periodo);
    }
}