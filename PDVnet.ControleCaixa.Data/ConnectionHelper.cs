using System.Configuration;

namespace PDVnet.ControleCaixa.Data;

public static class ConnectionHelper
{
    public static string ConnectionString =>
        ConfigurationManager.ConnectionStrings["PDVnetConnection"]!.ConnectionString;
}