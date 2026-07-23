using Microsoft.EntityFrameworkCore;
using PDVnet.ControleCaixa.Data.Interfaces;
using PDVnet.ControleCaixa.Model;
using PDVnet.ControleCaixa.Model.Enums;

namespace PDVnet.ControleCaixa.Data.Repository;

public class MovimentacaoRepository : IMovimentacaoRepository
{
    private readonly PDVnetControleCaixaDbContext _context;

    public MovimentacaoRepository(PDVnetControleCaixaDbContext context)
    {
        _context = context;
    }

    public async Task<MovimentacaoCaixa> AdicionarMovimentacao(MovimentacaoCaixa movimentacao)
    {
        _context.MovimentacoesCaixa.Add(movimentacao);
        await _context.SaveChangesAsync();
        return movimentacao;
    }

    public async Task<MovimentacaoCaixa> AtualizarMovimentacao(MovimentacaoCaixa movimentacao)
    {
        var entidade = await _context.MovimentacoesCaixa
            .FirstOrDefaultAsync(movimentacaoCaixa => movimentacaoCaixa.Id == movimentacao.Id);

        if (entidade == null)
            throw new Exception("Movimentação não encontrada");

        entidade.Descricao = movimentacao.Descricao;
        entidade.Categoria = movimentacao.Categoria;
        entidade.Valor = movimentacao.Valor;
        entidade.Tipo = movimentacao.Tipo;
        entidade.Status = movimentacao.Status;

        await _context.SaveChangesAsync();

        return entidade;
    }

    public async Task<IEnumerable<MovimentacaoCaixa>> ListarTodasMovimentacoes()
    {
        return await _context.MovimentacoesCaixa.ToListAsync();
    }

    public async Task<bool> ExcluirMovimentacao(int id)
    {
        var movimentacao = await BuscarMovimentacaoPorId(id);

        if (movimentacao == null)
        {
            return false;
        }

        _context.MovimentacoesCaixa.Remove(movimentacao);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<MovimentacaoCaixa?> BuscarMovimentacaoPorId(int id)
    {
        return await _context.MovimentacoesCaixa.FirstOrDefaultAsync(
            movimentacaoCaixa => movimentacaoCaixa.Id == id);
    }

    public async Task<IEnumerable<MovimentacaoCaixa>> FiltrarMovimentacoesCategoria(string? categoria)
    {
        return await _context.MovimentacoesCaixa
            .Where(movimentacaoCaixa => movimentacaoCaixa.Categoria == categoria)
            .ToListAsync();
    }

    public async Task<IEnumerable<MovimentacaoCaixa>> FiltrarMovimentacoesTipo(TipoMovimentacao tipo)
    {
        return await _context.MovimentacoesCaixa
            .Where(movimentacaoCaixa => movimentacaoCaixa.Tipo == tipo)
            .ToListAsync();
    }

    public async Task<IEnumerable<MovimentacaoCaixa>> FiltrarMovimentacoesPeriodo(string periodo)
    {
        var hoje = DateTime.Now;

        IQueryable<MovimentacaoCaixa> query = _context.MovimentacoesCaixa;

        switch (periodo.ToLower())
        {
            case "diario":
                query = query.Where(movimentoCaixa => movimentoCaixa.DataMovimento.Date == hoje.Date);
                break;

            case "semanal":
                query = query.Where(movimentoCaixa => movimentoCaixa.DataMovimento >= hoje.AddDays(-7));
                break;

            case "mensal":

                query = query.Where(movimentoCaixa =>
                    movimentoCaixa.DataMovimento.Month == hoje.Month &&
                    movimentoCaixa.DataMovimento.Year == hoje.Year);

                break;

            case "semestral":
                query = query.Where(movimentoCaixa => movimentoCaixa.DataMovimento >= hoje.AddMonths(-6));
                break;

            case "anual":
                query = query.Where(movimentoCaixa => movimentoCaixa.DataMovimento.Year == hoje.Year);
                break;
        }

        return await query.ToListAsync();
    }
}
