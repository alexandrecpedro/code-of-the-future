using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UnityOfWork.Entities;

namespace UnityOfWork.DAL;

public class GenericRepository<TEntity> where TEntity : class
{
    internal EscolaContext context;
    internal DbSet<TEntity> dbSet;

    public GenericRepository(EscolaContext context)
    {
        this.context = context;
        this.dbSet = context.Set<TEntity>();
    }

    public IEnumerable<TEntity> GetAll()
    {
        return dbSet.ToList();
    }

    public TEntity? GetByID(int id)
    {
        return dbSet.Find(id);
    }

     public TEntity Find(Expression<Func<TEntity, bool>> filter)
    {
        return dbSet.Where(filter).SingleOrDefault()!;
    }

    public TEntity Insert(TEntity entity)
    {
        dbSet.Add(entity);
        return entity;
    }

    public void Update(TEntity entity)
    {
        // checar essa questão
        context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        TEntity entity = dbSet.Find(id);
        dbSet.Remove(entity);
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