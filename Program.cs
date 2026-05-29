using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

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



//  -------- Session 3----------------
//Ex 6 step 1: See thread Starvation in Numbers
var sw = Stopwatch.StartNew();
for (int i = 0; i < 5; i++)
{
    Thread.Sleep(300);
}
Console.WriteLine($"Blocking sequential: {sw.ElapsedMilliseconds}ms");

sw.Restart();
for (int i = 0; i < 5; i++)
{
    await Task.Delay(300);
}
Console.WriteLine($"Async sequential: {sw.ElapsedMilliseconds}ms");


sw.Restart();
var tasks = Enumerable.Range(0, 5).Select(_ => Task.Delay(300));
await Task.WhenAll(tasks);
Console.WriteLine($"Async parallel: {sw.ElapsedMilliseconds}ms");



// Step 2 Build the TMS Student Fetcher

async Task<Student> FetchStudentAsync(string id)
{
    Console.WriteLine($" Fetching {id}...");
    await Task.Delay(300); // Simulate database latency
    return new Student
    {
        Id = id,
        Name = $"Student-{id}",
        Age = 20,
        GPA = id switch
        {
            "S1" => 3.8m,
            "S2" => 2.4m,
            "S3" => 3.5m,
            "S4" => 1.9m,
            "S5" => 3.2m,
            _ => 2.5m
        }
    };
}

async Task<Course> FetchCourseAsync(string code)
{
    Console.WriteLine($" Fetching course {code}...");
    await Task.Delay(200);
    return new Course
    {
        Code = code,
        Title = $"Course-{code}",
        Capacity = code switch
        {
            "CRS-101" => 2,
            "CRS-201" => 30,
            "CRS-301" => 15,
            _ => 25
        }
    };
}



// step 3
sw.Restart();

string[] studentIds = ["S1", "S2", "S3", "S4", "S5"];
string[] courseCodes = ["CRS-101", "CRS-201", "CRS-301"];
var studentTasks = studentIds.Select(id => FetchStudentAsync(id));
var courseTasks = courseCodes.Select(code => FetchCourseAsync(code));

Student[] fetchedStudents = await Task.WhenAll(studentTasks);
Course[] courses = await Task.WhenAll(courseTasks);
Console.WriteLine($"\nLoaded {fetchedStudents.Length} students and {courses.Length} courses in {sw.ElapsedMilliseconds}ms");
foreach (var s in fetchedStudents)
{
    Console.WriteLine($" {s.Name} GPA: {s.GPA}");
}


// Ex6 Part B
var enrollCourse = new Course { Code = "CRS-101", Title = "C# Mastery", Capacity = 2 };
var enrollService = new EnrollmentService();
var enrollments = new List<EnrollmentRecord>();
var failures = new List<string>();


sw.Restart();
foreach (var s in students)
{
    try
    {
        var record = enrollService.ProcessRegistration(s, enrollCourse);
        enrollCourse.EnrolledCount++;
        enrollments.Add(record);
        Console.WriteLine($" Enrolled: {s.Name}");
    }
    catch (InvalidOperationException ex)
    {
        failures.Add($"{student.Name}: {ex.Message}");
        Console.WriteLine($" Rejected: {student.Name} {ex.Message}");
    }
}

// Ex6B: Safe Fire-and-Forget
async Task SendConfirmationAsync(Student student)
{
    try
    {
        await Task.Delay(100);
        Console.WriteLine($" Email sent to {student.Name}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($" Email failed for {student.Name}: {ex.Message}");
    }
}

try
{
    var overflowCourse = new Course { Code = "CRS-999", Title = "Overflow Test", Capacity = 1 };
    enrollService.ProcessRegistration(
        new Student { Id = "S99", Name = "Test", Age = 20, GPA = 3.0m },
        overflowCourse
    );
}
catch (CapacityReachedException ex)
{
    Console.WriteLine($"\nDomain exception caught:");
    Console.WriteLine($" Course: {ex.CourseCode}");
    Console.WriteLine($" Message:{ex.Message}");
}
catch (ArgumentNullException ex)
{
    Console.WriteLine($"\nInvalid course setup:");
    Console.WriteLine($" Message: {ex.Message}");
}

//Ex 7B
sw.Stop();
decimal classAverage = students.Count > 0
?students.Average(s => s.GPA)
:0m;
Console.WriteLine("\n ======================ENROLLMENT SUMMARY=====================");
Console.WriteLine($"Total students loaded: {students.Count}");
Console.WriteLine($"Successful enrollments: {enrollments.Count}");
Console.WriteLine($"Failed enrollments: {failures.Count}");
Console.WriteLine($"Class Average GPA: {classAverage:F2}");
Console.WriteLine($"Total elapsed time: {sw.ElapsedMilliseconds}ms");

if (failures.Count > 0)
{
    Console.WriteLine("\n--- Failure Details ---");
    foreach(var failure in failures)
    {
        Console.WriteLine($" {failure}");
    }
}
Console.WriteLine("===============================================================");