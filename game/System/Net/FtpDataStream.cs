using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace System.Net
{
	// Token: 0x0200058A RID: 1418
	internal class FtpDataStream : Stream, ICloseEx
	{
		// Token: 0x06002DEB RID: 11755 RVA: 0x0009E5F4 File Offset: 0x0009C7F4
		internal FtpDataStream(NetworkStream networkStream, FtpWebRequest request, TriState writeOnly)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, ".ctor");
			}
			this._readable = true;
			this._writeable = true;
			if (writeOnly == TriState.True)
			{
				this._readable = false;
			}
			else if (writeOnly == TriState.False)
			{
				this._writeable = false;
			}
			this._networkStream = networkStream;
			this._request = request;
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x0009E650 File Offset: 0x0009C850
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					((ICloseEx)this).CloseEx(CloseExState.Normal);
				}
				else
				{
					((ICloseEx)this).CloseEx(CloseExState.Abort | CloseExState.Silent);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x0009E68C File Offset: 0x0009C88C
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("state = {0}", new object[]
				{
					closeState
				}), "CloseEx");
			}
			lock (this)
			{
				if (this._closing)
				{
					return;
				}
				this._closing = true;
				this._writeable = false;
				this._readable = false;
			}
			try
			{
				try
				{
					if ((closeState & CloseExState.Abort) == CloseExState.Normal)
					{
						this._networkStream.Close(-1);
					}
					else
					{
						this._networkStream.Close(0);
					}
				}
				finally
				{
					this._request.DataStreamClosed(closeState);
				}
			}
			catch (Exception ex)
			{
				bool flag2 = true;
				WebException ex2 = ex as WebException;
				if (ex2 != null)
				{
					FtpWebResponse ftpWebResponse = ex2.Response as FtpWebResponse;
					if (ftpWebResponse != null && !this._isFullyRead && ftpWebResponse.StatusCode == FtpStatusCode.ConnectionClosed)
					{
						flag2 = false;
					}
				}
				if (flag2 && (closeState & CloseExState.Silent) == CloseExState.Normal)
				{
					throw;
				}
			}
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x0009E798 File Offset: 0x0009C998
		private void CheckError()
		{
			if (this._request.Aborted)
			{
				throw ExceptionHelper.RequestAbortedException;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x0009E7AD File Offset: 0x0009C9AD
		public override bool CanRead
		{
			get
			{
				return this._readable;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06002DF0 RID: 11760 RVA: 0x0009E7B5 File Offset: 0x0009C9B5
		public override bool CanSeek
		{
			get
			{
				return this._networkStream.CanSeek;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x0009E7C2 File Offset: 0x0009C9C2
		public override bool CanWrite
		{
			get
			{
				return this._writeable;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x0009E7CA File Offset: 0x0009C9CA
		public override long Length
		{
			get
			{
				return this._networkStream.Length;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x0009E7D7 File Offset: 0x0009C9D7
		// (set) Token: 0x06002DF4 RID: 11764 RVA: 0x0009E7E4 File Offset: 0x0009C9E4
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

		// Token: 0x06002DF5 RID: 11765 RVA: 0x0009E7F4 File Offset: 0x0009C9F4
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckError();
			long result;
			try
			{
				result = this._networkStream.Seek(offset, origin);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return result;
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x0009E834 File Offset: 0x0009CA34
		public override int Read(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			int num;
			try
			{
				num = this._networkStream.Read(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			if (num == 0)
			{
				this._isFullyRead = true;
				this.Close();
			}
			return num;
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x0009E884 File Offset: 0x0009CA84
		public override void Write(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			try
			{
				this._networkStream.Write(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x0009E8C0 File Offset: 0x0009CAC0
		private void AsyncReadCallback(IAsyncResult ar)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)ar.AsyncState;
			try
			{
				try
				{
					int num = this._networkStream.EndRead(ar);
					if (num == 0)
					{
						this._isFullyRead = true;
						this.Close();
					}
					lazyAsyncResult.InvokeCallback(num);
				}
				catch (Exception result)
				{
					if (!lazyAsyncResult.IsCompleted)
					{
						lazyAsyncResult.InvokeCallback(result);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x0009E938 File Offset: 0x0009CB38
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this, state, callback);
			try
			{
				this._networkStream.BeginRead(buffer, offset, size, new AsyncCallback(this.AsyncReadCallback), lazyAsyncResult);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return lazyAsyncResult;
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x0009E990 File Offset: 0x0009CB90
		public override int EndRead(IAsyncResult ar)
		{
			int result;
			try
			{
				object obj = ((LazyAsyncResult)ar).InternalWaitForCompletion();
				Exception ex = obj as Exception;
				if (ex != null)
				{
					ExceptionDispatchInfo.Throw(ex);
				}
				result = (int)obj;
			}
			finally
			{
				this.CheckError();
			}
			return result;
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x0009E9D8 File Offset: 0x0009CBD8
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			IAsyncResult result;
			try
			{
				result = this._networkStream.BeginWrite(buffer, offset, size, callback, state);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return result;
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x0009EA1C File Offset: 0x0009CC1C
		public override void EndWrite(IAsyncResult asyncResult)
		{
			try
			{
				this._networkStream.EndWrite(asyncResult);
			}
			finally
			{
				this.CheckError();
			}
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x0009EA50 File Offset: 0x0009CC50
		public override void Flush()
		{
			this._networkStream.Flush();
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x0009EA5D File Offset: 0x0009CC5D
		public override void SetLength(long value)
		{
			this._networkStream.SetLength(value);
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06002DFF RID: 11775 RVA: 0x0009EA6B File Offset: 0x0009CC6B
		public override bool CanTimeout
		{
			get
			{
				return this._networkStream.CanTimeout;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06002E00 RID: 11776 RVA: 0x0009EA78 File Offset: 0x0009CC78
		// (set) Token: 0x06002E01 RID: 11777 RVA: 0x0009EA85 File Offset: 0x0009CC85
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

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06002E02 RID: 11778 RVA: 0x0009EA93 File Offset: 0x0009CC93
		// (set) Token: 0x06002E03 RID: 11779 RVA: 0x0009EAA0 File Offset: 0x0009CCA0
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

		// Token: 0x06002E04 RID: 11780 RVA: 0x0009EAAE File Offset: 0x0009CCAE
		internal void SetSocketTimeoutOption(int timeout)
		{
			this._networkStream.ReadTimeout = timeout;
			this._networkStream.WriteTimeout = timeout;
		}

		// Token: 0x04001942 RID: 6466
		private FtpWebRequest _request;

		// Token: 0x04001943 RID: 6467
		private NetworkStream _networkStream;

		// Token: 0x04001944 RID: 6468
		private bool _writeable;

		// Token: 0x04001945 RID: 6469
		private bool _readable;

		// Token: 0x04001946 RID: 6470
		private bool _isFullyRead;

		// Token: 0x04001947 RID: 6471
		private bool _closing;

		// Token: 0x04001948 RID: 6472
		private const int DefaultCloseTimeout = -1;
	}
}
