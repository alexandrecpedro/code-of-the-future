using Business.Entidades;

namespace business.tests.Entidades;

[TestClass]
public class ClienteTest
{
    [TestMethod]
    public void TestandoPropriedades()
    {
        var cliente = new Cliente();
        cliente.Id = 1;
        cliente.Nome = "Leo";
        cliente.Email = "leo@teste.com";

        Assert.AreEqual(1, cliente.Id);
        Assert.AreEqual("Leo", cliente.Nome);
        Assert.AreEqual("leo@teste.com", cliente.Email);
    }
}