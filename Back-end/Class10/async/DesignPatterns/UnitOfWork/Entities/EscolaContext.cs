using Microsoft.EntityFrameworkCore;

namespace UnityOfWork.Entities;

public class EscolaContext : DbContext
{
    // public EscolaContext() : base("name=Escola") {}
    public EscolaContext() : base() {}
    public EscolaContext(DbContextOptions<EscolaContext> options) : base(options) { }

    public virtual DbSet<Aluno> Alunos { get; set; }
    public virtual DbSet<Curso> Cursos { get; set; }
    public virtual DbSet<Matricula> Matriculas { get; set; }

    // protected void OnModelCreating(DbModelBuilder modelBuilder) {}

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     var conexao = "aqui vai a string de conexao";
    //     optionsBuilder.UseMySql(conexao, ServerVersion.AutoDetect(conexao));
    // }
}