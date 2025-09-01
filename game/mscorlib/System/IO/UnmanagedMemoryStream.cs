using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Provides access to unmanaged blocks of memory from managed code.</summary>
	// Token: 0x02000B29 RID: 2857
	public class UnmanagedMemoryStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class.</summary>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the required permission.</exception>
		// Token: 0x06006695 RID: 26261 RVA: 0x0015E326 File Offset: 0x0015C526
		protected UnmanagedMemoryStream()
		{
			this._mem = null;
			this._isOpen = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class in a safe buffer with a specified offset and length.</summary>
		/// <param name="buffer">The buffer to contain the unmanaged memory stream.</param>
		/// <param name="offset">The byte position in the buffer at which to start the unmanaged memory stream.</param>
		/// <param name="length">The length of the unmanaged memory stream.</param>
		// Token: 0x06006696 RID: 26262 RVA: 0x0015E33D File Offset: 0x0015C53D
		public UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length)
		{
			this.Initialize(buffer, offset, length, FileAccess.Read);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class in a safe buffer with a specified offset, length, and file access.</summary>
		/// <param name="buffer">The buffer to contain the unmanaged memory stream.</param>
		/// <param name="offset">The byte position in the buffer at which to start the unmanaged memory stream.</param>
		/// <param name="length">The length of the unmanaged memory stream.</param>
		/// <param name="access">The mode of file access to the unmanaged memory stream.</param>
		// Token: 0x06006697 RID: 26263 RVA: 0x0015E34F File Offset: 0x0015C54F
		public UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length, FileAccess access)
		{
			this.Initialize(buffer, offset, length, access);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class in a safe buffer with a specified offset, length, and file access.</summary>
		/// <param name="buffer">The buffer to contain the unmanaged memory stream.</param>
		/// <param name="offset">The byte position in the buffer at which to start the unmanaged memory stream.</param>
		/// <param name="length">The length of the unmanaged memory stream.</param>
		/// <param name="access">The mode of file access to the unmanaged memory stream.</param>
		// Token: 0x06006698 RID: 26264 RVA: 0x0015E364 File Offset: 0x0015C564
		protected unsafe void Initialize(SafeBuffer buffer, long offset, long length, FileAccess access)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length", "Non-negative number required.");
			}
			if (buffer.ByteLength < (ulong)(offset + length))
			{
				throw new ArgumentException("Offset and length were greater than the size of the SafeBuffer.");
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			if (this._isOpen)
			{
				throw new InvalidOperationException("The method cannot be called twice on the same instance.");
			}
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				buffer.AcquirePointer(ref ptr);
				if (ptr + offset + length < ptr)
				{
					throw new ArgumentException("The UnmanagedMemoryStream capacity would wrap around the high end of the address space.");
				}
			}
			finally
			{
				if (ptr != null)
				{
					buffer.ReleasePointer();
				}
			}
			this._offset = offset;
			this._buffer = buffer;
			this._length = length;
			this._capacity = length;
			this._access = access;
			this._isOpen = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class using the specified location and memory length.</summary>
		/// <param name="pointer">A pointer to an unmanaged memory location.</param>
		/// <param name="length">The length of the memory to use.</param>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pointer" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="length" /> value is less than zero.  
		/// -or-
		///  The <paramref name="length" /> is large enough to cause an overflow.</exception>
		// Token: 0x06006699 RID: 26265 RVA: 0x0015E458 File Offset: 0x0015C658
		[CLSCompliant(false)]
		public unsafe UnmanagedMemoryStream(byte* pointer, long length)
		{
			this.Initialize(pointer, length, length, FileAccess.Read);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class using the specified location, memory length, total amount of memory, and file access values.</summary>
		/// <param name="pointer">A pointer to an unmanaged memory location.</param>
		/// <param name="length">The length of the memory to use.</param>
		/// <param name="capacity">The total amount of memory assigned to the stream.</param>
		/// <param name="access">One of the <see cref="T:System.IO.FileAccess" /> values.</param>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pointer" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="length" /> value is less than zero.  
		/// -or-
		///  The <paramref name="capacity" /> value is less than zero.  
		/// -or-
		///  The <paramref name="length" /> value is greater than the <paramref name="capacity" /> value.</exception>
		// Token: 0x0600669A RID: 26266 RVA: 0x0015E46A File Offset: 0x0015C66A
		[CLSCompliant(false)]
		public unsafe UnmanagedMemoryStream(byte* pointer, long length, long capacity, FileAccess access)
		{
			this.Initialize(pointer, length, capacity, access);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class by using a pointer to an unmanaged memory location.</summary>
		/// <param name="pointer">A pointer to an unmanaged memory location.</param>
		/// <param name="length">The length of the memory to use.</param>
		/// <param name="capacity">The total amount of memory assigned to the stream.</param>
		/// <param name="access">One of the <see cref="T:System.IO.FileAccess" /> values.</param>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pointer" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="length" /> value is less than zero.  
		/// -or-
		///  The <paramref name="capacity" /> value is less than zero.  
		/// -or-
		///  The <paramref name="length" /> value is large enough to cause an overflow.</exception>
		// Token: 0x0600669B RID: 26267 RVA: 0x0015E480 File Offset: 0x0015C680
		[CLSCompliant(false)]
		protected unsafe void Initialize(byte* pointer, long length, long capacity, FileAccess access)
		{
			if (pointer == null)
			{
				throw new ArgumentNullException("pointer");
			}
			if (length < 0L || capacity < 0L)
			{
				throw new ArgumentOutOfRangeException((length < 0L) ? "length" : "capacity", "Non-negative number required.");
			}
			if (length > capacity)
			{
				throw new ArgumentOutOfRangeException("length", "The length cannot be greater than the capacity.");
			}
			if (pointer + capacity < pointer)
			{
				throw new ArgumentOutOfRangeException("capacity", "The UnmanagedMemoryStream capacity would wrap around the high end of the address space.");
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access", "Enum value was out of legal range.");
			}
			if (this._isOpen)
			{
				throw new InvalidOperationException("The method cannot be called twice on the same instance.");
			}
			this._mem = pointer;
			this._offset = 0L;
			this._length = length;
			this._capacity = capacity;
			this._access = access;
			this._isOpen = true;
		}

		/// <summary>Gets a value indicating whether a stream supports reading.</summary>
		/// <returns>
		///   <see langword="false" /> if the object was created by a constructor with an <paramref name="access" /> parameter that did not include reading the stream and if the stream is closed; otherwise, <see langword="true" />.</returns>
		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x0600669C RID: 26268 RVA: 0x0015E548 File Offset: 0x0015C748
		public override bool CanRead
		{
			get
			{
				return this._isOpen && (this._access & FileAccess.Read) > (FileAccess)0;
			}
		}

		/// <summary>Gets a value indicating whether a stream supports seeking.</summary>
		/// <returns>
		///   <see langword="false" /> if the stream is closed; otherwise, <see langword="true" />.</returns>
		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x0600669D RID: 26269 RVA: 0x0015E55F File Offset: 0x0015C75F
		public override bool CanSeek
		{
			get
			{
				return this._isOpen;
			}
		}

		/// <summary>Gets a value indicating whether a stream supports writing.</summary>
		/// <returns>
		///   <see langword="false" /> if the object was created by a constructor with an <paramref name="access" /> parameter value that supports writing or was created by a constructor that had no parameters, or if the stream is closed; otherwise, <see langword="true" />.</returns>
		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x0600669E RID: 26270 RVA: 0x0015E567 File Offset: 0x0015C767
		public override bool CanWrite
		{
			get
			{
				return this._isOpen && (this._access & FileAccess.Write) > (FileAccess)0;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.UnmanagedMemoryStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x0600669F RID: 26271 RVA: 0x0015E57E File Offset: 0x0015C77E
		protected override void Dispose(bool disposing)
		{
			this._isOpen = false;
			this._mem = null;
			base.Dispose(disposing);
		}

		// Token: 0x060066A0 RID: 26272 RVA: 0x0015E596 File Offset: 0x0015C796
		private void EnsureNotClosed()
		{
			if (!this._isOpen)
			{
				throw Error.GetStreamIsClosed();
			}
		}

		// Token: 0x060066A1 RID: 26273 RVA: 0x0015E5A6 File Offset: 0x0015C7A6
		private void EnsureReadable()
		{
			if (!this.CanRead)
			{
				throw Error.GetReadNotSupported();
			}
		}

		// Token: 0x060066A2 RID: 26274 RVA: 0x00156DC4 File Offset: 0x00154FC4
		private void EnsureWriteable()
		{
			if (!this.CanWrite)
			{
				throw Error.GetWriteNotSupported();
			}
		}

		/// <summary>Overrides the <see cref="M:System.IO.Stream.Flush" /> method so that no action is performed.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x060066A3 RID: 26275 RVA: 0x0015E5B6 File Offset: 0x0015C7B6
		public override void Flush()
		{
			this.EnsureNotClosed();
		}

		/// <summary>Overrides the <see cref="M:System.IO.Stream.FlushAsync(System.Threading.CancellationToken)" /> method so that the operation is cancelled if specified, but no other action is performed.  
		///  Available starting in .NET Framework 4.6</summary>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		// Token: 0x060066A4 RID: 26276 RVA: 0x0015E5C0 File Offset: 0x0015C7C0
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task result;
			try
			{
				this.Flush();
				result = Task.CompletedTask;
			}
			catch (Exception exception)
			{
				result = Task.FromException(exception);
			}
			return result;
		}

		/// <summary>Gets the length of the data in a stream.</summary>
		/// <returns>The length of the data in the stream.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x060066A5 RID: 26277 RVA: 0x0015E608 File Offset: 0x0015C808
		public override long Length
		{
			get
			{
				this.EnsureNotClosed();
				return Interlocked.Read(ref this._length);
			}
		}

		/// <summary>Gets the stream length (size) or the total amount of memory assigned to a stream (capacity).</summary>
		/// <returns>The size or capacity of the stream.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x060066A6 RID: 26278 RVA: 0x0015E61B File Offset: 0x0015C81B
		public long Capacity
		{
			get
			{
				this.EnsureNotClosed();
				return this._capacity;
			}
		}

		/// <summary>Gets or sets the current position in a stream.</summary>
		/// <returns>The current position in the stream.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The position is set to a value that is less than zero, or the position is larger than <see cref="F:System.Int32.MaxValue" /> or results in overflow when added to the current pointer.</exception>
		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x060066A7 RID: 26279 RVA: 0x0015E629 File Offset: 0x0015C829
		// (set) Token: 0x060066A8 RID: 26280 RVA: 0x0015E644 File Offset: 0x0015C844
		public override long Position
		{
			get
			{
				if (!this.CanSeek)
				{
					throw Error.GetStreamIsClosed();
				}
				return Interlocked.Read(ref this._position);
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Non-negative number required.");
				}
				if (!this.CanSeek)
				{
					throw Error.GetStreamIsClosed();
				}
				Interlocked.Exchange(ref this._position, value);
			}
		}

		/// <summary>Gets or sets a byte pointer to a stream based on the current position in the stream.</summary>
		/// <returns>A byte pointer.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The current position is larger than the capacity of the stream.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The position is being set is not a valid position in the current stream.</exception>
		/// <exception cref="T:System.IO.IOException">The pointer is being set to a lower value than the starting position of the stream.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream was initialized for use with a <see cref="T:System.Runtime.InteropServices.SafeBuffer" />. The <see cref="P:System.IO.UnmanagedMemoryStream.PositionPointer" /> property is valid only for streams that are initialized with a <see cref="T:System.Byte" /> pointer.</exception>
		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x060066A9 RID: 26281 RVA: 0x0015E678 File Offset: 0x0015C878
		// (set) Token: 0x060066AA RID: 26282 RVA: 0x0015E6C8 File Offset: 0x0015C8C8
		[CLSCompliant(false)]
		public unsafe byte* PositionPointer
		{
			get
			{
				if (this._buffer != null)
				{
					throw new NotSupportedException("This operation is not supported for an UnmanagedMemoryStream created from a SafeBuffer.");
				}
				this.EnsureNotClosed();
				long num = Interlocked.Read(ref this._position);
				if (num > this._capacity)
				{
					throw new IndexOutOfRangeException("Unmanaged memory stream position was beyond the capacity of the stream.");
				}
				return this._mem + num;
			}
			set
			{
				if (this._buffer != null)
				{
					throw new NotSupportedException("This operation is not supported for an UnmanagedMemoryStream created from a SafeBuffer.");
				}
				this.EnsureNotClosed();
				if (value < this._mem)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				long num = (long)(value - this._mem);
				if (num < 0L)
				{
					throw new ArgumentOutOfRangeException("offset", "UnmanagedMemoryStream length must be non-negative and less than 2^63 - 1 - baseAddress.");
				}
				Interlocked.Exchange(ref this._position, num);
			}
		}

		/// <summary>Reads the specified number of bytes into the specified array.</summary>
		/// <param name="buffer">When this method returns, contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source. This parameter is passed uninitialized.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream.</param>
		/// <param name="count">The maximum number of bytes to read from the current stream.</param>
		/// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying memory does not support reading.  
		/// -or-
		///  The <see cref="P:System.IO.UnmanagedMemoryStream.CanRead" /> property is set to <see langword="false" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is set to <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than zero.  
		/// -or-
		///  The <paramref name="count" /> parameter is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The length of the buffer array minus the <paramref name="offset" /> parameter is less than the <paramref name="count" /> parameter.</exception>
		// Token: 0x060066AB RID: 26283 RVA: 0x0015E730 File Offset: 0x0015C930
		public override int Read(byte[] buffer, int offset, int count)
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
			return this.ReadCore(new Span<byte>(buffer, offset, count));
		}

		// Token: 0x060066AC RID: 26284 RVA: 0x0015E799 File Offset: 0x0015C999
		public override int Read(Span<byte> buffer)
		{
			if (base.GetType() == typeof(UnmanagedMemoryStream))
			{
				return this.ReadCore(buffer);
			}
			return base.Read(buffer);
		}

		// Token: 0x060066AD RID: 26285 RVA: 0x0015E7C4 File Offset: 0x0015C9C4
		internal unsafe int ReadCore(Span<byte> buffer)
		{
			this.EnsureNotClosed();
			this.EnsureReadable();
			long num = Interlocked.Read(ref this._position);
			long num2 = Math.Min(Interlocked.Read(ref this._length) - num, (long)buffer.Length);
			if (num2 <= 0L)
			{
				return 0;
			}
			int num3 = (int)num2;
			if (num3 < 0)
			{
				return 0;
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(buffer))
			{
				byte* dest = reference;
				if (this._buffer != null)
				{
					byte* ptr = null;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						this._buffer.AcquirePointer(ref ptr);
						Buffer.Memcpy(dest, ptr + num + this._offset, num3, true);
						goto IL_A7;
					}
					finally
					{
						if (ptr != null)
						{
							this._buffer.ReleasePointer();
						}
					}
				}
				Buffer.Memcpy(dest, this._mem + num, num3, true);
				IL_A7:;
			}
			Interlocked.Exchange(ref this._position, num + num2);
			return num3;
		}

		/// <summary>Asynchronously reads the specified number of bytes into the specified array.  
		///  Available starting in .NET Framework 4.6</summary>
		/// <param name="buffer">The buffer to write the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.</returns>
		// Token: 0x060066AE RID: 26286 RVA: 0x0015E89C File Offset: 0x0015CA9C
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
			Task<int> task;
			try
			{
				int num = this.Read(buffer, offset, count);
				Task<int> lastReadTask = this._lastReadTask;
				Task<int> task2;
				if (lastReadTask == null || lastReadTask.Result != num)
				{
					task = (this._lastReadTask = Task.FromResult<int>(num));
					task2 = task;
				}
				else
				{
					task2 = lastReadTask;
				}
				task = task2;
			}
			catch (Exception exception)
			{
				task = Task.FromException<int>(exception);
			}
			return task;
		}

		// Token: 0x060066AF RID: 26287 RVA: 0x0015E954 File Offset: 0x0015CB54
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
			}
			ValueTask<int> result;
			try
			{
				ArraySegment<byte> arraySegment;
				result = new ValueTask<int>(MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment) ? this.Read(arraySegment.Array, arraySegment.Offset, arraySegment.Count) : this.Read(buffer.Span));
			}
			catch (Exception exception)
			{
				result = new ValueTask<int>(Task.FromException<int>(exception));
			}
			return result;
		}

		/// <summary>Reads a byte from a stream and advances the position within the stream by one byte, or returns -1 if at the end of the stream.</summary>
		/// <returns>The unsigned byte cast to an <see cref="T:System.Int32" /> object, or -1 if at the end of the stream.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying memory does not support reading.  
		/// -or-
		///  The current position is at the end of the stream.</exception>
		// Token: 0x060066B0 RID: 26288 RVA: 0x0015E9D8 File Offset: 0x0015CBD8
		public unsafe override int ReadByte()
		{
			this.EnsureNotClosed();
			this.EnsureReadable();
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			if (num >= num2)
			{
				return -1;
			}
			Interlocked.Exchange(ref this._position, num + 1L);
			if (this._buffer != null)
			{
				byte* ptr = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this._buffer.AcquirePointer(ref ptr);
					return (int)(ptr + num)[this._offset];
				}
				finally
				{
					if (ptr != null)
					{
						this._buffer.ReleasePointer();
					}
				}
			}
			return (int)this._mem[num];
		}

		/// <summary>Sets the current position of the current stream to the given value.</summary>
		/// <param name="offset">The point relative to origin to begin seeking from.</param>
		/// <param name="loc">Specifies the beginning, the end, or the current position as a reference point for origin, using a value of type <see cref="T:System.IO.SeekOrigin" />.</param>
		/// <returns>The new position in the stream.</returns>
		/// <exception cref="T:System.IO.IOException">An attempt was made to seek before the beginning of the stream.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> value is larger than the maximum size of the stream.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="loc" /> is invalid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x060066B1 RID: 26289 RVA: 0x0015EA7C File Offset: 0x0015CC7C
		public override long Seek(long offset, SeekOrigin loc)
		{
			this.EnsureNotClosed();
			switch (loc)
			{
			case SeekOrigin.Begin:
				if (offset < 0L)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				Interlocked.Exchange(ref this._position, offset);
				break;
			case SeekOrigin.Current:
			{
				long num = Interlocked.Read(ref this._position);
				if (offset + num < 0L)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				Interlocked.Exchange(ref this._position, offset + num);
				break;
			}
			case SeekOrigin.End:
			{
				long num2 = Interlocked.Read(ref this._length);
				if (num2 + offset < 0L)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				Interlocked.Exchange(ref this._position, num2 + offset);
				break;
			}
			default:
				throw new ArgumentException("Invalid seek origin.");
			}
			return Interlocked.Read(ref this._position);
		}

		/// <summary>Sets the length of a stream to a specified value.</summary>
		/// <param name="value">The length of the stream.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying memory does not support writing.  
		/// -or-
		///  An attempt is made to write to the stream and the <see cref="P:System.IO.UnmanagedMemoryStream.CanWrite" /> property is <see langword="false" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="value" /> exceeds the capacity of the stream.  
		/// -or-
		///  The specified <paramref name="value" /> is negative.</exception>
		// Token: 0x060066B2 RID: 26290 RVA: 0x0015EB38 File Offset: 0x0015CD38
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", "Non-negative number required.");
			}
			if (this._buffer != null)
			{
				throw new NotSupportedException("This operation is not supported for an UnmanagedMemoryStream created from a SafeBuffer.");
			}
			this.EnsureNotClosed();
			this.EnsureWriteable();
			if (value > this._capacity)
			{
				throw new IOException("Unable to expand length of this stream beyond its capacity.");
			}
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			if (value > num2)
			{
				Buffer.ZeroMemory(this._mem + num2, value - num2);
			}
			Interlocked.Exchange(ref this._length, value);
			if (num > value)
			{
				Interlocked.Exchange(ref this._position, value);
			}
		}

		/// <summary>Writes a block of bytes to the current stream using data from a buffer.</summary>
		/// <param name="buffer">The byte array from which to copy bytes to the current stream.</param>
		/// <param name="offset">The offset in the buffer at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The number of bytes to write to the current stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying memory does not support writing.  
		/// -or-
		///  An attempt is made to write to the stream and the <see cref="P:System.IO.UnmanagedMemoryStream.CanWrite" /> property is <see langword="false" />.  
		/// -or-
		///  The <paramref name="count" /> value is greater than the capacity of the stream.  
		/// -or-
		///  The position is at the end of the stream capacity.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">One of the specified parameters is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="offset" /> parameter minus the length of the <paramref name="buffer" /> parameter is less than the <paramref name="count" /> parameter.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060066B3 RID: 26291 RVA: 0x0015EBD8 File Offset: 0x0015CDD8
		public override void Write(byte[] buffer, int offset, int count)
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
			this.WriteCore(new Span<byte>(buffer, offset, count));
		}

		// Token: 0x060066B4 RID: 26292 RVA: 0x0015EC46 File Offset: 0x0015CE46
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			if (base.GetType() == typeof(UnmanagedMemoryStream))
			{
				this.WriteCore(buffer);
				return;
			}
			base.Write(buffer);
		}

		// Token: 0x060066B5 RID: 26293 RVA: 0x0015EC70 File Offset: 0x0015CE70
		internal unsafe void WriteCore(ReadOnlySpan<byte> buffer)
		{
			this.EnsureNotClosed();
			this.EnsureWriteable();
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			long num3 = num + (long)buffer.Length;
			if (num3 < 0L)
			{
				throw new IOException("Stream was too long.");
			}
			if (num3 > this._capacity)
			{
				throw new NotSupportedException("Unable to expand length of this stream beyond its capacity.");
			}
			if (this._buffer == null)
			{
				if (num > num2)
				{
					Buffer.ZeroMemory(this._mem + num2, num - num2);
				}
				if (num3 > num2)
				{
					Interlocked.Exchange(ref this._length, num3);
				}
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(buffer))
			{
				byte* src = reference;
				if (this._buffer != null)
				{
					if (this._capacity - num < (long)buffer.Length)
					{
						throw new ArgumentException("Not enough space available in the buffer.");
					}
					byte* ptr = null;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						this._buffer.AcquirePointer(ref ptr);
						Buffer.Memcpy(ptr + num + this._offset, src, buffer.Length, true);
						goto IL_10E;
					}
					finally
					{
						if (ptr != null)
						{
							this._buffer.ReleasePointer();
						}
					}
				}
				Buffer.Memcpy(this._mem + num, src, buffer.Length, true);
				IL_10E:;
			}
			Interlocked.Exchange(ref this._position, num3);
		}

		/// <summary>Asynchronously writes a sequence of bytes to the current stream, advances the current position within this stream by the number of bytes written, and monitors cancellation requests.  
		///  Available starting in .NET Framework 4.6</summary>
		/// <param name="buffer">The buffer to write data from.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> from which to begin copying bytes to the stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		// Token: 0x060066B6 RID: 26294 RVA: 0x0015EDAC File Offset: 0x0015CFAC
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
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task result;
			try
			{
				this.Write(buffer, offset, count);
				result = Task.CompletedTask;
			}
			catch (Exception exception)
			{
				result = Task.FromException(exception);
			}
			return result;
		}

		// Token: 0x060066B7 RID: 26295 RVA: 0x0015EE44 File Offset: 0x0015D044
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask(Task.FromCanceled(cancellationToken));
			}
			ValueTask valueTask;
			try
			{
				ArraySegment<byte> arraySegment;
				if (MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment))
				{
					this.Write(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
				}
				else
				{
					this.Write(buffer.Span);
				}
				valueTask = default(ValueTask);
				valueTask = valueTask;
			}
			catch (Exception exception)
			{
				valueTask = new ValueTask(Task.FromException(exception));
			}
			return valueTask;
		}

		/// <summary>Writes a byte to the current position in the file stream.</summary>
		/// <param name="value">A byte value written to the stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying memory does not support writing.  
		/// -or-
		///  An attempt is made to write to the stream and the <see cref="P:System.IO.UnmanagedMemoryStream.CanWrite" /> property is <see langword="false" />.  
		/// -or-
		///  The current position is at the end of the capacity of the stream.</exception>
		/// <exception cref="T:System.IO.IOException">The supplied <paramref name="value" /> causes the stream exceed its maximum capacity.</exception>
		// Token: 0x060066B8 RID: 26296 RVA: 0x0015EEC8 File Offset: 0x0015D0C8
		public unsafe override void WriteByte(byte value)
		{
			this.EnsureNotClosed();
			this.EnsureWriteable();
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			long num3 = num + 1L;
			if (num >= num2)
			{
				if (num3 < 0L)
				{
					throw new IOException("Stream was too long.");
				}
				if (num3 > this._capacity)
				{
					throw new NotSupportedException("Unable to expand length of this stream beyond its capacity.");
				}
				if (this._buffer == null)
				{
					if (num > num2)
					{
						Buffer.ZeroMemory(this._mem + num2, num - num2);
					}
					Interlocked.Exchange(ref this._length, num3);
				}
			}
			if (this._buffer != null)
			{
				byte* ptr = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this._buffer.AcquirePointer(ref ptr);
					(ptr + num)[this._offset] = value;
					goto IL_C4;
				}
				finally
				{
					if (ptr != null)
					{
						this._buffer.ReleasePointer();
					}
				}
			}
			this._mem[num] = value;
			IL_C4:
			Interlocked.Exchange(ref this._position, num3);
		}

		// Token: 0x04003C14 RID: 15380
		private SafeBuffer _buffer;

		// Token: 0x04003C15 RID: 15381
		private unsafe byte* _mem;

		// Token: 0x04003C16 RID: 15382
		private long _length;

		// Token: 0x04003C17 RID: 15383
		private long _capacity;

		// Token: 0x04003C18 RID: 15384
		private long _position;

		// Token: 0x04003C19 RID: 15385
		private long _offset;

		// Token: 0x04003C1A RID: 15386
		private FileAccess _access;

		// Token: 0x04003C1B RID: 15387
		internal bool _isOpen;

		// Token: 0x04003C1C RID: 15388
		private Task<int> _lastReadTask;
	}
}
