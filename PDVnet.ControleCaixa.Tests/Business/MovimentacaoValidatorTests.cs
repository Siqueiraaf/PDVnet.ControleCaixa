using PDVnet.ControleCaixa.Business.Exceptions;
using PDVnet.ControleCaixa.Business.Validators;
using PDVnet.ControleCaixa.Model;
using PDVnet.ControleCaixa.Model.Enums;

namespace PDVnet.ControleCaixa.Tests.Business;

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