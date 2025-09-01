using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;

namespace System.Data.SqlClient
{
	// Token: 0x0200022F RID: 559
	internal sealed class SqlCachedStream : Stream
	{
		// Token: 0x06001AE5 RID: 6885 RVA: 0x0007B9FE File Offset: 0x00079BFE
		internal SqlCachedStream(SqlCachedBuffer sqlBuf)
		{
			this._cachedBytes = sqlBuf.CachedBytes;
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x00006D61 File Offset: 0x00004F61
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x00006D61 File Offset: 0x00004F61
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x0007BA12 File Offset: 0x00079C12
		public override long Length
		{
			get
			{
				return this.TotalLength;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x0007BA1C File Offset: 0x00079C1C
		// (set) Token: 0x06001AEB RID: 6891 RVA: 0x0007BA63 File Offset: 0x00079C63
		public override long Position
		{
			get
			{
				long num = 0L;
				if (this._currentArrayIndex > 0)
				{
					for (int i = 0; i < this._currentArrayIndex; i++)
					{
						num += (long)this._cachedBytes[i].Length;
					}
				}
				return num + (long)this._currentPosition;
			}
			set
			{
				if (this._cachedBytes == null)
				{
					throw ADP.StreamClosed("set_Position");
				}
				this.SetInternalPosition(value, "set_Position");
			}
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x0007BA84 File Offset: 0x00079C84
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._cachedBytes != null)
				{
					this._cachedBytes.Clear();
				}
				this._cachedBytes = null;
				this._currentPosition = 0;
				this._currentArrayIndex = 0;
				this._totalLength = 0L;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00008E4B File Offset: 0x0000704B
		public override void Flush()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x0007BAE0 File Offset: 0x00079CE0
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			if (this._cachedBytes == null)
			{
				throw ADP.StreamClosed("Read");
			}
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw ADP.ArgumentOutOfRange(string.Empty, (offset < 0) ? "offset" : "count");
			}
			if (buffer.Length - offset < count)
			{
				throw ADP.ArgumentOutOfRange("count");
			}
			if (this._cachedBytes.Count <= this._currentArrayIndex)
			{
				return 0;
			}
			while (count > 0)
			{
				if (this._cachedBytes[this._currentArrayIndex].Length <= this._currentPosition)
				{
					this._currentArrayIndex++;
					if (this._cachedBytes.Count <= this._currentArrayIndex)
					{
						break;
					}
					this._currentPosition = 0;
				}
				int num2 = this._cachedBytes[this._currentArrayIndex].Length - this._currentPosition;
				if (num2 > count)
				{
					num2 = count;
				}
				Buffer.BlockCopy(this._cachedBytes[this._currentArrayIndex], this._currentPosition, buffer, offset, num2);
				this._currentPosition += num2;
				count -= num2;
				offset += num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x0007BC08 File Offset: 0x00079E08
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num = 0L;
			if (this._cachedBytes == null)
			{
				throw ADP.StreamClosed("Seek");
			}
			switch (origin)
			{
			case SeekOrigin.Begin:
				this.SetInternalPosition(offset, "offset");
				break;
			case SeekOrigin.Current:
				num = offset + this.Position;
				this.SetInternalPosition(num, "offset");
				break;
			case SeekOrigin.End:
				num = this.TotalLength + offset;
				this.SetInternalPosition(num, "offset");
				break;
			default:
				throw ADP.InvalidSeekOrigin("offset");
			}
			return num;
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x00008E4B File Offset: 0x0000704B
		public override void SetLength(long value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x00008E4B File Offset: 0x0000704B
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x0007BC88 File Offset: 0x00079E88
		private void SetInternalPosition(long lPos, string argumentName)
		{
			long num = lPos;
			if (num < 0L)
			{
				throw new ArgumentOutOfRangeException(argumentName);
			}
			for (int i = 0; i < this._cachedBytes.Count; i++)
			{
				if (num <= (long)this._cachedBytes[i].Length)
				{
					this._currentArrayIndex = i;
					this._currentPosition = (int)num;
					return;
				}
				num -= (long)this._cachedBytes[i].Length;
			}
			if (num > 0L)
			{
				throw new ArgumentOutOfRangeException(argumentName);
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x0007BCFC File Offset: 0x00079EFC
		private long TotalLength
		{
			get
			{
				if (this._totalLength == 0L && this._cachedBytes != null)
				{
					long num = 0L;
					for (int i = 0; i < this._cachedBytes.Count; i++)
					{
						num += (long)this._cachedBytes[i].Length;
					}
					this._totalLength = num;
				}
				return this._totalLength;
			}
		}

		// Token: 0x04001138 RID: 4408
		private int _currentPosition;

		// Token: 0x04001139 RID: 4409
		private int _currentArrayIndex;

		// Token: 0x0400113A RID: 4410
		private List<byte[]> _cachedBytes;

		// Token: 0x0400113B RID: 4411
		private long _totalLength;
	}
}
