using Moq;
using OrdeP.ControleCaixa.Business.Exceptions;
using OrdeP.ControleCaixa.Business.Services;
using OrdeP.ControleCaixa.Data.Interfaces;
using OrdeP.ControleCaixa.Model;
using OrdeP.ControleCaixa.Model.Enums;

namespace OrdeP.ControleCaixa.Tests.Business;

public class MovimentacaoServiceTests
{
    private readonly Mock<IMovimentacaoRepository> _repositoryMock;
    private readonly MovimentacaoService _service;

    public MovimentacaoServiceTests()
    {
        _repositoryMock = new Mock<IMovimentacaoRepository>();
        _service = new MovimentacaoService(_repositoryMock.Object);
    }

    [Fact]
    public async Task CadastrarMovimentacao_DeveLancarBusinessException_QuandoDescricaoForVazia()
    {
        // Arrange
        var movimentacao = CriarMovimentacao();
        movimentacao.Descricao = "";

        // Act
        var ex = await Assert.ThrowsAsync<BusinessException>(
            () => _service.CadastrarMovimentacao(movimentacao));

        // Assert
        Assert.Equal("A descrição é obrigatória.", ex.Message);

        _repositoryMock.Verify(
            x => x.AdicionarMovimentacao(It.IsAny<MovimentacaoCaixa>()),
            Times.Never);
    }

    [Fact]
    public async Task CadastrarMovimentacao_DeveLancarBusinessException_QuandoCategoriaForVazia()
    {
        // Arrange
        var movimentacao = CriarMovimentacao();
        movimentacao.Categoria = "";

        // Act
        var ex = await Assert.ThrowsAsync<BusinessException>(
            () => _service.CadastrarMovimentacao(movimentacao));

        // Assert
        Assert.Equal("A categoria é obrigatória.", ex.Message);
    }

    [Fact]
    public async Task CadastrarMovimentacao_DeveLancarBusinessException_QuandoValorForMenorOuIgualZero()
    {
        // Arrange
        var movimentacao = CriarMovimentacao();
        movimentacao.Valor = -5;

        // Act
        var ex = await Assert.ThrowsAsync<BusinessException>(
            () => _service.CadastrarMovimentacao(movimentacao));
        // Assert
        Assert.Equal("O valor deve ser maior que zero.", ex.Message);
    }

    [Fact]
    public async Task CadastrarMovimentacao_DeveLancarBusinessException_QuandoDescricaoForMaiorQue200Caracteres()
    {
        // Arrange
        var movimentacao = CriarMovimentacao();
        movimentacao.Descricao = "Esta é uma descrição criada exclusivamente para testes unitários " +
            "da aplicação Controle de Caixa. O objetivo é ultrapassar o limite máximo permitido " +
            "de duzentos caracteres para garantir que o validador lance corretamente a " +
            "exceção esperada durante a execução dos testes.";

        // Act
        var ex = await Assert.ThrowsAsync<BusinessException>(
            () => _service.CadastrarMovimentacao(movimentacao));

        // Assert
        Assert.Equal("A descrição deve ter no máximo 200 caracteres.", ex.Message);
    }

    
    private static MovimentacaoCaixa CriarMovimentacao()
    {
        return new MovimentacaoCaixa
        {
            Descricao = "Venda",
            Categoria = "Vendas",
            Valor = 100,
            Tipo = TipoMovimentacao.Entrada,
            Status = true
        };
    }

    [Fact]
    public async Task MeuTeste()
    {
        var movimentacao = CriarMovimentacao();
    }
}

