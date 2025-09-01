using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Implements a <see cref="T:System.IO.TextWriter" /> for writing characters to a stream in a particular encoding.</summary>
	// Token: 0x02000B18 RID: 2840
	[Serializable]
	public class StreamWriter : TextWriter
	{
		// Token: 0x0600657A RID: 25978 RVA: 0x0015A566 File Offset: 0x00158766
		private void CheckAsyncTaskInProgress()
		{
			if (!this._asyncWriteTask.IsCompleted)
			{
				StreamWriter.ThrowAsyncIOInProgress();
			}
		}

		// Token: 0x0600657B RID: 25979 RVA: 0x0015865B File Offset: 0x0015685B
		private static void ThrowAsyncIOInProgress()
		{
			throw new InvalidOperationException("The stream is currently in use by a previous operation on the stream.");
		}

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x0600657C RID: 25980 RVA: 0x0015A57A File Offset: 0x0015877A
		private static Encoding UTF8NoBOM
		{
			get
			{
				return EncodingHelper.UTF8Unmarked;
			}
		}

		// Token: 0x0600657D RID: 25981 RVA: 0x0015A581 File Offset: 0x00158781
		internal StreamWriter() : base(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified stream by using UTF-8 encoding and the default buffer size.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is not writable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		// Token: 0x0600657E RID: 25982 RVA: 0x0015A595 File Offset: 0x00158795
		public StreamWriter(Stream stream) : this(stream, StreamWriter.UTF8NoBOM, 1024, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified stream by using the specified encoding and the default buffer size.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is not writable.</exception>
		// Token: 0x0600657F RID: 25983 RVA: 0x0015A5A9 File Offset: 0x001587A9
		public StreamWriter(Stream stream, Encoding encoding) : this(stream, encoding, 1024, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified stream by using the specified encoding and buffer size.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="bufferSize">The buffer size, in bytes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is not writable.</exception>
		// Token: 0x06006580 RID: 25984 RVA: 0x0015A5B9 File Offset: 0x001587B9
		public StreamWriter(Stream stream, Encoding encoding, int bufferSize) : this(stream, encoding, bufferSize, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified stream by using the specified encoding and buffer size, and optionally leaves the stream open.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="bufferSize">The buffer size, in bytes.</param>
		/// <param name="leaveOpen">
		///   <see langword="true" /> to leave the stream open after the <see cref="T:System.IO.StreamWriter" /> object is disposed; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is not writable.</exception>
		// Token: 0x06006581 RID: 25985 RVA: 0x0015A5C8 File Offset: 0x001587C8
		public StreamWriter(Stream stream, Encoding encoding, int bufferSize, bool leaveOpen) : base(null)
		{
			if (stream == null || encoding == null)
			{
				throw new ArgumentNullException((stream == null) ? "stream" : "encoding");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException("Stream was not writable.");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
			}
			this.Init(stream, encoding, bufferSize, leaveOpen);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified file by using the default encoding and buffer size.</summary>
		/// <param name="path">The complete file path to write to. <paramref name="path" /> can be a file name.</param>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="path" /> contains the name of a system device (com1, com2, and so on).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label syntax.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06006582 RID: 25986 RVA: 0x0015A634 File Offset: 0x00158834
		public StreamWriter(string path) : this(path, false, StreamWriter.UTF8NoBOM, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified file by using the default encoding and buffer size. If the file exists, it can be either overwritten or appended to. If the file does not exist, this constructor creates a new file.</summary>
		/// <param name="path">The complete file path to write to.</param>
		/// <param name="append">
		///   <see langword="true" /> to append data to the file; <see langword="false" /> to overwrite the file. If the specified file does not exist, this parameter has no effect, and the constructor creates a new file.</param>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is empty.  
		/// -or-  
		/// <paramref name="path" /> contains the name of a system device (com1, com2, and so on).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label syntax.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06006583 RID: 25987 RVA: 0x0015A648 File Offset: 0x00158848
		public StreamWriter(string path, bool append) : this(path, append, StreamWriter.UTF8NoBOM, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified file by using the specified encoding and default buffer size. If the file exists, it can be either overwritten or appended to. If the file does not exist, this constructor creates a new file.</summary>
		/// <param name="path">The complete file path to write to.</param>
		/// <param name="append">
		///   <see langword="true" /> to append data to the file; <see langword="false" /> to overwrite the file. If the specified file does not exist, this parameter has no effect, and the constructor creates a new file.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is empty.  
		/// -or-  
		/// <paramref name="path" /> contains the name of a system device (com1, com2, and so on).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label syntax.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06006584 RID: 25988 RVA: 0x0015A65C File Offset: 0x0015885C
		public StreamWriter(string path, bool append, Encoding encoding) : this(path, append, encoding, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified file on the specified path, using the specified encoding and buffer size. If the file exists, it can be either overwritten or appended to. If the file does not exist, this constructor creates a new file.</summary>
		/// <param name="path">The complete file path to write to.</param>
		/// <param name="append">
		///   <see langword="true" /> to append data to the file; <see langword="false" /> to overwrite the file. If the specified file does not exist, this parameter has no effect, and the constructor creates a new file.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="bufferSize">The buffer size, in bytes.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="path" /> contains the name of a system device (com1, com2, and so on).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="encoding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label syntax.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		// Token: 0x06006585 RID: 25989 RVA: 0x0015A66C File Offset: 0x0015886C
		public StreamWriter(string path, bool append, Encoding encoding, int bufferSize)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
			}
			Stream streamArg = new FileStream(path, append ? FileMode.Append : FileMode.Create, FileAccess.Write, FileShare.Read, 4096, FileOptions.SequentialScan);
			this.Init(streamArg, encoding, bufferSize, false);
		}

		// Token: 0x06006586 RID: 25990 RVA: 0x0015A6F4 File Offset: 0x001588F4
		private void Init(Stream streamArg, Encoding encodingArg, int bufferSize, bool shouldLeaveOpen)
		{
			this._stream = streamArg;
			this._encoding = encodingArg;
			this._encoder = this._encoding.GetEncoder();
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this._charBuffer = new char[bufferSize];
			this._byteBuffer = new byte[this._encoding.GetMaxByteCount(bufferSize)];
			this._charLen = bufferSize;
			if (this._stream.CanSeek && this._stream.Position > 0L)
			{
				this._haveWrittenPreamble = true;
			}
			this._closable = !shouldLeaveOpen;
		}

		/// <summary>Closes the current <see langword="StreamWriter" /> object and the underlying stream.</summary>
		/// <exception cref="T:System.Text.EncoderFallbackException">The current encoding does not support displaying half of a Unicode surrogate pair.</exception>
		// Token: 0x06006587 RID: 25991 RVA: 0x0015A787 File Offset: 0x00158987
		public override void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.StreamWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		/// <exception cref="T:System.Text.EncoderFallbackException">The current encoding does not support displaying half of a Unicode surrogate pair.</exception>
		// Token: 0x06006588 RID: 25992 RVA: 0x0015A798 File Offset: 0x00158998
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this._stream != null && disposing)
				{
					this.CheckAsyncTaskInProgress();
					this.Flush(true, true);
				}
			}
			finally
			{
				if (!this.LeaveOpen && this._stream != null)
				{
					try
					{
						if (disposing)
						{
							this._stream.Close();
						}
					}
					finally
					{
						this._stream = null;
						this._byteBuffer = null;
						this._charBuffer = null;
						this._encoding = null;
						this._encoder = null;
						this._charLen = 0;
						base.Dispose(disposing);
					}
				}
			}
		}

		// Token: 0x06006589 RID: 25993 RVA: 0x0015A830 File Offset: 0x00158A30
		public override ValueTask DisposeAsync()
		{
			if (!(base.GetType() != typeof(StreamWriter)))
			{
				return this.DisposeAsyncCore();
			}
			return base.DisposeAsync();
		}

		// Token: 0x0600658A RID: 25994 RVA: 0x0015A858 File Offset: 0x00158A58
		private ValueTask DisposeAsyncCore()
		{
			StreamWriter.<DisposeAsyncCore>d__33 <DisposeAsyncCore>d__;
			<DisposeAsyncCore>d__.<>4__this = this;
			<DisposeAsyncCore>d__.<>t__builder = AsyncValueTaskMethodBuilder.Create();
			<DisposeAsyncCore>d__.<>1__state = -1;
			<DisposeAsyncCore>d__.<>t__builder.Start<StreamWriter.<DisposeAsyncCore>d__33>(ref <DisposeAsyncCore>d__);
			return <DisposeAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x0600658B RID: 25995 RVA: 0x0015A89C File Offset: 0x00158A9C
		private void CloseStreamFromDispose(bool disposing)
		{
			if (!this.LeaveOpen && this._stream != null)
			{
				try
				{
					if (disposing)
					{
						this._stream.Close();
					}
				}
				finally
				{
					this._stream = null;
					this._byteBuffer = null;
					this._charBuffer = null;
					this._encoding = null;
					this._encoder = null;
					this._charLen = 0;
					base.Dispose(disposing);
				}
			}
		}

		/// <summary>Clears all buffers for the current writer and causes any buffered data to be written to the underlying stream.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current writer is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">The current encoding does not support displaying half of a Unicode surrogate pair.</exception>
		// Token: 0x0600658C RID: 25996 RVA: 0x0015A90C File Offset: 0x00158B0C
		public override void Flush()
		{
			this.CheckAsyncTaskInProgress();
			this.Flush(true, true);
		}

		// Token: 0x0600658D RID: 25997 RVA: 0x0015A91C File Offset: 0x00158B1C
		private void Flush(bool flushStream, bool flushEncoder)
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			if (this._charPos == 0 && !flushStream && !flushEncoder)
			{
				return;
			}
			if (!this._haveWrittenPreamble)
			{
				this._haveWrittenPreamble = true;
				ReadOnlySpan<byte> preamble = this._encoding.Preamble;
				if (preamble.Length > 0)
				{
					this._stream.Write(preamble);
				}
			}
			int bytes = this._encoder.GetBytes(this._charBuffer, 0, this._charPos, this._byteBuffer, 0, flushEncoder);
			this._charPos = 0;
			if (bytes > 0)
			{
				this._stream.Write(this._byteBuffer, 0, bytes);
			}
			if (flushStream)
			{
				this._stream.Flush();
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.IO.StreamWriter" /> will flush its buffer to the underlying stream after every call to <see cref="M:System.IO.StreamWriter.Write(System.Char)" />.</summary>
		/// <returns>
		///   <see langword="true" /> to force <see cref="T:System.IO.StreamWriter" /> to flush its buffer; otherwise, <see langword="false" />.</returns>
		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x0600658E RID: 25998 RVA: 0x0015A9CA File Offset: 0x00158BCA
		// (set) Token: 0x0600658F RID: 25999 RVA: 0x0015A9D2 File Offset: 0x00158BD2
		public virtual bool AutoFlush
		{
			get
			{
				return this._autoFlush;
			}
			set
			{
				this.CheckAsyncTaskInProgress();
				this._autoFlush = value;
				if (value)
				{
					this.Flush(true, false);
				}
			}
		}

		/// <summary>Gets the underlying stream that interfaces with a backing store.</summary>
		/// <returns>The stream this <see langword="StreamWriter" /> is writing to.</returns>
		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x06006590 RID: 26000 RVA: 0x0015A9EC File Offset: 0x00158BEC
		public virtual Stream BaseStream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x06006591 RID: 26001 RVA: 0x0015A9F4 File Offset: 0x00158BF4
		internal bool LeaveOpen
		{
			get
			{
				return !this._closable;
			}
		}

		// Token: 0x170011C3 RID: 4547
		// (set) Token: 0x06006592 RID: 26002 RVA: 0x0015A9FF File Offset: 0x00158BFF
		internal bool HaveWrittenPreamble
		{
			set
			{
				this._haveWrittenPreamble = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Text.Encoding" /> in which the output is written.</summary>
		/// <returns>The <see cref="T:System.Text.Encoding" /> specified in the constructor for the current instance, or <see cref="T:System.Text.UTF8Encoding" /> if an encoding was not specified.</returns>
		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x06006593 RID: 26003 RVA: 0x0015AA08 File Offset: 0x00158C08
		public override Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
		}

		/// <summary>Writes a character to the stream.</summary>
		/// <param name="value">The character to write to the stream.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and current writer is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and the contents of the buffer cannot be written to the underlying fixed size stream because the <see cref="T:System.IO.StreamWriter" /> is at the end the stream.</exception>
		// Token: 0x06006594 RID: 26004 RVA: 0x0015AA10 File Offset: 0x00158C10
		public override void Write(char value)
		{
			this.CheckAsyncTaskInProgress();
			if (this._charPos == this._charLen)
			{
				this.Flush(false, false);
			}
			this._charBuffer[this._charPos] = value;
			this._charPos++;
			if (this._autoFlush)
			{
				this.Flush(true, false);
			}
		}

		/// <summary>Writes a character array to the stream.</summary>
		/// <param name="buffer">A character array containing the data to write. If <paramref name="buffer" /> is <see langword="null" />, nothing is written.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and current writer is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and the contents of the buffer cannot be written to the underlying fixed size stream because the <see cref="T:System.IO.StreamWriter" /> is at the end the stream.</exception>
		// Token: 0x06006595 RID: 26005 RVA: 0x0015AA65 File Offset: 0x00158C65
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void Write(char[] buffer)
		{
			this.WriteSpan(buffer, false);
		}

		/// <summary>Writes a subarray of characters to the stream.</summary>
		/// <param name="buffer">A character array that contains the data to write.</param>
		/// <param name="index">The character position in the buffer at which to start reading data.</param>
		/// <param name="count">The maximum number of characters to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and current writer is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and the contents of the buffer cannot be written to the underlying fixed size stream because the <see cref="T:System.IO.StreamWriter" /> is at the end the stream.</exception>
		// Token: 0x06006596 RID: 26006 RVA: 0x0015AA74 File Offset: 0x00158C74
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void Write(char[] buffer, int index, int count)
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
			this.WriteSpan(buffer.AsSpan(index, count), false);
		}

		// Token: 0x06006597 RID: 26007 RVA: 0x0015AAE3 File Offset: 0x00158CE3
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void Write(ReadOnlySpan<char> buffer)
		{
			if (base.GetType() == typeof(StreamWriter))
			{
				this.WriteSpan(buffer, false);
				return;
			}
			base.Write(buffer);
		}

		// Token: 0x06006598 RID: 26008 RVA: 0x0015AB0C File Offset: 0x00158D0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void WriteSpan(ReadOnlySpan<char> buffer, bool appendNewLine)
		{
			this.CheckAsyncTaskInProgress();
			if (buffer.Length <= 4 && buffer.Length <= this._charLen - this._charPos)
			{
				for (int i = 0; i < buffer.Length; i++)
				{
					char[] charBuffer = this._charBuffer;
					int charPos = this._charPos;
					this._charPos = charPos + 1;
					charBuffer[charPos] = *buffer[i];
				}
			}
			else
			{
				char[] charBuffer2 = this._charBuffer;
				if (charBuffer2 == null)
				{
					throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
				}
				fixed (char* reference = MemoryMarshal.GetReference<char>(buffer))
				{
					char* ptr = reference;
					fixed (char* ptr2 = &charBuffer2[0])
					{
						char* ptr3 = ptr2;
						char* ptr4 = ptr;
						int j = buffer.Length;
						int num = this._charPos;
						while (j > 0)
						{
							if (num == charBuffer2.Length)
							{
								this.Flush(false, false);
								num = 0;
							}
							int num2 = Math.Min(charBuffer2.Length - num, j);
							int num3 = num2 * 2;
							Buffer.MemoryCopy((void*)ptr4, (void*)(ptr3 + num), (long)num3, (long)num3);
							this._charPos += num2;
							num += num2;
							ptr4 += num2;
							j -= num2;
						}
					}
				}
			}
			if (appendNewLine)
			{
				char[] coreNewLine = this.CoreNewLine;
				for (int k = 0; k < coreNewLine.Length; k++)
				{
					if (this._charPos == this._charLen)
					{
						this.Flush(false, false);
					}
					this._charBuffer[this._charPos] = coreNewLine[k];
					this._charPos++;
				}
			}
			if (this._autoFlush)
			{
				this.Flush(true, false);
			}
		}

		/// <summary>Writes a string to the stream.</summary>
		/// <param name="value">The string to write to the stream. If <paramref name="value" /> is null, nothing is written.</param>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and current writer is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and the contents of the buffer cannot be written to the underlying fixed size stream because the <see cref="T:System.IO.StreamWriter" /> is at the end the stream.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06006599 RID: 26009 RVA: 0x0015AC8C File Offset: 0x00158E8C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void Write(string value)
		{
			this.WriteSpan(value, false);
		}

		// Token: 0x0600659A RID: 26010 RVA: 0x0015AC9B File Offset: 0x00158E9B
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void WriteLine(string value)
		{
			this.CheckAsyncTaskInProgress();
			this.WriteSpan(value, true);
		}

		// Token: 0x0600659B RID: 26011 RVA: 0x0015ACB0 File Offset: 0x00158EB0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void WriteLine(ReadOnlySpan<char> value)
		{
			if (base.GetType() == typeof(StreamWriter))
			{
				this.CheckAsyncTaskInProgress();
				this.WriteSpan(value, true);
				return;
			}
			base.WriteLine(value);
		}

		/// <summary>Writes a character to the stream asynchronously.</summary>
		/// <param name="value">The character to write to the stream.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream writer is currently in use by a previous write operation.</exception>
		// Token: 0x0600659C RID: 26012 RVA: 0x0015ACE0 File Offset: 0x00158EE0
		public override Task WriteAsync(char value)
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(value);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, value, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, false);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x0600659D RID: 26013 RVA: 0x0015AD58 File Offset: 0x00158F58
		private static Task WriteAsyncInternal(StreamWriter _this, char value, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
		{
			StreamWriter.<WriteAsyncInternal>d__57 <WriteAsyncInternal>d__;
			<WriteAsyncInternal>d__._this = _this;
			<WriteAsyncInternal>d__.value = value;
			<WriteAsyncInternal>d__.charBuffer = charBuffer;
			<WriteAsyncInternal>d__.charPos = charPos;
			<WriteAsyncInternal>d__.charLen = charLen;
			<WriteAsyncInternal>d__.coreNewLine = coreNewLine;
			<WriteAsyncInternal>d__.autoFlush = autoFlush;
			<WriteAsyncInternal>d__.appendNewLine = appendNewLine;
			<WriteAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAsyncInternal>d__.<>1__state = -1;
			<WriteAsyncInternal>d__.<>t__builder.Start<StreamWriter.<WriteAsyncInternal>d__57>(ref <WriteAsyncInternal>d__);
			return <WriteAsyncInternal>d__.<>t__builder.Task;
		}

		/// <summary>Writes a string to the stream asynchronously.</summary>
		/// <param name="value">The string to write to the stream. If <paramref name="value" /> is <see langword="null" />, nothing is written.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream writer is currently in use by a previous write operation.</exception>
		// Token: 0x0600659E RID: 26014 RVA: 0x0015ADD8 File Offset: 0x00158FD8
		public override Task WriteAsync(string value)
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(value);
			}
			if (value == null)
			{
				return Task.CompletedTask;
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, value, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, false);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x0600659F RID: 26015 RVA: 0x0015AE58 File Offset: 0x00159058
		private static Task WriteAsyncInternal(StreamWriter _this, string value, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
		{
			StreamWriter.<WriteAsyncInternal>d__59 <WriteAsyncInternal>d__;
			<WriteAsyncInternal>d__._this = _this;
			<WriteAsyncInternal>d__.value = value;
			<WriteAsyncInternal>d__.charBuffer = charBuffer;
			<WriteAsyncInternal>d__.charPos = charPos;
			<WriteAsyncInternal>d__.charLen = charLen;
			<WriteAsyncInternal>d__.coreNewLine = coreNewLine;
			<WriteAsyncInternal>d__.autoFlush = autoFlush;
			<WriteAsyncInternal>d__.appendNewLine = appendNewLine;
			<WriteAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAsyncInternal>d__.<>1__state = -1;
			<WriteAsyncInternal>d__.<>t__builder.Start<StreamWriter.<WriteAsyncInternal>d__59>(ref <WriteAsyncInternal>d__);
			return <WriteAsyncInternal>d__.<>t__builder.Task;
		}

		/// <summary>Writes a subarray of characters to the stream asynchronously.</summary>
		/// <param name="buffer">A character array that contains the data to write.</param>
		/// <param name="index">The character position in the buffer at which to begin reading data.</param>
		/// <param name="count">The maximum number of characters to write.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> plus <paramref name="count" /> is greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream writer is currently in use by a previous write operation.</exception>
		// Token: 0x060065A0 RID: 26016 RVA: 0x0015AED8 File Offset: 0x001590D8
		public override Task WriteAsync(char[] buffer, int index, int count)
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
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(buffer, index, count);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, new ReadOnlyMemory<char>(buffer, index, count), this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, false, default(CancellationToken));
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x060065A1 RID: 26017 RVA: 0x0015AFB0 File Offset: 0x001591B0
		public override Task WriteAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(buffer, cancellationToken);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task task = StreamWriter.WriteAsyncInternal(this, buffer, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, false, cancellationToken);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x060065A2 RID: 26018 RVA: 0x0015B038 File Offset: 0x00159238
		private static Task WriteAsyncInternal(StreamWriter _this, ReadOnlyMemory<char> source, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine, CancellationToken cancellationToken)
		{
			StreamWriter.<WriteAsyncInternal>d__62 <WriteAsyncInternal>d__;
			<WriteAsyncInternal>d__._this = _this;
			<WriteAsyncInternal>d__.source = source;
			<WriteAsyncInternal>d__.charBuffer = charBuffer;
			<WriteAsyncInternal>d__.charPos = charPos;
			<WriteAsyncInternal>d__.charLen = charLen;
			<WriteAsyncInternal>d__.coreNewLine = coreNewLine;
			<WriteAsyncInternal>d__.autoFlush = autoFlush;
			<WriteAsyncInternal>d__.appendNewLine = appendNewLine;
			<WriteAsyncInternal>d__.cancellationToken = cancellationToken;
			<WriteAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAsyncInternal>d__.<>1__state = -1;
			<WriteAsyncInternal>d__.<>t__builder.Start<StreamWriter.<WriteAsyncInternal>d__62>(ref <WriteAsyncInternal>d__);
			return <WriteAsyncInternal>d__.<>t__builder.Task;
		}

		/// <summary>Writes a line terminator asynchronously to the stream.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream writer is currently in use by a previous write operation.</exception>
		// Token: 0x060065A3 RID: 26019 RVA: 0x0015B0C0 File Offset: 0x001592C0
		public override Task WriteLineAsync()
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync();
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, ReadOnlyMemory<char>.Empty, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, true, default(CancellationToken));
			this._asyncWriteTask = task;
			return task;
		}

		/// <summary>Writes a character followed by a line terminator asynchronously to the stream.</summary>
		/// <param name="value">The character to write to the stream.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream writer is currently in use by a previous write operation.</exception>
		// Token: 0x060065A4 RID: 26020 RVA: 0x0015B144 File Offset: 0x00159344
		public override Task WriteLineAsync(char value)
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync(value);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, value, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, true);
			this._asyncWriteTask = task;
			return task;
		}

		/// <summary>Writes a string followed by a line terminator asynchronously to the stream.</summary>
		/// <param name="value">The string to write. If the value is <see langword="null" />, only a line terminator is written.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream writer is currently in use by a previous write operation.</exception>
		// Token: 0x060065A5 RID: 26021 RVA: 0x0015B1BC File Offset: 0x001593BC
		public override Task WriteLineAsync(string value)
		{
			if (value == null)
			{
				return this.WriteLineAsync();
			}
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync(value);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, value, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, true);
			this._asyncWriteTask = task;
			return task;
		}

		/// <summary>Writes a subarray of characters followed by a line terminator asynchronously to the stream.</summary>
		/// <param name="buffer">The character array to write data from.</param>
		/// <param name="index">The character position in the buffer at which to start reading data.</param>
		/// <param name="count">The maximum number of characters to write.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> plus <paramref name="count" /> is greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream writer is currently in use by a previous write operation.</exception>
		// Token: 0x060065A6 RID: 26022 RVA: 0x0015B23C File Offset: 0x0015943C
		public override Task WriteLineAsync(char[] buffer, int index, int count)
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
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync(buffer, index, count);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, new ReadOnlyMemory<char>(buffer, index, count), this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, true, default(CancellationToken));
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x060065A7 RID: 26023 RVA: 0x0015B314 File Offset: 0x00159514
		public override Task WriteLineAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync(buffer, cancellationToken);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task task = StreamWriter.WriteAsyncInternal(this, buffer, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, true, cancellationToken);
			this._asyncWriteTask = task;
			return task;
		}

		/// <summary>Clears all buffers for this stream asynchronously and causes any buffered data to be written to the underlying device.</summary>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x060065A8 RID: 26024 RVA: 0x0015B39C File Offset: 0x0015959C
		public override Task FlushAsync()
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.FlushAsync();
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = this.FlushAsyncInternal(true, true, this._charBuffer, this._charPos, default(CancellationToken));
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x170011C5 RID: 4549
		// (set) Token: 0x060065A9 RID: 26025 RVA: 0x0015B407 File Offset: 0x00159607
		private int CharPos_Prop
		{
			set
			{
				this._charPos = value;
			}
		}

		// Token: 0x170011C6 RID: 4550
		// (set) Token: 0x060065AA RID: 26026 RVA: 0x0015A9FF File Offset: 0x00158BFF
		private bool HaveWrittenPreamble_Prop
		{
			set
			{
				this._haveWrittenPreamble = value;
			}
		}

		// Token: 0x060065AB RID: 26027 RVA: 0x0015B410 File Offset: 0x00159610
		private Task FlushAsyncInternal(bool flushStream, bool flushEncoder, char[] sCharBuffer, int sCharPos, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			if (sCharPos == 0 && !flushStream && !flushEncoder)
			{
				return Task.CompletedTask;
			}
			Task result = StreamWriter.FlushAsyncInternal(this, flushStream, flushEncoder, sCharBuffer, sCharPos, this._haveWrittenPreamble, this._encoding, this._encoder, this._byteBuffer, this._stream, cancellationToken);
			this._charPos = 0;
			return result;
		}

		// Token: 0x060065AC RID: 26028 RVA: 0x0015B470 File Offset: 0x00159670
		private static Task FlushAsyncInternal(StreamWriter _this, bool flushStream, bool flushEncoder, char[] charBuffer, int charPos, bool haveWrittenPreamble, Encoding encoding, Encoder encoder, byte[] byteBuffer, Stream stream, CancellationToken cancellationToken)
		{
			StreamWriter.<FlushAsyncInternal>d__74 <FlushAsyncInternal>d__;
			<FlushAsyncInternal>d__._this = _this;
			<FlushAsyncInternal>d__.flushStream = flushStream;
			<FlushAsyncInternal>d__.flushEncoder = flushEncoder;
			<FlushAsyncInternal>d__.charBuffer = charBuffer;
			<FlushAsyncInternal>d__.charPos = charPos;
			<FlushAsyncInternal>d__.haveWrittenPreamble = haveWrittenPreamble;
			<FlushAsyncInternal>d__.encoding = encoding;
			<FlushAsyncInternal>d__.encoder = encoder;
			<FlushAsyncInternal>d__.byteBuffer = byteBuffer;
			<FlushAsyncInternal>d__.stream = stream;
			<FlushAsyncInternal>d__.cancellationToken = cancellationToken;
			<FlushAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FlushAsyncInternal>d__.<>1__state = -1;
			<FlushAsyncInternal>d__.<>t__builder.Start<StreamWriter.<FlushAsyncInternal>d__74>(ref <FlushAsyncInternal>d__);
			return <FlushAsyncInternal>d__.<>t__builder.Task;
		}

		// Token: 0x060065AD RID: 26029 RVA: 0x0015B50A File Offset: 0x0015970A
		// Note: this type is marked as 'beforefieldinit'.
		static StreamWriter()
		{
		}

		// Token: 0x04003B9E RID: 15262
		internal const int DefaultBufferSize = 1024;

		// Token: 0x04003B9F RID: 15263
		private const int DefaultFileStreamBufferSize = 4096;

		// Token: 0x04003BA0 RID: 15264
		private const int MinBufferSize = 128;

		// Token: 0x04003BA1 RID: 15265
		private const int DontCopyOnWriteLineThreshold = 512;

		/// <summary>Provides a <see langword="StreamWriter" /> with no backing store that can be written to, but not read from.</summary>
		// Token: 0x04003BA2 RID: 15266
		public new static readonly StreamWriter Null = new StreamWriter(Stream.Null, StreamWriter.UTF8NoBOM, 128, true);

		// Token: 0x04003BA3 RID: 15267
		private Stream _stream;

		// Token: 0x04003BA4 RID: 15268
		private Encoding _encoding;

		// Token: 0x04003BA5 RID: 15269
		private Encoder _encoder;

		// Token: 0x04003BA6 RID: 15270
		private byte[] _byteBuffer;

		// Token: 0x04003BA7 RID: 15271
		private char[] _charBuffer;

		// Token: 0x04003BA8 RID: 15272
		private int _charPos;

		// Token: 0x04003BA9 RID: 15273
		private int _charLen;

		// Token: 0x04003BAA RID: 15274
		private bool _autoFlush;

		// Token: 0x04003BAB RID: 15275
		private bool _haveWrittenPreamble;

		// Token: 0x04003BAC RID: 15276
		private bool _closable;

		// Token: 0x04003BAD RID: 15277
		private Task _asyncWriteTask = Task.CompletedTask;

		// Token: 0x02000B19 RID: 2841
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <DisposeAsyncCore>d__33 : IAsyncStateMachine
		{
			// Token: 0x060065AE RID: 26030 RVA: 0x0015B528 File Offset: 0x00159728
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				StreamWriter streamWriter = this.<>4__this;
				try
				{
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							if (streamWriter._stream == null)
							{
								goto IL_7D;
							}
							awaiter = streamWriter.FlushAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<DisposeAsyncCore>d__33>(ref awaiter, ref this);
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
						IL_7D:;
					}
					finally
					{
						if (num < 0)
						{
							streamWriter.CloseStreamFromDispose(true);
						}
					}
					GC.SuppressFinalize(streamWriter);
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

			// Token: 0x060065AF RID: 26031 RVA: 0x0015B610 File Offset: 0x00159810
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003BAE RID: 15278
			public int <>1__state;

			// Token: 0x04003BAF RID: 15279
			public AsyncValueTaskMethodBuilder <>t__builder;

			// Token: 0x04003BB0 RID: 15280
			public StreamWriter <>4__this;

			// Token: 0x04003BB1 RID: 15281
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000B1A RID: 2842
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAsyncInternal>d__57 : IAsyncStateMachine
		{
			// Token: 0x060065B0 RID: 26032 RVA: 0x0015B620 File Offset: 0x00159820
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_177;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_257;
					default:
						if (this.charPos != this.charLen)
						{
							goto IL_B1;
						}
						awaiter = this._this.FlushAsyncInternal(false, false, this.charBuffer, this.charPos, default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__57>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					this.charPos = 0;
					IL_B1:
					this.charBuffer[this.charPos] = this.value;
					int num2 = this.charPos;
					this.charPos = num2 + 1;
					if (this.appendNewLine)
					{
						this.<i>5__2 = 0;
						goto IL_1C3;
					}
					goto IL_1D6;
					IL_177:
					awaiter.GetResult();
					this.charPos = 0;
					IL_185:
					this.charBuffer[this.charPos] = this.coreNewLine[this.<i>5__2];
					num2 = this.charPos;
					this.charPos = num2 + 1;
					num2 = this.<i>5__2;
					this.<i>5__2 = num2 + 1;
					IL_1C3:
					if (this.<i>5__2 < this.coreNewLine.Length)
					{
						if (this.charPos != this.charLen)
						{
							goto IL_185;
						}
						awaiter = this._this.FlushAsyncInternal(false, false, this.charBuffer, this.charPos, default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__57>(ref awaiter, ref this);
							return;
						}
						goto IL_177;
					}
					IL_1D6:
					if (!this.autoFlush)
					{
						goto IL_265;
					}
					awaiter = this._this.FlushAsyncInternal(true, false, this.charBuffer, this.charPos, default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__57>(ref awaiter, ref this);
						return;
					}
					IL_257:
					awaiter.GetResult();
					this.charPos = 0;
					IL_265:
					this._this.CharPos_Prop = this.charPos;
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

			// Token: 0x060065B1 RID: 26033 RVA: 0x0015B8F0 File Offset: 0x00159AF0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003BB2 RID: 15282
			public int <>1__state;

			// Token: 0x04003BB3 RID: 15283
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04003BB4 RID: 15284
			public int charPos;

			// Token: 0x04003BB5 RID: 15285
			public int charLen;

			// Token: 0x04003BB6 RID: 15286
			public StreamWriter _this;

			// Token: 0x04003BB7 RID: 15287
			public char[] charBuffer;

			// Token: 0x04003BB8 RID: 15288
			public char value;

			// Token: 0x04003BB9 RID: 15289
			public bool appendNewLine;

			// Token: 0x04003BBA RID: 15290
			public char[] coreNewLine;

			// Token: 0x04003BBB RID: 15291
			public bool autoFlush;

			// Token: 0x04003BBC RID: 15292
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04003BBD RID: 15293
			private int <i>5__2;
		}

		// Token: 0x02000B1B RID: 2843
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAsyncInternal>d__59 : IAsyncStateMachine
		{
			// Token: 0x060065B2 RID: 26034 RVA: 0x0015B900 File Offset: 0x00159B00
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1E3;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2C4;
					default:
						this.<count>5__2 = this.value.Length;
						this.<index>5__3 = 0;
						goto IL_135;
					}
					IL_C1:
					awaiter.GetResult();
					this.charPos = 0;
					IL_CF:
					int num2 = this.charLen - this.charPos;
					if (num2 > this.<count>5__2)
					{
						num2 = this.<count>5__2;
					}
					this.value.CopyTo(this.<index>5__3, this.charBuffer, this.charPos, num2);
					this.charPos += num2;
					this.<index>5__3 += num2;
					this.<count>5__2 -= num2;
					IL_135:
					if (this.<count>5__2 <= 0)
					{
						if (this.appendNewLine)
						{
							this.<i>5__4 = 0;
							goto IL_22F;
						}
						goto IL_242;
					}
					else
					{
						if (this.charPos != this.charLen)
						{
							goto IL_CF;
						}
						awaiter = this._this.FlushAsyncInternal(false, false, this.charBuffer, this.charPos, default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__59>(ref awaiter, ref this);
							return;
						}
						goto IL_C1;
					}
					IL_1E3:
					awaiter.GetResult();
					this.charPos = 0;
					IL_1F1:
					this.charBuffer[this.charPos] = this.coreNewLine[this.<i>5__4];
					int num3 = this.charPos;
					this.charPos = num3 + 1;
					num3 = this.<i>5__4;
					this.<i>5__4 = num3 + 1;
					IL_22F:
					if (this.<i>5__4 < this.coreNewLine.Length)
					{
						if (this.charPos != this.charLen)
						{
							goto IL_1F1;
						}
						awaiter = this._this.FlushAsyncInternal(false, false, this.charBuffer, this.charPos, default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__59>(ref awaiter, ref this);
							return;
						}
						goto IL_1E3;
					}
					IL_242:
					if (!this.autoFlush)
					{
						goto IL_2D2;
					}
					awaiter = this._this.FlushAsyncInternal(true, false, this.charBuffer, this.charPos, default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__59>(ref awaiter, ref this);
						return;
					}
					IL_2C4:
					awaiter.GetResult();
					this.charPos = 0;
					IL_2D2:
					this._this.CharPos_Prop = this.charPos;
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

			// Token: 0x060065B3 RID: 26035 RVA: 0x0015BC3C File Offset: 0x00159E3C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003BBE RID: 15294
			public int <>1__state;

			// Token: 0x04003BBF RID: 15295
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04003BC0 RID: 15296
			public string value;

			// Token: 0x04003BC1 RID: 15297
			public int charPos;

			// Token: 0x04003BC2 RID: 15298
			public int charLen;

			// Token: 0x04003BC3 RID: 15299
			public StreamWriter _this;

			// Token: 0x04003BC4 RID: 15300
			public char[] charBuffer;

			// Token: 0x04003BC5 RID: 15301
			public bool appendNewLine;

			// Token: 0x04003BC6 RID: 15302
			public char[] coreNewLine;

			// Token: 0x04003BC7 RID: 15303
			public bool autoFlush;

			// Token: 0x04003BC8 RID: 15304
			private int <count>5__2;

			// Token: 0x04003BC9 RID: 15305
			private int <index>5__3;

			// Token: 0x04003BCA RID: 15306
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04003BCB RID: 15307
			private int <i>5__4;
		}

		// Token: 0x02000B1C RID: 2844
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAsyncInternal>d__62 : IAsyncStateMachine
		{
			// Token: 0x060065B4 RID: 26036 RVA: 0x0015BC4C File Offset: 0x00159E4C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1E5;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2C2;
					default:
						this.<copied>5__2 = 0;
						goto IL_131;
					}
					IL_AC:
					awaiter.GetResult();
					this.charPos = 0;
					IL_BA:
					int num2 = Math.Min(this.charLen - this.charPos, this.source.Length - this.<copied>5__2);
					this.source.Span.Slice(this.<copied>5__2, num2).CopyTo(new Span<char>(this.charBuffer, this.charPos, num2));
					this.charPos += num2;
					this.<copied>5__2 += num2;
					IL_131:
					if (this.<copied>5__2 >= this.source.Length)
					{
						if (this.appendNewLine)
						{
							this.<i>5__3 = 0;
							goto IL_231;
						}
						goto IL_244;
					}
					else
					{
						if (this.charPos != this.charLen)
						{
							goto IL_BA;
						}
						awaiter = this._this.FlushAsyncInternal(false, false, this.charBuffer, this.charPos, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__62>(ref awaiter, ref this);
							return;
						}
						goto IL_AC;
					}
					IL_1E5:
					awaiter.GetResult();
					this.charPos = 0;
					IL_1F3:
					this.charBuffer[this.charPos] = this.coreNewLine[this.<i>5__3];
					int num3 = this.charPos;
					this.charPos = num3 + 1;
					num3 = this.<i>5__3;
					this.<i>5__3 = num3 + 1;
					IL_231:
					if (this.<i>5__3 < this.coreNewLine.Length)
					{
						if (this.charPos != this.charLen)
						{
							goto IL_1F3;
						}
						awaiter = this._this.FlushAsyncInternal(false, false, this.charBuffer, this.charPos, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__62>(ref awaiter, ref this);
							return;
						}
						goto IL_1E5;
					}
					IL_244:
					if (!this.autoFlush)
					{
						goto IL_2D0;
					}
					awaiter = this._this.FlushAsyncInternal(true, false, this.charBuffer, this.charPos, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__62>(ref awaiter, ref this);
						return;
					}
					IL_2C2:
					awaiter.GetResult();
					this.charPos = 0;
					IL_2D0:
					this._this.CharPos_Prop = this.charPos;
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

			// Token: 0x060065B5 RID: 26037 RVA: 0x0015BF84 File Offset: 0x0015A184
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003BCC RID: 15308
			public int <>1__state;

			// Token: 0x04003BCD RID: 15309
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04003BCE RID: 15310
			public int charPos;

			// Token: 0x04003BCF RID: 15311
			public int charLen;

			// Token: 0x04003BD0 RID: 15312
			public StreamWriter _this;

			// Token: 0x04003BD1 RID: 15313
			public char[] charBuffer;

			// Token: 0x04003BD2 RID: 15314
			public CancellationToken cancellationToken;

			// Token: 0x04003BD3 RID: 15315
			public ReadOnlyMemory<char> source;

			// Token: 0x04003BD4 RID: 15316
			public bool appendNewLine;

			// Token: 0x04003BD5 RID: 15317
			public char[] coreNewLine;

			// Token: 0x04003BD6 RID: 15318
			public bool autoFlush;

			// Token: 0x04003BD7 RID: 15319
			private int <copied>5__2;

			// Token: 0x04003BD8 RID: 15320
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04003BD9 RID: 15321
			private int <i>5__3;
		}

		// Token: 0x02000B1D RID: 2845
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FlushAsyncInternal>d__74 : IAsyncStateMachine
		{
			// Token: 0x060065B6 RID: 26038 RVA: 0x0015BF94 File Offset: 0x0015A194
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter awaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
						this.<>1__state = -1;
						goto IL_161;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1D9;
					default:
					{
						if (this.haveWrittenPreamble)
						{
							goto IL_BA;
						}
						this._this.HaveWrittenPreamble_Prop = true;
						byte[] preamble = this.encoding.GetPreamble();
						if (preamble.Length == 0)
						{
							goto IL_BA;
						}
						awaiter = this.stream.WriteAsync(new ReadOnlyMemory<byte>(preamble), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, StreamWriter.<FlushAsyncInternal>d__74>(ref awaiter, ref this);
							return;
						}
						break;
					}
					}
					awaiter.GetResult();
					IL_BA:
					int bytes = this.encoder.GetBytes(this.charBuffer, 0, this.charPos, this.byteBuffer, 0, this.flushEncoder);
					if (bytes <= 0)
					{
						goto IL_168;
					}
					awaiter = this.stream.WriteAsync(new ReadOnlyMemory<byte>(this.byteBuffer, 0, bytes), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, StreamWriter.<FlushAsyncInternal>d__74>(ref awaiter, ref this);
						return;
					}
					IL_161:
					awaiter.GetResult();
					IL_168:
					if (!this.flushStream)
					{
						goto IL_1E0;
					}
					awaiter2 = this.stream.FlushAsync(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<FlushAsyncInternal>d__74>(ref awaiter2, ref this);
						return;
					}
					IL_1D9:
					awaiter2.GetResult();
					IL_1E0:;
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

			// Token: 0x060065B7 RID: 26039 RVA: 0x0015C1CC File Offset: 0x0015A3CC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003BDA RID: 15322
			public int <>1__state;

			// Token: 0x04003BDB RID: 15323
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04003BDC RID: 15324
			public bool haveWrittenPreamble;

			// Token: 0x04003BDD RID: 15325
			public StreamWriter _this;

			// Token: 0x04003BDE RID: 15326
			public Encoding encoding;

			// Token: 0x04003BDF RID: 15327
			public Stream stream;

			// Token: 0x04003BE0 RID: 15328
			public CancellationToken cancellationToken;

			// Token: 0x04003BE1 RID: 15329
			public Encoder encoder;

			// Token: 0x04003BE2 RID: 15330
			public char[] charBuffer;

			// Token: 0x04003BE3 RID: 15331
			public int charPos;

			// Token: 0x04003BE4 RID: 15332
			public byte[] byteBuffer;

			// Token: 0x04003BE5 RID: 15333
			public bool flushEncoder;

			// Token: 0x04003BE6 RID: 15334
			public bool flushStream;

			// Token: 0x04003BE7 RID: 15335
			private ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter <>u__1;

			// Token: 0x04003BE8 RID: 15336
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}
	}
}
