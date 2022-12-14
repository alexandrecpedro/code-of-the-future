namespace Basico.InterfaceComUsuario;

public class LeiaMostre
{
    public static void lerOuEscreverDadosNaTela()
    {
        Console.WriteLine("Digite o seu nome");
        var nome = Console.ReadLine();

        if(nome?.ToLower().Trim() == "leandro")
        {
            Console.WriteLine($"Opa e ai Leandro");
        }
        else if(nome?.ToLower().Trim() == "sheila")
        {
            Console.WriteLine($"Olha ai a Sheila");
        }
        else
        {
            Console.WriteLine($"Você digitou o nome: {nome}");
        }
    }
}