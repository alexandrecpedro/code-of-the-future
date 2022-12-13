using School.Models;

namespace School;

class Program
{
    public static void Main(string[] args)
    {
        // ATTRIBUTES
        var students = new List<Student>();

        while(true)
        {
            Console.Clear();

            Console.WriteLine("""
            Please, enter one of the options below:
            1 - Register student
            2 - List students
            3 - Exit
            """);
            
            int option = 0;
            int.TryParse(Console.ReadLine(), out option);

            switch (option)
            {
                case 1:
                    Console.Clear();
                    registerStudent(students);
                    break;
                case 2:
                    Console.Clear();
                    listStudents(students);
                    break;
                case 3:
                    Console.Clear();
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid option!");
                    Thread.Sleep(2000);
                    break;
            }
        }
    }

    // METHODS
    private static void registerStudent(List<Student> students)
    {
        var student = new Student();
        Console.Clear();
        Console.WriteLine("Enter the student's name");
        student.Name = Console.ReadLine()!;

        Console.WriteLine("Enter the student's enrollment");
        student.Enrollment = Console.ReadLine()!;

        Console.WriteLine("Enter the student's grades, separated by a comma");
        var sGrades = Console.ReadLine();

        var sGradesArray = sGrades.Split(",");
        var gradesList = new List<double>();
        foreach(var sGrade in sGradesArray)
        {
            double grade = 0;
            double.TryParse(sGrade, out grade);
            gradesList.Add(grade);
        }

        student.Grades = gradesList;
        students.Add(student);

        message("Student successfully registered!");
    }

    private static void message(string message)
    {
        Console.Clear();
        Console.WriteLine($"{message}");
        Thread.Sleep(2000);
    }

    private static void listStudents(List<Student> students)
    {
        Console.Clear();
        if (students.Count == 0)
        {
            message("No student registered!");
            return;
        }

        foreach (var student in students)
        {
            Console.WriteLine($"""
            ---------------------------
            Name: {student.Name}
            Enrollment: {student.Enrollment}
            Grades: {student.FormattedGrades()}
            Average: {student.Average().ToString("#.##")}
            Situation: {student.Situation()}
            ---------------------------
            """);
        }

        Thread.Sleep(5000);
    }
}