using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000799 RID: 1945
	internal static class UnsafeNativeMethods
	{
		// Token: 0x060044E4 RID: 17636
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int WindowsCreateString(string sourceString, int length, IntPtr* hstring);

		// Token: 0x060044E5 RID: 17637
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int WindowsDeleteString(IntPtr hstring);

		// Token: 0x060044E6 RID: 17638
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern char* WindowsGetStringRawBuffer(IntPtr hstring, uint* length);

		// Token: 0x060044E7 RID: 17639
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool RoOriginateLanguageException(int error, string message, IntPtr languageException);

		// Token: 0x060044E8 RID: 17640
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RoReportUnhandledError(IRestrictedErrorInfo error);

		// Token: 0x060044E9 RID: 17641
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IRestrictedErrorInfo GetRestrictedErrorInfo();
	}
}
