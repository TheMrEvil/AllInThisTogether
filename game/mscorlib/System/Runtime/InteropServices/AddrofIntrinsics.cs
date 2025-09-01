using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000781 RID: 1921
	internal static class AddrofIntrinsics
	{
		// Token: 0x0600447A RID: 17530 RVA: 0x000E3AAE File Offset: 0x000E1CAE
		internal static IntPtr AddrOf<T>(T ftn)
		{
			return Marshal.GetFunctionPointerForDelegate<T>(ftn);
		}
	}
}
