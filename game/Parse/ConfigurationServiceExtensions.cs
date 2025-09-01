using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Data;
using Parse.Platform.Configuration;

namespace Parse
{
	// Token: 0x0200001C RID: 28
	public static class ConfigurationServiceExtensions
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00007914 File Offset: 0x00005B14
		public static ParseConfiguration BuildConfiguration(this IServiceHub serviceHub, IDictionary<string, object> configurationData)
		{
			return ParseConfiguration.Create(configurationData, serviceHub.Decoder, serviceHub);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00007923 File Offset: 0x00005B23
		public static ParseConfiguration BuildConfiguration(this IParseDataDecoder dataDecoder, IDictionary<string, object> configurationData, IServiceHub serviceHub)
		{
			return ParseConfiguration.Create(configurationData, dataDecoder, serviceHub);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000792D File Offset: 0x00005B2D
		public static ParseConfiguration GetCurrentConfiguration(this IServiceHub serviceHub)
		{
			Task<ParseConfiguration> currentConfigAsync = serviceHub.ConfigurationController.CurrentConfigurationController.GetCurrentConfigAsync(serviceHub);
			currentConfigAsync.Wait();
			return currentConfigAsync.Result;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000794B File Offset: 0x00005B4B
		internal static void ClearCurrentConfig(this IServiceHub serviceHub)
		{
			serviceHub.ConfigurationController.CurrentConfigurationController.ClearCurrentConfigAsync().Wait();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00007962 File Offset: 0x00005B62
		internal static void ClearCurrentConfigInMemory(this IServiceHub serviceHub)
		{
			serviceHub.ConfigurationController.CurrentConfigurationController.ClearCurrentConfigInMemoryAsync().Wait();
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00007979 File Offset: 0x00005B79
		public static Task<ParseConfiguration> GetConfigurationAsync(this IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			return serviceHub.ConfigurationController.FetchConfigAsync(serviceHub.GetCurrentSessionToken(), serviceHub, cancellationToken);
		}
	}
}
