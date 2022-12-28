

using database.interfaces;
using negocio.models;

namespace negocio.services;

public class ProdutoServico
{
   public IRepositorio<Produto> repositorio;

    public ProdutoServico(IRepositorio<Produto> repositorio)
    {
        this.repositorio = repositorio;
    }

    public void Salvar(Produto produto)
    {
        repositorio.Salvar(produto);
    }

    public List<Produto> BuscarTodos(string criterio = "")
    {

        List<Produto>? listaDeProduto = new List<Produto>();
        if(string.IsNullOrEmpty(criterio))
        {
            listaDeProduto = repositorio.BuscarTodos();
        } else {
            listaDeProduto = repositorio.BuscarTodos(criterio);
        }
        return listaDeProduto;
    }

   public void ApagarPorId(int id)
    {
        repositorio.ApagarPorId(id);
    }

    public Produto? BuscarPorId(int id)
    {
        Produto? produto = repositorio.BuscaPorId(id);            
        return produto;
    }
}