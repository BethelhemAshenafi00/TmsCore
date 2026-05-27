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

var student = new Student { Id = "STU-001", Name = "Abebe", Age = 20, GPA = 3.5m };
Console.WriteLine($"Student: {student.Name} (Age: {student.Age}, GPA: {student.GPA})");   
try
{
    student.Age = 15;
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}
try
{
    student.GPA = 4.5m;
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


var service = new EnrollmentService();

var validStudent = new Student { Id = "S1", Name = "Abebe", Age = 22, GPA = 3.8m };
var validCourse = new Course { Code = "CS-401", Title = "C# Patterns", Capacity = 30 };

var result  = service.ProcessRegistration(validStudent, validCourse);
Console.WriteLine($"Enrolled: {result.StudentID} in {result.CourseCode}");

try
{
    service.ProcessRegistration(null, validCourse);
}
catch (ArgumentNullException ex)
{
    Console.WriteLine($"Guard caught: {ex.ParamName}");
}

var fullCourse = new Course{ Code = "CS-401", Title = "Full Course", Capacity = 1};
fullCourse.EnrolledCount = 1;
try
{
    service.ProcessRegistration(validStudent, fullCourse);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Business rule: {ex.Message}");
}

//Ex 5 step1
List<Student> students = [
    new Student {Id = "S1", Name = "Abebe", Age = 22, GPA = 3.8m},
    new Student {Id = "S2", Name = "Alemu", Age = 19, GPA = 2.4m},
    new Student {Id = "S3", Name = "Sara", Age = 21, GPA = 3.1m},
    new Student {Id = "S4", Name = "Mulu", Age = 20, GPA = 3.9m},
    new Student {Id = "S5", Name = "Yared", Age = 23, GPA = 2.0m},
    new Student {Id = "S6", Name = "Lily", Age = 18, GPA = 3.5m},
    new Student {Id = "S7", Name = "Dawit", Age = 24, GPA = 1.8m},
    new Student {Id = "S8", Name = "Hana", Age = 22, GPA = 2.9m}
];

// step 2 
var leaderboard = students
    .Where(s => s.GPA >= 3.5m)
    .OrderByDescending(s => s.GPA)
    .Take(3)
    .Select(s => s.Name)
    .ToArray();

Console.WriteLine($"Found {leaderboard.Count()} Honors Students");
foreach (var name in leaderboard)
{
    Console.WriteLine($"- {name}");
}

// step 3 class Average 
var averageGPA = students.Average(s => s.GPA);
Console.WriteLine($"Average GPA: {averageGPA:F2}");

decimal averageGpa = students.Average(s => s.GPA);
Console.WriteLine($"\nClass Average GPA: {averageGpa:F2}");

// step 4 group by academic standing
var standingGroups = students.GroupBy(s => s.GPA >= 3.5m ? "Honor" : s.GPA >= 2.5m ? "Good Standing" : "Academic Warning");
foreach (var group in standingGroups)
{
    Console.WriteLine("\n--- Academic Standing Report ---");
    foreach (var s in group)
    {
        Console.WriteLine($"- {s.Name} (GPA: {s.GPA})");
    }
}

// step 5 Collection Expressions with spread
// TODO 7: Use the spread operator (..) to merge two arrays and append a value.
// Stuck? Pattern: string[] combined = [..array1, ..array2, "extra"]

string[] backendCourses = ["C#", "ASP.NET"];
string[] frontendCourses = ["Typescript", "Angular"];
string[] allCourses = [..backendCourses, ..frontendCourses, "SQL"];
Console.WriteLine($"\nFull Curriculum: {string.Join(", ", allCourses)}");
