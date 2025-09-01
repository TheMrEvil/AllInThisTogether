using System;
using System.Runtime.InteropServices;

namespace Mono
{
	// Token: 0x02000028 RID: 40
	internal class CFNumber : CFObject
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002474 File Offset: 0x00000674
		public CFNumber(IntPtr handle, bool own) : base(handle, own)
		{
		}

		// Token: 0x0600004D RID: 77
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool CFNumberGetValue(IntPtr handle, IntPtr type, [MarshalAs(UnmanagedType.I1)] out bool value);

		// Token: 0x0600004E RID: 78 RVA: 0x00002650 File Offset: 0x00000850
		public static bool AsBool(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return false;
			}
			bool result;
			CFNumber.CFNumberGetValue(handle, (IntPtr)1, out result);
			return result;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000267C File Offset: 0x0000087C
		public static implicit operator bool(CFNumber number)
		{
			return CFNumber.AsBool(number.Handle);
		}

		// Token: 0x06000050 RID: 80
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool CFNumberGetValue(IntPtr handle, IntPtr type, out int value);

		// Token: 0x06000051 RID: 81 RVA: 0x0000268C File Offset: 0x0000088C
		public static int AsInt32(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return 0;
			}
			int result;
			CFNumber.CFNumberGetValue(handle, (IntPtr)9, out result);
			return result;
		}

		// Token: 0x06000052 RID: 82
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFNumberCreate(IntPtr allocator, IntPtr theType, IntPtr valuePtr);

		// Token: 0x06000053 RID: 83 RVA: 0x000026B9 File Offset: 0x000008B9
		public static CFNumber FromInt32(int number)
		{
			return new CFNumber(CFNumber.CFNumberCreate(IntPtr.Zero, (IntPtr)9, (IntPtr)number), true);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000026D8 File Offset: 0x000008D8
		public static implicit operator int(CFNumber number)
		{
			return CFNumber.AsInt32(number.Handle);
		}
	}
}
