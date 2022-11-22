using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient<ISourceFileToTemporaryFile, SourceFileToTemporaryFile>();
			services.AddTransient<IFileToConvert, FileToConvert>();
			services.AddTransient<ISaveFileToDisk, SaveFileToDisk>();
			services.AddScoped<ISendEmail, SendEmail>();

			return services;
		}

	}
}
