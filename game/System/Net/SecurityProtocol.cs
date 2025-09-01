using System;
using System.Security.Authentication;

namespace System.Net
{
	// Token: 0x0200056A RID: 1386
	internal static class SecurityProtocol
	{
		// Token: 0x0400183E RID: 6206
		public const SslProtocols DefaultSecurityProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;

		// Token: 0x0400183F RID: 6207
		public const SslProtocols SystemDefaultSecurityProtocols = SslProtocols.None;
	}
}
