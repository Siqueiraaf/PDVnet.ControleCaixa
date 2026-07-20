using Microsoft.EntityFrameworkCore;
using PDVnet.ControleCaixa.Business.Interfaces;
using PDVnet.ControleCaixa.Data;
using PDVnet.ControleCaixa.Model;

namespace PDVnet.ControleCaixa.Business.Services;

public class MovimentacaoService : IMovimentacaoService
{
    private readonly PDVnetControleCaixaDbContext _context;

    public MovimentacaoService(PDVnetControleCaixaDbContext context)
    {
        _context = context;
    }

    public async Task<MovimentacaoCaixa> CadastrarMovimentacao(MovimentacaoCaixa movimentacao)
    {
        movimentacao.Status = true;

        _context.MovimentacoesCaixa.Add(movimentacao);
        _context.SaveChanges();

        return movimentacao;
    }

    public async Task<MovimentacaoCaixa?> EditarMovimentacao(int id, MovimentacaoCaixa movimentacao)
    {
        var editarMovimentacao = await _context.MovimentacoesCaixa.FindAsync(id);

        if (editarMovimentacao == null) return null;

        editarMovimentacao.Descricao = movimentacao.Descricao;
        editarMovimentacao.Tipo = movimentacao.Tipo;
        editarMovimentacao.Categoria = movimentacao.Categoria;
        editarMovimentacao.Valor = movimentacao.Valor;
        editarMovimentacao.DataMovimento = movimentacao.DataMovimento;
        editarMovimentacao.Status = movimentacao.Status;

        await _context.SaveChangesAsync();
        return editarMovimentacao;
    }

    public async Task<IEnumerable<MovimentacaoCaixa>> ListarTodasMovimentacao()
    {
        return await _context.MovimentacoesCaixa.ToListAsync();
    }

    public async Task<bool> ExcluirMovimentacao(int id)
    {
        var excluirMovimentacao = await _context.MovimentacoesCaixa.FindAsync(id);

        if (excluirMovimentacao == null) return false;

        _context.MovimentacoesCaixa.Remove(excluirMovimentacao);
        await _context.SaveChangesAsync();

        return true;
    }
}
