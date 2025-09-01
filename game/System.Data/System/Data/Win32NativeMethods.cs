using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace System.Data
{
	// Token: 0x02000151 RID: 337
	internal static class Win32NativeMethods
	{
		// Token: 0x06001207 RID: 4615 RVA: 0x000552EC File Offset: 0x000534EC
		internal static bool IsTokenRestrictedWrapper(IntPtr token)
		{
			bool result;
			uint num = SNINativeMethodWrapper.UnmanagedIsTokenRestricted(token, out result);
			if (num != 0U)
			{
				Marshal.ThrowExceptionForHR((int)num);
			}
			return result;
		}
	}
}
