namespace PDVnet.ControleCaixa.UI.Helpers;

public static class MovimentacaoOptions
{
    public static List<string> Categorias { get; } =
    [
        "Todos",
        "Vendas",
        "Despesas Fixas",
        "Fornecedores",
        "Impostos",
        "Faturamento",
        "Tecnologia",
        "Outros"
    ];

    public static List<string> Tipos { get; } =
    [
        "Entrada",
        "Saida"
    ];

    public static List<string> PeriodosFiltro { get; } =
    [
        "Todos",
        "Hoje",
        "Últimos 7 dias",
        "Mensal",
        "Semestral",
        "Anual"
    ];

    public static List<string> TiposFiltro { get; } =
    [
        "Todos",
        "Entrada",
        "Saida"
    ];
}