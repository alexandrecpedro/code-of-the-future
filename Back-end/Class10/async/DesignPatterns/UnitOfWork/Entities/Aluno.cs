using System.ComponentModel.DataAnnotations.Schema;

namespace UnityOfWork.Entities;

[Table("Aluno")]
public class Aluno
{
    public Aluno()
    {
        Matriculas = new HashSet<Matricula>();
    }
    public int ID { get; set; }
    public string Nome { get; set; } = default!;
    public string Sobrenome { get; set; } = default!;
    public DateTime DataMatricula { get; set; }

    public virtual ICollection<Matricula> Matriculas { get; set; }
}