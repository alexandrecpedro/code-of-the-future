using System.Reflection;
using database.interfaces;
using MySql.Data.MySqlClient;
using negocio.models;

namespace database.mysql;
public class RepositorioMYSQL<T> : IRepositorio<T>
{
    public readonly string? conexao = Environment.GetEnvironmentVariable("CODIGO_MYSQL");

    private string NomeDaTabela()
    {
        var nome = typeof(T).Name.ToLower() + "s";

        TabelaAttribute? tabelaAttr = (TabelaAttribute?)typeof(T).GetCustomAttribute(typeof(TabelaAttribute));
        if(tabelaAttr != null)
            nome = tabelaAttr.Nome;

        return nome;
    }

    private string NomeDaPropriedade(PropertyInfo prop)
    {
        var nome = prop.Name.ToLower();

        ColunaAttribute? colunaAttr = (ColunaAttribute?)typeof(T).GetCustomAttribute(typeof(ColunaAttribute));
        if(colunaAttr != null)
            nome = colunaAttr.Nome;

        return nome;
    }

    public void Salvar(T obj)
    {
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();

            List<string> colunasArray = new List<string>();
            List<string> valoresArray = new List<string>();
            List<string> updateArray = new List<string>();

            foreach(var prop in typeof(T).GetProperties())
            {
                string nome = this.NomeDaPropriedade(prop);
                if(nome == "Id") continue;
                if(nome.Contains("data") || nome.Contains("created")|| nome.Contains("updated"))
                {
                    var data = Convert.ToDateTime(prop.GetValue(obj)).ToString("yyyy-MM-dd HH:MM:ss");
                    valoresArray.Add($"'{data}'");
                } else {
                    valoresArray.Add($"'{prop.GetValue(obj)}'");
                }

                colunasArray.Add(nome);
                updateArray.Add($"{nome}='{prop.GetValue(obj)}'");
            }

            string colunas = string.Join(", ", colunasArray.ToArray());
            string valores = string.Join(", ", valoresArray.ToArray());
            string update = string.Join(", ", updateArray.ToArray());


            string query = $"insert into {this.NomeDaTabela()} ({colunas})values({valores});";
            int? id = Convert.ToInt32(typeof(T).GetProperty("Id")?.GetValue(obj));
            if(id > 0)
                query = $"update {this.NomeDaTabela()} set {update} where id = {id};";
            System.Console.WriteLine("----------------");
            System.Console.WriteLine(query);
            System.Console.WriteLine("----------------");
            var command = new MySqlCommand(query, conn);
            command.ExecuteNonQuery();

            conn.Close();
        }
    }

    public List<T> BuscarTodos(string criterio = "")
    {
        var listaDeObjeto = new List<T>();
         using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"""select * from {this.NomeDaTabela()} """;
            if (!string.IsNullOrEmpty(criterio))
                query += $"where {criterio};";


            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader dataReader = command.ExecuteReader();
            while(dataReader.Read())
            {

                var objeto = Activator.CreateInstance<T>();

                foreach (var prop in typeof(T).GetProperties())
                {
                    var valor = dataReader[prop.Name];
                    objeto?.GetType().GetProperty(prop.Name)?.SetValue(objeto,valor);
                }
                listaDeObjeto.Add(objeto);
            }

            conn.Close();
        }

        return listaDeObjeto;
    }

    public void ApagarPorId(int id)
    {
        /* REFATORAR ID */
        var query = $"delete from {NomeDaTabela()} where id={id};";
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var command = new MySqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
    }

    public T? BuscaPorId(int id)
    {
      var objetos = this.BuscarTodos($"id = {id}");
        if (objetos.Count == 0) 
            return default(T);
        return objetos[0];
    }
}
