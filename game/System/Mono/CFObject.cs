using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ObjCRuntimeInternal;

namespace Mono
{
	// Token: 0x02000026 RID: 38
	internal class CFObject : IDisposable, INativeObject
	{
		// Token: 0x0600002E RID: 46
		[DllImport("/usr/lib/libSystem.dylib")]
		public static extern IntPtr dlopen(string path, int mode);

		// Token: 0x0600002F RID: 47
		[DllImport("/usr/lib/libSystem.dylib")]
		private static extern IntPtr dlsym(IntPtr handle, string symbol);

		// Token: 0x06000030 RID: 48
		[DllImport("/usr/lib/libSystem.dylib")]
		public static extern void dlclose(IntPtr handle);

		// Token: 0x06000031 RID: 49 RVA: 0x0000231D File Offset: 0x0000051D
		public static IntPtr GetIndirect(IntPtr handle, string symbol)
		{
			return CFObject.dlsym(handle, symbol);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002328 File Offset: 0x00000528
		public static CFString GetStringConstant(IntPtr handle, string symbol)
		{
			IntPtr intPtr = CFObject.dlsym(handle, symbol);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			IntPtr intPtr2 = Marshal.ReadIntPtr(intPtr);
			if (intPtr2 == IntPtr.Zero)
			{
				return null;
			}
			return new CFString(intPtr2, false);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000236C File Offset: 0x0000056C
		public static IntPtr GetIntPtr(IntPtr handle, string symbol)
		{
			IntPtr intPtr = CFObject.dlsym(handle, symbol);
			if (intPtr == IntPtr.Zero)
			{
				return IntPtr.Zero;
			}
			return Marshal.ReadIntPtr(intPtr);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000239C File Offset: 0x0000059C
		public static IntPtr GetCFObjectHandle(IntPtr handle, string symbol)
		{
			IntPtr intPtr = CFObject.dlsym(handle, symbol);
			if (intPtr == IntPtr.Zero)
			{
				return IntPtr.Zero;
			}
			return Marshal.ReadIntPtr(intPtr);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000023CA File Offset: 0x000005CA
		public CFObject(IntPtr handle, bool own)
		{
			this.Handle = handle;
			if (!own)
			{
				this.Retain();
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000023E4 File Offset: 0x000005E4
		~CFObject()
		{
			this.Dispose(false);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002414 File Offset: 0x00000614
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000241C File Offset: 0x0000061C
		public IntPtr Handle
		{
			[CompilerGenerated]
			get
			{
				return this.<Handle>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Handle>k__BackingField = value;
			}
		}

		// Token: 0x06000039 RID: 57
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		internal static extern IntPtr CFRetain(IntPtr handle);

		// Token: 0x0600003A RID: 58 RVA: 0x00002425 File Offset: 0x00000625
		private void Retain()
		{
			CFObject.CFRetain(this.Handle);
		}

		// Token: 0x0600003B RID: 59
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		internal static extern void CFRelease(IntPtr handle);

		// Token: 0x0600003C RID: 60 RVA: 0x00002433 File Offset: 0x00000633
		private void Release()
		{
			CFObject.CFRelease(this.Handle);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002440 File Offset: 0x00000640
		protected virtual void Dispose(bool disposing)
		{
			if (this.Handle != IntPtr.Zero)
			{
				this.Release();
				this.Handle = IntPtr.Zero;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002465 File Offset: 0x00000665
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000110 RID: 272
		public const string CoreFoundationLibrary = "/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation";

		// Token: 0x04000111 RID: 273
		private const string SystemLibrary = "/usr/lib/libSystem.dylib";

		// Token: 0x04000112 RID: 274
		[CompilerGenerated]
		private IntPtr <Handle>k__BackingField;
	}
}
