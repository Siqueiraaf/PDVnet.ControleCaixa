using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PDVnet.ControleCaixa.Data.Database;

namespace PDVnet.ControleCaixa.Data;

public class PDVnetControleCaixaDbContextFactory
    : IDesignTimeDbContextFactory<PDVnetControleCaixaDbContext>
{
    public PDVnetControleCaixaDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<PDVnetControleCaixaDbContext>();

        optionsBuilder.UseSqlServer(
            DatabaseConnection.ConnectionString);

        return new PDVnetControleCaixaDbContext(
            optionsBuilder.Options);
    }
}