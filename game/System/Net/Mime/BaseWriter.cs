using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Mail;
using System.Runtime.ExceptionServices;

namespace System.Net.Mime
{
	// Token: 0x020007F7 RID: 2039
	internal abstract class BaseWriter
	{
		// Token: 0x060040FA RID: 16634 RVA: 0x000DFCE0 File Offset: 0x000DDEE0
		protected BaseWriter(Stream stream, bool shouldEncodeLeadingDots)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this._stream = stream;
			this._shouldEncodeLeadingDots = shouldEncodeLeadingDots;
			this._onCloseHandler = new EventHandler(this.OnClose);
			this._bufferBuilder = new BufferBuilder();
			this._lineLength = 76;
		}

		// Token: 0x060040FB RID: 16635
		internal abstract void WriteHeaders(NameValueCollection headers, bool allowUnicode);

		// Token: 0x060040FC RID: 16636 RVA: 0x000DFD38 File Offset: 0x000DDF38
		internal void WriteHeader(string name, string value, bool allowUnicode)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this._isInContent)
			{
				throw new InvalidOperationException("This operation cannot be performed while in content.");
			}
			this.CheckBoundary();
			this._bufferBuilder.Append(name);
			this._bufferBuilder.Append(": ");
			this.WriteAndFold(value, name.Length + 2, allowUnicode);
			this._bufferBuilder.Append(BaseWriter.s_crlf);
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x000DFDB8 File Offset: 0x000DDFB8
		private void WriteAndFold(string value, int charsAlreadyOnLine, bool allowUnicode)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < value.Length; i++)
			{
				if (MailBnfHelper.IsFWSAt(value, i))
				{
					i += 2;
					this._bufferBuilder.Append(value, num2, i - num2, allowUnicode);
					num2 = i;
					num = i;
					charsAlreadyOnLine = 0;
				}
				else if (i - num2 > this._lineLength - charsAlreadyOnLine && num != num2)
				{
					this._bufferBuilder.Append(value, num2, num - num2, allowUnicode);
					this._bufferBuilder.Append(BaseWriter.s_crlf);
					num2 = num;
					charsAlreadyOnLine = 0;
				}
				else if (value[i] == MailBnfHelper.Space || value[i] == MailBnfHelper.Tab)
				{
					num = i;
				}
			}
			if (value.Length - num2 > 0)
			{
				this._bufferBuilder.Append(value, num2, value.Length - num2, allowUnicode);
			}
		}

		// Token: 0x060040FE RID: 16638 RVA: 0x000DFE7F File Offset: 0x000DE07F
		internal Stream GetContentStream()
		{
			return this.GetContentStream(null);
		}

		// Token: 0x060040FF RID: 16639 RVA: 0x000DFE88 File Offset: 0x000DE088
		private Stream GetContentStream(MultiAsyncResult multiResult)
		{
			if (this._isInContent)
			{
				throw new InvalidOperationException("This operation cannot be performed while in content.");
			}
			this._isInContent = true;
			this.CheckBoundary();
			this._bufferBuilder.Append(BaseWriter.s_crlf);
			this.Flush(multiResult);
			ClosableStream closableStream = new ClosableStream(new EightBitStream(this._stream, this._shouldEncodeLeadingDots), this._onCloseHandler);
			this._contentStream = closableStream;
			return closableStream;
		}

		// Token: 0x06004100 RID: 16640 RVA: 0x000DFEF4 File Offset: 0x000DE0F4
		internal IAsyncResult BeginGetContentStream(AsyncCallback callback, object state)
		{
			MultiAsyncResult multiAsyncResult = new MultiAsyncResult(this, callback, state);
			Stream contentStream = this.GetContentStream(multiAsyncResult);
			if (!(multiAsyncResult.Result is Exception))
			{
				multiAsyncResult.Result = contentStream;
			}
			multiAsyncResult.CompleteSequence();
			return multiAsyncResult;
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x000DFF30 File Offset: 0x000DE130
		internal Stream EndGetContentStream(IAsyncResult result)
		{
			object obj = MultiAsyncResult.End(result);
			Exception ex = obj as Exception;
			if (ex != null)
			{
				ExceptionDispatchInfo.Throw(ex);
			}
			return (Stream)obj;
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x000DFF58 File Offset: 0x000DE158
		protected void Flush(MultiAsyncResult multiResult)
		{
			if (this._bufferBuilder.Length > 0)
			{
				if (multiResult != null)
				{
					multiResult.Enter();
					IAsyncResult asyncResult = this._stream.BeginWrite(this._bufferBuilder.GetBuffer(), 0, this._bufferBuilder.Length, BaseWriter.s_onWrite, multiResult);
					if (asyncResult.CompletedSynchronously)
					{
						this._stream.EndWrite(asyncResult);
						multiResult.Leave();
					}
				}
				else
				{
					this._stream.Write(this._bufferBuilder.GetBuffer(), 0, this._bufferBuilder.Length);
				}
				this._bufferBuilder.Reset();
			}
		}

		// Token: 0x06004103 RID: 16643 RVA: 0x000DFFF0 File Offset: 0x000DE1F0
		protected static void OnWrite(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result.AsyncState;
				BaseWriter baseWriter = (BaseWriter)multiAsyncResult.Context;
				try
				{
					baseWriter._stream.EndWrite(result);
					multiAsyncResult.Leave();
				}
				catch (Exception result2)
				{
					multiAsyncResult.Leave(result2);
				}
			}
		}

		// Token: 0x06004104 RID: 16644
		internal abstract void Close();

		// Token: 0x06004105 RID: 16645
		protected abstract void OnClose(object sender, EventArgs args);

		// Token: 0x06004106 RID: 16646 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void CheckBoundary()
		{
		}

		// Token: 0x06004107 RID: 16647 RVA: 0x000E004C File Offset: 0x000DE24C
		// Note: this type is marked as 'beforefieldinit'.
		static BaseWriter()
		{
		}

		// Token: 0x04002777 RID: 10103
		private const int DefaultLineLength = 76;

		// Token: 0x04002778 RID: 10104
		private static readonly AsyncCallback s_onWrite = new AsyncCallback(BaseWriter.OnWrite);

		// Token: 0x04002779 RID: 10105
		protected static readonly byte[] s_crlf = new byte[]
		{
			13,
			10
		};

		// Token: 0x0400277A RID: 10106
		protected readonly BufferBuilder _bufferBuilder;

		// Token: 0x0400277B RID: 10107
		protected readonly Stream _stream;

		// Token: 0x0400277C RID: 10108
		private readonly EventHandler _onCloseHandler;

		// Token: 0x0400277D RID: 10109
		private readonly bool _shouldEncodeLeadingDots;

		// Token: 0x0400277E RID: 10110
		private int _lineLength;

		// Token: 0x0400277F RID: 10111
		protected Stream _contentStream;

		// Token: 0x04002780 RID: 10112
		protected bool _isInContent;
	}
}
