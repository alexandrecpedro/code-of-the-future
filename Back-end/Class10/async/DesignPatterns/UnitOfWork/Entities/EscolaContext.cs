using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace UnityOfWork.Entities;

public class EscolaContext : DbContext
{
    public EscolaContext() : base("name=Escola") {}

    public virtual DbSet<Aluno> Alunos { get; set; }
    public virtual DbSet<Curso> Cursos { get; set; }
    public virtual DbSet<Matricula> Matriculas { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder) {}
}