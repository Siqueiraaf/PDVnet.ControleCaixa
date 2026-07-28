using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDVnet.ControleCaixa.Model;

namespace PDVnet.ControleCaixa.Data.Mappings;

public class MovimentacaoCaixaMap : IEntityTypeConfiguration<MovimentacaoCaixa>
{
    public void Configure(EntityTypeBuilder<MovimentacaoCaixa> entity)
    {
        entity.ToTable("MovimentacaoCaixa");

        entity.HasKey(movimentoCaixa => movimentoCaixa.Id);

        entity.Property(movimentoCaixa => movimentoCaixa.Descricao).HasMaxLength(200).IsRequired();
        entity.Property(movimentoCaixa => movimentoCaixa.Tipo).IsRequired();
        entity.Property(movimentoCaixa => movimentoCaixa.Categoria).HasMaxLength(100);
        entity.Property(movimentoCaixa => movimentoCaixa.Valor).HasColumnType("decimal(10,2)");
        entity.Property(movimentoCaixa => movimentoCaixa.Status).IsRequired();
    }
}