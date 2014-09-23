using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppFramework.App_Start;
using StructureMap;

namespace AppFramework.DependencyResolution
{
    public class StructureMapFilterProvider : FilterAttributeFilterProvider
    {
        public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(controllerContext, actionDescriptor);

            var container = StructuremapMvc.StructureMapDependencyScope.Container;

            foreach (var filter in filters)
            {
                container.BuildUp(filter.Instance);
                yield return filter;
            }
        }
    }
}