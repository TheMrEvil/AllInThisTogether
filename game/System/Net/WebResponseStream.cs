using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020006D6 RID: 1750
	internal class WebResponseStream : WebConnectionStream
	{
		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06003835 RID: 14389 RVA: 0x000C6B02 File Offset: 0x000C4D02
		public WebRequestStream RequestStream
		{
			[CompilerGenerated]
			get
			{
				return this.<RequestStream>k__BackingField;
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06003836 RID: 14390 RVA: 0x000C6B0A File Offset: 0x000C4D0A
		// (set) Token: 0x06003837 RID: 14391 RVA: 0x000C6B12 File Offset: 0x000C4D12
		public WebHeaderCollection Headers
		{
			[CompilerGenerated]
			get
			{
				return this.<Headers>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Headers>k__BackingField = value;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06003838 RID: 14392 RVA: 0x000C6B1B File Offset: 0x000C4D1B
		// (set) Token: 0x06003839 RID: 14393 RVA: 0x000C6B23 File Offset: 0x000C4D23
		public HttpStatusCode StatusCode
		{
			[CompilerGenerated]
			get
			{
				return this.<StatusCode>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<StatusCode>k__BackingField = value;
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x0600383A RID: 14394 RVA: 0x000C6B2C File Offset: 0x000C4D2C
		// (set) Token: 0x0600383B RID: 14395 RVA: 0x000C6B34 File Offset: 0x000C4D34
		public string StatusDescription
		{
			[CompilerGenerated]
			get
			{
				return this.<StatusDescription>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<StatusDescription>k__BackingField = value;
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x0600383C RID: 14396 RVA: 0x000C6B3D File Offset: 0x000C4D3D
		// (set) Token: 0x0600383D RID: 14397 RVA: 0x000C6B45 File Offset: 0x000C4D45
		public Version Version
		{
			[CompilerGenerated]
			get
			{
				return this.<Version>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Version>k__BackingField = value;
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x0600383E RID: 14398 RVA: 0x000C6B4E File Offset: 0x000C4D4E
		// (set) Token: 0x0600383F RID: 14399 RVA: 0x000C6B56 File Offset: 0x000C4D56
		public bool KeepAlive
		{
			[CompilerGenerated]
			get
			{
				return this.<KeepAlive>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<KeepAlive>k__BackingField = value;
			}
		}

		// Token: 0x06003840 RID: 14400 RVA: 0x000C6B5F File Offset: 0x000C4D5F
		public WebResponseStream(WebRequestStream request) : base(request.Connection, request.Operation)
		{
			this.RequestStream = request;
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06003841 RID: 14401 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06003842 RID: 14402 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06003843 RID: 14403 RVA: 0x000C6B85 File Offset: 0x000C4D85
		// (set) Token: 0x06003844 RID: 14404 RVA: 0x000C6B8D File Offset: 0x000C4D8D
		private bool ChunkedRead
		{
			[CompilerGenerated]
			get
			{
				return this.<ChunkedRead>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ChunkedRead>k__BackingField = value;
			}
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x000C6B98 File Offset: 0x000C4D98
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			WebResponseStream.<ReadAsync>d__40 <ReadAsync>d__;
			<ReadAsync>d__.<>4__this = this;
			<ReadAsync>d__.buffer = buffer;
			<ReadAsync>d__.offset = offset;
			<ReadAsync>d__.count = count;
			<ReadAsync>d__.cancellationToken = cancellationToken;
			<ReadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadAsync>d__.<>1__state = -1;
			<ReadAsync>d__.<>t__builder.Start<WebResponseStream.<ReadAsync>d__40>(ref <ReadAsync>d__);
			return <ReadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06003846 RID: 14406 RVA: 0x000C6BFC File Offset: 0x000C4DFC
		private Task<int> ProcessRead(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			if (this.read_eof)
			{
				return Task.FromResult<int>(0);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			return HttpWebRequest.RunWithTimeout<int>((CancellationToken ct) => this.innerStream.ReadAsync(buffer, offset, size, ct), this.ReadTimeout, delegate()
			{
				this.Operation.Abort();
				this.innerStream.Dispose();
			}, () => this.Operation.Aborted, cancellationToken);
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x000C6C7C File Offset: 0x000C4E7C
		protected override bool TryReadFromBufferedContent(byte[] buffer, int offset, int count, out int result)
		{
			if (this.bufferedEntireContent)
			{
				BufferedReadStream bufferedReadStream = this.innerStream as BufferedReadStream;
				if (bufferedReadStream != null)
				{
					return bufferedReadStream.TryReadFromBuffer(buffer, offset, count, out result);
				}
			}
			result = 0;
			return false;
		}

		// Token: 0x06003848 RID: 14408 RVA: 0x000C6CB4 File Offset: 0x000C4EB4
		private bool CheckAuthHeader(string headerName)
		{
			string text = this.Headers[headerName];
			return text != null && text.IndexOf("NTLM", StringComparison.Ordinal) != -1;
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06003849 RID: 14409 RVA: 0x000C6CE8 File Offset: 0x000C4EE8
		private bool ExpectContent
		{
			get
			{
				return !(base.Request.Method == "HEAD") && (this.StatusCode >= HttpStatusCode.OK && this.StatusCode != HttpStatusCode.NoContent) && this.StatusCode != HttpStatusCode.NotModified;
			}
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x000C6D3C File Offset: 0x000C4F3C
		private void Initialize(BufferOffsetSize buffer)
		{
			string text = this.Headers["Transfer-Encoding"];
			bool flag = text != null && text.IndexOf("chunked", StringComparison.OrdinalIgnoreCase) != -1;
			string text2 = this.Headers["Content-Length"];
			long maxValue;
			if (!flag && !string.IsNullOrEmpty(text2))
			{
				if (!long.TryParse(text2, out maxValue))
				{
					maxValue = long.MaxValue;
				}
			}
			else
			{
				maxValue = long.MaxValue;
			}
			string text3 = null;
			if (this.ExpectContent)
			{
				text3 = this.Headers["Transfer-Encoding"];
			}
			this.ChunkedRead = (text3 != null && text3.IndexOf("chunked", StringComparison.OrdinalIgnoreCase) != -1);
			if (this.Version == HttpVersion.Version11 && this.RequestStream.KeepAlive)
			{
				this.KeepAlive = true;
				string text4 = this.Headers[base.ServicePoint.UsesProxy ? "Proxy-Connection" : "Connection"];
				if (text4 != null)
				{
					text4 = text4.ToLower();
					this.KeepAlive = (text4.IndexOf("keep-alive", StringComparison.Ordinal) != -1);
					if (text4.IndexOf("close", StringComparison.Ordinal) != -1)
					{
						this.KeepAlive = false;
					}
				}
				if (!this.ChunkedRead && maxValue == 9223372036854775807L)
				{
					this.KeepAlive = false;
				}
			}
			Stream stream;
			if (!this.ExpectContent || (!this.ChunkedRead && (long)buffer.Size >= maxValue))
			{
				this.bufferedEntireContent = true;
				this.innerStream = new BufferedReadStream(base.Operation, null, buffer);
				stream = this.innerStream;
			}
			else if (buffer.Size > 0)
			{
				stream = new BufferedReadStream(base.Operation, this.RequestStream.InnerStream, buffer);
			}
			else
			{
				stream = this.RequestStream.InnerStream;
			}
			if (this.ChunkedRead)
			{
				this.innerStream = new MonoChunkStream(base.Operation, stream, this.Headers);
			}
			else if (!this.bufferedEntireContent)
			{
				if (maxValue != 9223372036854775807L)
				{
					this.innerStream = new FixedSizeReadStream(base.Operation, stream, maxValue);
				}
				else
				{
					this.innerStream = new BufferedReadStream(base.Operation, stream, null);
				}
			}
			string a = this.Headers["Content-Encoding"];
			if (a == "gzip" && (base.Request.AutomaticDecompression & DecompressionMethods.GZip) != DecompressionMethods.None)
			{
				this.innerStream = ContentDecodeStream.Create(base.Operation, this.innerStream, ContentDecodeStream.Mode.GZip);
				this.Headers.Remove(HttpRequestHeader.ContentEncoding);
			}
			else if (a == "deflate" && (base.Request.AutomaticDecompression & DecompressionMethods.Deflate) != DecompressionMethods.None)
			{
				this.innerStream = ContentDecodeStream.Create(base.Operation, this.innerStream, ContentDecodeStream.Mode.Deflate);
				this.Headers.Remove(HttpRequestHeader.ContentEncoding);
			}
			if (!this.ExpectContent)
			{
				this.nextReadCalled = true;
				base.Operation.Finish(true, null);
			}
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x000C7014 File Offset: 0x000C5214
		private Task<byte[]> ReadAllAsyncInner(CancellationToken cancellationToken)
		{
			WebResponseStream.<ReadAllAsyncInner>d__47 <ReadAllAsyncInner>d__;
			<ReadAllAsyncInner>d__.<>4__this = this;
			<ReadAllAsyncInner>d__.cancellationToken = cancellationToken;
			<ReadAllAsyncInner>d__.<>t__builder = AsyncTaskMethodBuilder<byte[]>.Create();
			<ReadAllAsyncInner>d__.<>1__state = -1;
			<ReadAllAsyncInner>d__.<>t__builder.Start<WebResponseStream.<ReadAllAsyncInner>d__47>(ref <ReadAllAsyncInner>d__);
			return <ReadAllAsyncInner>d__.<>t__builder.Task;
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x000C7060 File Offset: 0x000C5260
		internal Task ReadAllAsync(bool resending, CancellationToken cancellationToken)
		{
			WebResponseStream.<ReadAllAsync>d__48 <ReadAllAsync>d__;
			<ReadAllAsync>d__.<>4__this = this;
			<ReadAllAsync>d__.resending = resending;
			<ReadAllAsync>d__.cancellationToken = cancellationToken;
			<ReadAllAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ReadAllAsync>d__.<>1__state = -1;
			<ReadAllAsync>d__.<>t__builder.Start<WebResponseStream.<ReadAllAsync>d__48>(ref <ReadAllAsync>d__);
			return <ReadAllAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x000C70B3 File Offset: 0x000C52B3
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return Task.FromException(new NotSupportedException("The stream does not support writing."));
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x000C70C4 File Offset: 0x000C52C4
		protected override void Close_internal(ref bool disposed)
		{
			if (!this.closed && !this.nextReadCalled)
			{
				this.nextReadCalled = true;
				if (this.read_eof || this.bufferedEntireContent)
				{
					disposed = true;
					WebReadStream webReadStream = this.innerStream;
					if (webReadStream != null)
					{
						webReadStream.Dispose();
					}
					this.innerStream = null;
					base.Operation.Finish(true, null);
					return;
				}
				this.closed = true;
				disposed = true;
				base.Operation.Finish(false, null);
			}
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x000C7138 File Offset: 0x000C5338
		private WebException GetReadException(WebExceptionStatus status, Exception error, string where)
		{
			error = base.GetException(error);
			string.Format("Error getting response stream ({0}): {1}", where, status);
			if (error == null)
			{
				return new WebException(string.Format("Error getting response stream ({0}): {1}", where, status), status);
			}
			WebException ex = error as WebException;
			if (ex != null)
			{
				return ex;
			}
			if (base.Operation.Aborted || error is OperationCanceledException || error is ObjectDisposedException)
			{
				return HttpWebRequest.CreateRequestAbortedException();
			}
			return new WebException(string.Format("Error getting response stream ({0}): {1} {2}", where, status, error.Message), status, WebExceptionInternalStatus.RequestFatal, error);
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x000C71CC File Offset: 0x000C53CC
		internal Task InitReadAsync(CancellationToken cancellationToken)
		{
			WebResponseStream.<InitReadAsync>d__52 <InitReadAsync>d__;
			<InitReadAsync>d__.<>4__this = this;
			<InitReadAsync>d__.cancellationToken = cancellationToken;
			<InitReadAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<InitReadAsync>d__.<>1__state = -1;
			<InitReadAsync>d__.<>t__builder.Start<WebResponseStream.<InitReadAsync>d__52>(ref <InitReadAsync>d__);
			return <InitReadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x000C7218 File Offset: 0x000C5418
		private bool GetResponse(BufferOffsetSize buffer, ref int pos, ref ReadState state)
		{
			string text = null;
			bool flag = false;
			bool flag2 = false;
			while (state != ReadState.Aborted)
			{
				if (state != ReadState.None)
				{
					goto IL_F2;
				}
				if (!WebConnection.ReadLine(buffer.Buffer, ref pos, buffer.Offset, ref text))
				{
					return false;
				}
				if (text == null)
				{
					flag2 = true;
				}
				else
				{
					flag2 = false;
					state = ReadState.Status;
					string[] array = text.Split(' ', StringSplitOptions.None);
					if (array.Length < 2)
					{
						throw this.GetReadException(WebExceptionStatus.ServerProtocolViolation, null, "GetResponse");
					}
					if (string.Compare(array[0], "HTTP/1.1", true) == 0)
					{
						this.Version = HttpVersion.Version11;
						base.ServicePoint.SetVersion(HttpVersion.Version11);
					}
					else
					{
						this.Version = HttpVersion.Version10;
						base.ServicePoint.SetVersion(HttpVersion.Version10);
					}
					this.StatusCode = (HttpStatusCode)uint.Parse(array[1]);
					if (array.Length >= 3)
					{
						this.StatusDescription = string.Join(" ", array, 2, array.Length - 2);
					}
					else
					{
						this.StatusDescription = string.Empty;
					}
					if (pos >= buffer.Offset)
					{
						return true;
					}
					goto IL_F2;
				}
				IL_27F:
				if (!flag2 && !flag)
				{
					throw this.GetReadException(WebExceptionStatus.ServerProtocolViolation, null, "GetResponse");
				}
				continue;
				IL_F2:
				flag2 = false;
				if (state != ReadState.Status)
				{
					goto IL_27F;
				}
				state = ReadState.Headers;
				this.Headers = new WebHeaderCollection();
				List<string> list = new List<string>();
				bool flag3 = false;
				while (!flag3 && WebConnection.ReadLine(buffer.Buffer, ref pos, buffer.Offset, ref text))
				{
					if (text == null)
					{
						flag3 = true;
					}
					else if (text.Length > 0 && (text[0] == ' ' || text[0] == '\t'))
					{
						int num = list.Count - 1;
						if (num < 0)
						{
							break;
						}
						string value = list[num] + text;
						list[num] = value;
					}
					else
					{
						list.Add(text);
					}
				}
				if (!flag3)
				{
					return false;
				}
				foreach (string text2 in list)
				{
					int num2 = text2.IndexOf(':');
					if (num2 == -1)
					{
						throw new ArgumentException("no colon found", "header");
					}
					string name = text2.Substring(0, num2);
					string value2 = text2.Substring(num2 + 1).Trim();
					if (WebHeaderCollection.AllowMultiValues(name))
					{
						this.Headers.AddInternal(name, value2);
					}
					else
					{
						this.Headers.SetInternal(name, value2);
					}
				}
				if (this.StatusCode != HttpStatusCode.Continue)
				{
					state = ReadState.Content;
					return true;
				}
				base.ServicePoint.SendContinue = true;
				if (pos >= buffer.Offset)
				{
					return true;
				}
				if (base.Request.ExpectContinue)
				{
					base.Request.DoContinueDelegate((int)this.StatusCode, this.Headers);
					base.Request.ExpectContinue = false;
				}
				state = ReadState.None;
				flag = true;
				goto IL_27F;
			}
			throw this.GetReadException(WebExceptionStatus.RequestCanceled, null, "GetResponse");
		}

		// Token: 0x04002112 RID: 8466
		private WebReadStream innerStream;

		// Token: 0x04002113 RID: 8467
		private bool nextReadCalled;

		// Token: 0x04002114 RID: 8468
		private bool bufferedEntireContent;

		// Token: 0x04002115 RID: 8469
		private WebCompletionSource pendingRead;

		// Token: 0x04002116 RID: 8470
		private object locker = new object();

		// Token: 0x04002117 RID: 8471
		private int nestedRead;

		// Token: 0x04002118 RID: 8472
		private bool read_eof;

		// Token: 0x04002119 RID: 8473
		[CompilerGenerated]
		private readonly WebRequestStream <RequestStream>k__BackingField;

		// Token: 0x0400211A RID: 8474
		[CompilerGenerated]
		private WebHeaderCollection <Headers>k__BackingField;

		// Token: 0x0400211B RID: 8475
		[CompilerGenerated]
		private HttpStatusCode <StatusCode>k__BackingField;

		// Token: 0x0400211C RID: 8476
		[CompilerGenerated]
		private string <StatusDescription>k__BackingField;

		// Token: 0x0400211D RID: 8477
		[CompilerGenerated]
		private Version <Version>k__BackingField;

		// Token: 0x0400211E RID: 8478
		[CompilerGenerated]
		private bool <KeepAlive>k__BackingField;

		// Token: 0x0400211F RID: 8479
		internal readonly string ME;

		// Token: 0x04002120 RID: 8480
		[CompilerGenerated]
		private bool <ChunkedRead>k__BackingField;

		// Token: 0x020006D7 RID: 1751
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAsync>d__40 : IAsyncStateMachine
		{
			// Token: 0x06003852 RID: 14418 RVA: 0x000C74CC File Offset: 0x000C56CC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebResponseStream webResponseStream = this.<>4__this;
				int result2;
				try
				{
					ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							goto IL_14D;
						}
						this.cancellationToken.ThrowIfCancellationRequested();
						if (this.buffer == null)
						{
							throw new ArgumentNullException("buffer");
						}
						int num2 = this.buffer.Length;
						if (this.offset < 0 || num2 < this.offset)
						{
							throw new ArgumentOutOfRangeException("offset");
						}
						if (this.count < 0 || num2 - this.offset < this.count)
						{
							throw new ArgumentOutOfRangeException("count");
						}
						if (Interlocked.CompareExchange(ref webResponseStream.nestedRead, 1, 0) != 0)
						{
							throw new InvalidOperationException("Invalid nested call.");
						}
						this.<completion>5__2 = new WebCompletionSource();
						goto IL_12F;
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
					}
					IL_127:
					awaiter.GetResult();
					IL_12F:
					if (!this.cancellationToken.IsCancellationRequested)
					{
						WebCompletionSource webCompletionSource = Interlocked.CompareExchange<WebCompletionSource>(ref webResponseStream.pendingRead, this.<completion>5__2, null);
						if (webCompletionSource != null)
						{
							awaiter = webCompletionSource.WaitForCompletion().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, WebResponseStream.<ReadAsync>d__40>(ref awaiter, ref this);
								return;
							}
							goto IL_127;
						}
					}
					this.<nbytes>5__3 = 0;
					this.<throwMe>5__4 = null;
					IL_14D:
					try
					{
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
						if (num != 1)
						{
							awaiter2 = webResponseStream.ProcessRead(this.buffer, this.offset, this.count, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 1);
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, WebResponseStream.<ReadAsync>d__40>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						int result = awaiter2.GetResult();
						this.<nbytes>5__3 = result;
					}
					catch (Exception error)
					{
						this.<throwMe>5__4 = webResponseStream.GetReadException(WebExceptionStatus.ReceiveFailure, error, "ReadAsync");
					}
					object locker;
					bool flag;
					if (this.<throwMe>5__4 != null)
					{
						locker = webResponseStream.locker;
						flag = false;
						try
						{
							Monitor.Enter(locker, ref flag);
							this.<completion>5__2.TrySetException(this.<throwMe>5__4);
							webResponseStream.pendingRead = null;
							webResponseStream.nestedRead = 0;
						}
						finally
						{
							if (num < 0 && flag)
							{
								Monitor.Exit(locker);
							}
						}
						webResponseStream.closed = true;
						webResponseStream.Operation.Finish(false, this.<throwMe>5__4);
						throw this.<throwMe>5__4;
					}
					locker = webResponseStream.locker;
					flag = false;
					try
					{
						Monitor.Enter(locker, ref flag);
						this.<completion>5__2.TrySetCompleted();
						webResponseStream.pendingRead = null;
						webResponseStream.nestedRead = 0;
					}
					finally
					{
						if (num < 0 && flag)
						{
							Monitor.Exit(locker);
						}
					}
					if (this.<nbytes>5__3 <= 0 && !webResponseStream.read_eof)
					{
						webResponseStream.read_eof = true;
						if (!webResponseStream.nextReadCalled && !webResponseStream.nextReadCalled)
						{
							webResponseStream.nextReadCalled = true;
							webResponseStream.Operation.Finish(true, null);
						}
					}
					result2 = this.<nbytes>5__3;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<completion>5__2 = null;
					this.<throwMe>5__4 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<completion>5__2 = null;
				this.<throwMe>5__4 = null;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06003853 RID: 14419 RVA: 0x000C7870 File Offset: 0x000C5A70
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04002121 RID: 8481
			public int <>1__state;

			// Token: 0x04002122 RID: 8482
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04002123 RID: 8483
			public CancellationToken cancellationToken;

			// Token: 0x04002124 RID: 8484
			public byte[] buffer;

			// Token: 0x04002125 RID: 8485
			public int offset;

			// Token: 0x04002126 RID: 8486
			public int count;

			// Token: 0x04002127 RID: 8487
			public WebResponseStream <>4__this;

			// Token: 0x04002128 RID: 8488
			private WebCompletionSource <completion>5__2;

			// Token: 0x04002129 RID: 8489
			private int <nbytes>5__3;

			// Token: 0x0400212A RID: 8490
			private Exception <throwMe>5__4;

			// Token: 0x0400212B RID: 8491
			private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x0400212C RID: 8492
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020006D8 RID: 1752
		[CompilerGenerated]
		private sealed class <>c__DisplayClass41_0
		{
			// Token: 0x06003854 RID: 14420 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass41_0()
			{
			}

			// Token: 0x06003855 RID: 14421 RVA: 0x000C787E File Offset: 0x000C5A7E
			internal Task<int> <ProcessRead>b__0(CancellationToken ct)
			{
				return this.<>4__this.innerStream.ReadAsync(this.buffer, this.offset, this.size, ct);
			}

			// Token: 0x06003856 RID: 14422 RVA: 0x000C78A3 File Offset: 0x000C5AA3
			internal void <ProcessRead>b__1()
			{
				this.<>4__this.Operation.Abort();
				this.<>4__this.innerStream.Dispose();
			}

			// Token: 0x06003857 RID: 14423 RVA: 0x000C78C5 File Offset: 0x000C5AC5
			internal bool <ProcessRead>b__2()
			{
				return this.<>4__this.Operation.Aborted;
			}

			// Token: 0x0400212D RID: 8493
			public WebResponseStream <>4__this;

			// Token: 0x0400212E RID: 8494
			public byte[] buffer;

			// Token: 0x0400212F RID: 8495
			public int offset;

			// Token: 0x04002130 RID: 8496
			public int size;
		}

		// Token: 0x020006D9 RID: 1753
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAllAsyncInner>d__47 : IAsyncStateMachine
		{
			// Token: 0x06003858 RID: 14424 RVA: 0x000C78D8 File Offset: 0x000C5AD8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebResponseStream webResponseStream = this.<>4__this;
				byte[] result2;
				try
				{
					if (num != 0)
					{
						this.<maximumSize>5__2 = (long)HttpWebRequest.DefaultMaximumErrorResponseLength << 16;
						this.<ms>5__3 = new MemoryStream();
					}
					try
					{
						if (num != 0)
						{
							goto IL_F4;
						}
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
						IL_C5:
						int result = awaiter.GetResult();
						if (result < 0)
						{
							throw new IOException();
						}
						if (result == 0)
						{
							goto IL_10A;
						}
						this.<ms>5__3.Write(this.<buffer>5__4, 0, result);
						this.<buffer>5__4 = null;
						IL_F4:
						if (this.<ms>5__3.Position < this.<maximumSize>5__2)
						{
							this.cancellationToken.ThrowIfCancellationRequested();
							this.<buffer>5__4 = new byte[16384];
							awaiter = webResponseStream.ProcessRead(this.<buffer>5__4, 0, this.<buffer>5__4.Length, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, WebResponseStream.<ReadAllAsyncInner>d__47>(ref awaiter, ref this);
								return;
							}
							goto IL_C5;
						}
						IL_10A:
						result2 = this.<ms>5__3.ToArray();
					}
					finally
					{
						if (num < 0 && this.<ms>5__3 != null)
						{
							((IDisposable)this.<ms>5__3).Dispose();
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06003859 RID: 14425 RVA: 0x000C7A78 File Offset: 0x000C5C78
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04002131 RID: 8497
			public int <>1__state;

			// Token: 0x04002132 RID: 8498
			public AsyncTaskMethodBuilder<byte[]> <>t__builder;

			// Token: 0x04002133 RID: 8499
			public CancellationToken cancellationToken;

			// Token: 0x04002134 RID: 8500
			public WebResponseStream <>4__this;

			// Token: 0x04002135 RID: 8501
			private long <maximumSize>5__2;

			// Token: 0x04002136 RID: 8502
			private MemoryStream <ms>5__3;

			// Token: 0x04002137 RID: 8503
			private byte[] <buffer>5__4;

			// Token: 0x04002138 RID: 8504
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020006DA RID: 1754
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAllAsync>d__48 : IAsyncStateMachine
		{
			// Token: 0x0600385A RID: 14426 RVA: 0x000C7A88 File Offset: 0x000C5C88
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebResponseStream webResponseStream = this.<>4__this;
				try
				{
					if (num != 0)
					{
						if (num == 1)
						{
							goto IL_16B;
						}
						if (webResponseStream.read_eof || webResponseStream.bufferedEntireContent || webResponseStream.nextReadCalled)
						{
							if (!webResponseStream.nextReadCalled)
							{
								webResponseStream.nextReadCalled = true;
								webResponseStream.Operation.Finish(true, null);
							}
							goto IL_2B3;
						}
						this.<completion>5__2 = new WebCompletionSource();
						this.<timeoutCts>5__3 = new CancellationTokenSource();
					}
					try
					{
						ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter awaiter;
						if (num == 0)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_128;
						}
						this.<timeoutTask>5__4 = Task.Delay(webResponseStream.ReadTimeout, this.<timeoutCts>5__3.Token);
						IL_8A:
						this.cancellationToken.ThrowIfCancellationRequested();
						WebCompletionSource webCompletionSource = Interlocked.CompareExchange<WebCompletionSource>(ref webResponseStream.pendingRead, this.<completion>5__2, null);
						if (webCompletionSource == null)
						{
							this.<timeoutTask>5__4 = null;
							goto IL_16B;
						}
						Task<object> task = webCompletionSource.WaitForCompletion();
						awaiter = Task.WhenAny(new Task[]
						{
							task,
							this.<timeoutTask>5__4
						}).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter, WebResponseStream.<ReadAllAsync>d__48>(ref awaiter, ref this);
							return;
						}
						IL_128:
						if (awaiter.GetResult() == this.<timeoutTask>5__4)
						{
							throw new WebException("The operation has timed out.", WebExceptionStatus.Timeout);
						}
						goto IL_8A;
					}
					finally
					{
						if (num < 0)
						{
							this.<timeoutCts>5__3.Cancel();
							this.<timeoutCts>5__3.Dispose();
						}
					}
					IL_16B:
					try
					{
						ConfiguredTaskAwaitable<byte[]>.ConfiguredTaskAwaiter awaiter2;
						if (num != 1)
						{
							this.cancellationToken.ThrowIfCancellationRequested();
							if (webResponseStream.read_eof || webResponseStream.bufferedEntireContent)
							{
								goto IL_2B3;
							}
							if (this.resending && !webResponseStream.KeepAlive)
							{
								webResponseStream.Close();
								goto IL_2B3;
							}
							awaiter2 = webResponseStream.ReadAllAsyncInner(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 1);
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<byte[]>.ConfiguredTaskAwaiter, WebResponseStream.<ReadAllAsync>d__48>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<byte[]>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						byte[] result = awaiter2.GetResult();
						BufferOffsetSize readBuffer = new BufferOffsetSize(result, 0, result.Length, false);
						webResponseStream.innerStream = new BufferedReadStream(webResponseStream.Operation, null, readBuffer);
						webResponseStream.bufferedEntireContent = true;
						webResponseStream.nextReadCalled = true;
						this.<completion>5__2.TrySetCompleted();
					}
					catch (Exception error)
					{
						this.<completion>5__2.TrySetException(error);
						throw;
					}
					finally
					{
						if (num < 0)
						{
							webResponseStream.pendingRead = null;
						}
					}
					webResponseStream.Operation.Finish(true, null);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<completion>5__2 = null;
					this.<timeoutCts>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_2B3:
				this.<>1__state = -2;
				this.<completion>5__2 = null;
				this.<timeoutCts>5__3 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600385B RID: 14427 RVA: 0x000C7DD0 File Offset: 0x000C5FD0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04002139 RID: 8505
			public int <>1__state;

			// Token: 0x0400213A RID: 8506
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400213B RID: 8507
			public WebResponseStream <>4__this;

			// Token: 0x0400213C RID: 8508
			public CancellationToken cancellationToken;

			// Token: 0x0400213D RID: 8509
			public bool resending;

			// Token: 0x0400213E RID: 8510
			private WebCompletionSource <completion>5__2;

			// Token: 0x0400213F RID: 8511
			private CancellationTokenSource <timeoutCts>5__3;

			// Token: 0x04002140 RID: 8512
			private Task <timeoutTask>5__4;

			// Token: 0x04002141 RID: 8513
			private ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04002142 RID: 8514
			private ConfiguredTaskAwaitable<byte[]>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020006DB RID: 1755
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InitReadAsync>d__52 : IAsyncStateMachine
		{
			// Token: 0x0600385C RID: 14428 RVA: 0x000C7DE0 File Offset: 0x000C5FE0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebResponseStream webResponseStream = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_DB;
					}
					this.<buffer>5__2 = new BufferOffsetSize(new byte[4096], false);
					this.<state>5__3 = ReadState.None;
					this.<position>5__4 = 0;
					IL_38:
					webResponseStream.Operation.ThrowIfClosedOrDisposed(this.cancellationToken);
					awaiter = webResponseStream.RequestStream.InnerStream.ReadAsync(this.<buffer>5__2.Buffer, this.<buffer>5__2.Offset, this.<buffer>5__2.Size, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, WebResponseStream.<InitReadAsync>d__52>(ref awaiter, ref this);
						return;
					}
					IL_DB:
					int result = awaiter.GetResult();
					if (result == 0)
					{
						throw webResponseStream.GetReadException(WebExceptionStatus.ReceiveFailure, null, "ReadDoneAsync2");
					}
					if (result < 0)
					{
						throw webResponseStream.GetReadException(WebExceptionStatus.ServerProtocolViolation, null, "ReadDoneAsync3");
					}
					this.<buffer>5__2.Offset += result;
					this.<buffer>5__2.Size -= result;
					if (this.<state>5__3 == ReadState.None)
					{
						try
						{
							int num2 = this.<position>5__4;
							if (!webResponseStream.GetResponse(this.<buffer>5__2, ref this.<position>5__4, ref this.<state>5__3))
							{
								this.<position>5__4 = num2;
							}
						}
						catch (Exception error)
						{
							throw webResponseStream.GetReadException(WebExceptionStatus.ServerProtocolViolation, error, "ReadDoneAsync4");
						}
					}
					if (this.<state>5__3 == ReadState.Aborted)
					{
						throw webResponseStream.GetReadException(WebExceptionStatus.RequestCanceled, null, "ReadDoneAsync5");
					}
					if (this.<state>5__3 != ReadState.Content)
					{
						int num3 = result * 2;
						if (num3 > this.<buffer>5__2.Size)
						{
							byte[] array = new byte[this.<buffer>5__2.Buffer.Length + num3];
							Buffer.BlockCopy(this.<buffer>5__2.Buffer, 0, array, 0, this.<buffer>5__2.Offset);
							this.<buffer>5__2 = new BufferOffsetSize(array, this.<buffer>5__2.Offset, array.Length - this.<buffer>5__2.Offset, false);
						}
						this.<state>5__3 = ReadState.None;
						this.<position>5__4 = 0;
						goto IL_38;
					}
					this.<buffer>5__2.Size = this.<buffer>5__2.Offset - this.<position>5__4;
					this.<buffer>5__2.Offset = this.<position>5__4;
					try
					{
						webResponseStream.Initialize(this.<buffer>5__2);
					}
					catch (Exception error2)
					{
						throw webResponseStream.GetReadException(WebExceptionStatus.ReceiveFailure, error2, "ReadDoneAsync6");
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<buffer>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<buffer>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600385D RID: 14429 RVA: 0x000C80E0 File Offset: 0x000C62E0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04002143 RID: 8515
			public int <>1__state;

			// Token: 0x04002144 RID: 8516
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04002145 RID: 8517
			public WebResponseStream <>4__this;

			// Token: 0x04002146 RID: 8518
			public CancellationToken cancellationToken;

			// Token: 0x04002147 RID: 8519
			private BufferOffsetSize <buffer>5__2;

			// Token: 0x04002148 RID: 8520
			private ReadState <state>5__3;

			// Token: 0x04002149 RID: 8521
			private int <position>5__4;

			// Token: 0x0400214A RID: 8522
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
