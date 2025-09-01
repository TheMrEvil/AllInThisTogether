using System;
using System.IO;
using System.Net.Security;
using Mono.Net.Security;
using Mono.Security.Interface;

namespace Mono.Btls
{
	// Token: 0x020000EF RID: 239
	internal class MonoBtlsStream : MobileAuthenticatedStream
	{
		// Token: 0x0600054A RID: 1354 RVA: 0x000052F4 File Offset: 0x000034F4
		public MonoBtlsStream(Stream innerStream, bool leaveInnerStreamOpen, SslStream owner, MonoTlsSettings settings, MobileTlsProvider provider) : base(innerStream, leaveInnerStreamOpen, owner, settings, provider)
		{
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000F61D File Offset: 0x0000D81D
		protected override MobileTlsContext CreateContext(MonoSslAuthenticationOptions options)
		{
			return new MonoBtlsContext(this, options);
		}
	}
}
