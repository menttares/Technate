namespace Technate.Models;

public class CourseView
{
    public int IdCourse { get; set; }
    public string CourseName { get; set; }
    public string CourseSubject { get; set; }
    public int CourseCode { get; set; }
    public DateTime DateCreate { get; set; }
    public string CreatorUsername { get; set; }
    public long StudentsCount { get; set; }
}