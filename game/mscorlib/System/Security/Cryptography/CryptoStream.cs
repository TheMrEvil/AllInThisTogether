using System;
using System.Buffers;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Security.Cryptography
{
	/// <summary>Defines a stream that links data streams to cryptographic transformations.</summary>
	// Token: 0x02000469 RID: 1129
	public class CryptoStream : Stream, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptoStream" /> class with a target data stream, the transformation to use, and the mode of the stream.</summary>
		/// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
		/// <param name="transform">The cryptographic transformation that is to be performed on the stream.</param>
		/// <param name="mode">One of the <see cref="T:System.Security.Cryptography.CryptoStreamMode" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is invalid.</exception>
		// Token: 0x06002DD2 RID: 11730 RVA: 0x000A415C File Offset: 0x000A235C
		public CryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode) : this(stream, transform, mode, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptoStream" /> class.</summary>
		/// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
		/// <param name="transform">The cryptographic transformation that is to be performed on the stream.</param>
		/// <param name="mode">The mode of the stream.</param>
		/// <param name="leaveOpen">
		///   <see langword="true" /> to not close the underlying stream when the <see cref="T:System.Security.Cryptography.CryptoStream" /> object is disposed; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is invalid.</exception>
		// Token: 0x06002DD3 RID: 11731 RVA: 0x000A4168 File Offset: 0x000A2368
		public CryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode, bool leaveOpen)
		{
			this._stream = stream;
			this._transformMode = mode;
			this._transform = transform;
			this._leaveOpen = leaveOpen;
			CryptoStreamMode transformMode = this._transformMode;
			if (transformMode != CryptoStreamMode.Read)
			{
				if (transformMode != CryptoStreamMode.Write)
				{
					throw new ArgumentException("Argument {0} should be larger than {1}.");
				}
				if (!this._stream.CanWrite)
				{
					throw new ArgumentException(SR.Format("Stream was not writable.", "stream"));
				}
				this._canWrite = true;
			}
			else
			{
				if (!this._stream.CanRead)
				{
					throw new ArgumentException(SR.Format("Stream was not readable.", "stream"));
				}
				this._canRead = true;
			}
			this.InitializeBuffer();
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Security.Cryptography.CryptoStream" /> is readable.</summary>
		/// <returns>
		///   <see langword="true" /> if the current stream is readable; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06002DD4 RID: 11732 RVA: 0x000A420F File Offset: 0x000A240F
		public override bool CanRead
		{
			get
			{
				return this._canRead;
			}
		}

		/// <summary>Gets a value indicating whether you can seek within the current <see cref="T:System.Security.Cryptography.CryptoStream" />.</summary>
		/// <returns>Always <see langword="false" />.</returns>
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06002DD5 RID: 11733 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Security.Cryptography.CryptoStream" /> is writable.</summary>
		/// <returns>
		///   <see langword="true" /> if the current stream is writable; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06002DD6 RID: 11734 RVA: 0x000A4217 File Offset: 0x000A2417
		public override bool CanWrite
		{
			get
			{
				return this._canWrite;
			}
		}

		/// <summary>Gets the length in bytes of the stream.</summary>
		/// <returns>This property is not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported.</exception>
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06002DD7 RID: 11735 RVA: 0x000A421F File Offset: 0x000A241F
		public override long Length
		{
			get
			{
				throw new NotSupportedException("Stream does not support seeking.");
			}
		}

		/// <summary>Gets or sets the position within the current stream.</summary>
		/// <returns>This property is not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported.</exception>
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06002DD8 RID: 11736 RVA: 0x000A421F File Offset: 0x000A241F
		// (set) Token: 0x06002DD9 RID: 11737 RVA: 0x000A421F File Offset: 0x000A241F
		public override long Position
		{
			get
			{
				throw new NotSupportedException("Stream does not support seeking.");
			}
			set
			{
				throw new NotSupportedException("Stream does not support seeking.");
			}
		}

		/// <summary>Gets a value indicating whether the final buffer block has been written to the underlying stream.</summary>
		/// <returns>
		///   <see langword="true" /> if the final block has been flushed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06002DDA RID: 11738 RVA: 0x000A422B File Offset: 0x000A242B
		public bool HasFlushedFinalBlock
		{
			get
			{
				return this._finalBlockTransformed;
			}
		}

		/// <summary>Updates the underlying data source or repository with the current state of the buffer, then clears the buffer.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key is corrupt which can cause invalid padding to the stream.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream is not writable.  
		///  -or-  
		///  The final block has already been transformed.</exception>
		// Token: 0x06002DDB RID: 11739 RVA: 0x000A4234 File Offset: 0x000A2434
		public void FlushFinalBlock()
		{
			if (this._finalBlockTransformed)
			{
				throw new NotSupportedException("FlushFinalBlock() method was called twice on a CryptoStream. It can only be called once.");
			}
			byte[] array = this._transform.TransformFinalBlock(this._inputBuffer, 0, this._inputBufferIndex);
			this._finalBlockTransformed = true;
			if (this._canWrite && this._outputBufferIndex > 0)
			{
				this._stream.Write(this._outputBuffer, 0, this._outputBufferIndex);
				this._outputBufferIndex = 0;
			}
			if (this._canWrite)
			{
				this._stream.Write(array, 0, array.Length);
			}
			CryptoStream cryptoStream = this._stream as CryptoStream;
			if (cryptoStream != null)
			{
				if (!cryptoStream.HasFlushedFinalBlock)
				{
					cryptoStream.FlushFinalBlock();
				}
			}
			else
			{
				this._stream.Flush();
			}
			if (this._inputBuffer != null)
			{
				Array.Clear(this._inputBuffer, 0, this._inputBuffer.Length);
			}
			if (this._outputBuffer != null)
			{
				Array.Clear(this._outputBuffer, 0, this._outputBuffer.Length);
			}
		}

		/// <summary>Clears all buffers for the current stream and causes any buffered data to be written to the underlying device.</summary>
		// Token: 0x06002DDC RID: 11740 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public override void Flush()
		{
		}

		/// <summary>Clears all buffers for the current stream asynchronously, causes any buffered data to be written to the underlying device, and monitors cancellation requests.</summary>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x06002DDD RID: 11741 RVA: 0x000A431E File Offset: 0x000A251E
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (base.GetType() != typeof(CryptoStream))
			{
				return base.FlushAsync(cancellationToken);
			}
			if (!cancellationToken.IsCancellationRequested)
			{
				return Task.CompletedTask;
			}
			return Task.FromCanceled(cancellationToken);
		}

		/// <summary>Sets the position within the current stream.</summary>
		/// <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter.</param>
		/// <param name="origin">A <see cref="T:System.IO.SeekOrigin" /> object indicating the reference point used to obtain the new position.</param>
		/// <returns>This method is not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06002DDE RID: 11742 RVA: 0x000A421F File Offset: 0x000A241F
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("Stream does not support seeking.");
		}

		/// <summary>Sets the length of the current stream.</summary>
		/// <param name="value">The desired length of the current stream in bytes.</param>
		/// <exception cref="T:System.NotSupportedException">This property exists only to support inheritance from <see cref="T:System.IO.Stream" />, and cannot be used.</exception>
		// Token: 0x06002DDF RID: 11743 RVA: 0x000A421F File Offset: 0x000A241F
		public override void SetLength(long value)
		{
			throw new NotSupportedException("Stream does not support seeking.");
		}

		/// <summary>Reads a sequence of bytes from the current stream asynchronously, advances the position within the stream by the number of bytes read, and monitors cancellation requests.</summary>
		/// <param name="buffer">The buffer to write the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <returns>A task that represents the asynchronous read operation. The value of the task object's <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous read operation.</exception>
		// Token: 0x06002DE0 RID: 11744 RVA: 0x000A4354 File Offset: 0x000A2554
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			this.CheckReadArguments(buffer, offset, count);
			return this.ReadAsyncInternal(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x000A436A File Offset: 0x000A256A
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(this.ReadAsync(buffer, offset, count, CancellationToken.None), callback, state);
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x000A4383 File Offset: 0x000A2583
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x000A438C File Offset: 0x000A258C
		private Task<int> ReadAsyncInternal(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			CryptoStream.<ReadAsyncInternal>d__37 <ReadAsyncInternal>d__;
			<ReadAsyncInternal>d__.<>4__this = this;
			<ReadAsyncInternal>d__.buffer = buffer;
			<ReadAsyncInternal>d__.offset = offset;
			<ReadAsyncInternal>d__.count = count;
			<ReadAsyncInternal>d__.cancellationToken = cancellationToken;
			<ReadAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadAsyncInternal>d__.<>1__state = -1;
			<ReadAsyncInternal>d__.<>t__builder.Start<CryptoStream.<ReadAsyncInternal>d__37>(ref <ReadAsyncInternal>d__);
			return <ReadAsyncInternal>d__.<>t__builder.Task;
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x000A43F0 File Offset: 0x000A25F0
		public override int ReadByte()
		{
			if (this._outputBufferIndex > 1)
			{
				int result = (int)this._outputBuffer[0];
				Buffer.BlockCopy(this._outputBuffer, 1, this._outputBuffer, 0, this._outputBufferIndex - 1);
				this._outputBufferIndex--;
				return result;
			}
			return base.ReadByte();
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x000A4440 File Offset: 0x000A2640
		public override void WriteByte(byte value)
		{
			if (this._inputBufferIndex + 1 < this._inputBlockSize)
			{
				byte[] inputBuffer = this._inputBuffer;
				int inputBufferIndex = this._inputBufferIndex;
				this._inputBufferIndex = inputBufferIndex + 1;
				inputBuffer[inputBufferIndex] = value;
				return;
			}
			base.WriteByte(value);
		}

		/// <summary>Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.</summary>
		/// <param name="buffer">An array of bytes. A maximum of <paramref name="count" /> bytes are read from the current stream and stored in <paramref name="buffer" />.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream.</param>
		/// <param name="count">The maximum number of bytes to be read from the current stream.</param>
		/// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.Cryptography.CryptoStreamMode" /> associated with current <see cref="T:System.Security.Cryptography.CryptoStream" /> object does not match the underlying stream.  For example, this exception is thrown when using <see cref="F:System.Security.Cryptography.CryptoStreamMode.Read" /> with an underlying stream that is write only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than zero.  
		///  -or-  
		///  The <paramref name="count" /> parameter is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">Thesum of the <paramref name="count" /> and <paramref name="offset" /> parameters is longer than the length of the buffer.</exception>
		// Token: 0x06002DE6 RID: 11750 RVA: 0x000A4480 File Offset: 0x000A2680
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckReadArguments(buffer, offset, count);
			return this.ReadAsyncCore(buffer, offset, count, default(CancellationToken), false).GetAwaiter().GetResult();
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x000A44B8 File Offset: 0x000A26B8
		private void CheckReadArguments(byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading.");
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
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x000A4514 File Offset: 0x000A2714
		private Task<int> ReadAsyncCore(byte[] buffer, int offset, int count, CancellationToken cancellationToken, bool useAsync)
		{
			CryptoStream.<ReadAsyncCore>d__42 <ReadAsyncCore>d__;
			<ReadAsyncCore>d__.<>4__this = this;
			<ReadAsyncCore>d__.buffer = buffer;
			<ReadAsyncCore>d__.offset = offset;
			<ReadAsyncCore>d__.count = count;
			<ReadAsyncCore>d__.cancellationToken = cancellationToken;
			<ReadAsyncCore>d__.useAsync = useAsync;
			<ReadAsyncCore>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadAsyncCore>d__.<>1__state = -1;
			<ReadAsyncCore>d__.<>t__builder.Start<CryptoStream.<ReadAsyncCore>d__42>(ref <ReadAsyncCore>d__);
			return <ReadAsyncCore>d__.<>t__builder.Task;
		}

		/// <summary>Writes a sequence of bytes to the current stream asynchronously, advances the current position within the stream by the number of bytes written, and monitors cancellation requests.</summary>
		/// <param name="buffer">The buffer to write data from.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> from which to begin writing bytes to the stream.</param>
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
		// Token: 0x06002DE9 RID: 11753 RVA: 0x000A4581 File Offset: 0x000A2781
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			this.CheckWriteArguments(buffer, offset, count);
			return this.WriteAsyncInternal(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x000A4597 File Offset: 0x000A2797
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(this.WriteAsync(buffer, offset, count, CancellationToken.None), callback, state);
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x000A45B0 File Offset: 0x000A27B0
		public override void EndWrite(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x000A45B8 File Offset: 0x000A27B8
		private Task WriteAsyncInternal(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			CryptoStream.<WriteAsyncInternal>d__46 <WriteAsyncInternal>d__;
			<WriteAsyncInternal>d__.<>4__this = this;
			<WriteAsyncInternal>d__.buffer = buffer;
			<WriteAsyncInternal>d__.offset = offset;
			<WriteAsyncInternal>d__.count = count;
			<WriteAsyncInternal>d__.cancellationToken = cancellationToken;
			<WriteAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAsyncInternal>d__.<>1__state = -1;
			<WriteAsyncInternal>d__.<>t__builder.Start<CryptoStream.<WriteAsyncInternal>d__46>(ref <WriteAsyncInternal>d__);
			return <WriteAsyncInternal>d__.<>t__builder.Task;
		}

		/// <summary>Writes a sequence of bytes to the current <see cref="T:System.Security.Cryptography.CryptoStream" /> and advances the current position within the stream by the number of bytes written.</summary>
		/// <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The number of bytes to be written to the current stream.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.Cryptography.CryptoStreamMode" /> associated with current <see cref="T:System.Security.Cryptography.CryptoStream" /> object does not match the underlying stream.  For example, this exception is thrown when using <see cref="F:System.Security.Cryptography.CryptoStreamMode.Write" /> with an underlying stream that is read only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than zero.  
		///  -or-  
		///  The <paramref name="count" /> parameter is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of the <paramref name="count" /> and <paramref name="offset" /> parameters is longer than the length of the buffer.</exception>
		// Token: 0x06002DED RID: 11757 RVA: 0x000A461C File Offset: 0x000A281C
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckWriteArguments(buffer, offset, count);
			this.WriteAsyncCore(buffer, offset, count, default(CancellationToken), false).GetAwaiter().GetResult();
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x000A4654 File Offset: 0x000A2854
		private void CheckWriteArguments(byte[] buffer, int offset, int count)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing.");
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
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x000A46B0 File Offset: 0x000A28B0
		private Task WriteAsyncCore(byte[] buffer, int offset, int count, CancellationToken cancellationToken, bool useAsync)
		{
			CryptoStream.<WriteAsyncCore>d__49 <WriteAsyncCore>d__;
			<WriteAsyncCore>d__.<>4__this = this;
			<WriteAsyncCore>d__.buffer = buffer;
			<WriteAsyncCore>d__.offset = offset;
			<WriteAsyncCore>d__.count = count;
			<WriteAsyncCore>d__.cancellationToken = cancellationToken;
			<WriteAsyncCore>d__.useAsync = useAsync;
			<WriteAsyncCore>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAsyncCore>d__.<>1__state = -1;
			<WriteAsyncCore>d__.<>t__builder.Start<CryptoStream.<WriteAsyncCore>d__49>(ref <WriteAsyncCore>d__);
			return <WriteAsyncCore>d__.<>t__builder.Task;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.CryptoStream" />.</summary>
		// Token: 0x06002DF0 RID: 11760 RVA: 0x000A471D File Offset: 0x000A291D
		public void Clear()
		{
			this.Close();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.CryptoStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002DF1 RID: 11761 RVA: 0x000A4728 File Offset: 0x000A2928
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (!this._finalBlockTransformed)
					{
						this.FlushFinalBlock();
					}
					if (!this._leaveOpen)
					{
						this._stream.Dispose();
					}
				}
			}
			finally
			{
				try
				{
					this._finalBlockTransformed = true;
					if (this._inputBuffer != null)
					{
						Array.Clear(this._inputBuffer, 0, this._inputBuffer.Length);
					}
					if (this._outputBuffer != null)
					{
						Array.Clear(this._outputBuffer, 0, this._outputBuffer.Length);
					}
					this._inputBuffer = null;
					this._outputBuffer = null;
					this._canRead = false;
					this._canWrite = false;
				}
				finally
				{
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000A47E0 File Offset: 0x000A29E0
		private void InitializeBuffer()
		{
			if (this._transform != null)
			{
				this._inputBlockSize = this._transform.InputBlockSize;
				this._inputBuffer = new byte[this._inputBlockSize];
				this._outputBlockSize = this._transform.OutputBlockSize;
				this._outputBuffer = new byte[this._outputBlockSize];
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x000A4839 File Offset: 0x000A2A39
		private SemaphoreSlim AsyncActiveSemaphore
		{
			get
			{
				return LazyInitializer.EnsureInitialized<SemaphoreSlim>(ref this._lazyAsyncActiveSemaphore, () => new SemaphoreSlim(1, 1));
			}
		}

		// Token: 0x040020D1 RID: 8401
		private readonly Stream _stream;

		// Token: 0x040020D2 RID: 8402
		private readonly ICryptoTransform _transform;

		// Token: 0x040020D3 RID: 8403
		private readonly CryptoStreamMode _transformMode;

		// Token: 0x040020D4 RID: 8404
		private byte[] _inputBuffer;

		// Token: 0x040020D5 RID: 8405
		private int _inputBufferIndex;

		// Token: 0x040020D6 RID: 8406
		private int _inputBlockSize;

		// Token: 0x040020D7 RID: 8407
		private byte[] _outputBuffer;

		// Token: 0x040020D8 RID: 8408
		private int _outputBufferIndex;

		// Token: 0x040020D9 RID: 8409
		private int _outputBlockSize;

		// Token: 0x040020DA RID: 8410
		private bool _canRead;

		// Token: 0x040020DB RID: 8411
		private bool _canWrite;

		// Token: 0x040020DC RID: 8412
		private bool _finalBlockTransformed;

		// Token: 0x040020DD RID: 8413
		private SemaphoreSlim _lazyAsyncActiveSemaphore;

		// Token: 0x040020DE RID: 8414
		private readonly bool _leaveOpen;

		// Token: 0x0200046A RID: 1130
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAsyncInternal>d__37 : IAsyncStateMachine
		{
			// Token: 0x06002DF4 RID: 11764 RVA: 0x000A4868 File Offset: 0x000A2A68
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				CryptoStream cryptoStream = this.<>4__this;
				int result;
				try
				{
					ForceAsyncAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							goto IL_8A;
						}
						this.<semaphore>5__2 = cryptoStream.AsyncActiveSemaphore;
						awaiter = this.<semaphore>5__2.WaitAsync().ForceAsync().GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ForceAsyncAwaiter, CryptoStream.<ReadAsyncInternal>d__37>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ForceAsyncAwaiter);
						num = (this.<>1__state = -1);
					}
					awaiter.GetResult();
					IL_8A:
					try
					{
						TaskAwaiter<int> awaiter2;
						if (num != 1)
						{
							awaiter2 = cryptoStream.ReadAsyncCore(this.buffer, this.offset, this.count, this.cancellationToken, true).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 1);
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<int>, CryptoStream.<ReadAsyncInternal>d__37>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(TaskAwaiter<int>);
							num = (this.<>1__state = -1);
						}
						result = awaiter2.GetResult();
					}
					finally
					{
						if (num < 0)
						{
							this.<semaphore>5__2.Release();
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<semaphore>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<semaphore>5__2 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06002DF5 RID: 11765 RVA: 0x000A49FC File Offset: 0x000A2BFC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040020DF RID: 8415
			public int <>1__state;

			// Token: 0x040020E0 RID: 8416
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x040020E1 RID: 8417
			public CryptoStream <>4__this;

			// Token: 0x040020E2 RID: 8418
			public byte[] buffer;

			// Token: 0x040020E3 RID: 8419
			public int offset;

			// Token: 0x040020E4 RID: 8420
			public int count;

			// Token: 0x040020E5 RID: 8421
			public CancellationToken cancellationToken;

			// Token: 0x040020E6 RID: 8422
			private SemaphoreSlim <semaphore>5__2;

			// Token: 0x040020E7 RID: 8423
			private ForceAsyncAwaiter <>u__1;

			// Token: 0x040020E8 RID: 8424
			private TaskAwaiter<int> <>u__2;
		}

		// Token: 0x0200046B RID: 1131
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAsyncCore>d__42 : IAsyncStateMachine
		{
			// Token: 0x06002DF6 RID: 11766 RVA: 0x000A4A0C File Offset: 0x000A2C0C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				CryptoStream cryptoStream = this.<>4__this;
				int result;
				try
				{
					ValueTaskAwaiter<int> awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ValueTaskAwaiter<int>);
							num = (this.<>1__state = -1);
							goto IL_4E4;
						}
						this.<bytesToDeliver>5__2 = this.count;
						this.<currentOutputIndex>5__3 = this.offset;
						if (cryptoStream._outputBufferIndex != 0)
						{
							if (cryptoStream._outputBufferIndex > this.count)
							{
								Buffer.BlockCopy(cryptoStream._outputBuffer, 0, this.buffer, this.offset, this.count);
								Buffer.BlockCopy(cryptoStream._outputBuffer, this.count, cryptoStream._outputBuffer, 0, cryptoStream._outputBufferIndex - this.count);
								cryptoStream._outputBufferIndex -= this.count;
								int length = cryptoStream._outputBuffer.Length - cryptoStream._outputBufferIndex;
								CryptographicOperations.ZeroMemory(new Span<byte>(cryptoStream._outputBuffer, cryptoStream._outputBufferIndex, length));
								result = this.count;
								goto IL_78D;
							}
							Buffer.BlockCopy(cryptoStream._outputBuffer, 0, this.buffer, this.offset, cryptoStream._outputBufferIndex);
							this.<bytesToDeliver>5__2 -= cryptoStream._outputBufferIndex;
							this.<currentOutputIndex>5__3 += cryptoStream._outputBufferIndex;
							int length2 = cryptoStream._outputBuffer.Length - cryptoStream._outputBufferIndex;
							CryptographicOperations.ZeroMemory(new Span<byte>(cryptoStream._outputBuffer, cryptoStream._outputBufferIndex, length2));
							cryptoStream._outputBufferIndex = 0;
						}
						if (cryptoStream._finalBlockTransformed)
						{
							result = this.count - this.<bytesToDeliver>5__2;
							goto IL_78D;
						}
						int num2 = this.<bytesToDeliver>5__2 / cryptoStream._outputBlockSize;
						if (num2 <= 1 || !cryptoStream._transform.CanTransformMultipleBlocks)
						{
							goto IL_63F;
						}
						this.<numWholeBlocksInBytes>5__4 = num2 * cryptoStream._inputBlockSize;
						this.<tempInputBuffer>5__5 = ArrayPool<byte>.Shared.Rent(this.<numWholeBlocksInBytes>5__4);
						this.<tempOutputBuffer>5__6 = null;
					}
					int num3;
					int num4;
					try
					{
						if (num != 0)
						{
							if (!this.useAsync)
							{
								num3 = cryptoStream._stream.Read(this.<tempInputBuffer>5__5, cryptoStream._inputBufferIndex, this.<numWholeBlocksInBytes>5__4 - cryptoStream._inputBufferIndex);
								goto IL_284;
							}
							awaiter = cryptoStream._stream.ReadAsync(new Memory<byte>(this.<tempInputBuffer>5__5, cryptoStream._inputBufferIndex, this.<numWholeBlocksInBytes>5__4 - cryptoStream._inputBufferIndex), this.cancellationToken).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ValueTaskAwaiter<int>, CryptoStream.<ReadAsyncCore>d__42>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ValueTaskAwaiter<int>);
							num = (this.<>1__state = -1);
						}
						num3 = awaiter.GetResult();
						IL_284:
						num4 = num3;
						int num5 = cryptoStream._inputBufferIndex + num4;
						if (num5 < cryptoStream._inputBlockSize)
						{
							Buffer.BlockCopy(this.<tempInputBuffer>5__5, cryptoStream._inputBufferIndex, cryptoStream._inputBuffer, cryptoStream._inputBufferIndex, num4);
							cryptoStream._inputBufferIndex = num5;
						}
						else
						{
							Buffer.BlockCopy(cryptoStream._inputBuffer, 0, this.<tempInputBuffer>5__5, 0, cryptoStream._inputBufferIndex);
							CryptographicOperations.ZeroMemory(new Span<byte>(cryptoStream._inputBuffer, 0, cryptoStream._inputBufferIndex));
							num4 += cryptoStream._inputBufferIndex;
							cryptoStream._inputBufferIndex = 0;
							int num6 = num4 / cryptoStream._inputBlockSize;
							int num7 = num6 * cryptoStream._inputBlockSize;
							int num8 = num4 - num7;
							if (num8 != 0)
							{
								cryptoStream._inputBufferIndex = num8;
								Buffer.BlockCopy(this.<tempInputBuffer>5__5, num7, cryptoStream._inputBuffer, 0, num8);
							}
							this.<tempOutputBuffer>5__6 = ArrayPool<byte>.Shared.Rent(num6 * cryptoStream._outputBlockSize);
							int num9 = cryptoStream._transform.TransformBlock(this.<tempInputBuffer>5__5, 0, num7, this.<tempOutputBuffer>5__6, 0);
							Buffer.BlockCopy(this.<tempOutputBuffer>5__6, 0, this.buffer, this.<currentOutputIndex>5__3, num9);
							CryptographicOperations.ZeroMemory(new Span<byte>(this.<tempOutputBuffer>5__6, 0, num9));
							ArrayPool<byte>.Shared.Return(this.<tempOutputBuffer>5__6, false);
							this.<tempOutputBuffer>5__6 = null;
							this.<bytesToDeliver>5__2 -= num9;
							this.<currentOutputIndex>5__3 += num9;
						}
					}
					finally
					{
						if (num < 0)
						{
							if (this.<tempOutputBuffer>5__6 != null)
							{
								CryptographicOperations.ZeroMemory(this.<tempOutputBuffer>5__6);
								ArrayPool<byte>.Shared.Return(this.<tempOutputBuffer>5__6, false);
								this.<tempOutputBuffer>5__6 = null;
							}
							CryptographicOperations.ZeroMemory(new Span<byte>(this.<tempInputBuffer>5__5, 0, this.<numWholeBlocksInBytes>5__4));
							ArrayPool<byte>.Shared.Return(this.<tempInputBuffer>5__5, false);
							this.<tempInputBuffer>5__5 = null;
						}
					}
					this.<tempInputBuffer>5__5 = null;
					this.<tempOutputBuffer>5__6 = null;
					goto IL_63F;
					IL_4E4:
					num3 = awaiter.GetResult();
					IL_515:
					num4 = num3;
					if (num4 != 0)
					{
						cryptoStream._inputBufferIndex += num4;
					}
					else
					{
						byte[] array = cryptoStream._transform.TransformFinalBlock(cryptoStream._inputBuffer, 0, cryptoStream._inputBufferIndex);
						cryptoStream._outputBuffer = array;
						cryptoStream._outputBufferIndex = array.Length;
						cryptoStream._finalBlockTransformed = true;
						if (this.<bytesToDeliver>5__2 < cryptoStream._outputBufferIndex)
						{
							Buffer.BlockCopy(cryptoStream._outputBuffer, 0, this.buffer, this.<currentOutputIndex>5__3, this.<bytesToDeliver>5__2);
							cryptoStream._outputBufferIndex -= this.<bytesToDeliver>5__2;
							Buffer.BlockCopy(cryptoStream._outputBuffer, this.<bytesToDeliver>5__2, cryptoStream._outputBuffer, 0, cryptoStream._outputBufferIndex);
							int length3 = cryptoStream._outputBuffer.Length - cryptoStream._outputBufferIndex;
							CryptographicOperations.ZeroMemory(new Span<byte>(cryptoStream._outputBuffer, cryptoStream._outputBufferIndex, length3));
							result = this.count;
							goto IL_78D;
						}
						Buffer.BlockCopy(cryptoStream._outputBuffer, 0, this.buffer, this.<currentOutputIndex>5__3, cryptoStream._outputBufferIndex);
						this.<bytesToDeliver>5__2 -= cryptoStream._outputBufferIndex;
						cryptoStream._outputBufferIndex = 0;
						CryptographicOperations.ZeroMemory(cryptoStream._outputBuffer);
						result = this.count - this.<bytesToDeliver>5__2;
						goto IL_78D;
					}
					IL_52C:
					if (cryptoStream._inputBufferIndex >= cryptoStream._inputBlockSize)
					{
						int num9 = cryptoStream._transform.TransformBlock(cryptoStream._inputBuffer, 0, cryptoStream._inputBlockSize, cryptoStream._outputBuffer, 0);
						cryptoStream._inputBufferIndex = 0;
						if (this.<bytesToDeliver>5__2 < num9)
						{
							Buffer.BlockCopy(cryptoStream._outputBuffer, 0, this.buffer, this.<currentOutputIndex>5__3, this.<bytesToDeliver>5__2);
							cryptoStream._outputBufferIndex = num9 - this.<bytesToDeliver>5__2;
							Buffer.BlockCopy(cryptoStream._outputBuffer, this.<bytesToDeliver>5__2, cryptoStream._outputBuffer, 0, cryptoStream._outputBufferIndex);
							int length4 = cryptoStream._outputBuffer.Length - cryptoStream._outputBufferIndex;
							CryptographicOperations.ZeroMemory(new Span<byte>(cryptoStream._outputBuffer, cryptoStream._outputBufferIndex, length4));
							result = this.count;
							goto IL_78D;
						}
						Buffer.BlockCopy(cryptoStream._outputBuffer, 0, this.buffer, this.<currentOutputIndex>5__3, num9);
						CryptographicOperations.ZeroMemory(new Span<byte>(cryptoStream._outputBuffer, 0, num9));
						this.<currentOutputIndex>5__3 += num9;
						this.<bytesToDeliver>5__2 -= num9;
					}
					else
					{
						if (!this.useAsync)
						{
							num3 = cryptoStream._stream.Read(cryptoStream._inputBuffer, cryptoStream._inputBufferIndex, cryptoStream._inputBlockSize - cryptoStream._inputBufferIndex);
							goto IL_515;
						}
						awaiter = cryptoStream._stream.ReadAsync(new Memory<byte>(cryptoStream._inputBuffer, cryptoStream._inputBufferIndex, cryptoStream._inputBlockSize - cryptoStream._inputBufferIndex), this.cancellationToken).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 1);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ValueTaskAwaiter<int>, CryptoStream.<ReadAsyncCore>d__42>(ref awaiter, ref this);
							return;
						}
						goto IL_4E4;
					}
					IL_63F:
					if (this.<bytesToDeliver>5__2 > 0)
					{
						goto IL_52C;
					}
					result = this.count;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_78D:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06002DF7 RID: 11767 RVA: 0x000A51F0 File Offset: 0x000A33F0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040020E9 RID: 8425
			public int <>1__state;

			// Token: 0x040020EA RID: 8426
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x040020EB RID: 8427
			public int count;

			// Token: 0x040020EC RID: 8428
			public int offset;

			// Token: 0x040020ED RID: 8429
			public CryptoStream <>4__this;

			// Token: 0x040020EE RID: 8430
			public byte[] buffer;

			// Token: 0x040020EF RID: 8431
			public bool useAsync;

			// Token: 0x040020F0 RID: 8432
			public CancellationToken cancellationToken;

			// Token: 0x040020F1 RID: 8433
			private int <bytesToDeliver>5__2;

			// Token: 0x040020F2 RID: 8434
			private int <currentOutputIndex>5__3;

			// Token: 0x040020F3 RID: 8435
			private int <numWholeBlocksInBytes>5__4;

			// Token: 0x040020F4 RID: 8436
			private byte[] <tempInputBuffer>5__5;

			// Token: 0x040020F5 RID: 8437
			private byte[] <tempOutputBuffer>5__6;

			// Token: 0x040020F6 RID: 8438
			private ValueTaskAwaiter<int> <>u__1;
		}

		// Token: 0x0200046C RID: 1132
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAsyncInternal>d__46 : IAsyncStateMachine
		{
			// Token: 0x06002DF8 RID: 11768 RVA: 0x000A5200 File Offset: 0x000A3400
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				CryptoStream cryptoStream = this.<>4__this;
				try
				{
					ForceAsyncAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							goto IL_89;
						}
						this.<semaphore>5__2 = cryptoStream.AsyncActiveSemaphore;
						awaiter = this.<semaphore>5__2.WaitAsync().ForceAsync().GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ForceAsyncAwaiter, CryptoStream.<WriteAsyncInternal>d__46>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ForceAsyncAwaiter);
						num = (this.<>1__state = -1);
					}
					awaiter.GetResult();
					IL_89:
					try
					{
						TaskAwaiter awaiter2;
						if (num != 1)
						{
							awaiter2 = cryptoStream.WriteAsyncCore(this.buffer, this.offset, this.count, this.cancellationToken, true).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 1);
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, CryptoStream.<WriteAsyncInternal>d__46>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(TaskAwaiter);
							num = (this.<>1__state = -1);
						}
						awaiter2.GetResult();
					}
					finally
					{
						if (num < 0)
						{
							this.<semaphore>5__2.Release();
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<semaphore>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<semaphore>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06002DF9 RID: 11769 RVA: 0x000A5394 File Offset: 0x000A3594
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040020F7 RID: 8439
			public int <>1__state;

			// Token: 0x040020F8 RID: 8440
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040020F9 RID: 8441
			public CryptoStream <>4__this;

			// Token: 0x040020FA RID: 8442
			public byte[] buffer;

			// Token: 0x040020FB RID: 8443
			public int offset;

			// Token: 0x040020FC RID: 8444
			public int count;

			// Token: 0x040020FD RID: 8445
			public CancellationToken cancellationToken;

			// Token: 0x040020FE RID: 8446
			private SemaphoreSlim <semaphore>5__2;

			// Token: 0x040020FF RID: 8447
			private ForceAsyncAwaiter <>u__1;

			// Token: 0x04002100 RID: 8448
			private TaskAwaiter <>u__2;
		}

		// Token: 0x0200046D RID: 1133
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAsyncCore>d__49 : IAsyncStateMachine
		{
			// Token: 0x06002DFA RID: 11770 RVA: 0x000A53A4 File Offset: 0x000A35A4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				CryptoStream cryptoStream = this.<>4__this;
				try
				{
					ValueTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ValueTaskAwaiter);
						num = (this.<>1__state = -1);
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ValueTaskAwaiter);
						num = (this.<>1__state = -1);
						goto IL_266;
					case 2:
						IL_2FA:
						try
						{
							if (num != 2)
							{
								this.<numOutputBytes>5__4 = cryptoStream._transform.TransformBlock(this.buffer, this.<currentInputIndex>5__3, this.<numWholeBlocksInBytes>5__5, this.<tempOutputBuffer>5__6, 0);
								if (!this.useAsync)
								{
									cryptoStream._stream.Write(this.<tempOutputBuffer>5__6, 0, this.<numOutputBytes>5__4);
									goto IL_3C9;
								}
								awaiter = cryptoStream._stream.WriteAsync(new ReadOnlyMemory<byte>(this.<tempOutputBuffer>5__6, 0, this.<numOutputBytes>5__4), this.cancellationToken).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (this.<>1__state = 2);
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ValueTaskAwaiter, CryptoStream.<WriteAsyncCore>d__49>(ref awaiter, ref this);
									return;
								}
							}
							else
							{
								awaiter = this.<>u__1;
								this.<>u__1 = default(ValueTaskAwaiter);
								num = (this.<>1__state = -1);
							}
							awaiter.GetResult();
							IL_3C9:
							this.<currentInputIndex>5__3 += this.<numWholeBlocksInBytes>5__5;
							this.<bytesToWrite>5__2 -= this.<numWholeBlocksInBytes>5__5;
						}
						finally
						{
							if (num < 0)
							{
								CryptographicOperations.ZeroMemory(new Span<byte>(this.<tempOutputBuffer>5__6, 0, this.<numOutputBytes>5__4));
								ArrayPool<byte>.Shared.Return(this.<tempOutputBuffer>5__6, false);
								this.<tempOutputBuffer>5__6 = null;
							}
						}
						this.<tempOutputBuffer>5__6 = null;
						goto IL_553;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ValueTaskAwaiter);
						num = (this.<>1__state = -1);
						goto IL_4D7;
					default:
						this.<bytesToWrite>5__2 = this.count;
						this.<currentInputIndex>5__3 = this.offset;
						if (cryptoStream._inputBufferIndex > 0)
						{
							if (this.count < cryptoStream._inputBlockSize - cryptoStream._inputBufferIndex)
							{
								Buffer.BlockCopy(this.buffer, this.offset, cryptoStream._inputBuffer, cryptoStream._inputBufferIndex, this.count);
								cryptoStream._inputBufferIndex += this.count;
								goto IL_57A;
							}
							Buffer.BlockCopy(this.buffer, this.offset, cryptoStream._inputBuffer, cryptoStream._inputBufferIndex, cryptoStream._inputBlockSize - cryptoStream._inputBufferIndex);
							this.<currentInputIndex>5__3 += cryptoStream._inputBlockSize - cryptoStream._inputBufferIndex;
							this.<bytesToWrite>5__2 -= cryptoStream._inputBlockSize - cryptoStream._inputBufferIndex;
							cryptoStream._inputBufferIndex = cryptoStream._inputBlockSize;
						}
						if (cryptoStream._outputBufferIndex <= 0)
						{
							goto IL_1B4;
						}
						if (!this.useAsync)
						{
							cryptoStream._stream.Write(cryptoStream._outputBuffer, 0, cryptoStream._outputBufferIndex);
							goto IL_1AD;
						}
						awaiter = cryptoStream._stream.WriteAsync(new ReadOnlyMemory<byte>(cryptoStream._outputBuffer, 0, cryptoStream._outputBufferIndex), this.cancellationToken).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ValueTaskAwaiter, CryptoStream.<WriteAsyncCore>d__49>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					IL_1AD:
					cryptoStream._outputBufferIndex = 0;
					IL_1B4:
					if (cryptoStream._inputBufferIndex != cryptoStream._inputBlockSize)
					{
						goto IL_553;
					}
					this.<numOutputBytes>5__4 = cryptoStream._transform.TransformBlock(cryptoStream._inputBuffer, 0, cryptoStream._inputBlockSize, cryptoStream._outputBuffer, 0);
					if (!this.useAsync)
					{
						cryptoStream._stream.Write(cryptoStream._outputBuffer, 0, this.<numOutputBytes>5__4);
						goto IL_287;
					}
					awaiter = cryptoStream._stream.WriteAsync(new ReadOnlyMemory<byte>(cryptoStream._outputBuffer, 0, this.<numOutputBytes>5__4), this.cancellationToken).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (this.<>1__state = 1);
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ValueTaskAwaiter, CryptoStream.<WriteAsyncCore>d__49>(ref awaiter, ref this);
						return;
					}
					IL_266:
					awaiter.GetResult();
					IL_287:
					cryptoStream._inputBufferIndex = 0;
					goto IL_553;
					IL_4D7:
					awaiter.GetResult();
					IL_4F8:
					this.<currentInputIndex>5__3 += cryptoStream._inputBlockSize;
					this.<bytesToWrite>5__2 -= cryptoStream._inputBlockSize;
					IL_553:
					if (this.<bytesToWrite>5__2 > 0)
					{
						if (this.<bytesToWrite>5__2 >= cryptoStream._inputBlockSize)
						{
							int num2 = this.<bytesToWrite>5__2 / cryptoStream._inputBlockSize;
							if (cryptoStream._transform.CanTransformMultipleBlocks && num2 > 1)
							{
								this.<numWholeBlocksInBytes>5__5 = num2 * cryptoStream._inputBlockSize;
								this.<tempOutputBuffer>5__6 = ArrayPool<byte>.Shared.Rent(num2 * cryptoStream._outputBlockSize);
								this.<numOutputBytes>5__4 = 0;
								goto IL_2FA;
							}
							this.<numOutputBytes>5__4 = cryptoStream._transform.TransformBlock(this.buffer, this.<currentInputIndex>5__3, cryptoStream._inputBlockSize, cryptoStream._outputBuffer, 0);
							if (!this.useAsync)
							{
								cryptoStream._stream.Write(cryptoStream._outputBuffer, 0, this.<numOutputBytes>5__4);
								goto IL_4F8;
							}
							awaiter = cryptoStream._stream.WriteAsync(new ReadOnlyMemory<byte>(cryptoStream._outputBuffer, 0, this.<numOutputBytes>5__4), this.cancellationToken).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 3);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ValueTaskAwaiter, CryptoStream.<WriteAsyncCore>d__49>(ref awaiter, ref this);
								return;
							}
							goto IL_4D7;
						}
						else
						{
							Buffer.BlockCopy(this.buffer, this.<currentInputIndex>5__3, cryptoStream._inputBuffer, 0, this.<bytesToWrite>5__2);
							cryptoStream._inputBufferIndex += this.<bytesToWrite>5__2;
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_57A:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06002DFB RID: 11771 RVA: 0x000A5974 File Offset: 0x000A3B74
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04002101 RID: 8449
			public int <>1__state;

			// Token: 0x04002102 RID: 8450
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04002103 RID: 8451
			public int count;

			// Token: 0x04002104 RID: 8452
			public int offset;

			// Token: 0x04002105 RID: 8453
			public CryptoStream <>4__this;

			// Token: 0x04002106 RID: 8454
			public byte[] buffer;

			// Token: 0x04002107 RID: 8455
			public bool useAsync;

			// Token: 0x04002108 RID: 8456
			public CancellationToken cancellationToken;

			// Token: 0x04002109 RID: 8457
			private int <bytesToWrite>5__2;

			// Token: 0x0400210A RID: 8458
			private int <currentInputIndex>5__3;

			// Token: 0x0400210B RID: 8459
			private int <numOutputBytes>5__4;

			// Token: 0x0400210C RID: 8460
			private ValueTaskAwaiter <>u__1;

			// Token: 0x0400210D RID: 8461
			private int <numWholeBlocksInBytes>5__5;

			// Token: 0x0400210E RID: 8462
			private byte[] <tempOutputBuffer>5__6;
		}

		// Token: 0x0200046E RID: 1134
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002DFC RID: 11772 RVA: 0x000A5982 File Offset: 0x000A3B82
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002DFD RID: 11773 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x06002DFE RID: 11774 RVA: 0x000A598E File Offset: 0x000A3B8E
			internal SemaphoreSlim <get_AsyncActiveSemaphore>b__54_0()
			{
				return new SemaphoreSlim(1, 1);
			}

			// Token: 0x0400210F RID: 8463
			public static readonly CryptoStream.<>c <>9 = new CryptoStream.<>c();

			// Token: 0x04002110 RID: 8464
			public static Func<SemaphoreSlim> <>9__54_0;
		}
	}
}
