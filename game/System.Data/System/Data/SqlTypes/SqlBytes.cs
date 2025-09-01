using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a mutable reference type that wraps either a <see cref="P:System.Data.SqlTypes.SqlBytes.Buffer" /> or a <see cref="P:System.Data.SqlTypes.SqlBytes.Stream" />.</summary>
	// Token: 0x02000309 RID: 777
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public sealed class SqlBytes : INullable, IXmlSerializable, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBytes" /> class.</summary>
		// Token: 0x060022E9 RID: 8937 RVA: 0x000A03D7 File Offset: 0x0009E5D7
		public SqlBytes()
		{
			this.SetNull();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBytes" /> class based on the specified byte array.</summary>
		/// <param name="buffer">The array of unsigned bytes.</param>
		// Token: 0x060022EA RID: 8938 RVA: 0x000A03E8 File Offset: 0x0009E5E8
		public SqlBytes(byte[] buffer)
		{
			this._rgbBuf = buffer;
			this._stream = null;
			if (this._rgbBuf == null)
			{
				this._state = SqlBytesCharsState.Null;
				this._lCurLen = -1L;
			}
			else
			{
				this._state = SqlBytesCharsState.Buffer;
				this._lCurLen = (long)this._rgbBuf.Length;
			}
			this._rgbWorkBuf = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBytes" /> class based on the specified <see cref="T:System.Data.SqlTypes.SqlBinary" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> value.</param>
		// Token: 0x060022EB RID: 8939 RVA: 0x000A043F File Offset: 0x0009E63F
		public SqlBytes(SqlBinary value) : this(value.IsNull ? null : value.Value)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBytes" /> class based on the specified <see cref="T:System.IO.Stream" /> value.</summary>
		/// <param name="s">A <see cref="T:System.IO.Stream" />.</param>
		// Token: 0x060022EC RID: 8940 RVA: 0x000A045A File Offset: 0x0009E65A
		public SqlBytes(Stream s)
		{
			this._rgbBuf = null;
			this._lCurLen = -1L;
			this._stream = s;
			this._state = ((s == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Stream);
			this._rgbWorkBuf = null;
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlBytes" /> is null.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.SqlTypes.SqlBytes" /> is null, <see langword="false" /> otherwise.</returns>
		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060022ED RID: 8941 RVA: 0x000A048C File Offset: 0x0009E68C
		public bool IsNull
		{
			get
			{
				return this._state == SqlBytesCharsState.Null;
			}
		}

		/// <summary>Returns a reference to the internal buffer.</summary>
		/// <returns>A reference to the internal buffer. For <see cref="T:System.Data.SqlTypes.SqlBytes" /> instances created on top of unmanaged pointers, it returns a managed copy of the internal buffer.</returns>
		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x000A0497 File Offset: 0x0009E697
		public byte[] Buffer
		{
			get
			{
				if (this.FStream())
				{
					this.CopyStreamToBuffer();
				}
				return this._rgbBuf;
			}
		}

		/// <summary>Gets the length of the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <returns>A <see cref="T:System.Int64" /> value representing the length of the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.  
		///  Returns -1 if no buffer is available to the instance or if the value is null.  
		///  Returns a <see cref="P:System.IO.Stream.Length" /> for a stream-wrapped instance.</returns>
		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x000A04B0 File Offset: 0x0009E6B0
		public long Length
		{
			get
			{
				SqlBytesCharsState state = this._state;
				if (state == SqlBytesCharsState.Null)
				{
					throw new SqlNullValueException();
				}
				if (state != SqlBytesCharsState.Stream)
				{
					return this._lCurLen;
				}
				return this._stream.Length;
			}
		}

		/// <summary>Gets the maximum length of the value of the internal buffer of this <see cref="T:System.Data.SqlTypes.SqlBytes" />.</summary>
		/// <returns>A long representing the maximum length of the value of the internal buffer. Returns -1 for a stream-wrapped <see cref="T:System.Data.SqlTypes.SqlBytes" />.</returns>
		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060022F0 RID: 8944 RVA: 0x000A04E5 File Offset: 0x0009E6E5
		public long MaxLength
		{
			get
			{
				if (this._state == SqlBytesCharsState.Stream)
				{
					return -1L;
				}
				if (this._rgbBuf != null)
				{
					return (long)this._rgbBuf.Length;
				}
				return -1L;
			}
		}

		/// <summary>Returns a managed copy of the value held by this <see cref="T:System.Data.SqlTypes.SqlBytes" />.</summary>
		/// <returns>The value of this <see cref="T:System.Data.SqlTypes.SqlBytes" /> as an array of bytes.</returns>
		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060022F1 RID: 8945 RVA: 0x000A0508 File Offset: 0x0009E708
		public byte[] Value
		{
			get
			{
				SqlBytesCharsState state = this._state;
				if (state != SqlBytesCharsState.Null)
				{
					byte[] array;
					if (state != SqlBytesCharsState.Stream)
					{
						array = new byte[this._lCurLen];
						Array.Copy(this._rgbBuf, 0, array, 0, (int)this._lCurLen);
					}
					else
					{
						if (this._stream.Length > 2147483647L)
						{
							throw new SqlTypeException("The buffer is insufficient. Read or write operation failed.");
						}
						array = new byte[this._stream.Length];
						if (this._stream.Position != 0L)
						{
							this._stream.Seek(0L, SeekOrigin.Begin);
						}
						this._stream.Read(array, 0, checked((int)this._stream.Length));
					}
					return array;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance at the specified index.</summary>
		/// <param name="offset">A <see cref="T:System.Int64" /> value.</param>
		/// <returns>A <see cref="T:System.Byte" /> value.</returns>
		// Token: 0x1700063B RID: 1595
		public byte this[long offset]
		{
			get
			{
				if (offset < 0L || offset >= this.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if (this._rgbWorkBuf == null)
				{
					this._rgbWorkBuf = new byte[1];
				}
				this.Read(offset, this._rgbWorkBuf, 0, 1);
				return this._rgbWorkBuf[0];
			}
			set
			{
				if (this._rgbWorkBuf == null)
				{
					this._rgbWorkBuf = new byte[1];
				}
				this._rgbWorkBuf[0] = value;
				this.Write(offset, this._rgbWorkBuf, 0, 1);
			}
		}

		/// <summary>Returns information about the storage state of this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.StorageState" /> enumeration.</returns>
		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x000A0638 File Offset: 0x0009E838
		public StorageState Storage
		{
			get
			{
				switch (this._state)
				{
				case SqlBytesCharsState.Null:
					throw new SqlNullValueException();
				case SqlBytesCharsState.Buffer:
					return StorageState.Buffer;
				case SqlBytesCharsState.Stream:
					return StorageState.Stream;
				}
				return StorageState.UnmanagedBuffer;
			}
		}

		/// <summary>Gets or sets the data of this <see cref="T:System.Data.SqlTypes.SqlBytes" /> as a stream.</summary>
		/// <returns>The stream that contains the SqlBytes data.</returns>
		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060022F5 RID: 8949 RVA: 0x000A066F File Offset: 0x0009E86F
		// (set) Token: 0x060022F6 RID: 8950 RVA: 0x000A0686 File Offset: 0x0009E886
		public Stream Stream
		{
			get
			{
				if (!this.FStream())
				{
					return new StreamOnSqlBytes(this);
				}
				return this._stream;
			}
			set
			{
				this._lCurLen = -1L;
				this._stream = value;
				this._state = ((value == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Stream);
			}
		}

		/// <summary>Sets this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance to null.</summary>
		// Token: 0x060022F7 RID: 8951 RVA: 0x000A06A4 File Offset: 0x0009E8A4
		public void SetNull()
		{
			this._lCurLen = -1L;
			this._stream = null;
			this._state = SqlBytesCharsState.Null;
		}

		/// <summary>Sets the length of this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <param name="value">The <see cref="T:System.Int64" /> long value representing the length.</param>
		// Token: 0x060022F8 RID: 8952 RVA: 0x000A06BC File Offset: 0x0009E8BC
		public void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			if (this.FStream())
			{
				this._stream.SetLength(value);
				return;
			}
			if (this._rgbBuf == null)
			{
				throw new SqlTypeException("There is no buffer. Read or write operation failed.");
			}
			if (value > (long)this._rgbBuf.Length)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			if (this.IsNull)
			{
				this._state = SqlBytesCharsState.Buffer;
			}
			this._lCurLen = value;
		}

		/// <summary>Copies bytes from this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance to the passed-in buffer and returns the number of copied bytes.</summary>
		/// <param name="offset">An <see cref="T:System.Int64" /> long value offset into the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</param>
		/// <param name="buffer">The byte array buffer to copy into.</param>
		/// <param name="offsetInBuffer">An <see cref="T:System.Int32" /> integer offset into the buffer to start copying into.</param>
		/// <param name="count">An <see cref="T:System.Int32" /> integer representing the number of bytes to copy.</param>
		/// <returns>An <see cref="T:System.Int64" /> long value representing the number of copied bytes.</returns>
		// Token: 0x060022F9 RID: 8953 RVA: 0x000A0730 File Offset: 0x0009E930
		public long Read(long offset, byte[] buffer, int offsetInBuffer, int count)
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset > this.Length || offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offsetInBuffer > buffer.Length || offsetInBuffer < 0)
			{
				throw new ArgumentOutOfRangeException("offsetInBuffer");
			}
			if (count < 0 || count > buffer.Length - offsetInBuffer)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if ((long)count > this.Length - offset)
			{
				count = (int)(this.Length - offset);
			}
			if (count != 0)
			{
				if (this._state == SqlBytesCharsState.Stream)
				{
					if (this._stream.Position != offset)
					{
						this._stream.Seek(offset, SeekOrigin.Begin);
					}
					this._stream.Read(buffer, offsetInBuffer, count);
				}
				else
				{
					Array.Copy(this._rgbBuf, offset, buffer, (long)offsetInBuffer, (long)count);
				}
			}
			return (long)count;
		}

		/// <summary>Copies bytes from the passed-in buffer to this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <param name="offset">An <see cref="T:System.Int64" /> long value offset into the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</param>
		/// <param name="buffer">The byte array buffer to copy into.</param>
		/// <param name="offsetInBuffer">An <see cref="T:System.Int32" /> integer offset into the buffer to start copying into.</param>
		/// <param name="count">An <see cref="T:System.Int32" /> integer representing the number of bytes to copy.</param>
		// Token: 0x060022FA RID: 8954 RVA: 0x000A0808 File Offset: 0x0009EA08
		public void Write(long offset, byte[] buffer, int offsetInBuffer, int count)
		{
			if (this.FStream())
			{
				if (this._stream.Position != offset)
				{
					this._stream.Seek(offset, SeekOrigin.Begin);
				}
				this._stream.Write(buffer, offsetInBuffer, count);
				return;
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (this._rgbBuf == null)
			{
				throw new SqlTypeException("There is no buffer. Read or write operation failed.");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset > (long)this._rgbBuf.Length)
			{
				throw new SqlTypeException("The buffer is insufficient. Read or write operation failed.");
			}
			if (offsetInBuffer < 0 || offsetInBuffer > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offsetInBuffer");
			}
			if (count < 0 || count > buffer.Length - offsetInBuffer)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if ((long)count > (long)this._rgbBuf.Length - offset)
			{
				throw new SqlTypeException("The buffer is insufficient. Read or write operation failed.");
			}
			if (this.IsNull)
			{
				if (offset != 0L)
				{
					throw new SqlTypeException("Cannot write to non-zero offset, because current value is Null.");
				}
				this._lCurLen = 0L;
				this._state = SqlBytesCharsState.Buffer;
			}
			else if (offset > this._lCurLen)
			{
				throw new SqlTypeException("Cannot write from an offset that is larger than current length. It would leave uninitialized data in the buffer.");
			}
			if (count != 0)
			{
				Array.Copy(buffer, (long)offsetInBuffer, this._rgbBuf, offset, (long)count);
				if (this._lCurLen < offset + (long)count)
				{
					this._lCurLen = offset + (long)count;
				}
			}
		}

		/// <summary>Constructs and returns a <see cref="T:System.Data.SqlTypes.SqlBinary" /> from this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBinary" /> from this instance.</returns>
		// Token: 0x060022FB RID: 8955 RVA: 0x000A0943 File Offset: 0x0009EB43
		public SqlBinary ToSqlBinary()
		{
			if (!this.IsNull)
			{
				return new SqlBinary(this.Value);
			}
			return SqlBinary.Null;
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlBytes" /> structure to a <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure.</summary>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlBytes" /> structure to be converted.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure.</returns>
		// Token: 0x060022FC RID: 8956 RVA: 0x000A095E File Offset: 0x0009EB5E
		public static explicit operator SqlBinary(SqlBytes value)
		{
			return value.ToSqlBinary();
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure to a <see cref="T:System.Data.SqlTypes.SqlBytes" /> structure.</summary>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure to be converted.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBytes" /> structure.</returns>
		// Token: 0x060022FD RID: 8957 RVA: 0x000A0966 File Offset: 0x0009EB66
		public static explicit operator SqlBytes(SqlBinary value)
		{
			return new SqlBytes(value);
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x000A096E File Offset: 0x0009EB6E
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			bool isNull = this.IsNull;
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x000A0978 File Offset: 0x0009EB78
		private void CopyStreamToBuffer()
		{
			long length = this._stream.Length;
			if (length >= 2147483647L)
			{
				throw new SqlTypeException("Cannot write from an offset that is larger than current length. It would leave uninitialized data in the buffer.");
			}
			if (this._rgbBuf == null || (long)this._rgbBuf.Length < length)
			{
				this._rgbBuf = new byte[length];
			}
			if (this._stream.Position != 0L)
			{
				this._stream.Seek(0L, SeekOrigin.Begin);
			}
			this._stream.Read(this._rgbBuf, 0, (int)length);
			this._stream = null;
			this._lCurLen = length;
			this._state = SqlBytesCharsState.Buffer;
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x000A0A0C File Offset: 0x0009EC0C
		internal bool FStream()
		{
			return this._state == SqlBytesCharsState.Stream;
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x000A0A17 File Offset: 0x0009EC17
		private void SetBuffer(byte[] buffer)
		{
			this._rgbBuf = buffer;
			this._lCurLen = ((this._rgbBuf == null) ? -1L : ((long)this._rgbBuf.Length));
			this._stream = null;
			this._state = ((this._rgbBuf == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Buffer);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</returns>
		// Token: 0x06002302 RID: 8962 RVA: 0x00003E32 File Offset: 0x00002032
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="r">
		///   <see langword="XmlReader" />
		/// </param>
		// Token: 0x06002303 RID: 8963 RVA: 0x000A0A54 File Offset: 0x0009EC54
		void IXmlSerializable.ReadXml(XmlReader r)
		{
			byte[] buffer = null;
			string attribute = r.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				r.ReadElementString();
				this.SetNull();
			}
			else
			{
				string text = r.ReadElementString();
				if (text == null)
				{
					buffer = Array.Empty<byte>();
				}
				else
				{
					text = text.Trim();
					if (text.Length == 0)
					{
						buffer = Array.Empty<byte>();
					}
					else
					{
						buffer = Convert.FromBase64String(text);
					}
				}
			}
			this.SetBuffer(buffer);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">
		///   <see langword="XmlWriter" />
		/// </param>
		// Token: 0x06002304 RID: 8964 RVA: 0x000A0AC8 File Offset: 0x0009ECC8
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			byte[] buffer = this.Buffer;
			writer.WriteString(Convert.ToBase64String(buffer, 0, (int)this.Length));
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		/// <returns>A <see langword="string" /> that indicates the XSD of the specified <see langword="XmlSchemaSet" />.</returns>
		// Token: 0x06002305 RID: 8965 RVA: 0x0009F3DB File Offset: 0x0009D5DB
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("base64Binary", "http://www.w3.org/2001/XMLSchema");
		}

		/// <summary>Gets serialization information with all the data needed to reinstantiate this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <param name="info">The object to be populated with serialization information.</param>
		/// <param name="context">The destination context of the serialization.</param>
		// Token: 0x06002306 RID: 8966 RVA: 0x0004549D File Offset: 0x0004369D
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Gets a null instance of this <see cref="T:System.Data.SqlTypes.SqlBytes" />.</summary>
		/// <returns>An instance whose <see cref="P:System.Data.SqlTypes.SqlBytes.IsNull" /> property returns <see langword="true" />.</returns>
		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x000A0B13 File Offset: 0x0009ED13
		public static SqlBytes Null
		{
			get
			{
				return new SqlBytes(null);
			}
		}

		// Token: 0x04001838 RID: 6200
		internal byte[] _rgbBuf;

		// Token: 0x04001839 RID: 6201
		private long _lCurLen;

		// Token: 0x0400183A RID: 6202
		internal Stream _stream;

		// Token: 0x0400183B RID: 6203
		private SqlBytesCharsState _state;

		// Token: 0x0400183C RID: 6204
		private byte[] _rgbWorkBuf;

		// Token: 0x0400183D RID: 6205
		private const long x_lMaxLen = 2147483647L;

		// Token: 0x0400183E RID: 6206
		private const long x_lNull = -1L;
	}
}
