using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.IO.Ports
{
	// Token: 0x0200053A RID: 1338
	internal class WinSerialStream : Stream, ISerialStream, IDisposable
	{
		// Token: 0x06002B30 RID: 11056
		[DllImport("kernel32", SetLastError = true)]
		private static extern int CreateFile(string port_name, uint desired_access, uint share_mode, uint security_attrs, uint creation, uint flags, uint template);

		// Token: 0x06002B31 RID: 11057
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool SetupComm(int handle, int read_buffer_size, int write_buffer_size);

		// Token: 0x06002B32 RID: 11058
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool PurgeComm(int handle, uint flags);

		// Token: 0x06002B33 RID: 11059
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool SetCommTimeouts(int handle, Timeouts timeouts);

		// Token: 0x06002B34 RID: 11060 RVA: 0x00093EF4 File Offset: 0x000920F4
		public WinSerialStream(string port_name, int baud_rate, int data_bits, Parity parity, StopBits sb, bool dtr_enable, bool rts_enable, Handshake hs, int read_timeout, int write_timeout, int read_buffer_size, int write_buffer_size)
		{
			this.handle = WinSerialStream.CreateFile((port_name != null && !port_name.StartsWith("\\\\.\\")) ? ("\\\\.\\" + port_name) : port_name, 3221225472U, 0U, 0U, 3U, 1073741824U, 0U);
			if (this.handle == -1)
			{
				this.ReportIOError(port_name);
			}
			this.SetAttributes(baud_rate, parity, data_bits, sb, hs);
			if (!WinSerialStream.PurgeComm(this.handle, 12U) || !WinSerialStream.SetupComm(this.handle, read_buffer_size, write_buffer_size))
			{
				this.ReportIOError(null);
			}
			this.read_timeout = read_timeout;
			this.write_timeout = write_timeout;
			this.timeouts = new Timeouts(read_timeout, write_timeout);
			if (!WinSerialStream.SetCommTimeouts(this.handle, this.timeouts))
			{
				this.ReportIOError(null);
			}
			this.SetSignal(SerialSignal.Dtr, dtr_enable);
			if (hs != Handshake.RequestToSend && hs != Handshake.RequestToSendXOnXOff)
			{
				this.SetSignal(SerialSignal.Rts, rts_enable);
			}
			NativeOverlapped structure = default(NativeOverlapped);
			this.write_event = new ManualResetEvent(false);
			structure.EventHandle = this.write_event.Handle;
			this.write_overlapped = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeOverlapped)));
			Marshal.StructureToPtr<NativeOverlapped>(structure, this.write_overlapped, true);
			NativeOverlapped structure2 = default(NativeOverlapped);
			this.read_event = new ManualResetEvent(false);
			structure2.EventHandle = this.read_event.Handle;
			this.read_overlapped = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeOverlapped)));
			Marshal.StructureToPtr<NativeOverlapped>(structure2, this.read_overlapped, true);
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002B35 RID: 11061 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06002B37 RID: 11063 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06002B38 RID: 11064 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06002B39 RID: 11065 RVA: 0x00094073 File Offset: 0x00092273
		// (set) Token: 0x06002B3A RID: 11066 RVA: 0x0009407C File Offset: 0x0009227C
		public override int ReadTimeout
		{
			get
			{
				return this.read_timeout;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.timeouts.SetValues(value, this.write_timeout);
				if (!WinSerialStream.SetCommTimeouts(this.handle, this.timeouts))
				{
					this.ReportIOError(null);
				}
				this.read_timeout = value;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x000940CF File Offset: 0x000922CF
		// (set) Token: 0x06002B3C RID: 11068 RVA: 0x000940D8 File Offset: 0x000922D8
		public override int WriteTimeout
		{
			get
			{
				return this.write_timeout;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.timeouts.SetValues(this.read_timeout, value);
				if (!WinSerialStream.SetCommTimeouts(this.handle, this.timeouts))
				{
					this.ReportIOError(null);
				}
				this.write_timeout = value;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002B3D RID: 11069 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x000044FA File Offset: 0x000026FA
		// (set) Token: 0x06002B3F RID: 11071 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002B40 RID: 11072
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool CloseHandle(int handle);

		// Token: 0x06002B41 RID: 11073 RVA: 0x0009412B File Offset: 0x0009232B
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			WinSerialStream.CloseHandle(this.handle);
			Marshal.FreeHGlobal(this.write_overlapped);
			Marshal.FreeHGlobal(this.read_overlapped);
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x00093D5D File Offset: 0x00091F5D
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x0000BC79 File Offset: 0x00009E79
		public override void Close()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x00094160 File Offset: 0x00092360
		~WinSerialStream()
		{
			this.Dispose(false);
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x00094190 File Offset: 0x00092390
		public override void Flush()
		{
			this.CheckDisposed();
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x000044FA File Offset: 0x000026FA
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002B48 RID: 11080
		[DllImport("kernel32", SetLastError = true)]
		private unsafe static extern bool ReadFile(int handle, byte* buffer, int bytes_to_read, out int bytes_read, IntPtr overlapped);

		// Token: 0x06002B49 RID: 11081
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool GetOverlappedResult(int handle, IntPtr overlapped, ref int bytes_transfered, bool wait);

		// Token: 0x06002B4A RID: 11082 RVA: 0x00094198 File Offset: 0x00092398
		public unsafe override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("offset or count less than zero.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			int num;
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				if (WinSerialStream.ReadFile(this.handle, ptr + offset, count, out num, this.read_overlapped))
				{
					return num;
				}
				if ((long)Marshal.GetLastWin32Error() != 997L)
				{
					this.ReportIOError(null);
				}
				if (!WinSerialStream.GetOverlappedResult(this.handle, this.read_overlapped, ref num, true))
				{
					this.ReportIOError(null);
				}
			}
			if (num == 0)
			{
				throw new TimeoutException();
			}
			return num;
		}

		// Token: 0x06002B4B RID: 11083
		[DllImport("kernel32", SetLastError = true)]
		private unsafe static extern bool WriteFile(int handle, byte* buffer, int bytes_to_write, out int bytes_written, IntPtr overlapped);

		// Token: 0x06002B4C RID: 11084 RVA: 0x00094258 File Offset: 0x00092458
		public unsafe override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			int num = 0;
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				if (WinSerialStream.WriteFile(this.handle, ptr + offset, count, out num, this.write_overlapped))
				{
					return;
				}
				if ((long)Marshal.GetLastWin32Error() != 997L)
				{
					this.ReportIOError(null);
				}
				if (!WinSerialStream.GetOverlappedResult(this.handle, this.write_overlapped, ref num, true))
				{
					this.ReportIOError(null);
				}
			}
			if (num < count)
			{
				throw new TimeoutException();
			}
		}

		// Token: 0x06002B4D RID: 11085
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool GetCommState(int handle, [Out] DCB dcb);

		// Token: 0x06002B4E RID: 11086
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool SetCommState(int handle, DCB dcb);

		// Token: 0x06002B4F RID: 11087 RVA: 0x00094314 File Offset: 0x00092514
		public void SetAttributes(int baud_rate, Parity parity, int data_bits, StopBits bits, Handshake hs)
		{
			DCB dcb = new DCB();
			if (!WinSerialStream.GetCommState(this.handle, dcb))
			{
				this.ReportIOError(null);
			}
			dcb.SetValues(baud_rate, parity, data_bits, bits, hs);
			if (!WinSerialStream.SetCommState(this.handle, dcb))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x00094360 File Offset: 0x00092560
		private void ReportIOError(string optional_arg)
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			string message;
			if (lastWin32Error - 2 > 1)
			{
				if (lastWin32Error != 87)
				{
					message = new Win32Exception().Message;
				}
				else
				{
					message = "Parameter is incorrect.";
				}
			}
			else
			{
				message = "The port `" + optional_arg + "' does not exist.";
			}
			throw new IOException(message);
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000943AC File Offset: 0x000925AC
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000943C7 File Offset: 0x000925C7
		public void DiscardInBuffer()
		{
			if (!WinSerialStream.PurgeComm(this.handle, 8U))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x000943DE File Offset: 0x000925DE
		public void DiscardOutBuffer()
		{
			if (!WinSerialStream.PurgeComm(this.handle, 4U))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x06002B54 RID: 11092
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool ClearCommError(int handle, out uint errors, out CommStat stat);

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002B55 RID: 11093 RVA: 0x000943F8 File Offset: 0x000925F8
		public int BytesToRead
		{
			get
			{
				uint num;
				CommStat commStat;
				if (!WinSerialStream.ClearCommError(this.handle, out num, out commStat))
				{
					this.ReportIOError(null);
				}
				return (int)commStat.BytesIn;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06002B56 RID: 11094 RVA: 0x00094424 File Offset: 0x00092624
		public int BytesToWrite
		{
			get
			{
				uint num;
				CommStat commStat;
				if (!WinSerialStream.ClearCommError(this.handle, out num, out commStat))
				{
					this.ReportIOError(null);
				}
				return (int)commStat.BytesOut;
			}
		}

		// Token: 0x06002B57 RID: 11095
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool GetCommModemStatus(int handle, out uint flags);

		// Token: 0x06002B58 RID: 11096 RVA: 0x00094450 File Offset: 0x00092650
		public SerialSignal GetSignals()
		{
			uint num;
			if (!WinSerialStream.GetCommModemStatus(this.handle, out num))
			{
				this.ReportIOError(null);
			}
			SerialSignal serialSignal = SerialSignal.None;
			if ((num & 128U) != 0U)
			{
				serialSignal |= SerialSignal.Cd;
			}
			if ((num & 16U) != 0U)
			{
				serialSignal |= SerialSignal.Cts;
			}
			if ((num & 32U) != 0U)
			{
				serialSignal |= SerialSignal.Dsr;
			}
			return serialSignal;
		}

		// Token: 0x06002B59 RID: 11097
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool EscapeCommFunction(int handle, uint flags);

		// Token: 0x06002B5A RID: 11098 RVA: 0x00094498 File Offset: 0x00092698
		public void SetSignal(SerialSignal signal, bool value)
		{
			if (signal != SerialSignal.Rts && signal != SerialSignal.Dtr)
			{
				throw new Exception("Wrong internal value");
			}
			uint flags;
			if (signal == SerialSignal.Rts)
			{
				if (value)
				{
					flags = 3U;
				}
				else
				{
					flags = 4U;
				}
			}
			else if (value)
			{
				flags = 5U;
			}
			else
			{
				flags = 6U;
			}
			if (!WinSerialStream.EscapeCommFunction(this.handle, flags))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x000944E7 File Offset: 0x000926E7
		public void SetBreakState(bool value)
		{
			if (!WinSerialStream.EscapeCommFunction(this.handle, value ? 8U : 9U))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x0400174C RID: 5964
		private const uint GenericRead = 2147483648U;

		// Token: 0x0400174D RID: 5965
		private const uint GenericWrite = 1073741824U;

		// Token: 0x0400174E RID: 5966
		private const uint OpenExisting = 3U;

		// Token: 0x0400174F RID: 5967
		private const uint FileFlagOverlapped = 1073741824U;

		// Token: 0x04001750 RID: 5968
		private const uint PurgeRxClear = 8U;

		// Token: 0x04001751 RID: 5969
		private const uint PurgeTxClear = 4U;

		// Token: 0x04001752 RID: 5970
		private const uint WinInfiniteTimeout = 4294967295U;

		// Token: 0x04001753 RID: 5971
		private const uint FileIOPending = 997U;

		// Token: 0x04001754 RID: 5972
		private const uint SetRts = 3U;

		// Token: 0x04001755 RID: 5973
		private const uint ClearRts = 4U;

		// Token: 0x04001756 RID: 5974
		private const uint SetDtr = 5U;

		// Token: 0x04001757 RID: 5975
		private const uint ClearDtr = 6U;

		// Token: 0x04001758 RID: 5976
		private const uint SetBreak = 8U;

		// Token: 0x04001759 RID: 5977
		private const uint ClearBreak = 9U;

		// Token: 0x0400175A RID: 5978
		private const uint CtsOn = 16U;

		// Token: 0x0400175B RID: 5979
		private const uint DsrOn = 32U;

		// Token: 0x0400175C RID: 5980
		private const uint RsldOn = 128U;

		// Token: 0x0400175D RID: 5981
		private const uint EvRxChar = 1U;

		// Token: 0x0400175E RID: 5982
		private const uint EvCts = 8U;

		// Token: 0x0400175F RID: 5983
		private const uint EvDsr = 16U;

		// Token: 0x04001760 RID: 5984
		private const uint EvRlsd = 32U;

		// Token: 0x04001761 RID: 5985
		private const uint EvBreak = 64U;

		// Token: 0x04001762 RID: 5986
		private const uint EvErr = 128U;

		// Token: 0x04001763 RID: 5987
		private const uint EvRing = 256U;

		// Token: 0x04001764 RID: 5988
		private int handle;

		// Token: 0x04001765 RID: 5989
		private int read_timeout;

		// Token: 0x04001766 RID: 5990
		private int write_timeout;

		// Token: 0x04001767 RID: 5991
		private bool disposed;

		// Token: 0x04001768 RID: 5992
		private IntPtr write_overlapped;

		// Token: 0x04001769 RID: 5993
		private IntPtr read_overlapped;

		// Token: 0x0400176A RID: 5994
		private ManualResetEvent read_event;

		// Token: 0x0400176B RID: 5995
		private ManualResetEvent write_event;

		// Token: 0x0400176C RID: 5996
		private Timeouts timeouts;
	}
}
