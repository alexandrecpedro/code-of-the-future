using Business.Models;

namespace ConsoleApp.UI;

internal class ClientesUI
{
    public static void Cadastrar()
    {
        var cliente = new Cliente();
        Console.WriteLine("========= [Cadastro de clientes] ==========");

        Console.WriteLine("Nome:");
        cliente.Nome = Console.ReadLine();

        Console.WriteLine("Email:");
        cliente.Email = Console.ReadLine();

        cliente.Salvar();

        MensagensUI.Mensagem("Cliente cadastrado com sucesso");
    }

    internal static void Atualizar()
    {
        Console.WriteLine("========= [Atualização de clientes] ==========");
        Console.WriteLine("Digite o Id ou o email do cliente");
        var idOuEmail = Console.ReadLine();
        
        if (string.IsNullOrEmpty(idOuEmail))
        {
            MensagensUI.Mensagem("Digite o Id ou o Email");
            ClientesUI.Atualizar();
            return;
        }

        var clientes = Cliente.BuscaPorIdOuEmail(idOuEmail);
        if (clientes.Count == 0)
        {
            MensagensUI.Mensagem("Cliente não localizado");
            ClientesUI.Atualizar();
            return;
        }

        Console.WriteLine("Digite o Id do cliente abaixo para atualizar");
        foreach (var clienteDb in clientes)
        {
            Console.WriteLine($"""
                Id: {clienteDb.Id}
                Nome: {clienteDb.Nome}
                Email: {clienteDb.Email}
                -----------------------
            """);
        }

        var cliente = new Cliente();
        cliente.Id = Convert.ToInt32(Console.ReadLine());
        Console.Clear();

        Console.WriteLine("Novo Nome:");
        cliente.Nome = Console.ReadLine();
        Console.Clear();

        Console.WriteLine("Novo Email:");
        cliente.Email = Console.ReadLine();
        Console.Clear();

        MensagensUI.Mensagem("Cliente atualizado com sucesso !!!");
    }

    internal static void Listar()
    {
        Console.WriteLine("====== Lista de clientes ========");
        var clientes = Cliente.Todos();
        foreach (var clienteDb in clientes)
        {
            Console.WriteLine($"""
                Id: {clienteDb.Id}
                Nome: {clienteDb.Nome}
                Email: {clienteDb.Email}
                -----------------------
            """);
        }
    }
}