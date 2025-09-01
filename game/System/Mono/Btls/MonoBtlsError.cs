using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000D9 RID: 217
	internal static class MonoBtlsError
	{
		// Token: 0x06000467 RID: 1127
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_error_peek_error();

		// Token: 0x06000468 RID: 1128
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_error_get_error();

		// Token: 0x06000469 RID: 1129
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_error_clear_error();

		// Token: 0x0600046A RID: 1130
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_error_peek_error_line(out IntPtr file, out int line);

		// Token: 0x0600046B RID: 1131
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_error_get_error_line(out IntPtr file, out int line);

		// Token: 0x0600046C RID: 1132
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_error_get_error_string_n(int error, IntPtr buf, int len);

		// Token: 0x0600046D RID: 1133
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_error_get_reason(int error);

		// Token: 0x0600046E RID: 1134 RVA: 0x0000DB3D File Offset: 0x0000BD3D
		public static int PeekError()
		{
			return MonoBtlsError.mono_btls_error_peek_error();
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000DB44 File Offset: 0x0000BD44
		public static int GetError()
		{
			return MonoBtlsError.mono_btls_error_get_error();
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000DB4B File Offset: 0x0000BD4B
		public static void ClearError()
		{
			MonoBtlsError.mono_btls_error_clear_error();
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000DB54 File Offset: 0x0000BD54
		public static string GetErrorString(int error)
		{
			int num = 1024;
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			string result;
			try
			{
				MonoBtlsError.mono_btls_error_get_error_string_n(error, intPtr, num);
				result = Marshal.PtrToStringAnsi(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000DBAC File Offset: 0x0000BDAC
		public static int PeekError(out string file, out int line)
		{
			IntPtr intPtr;
			int result = MonoBtlsError.mono_btls_error_peek_error_line(out intPtr, out line);
			if (intPtr != IntPtr.Zero)
			{
				file = Marshal.PtrToStringAnsi(intPtr);
				return result;
			}
			file = null;
			return result;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000DBDC File Offset: 0x0000BDDC
		public static int GetError(out string file, out int line)
		{
			IntPtr intPtr;
			int result = MonoBtlsError.mono_btls_error_get_error_line(out intPtr, out line);
			if (intPtr != IntPtr.Zero)
			{
				file = Marshal.PtrToStringAnsi(intPtr);
				return result;
			}
			file = null;
			return result;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000DC0A File Offset: 0x0000BE0A
		public static int GetErrorReason(int error)
		{
			return MonoBtlsError.mono_btls_error_get_reason(error);
		}
	}
}
