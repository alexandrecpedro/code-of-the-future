using System.Text;
using System.Text.Json;

namespace Business.Models;

public class Pessoa
{
    public string Id { get; set; } = default!;

    public string Nome { get; set; } = default!;

    public string Tipo { get; set; } = default!;

    public virtual void Cadastrar(Pessoa pessoa)
    {
        var path = Environment.GetEnvironmentVariable("JSON_CLASSES");
        string json = File.ReadAllText(path!);

        var pessoas = JsonSerializer.Deserialize<List<Pessoa>>(json);
        
        pessoas.Add(pessoa);

        json = JsonSerializer.Serialize(pessoas);

        File.WriteAllText(path!, json, Encoding.ASCII);
    }
}