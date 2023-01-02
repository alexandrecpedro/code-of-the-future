using Microsoft.EntityFrameworkCore;
using UnityOfWork.DAL.Interfaces;
using UnityOfWork.Entities;

namespace UnityOfWork.DAL;

public class AlunoRepository : IRepository<Aluno>
{
    private EscolaContext context;

    public AlunoRepository(EscolaContext context)
    {
        this.context = context;
    }

    public IEnumerable<Aluno> GetAll()
    {
        return context.Alunos.ToList();
    }

    public Aluno GetByID(int id)
    {
        return context.Alunos.Find(id);
    }

     public Aluno FindByName(string name)
    {
        return context.Alunos.Where(aluno => aluno.Nome == name).SingleOrDefault()!;
    }

    public Aluno Insert(Aluno aluno)
    {
        context.Alunos.Add(aluno);
        return aluno;
    }

    public void Update(Aluno aluno)
    {
        // checar essa questão
        context.Entry(aluno).State = EntityState.Modified;
    }

    public void Delete(int alunoID)
    {
        Aluno aluno = context.Alunos.Find(alunoID);
        context.Alunos.Remove(aluno);
    }

    public void Save()
    {
        context.SaveChanges();
    }

    // Safely deleting from memory
    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
            if (disposing)
                context.Dispose();

        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        // Garbage Collector (GC)
        GC.SuppressFinalize(this);
    }
}