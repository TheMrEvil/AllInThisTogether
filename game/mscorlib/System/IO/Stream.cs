using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Provides a generic view of a sequence of bytes. This is an abstract class.</summary>
	// Token: 0x02000B4A RID: 2890
	[Serializable]
	public abstract class Stream : MarshalByRefObject, IDisposable, IAsyncDisposable
	{
		// Token: 0x06006850 RID: 26704 RVA: 0x00164C52 File Offset: 0x00162E52
		internal SemaphoreSlim EnsureAsyncActiveSemaphoreInitialized()
		{
			return LazyInitializer.EnsureInitialized<SemaphoreSlim>(ref this._asyncActiveSemaphore, () => new SemaphoreSlim(1, 1));
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current stream supports reading.</summary>
		/// <returns>
		///   <see langword="true" /> if the stream supports reading; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06006851 RID: 26705
		public abstract bool CanRead { get; }

		/// <summary>When overridden in a derived class, gets a value indicating whether the current stream supports seeking.</summary>
		/// <returns>
		///   <see langword="true" /> if the stream supports seeking; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x06006852 RID: 26706
		public abstract bool CanSeek { get; }

		/// <summary>Gets a value that determines whether the current stream can time out.</summary>
		/// <returns>A value that determines whether the current stream can time out.</returns>
		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x06006853 RID: 26707 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool CanTimeout
		{
			get
			{
				return false;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current stream supports writing.</summary>
		/// <returns>
		///   <see langword="true" /> if the stream supports writing; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x06006854 RID: 26708
		public abstract bool CanWrite { get; }

		/// <summary>When overridden in a derived class, gets the length in bytes of the stream.</summary>
		/// <returns>A long value representing the length of the stream in bytes.</returns>
		/// <exception cref="T:System.NotSupportedException">A class derived from <see langword="Stream" /> does not support seeking.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06006855 RID: 26709
		public abstract long Length { get; }

		/// <summary>When overridden in a derived class, gets or sets the position within the current stream.</summary>
		/// <returns>The current position within the stream.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x06006856 RID: 26710
		// (set) Token: 0x06006857 RID: 26711
		public abstract long Position { get; set; }

		/// <summary>Gets or sets a value, in miliseconds, that determines how long the stream will attempt to read before timing out.</summary>
		/// <returns>A value, in miliseconds, that determines how long the stream will attempt to read before timing out.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.IO.Stream.ReadTimeout" /> method always throws an <see cref="T:System.InvalidOperationException" />.</exception>
		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x06006858 RID: 26712 RVA: 0x00164C7E File Offset: 0x00162E7E
		// (set) Token: 0x06006859 RID: 26713 RVA: 0x00164C7E File Offset: 0x00162E7E
		public virtual int ReadTimeout
		{
			get
			{
				throw new InvalidOperationException("Timeouts are not supported on this stream.");
			}
			set
			{
				throw new InvalidOperationException("Timeouts are not supported on this stream.");
			}
		}

		/// <summary>Gets or sets a value, in miliseconds, that determines how long the stream will attempt to write before timing out.</summary>
		/// <returns>A value, in miliseconds, that determines how long the stream will attempt to write before timing out.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.IO.Stream.WriteTimeout" /> method always throws an <see cref="T:System.InvalidOperationException" />.</exception>
		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x0600685A RID: 26714 RVA: 0x00164C7E File Offset: 0x00162E7E
		// (set) Token: 0x0600685B RID: 26715 RVA: 0x00164C7E File Offset: 0x00162E7E
		public virtual int WriteTimeout
		{
			get
			{
				throw new InvalidOperationException("Timeouts are not supported on this stream.");
			}
			set
			{
				throw new InvalidOperationException("Timeouts are not supported on this stream.");
			}
		}

		/// <summary>Asynchronously reads the bytes from the current stream and writes them to another stream.</summary>
		/// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
		/// <returns>A task that represents the asynchronous copy operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Either the current stream or the destination stream is disposed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading, or the destination stream does not support writing.</exception>
		// Token: 0x0600685C RID: 26716 RVA: 0x00164C8C File Offset: 0x00162E8C
		public Task CopyToAsync(Stream destination)
		{
			int copyBufferSize = this.GetCopyBufferSize();
			return this.CopyToAsync(destination, copyBufferSize);
		}

		/// <summary>Asynchronously reads the bytes from the current stream and writes them to another stream, using a specified buffer size.</summary>
		/// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
		/// <param name="bufferSize">The size, in bytes, of the buffer. This value must be greater than zero. The default size is 81920.</param>
		/// <returns>A task that represents the asynchronous copy operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="buffersize" /> is negative or zero.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Either the current stream or the destination stream is disposed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading, or the destination stream does not support writing.</exception>
		// Token: 0x0600685D RID: 26717 RVA: 0x00164CA8 File Offset: 0x00162EA8
		public Task CopyToAsync(Stream destination, int bufferSize)
		{
			return this.CopyToAsync(destination, bufferSize, CancellationToken.None);
		}

		// Token: 0x0600685E RID: 26718 RVA: 0x00164CB8 File Offset: 0x00162EB8
		public Task CopyToAsync(Stream destination, CancellationToken cancellationToken)
		{
			int copyBufferSize = this.GetCopyBufferSize();
			return this.CopyToAsync(destination, copyBufferSize, cancellationToken);
		}

		/// <summary>Asynchronously reads the bytes from the current stream and writes them to another stream, using a specified buffer size and cancellation token.</summary>
		/// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
		/// <param name="bufferSize">The size, in bytes, of the buffer. This value must be greater than zero. The default size is 81920.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <returns>A task that represents the asynchronous copy operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="buffersize" /> is negative or zero.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Either the current stream or the destination stream is disposed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading, or the destination stream does not support writing.</exception>
		// Token: 0x0600685F RID: 26719 RVA: 0x00164CD5 File Offset: 0x00162ED5
		public virtual Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			return this.CopyToAsyncInternal(destination, bufferSize, cancellationToken);
		}

		// Token: 0x06006860 RID: 26720 RVA: 0x00164CE8 File Offset: 0x00162EE8
		private Task CopyToAsyncInternal(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			Stream.<CopyToAsyncInternal>d__28 <CopyToAsyncInternal>d__;
			<CopyToAsyncInternal>d__.<>4__this = this;
			<CopyToAsyncInternal>d__.destination = destination;
			<CopyToAsyncInternal>d__.bufferSize = bufferSize;
			<CopyToAsyncInternal>d__.cancellationToken = cancellationToken;
			<CopyToAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CopyToAsyncInternal>d__.<>1__state = -1;
			<CopyToAsyncInternal>d__.<>t__builder.Start<Stream.<CopyToAsyncInternal>d__28>(ref <CopyToAsyncInternal>d__);
			return <CopyToAsyncInternal>d__.<>t__builder.Task;
		}

		/// <summary>Reads the bytes from the current stream and writes them to another stream.</summary>
		/// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading.  
		///  -or-  
		///  <paramref name="destination" /> does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Either the current stream or <paramref name="destination" /> were closed before the <see cref="M:System.IO.Stream.CopyTo(System.IO.Stream)" /> method was called.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06006861 RID: 26721 RVA: 0x00164D44 File Offset: 0x00162F44
		public void CopyTo(Stream destination)
		{
			int copyBufferSize = this.GetCopyBufferSize();
			this.CopyTo(destination, copyBufferSize);
		}

		/// <summary>Reads the bytes from the current stream and writes them to another stream, using a specified buffer size.</summary>
		/// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
		/// <param name="bufferSize">The size of the buffer. This value must be greater than zero. The default size is 81920.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading.  
		///  -or-  
		///  <paramref name="destination" /> does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Either the current stream or <paramref name="destination" /> were closed before the <see cref="M:System.IO.Stream.CopyTo(System.IO.Stream)" /> method was called.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06006862 RID: 26722 RVA: 0x00164D60 File Offset: 0x00162F60
		public virtual void CopyTo(Stream destination, int bufferSize)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			byte[] array = ArrayPool<byte>.Shared.Rent(bufferSize);
			try
			{
				int count;
				while ((count = this.Read(array, 0, array.Length)) != 0)
				{
					destination.Write(array, 0, count);
				}
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06006863 RID: 26723 RVA: 0x00164DBC File Offset: 0x00162FBC
		private int GetCopyBufferSize()
		{
			int num = 81920;
			if (this.CanSeek)
			{
				long length = this.Length;
				long position = this.Position;
				if (length <= position)
				{
					num = 1;
				}
				else
				{
					long num2 = length - position;
					if (num2 > 0L)
					{
						num = (int)Math.Min((long)num, num2);
					}
				}
			}
			return num;
		}

		/// <summary>Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream. Instead of calling this method, ensure that the stream is properly disposed.</summary>
		// Token: 0x06006864 RID: 26724 RVA: 0x00164E01 File Offset: 0x00163001
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.IO.Stream" />.</summary>
		// Token: 0x06006865 RID: 26725 RVA: 0x000A471D File Offset: 0x000A291D
		public void Dispose()
		{
			this.Close();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.Stream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06006866 RID: 26726 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06006867 RID: 26727
		public abstract void Flush();

		/// <summary>Asynchronously clears all buffers for this stream and causes any buffered data to be written to the underlying device.</summary>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x06006868 RID: 26728 RVA: 0x00164E10 File Offset: 0x00163010
		public Task FlushAsync()
		{
			return this.FlushAsync(CancellationToken.None);
		}

		/// <summary>Asynchronously clears all buffers for this stream, causes any buffered data to be written to the underlying device, and monitors cancellation requests.</summary>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x06006869 RID: 26729 RVA: 0x00164E1D File Offset: 0x0016301D
		public virtual Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(delegate(object state)
			{
				((Stream)state).Flush();
			}, this, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Allocates a <see cref="T:System.Threading.WaitHandle" /> object.</summary>
		/// <returns>A reference to the allocated <see langword="WaitHandle" />.</returns>
		// Token: 0x0600686A RID: 26730 RVA: 0x00164E50 File Offset: 0x00163050
		[Obsolete("CreateWaitHandle will be removed eventually.  Please use \"new ManualResetEvent(false)\" instead.")]
		protected virtual WaitHandle CreateWaitHandle()
		{
			return new ManualResetEvent(false);
		}

		/// <summary>Begins an asynchronous read operation. (Consider using <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" /> instead.)</summary>
		/// <param name="buffer">The buffer to read the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data read from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="callback">An optional asynchronous callback, to be called when the read is complete.</param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the asynchronous read, which could still be pending.</returns>
		/// <exception cref="T:System.IO.IOException">Attempted an asynchronous read past the end of the stream, or a disk error occurs.</exception>
		/// <exception cref="T:System.ArgumentException">One or more of the arguments is invalid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current <see langword="Stream" /> implementation does not support the read operation.</exception>
		// Token: 0x0600686B RID: 26731 RVA: 0x00164E58 File Offset: 0x00163058
		public virtual IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.BeginReadInternal(buffer, offset, count, callback, state, false, true);
		}

		// Token: 0x0600686C RID: 26732 RVA: 0x00164E6C File Offset: 0x0016306C
		internal IAsyncResult BeginReadInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, bool serializeAsynchronously, bool apm)
		{
			if (!this.CanRead)
			{
				throw Error.GetReadNotSupported();
			}
			SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
			Task task = null;
			if (serializeAsynchronously)
			{
				task = semaphoreSlim.WaitAsync();
			}
			else
			{
				semaphoreSlim.Wait();
			}
			Stream.ReadWriteTask readWriteTask = new Stream.ReadWriteTask(true, apm, delegate(object <p0>)
			{
				Stream.ReadWriteTask readWriteTask2 = Task.InternalCurrent as Stream.ReadWriteTask;
				int result;
				try
				{
					result = readWriteTask2._stream.Read(readWriteTask2._buffer, readWriteTask2._offset, readWriteTask2._count);
				}
				finally
				{
					if (!readWriteTask2._apm)
					{
						readWriteTask2._stream.FinishTrackingAsyncOperation();
					}
					readWriteTask2.ClearBeginState();
				}
				return result;
			}, state, this, buffer, offset, count, callback);
			if (task != null)
			{
				this.RunReadWriteTaskWhenReady(task, readWriteTask);
			}
			else
			{
				this.RunReadWriteTask(readWriteTask);
			}
			return readWriteTask;
		}

		/// <summary>Waits for the pending asynchronous read to complete. (Consider using <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" /> instead.)</summary>
		/// <param name="asyncResult">The reference to the pending asynchronous request to finish.</param>
		/// <returns>The number of bytes read from the stream, between zero (0) and the number of bytes you requested. Streams return zero (0) only at the end of the stream, otherwise, they should block until at least one byte is available.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A handle to the pending read operation is not available.  
		///  -or-  
		///  The pending operation does not support reading.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Stream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream.</exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or an internal error has occurred.</exception>
		// Token: 0x0600686D RID: 26733 RVA: 0x00164EE8 File Offset: 0x001630E8
		public virtual int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Stream.ReadWriteTask activeReadWriteTask = this._activeReadWriteTask;
			if (activeReadWriteTask == null)
			{
				throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndRead was called multiple times with the same IAsyncResult.");
			}
			if (activeReadWriteTask != asyncResult)
			{
				throw new InvalidOperationException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndRead was called multiple times with the same IAsyncResult.");
			}
			if (!activeReadWriteTask._isRead)
			{
				throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndRead was called multiple times with the same IAsyncResult.");
			}
			int result;
			try
			{
				result = activeReadWriteTask.GetAwaiter().GetResult();
			}
			finally
			{
				this.FinishTrackingAsyncOperation();
			}
			return result;
		}

		/// <summary>Asynchronously reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.</summary>
		/// <param name="buffer">The buffer to write the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous read operation.</exception>
		// Token: 0x0600686E RID: 26734 RVA: 0x00164F64 File Offset: 0x00163164
		public Task<int> ReadAsync(byte[] buffer, int offset, int count)
		{
			return this.ReadAsync(buffer, offset, count, CancellationToken.None);
		}

		/// <summary>Asynchronously reads a sequence of bytes from the current stream, advances the position within the stream by the number of bytes read, and monitors cancellation requests.</summary>
		/// <param name="buffer">The buffer to write the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous read operation.</exception>
		// Token: 0x0600686F RID: 26735 RVA: 0x00164F74 File Offset: 0x00163174
		public virtual Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return this.BeginEndReadAsync(buffer, offset, count);
			}
			return Task.FromCanceled<int>(cancellationToken);
		}

		// Token: 0x06006870 RID: 26736 RVA: 0x00164F90 File Offset: 0x00163190
		public virtual ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			ArraySegment<byte> arraySegment;
			if (MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment))
			{
				return new ValueTask<int>(this.ReadAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count, cancellationToken));
			}
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			return Stream.<ReadAsync>g__FinishReadAsync|44_0(this.ReadAsync(array, 0, buffer.Length, cancellationToken), array, buffer);
		}

		// Token: 0x06006871 RID: 26737 RVA: 0x00164FF8 File Offset: 0x001631F8
		private Task<int> BeginEndReadAsync(byte[] buffer, int offset, int count)
		{
			if (!this.HasOverriddenBeginEndRead())
			{
				return (Task<int>)this.BeginReadInternal(buffer, offset, count, null, null, true, false);
			}
			return TaskFactory<int>.FromAsyncTrim<Stream, Stream.ReadWriteParameters>(this, new Stream.ReadWriteParameters
			{
				Buffer = buffer,
				Offset = offset,
				Count = count
			}, (Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state) => stream.BeginRead(args.Buffer, args.Offset, args.Count, callback, state), (Stream stream, IAsyncResult asyncResult) => stream.EndRead(asyncResult));
		}

		/// <summary>Begins an asynchronous write operation. (Consider using <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" /> instead.)</summary>
		/// <param name="buffer">The buffer to write data from.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> from which to begin writing.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="callback">An optional asynchronous callback, to be called when the write is complete.</param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
		/// <returns>An <see langword="IAsyncResult" /> that represents the asynchronous write, which could still be pending.</returns>
		/// <exception cref="T:System.IO.IOException">Attempted an asynchronous write past the end of the stream, or a disk error occurs.</exception>
		/// <exception cref="T:System.ArgumentException">One or more of the arguments is invalid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current <see langword="Stream" /> implementation does not support the write operation.</exception>
		// Token: 0x06006872 RID: 26738 RVA: 0x00165085 File Offset: 0x00163285
		public virtual IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.BeginWriteInternal(buffer, offset, count, callback, state, false, true);
		}

		// Token: 0x06006873 RID: 26739 RVA: 0x00165098 File Offset: 0x00163298
		internal IAsyncResult BeginWriteInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, bool serializeAsynchronously, bool apm)
		{
			if (!this.CanWrite)
			{
				throw Error.GetWriteNotSupported();
			}
			SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
			Task task = null;
			if (serializeAsynchronously)
			{
				task = semaphoreSlim.WaitAsync();
			}
			else
			{
				semaphoreSlim.Wait();
			}
			Stream.ReadWriteTask readWriteTask = new Stream.ReadWriteTask(false, apm, delegate(object <p0>)
			{
				Stream.ReadWriteTask readWriteTask2 = Task.InternalCurrent as Stream.ReadWriteTask;
				int result;
				try
				{
					readWriteTask2._stream.Write(readWriteTask2._buffer, readWriteTask2._offset, readWriteTask2._count);
					result = 0;
				}
				finally
				{
					if (!readWriteTask2._apm)
					{
						readWriteTask2._stream.FinishTrackingAsyncOperation();
					}
					readWriteTask2.ClearBeginState();
				}
				return result;
			}, state, this, buffer, offset, count, callback);
			if (task != null)
			{
				this.RunReadWriteTaskWhenReady(task, readWriteTask);
			}
			else
			{
				this.RunReadWriteTask(readWriteTask);
			}
			return readWriteTask;
		}

		// Token: 0x06006874 RID: 26740 RVA: 0x00165114 File Offset: 0x00163314
		private void RunReadWriteTaskWhenReady(Task asyncWaiter, Stream.ReadWriteTask readWriteTask)
		{
			if (asyncWaiter.IsCompleted)
			{
				this.RunReadWriteTask(readWriteTask);
				return;
			}
			asyncWaiter.ContinueWith(delegate(Task t, object state)
			{
				Stream.ReadWriteTask readWriteTask2 = (Stream.ReadWriteTask)state;
				readWriteTask2._stream.RunReadWriteTask(readWriteTask2);
			}, readWriteTask, default(CancellationToken), TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		// Token: 0x06006875 RID: 26741 RVA: 0x0016516B File Offset: 0x0016336B
		private void RunReadWriteTask(Stream.ReadWriteTask readWriteTask)
		{
			this._activeReadWriteTask = readWriteTask;
			readWriteTask.m_taskScheduler = TaskScheduler.Default;
			readWriteTask.ScheduleAndStart(false);
		}

		// Token: 0x06006876 RID: 26742 RVA: 0x00165186 File Offset: 0x00163386
		private void FinishTrackingAsyncOperation()
		{
			this._activeReadWriteTask = null;
			this._asyncActiveSemaphore.Release();
		}

		/// <summary>Ends an asynchronous write operation. (Consider using <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" /> instead.)</summary>
		/// <param name="asyncResult">A reference to the outstanding asynchronous I/O request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A handle to the pending write operation is not available.  
		///  -or-  
		///  The pending operation does not support writing.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream.</exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or an internal error has occurred.</exception>
		// Token: 0x06006877 RID: 26743 RVA: 0x0016519C File Offset: 0x0016339C
		public virtual void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Stream.ReadWriteTask activeReadWriteTask = this._activeReadWriteTask;
			if (activeReadWriteTask == null)
			{
				throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndWrite was called multiple times with the same IAsyncResult.");
			}
			if (activeReadWriteTask != asyncResult)
			{
				throw new InvalidOperationException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndWrite was called multiple times with the same IAsyncResult.");
			}
			if (activeReadWriteTask._isRead)
			{
				throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndWrite was called multiple times with the same IAsyncResult.");
			}
			try
			{
				activeReadWriteTask.GetAwaiter().GetResult();
			}
			finally
			{
				this.FinishTrackingAsyncOperation();
			}
		}

		/// <summary>Asynchronously writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.</summary>
		/// <param name="buffer">The buffer to write data from.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> from which to begin copying bytes to the stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous write operation.</exception>
		// Token: 0x06006878 RID: 26744 RVA: 0x00165218 File Offset: 0x00163418
		public Task WriteAsync(byte[] buffer, int offset, int count)
		{
			return this.WriteAsync(buffer, offset, count, CancellationToken.None);
		}

		/// <summary>Asynchronously writes a sequence of bytes to the current stream, advances the current position within this stream by the number of bytes written, and monitors cancellation requests.</summary>
		/// <param name="buffer">The buffer to write data from.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> from which to begin copying bytes to the stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous write operation.</exception>
		// Token: 0x06006879 RID: 26745 RVA: 0x00165228 File Offset: 0x00163428
		public virtual Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return this.BeginEndWriteAsync(buffer, offset, count);
			}
			return Task.FromCanceled(cancellationToken);
		}

		// Token: 0x0600687A RID: 26746 RVA: 0x00165244 File Offset: 0x00163444
		public virtual ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			ArraySegment<byte> arraySegment;
			if (MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment))
			{
				return new ValueTask(this.WriteAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count, cancellationToken));
			}
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			buffer.Span.CopyTo(array);
			return new ValueTask(this.FinishWriteAsync(this.WriteAsync(array, 0, buffer.Length, cancellationToken), array));
		}

		// Token: 0x0600687B RID: 26747 RVA: 0x001652C0 File Offset: 0x001634C0
		private Task FinishWriteAsync(Task writeTask, byte[] localBuffer)
		{
			Stream.<FinishWriteAsync>d__57 <FinishWriteAsync>d__;
			<FinishWriteAsync>d__.writeTask = writeTask;
			<FinishWriteAsync>d__.localBuffer = localBuffer;
			<FinishWriteAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishWriteAsync>d__.<>1__state = -1;
			<FinishWriteAsync>d__.<>t__builder.Start<Stream.<FinishWriteAsync>d__57>(ref <FinishWriteAsync>d__);
			return <FinishWriteAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600687C RID: 26748 RVA: 0x0016530C File Offset: 0x0016350C
		private Task BeginEndWriteAsync(byte[] buffer, int offset, int count)
		{
			if (!this.HasOverriddenBeginEndWrite())
			{
				return (Task)this.BeginWriteInternal(buffer, offset, count, null, null, true, false);
			}
			return TaskFactory<VoidTaskResult>.FromAsyncTrim<Stream, Stream.ReadWriteParameters>(this, new Stream.ReadWriteParameters
			{
				Buffer = buffer,
				Offset = offset,
				Count = count
			}, (Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state) => stream.BeginWrite(args.Buffer, args.Offset, args.Count, callback, state), delegate(Stream stream, IAsyncResult asyncResult)
			{
				stream.EndWrite(asyncResult);
				return default(VoidTaskResult);
			});
		}

		/// <summary>When overridden in a derived class, sets the position within the current stream.</summary>
		/// <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter.</param>
		/// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the reference point used to obtain the new position.</param>
		/// <returns>The new position within the current stream.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking, such as if the stream is constructed from a pipe or console output.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		// Token: 0x0600687D RID: 26749
		public abstract long Seek(long offset, SeekOrigin origin);

		/// <summary>When overridden in a derived class, sets the length of the current stream.</summary>
		/// <param name="value">The desired length of the current stream in bytes.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking, such as if the stream is constructed from a pipe or console output.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		// Token: 0x0600687E RID: 26750
		public abstract void SetLength(long value);

		/// <summary>When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.</summary>
		/// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream.</param>
		/// <param name="count">The maximum number of bytes to be read from the current stream.</param>
		/// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		// Token: 0x0600687F RID: 26751
		public abstract int Read(byte[] buffer, int offset, int count);

		// Token: 0x06006880 RID: 26752 RVA: 0x0016539C File Offset: 0x0016359C
		public virtual int Read(Span<byte> buffer)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			int result;
			try
			{
				int num = this.Read(array, 0, buffer.Length);
				if ((ulong)num > (ulong)((long)buffer.Length))
				{
					throw new IOException("Stream was too long.");
				}
				new Span<byte>(array, 0, num).CopyTo(buffer);
				result = num;
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return result;
		}

		/// <summary>Reads a byte from the stream and advances the position within the stream by one byte, or returns -1 if at the end of the stream.</summary>
		/// <returns>The unsigned byte cast to an <see langword="Int32" />, or -1 if at the end of the stream.</returns>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		// Token: 0x06006881 RID: 26753 RVA: 0x00165418 File Offset: 0x00163618
		public virtual int ReadByte()
		{
			byte[] array = new byte[1];
			if (this.Read(array, 0, 1) == 0)
			{
				return -1;
			}
			return (int)array[0];
		}

		/// <summary>When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.</summary>
		/// <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The number of bytes to be written to the current stream.</param>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occured, such as the specified file cannot be found.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="M:System.IO.Stream.Write(System.Byte[],System.Int32,System.Int32)" /> was called after the stream was closed.</exception>
		// Token: 0x06006882 RID: 26754
		public abstract void Write(byte[] buffer, int offset, int count);

		// Token: 0x06006883 RID: 26755 RVA: 0x0016543C File Offset: 0x0016363C
		public virtual void Write(ReadOnlySpan<byte> buffer)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			try
			{
				buffer.CopyTo(array);
				this.Write(array, 0, buffer.Length);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		/// <summary>Writes a byte to the current position in the stream and advances the position within the stream by one byte.</summary>
		/// <param name="value">The byte to write to the stream.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing, or the stream is already closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		// Token: 0x06006884 RID: 26756 RVA: 0x00165498 File Offset: 0x00163698
		public virtual void WriteByte(byte value)
		{
			this.Write(new byte[]
			{
				value
			}, 0, 1);
		}

		/// <summary>Creates a thread-safe (synchronized) wrapper around the specified <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> object to synchronize.</param>
		/// <returns>A thread-safe <see cref="T:System.IO.Stream" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		// Token: 0x06006885 RID: 26757 RVA: 0x001654B9 File Offset: 0x001636B9
		public static Stream Synchronized(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (stream is Stream.SyncStream)
			{
				return stream;
			}
			return new Stream.SyncStream(stream);
		}

		/// <summary>Provides support for a <see cref="T:System.Diagnostics.Contracts.Contract" />.</summary>
		// Token: 0x06006886 RID: 26758 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Obsolete("Do not call or override this method.")]
		protected virtual void ObjectInvariant()
		{
		}

		// Token: 0x06006887 RID: 26759 RVA: 0x001654DC File Offset: 0x001636DC
		internal IAsyncResult BlockingBeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			Stream.SynchronousAsyncResult synchronousAsyncResult;
			try
			{
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(this.Read(buffer, offset, count), state);
			}
			catch (IOException ex)
			{
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(ex, state, false);
			}
			if (callback != null)
			{
				callback(synchronousAsyncResult);
			}
			return synchronousAsyncResult;
		}

		// Token: 0x06006888 RID: 26760 RVA: 0x00165524 File Offset: 0x00163724
		internal static int BlockingEndRead(IAsyncResult asyncResult)
		{
			return Stream.SynchronousAsyncResult.EndRead(asyncResult);
		}

		// Token: 0x06006889 RID: 26761 RVA: 0x0016552C File Offset: 0x0016372C
		internal IAsyncResult BlockingBeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			Stream.SynchronousAsyncResult synchronousAsyncResult;
			try
			{
				this.Write(buffer, offset, count);
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(state);
			}
			catch (IOException ex)
			{
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(ex, state, true);
			}
			if (callback != null)
			{
				callback(synchronousAsyncResult);
			}
			return synchronousAsyncResult;
		}

		// Token: 0x0600688A RID: 26762 RVA: 0x00165574 File Offset: 0x00163774
		internal static void BlockingEndWrite(IAsyncResult asyncResult)
		{
			Stream.SynchronousAsyncResult.EndWrite(asyncResult);
		}

		// Token: 0x0600688B RID: 26763 RVA: 0x000040F7 File Offset: 0x000022F7
		private bool HasOverriddenBeginEndRead()
		{
			return true;
		}

		// Token: 0x0600688C RID: 26764 RVA: 0x000040F7 File Offset: 0x000022F7
		private bool HasOverriddenBeginEndWrite()
		{
			return true;
		}

		// Token: 0x0600688D RID: 26765 RVA: 0x0016557C File Offset: 0x0016377C
		public virtual ValueTask DisposeAsync()
		{
			ValueTask valueTask;
			try
			{
				this.Dispose();
				valueTask = default(ValueTask);
				valueTask = valueTask;
			}
			catch (Exception exception)
			{
				valueTask = new ValueTask(Task.FromException(exception));
			}
			return valueTask;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Stream" /> class.</summary>
		// Token: 0x0600688E RID: 26766 RVA: 0x00053949 File Offset: 0x00051B49
		protected Stream()
		{
		}

		// Token: 0x0600688F RID: 26767 RVA: 0x001655BC File Offset: 0x001637BC
		// Note: this type is marked as 'beforefieldinit'.
		static Stream()
		{
		}

		// Token: 0x06006890 RID: 26768 RVA: 0x001655C8 File Offset: 0x001637C8
		[CompilerGenerated]
		internal static ValueTask<int> <ReadAsync>g__FinishReadAsync|44_0(Task<int> readTask, byte[] localBuffer, Memory<byte> localDestination)
		{
			Stream.<<ReadAsync>g__FinishReadAsync|44_0>d <<ReadAsync>g__FinishReadAsync|44_0>d;
			<<ReadAsync>g__FinishReadAsync|44_0>d.readTask = readTask;
			<<ReadAsync>g__FinishReadAsync|44_0>d.localBuffer = localBuffer;
			<<ReadAsync>g__FinishReadAsync|44_0>d.localDestination = localDestination;
			<<ReadAsync>g__FinishReadAsync|44_0>d.<>t__builder = AsyncValueTaskMethodBuilder<int>.Create();
			<<ReadAsync>g__FinishReadAsync|44_0>d.<>1__state = -1;
			<<ReadAsync>g__FinishReadAsync|44_0>d.<>t__builder.Start<Stream.<<ReadAsync>g__FinishReadAsync|44_0>d>(ref <<ReadAsync>g__FinishReadAsync|44_0>d);
			return <<ReadAsync>g__FinishReadAsync|44_0>d.<>t__builder.Task;
		}

		/// <summary>A <see langword="Stream" /> with no backing store.</summary>
		// Token: 0x04003CD1 RID: 15569
		public static readonly Stream Null = new Stream.NullStream();

		// Token: 0x04003CD2 RID: 15570
		private const int DefaultCopyBufferSize = 81920;

		// Token: 0x04003CD3 RID: 15571
		[NonSerialized]
		private Stream.ReadWriteTask _activeReadWriteTask;

		// Token: 0x04003CD4 RID: 15572
		[NonSerialized]
		private SemaphoreSlim _asyncActiveSemaphore;

		// Token: 0x02000B4B RID: 2891
		private struct ReadWriteParameters
		{
			// Token: 0x04003CD5 RID: 15573
			internal byte[] Buffer;

			// Token: 0x04003CD6 RID: 15574
			internal int Offset;

			// Token: 0x04003CD7 RID: 15575
			internal int Count;
		}

		// Token: 0x02000B4C RID: 2892
		private sealed class ReadWriteTask : Task<int>, ITaskCompletionAction
		{
			// Token: 0x06006891 RID: 26769 RVA: 0x0016561B File Offset: 0x0016381B
			internal void ClearBeginState()
			{
				this._stream = null;
				this._buffer = null;
			}

			// Token: 0x06006892 RID: 26770 RVA: 0x0016562C File Offset: 0x0016382C
			public ReadWriteTask(bool isRead, bool apm, Func<object, int> function, object state, Stream stream, byte[] buffer, int offset, int count, AsyncCallback callback) : base(function, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach)
			{
				this._isRead = isRead;
				this._apm = apm;
				this._stream = stream;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
				if (callback != null)
				{
					this._callback = callback;
					this._context = ExecutionContext.Capture();
					base.AddCompletionAction(this);
				}
			}

			// Token: 0x06006893 RID: 26771 RVA: 0x00165694 File Offset: 0x00163894
			private static void InvokeAsyncCallback(object completedTask)
			{
				Stream.ReadWriteTask readWriteTask = (Stream.ReadWriteTask)completedTask;
				AsyncCallback callback = readWriteTask._callback;
				readWriteTask._callback = null;
				callback(readWriteTask);
			}

			// Token: 0x06006894 RID: 26772 RVA: 0x001656BC File Offset: 0x001638BC
			void ITaskCompletionAction.Invoke(Task completingTask)
			{
				ExecutionContext context = this._context;
				if (context == null)
				{
					AsyncCallback callback = this._callback;
					this._callback = null;
					callback(completingTask);
					return;
				}
				this._context = null;
				ContextCallback contextCallback = Stream.ReadWriteTask.s_invokeAsyncCallback;
				if (contextCallback == null)
				{
					contextCallback = (Stream.ReadWriteTask.s_invokeAsyncCallback = new ContextCallback(Stream.ReadWriteTask.InvokeAsyncCallback));
				}
				ExecutionContext.RunInternal(context, contextCallback, this);
			}

			// Token: 0x17001213 RID: 4627
			// (get) Token: 0x06006895 RID: 26773 RVA: 0x000040F7 File Offset: 0x000022F7
			bool ITaskCompletionAction.InvokeMayRunArbitraryCode
			{
				get
				{
					return true;
				}
			}

			// Token: 0x04003CD8 RID: 15576
			internal readonly bool _isRead;

			// Token: 0x04003CD9 RID: 15577
			internal readonly bool _apm;

			// Token: 0x04003CDA RID: 15578
			internal Stream _stream;

			// Token: 0x04003CDB RID: 15579
			internal byte[] _buffer;

			// Token: 0x04003CDC RID: 15580
			internal readonly int _offset;

			// Token: 0x04003CDD RID: 15581
			internal readonly int _count;

			// Token: 0x04003CDE RID: 15582
			private AsyncCallback _callback;

			// Token: 0x04003CDF RID: 15583
			private ExecutionContext _context;

			// Token: 0x04003CE0 RID: 15584
			private static ContextCallback s_invokeAsyncCallback;
		}

		// Token: 0x02000B4D RID: 2893
		private sealed class NullStream : Stream
		{
			// Token: 0x06006896 RID: 26774 RVA: 0x00165712 File Offset: 0x00163912
			internal NullStream()
			{
			}

			// Token: 0x17001214 RID: 4628
			// (get) Token: 0x06006897 RID: 26775 RVA: 0x000040F7 File Offset: 0x000022F7
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001215 RID: 4629
			// (get) Token: 0x06006898 RID: 26776 RVA: 0x000040F7 File Offset: 0x000022F7
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001216 RID: 4630
			// (get) Token: 0x06006899 RID: 26777 RVA: 0x000040F7 File Offset: 0x000022F7
			public override bool CanSeek
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001217 RID: 4631
			// (get) Token: 0x0600689A RID: 26778 RVA: 0x0005CD46 File Offset: 0x0005AF46
			public override long Length
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x17001218 RID: 4632
			// (get) Token: 0x0600689B RID: 26779 RVA: 0x0005CD46 File Offset: 0x0005AF46
			// (set) Token: 0x0600689C RID: 26780 RVA: 0x00004BF9 File Offset: 0x00002DF9
			public override long Position
			{
				get
				{
					return 0L;
				}
				set
				{
				}
			}

			// Token: 0x0600689D RID: 26781 RVA: 0x0016571A File Offset: 0x0016391A
			public override void CopyTo(Stream destination, int bufferSize)
			{
				StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			}

			// Token: 0x0600689E RID: 26782 RVA: 0x00165724 File Offset: 0x00163924
			public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
			{
				StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
				if (!cancellationToken.IsCancellationRequested)
				{
					return Task.CompletedTask;
				}
				return Task.FromCanceled(cancellationToken);
			}

			// Token: 0x0600689F RID: 26783 RVA: 0x00004BF9 File Offset: 0x00002DF9
			protected override void Dispose(bool disposing)
			{
			}

			// Token: 0x060068A0 RID: 26784 RVA: 0x00004BF9 File Offset: 0x00002DF9
			public override void Flush()
			{
			}

			// Token: 0x060068A1 RID: 26785 RVA: 0x00165743 File Offset: 0x00163943
			public override Task FlushAsync(CancellationToken cancellationToken)
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					return Task.CompletedTask;
				}
				return Task.FromCanceled(cancellationToken);
			}

			// Token: 0x060068A2 RID: 26786 RVA: 0x0016575A File Offset: 0x0016395A
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (!this.CanRead)
				{
					throw Error.GetReadNotSupported();
				}
				return base.BlockingBeginRead(buffer, offset, count, callback, state);
			}

			// Token: 0x060068A3 RID: 26787 RVA: 0x00165777 File Offset: 0x00163977
			public override int EndRead(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				return Stream.BlockingEndRead(asyncResult);
			}

			// Token: 0x060068A4 RID: 26788 RVA: 0x0016578D File Offset: 0x0016398D
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (!this.CanWrite)
				{
					throw Error.GetWriteNotSupported();
				}
				return base.BlockingBeginWrite(buffer, offset, count, callback, state);
			}

			// Token: 0x060068A5 RID: 26789 RVA: 0x001657AA File Offset: 0x001639AA
			public override void EndWrite(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Stream.BlockingEndWrite(asyncResult);
			}

			// Token: 0x060068A6 RID: 26790 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
			public override int Read(byte[] buffer, int offset, int count)
			{
				return 0;
			}

			// Token: 0x060068A7 RID: 26791 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
			public override int Read(Span<byte> buffer)
			{
				return 0;
			}

			// Token: 0x060068A8 RID: 26792 RVA: 0x001657C0 File Offset: 0x001639C0
			public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				return Stream.NullStream.s_zeroTask;
			}

			// Token: 0x060068A9 RID: 26793 RVA: 0x001657C7 File Offset: 0x001639C7
			public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
			{
				return new ValueTask<int>(0);
			}

			// Token: 0x060068AA RID: 26794 RVA: 0x0012275A File Offset: 0x0012095A
			public override int ReadByte()
			{
				return -1;
			}

			// Token: 0x060068AB RID: 26795 RVA: 0x00004BF9 File Offset: 0x00002DF9
			public override void Write(byte[] buffer, int offset, int count)
			{
			}

			// Token: 0x060068AC RID: 26796 RVA: 0x00004BF9 File Offset: 0x00002DF9
			public override void Write(ReadOnlySpan<byte> buffer)
			{
			}

			// Token: 0x060068AD RID: 26797 RVA: 0x001657CF File Offset: 0x001639CF
			public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					return Task.CompletedTask;
				}
				return Task.FromCanceled(cancellationToken);
			}

			// Token: 0x060068AE RID: 26798 RVA: 0x001657E8 File Offset: 0x001639E8
			public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					return default(ValueTask);
				}
				return new ValueTask(Task.FromCanceled(cancellationToken));
			}

			// Token: 0x060068AF RID: 26799 RVA: 0x00004BF9 File Offset: 0x00002DF9
			public override void WriteByte(byte value)
			{
			}

			// Token: 0x060068B0 RID: 26800 RVA: 0x0005CD46 File Offset: 0x0005AF46
			public override long Seek(long offset, SeekOrigin origin)
			{
				return 0L;
			}

			// Token: 0x060068B1 RID: 26801 RVA: 0x00004BF9 File Offset: 0x00002DF9
			public override void SetLength(long length)
			{
			}

			// Token: 0x060068B2 RID: 26802 RVA: 0x00165813 File Offset: 0x00163A13
			// Note: this type is marked as 'beforefieldinit'.
			static NullStream()
			{
			}

			// Token: 0x04003CE1 RID: 15585
			private static readonly Task<int> s_zeroTask = Task.FromResult<int>(0);
		}

		// Token: 0x02000B4E RID: 2894
		private sealed class SynchronousAsyncResult : IAsyncResult
		{
			// Token: 0x060068B3 RID: 26803 RVA: 0x00165820 File Offset: 0x00163A20
			internal SynchronousAsyncResult(int bytesRead, object asyncStateObject)
			{
				this._bytesRead = bytesRead;
				this._stateObject = asyncStateObject;
			}

			// Token: 0x060068B4 RID: 26804 RVA: 0x00165836 File Offset: 0x00163A36
			internal SynchronousAsyncResult(object asyncStateObject)
			{
				this._stateObject = asyncStateObject;
				this._isWrite = true;
			}

			// Token: 0x060068B5 RID: 26805 RVA: 0x0016584C File Offset: 0x00163A4C
			internal SynchronousAsyncResult(Exception ex, object asyncStateObject, bool isWrite)
			{
				this._exceptionInfo = ExceptionDispatchInfo.Capture(ex);
				this._stateObject = asyncStateObject;
				this._isWrite = isWrite;
			}

			// Token: 0x17001219 RID: 4633
			// (get) Token: 0x060068B6 RID: 26806 RVA: 0x000040F7 File Offset: 0x000022F7
			public bool IsCompleted
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700121A RID: 4634
			// (get) Token: 0x060068B7 RID: 26807 RVA: 0x0016586E File Offset: 0x00163A6E
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					return LazyInitializer.EnsureInitialized<ManualResetEvent>(ref this._waitHandle, () => new ManualResetEvent(true));
				}
			}

			// Token: 0x1700121B RID: 4635
			// (get) Token: 0x060068B8 RID: 26808 RVA: 0x0016589A File Offset: 0x00163A9A
			public object AsyncState
			{
				get
				{
					return this._stateObject;
				}
			}

			// Token: 0x1700121C RID: 4636
			// (get) Token: 0x060068B9 RID: 26809 RVA: 0x000040F7 File Offset: 0x000022F7
			public bool CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060068BA RID: 26810 RVA: 0x001658A2 File Offset: 0x00163AA2
			internal void ThrowIfError()
			{
				if (this._exceptionInfo != null)
				{
					this._exceptionInfo.Throw();
				}
			}

			// Token: 0x060068BB RID: 26811 RVA: 0x001658B8 File Offset: 0x00163AB8
			internal static int EndRead(IAsyncResult asyncResult)
			{
				Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
				if (synchronousAsyncResult == null || synchronousAsyncResult._isWrite)
				{
					throw new ArgumentException("IAsyncResult object did not come from the corresponding async method on this type.");
				}
				if (synchronousAsyncResult._endXxxCalled)
				{
					throw new ArgumentException("EndRead can only be called once for each asynchronous operation.");
				}
				synchronousAsyncResult._endXxxCalled = true;
				synchronousAsyncResult.ThrowIfError();
				return synchronousAsyncResult._bytesRead;
			}

			// Token: 0x060068BC RID: 26812 RVA: 0x00165908 File Offset: 0x00163B08
			internal static void EndWrite(IAsyncResult asyncResult)
			{
				Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
				if (synchronousAsyncResult == null || !synchronousAsyncResult._isWrite)
				{
					throw new ArgumentException("IAsyncResult object did not come from the corresponding async method on this type.");
				}
				if (synchronousAsyncResult._endXxxCalled)
				{
					throw new ArgumentException("EndWrite can only be called once for each asynchronous operation.");
				}
				synchronousAsyncResult._endXxxCalled = true;
				synchronousAsyncResult.ThrowIfError();
			}

			// Token: 0x04003CE2 RID: 15586
			private readonly object _stateObject;

			// Token: 0x04003CE3 RID: 15587
			private readonly bool _isWrite;

			// Token: 0x04003CE4 RID: 15588
			private ManualResetEvent _waitHandle;

			// Token: 0x04003CE5 RID: 15589
			private ExceptionDispatchInfo _exceptionInfo;

			// Token: 0x04003CE6 RID: 15590
			private bool _endXxxCalled;

			// Token: 0x04003CE7 RID: 15591
			private int _bytesRead;

			// Token: 0x02000B4F RID: 2895
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x060068BD RID: 26813 RVA: 0x00165952 File Offset: 0x00163B52
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x060068BE RID: 26814 RVA: 0x0000259F File Offset: 0x0000079F
				public <>c()
				{
				}

				// Token: 0x060068BF RID: 26815 RVA: 0x0016595E File Offset: 0x00163B5E
				internal ManualResetEvent <get_AsyncWaitHandle>b__12_0()
				{
					return new ManualResetEvent(true);
				}

				// Token: 0x04003CE8 RID: 15592
				public static readonly Stream.SynchronousAsyncResult.<>c <>9 = new Stream.SynchronousAsyncResult.<>c();

				// Token: 0x04003CE9 RID: 15593
				public static Func<ManualResetEvent> <>9__12_0;
			}
		}

		// Token: 0x02000B50 RID: 2896
		private sealed class SyncStream : Stream, IDisposable
		{
			// Token: 0x060068C0 RID: 26816 RVA: 0x00165966 File Offset: 0x00163B66
			internal SyncStream(Stream stream)
			{
				if (stream == null)
				{
					throw new ArgumentNullException("stream");
				}
				this._stream = stream;
			}

			// Token: 0x1700121D RID: 4637
			// (get) Token: 0x060068C1 RID: 26817 RVA: 0x00165983 File Offset: 0x00163B83
			public override bool CanRead
			{
				get
				{
					return this._stream.CanRead;
				}
			}

			// Token: 0x1700121E RID: 4638
			// (get) Token: 0x060068C2 RID: 26818 RVA: 0x00165990 File Offset: 0x00163B90
			public override bool CanWrite
			{
				get
				{
					return this._stream.CanWrite;
				}
			}

			// Token: 0x1700121F RID: 4639
			// (get) Token: 0x060068C3 RID: 26819 RVA: 0x0016599D File Offset: 0x00163B9D
			public override bool CanSeek
			{
				get
				{
					return this._stream.CanSeek;
				}
			}

			// Token: 0x17001220 RID: 4640
			// (get) Token: 0x060068C4 RID: 26820 RVA: 0x001659AA File Offset: 0x00163BAA
			public override bool CanTimeout
			{
				get
				{
					return this._stream.CanTimeout;
				}
			}

			// Token: 0x17001221 RID: 4641
			// (get) Token: 0x060068C5 RID: 26821 RVA: 0x001659B8 File Offset: 0x00163BB8
			public override long Length
			{
				get
				{
					Stream stream = this._stream;
					long length;
					lock (stream)
					{
						length = this._stream.Length;
					}
					return length;
				}
			}

			// Token: 0x17001222 RID: 4642
			// (get) Token: 0x060068C6 RID: 26822 RVA: 0x00165A00 File Offset: 0x00163C00
			// (set) Token: 0x060068C7 RID: 26823 RVA: 0x00165A48 File Offset: 0x00163C48
			public override long Position
			{
				get
				{
					Stream stream = this._stream;
					long position;
					lock (stream)
					{
						position = this._stream.Position;
					}
					return position;
				}
				set
				{
					Stream stream = this._stream;
					lock (stream)
					{
						this._stream.Position = value;
					}
				}
			}

			// Token: 0x17001223 RID: 4643
			// (get) Token: 0x060068C8 RID: 26824 RVA: 0x00165A90 File Offset: 0x00163C90
			// (set) Token: 0x060068C9 RID: 26825 RVA: 0x00165A9D File Offset: 0x00163C9D
			public override int ReadTimeout
			{
				get
				{
					return this._stream.ReadTimeout;
				}
				set
				{
					this._stream.ReadTimeout = value;
				}
			}

			// Token: 0x17001224 RID: 4644
			// (get) Token: 0x060068CA RID: 26826 RVA: 0x00165AAB File Offset: 0x00163CAB
			// (set) Token: 0x060068CB RID: 26827 RVA: 0x00165AB8 File Offset: 0x00163CB8
			public override int WriteTimeout
			{
				get
				{
					return this._stream.WriteTimeout;
				}
				set
				{
					this._stream.WriteTimeout = value;
				}
			}

			// Token: 0x060068CC RID: 26828 RVA: 0x00165AC8 File Offset: 0x00163CC8
			public override void Close()
			{
				Stream stream = this._stream;
				lock (stream)
				{
					try
					{
						this._stream.Close();
					}
					finally
					{
						base.Dispose(true);
					}
				}
			}

			// Token: 0x060068CD RID: 26829 RVA: 0x00165B24 File Offset: 0x00163D24
			protected override void Dispose(bool disposing)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					try
					{
						if (disposing)
						{
							((IDisposable)this._stream).Dispose();
						}
					}
					finally
					{
						base.Dispose(disposing);
					}
				}
			}

			// Token: 0x060068CE RID: 26830 RVA: 0x00165B80 File Offset: 0x00163D80
			public override void Flush()
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.Flush();
				}
			}

			// Token: 0x060068CF RID: 26831 RVA: 0x00165BC8 File Offset: 0x00163DC8
			public override int Read(byte[] bytes, int offset, int count)
			{
				Stream stream = this._stream;
				int result;
				lock (stream)
				{
					result = this._stream.Read(bytes, offset, count);
				}
				return result;
			}

			// Token: 0x060068D0 RID: 26832 RVA: 0x00165C14 File Offset: 0x00163E14
			public override int Read(Span<byte> buffer)
			{
				Stream stream = this._stream;
				int result;
				lock (stream)
				{
					result = this._stream.Read(buffer);
				}
				return result;
			}

			// Token: 0x060068D1 RID: 26833 RVA: 0x00165C5C File Offset: 0x00163E5C
			public override int ReadByte()
			{
				Stream stream = this._stream;
				int result;
				lock (stream)
				{
					result = this._stream.ReadByte();
				}
				return result;
			}

			// Token: 0x060068D2 RID: 26834 RVA: 0x00165CA4 File Offset: 0x00163EA4
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				bool flag = this._stream.HasOverriddenBeginEndRead();
				Stream stream = this._stream;
				IAsyncResult result;
				lock (stream)
				{
					result = (flag ? this._stream.BeginRead(buffer, offset, count, callback, state) : this._stream.BeginReadInternal(buffer, offset, count, callback, state, true, true));
				}
				return result;
			}

			// Token: 0x060068D3 RID: 26835 RVA: 0x00165D18 File Offset: 0x00163F18
			public override int EndRead(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Stream stream = this._stream;
				int result;
				lock (stream)
				{
					result = this._stream.EndRead(asyncResult);
				}
				return result;
			}

			// Token: 0x060068D4 RID: 26836 RVA: 0x00165D70 File Offset: 0x00163F70
			public override long Seek(long offset, SeekOrigin origin)
			{
				Stream stream = this._stream;
				long result;
				lock (stream)
				{
					result = this._stream.Seek(offset, origin);
				}
				return result;
			}

			// Token: 0x060068D5 RID: 26837 RVA: 0x00165DBC File Offset: 0x00163FBC
			public override void SetLength(long length)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.SetLength(length);
				}
			}

			// Token: 0x060068D6 RID: 26838 RVA: 0x00165E04 File Offset: 0x00164004
			public override void Write(byte[] bytes, int offset, int count)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.Write(bytes, offset, count);
				}
			}

			// Token: 0x060068D7 RID: 26839 RVA: 0x00165E4C File Offset: 0x0016404C
			public override void Write(ReadOnlySpan<byte> buffer)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.Write(buffer);
				}
			}

			// Token: 0x060068D8 RID: 26840 RVA: 0x00165E94 File Offset: 0x00164094
			public override void WriteByte(byte b)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.WriteByte(b);
				}
			}

			// Token: 0x060068D9 RID: 26841 RVA: 0x00165EDC File Offset: 0x001640DC
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				bool flag = this._stream.HasOverriddenBeginEndWrite();
				Stream stream = this._stream;
				IAsyncResult result;
				lock (stream)
				{
					result = (flag ? this._stream.BeginWrite(buffer, offset, count, callback, state) : this._stream.BeginWriteInternal(buffer, offset, count, callback, state, true, true));
				}
				return result;
			}

			// Token: 0x060068DA RID: 26842 RVA: 0x00165F50 File Offset: 0x00164150
			public override void EndWrite(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.EndWrite(asyncResult);
				}
			}

			// Token: 0x04003CEA RID: 15594
			private Stream _stream;
		}

		// Token: 0x02000B51 RID: 2897
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060068DB RID: 26843 RVA: 0x00165FA4 File Offset: 0x001641A4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060068DC RID: 26844 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x060068DD RID: 26845 RVA: 0x000A598E File Offset: 0x000A3B8E
			internal SemaphoreSlim <EnsureAsyncActiveSemaphoreInitialized>b__4_0()
			{
				return new SemaphoreSlim(1, 1);
			}

			// Token: 0x060068DE RID: 26846 RVA: 0x00165FB0 File Offset: 0x001641B0
			internal void <FlushAsync>b__37_0(object state)
			{
				((Stream)state).Flush();
			}

			// Token: 0x060068DF RID: 26847 RVA: 0x00165FC0 File Offset: 0x001641C0
			internal int <BeginReadInternal>b__40_0(object <p0>)
			{
				Stream.ReadWriteTask readWriteTask = Task.InternalCurrent as Stream.ReadWriteTask;
				int result;
				try
				{
					result = readWriteTask._stream.Read(readWriteTask._buffer, readWriteTask._offset, readWriteTask._count);
				}
				finally
				{
					if (!readWriteTask._apm)
					{
						readWriteTask._stream.FinishTrackingAsyncOperation();
					}
					readWriteTask.ClearBeginState();
				}
				return result;
			}

			// Token: 0x060068E0 RID: 26848 RVA: 0x00166024 File Offset: 0x00164224
			internal IAsyncResult <BeginEndReadAsync>b__45_0(Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state)
			{
				return stream.BeginRead(args.Buffer, args.Offset, args.Count, callback, state);
			}

			// Token: 0x060068E1 RID: 26849 RVA: 0x00166041 File Offset: 0x00164241
			internal int <BeginEndReadAsync>b__45_1(Stream stream, IAsyncResult asyncResult)
			{
				return stream.EndRead(asyncResult);
			}

			// Token: 0x060068E2 RID: 26850 RVA: 0x0016604C File Offset: 0x0016424C
			internal int <BeginWriteInternal>b__48_0(object <p0>)
			{
				Stream.ReadWriteTask readWriteTask = Task.InternalCurrent as Stream.ReadWriteTask;
				int result;
				try
				{
					readWriteTask._stream.Write(readWriteTask._buffer, readWriteTask._offset, readWriteTask._count);
					result = 0;
				}
				finally
				{
					if (!readWriteTask._apm)
					{
						readWriteTask._stream.FinishTrackingAsyncOperation();
					}
					readWriteTask.ClearBeginState();
				}
				return result;
			}

			// Token: 0x060068E3 RID: 26851 RVA: 0x001660B0 File Offset: 0x001642B0
			internal void <RunReadWriteTaskWhenReady>b__49_0(Task t, object state)
			{
				Stream.ReadWriteTask readWriteTask = (Stream.ReadWriteTask)state;
				readWriteTask._stream.RunReadWriteTask(readWriteTask);
			}

			// Token: 0x060068E4 RID: 26852 RVA: 0x001660D0 File Offset: 0x001642D0
			internal IAsyncResult <BeginEndWriteAsync>b__58_0(Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state)
			{
				return stream.BeginWrite(args.Buffer, args.Offset, args.Count, callback, state);
			}

			// Token: 0x060068E5 RID: 26853 RVA: 0x001660F0 File Offset: 0x001642F0
			internal VoidTaskResult <BeginEndWriteAsync>b__58_1(Stream stream, IAsyncResult asyncResult)
			{
				stream.EndWrite(asyncResult);
				return default(VoidTaskResult);
			}

			// Token: 0x04003CEB RID: 15595
			public static readonly Stream.<>c <>9 = new Stream.<>c();

			// Token: 0x04003CEC RID: 15596
			public static Func<SemaphoreSlim> <>9__4_0;

			// Token: 0x04003CED RID: 15597
			public static Action<object> <>9__37_0;

			// Token: 0x04003CEE RID: 15598
			public static Func<object, int> <>9__40_0;

			// Token: 0x04003CEF RID: 15599
			public static Func<Stream, Stream.ReadWriteParameters, AsyncCallback, object, IAsyncResult> <>9__45_0;

			// Token: 0x04003CF0 RID: 15600
			public static Func<Stream, IAsyncResult, int> <>9__45_1;

			// Token: 0x04003CF1 RID: 15601
			public static Func<object, int> <>9__48_0;

			// Token: 0x04003CF2 RID: 15602
			public static Action<Task, object> <>9__49_0;

			// Token: 0x04003CF3 RID: 15603
			public static Func<Stream, Stream.ReadWriteParameters, AsyncCallback, object, IAsyncResult> <>9__58_0;

			// Token: 0x04003CF4 RID: 15604
			public static Func<Stream, IAsyncResult, VoidTaskResult> <>9__58_1;
		}

		// Token: 0x02000B52 RID: 2898
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <CopyToAsyncInternal>d__28 : IAsyncStateMachine
		{
			// Token: 0x060068E6 RID: 26854 RVA: 0x00166110 File Offset: 0x00164310
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				Stream stream = this.<>4__this;
				try
				{
					if (num > 1)
					{
						this.<buffer>5__2 = ArrayPool<byte>.Shared.Rent(this.bufferSize);
					}
					try
					{
						ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter awaiter;
						if (num == 0)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_A6;
						}
						ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter awaiter2;
						if (num == 1)
						{
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_12E;
						}
						IL_33:
						awaiter = stream.ReadAsync(new Memory<byte>(this.<buffer>5__2), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, Stream.<CopyToAsyncInternal>d__28>(ref awaiter, ref this);
							return;
						}
						IL_A6:
						int result = awaiter.GetResult();
						if (result == 0)
						{
							goto IL_152;
						}
						awaiter2 = this.destination.WriteAsync(new ReadOnlyMemory<byte>(this.<buffer>5__2, 0, result), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (this.<>1__state = 1);
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, Stream.<CopyToAsyncInternal>d__28>(ref awaiter2, ref this);
							return;
						}
						IL_12E:
						awaiter2.GetResult();
						goto IL_33;
					}
					finally
					{
						if (num < 0)
						{
							ArrayPool<byte>.Shared.Return(this.<buffer>5__2, false);
						}
					}
					IL_152:;
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

			// Token: 0x060068E7 RID: 26855 RVA: 0x001662E0 File Offset: 0x001644E0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003CF5 RID: 15605
			public int <>1__state;

			// Token: 0x04003CF6 RID: 15606
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04003CF7 RID: 15607
			public int bufferSize;

			// Token: 0x04003CF8 RID: 15608
			public Stream <>4__this;

			// Token: 0x04003CF9 RID: 15609
			public CancellationToken cancellationToken;

			// Token: 0x04003CFA RID: 15610
			public Stream destination;

			// Token: 0x04003CFB RID: 15611
			private byte[] <buffer>5__2;

			// Token: 0x04003CFC RID: 15612
			private ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter <>u__1;

			// Token: 0x04003CFD RID: 15613
			private ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter <>u__2;
		}

		// Token: 0x02000B53 RID: 2899
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FinishWriteAsync>d__57 : IAsyncStateMachine
		{
			// Token: 0x060068E8 RID: 26856 RVA: 0x001662F0 File Offset: 0x001644F0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = this.writeTask.ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, Stream.<FinishWriteAsync>d__57>(ref awaiter, ref this);
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
							ArrayPool<byte>.Shared.Return(this.localBuffer, false);
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

			// Token: 0x060068E9 RID: 26857 RVA: 0x001663CC File Offset: 0x001645CC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003CFE RID: 15614
			public int <>1__state;

			// Token: 0x04003CFF RID: 15615
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04003D00 RID: 15616
			public Task writeTask;

			// Token: 0x04003D01 RID: 15617
			public byte[] localBuffer;

			// Token: 0x04003D02 RID: 15618
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000B54 RID: 2900
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <<ReadAsync>g__FinishReadAsync|44_0>d : IAsyncStateMachine
		{
			// Token: 0x060068EA RID: 26858 RVA: 0x001663DC File Offset: 0x001645DC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				int result2;
				try
				{
					try
					{
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = this.readTask.ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, Stream.<<ReadAsync>g__FinishReadAsync|44_0>d>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						int result = awaiter.GetResult();
						new Span<byte>(this.localBuffer, 0, result).CopyTo(this.localDestination.Span);
						result2 = result;
					}
					finally
					{
						if (num < 0)
						{
							ArrayPool<byte>.Shared.Return(this.localBuffer, false);
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

			// Token: 0x060068EB RID: 26859 RVA: 0x001664E0 File Offset: 0x001646E0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003D03 RID: 15619
			public int <>1__state;

			// Token: 0x04003D04 RID: 15620
			public AsyncValueTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04003D05 RID: 15621
			public Task<int> readTask;

			// Token: 0x04003D06 RID: 15622
			public byte[] localBuffer;

			// Token: 0x04003D07 RID: 15623
			public Memory<byte> localDestination;

			// Token: 0x04003D08 RID: 15624
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
