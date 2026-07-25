namespace PDVnet.ControleCaixa.UI.Resources;

public static class MovimentacaoOptions
{
    public static List<string> Categorias { get; } =
    [
        "Vendas",
        "Despesas Fixas",
        "Fornecedores",
        "Impostos",
        "Faturamento",
        "Tecnologia",
        "Outros"
    ];

    public static List<string> CategoriasFiltro { get; } =
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

    public static List<string> TiposFiltro { get; } =
    [
        "Todos",
        "Entrada",
        "Saida"
    ];

    public static List<string> PeriodosFiltro { get; } =
    [
        "Todos",
        "Hoje",
        "Semanal",
        "Mensal",
        "Semestral",
        "Anual"
    ];
}