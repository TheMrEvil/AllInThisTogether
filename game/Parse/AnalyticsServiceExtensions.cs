using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Infrastructure.Utilities;

namespace Parse
{
	// Token: 0x0200001A RID: 26
	public static class AnalyticsServiceExtensions
	{
		// Token: 0x0600018C RID: 396 RVA: 0x000077FF File Offset: 0x000059FF
		public static Task TrackLaunchAsync(this IServiceHub serviceHub)
		{
			return serviceHub.TrackLaunchWithPushHashAsync(null);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007808 File Offset: 0x00005A08
		public static Task TrackAnalyticsEventAsync(this IServiceHub serviceHub, string name)
		{
			return serviceHub.TrackAnalyticsEventAsync(name, null);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007814 File Offset: 0x00005A14
		public static Task TrackAnalyticsEventAsync(this IServiceHub serviceHub, string name, IDictionary<string, string> dimensions)
		{
			if (name == null || name.Trim().Length == 0)
			{
				throw new ArgumentException("A name for the custom event must be provided.");
			}
			return serviceHub.CurrentUserController.GetCurrentSessionTokenAsync(serviceHub, default(CancellationToken)).OnSuccess((Task<string> task) => serviceHub.AnalyticsController.TrackEventAsync(name, dimensions, task.Result, serviceHub, default(CancellationToken))).Unwrap();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007898 File Offset: 0x00005A98
		private static Task TrackLaunchWithPushHashAsync(this IServiceHub serviceHub, string pushHash = null)
		{
			return serviceHub.CurrentUserController.GetCurrentSessionTokenAsync(serviceHub, default(CancellationToken)).OnSuccess((Task<string> task) => serviceHub.AnalyticsController.TrackAppOpenedAsync(pushHash, task.Result, serviceHub, default(CancellationToken))).Unwrap();
		}

		// Token: 0x020000C5 RID: 197
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0
		{
			// Token: 0x06000624 RID: 1572 RVA: 0x0001365A File Offset: 0x0001185A
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x06000625 RID: 1573 RVA: 0x00013664 File Offset: 0x00011864
			internal Task <TrackAnalyticsEventAsync>b__0(Task<string> task)
			{
				return this.serviceHub.AnalyticsController.TrackEventAsync(this.name, this.dimensions, task.Result, this.serviceHub, default(CancellationToken));
			}

			// Token: 0x04000167 RID: 359
			public IServiceHub serviceHub;

			// Token: 0x04000168 RID: 360
			public string name;

			// Token: 0x04000169 RID: 361
			public IDictionary<string, string> dimensions;
		}

		// Token: 0x020000C6 RID: 198
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x06000626 RID: 1574 RVA: 0x000136A2 File Offset: 0x000118A2
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x06000627 RID: 1575 RVA: 0x000136AC File Offset: 0x000118AC
			internal Task <TrackLaunchWithPushHashAsync>b__0(Task<string> task)
			{
				return this.serviceHub.AnalyticsController.TrackAppOpenedAsync(this.pushHash, task.Result, this.serviceHub, default(CancellationToken));
			}

			// Token: 0x0400016A RID: 362
			public IServiceHub serviceHub;

			// Token: 0x0400016B RID: 363
			public string pushHash;
		}
	}
}
