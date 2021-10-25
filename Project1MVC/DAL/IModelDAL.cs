using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1MVC.DAL
{
    public interface IModelDAL<T>
    {
        bool Add(T obj);
        T Get(int id);
        IEnumerable<T> GetAll();
        bool Update(T obj);
        bool Delete(T obj);
    }
}