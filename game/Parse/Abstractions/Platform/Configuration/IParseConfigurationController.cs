using System;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Platform.Configuration;

namespace Parse.Abstractions.Platform.Configuration
{
	// Token: 0x02000086 RID: 134
	public interface IParseConfigurationController
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000538 RID: 1336
		IParseCurrentConfigurationController CurrentConfigurationController { get; }

		// Token: 0x06000539 RID: 1337
		Task<ParseConfiguration> FetchConfigAsync(string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken));
	}
}
