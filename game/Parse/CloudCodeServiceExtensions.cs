using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;

namespace Parse
{
	// Token: 0x0200001B RID: 27
	public static class CloudCodeServiceExtensions
	{
		// Token: 0x06000190 RID: 400 RVA: 0x000078EE File Offset: 0x00005AEE
		public static Task<T> CallCloudCodeFunctionAsync<T>(this IServiceHub serviceHub, string name, IDictionary<string, object> parameters)
		{
			return serviceHub.CallCloudCodeFunctionAsync(name, parameters, CancellationToken.None);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000078FD File Offset: 0x00005AFD
		public static Task<T> CallCloudCodeFunctionAsync<T>(this IServiceHub serviceHub, string name, IDictionary<string, object> parameters, CancellationToken cancellationToken)
		{
			return serviceHub.CloudCodeController.CallFunctionAsync<T>(name, parameters, serviceHub.GetCurrentSessionToken(), serviceHub, cancellationToken);
		}
	}
}
