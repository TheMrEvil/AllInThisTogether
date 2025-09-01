using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>
	///   <see cref="T:System.Data.SqlTypes.SqlChars" /> is a mutable reference type that wraps a <see cref="T:System.Char" /> array or a <see cref="T:System.Data.SqlTypes.SqlString" /> instance.</summary>
	// Token: 0x0200030B RID: 779
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public sealed class SqlChars : INullable, IXmlSerializable, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlChars" /> class.</summary>
		// Token: 0x06002319 RID: 8985 RVA: 0x000A0EB0 File Offset: 0x0009F0B0
		public SqlChars()
		{
			this.SetNull();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlChars" /> class based on the specified character array.</summary>
		/// <param name="buffer">A <see cref="T:System.Char" /> array.</param>
		// Token: 0x0600231A RID: 8986 RVA: 0x000A0EC0 File Offset: 0x0009F0C0
		public SqlChars(char[] buffer)
		{
			this._rgchBuf = buffer;
			this._stream = null;
			if (this._rgchBuf == null)
			{
				this._state = SqlBytesCharsState.Null;
				this._lCurLen = -1L;
			}
			else
			{
				this._state = SqlBytesCharsState.Buffer;
				this._lCurLen = (long)this._rgchBuf.Length;
			}
			this._rgchWorkBuf = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlChars" /> class based on the specified <see cref="T:System.Data.SqlTypes.SqlString" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Data.SqlTypes.SqlString" />.</param>
		// Token: 0x0600231B RID: 8987 RVA: 0x000A0F17 File Offset: 0x0009F117
		public SqlChars(SqlString value) : this(value.IsNull ? null : value.Value.ToCharArray())
		{
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x000A0F37 File Offset: 0x0009F137
		internal SqlChars(SqlStreamChars s)
		{
			this._rgchBuf = null;
			this._lCurLen = -1L;
			this._stream = s;
			this._state = ((s == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Stream);
			this._rgchWorkBuf = null;
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlChars" /> is null.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.SqlTypes.SqlChars" /> is null. Otherwise, <see langword="false" />.</returns>
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x000A0F69 File Offset: 0x0009F169
		public bool IsNull
		{
			get
			{
				return this._state == SqlBytesCharsState.Null;
			}
		}

		/// <summary>Returns a reference to the internal buffer.</summary>
		/// <returns>A reference to the internal buffer. For <see cref="T:System.Data.SqlTypes.SqlChars" /> instances created on top of unmanaged pointers, it returns a managed copy of the internal buffer.</returns>
		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x000A0F74 File Offset: 0x0009F174
		public char[] Buffer
		{
			get
			{
				if (this.FStream())
				{
					this.CopyStreamToBuffer();
				}
				return this._rgchBuf;
			}
		}

		/// <summary>Gets the length of the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</summary>
		/// <returns>A <see cref="T:System.Int64" /> value that indicates the length in characters of the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.  
		///  Returns -1 if no buffer is available to the instance, or if the value is null.  
		///  Returns a <see cref="P:System.IO.Stream.Length" /> for a stream-wrapped instance.</returns>
		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x000A0F8C File Offset: 0x0009F18C
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

		/// <summary>Gets the maximum length in two-byte characters of the value the internal buffer can hold.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value representing the maximum length in two-byte characters of the value of the internal buffer.  
		///  Returns -1 for a stream-wrapped <see cref="T:System.Data.SqlTypes.SqlChars" />.</returns>
		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06002320 RID: 8992 RVA: 0x000A0FC1 File Offset: 0x0009F1C1
		public long MaxLength
		{
			get
			{
				if (this._state == SqlBytesCharsState.Stream)
				{
					return -1L;
				}
				if (this._rgchBuf != null)
				{
					return (long)this._rgchBuf.Length;
				}
				return -1L;
			}
		}

		/// <summary>Returns a managed copy of the value held by this <see cref="T:System.Data.SqlTypes.SqlChars" />.</summary>
		/// <returns>The value of this <see cref="T:System.Data.SqlTypes.SqlChars" /> as an array of characters.</returns>
		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06002321 RID: 8993 RVA: 0x000A0FE4 File Offset: 0x0009F1E4
		public char[] Value
		{
			get
			{
				SqlBytesCharsState state = this._state;
				if (state != SqlBytesCharsState.Null)
				{
					char[] array;
					if (state != SqlBytesCharsState.Stream)
					{
						array = new char[this._lCurLen];
						Array.Copy(this._rgchBuf, 0, array, 0, (int)this._lCurLen);
					}
					else
					{
						if (this._stream.Length > 2147483647L)
						{
							throw new SqlTypeException("The buffer is insufficient. Read or write operation failed.");
						}
						array = new char[this._stream.Length];
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

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlTypes.SqlChars" /> instance at the specified index.</summary>
		/// <param name="offset">An <see cref="T:System.Int64" /> value.</param>
		/// <returns>A <see cref="T:System.Char" /> value.</returns>
		// Token: 0x17000649 RID: 1609
		public char this[long offset]
		{
			get
			{
				if (offset < 0L || offset >= this.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if (this._rgchWorkBuf == null)
				{
					this._rgchWorkBuf = new char[1];
				}
				this.Read(offset, this._rgchWorkBuf, 0, 1);
				return this._rgchWorkBuf[0];
			}
			set
			{
				if (this._rgchWorkBuf == null)
				{
					this._rgchWorkBuf = new char[1];
				}
				this._rgchWorkBuf[0] = value;
				this.Write(offset, this._rgchWorkBuf, 0, 1);
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x000A1114 File Offset: 0x0009F314
		// (set) Token: 0x06002325 RID: 8997 RVA: 0x000A112B File Offset: 0x0009F32B
		internal SqlStreamChars Stream
		{
			get
			{
				if (!this.FStream())
				{
					return new StreamOnSqlChars(this);
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

		/// <summary>Returns information about the storage state of this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.StorageState" /> enumeration.</returns>
		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x000A114C File Offset: 0x0009F34C
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

		/// <summary>Sets this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance to null.</summary>
		// Token: 0x06002327 RID: 8999 RVA: 0x000A1183 File Offset: 0x0009F383
		public void SetNull()
		{
			this._lCurLen = -1L;
			this._stream = null;
			this._state = SqlBytesCharsState.Null;
		}

		/// <summary>Sets the length of this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</summary>
		/// <param name="value">The <see cref="T:System.Int64" /><see langword="long" /> value representing the length.</param>
		// Token: 0x06002328 RID: 9000 RVA: 0x000A119C File Offset: 0x0009F39C
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
			if (this._rgchBuf == null)
			{
				throw new SqlTypeException("There is no buffer. Read or write operation failed.");
			}
			if (value > (long)this._rgchBuf.Length)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			if (this.IsNull)
			{
				this._state = SqlBytesCharsState.Buffer;
			}
			this._lCurLen = value;
		}

		/// <summary>Copies characters from this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance to the passed-in buffer and returns the number of copied characters.</summary>
		/// <param name="offset">An <see cref="T:System.Int64" /><see langword="long" /> value offset into the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</param>
		/// <param name="buffer">The character array buffer to copy into.</param>
		/// <param name="offsetInBuffer">An <see cref="T:System.Int32" /> integer offset into the buffer to start copying into.</param>
		/// <param name="count">An <see cref="T:System.Int32" /> integer value representing the number of characters to copy.</param>
		/// <returns>An <see cref="T:System.Int64" /><see langword="long" /> value representing the number of copied bytes.</returns>
		// Token: 0x06002329 RID: 9001 RVA: 0x000A1210 File Offset: 0x0009F410
		public long Read(long offset, char[] buffer, int offsetInBuffer, int count)
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
					Array.Copy(this._rgchBuf, offset, buffer, (long)offsetInBuffer, (long)count);
				}
			}
			return (long)count;
		}

		/// <summary>Copies characters from the passed-in buffer to this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</summary>
		/// <param name="offset">A <see langword="long" /> value offset into the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</param>
		/// <param name="buffer">The character array buffer to copy into.</param>
		/// <param name="offsetInBuffer">An <see cref="T:System.Int32" /> integer offset into the buffer to start copying into.</param>
		/// <param name="count">An <see cref="T:System.Int32" /> integer representing the number of characters to copy.</param>
		// Token: 0x0600232A RID: 9002 RVA: 0x000A12E8 File Offset: 0x0009F4E8
		public void Write(long offset, char[] buffer, int offsetInBuffer, int count)
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
			if (this._rgchBuf == null)
			{
				throw new SqlTypeException("There is no buffer. Read or write operation failed.");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset > (long)this._rgchBuf.Length)
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
			if ((long)count > (long)this._rgchBuf.Length - offset)
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
				Array.Copy(buffer, (long)offsetInBuffer, this._rgchBuf, offset, (long)count);
				if (this._lCurLen < offset + (long)count)
				{
					this._lCurLen = offset + (long)count;
				}
			}
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance to its equivalent <see cref="T:System.Data.SqlTypes.SqlString" /> representation.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> representation of this type.</returns>
		// Token: 0x0600232B RID: 9003 RVA: 0x000A1423 File Offset: 0x0009F623
		public SqlString ToSqlString()
		{
			if (!this.IsNull)
			{
				return new string(this.Value);
			}
			return SqlString.Null;
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlChars" /> structure to a <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</summary>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlChars" /> structure to be converted.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</returns>
		// Token: 0x0600232C RID: 9004 RVA: 0x000A1443 File Offset: 0x0009F643
		public static explicit operator SqlString(SqlChars value)
		{
			return value.ToSqlString();
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlString" /> structure to a <see cref="T:System.Data.SqlTypes.SqlChars" /> structure.</summary>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlString" /> structure to be converted.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlChars" /> structure.</returns>
		// Token: 0x0600232D RID: 9005 RVA: 0x000A144B File Offset: 0x0009F64B
		public static explicit operator SqlChars(SqlString value)
		{
			return new SqlChars(value);
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x000A1453 File Offset: 0x0009F653
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			bool isNull = this.IsNull;
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x000A145C File Offset: 0x0009F65C
		internal bool FStream()
		{
			return this._state == SqlBytesCharsState.Stream;
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x000A1468 File Offset: 0x0009F668
		private void CopyStreamToBuffer()
		{
			long length = this._stream.Length;
			if (length >= 2147483647L)
			{
				throw new SqlTypeException("The buffer is insufficient. Read or write operation failed.");
			}
			if (this._rgchBuf == null || (long)this._rgchBuf.Length < length)
			{
				this._rgchBuf = new char[length];
			}
			if (this._stream.Position != 0L)
			{
				this._stream.Seek(0L, SeekOrigin.Begin);
			}
			this._stream.Read(this._rgchBuf, 0, (int)length);
			this._stream = null;
			this._lCurLen = length;
			this._state = SqlBytesCharsState.Buffer;
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x000A14FC File Offset: 0x0009F6FC
		private void SetBuffer(char[] buffer)
		{
			this._rgchBuf = buffer;
			this._lCurLen = ((this._rgchBuf == null) ? -1L : ((long)this._rgchBuf.Length));
			this._stream = null;
			this._state = ((this._rgchBuf == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Buffer);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</returns>
		// Token: 0x06002332 RID: 9010 RVA: 0x00003E32 File Offset: 0x00002032
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="r">
		///   <see langword="XmlReader" />
		/// </param>
		// Token: 0x06002333 RID: 9011 RVA: 0x000A153C File Offset: 0x0009F73C
		void IXmlSerializable.ReadXml(XmlReader r)
		{
			string attribute = r.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				r.ReadElementString();
				this.SetNull();
				return;
			}
			char[] buffer = r.ReadElementString().ToCharArray();
			this.SetBuffer(buffer);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">
		///   <see langword="XmlWriter" />
		/// </param>
		// Token: 0x06002334 RID: 9012 RVA: 0x000A1588 File Offset: 0x0009F788
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			char[] buffer = this.Buffer;
			writer.WriteString(new string(buffer, 0, (int)this.Length));
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		/// <returns>A <see langword="string" /> value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		// Token: 0x06002335 RID: 9013 RVA: 0x000A15D3 File Offset: 0x0009F7D3
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		/// <summary>Gets serialization information with all the data needed to reinstantiate this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</summary>
		/// <param name="info">The object to be populated with serialization information.</param>
		/// <param name="context">The destination context of the serialization.</param>
		// Token: 0x06002336 RID: 9014 RVA: 0x0004549D File Offset: 0x0004369D
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Returns a null instance of this <see cref="T:System.Data.SqlTypes.SqlChars" />.</summary>
		/// <returns>An instance whose <see cref="P:System.Data.SqlTypes.SqlChars.IsNull" /> property returns <see langword="true" />. For more information, see Handling Null Values.</returns>
		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06002337 RID: 9015 RVA: 0x000A15E4 File Offset: 0x0009F7E4
		public static SqlChars Null
		{
			get
			{
				return new SqlChars(null);
			}
		}

		// Token: 0x04001841 RID: 6209
		internal char[] _rgchBuf;

		// Token: 0x04001842 RID: 6210
		private long _lCurLen;

		// Token: 0x04001843 RID: 6211
		internal SqlStreamChars _stream;

		// Token: 0x04001844 RID: 6212
		private SqlBytesCharsState _state;

		// Token: 0x04001845 RID: 6213
		private char[] _rgchWorkBuf;

		// Token: 0x04001846 RID: 6214
		private const long x_lMaxLen = 2147483647L;

		// Token: 0x04001847 RID: 6215
		private const long x_lNull = -1L;
	}
}
