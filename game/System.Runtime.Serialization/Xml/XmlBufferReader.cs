using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000055 RID: 85
	internal class XmlBufferReader
	{
		// Token: 0x0600038E RID: 910 RVA: 0x00011E20 File Offset: 0x00010020
		public XmlBufferReader(XmlDictionaryReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00011E2F File Offset: 0x0001002F
		public XmlBufferReader(byte[] buffer)
		{
			this.reader = null;
			this.buffer = buffer;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00011E45 File Offset: 0x00010045
		public static XmlBufferReader Empty
		{
			get
			{
				return XmlBufferReader.empty;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00011E4C File Offset: 0x0001004C
		public byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00011E54 File Offset: 0x00010054
		public bool IsStreamed
		{
			get
			{
				return this.stream != null;
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00011E5F File Offset: 0x0001005F
		public void SetBuffer(Stream stream, IXmlDictionary dictionary, XmlBinaryReaderSession session)
		{
			if (this.streamBuffer == null)
			{
				this.streamBuffer = new byte[128];
			}
			this.SetBuffer(stream, this.streamBuffer, 0, 0, dictionary, session);
			this.windowOffset = 0;
			this.windowOffsetMax = this.streamBuffer.Length;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00011E9F File Offset: 0x0001009F
		public void SetBuffer(byte[] buffer, int offset, int count, IXmlDictionary dictionary, XmlBinaryReaderSession session)
		{
			this.SetBuffer(null, buffer, offset, count, dictionary, session);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00011EAF File Offset: 0x000100AF
		private void SetBuffer(Stream stream, byte[] buffer, int offset, int count, IXmlDictionary dictionary, XmlBinaryReaderSession session)
		{
			this.stream = stream;
			this.buffer = buffer;
			this.offsetMin = offset;
			this.offset = offset;
			this.offsetMax = offset + count;
			this.dictionary = dictionary;
			this.session = session;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00011EE8 File Offset: 0x000100E8
		public void Close()
		{
			if (this.streamBuffer != null && this.streamBuffer.Length > 4096)
			{
				this.streamBuffer = null;
			}
			if (this.stream != null)
			{
				this.stream.Close();
				this.stream = null;
			}
			this.buffer = XmlBufferReader.emptyByteArray;
			this.offset = 0;
			this.offsetMax = 0;
			this.windowOffset = 0;
			this.windowOffsetMax = 0;
			this.dictionary = null;
			this.session = null;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000397 RID: 919 RVA: 0x00011F62 File Offset: 0x00010162
		public bool EndOfFile
		{
			get
			{
				return this.offset == this.offsetMax && !this.TryEnsureByte();
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00011F80 File Offset: 0x00010180
		public byte GetByte()
		{
			int num = this.offset;
			if (num < this.offsetMax)
			{
				return this.buffer[num];
			}
			return this.GetByteHard();
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00011FAC File Offset: 0x000101AC
		public void SkipByte()
		{
			this.Advance(1);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00011FB5 File Offset: 0x000101B5
		private byte GetByteHard()
		{
			this.EnsureByte();
			return this.buffer[this.offset];
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00011FCA File Offset: 0x000101CA
		public byte[] GetBuffer(int count, out int offset)
		{
			offset = this.offset;
			if (offset <= this.offsetMax - count)
			{
				return this.buffer;
			}
			return this.GetBufferHard(count, out offset);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00011FF0 File Offset: 0x000101F0
		public byte[] GetBuffer(int count, out int offset, out int offsetMax)
		{
			offset = this.offset;
			if (offset <= this.offsetMax - count)
			{
				offsetMax = this.offset + count;
			}
			else
			{
				this.TryEnsureBytes(Math.Min(count, this.windowOffsetMax - offset));
				offsetMax = this.offsetMax;
			}
			return this.buffer;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00012041 File Offset: 0x00010241
		public byte[] GetBuffer(out int offset, out int offsetMax)
		{
			offset = this.offset;
			offsetMax = this.offsetMax;
			return this.buffer;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00012059 File Offset: 0x00010259
		private byte[] GetBufferHard(int count, out int offset)
		{
			offset = this.offset;
			this.EnsureBytes(count);
			return this.buffer;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00012070 File Offset: 0x00010270
		private void EnsureByte()
		{
			if (!this.TryEnsureByte())
			{
				XmlExceptionHelper.ThrowUnexpectedEndOfFile(this.reader);
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00012088 File Offset: 0x00010288
		private bool TryEnsureByte()
		{
			if (this.stream == null)
			{
				return false;
			}
			if (this.offsetMax >= this.windowOffsetMax)
			{
				XmlExceptionHelper.ThrowMaxBytesPerReadExceeded(this.reader, this.windowOffsetMax - this.windowOffset);
			}
			if (this.offsetMax >= this.buffer.Length)
			{
				return this.TryEnsureBytes(1);
			}
			int num = this.stream.ReadByte();
			if (num == -1)
			{
				return false;
			}
			byte[] array = this.buffer;
			int num2 = this.offsetMax;
			this.offsetMax = num2 + 1;
			array[num2] = (byte)num;
			return true;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001210A File Offset: 0x0001030A
		private void EnsureBytes(int count)
		{
			if (!this.TryEnsureBytes(count))
			{
				XmlExceptionHelper.ThrowUnexpectedEndOfFile(this.reader);
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00012120 File Offset: 0x00010320
		private bool TryEnsureBytes(int count)
		{
			if (this.stream == null)
			{
				return false;
			}
			if (this.offset > 2147483647 - count)
			{
				XmlExceptionHelper.ThrowMaxBytesPerReadExceeded(this.reader, this.windowOffsetMax - this.windowOffset);
			}
			int num = this.offset + count;
			if (num < this.offsetMax)
			{
				return true;
			}
			if (num > this.windowOffsetMax)
			{
				XmlExceptionHelper.ThrowMaxBytesPerReadExceeded(this.reader, this.windowOffsetMax - this.windowOffset);
			}
			if (num > this.buffer.Length)
			{
				byte[] dst = new byte[Math.Max(num, this.buffer.Length * 2)];
				System.Buffer.BlockCopy(this.buffer, 0, dst, 0, this.offsetMax);
				this.buffer = dst;
				this.streamBuffer = dst;
			}
			int num2;
			for (int i = num - this.offsetMax; i > 0; i -= num2)
			{
				num2 = this.stream.Read(this.buffer, this.offsetMax, i);
				if (num2 == 0)
				{
					return false;
				}
				this.offsetMax += num2;
			}
			return true;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00012216 File Offset: 0x00010416
		public void Advance(int count)
		{
			this.offset += count;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00012228 File Offset: 0x00010428
		public void InsertBytes(byte[] buffer, int offset, int count)
		{
			if (this.offsetMax > buffer.Length - count)
			{
				byte[] dst = new byte[this.offsetMax + count];
				System.Buffer.BlockCopy(this.buffer, 0, dst, 0, this.offsetMax);
				this.buffer = dst;
				this.streamBuffer = dst;
			}
			System.Buffer.BlockCopy(this.buffer, this.offset, this.buffer, this.offset + count, this.offsetMax - this.offset);
			this.offsetMax += count;
			System.Buffer.BlockCopy(buffer, offset, this.buffer, this.offset, count);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000122C0 File Offset: 0x000104C0
		public void SetWindow(int windowOffset, int windowLength)
		{
			if (windowOffset > 2147483647 - windowLength)
			{
				windowLength = int.MaxValue - windowOffset;
			}
			if (this.offset != windowOffset)
			{
				System.Buffer.BlockCopy(this.buffer, this.offset, this.buffer, windowOffset, this.offsetMax - this.offset);
				this.offsetMax = windowOffset + (this.offsetMax - this.offset);
				this.offset = windowOffset;
			}
			this.windowOffset = windowOffset;
			this.windowOffsetMax = Math.Max(windowOffset + windowLength, this.offsetMax);
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00012345 File Offset: 0x00010545
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x0001234D File Offset: 0x0001054D
		public int Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00012356 File Offset: 0x00010556
		public int ReadBytes(int count)
		{
			int num = this.offset;
			if (num > this.offsetMax - count)
			{
				this.EnsureBytes(count);
			}
			this.offset += count;
			return num;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00012380 File Offset: 0x00010580
		public int ReadMultiByteUInt31()
		{
			int num = (int)this.GetByte();
			this.Advance(1);
			if ((num & 128) == 0)
			{
				return num;
			}
			num &= 127;
			int @byte = (int)this.GetByte();
			this.Advance(1);
			num |= (@byte & 127) << 7;
			if ((@byte & 128) == 0)
			{
				return num;
			}
			int byte2 = (int)this.GetByte();
			this.Advance(1);
			num |= (byte2 & 127) << 14;
			if ((byte2 & 128) == 0)
			{
				return num;
			}
			int byte3 = (int)this.GetByte();
			this.Advance(1);
			num |= (byte3 & 127) << 21;
			if ((byte3 & 128) == 0)
			{
				return num;
			}
			int byte4 = (int)this.GetByte();
			this.Advance(1);
			num |= byte4 << 28;
			if ((byte4 & 248) != 0)
			{
				XmlExceptionHelper.ThrowInvalidBinaryFormat(this.reader);
			}
			return num;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00012440 File Offset: 0x00010640
		public int ReadUInt8()
		{
			int @byte = (int)this.GetByte();
			this.Advance(1);
			return @byte;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001244F File Offset: 0x0001064F
		public int ReadInt8()
		{
			return (int)((sbyte)this.ReadUInt8());
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00012458 File Offset: 0x00010658
		public int ReadUInt16()
		{
			int num;
			byte[] array = this.GetBuffer(2, out num);
			int result = (int)array[num] + ((int)array[num + 1] << 8);
			this.Advance(2);
			return result;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00012481 File Offset: 0x00010681
		public int ReadInt16()
		{
			return (int)((short)this.ReadUInt16());
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001248C File Offset: 0x0001068C
		public int ReadInt32()
		{
			int num;
			byte[] array = this.GetBuffer(4, out num);
			byte b = array[num];
			byte b2 = array[num + 1];
			byte b3 = array[num + 2];
			int num2 = (int)array[num + 3];
			this.Advance(4);
			return (((num2 << 8) + (int)b3 << 8) + (int)b2 << 8) + (int)b;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000124C9 File Offset: 0x000106C9
		public int ReadUInt31()
		{
			int num = this.ReadInt32();
			if (num < 0)
			{
				XmlExceptionHelper.ThrowInvalidBinaryFormat(this.reader);
			}
			return num;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000124E0 File Offset: 0x000106E0
		public long ReadInt64()
		{
			long num = (long)((ulong)this.ReadInt32());
			return (long)(((ulong)this.ReadInt32() << 32) + (ulong)num);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00012504 File Offset: 0x00010704
		[SecuritySafeCritical]
		public unsafe float ReadSingle()
		{
			int num;
			byte[] array = this.GetBuffer(4, out num);
			float result;
			byte* ptr = (byte*)(&result);
			*ptr = array[num];
			ptr[1] = array[num + 1];
			ptr[2] = array[num + 2];
			ptr[3] = array[num + 3];
			this.Advance(4);
			return result;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00012548 File Offset: 0x00010748
		[SecuritySafeCritical]
		public unsafe double ReadDouble()
		{
			int num;
			byte[] array = this.GetBuffer(8, out num);
			double result;
			byte* ptr = (byte*)(&result);
			*ptr = array[num];
			ptr[1] = array[num + 1];
			ptr[2] = array[num + 2];
			ptr[3] = array[num + 3];
			ptr[4] = array[num + 4];
			ptr[5] = array[num + 5];
			ptr[6] = array[num + 6];
			ptr[7] = array[num + 7];
			this.Advance(8);
			return result;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000125B0 File Offset: 0x000107B0
		[SecuritySafeCritical]
		public unsafe decimal ReadDecimal()
		{
			int num;
			byte[] array = this.GetBuffer(16, out num);
			byte b = array[num];
			byte b2 = array[num + 1];
			byte b3 = array[num + 2];
			int num2 = ((((int)array[num + 3] << 8) + (int)b3 << 8) + (int)b2 << 8) + (int)b;
			if ((num2 & 2130771967) == 0 && (num2 & 16711680) <= 1835008)
			{
				decimal result;
				byte* ptr = (byte*)(&result);
				for (int i = 0; i < 16; i++)
				{
					ptr[i] = array[num + i];
				}
				this.Advance(16);
				return result;
			}
			XmlExceptionHelper.ThrowInvalidBinaryFormat(this.reader);
			return 0m;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00012644 File Offset: 0x00010844
		public UniqueId ReadUniqueId()
		{
			int num;
			UniqueId result = new UniqueId(this.GetBuffer(16, out num), num);
			this.Advance(16);
			return result;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001266C File Offset: 0x0001086C
		public DateTime ReadDateTime()
		{
			long dateData = 0L;
			DateTime result;
			try
			{
				dateData = this.ReadInt64();
				result = DateTime.FromBinary(dateData);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(dateData.ToString(CultureInfo.InvariantCulture), "DateTime", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(dateData.ToString(CultureInfo.InvariantCulture), "DateTime", exception2));
			}
			catch (OverflowException exception3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(dateData.ToString(CultureInfo.InvariantCulture), "DateTime", exception3));
			}
			return result;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00012714 File Offset: 0x00010914
		public TimeSpan ReadTimeSpan()
		{
			long value = 0L;
			TimeSpan result;
			try
			{
				value = this.ReadInt64();
				result = TimeSpan.FromTicks(value);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value.ToString(CultureInfo.InvariantCulture), "TimeSpan", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value.ToString(CultureInfo.InvariantCulture), "TimeSpan", exception2));
			}
			catch (OverflowException exception3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value.ToString(CultureInfo.InvariantCulture), "TimeSpan", exception3));
			}
			return result;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x000127BC File Offset: 0x000109BC
		public Guid ReadGuid()
		{
			int num;
			this.GetBuffer(16, out num);
			Guid result = this.GetGuid(num);
			this.Advance(16);
			return result;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000127E4 File Offset: 0x000109E4
		public string ReadUTF8String(int length)
		{
			int num;
			this.GetBuffer(length, out num);
			char[] charBuffer = this.GetCharBuffer(length);
			int length2 = this.GetChars(num, length, charBuffer);
			string result = new string(charBuffer, 0, length2);
			this.Advance(length);
			return result;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001281C File Offset: 0x00010A1C
		[SecurityCritical]
		public unsafe void UnsafeReadArray(byte* dst, byte* dstMax)
		{
			this.UnsafeReadArray(dst, (int)((long)(dstMax - dst)));
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001282C File Offset: 0x00010A2C
		[SecurityCritical]
		private unsafe void UnsafeReadArray(byte* dst, int length)
		{
			if (this.stream != null)
			{
				while (length >= 256)
				{
					byte[] array = this.GetBuffer(256, out this.offset);
					for (int i = 0; i < 256; i++)
					{
						*(dst++) = array[this.offset + i];
					}
					this.Advance(256);
					length -= 256;
				}
			}
			if (length > 0)
			{
				fixed (byte* ptr = &this.GetBuffer(length, out this.offset)[this.offset])
				{
					byte* ptr2 = ptr;
					byte* ptr3 = dst + length;
					while (dst < ptr3)
					{
						*dst = *ptr2;
						dst++;
						ptr2++;
					}
				}
				this.Advance(length);
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000128D5 File Offset: 0x00010AD5
		private char[] GetCharBuffer(int count)
		{
			if (count > 1024)
			{
				return new char[count];
			}
			if (this.chars == null || this.chars.Length < count)
			{
				this.chars = new char[count];
			}
			return this.chars;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001290C File Offset: 0x00010B0C
		private int GetChars(int offset, int length, char[] chars)
		{
			byte[] array = this.buffer;
			for (int i = 0; i < length; i++)
			{
				byte b = array[offset + i];
				if (b >= 128)
				{
					return i + XmlConverter.ToChars(array, offset + i, length - i, chars, i);
				}
				chars[i] = (char)b;
			}
			return length;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00012950 File Offset: 0x00010B50
		private int GetChars(int offset, int length, char[] chars, int charOffset)
		{
			byte[] array = this.buffer;
			for (int i = 0; i < length; i++)
			{
				byte b = array[offset + i];
				if (b >= 128)
				{
					return i + XmlConverter.ToChars(array, offset + i, length - i, chars, charOffset + i);
				}
				chars[charOffset + i] = (char)b;
			}
			return length;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001299C File Offset: 0x00010B9C
		public string GetString(int offset, int length)
		{
			char[] charBuffer = this.GetCharBuffer(length);
			int length2 = this.GetChars(offset, length, charBuffer);
			return new string(charBuffer, 0, length2);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000129C3 File Offset: 0x00010BC3
		public string GetUnicodeString(int offset, int length)
		{
			return XmlConverter.ToStringUnicode(this.buffer, offset, length);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000129D4 File Offset: 0x00010BD4
		public string GetString(int offset, int length, XmlNameTable nameTable)
		{
			char[] charBuffer = this.GetCharBuffer(length);
			int length2 = this.GetChars(offset, length, charBuffer);
			return nameTable.Add(charBuffer, 0, length2);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000129FC File Offset: 0x00010BFC
		public int GetEscapedChars(int offset, int length, char[] chars)
		{
			byte[] array = this.buffer;
			int num = 0;
			int num2 = offset;
			int num3 = offset + length;
			for (;;)
			{
				if (offset >= num3 || !this.IsAttrChar((int)array[offset]))
				{
					num += this.GetChars(num2, offset - num2, chars, num);
					if (offset == num3)
					{
						break;
					}
					num2 = offset;
					if (array[offset] == 38)
					{
						while (offset < num3 && array[offset] != 59)
						{
							offset++;
						}
						offset++;
						int charEntity = this.GetCharEntity(num2, offset - num2);
						num2 = offset;
						if (charEntity > 65535)
						{
							SurrogateChar surrogateChar = new SurrogateChar(charEntity);
							chars[num++] = surrogateChar.HighChar;
							chars[num++] = surrogateChar.LowChar;
						}
						else
						{
							chars[num++] = (char)charEntity;
						}
					}
					else if (array[offset] == 10 || array[offset] == 9)
					{
						chars[num++] = ' ';
						offset++;
						num2 = offset;
					}
					else
					{
						chars[num++] = ' ';
						offset++;
						if (offset < num3 && array[offset] == 10)
						{
							offset++;
						}
						num2 = offset;
					}
				}
				else
				{
					offset++;
				}
			}
			return num;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00012AFD File Offset: 0x00010CFD
		private bool IsAttrChar(int ch)
		{
			return ch - 9 > 1 && ch != 13 && ch != 38;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00012B14 File Offset: 0x00010D14
		public string GetEscapedString(int offset, int length)
		{
			char[] charBuffer = this.GetCharBuffer(length);
			int escapedChars = this.GetEscapedChars(offset, length, charBuffer);
			return new string(charBuffer, 0, escapedChars);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00012B3C File Offset: 0x00010D3C
		public string GetEscapedString(int offset, int length, XmlNameTable nameTable)
		{
			char[] charBuffer = this.GetCharBuffer(length);
			int escapedChars = this.GetEscapedChars(offset, length, charBuffer);
			return nameTable.Add(charBuffer, 0, escapedChars);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00012B64 File Offset: 0x00010D64
		private int GetLessThanCharEntity(int offset, int length)
		{
			byte[] array = this.buffer;
			if (length != 4 || array[offset + 1] != 108 || array[offset + 2] != 116)
			{
				XmlExceptionHelper.ThrowInvalidCharRef(this.reader);
			}
			return 60;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00012B9C File Offset: 0x00010D9C
		private int GetGreaterThanCharEntity(int offset, int length)
		{
			byte[] array = this.buffer;
			if (length != 4 || array[offset + 1] != 103 || array[offset + 2] != 116)
			{
				XmlExceptionHelper.ThrowInvalidCharRef(this.reader);
			}
			return 62;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00012BD4 File Offset: 0x00010DD4
		private int GetQuoteCharEntity(int offset, int length)
		{
			byte[] array = this.buffer;
			if (length != 6 || array[offset + 1] != 113 || array[offset + 2] != 117 || array[offset + 3] != 111 || array[offset + 4] != 116)
			{
				XmlExceptionHelper.ThrowInvalidCharRef(this.reader);
			}
			return 34;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00012C20 File Offset: 0x00010E20
		private int GetAmpersandCharEntity(int offset, int length)
		{
			byte[] array = this.buffer;
			if (length != 5 || array[offset + 1] != 97 || array[offset + 2] != 109 || array[offset + 3] != 112)
			{
				XmlExceptionHelper.ThrowInvalidCharRef(this.reader);
			}
			return 38;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00012C60 File Offset: 0x00010E60
		private int GetApostropheCharEntity(int offset, int length)
		{
			byte[] array = this.buffer;
			if (length != 6 || array[offset + 1] != 97 || array[offset + 2] != 112 || array[offset + 3] != 111 || array[offset + 4] != 115)
			{
				XmlExceptionHelper.ThrowInvalidCharRef(this.reader);
			}
			return 39;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00012CAC File Offset: 0x00010EAC
		private int GetDecimalCharEntity(int offset, int length)
		{
			byte[] array = this.buffer;
			int num = 0;
			for (int i = 2; i < length - 1; i++)
			{
				byte b = array[offset + i];
				if (b < 48 || b > 57)
				{
					XmlExceptionHelper.ThrowInvalidCharRef(this.reader);
				}
				num = num * 10 + (int)(b - 48);
				if (num > 1114111)
				{
					XmlExceptionHelper.ThrowInvalidCharRef(this.reader);
				}
			}
			return num;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00012D0C File Offset: 0x00010F0C
		private int GetHexCharEntity(int offset, int length)
		{
			byte[] array = this.buffer;
			int num = 0;
			for (int i = 3; i < length - 1; i++)
			{
				byte b = array[offset + i];
				int num2 = 0;
				if (b >= 48 && b <= 57)
				{
					num2 = (int)(b - 48);
				}
				else if (b >= 97 && b <= 102)
				{
					num2 = (int)(10 + (b - 97));
				}
				else if (b >= 65 && b <= 70)
				{
					num2 = (int)(10 + (b - 65));
				}
				else
				{
					XmlExceptionHelper.ThrowInvalidCharRef(this.reader);
				}
				num = num * 16 + num2;
				if (num > 1114111)
				{
					XmlExceptionHelper.ThrowInvalidCharRef(this.reader);
				}
			}
			return num;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00012D9C File Offset: 0x00010F9C
		public int GetCharEntity(int offset, int length)
		{
			if (length < 3)
			{
				XmlExceptionHelper.ThrowInvalidCharRef(this.reader);
			}
			byte[] array = this.buffer;
			byte b = array[offset + 1];
			if (b <= 97)
			{
				if (b != 35)
				{
					if (b == 97)
					{
						if (array[offset + 2] == 109)
						{
							return this.GetAmpersandCharEntity(offset, length);
						}
						return this.GetApostropheCharEntity(offset, length);
					}
				}
				else
				{
					if (array[offset + 2] == 120)
					{
						return this.GetHexCharEntity(offset, length);
					}
					return this.GetDecimalCharEntity(offset, length);
				}
			}
			else
			{
				if (b == 103)
				{
					return this.GetGreaterThanCharEntity(offset, length);
				}
				if (b == 108)
				{
					return this.GetLessThanCharEntity(offset, length);
				}
				if (b == 113)
				{
					return this.GetQuoteCharEntity(offset, length);
				}
			}
			XmlExceptionHelper.ThrowInvalidCharRef(this.reader);
			return 0;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00012E44 File Offset: 0x00011044
		public bool IsWhitespaceKey(int key)
		{
			string value = this.GetDictionaryString(key).Value;
			for (int i = 0; i < value.Length; i++)
			{
				if (!XmlConverter.IsWhitespace(value[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00012E80 File Offset: 0x00011080
		public bool IsWhitespaceUTF8(int offset, int length)
		{
			byte[] array = this.buffer;
			for (int i = 0; i < length; i++)
			{
				if (!XmlConverter.IsWhitespace((char)array[offset + i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00012EB0 File Offset: 0x000110B0
		public bool IsWhitespaceUnicode(int offset, int length)
		{
			byte[] array = this.buffer;
			for (int i = 0; i < length; i += 2)
			{
				if (!XmlConverter.IsWhitespace((char)this.GetInt16(offset + i)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00012EE4 File Offset: 0x000110E4
		public bool Equals2(int key1, int key2, XmlBufferReader bufferReader2)
		{
			return key1 == key2 || this.GetDictionaryString(key1).Value == bufferReader2.GetDictionaryString(key2).Value;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00012F09 File Offset: 0x00011109
		public bool Equals2(int key1, XmlDictionaryString xmlString2)
		{
			if ((key1 & 1) == 0 && xmlString2.Dictionary == this.dictionary)
			{
				return xmlString2.Key == key1 >> 1;
			}
			return this.GetDictionaryString(key1).Value == xmlString2.Value;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00012F44 File Offset: 0x00011144
		public bool Equals2(int offset1, int length1, byte[] buffer2)
		{
			int num = buffer2.Length;
			if (length1 != num)
			{
				return false;
			}
			byte[] array = this.buffer;
			for (int i = 0; i < length1; i++)
			{
				if (array[offset1 + i] != buffer2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00012F7C File Offset: 0x0001117C
		public bool Equals2(int offset1, int length1, XmlBufferReader bufferReader2, int offset2, int length2)
		{
			if (length1 != length2)
			{
				return false;
			}
			byte[] array = this.buffer;
			byte[] array2 = bufferReader2.buffer;
			for (int i = 0; i < length1; i++)
			{
				if (array[offset1 + i] != array2[offset2 + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00012FBC File Offset: 0x000111BC
		public bool Equals2(int offset1, int length1, int offset2, int length2)
		{
			if (length1 != length2)
			{
				return false;
			}
			if (offset1 == offset2)
			{
				return true;
			}
			byte[] array = this.buffer;
			for (int i = 0; i < length1; i++)
			{
				if (array[offset1 + i] != array[offset2 + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00012FF8 File Offset: 0x000111F8
		[SecuritySafeCritical]
		public unsafe bool Equals2(int offset1, int length1, string s2)
		{
			int length2 = s2.Length;
			if (length1 < length2 || length1 > length2 * 3)
			{
				return false;
			}
			byte[] array = this.buffer;
			if (length1 < 8)
			{
				int num = Math.Min(length1, length2);
				for (int i = 0; i < num; i++)
				{
					byte b = array[offset1 + i];
					if (b >= 128)
					{
						return XmlConverter.ToString(array, offset1, length1) == s2;
					}
					if (s2[i] != (char)b)
					{
						return false;
					}
				}
				return length1 == length2;
			}
			int num2 = Math.Min(length1, length2);
			fixed (byte* ptr = &array[offset1])
			{
				byte* ptr2 = ptr;
				byte* ptr3 = ptr2 + num2;
				fixed (string text = s2)
				{
					char* ptr4 = text;
					if (ptr4 != null)
					{
						ptr4 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr5 = ptr4;
					int num3 = 0;
					while (ptr2 < ptr3 && *ptr2 < 128)
					{
						num3 = (int)(*ptr2 - (byte)(*ptr5));
						if (num3 != 0)
						{
							break;
						}
						ptr2++;
						ptr5++;
					}
					if (num3 != 0)
					{
						return false;
					}
					if (ptr2 == ptr3)
					{
						return length1 == length2;
					}
				}
			}
			return XmlConverter.ToString(array, offset1, length1) == s2;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00013104 File Offset: 0x00011304
		public int Compare(int offset1, int length1, int offset2, int length2)
		{
			byte[] array = this.buffer;
			int num = Math.Min(length1, length2);
			for (int i = 0; i < num; i++)
			{
				int num2 = (int)(array[offset1 + i] - array[offset2 + i]);
				if (num2 != 0)
				{
					return num2;
				}
			}
			return length1 - length2;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00013142 File Offset: 0x00011342
		public byte GetByte(int offset)
		{
			return this.buffer[offset];
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001314C File Offset: 0x0001134C
		public int GetInt8(int offset)
		{
			return (int)((sbyte)this.GetByte(offset));
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00013158 File Offset: 0x00011358
		public int GetInt16(int offset)
		{
			byte[] array = this.buffer;
			return (int)((short)((int)array[offset] + ((int)array[offset + 1] << 8)));
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00013178 File Offset: 0x00011378
		public int GetInt32(int offset)
		{
			byte[] array = this.buffer;
			byte b = array[offset];
			byte b2 = array[offset + 1];
			byte b3 = array[offset + 2];
			return ((((int)array[offset + 3] << 8) + (int)b3 << 8) + (int)b2 << 8) + (int)b;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x000131AC File Offset: 0x000113AC
		public long GetInt64(int offset)
		{
			byte[] array = this.buffer;
			byte b = array[offset];
			byte b2 = array[offset + 1];
			byte b3 = array[offset + 2];
			long num = (long)((ulong)(((((int)array[offset + 3] << 8) + (int)b3 << 8) + (int)b2 << 8) + (int)b));
			b = array[offset + 4];
			b2 = array[offset + 5];
			b3 = array[offset + 6];
			return (long)(((ulong)(((((int)array[offset + 7] << 8) + (int)b3 << 8) + (int)b2 << 8) + (int)b) << 32) + (ulong)num);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001320A File Offset: 0x0001140A
		public ulong GetUInt64(int offset)
		{
			return (ulong)this.GetInt64(offset);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00013214 File Offset: 0x00011414
		[SecuritySafeCritical]
		public unsafe float GetSingle(int offset)
		{
			byte[] array = this.buffer;
			float result;
			byte* ptr = (byte*)(&result);
			*ptr = array[offset];
			ptr[1] = array[offset + 1];
			ptr[2] = array[offset + 2];
			ptr[3] = array[offset + 3];
			return result;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00013250 File Offset: 0x00011450
		[SecuritySafeCritical]
		public unsafe double GetDouble(int offset)
		{
			byte[] array = this.buffer;
			double result;
			byte* ptr = (byte*)(&result);
			*ptr = array[offset];
			ptr[1] = array[offset + 1];
			ptr[2] = array[offset + 2];
			ptr[3] = array[offset + 3];
			ptr[4] = array[offset + 4];
			ptr[5] = array[offset + 5];
			ptr[6] = array[offset + 6];
			ptr[7] = array[offset + 7];
			return result;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000132B0 File Offset: 0x000114B0
		[SecuritySafeCritical]
		public unsafe decimal GetDecimal(int offset)
		{
			byte[] array = this.buffer;
			byte b = array[offset];
			byte b2 = array[offset + 1];
			byte b3 = array[offset + 2];
			int num = ((((int)array[offset + 3] << 8) + (int)b3 << 8) + (int)b2 << 8) + (int)b;
			if ((num & 2130771967) == 0 && (num & 16711680) <= 1835008)
			{
				decimal result;
				byte* ptr = (byte*)(&result);
				for (int i = 0; i < 16; i++)
				{
					ptr[i] = array[offset + i];
				}
				return result;
			}
			XmlExceptionHelper.ThrowInvalidBinaryFormat(this.reader);
			return 0m;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00013335 File Offset: 0x00011535
		public UniqueId GetUniqueId(int offset)
		{
			return new UniqueId(this.buffer, offset);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00013343 File Offset: 0x00011543
		public Guid GetGuid(int offset)
		{
			if (this.guid == null)
			{
				this.guid = new byte[16];
			}
			System.Buffer.BlockCopy(this.buffer, offset, this.guid, 0, this.guid.Length);
			return new Guid(this.guid);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00013380 File Offset: 0x00011580
		public void GetBase64(int srcOffset, byte[] buffer, int dstOffset, int count)
		{
			System.Buffer.BlockCopy(this.buffer, srcOffset, buffer, dstOffset, count);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00013392 File Offset: 0x00011592
		public XmlBinaryNodeType GetNodeType()
		{
			return (XmlBinaryNodeType)this.GetByte();
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001339A File Offset: 0x0001159A
		public void SkipNodeType()
		{
			this.SkipByte();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000133A4 File Offset: 0x000115A4
		public object[] GetList(int offset, int count)
		{
			int num = this.Offset;
			this.Offset = offset;
			object[] result;
			try
			{
				object[] array = new object[count];
				for (int i = 0; i < count; i++)
				{
					XmlBinaryNodeType nodeType = this.GetNodeType();
					this.SkipNodeType();
					this.ReadValue(nodeType, this.listValue);
					array[i] = this.listValue.ToObject();
				}
				result = array;
			}
			finally
			{
				this.Offset = num;
			}
			return result;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001341C File Offset: 0x0001161C
		public XmlDictionaryString GetDictionaryString(int key)
		{
			IXmlDictionary xmlDictionary;
			if ((key & 1) != 0)
			{
				xmlDictionary = this.session;
			}
			else
			{
				xmlDictionary = this.dictionary;
			}
			XmlDictionaryString result;
			if (!xmlDictionary.TryLookup(key >> 1, out result))
			{
				XmlExceptionHelper.ThrowInvalidBinaryFormat(this.reader);
			}
			return result;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00013458 File Offset: 0x00011658
		public int ReadDictionaryKey()
		{
			int num = this.ReadMultiByteUInt31();
			if ((num & 1) != 0)
			{
				if (this.session == null)
				{
					XmlExceptionHelper.ThrowInvalidBinaryFormat(this.reader);
				}
				int num2 = num >> 1;
				XmlDictionaryString xmlDictionaryString;
				if (!this.session.TryLookup(num2, out xmlDictionaryString))
				{
					if (num2 < 0 || num2 > 536870911)
					{
						XmlExceptionHelper.ThrowXmlDictionaryStringIDOutOfRange(this.reader);
					}
					XmlExceptionHelper.ThrowXmlDictionaryStringIDUndefinedSession(this.reader, num2);
				}
			}
			else
			{
				if (this.dictionary == null)
				{
					XmlExceptionHelper.ThrowInvalidBinaryFormat(this.reader);
				}
				int num3 = num >> 1;
				XmlDictionaryString xmlDictionaryString2;
				if (!this.dictionary.TryLookup(num3, out xmlDictionaryString2))
				{
					if (num3 < 0 || num3 > 536870911)
					{
						XmlExceptionHelper.ThrowXmlDictionaryStringIDOutOfRange(this.reader);
					}
					XmlExceptionHelper.ThrowXmlDictionaryStringIDUndefinedStatic(this.reader, num3);
				}
			}
			return num;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00013508 File Offset: 0x00011708
		public void ReadValue(XmlBinaryNodeType nodeType, ValueHandle value)
		{
			switch (nodeType)
			{
			case XmlBinaryNodeType.MinText:
				value.SetValue(ValueHandleType.Zero);
				return;
			case XmlBinaryNodeType.ZeroTextWithEndElement:
			case XmlBinaryNodeType.OneTextWithEndElement:
			case XmlBinaryNodeType.FalseTextWithEndElement:
			case XmlBinaryNodeType.TrueTextWithEndElement:
			case XmlBinaryNodeType.Int8TextWithEndElement:
			case XmlBinaryNodeType.Int16TextWithEndElement:
			case XmlBinaryNodeType.Int32TextWithEndElement:
			case XmlBinaryNodeType.Int64TextWithEndElement:
			case XmlBinaryNodeType.FloatTextWithEndElement:
			case XmlBinaryNodeType.DoubleTextWithEndElement:
			case XmlBinaryNodeType.DecimalTextWithEndElement:
			case XmlBinaryNodeType.DateTimeTextWithEndElement:
			case XmlBinaryNodeType.Chars8TextWithEndElement:
			case XmlBinaryNodeType.Chars16TextWithEndElement:
			case XmlBinaryNodeType.Chars32TextWithEndElement:
			case XmlBinaryNodeType.Bytes8TextWithEndElement:
			case XmlBinaryNodeType.Bytes16TextWithEndElement:
			case XmlBinaryNodeType.Bytes32TextWithEndElement:
				break;
			case XmlBinaryNodeType.OneText:
				value.SetValue(ValueHandleType.One);
				return;
			case XmlBinaryNodeType.FalseText:
				value.SetValue(ValueHandleType.False);
				return;
			case XmlBinaryNodeType.TrueText:
				value.SetValue(ValueHandleType.True);
				return;
			case XmlBinaryNodeType.Int8Text:
				this.ReadValue(value, ValueHandleType.Int8, 1);
				return;
			case XmlBinaryNodeType.Int16Text:
				this.ReadValue(value, ValueHandleType.Int16, 2);
				return;
			case XmlBinaryNodeType.Int32Text:
				this.ReadValue(value, ValueHandleType.Int32, 4);
				return;
			case XmlBinaryNodeType.Int64Text:
				this.ReadValue(value, ValueHandleType.Int64, 8);
				return;
			case XmlBinaryNodeType.FloatText:
				this.ReadValue(value, ValueHandleType.Single, 4);
				return;
			case XmlBinaryNodeType.DoubleText:
				this.ReadValue(value, ValueHandleType.Double, 8);
				return;
			case XmlBinaryNodeType.DecimalText:
				this.ReadValue(value, ValueHandleType.Decimal, 16);
				return;
			case XmlBinaryNodeType.DateTimeText:
				this.ReadValue(value, ValueHandleType.DateTime, 8);
				return;
			case XmlBinaryNodeType.Chars8Text:
				this.ReadValue(value, ValueHandleType.UTF8, this.ReadUInt8());
				return;
			case XmlBinaryNodeType.Chars16Text:
				this.ReadValue(value, ValueHandleType.UTF8, this.ReadUInt16());
				return;
			case XmlBinaryNodeType.Chars32Text:
				this.ReadValue(value, ValueHandleType.UTF8, this.ReadUInt31());
				return;
			case XmlBinaryNodeType.Bytes8Text:
				this.ReadValue(value, ValueHandleType.Base64, this.ReadUInt8());
				return;
			case XmlBinaryNodeType.Bytes16Text:
				this.ReadValue(value, ValueHandleType.Base64, this.ReadUInt16());
				return;
			case XmlBinaryNodeType.Bytes32Text:
				this.ReadValue(value, ValueHandleType.Base64, this.ReadUInt31());
				return;
			case XmlBinaryNodeType.StartListText:
				this.ReadList(value);
				return;
			default:
				switch (nodeType)
				{
				case XmlBinaryNodeType.EmptyText:
					value.SetValue(ValueHandleType.Empty);
					return;
				case XmlBinaryNodeType.DictionaryText:
					value.SetDictionaryValue(this.ReadDictionaryKey());
					return;
				case XmlBinaryNodeType.UniqueIdText:
					this.ReadValue(value, ValueHandleType.UniqueId, 16);
					return;
				case XmlBinaryNodeType.TimeSpanText:
					this.ReadValue(value, ValueHandleType.TimeSpan, 8);
					return;
				case XmlBinaryNodeType.GuidText:
					this.ReadValue(value, ValueHandleType.Guid, 16);
					return;
				case XmlBinaryNodeType.UInt64Text:
					this.ReadValue(value, ValueHandleType.UInt64, 8);
					return;
				case XmlBinaryNodeType.BoolText:
					value.SetValue((this.ReadUInt8() != 0) ? ValueHandleType.True : ValueHandleType.False);
					return;
				case XmlBinaryNodeType.UnicodeChars8Text:
					this.ReadUnicodeValue(value, this.ReadUInt8());
					return;
				case XmlBinaryNodeType.UnicodeChars16Text:
					this.ReadUnicodeValue(value, this.ReadUInt16());
					return;
				case XmlBinaryNodeType.UnicodeChars32Text:
					this.ReadUnicodeValue(value, this.ReadUInt31());
					return;
				case XmlBinaryNodeType.QNameDictionaryText:
					this.ReadQName(value);
					return;
				}
				break;
			}
			XmlExceptionHelper.ThrowInvalidBinaryFormat(this.reader);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001378C File Offset: 0x0001198C
		private void ReadValue(ValueHandle value, ValueHandleType type, int length)
		{
			int num = this.ReadBytes(length);
			value.SetValue(type, num, length);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000137AA File Offset: 0x000119AA
		private void ReadUnicodeValue(ValueHandle value, int length)
		{
			if ((length & 1) != 0)
			{
				XmlExceptionHelper.ThrowInvalidBinaryFormat(this.reader);
			}
			this.ReadValue(value, ValueHandleType.Unicode, length);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000137C8 File Offset: 0x000119C8
		private void ReadList(ValueHandle value)
		{
			if (this.listValue == null)
			{
				this.listValue = new ValueHandle(this);
			}
			int num = 0;
			int num2 = this.Offset;
			for (;;)
			{
				XmlBinaryNodeType nodeType = this.GetNodeType();
				this.SkipNodeType();
				if (nodeType == XmlBinaryNodeType.StartListText)
				{
					XmlExceptionHelper.ThrowInvalidBinaryFormat(this.reader);
				}
				if (nodeType == XmlBinaryNodeType.EndListText)
				{
					break;
				}
				this.ReadValue(nodeType, this.listValue);
				num++;
			}
			value.SetValue(ValueHandleType.List, num2, num);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00013838 File Offset: 0x00011A38
		public void ReadQName(ValueHandle value)
		{
			int num = this.ReadUInt8();
			if (num >= 26)
			{
				XmlExceptionHelper.ThrowInvalidBinaryFormat(this.reader);
			}
			int key = this.ReadDictionaryKey();
			value.SetQNameValue(num, key);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001386C File Offset: 0x00011A6C
		public int[] GetRows()
		{
			if (this.buffer == null)
			{
				return new int[1];
			}
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.offsetMin);
			for (int i = this.offsetMin; i < this.offsetMax; i++)
			{
				if (this.buffer[i] == 13 || this.buffer[i] == 10)
				{
					if (i + 1 < this.offsetMax && this.buffer[i + 1] == 10)
					{
						i++;
					}
					arrayList.Add(i + 1);
				}
			}
			return (int[])arrayList.ToArray(typeof(int));
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001390F File Offset: 0x00011B0F
		// Note: this type is marked as 'beforefieldinit'.
		static XmlBufferReader()
		{
		}

		// Token: 0x0400022E RID: 558
		private XmlDictionaryReader reader;

		// Token: 0x0400022F RID: 559
		private Stream stream;

		// Token: 0x04000230 RID: 560
		private byte[] streamBuffer;

		// Token: 0x04000231 RID: 561
		private byte[] buffer;

		// Token: 0x04000232 RID: 562
		private int offsetMin;

		// Token: 0x04000233 RID: 563
		private int offsetMax;

		// Token: 0x04000234 RID: 564
		private IXmlDictionary dictionary;

		// Token: 0x04000235 RID: 565
		private XmlBinaryReaderSession session;

		// Token: 0x04000236 RID: 566
		private byte[] guid;

		// Token: 0x04000237 RID: 567
		private int offset;

		// Token: 0x04000238 RID: 568
		private const int maxBytesPerChar = 3;

		// Token: 0x04000239 RID: 569
		private char[] chars;

		// Token: 0x0400023A RID: 570
		private int windowOffset;

		// Token: 0x0400023B RID: 571
		private int windowOffsetMax;

		// Token: 0x0400023C RID: 572
		private ValueHandle listValue;

		// Token: 0x0400023D RID: 573
		private static byte[] emptyByteArray = new byte[0];

		// Token: 0x0400023E RID: 574
		private static XmlBufferReader empty = new XmlBufferReader(XmlBufferReader.emptyByteArray);
	}
}
