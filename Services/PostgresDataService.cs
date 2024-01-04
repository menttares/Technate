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


    public async Task<string> CreateCourseAsync(string courseName, string courseSubject, string userEmail)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand("SELECT create_course(@CourseName, @CourseSubject, @UserEmail)", connection))
            {
                command.Parameters.AddWithValue("@CourseName", courseName);
                command.Parameters.AddWithValue("@CourseSubject", string.IsNullOrEmpty(courseSubject) ? (object)DBNull.Value : courseSubject);
                command.Parameters.AddWithValue("@UserEmail", userEmail);

                object result = command.ExecuteScalar();

                // Выполняем запрос и получаем результат
                string courseId = result.ToString();
                return courseId;
            }
        }
    }

    public int GetUserIdByEmail(string email)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand("SELECT get_user_id_by_email(@p_email)", connection))
            {
                // Добавляем параметры
                command.Parameters.AddWithValue("@p_email", email);

                // Выполняем запрос и получаем результат
                var result = command.ExecuteScalar();

                // Проверяем на null и возвращаем результат
                return result != null ? Convert.ToInt32(result) : -1; // Здесь -1 может быть значением по умолчанию или кодом ошибки в случае отсутствия пользователя
            }
        }
    }


    public async Task<int> AddUserAsync(string username, string email, string password)
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

                // Выполняем запрос и получаем результат
                object result = command.ExecuteScalar();

                // Преобразуем результат в целое число
                int newUserId =  Convert.ToInt32(result);

                return newUserId;
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