using Microsoft.EntityFrameworkCore;
using PDVnet.ControleCaixa.Model;

namespace PDVnet.ControleCaixa.Data.Context;

public class PDVnetControleCaixaDbContext : DbContext
{
    public PDVnetControleCaixaDbContext(DbContextOptions<PDVnetControleCaixaDbContext> options)
        : base(options) { }

    public DbSet<MovimentacaoCaixa> MovimentacoesCaixa { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PDVnetControleCaixaDbContext).Assembly);
    }
}