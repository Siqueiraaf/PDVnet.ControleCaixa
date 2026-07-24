using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Configuration;

namespace PDVnet.ControleCaixa.Data.Contexts;

public class PDVnetControleCaixaDbContextFactory
    : IDesignTimeDbContextFactory<PDVnetControleCaixaDbContext>
{
    public PDVnetControleCaixaDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PDVnetControleCaixaDbContext>();

        var connectionString = ConfigurationManager
            .ConnectionStrings["PDVnetConnection"]!.ConnectionString;

        optionsBuilder.UseSqlServer(connectionString);

        return new PDVnetControleCaixaDbContext(optionsBuilder.Options);
    }
}