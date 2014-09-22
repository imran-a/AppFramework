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

            RunTaskType<IRunAtInit>();
            RunTaskType<IRunAtStartup>();

        }

        public void Application_BeginRequest()
        {
            RunTaskType<IRunOnEachRequest>();
        }

        public void Application_Error()
        {
            RunTaskType<IRunOnError>();
        }

        public void Application_EndRequest()
        {
            RunTaskType<IRunAfterEachRequest>();
        }

        private void RunTaskType<T>() 
        {
            StructuremapMvc.StructureMapDependencyScope.CreateNestedContainer();
            var container = StructuremapMvc.StructureMapDependencyScope.CurrentNestedContainer;
            foreach (var task in container.GetAllInstances<T>())
            {
                ((IRunTaskExecuter) task).Execute();
            }
        }
    }
}
