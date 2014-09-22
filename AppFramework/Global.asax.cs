using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AppFramework.App_Start;
using AppFramework.Infrastructure.Tasks;

namespace AppFramework
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            StructuremapMvc.StructureMapDependencyScope.CreateNestedContainer();
            using (var container = StructuremapMvc.StructureMapDependencyScope.CurrentNestedContainer)
            {
                foreach (var task in container.GetAllInstances<IRunAtInit>())
                {
                    task.Execute();
                }

                foreach (var task in container.GetAllInstances<IRunAtStartup>())
                {
                    task.Execute();
                }
            }
        }

        public void Application_BeginRequest()
        {
            StructuremapMvc.StructureMapDependencyScope.CreateNestedContainer();
            var container = StructuremapMvc.StructureMapDependencyScope.CurrentNestedContainer;
            foreach (var task in container.GetAllInstances<IRunOnEachRequest>())
            {
                task.Execute();
            }
        }

        public void Application_Error()
        {
            StructuremapMvc.StructureMapDependencyScope.CreateNestedContainer();
            var container = StructuremapMvc.StructureMapDependencyScope.CurrentNestedContainer;
            foreach (var task in container.GetAllInstances<IRunOnError>())
            {
                task.Execute();
            }
        }

        public void Application_EndRequest()
        {
            StructuremapMvc.StructureMapDependencyScope.CreateNestedContainer();
            var container = StructuremapMvc.StructureMapDependencyScope.CurrentNestedContainer;
            foreach (var task in container.GetAllInstances<IRunAfterEachRequest>())
            {
                task.Execute();
            }
        }
    }
}
