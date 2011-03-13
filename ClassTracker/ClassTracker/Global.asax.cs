﻿
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using NHibernate;
using Ninject;
using Ninject.Web.Mvc;


namespace ClassTracker
{
    public class MvcApplication : NinjectHttpApplication
    {
        ISessionFactory sessionFactory;

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = "" });

        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            // Use Ninject to register all of our controllers.
            // Reportedly only for MVC1
            // RegisterAllControllersIn(Assembly.GetExecutingAssembly());
        }

        protected void MvcApplication_BeginRequest()
        {
            sessionFactory.OpenSession();
        }

        protected void MvcApplication_EndRequest()
        {

        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }
    }
}
