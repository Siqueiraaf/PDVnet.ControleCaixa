using PDVnet.ControleCaixa.Model.Enums;

namespace PDVnet.ControleCaixa.Model;

public class MovimentacaoCaixa
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public TipoMovimentacao Tipo { get; set; }
    public string Categoria { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataMovimento { get; set; } = DateTime.Now;
    public bool Status { get; set; }
}