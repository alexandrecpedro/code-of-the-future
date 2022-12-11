using System.Collections.Generic;
using Programa.Models;

namespace Programa.Services;

public class ProdutoServico
{
    /* ATTRIBUTES */
    public List<Produto> listaProdutos = new List<Produto>();
    private static ProdutoServico instancia = default!; // cannot be null

    /* CONSTRUCTOR */
    private ProdutoServico() { }

    /* GETTERS/SETTERS */
    public static ProdutoServico Get() {
        if (instancia == null) instancia = new ProdutoServico();
        return instancia;
    }

    /* METHODS */
    public List<Produto> Lista = new List<Produto>();
}