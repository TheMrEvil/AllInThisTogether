using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200001A RID: 26
	internal class EncodingStreamWrapper : Stream
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002670 File Offset: 0x00000870
		public EncodingStreamWrapper(Stream stream, Encoding encoding)
		{
			try
			{
				this.isReading = true;
				this.stream = new BufferedStream(stream);
				EncodingStreamWrapper.SupportedEncoding supportedEncoding = EncodingStreamWrapper.GetSupportedEncoding(encoding);
				EncodingStreamWrapper.SupportedEncoding supportedEncoding2 = this.ReadBOMEncoding(encoding == null);
				if (supportedEncoding != EncodingStreamWrapper.SupportedEncoding.None && supportedEncoding != supportedEncoding2)
				{
					EncodingStreamWrapper.ThrowExpectedEncodingMismatch(supportedEncoding, supportedEncoding2);
				}
				if (supportedEncoding2 == EncodingStreamWrapper.SupportedEncoding.UTF8)
				{
					this.FillBuffer(2);
					if (this.bytes[this.byteOffset + 1] == 63 && this.bytes[this.byteOffset] == 60)
					{
						this.FillBuffer(128);
						EncodingStreamWrapper.CheckUTF8DeclarationEncoding(this.bytes, this.byteOffset, this.byteCount, supportedEncoding2, supportedEncoding);
					}
				}
				else
				{
					this.EnsureBuffers();
					this.FillBuffer(254);
					this.SetReadDocumentEncoding(supportedEncoding2);
					this.CleanupCharBreak();
					int charCount = this.encoding.GetChars(this.bytes, this.byteOffset, this.byteCount, this.chars, 0);
					this.byteOffset = 0;
					this.byteCount = EncodingStreamWrapper.ValidatingUTF8.GetBytes(this.chars, 0, charCount, this.bytes, 0);
					if (this.bytes[1] == 63 && this.bytes[0] == 60)
					{
						EncodingStreamWrapper.CheckUTF8DeclarationEncoding(this.bytes, 0, this.byteCount, supportedEncoding2, supportedEncoding);
					}
					else if (supportedEncoding == EncodingStreamWrapper.SupportedEncoding.None)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("An XML declaration with an encoding is required for all non-UTF8 documents.")));
					}
				}
			}
			catch (DecoderFallbackException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Invalid byte encoding."), innerException));
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002804 File Offset: 0x00000A04
		private void SetReadDocumentEncoding(EncodingStreamWrapper.SupportedEncoding e)
		{
			this.EnsureBuffers();
			this.encodingCode = e;
			this.encoding = EncodingStreamWrapper.GetEncoding(e);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000281F File Offset: 0x00000A1F
		private static Encoding GetEncoding(EncodingStreamWrapper.SupportedEncoding e)
		{
			switch (e)
			{
			case EncodingStreamWrapper.SupportedEncoding.UTF8:
				return EncodingStreamWrapper.ValidatingUTF8;
			case EncodingStreamWrapper.SupportedEncoding.UTF16LE:
				return EncodingStreamWrapper.ValidatingUTF16;
			case EncodingStreamWrapper.SupportedEncoding.UTF16BE:
				return EncodingStreamWrapper.ValidatingBEUTF16;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("XML encoding not supported.")));
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000285B File Offset: 0x00000A5B
		private static Encoding GetSafeEncoding(EncodingStreamWrapper.SupportedEncoding e)
		{
			switch (e)
			{
			case EncodingStreamWrapper.SupportedEncoding.UTF8:
				return EncodingStreamWrapper.SafeUTF8;
			case EncodingStreamWrapper.SupportedEncoding.UTF16LE:
				return EncodingStreamWrapper.SafeUTF16;
			case EncodingStreamWrapper.SupportedEncoding.UTF16BE:
				return EncodingStreamWrapper.SafeBEUTF16;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("XML encoding not supported.")));
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002897 File Offset: 0x00000A97
		private static string GetEncodingName(EncodingStreamWrapper.SupportedEncoding enc)
		{
			switch (enc)
			{
			case EncodingStreamWrapper.SupportedEncoding.UTF8:
				return "utf-8";
			case EncodingStreamWrapper.SupportedEncoding.UTF16LE:
				return "utf-16LE";
			case EncodingStreamWrapper.SupportedEncoding.UTF16BE:
				return "utf-16BE";
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("XML encoding not supported.")));
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000028D4 File Offset: 0x00000AD4
		private static EncodingStreamWrapper.SupportedEncoding GetSupportedEncoding(Encoding encoding)
		{
			if (encoding == null)
			{
				return EncodingStreamWrapper.SupportedEncoding.None;
			}
			if (encoding.WebName == EncodingStreamWrapper.ValidatingUTF8.WebName)
			{
				return EncodingStreamWrapper.SupportedEncoding.UTF8;
			}
			if (encoding.WebName == EncodingStreamWrapper.ValidatingUTF16.WebName)
			{
				return EncodingStreamWrapper.SupportedEncoding.UTF16LE;
			}
			if (encoding.WebName == EncodingStreamWrapper.ValidatingBEUTF16.WebName)
			{
				return EncodingStreamWrapper.SupportedEncoding.UTF16BE;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("XML encoding not supported.")));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002948 File Offset: 0x00000B48
		public EncodingStreamWrapper(Stream stream, Encoding encoding, bool emitBOM)
		{
			this.isReading = false;
			this.encoding = encoding;
			this.stream = new BufferedStream(stream);
			this.encodingCode = EncodingStreamWrapper.GetSupportedEncoding(encoding);
			if (this.encodingCode != EncodingStreamWrapper.SupportedEncoding.UTF8)
			{
				this.EnsureBuffers();
				this.dec = EncodingStreamWrapper.ValidatingUTF8.GetDecoder();
				this.enc = this.encoding.GetEncoder();
				if (emitBOM)
				{
					byte[] preamble = this.encoding.GetPreamble();
					if (preamble.Length != 0)
					{
						this.stream.Write(preamble, 0, preamble.Length);
					}
				}
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000029E0 File Offset: 0x00000BE0
		private EncodingStreamWrapper.SupportedEncoding ReadBOMEncoding(bool notOutOfBand)
		{
			int num = this.stream.ReadByte();
			int num2 = this.stream.ReadByte();
			int num3 = this.stream.ReadByte();
			int num4 = this.stream.ReadByte();
			if (num4 == -1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Unexpected end of file.")));
			}
			int num5;
			EncodingStreamWrapper.SupportedEncoding result = EncodingStreamWrapper.ReadBOMEncoding((byte)num, (byte)num2, (byte)num3, (byte)num4, notOutOfBand, out num5);
			this.EnsureByteBuffer();
			switch (num5)
			{
			case 1:
				this.bytes[0] = (byte)num4;
				break;
			case 2:
				this.bytes[0] = (byte)num3;
				this.bytes[1] = (byte)num4;
				break;
			case 4:
				this.bytes[0] = (byte)num;
				this.bytes[1] = (byte)num2;
				this.bytes[2] = (byte)num3;
				this.bytes[3] = (byte)num4;
				break;
			}
			this.byteCount = num5;
			return result;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002AC0 File Offset: 0x00000CC0
		private static EncodingStreamWrapper.SupportedEncoding ReadBOMEncoding(byte b1, byte b2, byte b3, byte b4, bool notOutOfBand, out int preserve)
		{
			EncodingStreamWrapper.SupportedEncoding result = EncodingStreamWrapper.SupportedEncoding.UTF8;
			preserve = 0;
			if (b1 == 60 && b2 != 0)
			{
				result = EncodingStreamWrapper.SupportedEncoding.UTF8;
				preserve = 4;
			}
			else if (b1 == 255 && b2 == 254)
			{
				result = EncodingStreamWrapper.SupportedEncoding.UTF16LE;
				preserve = 2;
			}
			else if (b1 == 254 && b2 == 255)
			{
				result = EncodingStreamWrapper.SupportedEncoding.UTF16BE;
				preserve = 2;
			}
			else if (b1 == 0 && b2 == 60)
			{
				result = EncodingStreamWrapper.SupportedEncoding.UTF16BE;
				if (notOutOfBand && (b3 != 0 || b4 != 63))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("An XML declaration is required for all non-UTF8 documents.")));
				}
				preserve = 4;
			}
			else if (b1 == 60 && b2 == 0)
			{
				result = EncodingStreamWrapper.SupportedEncoding.UTF16LE;
				if (notOutOfBand && (b3 != 63 || b4 != 0))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("An XML declaration is required for all non-UTF8 documents.")));
				}
				preserve = 4;
			}
			else if (b1 == 239 && b2 == 187)
			{
				if (notOutOfBand && b3 != 191)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Unrecognized Byte Order Mark.")));
				}
				preserve = 1;
			}
			else
			{
				preserve = 4;
			}
			return result;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002BBC File Offset: 0x00000DBC
		private void FillBuffer(int count)
		{
			int num;
			for (count -= this.byteCount; count > 0; count -= num)
			{
				num = this.stream.Read(this.bytes, this.byteOffset + this.byteCount, count);
				if (num == 0)
				{
					break;
				}
				this.byteCount += num;
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002C0F File Offset: 0x00000E0F
		private void EnsureBuffers()
		{
			this.EnsureByteBuffer();
			if (this.chars == null)
			{
				this.chars = new char[128];
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002C2F File Offset: 0x00000E2F
		private void EnsureByteBuffer()
		{
			if (this.bytes != null)
			{
				return;
			}
			this.bytes = new byte[512];
			this.byteOffset = 0;
			this.byteCount = 0;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002C58 File Offset: 0x00000E58
		private static void CheckUTF8DeclarationEncoding(byte[] buffer, int offset, int count, EncodingStreamWrapper.SupportedEncoding e, EncodingStreamWrapper.SupportedEncoding expectedEnc)
		{
			byte b = 0;
			int num = -1;
			int num2 = offset + Math.Min(count, 128);
			int num3 = 0;
			for (int i = offset + 2; i < num2; i++)
			{
				if (b != 0)
				{
					if (buffer[i] == b)
					{
						b = 0;
					}
				}
				else if (buffer[i] == 39 || buffer[i] == 34)
				{
					b = buffer[i];
				}
				else if (buffer[i] == 61)
				{
					if (num3 == 1)
					{
						num = i;
						break;
					}
					num3++;
				}
				else if (buffer[i] == 63)
				{
					break;
				}
			}
			if (num == -1)
			{
				if (e != EncodingStreamWrapper.SupportedEncoding.UTF8 && expectedEnc == EncodingStreamWrapper.SupportedEncoding.None)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("An XML declaration with an encoding is required for all non-UTF8 documents.")));
				}
				return;
			}
			else
			{
				if (num < 28)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Malformed XML declaration.")));
				}
				int i = num - 1;
				while (EncodingStreamWrapper.IsWhitespace(buffer[i]))
				{
					i--;
				}
				if (!EncodingStreamWrapper.Compare(EncodingStreamWrapper.encodingAttr, buffer, i - EncodingStreamWrapper.encodingAttr.Length + 1))
				{
					if (e != EncodingStreamWrapper.SupportedEncoding.UTF8 && expectedEnc == EncodingStreamWrapper.SupportedEncoding.None)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("An XML declaration with an encoding is required for all non-UTF8 documents.")));
					}
					return;
				}
				else
				{
					i = num + 1;
					while (i < num2 && EncodingStreamWrapper.IsWhitespace(buffer[i]))
					{
						i++;
					}
					if (buffer[i] != 39 && buffer[i] != 34)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Malformed XML declaration.")));
					}
					b = buffer[i];
					int num4 = i;
					i = num4 + 1;
					while (buffer[i] != b && i < num2)
					{
						i++;
					}
					if (buffer[i] != b)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Malformed XML declaration.")));
					}
					int num5 = num4 + 1;
					int num6 = i - num5;
					EncodingStreamWrapper.SupportedEncoding supportedEncoding = e;
					if (num6 == EncodingStreamWrapper.encodingUTF8.Length && EncodingStreamWrapper.CompareCaseInsensitive(EncodingStreamWrapper.encodingUTF8, buffer, num5))
					{
						supportedEncoding = EncodingStreamWrapper.SupportedEncoding.UTF8;
					}
					else if (num6 == EncodingStreamWrapper.encodingUnicodeLE.Length && EncodingStreamWrapper.CompareCaseInsensitive(EncodingStreamWrapper.encodingUnicodeLE, buffer, num5))
					{
						supportedEncoding = EncodingStreamWrapper.SupportedEncoding.UTF16LE;
					}
					else if (num6 == EncodingStreamWrapper.encodingUnicodeBE.Length && EncodingStreamWrapper.CompareCaseInsensitive(EncodingStreamWrapper.encodingUnicodeBE, buffer, num5))
					{
						supportedEncoding = EncodingStreamWrapper.SupportedEncoding.UTF16BE;
					}
					else if (num6 == EncodingStreamWrapper.encodingUnicode.Length && EncodingStreamWrapper.CompareCaseInsensitive(EncodingStreamWrapper.encodingUnicode, buffer, num5))
					{
						if (e == EncodingStreamWrapper.SupportedEncoding.UTF8)
						{
							EncodingStreamWrapper.ThrowEncodingMismatch(EncodingStreamWrapper.SafeUTF8.GetString(buffer, num5, num6), EncodingStreamWrapper.SafeUTF8.GetString(EncodingStreamWrapper.encodingUTF8, 0, EncodingStreamWrapper.encodingUTF8.Length));
						}
					}
					else
					{
						EncodingStreamWrapper.ThrowEncodingMismatch(EncodingStreamWrapper.SafeUTF8.GetString(buffer, num5, num6), e);
					}
					if (e != supportedEncoding)
					{
						EncodingStreamWrapper.ThrowEncodingMismatch(EncodingStreamWrapper.SafeUTF8.GetString(buffer, num5, num6), e);
					}
					return;
				}
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002EAC File Offset: 0x000010AC
		private static bool CompareCaseInsensitive(byte[] key, byte[] buffer, int offset)
		{
			for (int i = 0; i < key.Length; i++)
			{
				if (key[i] != buffer[offset + i] && (char)key[i] != char.ToLower((char)buffer[offset + i], CultureInfo.InvariantCulture))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002EE8 File Offset: 0x000010E8
		private static bool Compare(byte[] key, byte[] buffer, int offset)
		{
			for (int i = 0; i < key.Length; i++)
			{
				if (key[i] != buffer[offset + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002F10 File Offset: 0x00001110
		private static bool IsWhitespace(byte ch)
		{
			return ch == 32 || ch == 10 || ch == 9 || ch == 13;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002F28 File Offset: 0x00001128
		internal static ArraySegment<byte> ProcessBuffer(byte[] buffer, int offset, int count, Encoding encoding)
		{
			if (count < 4)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Unexpected end of file.")));
			}
			ArraySegment<byte> result;
			try
			{
				EncodingStreamWrapper.SupportedEncoding supportedEncoding = EncodingStreamWrapper.GetSupportedEncoding(encoding);
				int num;
				EncodingStreamWrapper.SupportedEncoding supportedEncoding2 = EncodingStreamWrapper.ReadBOMEncoding(buffer[offset], buffer[offset + 1], buffer[offset + 2], buffer[offset + 3], encoding == null, out num);
				if (supportedEncoding != EncodingStreamWrapper.SupportedEncoding.None && supportedEncoding != supportedEncoding2)
				{
					EncodingStreamWrapper.ThrowExpectedEncodingMismatch(supportedEncoding, supportedEncoding2);
				}
				offset += 4 - num;
				count -= 4 - num;
				if (supportedEncoding2 == EncodingStreamWrapper.SupportedEncoding.UTF8)
				{
					if (buffer[offset + 1] != 63 || buffer[offset] != 60)
					{
						result = new ArraySegment<byte>(buffer, offset, count);
					}
					else
					{
						EncodingStreamWrapper.CheckUTF8DeclarationEncoding(buffer, offset, count, supportedEncoding2, supportedEncoding);
						result = new ArraySegment<byte>(buffer, offset, count);
					}
				}
				else
				{
					Encoding safeEncoding = EncodingStreamWrapper.GetSafeEncoding(supportedEncoding2);
					int num2 = Math.Min(count, 256);
					char[] array = new char[safeEncoding.GetMaxCharCount(num2)];
					int charCount = safeEncoding.GetChars(buffer, offset, num2, array, 0);
					byte[] array2 = new byte[EncodingStreamWrapper.ValidatingUTF8.GetMaxByteCount(charCount)];
					int count2 = EncodingStreamWrapper.ValidatingUTF8.GetBytes(array, 0, charCount, array2, 0);
					if (array2[1] == 63 && array2[0] == 60)
					{
						EncodingStreamWrapper.CheckUTF8DeclarationEncoding(array2, 0, count2, supportedEncoding2, supportedEncoding);
					}
					else if (supportedEncoding == EncodingStreamWrapper.SupportedEncoding.None)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("An XML declaration with an encoding is required for all non-UTF8 documents.")));
					}
					result = new ArraySegment<byte>(EncodingStreamWrapper.ValidatingUTF8.GetBytes(EncodingStreamWrapper.GetEncoding(supportedEncoding2).GetChars(buffer, offset, count)));
				}
			}
			catch (DecoderFallbackException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Invalid byte encoding."), innerException));
			}
			return result;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000030B0 File Offset: 0x000012B0
		private static void ThrowExpectedEncodingMismatch(EncodingStreamWrapper.SupportedEncoding expEnc, EncodingStreamWrapper.SupportedEncoding actualEnc)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("The expected encoding '{0}' does not match the actual encoding '{1}'.", new object[]
			{
				EncodingStreamWrapper.GetEncodingName(expEnc),
				EncodingStreamWrapper.GetEncodingName(actualEnc)
			})));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000030DE File Offset: 0x000012DE
		private static void ThrowEncodingMismatch(string declEnc, EncodingStreamWrapper.SupportedEncoding enc)
		{
			EncodingStreamWrapper.ThrowEncodingMismatch(declEnc, EncodingStreamWrapper.GetEncodingName(enc));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000030EC File Offset: 0x000012EC
		private static void ThrowEncodingMismatch(string declEnc, string docEnc)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("The encoding in the declaration '{0}' does not match the encoding of the document '{1}'.", new object[]
			{
				declEnc,
				docEnc
			})));
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003110 File Offset: 0x00001310
		public override bool CanRead
		{
			get
			{
				return this.isReading && this.stream.CanRead;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003127 File Offset: 0x00001327
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000312A File Offset: 0x0000132A
		public override bool CanWrite
		{
			get
			{
				return !this.isReading && this.stream.CanWrite;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003141 File Offset: 0x00001341
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003141 File Offset: 0x00001341
		public override long Position
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}
			set
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000314D File Offset: 0x0000134D
		public override void Close()
		{
			this.Flush();
			base.Close();
			this.stream.Close();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003166 File Offset: 0x00001366
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003173 File Offset: 0x00001373
		public override int ReadByte()
		{
			if (this.byteCount == 0 && this.encodingCode == EncodingStreamWrapper.SupportedEncoding.UTF8)
			{
				return this.stream.ReadByte();
			}
			if (this.Read(this.byteBuffer, 0, 1) == 0)
			{
				return -1;
			}
			return (int)this.byteBuffer[0];
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000031AC File Offset: 0x000013AC
		public override int Read(byte[] buffer, int offset, int count)
		{
			int result;
			try
			{
				if (this.byteCount == 0)
				{
					if (this.encodingCode == EncodingStreamWrapper.SupportedEncoding.UTF8)
					{
						return this.stream.Read(buffer, offset, count);
					}
					this.byteOffset = 0;
					this.byteCount = this.stream.Read(this.bytes, this.byteCount, (this.chars.Length - 1) * 2);
					if (this.byteCount == 0)
					{
						return 0;
					}
					this.CleanupCharBreak();
					int charCount = this.encoding.GetChars(this.bytes, 0, this.byteCount, this.chars, 0);
					this.byteCount = Encoding.UTF8.GetBytes(this.chars, 0, charCount, this.bytes, 0);
				}
				if (this.byteCount < count)
				{
					count = this.byteCount;
				}
				Buffer.BlockCopy(this.bytes, this.byteOffset, buffer, offset, count);
				this.byteOffset += count;
				this.byteCount -= count;
				result = count;
			}
			catch (DecoderFallbackException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Invalid byte encoding."), innerException));
			}
			return result;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000032D4 File Offset: 0x000014D4
		private void CleanupCharBreak()
		{
			int num = this.byteOffset + this.byteCount;
			if (this.byteCount % 2 != 0)
			{
				int num2 = this.stream.ReadByte();
				if (num2 < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Unexpected end of file.")));
				}
				this.bytes[num++] = (byte)num2;
				this.byteCount++;
			}
			int num3;
			if (this.encodingCode == EncodingStreamWrapper.SupportedEncoding.UTF16LE)
			{
				num3 = (int)this.bytes[num - 2] + ((int)this.bytes[num - 1] << 8);
			}
			else
			{
				num3 = (int)this.bytes[num - 1] + ((int)this.bytes[num - 2] << 8);
			}
			if ((num3 & 56320) != 56320 && num3 >= 55296 && num3 <= 56319)
			{
				int num4 = this.stream.ReadByte();
				int num5 = this.stream.ReadByte();
				if (num5 < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Unexpected end of file.")));
				}
				this.bytes[num++] = (byte)num4;
				this.bytes[num++] = (byte)num5;
				this.byteCount += 2;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003141 File Offset: 0x00001341
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000033F1 File Offset: 0x000015F1
		public override void WriteByte(byte b)
		{
			if (this.encodingCode == EncodingStreamWrapper.SupportedEncoding.UTF8)
			{
				this.stream.WriteByte(b);
				return;
			}
			this.byteBuffer[0] = b;
			this.Write(this.byteBuffer, 0, 1);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003420 File Offset: 0x00001620
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.encodingCode == EncodingStreamWrapper.SupportedEncoding.UTF8)
			{
				this.stream.Write(buffer, offset, count);
				return;
			}
			while (count > 0)
			{
				int num = (this.chars.Length < count) ? this.chars.Length : count;
				int charCount = this.dec.GetChars(buffer, offset, num, this.chars, 0, false);
				this.byteCount = this.enc.GetBytes(this.chars, 0, charCount, this.bytes, 0, false);
				this.stream.Write(this.bytes, 0, this.byteCount);
				offset += num;
				count -= num;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000034BC File Offset: 0x000016BC
		public override bool CanTimeout
		{
			get
			{
				return this.stream.CanTimeout;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000034C9 File Offset: 0x000016C9
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000034D6 File Offset: 0x000016D6
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000034E3 File Offset: 0x000016E3
		public override int ReadTimeout
		{
			get
			{
				return this.stream.ReadTimeout;
			}
			set
			{
				this.stream.ReadTimeout = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000034F1 File Offset: 0x000016F1
		// (set) Token: 0x06000088 RID: 136 RVA: 0x000034FE File Offset: 0x000016FE
		public override int WriteTimeout
		{
			get
			{
				return this.stream.WriteTimeout;
			}
			set
			{
				this.stream.WriteTimeout = value;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003141 File Offset: 0x00001341
		public override void SetLength(long value)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000350C File Offset: 0x0000170C
		// Note: this type is marked as 'beforefieldinit'.
		static EncodingStreamWrapper()
		{
		}

		// Token: 0x04000017 RID: 23
		private static readonly UTF8Encoding SafeUTF8 = new UTF8Encoding(false, false);

		// Token: 0x04000018 RID: 24
		private static readonly UnicodeEncoding SafeUTF16 = new UnicodeEncoding(false, false, false);

		// Token: 0x04000019 RID: 25
		private static readonly UnicodeEncoding SafeBEUTF16 = new UnicodeEncoding(true, false, false);

		// Token: 0x0400001A RID: 26
		private static readonly UTF8Encoding ValidatingUTF8 = new UTF8Encoding(false, true);

		// Token: 0x0400001B RID: 27
		private static readonly UnicodeEncoding ValidatingUTF16 = new UnicodeEncoding(false, false, true);

		// Token: 0x0400001C RID: 28
		private static readonly UnicodeEncoding ValidatingBEUTF16 = new UnicodeEncoding(true, false, true);

		// Token: 0x0400001D RID: 29
		private const int BufferLength = 128;

		// Token: 0x0400001E RID: 30
		private static readonly byte[] encodingAttr = new byte[]
		{
			101,
			110,
			99,
			111,
			100,
			105,
			110,
			103
		};

		// Token: 0x0400001F RID: 31
		private static readonly byte[] encodingUTF8 = new byte[]
		{
			117,
			116,
			102,
			45,
			56
		};

		// Token: 0x04000020 RID: 32
		private static readonly byte[] encodingUnicode = new byte[]
		{
			117,
			116,
			102,
			45,
			49,
			54
		};

		// Token: 0x04000021 RID: 33
		private static readonly byte[] encodingUnicodeLE = new byte[]
		{
			117,
			116,
			102,
			45,
			49,
			54,
			108,
			101
		};

		// Token: 0x04000022 RID: 34
		private static readonly byte[] encodingUnicodeBE = new byte[]
		{
			117,
			116,
			102,
			45,
			49,
			54,
			98,
			101
		};

		// Token: 0x04000023 RID: 35
		private EncodingStreamWrapper.SupportedEncoding encodingCode;

		// Token: 0x04000024 RID: 36
		private Encoding encoding;

		// Token: 0x04000025 RID: 37
		private Encoder enc;

		// Token: 0x04000026 RID: 38
		private Decoder dec;

		// Token: 0x04000027 RID: 39
		private bool isReading;

		// Token: 0x04000028 RID: 40
		private Stream stream;

		// Token: 0x04000029 RID: 41
		private char[] chars;

		// Token: 0x0400002A RID: 42
		private byte[] bytes;

		// Token: 0x0400002B RID: 43
		private int byteOffset;

		// Token: 0x0400002C RID: 44
		private int byteCount;

		// Token: 0x0400002D RID: 45
		private byte[] byteBuffer = new byte[1];

		// Token: 0x0200001B RID: 27
		private enum SupportedEncoding
		{
			// Token: 0x0400002F RID: 47
			UTF8,
			// Token: 0x04000030 RID: 48
			UTF16LE,
			// Token: 0x04000031 RID: 49
			UTF16BE,
			// Token: 0x04000032 RID: 50
			None
		}
	}
}
