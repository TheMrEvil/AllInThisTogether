using System;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x02000170 RID: 368
	internal class JsonEncodingStreamWrapper : Stream
	{
		// Token: 0x0600133B RID: 4923 RVA: 0x0004AB6C File Offset: 0x00048D6C
		public JsonEncodingStreamWrapper(Stream stream, Encoding encoding, bool isReader)
		{
			this.isReading = isReader;
			if (isReader)
			{
				this.InitForReading(stream, encoding);
				return;
			}
			if (encoding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("encoding");
			}
			this.InitForWriting(stream, encoding);
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x0004ABA9 File Offset: 0x00048DA9
		public override bool CanRead
		{
			get
			{
				return this.isReading && this.stream.CanRead;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x00003127 File Offset: 0x00001327
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x0004ABC0 File Offset: 0x00048DC0
		public override bool CanTimeout
		{
			get
			{
				return this.stream.CanTimeout;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x0004ABCD File Offset: 0x00048DCD
		public override bool CanWrite
		{
			get
			{
				return !this.isReading && this.stream.CanWrite;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x0004ABE4 File Offset: 0x00048DE4
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x00003141 File Offset: 0x00001341
		// (set) Token: 0x06001342 RID: 4930 RVA: 0x00003141 File Offset: 0x00001341
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

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x0004ABF1 File Offset: 0x00048DF1
		// (set) Token: 0x06001344 RID: 4932 RVA: 0x0004ABFE File Offset: 0x00048DFE
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

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x0004AC0C File Offset: 0x00048E0C
		// (set) Token: 0x06001346 RID: 4934 RVA: 0x0004AC19 File Offset: 0x00048E19
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

		// Token: 0x06001347 RID: 4935 RVA: 0x0004AC28 File Offset: 0x00048E28
		public static ArraySegment<byte> ProcessBuffer(byte[] buffer, int offset, int count, Encoding encoding)
		{
			ArraySegment<byte> result;
			try
			{
				JsonEncodingStreamWrapper.SupportedEncoding supportedEncoding = JsonEncodingStreamWrapper.GetSupportedEncoding(encoding);
				JsonEncodingStreamWrapper.SupportedEncoding supportedEncoding2;
				if (count < 2)
				{
					supportedEncoding2 = JsonEncodingStreamWrapper.SupportedEncoding.UTF8;
				}
				else
				{
					supportedEncoding2 = JsonEncodingStreamWrapper.ReadEncoding(buffer[offset], buffer[offset + 1]);
				}
				if (supportedEncoding != JsonEncodingStreamWrapper.SupportedEncoding.None && supportedEncoding != supportedEncoding2)
				{
					JsonEncodingStreamWrapper.ThrowExpectedEncodingMismatch(supportedEncoding, supportedEncoding2);
				}
				if (supportedEncoding2 == JsonEncodingStreamWrapper.SupportedEncoding.UTF8)
				{
					result = new ArraySegment<byte>(buffer, offset, count);
				}
				else
				{
					result = new ArraySegment<byte>(JsonEncodingStreamWrapper.ValidatingUTF8.GetBytes(JsonEncodingStreamWrapper.GetEncoding(supportedEncoding2).GetChars(buffer, offset, count)));
				}
			}
			catch (DecoderFallbackException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Invalid bytes in JSON."), innerException));
			}
			return result;
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0004ACB8 File Offset: 0x00048EB8
		public override void Close()
		{
			this.Flush();
			base.Close();
			this.stream.Close();
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0004ACD1 File Offset: 0x00048ED1
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0004ACE0 File Offset: 0x00048EE0
		public override int Read(byte[] buffer, int offset, int count)
		{
			int result;
			try
			{
				if (this.byteCount == 0)
				{
					if (this.encodingCode == JsonEncodingStreamWrapper.SupportedEncoding.UTF8)
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
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Invalid bytes in JSON."), innerException));
			}
			return result;
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0004AE08 File Offset: 0x00049008
		public override int ReadByte()
		{
			if (this.byteCount == 0 && this.encodingCode == JsonEncodingStreamWrapper.SupportedEncoding.UTF8)
			{
				return this.stream.ReadByte();
			}
			if (this.Read(this.byteBuffer, 0, 1) == 0)
			{
				return -1;
			}
			return (int)this.byteBuffer[0];
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00003141 File Offset: 0x00001341
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00003141 File Offset: 0x00001341
		public override void SetLength(long value)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0004AE40 File Offset: 0x00049040
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.encodingCode == JsonEncodingStreamWrapper.SupportedEncoding.UTF8)
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

		// Token: 0x0600134F RID: 4943 RVA: 0x0004AEDC File Offset: 0x000490DC
		public override void WriteByte(byte b)
		{
			if (this.encodingCode == JsonEncodingStreamWrapper.SupportedEncoding.UTF8)
			{
				this.stream.WriteByte(b);
				return;
			}
			this.byteBuffer[0] = b;
			this.Write(this.byteBuffer, 0, 1);
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0004AF0A File Offset: 0x0004910A
		private static Encoding GetEncoding(JsonEncodingStreamWrapper.SupportedEncoding e)
		{
			switch (e)
			{
			case JsonEncodingStreamWrapper.SupportedEncoding.UTF8:
				return JsonEncodingStreamWrapper.ValidatingUTF8;
			case JsonEncodingStreamWrapper.SupportedEncoding.UTF16LE:
				return JsonEncodingStreamWrapper.ValidatingUTF16;
			case JsonEncodingStreamWrapper.SupportedEncoding.UTF16BE:
				return JsonEncodingStreamWrapper.ValidatingBEUTF16;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("JSON Encoding is not supported.")));
			}
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x0004AF46 File Offset: 0x00049146
		private static string GetEncodingName(JsonEncodingStreamWrapper.SupportedEncoding enc)
		{
			switch (enc)
			{
			case JsonEncodingStreamWrapper.SupportedEncoding.UTF8:
				return "utf-8";
			case JsonEncodingStreamWrapper.SupportedEncoding.UTF16LE:
				return "utf-16LE";
			case JsonEncodingStreamWrapper.SupportedEncoding.UTF16BE:
				return "utf-16BE";
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("JSON Encoding is not supported.")));
			}
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0004AF84 File Offset: 0x00049184
		private static JsonEncodingStreamWrapper.SupportedEncoding GetSupportedEncoding(Encoding encoding)
		{
			if (encoding == null)
			{
				return JsonEncodingStreamWrapper.SupportedEncoding.None;
			}
			if (encoding.WebName == JsonEncodingStreamWrapper.ValidatingUTF8.WebName)
			{
				return JsonEncodingStreamWrapper.SupportedEncoding.UTF8;
			}
			if (encoding.WebName == JsonEncodingStreamWrapper.ValidatingUTF16.WebName)
			{
				return JsonEncodingStreamWrapper.SupportedEncoding.UTF16LE;
			}
			if (encoding.WebName == JsonEncodingStreamWrapper.ValidatingBEUTF16.WebName)
			{
				return JsonEncodingStreamWrapper.SupportedEncoding.UTF16BE;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("JSON Encoding is not supported.")));
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0004AFF5 File Offset: 0x000491F5
		private static JsonEncodingStreamWrapper.SupportedEncoding ReadEncoding(byte b1, byte b2)
		{
			if (b1 == 0 && b2 != 0)
			{
				return JsonEncodingStreamWrapper.SupportedEncoding.UTF16BE;
			}
			if (b1 != 0 && b2 == 0)
			{
				return JsonEncodingStreamWrapper.SupportedEncoding.UTF16LE;
			}
			if (b1 == 0 && b2 == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Invalid bytes in JSON.")));
			}
			return JsonEncodingStreamWrapper.SupportedEncoding.UTF8;
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0004B023 File Offset: 0x00049223
		private static void ThrowExpectedEncodingMismatch(JsonEncodingStreamWrapper.SupportedEncoding expEnc, JsonEncodingStreamWrapper.SupportedEncoding actualEnc)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Expected encoding '{0}', got '{1}' instead.", new object[]
			{
				JsonEncodingStreamWrapper.GetEncodingName(expEnc),
				JsonEncodingStreamWrapper.GetEncodingName(actualEnc)
			})));
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0004B054 File Offset: 0x00049254
		private void CleanupCharBreak()
		{
			int num = this.byteOffset + this.byteCount;
			if (this.byteCount % 2 != 0)
			{
				int num2 = this.stream.ReadByte();
				if (num2 < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Unexpected end of file in JSON.")));
				}
				this.bytes[num++] = (byte)num2;
				this.byteCount++;
			}
			int num3;
			if (this.encodingCode == JsonEncodingStreamWrapper.SupportedEncoding.UTF16LE)
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
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Unexpected end of file in JSON.")));
				}
				this.bytes[num++] = (byte)num4;
				this.bytes[num++] = (byte)num5;
				this.byteCount += 2;
			}
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0004B171 File Offset: 0x00049371
		private void EnsureBuffers()
		{
			this.EnsureByteBuffer();
			if (this.chars == null)
			{
				this.chars = new char[128];
			}
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0004B191 File Offset: 0x00049391
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

		// Token: 0x06001358 RID: 4952 RVA: 0x0004B1BC File Offset: 0x000493BC
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

		// Token: 0x06001359 RID: 4953 RVA: 0x0004B210 File Offset: 0x00049410
		private void InitForReading(Stream inputStream, Encoding expectedEncoding)
		{
			try
			{
				this.stream = new BufferedStream(inputStream);
				JsonEncodingStreamWrapper.SupportedEncoding supportedEncoding = JsonEncodingStreamWrapper.GetSupportedEncoding(expectedEncoding);
				JsonEncodingStreamWrapper.SupportedEncoding supportedEncoding2 = this.ReadEncoding();
				if (supportedEncoding != JsonEncodingStreamWrapper.SupportedEncoding.None && supportedEncoding != supportedEncoding2)
				{
					JsonEncodingStreamWrapper.ThrowExpectedEncodingMismatch(supportedEncoding, supportedEncoding2);
				}
				if (supportedEncoding2 != JsonEncodingStreamWrapper.SupportedEncoding.UTF8)
				{
					this.EnsureBuffers();
					this.FillBuffer(254);
					this.encodingCode = supportedEncoding2;
					this.encoding = JsonEncodingStreamWrapper.GetEncoding(supportedEncoding2);
					this.CleanupCharBreak();
					int charCount = this.encoding.GetChars(this.bytes, this.byteOffset, this.byteCount, this.chars, 0);
					this.byteOffset = 0;
					this.byteCount = JsonEncodingStreamWrapper.ValidatingUTF8.GetBytes(this.chars, 0, charCount, this.bytes, 0);
				}
			}
			catch (DecoderFallbackException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Invalid bytes in JSON."), innerException));
			}
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0004B2E8 File Offset: 0x000494E8
		private void InitForWriting(Stream outputStream, Encoding writeEncoding)
		{
			this.encoding = writeEncoding;
			this.stream = new BufferedStream(outputStream);
			this.encodingCode = JsonEncodingStreamWrapper.GetSupportedEncoding(writeEncoding);
			if (this.encodingCode != JsonEncodingStreamWrapper.SupportedEncoding.UTF8)
			{
				this.EnsureBuffers();
				this.dec = JsonEncodingStreamWrapper.ValidatingUTF8.GetDecoder();
				this.enc = this.encoding.GetEncoder();
			}
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0004B344 File Offset: 0x00049544
		private JsonEncodingStreamWrapper.SupportedEncoding ReadEncoding()
		{
			int num = this.stream.ReadByte();
			int num2 = this.stream.ReadByte();
			this.EnsureByteBuffer();
			JsonEncodingStreamWrapper.SupportedEncoding result;
			if (num == -1)
			{
				result = JsonEncodingStreamWrapper.SupportedEncoding.UTF8;
				this.byteCount = 0;
			}
			else if (num2 == -1)
			{
				result = JsonEncodingStreamWrapper.SupportedEncoding.UTF8;
				this.bytes[0] = (byte)num;
				this.byteCount = 1;
			}
			else
			{
				result = JsonEncodingStreamWrapper.ReadEncoding((byte)num, (byte)num2);
				this.bytes[0] = (byte)num;
				this.bytes[1] = (byte)num2;
				this.byteCount = 2;
			}
			return result;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0004B3C0 File Offset: 0x000495C0
		// Note: this type is marked as 'beforefieldinit'.
		static JsonEncodingStreamWrapper()
		{
		}

		// Token: 0x0400099D RID: 2461
		private static readonly UnicodeEncoding SafeBEUTF16 = new UnicodeEncoding(true, false, false);

		// Token: 0x0400099E RID: 2462
		private static readonly UnicodeEncoding SafeUTF16 = new UnicodeEncoding(false, false, false);

		// Token: 0x0400099F RID: 2463
		private static readonly UTF8Encoding SafeUTF8 = new UTF8Encoding(false, false);

		// Token: 0x040009A0 RID: 2464
		private static readonly UnicodeEncoding ValidatingBEUTF16 = new UnicodeEncoding(true, false, true);

		// Token: 0x040009A1 RID: 2465
		private static readonly UnicodeEncoding ValidatingUTF16 = new UnicodeEncoding(false, false, true);

		// Token: 0x040009A2 RID: 2466
		private static readonly UTF8Encoding ValidatingUTF8 = new UTF8Encoding(false, true);

		// Token: 0x040009A3 RID: 2467
		private const int BufferLength = 128;

		// Token: 0x040009A4 RID: 2468
		private byte[] byteBuffer = new byte[1];

		// Token: 0x040009A5 RID: 2469
		private int byteCount;

		// Token: 0x040009A6 RID: 2470
		private int byteOffset;

		// Token: 0x040009A7 RID: 2471
		private byte[] bytes;

		// Token: 0x040009A8 RID: 2472
		private char[] chars;

		// Token: 0x040009A9 RID: 2473
		private Decoder dec;

		// Token: 0x040009AA RID: 2474
		private Encoder enc;

		// Token: 0x040009AB RID: 2475
		private Encoding encoding;

		// Token: 0x040009AC RID: 2476
		private JsonEncodingStreamWrapper.SupportedEncoding encodingCode;

		// Token: 0x040009AD RID: 2477
		private bool isReading;

		// Token: 0x040009AE RID: 2478
		private Stream stream;

		// Token: 0x02000171 RID: 369
		private enum SupportedEncoding
		{
			// Token: 0x040009B0 RID: 2480
			UTF8,
			// Token: 0x040009B1 RID: 2481
			UTF16LE,
			// Token: 0x040009B2 RID: 2482
			UTF16BE,
			// Token: 0x040009B3 RID: 2483
			None
		}
	}
}
