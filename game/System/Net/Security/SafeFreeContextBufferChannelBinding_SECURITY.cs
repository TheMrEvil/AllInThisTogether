using System;

namespace System.Net.Security
{
	// Token: 0x0200084B RID: 2123
	internal sealed class SafeFreeContextBufferChannelBinding_SECURITY : SafeFreeContextBufferChannelBinding
	{
		// Token: 0x0600438B RID: 17291 RVA: 0x000EB6E0 File Offset: 0x000E98E0
		protected override bool ReleaseHandle()
		{
			return Interop.SspiCli.FreeContextBuffer(this.handle) == 0;
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x000EBA6E File Offset: 0x000E9C6E
		public SafeFreeContextBufferChannelBinding_SECURITY()
		{
		}
	}
}
