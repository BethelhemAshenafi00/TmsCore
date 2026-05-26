//M1-Lab-Session-1
using System.Diagnostics;
/* int? score = null;

 Console.WriteLine(score.Value);

 */

/* Console.WriteLine("Enter Name");
string? name = Console.ReadLine();
 */

// Getting Inputs
/* using System.Runtime.InteropServices.Marshalling;

Console.WriteLine("what is your name?");
 string? name = Console.ReadLine();

 Console.WriteLine("How old are you?");
 string? input = Console.ReadLine();
 System.Console.WriteLine($"Hello {name} Your are {input} years old.");
 */

/* Console.WriteLine("Enter salary:");
double salary = Convert.ToDouble(Console.ReadLine());

Console.WriteLine($"Salary is: " + salary); */




// session 3 Ex6
//step 1
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

Student[] students = await Task.WhenAll(studentTasks);
Course[] courses = await Task.WhenAll(courseTasks);
Console.WriteLine($"\nLoaded {students.Length} students and {courses.Length} courses in {sw.ElapsedMilliseconds}ms");
foreach (var s in students)
{
    Console.WriteLine($" {s.Name} GPA: {s.GPA}");
}


// Ex6 Part B
var enrollCourse = new Course { Code = "CRS-101", Title = "C# Mastery", Capacity = 2 };
var enrollService = new EnrollmentService();
var enrollments = new List<EnrollmentRecord>();
var failures = new List<string>();
sw.Restart();
foreach (var student in students)
{
    try
    {
        var record = enrollService.ProcessRegistration(student, enrollCourse);
        enrollCourse.EnrolledCount++;
        enrollments.Add(record);
        Console.WriteLine($" Enrolled: {student.Name}");
    }
    catch (InvalidOperationException ex)
    {
        failures.Add($"{student.Name}: {ex.Message}");
        Console.WriteLine($" Rejected: {student.Name} {ex.Message}");
    }
}

// Ex6: Safe Fire-and-Forget
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
