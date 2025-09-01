using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000595 RID: 1429
	internal class NetworkStreamWrapper : Stream
	{
		// Token: 0x06002E6D RID: 11885 RVA: 0x000A0AE2 File Offset: 0x0009ECE2
		internal NetworkStreamWrapper(TcpClient client)
		{
			this._client = client;
			this._networkStream = client.GetStream();
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06002E6E RID: 11886 RVA: 0x000A0AFD File Offset: 0x0009ECFD
		protected bool UsingSecureStream
		{
			get
			{
				return this._networkStream is TlsStream;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06002E6F RID: 11887 RVA: 0x000A0B0D File Offset: 0x0009ED0D
		internal IPAddress ServerAddress
		{
			get
			{
				return ((IPEndPoint)this.Socket.RemoteEndPoint).Address;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06002E70 RID: 11888 RVA: 0x000A0B24 File Offset: 0x0009ED24
		internal Socket Socket
		{
			get
			{
				return this._client.Client;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002E71 RID: 11889 RVA: 0x000A0B31 File Offset: 0x0009ED31
		// (set) Token: 0x06002E72 RID: 11890 RVA: 0x000A0B39 File Offset: 0x0009ED39
		internal NetworkStream NetworkStream
		{
			get
			{
				return this._networkStream;
			}
			set
			{
				this._networkStream = value;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002E73 RID: 11891 RVA: 0x000A0B42 File Offset: 0x0009ED42
		public override bool CanRead
		{
			get
			{
				return this._networkStream.CanRead;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002E74 RID: 11892 RVA: 0x000A0B4F File Offset: 0x0009ED4F
		public override bool CanSeek
		{
			get
			{
				return this._networkStream.CanSeek;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002E75 RID: 11893 RVA: 0x000A0B5C File Offset: 0x0009ED5C
		public override bool CanWrite
		{
			get
			{
				return this._networkStream.CanWrite;
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002E76 RID: 11894 RVA: 0x000A0B69 File Offset: 0x0009ED69
		public override bool CanTimeout
		{
			get
			{
				return this._networkStream.CanTimeout;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06002E77 RID: 11895 RVA: 0x000A0B76 File Offset: 0x0009ED76
		// (set) Token: 0x06002E78 RID: 11896 RVA: 0x000A0B83 File Offset: 0x0009ED83
		public override int ReadTimeout
		{
			get
			{
				return this._networkStream.ReadTimeout;
			}
			set
			{
				this._networkStream.ReadTimeout = value;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06002E79 RID: 11897 RVA: 0x000A0B91 File Offset: 0x0009ED91
		// (set) Token: 0x06002E7A RID: 11898 RVA: 0x000A0B9E File Offset: 0x0009ED9E
		public override int WriteTimeout
		{
			get
			{
				return this._networkStream.WriteTimeout;
			}
			set
			{
				this._networkStream.WriteTimeout = value;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06002E7B RID: 11899 RVA: 0x000A0BAC File Offset: 0x0009EDAC
		public override long Length
		{
			get
			{
				return this._networkStream.Length;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06002E7C RID: 11900 RVA: 0x000A0BB9 File Offset: 0x0009EDB9
		// (set) Token: 0x06002E7D RID: 11901 RVA: 0x000A0BC6 File Offset: 0x0009EDC6
		public override long Position
		{
			get
			{
				return this._networkStream.Position;
			}
			set
			{
				this._networkStream.Position = value;
			}
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x000A0BD4 File Offset: 0x0009EDD4
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._networkStream.Seek(offset, origin);
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x000A0BE3 File Offset: 0x0009EDE3
		public override int Read(byte[] buffer, int offset, int size)
		{
			return this._networkStream.Read(buffer, offset, size);
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x000A0BF3 File Offset: 0x0009EDF3
		public override void Write(byte[] buffer, int offset, int size)
		{
			this._networkStream.Write(buffer, offset, size);
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x000A0C04 File Offset: 0x0009EE04
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.CloseSocket();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x000A0C34 File Offset: 0x0009EE34
		internal void CloseSocket()
		{
			this._networkStream.Close();
			this._client.Dispose();
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x000A0C4C File Offset: 0x0009EE4C
		public void Close(int timeout)
		{
			this._networkStream.Close(timeout);
			this._client.Dispose();
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x000A0C65 File Offset: 0x0009EE65
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this._networkStream.BeginRead(buffer, offset, size, callback, state);
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x000A0C79 File Offset: 0x0009EE79
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this._networkStream.EndRead(asyncResult);
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x000A0C87 File Offset: 0x0009EE87
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._networkStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x000A0C99 File Offset: 0x0009EE99
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this._networkStream.BeginWrite(buffer, offset, size, callback, state);
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x000A0CAD File Offset: 0x0009EEAD
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this._networkStream.EndWrite(asyncResult);
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x000A0CBB File Offset: 0x0009EEBB
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._networkStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x000A0CCD File Offset: 0x0009EECD
		public override void Flush()
		{
			this._networkStream.Flush();
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x000A0CDA File Offset: 0x0009EEDA
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._networkStream.FlushAsync(cancellationToken);
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x000A0CE8 File Offset: 0x0009EEE8
		public override void SetLength(long value)
		{
			this._networkStream.SetLength(value);
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x000A0CF6 File Offset: 0x0009EEF6
		internal void SetSocketTimeoutOption(int timeout)
		{
			this._networkStream.ReadTimeout = timeout;
			this._networkStream.WriteTimeout = timeout;
		}

		// Token: 0x040019A4 RID: 6564
		private TcpClient _client;

		// Token: 0x040019A5 RID: 6565
		private NetworkStream _networkStream;
	}
}
