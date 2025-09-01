using System;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Security
{
	// Token: 0x02000847 RID: 2119
	internal sealed class SafeCredentialReference : CriticalHandleMinusOneIsInvalid
	{
		// Token: 0x0600437D RID: 17277 RVA: 0x000EB8B8 File Offset: 0x000E9AB8
		internal static SafeCredentialReference CreateReference(SafeFreeCredentials target)
		{
			SafeCredentialReference safeCredentialReference = new SafeCredentialReference(target);
			if (safeCredentialReference.IsInvalid)
			{
				return null;
			}
			return safeCredentialReference;
		}

		// Token: 0x0600437E RID: 17278 RVA: 0x000EB8D8 File Offset: 0x000E9AD8
		private SafeCredentialReference(SafeFreeCredentials target)
		{
			bool flag = false;
			target.DangerousAddRef(ref flag);
			this.Target = target;
			base.SetHandle(new IntPtr(0));
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x000EB908 File Offset: 0x000E9B08
		protected override bool ReleaseHandle()
		{
			SafeFreeCredentials target = this.Target;
			if (target != null)
			{
				target.DangerousRelease();
			}
			this.Target = null;
			return true;
		}

		// Token: 0x040028CE RID: 10446
		internal SafeFreeCredentials Target;
	}
}
