using System;
using System.Data.Common;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Data.SqlTypes
{
	// Token: 0x0200030C RID: 780
	internal sealed class StreamOnSqlChars : SqlStreamChars
	{
		// Token: 0x06002338 RID: 9016 RVA: 0x000A15EC File Offset: 0x0009F7EC
		internal StreamOnSqlChars(SqlChars s)
		{
			this._sqlchars = s;
			this._lPosition = 0L;
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x000A1603 File Offset: 0x0009F803
		public override bool IsNull
		{
			get
			{
				return this._sqlchars == null || this._sqlchars.IsNull;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x0600233A RID: 9018 RVA: 0x000A161A File Offset: 0x0009F81A
		public override long Length
		{
			get
			{
				this.CheckIfStreamClosed("get_Length");
				return this._sqlchars.Length;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x0600233B RID: 9019 RVA: 0x000A1632 File Offset: 0x0009F832
		// (set) Token: 0x0600233C RID: 9020 RVA: 0x000A1645 File Offset: 0x0009F845
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
				if (value < 0L || value > this._sqlchars.Length)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lPosition = value;
			}
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x000A1678 File Offset: 0x0009F878
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckIfStreamClosed("Seek");
			switch (origin)
			{
			case SeekOrigin.Begin:
				if (offset < 0L || offset > this._sqlchars.Length)
				{
					throw ADP.ArgumentOutOfRange("offset");
				}
				this._lPosition = offset;
				break;
			case SeekOrigin.Current:
			{
				long num = this._lPosition + offset;
				if (num < 0L || num > this._sqlchars.Length)
				{
					throw ADP.ArgumentOutOfRange("offset");
				}
				this._lPosition = num;
				break;
			}
			case SeekOrigin.End:
			{
				long num = this._sqlchars.Length + offset;
				if (num < 0L || num > this._sqlchars.Length)
				{
					throw ADP.ArgumentOutOfRange("offset");
				}
				this._lPosition = num;
				break;
			}
			default:
				throw ADP.ArgumentOutOfRange("offset");
			}
			return this._lPosition;
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x000A1748 File Offset: 0x0009F948
		public override int Read(char[] buffer, int offset, int count)
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
			int num = (int)this._sqlchars.Read(this._lPosition, buffer, offset, count);
			this._lPosition += (long)num;
			return num;
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x000A17C0 File Offset: 0x0009F9C0
		public override void Write(char[] buffer, int offset, int count)
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
			this._sqlchars.Write(this._lPosition, buffer, offset, count);
			this._lPosition += (long)count;
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x000A1835 File Offset: 0x0009FA35
		public override void SetLength(long value)
		{
			this.CheckIfStreamClosed("SetLength");
			this._sqlchars.SetLength(value);
			if (this._lPosition > value)
			{
				this._lPosition = value;
			}
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x000A185E File Offset: 0x0009FA5E
		protected override void Dispose(bool disposing)
		{
			this._sqlchars = null;
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x000A1867 File Offset: 0x0009FA67
		private bool FClosed()
		{
			return this._sqlchars == null;
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x000A1872 File Offset: 0x0009FA72
		private void CheckIfStreamClosed([CallerMemberName] string methodname = "")
		{
			if (this.FClosed())
			{
				throw ADP.StreamClosed(methodname);
			}
		}

		// Token: 0x04001848 RID: 6216
		private SqlChars _sqlchars;

		// Token: 0x04001849 RID: 6217
		private long _lPosition;
	}
}
