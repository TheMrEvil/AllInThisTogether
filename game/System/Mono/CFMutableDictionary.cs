using System;
using System.Runtime.InteropServices;

namespace Mono
{
	// Token: 0x0200002D RID: 45
	internal class CFMutableDictionary : CFDictionary
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00002A5F File Offset: 0x00000C5F
		public CFMutableDictionary(IntPtr handle, bool own) : base(handle, own)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002A69 File Offset: 0x00000C69
		public void SetValue(IntPtr key, IntPtr val)
		{
			CFMutableDictionary.CFDictionarySetValue(base.Handle, key, val);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002A78 File Offset: 0x00000C78
		public static CFMutableDictionary Create()
		{
			IntPtr intPtr = CFMutableDictionary.CFDictionaryCreateMutable(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidOperationException();
			}
			return new CFMutableDictionary(intPtr, true);
		}

		// Token: 0x0600007C RID: 124
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFDictionarySetValue(IntPtr handle, IntPtr key, IntPtr val);

		// Token: 0x0600007D RID: 125
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDictionaryCreateMutable(IntPtr allocator, IntPtr capacity, IntPtr keyCallback, IntPtr valueCallbacks);
	}
}
