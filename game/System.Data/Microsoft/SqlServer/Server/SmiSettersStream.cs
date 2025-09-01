using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000037 RID: 55
	internal class SmiSettersStream : Stream
	{
		// Token: 0x06000227 RID: 551 RVA: 0x00007F88 File Offset: 0x00006188
		internal SmiSettersStream(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData)
		{
			this._sink = sink;
			this._setters = setters;
			this._ordinal = ordinal;
			this._lengthWritten = 0L;
			this._metaData = metaData;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00006D61 File Offset: 0x00004F61
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00007FB5 File Offset: 0x000061B5
		public override long Length
		{
			get
			{
				return this._lengthWritten;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00007FB5 File Offset: 0x000061B5
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00006DA0 File Offset: 0x00004FA0
		public override long Position
		{
			get
			{
				return this._lengthWritten;
			}
			set
			{
				throw SQL.StreamSeekNotSupported();
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00007FBD File Offset: 0x000061BD
		public override void Flush()
		{
			this._lengthWritten = ValueUtilsSmi.SetBytesLength(this._sink, this._setters, this._ordinal, this._metaData, this._lengthWritten);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00006DA0 File Offset: 0x00004FA0
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw SQL.StreamSeekNotSupported();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00007FE8 File Offset: 0x000061E8
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw ADP.ArgumentOutOfRange("value");
			}
			ValueUtilsSmi.SetBytesLength(this._sink, this._setters, this._ordinal, this._metaData, value);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00008019 File Offset: 0x00006219
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw SQL.StreamReadNotSupported();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00008020 File Offset: 0x00006220
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._lengthWritten += ValueUtilsSmi.SetBytes(this._sink, this._setters, this._ordinal, this._metaData, this._lengthWritten, buffer, offset, count);
		}

		// Token: 0x040004B4 RID: 1204
		private SmiEventSink_Default _sink;

		// Token: 0x040004B5 RID: 1205
		private ITypedSettersV3 _setters;

		// Token: 0x040004B6 RID: 1206
		private int _ordinal;

		// Token: 0x040004B7 RID: 1207
		private long _lengthWritten;

		// Token: 0x040004B8 RID: 1208
		private SmiMetaData _metaData;
	}
}
