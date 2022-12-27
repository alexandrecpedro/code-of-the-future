using Database.MySql;
using Negocio.Entidades;
using Negocio.Servicos;

namespace ConsoleApp.UI;

internal class ProdutosUI
{
    private static Servico<Produto> produtoServico = new Servico<Produto>(new RepositorioMYSQL<Produto>());
    
    public static void Cadastrar()
    {
        var produto = new Produto();
        Console.WriteLine("========= [Cadastro de produtos] ==========");

        produto.Id = Guid.NewGuid().ToString();

        Console.WriteLine("Nome do produto:");
        produto.Nome = Console.ReadLine();
        Console.Clear();

        Console.WriteLine("Descrição do produto:");
        produto.Descricao = Console.ReadLine();
        Console.Clear();

        produto.DataCriacao = DateTime.Now;
        Console.Clear();

        Console.WriteLine("Data de Validade:");
        produto.DataValidade = Convert.ToDateTime(Console.ReadLine());
        Console.Clear();

        Console.WriteLine("Quantidade em estoque:");
        produto.QuantidadeEstoque = Convert.ToInt32(Console.ReadLine());
        Console.Clear();

        produtoServico.Salvar(produto);

        MensagensUI.Mensagem("Produto cadastrado com sucesso");
    }

    internal static void Atualizar()
    {
        Console.WriteLine("========= [Atualização de produtos] ==========");
        Console.WriteLine("Digite o Id do produto");
        var id = Console.ReadLine().Trim();
        
        if (string.IsNullOrEmpty(id))
        {
            MensagensUI.Mensagem("Digite o Id do produto");
            ProdutosUI.Atualizar();
            return;
        }

        Listar();
        Console.WriteLine("Digite o Id do produto para atualizar");

        var produto = new Produto();
        produto.Id = Console.ReadLine();
        Console.Clear();

        Console.WriteLine("Novo Nome:");
        produto.Nome = Console.ReadLine();
        Console.Clear();

        Console.WriteLine("Nova Descrição:");
        produto.Descricao = Console.ReadLine();
        Console.Clear();

        Console.WriteLine("Data de Validade:");
        produto.DataValidade = Convert.ToDateTime(Console.ReadLine());
        Console.Clear();

        Console.WriteLine("Quantidade em estoque:");
        produto.QuantidadeEstoque = Convert.ToInt32(Console.ReadLine());
        Console.Clear();

        produtoServico.Salvar(produto);

        MensagensUI.Mensagem("Produto atualizado com sucesso !!!");
    }

    internal static void Listar()
    {
        Console.WriteLine("====== Lista de produtos ========");
        var produtos = produtoServico.BuscarTodos();
        foreach (var produtoDb in produtos)
        {
            Console.WriteLine($"""
                Id: {produtoDb.Id}
                Nome: {produtoDb.Nome}
                Descrição: {produtoDb.Descricao}
                Data de Criação: {produtoDb.DataCriacao}
                Data de Validade: {produtoDb.DataValidade}
                Quantidade em Estoque: {produtoDb.QuantidadeEstoque}
                -----------------------
            """);
        }
    }
}