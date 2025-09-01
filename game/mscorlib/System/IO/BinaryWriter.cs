using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Mono.Security;

namespace System.IO
{
	/// <summary>Writes primitive types in binary to a stream and supports writing strings in a specific encoding.</summary>
	// Token: 0x02000B58 RID: 2904
	[ComVisible(true)]
	[Serializable]
	public class BinaryWriter : IDisposable, IAsyncDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.BinaryWriter" /> class that writes to a stream.</summary>
		// Token: 0x0600691B RID: 26907 RVA: 0x001673C7 File Offset: 0x001655C7
		protected BinaryWriter()
		{
			this.OutStream = Stream.Null;
			this._buffer = new byte[16];
			this._encoding = new UTF8Encoding(false, true);
			this._encoder = this._encoding.GetEncoder();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.BinaryWriter" /> class based on the specified stream and using UTF-8 encoding.</summary>
		/// <param name="output">The output stream.</param>
		/// <exception cref="T:System.ArgumentException">The stream does not support writing or is already closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="output" /> is <see langword="null" />.</exception>
		// Token: 0x0600691C RID: 26908 RVA: 0x00167405 File Offset: 0x00165605
		public BinaryWriter(Stream output) : this(output, new UTF8Encoding(false, true), false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.BinaryWriter" /> class based on the specified stream and character encoding.</summary>
		/// <param name="output">The output stream.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <exception cref="T:System.ArgumentException">The stream does not support writing or is already closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="output" /> or <paramref name="encoding" /> is <see langword="null" />.</exception>
		// Token: 0x0600691D RID: 26909 RVA: 0x00167416 File Offset: 0x00165616
		public BinaryWriter(Stream output, Encoding encoding) : this(output, encoding, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.BinaryWriter" /> class based on the specified stream and character encoding, and optionally leaves the stream open.</summary>
		/// <param name="output">The output stream.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="leaveOpen">
		///   <see langword="true" /> to leave the stream open after the <see cref="T:System.IO.BinaryWriter" /> object is disposed; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">The stream does not support writing or is already closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="output" /> or <paramref name="encoding" /> is <see langword="null" />.</exception>
		// Token: 0x0600691E RID: 26910 RVA: 0x00167424 File Offset: 0x00165624
		public BinaryWriter(Stream output, Encoding encoding, bool leaveOpen)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (!output.CanWrite)
			{
				throw new ArgumentException(Environment.GetResourceString("Stream was not writable."));
			}
			this.OutStream = output;
			this._buffer = new byte[16];
			this._encoding = encoding;
			this._encoder = this._encoding.GetEncoder();
			this._leaveOpen = leaveOpen;
		}

		/// <summary>Closes the current <see cref="T:System.IO.BinaryWriter" /> and the underlying stream.</summary>
		// Token: 0x0600691F RID: 26911 RVA: 0x0016749E File Offset: 0x0016569E
		public virtual void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.BinaryWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06006920 RID: 26912 RVA: 0x001674A7 File Offset: 0x001656A7
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._leaveOpen)
				{
					this.OutStream.Flush();
					return;
				}
				this.OutStream.Close();
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.IO.BinaryWriter" /> class.</summary>
		// Token: 0x06006921 RID: 26913 RVA: 0x0016749E File Offset: 0x0016569E
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Gets the underlying stream of the <see cref="T:System.IO.BinaryWriter" />.</summary>
		/// <returns>The underlying stream associated with the <see langword="BinaryWriter" />.</returns>
		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x06006922 RID: 26914 RVA: 0x001674CB File Offset: 0x001656CB
		public virtual Stream BaseStream
		{
			get
			{
				this.Flush();
				return this.OutStream;
			}
		}

		/// <summary>Clears all buffers for the current writer and causes any buffered data to be written to the underlying device.</summary>
		// Token: 0x06006923 RID: 26915 RVA: 0x001674D9 File Offset: 0x001656D9
		public virtual void Flush()
		{
			this.OutStream.Flush();
		}

		/// <summary>Sets the position within the current stream.</summary>
		/// <param name="offset">A byte offset relative to <paramref name="origin" />.</param>
		/// <param name="origin">A field of <see cref="T:System.IO.SeekOrigin" /> indicating the reference point from which the new position is to be obtained.</param>
		/// <returns>The position with the current stream.</returns>
		/// <exception cref="T:System.IO.IOException">The file pointer was moved to an invalid location.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.IO.SeekOrigin" /> value is invalid.</exception>
		// Token: 0x06006924 RID: 26916 RVA: 0x001674E6 File Offset: 0x001656E6
		public virtual long Seek(int offset, SeekOrigin origin)
		{
			return this.OutStream.Seek((long)offset, origin);
		}

		// Token: 0x06006925 RID: 26917 RVA: 0x001674F6 File Offset: 0x001656F6
		public virtual void Write(ReadOnlySpan<byte> buffer)
		{
			this.Write(buffer.ToArray());
		}

		// Token: 0x06006926 RID: 26918 RVA: 0x00167505 File Offset: 0x00165705
		public virtual void Write(ReadOnlySpan<char> buffer)
		{
			this.Write(buffer.ToArray());
		}

		// Token: 0x06006927 RID: 26919 RVA: 0x00167514 File Offset: 0x00165714
		public virtual ValueTask DisposeAsync()
		{
			ValueTask result;
			try
			{
				if (base.GetType() == typeof(BinaryWriter))
				{
					if (this._leaveOpen)
					{
						return new ValueTask(this.OutStream.FlushAsync());
					}
					this.OutStream.Close();
				}
				else
				{
					this.Dispose();
				}
				result = default(ValueTask);
			}
			catch (Exception exception)
			{
				result = new ValueTask(Task.FromException(exception));
			}
			return result;
		}

		/// <summary>Writes a one-byte <see langword="Boolean" /> value to the current stream, with 0 representing <see langword="false" /> and 1 representing <see langword="true" />.</summary>
		/// <param name="value">The <see langword="Boolean" /> value to write (0 or 1).</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x06006928 RID: 26920 RVA: 0x00167590 File Offset: 0x00165790
		public virtual void Write(bool value)
		{
			this._buffer[0] = (value ? 1 : 0);
			this.OutStream.Write(this._buffer, 0, 1);
		}

		/// <summary>Writes an unsigned byte to the current stream and advances the stream position by one byte.</summary>
		/// <param name="value">The unsigned byte to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x06006929 RID: 26921 RVA: 0x001675B5 File Offset: 0x001657B5
		public virtual void Write(byte value)
		{
			this.OutStream.WriteByte(value);
		}

		/// <summary>Writes a signed byte to the current stream and advances the stream position by one byte.</summary>
		/// <param name="value">The signed byte to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x0600692A RID: 26922 RVA: 0x001675C3 File Offset: 0x001657C3
		[CLSCompliant(false)]
		public virtual void Write(sbyte value)
		{
			this.OutStream.WriteByte((byte)value);
		}

		/// <summary>Writes a byte array to the underlying stream.</summary>
		/// <param name="buffer">A byte array containing the data to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		// Token: 0x0600692B RID: 26923 RVA: 0x001675D2 File Offset: 0x001657D2
		public virtual void Write(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.OutStream.Write(buffer, 0, buffer.Length);
		}

		/// <summary>Writes a region of a byte array to the current stream.</summary>
		/// <param name="buffer">A byte array containing the data to write.</param>
		/// <param name="index">The index of the first byte to read from <paramref name="buffer" /> and to write to the stream.</param>
		/// <param name="count">The number of bytes to read from <paramref name="buffer" /> and to write to the stream.</param>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x0600692C RID: 26924 RVA: 0x001675F2 File Offset: 0x001657F2
		public virtual void Write(byte[] buffer, int index, int count)
		{
			this.OutStream.Write(buffer, index, count);
		}

		/// <summary>Writes a Unicode character to the current stream and advances the current position of the stream in accordance with the <see langword="Encoding" /> used and the specific characters being written to the stream.</summary>
		/// <param name="ch">The non-surrogate, Unicode character to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ch" /> is a single surrogate character.</exception>
		// Token: 0x0600692D RID: 26925 RVA: 0x00167604 File Offset: 0x00165804
		[SecuritySafeCritical]
		public unsafe virtual void Write(char ch)
		{
			if (char.IsSurrogate(ch))
			{
				throw new ArgumentException(Environment.GetResourceString("Unicode surrogate characters must be written out as pairs together in the same call, not individually. Consider passing in a character array instead."));
			}
			byte[] array;
			byte* bytes;
			if ((array = this._buffer) == null || array.Length == 0)
			{
				bytes = null;
			}
			else
			{
				bytes = &array[0];
			}
			int bytes2 = this._encoder.GetBytes(&ch, 1, bytes, this._buffer.Length, true);
			array = null;
			this.OutStream.Write(this._buffer, 0, bytes2);
		}

		/// <summary>Writes a character array to the current stream and advances the current position of the stream in accordance with the <see langword="Encoding" /> used and the specific characters being written to the stream.</summary>
		/// <param name="chars">A character array containing the data to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x0600692E RID: 26926 RVA: 0x00167678 File Offset: 0x00165878
		public virtual void Write(char[] chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			byte[] bytes = this._encoding.GetBytes(chars, 0, chars.Length);
			this.OutStream.Write(bytes, 0, bytes.Length);
		}

		/// <summary>Writes a section of a character array to the current stream, and advances the current position of the stream in accordance with the <see langword="Encoding" /> used and perhaps the specific characters being written to the stream.</summary>
		/// <param name="chars">A character array containing the data to write.</param>
		/// <param name="index">The index of the first character to read from <paramref name="chars" /> and to write to the stream.</param>
		/// <param name="count">The number of characters to read from <paramref name="chars" /> and to write to the stream.</param>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x0600692F RID: 26927 RVA: 0x001676B4 File Offset: 0x001658B4
		public virtual void Write(char[] chars, int index, int count)
		{
			byte[] bytes = this._encoding.GetBytes(chars, index, count);
			this.OutStream.Write(bytes, 0, bytes.Length);
		}

		/// <summary>Writes an eight-byte floating-point value to the current stream and advances the stream position by eight bytes.</summary>
		/// <param name="value">The eight-byte floating-point value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x06006930 RID: 26928 RVA: 0x001676E0 File Offset: 0x001658E0
		[SecuritySafeCritical]
		public virtual void Write(double value)
		{
			this.OutStream.Write(BitConverterLE.GetBytes(value), 0, 8);
		}

		/// <summary>Writes a decimal value to the current stream and advances the stream position by sixteen bytes.</summary>
		/// <param name="value">The decimal value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x06006931 RID: 26929 RVA: 0x001676F5 File Offset: 0x001658F5
		public virtual void Write(decimal value)
		{
			decimal.GetBytes(value, this._buffer);
			this.OutStream.Write(this._buffer, 0, 16);
		}

		/// <summary>Writes a two-byte signed integer to the current stream and advances the stream position by two bytes.</summary>
		/// <param name="value">The two-byte signed integer to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x06006932 RID: 26930 RVA: 0x00167718 File Offset: 0x00165918
		public virtual void Write(short value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this.OutStream.Write(this._buffer, 0, 2);
		}

		/// <summary>Writes a two-byte unsigned integer to the current stream and advances the stream position by two bytes.</summary>
		/// <param name="value">The two-byte unsigned integer to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x06006933 RID: 26931 RVA: 0x00167718 File Offset: 0x00165918
		[CLSCompliant(false)]
		public virtual void Write(ushort value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this.OutStream.Write(this._buffer, 0, 2);
		}

		/// <summary>Writes a four-byte signed integer to the current stream and advances the stream position by four bytes.</summary>
		/// <param name="value">The four-byte signed integer to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x06006934 RID: 26932 RVA: 0x00167744 File Offset: 0x00165944
		public virtual void Write(int value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this._buffer[2] = (byte)(value >> 16);
			this._buffer[3] = (byte)(value >> 24);
			this.OutStream.Write(this._buffer, 0, 4);
		}

		/// <summary>Writes a four-byte unsigned integer to the current stream and advances the stream position by four bytes.</summary>
		/// <param name="value">The four-byte unsigned integer to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x06006935 RID: 26933 RVA: 0x00167794 File Offset: 0x00165994
		[CLSCompliant(false)]
		public virtual void Write(uint value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this._buffer[2] = (byte)(value >> 16);
			this._buffer[3] = (byte)(value >> 24);
			this.OutStream.Write(this._buffer, 0, 4);
		}

		/// <summary>Writes an eight-byte signed integer to the current stream and advances the stream position by eight bytes.</summary>
		/// <param name="value">The eight-byte signed integer to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x06006936 RID: 26934 RVA: 0x001677E4 File Offset: 0x001659E4
		public virtual void Write(long value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this._buffer[2] = (byte)(value >> 16);
			this._buffer[3] = (byte)(value >> 24);
			this._buffer[4] = (byte)(value >> 32);
			this._buffer[5] = (byte)(value >> 40);
			this._buffer[6] = (byte)(value >> 48);
			this._buffer[7] = (byte)(value >> 56);
			this.OutStream.Write(this._buffer, 0, 8);
		}

		/// <summary>Writes an eight-byte unsigned integer to the current stream and advances the stream position by eight bytes.</summary>
		/// <param name="value">The eight-byte unsigned integer to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x06006937 RID: 26935 RVA: 0x00167868 File Offset: 0x00165A68
		[CLSCompliant(false)]
		public virtual void Write(ulong value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this._buffer[2] = (byte)(value >> 16);
			this._buffer[3] = (byte)(value >> 24);
			this._buffer[4] = (byte)(value >> 32);
			this._buffer[5] = (byte)(value >> 40);
			this._buffer[6] = (byte)(value >> 48);
			this._buffer[7] = (byte)(value >> 56);
			this.OutStream.Write(this._buffer, 0, 8);
		}

		/// <summary>Writes a four-byte floating-point value to the current stream and advances the stream position by four bytes.</summary>
		/// <param name="value">The four-byte floating-point value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x06006938 RID: 26936 RVA: 0x001678EC File Offset: 0x00165AEC
		[SecuritySafeCritical]
		public virtual void Write(float value)
		{
			this.OutStream.Write(BitConverterLE.GetBytes(value), 0, 4);
		}

		/// <summary>Writes a length-prefixed string to this stream in the current encoding of the <see cref="T:System.IO.BinaryWriter" />, and advances the current position of the stream in accordance with the encoding used and the specific characters being written to the stream.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x06006939 RID: 26937 RVA: 0x00167904 File Offset: 0x00165B04
		[SecuritySafeCritical]
		public unsafe virtual void Write(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int byteCount = this._encoding.GetByteCount(value);
			this.Write7BitEncodedInt(byteCount);
			if (this._largeByteBuffer == null)
			{
				this._largeByteBuffer = new byte[256];
				this._maxChars = this._largeByteBuffer.Length / this._encoding.GetMaxByteCount(1);
			}
			if (byteCount <= this._largeByteBuffer.Length)
			{
				this._encoding.GetBytes(value, 0, value.Length, this._largeByteBuffer, 0);
				this.OutStream.Write(this._largeByteBuffer, 0, byteCount);
				return;
			}
			int num = 0;
			int num2;
			for (int i = value.Length; i > 0; i -= num2)
			{
				num2 = ((i > this._maxChars) ? this._maxChars : i);
				if (num < 0 || num2 < 0 || checked(num + num2) > value.Length)
				{
					throw new ArgumentOutOfRangeException("charCount");
				}
				int bytes2;
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					byte[] array;
					byte* bytes;
					if ((array = this._largeByteBuffer) == null || array.Length == 0)
					{
						bytes = null;
					}
					else
					{
						bytes = &array[0];
					}
					bytes2 = this._encoder.GetBytes(checked(ptr + num), num2, bytes, this._largeByteBuffer.Length, num2 == i);
					array = null;
				}
				this.OutStream.Write(this._largeByteBuffer, 0, bytes2);
				num += num2;
			}
		}

		/// <summary>Writes a 32-bit integer in a compressed format.</summary>
		/// <param name="value">The 32-bit integer to be written.</param>
		/// <exception cref="T:System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed.</exception>
		// Token: 0x0600693A RID: 26938 RVA: 0x00167A64 File Offset: 0x00165C64
		protected void Write7BitEncodedInt(int value)
		{
			uint num;
			for (num = (uint)value; num >= 128U; num >>= 7)
			{
				this.Write((byte)(num | 128U));
			}
			this.Write((byte)num);
		}

		// Token: 0x0600693B RID: 26939 RVA: 0x00167A97 File Offset: 0x00165C97
		// Note: this type is marked as 'beforefieldinit'.
		static BinaryWriter()
		{
		}

		/// <summary>Specifies a <see cref="T:System.IO.BinaryWriter" /> with no backing store.</summary>
		// Token: 0x04003D1A RID: 15642
		public static readonly BinaryWriter Null = new BinaryWriter();

		/// <summary>Holds the underlying stream.</summary>
		// Token: 0x04003D1B RID: 15643
		protected Stream OutStream;

		// Token: 0x04003D1C RID: 15644
		private byte[] _buffer;

		// Token: 0x04003D1D RID: 15645
		private Encoding _encoding;

		// Token: 0x04003D1E RID: 15646
		private Encoder _encoder;

		// Token: 0x04003D1F RID: 15647
		[OptionalField]
		private bool _leaveOpen;

		// Token: 0x04003D20 RID: 15648
		[OptionalField]
		private char[] _tmpOneCharBuffer;

		// Token: 0x04003D21 RID: 15649
		private byte[] _largeByteBuffer;

		// Token: 0x04003D22 RID: 15650
		private int _maxChars;

		// Token: 0x04003D23 RID: 15651
		private const int LargeByteBufferSize = 256;
	}
}
