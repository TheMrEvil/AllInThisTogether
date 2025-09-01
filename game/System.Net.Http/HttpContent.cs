using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http
{
	/// <summary>A base class representing an HTTP entity body and content headers.</summary>
	// Token: 0x0200001F RID: 31
	public abstract class HttpContent : IDisposable
	{
		/// <summary>Gets the HTTP content headers as defined in RFC 2616.</summary>
		/// <returns>The content headers as defined in RFC 2616.</returns>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00004F24 File Offset: 0x00003124
		public HttpContentHeaders Headers
		{
			get
			{
				HttpContentHeaders result;
				if ((result = this.headers) == null)
				{
					result = (this.headers = new HttpContentHeaders(this));
				}
				return result;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00004F4C File Offset: 0x0000314C
		internal long? LoadedBufferLength
		{
			get
			{
				if (this.buffer != null)
				{
					return new long?(this.buffer.Length);
				}
				return null;
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004F7B File Offset: 0x0000317B
		internal void CopyTo(Stream stream)
		{
			this.CopyToAsync(stream).Wait();
		}

		/// <summary>Serialize the HTTP content into a stream of bytes and copies it to the stream object provided as the <paramref name="stream" /> parameter.</summary>
		/// <param name="stream">The target stream.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06000111 RID: 273 RVA: 0x00004F89 File Offset: 0x00003189
		public Task CopyToAsync(Stream stream)
		{
			return this.CopyToAsync(stream, null);
		}

		/// <summary>Serialize the HTTP content into a stream of bytes and copies it to the stream object provided as the <paramref name="stream" /> parameter.</summary>
		/// <param name="stream">The target stream.</param>
		/// <param name="context">Information about the transport (channel binding token, for example). This parameter may be <see langword="null" />.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06000112 RID: 274 RVA: 0x00004F93 File Offset: 0x00003193
		public Task CopyToAsync(Stream stream, TransportContext context)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (this.buffer != null)
			{
				return this.buffer.CopyToAsync(stream);
			}
			return this.SerializeToStreamAsync(stream, context);
		}

		/// <summary>Serialize the HTTP content to a memory stream as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06000113 RID: 275 RVA: 0x00004FC0 File Offset: 0x000031C0
		protected virtual Task<Stream> CreateContentReadStreamAsync()
		{
			HttpContent.<CreateContentReadStreamAsync>d__12 <CreateContentReadStreamAsync>d__;
			<CreateContentReadStreamAsync>d__.<>4__this = this;
			<CreateContentReadStreamAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Stream>.Create();
			<CreateContentReadStreamAsync>d__.<>1__state = -1;
			<CreateContentReadStreamAsync>d__.<>t__builder.Start<HttpContent.<CreateContentReadStreamAsync>d__12>(ref <CreateContentReadStreamAsync>d__);
			return <CreateContentReadStreamAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005003 File Offset: 0x00003203
		private static HttpContent.FixedMemoryStream CreateFixedMemoryStream(long maxBufferSize)
		{
			return new HttpContent.FixedMemoryStream(maxBufferSize);
		}

		/// <summary>Releases the unmanaged resources and disposes of the managed resources used by the <see cref="T:System.Net.Http.HttpContent" />.</summary>
		// Token: 0x06000115 RID: 277 RVA: 0x0000500B File Offset: 0x0000320B
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.HttpContent" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		// Token: 0x06000116 RID: 278 RVA: 0x00005014 File Offset: 0x00003214
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				if (this.buffer != null)
				{
					this.buffer.Dispose();
				}
			}
		}

		/// <summary>Serialize the HTTP content to a memory buffer as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06000117 RID: 279 RVA: 0x0000503B File Offset: 0x0000323B
		public Task LoadIntoBufferAsync()
		{
			return this.LoadIntoBufferAsync(2147483647L);
		}

		/// <summary>Serialize the HTTP content to a memory buffer as an asynchronous operation.</summary>
		/// <param name="maxBufferSize">The maximum size, in bytes, of the buffer to use.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06000118 RID: 280 RVA: 0x0000504C File Offset: 0x0000324C
		public Task LoadIntoBufferAsync(long maxBufferSize)
		{
			HttpContent.<LoadIntoBufferAsync>d__17 <LoadIntoBufferAsync>d__;
			<LoadIntoBufferAsync>d__.<>4__this = this;
			<LoadIntoBufferAsync>d__.maxBufferSize = maxBufferSize;
			<LoadIntoBufferAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadIntoBufferAsync>d__.<>1__state = -1;
			<LoadIntoBufferAsync>d__.<>t__builder.Start<HttpContent.<LoadIntoBufferAsync>d__17>(ref <LoadIntoBufferAsync>d__);
			return <LoadIntoBufferAsync>d__.<>t__builder.Task;
		}

		/// <summary>Serialize the HTTP content and return a stream that represents the content as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06000119 RID: 281 RVA: 0x00005098 File Offset: 0x00003298
		public Task<Stream> ReadAsStreamAsync()
		{
			HttpContent.<ReadAsStreamAsync>d__18 <ReadAsStreamAsync>d__;
			<ReadAsStreamAsync>d__.<>4__this = this;
			<ReadAsStreamAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Stream>.Create();
			<ReadAsStreamAsync>d__.<>1__state = -1;
			<ReadAsStreamAsync>d__.<>t__builder.Start<HttpContent.<ReadAsStreamAsync>d__18>(ref <ReadAsStreamAsync>d__);
			return <ReadAsStreamAsync>d__.<>t__builder.Task;
		}

		/// <summary>Serialize the HTTP content to a byte array as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x0600011A RID: 282 RVA: 0x000050DC File Offset: 0x000032DC
		public Task<byte[]> ReadAsByteArrayAsync()
		{
			HttpContent.<ReadAsByteArrayAsync>d__19 <ReadAsByteArrayAsync>d__;
			<ReadAsByteArrayAsync>d__.<>4__this = this;
			<ReadAsByteArrayAsync>d__.<>t__builder = AsyncTaskMethodBuilder<byte[]>.Create();
			<ReadAsByteArrayAsync>d__.<>1__state = -1;
			<ReadAsByteArrayAsync>d__.<>t__builder.Start<HttpContent.<ReadAsByteArrayAsync>d__19>(ref <ReadAsByteArrayAsync>d__);
			return <ReadAsByteArrayAsync>d__.<>t__builder.Task;
		}

		/// <summary>Serialize the HTTP content to a string as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x0600011B RID: 283 RVA: 0x00005120 File Offset: 0x00003320
		public Task<string> ReadAsStringAsync()
		{
			HttpContent.<ReadAsStringAsync>d__20 <ReadAsStringAsync>d__;
			<ReadAsStringAsync>d__.<>4__this = this;
			<ReadAsStringAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadAsStringAsync>d__.<>1__state = -1;
			<ReadAsStringAsync>d__.<>t__builder.Start<HttpContent.<ReadAsStringAsync>d__20>(ref <ReadAsStringAsync>d__);
			return <ReadAsStringAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005164 File Offset: 0x00003364
		private static Encoding GetEncodingFromBuffer(byte[] buffer, int length, ref int preambleLength)
		{
			foreach (Encoding encoding in new Encoding[]
			{
				Encoding.UTF8,
				Encoding.UTF32,
				Encoding.Unicode
			})
			{
				if ((preambleLength = HttpContent.StartsWith(buffer, length, encoding.GetPreamble())) != 0)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000051BC File Offset: 0x000033BC
		private static int StartsWith(byte[] array, int length, byte[] value)
		{
			if (length < value.Length)
			{
				return 0;
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (array[i] != value[i])
				{
					return 0;
				}
			}
			return value.Length;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000051EC File Offset: 0x000033EC
		internal Task SerializeToStreamAsync_internal(Stream stream, TransportContext context)
		{
			return this.SerializeToStreamAsync(stream, context);
		}

		/// <summary>Serialize the HTTP content to a stream as an asynchronous operation.</summary>
		/// <param name="stream">The target stream.</param>
		/// <param name="context">Information about the transport (channel binding token, for example). This parameter may be <see langword="null" />.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x0600011F RID: 287
		protected abstract Task SerializeToStreamAsync(Stream stream, TransportContext context);

		/// <summary>Determines whether the HTTP content has a valid length in bytes.</summary>
		/// <param name="length">The length in bytes of the HTTP content.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="length" /> is a valid length; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000120 RID: 288
		protected internal abstract bool TryComputeLength(out long length);

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpContent" /> class.</summary>
		// Token: 0x06000121 RID: 289 RVA: 0x000022B8 File Offset: 0x000004B8
		protected HttpContent()
		{
		}

		// Token: 0x0400008F RID: 143
		private HttpContent.FixedMemoryStream buffer;

		// Token: 0x04000090 RID: 144
		private Stream stream;

		// Token: 0x04000091 RID: 145
		private bool disposed;

		// Token: 0x04000092 RID: 146
		private HttpContentHeaders headers;

		// Token: 0x02000020 RID: 32
		private sealed class FixedMemoryStream : MemoryStream
		{
			// Token: 0x06000122 RID: 290 RVA: 0x000051F6 File Offset: 0x000033F6
			public FixedMemoryStream(long maxSize)
			{
				this.maxSize = maxSize;
			}

			// Token: 0x06000123 RID: 291 RVA: 0x00005205 File Offset: 0x00003405
			private void CheckOverflow(int count)
			{
				if (this.Length + (long)count > this.maxSize)
				{
					throw new HttpRequestException(string.Format("Cannot write more bytes to the buffer than the configured maximum buffer size: {0}", this.maxSize));
				}
			}

			// Token: 0x06000124 RID: 292 RVA: 0x00005233 File Offset: 0x00003433
			public override void WriteByte(byte value)
			{
				this.CheckOverflow(1);
				base.WriteByte(value);
			}

			// Token: 0x06000125 RID: 293 RVA: 0x00005243 File Offset: 0x00003443
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.CheckOverflow(count);
				base.Write(buffer, offset, count);
			}

			// Token: 0x04000093 RID: 147
			private readonly long maxSize;
		}

		// Token: 0x02000021 RID: 33
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <CreateContentReadStreamAsync>d__12 : IAsyncStateMachine
		{
			// Token: 0x06000126 RID: 294 RVA: 0x00005258 File Offset: 0x00003458
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				HttpContent httpContent = this.<>4__this;
				Stream buffer;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = httpContent.LoadIntoBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, HttpContent.<CreateContentReadStreamAsync>d__12>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					buffer = httpContent.buffer;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(buffer);
			}

			// Token: 0x06000127 RID: 295 RVA: 0x00005320 File Offset: 0x00003520
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000094 RID: 148
			public int <>1__state;

			// Token: 0x04000095 RID: 149
			public AsyncTaskMethodBuilder<Stream> <>t__builder;

			// Token: 0x04000096 RID: 150
			public HttpContent <>4__this;

			// Token: 0x04000097 RID: 151
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000022 RID: 34
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <LoadIntoBufferAsync>d__17 : IAsyncStateMachine
		{
			// Token: 0x06000128 RID: 296 RVA: 0x00005330 File Offset: 0x00003530
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				HttpContent httpContent = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (httpContent.disposed)
						{
							throw new ObjectDisposedException(httpContent.GetType().ToString());
						}
						if (httpContent.buffer != null)
						{
							goto IL_DA;
						}
						httpContent.buffer = HttpContent.CreateFixedMemoryStream(this.maxBufferSize);
						awaiter = httpContent.SerializeToStreamAsync(httpContent.buffer, null).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, HttpContent.<LoadIntoBufferAsync>d__17>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					httpContent.buffer.Seek(0L, SeekOrigin.Begin);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_DA:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000129 RID: 297 RVA: 0x0000543C File Offset: 0x0000363C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000098 RID: 152
			public int <>1__state;

			// Token: 0x04000099 RID: 153
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400009A RID: 154
			public HttpContent <>4__this;

			// Token: 0x0400009B RID: 155
			public long maxBufferSize;

			// Token: 0x0400009C RID: 156
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000023 RID: 35
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAsStreamAsync>d__18 : IAsyncStateMachine
		{
			// Token: 0x0600012A RID: 298 RVA: 0x0000544C File Offset: 0x0000364C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				HttpContent httpContent = this.<>4__this;
				Stream result;
				try
				{
					ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (httpContent.disposed)
						{
							throw new ObjectDisposedException(httpContent.GetType().ToString());
						}
						if (httpContent.buffer != null)
						{
							result = new MemoryStream(httpContent.buffer.GetBuffer(), 0, (int)httpContent.buffer.Length, false);
							goto IL_F0;
						}
						if (httpContent.stream != null)
						{
							goto IL_CE;
						}
						awaiter = httpContent.CreateContentReadStreamAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter, HttpContent.<ReadAsStreamAsync>d__18>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					Stream result2 = awaiter.GetResult();
					httpContent.stream = result2;
					IL_CE:
					result = httpContent.stream;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_F0:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600012B RID: 299 RVA: 0x00005570 File Offset: 0x00003770
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400009D RID: 157
			public int <>1__state;

			// Token: 0x0400009E RID: 158
			public AsyncTaskMethodBuilder<Stream> <>t__builder;

			// Token: 0x0400009F RID: 159
			public HttpContent <>4__this;

			// Token: 0x040000A0 RID: 160
			private ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000024 RID: 36
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAsByteArrayAsync>d__19 : IAsyncStateMachine
		{
			// Token: 0x0600012C RID: 300 RVA: 0x00005580 File Offset: 0x00003780
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				HttpContent httpContent = this.<>4__this;
				byte[] result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = httpContent.LoadIntoBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, HttpContent.<ReadAsByteArrayAsync>d__19>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					result = httpContent.buffer.ToArray();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600012D RID: 301 RVA: 0x0000564C File Offset: 0x0000384C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040000A1 RID: 161
			public int <>1__state;

			// Token: 0x040000A2 RID: 162
			public AsyncTaskMethodBuilder<byte[]> <>t__builder;

			// Token: 0x040000A3 RID: 163
			public HttpContent <>4__this;

			// Token: 0x040000A4 RID: 164
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000025 RID: 37
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAsStringAsync>d__20 : IAsyncStateMachine
		{
			// Token: 0x0600012E RID: 302 RVA: 0x0000565C File Offset: 0x0000385C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				HttpContent httpContent = this.<>4__this;
				string result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = httpContent.LoadIntoBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, HttpContent.<ReadAsStringAsync>d__20>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					if (httpContent.buffer.Length == 0L)
					{
						result = string.Empty;
					}
					else
					{
						byte[] buffer = httpContent.buffer.GetBuffer();
						int num2 = (int)httpContent.buffer.Length;
						int num3 = 0;
						Encoding encoding;
						if (httpContent.headers != null && httpContent.headers.ContentType != null && httpContent.headers.ContentType.CharSet != null)
						{
							encoding = Encoding.GetEncoding(httpContent.headers.ContentType.CharSet);
							num3 = HttpContent.StartsWith(buffer, num2, encoding.GetPreamble());
						}
						else
						{
							encoding = (HttpContent.GetEncodingFromBuffer(buffer, num2, ref num3) ?? Encoding.UTF8);
						}
						result = encoding.GetString(buffer, num3, num2 - num3);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600012F RID: 303 RVA: 0x000057D8 File Offset: 0x000039D8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040000A5 RID: 165
			public int <>1__state;

			// Token: 0x040000A6 RID: 166
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x040000A7 RID: 167
			public HttpContent <>4__this;

			// Token: 0x040000A8 RID: 168
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
