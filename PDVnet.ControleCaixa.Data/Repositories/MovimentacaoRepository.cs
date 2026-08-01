using PDVnet.ControleCaixa.Data.Helpers;
using PDVnet.ControleCaixa.Data.Interfaces;
using PDVnet.ControleCaixa.Model;
using PDVnet.ControleCaixa.Model.DTOs;
using PDVnet.ControleCaixa.Model.Enums;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace PDVnet.ControleCaixa.Data.Repository;

public class MovimentacaoRepository : IMovimentacaoRepository
{
    private readonly string _connectionString;

    public MovimentacaoRepository()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["PDVnetConnection"].ConnectionString;
    }

    public async Task<MovimentacaoCaixa> AdicionarMovimentacao(MovimentacaoCaixa movimentacao)
    {
        using SqlConnection conexao = new(_connectionString);

        const string sql = @"
            INSERT INTO MovimentacaoCaixa
            (Descricao,Categoria,Valor,Tipo,Status,DataMovimento)
            VALUES
            (@Descricao,@Categoria,@Valor,@Tipo,@Status,@DataMovimento)";

        using SqlCommand comando = new(sql, conexao);

        comando.Parameters.AddWithValue("@Descricao", movimentacao.Descricao);
        comando.Parameters.AddWithValue("@Categoria", movimentacao.Categoria);
        comando.Parameters.AddWithValue("@Valor", movimentacao.Valor);
        comando.Parameters.AddWithValue("@Tipo", (int)movimentacao.Tipo);
        comando.Parameters.AddWithValue("@Status", movimentacao.Status);
        comando.Parameters.AddWithValue("@DataMovimento", movimentacao.DataMovimento);

        await conexao.OpenAsync();
        await comando.ExecuteNonQueryAsync();

        return movimentacao;
    }

    public async Task<MovimentacaoCaixa> AtualizarMovimentacao(MovimentacaoCaixa movimentacao)
    {
        var antesAlteracao = await BuscarMovimentacaoPorId(movimentacao.Id);

        if (antesAlteracao == null)
            throw new Exception("Movimentação não encontrada");

        using SqlConnection conexao = new(_connectionString);

        const string sql = @"
        UPDATE MovimentacaoCaixa
        SET
            Descricao = @Descricao,
            Categoria = @Categoria,
            Valor = @Valor,
            Tipo = @Tipo,
            Status = @Status
        WHERE Id = @Id";

        using SqlCommand comando = new(sql, conexao);

        comando.Parameters.AddWithValue("@Id", movimentacao.Id);
        comando.Parameters.AddWithValue("@Descricao", movimentacao.Descricao);
        comando.Parameters.AddWithValue("@Categoria", movimentacao.Categoria);
        comando.Parameters.AddWithValue("@Valor", movimentacao.Valor);
        comando.Parameters.AddWithValue("@Tipo", (int)movimentacao.Tipo);
        comando.Parameters.AddWithValue("@Status", movimentacao.Status);

        await conexao.OpenAsync();
        await comando.ExecuteNonQueryAsync();

        var depoisAlteracao = await BuscarMovimentacaoPorId(movimentacao.Id);

        Log.Edicao(antesAlteracao, depoisAlteracao!);

        return depoisAlteracao!;
    }

    private static MovimentacaoCaixa MapearMovimentacao(SqlDataReader reader)
    {
        return new MovimentacaoCaixa
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Descricao = reader.GetString(reader.GetOrdinal("Descricao")),
            Categoria = reader.GetString(reader.GetOrdinal("Categoria")),
            Valor = reader.GetDecimal(reader.GetOrdinal("Valor")),
            Tipo = (TipoMovimentacao)reader.GetInt32(reader.GetOrdinal("Tipo")),
            Status = reader.GetBoolean(reader.GetOrdinal("Status")),
            DataMovimento = reader.GetDateTime(reader.GetOrdinal("DataMovimento"))
        };
    }

    public async Task<IEnumerable<MovimentacaoCaixa>> ListarTodasMovimentacoes()
    {
        List<MovimentacaoCaixa> lista = [];

        using SqlConnection conexao = new(_connectionString);

        const string sql = @"
            SELECT * FROM MovimentacaoCaixa";

        using SqlCommand comando = new(sql, conexao);

        await conexao.OpenAsync();

        using SqlDataReader reader = await comando.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(MapearMovimentacao(reader));
        }

        return lista;
    }

    public async Task<bool> ExcluirMovimentacao(int id)
    {
        var movimentacao = await BuscarMovimentacaoPorId(id);

        if (movimentacao == null)
            return false;

        using SqlConnection conexao = new(_connectionString);

        const string sql = @"
            UPDATE MovimentacaoCaixa
            SET Status = 0
            WHERE Id = @Id";

        using SqlCommand comando = new(sql, conexao);

        comando.Parameters.AddWithValue("@Id", id);

        await conexao.OpenAsync();

        int linhasAfetadas = await comando.ExecuteNonQueryAsync();

        if (linhasAfetadas == 0)
            return false;

        movimentacao.Status = false;

        Log.Exclusao(movimentacao);

        return true;
    }

    public async Task<MovimentacaoCaixa?> BuscarMovimentacaoPorId(int id)
    {
        using SqlConnection conexao = new(_connectionString);

        const string sql = @"
            SELECT * FROM MovimentacaoCaixa 
            WHERE Id=@Id";

        using SqlCommand comando = new(sql, conexao);

        comando.Parameters.AddWithValue("@Id", id);

        await conexao.OpenAsync();

        using SqlDataReader reader = await comando.ExecuteReaderAsync();

        if (!reader.Read())
            return null;

        return MapearMovimentacao(reader);
    }

    private static string AplicarFiltroPeriodo(string sql, string periodo)
    {
        switch (periodo)
        {
            case "Hoje":
                sql += " AND CAST(DataMovimento AS DATE) = CAST(GETDATE() AS DATE)";
                break;

            case "Semanal":
                sql += " AND DataMovimento >= DATEADD(DAY, -7, GETDATE())";
                break;

            case "Mensal":
                sql += " AND MONTH(DataMovimento) = MONTH(GETDATE()) AND YEAR(DataMovimento) = YEAR(GETDATE())";
                break;

            case "Semestral":
                sql += " AND DataMovimento >= DATEADD(MONTH, -6, GETDATE())";
                break;

            case "Anual":
                sql += " AND YEAR(DataMovimento) = YEAR(GETDATE())";
                break;
        }

        return sql;
    }

    public async Task<IEnumerable<MovimentacaoCaixa>> FiltrarMovimentacoes(string? categoria, string? tipo, string? periodo)
    {
        List<MovimentacaoCaixa> lista = [];

        using SqlConnection conexao = new(_connectionString);

        string sql = @"
            SELECT *
            FROM MovimentacaoCaixa
            WHERE 1 = 1";

        using SqlCommand comando = new();

        comando.Connection = conexao;

        if (!string.IsNullOrWhiteSpace(categoria) && categoria != "Todos")
        {
            sql += " AND Categoria = @Categoria";
            comando.Parameters.AddWithValue("@Categoria", categoria);
        }

        if (!string.IsNullOrWhiteSpace(tipo) && tipo != "Todos")
        {
            sql += " AND Tipo = @Tipo";
            comando.Parameters.AddWithValue("@Tipo", (int)Enum.Parse<TipoMovimentacao>(tipo));
        }

        if (!string.IsNullOrWhiteSpace(periodo) && periodo != "Todos")
        {
            switch (periodo)
            {
                case "Hoje":
                    sql += " AND CAST(DataMovimento AS DATE) = CAST(GETDATE() AS DATE)";
                    break;

                case "Semanal":
                    sql += " AND DataMovimento >= DATEADD(DAY,-7,GETDATE())";
                    break;

                case "Mensal":
                    sql += " AND MONTH(DataMovimento)=MONTH(GETDATE()) AND YEAR(DataMovimento)=YEAR(GETDATE())";
                    break;

                case "Semestral":
                    sql += " AND DataMovimento >= DATEADD(MONTH,-6,GETDATE())";
                    break;

                case "Anual":
                    sql += " AND YEAR(DataMovimento)=YEAR(GETDATE())";
                    break;
            }
        }

        comando.CommandText = sql;

        await conexao.OpenAsync();

        using SqlDataReader reader = await comando.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(MapearMovimentacao(reader));
        }

        return lista;
    }

    public async Task<PaginacaoDto<MovimentacaoCaixa>> ListarComPaginacao(int pagina, int tamanhoPagina, string? categoria, string? tipo, string? periodo)
    {
        List<MovimentacaoCaixa> itens = [];

        using SqlConnection conexao = new(_connectionString);

        string where = " WHERE 1 = 1 ";

        using SqlCommand comando = new();
        comando.Connection = conexao;

        if (!string.IsNullOrWhiteSpace(categoria) && categoria != "Todos")
        {
            where += " AND Categoria = @Categoria";
            comando.Parameters.AddWithValue("@Categoria", categoria);
        }

        if (!string.IsNullOrWhiteSpace(tipo) && tipo != "Todos")
        {
            where += " AND Tipo = @Tipo";
            comando.Parameters.AddWithValue("@Tipo", (int)Enum.Parse<TipoMovimentacao>(tipo));
        }

        if (!string.IsNullOrWhiteSpace(periodo) && periodo != "Todos")
        {
            where = AplicarFiltroPeriodo(where, periodo);
        }

        await conexao.OpenAsync();

        comando.CommandText = $"SELECT COUNT(*) FROM MovimentacaoCaixa {where}";
        int total = (int)await comando.ExecuteScalarAsync();

        int offset = (pagina - 1) * tamanhoPagina;

        comando.CommandText = $@"
            SELECT *
            FROM MovimentacaoCaixa
        {where}
            ORDER BY Id DESC
            OFFSET @Offset ROWS
            FETCH NEXT @TamanhoPagina ROWS ONLY";

        comando.Parameters.AddWithValue("@Offset", offset);
        comando.Parameters.AddWithValue("@TamanhoPagina", tamanhoPagina);

        using SqlDataReader reader = await comando.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            itens.Add(MapearMovimentacao(reader));
        }

        return new PaginacaoDto<MovimentacaoCaixa>
        {
            Itens = itens,
            PaginaAtual = pagina,
            TotalPaginas = (int)Math.Ceiling((double)total / tamanhoPagina),
            TotalRegistros = total
        };
    }
}
