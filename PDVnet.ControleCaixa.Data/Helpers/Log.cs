using PDVnet.ControleCaixa.Model;

namespace PDVnet.ControleCaixa.Data.Helpers;

public static class Log
{
    private static readonly string Caminho = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");

    public static void Registrar(string mensagem)
    {
        var linha = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] {mensagem}";
        File.AppendAllText(Caminho, linha + Environment.NewLine);
    }

    public static void Exclusao(MovimentacaoCaixa movimentacao)
    {
        Registrar(
            $"EXCLUSÃO: Id={movimentacao.Id} | " +
            $"Descrição={movimentacao.Descricao} | " +
            $"Categoria={movimentacao.Categoria} | " +
            $"Tipo={movimentacao.Tipo} | " +
            $"Valor={movimentacao.Valor:C} | " +
            $"Status={movimentacao.Status}");
    }

    public static void Edicao(MovimentacaoCaixa antesAlteracao, MovimentacaoCaixa depoisAlteracao)
    {
        var alteracoes = new List<string>();

        if (antesAlteracao.Descricao != depoisAlteracao.Descricao)
            alteracoes.Add($"Descrição: '{antesAlteracao.Descricao}' -> '{depoisAlteracao.Descricao}'");

        if (antesAlteracao.Categoria != depoisAlteracao.Categoria)
            alteracoes.Add($"Categoria: '{antesAlteracao.Categoria}' -> '{depoisAlteracao.Categoria}'");

        if (antesAlteracao.Valor != depoisAlteracao.Valor)
            alteracoes.Add($"Valor: {antesAlteracao.Valor:C} -> {depoisAlteracao.Valor:C}");

        if (antesAlteracao.Tipo != depoisAlteracao.Tipo)
            alteracoes.Add($"Tipo: {antesAlteracao.Tipo} -> {depoisAlteracao.Tipo}");

        if (antesAlteracao.Status != depoisAlteracao.Status)
            alteracoes.Add($"Status: {antesAlteracao.Status} -> {depoisAlteracao.Status}");

        if (alteracoes.Count == 0)
            return;

        Registrar($"EDIÇÃO: Id={depoisAlteracao.Id} | {string.Join(" | ", alteracoes)}");
    }
}
