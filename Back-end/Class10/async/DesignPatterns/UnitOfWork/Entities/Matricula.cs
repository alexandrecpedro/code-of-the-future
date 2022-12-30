using System.ComponentModel.DataAnnotations.Schema;

namespace UnityOfWork.Entities;

[Table("Matricula")]
public class Matricula
{
    public int ID { get; set; }
    public int CursoID { get; set; }
    public int AlunoID { get; set; }
    public int? Nota { get; set; }
    
    public virtual Aluno Aluno { get; set; }

    public virtual Curso Curso { get; set; }
}

public enum Nota {
    A, B, C, D, E, F
}