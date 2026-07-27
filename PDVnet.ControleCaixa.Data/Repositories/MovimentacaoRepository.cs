using Microsoft.EntityFrameworkCore;
using PDVnet.ControleCaixa.Data.Context;
using PDVnet.ControleCaixa.Data.Helpers;
using PDVnet.ControleCaixa.Data.Interfaces;
using PDVnet.ControleCaixa.Model;
using PDVnet.ControleCaixa.Model.DTOs;
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

        var antesAlteracao = new MovimentacaoCaixa
        {
            Id = entidade.Id,
            Descricao = entidade.Descricao,
            Categoria = entidade.Categoria,
            Valor = entidade.Valor,
            Tipo = entidade.Tipo,
            Status = entidade.Status
        };

        entidade.Descricao = movimentacao.Descricao;
        entidade.Categoria = movimentacao.Categoria;
        entidade.Valor = movimentacao.Valor;
        entidade.Tipo = movimentacao.Tipo;
        entidade.Status = movimentacao.Status;

        await _context.SaveChangesAsync();

        var depoisAlteracao = entidade;

        Log.Edicao(antesAlteracao, depoisAlteracao);

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

        Log.Exclusao(movimentacao);

        return true;
    }

    public async Task<MovimentacaoCaixa?> BuscarMovimentacaoPorId(int id)
    {
        return await _context.MovimentacoesCaixa.FirstOrDefaultAsync(
            movimentacaoCaixa => movimentacaoCaixa.Id == id);
    }

    private static IQueryable<MovimentacaoCaixa> AplicarFiltroPeriodo(IQueryable<MovimentacaoCaixa> query, string periodo)
    {
        var hoje = DateTime.Now;

        switch (periodo)
        {
            case "Hoje":
                query = query.Where(movimentacaoCaixa => movimentacaoCaixa.DataMovimento.Date == hoje.Date);
                break;

            case "Semanal":
                query = query.Where(movimentacaoCaixa => movimentacaoCaixa.DataMovimento >= hoje.AddDays(-7));
                break;

            case "Mensal":
                query = query.Where(movimentacaoCaixa =>
                    movimentacaoCaixa.DataMovimento.Month == hoje.Month &&
                    movimentacaoCaixa.DataMovimento.Year == hoje.Year);
                break;

            case "Semestral":
                query = query.Where(movimentacaoCaixa => movimentacaoCaixa.DataMovimento >= hoje.AddMonths(-6));
                break;

            case "Anual":
                query = query.Where(movimentacaoCaixa => movimentacaoCaixa.DataMovimento.Year == hoje.Year);
                break;
        }

        return query;
    }

    public async Task<IEnumerable<MovimentacaoCaixa>> FiltrarMovimentacoes(string? categoria, string? tipo, string? periodo)
    {
        return await CriarConsulta(categoria, tipo, periodo)
        .ToListAsync();
    }

    private IQueryable<MovimentacaoCaixa> CriarConsulta(string? categoria, string? tipo, string? periodo)
    {
        var query = _context.MovimentacoesCaixa.AsQueryable();

        if (!string.IsNullOrEmpty(categoria) && categoria != "Todos")
        {
            query = query.Where(movimentacaoCaixa => movimentacaoCaixa.Categoria == categoria);
        }

        if (!string.IsNullOrEmpty(tipo) && tipo != "Todos")
        {
            var tipoEnum = Enum.Parse<TipoMovimentacao>(tipo);

            query = query.Where(movimentacaoCaixa => movimentacaoCaixa.Tipo == tipoEnum);
        }

        if (!string.IsNullOrEmpty(periodo) && periodo != "Todos")
        {
            query = AplicarFiltroPeriodo(query, periodo);
        }

        return query;
    }

    public async Task<PaginacaoDto<MovimentacaoCaixa>> ListarComPaginacao(
        int pagina,
        int tamanhoPagina,
        string? categoria,
        string? tipo,
        string? periodo)
    {
        var query = CriarConsulta(categoria, tipo, periodo);

        var total = await query.CountAsync();

        var itens = await query
            .OrderByDescending(x => x.Id)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();

        return new PaginacaoDto<MovimentacaoCaixa>
        {
            Itens = itens,
            PaginaAtual = pagina,
            TotalPaginas = (int)Math.Ceiling((double)total / tamanhoPagina),
            TotalRegistros = total
        };
    }
}
