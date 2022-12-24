using Database.Atributos;
using Database.Interfaces;
using Database.MySql.Interfaces;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Reflection;

namespace Database.MySQL.Repositorios;

public class RepositorioMySQL<T> : IRepositorio<T>, IRepositorioSql<T>
{
    // exemplo de implementação driver mysql
    // https://github.com/Didox/Desafio21dias_TDD_ORM_Repository/blob/main/APIDesafio/Servicos/Database/SqlRepositorio.cs

    private static readonly string? connection = Environment.GetEnvironmentVariable("DATABASE_CODE_OF_THE_FUTURE")!;

    // public static string PreparaCampoQuery(string valor)
    // {
    //     valor = valor.Replace("'", "");
    //     valor = valor.Replace("[", "[[]");
    //     valor = valor.Replace("%", "[%]");
    //     valor = valor.Replace("_", "[_]");
    //     return valor;
    // }

    private string nomeTabela()
    {
        var nome = typeof(T).Name.ToLower() + "s";

        TabelaAttribute? tabelaAttr = (TabelaAttribute?) typeof(T).GetCustomAttribute(typeof(TabelaAttribute));
        if (tabelaAttr != null)
            nome = tabelaAttr.Nome;
            
        return nome;
    }

    private string nomePropriedade(PropertyInfo prop)
    {
        var nome = prop.Name.ToLower();

        ColunaAttribute? colunaAttr = (ColunaAttribute?) typeof(T).GetCustomAttribute(typeof(ColunaAttribute));
        if (colunaAttr != null)
            nome = colunaAttr.Nome;
        
        return nome;
    }

    private void preencherObjeto(object modelo, SqlDataReader dr)
    {
        foreach (var prop in modelo.GetType().GetProperties())
        {
            try
            {
                if (dr[nomePropriedade(prop)] == DBNull.Value) continue;
                prop.SetValue(modelo, dr[nomePropriedade(prop)]);
            }
            catch { }
        }
    }

    public void Salvar(T obj)
    {
        Console.WriteLine("======[" + connection + "]=========");
        using (var conn = new MySqlConnection(connection))
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

    public List<T> BuscaPorIdOuEmail(string idOuEmail)
    {
        var list = new List<T>();

        using (var conn = new MySqlConnection(connection))
        {
            conn.Open();
            var query = $"""
                select * from {nomeTabela()}
                where id = '{idOuEmail}' or 
                      email like '%{idOuEmail}%'
            """;

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

    public List<T> Todos(string criterio = "")
    {
        var list = new List<T>();
        using (var conn = new MySqlConnection(connection))
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

    public void ApagaPorId(int id)
    {
        using(var conn = new MySqlConnection(connection))
        {
            conn.Open();
            var query = $"delete from {nomeTabela()} where id = {id};";

            var command = new MySqlCommand(query, conn);
            command.ExecuteNonQuery();

            conn.Close();
        }
    }

    public T? BuscaPorId(int id)
    {
        var objetos = this.Todos($"id = {id}");
        if (objetos.Count == 0) 
            return default(T);
        return objetos[0];
        /* // var list = new List<T>();
        T? objeto = default(T);
        using(var conn = new MySqlConnection(connection))
        {
            conn.Open();
            var query = $"""
                select * from {nomeTabela()} where id = '{id}'
            """;

            var command = new MySqlCommand(query, conn);
            var dataReader = command.ExecuteReader();
            while(dataReader.Read())
            {
                var obj = Activator.CreateInstance<T>();

                foreach (var prop in typeof(T).GetProperties())
                {
                    var valor = dataReader[nomePropriedade(prop)].ToString();
                    obj?.GetType().GetProperty(nomePropriedade(prop))?.SetValue(obj, valor);
                }

                // list.Add(obj);
                objeto = obj;
            }

            conn.Close();
        }

        return objeto;
        // return list.Find(obj => Convert.ToInt32(obj?.GetType().GetProperty("Id")?.GetValue(obj, null)) == id); */
    }
}