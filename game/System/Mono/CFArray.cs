using System;
using System.Runtime.InteropServices;
using ObjCRuntimeInternal;

namespace Mono
{
	// Token: 0x02000027 RID: 39
	internal class CFArray : CFObject
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002474 File Offset: 0x00000674
		public CFArray(IntPtr handle, bool own) : base(handle, own)
		{
		}

		// Token: 0x06000040 RID: 64
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFArrayCreate(IntPtr allocator, IntPtr values, IntPtr numValues, IntPtr callbacks);

		// Token: 0x06000041 RID: 65 RVA: 0x00002480 File Offset: 0x00000680
		static CFArray()
		{
			IntPtr intPtr = CFObject.dlopen("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", 0);
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			try
			{
				CFArray.kCFTypeArrayCallbacks = CFObject.GetIndirect(intPtr, "kCFTypeArrayCallBacks");
			}
			finally
			{
				CFObject.dlclose(intPtr);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000024D4 File Offset: 0x000006D4
		public static CFArray FromNativeObjects(params INativeObject[] values)
		{
			return new CFArray(CFArray.Create(values), true);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000024E4 File Offset: 0x000006E4
		public unsafe static IntPtr Create(params IntPtr[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			IntPtr* value;
			if (values == null || values.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &values[0];
			}
			return CFArray.CFArrayCreate(IntPtr.Zero, (IntPtr)((void*)value), (IntPtr)values.Length, CFArray.kCFTypeArrayCallbacks);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002534 File Offset: 0x00000734
		internal unsafe static CFArray CreateArray(params IntPtr[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			IntPtr* value;
			if (values == null || values.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &values[0];
			}
			return new CFArray(CFArray.CFArrayCreate(IntPtr.Zero, (IntPtr)((void*)value), (IntPtr)values.Length, CFArray.kCFTypeArrayCallbacks), false);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000024D4 File Offset: 0x000006D4
		public static CFArray CreateArray(params INativeObject[] values)
		{
			return new CFArray(CFArray.Create(values), true);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000258C File Offset: 0x0000078C
		public static IntPtr Create(params INativeObject[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			IntPtr[] array = new IntPtr[values.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = values[i].Handle;
			}
			return CFArray.Create(array);
		}

		// Token: 0x06000047 RID: 71
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFArrayGetCount(IntPtr handle);

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000025CF File Offset: 0x000007CF
		public int Count
		{
			get
			{
				return (int)CFArray.CFArrayGetCount(base.Handle);
			}
		}

		// Token: 0x06000049 RID: 73
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFArrayGetValueAtIndex(IntPtr handle, IntPtr index);

		// Token: 0x17000007 RID: 7
		public IntPtr this[int index]
		{
			get
			{
				return CFArray.CFArrayGetValueAtIndex(base.Handle, (IntPtr)index);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000025F4 File Offset: 0x000007F4
		public static T[] ArrayFromHandle<T>(IntPtr handle, Func<IntPtr, T> creation) where T : class, INativeObject
		{
			if (handle == IntPtr.Zero)
			{
				return null;
			}
			IntPtr value = CFArray.CFArrayGetCount(handle);
			T[] array = new T[(int)value];
			for (uint num = 0U; num < (uint)((int)value); num += 1U)
			{
				array[(int)num] = creation(CFArray.CFArrayGetValueAtIndex(handle, (IntPtr)((long)((ulong)num))));
			}
			return array;
		}

		// Token: 0x04000113 RID: 275
		private static readonly IntPtr kCFTypeArrayCallbacks;
	}
}
