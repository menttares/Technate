using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;

namespace Technate.Services;
public class PostgresDataService
{
    private readonly string _connectionString;

    public PostgresDataService(string connectionString)
    {
        _connectionString = connectionString;
    }


    public DataTable ExecuteQuery(string sql)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
            {
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
    }
}