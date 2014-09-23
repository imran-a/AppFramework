using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.TypeRules;

namespace AppFramework.DependencyResolution
{
    public class ActionFilterRegistry : Registry
    {
        public ActionFilterRegistry()
        {
            For<IFilterProvider>().Use<StructureMapFilterProvider>();

            Policies.SetAllProperties(x =>
                x.Matching(p =>
                    p.DeclaringType.CanBeCastTo(typeof(ActionFilterAttribute)) &&
                    p.DeclaringType.Namespace.StartsWith(typeof(ActionFilterRegistry).Namespace.Split('.')[0]) &&
                    !p.PropertyType.IsPrimitive &&
                    p.PropertyType != typeof(string)));
        }
    }
}