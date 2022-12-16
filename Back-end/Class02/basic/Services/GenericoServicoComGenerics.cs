using Basico.Interfaces;
using Basico.Models;

namespace Basico.Services;

public class GenericoServicoComGenerics<T>
{
    public static void ImprimeNome(T objeto)
    {
        var nome = typeof(T).GetProperty("Nome")?.GetValue(objeto);

        // === setando dado na propriedade
        // typeof(T).GetProperty("Nome")?.SetValue(objeto, "Leandro");

        // === Executando método
        // var nome2 = typeof(T).GetMethod("NomeMinusculo")?.Invoke(objeto, null);
        
        // === Executando método com paramatros
        // var parametros = new List<string>();
        // parametros.Add("Danilo");
        // parametros.Add("Santos");
        // var nome_min = typeof(T).GetMethod("MetodoComDoisParametros")?.Invoke(objeto, parametros.ToArray());

        Console.WriteLine($"O nome é: {nome}");
    }
}