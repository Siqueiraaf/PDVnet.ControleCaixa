using OrdeP.ControleCaixa.Business.Exceptions;
using OrdeP.ControleCaixa.Business.Validators;
using OrdeP.ControleCaixa.Model;
using OrdeP.ControleCaixa.Model.Enums;

namespace OrdeP.ControleCaixa.Tests.Business;

public class MovimentacaoValidatorTests
{
    [Fact]
    public void Validar_DeveLancarBusinessException_QuandoDescricaoForVazia()
    {
        // Arrange
        var movimentacao = new MovimentacaoCaixa
        {
            Descricao = "",
            Categoria = "Vendas",
            Valor = 100,
            Tipo = TipoMovimentacao.Entrada,
            Status = true
        };

        // Act
        var ex = Assert.Throws<BusinessException>(
            () => MovimentacaoValidator.Validar(movimentacao));

        // Assert
        Assert.Equal("A descrição é obrigatória.", ex.Message);
    }
}