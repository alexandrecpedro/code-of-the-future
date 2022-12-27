using System.Reflection;
using Database.Interfaces;
using MySql.Data.MySqlClient;

namespace Database.MySql;
public class RepositorioMYSQL<T> : IRepositorio<T>
{
    private readonly string? conexao = Environment.GetEnvironmentVariable("DATABASE_URL_PRODUTO");

    private string nomeTabela()
    {
        var nome = typeof(T).Name.ToLower() + "s";
        return nome;
    }

    private string nomePropriedade(PropertyInfo prop)
    {
        var nome = prop.Name.ToLower();
        return nome;
    }

    public void Apagar(string id)
    {
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"delete from {nomeTabela()} where id = {id};";

            var command = new MySqlCommand(query, conn);
            command.ExecuteNonQuery();

            conn.Close();
        }
    }

    public T? BuscaPorId(string id)
    {
        var objetos = this.BuscarTodos($"id = {id}");
        if (objetos.Count == 0) 
            return default(T);
        return objetos[0];
    }

    public List<T> BuscarTodos(string criterio = "")
    {
        var list = new List<T>();
        using (var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"""
                select * from {nomeTabela()}
            """;

            if (!string.IsNullOrEmpty(criterio))
                query += $"where {criterio}";
            
            var command = new MySqlCommand(query, conn);
            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                var obj = Activator.CreateInstance<T>();

                foreach (var prop in typeof(T).GetProperties())
                {
                    var valor = dataReader[nomePropriedade(prop)].ToString();
                    obj?.GetType().GetProperty(nomePropriedade(prop))?.SetValue(obj, valor);
                }
                
                list.Add((T) obj);
            }

            conn.Close();
        }

        return list;
    }

    public void Salvar(T obj)
    {
        Console.WriteLine("======[" + conexao + "]=========");
        using (var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            List<string> colunasArray = new List<string>();
            List<string?> valoresArray = new List<string?>();
            List<string?> updateArray = new List<string?>();
            foreach (var prop in typeof(T).GetProperties())
            {
                var nome = this.nomePropriedade(prop);
                if (nome == "Id") continue;
                colunasArray.Add(nome);
                valoresArray.Add($"'{prop.GetValue(obj)}'");
                updateArray.Add($"${nome}='{prop.GetValue(obj)}'");
            }
            var colunas = string.Join(", ", colunasArray.ToArray());
            var valores = string.Join(", ", valoresArray.ToArray());
            var update = string.Join(", ", updateArray.ToArray());

            var query = $"insert into {nomeTabela()} ({colunas}) values ({valores});";
            var id = Convert.ToInt32(typeof(T).GetProperty("Id")?.GetValue(obj));
            if(id > 0)
                query = $"update {nomeTabela()} set {update} where id = {id};";

            var command = new MySqlCommand(query, conn);
            command.ExecuteNonQuery();

            conn.Close();
        }
    }
}
