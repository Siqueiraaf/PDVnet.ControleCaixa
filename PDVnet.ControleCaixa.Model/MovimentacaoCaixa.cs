using PDVnet.ControleCaixa.Model.Enums;

namespace PDVnet.ControleCaixa.Model;

public class MovimentacaoCaixa
{
    public int Id { get; set; }
    public required string Descricao { get; set; }
    public required TipoMovimentacao Tipo { get; set; }
    public string? Categoria { get; set; }
    public required decimal Valor { get; set; }
    public DateTime DataMovimento { get; init; }
    public bool Status { get; set; }
}