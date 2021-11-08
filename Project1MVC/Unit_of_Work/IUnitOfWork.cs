using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.DAL
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}