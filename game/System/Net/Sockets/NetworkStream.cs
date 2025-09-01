using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	/// <summary>Provides the underlying stream of data for network access.</summary>
	// Token: 0x02000792 RID: 1938
	public class NetworkStream : Stream
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.Sockets.NetworkStream" /> class for the specified <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="socket">The <see cref="T:System.Net.Sockets.Socket" /> that the <see cref="T:System.Net.Sockets.NetworkStream" /> will use to send and receive data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="socket" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">The <paramref name="socket" /> parameter is not connected.  
		///  -or-  
		///  The <see cref="P:System.Net.Sockets.Socket.SocketType" /> property of the <paramref name="socket" /> parameter is not <see cref="F:System.Net.Sockets.SocketType.Stream" />.  
		///  -or-  
		///  The <paramref name="socket" /> parameter is in a nonblocking state.</exception>
		// Token: 0x06003CF1 RID: 15601 RVA: 0x000CF654 File Offset: 0x000CD854
		public NetworkStream(Socket socket) : this(socket, FileAccess.ReadWrite, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.NetworkStream" /> class for the specified <see cref="T:System.Net.Sockets.Socket" /> with the specified <see cref="T:System.Net.Sockets.Socket" /> ownership.</summary>
		/// <param name="socket">The <see cref="T:System.Net.Sockets.Socket" /> that the <see cref="T:System.Net.Sockets.NetworkStream" /> will use to send and receive data.</param>
		/// <param name="ownsSocket">Set to <see langword="true" /> to indicate that the <see cref="T:System.Net.Sockets.NetworkStream" /> will take ownership of the <see cref="T:System.Net.Sockets.Socket" />; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="socket" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">The <paramref name="socket" /> parameter is not connected.  
		///  -or-  
		///  the value of the <see cref="P:System.Net.Sockets.Socket.SocketType" /> property of the <paramref name="socket" /> parameter is not <see cref="F:System.Net.Sockets.SocketType.Stream" />.  
		///  -or-  
		///  the <paramref name="socket" /> parameter is in a nonblocking state.</exception>
		// Token: 0x06003CF2 RID: 15602 RVA: 0x000CF65F File Offset: 0x000CD85F
		public NetworkStream(Socket socket, bool ownsSocket) : this(socket, FileAccess.ReadWrite, ownsSocket)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Sockets.NetworkStream" /> class for the specified <see cref="T:System.Net.Sockets.Socket" /> with the specified access rights.</summary>
		/// <param name="socket">The <see cref="T:System.Net.Sockets.Socket" /> that the <see cref="T:System.Net.Sockets.NetworkStream" /> will use to send and receive data.</param>
		/// <param name="access">A bitwise combination of the <see cref="T:System.IO.FileAccess" /> values that specify the type of access given to the <see cref="T:System.Net.Sockets.NetworkStream" /> over the provided <see cref="T:System.Net.Sockets.Socket" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="socket" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">The <paramref name="socket" /> parameter is not connected.  
		///  -or-  
		///  the <see cref="P:System.Net.Sockets.Socket.SocketType" /> property of the <paramref name="socket" /> parameter is not <see cref="F:System.Net.Sockets.SocketType.Stream" />.  
		///  -or-  
		///  the <paramref name="socket" /> parameter is in a nonblocking state.</exception>
		// Token: 0x06003CF3 RID: 15603 RVA: 0x000CF66A File Offset: 0x000CD86A
		public NetworkStream(Socket socket, FileAccess access) : this(socket, access, false)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Sockets.NetworkStream" /> class for the specified <see cref="T:System.Net.Sockets.Socket" /> with the specified access rights and the specified <see cref="T:System.Net.Sockets.Socket" /> ownership.</summary>
		/// <param name="socket">The <see cref="T:System.Net.Sockets.Socket" /> that the <see cref="T:System.Net.Sockets.NetworkStream" /> will use to send and receive data.</param>
		/// <param name="access">A bitwise combination of the <see cref="T:System.IO.FileAccess" /> values that specifies the type of access given to the <see cref="T:System.Net.Sockets.NetworkStream" /> over the provided <see cref="T:System.Net.Sockets.Socket" />.</param>
		/// <param name="ownsSocket">Set to <see langword="true" /> to indicate that the <see cref="T:System.Net.Sockets.NetworkStream" /> will take ownership of the <see cref="T:System.Net.Sockets.Socket" />; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="socket" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">The <paramref name="socket" /> parameter is not connected.  
		///  -or-  
		///  The <see cref="P:System.Net.Sockets.Socket.SocketType" /> property of the <paramref name="socket" /> parameter is not <see cref="F:System.Net.Sockets.SocketType.Stream" />.  
		///  -or-  
		///  The <paramref name="socket" /> parameter is in a nonblocking state.</exception>
		// Token: 0x06003CF4 RID: 15604 RVA: 0x000CF678 File Offset: 0x000CD878
		public NetworkStream(Socket socket, FileAccess access, bool ownsSocket)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}
			if (!socket.Blocking)
			{
				throw new IOException("The operation is not allowed on a non-blocking Socket.");
			}
			if (!socket.Connected)
			{
				throw new IOException("The operation is not allowed on non-connected sockets.");
			}
			if (socket.SocketType != SocketType.Stream)
			{
				throw new IOException("The operation is not allowed on non-stream oriented sockets.");
			}
			this._streamSocket = socket;
			this._ownsSocket = ownsSocket;
			switch (access)
			{
			case FileAccess.Read:
				this._readable = true;
				return;
			case FileAccess.Write:
				this._writeable = true;
				return;
			}
			this._readable = true;
			this._writeable = true;
		}

		/// <summary>Gets the underlying <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> that represents the underlying network connection.</returns>
		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06003CF5 RID: 15605 RVA: 0x000CF72A File Offset: 0x000CD92A
		protected Socket Socket
		{
			get
			{
				return this._streamSocket;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Net.Sockets.NetworkStream" /> can be read.</summary>
		/// <returns>
		///   <see langword="true" /> to indicate that the <see cref="T:System.Net.Sockets.NetworkStream" /> can be read; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06003CF6 RID: 15606 RVA: 0x000CF732 File Offset: 0x000CD932
		// (set) Token: 0x06003CF7 RID: 15607 RVA: 0x000CF73A File Offset: 0x000CD93A
		protected bool Readable
		{
			get
			{
				return this._readable;
			}
			set
			{
				this._readable = value;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Net.Sockets.NetworkStream" /> is writable.</summary>
		/// <returns>
		///   <see langword="true" /> if data can be written to the stream; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06003CF8 RID: 15608 RVA: 0x000CF743 File Offset: 0x000CD943
		// (set) Token: 0x06003CF9 RID: 15609 RVA: 0x000CF74B File Offset: 0x000CD94B
		protected bool Writeable
		{
			get
			{
				return this._writeable;
			}
			set
			{
				this._writeable = value;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Net.Sockets.NetworkStream" /> supports reading.</summary>
		/// <returns>
		///   <see langword="true" /> if data can be read from the stream; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06003CFA RID: 15610 RVA: 0x000CF732 File Offset: 0x000CD932
		public override bool CanRead
		{
			get
			{
				return this._readable;
			}
		}

		/// <summary>Gets a value that indicates whether the stream supports seeking. This property is not currently supported.This property always returns <see langword="false" />.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases to indicate that <see cref="T:System.Net.Sockets.NetworkStream" /> cannot seek a specific location in the stream.</returns>
		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06003CFB RID: 15611 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Net.Sockets.NetworkStream" /> supports writing.</summary>
		/// <returns>
		///   <see langword="true" /> if data can be written to the <see cref="T:System.Net.Sockets.NetworkStream" />; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06003CFC RID: 15612 RVA: 0x000CF743 File Offset: 0x000CD943
		public override bool CanWrite
		{
			get
			{
				return this._writeable;
			}
		}

		/// <summary>Indicates whether timeout properties are usable for <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06003CFD RID: 15613 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets the amount of time that a read operation blocks waiting for data.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the amount of time, in milliseconds, that will elapse before a read operation fails. The default value, <see cref="F:System.Threading.Timeout.Infinite" />, specifies that the read operation does not time out.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than or equal to zero and is not <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06003CFE RID: 15614 RVA: 0x000CF754 File Offset: 0x000CD954
		// (set) Token: 0x06003CFF RID: 15615 RVA: 0x000CF782 File Offset: 0x000CD982
		public override int ReadTimeout
		{
			get
			{
				int num = (int)this._streamSocket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
				if (num == 0)
				{
					return -1;
				}
				return num;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", "Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value > 0.");
				}
				this.SetSocketTimeoutOption(SocketShutdown.Receive, value, false);
			}
		}

		/// <summary>Gets or sets the amount of time that a write operation blocks waiting for data.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the amount of time, in milliseconds, that will elapse before a write operation fails. The default value, <see cref="F:System.Threading.Timeout.Infinite" />, specifies that the write operation does not time out.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than or equal to zero and is not <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06003D00 RID: 15616 RVA: 0x000CF7A8 File Offset: 0x000CD9A8
		// (set) Token: 0x06003D01 RID: 15617 RVA: 0x000CF7D6 File Offset: 0x000CD9D6
		public override int WriteTimeout
		{
			get
			{
				int num = (int)this._streamSocket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout);
				if (num == 0)
				{
					return -1;
				}
				return num;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", "Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value > 0.");
				}
				this.SetSocketTimeoutOption(SocketShutdown.Send, value, false);
			}
		}

		/// <summary>Gets a value that indicates whether data is available on the <see cref="T:System.Net.Sockets.NetworkStream" /> to be read.</summary>
		/// <returns>
		///   <see langword="true" /> if data is available on the stream to be read; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="P:System.Net.Sockets.SocketException.ErrorCode" /> property to obtain the specific error code and refer to the Windows Sockets version 2 API error code documentation for a detailed description of the error.</exception>
		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06003D02 RID: 15618 RVA: 0x000CF7F9 File Offset: 0x000CD9F9
		public virtual bool DataAvailable
		{
			get
			{
				if (this._cleanedUp)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return this._streamSocket.Available != 0;
			}
		}

		/// <summary>Gets the length of the data available on the stream. This property is not currently supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The length of the data available on the stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Any use of this property.</exception>
		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06003D03 RID: 15619 RVA: 0x000C3C76 File Offset: 0x000C1E76
		public override long Length
		{
			get
			{
				throw new NotSupportedException("This stream does not support seek operations.");
			}
		}

		/// <summary>Gets or sets the current position in the stream. This property is not currently supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The current position in the stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Any use of this property.</exception>
		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06003D04 RID: 15620 RVA: 0x000C3C76 File Offset: 0x000C1E76
		// (set) Token: 0x06003D05 RID: 15621 RVA: 0x000C3C76 File Offset: 0x000C1E76
		public override long Position
		{
			get
			{
				throw new NotSupportedException("This stream does not support seek operations.");
			}
			set
			{
				throw new NotSupportedException("This stream does not support seek operations.");
			}
		}

		/// <summary>Sets the current position of the stream to the given value. This method is not currently supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="offset">This parameter is not used.</param>
		/// <param name="origin">This parameter is not used.</param>
		/// <returns>The position in the stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Any use of this property.</exception>
		// Token: 0x06003D06 RID: 15622 RVA: 0x000C3C76 File Offset: 0x000C1E76
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("This stream does not support seek operations.");
		}

		/// <summary>Reads data from the <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the location in memory to store data read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</param>
		/// <param name="offset">The location in <paramref name="buffer" /> to begin storing the data to.</param>
		/// <param name="size">The number of bytes to read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</param>
		/// <returns>The number of bytes read from the <see cref="T:System.Net.Sockets.NetworkStream" />, or 0 if the socket is closed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.  
		///  -or-  
		///  The <paramref name="offset" /> parameter is greater than the length of <paramref name="buffer" />.  
		///  -or-  
		///  The <paramref name="size" /> parameter is less than 0.  
		///  -or-  
		///  The <paramref name="size" /> parameter is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.  
		///  -or-  
		///  An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.  
		///  -or-  
		///  There is a failure reading from the network.</exception>
		// Token: 0x06003D07 RID: 15623 RVA: 0x000CF824 File Offset: 0x000CDA24
		public override int Read(byte[] buffer, int offset, int size)
		{
			bool canRead = this.CanRead;
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canRead)
			{
				throw new InvalidOperationException("The stream does not support reading.");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if ((ulong)offset > (ulong)((long)buffer.Length))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if ((ulong)size > (ulong)((long)(buffer.Length - offset)))
			{
				throw new ArgumentOutOfRangeException("size");
			}
			int result;
			try
			{
				result = this._streamSocket.Receive(buffer, offset, size, SocketFlags.None);
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to read data from the transport connection: {0}.", ex.Message), ex);
			}
			return result;
		}

		// Token: 0x06003D08 RID: 15624 RVA: 0x000CF8F0 File Offset: 0x000CDAF0
		public override int Read(Span<byte> destination)
		{
			if (base.GetType() != typeof(NetworkStream))
			{
				return base.Read(destination);
			}
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!this.CanRead)
			{
				throw new InvalidOperationException("The stream does not support reading.");
			}
			SocketError socketError;
			int result = this._streamSocket.Receive(destination, SocketFlags.None, out socketError);
			if (socketError != SocketError.Success)
			{
				SocketException ex = new SocketException((int)socketError);
				throw new IOException(SR.Format("Unable to read data from the transport connection: {0}.", ex.Message), ex);
			}
			return result;
		}

		// Token: 0x06003D09 RID: 15625 RVA: 0x000CF97C File Offset: 0x000CDB7C
		public unsafe override int ReadByte()
		{
			byte result;
			if (this.Read(new Span<byte>((void*)(&result), 1)) != 0)
			{
				return (int)result;
			}
			return -1;
		}

		/// <summary>Writes data to the <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to write to the <see cref="T:System.Net.Sockets.NetworkStream" />.</param>
		/// <param name="offset">The location in <paramref name="buffer" /> from which to start writing data.</param>
		/// <param name="size">The number of bytes to write to the <see cref="T:System.Net.Sockets.NetworkStream" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.  
		///  -or-  
		///  The <paramref name="offset" /> parameter is greater than the length of <paramref name="buffer" />.  
		///  -or-  
		///  The <paramref name="size" /> parameter is less than 0.  
		///  -or-  
		///  The <paramref name="size" /> parameter is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		/// <exception cref="T:System.IO.IOException">There was a failure while writing to the network.  
		///  -or-  
		///  An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.  
		///  -or-  
		///  There was a failure reading from the network.</exception>
		// Token: 0x06003D0A RID: 15626 RVA: 0x000CF9A0 File Offset: 0x000CDBA0
		public override void Write(byte[] buffer, int offset, int size)
		{
			bool canWrite = this.CanWrite;
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canWrite)
			{
				throw new InvalidOperationException("The stream does not support writing.");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if ((ulong)offset > (ulong)((long)buffer.Length))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if ((ulong)size > (ulong)((long)(buffer.Length - offset)))
			{
				throw new ArgumentOutOfRangeException("size");
			}
			try
			{
				this._streamSocket.Send(buffer, offset, size, SocketFlags.None);
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to write data to the transport connection: {0}.", ex.Message), ex);
			}
		}

		// Token: 0x06003D0B RID: 15627 RVA: 0x000CFA6C File Offset: 0x000CDC6C
		public override void Write(ReadOnlySpan<byte> source)
		{
			if (base.GetType() != typeof(NetworkStream))
			{
				base.Write(source);
				return;
			}
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!this.CanWrite)
			{
				throw new InvalidOperationException("The stream does not support writing.");
			}
			SocketError socketError;
			this._streamSocket.Send(source, SocketFlags.None, out socketError);
			if (socketError != SocketError.Success)
			{
				SocketException ex = new SocketException((int)socketError);
				throw new IOException(SR.Format("Unable to write data to the transport connection: {0}.", ex.Message), ex);
			}
		}

		// Token: 0x06003D0C RID: 15628 RVA: 0x000CFAF7 File Offset: 0x000CDCF7
		public unsafe override void WriteByte(byte value)
		{
			this.Write(new ReadOnlySpan<byte>((void*)(&value), 1));
		}

		/// <summary>Closes the <see cref="T:System.Net.Sockets.NetworkStream" /> after waiting the specified time to allow data to be sent.</summary>
		/// <param name="timeout">A 32-bit signed integer that specifies the number of milliseconds to wait to send any remaining data before closing.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="timeout" /> parameter is less than -1.</exception>
		// Token: 0x06003D0D RID: 15629 RVA: 0x000CFB08 File Offset: 0x000CDD08
		public void Close(int timeout)
		{
			if (timeout < -1)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			this._closeTimeout = timeout;
			base.Dispose();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Sockets.NetworkStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06003D0E RID: 15630 RVA: 0x000CFB28 File Offset: 0x000CDD28
		protected override void Dispose(bool disposing)
		{
			int cleanedUp = this._cleanedUp ? 1 : 0;
			this._cleanedUp = true;
			if (cleanedUp == 0 && disposing)
			{
				this._readable = false;
				this._writeable = false;
				if (this._ownsSocket)
				{
					this._streamSocket.InternalShutdown(SocketShutdown.Both);
					this._streamSocket.Close(this._closeTimeout);
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		// Token: 0x06003D0F RID: 15631 RVA: 0x000CFB88 File Offset: 0x000CDD88
		~NetworkStream()
		{
			this.Dispose(false);
		}

		/// <summary>Begins an asynchronous read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the location in memory to store data read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</param>
		/// <param name="offset">The location in <paramref name="buffer" /> to begin storing the data.</param>
		/// <param name="size">The number of bytes to read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate that is executed when <see cref="M:System.Net.Sockets.NetworkStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> completes.</param>
		/// <param name="state">An object that contains any additional user-defined data.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the asynchronous call.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.  
		///  -or-  
		///  The <paramref name="offset" /> parameter is greater than the length of the <paramref name="buffer" /> paramater.  
		///  -or-  
		///  The <paramref name="size" /> is less than 0.  
		///  -or-  
		///  The <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.  
		///  -or-  
		///  There was a failure while reading from the network.  
		///  -or-  
		///  An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.</exception>
		// Token: 0x06003D10 RID: 15632 RVA: 0x000CFBB8 File Offset: 0x000CDDB8
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			bool canRead = this.CanRead;
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canRead)
			{
				throw new InvalidOperationException("The stream does not support reading.");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if ((ulong)offset > (ulong)((long)buffer.Length))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if ((ulong)size > (ulong)((long)(buffer.Length - offset)))
			{
				throw new ArgumentOutOfRangeException("size");
			}
			IAsyncResult result;
			try
			{
				result = this._streamSocket.BeginReceive(buffer, offset, size, SocketFlags.None, callback, state);
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to read data from the transport connection: {0}.", ex.Message), ex);
			}
			return result;
		}

		/// <summary>Handles the end of an asynchronous read.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that represents an asynchronous call.</param>
		/// <returns>The number of bytes read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="asyncResult" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.  
		///  -or-  
		///  An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.</exception>
		// Token: 0x06003D11 RID: 15633 RVA: 0x000CFC88 File Offset: 0x000CDE88
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			int result;
			try
			{
				result = this._streamSocket.EndReceive(asyncResult);
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to read data from the transport connection: {0}.", ex.Message), ex);
			}
			return result;
		}

		/// <summary>Begins an asynchronous write to a stream.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to write to the <see cref="T:System.Net.Sockets.NetworkStream" />.</param>
		/// <param name="offset">The location in <paramref name="buffer" /> to begin sending the data.</param>
		/// <param name="size">The number of bytes to write to the <see cref="T:System.Net.Sockets.NetworkStream" />.</param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate that is executed when <see cref="M:System.Net.Sockets.NetworkStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> completes.</param>
		/// <param name="state">An object that contains any additional user-defined data.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the asynchronous call.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.  
		///  -or-  
		///  The <paramref name="offset" /> parameter is greater than the length of <paramref name="buffer" />.  
		///  -or-  
		///  The <paramref name="size" /> parameter is less than 0.  
		///  -or-  
		///  The <paramref name="size" /> parameter is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.  
		///  -or-  
		///  There was a failure while writing to the network.  
		///  -or-  
		///  An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.</exception>
		// Token: 0x06003D12 RID: 15634 RVA: 0x000CFD18 File Offset: 0x000CDF18
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			bool canWrite = this.CanWrite;
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canWrite)
			{
				throw new InvalidOperationException("The stream does not support writing.");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if ((ulong)offset > (ulong)((long)buffer.Length))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if ((ulong)size > (ulong)((long)(buffer.Length - offset)))
			{
				throw new ArgumentOutOfRangeException("size");
			}
			IAsyncResult result;
			try
			{
				result = this._streamSocket.BeginSend(buffer, offset, size, SocketFlags.None, callback, state);
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to write data to the transport connection: {0}.", ex.Message), ex);
			}
			return result;
		}

		/// <summary>Handles the end of an asynchronous write.</summary>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> that represents the asynchronous call.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.  
		///  -or-  
		///  An error occurred while writing to the network.  
		///  -or-  
		///  An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.</exception>
		// Token: 0x06003D13 RID: 15635 RVA: 0x000CFDE8 File Offset: 0x000CDFE8
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			try
			{
				this._streamSocket.EndSend(asyncResult);
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to write data to the transport connection: {0}.", ex.Message), ex);
			}
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x000CFE74 File Offset: 0x000CE074
		public override Task<int> ReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			bool canRead = this.CanRead;
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canRead)
			{
				throw new InvalidOperationException("The stream does not support reading.");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if ((ulong)offset > (ulong)((long)buffer.Length))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if ((ulong)size > (ulong)((long)(buffer.Length - offset)))
			{
				throw new ArgumentOutOfRangeException("size");
			}
			Task<int> result;
			try
			{
				result = this._streamSocket.ReceiveAsync(new Memory<byte>(buffer, offset, size), SocketFlags.None, true, cancellationToken).AsTask();
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to read data from the transport connection: {0}.", ex.Message), ex);
			}
			return result;
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x000CFF50 File Offset: 0x000CE150
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken)
		{
			bool canRead = this.CanRead;
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canRead)
			{
				throw new InvalidOperationException("The stream does not support reading.");
			}
			ValueTask<int> result;
			try
			{
				result = this._streamSocket.ReceiveAsync(buffer, SocketFlags.None, true, cancellationToken);
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to read data from the transport connection: {0}.", ex.Message), ex);
			}
			return result;
		}

		// Token: 0x06003D16 RID: 15638 RVA: 0x000CFFE8 File Offset: 0x000CE1E8
		public override Task WriteAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			bool canWrite = this.CanWrite;
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canWrite)
			{
				throw new InvalidOperationException("The stream does not support writing.");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if ((ulong)offset > (ulong)((long)buffer.Length))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if ((ulong)size > (ulong)((long)(buffer.Length - offset)))
			{
				throw new ArgumentOutOfRangeException("size");
			}
			Task result;
			try
			{
				result = this._streamSocket.SendAsyncForNetworkStream(new ReadOnlyMemory<byte>(buffer, offset, size), SocketFlags.None, cancellationToken).AsTask();
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to write data to the transport connection: {0}.", ex.Message), ex);
			}
			return result;
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x000D00C4 File Offset: 0x000CE2C4
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken)
		{
			bool canWrite = this.CanWrite;
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canWrite)
			{
				throw new InvalidOperationException("The stream does not support writing.");
			}
			ValueTask result;
			try
			{
				result = this._streamSocket.SendAsyncForNetworkStream(buffer, SocketFlags.None, cancellationToken);
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to write data to the transport connection: {0}.", ex.Message), ex);
			}
			return result;
		}

		/// <summary>Flushes data from the stream. This method is reserved for future use.</summary>
		// Token: 0x06003D18 RID: 15640 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Flush()
		{
		}

		/// <summary>Flushes data from the stream as an asynchronous operation.</summary>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that this  operation should be canceled.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06003D19 RID: 15641 RVA: 0x0008EB07 File Offset: 0x0008CD07
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		/// <summary>Sets the length of the stream. This method always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">This parameter is not used.</param>
		/// <exception cref="T:System.NotSupportedException">Any use of this property.</exception>
		// Token: 0x06003D1A RID: 15642 RVA: 0x000C3C76 File Offset: 0x000C1E76
		public override void SetLength(long value)
		{
			throw new NotSupportedException("This stream does not support seek operations.");
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x000D0158 File Offset: 0x000CE358
		internal void SetSocketTimeoutOption(SocketShutdown mode, int timeout, bool silent)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, mode, timeout, silent, "SetSocketTimeoutOption");
			}
			if (timeout < 0)
			{
				timeout = 0;
			}
			if ((mode == SocketShutdown.Send || mode == SocketShutdown.Both) && timeout != this._currentWriteTimeout)
			{
				this._streamSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, timeout, silent);
				this._currentWriteTimeout = timeout;
			}
			if ((mode == SocketShutdown.Receive || mode == SocketShutdown.Both) && timeout != this._currentReadTimeout)
			{
				this._streamSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, timeout, silent);
				this._currentReadTimeout = timeout;
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06003D1C RID: 15644 RVA: 0x000D01F0 File Offset: 0x000CE3F0
		internal Socket InternalSocket
		{
			get
			{
				Socket streamSocket = this._streamSocket;
				if (this._cleanedUp || streamSocket == null)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return streamSocket;
			}
		}

		// Token: 0x0400241B RID: 9243
		private readonly Socket _streamSocket;

		// Token: 0x0400241C RID: 9244
		private readonly bool _ownsSocket;

		// Token: 0x0400241D RID: 9245
		private bool _readable;

		// Token: 0x0400241E RID: 9246
		private bool _writeable;

		// Token: 0x0400241F RID: 9247
		private int _closeTimeout = -1;

		// Token: 0x04002420 RID: 9248
		private volatile bool _cleanedUp;

		// Token: 0x04002421 RID: 9249
		private int _currentReadTimeout = -1;

		// Token: 0x04002422 RID: 9250
		private int _currentWriteTimeout = -1;
	}
}
