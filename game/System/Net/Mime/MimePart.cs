using System;
using System.IO;
using System.Net.Mail;
using System.Runtime.ExceptionServices;

namespace System.Net.Mime
{
	// Token: 0x02000809 RID: 2057
	internal class MimePart : MimeBasePart, IDisposable
	{
		// Token: 0x0600417C RID: 16764 RVA: 0x000E1D78 File Offset: 0x000DFF78
		internal MimePart()
		{
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x000E1D80 File Offset: 0x000DFF80
		public void Dispose()
		{
			if (this._stream != null)
			{
				this._stream.Close();
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x0600417E RID: 16766 RVA: 0x000E1D95 File Offset: 0x000DFF95
		internal Stream Stream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x0600417F RID: 16767 RVA: 0x000E1D9D File Offset: 0x000DFF9D
		// (set) Token: 0x06004180 RID: 16768 RVA: 0x000E1DA5 File Offset: 0x000DFFA5
		internal ContentDisposition ContentDisposition
		{
			get
			{
				return this._contentDisposition;
			}
			set
			{
				this._contentDisposition = value;
				if (value == null)
				{
					((HeaderCollection)base.Headers).InternalRemove(MailHeaderInfo.GetString(MailHeaderID.ContentDisposition));
					return;
				}
				this._contentDisposition.PersistIfNeeded((HeaderCollection)base.Headers, true);
			}
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06004181 RID: 16769 RVA: 0x000E1DE0 File Offset: 0x000DFFE0
		// (set) Token: 0x06004182 RID: 16770 RVA: 0x000E1E40 File Offset: 0x000E0040
		internal TransferEncoding TransferEncoding
		{
			get
			{
				string text = base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)];
				if (text.Equals("base64", StringComparison.OrdinalIgnoreCase))
				{
					return TransferEncoding.Base64;
				}
				if (text.Equals("quoted-printable", StringComparison.OrdinalIgnoreCase))
				{
					return TransferEncoding.QuotedPrintable;
				}
				if (text.Equals("7bit", StringComparison.OrdinalIgnoreCase))
				{
					return TransferEncoding.SevenBit;
				}
				if (text.Equals("8bit", StringComparison.OrdinalIgnoreCase))
				{
					return TransferEncoding.EightBit;
				}
				return TransferEncoding.Unknown;
			}
			set
			{
				if (value == TransferEncoding.Base64)
				{
					base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)] = "base64";
					return;
				}
				if (value == TransferEncoding.QuotedPrintable)
				{
					base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)] = "quoted-printable";
					return;
				}
				if (value == TransferEncoding.SevenBit)
				{
					base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)] = "7bit";
					return;
				}
				if (value == TransferEncoding.EightBit)
				{
					base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)] = "8bit";
					return;
				}
				throw new NotSupportedException(SR.Format("The MIME transfer encoding '{0}' is not supported.", value));
			}
		}

		// Token: 0x06004183 RID: 16771 RVA: 0x000E1ED0 File Offset: 0x000E00D0
		internal void SetContent(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (this._streamSet)
			{
				this._stream.Close();
				this._stream = null;
				this._streamSet = false;
			}
			this._stream = stream;
			this._streamSet = true;
			this._streamUsedOnce = false;
			this.TransferEncoding = TransferEncoding.Base64;
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x000E1F28 File Offset: 0x000E0128
		internal void SetContent(Stream stream, string name, string mimeType)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (mimeType != null && mimeType != string.Empty)
			{
				this._contentType = new ContentType(mimeType);
			}
			if (name != null && name != string.Empty)
			{
				base.ContentType.Name = name;
			}
			this.SetContent(stream);
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x000E1F82 File Offset: 0x000E0182
		internal void SetContent(Stream stream, ContentType contentType)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this._contentType = contentType;
			this.SetContent(stream);
		}

		// Token: 0x06004186 RID: 16774 RVA: 0x000E1FA0 File Offset: 0x000E01A0
		internal void Complete(IAsyncResult result, Exception e)
		{
			MimePart.MimePartContext mimePartContext = (MimePart.MimePartContext)result.AsyncState;
			if (mimePartContext._completed)
			{
				ExceptionDispatchInfo.Throw(e);
			}
			try
			{
				if (mimePartContext._outputStream != null)
				{
					mimePartContext._outputStream.Close();
				}
			}
			catch (Exception ex)
			{
				if (e == null)
				{
					e = ex;
				}
			}
			mimePartContext._completed = true;
			mimePartContext._result.InvokeCallback(e);
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x000E200C File Offset: 0x000E020C
		internal void ReadCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimePart.MimePartContext)result.AsyncState)._completedSynchronously = false;
			try
			{
				this.ReadCallbackHandler(result);
			}
			catch (Exception e)
			{
				this.Complete(result, e);
			}
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x000E2058 File Offset: 0x000E0258
		internal void ReadCallbackHandler(IAsyncResult result)
		{
			MimePart.MimePartContext mimePartContext = (MimePart.MimePartContext)result.AsyncState;
			mimePartContext._bytesLeft = this.Stream.EndRead(result);
			if (mimePartContext._bytesLeft > 0)
			{
				IAsyncResult asyncResult = mimePartContext._outputStream.BeginWrite(mimePartContext._buffer, 0, mimePartContext._bytesLeft, this._writeCallback, mimePartContext);
				if (asyncResult.CompletedSynchronously)
				{
					this.WriteCallbackHandler(asyncResult);
					return;
				}
			}
			else
			{
				this.Complete(result, null);
			}
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x000E20C4 File Offset: 0x000E02C4
		internal void WriteCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimePart.MimePartContext)result.AsyncState)._completedSynchronously = false;
			try
			{
				this.WriteCallbackHandler(result);
			}
			catch (Exception e)
			{
				this.Complete(result, e);
			}
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x000E2110 File Offset: 0x000E0310
		internal void WriteCallbackHandler(IAsyncResult result)
		{
			MimePart.MimePartContext mimePartContext = (MimePart.MimePartContext)result.AsyncState;
			mimePartContext._outputStream.EndWrite(result);
			IAsyncResult asyncResult = this.Stream.BeginRead(mimePartContext._buffer, 0, mimePartContext._buffer.Length, this._readCallback, mimePartContext);
			if (asyncResult.CompletedSynchronously)
			{
				this.ReadCallbackHandler(asyncResult);
			}
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x000E2168 File Offset: 0x000E0368
		internal Stream GetEncodedStream(Stream stream)
		{
			Stream stream2 = stream;
			if (this.TransferEncoding == TransferEncoding.Base64)
			{
				stream2 = new Base64Stream(stream2, new Base64WriteStateInfo());
			}
			else if (this.TransferEncoding == TransferEncoding.QuotedPrintable)
			{
				stream2 = new QuotedPrintableStream(stream2, true);
			}
			else if (this.TransferEncoding == TransferEncoding.SevenBit || this.TransferEncoding == TransferEncoding.EightBit)
			{
				stream2 = new EightBitStream(stream2);
			}
			return stream2;
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x000E21BC File Offset: 0x000E03BC
		internal void ContentStreamCallbackHandler(IAsyncResult result)
		{
			MimePart.MimePartContext mimePartContext = (MimePart.MimePartContext)result.AsyncState;
			Stream stream = mimePartContext._writer.EndGetContentStream(result);
			mimePartContext._outputStream = this.GetEncodedStream(stream);
			this._readCallback = new AsyncCallback(this.ReadCallback);
			this._writeCallback = new AsyncCallback(this.WriteCallback);
			IAsyncResult asyncResult = this.Stream.BeginRead(mimePartContext._buffer, 0, mimePartContext._buffer.Length, this._readCallback, mimePartContext);
			if (asyncResult.CompletedSynchronously)
			{
				this.ReadCallbackHandler(asyncResult);
			}
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x000E2244 File Offset: 0x000E0444
		internal void ContentStreamCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimePart.MimePartContext)result.AsyncState)._completedSynchronously = false;
			try
			{
				this.ContentStreamCallbackHandler(result);
			}
			catch (Exception e)
			{
				this.Complete(result, e);
			}
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x000E2290 File Offset: 0x000E0490
		internal override IAsyncResult BeginSend(BaseWriter writer, AsyncCallback callback, bool allowUnicode, object state)
		{
			base.PrepareHeaders(allowUnicode);
			writer.WriteHeaders(base.Headers, allowUnicode);
			MimeBasePart.MimePartAsyncResult result = new MimeBasePart.MimePartAsyncResult(this, state, callback);
			MimePart.MimePartContext state2 = new MimePart.MimePartContext(writer, result);
			this.ResetStream();
			this._streamUsedOnce = true;
			IAsyncResult asyncResult = writer.BeginGetContentStream(new AsyncCallback(this.ContentStreamCallback), state2);
			if (asyncResult.CompletedSynchronously)
			{
				this.ContentStreamCallbackHandler(asyncResult);
			}
			return result;
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x000E22F4 File Offset: 0x000E04F4
		internal override void Send(BaseWriter writer, bool allowUnicode)
		{
			if (this.Stream != null)
			{
				byte[] buffer = new byte[17408];
				base.PrepareHeaders(allowUnicode);
				writer.WriteHeaders(base.Headers, allowUnicode);
				Stream stream = writer.GetContentStream();
				stream = this.GetEncodedStream(stream);
				this.ResetStream();
				this._streamUsedOnce = true;
				int count;
				while ((count = this.Stream.Read(buffer, 0, 17408)) > 0)
				{
					stream.Write(buffer, 0, count);
				}
				stream.Close();
			}
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x000E236C File Offset: 0x000E056C
		internal void ResetStream()
		{
			if (!this._streamUsedOnce)
			{
				return;
			}
			if (this.Stream.CanSeek)
			{
				this.Stream.Seek(0L, SeekOrigin.Begin);
				this._streamUsedOnce = false;
				return;
			}
			throw new InvalidOperationException("One of the streams has already been used and can't be reset to the origin.");
		}

		// Token: 0x040027C2 RID: 10178
		private Stream _stream;

		// Token: 0x040027C3 RID: 10179
		private bool _streamSet;

		// Token: 0x040027C4 RID: 10180
		private bool _streamUsedOnce;

		// Token: 0x040027C5 RID: 10181
		private AsyncCallback _readCallback;

		// Token: 0x040027C6 RID: 10182
		private AsyncCallback _writeCallback;

		// Token: 0x040027C7 RID: 10183
		private const int maxBufferSize = 17408;

		// Token: 0x0200080A RID: 2058
		internal class MimePartContext
		{
			// Token: 0x06004191 RID: 16785 RVA: 0x000E23A5 File Offset: 0x000E05A5
			internal MimePartContext(BaseWriter writer, LazyAsyncResult result)
			{
				this._writer = writer;
				this._result = result;
				this._buffer = new byte[17408];
			}

			// Token: 0x040027C8 RID: 10184
			internal Stream _outputStream;

			// Token: 0x040027C9 RID: 10185
			internal LazyAsyncResult _result;

			// Token: 0x040027CA RID: 10186
			internal int _bytesLeft;

			// Token: 0x040027CB RID: 10187
			internal BaseWriter _writer;

			// Token: 0x040027CC RID: 10188
			internal byte[] _buffer;

			// Token: 0x040027CD RID: 10189
			internal bool _completed;

			// Token: 0x040027CE RID: 10190
			internal bool _completedSynchronously = true;
		}
	}
}
