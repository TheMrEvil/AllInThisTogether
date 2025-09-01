using System;
using System.Runtime.InteropServices;
using System.Threading;
using Mono.Util;

namespace System.IO.Compression
{
	// Token: 0x02000549 RID: 1353
	internal class DeflateStreamNative
	{
		// Token: 0x06002C06 RID: 11270 RVA: 0x0000219B File Offset: 0x0000039B
		private DeflateStreamNative()
		{
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x00095E58 File Offset: 0x00094058
		public static DeflateStreamNative Create(Stream compressedStream, CompressionMode mode, bool gzip)
		{
			DeflateStreamNative deflateStreamNative = new DeflateStreamNative();
			deflateStreamNative.data = GCHandle.Alloc(deflateStreamNative);
			deflateStreamNative.feeder = ((mode == CompressionMode.Compress) ? new DeflateStreamNative.UnmanagedReadOrWrite(DeflateStreamNative.UnmanagedWrite) : new DeflateStreamNative.UnmanagedReadOrWrite(DeflateStreamNative.UnmanagedRead));
			deflateStreamNative.z_stream = DeflateStreamNative.CreateZStream(mode, gzip, deflateStreamNative.feeder, GCHandle.ToIntPtr(deflateStreamNative.data));
			if (deflateStreamNative.z_stream.IsInvalid)
			{
				deflateStreamNative.Dispose(true);
				return null;
			}
			deflateStreamNative.base_stream = compressedStream;
			return deflateStreamNative;
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x00095ED8 File Offset: 0x000940D8
		~DeflateStreamNative()
		{
			this.Dispose(false);
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x00095F08 File Offset: 0x00094108
		public void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				GC.SuppressFinalize(this);
			}
			else
			{
				this.base_stream = Stream.Null;
			}
			this.io_buffer = null;
			if (this.z_stream != null && !this.z_stream.IsInvalid)
			{
				this.z_stream.Dispose();
			}
			GCHandle gchandle = this.data;
			if (this.data.IsAllocated)
			{
				this.data.Free();
			}
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x00095F80 File Offset: 0x00094180
		public void Flush()
		{
			int result = DeflateStreamNative.Flush(this.z_stream);
			this.CheckResult(result, "Flush");
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x00095FA8 File Offset: 0x000941A8
		public int ReadZStream(IntPtr buffer, int length)
		{
			int result = DeflateStreamNative.ReadZStream(this.z_stream, buffer, length);
			this.CheckResult(result, "ReadInternal");
			return result;
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x00095FD0 File Offset: 0x000941D0
		public void WriteZStream(IntPtr buffer, int length)
		{
			int result = DeflateStreamNative.WriteZStream(this.z_stream, buffer, length);
			this.CheckResult(result, "WriteInternal");
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x00095FF8 File Offset: 0x000941F8
		[MonoPInvokeCallback(typeof(DeflateStreamNative.UnmanagedReadOrWrite))]
		private static int UnmanagedRead(IntPtr buffer, int length, IntPtr data)
		{
			DeflateStreamNative deflateStreamNative = GCHandle.FromIntPtr(data).Target as DeflateStreamNative;
			if (deflateStreamNative == null)
			{
				return -1;
			}
			return deflateStreamNative.UnmanagedRead(buffer, length);
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x00096028 File Offset: 0x00094228
		private int UnmanagedRead(IntPtr buffer, int length)
		{
			if (this.io_buffer == null)
			{
				this.io_buffer = new byte[4096];
			}
			int count = Math.Min(length, this.io_buffer.Length);
			int num;
			try
			{
				num = this.base_stream.Read(this.io_buffer, 0, count);
			}
			catch (Exception ex)
			{
				this.last_error = ex;
				return -12;
			}
			if (num > 0)
			{
				Marshal.Copy(this.io_buffer, 0, buffer, num);
			}
			return num;
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x000960A4 File Offset: 0x000942A4
		[MonoPInvokeCallback(typeof(DeflateStreamNative.UnmanagedReadOrWrite))]
		private static int UnmanagedWrite(IntPtr buffer, int length, IntPtr data)
		{
			DeflateStreamNative deflateStreamNative = GCHandle.FromIntPtr(data).Target as DeflateStreamNative;
			if (deflateStreamNative == null)
			{
				return -1;
			}
			return deflateStreamNative.UnmanagedWrite(buffer, length);
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x000960D4 File Offset: 0x000942D4
		private unsafe int UnmanagedWrite(IntPtr buffer, int length)
		{
			int num = 0;
			while (length > 0)
			{
				if (this.io_buffer == null)
				{
					this.io_buffer = new byte[4096];
				}
				int num2 = Math.Min(length, this.io_buffer.Length);
				Marshal.Copy(buffer, this.io_buffer, 0, num2);
				try
				{
					this.base_stream.Write(this.io_buffer, 0, num2);
				}
				catch (Exception ex)
				{
					this.last_error = ex;
					return -12;
				}
				buffer = new IntPtr((void*)((byte*)buffer.ToPointer() + num2));
				length -= num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x0009616C File Offset: 0x0009436C
		private void CheckResult(int result, string where)
		{
			if (result >= 0)
			{
				return;
			}
			Exception ex = Interlocked.Exchange<Exception>(ref this.last_error, null);
			if (ex != null)
			{
				throw ex;
			}
			string str;
			switch (result)
			{
			case -11:
				str = "IO error";
				goto IL_94;
			case -10:
				str = "Invalid argument(s)";
				goto IL_94;
			case -6:
				str = "Invalid version";
				goto IL_94;
			case -5:
				str = "Internal error (no progress possible)";
				goto IL_94;
			case -4:
				str = "Not enough memory";
				goto IL_94;
			case -3:
				str = "Corrupted data";
				goto IL_94;
			case -2:
				str = "Internal error";
				goto IL_94;
			case -1:
				str = "Unknown error";
				goto IL_94;
			}
			str = "Unknown error";
			IL_94:
			throw new IOException(str + " " + where);
		}

		// Token: 0x06002C12 RID: 11282
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern DeflateStreamNative.SafeDeflateStreamHandle CreateZStream(CompressionMode compress, bool gzip, DeflateStreamNative.UnmanagedReadOrWrite feeder, IntPtr data);

		// Token: 0x06002C13 RID: 11283
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int CloseZStream(IntPtr stream);

		// Token: 0x06002C14 RID: 11284
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int Flush(DeflateStreamNative.SafeDeflateStreamHandle stream);

		// Token: 0x06002C15 RID: 11285
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int ReadZStream(DeflateStreamNative.SafeDeflateStreamHandle stream, IntPtr buffer, int length);

		// Token: 0x06002C16 RID: 11286
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int WriteZStream(DeflateStreamNative.SafeDeflateStreamHandle stream, IntPtr buffer, int length);

		// Token: 0x040017B1 RID: 6065
		private const int BufferSize = 4096;

		// Token: 0x040017B2 RID: 6066
		private DeflateStreamNative.UnmanagedReadOrWrite feeder;

		// Token: 0x040017B3 RID: 6067
		private Stream base_stream;

		// Token: 0x040017B4 RID: 6068
		private DeflateStreamNative.SafeDeflateStreamHandle z_stream;

		// Token: 0x040017B5 RID: 6069
		private GCHandle data;

		// Token: 0x040017B6 RID: 6070
		private bool disposed;

		// Token: 0x040017B7 RID: 6071
		private byte[] io_buffer;

		// Token: 0x040017B8 RID: 6072
		private Exception last_error;

		// Token: 0x0200054A RID: 1354
		// (Invoke) Token: 0x06002C18 RID: 11288
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int UnmanagedReadOrWrite(IntPtr buffer, int length, IntPtr data);

		// Token: 0x0200054B RID: 1355
		private sealed class SafeDeflateStreamHandle : SafeHandle
		{
			// Token: 0x170008FB RID: 2299
			// (get) Token: 0x06002C1B RID: 11291 RVA: 0x0000DF6C File Offset: 0x0000C16C
			public override bool IsInvalid
			{
				get
				{
					return this.handle == IntPtr.Zero;
				}
			}

			// Token: 0x06002C1C RID: 11292 RVA: 0x0000DF54 File Offset: 0x0000C154
			private SafeDeflateStreamHandle() : base(IntPtr.Zero, true)
			{
			}

			// Token: 0x06002C1D RID: 11293 RVA: 0x0009621E File Offset: 0x0009441E
			internal SafeDeflateStreamHandle(IntPtr handle) : base(handle, true)
			{
			}

			// Token: 0x06002C1E RID: 11294 RVA: 0x00096228 File Offset: 0x00094428
			protected override bool ReleaseHandle()
			{
				try
				{
					DeflateStreamNative.CloseZStream(this.handle);
				}
				catch
				{
				}
				return true;
			}
		}
	}
}
