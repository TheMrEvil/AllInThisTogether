using System;

namespace System.Net.Security
{
	// Token: 0x02000844 RID: 2116
	internal sealed class SafeFreeContextBuffer_SECURITY : SafeFreeContextBuffer
	{
		// Token: 0x06004372 RID: 17266 RVA: 0x000EB6D8 File Offset: 0x000E98D8
		internal SafeFreeContextBuffer_SECURITY()
		{
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x000EB6E0 File Offset: 0x000E98E0
		protected override bool ReleaseHandle()
		{
			return Interop.SspiCli.FreeContextBuffer(this.handle) == 0;
		}
	}
}
