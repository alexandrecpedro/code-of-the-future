namespace negocio.models;
public record Produto
{
    public int Id {get;set;} = default!;
    public string Nome {get;set;} = default!;
    public string Descricao {get;set;} = default!;
    public int Quantidade {get;set;}
    public DateTime Data_vencimento {get;set;}
    public DateTime Created_At {get;set;}
    public DateTime Updated_At {get;set;}

    public Produto() 
    {
        this.Created_At = DateTime.Now;
        this.Updated_At = DateTime.Now;
    }
}