using System;
using System.IO;

namespace WebSocketSharp.Net
{
	// Token: 0x02000037 RID: 55
	internal class ChunkedRequestStream : RequestStream
	{
		// Token: 0x060003AD RID: 941 RVA: 0x00016B25 File Offset: 0x00014D25
		static ChunkedRequestStream()
		{
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00016B32 File Offset: 0x00014D32
		internal ChunkedRequestStream(Stream innerStream, byte[] initialBuffer, int offset, int count, HttpListenerContext context) : base(innerStream, initialBuffer, offset, count, -1L)
		{
			this._context = context;
			this._decoder = new ChunkStream((WebHeaderCollection)context.Request.Headers);
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00016B68 File Offset: 0x00014D68
		internal bool HasRemainingBuffer
		{
			get
			{
				return this._decoder.Count + base.Count > 0;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00016B90 File Offset: 0x00014D90
		internal byte[] RemainingBuffer
		{
			get
			{
				byte[] result;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					int count = this._decoder.Count;
					bool flag = count > 0;
					if (flag)
					{
						memoryStream.Write(this._decoder.EndBuffer, this._decoder.Offset, count);
					}
					count = base.Count;
					bool flag2 = count > 0;
					if (flag2)
					{
						memoryStream.Write(base.InitialBuffer, base.Offset, count);
					}
					memoryStream.Close();
					result = memoryStream.ToArray();
				}
				return result;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00016C28 File Offset: 0x00014E28
		private void onRead(IAsyncResult asyncResult)
		{
			ReadBufferState readBufferState = (ReadBufferState)asyncResult.AsyncState;
			HttpStreamAsyncResult asyncResult2 = readBufferState.AsyncResult;
			try
			{
				int num = base.EndRead(asyncResult);
				this._decoder.Write(asyncResult2.Buffer, asyncResult2.Offset, num);
				num = this._decoder.Read(readBufferState.Buffer, readBufferState.Offset, readBufferState.Count);
				readBufferState.Offset += num;
				readBufferState.Count -= num;
				bool flag = readBufferState.Count == 0 || !this._decoder.WantsMore || num == 0;
				if (flag)
				{
					this._noMoreData = (!this._decoder.WantsMore && num == 0);
					asyncResult2.Count = readBufferState.InitialCount - readBufferState.Count;
					asyncResult2.Complete();
				}
				else
				{
					base.BeginRead(asyncResult2.Buffer, asyncResult2.Offset, asyncResult2.Count, new AsyncCallback(this.onRead), readBufferState);
				}
			}
			catch (Exception exception)
			{
				this._context.ErrorMessage = "I/O operation aborted";
				this._context.SendError();
				asyncResult2.Complete(exception);
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00016D64 File Offset: 0x00014F64
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				string objectName = base.GetType().ToString();
				throw new ObjectDisposedException(objectName);
			}
			bool flag = buffer == null;
			if (flag)
			{
				throw new ArgumentNullException("buffer");
			}
			bool flag2 = offset < 0;
			if (flag2)
			{
				string message = "A negative value.";
				throw new ArgumentOutOfRangeException("offset", message);
			}
			bool flag3 = count < 0;
			if (flag3)
			{
				string message2 = "A negative value.";
				throw new ArgumentOutOfRangeException("count", message2);
			}
			int num = buffer.Length;
			bool flag4 = offset + count > num;
			if (flag4)
			{
				string message3 = "The sum of 'offset' and 'count' is greater than the length of 'buffer'.";
				throw new ArgumentException(message3);
			}
			HttpStreamAsyncResult httpStreamAsyncResult = new HttpStreamAsyncResult(callback, state);
			bool noMoreData = this._noMoreData;
			IAsyncResult result;
			if (noMoreData)
			{
				httpStreamAsyncResult.Complete();
				result = httpStreamAsyncResult;
			}
			else
			{
				int num2 = this._decoder.Read(buffer, offset, count);
				offset += num2;
				count -= num2;
				bool flag5 = count == 0;
				if (flag5)
				{
					httpStreamAsyncResult.Count = num2;
					httpStreamAsyncResult.Complete();
					result = httpStreamAsyncResult;
				}
				else
				{
					bool flag6 = !this._decoder.WantsMore;
					if (flag6)
					{
						this._noMoreData = (num2 == 0);
						httpStreamAsyncResult.Count = num2;
						httpStreamAsyncResult.Complete();
						result = httpStreamAsyncResult;
					}
					else
					{
						httpStreamAsyncResult.Buffer = new byte[ChunkedRequestStream._bufferLength];
						httpStreamAsyncResult.Offset = 0;
						httpStreamAsyncResult.Count = ChunkedRequestStream._bufferLength;
						ReadBufferState readBufferState = new ReadBufferState(buffer, offset, count, httpStreamAsyncResult);
						readBufferState.InitialCount += num2;
						base.BeginRead(httpStreamAsyncResult.Buffer, httpStreamAsyncResult.Offset, httpStreamAsyncResult.Count, new AsyncCallback(this.onRead), readBufferState);
						result = httpStreamAsyncResult;
					}
				}
			}
			return result;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00016F08 File Offset: 0x00015108
		public override void Close()
		{
			bool disposed = this._disposed;
			if (!disposed)
			{
				base.Close();
				this._disposed = true;
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00016F30 File Offset: 0x00015130
		public override int EndRead(IAsyncResult asyncResult)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				string objectName = base.GetType().ToString();
				throw new ObjectDisposedException(objectName);
			}
			bool flag = asyncResult == null;
			if (flag)
			{
				throw new ArgumentNullException("asyncResult");
			}
			HttpStreamAsyncResult httpStreamAsyncResult = asyncResult as HttpStreamAsyncResult;
			bool flag2 = httpStreamAsyncResult == null;
			if (flag2)
			{
				string message = "A wrong IAsyncResult instance.";
				throw new ArgumentException(message, "asyncResult");
			}
			bool flag3 = !httpStreamAsyncResult.IsCompleted;
			if (flag3)
			{
				httpStreamAsyncResult.AsyncWaitHandle.WaitOne();
			}
			bool hasException = httpStreamAsyncResult.HasException;
			if (hasException)
			{
				string message2 = "The I/O operation has been aborted.";
				throw new HttpListenerException(995, message2);
			}
			return httpStreamAsyncResult.Count;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00016FE0 File Offset: 0x000151E0
		public override int Read(byte[] buffer, int offset, int count)
		{
			IAsyncResult asyncResult = this.BeginRead(buffer, offset, count, null, null);
			return this.EndRead(asyncResult);
		}

		// Token: 0x04000192 RID: 402
		private static readonly int _bufferLength = 8192;

		// Token: 0x04000193 RID: 403
		private HttpListenerContext _context;

		// Token: 0x04000194 RID: 404
		private ChunkStream _decoder;

		// Token: 0x04000195 RID: 405
		private bool _disposed;

		// Token: 0x04000196 RID: 406
		private bool _noMoreData;
	}
}
