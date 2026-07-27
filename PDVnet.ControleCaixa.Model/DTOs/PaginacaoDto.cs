namespace PDVnet.ControleCaixa.Model.DTOs;

public class PaginacaoDto<T>
{
    public IEnumerable<T> Itens { get; set; } = [];
    public int PaginaAtual { get; set; }
    public int TotalPaginas { get; set; }
    public int TotalRegistros { get; set; }
}
