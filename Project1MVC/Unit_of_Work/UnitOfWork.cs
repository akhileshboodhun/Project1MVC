using Project1MVC.DAL;
using Project1MVC.Models;
using Project1MVC.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project1MVC.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool disposedValue;
        private readonly IDBProvider dbProvider;
        private readonly SqlTransaction transaction;
        private IEquipmentService equipmentService = null;

        public UnitOfWork(IDBProvider provider)
        {
            dbProvider = provider;
        }

        public IEquipmentService EquipmentService
        {
            get
            {
                if (this.equipmentService == null)
                {
                    this.equipmentService = new EquipmentService(new EquipmentRepository(dbProvider));
                }
                return equipmentService;
            }
        }

        public void Commit()
        {
            transaction.Commit(); // TODO: use try-block here
        }

        public void Rollback()
        {
            transaction.Rollback();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}