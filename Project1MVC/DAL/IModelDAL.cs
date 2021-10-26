using System.Collections.Generic;

namespace Project1MVC.DAL
{
    public interface IModelDAL<T>
    {
        bool Add(T obj);
        T Get(int id);
        List<T> GetAll();
        bool Update(T obj);
        bool Delete(int id);
    }
}