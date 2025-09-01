using System;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Security
{
	// Token: 0x02000845 RID: 2117
	internal sealed class SafeFreeCertContext : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06004374 RID: 17268 RVA: 0x00013B6C File Offset: 0x00011D6C
		internal SafeFreeCertContext() : base(true)
		{
		}

		// Token: 0x06004375 RID: 17269 RVA: 0x00013B95 File Offset: 0x00011D95
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x06004376 RID: 17270 RVA: 0x000EB6F0 File Offset: 0x000E98F0
		protected override bool ReleaseHandle()
		{
			Interop.Crypt32.CertFreeCertificateContext(this.handle);
			return true;
		}

		// Token: 0x040028CC RID: 10444
		private const uint CRYPT_ACQUIRE_SILENT_FLAG = 64U;
	}
}
