using System;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x02000603 RID: 1539
	internal class CachedTransportContext : TransportContext
	{
		// Token: 0x060030AA RID: 12458 RVA: 0x000A76DD File Offset: 0x000A58DD
		internal CachedTransportContext(ChannelBinding binding)
		{
			this.binding = binding;
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000A76EC File Offset: 0x000A58EC
		public override ChannelBinding GetChannelBinding(ChannelBindingKind kind)
		{
			if (kind != ChannelBindingKind.Endpoint)
			{
				return null;
			}
			return this.binding;
		}

		// Token: 0x04001C39 RID: 7225
		private ChannelBinding binding;
	}
}
