
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


//OOP concept Examples
using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Cryptography;
// 1 Encapsulation using private fields with public property 
/* public class Person
{
    private string name; // private field
    public string Name // public property for controlled access
    {
        get {return name; }
        set
        {
            if(!string.IsNullOrWhiteSpace(value))
            name = value;
            else
            throw new ArgumentException("Name cannot be empty");
        }
    }
    public Person(string name)
    {
        Name = name;
    }
*/
var sw = Stopwatch.StartNew();
for (int i = 0; i < 5; i++)
{
    Thread.Sleep(300);
}
Console.WriteLine($"Blocking sequential:{sw.ElapsedMilliseconds}ms");

//Async but still sequential
sw.Restart();
for(int i =0; i < 5; i++)
{
    await Task.Delay(300);
}
Console.WriteLine($"Async sequential: {sw.ElapsedMilliseconds}ms");

// the right way
sw.Restart();
var tasks = Enumerable.Range(0, 5).Select(_=>Task.Delay(300));
await Task.WhenAll(tasks);
Console.WriteLine($"ASync parallel: {sw.ElapsedMilliseconds}ms");



// step 2

async Task<Student> FetchStudentAsync(string id)
{
    Console.WriteLine($"FEtching {id}...");
    await Task.Delay(300);
    return new Student
    {
        id = id,
        Name = $"Student-{id}",
        Age = 20,
        GPA = id switch
        {
            "S1" => 3.8m,
            "S2" => 2.4m,
            "S3" => 3.5m,
            "S4" => 1.9m,
            "S5" => 3.2m,
            _=> 2.5m
        }
    };
}

//Now add a second method that fetches a course
async Task<Course> FetchCourseAsync(string code)
{
    Console.WriteLine($"Fetching course {code}...");
    await Task.Delay(200);
    return new Course
    {
        Code = code,
        Title = $"Course-{code}",
        Capacity = code switch
        {
            "CRS-101" => 2,
            "CRS-201" => 30,
            "CRS-301"=> 15,
            _=> 25
        }
    };
}
sw.Restart();
string[] studentIds = ["S1", "S2", "S3", "S4", "S5"];
string[] courseCodes = ["CRS-101", "CRS-201", "CRS-301"];
var studentTasks = studentIds.Select(id => FetchStudentAsync(id));
var courseTasks = courseCodes.Select(code => FetchCourseAsync(code));

Student[] students = await Task.WhenAll(studentTasks);
Course[] courses = await Task.WhenAll(courseTasks);

Console.WriteLine($"\nLoaded {students.Length} and {courses.Length} course in {sw.ElapsedMilliseconds}ms");
foreach(var s in students)
{
    Console.WriteLine($" {s.Name} GPA: {s.GPA}");
}