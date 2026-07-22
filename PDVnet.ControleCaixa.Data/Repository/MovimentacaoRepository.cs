using Microsoft.EntityFrameworkCore;
using PDVnet.ControleCaixa.Data.Interfaces;
using PDVnet.ControleCaixa.Model;

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
        entidade.DataMovimento = movimentacao.DataMovimento;


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
}
