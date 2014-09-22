using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace AppFramework.DependencyResolution
{
    public class StandardRegistry : Registry
    {
        // structuremap registry that maps ISomething to Something by convention

        public StandardRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
        }
    }
}