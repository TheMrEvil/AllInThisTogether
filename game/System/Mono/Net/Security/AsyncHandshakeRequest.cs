using System;

namespace Mono.Net.Security
{
	// Token: 0x0200008F RID: 143
	internal class AsyncHandshakeRequest : AsyncProtocolRequest
	{
		// Token: 0x06000238 RID: 568 RVA: 0x00006A9E File Offset: 0x00004C9E
		public AsyncHandshakeRequest(MobileAuthenticatedStream parent, bool sync) : base(parent, sync)
		{
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00006AA8 File Offset: 0x00004CA8
		protected override AsyncOperationStatus Run(AsyncOperationStatus status)
		{
			return base.Parent.ProcessHandshake(status, false);
		}
	}
}
