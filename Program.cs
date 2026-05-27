//M1-Lab-Session 
//Ex-1  step 2
string? region = null;
string? upperRegion = region?.ToUpper();
Console.WriteLine($"Region (conditional): {upperRegion}");
 
string displayRegion = region ??"Unassigned";
Console.WriteLine($"Region (coalesced): {displayRegion}");

region ??= "Addis Ababa";
Console.WriteLine($"Region (assigned): {region}");

//step 3
string studentName = "Abebe";
string studentId = "STU-001";
int enrollmentCount = 3;
decimal grantAmount = 1999.99m;
DateTime enrolledAt = DateTime.UtcNow;
string? campusRegion = null;

Console.WriteLine($"Student: {studentName} ({studentId})");
Console.WriteLine($"Course: {enrollmentCount}");
Console.WriteLine($"Grant: {grantAmount:F2}");
Console.WriteLine($"Enrolled: {enrolledAt:yyy-MM-dd}");
Console.WriteLine($"Campus: {campusRegion ?? "Not assigned"}");


//Ex 2 step1
/* double grantPerStudent = 1999.99;
double totalAllocation = grantPerStudent * 100_000;
 */

// step2
decimal grantPerStudent = 1999.99m;
decimal totalAllocation = grantPerStudent * 100_000m;
Console.WriteLine($"Total allocated (decimal): {totalAllocation}");
Console.WriteLine($"Total allocated (formatted): {totalAllocation}");

//Ex 3 part 1
var enrollment = new EnrollmentRecord("STU-001", "CS-401", DateTime.UtcNow);
Console.WriteLine(enrollment);

var corrected = enrollment with { CourseCode = "CS-402" };
Console.WriteLine(corrected);

var duplicate = new EnrollmentRecord("STU-001", "CS-401", enrollment.EnrolledAt);
Console.WriteLine($"Same data? {enrollment == duplicate}");



// Ex 3 part 2
var course = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 3 };
Console.WriteLine($"Course: {course.Title} (Capacity: {course.Capacity})");


try
{
    course.Capacity = -5;
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

try
{
    course.Title = " ";
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

var s = new Student { Id = "STU-001", Name = "Abebe", Age = 20, GPA = 3.5m };
Console.WriteLine($"Student: {s.Name} (Age: {s.Age}, GPA: {s.GPA})");   
try
{
    s.Age = 15;
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}
try
{
    s.GPA = 4.5m;
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

//step 3
void PrintGradeReport(IEnumerable<IGradable> assessments)
{
    Console.WriteLine("--- Grade Report ---");
    foreach (var item in assessments)
    {
        Console.WriteLine($"{item.Title}: {item.CalculateGrade():F2}%");
    }
}

IGradable[] cohortAssessments = [
    new Quiz{Title = "C# Basics", CorrectAnswers = 18, TotalQuestions = 20},
    new LabAssignment{Title = "Registration API", FunctionalityScore = 90m, CodeQualityScore = 85m}
];
PrintGradeReport(cohortAssessments);