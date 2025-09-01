using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;

namespace Parse.Abstractions.Platform.Cloud
{
	// Token: 0x02000088 RID: 136
	public interface IParseCloudCodeController
	{
		// Token: 0x0600053E RID: 1342
		Task<T> CallFunctionAsync<T>(string name, IDictionary<string, object> parameters, string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken));
	}
}
