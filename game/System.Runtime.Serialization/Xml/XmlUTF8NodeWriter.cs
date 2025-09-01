using System;
using System.IO;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200009E RID: 158
	internal class XmlUTF8NodeWriter : XmlStreamNodeWriter
	{
		// Token: 0x06000852 RID: 2130 RVA: 0x000224CF File Offset: 0x000206CF
		public XmlUTF8NodeWriter() : this(XmlUTF8NodeWriter.defaultIsEscapedAttributeChar, XmlUTF8NodeWriter.defaultIsEscapedElementChar)
		{
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000224E1 File Offset: 0x000206E1
		public XmlUTF8NodeWriter(bool[] isEscapedAttributeChar, bool[] isEscapedElementChar)
		{
			this.isEscapedAttributeChar = isEscapedAttributeChar;
			this.isEscapedElementChar = isEscapedElementChar;
			this.inAttribute = false;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00022500 File Offset: 0x00020700
		public new void SetOutput(Stream stream, bool ownsStream, Encoding encoding)
		{
			Encoding encoding2 = null;
			if (encoding != null && encoding.CodePage == Encoding.UTF8.CodePage)
			{
				encoding2 = encoding;
				encoding = null;
			}
			base.SetOutput(stream, ownsStream, encoding2);
			this.encoding = encoding;
			this.inAttribute = false;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00022540 File Offset: 0x00020740
		public Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00022548 File Offset: 0x00020748
		private byte[] GetCharEntityBuffer()
		{
			if (this.entityChars == null)
			{
				this.entityChars = new byte[32];
			}
			return this.entityChars;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00022565 File Offset: 0x00020765
		private char[] GetCharBuffer(int charCount)
		{
			if (charCount >= 256)
			{
				return new char[charCount];
			}
			if (this.chars == null || this.chars.Length < charCount)
			{
				this.chars = new char[charCount];
			}
			return this.chars;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0002259C File Offset: 0x0002079C
		public override void WriteDeclaration()
		{
			if (this.encoding == null)
			{
				base.WriteUTF8Chars(XmlUTF8NodeWriter.utf8Decl, 0, XmlUTF8NodeWriter.utf8Decl.Length);
				return;
			}
			base.WriteUTF8Chars(XmlUTF8NodeWriter.startDecl, 0, XmlUTF8NodeWriter.startDecl.Length);
			if (this.encoding.WebName == Encoding.BigEndianUnicode.WebName)
			{
				base.WriteUTF8Chars("utf-16BE");
			}
			else
			{
				base.WriteUTF8Chars(this.encoding.WebName);
			}
			base.WriteUTF8Chars(XmlUTF8NodeWriter.endDecl, 0, XmlUTF8NodeWriter.endDecl.Length);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00022628 File Offset: 0x00020828
		public override void WriteCData(string text)
		{
			int num;
			byte[] buffer = base.GetBuffer(9, out num);
			buffer[num] = 60;
			buffer[num + 1] = 33;
			buffer[num + 2] = 91;
			buffer[num + 3] = 67;
			buffer[num + 4] = 68;
			buffer[num + 5] = 65;
			buffer[num + 6] = 84;
			buffer[num + 7] = 65;
			buffer[num + 8] = 91;
			base.Advance(9);
			base.WriteUTF8Chars(text);
			byte[] buffer2 = base.GetBuffer(3, out num);
			buffer2[num] = 93;
			buffer2[num + 1] = 93;
			buffer2[num + 2] = 62;
			base.Advance(3);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000226AC File Offset: 0x000208AC
		private void WriteStartComment()
		{
			int num;
			byte[] buffer = base.GetBuffer(4, out num);
			buffer[num] = 60;
			buffer[num + 1] = 33;
			buffer[num + 2] = 45;
			buffer[num + 3] = 45;
			base.Advance(4);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x000226E4 File Offset: 0x000208E4
		private void WriteEndComment()
		{
			int num;
			byte[] buffer = base.GetBuffer(3, out num);
			buffer[num] = 45;
			buffer[num + 1] = 45;
			buffer[num + 2] = 62;
			base.Advance(3);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00022713 File Offset: 0x00020913
		public override void WriteComment(string text)
		{
			this.WriteStartComment();
			base.WriteUTF8Chars(text);
			this.WriteEndComment();
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00022728 File Offset: 0x00020928
		public override void WriteStartElement(string prefix, string localName)
		{
			base.WriteByte('<');
			if (prefix.Length != 0)
			{
				this.WritePrefix(prefix);
				base.WriteByte(':');
			}
			this.WriteLocalName(localName);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00022750 File Offset: 0x00020950
		public override void WriteStartElement(string prefix, XmlDictionaryString localName)
		{
			this.WriteStartElement(prefix, localName.Value);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0002275F File Offset: 0x0002095F
		public override void WriteStartElement(byte[] prefixBuffer, int prefixOffset, int prefixLength, byte[] localNameBuffer, int localNameOffset, int localNameLength)
		{
			base.WriteByte('<');
			if (prefixLength != 0)
			{
				this.WritePrefix(prefixBuffer, prefixOffset, prefixLength);
				base.WriteByte(':');
			}
			this.WriteLocalName(localNameBuffer, localNameOffset, localNameLength);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00022789 File Offset: 0x00020989
		public override void WriteEndStartElement(bool isEmpty)
		{
			if (!isEmpty)
			{
				base.WriteByte('>');
				return;
			}
			base.WriteBytes('/', '>');
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000227A1 File Offset: 0x000209A1
		public override void WriteEndElement(string prefix, string localName)
		{
			base.WriteBytes('<', '/');
			if (prefix.Length != 0)
			{
				this.WritePrefix(prefix);
				base.WriteByte(':');
			}
			this.WriteLocalName(localName);
			base.WriteByte('>');
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000227D3 File Offset: 0x000209D3
		public override void WriteEndElement(byte[] prefixBuffer, int prefixOffset, int prefixLength, byte[] localNameBuffer, int localNameOffset, int localNameLength)
		{
			base.WriteBytes('<', '/');
			if (prefixLength != 0)
			{
				this.WritePrefix(prefixBuffer, prefixOffset, prefixLength);
				base.WriteByte(':');
			}
			this.WriteLocalName(localNameBuffer, localNameOffset, localNameLength);
			base.WriteByte('>');
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00022808 File Offset: 0x00020A08
		private void WriteStartXmlnsAttribute()
		{
			int num;
			byte[] buffer = base.GetBuffer(6, out num);
			buffer[num] = 32;
			buffer[num + 1] = 120;
			buffer[num + 2] = 109;
			buffer[num + 3] = 108;
			buffer[num + 4] = 110;
			buffer[num + 5] = 115;
			base.Advance(6);
			this.inAttribute = true;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00022853 File Offset: 0x00020A53
		public override void WriteXmlnsAttribute(string prefix, string ns)
		{
			this.WriteStartXmlnsAttribute();
			if (prefix.Length != 0)
			{
				base.WriteByte(':');
				this.WritePrefix(prefix);
			}
			base.WriteBytes('=', '"');
			this.WriteEscapedText(ns);
			this.WriteEndAttribute();
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00022889 File Offset: 0x00020A89
		public override void WriteXmlnsAttribute(string prefix, XmlDictionaryString ns)
		{
			this.WriteXmlnsAttribute(prefix, ns.Value);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00022898 File Offset: 0x00020A98
		public override void WriteXmlnsAttribute(byte[] prefixBuffer, int prefixOffset, int prefixLength, byte[] nsBuffer, int nsOffset, int nsLength)
		{
			this.WriteStartXmlnsAttribute();
			if (prefixLength != 0)
			{
				base.WriteByte(':');
				this.WritePrefix(prefixBuffer, prefixOffset, prefixLength);
			}
			base.WriteBytes('=', '"');
			this.WriteEscapedText(nsBuffer, nsOffset, nsLength);
			this.WriteEndAttribute();
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000228D0 File Offset: 0x00020AD0
		public override void WriteStartAttribute(string prefix, string localName)
		{
			base.WriteByte(' ');
			if (prefix.Length != 0)
			{
				this.WritePrefix(prefix);
				base.WriteByte(':');
			}
			this.WriteLocalName(localName);
			base.WriteBytes('=', '"');
			this.inAttribute = true;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00022909 File Offset: 0x00020B09
		public override void WriteStartAttribute(string prefix, XmlDictionaryString localName)
		{
			this.WriteStartAttribute(prefix, localName.Value);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00022918 File Offset: 0x00020B18
		public override void WriteStartAttribute(byte[] prefixBuffer, int prefixOffset, int prefixLength, byte[] localNameBuffer, int localNameOffset, int localNameLength)
		{
			base.WriteByte(' ');
			if (prefixLength != 0)
			{
				this.WritePrefix(prefixBuffer, prefixOffset, prefixLength);
				base.WriteByte(':');
			}
			this.WriteLocalName(localNameBuffer, localNameOffset, localNameLength);
			base.WriteBytes('=', '"');
			this.inAttribute = true;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00022953 File Offset: 0x00020B53
		public override void WriteEndAttribute()
		{
			base.WriteByte('"');
			this.inAttribute = false;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00022964 File Offset: 0x00020B64
		private void WritePrefix(string prefix)
		{
			if (prefix.Length == 1)
			{
				base.WriteUTF8Char((int)prefix[0]);
				return;
			}
			base.WriteUTF8Chars(prefix);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00022984 File Offset: 0x00020B84
		private void WritePrefix(byte[] prefixBuffer, int prefixOffset, int prefixLength)
		{
			if (prefixLength == 1)
			{
				base.WriteUTF8Char((int)prefixBuffer[prefixOffset]);
				return;
			}
			base.WriteUTF8Chars(prefixBuffer, prefixOffset, prefixLength);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0002299D File Offset: 0x00020B9D
		private void WriteLocalName(string localName)
		{
			base.WriteUTF8Chars(localName);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000229A6 File Offset: 0x00020BA6
		private void WriteLocalName(byte[] localNameBuffer, int localNameOffset, int localNameLength)
		{
			base.WriteUTF8Chars(localNameBuffer, localNameOffset, localNameLength);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000229B1 File Offset: 0x00020BB1
		public override void WriteEscapedText(XmlDictionaryString s)
		{
			this.WriteEscapedText(s.Value);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000229C0 File Offset: 0x00020BC0
		[SecuritySafeCritical]
		public unsafe override void WriteEscapedText(string s)
		{
			int length = s.Length;
			if (length > 0)
			{
				fixed (string text = s)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					this.UnsafeWriteEscapedText(ptr, length);
				}
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000229F4 File Offset: 0x00020BF4
		[SecuritySafeCritical]
		public unsafe override void WriteEscapedText(char[] s, int offset, int count)
		{
			if (count > 0)
			{
				fixed (char* ptr = &s[offset])
				{
					char* ptr2 = ptr;
					this.UnsafeWriteEscapedText(ptr2, count);
				}
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00022A1C File Offset: 0x00020C1C
		[SecurityCritical]
		private unsafe void UnsafeWriteEscapedText(char* chars, int count)
		{
			bool[] array = this.inAttribute ? this.isEscapedAttributeChar : this.isEscapedElementChar;
			int num = array.Length;
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				char c = chars[i];
				if (((int)c < num && array[(int)c]) || c >= '￾')
				{
					base.UnsafeWriteUTF8Chars(chars + num2, i - num2);
					this.WriteCharEntity((int)c);
					num2 = i + 1;
				}
			}
			base.UnsafeWriteUTF8Chars(chars + num2, count - num2);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00022A9C File Offset: 0x00020C9C
		public override void WriteEscapedText(byte[] chars, int offset, int count)
		{
			bool[] array = this.inAttribute ? this.isEscapedAttributeChar : this.isEscapedElementChar;
			int num = array.Length;
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				byte b = chars[offset + i];
				if ((int)b < num && array[(int)b])
				{
					base.WriteUTF8Chars(chars, offset + num2, i - num2);
					this.WriteCharEntity((int)b);
					num2 = i + 1;
				}
				else if (b == 239 && offset + i + 2 < count)
				{
					int num3 = (int)chars[offset + i + 1];
					byte b2 = chars[offset + i + 2];
					if (num3 == 191 && (b2 == 190 || b2 == 191))
					{
						base.WriteUTF8Chars(chars, offset + num2, i - num2);
						this.WriteCharEntity((b2 == 190) ? 65534 : 65535);
						num2 = i + 3;
					}
				}
			}
			base.WriteUTF8Chars(chars, offset + num2, count - num2);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00022B78 File Offset: 0x00020D78
		public void WriteText(int ch)
		{
			base.WriteUTF8Char(ch);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000229A6 File Offset: 0x00020BA6
		public override void WriteText(byte[] chars, int offset, int count)
		{
			base.WriteUTF8Chars(chars, offset, count);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00022B84 File Offset: 0x00020D84
		[SecuritySafeCritical]
		public unsafe override void WriteText(char[] chars, int offset, int count)
		{
			if (count > 0)
			{
				fixed (char* ptr = &chars[offset])
				{
					char* ptr2 = ptr;
					base.UnsafeWriteUTF8Chars(ptr2, count);
				}
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0002299D File Offset: 0x00020B9D
		public override void WriteText(string value)
		{
			base.WriteUTF8Chars(value);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00022BAB File Offset: 0x00020DAB
		public override void WriteText(XmlDictionaryString value)
		{
			base.WriteUTF8Chars(value.Value);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00022BBC File Offset: 0x00020DBC
		public void WriteLessThanCharEntity()
		{
			int num;
			byte[] buffer = base.GetBuffer(4, out num);
			buffer[num] = 38;
			buffer[num + 1] = 108;
			buffer[num + 2] = 116;
			buffer[num + 3] = 59;
			base.Advance(4);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00022BF4 File Offset: 0x00020DF4
		public void WriteGreaterThanCharEntity()
		{
			int num;
			byte[] buffer = base.GetBuffer(4, out num);
			buffer[num] = 38;
			buffer[num + 1] = 103;
			buffer[num + 2] = 116;
			buffer[num + 3] = 59;
			base.Advance(4);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00022C2C File Offset: 0x00020E2C
		public void WriteAmpersandCharEntity()
		{
			int num;
			byte[] buffer = base.GetBuffer(5, out num);
			buffer[num] = 38;
			buffer[num + 1] = 97;
			buffer[num + 2] = 109;
			buffer[num + 3] = 112;
			buffer[num + 4] = 59;
			base.Advance(5);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00022C6C File Offset: 0x00020E6C
		public void WriteApostropheCharEntity()
		{
			int num;
			byte[] buffer = base.GetBuffer(6, out num);
			buffer[num] = 38;
			buffer[num + 1] = 97;
			buffer[num + 2] = 112;
			buffer[num + 3] = 111;
			buffer[num + 4] = 115;
			buffer[num + 5] = 59;
			base.Advance(6);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00022CB0 File Offset: 0x00020EB0
		public void WriteQuoteCharEntity()
		{
			int num;
			byte[] buffer = base.GetBuffer(6, out num);
			buffer[num] = 38;
			buffer[num + 1] = 113;
			buffer[num + 2] = 117;
			buffer[num + 3] = 111;
			buffer[num + 4] = 116;
			buffer[num + 5] = 59;
			base.Advance(6);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00022CF4 File Offset: 0x00020EF4
		private void WriteHexCharEntity(int ch)
		{
			byte[] charEntityBuffer = this.GetCharEntityBuffer();
			int num = 32;
			charEntityBuffer[--num] = 59;
			num -= this.ToBase16(charEntityBuffer, num, (uint)ch);
			charEntityBuffer[--num] = 120;
			charEntityBuffer[--num] = 35;
			charEntityBuffer[--num] = 38;
			base.WriteUTF8Chars(charEntityBuffer, num, 32 - num);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00022D48 File Offset: 0x00020F48
		public override void WriteCharEntity(int ch)
		{
			if (ch <= 38)
			{
				if (ch == 34)
				{
					this.WriteQuoteCharEntity();
					return;
				}
				if (ch == 38)
				{
					this.WriteAmpersandCharEntity();
					return;
				}
			}
			else
			{
				if (ch == 39)
				{
					this.WriteApostropheCharEntity();
					return;
				}
				if (ch == 60)
				{
					this.WriteLessThanCharEntity();
					return;
				}
				if (ch == 62)
				{
					this.WriteGreaterThanCharEntity();
					return;
				}
			}
			this.WriteHexCharEntity(ch);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00022DA4 File Offset: 0x00020FA4
		private int ToBase16(byte[] chars, int offset, uint value)
		{
			int num = 0;
			do
			{
				num++;
				chars[--offset] = XmlUTF8NodeWriter.digits[(int)(value & 15U)];
				value /= 16U;
			}
			while (value != 0U);
			return num;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00022DD4 File Offset: 0x00020FD4
		public override void WriteBoolText(bool value)
		{
			int offset;
			byte[] buffer = base.GetBuffer(5, out offset);
			base.Advance(XmlConverter.ToChars(value, buffer, offset));
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00022DFC File Offset: 0x00020FFC
		public override void WriteDecimalText(decimal value)
		{
			int offset;
			byte[] buffer = base.GetBuffer(40, out offset);
			base.Advance(XmlConverter.ToChars(value, buffer, offset));
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00022E24 File Offset: 0x00021024
		public override void WriteDoubleText(double value)
		{
			int offset;
			byte[] buffer = base.GetBuffer(32, out offset);
			base.Advance(XmlConverter.ToChars(value, buffer, offset));
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00022E4C File Offset: 0x0002104C
		public override void WriteFloatText(float value)
		{
			int offset;
			byte[] buffer = base.GetBuffer(16, out offset);
			base.Advance(XmlConverter.ToChars(value, buffer, offset));
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00022E74 File Offset: 0x00021074
		public override void WriteDateTimeText(DateTime value)
		{
			int offset;
			byte[] buffer = base.GetBuffer(64, out offset);
			base.Advance(XmlConverter.ToChars(value, buffer, offset));
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00022E9C File Offset: 0x0002109C
		public override void WriteUniqueIdText(UniqueId value)
		{
			if (value.IsGuid)
			{
				int charArrayLength = value.CharArrayLength;
				char[] charBuffer = this.GetCharBuffer(charArrayLength);
				value.ToCharArray(charBuffer, 0);
				this.WriteText(charBuffer, 0, charArrayLength);
				return;
			}
			this.WriteEscapedText(value.ToString());
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00022EE0 File Offset: 0x000210E0
		public override void WriteInt32Text(int value)
		{
			int offset;
			byte[] buffer = base.GetBuffer(16, out offset);
			base.Advance(XmlConverter.ToChars(value, buffer, offset));
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00022F08 File Offset: 0x00021108
		public override void WriteInt64Text(long value)
		{
			int offset;
			byte[] buffer = base.GetBuffer(32, out offset);
			base.Advance(XmlConverter.ToChars(value, buffer, offset));
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00022F30 File Offset: 0x00021130
		public override void WriteUInt64Text(ulong value)
		{
			int offset;
			byte[] buffer = base.GetBuffer(32, out offset);
			base.Advance(XmlConverter.ToChars(value, buffer, offset));
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00022F56 File Offset: 0x00021156
		public override void WriteGuidText(Guid value)
		{
			this.WriteText(value.ToString());
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00022F6B File Offset: 0x0002116B
		public override void WriteBase64Text(byte[] trailBytes, int trailByteCount, byte[] buffer, int offset, int count)
		{
			if (trailByteCount > 0)
			{
				this.InternalWriteBase64Text(trailBytes, 0, trailByteCount);
			}
			this.InternalWriteBase64Text(buffer, offset, count);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00022F88 File Offset: 0x00021188
		private void InternalWriteBase64Text(byte[] buffer, int offset, int count)
		{
			Base64Encoding base64Encoding = XmlConverter.Base64Encoding;
			while (count >= 3)
			{
				int num = Math.Min(384, count - count % 3);
				int count2 = num / 3 * 4;
				int charIndex;
				byte[] buffer2 = base.GetBuffer(count2, out charIndex);
				base.Advance(base64Encoding.GetChars(buffer, offset, num, buffer2, charIndex));
				offset += num;
				count -= num;
			}
			if (count > 0)
			{
				int charIndex2;
				byte[] buffer3 = base.GetBuffer(4, out charIndex2);
				base.Advance(base64Encoding.GetChars(buffer, offset, count, buffer3, charIndex2));
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00023000 File Offset: 0x00021200
		internal override AsyncCompletionResult WriteBase64TextAsync(AsyncEventArgs<XmlNodeWriterWriteBase64TextArgs> xmlNodeWriterState)
		{
			if (this.internalWriteBase64TextAsyncWriter == null)
			{
				this.internalWriteBase64TextAsyncWriter = new XmlUTF8NodeWriter.InternalWriteBase64TextAsyncWriter(this);
			}
			return this.internalWriteBase64TextAsyncWriter.StartAsync(xmlNodeWriterState);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00023022 File Offset: 0x00021222
		public override IAsyncResult BeginWriteBase64Text(byte[] trailBytes, int trailByteCount, byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return new XmlUTF8NodeWriter.WriteBase64TextAsyncResult(trailBytes, trailByteCount, buffer, offset, count, this, callback, state);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00023035 File Offset: 0x00021235
		public override void EndWriteBase64Text(IAsyncResult result)
		{
			XmlUTF8NodeWriter.WriteBase64TextAsyncResult.End(result);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0002303D File Offset: 0x0002123D
		private IAsyncResult BeginInternalWriteBase64Text(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return new XmlUTF8NodeWriter.InternalWriteBase64TextAsyncResult(buffer, offset, count, this, callback, state);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0002304C File Offset: 0x0002124C
		private void EndInternalWriteBase64Text(IAsyncResult result)
		{
			XmlUTF8NodeWriter.InternalWriteBase64TextAsyncResult.End(result);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00023054 File Offset: 0x00021254
		public override void WriteTimeSpanText(TimeSpan value)
		{
			this.WriteText(XmlConvert.ToString(value));
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0000A8EE File Offset: 0x00008AEE
		public override void WriteStartListText()
		{
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00023062 File Offset: 0x00021262
		public override void WriteListSeparator()
		{
			base.WriteByte(' ');
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0000A8EE File Offset: 0x00008AEE
		public override void WriteEndListText()
		{
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0002306C File Offset: 0x0002126C
		public override void WriteQualifiedName(string prefix, XmlDictionaryString localName)
		{
			if (prefix.Length != 0)
			{
				this.WritePrefix(prefix);
				base.WriteByte(':');
			}
			this.WriteText(localName);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0002308C File Offset: 0x0002128C
		// Note: this type is marked as 'beforefieldinit'.
		static XmlUTF8NodeWriter()
		{
		}

		// Token: 0x040003AD RID: 941
		private byte[] entityChars;

		// Token: 0x040003AE RID: 942
		private bool[] isEscapedAttributeChar;

		// Token: 0x040003AF RID: 943
		private bool[] isEscapedElementChar;

		// Token: 0x040003B0 RID: 944
		private bool inAttribute;

		// Token: 0x040003B1 RID: 945
		private const int bufferLength = 512;

		// Token: 0x040003B2 RID: 946
		private const int maxEntityLength = 32;

		// Token: 0x040003B3 RID: 947
		private const int maxBytesPerChar = 3;

		// Token: 0x040003B4 RID: 948
		private Encoding encoding;

		// Token: 0x040003B5 RID: 949
		private char[] chars;

		// Token: 0x040003B6 RID: 950
		private XmlUTF8NodeWriter.InternalWriteBase64TextAsyncWriter internalWriteBase64TextAsyncWriter;

		// Token: 0x040003B7 RID: 951
		private static readonly byte[] startDecl = new byte[]
		{
			60,
			63,
			120,
			109,
			108,
			32,
			118,
			101,
			114,
			115,
			105,
			111,
			110,
			61,
			34,
			49,
			46,
			48,
			34,
			32,
			101,
			110,
			99,
			111,
			100,
			105,
			110,
			103,
			61,
			34
		};

		// Token: 0x040003B8 RID: 952
		private static readonly byte[] endDecl = new byte[]
		{
			34,
			63,
			62
		};

		// Token: 0x040003B9 RID: 953
		private static readonly byte[] utf8Decl = new byte[]
		{
			60,
			63,
			120,
			109,
			108,
			32,
			118,
			101,
			114,
			115,
			105,
			111,
			110,
			61,
			34,
			49,
			46,
			48,
			34,
			32,
			101,
			110,
			99,
			111,
			100,
			105,
			110,
			103,
			61,
			34,
			117,
			116,
			102,
			45,
			56,
			34,
			63,
			62
		};

		// Token: 0x040003BA RID: 954
		private static readonly byte[] digits = new byte[]
		{
			48,
			49,
			50,
			51,
			52,
			53,
			54,
			55,
			56,
			57,
			65,
			66,
			67,
			68,
			69,
			70
		};

		// Token: 0x040003BB RID: 955
		private static readonly bool[] defaultIsEscapedAttributeChar = new bool[]
		{
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			true,
			false,
			false,
			false,
			true,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			true,
			false,
			true,
			false
		};

		// Token: 0x040003BC RID: 956
		private static readonly bool[] defaultIsEscapedElementChar = new bool[]
		{
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			false,
			false,
			false,
			false,
			true,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			true,
			false,
			true,
			false
		};

		// Token: 0x0200009F RID: 159
		private class InternalWriteBase64TextAsyncWriter
		{
			// Token: 0x06000898 RID: 2200 RVA: 0x00023122 File Offset: 0x00021322
			public InternalWriteBase64TextAsyncWriter(XmlUTF8NodeWriter writer)
			{
				this.writer = writer;
				this.writerState = new AsyncEventArgs<XmlWriteBase64AsyncArguments>();
				this.writerArgs = new XmlWriteBase64AsyncArguments();
			}

			// Token: 0x06000899 RID: 2201 RVA: 0x00023148 File Offset: 0x00021348
			internal AsyncCompletionResult StartAsync(AsyncEventArgs<XmlNodeWriterWriteBase64TextArgs> xmlNodeWriterState)
			{
				this.nodeState = xmlNodeWriterState;
				XmlNodeWriterWriteBase64TextArgs arguments = xmlNodeWriterState.Arguments;
				if (arguments.TrailCount > 0)
				{
					this.writerArgs.Buffer = arguments.TrailBuffer;
					this.writerArgs.Offset = 0;
					this.writerArgs.Count = arguments.TrailCount;
					this.writerState.Set(XmlUTF8NodeWriter.InternalWriteBase64TextAsyncWriter.onTrailByteComplete, this.writerArgs, this);
					if (this.InternalWriteBase64TextAsync(this.writerState) != AsyncCompletionResult.Completed)
					{
						return AsyncCompletionResult.Queued;
					}
					this.writerState.Complete(true);
				}
				if (this.WriteBufferAsync() == AsyncCompletionResult.Completed)
				{
					this.nodeState = null;
					return AsyncCompletionResult.Completed;
				}
				return AsyncCompletionResult.Queued;
			}

			// Token: 0x0600089A RID: 2202 RVA: 0x000231E4 File Offset: 0x000213E4
			private static void OnTrailBytesComplete(IAsyncEventArgs eventArgs)
			{
				XmlUTF8NodeWriter.InternalWriteBase64TextAsyncWriter internalWriteBase64TextAsyncWriter = (XmlUTF8NodeWriter.InternalWriteBase64TextAsyncWriter)eventArgs.AsyncState;
				bool flag = false;
				try
				{
					if (eventArgs.Exception != null)
					{
						Exception exception = eventArgs.Exception;
						flag = true;
					}
					else if (internalWriteBase64TextAsyncWriter.WriteBufferAsync() == AsyncCompletionResult.Completed)
					{
						flag = true;
					}
				}
				catch (Exception exception2)
				{
					if (Fx.IsFatal(exception2))
					{
						throw;
					}
					flag = true;
				}
				if (flag)
				{
					AsyncEventArgs<XmlNodeWriterWriteBase64TextArgs> asyncEventArgs = internalWriteBase64TextAsyncWriter.nodeState;
					internalWriteBase64TextAsyncWriter.nodeState = null;
					asyncEventArgs.Complete(false, eventArgs.Exception);
				}
			}

			// Token: 0x0600089B RID: 2203 RVA: 0x0002325C File Offset: 0x0002145C
			private AsyncCompletionResult WriteBufferAsync()
			{
				this.writerArgs.Buffer = this.nodeState.Arguments.Buffer;
				this.writerArgs.Offset = this.nodeState.Arguments.Offset;
				this.writerArgs.Count = this.nodeState.Arguments.Count;
				this.writerState.Set(XmlUTF8NodeWriter.InternalWriteBase64TextAsyncWriter.onWriteComplete, this.writerArgs, this);
				if (this.InternalWriteBase64TextAsync(this.writerState) == AsyncCompletionResult.Completed)
				{
					this.writerState.Complete(true);
					return AsyncCompletionResult.Completed;
				}
				return AsyncCompletionResult.Queued;
			}

			// Token: 0x0600089C RID: 2204 RVA: 0x000232F0 File Offset: 0x000214F0
			private static void OnWriteComplete(IAsyncEventArgs eventArgs)
			{
				XmlUTF8NodeWriter.InternalWriteBase64TextAsyncWriter internalWriteBase64TextAsyncWriter = (XmlUTF8NodeWriter.InternalWriteBase64TextAsyncWriter)eventArgs.AsyncState;
				AsyncEventArgs<XmlNodeWriterWriteBase64TextArgs> asyncEventArgs = internalWriteBase64TextAsyncWriter.nodeState;
				internalWriteBase64TextAsyncWriter.nodeState = null;
				asyncEventArgs.Complete(false, eventArgs.Exception);
			}

			// Token: 0x0600089D RID: 2205 RVA: 0x00023324 File Offset: 0x00021524
			private AsyncCompletionResult InternalWriteBase64TextAsync(AsyncEventArgs<XmlWriteBase64AsyncArguments> writerState)
			{
				XmlStreamNodeWriter.GetBufferAsyncEventArgs getBufferAsyncEventArgs = this.getBufferState;
				XmlStreamNodeWriter.GetBufferArgs getBufferArgs = this.getBufferArgs;
				XmlWriteBase64AsyncArguments arguments = writerState.Arguments;
				if (getBufferAsyncEventArgs == null)
				{
					getBufferAsyncEventArgs = new XmlStreamNodeWriter.GetBufferAsyncEventArgs();
					getBufferArgs = new XmlStreamNodeWriter.GetBufferArgs();
					this.getBufferState = getBufferAsyncEventArgs;
					this.getBufferArgs = getBufferArgs;
				}
				Base64Encoding base64Encoding = XmlConverter.Base64Encoding;
				while (arguments.Count >= 3)
				{
					int num = Math.Min(384, arguments.Count - arguments.Count % 3);
					int count = num / 3 * 4;
					getBufferArgs.Count = count;
					getBufferAsyncEventArgs.Set(XmlUTF8NodeWriter.InternalWriteBase64TextAsyncWriter.onGetBufferComplete, getBufferArgs, this);
					if (this.writer.GetBufferAsync(getBufferAsyncEventArgs) != AsyncCompletionResult.Completed)
					{
						return AsyncCompletionResult.Queued;
					}
					XmlStreamNodeWriter.GetBufferEventResult result = getBufferAsyncEventArgs.Result;
					getBufferAsyncEventArgs.Complete(true);
					this.writer.Advance(base64Encoding.GetChars(arguments.Buffer, arguments.Offset, num, result.Buffer, result.Offset));
					arguments.Offset += num;
					arguments.Count -= num;
				}
				if (arguments.Count > 0)
				{
					getBufferArgs.Count = 4;
					getBufferAsyncEventArgs.Set(XmlUTF8NodeWriter.InternalWriteBase64TextAsyncWriter.onGetBufferComplete, getBufferArgs, this);
					if (this.writer.GetBufferAsync(getBufferAsyncEventArgs) != AsyncCompletionResult.Completed)
					{
						return AsyncCompletionResult.Queued;
					}
					XmlStreamNodeWriter.GetBufferEventResult result2 = getBufferAsyncEventArgs.Result;
					getBufferAsyncEventArgs.Complete(true);
					this.writer.Advance(base64Encoding.GetChars(arguments.Buffer, arguments.Offset, arguments.Count, result2.Buffer, result2.Offset));
				}
				return AsyncCompletionResult.Completed;
			}

			// Token: 0x0600089E RID: 2206 RVA: 0x00023490 File Offset: 0x00021690
			private static void OnGetBufferComplete(IAsyncEventArgs state)
			{
				XmlStreamNodeWriter.GetBufferEventResult result = ((XmlStreamNodeWriter.GetBufferAsyncEventArgs)state).Result;
				XmlUTF8NodeWriter.InternalWriteBase64TextAsyncWriter internalWriteBase64TextAsyncWriter = (XmlUTF8NodeWriter.InternalWriteBase64TextAsyncWriter)state.AsyncState;
				XmlWriteBase64AsyncArguments arguments = internalWriteBase64TextAsyncWriter.writerState.Arguments;
				Exception exception = null;
				bool flag = false;
				try
				{
					if (state.Exception != null)
					{
						exception = state.Exception;
						flag = true;
					}
					else
					{
						byte[] buffer = result.Buffer;
						int offset = result.Offset;
						Base64Encoding base64Encoding = XmlConverter.Base64Encoding;
						int num = Math.Min(384, arguments.Count - arguments.Count % 3);
						int num2 = num / 3;
						internalWriteBase64TextAsyncWriter.writer.Advance(base64Encoding.GetChars(arguments.Buffer, arguments.Offset, num, buffer, offset));
						if (num >= 3)
						{
							arguments.Offset += num;
							arguments.Count -= num;
						}
						if (internalWriteBase64TextAsyncWriter.InternalWriteBase64TextAsync(internalWriteBase64TextAsyncWriter.writerState) == AsyncCompletionResult.Completed)
						{
							flag = true;
						}
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
					flag = true;
				}
				if (flag)
				{
					internalWriteBase64TextAsyncWriter.writerState.Complete(false, exception);
				}
			}

			// Token: 0x0600089F RID: 2207 RVA: 0x000235A0 File Offset: 0x000217A0
			// Note: this type is marked as 'beforefieldinit'.
			static InternalWriteBase64TextAsyncWriter()
			{
			}

			// Token: 0x040003BD RID: 957
			private AsyncEventArgs<XmlNodeWriterWriteBase64TextArgs> nodeState;

			// Token: 0x040003BE RID: 958
			private AsyncEventArgs<XmlWriteBase64AsyncArguments> writerState;

			// Token: 0x040003BF RID: 959
			private XmlWriteBase64AsyncArguments writerArgs;

			// Token: 0x040003C0 RID: 960
			private XmlUTF8NodeWriter writer;

			// Token: 0x040003C1 RID: 961
			private XmlStreamNodeWriter.GetBufferAsyncEventArgs getBufferState;

			// Token: 0x040003C2 RID: 962
			private XmlStreamNodeWriter.GetBufferArgs getBufferArgs;

			// Token: 0x040003C3 RID: 963
			private static AsyncEventArgsCallback onTrailByteComplete = new AsyncEventArgsCallback(XmlUTF8NodeWriter.InternalWriteBase64TextAsyncWriter.OnTrailBytesComplete);

			// Token: 0x040003C4 RID: 964
			private static AsyncEventArgsCallback onWriteComplete = new AsyncEventArgsCallback(XmlUTF8NodeWriter.InternalWriteBase64TextAsyncWriter.OnWriteComplete);

			// Token: 0x040003C5 RID: 965
			private static AsyncEventArgsCallback onGetBufferComplete = new AsyncEventArgsCallback(XmlUTF8NodeWriter.InternalWriteBase64TextAsyncWriter.OnGetBufferComplete);
		}

		// Token: 0x020000A0 RID: 160
		private class WriteBase64TextAsyncResult : AsyncResult
		{
			// Token: 0x060008A0 RID: 2208 RVA: 0x000235D8 File Offset: 0x000217D8
			public WriteBase64TextAsyncResult(byte[] trailBytes, int trailByteCount, byte[] buffer, int offset, int count, XmlUTF8NodeWriter writer, AsyncCallback callback, object state) : base(callback, state)
			{
				this.writer = writer;
				this.trailBytes = trailBytes;
				this.trailByteCount = trailByteCount;
				this.buffer = buffer;
				this.offset = offset;
				this.count = count;
				if (this.HandleWriteTrailBytes(null))
				{
					base.Complete(true);
				}
			}

			// Token: 0x060008A1 RID: 2209 RVA: 0x0002362C File Offset: 0x0002182C
			private static bool OnTrailBytesComplete(IAsyncResult result)
			{
				return ((XmlUTF8NodeWriter.WriteBase64TextAsyncResult)result.AsyncState).HandleWriteTrailBytes(result);
			}

			// Token: 0x060008A2 RID: 2210 RVA: 0x0002363F File Offset: 0x0002183F
			private static bool OnComplete(IAsyncResult result)
			{
				return ((XmlUTF8NodeWriter.WriteBase64TextAsyncResult)result.AsyncState).HandleWriteBase64Text(result);
			}

			// Token: 0x060008A3 RID: 2211 RVA: 0x00023654 File Offset: 0x00021854
			private bool HandleWriteTrailBytes(IAsyncResult result)
			{
				if (this.trailByteCount > 0)
				{
					if (result == null)
					{
						result = this.writer.BeginInternalWriteBase64Text(this.trailBytes, 0, this.trailByteCount, base.PrepareAsyncCompletion(XmlUTF8NodeWriter.WriteBase64TextAsyncResult.onTrailBytesComplete), this);
						if (!result.CompletedSynchronously)
						{
							return false;
						}
					}
					this.writer.EndInternalWriteBase64Text(result);
				}
				return this.HandleWriteBase64Text(null);
			}

			// Token: 0x060008A4 RID: 2212 RVA: 0x000236B0 File Offset: 0x000218B0
			private bool HandleWriteBase64Text(IAsyncResult result)
			{
				if (result == null)
				{
					result = this.writer.BeginInternalWriteBase64Text(this.buffer, this.offset, this.count, base.PrepareAsyncCompletion(XmlUTF8NodeWriter.WriteBase64TextAsyncResult.onComplete), this);
					if (!result.CompletedSynchronously)
					{
						return false;
					}
				}
				this.writer.EndInternalWriteBase64Text(result);
				return true;
			}

			// Token: 0x060008A5 RID: 2213 RVA: 0x00023702 File Offset: 0x00021902
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<XmlUTF8NodeWriter.WriteBase64TextAsyncResult>(result);
			}

			// Token: 0x060008A6 RID: 2214 RVA: 0x0002370B File Offset: 0x0002190B
			// Note: this type is marked as 'beforefieldinit'.
			static WriteBase64TextAsyncResult()
			{
			}

			// Token: 0x040003C6 RID: 966
			private static AsyncResult.AsyncCompletion onTrailBytesComplete = new AsyncResult.AsyncCompletion(XmlUTF8NodeWriter.WriteBase64TextAsyncResult.OnTrailBytesComplete);

			// Token: 0x040003C7 RID: 967
			private static AsyncResult.AsyncCompletion onComplete = new AsyncResult.AsyncCompletion(XmlUTF8NodeWriter.WriteBase64TextAsyncResult.OnComplete);

			// Token: 0x040003C8 RID: 968
			private byte[] trailBytes;

			// Token: 0x040003C9 RID: 969
			private int trailByteCount;

			// Token: 0x040003CA RID: 970
			private byte[] buffer;

			// Token: 0x040003CB RID: 971
			private int offset;

			// Token: 0x040003CC RID: 972
			private int count;

			// Token: 0x040003CD RID: 973
			private XmlUTF8NodeWriter writer;
		}

		// Token: 0x020000A1 RID: 161
		private class InternalWriteBase64TextAsyncResult : AsyncResult
		{
			// Token: 0x060008A7 RID: 2215 RVA: 0x00023730 File Offset: 0x00021930
			public InternalWriteBase64TextAsyncResult(byte[] buffer, int offset, int count, XmlUTF8NodeWriter writer, AsyncCallback callback, object state) : base(callback, state)
			{
				this.buffer = buffer;
				this.offset = offset;
				this.count = count;
				this.writer = writer;
				this.encoding = XmlConverter.Base64Encoding;
				if (this.ContinueWork())
				{
					base.Complete(true);
				}
			}

			// Token: 0x060008A8 RID: 2216 RVA: 0x0002377E File Offset: 0x0002197E
			private static bool OnWriteTrailingCharacters(IAsyncResult result)
			{
				return ((XmlUTF8NodeWriter.InternalWriteBase64TextAsyncResult)result.AsyncState).HandleWriteTrailingCharacters(result);
			}

			// Token: 0x060008A9 RID: 2217 RVA: 0x00023791 File Offset: 0x00021991
			private bool ContinueWork()
			{
				while (this.count >= 3)
				{
					if (!this.HandleWriteCharacters(null))
					{
						return false;
					}
				}
				return this.count <= 0 || this.HandleWriteTrailingCharacters(null);
			}

			// Token: 0x060008AA RID: 2218 RVA: 0x000237BC File Offset: 0x000219BC
			private bool HandleWriteCharacters(IAsyncResult result)
			{
				int num = Math.Min(384, this.count - this.count % 3);
				int num2 = num / 3 * 4;
				if (result == null)
				{
					result = this.writer.BeginGetBuffer(num2, XmlUTF8NodeWriter.InternalWriteBase64TextAsyncResult.onWriteCharacters, this);
					if (!result.CompletedSynchronously)
					{
						return false;
					}
				}
				int charIndex;
				byte[] chars = this.writer.EndGetBuffer(result, out charIndex);
				this.writer.Advance(this.encoding.GetChars(this.buffer, this.offset, num, chars, charIndex));
				this.offset += num;
				this.count -= num;
				return true;
			}

			// Token: 0x060008AB RID: 2219 RVA: 0x0002385C File Offset: 0x00021A5C
			private bool HandleWriteTrailingCharacters(IAsyncResult result)
			{
				if (result == null)
				{
					result = this.writer.BeginGetBuffer(4, base.PrepareAsyncCompletion(XmlUTF8NodeWriter.InternalWriteBase64TextAsyncResult.onWriteTrailingCharacters), this);
					if (!result.CompletedSynchronously)
					{
						return false;
					}
				}
				int charIndex;
				byte[] chars = this.writer.EndGetBuffer(result, out charIndex);
				this.writer.Advance(this.encoding.GetChars(this.buffer, this.offset, this.count, chars, charIndex));
				return true;
			}

			// Token: 0x060008AC RID: 2220 RVA: 0x000238CC File Offset: 0x00021ACC
			private static void OnWriteCharacters(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				XmlUTF8NodeWriter.InternalWriteBase64TextAsyncResult internalWriteBase64TextAsyncResult = (XmlUTF8NodeWriter.InternalWriteBase64TextAsyncResult)result.AsyncState;
				Exception exception = null;
				bool flag = false;
				try
				{
					internalWriteBase64TextAsyncResult.HandleWriteCharacters(result);
					flag = internalWriteBase64TextAsyncResult.ContinueWork();
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					internalWriteBase64TextAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x060008AD RID: 2221 RVA: 0x00023930 File Offset: 0x00021B30
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<XmlUTF8NodeWriter.InternalWriteBase64TextAsyncResult>(result);
			}

			// Token: 0x060008AE RID: 2222 RVA: 0x00023939 File Offset: 0x00021B39
			// Note: this type is marked as 'beforefieldinit'.
			static InternalWriteBase64TextAsyncResult()
			{
			}

			// Token: 0x040003CE RID: 974
			private byte[] buffer;

			// Token: 0x040003CF RID: 975
			private int offset;

			// Token: 0x040003D0 RID: 976
			private int count;

			// Token: 0x040003D1 RID: 977
			private Base64Encoding encoding;

			// Token: 0x040003D2 RID: 978
			private XmlUTF8NodeWriter writer;

			// Token: 0x040003D3 RID: 979
			private static AsyncCallback onWriteCharacters = Fx.ThunkCallback(new AsyncCallback(XmlUTF8NodeWriter.InternalWriteBase64TextAsyncResult.OnWriteCharacters));

			// Token: 0x040003D4 RID: 980
			private static AsyncResult.AsyncCompletion onWriteTrailingCharacters = new AsyncResult.AsyncCompletion(XmlUTF8NodeWriter.InternalWriteBase64TextAsyncResult.OnWriteTrailingCharacters);
		}
	}
}
