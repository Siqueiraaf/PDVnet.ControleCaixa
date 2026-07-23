using PDVnet.ControleCaixa.Business.Exceptions;
using PDVnet.ControleCaixa.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDVnet.ControleCaixa.Business.Validators;

public static class MovimentacaoValidator
{
    public static void Validar(MovimentacaoCaixa movimentacao)
    {
        if (string.IsNullOrWhiteSpace(movimentacao.Descricao))
            throw new BusinessException("A descrição é obrigatória.");

        if (string.IsNullOrWhiteSpace(movimentacao.Categoria))
            throw new BusinessException("A categoria é obrigatória.");

        if (movimentacao.Valor <= 0)
            throw new BusinessException("O valor deve ser maior que zero.");

        if (movimentacao.Descricao.Length > 200)
            throw new BusinessException("A descrição deve ter no máximo 200 caracteres.");
    }
}
