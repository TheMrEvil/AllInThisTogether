using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Implements a <see cref="T:System.IO.TextReader" /> that reads characters from a byte stream in a particular encoding.</summary>
	// Token: 0x02000B12 RID: 2834
	[Serializable]
	public class StreamReader : TextReader
	{
		// Token: 0x06006537 RID: 25911 RVA: 0x00158647 File Offset: 0x00156847
		private void CheckAsyncTaskInProgress()
		{
			if (!this._asyncReadTask.IsCompleted)
			{
				StreamReader.ThrowAsyncIOInProgress();
			}
		}

		// Token: 0x06006538 RID: 25912 RVA: 0x0015865B File Offset: 0x0015685B
		private static void ThrowAsyncIOInProgress()
		{
			throw new InvalidOperationException("The stream is currently in use by a previous operation on the stream.");
		}

		// Token: 0x06006539 RID: 25913 RVA: 0x00158667 File Offset: 0x00156867
		internal StreamReader()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream.</summary>
		/// <param name="stream">The stream to be read.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		// Token: 0x0600653A RID: 25914 RVA: 0x0015867A File Offset: 0x0015687A
		public StreamReader(Stream stream) : this(stream, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified byte order mark detection option.</summary>
		/// <param name="stream">The stream to be read.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		// Token: 0x0600653B RID: 25915 RVA: 0x00158684 File Offset: 0x00156884
		public StreamReader(Stream stream, bool detectEncodingFromByteOrderMarks) : this(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks, 1024, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified character encoding.</summary>
		/// <param name="stream">The stream to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is <see langword="null" />.</exception>
		// Token: 0x0600653C RID: 25916 RVA: 0x00158699 File Offset: 0x00156899
		public StreamReader(Stream stream, Encoding encoding) : this(stream, encoding, true, 1024, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified character encoding and byte order mark detection option.</summary>
		/// <param name="stream">The stream to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is <see langword="null" />.</exception>
		// Token: 0x0600653D RID: 25917 RVA: 0x001586AA File Offset: 0x001568AA
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks) : this(stream, encoding, detectEncodingFromByteOrderMarks, 1024, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified character encoding, byte order mark detection option, and buffer size.</summary>
		/// <param name="stream">The stream to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <param name="bufferSize">The minimum buffer size.</param>
		/// <exception cref="T:System.ArgumentException">The stream does not support reading.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is less than or equal to zero.</exception>
		// Token: 0x0600653E RID: 25918 RVA: 0x001586BB File Offset: 0x001568BB
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize) : this(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream based on the specified character encoding, byte order mark detection option, and buffer size, and optionally leaves the stream open.</summary>
		/// <param name="stream">The stream to read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">
		///   <see langword="true" /> to look for byte order marks at the beginning of the file; otherwise, <see langword="false" />.</param>
		/// <param name="bufferSize">The minimum buffer size.</param>
		/// <param name="leaveOpen">
		///   <see langword="true" /> to leave the stream open after the <see cref="T:System.IO.StreamReader" /> object is disposed; otherwise, <see langword="false" />.</param>
		// Token: 0x0600653F RID: 25919 RVA: 0x001586CC File Offset: 0x001568CC
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool leaveOpen)
		{
			if (stream == null || encoding == null)
			{
				throw new ArgumentNullException((stream == null) ? "stream" : "encoding");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream was not readable.");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
			}
			this.Init(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name.</summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		// Token: 0x06006540 RID: 25920 RVA: 0x0015873A File Offset: 0x0015693A
		public StreamReader(string path) : this(path, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified byte order mark detection option.</summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		// Token: 0x06006541 RID: 25921 RVA: 0x00158744 File Offset: 0x00156944
		public StreamReader(string path, bool detectEncodingFromByteOrderMarks) : this(path, Encoding.UTF8, detectEncodingFromByteOrderMarks, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified character encoding.</summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="encoding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		// Token: 0x06006542 RID: 25922 RVA: 0x00158758 File Offset: 0x00156958
		public StreamReader(string path, Encoding encoding) : this(path, encoding, true, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified character encoding and byte order mark detection option.</summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="encoding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		// Token: 0x06006543 RID: 25923 RVA: 0x00158768 File Offset: 0x00156968
		public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks) : this(path, encoding, detectEncodingFromByteOrderMarks, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified character encoding, byte order mark detection option, and buffer size.</summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <param name="bufferSize">The minimum buffer size, in number of 16-bit characters.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="encoding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="buffersize" /> is less than or equal to zero.</exception>
		// Token: 0x06006544 RID: 25924 RVA: 0x00158778 File Offset: 0x00156978
		public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
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
			Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan);
			this.Init(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, false);
		}

		// Token: 0x06006545 RID: 25925 RVA: 0x001587FC File Offset: 0x001569FC
		private void Init(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool leaveOpen)
		{
			this._stream = stream;
			this._encoding = encoding;
			this._decoder = encoding.GetDecoder();
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this._byteBuffer = new byte[bufferSize];
			this._maxCharsPerBuffer = encoding.GetMaxCharCount(bufferSize);
			this._charBuffer = new char[this._maxCharsPerBuffer];
			this._byteLen = 0;
			this._bytePos = 0;
			this._detectEncoding = detectEncodingFromByteOrderMarks;
			this._checkPreamble = (encoding.Preamble.Length > 0);
			this._isBlocked = false;
			this._closable = !leaveOpen;
		}

		// Token: 0x06006546 RID: 25926 RVA: 0x0015889D File Offset: 0x00156A9D
		internal void Init(Stream stream)
		{
			this._stream = stream;
			this._closable = true;
		}

		/// <summary>Closes the <see cref="T:System.IO.StreamReader" /> object and the underlying stream, and releases any system resources associated with the reader.</summary>
		// Token: 0x06006547 RID: 25927 RVA: 0x001588AD File Offset: 0x00156AAD
		public override void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Closes the underlying stream, releases the unmanaged resources used by the <see cref="T:System.IO.StreamReader" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06006548 RID: 25928 RVA: 0x001588B8 File Offset: 0x00156AB8
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this.LeaveOpen && disposing && this._stream != null)
				{
					this._stream.Close();
				}
			}
			finally
			{
				if (!this.LeaveOpen && this._stream != null)
				{
					this._stream = null;
					this._encoding = null;
					this._decoder = null;
					this._byteBuffer = null;
					this._charBuffer = null;
					this._charPos = 0;
					this._charLen = 0;
					base.Dispose(disposing);
				}
			}
		}

		/// <summary>Gets the current character encoding that the current <see cref="T:System.IO.StreamReader" /> object is using.</summary>
		/// <returns>The current character encoding used by the current reader. The value can be different after the first call to any <see cref="Overload:System.IO.StreamReader.Read" /> method of <see cref="T:System.IO.StreamReader" />, since encoding autodetection is not done until the first call to a <see cref="Overload:System.IO.StreamReader.Read" /> method.</returns>
		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06006549 RID: 25929 RVA: 0x00158940 File Offset: 0x00156B40
		public virtual Encoding CurrentEncoding
		{
			get
			{
				return this._encoding;
			}
		}

		/// <summary>Returns the underlying stream.</summary>
		/// <returns>The underlying stream.</returns>
		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x0600654A RID: 25930 RVA: 0x00158948 File Offset: 0x00156B48
		public virtual Stream BaseStream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x0600654B RID: 25931 RVA: 0x00158950 File Offset: 0x00156B50
		internal bool LeaveOpen
		{
			get
			{
				return !this._closable;
			}
		}

		/// <summary>Clears the internal buffer.</summary>
		// Token: 0x0600654C RID: 25932 RVA: 0x0015895B File Offset: 0x00156B5B
		public void DiscardBufferedData()
		{
			this.CheckAsyncTaskInProgress();
			this._byteLen = 0;
			this._charLen = 0;
			this._charPos = 0;
			if (this._encoding != null)
			{
				this._decoder = this._encoding.GetDecoder();
			}
			this._isBlocked = false;
		}

		/// <summary>Gets a value that indicates whether the current stream position is at the end of the stream.</summary>
		/// <returns>
		///   <see langword="true" /> if the current stream position is at the end of the stream; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The underlying stream has been disposed.</exception>
		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x0600654D RID: 25933 RVA: 0x00158998 File Offset: 0x00156B98
		public bool EndOfStream
		{
			get
			{
				if (this._stream == null)
				{
					throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
				}
				this.CheckAsyncTaskInProgress();
				return this._charPos >= this._charLen && this.ReadBuffer() == 0;
			}
		}

		/// <summary>Returns the next available character but does not consume it.</summary>
		/// <returns>An integer representing the next character to be read, or -1 if there are no characters to be read or if the stream does not support seeking.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x0600654E RID: 25934 RVA: 0x001589D0 File Offset: 0x00156BD0
		public override int Peek()
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			if (this._charPos == this._charLen && (this._isBlocked || this.ReadBuffer() == 0))
			{
				return -1;
			}
			return (int)this._charBuffer[this._charPos];
		}

		/// <summary>Reads the next character from the input stream and advances the character position by one character.</summary>
		/// <returns>The next character from the input stream represented as an <see cref="T:System.Int32" /> object, or -1 if no more characters are available.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x0600654F RID: 25935 RVA: 0x00158A24 File Offset: 0x00156C24
		public override int Read()
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			if (this._charPos == this._charLen && this.ReadBuffer() == 0)
			{
				return -1;
			}
			int result = (int)this._charBuffer[this._charPos];
			this._charPos++;
			return result;
		}

		/// <summary>Reads a specified maximum of characters from the current stream into a buffer, beginning at the specified index.</summary>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (index + count - 1) replaced by the characters read from the current source.</param>
		/// <param name="index">The index of <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read.</param>
		/// <returns>The number of characters that have been read, or 0 if at the end of the stream and no data was read. The number will be less than or equal to the <paramref name="count" /> parameter, depending on whether the data is available within the stream.</returns>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as the stream is closed.</exception>
		// Token: 0x06006550 RID: 25936 RVA: 0x00158A80 File Offset: 0x00156C80
		public override int Read(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			return this.ReadSpan(new Span<char>(buffer, index, count));
		}

		// Token: 0x06006551 RID: 25937 RVA: 0x00158AE4 File Offset: 0x00156CE4
		public override int Read(Span<char> buffer)
		{
			if (!(base.GetType() == typeof(StreamReader)))
			{
				return base.Read(buffer);
			}
			return this.ReadSpan(buffer);
		}

		// Token: 0x06006552 RID: 25938 RVA: 0x00158B0C File Offset: 0x00156D0C
		private int ReadSpan(Span<char> buffer)
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			int num = 0;
			bool flag = false;
			int i = buffer.Length;
			while (i > 0)
			{
				int num2 = this._charLen - this._charPos;
				if (num2 == 0)
				{
					num2 = this.ReadBuffer(buffer.Slice(num), out flag);
				}
				if (num2 == 0)
				{
					break;
				}
				if (num2 > i)
				{
					num2 = i;
				}
				if (!flag)
				{
					new Span<char>(this._charBuffer, this._charPos, num2).CopyTo(buffer.Slice(num));
					this._charPos += num2;
				}
				num += num2;
				i -= num2;
				if (this._isBlocked)
				{
					break;
				}
			}
			return num;
		}

		/// <summary>Reads all characters from the current position to the end of the stream.</summary>
		/// <returns>The rest of the stream as a string, from the current position to the end. If the current position is at the end of the stream, returns an empty string ("").</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06006553 RID: 25939 RVA: 0x00158BB8 File Offset: 0x00156DB8
		public override string ReadToEnd()
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			StringBuilder stringBuilder = new StringBuilder(this._charLen - this._charPos);
			do
			{
				stringBuilder.Append(this._charBuffer, this._charPos, this._charLen - this._charPos);
				this._charPos = this._charLen;
				this.ReadBuffer();
			}
			while (this._charLen > 0);
			return stringBuilder.ToString();
		}

		/// <summary>Reads a specified maximum number of characters from the current stream and writes the data to a buffer, beginning at the specified index.</summary>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (index + count - 1) replaced by the characters read from the current source.</param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read.</param>
		/// <returns>The number of characters that have been read. The number will be less than or equal to <paramref name="count" />, depending on whether all input characters have been read.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.StreamReader" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06006554 RID: 25940 RVA: 0x00158C34 File Offset: 0x00156E34
		public override int ReadBlock(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			return base.ReadBlock(buffer, index, count);
		}

		// Token: 0x06006555 RID: 25941 RVA: 0x00158CB0 File Offset: 0x00156EB0
		public override int ReadBlock(Span<char> buffer)
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadBlock(buffer);
			}
			int num = 0;
			int num2;
			do
			{
				num2 = this.ReadSpan(buffer.Slice(num));
				num += num2;
			}
			while (num2 > 0 && num < buffer.Length);
			return num;
		}

		// Token: 0x06006556 RID: 25942 RVA: 0x00158D00 File Offset: 0x00156F00
		private void CompressBuffer(int n)
		{
			Buffer.BlockCopy(this._byteBuffer, n, this._byteBuffer, 0, this._byteLen - n);
			this._byteLen -= n;
		}

		// Token: 0x06006557 RID: 25943 RVA: 0x00158D2C File Offset: 0x00156F2C
		private void DetectEncoding()
		{
			if (this._byteLen < 2)
			{
				return;
			}
			this._detectEncoding = false;
			bool flag = false;
			if (this._byteBuffer[0] == 254 && this._byteBuffer[1] == 255)
			{
				this._encoding = Encoding.BigEndianUnicode;
				this.CompressBuffer(2);
				flag = true;
			}
			else if (this._byteBuffer[0] == 255 && this._byteBuffer[1] == 254)
			{
				if (this._byteLen < 4 || this._byteBuffer[2] != 0 || this._byteBuffer[3] != 0)
				{
					this._encoding = Encoding.Unicode;
					this.CompressBuffer(2);
					flag = true;
				}
				else
				{
					this._encoding = Encoding.UTF32;
					this.CompressBuffer(4);
					flag = true;
				}
			}
			else if (this._byteLen >= 3 && this._byteBuffer[0] == 239 && this._byteBuffer[1] == 187 && this._byteBuffer[2] == 191)
			{
				this._encoding = Encoding.UTF8;
				this.CompressBuffer(3);
				flag = true;
			}
			else if (this._byteLen >= 4 && this._byteBuffer[0] == 0 && this._byteBuffer[1] == 0 && this._byteBuffer[2] == 254 && this._byteBuffer[3] == 255)
			{
				this._encoding = new UTF32Encoding(true, true);
				this.CompressBuffer(4);
				flag = true;
			}
			else if (this._byteLen == 2)
			{
				this._detectEncoding = true;
			}
			if (flag)
			{
				this._decoder = this._encoding.GetDecoder();
				int maxCharCount = this._encoding.GetMaxCharCount(this._byteBuffer.Length);
				if (maxCharCount > this._maxCharsPerBuffer)
				{
					this._charBuffer = new char[maxCharCount];
				}
				this._maxCharsPerBuffer = maxCharCount;
			}
		}

		// Token: 0x06006558 RID: 25944 RVA: 0x00158EE4 File Offset: 0x001570E4
		private unsafe bool IsPreamble()
		{
			if (!this._checkPreamble)
			{
				return this._checkPreamble;
			}
			ReadOnlySpan<byte> preamble = this._encoding.Preamble;
			int num = (this._byteLen >= preamble.Length) ? (preamble.Length - this._bytePos) : (this._byteLen - this._bytePos);
			int i = 0;
			while (i < num)
			{
				if (this._byteBuffer[this._bytePos] != *preamble[this._bytePos])
				{
					this._bytePos = 0;
					this._checkPreamble = false;
					break;
				}
				i++;
				this._bytePos++;
			}
			if (this._checkPreamble && this._bytePos == preamble.Length)
			{
				this.CompressBuffer(preamble.Length);
				this._bytePos = 0;
				this._checkPreamble = false;
				this._detectEncoding = false;
			}
			return this._checkPreamble;
		}

		// Token: 0x06006559 RID: 25945 RVA: 0x00158FC0 File Offset: 0x001571C0
		internal virtual int ReadBuffer()
		{
			this._charLen = 0;
			this._charPos = 0;
			if (!this._checkPreamble)
			{
				this._byteLen = 0;
			}
			for (;;)
			{
				if (this._checkPreamble)
				{
					int num = this._stream.Read(this._byteBuffer, this._bytePos, this._byteBuffer.Length - this._bytePos);
					if (num == 0)
					{
						break;
					}
					this._byteLen += num;
				}
				else
				{
					this._byteLen = this._stream.Read(this._byteBuffer, 0, this._byteBuffer.Length);
					if (this._byteLen == 0)
					{
						goto Block_5;
					}
				}
				this._isBlocked = (this._byteLen < this._byteBuffer.Length);
				if (!this.IsPreamble())
				{
					if (this._detectEncoding && this._byteLen >= 2)
					{
						this.DetectEncoding();
					}
					this._charLen += this._decoder.GetChars(this._byteBuffer, 0, this._byteLen, this._charBuffer, this._charLen);
				}
				if (this._charLen != 0)
				{
					goto Block_9;
				}
			}
			if (this._byteLen > 0)
			{
				this._charLen += this._decoder.GetChars(this._byteBuffer, 0, this._byteLen, this._charBuffer, this._charLen);
				this._bytePos = (this._byteLen = 0);
			}
			return this._charLen;
			Block_5:
			return this._charLen;
			Block_9:
			return this._charLen;
		}

		// Token: 0x0600655A RID: 25946 RVA: 0x00159128 File Offset: 0x00157328
		private int ReadBuffer(Span<char> userBuffer, out bool readToUserBuffer)
		{
			this._charLen = 0;
			this._charPos = 0;
			if (!this._checkPreamble)
			{
				this._byteLen = 0;
			}
			int num = 0;
			readToUserBuffer = (userBuffer.Length >= this._maxCharsPerBuffer);
			for (;;)
			{
				if (this._checkPreamble)
				{
					int num2 = this._stream.Read(this._byteBuffer, this._bytePos, this._byteBuffer.Length - this._bytePos);
					if (num2 == 0)
					{
						break;
					}
					this._byteLen += num2;
				}
				else
				{
					this._byteLen = this._stream.Read(this._byteBuffer, 0, this._byteBuffer.Length);
					if (this._byteLen == 0)
					{
						goto IL_1CD;
					}
				}
				this._isBlocked = (this._byteLen < this._byteBuffer.Length);
				if (!this.IsPreamble())
				{
					if (this._detectEncoding && this._byteLen >= 2)
					{
						this.DetectEncoding();
						readToUserBuffer = (userBuffer.Length >= this._maxCharsPerBuffer);
					}
					this._charPos = 0;
					if (readToUserBuffer)
					{
						num += this._decoder.GetChars(new ReadOnlySpan<byte>(this._byteBuffer, 0, this._byteLen), userBuffer.Slice(num), false);
						this._charLen = 0;
					}
					else
					{
						num = this._decoder.GetChars(this._byteBuffer, 0, this._byteLen, this._charBuffer, num);
						this._charLen += num;
					}
				}
				if (num != 0)
				{
					goto IL_1CD;
				}
			}
			if (this._byteLen > 0)
			{
				if (readToUserBuffer)
				{
					num = this._decoder.GetChars(new ReadOnlySpan<byte>(this._byteBuffer, 0, this._byteLen), userBuffer.Slice(num), false);
					this._charLen = 0;
				}
				else
				{
					num = this._decoder.GetChars(this._byteBuffer, 0, this._byteLen, this._charBuffer, num);
					this._charLen += num;
				}
			}
			return num;
			IL_1CD:
			this._isBlocked &= (num < userBuffer.Length);
			return num;
		}

		/// <summary>Reads a line of characters from the current stream and returns the data as a string.</summary>
		/// <returns>The next line from the input stream, or <see langword="null" /> if the end of the input stream is reached.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x0600655B RID: 25947 RVA: 0x0015931C File Offset: 0x0015751C
		public override string ReadLine()
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			if (this._charPos == this._charLen && this.ReadBuffer() == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int num;
			char c;
			for (;;)
			{
				num = this._charPos;
				do
				{
					c = this._charBuffer[num];
					if (c == '\r' || c == '\n')
					{
						goto IL_51;
					}
					num++;
				}
				while (num < this._charLen);
				num = this._charLen - this._charPos;
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(num + 80);
				}
				stringBuilder.Append(this._charBuffer, this._charPos, num);
				if (this.ReadBuffer() <= 0)
				{
					goto Block_11;
				}
			}
			IL_51:
			string result;
			if (stringBuilder != null)
			{
				stringBuilder.Append(this._charBuffer, this._charPos, num - this._charPos);
				result = stringBuilder.ToString();
			}
			else
			{
				result = new string(this._charBuffer, this._charPos, num - this._charPos);
			}
			this._charPos = num + 1;
			if (c == '\r' && (this._charPos < this._charLen || this.ReadBuffer() > 0) && this._charBuffer[this._charPos] == '\n')
			{
				this._charPos++;
			}
			return result;
			Block_11:
			return stringBuilder.ToString();
		}

		/// <summary>Reads a line of characters asynchronously from the current stream and returns the data as a string.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the next line from the stream, or is <see langword="null" /> if all the characters have been read.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation.</exception>
		// Token: 0x0600655C RID: 25948 RVA: 0x00159454 File Offset: 0x00157654
		public override Task<string> ReadLineAsync()
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadLineAsync();
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			Task<string> task = this.ReadLineAsyncInternal();
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x0600655D RID: 25949 RVA: 0x001594A8 File Offset: 0x001576A8
		private Task<string> ReadLineAsyncInternal()
		{
			StreamReader.<ReadLineAsyncInternal>d__61 <ReadLineAsyncInternal>d__;
			<ReadLineAsyncInternal>d__.<>4__this = this;
			<ReadLineAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadLineAsyncInternal>d__.<>1__state = -1;
			<ReadLineAsyncInternal>d__.<>t__builder.Start<StreamReader.<ReadLineAsyncInternal>d__61>(ref <ReadLineAsyncInternal>d__);
			return <ReadLineAsyncInternal>d__.<>t__builder.Task;
		}

		/// <summary>Reads all characters from the current position to the end of the stream asynchronously and returns them as one string.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains a string with the characters from the current position to the end of the stream.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation.</exception>
		// Token: 0x0600655E RID: 25950 RVA: 0x001594EC File Offset: 0x001576EC
		public override Task<string> ReadToEndAsync()
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadToEndAsync();
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			Task<string> task = this.ReadToEndAsyncInternal();
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x0600655F RID: 25951 RVA: 0x00159540 File Offset: 0x00157740
		private Task<string> ReadToEndAsyncInternal()
		{
			StreamReader.<ReadToEndAsyncInternal>d__63 <ReadToEndAsyncInternal>d__;
			<ReadToEndAsyncInternal>d__.<>4__this = this;
			<ReadToEndAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadToEndAsyncInternal>d__.<>1__state = -1;
			<ReadToEndAsyncInternal>d__.<>t__builder.Start<StreamReader.<ReadToEndAsyncInternal>d__63>(ref <ReadToEndAsyncInternal>d__);
			return <ReadToEndAsyncInternal>d__.<>t__builder.Task;
		}

		/// <summary>Reads a specified maximum number of characters from the current stream asynchronously and writes the data to a buffer, beginning at the specified index.</summary>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> - 1) replaced by the characters read from the current source.</param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read. If the end of the stream is reached before the specified number of characters is written into the buffer, the current method returns.</param>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of characters read into the buffer. The result value can be less than the number of characters requested if the number of characters currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="index" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation.</exception>
		// Token: 0x06006560 RID: 25952 RVA: 0x00159584 File Offset: 0x00157784
		public override Task<int> ReadAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadAsync(buffer, index, count);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			Task<int> task = this.ReadAsyncInternal(new Memory<char>(buffer, index, count), default(CancellationToken)).AsTask();
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x06006561 RID: 25953 RVA: 0x00159640 File Offset: 0x00157840
		public override ValueTask<int> ReadAsync(Memory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadAsync(buffer, cancellationToken);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
			}
			return this.ReadAsyncInternal(buffer, cancellationToken);
		}

		// Token: 0x06006562 RID: 25954 RVA: 0x001596A4 File Offset: 0x001578A4
		internal override ValueTask<int> ReadAsyncInternal(Memory<char> buffer, CancellationToken cancellationToken)
		{
			StreamReader.<ReadAsyncInternal>d__66 <ReadAsyncInternal>d__;
			<ReadAsyncInternal>d__.<>4__this = this;
			<ReadAsyncInternal>d__.buffer = buffer;
			<ReadAsyncInternal>d__.cancellationToken = cancellationToken;
			<ReadAsyncInternal>d__.<>t__builder = AsyncValueTaskMethodBuilder<int>.Create();
			<ReadAsyncInternal>d__.<>1__state = -1;
			<ReadAsyncInternal>d__.<>t__builder.Start<StreamReader.<ReadAsyncInternal>d__66>(ref <ReadAsyncInternal>d__);
			return <ReadAsyncInternal>d__.<>t__builder.Task;
		}

		/// <summary>Reads a specified maximum number of characters from the current stream asynchronously and writes the data to a buffer, beginning at the specified index.</summary>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> - 1) replaced by the characters read from the current source.</param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read. If the end of the stream is reached before the specified number of characters is written into the buffer, the method returns.</param>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of characters read into the buffer. The result value can be less than the number of characters requested if the number of characters currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="index" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation.</exception>
		// Token: 0x06006563 RID: 25955 RVA: 0x001596F8 File Offset: 0x001578F8
		public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadBlockAsync(buffer, index, count);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			Task<int> task = base.ReadBlockAsync(buffer, index, count);
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x06006564 RID: 25956 RVA: 0x0015979C File Offset: 0x0015799C
		public override ValueTask<int> ReadBlockAsync(Memory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadBlockAsync(buffer, cancellationToken);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
			}
			ValueTask<int> result = base.ReadBlockAsyncInternal(buffer, cancellationToken);
			if (result.IsCompletedSuccessfully)
			{
				return result;
			}
			Task<int> task = result.AsTask();
			this._asyncReadTask = task;
			return new ValueTask<int>(task);
		}

		// Token: 0x06006565 RID: 25957 RVA: 0x00159824 File Offset: 0x00157A24
		private Task<int> ReadBufferAsync()
		{
			StreamReader.<ReadBufferAsync>d__69 <ReadBufferAsync>d__;
			<ReadBufferAsync>d__.<>4__this = this;
			<ReadBufferAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadBufferAsync>d__.<>1__state = -1;
			<ReadBufferAsync>d__.<>t__builder.Start<StreamReader.<ReadBufferAsync>d__69>(ref <ReadBufferAsync>d__);
			return <ReadBufferAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06006566 RID: 25958 RVA: 0x00159867 File Offset: 0x00157A67
		internal bool DataAvailable()
		{
			return this._charPos < this._charLen;
		}

		// Token: 0x06006567 RID: 25959 RVA: 0x00159877 File Offset: 0x00157A77
		// Note: this type is marked as 'beforefieldinit'.
		static StreamReader()
		{
		}

		/// <summary>A <see cref="T:System.IO.StreamReader" /> object around an empty stream.</summary>
		// Token: 0x04003B6D RID: 15213
		public new static readonly StreamReader Null = new StreamReader.NullStreamReader();

		// Token: 0x04003B6E RID: 15214
		private const int DefaultBufferSize = 1024;

		// Token: 0x04003B6F RID: 15215
		private const int DefaultFileStreamBufferSize = 4096;

		// Token: 0x04003B70 RID: 15216
		private const int MinBufferSize = 128;

		// Token: 0x04003B71 RID: 15217
		private Stream _stream;

		// Token: 0x04003B72 RID: 15218
		private Encoding _encoding;

		// Token: 0x04003B73 RID: 15219
		private Decoder _decoder;

		// Token: 0x04003B74 RID: 15220
		private byte[] _byteBuffer;

		// Token: 0x04003B75 RID: 15221
		private char[] _charBuffer;

		// Token: 0x04003B76 RID: 15222
		private int _charPos;

		// Token: 0x04003B77 RID: 15223
		private int _charLen;

		// Token: 0x04003B78 RID: 15224
		private int _byteLen;

		// Token: 0x04003B79 RID: 15225
		private int _bytePos;

		// Token: 0x04003B7A RID: 15226
		private int _maxCharsPerBuffer;

		// Token: 0x04003B7B RID: 15227
		private bool _detectEncoding;

		// Token: 0x04003B7C RID: 15228
		private bool _checkPreamble;

		// Token: 0x04003B7D RID: 15229
		private bool _isBlocked;

		// Token: 0x04003B7E RID: 15230
		private bool _closable;

		// Token: 0x04003B7F RID: 15231
		private Task _asyncReadTask = Task.CompletedTask;

		// Token: 0x02000B13 RID: 2835
		private class NullStreamReader : StreamReader
		{
			// Token: 0x06006568 RID: 25960 RVA: 0x00159883 File Offset: 0x00157A83
			internal NullStreamReader()
			{
				base.Init(Stream.Null);
			}

			// Token: 0x170011BD RID: 4541
			// (get) Token: 0x06006569 RID: 25961 RVA: 0x00159896 File Offset: 0x00157A96
			public override Stream BaseStream
			{
				get
				{
					return Stream.Null;
				}
			}

			// Token: 0x170011BE RID: 4542
			// (get) Token: 0x0600656A RID: 25962 RVA: 0x0015989D File Offset: 0x00157A9D
			public override Encoding CurrentEncoding
			{
				get
				{
					return Encoding.Unicode;
				}
			}

			// Token: 0x0600656B RID: 25963 RVA: 0x00004BF9 File Offset: 0x00002DF9
			protected override void Dispose(bool disposing)
			{
			}

			// Token: 0x0600656C RID: 25964 RVA: 0x0012275A File Offset: 0x0012095A
			public override int Peek()
			{
				return -1;
			}

			// Token: 0x0600656D RID: 25965 RVA: 0x0012275A File Offset: 0x0012095A
			public override int Read()
			{
				return -1;
			}

			// Token: 0x0600656E RID: 25966 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
			public override int Read(char[] buffer, int index, int count)
			{
				return 0;
			}

			// Token: 0x0600656F RID: 25967 RVA: 0x0000AF5E File Offset: 0x0000915E
			public override string ReadLine()
			{
				return null;
			}

			// Token: 0x06006570 RID: 25968 RVA: 0x000258DB File Offset: 0x00023ADB
			public override string ReadToEnd()
			{
				return string.Empty;
			}

			// Token: 0x06006571 RID: 25969 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
			internal override int ReadBuffer()
			{
				return 0;
			}
		}

		// Token: 0x02000B14 RID: 2836
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadLineAsyncInternal>d__61 : IAsyncStateMachine
		{
			// Token: 0x06006572 RID: 25970 RVA: 0x001598A4 File Offset: 0x00157AA4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				StreamReader streamReader = this.<>4__this;
				string result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					bool flag;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1A9;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_287;
					default:
						flag = (streamReader._charPos == streamReader._charLen);
						if (!flag)
						{
							goto IL_9E;
						}
						awaiter = streamReader.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, StreamReader.<ReadLineAsyncInternal>d__61>(ref awaiter, ref this);
							return;
						}
						break;
					}
					flag = (awaiter.GetResult() == 0);
					IL_9E:
					if (flag)
					{
						result = null;
						goto IL_2C2;
					}
					this.<sb>5__2 = null;
					IL_AF:
					char[] charBuffer = streamReader._charBuffer;
					int charLen = streamReader._charLen;
					int num2 = streamReader._charPos;
					int num3 = num2;
					char c;
					for (;;)
					{
						c = charBuffer[num3];
						if (c == '\r' || c == '\n')
						{
							break;
						}
						num3++;
						if (num3 >= charLen)
						{
							goto Block_14;
						}
					}
					if (this.<sb>5__2 != null)
					{
						this.<sb>5__2.Append(charBuffer, num2, num3 - num2);
						this.<s>5__3 = this.<sb>5__2.ToString();
					}
					else
					{
						this.<s>5__3 = new string(charBuffer, num2, num3 - num2);
					}
					num2 = (streamReader._charPos = num3 + 1);
					flag = (c == '\r');
					if (!flag)
					{
						goto IL_1B8;
					}
					bool flag2 = num2 < charLen;
					if (flag2)
					{
						goto IL_1B5;
					}
					awaiter = streamReader.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, StreamReader.<ReadLineAsyncInternal>d__61>(ref awaiter, ref this);
						return;
					}
					goto IL_1A9;
					Block_14:
					num3 = charLen - num2;
					if (this.<sb>5__2 == null)
					{
						this.<sb>5__2 = new StringBuilder(num3 + 80);
					}
					this.<sb>5__2.Append(charBuffer, num2, num3);
					awaiter = streamReader.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, StreamReader.<ReadLineAsyncInternal>d__61>(ref awaiter, ref this);
						return;
					}
					goto IL_287;
					IL_1A9:
					flag2 = (awaiter.GetResult() > 0);
					IL_1B5:
					flag = flag2;
					IL_1B8:
					if (flag)
					{
						num2 = streamReader._charPos;
						if (streamReader._charBuffer[num2] == '\n')
						{
							streamReader._charPos = num2 + 1;
						}
					}
					result = this.<s>5__3;
					goto IL_2C2;
					IL_287:
					if (awaiter.GetResult() > 0)
					{
						goto IL_AF;
					}
					result = this.<sb>5__2.ToString();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<sb>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_2C2:
				this.<>1__state = -2;
				this.<sb>5__2 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06006573 RID: 25971 RVA: 0x00159BAC File Offset: 0x00157DAC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003B80 RID: 15232
			public int <>1__state;

			// Token: 0x04003B81 RID: 15233
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x04003B82 RID: 15234
			public StreamReader <>4__this;

			// Token: 0x04003B83 RID: 15235
			private StringBuilder <sb>5__2;

			// Token: 0x04003B84 RID: 15236
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04003B85 RID: 15237
			private string <s>5__3;
		}

		// Token: 0x02000B15 RID: 2837
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadToEndAsyncInternal>d__63 : IAsyncStateMachine
		{
			// Token: 0x06006574 RID: 25972 RVA: 0x00159BBC File Offset: 0x00157DBC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				StreamReader streamReader = this.<>4__this;
				string result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_B8;
					}
					this.<sb>5__2 = new StringBuilder(streamReader._charLen - streamReader._charPos);
					IL_2C:
					int charPos = streamReader._charPos;
					this.<sb>5__2.Append(streamReader._charBuffer, charPos, streamReader._charLen - charPos);
					streamReader._charPos = streamReader._charLen;
					awaiter = streamReader.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, StreamReader.<ReadToEndAsyncInternal>d__63>(ref awaiter, ref this);
						return;
					}
					IL_B8:
					awaiter.GetResult();
					if (streamReader._charLen > 0)
					{
						goto IL_2C;
					}
					result = this.<sb>5__2.ToString();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<sb>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<sb>5__2 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06006575 RID: 25973 RVA: 0x00159CF0 File Offset: 0x00157EF0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003B86 RID: 15238
			public int <>1__state;

			// Token: 0x04003B87 RID: 15239
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x04003B88 RID: 15240
			public StreamReader <>4__this;

			// Token: 0x04003B89 RID: 15241
			private StringBuilder <sb>5__2;

			// Token: 0x04003B8A RID: 15242
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000B16 RID: 2838
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAsyncInternal>d__66 : IAsyncStateMachine
		{
			// Token: 0x06006576 RID: 25974 RVA: 0x00159D00 File Offset: 0x00157F00
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				StreamReader streamReader = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter awaiter2;
					bool flag;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1D1;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
						this.<>1__state = -1;
						goto IL_30F;
					default:
						flag = (streamReader._charPos == streamReader._charLen);
						if (!flag)
						{
							goto IL_9E;
						}
						awaiter = streamReader.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, StreamReader.<ReadAsyncInternal>d__66>(ref awaiter, ref this);
							return;
						}
						break;
					}
					flag = (awaiter.GetResult() == 0);
					IL_9E:
					if (flag)
					{
						result = 0;
						goto IL_507;
					}
					this.<charsRead>5__2 = 0;
					this.<readToUserBuffer>5__3 = false;
					this.<tmpByteBuffer>5__4 = streamReader._byteBuffer;
					this.<tmpStream>5__5 = streamReader._stream;
					this.<count>5__6 = this.buffer.Length;
					goto IL_4CB;
					IL_136:
					if (streamReader._checkPreamble)
					{
						int bytePos = streamReader._bytePos;
						awaiter2 = this.<tmpStream>5__5.ReadAsync(new Memory<byte>(this.<tmpByteBuffer>5__4, bytePos, this.<tmpByteBuffer>5__4.Length - bytePos), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, StreamReader.<ReadAsyncInternal>d__66>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<tmpStream>5__5.ReadAsync(new Memory<byte>(this.<tmpByteBuffer>5__4), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, StreamReader.<ReadAsyncInternal>d__66>(ref awaiter2, ref this);
							return;
						}
						goto IL_30F;
					}
					IL_1D1:
					int result2 = awaiter2.GetResult();
					if (result2 == 0)
					{
						if (streamReader._byteLen > 0)
						{
							if (this.<readToUserBuffer>5__3)
							{
								this.<n>5__7 = streamReader._decoder.GetChars(new ReadOnlySpan<byte>(this.<tmpByteBuffer>5__4, 0, streamReader._byteLen), this.buffer.Span.Slice(this.<charsRead>5__2), false);
								streamReader._charLen = 0;
							}
							else
							{
								this.<n>5__7 = streamReader._decoder.GetChars(this.<tmpByteBuffer>5__4, 0, streamReader._byteLen, streamReader._charBuffer, 0);
								streamReader._charLen += this.<n>5__7;
							}
						}
						streamReader._isBlocked = true;
						goto IL_423;
					}
					streamReader._byteLen += result2;
					goto IL_334;
					IL_30F:
					int result3 = awaiter2.GetResult();
					streamReader._byteLen = result3;
					if (streamReader._byteLen == 0)
					{
						streamReader._isBlocked = true;
						goto IL_423;
					}
					IL_334:
					streamReader._isBlocked = (streamReader._byteLen < this.<tmpByteBuffer>5__4.Length);
					if (!streamReader.IsPreamble())
					{
						if (streamReader._detectEncoding && streamReader._byteLen >= 2)
						{
							streamReader.DetectEncoding();
							this.<readToUserBuffer>5__3 = (this.<count>5__6 >= streamReader._maxCharsPerBuffer);
						}
						streamReader._charPos = 0;
						if (this.<readToUserBuffer>5__3)
						{
							this.<n>5__7 += streamReader._decoder.GetChars(new ReadOnlySpan<byte>(this.<tmpByteBuffer>5__4, 0, streamReader._byteLen), this.buffer.Span.Slice(this.<charsRead>5__2), false);
							streamReader._charLen = 0;
						}
						else
						{
							this.<n>5__7 = streamReader._decoder.GetChars(this.<tmpByteBuffer>5__4, 0, streamReader._byteLen, streamReader._charBuffer, 0);
							streamReader._charLen += this.<n>5__7;
						}
					}
					if (this.<n>5__7 == 0)
					{
						goto IL_136;
					}
					IL_423:
					if (this.<n>5__7 == 0)
					{
						goto IL_4D7;
					}
					IL_42E:
					if (this.<n>5__7 > this.<count>5__6)
					{
						this.<n>5__7 = this.<count>5__6;
					}
					if (!this.<readToUserBuffer>5__3)
					{
						new Span<char>(streamReader._charBuffer, streamReader._charPos, this.<n>5__7).CopyTo(this.buffer.Span.Slice(this.<charsRead>5__2));
						streamReader._charPos += this.<n>5__7;
					}
					this.<charsRead>5__2 += this.<n>5__7;
					this.<count>5__6 -= this.<n>5__7;
					if (streamReader._isBlocked)
					{
						goto IL_4D7;
					}
					IL_4CB:
					if (this.<count>5__6 > 0)
					{
						this.<n>5__7 = streamReader._charLen - streamReader._charPos;
						if (this.<n>5__7 == 0)
						{
							streamReader._charLen = 0;
							streamReader._charPos = 0;
							if (!streamReader._checkPreamble)
							{
								streamReader._byteLen = 0;
							}
							this.<readToUserBuffer>5__3 = (this.<count>5__6 >= streamReader._maxCharsPerBuffer);
							goto IL_136;
						}
						goto IL_42E;
					}
					IL_4D7:
					result = this.<charsRead>5__2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<tmpByteBuffer>5__4 = null;
					this.<tmpStream>5__5 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_507:
				this.<>1__state = -2;
				this.<tmpByteBuffer>5__4 = null;
				this.<tmpStream>5__5 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06006577 RID: 25975 RVA: 0x0015A254 File Offset: 0x00158454
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003B8B RID: 15243
			public int <>1__state;

			// Token: 0x04003B8C RID: 15244
			public AsyncValueTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04003B8D RID: 15245
			public StreamReader <>4__this;

			// Token: 0x04003B8E RID: 15246
			public Memory<char> buffer;

			// Token: 0x04003B8F RID: 15247
			public CancellationToken cancellationToken;

			// Token: 0x04003B90 RID: 15248
			private int <charsRead>5__2;

			// Token: 0x04003B91 RID: 15249
			private bool <readToUserBuffer>5__3;

			// Token: 0x04003B92 RID: 15250
			private byte[] <tmpByteBuffer>5__4;

			// Token: 0x04003B93 RID: 15251
			private Stream <tmpStream>5__5;

			// Token: 0x04003B94 RID: 15252
			private int <count>5__6;

			// Token: 0x04003B95 RID: 15253
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04003B96 RID: 15254
			private int <n>5__7;

			// Token: 0x04003B97 RID: 15255
			private ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter <>u__2;
		}

		// Token: 0x02000B17 RID: 2839
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadBufferAsync>d__69 : IAsyncStateMachine
		{
			// Token: 0x06006578 RID: 25976 RVA: 0x0015A264 File Offset: 0x00158464
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				StreamReader streamReader = this.<>4__this;
				int charLen;
				try
				{
					ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
						this.<>1__state = -1;
						goto IL_EC;
					}
					if (num == 1)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1E0;
					}
					streamReader._charLen = 0;
					streamReader._charPos = 0;
					this.<tmpByteBuffer>5__2 = streamReader._byteBuffer;
					this.<tmpStream>5__3 = streamReader._stream;
					if (!streamReader._checkPreamble)
					{
						streamReader._byteLen = 0;
					}
					IL_50:
					if (streamReader._checkPreamble)
					{
						int bytePos = streamReader._bytePos;
						awaiter = this.<tmpStream>5__3.ReadAsync(new Memory<byte>(this.<tmpByteBuffer>5__2, bytePos, this.<tmpByteBuffer>5__2.Length - bytePos), default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, StreamReader.<ReadBufferAsync>d__69>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<tmpStream>5__3.ReadAsync(new Memory<byte>(this.<tmpByteBuffer>5__2), default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, StreamReader.<ReadBufferAsync>d__69>(ref awaiter, ref this);
							return;
						}
						goto IL_1E0;
					}
					IL_EC:
					int result = awaiter.GetResult();
					if (result == 0)
					{
						if (streamReader._byteLen > 0)
						{
							streamReader._charLen += streamReader._decoder.GetChars(this.<tmpByteBuffer>5__2, 0, streamReader._byteLen, streamReader._charBuffer, streamReader._charLen);
							streamReader._bytePos = 0;
							streamReader._byteLen = 0;
						}
						charLen = streamReader._charLen;
						goto IL_2A6;
					}
					streamReader._byteLen += result;
					goto IL_205;
					IL_1E0:
					int result2 = awaiter.GetResult();
					streamReader._byteLen = result2;
					if (streamReader._byteLen == 0)
					{
						charLen = streamReader._charLen;
						goto IL_2A6;
					}
					IL_205:
					streamReader._isBlocked = (streamReader._byteLen < this.<tmpByteBuffer>5__2.Length);
					if (!streamReader.IsPreamble())
					{
						if (streamReader._detectEncoding && streamReader._byteLen >= 2)
						{
							streamReader.DetectEncoding();
						}
						streamReader._charLen += streamReader._decoder.GetChars(this.<tmpByteBuffer>5__2, 0, streamReader._byteLen, streamReader._charBuffer, streamReader._charLen);
					}
					if (streamReader._charLen == 0)
					{
						goto IL_50;
					}
					charLen = streamReader._charLen;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<tmpByteBuffer>5__2 = null;
					this.<tmpStream>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_2A6:
				this.<>1__state = -2;
				this.<tmpByteBuffer>5__2 = null;
				this.<tmpStream>5__3 = null;
				this.<>t__builder.SetResult(charLen);
			}

			// Token: 0x06006579 RID: 25977 RVA: 0x0015A558 File Offset: 0x00158758
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003B98 RID: 15256
			public int <>1__state;

			// Token: 0x04003B99 RID: 15257
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04003B9A RID: 15258
			public StreamReader <>4__this;

			// Token: 0x04003B9B RID: 15259
			private byte[] <tmpByteBuffer>5__2;

			// Token: 0x04003B9C RID: 15260
			private Stream <tmpStream>5__3;

			// Token: 0x04003B9D RID: 15261
			private ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter <>u__1;
		}
	}
}
