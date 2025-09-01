using System;

namespace System.Net.Security
{
	// Token: 0x02000849 RID: 2121
	internal sealed class SafeDeleteContext_SECURITY : SafeDeleteContext
	{
		// Token: 0x06004382 RID: 17282 RVA: 0x000EB945 File Offset: 0x000E9B45
		internal SafeDeleteContext_SECURITY()
		{
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x000EB94D File Offset: 0x000E9B4D
		protected override bool ReleaseHandle()
		{
			if (this._EffectiveCredential != null)
			{
				this._EffectiveCredential.DangerousRelease();
			}
			return Interop.SspiCli.DeleteSecurityContext(ref this._handle) == 0;
		}
	}
}
