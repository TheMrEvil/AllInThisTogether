using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000CD RID: 205
	internal class MonoBtlsBio : MonoBtlsObject
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x0000CA92 File Offset: 0x0000AC92
		internal MonoBtlsBio(MonoBtlsBio.BoringBioHandle handle) : base(handle)
		{
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000CA9B File Offset: 0x0000AC9B
		protected internal new MonoBtlsBio.BoringBioHandle Handle
		{
			get
			{
				return (MonoBtlsBio.BoringBioHandle)base.Handle;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000CAA8 File Offset: 0x0000ACA8
		public static MonoBtlsBio CreateMonoStream(Stream stream)
		{
			return MonoBtlsBioMono.CreateStream(stream, false);
		}

		// Token: 0x06000409 RID: 1033
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_bio_read(IntPtr bio, IntPtr data, int len);

		// Token: 0x0600040A RID: 1034
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_bio_write(IntPtr bio, IntPtr data, int len);

		// Token: 0x0600040B RID: 1035
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_bio_flush(IntPtr bio);

		// Token: 0x0600040C RID: 1036
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_bio_indent(IntPtr bio, uint indent, uint max_indent);

		// Token: 0x0600040D RID: 1037
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_bio_hexdump(IntPtr bio, IntPtr data, int len, uint indent);

		// Token: 0x0600040E RID: 1038
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_bio_print_errors(IntPtr bio);

		// Token: 0x0600040F RID: 1039
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_bio_free(IntPtr handle);

		// Token: 0x06000410 RID: 1040 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		public int Read(byte[] buffer, int offset, int size)
		{
			base.CheckThrow();
			IntPtr intPtr = Marshal.AllocHGlobal(size);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			bool flag = false;
			int result;
			try
			{
				this.Handle.DangerousAddRef(ref flag);
				int num = MonoBtlsBio.mono_btls_bio_read(this.Handle.DangerousGetHandle(), intPtr, size);
				if (num > 0)
				{
					Marshal.Copy(intPtr, buffer, offset, num);
				}
				result = num;
			}
			finally
			{
				if (flag)
				{
					this.Handle.DangerousRelease();
				}
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000CB3C File Offset: 0x0000AD3C
		public int Write(byte[] buffer, int offset, int size)
		{
			base.CheckThrow();
			IntPtr intPtr = Marshal.AllocHGlobal(size);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			bool flag = false;
			int result;
			try
			{
				this.Handle.DangerousAddRef(ref flag);
				Marshal.Copy(buffer, offset, intPtr, size);
				result = MonoBtlsBio.mono_btls_bio_write(this.Handle.DangerousGetHandle(), intPtr, size);
			}
			finally
			{
				if (flag)
				{
					this.Handle.DangerousRelease();
				}
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		public int Flush()
		{
			base.CheckThrow();
			bool flag = false;
			int result;
			try
			{
				this.Handle.DangerousAddRef(ref flag);
				result = MonoBtlsBio.mono_btls_bio_flush(this.Handle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					this.Handle.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000CC14 File Offset: 0x0000AE14
		public int Indent(uint indent, uint max_indent)
		{
			base.CheckThrow();
			bool flag = false;
			int result;
			try
			{
				this.Handle.DangerousAddRef(ref flag);
				result = MonoBtlsBio.mono_btls_bio_indent(this.Handle.DangerousGetHandle(), indent, max_indent);
			}
			finally
			{
				if (flag)
				{
					this.Handle.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000CC6C File Offset: 0x0000AE6C
		public int HexDump(byte[] buffer, uint indent)
		{
			base.CheckThrow();
			IntPtr intPtr = Marshal.AllocHGlobal(buffer.Length);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			bool flag = false;
			int result;
			try
			{
				this.Handle.DangerousAddRef(ref flag);
				Marshal.Copy(buffer, 0, intPtr, buffer.Length);
				result = MonoBtlsBio.mono_btls_bio_hexdump(this.Handle.DangerousGetHandle(), intPtr, buffer.Length, indent);
			}
			finally
			{
				if (flag)
				{
					this.Handle.DangerousRelease();
				}
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000CCF4 File Offset: 0x0000AEF4
		public void PrintErrors()
		{
			base.CheckThrow();
			bool flag = false;
			try
			{
				this.Handle.DangerousAddRef(ref flag);
				MonoBtlsBio.mono_btls_bio_print_errors(this.Handle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					this.Handle.DangerousRelease();
				}
			}
		}

		// Token: 0x020000CE RID: 206
		protected internal class BoringBioHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x06000416 RID: 1046 RVA: 0x0000CD48 File Offset: 0x0000AF48
			public BoringBioHandle(IntPtr handle) : base(handle, true)
			{
			}

			// Token: 0x06000417 RID: 1047 RVA: 0x0000CD52 File Offset: 0x0000AF52
			protected override bool ReleaseHandle()
			{
				if (this.handle != IntPtr.Zero)
				{
					MonoBtlsBio.mono_btls_bio_free(this.handle);
					this.handle = IntPtr.Zero;
				}
				return true;
			}
		}
	}
}
