using System;
using System.Data.SqlClient;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000029 RID: 41
	internal class SmiGettersStream : Stream
	{
		// Token: 0x06000185 RID: 389 RVA: 0x00006D34 File Offset: 0x00004F34
		internal SmiGettersStream(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			this._sink = sink;
			this._getters = getters;
			this._ordinal = ordinal;
			this._readPosition = 0L;
			this._metaData = metaData;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00006D61 File Offset: 0x00004F61
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00006D68 File Offset: 0x00004F68
		public override long Length
		{
			get
			{
				return ValueUtilsSmi.GetBytesInternal(this._sink, this._getters, this._ordinal, this._metaData, 0L, null, 0, 0, false);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00006D98 File Offset: 0x00004F98
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00006DA0 File Offset: 0x00004FA0
		public override long Position
		{
			get
			{
				return this._readPosition;
			}
			set
			{
				throw SQL.StreamSeekNotSupported();
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006DA7 File Offset: 0x00004FA7
		public override void Flush()
		{
			throw SQL.StreamWriteNotSupported();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00006DA0 File Offset: 0x00004FA0
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw SQL.StreamSeekNotSupported();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006DA7 File Offset: 0x00004FA7
		public override void SetLength(long value)
		{
			throw SQL.StreamWriteNotSupported();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006DB0 File Offset: 0x00004FB0
		public override int Read(byte[] buffer, int offset, int count)
		{
			long bytesInternal = ValueUtilsSmi.GetBytesInternal(this._sink, this._getters, this._ordinal, this._metaData, this._readPosition, buffer, offset, count, false);
			this._readPosition += bytesInternal;
			return checked((int)bytesInternal);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006DA7 File Offset: 0x00004FA7
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw SQL.StreamWriteNotSupported();
		}

		// Token: 0x04000451 RID: 1105
		private SmiEventSink_Default _sink;

		// Token: 0x04000452 RID: 1106
		private ITypedGettersV3 _getters;

		// Token: 0x04000453 RID: 1107
		private int _ordinal;

		// Token: 0x04000454 RID: 1108
		private long _readPosition;

		// Token: 0x04000455 RID: 1109
		private SmiMetaData _metaData;
	}
}
