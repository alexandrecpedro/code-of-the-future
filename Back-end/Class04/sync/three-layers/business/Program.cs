using System.Text.Json;
using Business.Models;

namespace Business;

public class Program
{
    public static void Main(string[] args)
    {
        var path = Environment.GetEnvironmentVariable(@"JSON_CLASSES");
        var pessoas = JsonSerializer.Deserialize<List<Pessoa>>(File.ReadAllText(path!));

        while (true)
        {
            Console.Clear();

            Console.WriteLine("""
            =================[Seja bem-vindo]=================
            O que você deseja fazer?
            1 - Cadastrar cliente
            2 - Cadastrar fornecedor
            3 - Listar pessoas cadastradas
            4 - Sair do sistema
            """);

            var opcao = Console.ReadLine()?.Trim();
            Console.Clear();

            switch (opcao)
            {
                case "1":
                    Console.Clear();
                    cadastrarCliente();
                    break;
                case "2":
                    Console.Clear();
                    cadastrarFornecedor();
                    break;
                case "3":
                    Console.Clear();
                    listarPessoasCadastradas();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }

        void cadastrarCliente()
        {
            Console.WriteLine("Informe o nome do cliente:");
            var nomeCliente = Console.ReadLine();
            Console.Clear();

            Console.WriteLine($"Informe o CPF do {nomeCliente}: ");
            var cpfCliente = Console.ReadLine();
            Console.Clear();

            var cliente = new Cliente(nomeCliente, cpfCliente);
            cliente.CadastrarCliente();

            mensagem($""" {nomeCliente} cadastrado com sucesso. """);
        }

        void mensagem(string msg)
        {
            Console.Clear();
            Console.WriteLine(msg);
            Thread.Sleep(1500);
        }

        void cadastrarFornecedor()
        {
            Console.WriteLine("Informe o nome do fornecedor:");
            var nomeFornecedor = Console.ReadLine();
            Console.Clear();

            Console.WriteLine($"Informe o CNPJ do {nomeFornecedor}: ");
            var cnpjFornecedor = Console.ReadLine();
            Console.Clear();

            var fornecedor = new Fornecedor(nomeFornecedor, cnpjFornecedor);
            fornecedor.CadastrarFornecedor();

            mensagem($""" {nomeFornecedor} cadastrado com sucesso. """);    
        }

        void listarPessoasCadastradas()
        {
            
            if(pessoas?.Count == 0)
            {
                menuCadastraPessoaSeNaoExiste();
            }

            mostrarPessoasCadastradas(false, 0, "===============[ Selecione uma pessoa da lista ]===================");
        }

        void menuCadastraPessoaSeNaoExiste()
        {
            Console.WriteLine("""
            O que você deseja fazer?
            1 - Cadastrar cliente
            2 - Cadastrar fornecedor
            3 - Voltar ao menu
            4 - Sair do programa
            """);

            var opcao = Console.ReadLine()?.Trim();

            switch(opcao)
            {
                case "1":
                    cadastrarCliente();
                    break;
                case "2":
                    cadastrarFornecedor();
                    break;
                case "3":
                    System.Environment.Exit(0);
                    break;
                case "4":
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }

        void mostrarPessoasCadastradas(
            bool sleep = true,
            int timerSleep = 2000,
            string header = "===============[ Lista de pessoas cadastradas ]===================")
        {
            Console.Clear();
            Console.WriteLine(header);

            foreach(var pessoa in pessoas!)
            {
                Console.WriteLine($"""
                ID: {pessoa.Id}
                Nome: {pessoa.Nome}
                Tipo: {pessoa.Tipo}
                """);

                if (pessoa.Tipo == "Pessoa Física")
                {
                    Console.WriteLine($"""
                        CPF: {((Cliente)pessoa).CPF}
                        ----------------------------
                    """);
                }
                else
                {
                    Console.WriteLine($"""
                        CNPJ: {((Fornecedor)pessoa).CNPJ}
                        ----------------------------
                    """);
                }

                if (sleep)
                {
                    Thread.Sleep(timerSleep);
                    Console.Clear();
                }
            }
        }
    }
}