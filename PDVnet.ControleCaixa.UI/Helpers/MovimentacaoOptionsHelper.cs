namespace PDVnet.ControleCaixa.UI.Helpers;

public static class MovimentacaoOptionsHelper
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
        "Últimos 7 dias",
        "Mensal",
        "Semestral",
        "Anual"
    ];
}