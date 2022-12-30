namespace UnityOfWork.DAL.Interfaces;

interface IRepository<T> : IDisposable
{
    IEnumerable<T> GetAll();
    T GetByID(int id);
    T FindByName(string name);
    T Insert(T objeto);
    void Update(T objeto);
    void Delete(int objetoID);
    void Save();
}