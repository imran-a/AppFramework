using AutoMapper;

namespace AppFramework.Infrastructure.Mapping
{
	public interface IHaveCustomMappings
	{
		void CreateMappings(IConfiguration configuration);
	}
}