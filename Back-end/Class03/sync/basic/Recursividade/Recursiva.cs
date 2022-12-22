namespace Basico.Recursividade;

public class Recursiva
{
    public static void MostraNumeros(int numero = 0)
    {
        Console.WriteLine($"O numero é: {numero}");

        if(numero > 100) return;
        MostraNumeros(numero + 1);
    }

    public static void CapturaNome()
    {
        Console.WriteLine("Por favor digite o nome:");
        var nome = Console.ReadLine()?.Trim();
        if(string.IsNullOrEmpty(nome))
        {
            Console.Clear();
            Console.WriteLine("Você digitou um nome inválido, digite novamente");
            Thread.Sleep(2000);
            Console.Clear();

            CapturaNome();
            return;
        }
        
        Console.WriteLine($"Você digitou no nome {nome}");
    }

    public static List<double> CalculaValores(List<double> lista)
    {
        lista.Add(new Random().NextDouble());

        if(lista.Count == 5) return lista;
        
        return CalculaValores(lista);
    }
}