using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Creates a stream whose backing store is memory.</summary>
	// Token: 0x02000B0C RID: 2828
	[Serializable]
	public class MemoryStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.MemoryStream" /> class with an expandable capacity initialized to zero.</summary>
		// Token: 0x060064E3 RID: 25827 RVA: 0x00156C07 File Offset: 0x00154E07
		public MemoryStream() : this(0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.MemoryStream" /> class with an expandable capacity initialized as specified.</summary>
		/// <param name="capacity">The initial size of the internal array in bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is negative.</exception>
		// Token: 0x060064E4 RID: 25828 RVA: 0x00156C10 File Offset: 0x00154E10
		public MemoryStream(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "Capacity must be positive.");
			}
			this._buffer = ((capacity != 0) ? new byte[capacity] : Array.Empty<byte>());
			this._capacity = capacity;
			this._expandable = true;
			this._writable = true;
			this._exposable = true;
			this._origin = 0;
			this._isOpen = true;
		}

		/// <summary>Initializes a new non-resizable instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified byte array.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create the current stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		// Token: 0x060064E5 RID: 25829 RVA: 0x00156C77 File Offset: 0x00154E77
		public MemoryStream(byte[] buffer) : this(buffer, true)
		{
		}

		/// <summary>Initializes a new non-resizable instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified byte array with the <see cref="P:System.IO.MemoryStream.CanWrite" /> property set as specified.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create this stream.</param>
		/// <param name="writable">The setting of the <see cref="P:System.IO.MemoryStream.CanWrite" /> property, which determines whether the stream supports writing.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		// Token: 0x060064E6 RID: 25830 RVA: 0x00156C84 File Offset: 0x00154E84
		public MemoryStream(byte[] buffer, bool writable)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			this._buffer = buffer;
			this._length = (this._capacity = buffer.Length);
			this._writable = writable;
			this._exposable = false;
			this._origin = 0;
			this._isOpen = true;
		}

		/// <summary>Initializes a new non-resizable instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified region (index) of a byte array.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create this stream.</param>
		/// <param name="index">The index into <paramref name="buffer" /> at which the stream begins.</param>
		/// <param name="count">The length of the stream in bytes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		// Token: 0x060064E7 RID: 25831 RVA: 0x00156CDF File Offset: 0x00154EDF
		public MemoryStream(byte[] buffer, int index, int count) : this(buffer, index, count, true, false)
		{
		}

		/// <summary>Initializes a new non-resizable instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified region of a byte array, with the <see cref="P:System.IO.MemoryStream.CanWrite" /> property set as specified.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create this stream.</param>
		/// <param name="index">The index in <paramref name="buffer" /> at which the stream begins.</param>
		/// <param name="count">The length of the stream in bytes.</param>
		/// <param name="writable">The setting of the <see cref="P:System.IO.MemoryStream.CanWrite" /> property, which determines whether the stream supports writing.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> are negative.</exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		// Token: 0x060064E8 RID: 25832 RVA: 0x00156CEC File Offset: 0x00154EEC
		public MemoryStream(byte[] buffer, int index, int count, bool writable) : this(buffer, index, count, writable, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified region of a byte array, with the <see cref="P:System.IO.MemoryStream.CanWrite" /> property set as specified, and the ability to call <see cref="M:System.IO.MemoryStream.GetBuffer" /> set as specified.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create this stream.</param>
		/// <param name="index">The index into <paramref name="buffer" /> at which the stream begins.</param>
		/// <param name="count">The length of the stream in bytes.</param>
		/// <param name="writable">The setting of the <see cref="P:System.IO.MemoryStream.CanWrite" /> property, which determines whether the stream supports writing.</param>
		/// <param name="publiclyVisible">
		///   <see langword="true" /> to enable <see cref="M:System.IO.MemoryStream.GetBuffer" />, which returns the unsigned byte array from which the stream was created; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		// Token: 0x060064E9 RID: 25833 RVA: 0x00156CFC File Offset: 0x00154EFC
		public MemoryStream(byte[] buffer, int index, int count, bool writable, bool publiclyVisible)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			this._buffer = buffer;
			this._position = index;
			this._origin = index;
			this._length = (this._capacity = index + count);
			this._writable = writable;
			this._exposable = publiclyVisible;
			this._expandable = false;
			this._isOpen = true;
		}

		/// <summary>Gets a value indicating whether the current stream supports reading.</summary>
		/// <returns>
		///   <see langword="true" /> if the stream is open.</returns>
		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x060064EA RID: 25834 RVA: 0x00156DA4 File Offset: 0x00154FA4
		public override bool CanRead
		{
			get
			{
				return this._isOpen;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports seeking.</summary>
		/// <returns>
		///   <see langword="true" /> if the stream is open.</returns>
		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x060064EB RID: 25835 RVA: 0x00156DA4 File Offset: 0x00154FA4
		public override bool CanSeek
		{
			get
			{
				return this._isOpen;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports writing.</summary>
		/// <returns>
		///   <see langword="true" /> if the stream supports writing; otherwise, <see langword="false" />.</returns>
		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x060064EC RID: 25836 RVA: 0x00156DAC File Offset: 0x00154FAC
		public override bool CanWrite
		{
			get
			{
				return this._writable;
			}
		}

		// Token: 0x060064ED RID: 25837 RVA: 0x00156DB4 File Offset: 0x00154FB4
		private void EnsureNotClosed()
		{
			if (!this._isOpen)
			{
				throw Error.GetStreamIsClosed();
			}
		}

		// Token: 0x060064EE RID: 25838 RVA: 0x00156DC4 File Offset: 0x00154FC4
		private void EnsureWriteable()
		{
			if (!this.CanWrite)
			{
				throw Error.GetWriteNotSupported();
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.MemoryStream" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060064EF RID: 25839 RVA: 0x00156DD4 File Offset: 0x00154FD4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this._isOpen = false;
					this._writable = false;
					this._expandable = false;
					this._lastReadTask = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060064F0 RID: 25840 RVA: 0x00156E1C File Offset: 0x0015501C
		private bool EnsureCapacity(int value)
		{
			if (value < 0)
			{
				throw new IOException("Stream was too long.");
			}
			if (value > this._capacity)
			{
				int num = value;
				if (num < 256)
				{
					num = 256;
				}
				if (num < this._capacity * 2)
				{
					num = this._capacity * 2;
				}
				if (this._capacity * 2 > 2147483591)
				{
					num = ((value > 2147483591) ? value : 2147483591);
				}
				this.Capacity = num;
				return true;
			}
			return false;
		}

		/// <summary>Overrides the <see cref="M:System.IO.Stream.Flush" /> method so that no action is performed.</summary>
		// Token: 0x060064F1 RID: 25841 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public override void Flush()
		{
		}

		/// <summary>Asynchronously clears all buffers for this stream, and monitors cancellation requests.</summary>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x060064F2 RID: 25842 RVA: 0x00156E90 File Offset: 0x00155090
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

		/// <summary>Returns the array of unsigned bytes from which this stream was created.</summary>
		/// <returns>The byte array from which this stream was created, or the underlying array if a byte array was not provided to the <see cref="T:System.IO.MemoryStream" /> constructor during construction of the current instance.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The <see langword="MemoryStream" /> instance was not created with a publicly visible buffer.</exception>
		// Token: 0x060064F3 RID: 25843 RVA: 0x00156ED8 File Offset: 0x001550D8
		public virtual byte[] GetBuffer()
		{
			if (!this._exposable)
			{
				throw new UnauthorizedAccessException("MemoryStream's internal buffer cannot be accessed.");
			}
			return this._buffer;
		}

		/// <summary>Returns the array of unsigned bytes from which this stream was created. The return value indicates whether the conversion succeeded.</summary>
		/// <param name="buffer">The byte array segment from which this stream was created.</param>
		/// <returns>
		///   <see langword="true" /> if the conversion was successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x060064F4 RID: 25844 RVA: 0x00156EF3 File Offset: 0x001550F3
		public virtual bool TryGetBuffer(out ArraySegment<byte> buffer)
		{
			if (!this._exposable)
			{
				buffer = default(ArraySegment<byte>);
				return false;
			}
			buffer = new ArraySegment<byte>(this._buffer, this._origin, this._length - this._origin);
			return true;
		}

		// Token: 0x060064F5 RID: 25845 RVA: 0x00156F2B File Offset: 0x0015512B
		internal byte[] InternalGetBuffer()
		{
			return this._buffer;
		}

		// Token: 0x060064F6 RID: 25846 RVA: 0x00156F33 File Offset: 0x00155133
		internal void InternalGetOriginAndLength(out int origin, out int length)
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			origin = this._origin;
			length = this._length;
		}

		// Token: 0x060064F7 RID: 25847 RVA: 0x00156F52 File Offset: 0x00155152
		internal int InternalGetPosition()
		{
			return this._position;
		}

		// Token: 0x060064F8 RID: 25848 RVA: 0x00156F5C File Offset: 0x0015515C
		internal int InternalReadInt32()
		{
			this.EnsureNotClosed();
			int num = this._position += 4;
			if (num > this._length)
			{
				this._position = this._length;
				throw Error.GetEndOfFile();
			}
			return (int)this._buffer[num - 4] | (int)this._buffer[num - 3] << 8 | (int)this._buffer[num - 2] << 16 | (int)this._buffer[num - 1] << 24;
		}

		// Token: 0x060064F9 RID: 25849 RVA: 0x00156FD0 File Offset: 0x001551D0
		internal int InternalEmulateRead(int count)
		{
			this.EnsureNotClosed();
			int num = this._length - this._position;
			if (num > count)
			{
				num = count;
			}
			if (num < 0)
			{
				num = 0;
			}
			this._position += num;
			return num;
		}

		/// <summary>Gets or sets the number of bytes allocated for this stream.</summary>
		/// <returns>The length of the usable portion of the buffer for the stream.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A capacity is set that is negative or less than the current length of the stream.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see langword="set" /> is invoked on a stream whose capacity cannot be modified.</exception>
		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x060064FA RID: 25850 RVA: 0x0015700C File Offset: 0x0015520C
		// (set) Token: 0x060064FB RID: 25851 RVA: 0x00157024 File Offset: 0x00155224
		public virtual int Capacity
		{
			get
			{
				this.EnsureNotClosed();
				return this._capacity - this._origin;
			}
			set
			{
				if ((long)value < this.Length)
				{
					throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
				}
				this.EnsureNotClosed();
				if (!this._expandable && value != this.Capacity)
				{
					throw new NotSupportedException("Memory stream is not expandable.");
				}
				if (this._expandable && value != this._capacity)
				{
					if (value > 0)
					{
						byte[] array = new byte[value];
						if (this._length > 0)
						{
							Buffer.BlockCopy(this._buffer, 0, array, 0, this._length);
						}
						this._buffer = array;
					}
					else
					{
						this._buffer = null;
					}
					this._capacity = value;
				}
			}
		}

		/// <summary>Gets the length of the stream in bytes.</summary>
		/// <returns>The length of the stream in bytes.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x060064FC RID: 25852 RVA: 0x001570BD File Offset: 0x001552BD
		public override long Length
		{
			get
			{
				this.EnsureNotClosed();
				return (long)(this._length - this._origin);
			}
		}

		/// <summary>Gets or sets the current position within the stream.</summary>
		/// <returns>The current position within the stream.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The position is set to a negative value or a value greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x060064FD RID: 25853 RVA: 0x001570D3 File Offset: 0x001552D3
		// (set) Token: 0x060064FE RID: 25854 RVA: 0x001570EC File Offset: 0x001552EC
		public override long Position
		{
			get
			{
				this.EnsureNotClosed();
				return (long)(this._position - this._origin);
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Non-negative number required.");
				}
				this.EnsureNotClosed();
				if (value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value", "Stream length must be non-negative and less than 2^31 - 1 - origin.");
				}
				this._position = this._origin + (int)value;
			}
		}

		/// <summary>Reads a block of bytes from the current stream and writes the data to a buffer.</summary>
		/// <param name="buffer">When this method returns, contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the characters read from the current stream.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing data from the current stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <returns>The total number of bytes written into the buffer. This can be less than the number of bytes requested if that number of bytes are not currently available, or zero if the end of the stream is reached before any bytes are read.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> subtracted from the buffer length is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream instance is closed.</exception>
		// Token: 0x060064FF RID: 25855 RVA: 0x0015713C File Offset: 0x0015533C
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
			this.EnsureNotClosed();
			int num = this._length - this._position;
			if (num > count)
			{
				num = count;
			}
			if (num <= 0)
			{
				return 0;
			}
			if (num <= 8)
			{
				int num2 = num;
				while (--num2 >= 0)
				{
					buffer[offset + num2] = this._buffer[this._position + num2];
				}
			}
			else
			{
				Buffer.BlockCopy(this._buffer, this._position, buffer, offset, num);
			}
			this._position += num;
			return num;
		}

		// Token: 0x06006500 RID: 25856 RVA: 0x00157200 File Offset: 0x00155400
		public override int Read(Span<byte> buffer)
		{
			if (base.GetType() != typeof(MemoryStream))
			{
				return base.Read(buffer);
			}
			this.EnsureNotClosed();
			int num = Math.Min(this._length - this._position, buffer.Length);
			if (num <= 0)
			{
				return 0;
			}
			new Span<byte>(this._buffer, this._position, num).CopyTo(buffer);
			this._position += num;
			return num;
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
		// Token: 0x06006501 RID: 25857 RVA: 0x0015727C File Offset: 0x0015547C
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
			catch (OperationCanceledException exception)
			{
				task = Task.FromCancellation<int>(exception);
			}
			catch (Exception exception2)
			{
				task = Task.FromException<int>(exception2);
			}
			return task;
		}

		// Token: 0x06006502 RID: 25858 RVA: 0x00157348 File Offset: 0x00155548
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
			catch (OperationCanceledException exception)
			{
				result = new ValueTask<int>(Task.FromCancellation<int>(exception));
			}
			catch (Exception exception2)
			{
				result = new ValueTask<int>(Task.FromException<int>(exception2));
			}
			return result;
		}

		/// <summary>Reads a byte from the current stream.</summary>
		/// <returns>The byte cast to a <see cref="T:System.Int32" />, or -1 if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current stream instance is closed.</exception>
		// Token: 0x06006503 RID: 25859 RVA: 0x001573E4 File Offset: 0x001555E4
		public override int ReadByte()
		{
			this.EnsureNotClosed();
			if (this._position >= this._length)
			{
				return -1;
			}
			byte[] buffer = this._buffer;
			int position = this._position;
			this._position = position + 1;
			return buffer[position];
		}

		// Token: 0x06006504 RID: 25860 RVA: 0x00157420 File Offset: 0x00155620
		public override void CopyTo(Stream destination, int bufferSize)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			if (base.GetType() != typeof(MemoryStream))
			{
				base.CopyTo(destination, bufferSize);
				return;
			}
			int position = this._position;
			int num = this.InternalEmulateRead(this._length - position);
			if (num > 0)
			{
				destination.Write(this._buffer, position, num);
			}
		}

		/// <summary>Asynchronously reads all the bytes from the current stream and writes them to another stream, using a specified buffer size and cancellation token.</summary>
		/// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
		/// <param name="bufferSize">The size, in bytes, of the buffer. This value must be greater than zero.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous copy operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="buffersize" /> is negative or zero.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Either the current stream or the destination stream is disposed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading, or the destination stream does not support writing.</exception>
		// Token: 0x06006505 RID: 25861 RVA: 0x00157480 File Offset: 0x00155680
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			if (base.GetType() != typeof(MemoryStream))
			{
				return base.CopyToAsync(destination, bufferSize, cancellationToken);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			int position = this._position;
			int num = this.InternalEmulateRead(this._length - this._position);
			if (num == 0)
			{
				return Task.CompletedTask;
			}
			MemoryStream memoryStream = destination as MemoryStream;
			if (memoryStream == null)
			{
				return destination.WriteAsync(this._buffer, position, num, cancellationToken);
			}
			Task result;
			try
			{
				memoryStream.Write(this._buffer, position, num);
				result = Task.CompletedTask;
			}
			catch (Exception exception)
			{
				result = Task.FromException(exception);
			}
			return result;
		}

		/// <summary>Sets the position within the current stream to the specified value.</summary>
		/// <param name="offset">The new position within the stream. This is relative to the <paramref name="loc" /> parameter, and can be positive or negative.</param>
		/// <param name="loc">A value of type <see cref="T:System.IO.SeekOrigin" />, which acts as the seek reference point.</param>
		/// <returns>The new position within the stream, calculated by combining the initial reference point and the offset.</returns>
		/// <exception cref="T:System.IO.IOException">Seeking is attempted before the beginning of the stream.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentException">There is an invalid <see cref="T:System.IO.SeekOrigin" />.  
		///  -or-  
		///  <paramref name="offset" /> caused an arithmetic overflow.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream instance is closed.</exception>
		// Token: 0x06006506 RID: 25862 RVA: 0x00157534 File Offset: 0x00155734
		public override long Seek(long offset, SeekOrigin loc)
		{
			this.EnsureNotClosed();
			if (offset > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("offset", "Stream length must be non-negative and less than 2^31 - 1 - origin.");
			}
			switch (loc)
			{
			case SeekOrigin.Begin:
			{
				int num = this._origin + (int)offset;
				if (offset < 0L || num < this._origin)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				this._position = num;
				break;
			}
			case SeekOrigin.Current:
			{
				int num2 = this._position + (int)offset;
				if ((long)this._position + offset < (long)this._origin || num2 < this._origin)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				this._position = num2;
				break;
			}
			case SeekOrigin.End:
			{
				int num3 = this._length + (int)offset;
				if ((long)this._length + offset < (long)this._origin || num3 < this._origin)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				this._position = num3;
				break;
			}
			default:
				throw new ArgumentException("Invalid seek origin.");
			}
			return (long)this._position;
		}

		/// <summary>Sets the length of the current stream to the specified value.</summary>
		/// <param name="value">The value at which to set the length.</param>
		/// <exception cref="T:System.NotSupportedException">The current stream is not resizable and <paramref name="value" /> is larger than the current capacity.  
		///  -or-  
		///  The current stream does not support writing.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is negative or is greater than the maximum length of the <see cref="T:System.IO.MemoryStream" />, where the maximum length is(<see cref="F:System.Int32.MaxValue" /> - origin), and origin is the index into the underlying buffer at which the stream starts.</exception>
		// Token: 0x06006507 RID: 25863 RVA: 0x00157628 File Offset: 0x00155828
		public override void SetLength(long value)
		{
			if (value < 0L || value > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("value", "Stream length must be non-negative and less than 2^31 - 1 - origin.");
			}
			this.EnsureWriteable();
			if (value > (long)(2147483647 - this._origin))
			{
				throw new ArgumentOutOfRangeException("value", "Stream length must be non-negative and less than 2^31 - 1 - origin.");
			}
			int num = this._origin + (int)value;
			if (!this.EnsureCapacity(num) && num > this._length)
			{
				Array.Clear(this._buffer, this._length, num - this._length);
			}
			this._length = num;
			if (this._position > num)
			{
				this._position = num;
			}
		}

		/// <summary>Writes the stream contents to a byte array, regardless of the <see cref="P:System.IO.MemoryStream.Position" /> property.</summary>
		/// <returns>A new byte array.</returns>
		// Token: 0x06006508 RID: 25864 RVA: 0x001576C8 File Offset: 0x001558C8
		public virtual byte[] ToArray()
		{
			int num = this._length - this._origin;
			if (num == 0)
			{
				return Array.Empty<byte>();
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(this._buffer, this._origin, array, 0, num);
			return array;
		}

		/// <summary>Writes a block of bytes to the current stream using data read from a buffer.</summary>
		/// <param name="buffer">The buffer to write data from.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing. For additional information see <see cref="P:System.IO.Stream.CanWrite" />.  
		///  -or-  
		///  The current position is closer than <paramref name="count" /> bytes to the end of the stream, and the capacity cannot be modified.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> subtracted from the buffer length is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> are negative.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream instance is closed.</exception>
		// Token: 0x06006509 RID: 25865 RVA: 0x00157708 File Offset: 0x00155908
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
			this.EnsureNotClosed();
			this.EnsureWriteable();
			int num = this._position + count;
			if (num < 0)
			{
				throw new IOException("Stream was too long.");
			}
			if (num > this._length)
			{
				bool flag = this._position > this._length;
				if (num > this._capacity && this.EnsureCapacity(num))
				{
					flag = false;
				}
				if (flag)
				{
					Array.Clear(this._buffer, this._length, num - this._length);
				}
				this._length = num;
			}
			if (count <= 8 && buffer != this._buffer)
			{
				int num2 = count;
				while (--num2 >= 0)
				{
					this._buffer[this._position + num2] = buffer[offset + num2];
				}
			}
			else
			{
				Buffer.BlockCopy(buffer, offset, this._buffer, this._position, count);
			}
			this._position = num;
		}

		// Token: 0x0600650A RID: 25866 RVA: 0x00157820 File Offset: 0x00155A20
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			if (base.GetType() != typeof(MemoryStream))
			{
				base.Write(buffer);
				return;
			}
			this.EnsureNotClosed();
			this.EnsureWriteable();
			int num = this._position + buffer.Length;
			if (num < 0)
			{
				throw new IOException("Stream was too long.");
			}
			if (num > this._length)
			{
				bool flag = this._position > this._length;
				if (num > this._capacity && this.EnsureCapacity(num))
				{
					flag = false;
				}
				if (flag)
				{
					Array.Clear(this._buffer, this._length, num - this._length);
				}
				this._length = num;
			}
			buffer.CopyTo(new Span<byte>(this._buffer, this._position, buffer.Length));
			this._position = num;
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
		// Token: 0x0600650B RID: 25867 RVA: 0x001578EC File Offset: 0x00155AEC
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
			catch (OperationCanceledException exception)
			{
				result = Task.FromCancellation<VoidTaskResult>(exception);
			}
			catch (Exception exception2)
			{
				result = Task.FromException(exception2);
			}
			return result;
		}

		// Token: 0x0600650C RID: 25868 RVA: 0x00157998 File Offset: 0x00155B98
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
			catch (OperationCanceledException exception)
			{
				valueTask = new ValueTask(Task.FromCancellation<VoidTaskResult>(exception));
			}
			catch (Exception exception2)
			{
				valueTask = new ValueTask(Task.FromException(exception2));
			}
			return valueTask;
		}

		/// <summary>Writes a byte to the current stream at the current position.</summary>
		/// <param name="value">The byte to write.</param>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing. For additional information see <see cref="P:System.IO.Stream.CanWrite" />.  
		///  -or-  
		///  The current position is at the end of the stream, and the capacity cannot be modified.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream is closed.</exception>
		// Token: 0x0600650D RID: 25869 RVA: 0x00157A34 File Offset: 0x00155C34
		public override void WriteByte(byte value)
		{
			this.EnsureNotClosed();
			this.EnsureWriteable();
			if (this._position >= this._length)
			{
				int num = this._position + 1;
				bool flag = this._position > this._length;
				if (num >= this._capacity && this.EnsureCapacity(num))
				{
					flag = false;
				}
				if (flag)
				{
					Array.Clear(this._buffer, this._length, this._position - this._length);
				}
				this._length = num;
			}
			byte[] buffer = this._buffer;
			int position = this._position;
			this._position = position + 1;
			buffer[position] = value;
		}

		/// <summary>Writes the entire contents of this memory stream to another stream.</summary>
		/// <param name="stream">The stream to write this memory stream to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current or target stream is closed.</exception>
		// Token: 0x0600650E RID: 25870 RVA: 0x00157AC8 File Offset: 0x00155CC8
		public virtual void WriteTo(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream", "Stream cannot be null.");
			}
			this.EnsureNotClosed();
			stream.Write(this._buffer, this._origin, this._length - this._origin);
		}

		// Token: 0x04003B4B RID: 15179
		private byte[] _buffer;

		// Token: 0x04003B4C RID: 15180
		private int _origin;

		// Token: 0x04003B4D RID: 15181
		private int _position;

		// Token: 0x04003B4E RID: 15182
		private int _length;

		// Token: 0x04003B4F RID: 15183
		private int _capacity;

		// Token: 0x04003B50 RID: 15184
		private bool _expandable;

		// Token: 0x04003B51 RID: 15185
		private bool _writable;

		// Token: 0x04003B52 RID: 15186
		private bool _exposable;

		// Token: 0x04003B53 RID: 15187
		private bool _isOpen;

		// Token: 0x04003B54 RID: 15188
		[NonSerialized]
		private Task<int> _lastReadTask;

		// Token: 0x04003B55 RID: 15189
		private const int MemStreamMaxLength = 2147483647;
	}
}
