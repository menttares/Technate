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

    public async Task AddUserAsync(string username, string email, string password)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand("SELECT add_user(@p_username, @p_email, @p_password)", connection))
            {
                

                // Добавляем параметры
                command.Parameters.AddWithValue("@p_username", username);
                command.Parameters.AddWithValue("@p_email", email);
                command.Parameters.AddWithValue("@p_password", password);

                // Выполняем запрос
                command.ExecuteNonQuery();
            }
        }
    }

    public string GetUsernameByEmail(string email)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand("SELECT get_username_by_email(@Email)", connection))
            {
                command.Parameters.AddWithValue("@Email", email);

                // Выполняем запрос и получаем результат
                string username = command.ExecuteScalar() as string;
                return username;
            }
        }
    }

    public bool UserExists(string email)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand("SELECT user_exists(@Email)", connection))
            {
                command.Parameters.AddWithValue("@Email", email);

                // Выполняем запрос и получаем результат
                bool exists = (bool)command.ExecuteScalar();
                return exists;
            }
        }
    }

    public bool VerifyPassword(string email, string password)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand("SELECT verify_password(@Email, @Password)", connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                // Выполняем запрос и получаем результат
                bool verified = (bool)command.ExecuteScalar();
                return verified;
            }
        }
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