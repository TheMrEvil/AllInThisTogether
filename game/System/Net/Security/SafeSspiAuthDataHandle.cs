using System;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Security
{
	// Token: 0x02000842 RID: 2114
	internal sealed class SafeSspiAuthDataHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600436A RID: 17258 RVA: 0x00013B6C File Offset: 0x00011D6C
		public SafeSspiAuthDataHandle() : base(true)
		{
		}

		// Token: 0x0600436B RID: 17259 RVA: 0x000EB5D7 File Offset: 0x000E97D7
		protected override bool ReleaseHandle()
		{
			return Interop.SspiCli.SspiFreeAuthIdentity(this.handle) == Interop.SECURITY_STATUS.OK;
		}
	}
}
