using Singleton.Models;

public class Program
{
    /// <summary>
    /// Singleton Design Pattern
    /// </summary>
    public static void Main(string[] args)
    {
        var b1 = DatabaseConnection.Instance;
        var b2 = DatabaseConnection.Instance;
        var b3 = DatabaseConnection.Instance;
        var b4 = DatabaseConnection.Instance;

        // Confirma se os objetos são a mesma instância
        var resultado1 = b1.ExecutaQuery("SELECT * FROM ALUNOS");
        Console.WriteLine(resultado1);

        var resultado2 = b2.ExecutaQuery("SELECT * FROM PROFESSORES");
        Console.WriteLine(resultado2);
        
        var resultado3 = b3.ExecutaQuery("SELECT * FROM CURSOS");
        Console.WriteLine(resultado3);
        
        var resultado4 = b4.ExecutaQuery("SELECT * FROM AULAS");
        Console.WriteLine(resultado4);

        if (b1 == b2 && b2 == b3 && b3 == b4)
        {
            Console.WriteLine("Objetos são da mesma instância!\n");
        }
        else
        {
            Console.WriteLine("Objetos são de instâncias diferentes!\n");
        }

        Console.ReadKey();
    }
}
