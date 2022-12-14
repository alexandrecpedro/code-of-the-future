namespace Basico.Condicionais;

public class UsandoCondicional
{
    public static void usandoSwitchParaOpcoesDeMenu()
    {
        Console.WriteLine($"Digite as opções\n 1\n 2");
        var opcao = Convert.ToInt16(Console.ReadLine());
        switch (opcao)
        {
            case 1:
                Console.WriteLine("Você digitou a opção 1");
                break;
            case 2:
                Console.WriteLine("Você digitou a opção 2");
                break;
            default:
                Console.WriteLine("Nenhuma das opcões");
                break;
        }
    }
}