public class EnrollmentService
{
    public EnrollmentRecord ProcessRegistration(Student? student, Course? course)
    {
        if (student == null) throw new ArgumentNullException(nameof(student));
        if (course == null) throw new ArgumentNullException(nameof(course));

        if (course.EnrolledCount >= course.Capacity)
            throw new InvalidOperationException("Course is full");

        switch (student.GPA)
        {
            case >= 3.5m:
                Console.WriteLine("Honor");
                break;
            case >= 2.5m:
                Console.WriteLine("Good Standing");
                break;
            default:
                Console.WriteLine("Academic Warning");
                break;
        }
            
        course.EnrolledCount++;
        return new EnrollmentRecord(student.Id, course.Code ?? string.Empty, DateTime.UtcNow);
    }
}