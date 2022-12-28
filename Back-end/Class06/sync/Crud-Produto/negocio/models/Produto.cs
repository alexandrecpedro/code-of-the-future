namespace negocio.models;
public record Produto
{
    public int Id {get;set;} = default!;
    public string Nome {get;set;} = default!;
    public string Descricao {get;set;} = default!;
    public int Quantidade {get;set;}
    public DateTime data_vencimento {get;set;}
    public DateTime created_at {get;set;}
    public DateTime updated_at {get;set;}

    public Produto() 
    {
        this.created_at = DateTime.Now;
        this.updated_at = DateTime.Now;
    }
}