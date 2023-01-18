namespace locacao_veiculos.Servicos;

public struct LoginService
{
    public static bool Logar(string email, string senha)
    {
        return email == "teste@teste.com" && senha == "123456";
    }
}