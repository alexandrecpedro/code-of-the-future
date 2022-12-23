/* Faça um programa que leia itens de importação
para isso você terá que criar um programa para ler arquivos json
transformar estes arquivos em objetos
armezenar os cliente na lista em memória
mostrar o resultado na tela como um relatório


terá uma pasta imports com os arquivos
imports
    cliente1.json
    cliente2.json
    cliente3.json
    cliente4.json
    cliente5.json   

    Conteúdo do arquivo:
        {"nome": "Danilo", "telefone": "123432123"} */

using System.IO;
using System.Text.Json;
using ReadFile.Entity;
using JsonReadFile.Models;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("List of clients");
        Console.WriteLine("---------------------------------------");
        ReadFilesJson(1);

        /* string conteudoDoArquivo = """
            {"nome": "Danilo", "telefone": "123456"}
            """;

        var cliente = JsonSerializer.Deserialize<Cliente>(conteudoDoArquivo, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        });

    var json = JsonSerializer.Serialize(cliente); */
    }

    private static void ReadFilesJson(int fileName)
    {
        var basic_path = Environment.GetEnvironmentVariable("JSON_READ_FILE");
        var path = @$"{basic_path}/cliente{fileName}.json";

        if (File.Exists(path))
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                var client = JsonSerializer.Deserialize<Client>(json);
                Console.WriteLine($"Name: {client.Name} \nPhone: {client.Phone}");
                Console.WriteLine("---------------------------------------");
            }
        }

        path = @$"{basic_path}/client{fileName + 1}.json";

        if (File.Exists(path))
        {
            ReadFilesJson(fileName + 1);
        }
    }
}