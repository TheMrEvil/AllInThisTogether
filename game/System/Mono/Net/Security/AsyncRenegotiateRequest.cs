using System;

namespace Mono.Net.Security
{
	// Token: 0x02000094 RID: 148
	internal class AsyncRenegotiateRequest : AsyncProtocolRequest
	{
		// Token: 0x06000245 RID: 581 RVA: 0x00006C35 File Offset: 0x00004E35
		public AsyncRenegotiateRequest(MobileAuthenticatedStream parent) : base(parent, false)
		{
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00006C4D File Offset: 0x00004E4D
		protected override AsyncOperationStatus Run(AsyncOperationStatus status)
		{
			return base.Parent.ProcessHandshake(status, true);
		}
	}
}
