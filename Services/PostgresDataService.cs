using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;
using System.Collections.Generic;
using Dapper;
using Technate.Models;

namespace Technate.Services;
public class PostgresDataService
{
    private readonly string _connectionString;

    public PostgresDataService(string connectionString)
    {
        _connectionString = connectionString;
    }


    public CourseView GetCourseByCode(int courseCode)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            // Вызываем хранимую функцию get_course_by_code
            var result = connection.QueryFirstOrDefault<CourseView>("SELECT * FROM get_course_by_code(@CourseCode)", new { CourseCode = courseCode });

            return result;
        }
    }

    public List<CourseView> GetCoursesByUserEmail(string email)
    {
        try
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                // Выполняем запрос
                using (var command = new NpgsqlCommand("SELECT * FROM get_courses_by_user_email(@email)", connection))
                {
                    command.Parameters.AddWithValue("@email", email);

                    using (var reader = command.ExecuteReader())
                    {
                        List<CourseView> courses = new List<CourseView>();

                        while (reader.Read())
                        {
                            CourseView course = new CourseView
                            {
                                IdCourse = Convert.ToInt32(reader["id_course"]),
                                CourseName = reader["coursename"].ToString(),
                                CourseSubject = reader["coursesubject"].ToString(),
                                CourseCode = Convert.ToInt32(reader["coursecode"]),
                                DateCreate = Convert.ToDateTime(reader["datecreate"]),
                                CreatorUsername = reader["creator_username"].ToString(),
                                StudentsCount = Convert.ToInt64(reader["students_count"])
                            };

                            Console.WriteLine($"IdCourse: {course.IdCourse}, CourseName: {course.CourseName}, CourseSubject: {course.CourseSubject}, CourseCode: {course.CourseCode}, DateCreate: {course.DateCreate}, CreatorUsername: {course.CreatorUsername}, StudentsCount: {course.StudentsCount}");

                            courses.Add(course);
                        }

                        return courses;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while reading data: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
            Console.WriteLine($"Source: {ex.Source}");
            Console.WriteLine($"TargetSite: {ex.TargetSite}");
            return null;
        }
    }



    public List<CourseView> GetCoursesByCreatorEmail(string email)
    {
        try
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                // Выполняем запрос
                using (var command = new NpgsqlCommand("SELECT * from get_courses_by_creator_email(@email)", connection))
                {
                    command.Parameters.AddWithValue("@email", email);

                    using (var reader = command.ExecuteReader())
                    {
                        List<CourseView> courses = new List<CourseView>();

                        while (reader.Read())
                        {
                            CourseView course = new CourseView
                            {
                                IdCourse = Convert.ToInt32(reader["id_course"]),
                                CourseName = reader["coursename"].ToString(),
                                CourseSubject = reader["coursesubject"].ToString(),
                                CourseCode = Convert.ToInt32(reader["coursecode"]),
                                DateCreate = Convert.ToDateTime(reader["datecreate"]),
                                CreatorUsername = reader["creator_username"].ToString(),
                                StudentsCount = Convert.ToInt64(reader["students_count"])
                            };

                            Console.WriteLine($"IdCourse: {course.IdCourse}, CourseName: {course.CourseName}, CourseSubject: {course.CourseSubject}, CourseCode: {course.CourseCode}, DateCreate: {course.DateCreate}, CreatorUsername: {course.CreatorUsername}, StudentsCount: {course.StudentsCount}");

                            courses.Add(course);
                        }

                        return courses;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while reading data: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
            Console.WriteLine($"Source: {ex.Source}");
            Console.WriteLine($"TargetSite: {ex.TargetSite}");
            return null;
        }
    }

    public int AddUserToCourse(int userId, int courseCode)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new NpgsqlCommand("select add_user_to_course(@user_id, @course_code)", connection))
            {
                

                // Добавляем параметры
                command.Parameters.AddWithValue("@user_id", userId);
                command.Parameters.AddWithValue("@course_code", courseCode);

                // Добавляем параметр для возвращаемого значения
                var returnParameter = command.Parameters.Add("@result", NpgsqlDbType.Integer);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                // Выполняем запрос
                command.ExecuteNonQuery();

                // Получаем возвращаемое значение
                int result = Convert.ToInt32(returnParameter.Value);

                return result;
            }
        }
    }

    public int CreateCourse(string courseName, string courseDescription, int userId)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand("SELECT create_course_and_return_code(@CourseName, @CourseDescription, @UserId)", connection))
            {
                command.Parameters.AddWithValue("@CourseName", courseName);
                command.Parameters.AddWithValue("@CourseDescription", string.IsNullOrEmpty(courseDescription) ? (object)DBNull.Value : courseDescription);
                command.Parameters.AddWithValue("@UserId", userId);

                // Выполняем запрос и получаем результат
                object result = command.ExecuteScalar();

                // Преобразуем результат в int
                int courseCode = Convert.ToInt32(result.ToString());
                return courseCode;
            }
        }
    }


    public int GetUserIdByEmail(string email)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand("SELECT user_exists_on_email(@p_email)", connection))
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


    public int AddUser(string username, string email, string password)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand("SELECT create_user(@p_username, @p_email, @p_password)", connection))
            {
                // Добавляем параметры
                command.Parameters.AddWithValue("@p_username", username);
                command.Parameters.AddWithValue("@p_email", email);
                command.Parameters.AddWithValue("@p_password", password);

                // Выполняем запрос и получаем результат
                object result = command.ExecuteScalar();

                // Преобразуем результат в целое число
                int newUserId = Convert.ToInt32(result);

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

    public int UserExists(string email)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand("SELECT user_exists_on_email(@Email)", connection))
            {
                command.Parameters.AddWithValue("@Email", email);

                // Выполняем запрос и получаем результат
                int exists = int.Parse(command.ExecuteScalar().ToString());
                return exists;
            }
        }
    }

    public int VerifyPassword(string email, string password)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand("SELECT verify_password(@Email, @Password)", connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                // Выполняем запрос и получаем результат
                int verified = int.Parse(command.ExecuteScalar().ToString());
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