using Business.Entidades;
using business.tests.Repositorios.Mocks;

namespace business.tests.Repositorios;

[TestClass]
public class ClienteRepositorioTest
{
    [TestMethod]
    public void TestandoSalvarNoBancoDeDados()
    {
        new ClienteRepositorioMock().Salvar(new Cliente{
            Nome = "Jamil",
            Email = "jamil@teste.com"
        });

        var quantidade = new ClienteRepositorioMock().Todos().Count;

        Assert.AreEqual(1, quantidade);
    }
}