public class Student
{
   public required string Id;
    public required string Name;
    public int Age;
    public decimal GPA;

}

public class Course
{
    public required string Code;
    public required string Title;
    public int Capacity;
     public int EnrolledCount; 
}

public class EnrollmentRecord {
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

