using Project1MVC.DAL;
using Project1MVC.Models;
using Project1MVC.Services;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.Mvc5;

namespace Project1MVC
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IEquipmentService, EquipmentService>(new PerThreadLifetimeManager());
            container.RegisterType<IRepository<Equipment>, EquipmentRepository>(new PerThreadLifetimeManager());
            container.RegisterType<IDBProvider, DBManager>(new PerThreadLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}