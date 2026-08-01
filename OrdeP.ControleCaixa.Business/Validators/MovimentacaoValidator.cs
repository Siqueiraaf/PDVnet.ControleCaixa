using OrdeP.ControleCaixa.Business.Exceptions;
using OrdeP.ControleCaixa.Model;

namespace OrdeP.ControleCaixa.Business.Validators;

public static class MovimentacaoValidator
{
    public static void Validar(MovimentacaoCaixa movimentacao)
    {
        if (string.IsNullOrWhiteSpace(movimentacao.Descricao))
            throw new BusinessException("A descrição é obrigatória.");

        if (movimentacao.Valor <= 0)
            throw new BusinessException("O valor deve ser maior que zero.");

        if (movimentacao.Descricao.Length > 200)
            throw new BusinessException("A descrição deve ter no máximo 200 caracteres.");

        if (movimentacao.Valor > 99999999.99m)
            throw new BusinessException("O valor informado ultrapassa o limite permitido.");
    }
}
