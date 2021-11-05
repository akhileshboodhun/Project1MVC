using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Project1MVC.Services;
using Project1MVC.DAL;
using Project1MVC.Models;
using Unity.Lifetime;

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
            //container.RegisterSingleton<IEquipmentService, EquipmentService>(EquipmentService.Instance);
            //container.RegisterType<IRepository<Equipment>, EquipmentRepository>();

            //container.RegisterInstance<IEquipmentService>(EquipmentService.Instance, new PerThreadLifetimeManager());
            container.RegisterInstance<IRepository<Equipment>>(EquipmentRepository.Instance);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}