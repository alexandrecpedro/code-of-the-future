using locacao_veiculos.Servicos;

namespace locacao_veiculos.test.Servicos;

[TestClass]
public class LoginTest
{
    [TestMethod]
    public void TestandoLogin()
    {
        //Arrange (Organizando variáveis para execução do teste)
        var email = "teste@teste.com";
        var senha = "123456";

        //Act (Executando a função do teste)
        var resultado = LoginService.Logar(email, senha);

        //Assert (validando o retorno da execução)
        Assert.IsTrue(resultado);
    }
}