using System;

namespace Mono.Net.Security
{
	// Token: 0x02000093 RID: 147
	internal class AsyncShutdownRequest : AsyncProtocolRequest
	{
		// Token: 0x06000243 RID: 579 RVA: 0x00006C35 File Offset: 0x00004E35
		public AsyncShutdownRequest(MobileAuthenticatedStream parent) : base(parent, false)
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00006C3F File Offset: 0x00004E3F
		protected override AsyncOperationStatus Run(AsyncOperationStatus status)
		{
			return base.Parent.ProcessShutdown(status);
		}
	}
}
