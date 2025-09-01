using System;
using System.Runtime.InteropServices;
using ObjCRuntimeInternal;

namespace Mono
{
	// Token: 0x0200002E RID: 46
	internal class CFBoolean : INativeObject, IDisposable
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00002AAC File Offset: 0x00000CAC
		static CFBoolean()
		{
			IntPtr value = CFObject.dlopen("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", 0);
			if (value == IntPtr.Zero)
			{
				return;
			}
			try
			{
				CFBoolean.True = new CFBoolean(CFObject.GetCFObjectHandle(value, "kCFBooleanTrue"), false);
				CFBoolean.False = new CFBoolean(CFObject.GetCFObjectHandle(value, "kCFBooleanFalse"), false);
			}
			finally
			{
				CFObject.dlclose(value);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002B1C File Offset: 0x00000D1C
		internal CFBoolean(IntPtr handle, bool owns)
		{
			this.handle = handle;
			if (!owns)
			{
				CFObject.CFRetain(handle);
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002B38 File Offset: 0x00000D38
		~CFBoolean()
		{
			this.Dispose(false);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002B68 File Offset: 0x00000D68
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002B70 File Offset: 0x00000D70
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002B7F File Offset: 0x00000D7F
		protected virtual void Dispose(bool disposing)
		{
			if (this.handle != IntPtr.Zero)
			{
				CFObject.CFRelease(this.handle);
				this.handle = IntPtr.Zero;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002BA9 File Offset: 0x00000DA9
		public static implicit operator bool(CFBoolean value)
		{
			return value.Value;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002BB1 File Offset: 0x00000DB1
		public static explicit operator CFBoolean(bool value)
		{
			return CFBoolean.FromBoolean(value);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002BB9 File Offset: 0x00000DB9
		public static CFBoolean FromBoolean(bool value)
		{
			if (!value)
			{
				return CFBoolean.False;
			}
			return CFBoolean.True;
		}

		// Token: 0x06000087 RID: 135
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool CFBooleanGetValue(IntPtr boolean);

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002BC9 File Offset: 0x00000DC9
		public bool Value
		{
			get
			{
				return CFBoolean.CFBooleanGetValue(this.handle);
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002BD6 File Offset: 0x00000DD6
		public static bool GetValue(IntPtr boolean)
		{
			return CFBoolean.CFBooleanGetValue(boolean);
		}

		// Token: 0x04000119 RID: 281
		private IntPtr handle;

		// Token: 0x0400011A RID: 282
		public static readonly CFBoolean True;

		// Token: 0x0400011B RID: 283
		public static readonly CFBoolean False;
	}
}
