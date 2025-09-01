using System;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Platform.Configuration;

namespace Parse.Abstractions.Platform.Configuration
{
	// Token: 0x02000087 RID: 135
	public interface IParseCurrentConfigurationController
	{
		// Token: 0x0600053A RID: 1338
		Task<ParseConfiguration> GetCurrentConfigAsync(IServiceHub serviceHub);

		// Token: 0x0600053B RID: 1339
		Task SetCurrentConfigAsync(ParseConfiguration config);

		// Token: 0x0600053C RID: 1340
		Task ClearCurrentConfigAsync();

		// Token: 0x0600053D RID: 1341
		Task ClearCurrentConfigInMemoryAsync();
	}
}
