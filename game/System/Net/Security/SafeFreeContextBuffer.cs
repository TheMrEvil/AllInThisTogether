using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Security
{
	// Token: 0x02000843 RID: 2115
	internal abstract class SafeFreeContextBuffer : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600436C RID: 17260 RVA: 0x00013B6C File Offset: 0x00011D6C
		protected SafeFreeContextBuffer() : base(true)
		{
		}

		// Token: 0x0600436D RID: 17261 RVA: 0x00013B95 File Offset: 0x00011D95
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x0600436E RID: 17262 RVA: 0x000EB5E8 File Offset: 0x000E97E8
		internal static int EnumeratePackages(out int pkgnum, out SafeFreeContextBuffer pkgArray)
		{
			SafeFreeContextBuffer_SECURITY safeFreeContextBuffer_SECURITY = null;
			int num = Interop.SspiCli.EnumerateSecurityPackagesW(out pkgnum, out safeFreeContextBuffer_SECURITY);
			pkgArray = safeFreeContextBuffer_SECURITY;
			if (num != 0 && pkgArray != null)
			{
				pkgArray.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x0600436F RID: 17263 RVA: 0x000EB610 File Offset: 0x000E9810
		internal static SafeFreeContextBuffer CreateEmptyHandle()
		{
			return new SafeFreeContextBuffer_SECURITY();
		}

		// Token: 0x06004370 RID: 17264 RVA: 0x000EB618 File Offset: 0x000E9818
		public unsafe static int QueryContextAttributes(SafeDeleteContext phContext, Interop.SspiCli.ContextAttribute contextAttribute, byte* buffer, SafeHandle refHandle)
		{
			int num = -2146893055;
			try
			{
				bool flag = false;
				phContext.DangerousAddRef(ref flag);
				num = Interop.SspiCli.QueryContextAttributesW(ref phContext._handle, contextAttribute, (void*)buffer);
			}
			finally
			{
				phContext.DangerousRelease();
			}
			if (num == 0 && refHandle != null)
			{
				if (refHandle is SafeFreeContextBuffer)
				{
					((SafeFreeContextBuffer)refHandle).Set(*(IntPtr*)buffer);
				}
				else
				{
					((SafeFreeCertContext)refHandle).Set(*(IntPtr*)buffer);
				}
			}
			if (num != 0 && refHandle != null)
			{
				refHandle.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x06004371 RID: 17265 RVA: 0x000EB694 File Offset: 0x000E9894
		public static int SetContextAttributes(SafeDeleteContext phContext, Interop.SspiCli.ContextAttribute contextAttribute, byte[] buffer)
		{
			int result;
			try
			{
				bool flag = false;
				phContext.DangerousAddRef(ref flag);
				result = Interop.SspiCli.SetContextAttributesW(ref phContext._handle, contextAttribute, buffer, buffer.Length);
			}
			finally
			{
				phContext.DangerousRelease();
			}
			return result;
		}
	}
}
