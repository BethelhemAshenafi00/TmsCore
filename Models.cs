public record EnrollmentRecord(string StudentID, string CourseCode, DateTime EnrolledAt);


public class Student
{
    public required string Id;
    public required string Name;
    public int Age
    {
        get;
        set => field = value is >= 16 and <= 100
        ? value
        : throw new ArgumentOutOfRangeException(nameof(value), "Age must be between 16 and 100.");
    }
    public decimal GPA
    {
        get;
        set => field = value is >= 0.0m and <= 4.0m
        ? value
        : throw new ArgumentOutOfRangeException(nameof(value), "GPA must be between 0.0and 4.0.");
    }
}

public class Course
{
    private int _capacity;
    public string? Code { get; set; }
    public string? Title { get; set; }
    public int EnrolledCount { get; set; }
    public int Capacity
    {
        get => _capacity;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, nameof(value));
            _capacity = value;
        }
    }
}

public interface IGradable
{
    string Title {get;}
    decimal CalculateGrade();
}

public class Quiz: IGradable
{
    public required string Title {get; init;}
    public required int CorrectAnswers {get; init;}
    public required int TotalQuestions {get; init;}

    public decimal CalculateGrade()
    {
        if (TotalQuestions == 0) return 0m;
        return (decimal)CorrectAnswers / TotalQuestions * 100m;
    }
}
public class LabAssignment: IGradable
{
    public required string Title {get; init;}
    public required decimal FunctionalityScore {get; init;}
    public required decimal CodeQualityScore {get; init;}
    
    public decimal CalculateGrade()
    {
        return (FunctionalityScore * 0.7m) + (CodeQualityScore * 0.3m);
    }
}


/* public class EnrollmentRecord {
     public Student Student {get; set;} 
     public Course Course {get; set;}
      }
public class EnrollmentService
{
    public EnrollmentRecord ProcessRegistration(Student student, Course course)
    {
        if (course.EnrolledCount >= course.Capacity)
            throw new InvalidOperationException("Course is full");
        course.EnrolledCount++;
        return new EnrollmentRecord { Student = student, Course = course };
    }
}


 */
