using locacao_veiculos.ModelViews;

namespace locacao_veiculos.test.ModelViews;

[TestClass]
public class PedidoResumidoTest
{
    [TestMethod]
    public void TestandoPedidoResumido()
    {
        //Arrange (Organizando variáveis para execução do teste)
        var pedidoResumido = new PedidoResumido();

        //Act (Executando a função do teste)
        var data = new DateTime();
        pedidoResumido.DataEntregaPedido = data;
        pedidoResumido.MarcaDoCarro = "Ford";

        //Assert (validando o retorno da execução)
        Assert.AreEqual(data, pedidoResumido.DataEntregaPedido);
        Assert.AreEqual("Ford", pedidoResumido.MarcaDoCarro);
    }
}