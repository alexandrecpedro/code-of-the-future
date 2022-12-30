using UnityOfWork.Entities;

namespace UnityOfWork.DAL;

public class UnitOfWork : IDisposable
{
    private EscolaContext context = new EscolaContext();
    private GenericRepository<Aluno>? alunoRepository;

    public GenericRepository<Aluno> AlunoRepository
    {
        get
        {
            if (alunoRepository is null)
            {
                alunoRepository = new GenericRepository<Aluno>(context);
            }
            return alunoRepository;
        }
    }

    private GenericRepository<Curso>? cursoRepository;

    public GenericRepository<Curso> CursoRepository
    {
        get
        {
            if (cursoRepository is null)
            {
                cursoRepository = new GenericRepository<Curso>(context);
            }
            return cursoRepository;
        }
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