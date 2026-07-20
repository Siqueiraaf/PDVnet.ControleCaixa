namespace PDVnet.ControleCaixa.Model;

public class MovimentacaoCaixa
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public int Tipo { get; set; }
    public string Categoria { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataMovimento { get; set; }
    public bool Status { get; set; }
}