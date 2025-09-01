using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x02000009 RID: 9
	internal static class HttpUtilities
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000278E File Offset: 0x0000098E
		internal static Version DefaultRequestVersion
		{
			get
			{
				return HttpVersion.Version20;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002795 File Offset: 0x00000995
		internal static Version DefaultResponseVersion
		{
			get
			{
				return HttpVersion.Version11;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000279C File Offset: 0x0000099C
		internal static bool IsHttpUri(Uri uri)
		{
			return HttpUtilities.IsSupportedScheme(uri.Scheme);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000027A9 File Offset: 0x000009A9
		internal static bool IsSupportedScheme(string scheme)
		{
			return HttpUtilities.IsSupportedNonSecureScheme(scheme) || HttpUtilities.IsSupportedSecureScheme(scheme);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000027BB File Offset: 0x000009BB
		internal static bool IsSupportedNonSecureScheme(string scheme)
		{
			return string.Equals(scheme, "http", StringComparison.OrdinalIgnoreCase) || HttpUtilities.IsNonSecureWebSocketScheme(scheme);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000027D3 File Offset: 0x000009D3
		internal static bool IsSupportedSecureScheme(string scheme)
		{
			return string.Equals(scheme, "https", StringComparison.OrdinalIgnoreCase) || HttpUtilities.IsSecureWebSocketScheme(scheme);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000027EB File Offset: 0x000009EB
		internal static bool IsNonSecureWebSocketScheme(string scheme)
		{
			return string.Equals(scheme, "ws", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000027F9 File Offset: 0x000009F9
		internal static bool IsSecureWebSocketScheme(string scheme)
		{
			return string.Equals(scheme, "wss", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002807 File Offset: 0x00000A07
		internal static Task ContinueWithStandard<T>(this Task<T> task, object state, Action<Task<T>, object> continuation)
		{
			return task.ContinueWith(continuation, state, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}
	}
}
