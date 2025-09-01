using System;
using System.Runtime.InteropServices;

namespace System.Data
{
	// Token: 0x02000150 RID: 336
	internal static class SafeNativeMethods
	{
		// Token: 0x06001203 RID: 4611 RVA: 0x000552BD File Offset: 0x000534BD
		internal static IntPtr LocalAlloc(IntPtr initialSize)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(initialSize);
			SafeNativeMethods.ZeroMemory(intPtr, (int)initialSize);
			return intPtr;
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x000552D1 File Offset: 0x000534D1
		internal static void LocalFree(IntPtr ptr)
		{
			Marshal.FreeHGlobal(ptr);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x000552D9 File Offset: 0x000534D9
		internal static void ZeroMemory(IntPtr ptr, int length)
		{
			Marshal.Copy(new byte[length], 0, ptr, length);
		}

		// Token: 0x06001206 RID: 4614
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
		internal static extern IntPtr GetProcAddress(IntPtr HModule, [MarshalAs(UnmanagedType.LPStr)] [In] string funcName);
	}
}
