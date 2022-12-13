namespace School.Models;

/// <summary>
/// A student class that we are building at training
/// </summary>
public class Student : Person
{
    // ATTRIBUTES
    /* private string name;
    public string Name {
        get
        {
            return this.name.ToUpper();
        }
        set{
            this.name = value.ToLower();
        }
    } */

    /// <summary>
    /// The enrollment serves to register a student
    /// </summary>
    public string Enrollment { get; set; }  = default!;
    public List<double> Grades { get; set; }  = default!;

    // CONSTRUCTOR
    /// <summary>
    /// Constructor without parameter
    /// </summary>
    public Student() { }

    /// <summary>
    /// Constructor with 2 parameters
    /// </summary>
    /// <param name="name">Fill in the name property</param>
    /// <param name="enrollment">Fill in the enrollment property</param>
    public Student(string name, string enrollment)
    {
        this.Name = name;
        this.Enrollment = enrollment;
        this.privateItem = "sss";
    }

    // METHODS
    public double Average()
    {
        double sumGrades = 0;
        foreach (var grade in this.Grades)
            sumGrades += grade;
        
        return sumGrades / this.Grades.Count;
    }

    public string Situation()
    {
        return this.Average() > 6 ? "Passed" : "Fail";
    }

    public string FormattedGrades()
    {
        return string.Join(", ", this.Grades);
    }

    /// <summary>
    /// A test method without parameter
    /// </summary>
    /// <returns></returns>
    public int Test()
    {
        return 1;
    }

    /// <summary>
    /// A test method with only one parameter
    /// </summary>
    /// <param name="x">to whom we will add 1</param>
    /// <returns></returns>
    public int Test(int x)
    {
        return 1 + x;
    }

    /// <summary>
    /// A test method with 2 parameters
    /// </summary>
    /// <param name="x">an int number to whom we will add 1</param>
    /// <param name="name">a test parameter</param>
    /// <returns>returns an integer</returns>
    public int Test(int x, string name)
    {
        return 1 + x;
    }

    public sealed override void Save()
    {
        Console.WriteLine("Daughter class");
        base.Save();
    }
}