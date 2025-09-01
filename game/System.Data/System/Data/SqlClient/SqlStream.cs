using System;
using System.Data.Common;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x0200022E RID: 558
	internal sealed class SqlStream : Stream
	{
		// Token: 0x06001AD4 RID: 6868 RVA: 0x0007B6D2 File Offset: 0x000798D2
		internal SqlStream(SqlDataReader reader, bool addByteOrderMark, bool processAllRows) : this(0, reader, addByteOrderMark, processAllRows, true)
		{
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0007B6DF File Offset: 0x000798DF
		internal SqlStream(int columnOrdinal, SqlDataReader reader, bool addByteOrderMark, bool processAllRows, bool advanceReader)
		{
			this._columnOrdinal = columnOrdinal;
			this._reader = reader;
			this._bom = (addByteOrderMark ? 65279 : 0);
			this._processAllRows = processAllRows;
			this._advanceReader = advanceReader;
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x00006D61 File Offset: 0x00004F61
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x00008E4B File Offset: 0x0000704B
		public override long Length
		{
			get
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x00008E4B File Offset: 0x0000704B
		// (set) Token: 0x06001ADB RID: 6875 RVA: 0x00008E4B File Offset: 0x0000704B
		public override long Position
		{
			get
			{
				throw ADP.NotSupported();
			}
			set
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0007B718 File Offset: 0x00079918
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._advanceReader && this._reader != null && !this._reader.IsClosed)
				{
					this._reader.Close();
				}
				this._reader = null;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00008E4B File Offset: 0x0000704B
		public override void Flush()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0007B774 File Offset: 0x00079974
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			int num2 = 0;
			if (this._reader == null)
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
			if (this._bom > 0)
			{
				this._bufferedData = new byte[2];
				num2 = this.ReadBytes(this._bufferedData, 0, 2);
				if (num2 < 2 || (this._bufferedData[0] == 223 && this._bufferedData[1] == 255))
				{
					this._bom = 0;
				}
				while (count > 0 && this._bom > 0)
				{
					buffer[offset] = (byte)this._bom;
					this._bom >>= 8;
					offset++;
					count--;
					num++;
				}
			}
			if (num2 > 0)
			{
				while (count > 0)
				{
					buffer[offset++] = this._bufferedData[0];
					num++;
					count--;
					if (num2 > 1 && count > 0)
					{
						buffer[offset++] = this._bufferedData[1];
						num++;
						count--;
						break;
					}
				}
				this._bufferedData = null;
			}
			return num + this.ReadBytes(buffer, offset, count);
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0007B8B8 File Offset: 0x00079AB8
		private static bool AdvanceToNextRow(SqlDataReader reader)
		{
			while (!reader.Read())
			{
				if (!reader.NextResult())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0007B8D0 File Offset: 0x00079AD0
		private int ReadBytes(byte[] buffer, int offset, int count)
		{
			bool flag = true;
			int num = 0;
			if (this._reader.IsClosed || this._endOfColumn)
			{
				return 0;
			}
			try
			{
				while (count > 0)
				{
					if (this._advanceReader && this._bytesCol == 0L)
					{
						flag = false;
						if ((!this._readFirstRow || this._processAllRows) && SqlStream.AdvanceToNextRow(this._reader))
						{
							this._readFirstRow = true;
							if (this._reader.IsDBNull(this._columnOrdinal))
							{
								continue;
							}
							flag = true;
						}
					}
					if (!flag)
					{
						break;
					}
					int num2 = (int)this._reader.GetBytesInternal(this._columnOrdinal, this._bytesCol, buffer, offset, count);
					if (num2 < count)
					{
						this._bytesCol = 0L;
						flag = false;
						if (!this._advanceReader)
						{
							this._endOfColumn = true;
						}
					}
					else
					{
						this._bytesCol += (long)num2;
					}
					count -= num2;
					offset += num2;
					num += num2;
				}
				if (!flag && this._advanceReader)
				{
					this._reader.Close();
				}
			}
			catch (Exception e)
			{
				if (this._advanceReader && ADP.IsCatchableExceptionType(e))
				{
					this._reader.Close();
				}
				throw;
			}
			return num;
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0007B9F4 File Offset: 0x00079BF4
		internal XmlReader ToXmlReader(bool async = false)
		{
			return SqlTypeWorkarounds.SqlXmlCreateSqlXmlReader(this, true, async);
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x00008E4B File Offset: 0x0000704B
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x00008E4B File Offset: 0x0000704B
		public override void SetLength(long value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00008E4B File Offset: 0x0000704B
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x0400112F RID: 4399
		private SqlDataReader _reader;

		// Token: 0x04001130 RID: 4400
		private int _columnOrdinal;

		// Token: 0x04001131 RID: 4401
		private long _bytesCol;

		// Token: 0x04001132 RID: 4402
		private int _bom;

		// Token: 0x04001133 RID: 4403
		private byte[] _bufferedData;

		// Token: 0x04001134 RID: 4404
		private bool _processAllRows;

		// Token: 0x04001135 RID: 4405
		private bool _advanceReader;

		// Token: 0x04001136 RID: 4406
		private bool _readFirstRow;

		// Token: 0x04001137 RID: 4407
		private bool _endOfColumn;
	}
}
