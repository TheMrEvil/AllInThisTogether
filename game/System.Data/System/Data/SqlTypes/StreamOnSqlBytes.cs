using System;
using System.Data.Common;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Data.SqlTypes
{
	// Token: 0x0200030A RID: 778
	internal sealed class StreamOnSqlBytes : Stream
	{
		// Token: 0x06002308 RID: 8968 RVA: 0x000A0B1B File Offset: 0x0009ED1B
		internal StreamOnSqlBytes(SqlBytes sb)
		{
			this._sb = sb;
			this._lPosition = 0L;
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x000A0B32 File Offset: 0x0009ED32
		public override bool CanRead
		{
			get
			{
				return this._sb != null && !this._sb.IsNull;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x000A0B4C File Offset: 0x0009ED4C
		public override bool CanSeek
		{
			get
			{
				return this._sb != null;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x0600230B RID: 8971 RVA: 0x000A0B57 File Offset: 0x0009ED57
		public override bool CanWrite
		{
			get
			{
				return this._sb != null && (!this._sb.IsNull || this._sb._rgbBuf != null);
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x000A0B80 File Offset: 0x0009ED80
		public override long Length
		{
			get
			{
				this.CheckIfStreamClosed("get_Length");
				return this._sb.Length;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x0600230D RID: 8973 RVA: 0x000A0B98 File Offset: 0x0009ED98
		// (set) Token: 0x0600230E RID: 8974 RVA: 0x000A0BAB File Offset: 0x0009EDAB
		public override long Position
		{
			get
			{
				this.CheckIfStreamClosed("get_Position");
				return this._lPosition;
			}
			set
			{
				this.CheckIfStreamClosed("set_Position");
				if (value < 0L || value > this._sb.Length)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lPosition = value;
			}
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x000A0BE0 File Offset: 0x0009EDE0
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckIfStreamClosed("Seek");
			switch (origin)
			{
			case SeekOrigin.Begin:
				if (offset < 0L || offset > this._sb.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._lPosition = offset;
				break;
			case SeekOrigin.Current:
			{
				long num = this._lPosition + offset;
				if (num < 0L || num > this._sb.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._lPosition = num;
				break;
			}
			case SeekOrigin.End:
			{
				long num = this._sb.Length + offset;
				if (num < 0L || num > this._sb.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._lPosition = num;
				break;
			}
			default:
				throw ADP.InvalidSeekOrigin("offset");
			}
			return this._lPosition;
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x000A0CB0 File Offset: 0x0009EEB0
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckIfStreamClosed("Read");
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int num = (int)this._sb.Read(this._lPosition, buffer, offset, count);
			this._lPosition += (long)num;
			return num;
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000A0D28 File Offset: 0x0009EF28
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckIfStreamClosed("Write");
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this._sb.Write(this._lPosition, buffer, offset, count);
			this._lPosition += (long)count;
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000A0DA0 File Offset: 0x0009EFA0
		public override int ReadByte()
		{
			this.CheckIfStreamClosed("ReadByte");
			if (this._lPosition >= this._sb.Length)
			{
				return -1;
			}
			int result = (int)this._sb[this._lPosition];
			this._lPosition += 1L;
			return result;
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x000A0DED File Offset: 0x0009EFED
		public override void WriteByte(byte value)
		{
			this.CheckIfStreamClosed("WriteByte");
			this._sb[this._lPosition] = value;
			this._lPosition += 1L;
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x000A0E1B File Offset: 0x0009F01B
		public override void SetLength(long value)
		{
			this.CheckIfStreamClosed("SetLength");
			this._sb.SetLength(value);
			if (this._lPosition > value)
			{
				this._lPosition = value;
			}
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x000A0E44 File Offset: 0x0009F044
		public override void Flush()
		{
			if (this._sb.FStream())
			{
				this._sb._stream.Flush();
			}
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x000A0E64 File Offset: 0x0009F064
		protected override void Dispose(bool disposing)
		{
			try
			{
				this._sb = null;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x000A0E94 File Offset: 0x0009F094
		private bool FClosed()
		{
			return this._sb == null;
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x000A0E9F File Offset: 0x0009F09F
		private void CheckIfStreamClosed([CallerMemberName] string methodname = "")
		{
			if (this.FClosed())
			{
				throw ADP.StreamClosed(methodname);
			}
		}

		// Token: 0x0400183F RID: 6207
		private SqlBytes _sb;

		// Token: 0x04001840 RID: 6208
		private long _lPosition;
	}
}
