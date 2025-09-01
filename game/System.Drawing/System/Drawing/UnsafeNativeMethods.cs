using System;
using System.Internal;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x02000043 RID: 67
	internal class UnsafeNativeMethods
	{
		// Token: 0x0600015C RID: 348
		[DllImport("kernel32", CharSet = CharSet.Auto, EntryPoint = "RtlMoveMemory", ExactSpelling = true, SetLastError = true)]
		public static extern void CopyMemory(HandleRef destData, HandleRef srcData, int size);

		// Token: 0x0600015D RID: 349
		[DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "GetDC", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntGetDC(HandleRef hWnd);

		// Token: 0x0600015E RID: 350 RVA: 0x00006155 File Offset: 0x00004355
		public static IntPtr GetDC(HandleRef hWnd)
		{
			return System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntGetDC(hWnd), SafeNativeMethods.CommonHandles.HDC);
		}

		// Token: 0x0600015F RID: 351
		[DllImport("gdi32", CharSet = CharSet.Auto, EntryPoint = "DeleteDC", ExactSpelling = true, SetLastError = true)]
		private static extern bool IntDeleteDC(HandleRef hDC);

		// Token: 0x06000160 RID: 352 RVA: 0x00006167 File Offset: 0x00004367
		public static bool DeleteDC(HandleRef hDC)
		{
			System.Internal.HandleCollector.Remove((IntPtr)hDC, SafeNativeMethods.CommonHandles.GDI);
			return UnsafeNativeMethods.IntDeleteDC(hDC);
		}

		// Token: 0x06000161 RID: 353
		[DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "ReleaseDC", ExactSpelling = true, SetLastError = true)]
		private static extern int IntReleaseDC(HandleRef hWnd, HandleRef hDC);

		// Token: 0x06000162 RID: 354 RVA: 0x00006180 File Offset: 0x00004380
		public static int ReleaseDC(HandleRef hWnd, HandleRef hDC)
		{
			System.Internal.HandleCollector.Remove((IntPtr)hDC, SafeNativeMethods.CommonHandles.HDC);
			return UnsafeNativeMethods.IntReleaseDC(hWnd, hDC);
		}

		// Token: 0x06000163 RID: 355
		[DllImport("gdi32", CharSet = CharSet.Auto, EntryPoint = "CreateCompatibleDC", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreateCompatibleDC(HandleRef hDC);

		// Token: 0x06000164 RID: 356 RVA: 0x0000619A File Offset: 0x0000439A
		public static IntPtr CreateCompatibleDC(HandleRef hDC)
		{
			return System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntCreateCompatibleDC(hDC), SafeNativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06000165 RID: 357
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetStockObject(int nIndex);

		// Token: 0x06000166 RID: 358
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetSystemDefaultLCID();

		// Token: 0x06000167 RID: 359
		[DllImport("user32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetSystemMetrics(int nIndex);

		// Token: 0x06000168 RID: 360
		[DllImport("user32", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SystemParametersInfo(int uiAction, int uiParam, [In] [Out] NativeMethods.NONCLIENTMETRICS pvParam, int fWinIni);

		// Token: 0x06000169 RID: 361
		[DllImport("user32", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SystemParametersInfo(int uiAction, int uiParam, [In] [Out] SafeNativeMethods.LOGFONT pvParam, int fWinIni);

		// Token: 0x0600016A RID: 362
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetDeviceCaps(HandleRef hDC, int nIndex);

		// Token: 0x0600016B RID: 363
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetObjectType(HandleRef hObject);

		// Token: 0x0600016C RID: 364 RVA: 0x00002050 File Offset: 0x00000250
		public UnsafeNativeMethods()
		{
		}

		// Token: 0x02000044 RID: 68
		[Guid("0000000C-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IStream
		{
			// Token: 0x0600016D RID: 365
			int Read([In] IntPtr buf, [In] int len);

			// Token: 0x0600016E RID: 366
			int Write([In] IntPtr buf, [In] int len);

			// Token: 0x0600016F RID: 367
			[return: MarshalAs(UnmanagedType.I8)]
			long Seek([MarshalAs(UnmanagedType.I8)] [In] long dlibMove, [In] int dwOrigin);

			// Token: 0x06000170 RID: 368
			void SetSize([MarshalAs(UnmanagedType.I8)] [In] long libNewSize);

			// Token: 0x06000171 RID: 369
			[return: MarshalAs(UnmanagedType.I8)]
			long CopyTo([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IStream pstm, [MarshalAs(UnmanagedType.I8)] [In] long cb, [MarshalAs(UnmanagedType.LPArray)] [Out] long[] pcbRead);

			// Token: 0x06000172 RID: 370
			void Commit([In] int grfCommitFlags);

			// Token: 0x06000173 RID: 371
			void Revert();

			// Token: 0x06000174 RID: 372
			void LockRegion([MarshalAs(UnmanagedType.I8)] [In] long libOffset, [MarshalAs(UnmanagedType.I8)] [In] long cb, [In] int dwLockType);

			// Token: 0x06000175 RID: 373
			void UnlockRegion([MarshalAs(UnmanagedType.I8)] [In] long libOffset, [MarshalAs(UnmanagedType.I8)] [In] long cb, [In] int dwLockType);

			// Token: 0x06000176 RID: 374
			void Stat([In] IntPtr pStatstg, [In] int grfStatFlag);

			// Token: 0x06000177 RID: 375
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IStream Clone();
		}

		// Token: 0x02000045 RID: 69
		internal class ComStreamFromDataStream : UnsafeNativeMethods.IStream
		{
			// Token: 0x06000178 RID: 376 RVA: 0x000061AC File Offset: 0x000043AC
			internal ComStreamFromDataStream(Stream dataStream)
			{
				if (dataStream == null)
				{
					throw new ArgumentNullException("dataStream");
				}
				this.dataStream = dataStream;
			}

			// Token: 0x06000179 RID: 377 RVA: 0x000061D4 File Offset: 0x000043D4
			private void ActualizeVirtualPosition()
			{
				if (this._virtualPosition == -1L)
				{
					return;
				}
				if (this._virtualPosition > this.dataStream.Length)
				{
					this.dataStream.SetLength(this._virtualPosition);
				}
				this.dataStream.Position = this._virtualPosition;
				this._virtualPosition = -1L;
			}

			// Token: 0x0600017A RID: 378 RVA: 0x00006229 File Offset: 0x00004429
			public virtual UnsafeNativeMethods.IStream Clone()
			{
				UnsafeNativeMethods.ComStreamFromDataStream.NotImplemented();
				return null;
			}

			// Token: 0x0600017B RID: 379 RVA: 0x00006231 File Offset: 0x00004431
			public virtual void Commit(int grfCommitFlags)
			{
				this.dataStream.Flush();
				this.ActualizeVirtualPosition();
			}

			// Token: 0x0600017C RID: 380 RVA: 0x00006244 File Offset: 0x00004444
			public virtual long CopyTo(UnsafeNativeMethods.IStream pstm, long cb, long[] pcbRead)
			{
				int num = 4096;
				IntPtr intPtr = Marshal.AllocHGlobal(num);
				if (intPtr == IntPtr.Zero)
				{
					throw new OutOfMemoryException();
				}
				long num2 = 0L;
				try
				{
					while (num2 < cb)
					{
						int num3 = num;
						if (num2 + (long)num3 > cb)
						{
							num3 = (int)(cb - num2);
						}
						int num4 = this.Read(intPtr, num3);
						if (num4 == 0)
						{
							break;
						}
						if (pstm.Write(intPtr, num4) != num4)
						{
							throw UnsafeNativeMethods.ComStreamFromDataStream.EFail("Wrote an incorrect number of bytes");
						}
						num2 += (long)num4;
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (pcbRead != null && pcbRead.Length != 0)
				{
					pcbRead[0] = num2;
				}
				return num2;
			}

			// Token: 0x0600017D RID: 381 RVA: 0x000049FE File Offset: 0x00002BFE
			public virtual void LockRegion(long libOffset, long cb, int dwLockType)
			{
			}

			// Token: 0x0600017E RID: 382 RVA: 0x000062DC File Offset: 0x000044DC
			protected static ExternalException EFail(string msg)
			{
				throw new ExternalException(msg, -2147467259);
			}

			// Token: 0x0600017F RID: 383 RVA: 0x000062E9 File Offset: 0x000044E9
			protected static void NotImplemented()
			{
				throw new ExternalException(SR.Format("Not implemented.", Array.Empty<object>()), -2147467263);
			}

			// Token: 0x06000180 RID: 384 RVA: 0x00006304 File Offset: 0x00004504
			public virtual int Read(IntPtr buf, int length)
			{
				byte[] array = new byte[length];
				int result = this.Read(array, length);
				Marshal.Copy(array, 0, buf, length);
				return result;
			}

			// Token: 0x06000181 RID: 385 RVA: 0x00006329 File Offset: 0x00004529
			public virtual int Read(byte[] buffer, int length)
			{
				this.ActualizeVirtualPosition();
				return this.dataStream.Read(buffer, 0, length);
			}

			// Token: 0x06000182 RID: 386 RVA: 0x0000633F File Offset: 0x0000453F
			public virtual void Revert()
			{
				UnsafeNativeMethods.ComStreamFromDataStream.NotImplemented();
			}

			// Token: 0x06000183 RID: 387 RVA: 0x00006348 File Offset: 0x00004548
			public virtual long Seek(long offset, int origin)
			{
				long num = this._virtualPosition;
				if (this._virtualPosition == -1L)
				{
					num = this.dataStream.Position;
				}
				long length = this.dataStream.Length;
				switch (origin)
				{
				case 0:
					if (offset <= length)
					{
						this.dataStream.Position = offset;
						this._virtualPosition = -1L;
					}
					else
					{
						this._virtualPosition = offset;
					}
					break;
				case 1:
					if (offset + num <= length)
					{
						this.dataStream.Position = num + offset;
						this._virtualPosition = -1L;
					}
					else
					{
						this._virtualPosition = offset + num;
					}
					break;
				case 2:
					if (offset <= 0L)
					{
						this.dataStream.Position = length + offset;
						this._virtualPosition = -1L;
					}
					else
					{
						this._virtualPosition = length + offset;
					}
					break;
				}
				if (this._virtualPosition != -1L)
				{
					return this._virtualPosition;
				}
				return this.dataStream.Position;
			}

			// Token: 0x06000184 RID: 388 RVA: 0x00006420 File Offset: 0x00004620
			public virtual void SetSize(long value)
			{
				this.dataStream.SetLength(value);
			}

			// Token: 0x06000185 RID: 389 RVA: 0x0000633F File Offset: 0x0000453F
			public virtual void Stat(IntPtr pstatstg, int grfStatFlag)
			{
				UnsafeNativeMethods.ComStreamFromDataStream.NotImplemented();
			}

			// Token: 0x06000186 RID: 390 RVA: 0x000049FE File Offset: 0x00002BFE
			public virtual void UnlockRegion(long libOffset, long cb, int dwLockType)
			{
			}

			// Token: 0x06000187 RID: 391 RVA: 0x00006430 File Offset: 0x00004630
			public virtual int Write(IntPtr buf, int length)
			{
				byte[] array = new byte[length];
				Marshal.Copy(buf, array, 0, length);
				return this.Write(array, length);
			}

			// Token: 0x06000188 RID: 392 RVA: 0x00006455 File Offset: 0x00004655
			public virtual int Write(byte[] buffer, int length)
			{
				this.ActualizeVirtualPosition();
				this.dataStream.Write(buffer, 0, length);
				return length;
			}

			// Token: 0x0400037B RID: 891
			protected Stream dataStream;

			// Token: 0x0400037C RID: 892
			private long _virtualPosition = -1L;
		}
	}
}
