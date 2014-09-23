using AppFramework.Infrastructure.Tasks;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace AppFramework.DependencyResolution
{
    public class TaskRegistry : Registry
    {
        public TaskRegistry()
        {
            Scan(scan =>
            {
                scan.AssembliesFromApplicationBaseDirectory(a => a.FullName.StartsWith(typeof(TaskRegistry).Namespace.Split('.')[0]));
                scan.AddAllTypesOf<IRunAtInit>();
                scan.AddAllTypesOf<IRunAtStartup>();
                scan.AddAllTypesOf<IRunOnEachRequest>();
                scan.AddAllTypesOf<IRunOnError>();
                scan.AddAllTypesOf<IRunAfterEachRequest>();
            });
        }
    }
}