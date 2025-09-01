using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Adds a buffering layer to read and write operations on another stream. This class cannot be inherited.</summary>
	// Token: 0x02000B41 RID: 2881
	public sealed class BufferedStream : Stream
	{
		// Token: 0x0600680A RID: 26634 RVA: 0x001629A3 File Offset: 0x00160BA3
		internal SemaphoreSlim LazyEnsureAsyncActiveSemaphoreInitialized()
		{
			return LazyInitializer.EnsureInitialized<SemaphoreSlim>(ref this._asyncActiveSemaphore, () => new SemaphoreSlim(1, 1));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.BufferedStream" /> class with a default buffer size of 4096 bytes.</summary>
		/// <param name="stream">The current stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		// Token: 0x0600680B RID: 26635 RVA: 0x001629CF File Offset: 0x00160BCF
		public BufferedStream(Stream stream) : this(stream, 4096)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.BufferedStream" /> class with the specified buffer size.</summary>
		/// <param name="stream">The current stream.</param>
		/// <param name="bufferSize">The buffer size in bytes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative.</exception>
		// Token: 0x0600680C RID: 26636 RVA: 0x001629E0 File Offset: 0x00160BE0
		public BufferedStream(Stream stream, int bufferSize)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", SR.Format("'{0}' must be greater than zero.", "bufferSize"));
			}
			this._stream = stream;
			this._bufferSize = bufferSize;
			if (!this._stream.CanRead && !this._stream.CanWrite)
			{
				throw new ObjectDisposedException(null, "Cannot access a closed Stream.");
			}
		}

		// Token: 0x0600680D RID: 26637 RVA: 0x00162A53 File Offset: 0x00160C53
		private void EnsureNotClosed()
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot access a closed Stream.");
			}
		}

		// Token: 0x0600680E RID: 26638 RVA: 0x00162A69 File Offset: 0x00160C69
		private void EnsureCanSeek()
		{
			if (!this._stream.CanSeek)
			{
				throw new NotSupportedException("Stream does not support seeking.");
			}
		}

		// Token: 0x0600680F RID: 26639 RVA: 0x00162A83 File Offset: 0x00160C83
		private void EnsureCanRead()
		{
			if (!this._stream.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading.");
			}
		}

		// Token: 0x06006810 RID: 26640 RVA: 0x00162A9D File Offset: 0x00160C9D
		private void EnsureCanWrite()
		{
			if (!this._stream.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing.");
			}
		}

		// Token: 0x06006811 RID: 26641 RVA: 0x00162AB8 File Offset: 0x00160CB8
		private void EnsureShadowBufferAllocated()
		{
			if (this._buffer.Length != this._bufferSize || this._bufferSize >= 81920)
			{
				return;
			}
			byte[] array = new byte[Math.Min(this._bufferSize + this._bufferSize, 81920)];
			Buffer.BlockCopy(this._buffer, 0, array, 0, this._writePos);
			this._buffer = array;
		}

		// Token: 0x06006812 RID: 26642 RVA: 0x00162B1B File Offset: 0x00160D1B
		private void EnsureBufferAllocated()
		{
			if (this._buffer == null)
			{
				this._buffer = new byte[this._bufferSize];
			}
		}

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06006813 RID: 26643 RVA: 0x00162B36 File Offset: 0x00160D36
		public Stream UnderlyingStream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06006814 RID: 26644 RVA: 0x00162B3E File Offset: 0x00160D3E
		public int BufferSize
		{
			get
			{
				return this._bufferSize;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports reading.</summary>
		/// <returns>
		///   <see langword="true" /> if the stream supports reading; <see langword="false" /> if the stream is closed or was opened with write-only access.</returns>
		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06006815 RID: 26645 RVA: 0x00162B46 File Offset: 0x00160D46
		public override bool CanRead
		{
			get
			{
				return this._stream != null && this._stream.CanRead;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports writing.</summary>
		/// <returns>
		///   <see langword="true" /> if the stream supports writing; <see langword="false" /> if the stream is closed or was opened with read-only access.</returns>
		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06006816 RID: 26646 RVA: 0x00162B5D File Offset: 0x00160D5D
		public override bool CanWrite
		{
			get
			{
				return this._stream != null && this._stream.CanWrite;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports seeking.</summary>
		/// <returns>
		///   <see langword="true" /> if the stream supports seeking; <see langword="false" /> if the stream is closed or if the stream was constructed from an operating system handle such as a pipe or output to the console.</returns>
		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06006817 RID: 26647 RVA: 0x00162B74 File Offset: 0x00160D74
		public override bool CanSeek
		{
			get
			{
				return this._stream != null && this._stream.CanSeek;
			}
		}

		/// <summary>Gets the stream length in bytes.</summary>
		/// <returns>The stream length in bytes.</returns>
		/// <exception cref="T:System.IO.IOException">The underlying stream is <see langword="null" /> or closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06006818 RID: 26648 RVA: 0x00162B8B File Offset: 0x00160D8B
		public override long Length
		{
			get
			{
				this.EnsureNotClosed();
				if (this._writePos > 0)
				{
					this.FlushWrite();
				}
				return this._stream.Length;
			}
		}

		/// <summary>Gets the position within the current stream.</summary>
		/// <returns>The position within the current stream.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value passed to <see cref="M:System.IO.BufferedStream.Seek(System.Int64,System.IO.SeekOrigin)" /> is negative.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as the stream being closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06006819 RID: 26649 RVA: 0x00162BAD File Offset: 0x00160DAD
		// (set) Token: 0x0600681A RID: 26650 RVA: 0x00162BDC File Offset: 0x00160DDC
		public override long Position
		{
			get
			{
				this.EnsureNotClosed();
				this.EnsureCanSeek();
				return this._stream.Position + (long)(this._readPos - this._readLen + this._writePos);
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Non-negative number required.");
				}
				this.EnsureNotClosed();
				this.EnsureCanSeek();
				if (this._writePos > 0)
				{
					this.FlushWrite();
				}
				this._readPos = 0;
				this._readLen = 0;
				this._stream.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x0600681B RID: 26651 RVA: 0x00162C38 File Offset: 0x00160E38
		public override ValueTask DisposeAsync()
		{
			BufferedStream.<DisposeAsync>d__34 <DisposeAsync>d__;
			<DisposeAsync>d__.<>4__this = this;
			<DisposeAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder.Create();
			<DisposeAsync>d__.<>1__state = -1;
			<DisposeAsync>d__.<>t__builder.Start<BufferedStream.<DisposeAsync>d__34>(ref <DisposeAsync>d__);
			return <DisposeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600681C RID: 26652 RVA: 0x00162C7C File Offset: 0x00160E7C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._stream != null)
				{
					try
					{
						this.Flush();
					}
					finally
					{
						this._stream.Dispose();
					}
				}
			}
			finally
			{
				this._stream = null;
				this._buffer = null;
				base.Dispose(disposing);
			}
		}

		/// <summary>Clears all buffers for this stream and causes any buffered data to be written to the underlying device.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.IO.IOException">The data source or repository is not open.</exception>
		// Token: 0x0600681D RID: 26653 RVA: 0x00162CDC File Offset: 0x00160EDC
		public override void Flush()
		{
			this.EnsureNotClosed();
			if (this._writePos > 0)
			{
				this.FlushWrite();
				return;
			}
			if (this._readPos < this._readLen)
			{
				if (this._stream.CanSeek)
				{
					this.FlushRead();
				}
				if (this._stream.CanWrite)
				{
					this._stream.Flush();
				}
				return;
			}
			if (this._stream.CanWrite)
			{
				this._stream.Flush();
			}
			this._writePos = (this._readPos = (this._readLen = 0));
		}

		/// <summary>Asynchronously clears all buffers for this stream, causes any buffered data to be written to the underlying device, and monitors cancellation requests.</summary>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x0600681E RID: 26654 RVA: 0x00162D6A File Offset: 0x00160F6A
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			this.EnsureNotClosed();
			return this.FlushAsyncInternal(cancellationToken);
		}

		// Token: 0x0600681F RID: 26655 RVA: 0x00162D8C File Offset: 0x00160F8C
		private Task FlushAsyncInternal(CancellationToken cancellationToken)
		{
			BufferedStream.<FlushAsyncInternal>d__38 <FlushAsyncInternal>d__;
			<FlushAsyncInternal>d__.<>4__this = this;
			<FlushAsyncInternal>d__.cancellationToken = cancellationToken;
			<FlushAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FlushAsyncInternal>d__.<>1__state = -1;
			<FlushAsyncInternal>d__.<>t__builder.Start<BufferedStream.<FlushAsyncInternal>d__38>(ref <FlushAsyncInternal>d__);
			return <FlushAsyncInternal>d__.<>t__builder.Task;
		}

		// Token: 0x06006820 RID: 26656 RVA: 0x00162DD7 File Offset: 0x00160FD7
		private void FlushRead()
		{
			if (this._readPos - this._readLen != 0)
			{
				this._stream.Seek((long)(this._readPos - this._readLen), SeekOrigin.Current);
			}
			this._readPos = 0;
			this._readLen = 0;
		}

		// Token: 0x06006821 RID: 26657 RVA: 0x00162E14 File Offset: 0x00161014
		private void ClearReadBufferBeforeWrite()
		{
			if (this._readPos == this._readLen)
			{
				this._readPos = (this._readLen = 0);
				return;
			}
			if (!this._stream.CanSeek)
			{
				throw new NotSupportedException("Cannot write to a BufferedStream while the read buffer is not empty if the underlying stream is not seekable. Ensure that the stream underlying this BufferedStream can seek or avoid interleaving read and write operations on this BufferedStream.");
			}
			this.FlushRead();
		}

		// Token: 0x06006822 RID: 26658 RVA: 0x00162E5E File Offset: 0x0016105E
		private void FlushWrite()
		{
			this._stream.Write(this._buffer, 0, this._writePos);
			this._writePos = 0;
			this._stream.Flush();
		}

		// Token: 0x06006823 RID: 26659 RVA: 0x00162E8C File Offset: 0x0016108C
		private Task FlushWriteAsync(CancellationToken cancellationToken)
		{
			BufferedStream.<FlushWriteAsync>d__42 <FlushWriteAsync>d__;
			<FlushWriteAsync>d__.<>4__this = this;
			<FlushWriteAsync>d__.cancellationToken = cancellationToken;
			<FlushWriteAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FlushWriteAsync>d__.<>1__state = -1;
			<FlushWriteAsync>d__.<>t__builder.Start<BufferedStream.<FlushWriteAsync>d__42>(ref <FlushWriteAsync>d__);
			return <FlushWriteAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06006824 RID: 26660 RVA: 0x00162ED8 File Offset: 0x001610D8
		private int ReadFromBuffer(byte[] array, int offset, int count)
		{
			int num = this._readLen - this._readPos;
			if (num == 0)
			{
				return 0;
			}
			if (num > count)
			{
				num = count;
			}
			Buffer.BlockCopy(this._buffer, this._readPos, array, offset, num);
			this._readPos += num;
			return num;
		}

		// Token: 0x06006825 RID: 26661 RVA: 0x00162F24 File Offset: 0x00161124
		private int ReadFromBuffer(Span<byte> destination)
		{
			int num = Math.Min(this._readLen - this._readPos, destination.Length);
			if (num > 0)
			{
				new ReadOnlySpan<byte>(this._buffer, this._readPos, num).CopyTo(destination);
				this._readPos += num;
			}
			return num;
		}

		// Token: 0x06006826 RID: 26662 RVA: 0x00162F7C File Offset: 0x0016117C
		private int ReadFromBuffer(byte[] array, int offset, int count, out Exception error)
		{
			int result;
			try
			{
				error = null;
				result = this.ReadFromBuffer(array, offset, count);
			}
			catch (Exception ex)
			{
				error = ex;
				result = 0;
			}
			return result;
		}

		/// <summary>Copies bytes from the current buffered stream to an array.</summary>
		/// <param name="array">The buffer to which bytes are to be copied.</param>
		/// <param name="offset">The byte offset in the buffer at which to begin reading bytes.</param>
		/// <param name="count">The number of bytes to be read.</param>
		/// <returns>The total number of bytes read into <paramref name="array" />. This can be less than the number of bytes requested if that many bytes are not currently available, or 0 if the end of the stream has been reached before any data can be read.</returns>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="array" /> minus <paramref name="offset" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.IO.IOException">The stream is not open or is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		// Token: 0x06006827 RID: 26663 RVA: 0x00162FB4 File Offset: 0x001611B4
		public override int Read(byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			this.EnsureNotClosed();
			this.EnsureCanRead();
			int num = this.ReadFromBuffer(array, offset, count);
			if (num == count)
			{
				return num;
			}
			int num2 = num;
			if (num > 0)
			{
				count -= num;
				offset += num;
			}
			this._readPos = (this._readLen = 0);
			if (this._writePos > 0)
			{
				this.FlushWrite();
			}
			if (count >= this._bufferSize)
			{
				return this._stream.Read(array, offset, count) + num2;
			}
			this.EnsureBufferAllocated();
			this._readLen = this._stream.Read(this._buffer, 0, this._bufferSize);
			num = this.ReadFromBuffer(array, offset, count);
			return num + num2;
		}

		// Token: 0x06006828 RID: 26664 RVA: 0x001630A8 File Offset: 0x001612A8
		public override int Read(Span<byte> destination)
		{
			this.EnsureNotClosed();
			this.EnsureCanRead();
			int num = this.ReadFromBuffer(destination);
			if (num == destination.Length)
			{
				return num;
			}
			if (num > 0)
			{
				destination = destination.Slice(num);
			}
			this._readPos = (this._readLen = 0);
			if (this._writePos > 0)
			{
				this.FlushWrite();
			}
			if (destination.Length >= this._bufferSize)
			{
				return this._stream.Read(destination) + num;
			}
			this.EnsureBufferAllocated();
			this._readLen = this._stream.Read(this._buffer, 0, this._bufferSize);
			return this.ReadFromBuffer(destination) + num;
		}

		// Token: 0x06006829 RID: 26665 RVA: 0x00163150 File Offset: 0x00161350
		private Task<int> LastSyncCompletedReadTask(int val)
		{
			Task<int> task = this._lastSyncCompletedReadTask;
			if (task != null && task.Result == val)
			{
				return task;
			}
			task = Task.FromResult<int>(val);
			this._lastSyncCompletedReadTask = task;
			return task;
		}

		/// <summary>Asynchronously reads a sequence of bytes from the current stream, advances the position within the stream by the number of bytes read, and monitors cancellation requests.</summary>
		/// <param name="buffer">The buffer to write the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous read operation.</exception>
		// Token: 0x0600682A RID: 26666 RVA: 0x00163184 File Offset: 0x00161384
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			this.EnsureNotClosed();
			this.EnsureCanRead();
			int num = 0;
			SemaphoreSlim semaphoreSlim = this.LazyEnsureAsyncActiveSemaphoreInitialized();
			Task task = semaphoreSlim.WaitAsync();
			if (task.IsCompletedSuccessfully)
			{
				bool flag = true;
				try
				{
					Exception ex;
					num = this.ReadFromBuffer(buffer, offset, count, out ex);
					flag = (num == count || ex != null);
					if (flag)
					{
						return (ex == null) ? this.LastSyncCompletedReadTask(num) : Task.FromException<int>(ex);
					}
				}
				finally
				{
					if (flag)
					{
						semaphoreSlim.Release();
					}
				}
			}
			return this.ReadFromUnderlyingStreamAsync(new Memory<byte>(buffer, offset + num, count - num), cancellationToken, num, task).AsTask();
		}

		// Token: 0x0600682B RID: 26667 RVA: 0x0016328C File Offset: 0x0016148C
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
			}
			this.EnsureNotClosed();
			this.EnsureCanRead();
			int num = 0;
			SemaphoreSlim semaphoreSlim = this.LazyEnsureAsyncActiveSemaphoreInitialized();
			Task task = semaphoreSlim.WaitAsync();
			if (task.IsCompletedSuccessfully)
			{
				bool flag = true;
				try
				{
					num = this.ReadFromBuffer(buffer.Span);
					flag = (num == buffer.Length);
					if (flag)
					{
						return new ValueTask<int>(num);
					}
				}
				finally
				{
					if (flag)
					{
						semaphoreSlim.Release();
					}
				}
			}
			return this.ReadFromUnderlyingStreamAsync(buffer.Slice(num), cancellationToken, num, task);
		}

		// Token: 0x0600682C RID: 26668 RVA: 0x0016332C File Offset: 0x0016152C
		private ValueTask<int> ReadFromUnderlyingStreamAsync(Memory<byte> buffer, CancellationToken cancellationToken, int bytesAlreadySatisfied, Task semaphoreLockTask)
		{
			BufferedStream.<ReadFromUnderlyingStreamAsync>d__51 <ReadFromUnderlyingStreamAsync>d__;
			<ReadFromUnderlyingStreamAsync>d__.<>4__this = this;
			<ReadFromUnderlyingStreamAsync>d__.buffer = buffer;
			<ReadFromUnderlyingStreamAsync>d__.cancellationToken = cancellationToken;
			<ReadFromUnderlyingStreamAsync>d__.bytesAlreadySatisfied = bytesAlreadySatisfied;
			<ReadFromUnderlyingStreamAsync>d__.semaphoreLockTask = semaphoreLockTask;
			<ReadFromUnderlyingStreamAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<int>.Create();
			<ReadFromUnderlyingStreamAsync>d__.<>1__state = -1;
			<ReadFromUnderlyingStreamAsync>d__.<>t__builder.Start<BufferedStream.<ReadFromUnderlyingStreamAsync>d__51>(ref <ReadFromUnderlyingStreamAsync>d__);
			return <ReadFromUnderlyingStreamAsync>d__.<>t__builder.Task;
		}

		/// <summary>Begins an asynchronous read operation. (Consider using <see cref="M:System.IO.BufferedStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead.)</summary>
		/// <param name="buffer">The buffer to read the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data read from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="callback">An optional asynchronous callback, to be called when the read is complete.</param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
		/// <returns>An object that represents the asynchronous read, which could still be pending.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.IO.IOException">Attempted an asynchronous read past the end of the stream.</exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="offset" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support the read operation.</exception>
		// Token: 0x0600682D RID: 26669 RVA: 0x000A436A File Offset: 0x000A256A
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(this.ReadAsync(buffer, offset, count, CancellationToken.None), callback, state);
		}

		/// <summary>Waits for the pending asynchronous read operation to complete. (Consider using <see cref="M:System.IO.BufferedStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead.)</summary>
		/// <param name="asyncResult">The reference to the pending asynchronous request to wait for.</param>
		/// <returns>The number of bytes read from the stream, between 0 (zero) and the number of bytes you requested. Streams only return 0 only at the end of the stream, otherwise, they should block until at least 1 byte is available.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">This <see cref="T:System.IAsyncResult" /> object was not created by calling <see cref="M:System.IO.BufferedStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> on this class.</exception>
		// Token: 0x0600682E RID: 26670 RVA: 0x000A4383 File Offset: 0x000A2583
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		/// <summary>Reads a byte from the underlying stream and returns the byte cast to an <see langword="int" />, or returns -1 if reading from the end of the stream.</summary>
		/// <returns>The byte cast to an <see langword="int" />, or -1 if reading from the end of the stream.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as the stream being closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		// Token: 0x0600682F RID: 26671 RVA: 0x00163390 File Offset: 0x00161590
		public override int ReadByte()
		{
			if (this._readPos == this._readLen)
			{
				return this.ReadByteSlow();
			}
			byte[] buffer = this._buffer;
			int readPos = this._readPos;
			this._readPos = readPos + 1;
			return buffer[readPos];
		}

		// Token: 0x06006830 RID: 26672 RVA: 0x001633CC File Offset: 0x001615CC
		private int ReadByteSlow()
		{
			this.EnsureNotClosed();
			this.EnsureCanRead();
			if (this._writePos > 0)
			{
				this.FlushWrite();
			}
			this.EnsureBufferAllocated();
			this._readLen = this._stream.Read(this._buffer, 0, this._bufferSize);
			this._readPos = 0;
			if (this._readLen == 0)
			{
				return -1;
			}
			byte[] buffer = this._buffer;
			int readPos = this._readPos;
			this._readPos = readPos + 1;
			return buffer[readPos];
		}

		// Token: 0x06006831 RID: 26673 RVA: 0x00163444 File Offset: 0x00161644
		private void WriteToBuffer(byte[] array, ref int offset, ref int count)
		{
			int num = Math.Min(this._bufferSize - this._writePos, count);
			if (num <= 0)
			{
				return;
			}
			this.EnsureBufferAllocated();
			Buffer.BlockCopy(array, offset, this._buffer, this._writePos, num);
			this._writePos += num;
			count -= num;
			offset += num;
		}

		// Token: 0x06006832 RID: 26674 RVA: 0x001634A0 File Offset: 0x001616A0
		private int WriteToBuffer(ReadOnlySpan<byte> buffer)
		{
			int num = Math.Min(this._bufferSize - this._writePos, buffer.Length);
			if (num > 0)
			{
				this.EnsureBufferAllocated();
				buffer.Slice(0, num).CopyTo(new Span<byte>(this._buffer, this._writePos, num));
				this._writePos += num;
			}
			return num;
		}

		// Token: 0x06006833 RID: 26675 RVA: 0x00163504 File Offset: 0x00161704
		private void WriteToBuffer(byte[] array, ref int offset, ref int count, out Exception error)
		{
			try
			{
				error = null;
				this.WriteToBuffer(array, ref offset, ref count);
			}
			catch (Exception ex)
			{
				error = ex;
			}
		}

		/// <summary>Copies bytes to the buffered stream and advances the current position within the buffered stream by the number of bytes written.</summary>
		/// <param name="array">The byte array from which to copy <paramref name="count" /> bytes to the current buffered stream.</param>
		/// <param name="offset">The offset in the buffer at which to begin copying bytes to the current buffered stream.</param>
		/// <param name="count">The number of bytes to be written to the current buffered stream.</param>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="array" /> minus <paramref name="offset" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		// Token: 0x06006834 RID: 26676 RVA: 0x00163538 File Offset: 0x00161738
		public override void Write(byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			this.EnsureNotClosed();
			this.EnsureCanWrite();
			if (this._writePos == 0)
			{
				this.ClearReadBufferBeforeWrite();
			}
			int num = checked(this._writePos + count);
			if (checked(num + count >= this._bufferSize + this._bufferSize))
			{
				if (this._writePos > 0)
				{
					if (num <= this._bufferSize + this._bufferSize && num <= 81920)
					{
						this.EnsureShadowBufferAllocated();
						Buffer.BlockCopy(array, offset, this._buffer, this._writePos, count);
						this._stream.Write(this._buffer, 0, num);
						this._writePos = 0;
						return;
					}
					this._stream.Write(this._buffer, 0, this._writePos);
					this._writePos = 0;
				}
				this._stream.Write(array, offset, count);
				return;
			}
			this.WriteToBuffer(array, ref offset, ref count);
			if (this._writePos < this._bufferSize)
			{
				return;
			}
			this._stream.Write(this._buffer, 0, this._writePos);
			this._writePos = 0;
			this.WriteToBuffer(array, ref offset, ref count);
		}

		// Token: 0x06006835 RID: 26677 RVA: 0x00163694 File Offset: 0x00161894
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			this.EnsureNotClosed();
			this.EnsureCanWrite();
			if (this._writePos == 0)
			{
				this.ClearReadBufferBeforeWrite();
			}
			int num = checked(this._writePos + buffer.Length);
			if (checked(num + buffer.Length >= this._bufferSize + this._bufferSize))
			{
				if (this._writePos > 0)
				{
					if (num <= this._bufferSize + this._bufferSize && num <= 81920)
					{
						this.EnsureShadowBufferAllocated();
						buffer.CopyTo(new Span<byte>(this._buffer, this._writePos, buffer.Length));
						this._stream.Write(this._buffer, 0, num);
						this._writePos = 0;
						return;
					}
					this._stream.Write(this._buffer, 0, this._writePos);
					this._writePos = 0;
				}
				this._stream.Write(buffer);
				return;
			}
			int start = this.WriteToBuffer(buffer);
			if (this._writePos < this._bufferSize)
			{
				return;
			}
			buffer = buffer.Slice(start);
			this._stream.Write(this._buffer, 0, this._writePos);
			this._writePos = 0;
			start = this.WriteToBuffer(buffer);
		}

		/// <summary>Asynchronously writes a sequence of bytes to the current stream, advances the current position within this stream by the number of bytes written, and monitors cancellation requests.</summary>
		/// <param name="buffer">The buffer to write data from.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> from which to begin copying bytes to the stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous write operation.</exception>
		// Token: 0x06006836 RID: 26678 RVA: 0x001637BC File Offset: 0x001619BC
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			return this.WriteAsync(new ReadOnlyMemory<byte>(buffer, offset, count), cancellationToken).AsTask();
		}

		// Token: 0x06006837 RID: 26679 RVA: 0x00163830 File Offset: 0x00161A30
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask(Task.FromCanceled<int>(cancellationToken));
			}
			this.EnsureNotClosed();
			this.EnsureCanWrite();
			SemaphoreSlim semaphoreSlim = this.LazyEnsureAsyncActiveSemaphoreInitialized();
			Task task = semaphoreSlim.WaitAsync();
			if (task.IsCompletedSuccessfully)
			{
				bool flag = true;
				try
				{
					if (this._writePos == 0)
					{
						this.ClearReadBufferBeforeWrite();
					}
					flag = (buffer.Length < this._bufferSize - this._writePos);
					if (flag)
					{
						this.WriteToBuffer(buffer.Span);
						return default(ValueTask);
					}
				}
				finally
				{
					if (flag)
					{
						semaphoreSlim.Release();
					}
				}
			}
			return new ValueTask(this.WriteToUnderlyingStreamAsync(buffer, cancellationToken, task));
		}

		// Token: 0x06006838 RID: 26680 RVA: 0x001638E8 File Offset: 0x00161AE8
		private Task WriteToUnderlyingStreamAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken, Task semaphoreLockTask)
		{
			BufferedStream.<WriteToUnderlyingStreamAsync>d__63 <WriteToUnderlyingStreamAsync>d__;
			<WriteToUnderlyingStreamAsync>d__.<>4__this = this;
			<WriteToUnderlyingStreamAsync>d__.buffer = buffer;
			<WriteToUnderlyingStreamAsync>d__.cancellationToken = cancellationToken;
			<WriteToUnderlyingStreamAsync>d__.semaphoreLockTask = semaphoreLockTask;
			<WriteToUnderlyingStreamAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteToUnderlyingStreamAsync>d__.<>1__state = -1;
			<WriteToUnderlyingStreamAsync>d__.<>t__builder.Start<BufferedStream.<WriteToUnderlyingStreamAsync>d__63>(ref <WriteToUnderlyingStreamAsync>d__);
			return <WriteToUnderlyingStreamAsync>d__.<>t__builder.Task;
		}

		/// <summary>Begins an asynchronous write operation. (Consider using <see cref="M:System.IO.BufferedStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead.)</summary>
		/// <param name="buffer">The buffer containing data to write to the current stream.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="callback">The method to be called when the asynchronous write operation is completed.</param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
		/// <returns>An object that references the asynchronous write which could still be pending.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffer" /> length minus <paramref name="offset" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception>
		// Token: 0x06006839 RID: 26681 RVA: 0x000A4597 File Offset: 0x000A2797
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(this.WriteAsync(buffer, offset, count, CancellationToken.None), callback, state);
		}

		/// <summary>Ends an asynchronous write operation and blocks until the I/O operation is complete. (Consider using <see cref="M:System.IO.BufferedStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead.)</summary>
		/// <param name="asyncResult">The pending asynchronous request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">This <see cref="T:System.IAsyncResult" /> object was not created by calling <see cref="M:System.IO.BufferedStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> on this class.</exception>
		// Token: 0x0600683A RID: 26682 RVA: 0x000A45B0 File Offset: 0x000A27B0
		public override void EndWrite(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		/// <summary>Writes a byte to the current position in the buffered stream.</summary>
		/// <param name="value">A byte to write to the stream.</param>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		// Token: 0x0600683B RID: 26683 RVA: 0x00163944 File Offset: 0x00161B44
		public override void WriteByte(byte value)
		{
			this.EnsureNotClosed();
			if (this._writePos == 0)
			{
				this.EnsureCanWrite();
				this.ClearReadBufferBeforeWrite();
				this.EnsureBufferAllocated();
			}
			if (this._writePos >= this._bufferSize - 1)
			{
				this.FlushWrite();
			}
			byte[] buffer = this._buffer;
			int writePos = this._writePos;
			this._writePos = writePos + 1;
			buffer[writePos] = value;
		}

		/// <summary>Sets the position within the current buffered stream.</summary>
		/// <param name="offset">A byte offset relative to <paramref name="origin" />.</param>
		/// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the reference point from which to obtain the new position.</param>
		/// <returns>The new position within the current buffered stream.</returns>
		/// <exception cref="T:System.IO.IOException">The stream is not open or is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		// Token: 0x0600683C RID: 26684 RVA: 0x001639A0 File Offset: 0x00161BA0
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.EnsureNotClosed();
			this.EnsureCanSeek();
			if (this._writePos > 0)
			{
				this.FlushWrite();
				return this._stream.Seek(offset, origin);
			}
			if (this._readLen - this._readPos > 0 && origin == SeekOrigin.Current)
			{
				offset -= (long)(this._readLen - this._readPos);
			}
			long position = this.Position;
			long num = this._stream.Seek(offset, origin);
			this._readPos = (int)(num - (position - (long)this._readPos));
			if (0 <= this._readPos && this._readPos < this._readLen)
			{
				this._stream.Seek((long)(this._readLen - this._readPos), SeekOrigin.Current);
			}
			else
			{
				this._readPos = (this._readLen = 0);
			}
			return num;
		}

		/// <summary>Sets the length of the buffered stream.</summary>
		/// <param name="value">An integer indicating the desired length of the current buffered stream in bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is negative.</exception>
		/// <exception cref="T:System.IO.IOException">The stream is not open or is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		// Token: 0x0600683D RID: 26685 RVA: 0x00163A68 File Offset: 0x00161C68
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", "Non-negative number required.");
			}
			this.EnsureNotClosed();
			this.EnsureCanSeek();
			this.EnsureCanWrite();
			this.Flush();
			this._stream.SetLength(value);
		}

		// Token: 0x0600683E RID: 26686 RVA: 0x00163AA4 File Offset: 0x00161CA4
		public override void CopyTo(Stream destination, int bufferSize)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			int num = this._readLen - this._readPos;
			if (num > 0)
			{
				destination.Write(this._buffer, this._readPos, num);
				this._readPos = (this._readLen = 0);
			}
			else if (this._writePos > 0)
			{
				this.FlushWrite();
			}
			this._stream.CopyTo(destination, bufferSize);
		}

		// Token: 0x0600683F RID: 26687 RVA: 0x00163B0C File Offset: 0x00161D0C
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			if (!cancellationToken.IsCancellationRequested)
			{
				return this.CopyToAsyncCore(destination, bufferSize, cancellationToken);
			}
			return Task.FromCanceled<int>(cancellationToken);
		}

		// Token: 0x06006840 RID: 26688 RVA: 0x00163B30 File Offset: 0x00161D30
		private Task CopyToAsyncCore(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			BufferedStream.<CopyToAsyncCore>d__71 <CopyToAsyncCore>d__;
			<CopyToAsyncCore>d__.<>4__this = this;
			<CopyToAsyncCore>d__.destination = destination;
			<CopyToAsyncCore>d__.bufferSize = bufferSize;
			<CopyToAsyncCore>d__.cancellationToken = cancellationToken;
			<CopyToAsyncCore>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CopyToAsyncCore>d__.<>1__state = -1;
			<CopyToAsyncCore>d__.<>t__builder.Start<BufferedStream.<CopyToAsyncCore>d__71>(ref <CopyToAsyncCore>d__);
			return <CopyToAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x04003C87 RID: 15495
		private const int MaxShadowBufferSize = 81920;

		// Token: 0x04003C88 RID: 15496
		private const int DefaultBufferSize = 4096;

		// Token: 0x04003C89 RID: 15497
		private Stream _stream;

		// Token: 0x04003C8A RID: 15498
		private byte[] _buffer;

		// Token: 0x04003C8B RID: 15499
		private readonly int _bufferSize;

		// Token: 0x04003C8C RID: 15500
		private int _readPos;

		// Token: 0x04003C8D RID: 15501
		private int _readLen;

		// Token: 0x04003C8E RID: 15502
		private int _writePos;

		// Token: 0x04003C8F RID: 15503
		private Task<int> _lastSyncCompletedReadTask;

		// Token: 0x04003C90 RID: 15504
		private SemaphoreSlim _asyncActiveSemaphore;

		// Token: 0x02000B42 RID: 2882
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06006841 RID: 26689 RVA: 0x00163B8B File Offset: 0x00161D8B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06006842 RID: 26690 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x06006843 RID: 26691 RVA: 0x000A598E File Offset: 0x000A3B8E
			internal SemaphoreSlim <LazyEnsureAsyncActiveSemaphoreInitialized>b__10_0()
			{
				return new SemaphoreSlim(1, 1);
			}

			// Token: 0x04003C91 RID: 15505
			public static readonly BufferedStream.<>c <>9 = new BufferedStream.<>c();

			// Token: 0x04003C92 RID: 15506
			public static Func<SemaphoreSlim> <>9__10_0;
		}

		// Token: 0x02000B43 RID: 2883
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <DisposeAsync>d__34 : IAsyncStateMachine
		{
			// Token: 0x06006844 RID: 26692 RVA: 0x00163B98 File Offset: 0x00161D98
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				BufferedStream bufferedStream = this.<>4__this;
				try
				{
					try
					{
						ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter awaiter;
						if (num != 0)
						{
							if (num == 1)
							{
								awaiter = this.<>u__2;
								this.<>u__2 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
								num = (this.<>1__state = -1);
								goto IL_116;
							}
							if (bufferedStream._stream == null)
							{
								goto IL_147;
							}
							this.<>7__wrap1 = null;
							this.<>7__wrap2 = 0;
						}
						try
						{
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
							if (num != 0)
							{
								awaiter2 = bufferedStream.FlushAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (this.<>1__state = 0);
									this.<>u__1 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<DisposeAsync>d__34>(ref awaiter2, ref this);
									return;
								}
							}
							else
							{
								awaiter2 = this.<>u__1;
								this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								num = (this.<>1__state = -1);
							}
							awaiter2.GetResult();
						}
						catch (object obj)
						{
							this.<>7__wrap1 = obj;
						}
						awaiter = bufferedStream._stream.DisposeAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 1);
							this.<>u__2 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, BufferedStream.<DisposeAsync>d__34>(ref awaiter, ref this);
							return;
						}
						IL_116:
						awaiter.GetResult();
						object obj = this.<>7__wrap1;
						if (obj != null)
						{
							Exception ex = obj as Exception;
							if (ex == null)
							{
								throw obj;
							}
							ExceptionDispatchInfo.Capture(ex).Throw();
						}
						this.<>7__wrap1 = null;
						IL_147:;
					}
					finally
					{
						if (num < 0)
						{
							bufferedStream._stream = null;
							bufferedStream._buffer = null;
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

			// Token: 0x06006845 RID: 26693 RVA: 0x00163D7C File Offset: 0x00161F7C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003C93 RID: 15507
			public int <>1__state;

			// Token: 0x04003C94 RID: 15508
			public AsyncValueTaskMethodBuilder <>t__builder;

			// Token: 0x04003C95 RID: 15509
			public BufferedStream <>4__this;

			// Token: 0x04003C96 RID: 15510
			private object <>7__wrap1;

			// Token: 0x04003C97 RID: 15511
			private int <>7__wrap2;

			// Token: 0x04003C98 RID: 15512
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04003C99 RID: 15513
			private ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter <>u__2;
		}

		// Token: 0x02000B44 RID: 2884
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FlushAsyncInternal>d__38 : IAsyncStateMachine
		{
			// Token: 0x06006846 RID: 26694 RVA: 0x00163D8C File Offset: 0x00161F8C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				BufferedStream bufferedStream = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num - 1 <= 2)
						{
							goto IL_8C;
						}
						this.<sem>5__2 = bufferedStream.LazyEnsureAsyncActiveSemaphoreInitialized();
						awaiter = this.<sem>5__2.WaitAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<FlushAsyncInternal>d__38>(ref awaiter, ref this);
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
					IL_8C:
					try
					{
						switch (num)
						{
						case 1:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							break;
						case 2:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_1B2;
						case 3:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_230;
						default:
							if (bufferedStream._writePos > 0)
							{
								awaiter = bufferedStream.FlushWriteAsync(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (this.<>1__state = 1);
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<FlushAsyncInternal>d__38>(ref awaiter, ref this);
									return;
								}
							}
							else if (bufferedStream._readPos < bufferedStream._readLen)
							{
								if (bufferedStream._stream.CanSeek)
								{
									bufferedStream.FlushRead();
								}
								if (!bufferedStream._stream.CanWrite)
								{
									goto IL_1B9;
								}
								awaiter = bufferedStream._stream.FlushAsync(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (this.<>1__state = 2);
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<FlushAsyncInternal>d__38>(ref awaiter, ref this);
									return;
								}
								goto IL_1B2;
							}
							else
							{
								if (!bufferedStream._stream.CanWrite)
								{
									goto IL_237;
								}
								awaiter = bufferedStream._stream.FlushAsync(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (this.<>1__state = 3);
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<FlushAsyncInternal>d__38>(ref awaiter, ref this);
									return;
								}
								goto IL_230;
							}
							break;
						}
						awaiter.GetResult();
						goto IL_26C;
						IL_1B2:
						awaiter.GetResult();
						IL_1B9:
						goto IL_26C;
						IL_230:
						awaiter.GetResult();
						IL_237:;
					}
					finally
					{
						if (num < 0)
						{
							this.<sem>5__2.Release();
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<sem>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_26C:
				this.<>1__state = -2;
				this.<sem>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06006847 RID: 26695 RVA: 0x00164054 File Offset: 0x00162254
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003C9A RID: 15514
			public int <>1__state;

			// Token: 0x04003C9B RID: 15515
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04003C9C RID: 15516
			public BufferedStream <>4__this;

			// Token: 0x04003C9D RID: 15517
			public CancellationToken cancellationToken;

			// Token: 0x04003C9E RID: 15518
			private SemaphoreSlim <sem>5__2;

			// Token: 0x04003C9F RID: 15519
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000B45 RID: 2885
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FlushWriteAsync>d__42 : IAsyncStateMachine
		{
			// Token: 0x06006848 RID: 26696 RVA: 0x00164064 File Offset: 0x00162264
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				BufferedStream bufferedStream = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_10D;
						}
						awaiter2 = bufferedStream._stream.WriteAsync(new ReadOnlyMemory<byte>(bufferedStream._buffer, 0, bufferedStream._writePos), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, BufferedStream.<FlushWriteAsync>d__42>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter2.GetResult();
					bufferedStream._writePos = 0;
					awaiter = bufferedStream._stream.FlushAsync(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<FlushWriteAsync>d__42>(ref awaiter, ref this);
						return;
					}
					IL_10D:
					awaiter.GetResult();
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

			// Token: 0x06006849 RID: 26697 RVA: 0x001641D0 File Offset: 0x001623D0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003CA0 RID: 15520
			public int <>1__state;

			// Token: 0x04003CA1 RID: 15521
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04003CA2 RID: 15522
			public BufferedStream <>4__this;

			// Token: 0x04003CA3 RID: 15523
			public CancellationToken cancellationToken;

			// Token: 0x04003CA4 RID: 15524
			private ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter <>u__1;

			// Token: 0x04003CA5 RID: 15525
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x02000B46 RID: 2886
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadFromUnderlyingStreamAsync>d__51 : IAsyncStateMachine
		{
			// Token: 0x0600684A RID: 26698 RVA: 0x001641E0 File Offset: 0x001623E0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				BufferedStream bufferedStream = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num - 1 <= 2)
						{
							goto IL_7C;
						}
						awaiter = this.semaphoreLockTask.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<ReadFromUnderlyingStreamAsync>d__51>(ref awaiter, ref this);
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
					IL_7C:
					try
					{
						ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter awaiter2;
						int num2;
						switch (num)
						{
						case 1:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							break;
						case 2:
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_207;
						case 3:
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_2A7;
						default:
							num2 = bufferedStream.ReadFromBuffer(this.buffer.Span);
							if (num2 == this.buffer.Length)
							{
								result = this.bytesAlreadySatisfied + num2;
								goto IL_301;
							}
							if (num2 > 0)
							{
								this.buffer = this.buffer.Slice(num2);
								this.bytesAlreadySatisfied += num2;
							}
							bufferedStream._readPos = (bufferedStream._readLen = 0);
							if (bufferedStream._writePos <= 0)
							{
								goto IL_16F;
							}
							awaiter = bufferedStream.FlushWriteAsync(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 1);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<ReadFromUnderlyingStreamAsync>d__51>(ref awaiter, ref this);
								return;
							}
							break;
						}
						awaiter.GetResult();
						IL_16F:
						if (this.buffer.Length >= bufferedStream._bufferSize)
						{
							this.<>7__wrap1 = this.bytesAlreadySatisfied;
							awaiter2 = bufferedStream._stream.ReadAsync(this.buffer, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 2);
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, BufferedStream.<ReadFromUnderlyingStreamAsync>d__51>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							bufferedStream.EnsureBufferAllocated();
							awaiter2 = bufferedStream._stream.ReadAsync(new Memory<byte>(bufferedStream._buffer, 0, bufferedStream._bufferSize), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 3);
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, BufferedStream.<ReadFromUnderlyingStreamAsync>d__51>(ref awaiter2, ref this);
								return;
							}
							goto IL_2A7;
						}
						IL_207:
						int result2 = awaiter2.GetResult();
						result = this.<>7__wrap1 + result2;
						goto IL_301;
						IL_2A7:
						result2 = awaiter2.GetResult();
						bufferedStream._readLen = result2;
						num2 = bufferedStream.ReadFromBuffer(this.buffer.Span);
						result = this.bytesAlreadySatisfied + num2;
					}
					finally
					{
						if (num < 0)
						{
							bufferedStream.LazyEnsureAsyncActiveSemaphoreInitialized().Release();
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_301:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600684B RID: 26699 RVA: 0x00164538 File Offset: 0x00162738
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003CA6 RID: 15526
			public int <>1__state;

			// Token: 0x04003CA7 RID: 15527
			public AsyncValueTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04003CA8 RID: 15528
			public Task semaphoreLockTask;

			// Token: 0x04003CA9 RID: 15529
			public BufferedStream <>4__this;

			// Token: 0x04003CAA RID: 15530
			public Memory<byte> buffer;

			// Token: 0x04003CAB RID: 15531
			public int bytesAlreadySatisfied;

			// Token: 0x04003CAC RID: 15532
			public CancellationToken cancellationToken;

			// Token: 0x04003CAD RID: 15533
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04003CAE RID: 15534
			private int <>7__wrap1;

			// Token: 0x04003CAF RID: 15535
			private ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter <>u__2;
		}

		// Token: 0x02000B47 RID: 2887
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteToUnderlyingStreamAsync>d__63 : IAsyncStateMachine
		{
			// Token: 0x0600684C RID: 26700 RVA: 0x00164548 File Offset: 0x00162748
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				BufferedStream bufferedStream = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num - 1 <= 3)
						{
							goto IL_7B;
						}
						awaiter = this.semaphoreLockTask.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<WriteToUnderlyingStreamAsync>d__63>(ref awaiter, ref this);
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
					IL_7B:
					try
					{
						ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter awaiter2;
						switch (num)
						{
						case 1:
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
							num = (this.<>1__state = -1);
							break;
						case 2:
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_294;
						case 3:
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_329;
						case 4:
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_3AA;
						default:
						{
							if (bufferedStream._writePos == 0)
							{
								bufferedStream.ClearReadBufferBeforeWrite();
							}
							int num2 = checked(bufferedStream._writePos + this.buffer.Length);
							if (checked(num2 + this.buffer.Length < bufferedStream._bufferSize + bufferedStream._bufferSize))
							{
								this.buffer = this.buffer.Slice(bufferedStream.WriteToBuffer(this.buffer.Span));
								if (bufferedStream._writePos < bufferedStream._bufferSize)
								{
									goto IL_3DF;
								}
								awaiter2 = bufferedStream._stream.WriteAsync(new ReadOnlyMemory<byte>(bufferedStream._buffer, 0, bufferedStream._writePos), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (this.<>1__state = 1);
									this.<>u__2 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, BufferedStream.<WriteToUnderlyingStreamAsync>d__63>(ref awaiter2, ref this);
									return;
								}
							}
							else
							{
								if (bufferedStream._writePos <= 0)
								{
									goto IL_337;
								}
								if (num2 <= bufferedStream._bufferSize + bufferedStream._bufferSize && num2 <= 81920)
								{
									bufferedStream.EnsureShadowBufferAllocated();
									this.buffer.Span.CopyTo(new Span<byte>(bufferedStream._buffer, bufferedStream._writePos, this.buffer.Length));
									awaiter2 = bufferedStream._stream.WriteAsync(new ReadOnlyMemory<byte>(bufferedStream._buffer, 0, num2), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
									if (!awaiter2.IsCompleted)
									{
										num = (this.<>1__state = 2);
										this.<>u__2 = awaiter2;
										this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, BufferedStream.<WriteToUnderlyingStreamAsync>d__63>(ref awaiter2, ref this);
										return;
									}
									goto IL_294;
								}
								else
								{
									awaiter2 = bufferedStream._stream.WriteAsync(new ReadOnlyMemory<byte>(bufferedStream._buffer, 0, bufferedStream._writePos), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
									if (!awaiter2.IsCompleted)
									{
										num = (this.<>1__state = 3);
										this.<>u__2 = awaiter2;
										this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, BufferedStream.<WriteToUnderlyingStreamAsync>d__63>(ref awaiter2, ref this);
										return;
									}
									goto IL_329;
								}
							}
							break;
						}
						}
						awaiter2.GetResult();
						bufferedStream._writePos = 0;
						bufferedStream.WriteToBuffer(this.buffer.Span);
						goto IL_3B1;
						IL_294:
						awaiter2.GetResult();
						bufferedStream._writePos = 0;
						goto IL_3DF;
						IL_329:
						awaiter2.GetResult();
						bufferedStream._writePos = 0;
						IL_337:
						awaiter2 = bufferedStream._stream.WriteAsync(this.buffer, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (this.<>1__state = 4);
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, BufferedStream.<WriteToUnderlyingStreamAsync>d__63>(ref awaiter2, ref this);
							return;
						}
						IL_3AA:
						awaiter2.GetResult();
						IL_3B1:;
					}
					finally
					{
						if (num < 0)
						{
							bufferedStream.LazyEnsureAsyncActiveSemaphoreInitialized().Release();
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_3DF:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600684D RID: 26701 RVA: 0x0016497C File Offset: 0x00162B7C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003CB0 RID: 15536
			public int <>1__state;

			// Token: 0x04003CB1 RID: 15537
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04003CB2 RID: 15538
			public Task semaphoreLockTask;

			// Token: 0x04003CB3 RID: 15539
			public BufferedStream <>4__this;

			// Token: 0x04003CB4 RID: 15540
			public ReadOnlyMemory<byte> buffer;

			// Token: 0x04003CB5 RID: 15541
			public CancellationToken cancellationToken;

			// Token: 0x04003CB6 RID: 15542
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04003CB7 RID: 15543
			private ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter <>u__2;
		}

		// Token: 0x02000B48 RID: 2888
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <CopyToAsyncCore>d__71 : IAsyncStateMachine
		{
			// Token: 0x0600684E RID: 26702 RVA: 0x0016498C File Offset: 0x00162B8C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				BufferedStream bufferedStream = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num - 1 <= 2)
						{
							goto IL_80;
						}
						awaiter = bufferedStream.LazyEnsureAsyncActiveSemaphoreInitialized().WaitAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<CopyToAsyncCore>d__71>(ref awaiter, ref this);
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
					IL_80:
					try
					{
						ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter awaiter2;
						switch (num)
						{
						case 1:
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
							num = (this.<>1__state = -1);
							break;
						case 2:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_1B6;
						case 3:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_22E;
						default:
						{
							int num2 = bufferedStream._readLen - bufferedStream._readPos;
							if (num2 > 0)
							{
								awaiter2 = this.destination.WriteAsync(new ReadOnlyMemory<byte>(bufferedStream._buffer, bufferedStream._readPos, num2), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (this.<>1__state = 1);
									this.<>u__2 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, BufferedStream.<CopyToAsyncCore>d__71>(ref awaiter2, ref this);
									return;
								}
							}
							else
							{
								if (bufferedStream._writePos <= 0)
								{
									goto IL_1BD;
								}
								awaiter = bufferedStream.FlushWriteAsync(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (this.<>1__state = 2);
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<CopyToAsyncCore>d__71>(ref awaiter, ref this);
									return;
								}
								goto IL_1B6;
							}
							break;
						}
						}
						awaiter2.GetResult();
						bufferedStream._readPos = (bufferedStream._readLen = 0);
						goto IL_1BD;
						IL_1B6:
						awaiter.GetResult();
						IL_1BD:
						awaiter = bufferedStream._stream.CopyToAsync(this.destination, this.bufferSize, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 3);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<CopyToAsyncCore>d__71>(ref awaiter, ref this);
							return;
						}
						IL_22E:
						awaiter.GetResult();
					}
					finally
					{
						if (num < 0)
						{
							bufferedStream._asyncActiveSemaphore.Release();
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

			// Token: 0x0600684F RID: 26703 RVA: 0x00164C44 File Offset: 0x00162E44
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003CB8 RID: 15544
			public int <>1__state;

			// Token: 0x04003CB9 RID: 15545
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04003CBA RID: 15546
			public BufferedStream <>4__this;

			// Token: 0x04003CBB RID: 15547
			public Stream destination;

			// Token: 0x04003CBC RID: 15548
			public CancellationToken cancellationToken;

			// Token: 0x04003CBD RID: 15549
			public int bufferSize;

			// Token: 0x04003CBE RID: 15550
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04003CBF RID: 15551
			private ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter <>u__2;
		}
	}
}
