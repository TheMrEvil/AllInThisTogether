using System;

namespace WebSocketSharp.Net
{
	// Token: 0x02000034 RID: 52
	internal class ReadBufferState
	{
		// Token: 0x0600039F RID: 927 RVA: 0x000169B2 File Offset: 0x00014BB2
		public ReadBufferState(byte[] buffer, int offset, int count, HttpStreamAsyncResult asyncResult)
		{
			this._buffer = buffer;
			this._offset = offset;
			this._count = count;
			this._asyncResult = asyncResult;
			this._initialCount = count;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x000169E0 File Offset: 0x00014BE0
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x000169F8 File Offset: 0x00014BF8
		public HttpStreamAsyncResult AsyncResult
		{
			get
			{
				return this._asyncResult;
			}
			set
			{
				this._asyncResult = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00016A04 File Offset: 0x00014C04
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x00016A1C File Offset: 0x00014C1C
		public byte[] Buffer
		{
			get
			{
				return this._buffer;
			}
			set
			{
				this._buffer = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00016A28 File Offset: 0x00014C28
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x00016A40 File Offset: 0x00014C40
		public int Count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00016A4C File Offset: 0x00014C4C
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x00016A64 File Offset: 0x00014C64
		public int InitialCount
		{
			get
			{
				return this._initialCount;
			}
			set
			{
				this._initialCount = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00016A70 File Offset: 0x00014C70
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x00016A88 File Offset: 0x00014C88
		public int Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				this._offset = value;
			}
		}

		// Token: 0x04000185 RID: 389
		private HttpStreamAsyncResult _asyncResult;

		// Token: 0x04000186 RID: 390
		private byte[] _buffer;

		// Token: 0x04000187 RID: 391
		private int _count;

		// Token: 0x04000188 RID: 392
		private int _initialCount;

		// Token: 0x04000189 RID: 393
		private int _offset;
	}
}
