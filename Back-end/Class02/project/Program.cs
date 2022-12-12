using System;
using System.Threading;
using Programa.Models;
using Programa.Services;

namespace Programa;

// Default = internal class Program
public class Program
{
    public static void Main(string[] args)
    {
        var produtos = new List<Produto>();

        while (true)
        {
            Console.Clear();

            Console.WriteLine("""
            =================[Seja bem-vindo ao armazém do Sr Roberto]=================
            O que você deseja fazer?
            1 - Cadastrar produto
            2 - Listar produtos cadastrados
            3 - Quantidade total de itens no estoque
            4 - Sair do sistema
            """);

            var opcao = Console.ReadLine()?.Trim();
            Console.Clear();

            switch (opcao)
            {
                case "1":
                    Console.Clear();
                    cadastrarProduto();
                    break;
                case "2":
                    Console.Clear();
                    listarProdutosCadastrados();
                    break;
                case "3":
                    Console.Clear();
                    listarQuantidadeItensEstoque();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }

        void cadastrarProduto()
        {
            var id = Guid.NewGuid().ToString();

            Console.WriteLine("Informe o nome do produto:");
            var nomeProduto = Console.ReadLine();
            Console.Clear();

            Console.WriteLine($"Informe a quantidade do produto {nomeProduto}: ");
            var quantidadeEmEstoque = Convert.ToInt16(Console.ReadLine());
            Console.Clear();

            if(produtos.Count > 0)
            {
                Produto? produto = produtos.Find(prod => prod.ID == id);
                if(produto != null)
                {
                    mensagem($"Produto já cadastrado com este id {id}. \nCadastre novamente");
                    cadastrarProduto();
                    return;
                }
            }

            produtos.Add(new Produto
            {
                ID = id,
                NomeProduto = nomeProduto ?? "[Novo produto]",
                QuantidadeEmEstoque = quantidadeEmEstoque
            });

            mensagem($""" {nomeProduto} cadastrado com sucesso. """);    
        }

        void mensagem(string msg)
        {
            Console.Clear();
            Console.WriteLine(msg);
            Thread.Sleep(1500);
        }

        void listarProdutosCadastrados()
        {
            if(produtos.Count == 0)
            {
                menuCadastraProdutoSeNaoExiste();
            }

            mostrarProdutosCadastrados(false, 0, "===============[ Selecione um produto da lista ]===================");
        }

        void menuCadastraProdutoSeNaoExiste()
        {
            Console.WriteLine("""
            O que você deseja fazer?
            1 - Cadastrar produto
            2 - Voltar ao menu
            3 - Sair do programa
            """);

            var opcao = Console.ReadLine()?.Trim();

            switch(opcao)
            {
                case "1":
                    cadastrarProduto();
                    break;
                case "3":
                    break;
                case "2":
                    System.Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }

        void mostrarProdutosCadastrados(
            bool sleep = true,
            int timerSleep = 2000,
            string header = "===============[ Lista de produtos cadastrados ]===================")
        {
            Console.Clear();
            Console.WriteLine(header);

            foreach(var produto in produtos)
            {
                Console.WriteLine($"""
                ID: {produto.ID}
                Nome do Produto: {produto.NomeProduto}
                ----------------------------
                """);

                if(sleep)
                {
                    Thread.Sleep(timerSleep);
                    Console.Clear();
                }
            }
        }

        void listarQuantidadeItensEstoque()
        {
            if(produtos.Count == 0)
            {
                menuCadastraProdutoSeNaoExiste();
            }

            mostrarItensEstoque(false, 0, "===============[ Selecione um produto do estoque ]===================");
        }

        void mostrarItensEstoque(
            bool sleep = true,
            int timerSleep = 3000,
            string header = "===============[ Quantidade em estoque ]===================")
        {
            Console.Clear();
            Console.WriteLine(header);

            foreach(var produto in produtos)
            {
                Console.WriteLine($"""
                Nome do Produto: {produto.NomeProduto}
                Quantidade em estoque do produto: {produto.QuantidadeEmEstoque}
                ----------------------------
                """);

                if(sleep)
                {
                    Thread.Sleep(timerSleep);
                    Console.Clear();
                }
            }
        }
    }
}