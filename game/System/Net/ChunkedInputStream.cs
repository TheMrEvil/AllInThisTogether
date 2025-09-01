using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x0200066E RID: 1646
	internal class ChunkedInputStream : RequestStream
	{
		// Token: 0x060033E4 RID: 13284 RVA: 0x000B4D44 File Offset: 0x000B2F44
		public ChunkedInputStream(HttpListenerContext context, Stream stream, byte[] buffer, int offset, int length) : base(stream, buffer, offset, length)
		{
			this.context = context;
			WebHeaderCollection headers = (WebHeaderCollection)context.Request.Headers;
			this.decoder = new MonoChunkParser(headers);
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x060033E5 RID: 13285 RVA: 0x000B4D81 File Offset: 0x000B2F81
		// (set) Token: 0x060033E6 RID: 13286 RVA: 0x000B4D89 File Offset: 0x000B2F89
		public MonoChunkParser Decoder
		{
			get
			{
				return this.decoder;
			}
			set
			{
				this.decoder = value;
			}
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x000B4D94 File Offset: 0x000B2F94
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			IAsyncResult asyncResult = this.BeginRead(buffer, offset, count, null, null);
			return this.EndRead(asyncResult);
		}

		// Token: 0x060033E8 RID: 13288 RVA: 0x000B4DB4 File Offset: 0x000B2FB4
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || offset > num)
			{
				throw new ArgumentOutOfRangeException("offset exceeds the size of buffer");
			}
			if (count < 0 || offset > num - count)
			{
				throw new ArgumentOutOfRangeException("offset+size exceeds the size of buffer");
			}
			HttpStreamAsyncResult httpStreamAsyncResult = new HttpStreamAsyncResult();
			httpStreamAsyncResult.Callback = cback;
			httpStreamAsyncResult.State = state;
			if (this.no_more_data)
			{
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			int num2 = this.decoder.Read(buffer, offset, count);
			offset += num2;
			count -= num2;
			if (count == 0)
			{
				httpStreamAsyncResult.Count = num2;
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			if (!this.decoder.WantMore)
			{
				this.no_more_data = (num2 == 0);
				httpStreamAsyncResult.Count = num2;
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			httpStreamAsyncResult.Buffer = new byte[8192];
			httpStreamAsyncResult.Offset = 0;
			httpStreamAsyncResult.Count = 8192;
			ChunkedInputStream.ReadBufferState readBufferState = new ChunkedInputStream.ReadBufferState(buffer, offset, count, httpStreamAsyncResult);
			readBufferState.InitialCount += num2;
			base.BeginRead(httpStreamAsyncResult.Buffer, httpStreamAsyncResult.Offset, httpStreamAsyncResult.Count, new AsyncCallback(this.OnRead), readBufferState);
			return httpStreamAsyncResult;
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x000B4EEC File Offset: 0x000B30EC
		private void OnRead(IAsyncResult base_ares)
		{
			ChunkedInputStream.ReadBufferState readBufferState = (ChunkedInputStream.ReadBufferState)base_ares.AsyncState;
			HttpStreamAsyncResult ares = readBufferState.Ares;
			try
			{
				int num = base.EndRead(base_ares);
				this.decoder.Write(ares.Buffer, ares.Offset, num);
				num = this.decoder.Read(readBufferState.Buffer, readBufferState.Offset, readBufferState.Count);
				readBufferState.Offset += num;
				readBufferState.Count -= num;
				if (readBufferState.Count == 0 || !this.decoder.WantMore || num == 0)
				{
					this.no_more_data = (!this.decoder.WantMore && num == 0);
					ares.Count = readBufferState.InitialCount - readBufferState.Count;
					ares.Complete();
				}
				else
				{
					ares.Offset = 0;
					ares.Count = Math.Min(8192, this.decoder.ChunkLeft + 6);
					base.BeginRead(ares.Buffer, ares.Offset, ares.Count, new AsyncCallback(this.OnRead), readBufferState);
				}
			}
			catch (Exception ex)
			{
				this.context.Connection.SendError(ex.Message, 400);
				ares.Complete(ex);
			}
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x000B5034 File Offset: 0x000B3234
		public override int EndRead(IAsyncResult ares)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			HttpStreamAsyncResult httpStreamAsyncResult = ares as HttpStreamAsyncResult;
			if (ares == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "ares");
			}
			if (!ares.IsCompleted)
			{
				ares.AsyncWaitHandle.WaitOne();
			}
			if (httpStreamAsyncResult.Error != null)
			{
				throw new HttpListenerException(400, "I/O operation aborted: " + httpStreamAsyncResult.Error.Message);
			}
			return httpStreamAsyncResult.Count;
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x000B50B6 File Offset: 0x000B32B6
		public override void Close()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				base.Close();
			}
		}

		// Token: 0x04001E66 RID: 7782
		private bool disposed;

		// Token: 0x04001E67 RID: 7783
		private MonoChunkParser decoder;

		// Token: 0x04001E68 RID: 7784
		private HttpListenerContext context;

		// Token: 0x04001E69 RID: 7785
		private bool no_more_data;

		// Token: 0x0200066F RID: 1647
		private class ReadBufferState
		{
			// Token: 0x060033EC RID: 13292 RVA: 0x000B50CD File Offset: 0x000B32CD
			public ReadBufferState(byte[] buffer, int offset, int count, HttpStreamAsyncResult ares)
			{
				this.Buffer = buffer;
				this.Offset = offset;
				this.Count = count;
				this.InitialCount = count;
				this.Ares = ares;
			}

			// Token: 0x04001E6A RID: 7786
			public byte[] Buffer;

			// Token: 0x04001E6B RID: 7787
			public int Offset;

			// Token: 0x04001E6C RID: 7788
			public int Count;

			// Token: 0x04001E6D RID: 7789
			public int InitialCount;

			// Token: 0x04001E6E RID: 7790
			public HttpStreamAsyncResult Ares;
		}
	}
}
