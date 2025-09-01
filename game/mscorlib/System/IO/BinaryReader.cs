using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Mono.Security;

namespace System.IO
{
	/// <summary>Reads primitive data types as binary values in a specific encoding.</summary>
	// Token: 0x02000B57 RID: 2903
	[ComVisible(true)]
	public class BinaryReader : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.BinaryReader" /> class based on the specified stream and using UTF-8 encoding.</summary>
		/// <param name="input">The input stream.</param>
		/// <exception cref="T:System.ArgumentException">The stream does not support reading, is <see langword="null" />, or is already closed.</exception>
		// Token: 0x060068FA RID: 26874 RVA: 0x0016685B File Offset: 0x00164A5B
		public BinaryReader(Stream input) : this(input, new UTF8Encoding(), false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.BinaryReader" /> class based on the specified stream and character encoding.</summary>
		/// <param name="input">The input stream.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <exception cref="T:System.ArgumentException">The stream does not support reading, is <see langword="null" />, or is already closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="encoding" /> is <see langword="null" />.</exception>
		// Token: 0x060068FB RID: 26875 RVA: 0x0016686A File Offset: 0x00164A6A
		public BinaryReader(Stream input, Encoding encoding) : this(input, encoding, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.BinaryReader" /> class based on the specified stream and character encoding, and optionally leaves the stream open.</summary>
		/// <param name="input">The input stream.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="leaveOpen">
		///   <see langword="true" /> to leave the stream open after the <see cref="T:System.IO.BinaryReader" /> object is disposed; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">The stream does not support reading, is <see langword="null" />, or is already closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="encoding" /> or <paramref name="input" /> is <see langword="null" />.</exception>
		// Token: 0x060068FC RID: 26876 RVA: 0x00166878 File Offset: 0x00164A78
		public BinaryReader(Stream input, Encoding encoding, bool leaveOpen)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (!input.CanRead)
			{
				throw new ArgumentException(Environment.GetResourceString("Stream was not readable."));
			}
			this.m_stream = input;
			this.m_decoder = encoding.GetDecoder();
			this.m_maxCharsSize = encoding.GetMaxCharCount(128);
			int num = encoding.GetMaxByteCount(1);
			if (num < 16)
			{
				num = 16;
			}
			this.m_buffer = new byte[num];
			this.m_2BytesPerChar = (encoding is UnicodeEncoding);
			this.m_isMemoryStream = (this.m_stream.GetType() == typeof(MemoryStream));
			this.m_leaveOpen = leaveOpen;
		}

		/// <summary>Exposes access to the underlying stream of the <see cref="T:System.IO.BinaryReader" />.</summary>
		/// <returns>The underlying stream associated with the <see langword="BinaryReader" />.</returns>
		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x060068FD RID: 26877 RVA: 0x00166935 File Offset: 0x00164B35
		public virtual Stream BaseStream
		{
			get
			{
				return this.m_stream;
			}
		}

		/// <summary>Closes the current reader and the underlying stream.</summary>
		// Token: 0x060068FE RID: 26878 RVA: 0x0016693D File Offset: 0x00164B3D
		public virtual void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.BinaryReader" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060068FF RID: 26879 RVA: 0x00166948 File Offset: 0x00164B48
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				Stream stream = this.m_stream;
				this.m_stream = null;
				if (stream != null && !this.m_leaveOpen)
				{
					stream.Close();
				}
			}
			this.m_stream = null;
			this.m_buffer = null;
			this.m_decoder = null;
			this.m_charBytes = null;
			this.m_singleChar = null;
			this.m_charBuffer = null;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.IO.BinaryReader" /> class.</summary>
		// Token: 0x06006900 RID: 26880 RVA: 0x0016693D File Offset: 0x00164B3D
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Returns the next available character and does not advance the byte or character position.</summary>
		/// <returns>The next available character, or -1 if no more characters are available or the stream does not support seeking.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ArgumentException">The current character cannot be decoded into the internal character buffer by using the <see cref="T:System.Text.Encoding" /> selected for the stream.</exception>
		// Token: 0x06006901 RID: 26881 RVA: 0x001669A4 File Offset: 0x00164BA4
		public virtual int PeekChar()
		{
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			if (!this.m_stream.CanSeek)
			{
				return -1;
			}
			long position = this.m_stream.Position;
			int result = this.Read();
			this.m_stream.Position = position;
			return result;
		}

		/// <summary>Reads characters from the underlying stream and advances the current position of the stream in accordance with the <see langword="Encoding" /> used and the specific character being read from the stream.</summary>
		/// <returns>The next character from the input stream, or -1 if no characters are currently available.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x06006902 RID: 26882 RVA: 0x001669EB File Offset: 0x00164BEB
		public virtual int Read()
		{
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			return this.InternalReadOneChar();
		}

		/// <summary>Reads a <see langword="Boolean" /> value from the current stream and advances the current position of the stream by one byte.</summary>
		/// <returns>
		///   <see langword="true" /> if the byte is nonzero; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06006903 RID: 26883 RVA: 0x00166A00 File Offset: 0x00164C00
		public virtual bool ReadBoolean()
		{
			this.FillBuffer(1);
			return this.m_buffer[0] > 0;
		}

		/// <summary>Reads the next byte from the current stream and advances the current position of the stream by one byte.</summary>
		/// <returns>The next byte read from the current stream.</returns>
		/// <exception cref="T:System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06006904 RID: 26884 RVA: 0x00166A14 File Offset: 0x00164C14
		public virtual byte ReadByte()
		{
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			int num = this.m_stream.ReadByte();
			if (num == -1)
			{
				__Error.EndOfFile();
			}
			return (byte)num;
		}

		/// <summary>Reads a signed byte from this stream and advances the current position of the stream by one byte.</summary>
		/// <returns>A signed byte read from the current stream.</returns>
		/// <exception cref="T:System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06006905 RID: 26885 RVA: 0x00166A38 File Offset: 0x00164C38
		[CLSCompliant(false)]
		public virtual sbyte ReadSByte()
		{
			this.FillBuffer(1);
			return (sbyte)this.m_buffer[0];
		}

		/// <summary>Reads the next character from the current stream and advances the current position of the stream in accordance with the <see langword="Encoding" /> used and the specific character being read from the stream.</summary>
		/// <returns>A character read from the current stream.</returns>
		/// <exception cref="T:System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ArgumentException">A surrogate character was read.</exception>
		// Token: 0x06006906 RID: 26886 RVA: 0x00166A4A File Offset: 0x00164C4A
		public virtual char ReadChar()
		{
			int num = this.Read();
			if (num == -1)
			{
				__Error.EndOfFile();
			}
			return (char)num;
		}

		/// <summary>Reads a 2-byte signed integer from the current stream and advances the current position of the stream by two bytes.</summary>
		/// <returns>A 2-byte signed integer read from the current stream.</returns>
		/// <exception cref="T:System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06006907 RID: 26887 RVA: 0x00166A5C File Offset: 0x00164C5C
		public virtual short ReadInt16()
		{
			this.FillBuffer(2);
			return (short)((int)this.m_buffer[0] | (int)this.m_buffer[1] << 8);
		}

		/// <summary>Reads a 2-byte unsigned integer from the current stream using little-endian encoding and advances the position of the stream by two bytes.</summary>
		/// <returns>A 2-byte unsigned integer read from this stream.</returns>
		/// <exception cref="T:System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06006908 RID: 26888 RVA: 0x00166A79 File Offset: 0x00164C79
		[CLSCompliant(false)]
		public virtual ushort ReadUInt16()
		{
			this.FillBuffer(2);
			return (ushort)((int)this.m_buffer[0] | (int)this.m_buffer[1] << 8);
		}

		/// <summary>Reads a 4-byte signed integer from the current stream and advances the current position of the stream by four bytes.</summary>
		/// <returns>A 4-byte signed integer read from the current stream.</returns>
		/// <exception cref="T:System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06006909 RID: 26889 RVA: 0x00166A98 File Offset: 0x00164C98
		public virtual int ReadInt32()
		{
			if (this.m_isMemoryStream)
			{
				if (this.m_stream == null)
				{
					__Error.FileNotOpen();
				}
				return (this.m_stream as MemoryStream).InternalReadInt32();
			}
			this.FillBuffer(4);
			return (int)this.m_buffer[0] | (int)this.m_buffer[1] << 8 | (int)this.m_buffer[2] << 16 | (int)this.m_buffer[3] << 24;
		}

		/// <summary>Reads a 4-byte unsigned integer from the current stream and advances the position of the stream by four bytes.</summary>
		/// <returns>A 4-byte unsigned integer read from this stream.</returns>
		/// <exception cref="T:System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x0600690A RID: 26890 RVA: 0x00166AFD File Offset: 0x00164CFD
		[CLSCompliant(false)]
		public virtual uint ReadUInt32()
		{
			this.FillBuffer(4);
			return (uint)((int)this.m_buffer[0] | (int)this.m_buffer[1] << 8 | (int)this.m_buffer[2] << 16 | (int)this.m_buffer[3] << 24);
		}

		/// <summary>Reads an 8-byte signed integer from the current stream and advances the current position of the stream by eight bytes.</summary>
		/// <returns>An 8-byte signed integer read from the current stream.</returns>
		/// <exception cref="T:System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x0600690B RID: 26891 RVA: 0x00166B34 File Offset: 0x00164D34
		public virtual long ReadInt64()
		{
			this.FillBuffer(8);
			uint num = (uint)((int)this.m_buffer[0] | (int)this.m_buffer[1] << 8 | (int)this.m_buffer[2] << 16 | (int)this.m_buffer[3] << 24);
			return (long)((ulong)((int)this.m_buffer[4] | (int)this.m_buffer[5] << 8 | (int)this.m_buffer[6] << 16 | (int)this.m_buffer[7] << 24) << 32 | (ulong)num);
		}

		/// <summary>Reads an 8-byte unsigned integer from the current stream and advances the position of the stream by eight bytes.</summary>
		/// <returns>An 8-byte unsigned integer read from this stream.</returns>
		/// <exception cref="T:System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x0600690C RID: 26892 RVA: 0x00166BA8 File Offset: 0x00164DA8
		[CLSCompliant(false)]
		public virtual ulong ReadUInt64()
		{
			this.FillBuffer(8);
			uint num = (uint)((int)this.m_buffer[0] | (int)this.m_buffer[1] << 8 | (int)this.m_buffer[2] << 16 | (int)this.m_buffer[3] << 24);
			return (ulong)((int)this.m_buffer[4] | (int)this.m_buffer[5] << 8 | (int)this.m_buffer[6] << 16 | (int)this.m_buffer[7] << 24) << 32 | (ulong)num;
		}

		/// <summary>Reads a 4-byte floating point value from the current stream and advances the current position of the stream by four bytes.</summary>
		/// <returns>A 4-byte floating point value read from the current stream.</returns>
		/// <exception cref="T:System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x0600690D RID: 26893 RVA: 0x00166C1A File Offset: 0x00164E1A
		[SecuritySafeCritical]
		public virtual float ReadSingle()
		{
			this.FillBuffer(4);
			return BitConverterLE.ToSingle(this.m_buffer, 0);
		}

		/// <summary>Reads an 8-byte floating point value from the current stream and advances the current position of the stream by eight bytes.</summary>
		/// <returns>An 8-byte floating point value read from the current stream.</returns>
		/// <exception cref="T:System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x0600690E RID: 26894 RVA: 0x00166C2F File Offset: 0x00164E2F
		[SecuritySafeCritical]
		public virtual double ReadDouble()
		{
			this.FillBuffer(8);
			return BitConverterLE.ToDouble(this.m_buffer, 0);
		}

		/// <summary>Reads a decimal value from the current stream and advances the current position of the stream by sixteen bytes.</summary>
		/// <returns>A decimal value read from the current stream.</returns>
		/// <exception cref="T:System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x0600690F RID: 26895 RVA: 0x00166C44 File Offset: 0x00164E44
		public virtual decimal ReadDecimal()
		{
			this.FillBuffer(16);
			decimal result;
			try
			{
				int[] array = new int[4];
				Buffer.BlockCopy(this.m_buffer, 0, array, 0, 16);
				if (!BitConverter.IsLittleEndian)
				{
					for (int i = 0; i < 4; i++)
					{
						array[i] = BinaryPrimitives.ReverseEndianness(array[i]);
					}
				}
				result = new decimal(array);
			}
			catch (ArgumentException innerException)
			{
				throw new IOException(Environment.GetResourceString("Decimal byte array constructor requires an array of length four containing valid decimal bytes."), innerException);
			}
			return result;
		}

		/// <summary>Reads a string from the current stream. The string is prefixed with the length, encoded as an integer seven bits at a time.</summary>
		/// <returns>The string being read.</returns>
		/// <exception cref="T:System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06006910 RID: 26896 RVA: 0x00166CBC File Offset: 0x00164EBC
		public virtual string ReadString()
		{
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			int num = 0;
			int num2 = this.Read7BitEncodedInt();
			if (num2 < 0)
			{
				throw new IOException(Environment.GetResourceString("BinaryReader encountered an invalid string length of {0} characters.", new object[]
				{
					num2
				}));
			}
			if (num2 == 0)
			{
				return string.Empty;
			}
			if (this.m_charBytes == null)
			{
				this.m_charBytes = new byte[128];
			}
			if (this.m_charBuffer == null)
			{
				this.m_charBuffer = new char[this.m_maxCharsSize];
			}
			StringBuilder stringBuilder = null;
			int chars;
			for (;;)
			{
				int count = (num2 - num > 128) ? 128 : (num2 - num);
				int num3 = this.m_stream.Read(this.m_charBytes, 0, count);
				if (num3 == 0)
				{
					__Error.EndOfFile();
				}
				chars = this.m_decoder.GetChars(this.m_charBytes, 0, num3, this.m_charBuffer, 0);
				if (num == 0 && num3 == num2)
				{
					break;
				}
				if (stringBuilder == null)
				{
					stringBuilder = StringBuilderCache.Acquire(num2);
				}
				stringBuilder.Append(this.m_charBuffer, 0, chars);
				num += num3;
				if (num >= num2)
				{
					goto Block_11;
				}
			}
			return new string(this.m_charBuffer, 0, chars);
			Block_11:
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		/// <summary>Reads the specified number of characters from the stream, starting from a specified point in the character array.</summary>
		/// <param name="buffer">The buffer to read data into.</param>
		/// <param name="index">The starting point in the buffer at which to begin reading into the buffer.</param>
		/// <param name="count">The number of characters to read.</param>
		/// <returns>The total number of characters read into the buffer. This might be less than the number of characters requested if that many characters are not currently available, or it might be zero if the end of the stream is reached.</returns>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.  
		///  -or-  
		///  The number of decoded characters to read is greater than <paramref name="count" />. This can happen if a Unicode decoder returns fallback characters or a surrogate pair.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06006911 RID: 26897 RVA: 0x00166DD4 File Offset: 0x00164FD4
		[SecuritySafeCritical]
		public virtual int Read(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			return this.InternalReadChars(buffer, index, count);
		}

		// Token: 0x06006912 RID: 26898 RVA: 0x00166E5C File Offset: 0x0016505C
		[SecurityCritical]
		private unsafe int InternalReadChars(char[] buffer, int index, int count)
		{
			int i = count;
			if (this.m_charBytes == null)
			{
				this.m_charBytes = new byte[128];
			}
			while (i > 0)
			{
				int num = i;
				DecoderNLS decoderNLS = this.m_decoder as DecoderNLS;
				if (decoderNLS != null && decoderNLS.HasState && num > 1)
				{
					num--;
				}
				if (this.m_2BytesPerChar)
				{
					num <<= 1;
				}
				if (num > 128)
				{
					num = 128;
				}
				int num2 = 0;
				byte[] array;
				if (this.m_isMemoryStream)
				{
					MemoryStream memoryStream = this.m_stream as MemoryStream;
					num2 = memoryStream.InternalGetPosition();
					num = memoryStream.InternalEmulateRead(num);
					array = memoryStream.InternalGetBuffer();
				}
				else
				{
					num = this.m_stream.Read(this.m_charBytes, 0, num);
					array = this.m_charBytes;
				}
				if (num == 0)
				{
					return count - i;
				}
				int chars;
				checked
				{
					if (num2 < 0 || num < 0 || num2 + num > array.Length)
					{
						throw new ArgumentOutOfRangeException("byteCount");
					}
					if (index < 0 || i < 0 || index + i > buffer.Length)
					{
						throw new ArgumentOutOfRangeException("charsRemaining");
					}
					byte[] array2;
					byte* ptr;
					if ((array2 = array) == null || array2.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array2[0];
					}
					fixed (char[] array3 = buffer)
					{
						char* ptr2;
						if (buffer == null || array3.Length == 0)
						{
							ptr2 = null;
						}
						else
						{
							ptr2 = &array3[0];
						}
						chars = this.m_decoder.GetChars(ptr + num2, num, ptr2 + index, i, false);
					}
					array2 = null;
				}
				i -= chars;
				index += chars;
			}
			return count - i;
		}

		// Token: 0x06006913 RID: 26899 RVA: 0x00166FC8 File Offset: 0x001651C8
		private int InternalReadOneChar()
		{
			int num = 0;
			long num2 = 0L;
			if (this.m_stream.CanSeek)
			{
				num2 = this.m_stream.Position;
			}
			if (this.m_charBytes == null)
			{
				this.m_charBytes = new byte[128];
			}
			if (this.m_singleChar == null)
			{
				this.m_singleChar = new char[1];
			}
			while (num == 0)
			{
				int num3 = this.m_2BytesPerChar ? 2 : 1;
				int num4 = this.m_stream.ReadByte();
				this.m_charBytes[0] = (byte)num4;
				if (num4 == -1)
				{
					num3 = 0;
				}
				if (num3 == 2)
				{
					num4 = this.m_stream.ReadByte();
					this.m_charBytes[1] = (byte)num4;
					if (num4 == -1)
					{
						num3 = 1;
					}
				}
				if (num3 == 0)
				{
					return -1;
				}
				try
				{
					num = this.m_decoder.GetChars(this.m_charBytes, 0, num3, this.m_singleChar, 0);
				}
				catch
				{
					if (this.m_stream.CanSeek)
					{
						this.m_stream.Seek(num2 - this.m_stream.Position, SeekOrigin.Current);
					}
					throw;
				}
			}
			if (num == 0)
			{
				return -1;
			}
			return (int)this.m_singleChar[0];
		}

		/// <summary>Reads the specified number of characters from the current stream, returns the data in a character array, and advances the current position in accordance with the <see langword="Encoding" /> used and the specific character being read from the stream.</summary>
		/// <param name="count">The number of characters to read.</param>
		/// <returns>A character array containing data read from the underlying stream. This might be less than the number of characters requested if the end of the stream is reached.</returns>
		/// <exception cref="T:System.ArgumentException">The number of decoded characters to read is greater than <paramref name="count" />. This can happen if a Unicode decoder returns fallback characters or a surrogate pair.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is negative.</exception>
		// Token: 0x06006914 RID: 26900 RVA: 0x001670E4 File Offset: 0x001652E4
		[SecuritySafeCritical]
		public virtual char[] ReadChars(int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			if (count == 0)
			{
				return EmptyArray<char>.Value;
			}
			char[] array = new char[count];
			int num = this.InternalReadChars(array, 0, count);
			if (num != count)
			{
				char[] array2 = new char[num];
				Buffer.InternalBlockCopy(array, 0, array2, 0, 2 * num);
				array = array2;
			}
			return array;
		}

		// Token: 0x06006915 RID: 26901 RVA: 0x0016714C File Offset: 0x0016534C
		public virtual int Read(Span<char> buffer)
		{
			char[] array = ArrayPool<char>.Shared.Rent(buffer.Length);
			int result;
			try
			{
				int num = this.InternalReadChars(array, 0, buffer.Length);
				if (num > buffer.Length)
				{
					throw new IOException("Stream was too long.");
				}
				new ReadOnlySpan<char>(array, 0, num).CopyTo(buffer);
				result = num;
			}
			finally
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
			return result;
		}

		// Token: 0x06006916 RID: 26902 RVA: 0x001671C4 File Offset: 0x001653C4
		public virtual int Read(Span<byte> buffer)
		{
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			return this.m_stream.Read(buffer);
		}

		/// <summary>Reads the specified number of bytes from the stream, starting from a specified point in the byte array.</summary>
		/// <param name="buffer">The buffer to read data into.</param>
		/// <param name="index">The starting point in the buffer at which to begin reading into the buffer.</param>
		/// <param name="count">The number of bytes to read.</param>
		/// <returns>The number of bytes read into <paramref name="buffer" />. This might be less than the number of bytes requested if that many bytes are not available, or it might be zero if the end of the stream is reached.</returns>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.  
		///  -or-  
		///  The number of decoded characters to read is greater than <paramref name="count" />. This can happen if a Unicode decoder returns fallback characters or a surrogate pair.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06006917 RID: 26903 RVA: 0x001671E0 File Offset: 0x001653E0
		public virtual int Read(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			return this.m_stream.Read(buffer, index, count);
		}

		/// <summary>Reads the specified number of bytes from the current stream into a byte array and advances the current position by that number of bytes.</summary>
		/// <param name="count">The number of bytes to read. This value must be 0 or a non-negative number or an exception will occur.</param>
		/// <returns>A byte array containing data read from the underlying stream. This might be less than the number of bytes requested if the end of the stream is reached.</returns>
		/// <exception cref="T:System.ArgumentException">The number of decoded characters to read is greater than <paramref name="count" />. This can happen if a Unicode decoder returns fallback characters or a surrogate pair.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is negative.</exception>
		// Token: 0x06006918 RID: 26904 RVA: 0x0016726C File Offset: 0x0016546C
		public virtual byte[] ReadBytes(int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			if (count == 0)
			{
				return EmptyArray<byte>.Value;
			}
			byte[] array = new byte[count];
			int num = 0;
			do
			{
				int num2 = this.m_stream.Read(array, num, count);
				if (num2 == 0)
				{
					break;
				}
				num += num2;
				count -= num2;
			}
			while (count > 0);
			if (num != array.Length)
			{
				byte[] array2 = new byte[num];
				Buffer.InternalBlockCopy(array, 0, array2, 0, num);
				array = array2;
			}
			return array;
		}

		/// <summary>Fills the internal buffer with the specified number of bytes read from the stream.</summary>
		/// <param name="numBytes">The number of bytes to be read.</param>
		/// <exception cref="T:System.IO.EndOfStreamException">The end of the stream is reached before <paramref name="numBytes" /> could be read.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Requested <paramref name="numBytes" /> is larger than the internal buffer size.</exception>
		// Token: 0x06006919 RID: 26905 RVA: 0x001672EC File Offset: 0x001654EC
		protected virtual void FillBuffer(int numBytes)
		{
			if (this.m_buffer != null && (numBytes < 0 || numBytes > this.m_buffer.Length))
			{
				throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("The number of bytes requested does not fit into BinaryReader's internal buffer."));
			}
			int num = 0;
			if (this.m_stream == null)
			{
				__Error.FileNotOpen();
			}
			if (numBytes == 1)
			{
				int num2 = this.m_stream.ReadByte();
				if (num2 == -1)
				{
					__Error.EndOfFile();
				}
				this.m_buffer[0] = (byte)num2;
				return;
			}
			do
			{
				int num2 = this.m_stream.Read(this.m_buffer, num, numBytes - num);
				if (num2 == 0)
				{
					__Error.EndOfFile();
				}
				num += num2;
			}
			while (num < numBytes);
		}

		/// <summary>Reads in a 32-bit integer in compressed format.</summary>
		/// <returns>A 32-bit integer in compressed format.</returns>
		/// <exception cref="T:System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.FormatException">The stream is corrupted.</exception>
		// Token: 0x0600691A RID: 26906 RVA: 0x00167380 File Offset: 0x00165580
		protected internal int Read7BitEncodedInt()
		{
			int num = 0;
			int num2 = 0;
			while (num2 != 35)
			{
				byte b = this.ReadByte();
				num |= (int)(b & 127) << num2;
				num2 += 7;
				if ((b & 128) == 0)
				{
					return num;
				}
			}
			throw new FormatException(Environment.GetResourceString("Too many bytes in what should have been a 7 bit encoded Int32."));
		}

		// Token: 0x04003D0F RID: 15631
		private const int MaxCharBytesSize = 128;

		// Token: 0x04003D10 RID: 15632
		private Stream m_stream;

		// Token: 0x04003D11 RID: 15633
		private byte[] m_buffer;

		// Token: 0x04003D12 RID: 15634
		private Decoder m_decoder;

		// Token: 0x04003D13 RID: 15635
		private byte[] m_charBytes;

		// Token: 0x04003D14 RID: 15636
		private char[] m_singleChar;

		// Token: 0x04003D15 RID: 15637
		private char[] m_charBuffer;

		// Token: 0x04003D16 RID: 15638
		private int m_maxCharsSize;

		// Token: 0x04003D17 RID: 15639
		private bool m_2BytesPerChar;

		// Token: 0x04003D18 RID: 15640
		private bool m_isMemoryStream;

		// Token: 0x04003D19 RID: 15641
		private bool m_leaveOpen;
	}
}
