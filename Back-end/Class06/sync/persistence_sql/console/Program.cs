using ConsoleApp.UI;

while (true)
{
    Console.Clear();
    Console.WriteLine("""
        1 - Cadastrar produtos
        2 - Atualizar produtos
        3 - Listar produtos
        4 - Sair
    """);

    bool sair = false;
    var opcao = Console.ReadLine()?.Trim();
    Console.Clear();
    
    switch (opcao)
    {
        case "1":
            Console.Clear();
            ProdutosUI.Cadastrar();
            break;
        case "2":
            Console.Clear();
            ProdutosUI.Atualizar();
            break;
        case "3":
            Console.Clear();
            ProdutosUI.Listar();
            break;
        default:
            sair = true;
            break;
    }

    if (sair) break;
}