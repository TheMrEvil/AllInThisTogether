using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Mono
{
	// Token: 0x0200002C RID: 44
	internal class CFDictionary : CFObject
	{
		// Token: 0x0600006D RID: 109 RVA: 0x000028FC File Offset: 0x00000AFC
		static CFDictionary()
		{
			IntPtr intPtr = CFObject.dlopen("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", 0);
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			try
			{
				CFDictionary.KeyCallbacks = CFObject.GetIndirect(intPtr, "kCFTypeDictionaryKeyCallBacks");
				CFDictionary.ValueCallbacks = CFObject.GetIndirect(intPtr, "kCFTypeDictionaryValueCallBacks");
			}
			finally
			{
				CFObject.dlclose(intPtr);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002474 File Offset: 0x00000674
		public CFDictionary(IntPtr handle, bool own) : base(handle, own)
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002960 File Offset: 0x00000B60
		public static CFDictionary FromObjectAndKey(IntPtr obj, IntPtr key)
		{
			return new CFDictionary(CFDictionary.CFDictionaryCreate(IntPtr.Zero, new IntPtr[]
			{
				key
			}, new IntPtr[]
			{
				obj
			}, (IntPtr)1, CFDictionary.KeyCallbacks, CFDictionary.ValueCallbacks), true);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002998 File Offset: 0x00000B98
		public static CFDictionary FromKeysAndObjects(IList<Tuple<IntPtr, IntPtr>> items)
		{
			IntPtr[] array = new IntPtr[items.Count];
			IntPtr[] array2 = new IntPtr[items.Count];
			for (int i = 0; i < items.Count; i++)
			{
				array[i] = items[i].Item1;
				array2[i] = items[i].Item2;
			}
			return new CFDictionary(CFDictionary.CFDictionaryCreate(IntPtr.Zero, array, array2, (IntPtr)items.Count, CFDictionary.KeyCallbacks, CFDictionary.ValueCallbacks), true);
		}

		// Token: 0x06000071 RID: 113
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDictionaryCreate(IntPtr allocator, IntPtr[] keys, IntPtr[] vals, IntPtr len, IntPtr keyCallbacks, IntPtr valCallbacks);

		// Token: 0x06000072 RID: 114
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDictionaryGetValue(IntPtr handle, IntPtr key);

		// Token: 0x06000073 RID: 115
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDictionaryCreateCopy(IntPtr allocator, IntPtr handle);

		// Token: 0x06000074 RID: 116 RVA: 0x00002A13 File Offset: 0x00000C13
		public CFDictionary Copy()
		{
			return new CFDictionary(CFDictionary.CFDictionaryCreateCopy(IntPtr.Zero, base.Handle), true);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002A2B File Offset: 0x00000C2B
		public CFMutableDictionary MutableCopy()
		{
			return new CFMutableDictionary(CFDictionary.CFDictionaryCreateMutableCopy(IntPtr.Zero, IntPtr.Zero, base.Handle), true);
		}

		// Token: 0x06000076 RID: 118
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDictionaryCreateMutableCopy(IntPtr allocator, IntPtr capacity, IntPtr theDict);

		// Token: 0x06000077 RID: 119 RVA: 0x00002A48 File Offset: 0x00000C48
		public IntPtr GetValue(IntPtr key)
		{
			return CFDictionary.CFDictionaryGetValue(base.Handle, key);
		}

		// Token: 0x1700000C RID: 12
		public IntPtr this[IntPtr key]
		{
			get
			{
				return this.GetValue(key);
			}
		}

		// Token: 0x04000117 RID: 279
		private static readonly IntPtr KeyCallbacks;

		// Token: 0x04000118 RID: 280
		private static readonly IntPtr ValueCallbacks;
	}
}
