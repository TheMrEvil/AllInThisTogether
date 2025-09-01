using System;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;

namespace Parse
{
	// Token: 0x0200001D RID: 29
	public static class InstallationServiceExtensions
	{
		// Token: 0x06000198 RID: 408 RVA: 0x0000798E File Offset: 0x00005B8E
		public static ParseQuery<ParseInstallation> GetInstallationQuery(this IServiceHub serviceHub)
		{
			return new ParseQuery<ParseInstallation>(serviceHub);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007998 File Offset: 0x00005B98
		public static ParseInstallation GetCurrentInstallation(this IServiceHub serviceHub)
		{
			Task<ParseInstallation> async = serviceHub.CurrentInstallationController.GetAsync(serviceHub, default(CancellationToken));
			async.Wait();
			return async.Result;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000079C5 File Offset: 0x00005BC5
		internal static void ClearInMemoryInstallation(this IServiceHub serviceHub)
		{
			serviceHub.CurrentInstallationController.ClearFromMemory();
		}
	}
}
