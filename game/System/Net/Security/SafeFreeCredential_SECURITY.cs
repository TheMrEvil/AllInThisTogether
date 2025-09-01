using System;

namespace System.Net.Security
{
	// Token: 0x02000848 RID: 2120
	internal sealed class SafeFreeCredential_SECURITY : SafeFreeCredentials
	{
		// Token: 0x06004380 RID: 17280 RVA: 0x000EB92D File Offset: 0x000E9B2D
		public SafeFreeCredential_SECURITY()
		{
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x000EB935 File Offset: 0x000E9B35
		protected override bool ReleaseHandle()
		{
			return Interop.SspiCli.FreeCredentialsHandle(ref this._handle) == 0;
		}
	}
}
