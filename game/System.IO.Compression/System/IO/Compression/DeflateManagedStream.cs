using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO.Compression
{
	// Token: 0x02000015 RID: 21
	internal sealed class DeflateManagedStream : Stream
	{
		// Token: 0x06000068 RID: 104 RVA: 0x000035D1 File Offset: 0x000017D1
		internal DeflateManagedStream(Stream stream, ZipArchiveEntry.CompressionMethodValues method)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream does not support reading.", "stream");
			}
			this.InitializeInflater(stream, false, null, method);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000360C File Offset: 0x0000180C
		internal void InitializeInflater(Stream stream, bool leaveOpen, IFileFormatReader reader = null, ZipArchiveEntry.CompressionMethodValues method = ZipArchiveEntry.CompressionMethodValues.Deflate)
		{
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream does not support reading.", "stream");
			}
			this._inflater = new InflaterManaged(reader, method == ZipArchiveEntry.CompressionMethodValues.Deflate64);
			this._stream = stream;
			this._mode = CompressionMode.Decompress;
			this._leaveOpen = leaveOpen;
			this._buffer = new byte[8192];
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000366C File Offset: 0x0000186C
		internal void SetFileFormatWriter(IFileFormatWriter writer)
		{
			if (writer != null)
			{
				this._formatWriter = writer;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003678 File Offset: 0x00001878
		public override bool CanRead
		{
			get
			{
				return this._stream != null && this._mode == CompressionMode.Decompress && this._stream.CanRead;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003699 File Offset: 0x00001899
		public override bool CanWrite
		{
			get
			{
				return this._stream != null && this._mode == CompressionMode.Compress && this._stream.CanWrite;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002289 File Offset: 0x00000489
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000036BB File Offset: 0x000018BB
		public override long Length
		{
			get
			{
				throw new NotSupportedException("This operation is not supported.");
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000036BB File Offset: 0x000018BB
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000036BB File Offset: 0x000018BB
		public override long Position
		{
			get
			{
				throw new NotSupportedException("This operation is not supported.");
			}
			set
			{
				throw new NotSupportedException("This operation is not supported.");
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000036C7 File Offset: 0x000018C7
		public override void Flush()
		{
			this.EnsureNotDisposed();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000036CF File Offset: 0x000018CF
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			this.EnsureNotDisposed();
			if (!cancellationToken.IsCancellationRequested)
			{
				return Task.CompletedTask;
			}
			return Task.FromCanceled(cancellationToken);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000036BB File Offset: 0x000018BB
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000036BB File Offset: 0x000018BB
		public override void SetLength(long value)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000036EC File Offset: 0x000018EC
		public override int Read(byte[] array, int offset, int count)
		{
			this.EnsureDecompressionMode();
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			int num = offset;
			int num2 = count;
			for (;;)
			{
				int num3 = this._inflater.Inflate(array, num, num2);
				num += num3;
				num2 -= num3;
				if (num2 == 0 || this._inflater.Finished())
				{
					goto IL_8A;
				}
				int num4 = this._stream.Read(this._buffer, 0, this._buffer.Length);
				if (num4 <= 0)
				{
					goto IL_8A;
				}
				if (num4 > this._buffer.Length)
				{
					break;
				}
				this._inflater.SetInput(this._buffer, 0, num4);
			}
			throw new InvalidDataException("Found invalid data while decoding.");
			IL_8A:
			return count - num2;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003788 File Offset: 0x00001988
		private void ValidateParameters(byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException("Offset plus count is larger than the length of target array.");
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000037D4 File Offset: 0x000019D4
		private void EnsureNotDisposed()
		{
			if (this._stream == null)
			{
				DeflateManagedStream.ThrowStreamClosedException();
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000037E3 File Offset: 0x000019E3
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowStreamClosedException()
		{
			throw new ObjectDisposedException(null, "Can not access a closed Stream.");
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000037F0 File Offset: 0x000019F0
		private void EnsureDecompressionMode()
		{
			if (this._mode != CompressionMode.Decompress)
			{
				DeflateManagedStream.ThrowCannotReadFromDeflateManagedStreamException();
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000037FF File Offset: 0x000019FF
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowCannotReadFromDeflateManagedStreamException()
		{
			throw new InvalidOperationException("Reading from the compression stream is not supported.");
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000380B File Offset: 0x00001A0B
		private void EnsureCompressionMode()
		{
			if (this._mode != CompressionMode.Compress)
			{
				DeflateManagedStream.ThrowCannotWriteToDeflateManagedStreamException();
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000381B File Offset: 0x00001A1B
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowCannotWriteToDeflateManagedStreamException()
		{
			throw new InvalidOperationException("Writing to the compression stream is not supported.");
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002464 File Offset: 0x00000664
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.ReadAsync(buffer, offset, count, CancellationToken.None), asyncCallback, asyncState);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000247D File Offset: 0x0000067D
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003828 File Offset: 0x00001A28
		public override Task<int> ReadAsync(byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			this.EnsureDecompressionMode();
			if (this._asyncOperations != 0)
			{
				throw new InvalidOperationException("Only one asynchronous reader or writer is allowed time at one time.");
			}
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			Interlocked.Increment(ref this._asyncOperations);
			Task<int> task = null;
			Task<int> result;
			try
			{
				int num = this._inflater.Inflate(array, offset, count);
				if (num != 0)
				{
					result = Task.FromResult<int>(num);
				}
				else if (this._inflater.Finished())
				{
					result = Task.FromResult<int>(0);
				}
				else
				{
					task = this._stream.ReadAsync(this._buffer, 0, this._buffer.Length, cancellationToken);
					if (task == null)
					{
						throw new InvalidOperationException("Stream does not support reading.");
					}
					result = this.ReadAsyncCore(task, array, offset, count, cancellationToken);
				}
			}
			finally
			{
				if (task == null)
				{
					Interlocked.Decrement(ref this._asyncOperations);
				}
			}
			return result;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003908 File Offset: 0x00001B08
		private Task<int> ReadAsyncCore(Task<int> readTask, byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			DeflateManagedStream.<ReadAsyncCore>d__40 <ReadAsyncCore>d__;
			<ReadAsyncCore>d__.<>4__this = this;
			<ReadAsyncCore>d__.readTask = readTask;
			<ReadAsyncCore>d__.array = array;
			<ReadAsyncCore>d__.offset = offset;
			<ReadAsyncCore>d__.count = count;
			<ReadAsyncCore>d__.cancellationToken = cancellationToken;
			<ReadAsyncCore>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadAsyncCore>d__.<>1__state = -1;
			<ReadAsyncCore>d__.<>t__builder.Start<DeflateManagedStream.<ReadAsyncCore>d__40>(ref <ReadAsyncCore>d__);
			return <ReadAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003975 File Offset: 0x00001B75
		public override void Write(byte[] array, int offset, int count)
		{
			this.EnsureCompressionMode();
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			this.DoMaintenance(array, offset, count);
			this.WriteDeflaterOutput();
			this._deflater.SetInput(array, offset, count);
			this.WriteDeflaterOutput();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000039B0 File Offset: 0x00001BB0
		private void WriteDeflaterOutput()
		{
			while (!this._deflater.NeedsInput())
			{
				int deflateOutput = this._deflater.GetDeflateOutput(this._buffer);
				if (deflateOutput > 0)
				{
					this._stream.Write(this._buffer, 0, deflateOutput);
				}
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000039F8 File Offset: 0x00001BF8
		private void DoMaintenance(byte[] array, int offset, int count)
		{
			if (count <= 0)
			{
				return;
			}
			this._wroteBytes = true;
			if (this._formatWriter == null)
			{
				return;
			}
			if (!this._wroteHeader)
			{
				byte[] header = this._formatWriter.GetHeader();
				this._stream.Write(header, 0, header.Length);
				this._wroteHeader = true;
			}
			this._formatWriter.UpdateWithBytesRead(array, offset, count);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003A54 File Offset: 0x00001C54
		private void PurgeBuffers(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			if (this._stream == null)
			{
				return;
			}
			this.Flush();
			if (this._mode != CompressionMode.Compress)
			{
				return;
			}
			if (this._wroteBytes)
			{
				this.WriteDeflaterOutput();
				bool flag;
				do
				{
					int num;
					flag = this._deflater.Finish(this._buffer, out num);
					if (num > 0)
					{
						this._stream.Write(this._buffer, 0, num);
					}
				}
				while (!flag);
			}
			else
			{
				bool flag2;
				do
				{
					int num2;
					flag2 = this._deflater.Finish(this._buffer, out num2);
				}
				while (!flag2);
			}
			if (this._formatWriter != null && this._wroteHeader)
			{
				byte[] footer = this._formatWriter.GetFooter();
				this._stream.Write(footer, 0, footer.Length);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003B04 File Offset: 0x00001D04
		protected override void Dispose(bool disposing)
		{
			try
			{
				this.PurgeBuffers(disposing);
			}
			finally
			{
				try
				{
					if (disposing && !this._leaveOpen && this._stream != null)
					{
						this._stream.Dispose();
					}
				}
				finally
				{
					this._stream = null;
					try
					{
						DeflaterManaged deflater = this._deflater;
						if (deflater != null)
						{
							deflater.Dispose();
						}
						InflaterManaged inflater = this._inflater;
						if (inflater != null)
						{
							inflater.Dispose();
						}
					}
					finally
					{
						this._deflater = null;
						this._inflater = null;
						base.Dispose(disposing);
					}
				}
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public override Task WriteAsync(byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			this.EnsureCompressionMode();
			if (this._asyncOperations != 0)
			{
				throw new InvalidOperationException("Only one asynchronous reader or writer is allowed time at one time.");
			}
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			return this.WriteAsyncCore(array, offset, count, cancellationToken);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003BFC File Offset: 0x00001DFC
		private Task WriteAsyncCore(byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			DeflateManagedStream.<WriteAsyncCore>d__47 <WriteAsyncCore>d__;
			<WriteAsyncCore>d__.<>4__this = this;
			<WriteAsyncCore>d__.array = array;
			<WriteAsyncCore>d__.offset = offset;
			<WriteAsyncCore>d__.count = count;
			<WriteAsyncCore>d__.cancellationToken = cancellationToken;
			<WriteAsyncCore>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAsyncCore>d__.<>1__state = -1;
			<WriteAsyncCore>d__.<>t__builder.Start<DeflateManagedStream.<WriteAsyncCore>d__47>(ref <WriteAsyncCore>d__);
			return <WriteAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002621 File Offset: 0x00000821
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.WriteAsync(buffer, offset, count, CancellationToken.None), asyncCallback, asyncState);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000263A File Offset: 0x0000083A
		public override void EndWrite(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003C60 File Offset: 0x00001E60
		[DebuggerHidden]
		[CompilerGenerated]
		private Task <>n__0(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return base.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x040000BB RID: 187
		internal const int DefaultBufferSize = 8192;

		// Token: 0x040000BC RID: 188
		private Stream _stream;

		// Token: 0x040000BD RID: 189
		private CompressionMode _mode;

		// Token: 0x040000BE RID: 190
		private bool _leaveOpen;

		// Token: 0x040000BF RID: 191
		private InflaterManaged _inflater;

		// Token: 0x040000C0 RID: 192
		private DeflaterManaged _deflater;

		// Token: 0x040000C1 RID: 193
		private byte[] _buffer;

		// Token: 0x040000C2 RID: 194
		private int _asyncOperations;

		// Token: 0x040000C3 RID: 195
		private IFileFormatWriter _formatWriter;

		// Token: 0x040000C4 RID: 196
		private bool _wroteHeader;

		// Token: 0x040000C5 RID: 197
		private bool _wroteBytes;

		// Token: 0x02000016 RID: 22
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAsyncCore>d__40 : IAsyncStateMachine
		{
			// Token: 0x0600008B RID: 139 RVA: 0x00003C70 File Offset: 0x00001E70
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DeflateManagedStream deflateManagedStream = this.<>4__this;
				int result;
				try
				{
					try
					{
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
						if (num == 0)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_75;
						}
						IL_14:
						awaiter = this.readTask.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, DeflateManagedStream.<ReadAsyncCore>d__40>(ref awaiter, ref this);
							return;
						}
						IL_75:
						int num2 = awaiter.GetResult();
						deflateManagedStream.EnsureNotDisposed();
						if (num2 <= 0)
						{
							result = 0;
						}
						else
						{
							if (num2 > deflateManagedStream._buffer.Length)
							{
								throw new InvalidDataException("Found invalid data while decoding.");
							}
							this.cancellationToken.ThrowIfCancellationRequested();
							deflateManagedStream._inflater.SetInput(deflateManagedStream._buffer, 0, num2);
							num2 = deflateManagedStream._inflater.Inflate(this.array, this.offset, this.count);
							if (num2 == 0 && !deflateManagedStream._inflater.Finished())
							{
								this.readTask = deflateManagedStream._stream.ReadAsync(deflateManagedStream._buffer, 0, deflateManagedStream._buffer.Length, this.cancellationToken);
								if (this.readTask == null)
								{
									throw new InvalidOperationException("Stream does not support reading.");
								}
								goto IL_14;
							}
							else
							{
								result = num2;
							}
						}
					}
					finally
					{
						if (num < 0)
						{
							Interlocked.Decrement(ref deflateManagedStream._asyncOperations);
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
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600008C RID: 140 RVA: 0x00003E20 File Offset: 0x00002020
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040000C6 RID: 198
			public int <>1__state;

			// Token: 0x040000C7 RID: 199
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x040000C8 RID: 200
			public Task<int> readTask;

			// Token: 0x040000C9 RID: 201
			public DeflateManagedStream <>4__this;

			// Token: 0x040000CA RID: 202
			public CancellationToken cancellationToken;

			// Token: 0x040000CB RID: 203
			public byte[] array;

			// Token: 0x040000CC RID: 204
			public int offset;

			// Token: 0x040000CD RID: 205
			public int count;

			// Token: 0x040000CE RID: 206
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000017 RID: 23
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAsyncCore>d__47 : IAsyncStateMachine
		{
			// Token: 0x0600008D RID: 141 RVA: 0x00003E30 File Offset: 0x00002030
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				DeflateManagedStream deflateManagedStream = this.<>4__this;
				try
				{
					if (num != 0)
					{
						Interlocked.Increment(ref deflateManagedStream._asyncOperations);
					}
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = deflateManagedStream.<>n__0(this.array, this.offset, this.count, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, DeflateManagedStream.<WriteAsyncCore>d__47>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						awaiter.GetResult();
					}
					finally
					{
						if (num < 0)
						{
							Interlocked.Decrement(ref deflateManagedStream._asyncOperations);
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
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600008E RID: 142 RVA: 0x00003F34 File Offset: 0x00002134
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040000CF RID: 207
			public int <>1__state;

			// Token: 0x040000D0 RID: 208
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040000D1 RID: 209
			public DeflateManagedStream <>4__this;

			// Token: 0x040000D2 RID: 210
			public byte[] array;

			// Token: 0x040000D3 RID: 211
			public int offset;

			// Token: 0x040000D4 RID: 212
			public int count;

			// Token: 0x040000D5 RID: 213
			public CancellationToken cancellationToken;

			// Token: 0x040000D6 RID: 214
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
