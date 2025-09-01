using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020005EE RID: 1518
	[StructLayout(LayoutKind.Sequential)]
	internal class SecChannelBindings
	{
		// Token: 0x06003063 RID: 12387 RVA: 0x0000219B File Offset: 0x0000039B
		public SecChannelBindings()
		{
		}

		// Token: 0x04001BAE RID: 7086
		internal int dwInitiatorAddrType;

		// Token: 0x04001BAF RID: 7087
		internal int cbInitiatorLength;

		// Token: 0x04001BB0 RID: 7088
		internal int dwInitiatorOffset;

		// Token: 0x04001BB1 RID: 7089
		internal int dwAcceptorAddrType;

		// Token: 0x04001BB2 RID: 7090
		internal int cbAcceptorLength;

		// Token: 0x04001BB3 RID: 7091
		internal int dwAcceptorOffset;

		// Token: 0x04001BB4 RID: 7092
		internal int cbApplicationDataLength;

		// Token: 0x04001BB5 RID: 7093
		internal int dwApplicationDataOffset;
	}
}
