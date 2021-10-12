using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1MVC.Services
{
    interface IManageData<T>
    {
        void Add(T obj);
        T Get(Guid id);
        IEnumerable<T> GetAll();
        void Update(T obj);
        void Delete(T obj);

    }
}
