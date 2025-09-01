using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;

namespace System.Net.Http
{
	// Token: 0x02000011 RID: 17
	internal static class PlatformHelper
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00003AE6 File Offset: 0x00001CE6
		internal static bool IsContentHeader(string name)
		{
			return HttpHeaders.GetKnownHeaderKind(name) == HttpHeaderKind.Content;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003AF1 File Offset: 0x00001CF1
		internal static string GetSingleHeaderString(string name, IEnumerable<string> values)
		{
			return HttpHeaders.GetSingleHeaderString(name, values);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003AFA File Offset: 0x00001CFA
		internal static StreamContent CreateStreamContent(Stream stream, CancellationToken cancellationToken)
		{
			return new StreamContent(stream, cancellationToken);
		}
	}
}
