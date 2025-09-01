using System;
using System.Runtime.InteropServices;
using ObjCRuntimeInternal;

namespace Mono
{
	// Token: 0x0200002F RID: 47
	internal class CFDate : INativeObject, IDisposable
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00002BDE File Offset: 0x00000DDE
		internal CFDate(IntPtr handle, bool owns)
		{
			this.handle = handle;
			if (!owns)
			{
				CFObject.CFRetain(handle);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002BF8 File Offset: 0x00000DF8
		~CFDate()
		{
			this.Dispose(false);
		}

		// Token: 0x0600008C RID: 140
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDateCreate(IntPtr allocator, double at);

		// Token: 0x0600008D RID: 141 RVA: 0x00002C28 File Offset: 0x00000E28
		public static CFDate Create(DateTime date)
		{
			DateTime d = new DateTime(2001, 1, 1);
			double totalSeconds = (date - d).TotalSeconds;
			IntPtr value = CFDate.CFDateCreate(IntPtr.Zero, totalSeconds);
			if (value == IntPtr.Zero)
			{
				throw new NotSupportedException();
			}
			return new CFDate(value, true);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002C77 File Offset: 0x00000E77
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002C7F File Offset: 0x00000E7F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002C8E File Offset: 0x00000E8E
		protected virtual void Dispose(bool disposing)
		{
			if (this.handle != IntPtr.Zero)
			{
				CFObject.CFRelease(this.handle);
				this.handle = IntPtr.Zero;
			}
		}

		// Token: 0x0400011C RID: 284
		private IntPtr handle;
	}
}
