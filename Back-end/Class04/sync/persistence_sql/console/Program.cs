using ConsoleApp.UI;

while (true)
{
    Console.WriteLine("""
        1 - Cadastrar cliente
        2 - Atualizar cliente
        3 - Listar cliente
        4 - Sair
    """);

    bool sair = false;
    var opcao = Console.ReadLine()?.Trim();
    Console.Clear();
    
    switch (opcao)
    {
        case "1":
            Console.Clear();
            ClientesUI.Cadastrar();
            break;
        case "2":
            Console.Clear();
            ClientesUI.Atualizar();
            break;
        case "3":
            Console.Clear();
            ClientesUI.Listar();
            break;
        default:
            sair = true;
            break;
    }

    if (sair) break;
}