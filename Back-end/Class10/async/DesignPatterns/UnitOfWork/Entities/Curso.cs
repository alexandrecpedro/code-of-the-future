using System.ComponentModel.DataAnnotations.Schema;

namespace UnityOfWork.Entities;

[Table("Curso")]
public class Curso
{
    public Curso()
    {
        Matriculas = new HashSet<Matricula>();
    }

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int ID { get; set; }
    public string Titulo { get; set; } = default!;
    public string Creditos { get; set; } = default!;
    public DateTime DataMatricula { get; set; }
    public virtual ICollection<Matricula> Matriculas { get; set; }
}