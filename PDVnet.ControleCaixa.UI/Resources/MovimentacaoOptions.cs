namespace PDVnet.ControleCaixa.UI.Resources;

public static class MovimentacaoOptions
{
    public static List<string> Categorias { get; } =
    [
        "Vendas",
        "Serviços",
        "Fornecedores",
        "Despesas Fixas",
        "Compras",
        "Tecnologia",
        "Impostos",
        "Salários",
        "Manutenção",
        "Investimentos",
        "Outros"
    ];

    public static List<string> CategoriasFiltro { get; } =
    [
        "Todos",
        "Vendas",
        "Serviços",
        "Fornecedores",
        "Despesas Fixas",
        "Compras",
        "Tecnologia",
        "Impostos",
        "Salários",
        "Manutenção",
        "Investimentos",
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