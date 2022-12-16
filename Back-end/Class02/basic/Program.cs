using System.Text.Json;
using Basico.Condicionais;
using Basico.InterfaceComUsuario;
using Basico.Models;
using Basico.Recursividade;
using Basico.Services;

namespace basico;

class Program
{
    // public const string CAMINHO_ARQUIVO = @"c:\temp\imports";
    public const string CAMINHO_ARQUIVO = "/Users/danilo/Desktop/codigo-do-futuro/dotnet/basico/imports";
    
    static void Main(string[] args)
    {
        var usuario = new Usuario();
        // usuario.ClientePorCompleto3();

        var cliente = new Cliente
        {
            Nome = "Jucileia"
        };

        // cliente.ClientePorCompleto3();

        cliente.NomeMaiusculo();
        cliente.NomeMinusculo();

        Unica.Get().Teste = "ssss";

        GenericoServico.ImprimeNome(cliente);

        var fornecedor = new Fornecedor("Didox Business");

        Console.WriteLine(fornecedor.NomeMaiusculo());

        GenericoServico.ImprimeNome(fornecedor);


        GenericoServicoComGenerics<Cliente>.ImprimeNome(cliente);
        GenericoServicoComGenerics<Fornecedor>.ImprimeNome(fornecedor);
        
        /*
        var nivaldo = new Cliente()
        {
            Nome = "Nivaldo", Telefone = "(11) 11111-1111"
        };

        Console.WriteLine(nivaldo.ClientePorCompleto("Cliente Nivaldo instância: "));

        var leticia = new Cliente()
        {
            Nome = "Leticia", Telefone = "(11) 11111-1111"
        };

        Console.WriteLine(leticia.ClientePorCompleto());
        
        string conteudoDoArquivo = """
        {"nome": "Danilo", "telefone": "123432123"}
        """;
        var cliente = JsonSerializer.Deserialize<Cliente>(conteudoDoArquivo, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        var json = JsonSerializer.Serialize(cliente);
        var x = "";

        // var lista = Recursiva.CalculaValores(new List<double>());

        // foreach(var item in lista)
        // {
        //     Console.WriteLine(item);
        // }


        // LeiaMostre.lerOuEscreverDadosNaTela();
        // UsandoCondicional.usandoSwitchParaOpcoesDeMenu();
        // utilizandoLoop();
        // var oQueRetornou = utilizandoArrayOuLista("ssss");
        
        */
    }

    private static string utilizandoArrayOuLista(string outroNome)
    {
        /*
        // string[] nomes = new string[4];
        // nomes[0] = "Danilo";
        // nomes[1] = "Alexandre";
        // nomes[2] = "Pedro";
        // nomes[3] = "Bia";
        string[] nomes = new string[]{ "Danilo", "Alexandre", "Pedro", "Bia" };
        
        foreach(var nome in nomes)
        {
            Console.WriteLine(nome);
        }
        */

        List<string> nomes = new List<string>(){ "Danilo", "Alexandre", "Pedro", "Bia" };
        nomes.Add("Leanderson");
        nomes.Add("Gustavão");

        foreach(var nome in nomes)
        {
            Console.WriteLine(nome);
        }

        return "sddds";
    }

    private static void utilizandoLoop()
    {
        int i = 0;
        do
        {
            Console.WriteLine(i);
            i++;
        }
        while(i<100);
        

        int x = 0;
        while(x<100)
        {
            Console.WriteLine(x);
            x++;
        }
        

        for(var y=0; y<100; y++)
        {
            if(y > 10) break;

            if(y > 89 && y < 100) continue;

            Console.WriteLine(y);
        }
    }
}