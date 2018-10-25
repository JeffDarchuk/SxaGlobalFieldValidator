using Microsoft.Extensions.DependencyInjection;
using Sitecore.Abstractions;
using Sitecore.DependencyInjection;

namespace JeffDarchuk.Foundation.ContentValidation
{
	public class ContentValidationConfigurator : IServicesConfigurator
	{
		public void Configure(IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<BaseValidatorManager, GlobalFieldValidatorManager>();
		}
	}
}