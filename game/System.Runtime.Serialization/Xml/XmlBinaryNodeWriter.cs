using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200004E RID: 78
	internal class XmlBinaryNodeWriter : XmlStreamNodeWriter
	{
		// Token: 0x0600031C RID: 796 RVA: 0x0000FCE0 File Offset: 0x0000DEE0
		public XmlBinaryNodeWriter()
		{
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
		public void SetOutput(Stream stream, IXmlDictionary dictionary, XmlBinaryWriterSession session, bool ownsStream)
		{
			this.dictionary = dictionary;
			this.session = session;
			this.inAttribute = false;
			this.inList = false;
			this.attributeValue.Clear();
			this.textNodeOffset = -1;
			base.SetOutput(stream, ownsStream, null);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000FD22 File Offset: 0x0000DF22
		private void WriteNode(XmlBinaryNodeType nodeType)
		{
			base.WriteByte((byte)nodeType);
			this.textNodeOffset = -1;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000FD33 File Offset: 0x0000DF33
		private void WroteAttributeValue()
		{
			if (this.wroteAttributeValue && !this.inList)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("Only a single typed value may be written inside an attribute or content.")));
			}
			this.wroteAttributeValue = true;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000FD61 File Offset: 0x0000DF61
		private void WriteTextNode(XmlBinaryNodeType nodeType)
		{
			if (this.inAttribute)
			{
				this.WroteAttributeValue();
			}
			base.WriteByte((byte)nodeType);
			this.textNodeOffset = base.BufferOffset - 1;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000FD87 File Offset: 0x0000DF87
		private byte[] GetTextNodeBuffer(int size, out int offset)
		{
			if (this.inAttribute)
			{
				this.WroteAttributeValue();
			}
			byte[] buffer = base.GetBuffer(size, out offset);
			this.textNodeOffset = offset;
			return buffer;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000FDA8 File Offset: 0x0000DFA8
		private void WriteTextNodeWithLength(XmlBinaryNodeType nodeType, int length)
		{
			int num;
			byte[] textNodeBuffer = this.GetTextNodeBuffer(5, out num);
			if (length < 256)
			{
				textNodeBuffer[num] = (byte)nodeType;
				textNodeBuffer[num + 1] = (byte)length;
				base.Advance(2);
				return;
			}
			if (length < 65536)
			{
				textNodeBuffer[num] = (byte)(nodeType + 2);
				textNodeBuffer[num + 1] = (byte)length;
				length >>= 8;
				textNodeBuffer[num + 2] = (byte)length;
				base.Advance(3);
				return;
			}
			textNodeBuffer[num] = (byte)(nodeType + 4);
			textNodeBuffer[num + 1] = (byte)length;
			length >>= 8;
			textNodeBuffer[num + 2] = (byte)length;
			length >>= 8;
			textNodeBuffer[num + 3] = (byte)length;
			length >>= 8;
			textNodeBuffer[num + 4] = (byte)length;
			base.Advance(5);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000FE40 File Offset: 0x0000E040
		private void WriteTextNodeWithInt64(XmlBinaryNodeType nodeType, long value)
		{
			int num;
			byte[] textNodeBuffer = this.GetTextNodeBuffer(9, out num);
			textNodeBuffer[num] = (byte)nodeType;
			textNodeBuffer[num + 1] = (byte)value;
			value >>= 8;
			textNodeBuffer[num + 2] = (byte)value;
			value >>= 8;
			textNodeBuffer[num + 3] = (byte)value;
			value >>= 8;
			textNodeBuffer[num + 4] = (byte)value;
			value >>= 8;
			textNodeBuffer[num + 5] = (byte)value;
			value >>= 8;
			textNodeBuffer[num + 6] = (byte)value;
			value >>= 8;
			textNodeBuffer[num + 7] = (byte)value;
			value >>= 8;
			textNodeBuffer[num + 8] = (byte)value;
			base.Advance(9);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000A8EE File Offset: 0x00008AEE
		public override void WriteDeclaration()
		{
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000FEC0 File Offset: 0x0000E0C0
		public override void WriteStartElement(string prefix, string localName)
		{
			if (prefix.Length == 0)
			{
				this.WriteNode(XmlBinaryNodeType.MinElement);
				this.WriteName(localName);
				return;
			}
			char c = prefix[0];
			if (prefix.Length == 1 && c >= 'a' && c <= 'z')
			{
				this.WritePrefixNode(XmlBinaryNodeType.PrefixElementA, (int)(c - 'a'));
				this.WriteName(localName);
				return;
			}
			this.WriteNode(XmlBinaryNodeType.Element);
			this.WriteName(prefix);
			this.WriteName(localName);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000FF2A File Offset: 0x0000E12A
		private void WritePrefixNode(XmlBinaryNodeType nodeType, int ch)
		{
			this.WriteNode(nodeType + ch);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000FF38 File Offset: 0x0000E138
		public override void WriteStartElement(string prefix, XmlDictionaryString localName)
		{
			int key;
			if (!this.TryGetKey(localName, out key))
			{
				this.WriteStartElement(prefix, localName.Value);
				return;
			}
			if (prefix.Length == 0)
			{
				this.WriteNode(XmlBinaryNodeType.ShortDictionaryElement);
				this.WriteDictionaryString(localName, key);
				return;
			}
			char c = prefix[0];
			if (prefix.Length == 1 && c >= 'a' && c <= 'z')
			{
				this.WritePrefixNode(XmlBinaryNodeType.PrefixDictionaryElementA, (int)(c - 'a'));
				this.WriteDictionaryString(localName, key);
				return;
			}
			this.WriteNode(XmlBinaryNodeType.DictionaryElement);
			this.WriteName(prefix);
			this.WriteDictionaryString(localName, key);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000FFBE File Offset: 0x0000E1BE
		public override void WriteEndStartElement(bool isEmpty)
		{
			if (isEmpty)
			{
				this.WriteEndElement();
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000FFC9 File Offset: 0x0000E1C9
		public override void WriteEndElement(string prefix, string localName)
		{
			this.WriteEndElement();
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000FFD4 File Offset: 0x0000E1D4
		private void WriteEndElement()
		{
			if (this.textNodeOffset != -1)
			{
				byte[] streamBuffer = base.StreamBuffer;
				XmlBinaryNodeType xmlBinaryNodeType = (XmlBinaryNodeType)streamBuffer[this.textNodeOffset];
				streamBuffer[this.textNodeOffset] = (byte)(xmlBinaryNodeType + 1);
				this.textNodeOffset = -1;
				return;
			}
			this.WriteNode(XmlBinaryNodeType.EndElement);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00010014 File Offset: 0x0000E214
		public override void WriteStartAttribute(string prefix, string localName)
		{
			if (prefix.Length == 0)
			{
				this.WriteNode(XmlBinaryNodeType.MinAttribute);
				this.WriteName(localName);
			}
			else
			{
				char c = prefix[0];
				if (prefix.Length == 1 && c >= 'a' && c <= 'z')
				{
					this.WritePrefixNode(XmlBinaryNodeType.PrefixAttributeA, (int)(c - 'a'));
					this.WriteName(localName);
				}
				else
				{
					this.WriteNode(XmlBinaryNodeType.Attribute);
					this.WriteName(prefix);
					this.WriteName(localName);
				}
			}
			this.inAttribute = true;
			this.wroteAttributeValue = false;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0001008C File Offset: 0x0000E28C
		public override void WriteStartAttribute(string prefix, XmlDictionaryString localName)
		{
			int key;
			if (!this.TryGetKey(localName, out key))
			{
				this.WriteStartAttribute(prefix, localName.Value);
				return;
			}
			if (prefix.Length == 0)
			{
				this.WriteNode(XmlBinaryNodeType.ShortDictionaryAttribute);
				this.WriteDictionaryString(localName, key);
			}
			else
			{
				char c = prefix[0];
				if (prefix.Length == 1 && c >= 'a' && c <= 'z')
				{
					this.WritePrefixNode(XmlBinaryNodeType.PrefixDictionaryAttributeA, (int)(c - 'a'));
					this.WriteDictionaryString(localName, key);
				}
				else
				{
					this.WriteNode(XmlBinaryNodeType.DictionaryAttribute);
					this.WriteName(prefix);
					this.WriteDictionaryString(localName, key);
				}
			}
			this.inAttribute = true;
			this.wroteAttributeValue = false;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00010120 File Offset: 0x0000E320
		public override void WriteEndAttribute()
		{
			this.inAttribute = false;
			if (!this.wroteAttributeValue)
			{
				this.attributeValue.WriteTo(this);
			}
			this.textNodeOffset = -1;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00010144 File Offset: 0x0000E344
		public override void WriteXmlnsAttribute(string prefix, string ns)
		{
			if (prefix.Length == 0)
			{
				this.WriteNode(XmlBinaryNodeType.ShortXmlnsAttribute);
				this.WriteName(ns);
				return;
			}
			this.WriteNode(XmlBinaryNodeType.XmlnsAttribute);
			this.WriteName(prefix);
			this.WriteName(ns);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00010174 File Offset: 0x0000E374
		public override void WriteXmlnsAttribute(string prefix, XmlDictionaryString ns)
		{
			int key;
			if (!this.TryGetKey(ns, out key))
			{
				this.WriteXmlnsAttribute(prefix, ns.Value);
				return;
			}
			if (prefix.Length == 0)
			{
				this.WriteNode(XmlBinaryNodeType.ShortDictionaryXmlnsAttribute);
				this.WriteDictionaryString(ns, key);
				return;
			}
			this.WriteNode(XmlBinaryNodeType.DictionaryXmlnsAttribute);
			this.WriteName(prefix);
			this.WriteDictionaryString(ns, key);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000101CC File Offset: 0x0000E3CC
		private bool TryGetKey(XmlDictionaryString s, out int key)
		{
			key = -1;
			if (s.Dictionary == this.dictionary)
			{
				key = s.Key * 2;
				return true;
			}
			XmlDictionaryString xmlDictionaryString;
			if (this.dictionary != null && this.dictionary.TryLookup(s, out xmlDictionaryString))
			{
				key = xmlDictionaryString.Key * 2;
				return true;
			}
			if (this.session == null)
			{
				return false;
			}
			int num;
			if (!this.session.TryLookup(s, out num) && !this.session.TryAdd(s, out num))
			{
				return false;
			}
			key = num * 2 + 1;
			return true;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001024E File Offset: 0x0000E44E
		private void WriteDictionaryString(XmlDictionaryString s, int key)
		{
			this.WriteMultiByteInt32(key);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00010258 File Offset: 0x0000E458
		[SecuritySafeCritical]
		private unsafe void WriteName(string s)
		{
			int length = s.Length;
			if (length == 0)
			{
				base.WriteByte(0);
				return;
			}
			fixed (string text = s)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.UnsafeWriteName(ptr, length);
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00010294 File Offset: 0x0000E494
		[SecurityCritical]
		private unsafe void UnsafeWriteName(char* chars, int charCount)
		{
			if (charCount < 42)
			{
				int num;
				byte[] buffer = base.GetBuffer(1 + charCount * 3, out num);
				int num2 = base.UnsafeGetUTF8Chars(chars, charCount, buffer, num + 1);
				buffer[num] = (byte)num2;
				base.Advance(1 + num2);
				return;
			}
			int i = base.UnsafeGetUTF8Length(chars, charCount);
			this.WriteMultiByteInt32(i);
			base.UnsafeWriteUTF8Chars(chars, charCount);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000102E8 File Offset: 0x0000E4E8
		private void WriteMultiByteInt32(int i)
		{
			int num;
			byte[] buffer = base.GetBuffer(5, out num);
			int num2 = num;
			while (((long)i & (long)((ulong)-128)) != 0L)
			{
				buffer[num++] = (byte)((i & 127) | 128);
				i >>= 7;
			}
			buffer[num++] = (byte)i;
			base.Advance(num - num2);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00010334 File Offset: 0x0000E534
		public override void WriteComment(string value)
		{
			this.WriteNode(XmlBinaryNodeType.Comment);
			this.WriteName(value);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00010344 File Offset: 0x0000E544
		public override void WriteCData(string value)
		{
			this.WriteText(value);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0001034D File Offset: 0x0000E54D
		private void WriteEmptyText()
		{
			this.WriteTextNode(XmlBinaryNodeType.EmptyText);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0001035A File Offset: 0x0000E55A
		public override void WriteBoolText(bool value)
		{
			if (value)
			{
				this.WriteTextNode(XmlBinaryNodeType.TrueText);
				return;
			}
			this.WriteTextNode(XmlBinaryNodeType.FalseText);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00010378 File Offset: 0x0000E578
		public override void WriteInt32Text(int value)
		{
			if (value >= -128 && value < 128)
			{
				if (value == 0)
				{
					this.WriteTextNode(XmlBinaryNodeType.MinText);
					return;
				}
				if (value == 1)
				{
					this.WriteTextNode(XmlBinaryNodeType.OneText);
					return;
				}
				int num;
				byte[] textNodeBuffer = this.GetTextNodeBuffer(2, out num);
				textNodeBuffer[num] = 136;
				textNodeBuffer[num + 1] = (byte)value;
				base.Advance(2);
				return;
			}
			else
			{
				if (value >= -32768 && value < 32768)
				{
					int num2;
					byte[] textNodeBuffer2 = this.GetTextNodeBuffer(3, out num2);
					textNodeBuffer2[num2] = 138;
					textNodeBuffer2[num2 + 1] = (byte)value;
					value >>= 8;
					textNodeBuffer2[num2 + 2] = (byte)value;
					base.Advance(3);
					return;
				}
				int num3;
				byte[] textNodeBuffer3 = this.GetTextNodeBuffer(5, out num3);
				textNodeBuffer3[num3] = 140;
				textNodeBuffer3[num3 + 1] = (byte)value;
				value >>= 8;
				textNodeBuffer3[num3 + 2] = (byte)value;
				value >>= 8;
				textNodeBuffer3[num3 + 3] = (byte)value;
				value >>= 8;
				textNodeBuffer3[num3 + 4] = (byte)value;
				base.Advance(5);
				return;
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0001044D File Offset: 0x0000E64D
		public override void WriteInt64Text(long value)
		{
			if (value >= -2147483648L && value <= 2147483647L)
			{
				this.WriteInt32Text((int)value);
				return;
			}
			this.WriteTextNodeWithInt64(XmlBinaryNodeType.Int64Text, value);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00010476 File Offset: 0x0000E676
		public override void WriteUInt64Text(ulong value)
		{
			if (value <= 9223372036854775807UL)
			{
				this.WriteInt64Text((long)value);
				return;
			}
			this.WriteTextNodeWithInt64(XmlBinaryNodeType.UInt64Text, (long)value);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00010498 File Offset: 0x0000E698
		private void WriteInt64(long value)
		{
			int num;
			byte[] buffer = base.GetBuffer(8, out num);
			buffer[num] = (byte)value;
			value >>= 8;
			buffer[num + 1] = (byte)value;
			value >>= 8;
			buffer[num + 2] = (byte)value;
			value >>= 8;
			buffer[num + 3] = (byte)value;
			value >>= 8;
			buffer[num + 4] = (byte)value;
			value >>= 8;
			buffer[num + 5] = (byte)value;
			value >>= 8;
			buffer[num + 6] = (byte)value;
			value >>= 8;
			buffer[num + 7] = (byte)value;
			base.Advance(8);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00010510 File Offset: 0x0000E710
		public override void WriteBase64Text(byte[] trailBytes, int trailByteCount, byte[] base64Buffer, int base64Offset, int base64Count)
		{
			if (this.inAttribute)
			{
				this.attributeValue.WriteBase64Text(trailBytes, trailByteCount, base64Buffer, base64Offset, base64Count);
				return;
			}
			int num = trailByteCount + base64Count;
			if (num > 0)
			{
				this.WriteTextNodeWithLength(XmlBinaryNodeType.Bytes8Text, num);
				if (trailByteCount > 0)
				{
					int num2;
					byte[] buffer = base.GetBuffer(trailByteCount, out num2);
					for (int i = 0; i < trailByteCount; i++)
					{
						buffer[num2 + i] = trailBytes[i];
					}
					base.Advance(trailByteCount);
				}
				if (base64Count > 0)
				{
					base.WriteBytes(base64Buffer, base64Offset, base64Count);
					return;
				}
			}
			else
			{
				this.WriteEmptyText();
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00010590 File Offset: 0x0000E790
		public override void WriteText(XmlDictionaryString value)
		{
			if (this.inAttribute)
			{
				this.attributeValue.WriteText(value);
				return;
			}
			int key;
			if (!this.TryGetKey(value, out key))
			{
				this.WriteText(value.Value);
				return;
			}
			this.WriteTextNode(XmlBinaryNodeType.DictionaryText);
			this.WriteDictionaryString(value, key);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000105E0 File Offset: 0x0000E7E0
		[SecuritySafeCritical]
		public unsafe override void WriteText(string value)
		{
			if (this.inAttribute)
			{
				this.attributeValue.WriteText(value);
				return;
			}
			if (value.Length > 0)
			{
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					this.UnsafeWriteText(ptr, value.Length);
				}
				return;
			}
			this.WriteEmptyText();
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00010634 File Offset: 0x0000E834
		[SecuritySafeCritical]
		public unsafe override void WriteText(char[] chars, int offset, int count)
		{
			if (this.inAttribute)
			{
				this.attributeValue.WriteText(new string(chars, offset, count));
				return;
			}
			if (count > 0)
			{
				fixed (char* ptr = &chars[offset])
				{
					char* chars2 = ptr;
					this.UnsafeWriteText(chars2, count);
				}
				return;
			}
			this.WriteEmptyText();
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0001067E File Offset: 0x0000E87E
		public override void WriteText(byte[] chars, int charOffset, int charCount)
		{
			this.WriteTextNodeWithLength(XmlBinaryNodeType.Chars8Text, charCount);
			base.WriteBytes(chars, charOffset, charCount);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00010698 File Offset: 0x0000E898
		[SecurityCritical]
		private unsafe void UnsafeWriteText(char* chars, int charCount)
		{
			if (charCount == 1)
			{
				char c = *chars;
				if (c == '0')
				{
					this.WriteTextNode(XmlBinaryNodeType.MinText);
					return;
				}
				if (c == '1')
				{
					this.WriteTextNode(XmlBinaryNodeType.OneText);
					return;
				}
			}
			if (charCount <= 85)
			{
				int num;
				byte[] buffer = base.GetBuffer(2 + charCount * 3, out num);
				int num2 = base.UnsafeGetUTF8Chars(chars, charCount, buffer, num + 2);
				if (num2 / 2 <= charCount)
				{
					buffer[num] = 152;
				}
				else
				{
					buffer[num] = 182;
					num2 = base.UnsafeGetUnicodeChars(chars, charCount, buffer, num + 2);
				}
				this.textNodeOffset = num;
				buffer[num + 1] = (byte)num2;
				base.Advance(2 + num2);
				return;
			}
			int num3 = base.UnsafeGetUTF8Length(chars, charCount);
			if (num3 / 2 > charCount)
			{
				this.WriteTextNodeWithLength(XmlBinaryNodeType.UnicodeChars8Text, charCount * 2);
				base.UnsafeWriteUnicodeChars(chars, charCount);
				return;
			}
			this.WriteTextNodeWithLength(XmlBinaryNodeType.Chars8Text, num3);
			base.UnsafeWriteUTF8Chars(chars, charCount);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00010344 File Offset: 0x0000E544
		public override void WriteEscapedText(string value)
		{
			this.WriteText(value);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00010768 File Offset: 0x0000E968
		public override void WriteEscapedText(XmlDictionaryString value)
		{
			this.WriteText(value);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00010771 File Offset: 0x0000E971
		public override void WriteEscapedText(char[] chars, int offset, int count)
		{
			this.WriteText(chars, offset, count);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0001077C File Offset: 0x0000E97C
		public override void WriteEscapedText(byte[] chars, int offset, int count)
		{
			this.WriteText(chars, offset, count);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00010788 File Offset: 0x0000E988
		public override void WriteCharEntity(int ch)
		{
			if (ch > 65535)
			{
				SurrogateChar surrogateChar = new SurrogateChar(ch);
				char[] chars = new char[]
				{
					surrogateChar.HighChar,
					surrogateChar.LowChar
				};
				this.WriteText(chars, 0, 2);
				return;
			}
			char[] chars2 = new char[]
			{
				(char)ch
			};
			this.WriteText(chars2, 0, 1);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000107E0 File Offset: 0x0000E9E0
		[SecuritySafeCritical]
		public unsafe override void WriteFloatText(float f)
		{
			long value;
			if (f >= -9.223372E+18f && f <= 9.223372E+18f && (float)(value = (long)f) == f)
			{
				this.WriteInt64Text(value);
				return;
			}
			int num;
			byte[] textNodeBuffer = this.GetTextNodeBuffer(5, out num);
			byte* ptr = (byte*)(&f);
			textNodeBuffer[num] = 144;
			textNodeBuffer[num + 1] = *ptr;
			textNodeBuffer[num + 2] = ptr[1];
			textNodeBuffer[num + 3] = ptr[2];
			textNodeBuffer[num + 4] = ptr[3];
			base.Advance(5);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0001084C File Offset: 0x0000EA4C
		[SecuritySafeCritical]
		public unsafe override void WriteDoubleText(double d)
		{
			float value;
			if (d >= -3.4028234663852886E+38 && d <= 3.4028234663852886E+38 && (double)(value = (float)d) == d)
			{
				this.WriteFloatText(value);
				return;
			}
			int num;
			byte[] textNodeBuffer = this.GetTextNodeBuffer(9, out num);
			byte* ptr = (byte*)(&d);
			textNodeBuffer[num] = 146;
			textNodeBuffer[num + 1] = *ptr;
			textNodeBuffer[num + 2] = ptr[1];
			textNodeBuffer[num + 3] = ptr[2];
			textNodeBuffer[num + 4] = ptr[3];
			textNodeBuffer[num + 5] = ptr[4];
			textNodeBuffer[num + 6] = ptr[5];
			textNodeBuffer[num + 7] = ptr[6];
			textNodeBuffer[num + 8] = ptr[7];
			base.Advance(9);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000108E4 File Offset: 0x0000EAE4
		[SecuritySafeCritical]
		public unsafe override void WriteDecimalText(decimal d)
		{
			int num;
			byte[] textNodeBuffer = this.GetTextNodeBuffer(17, out num);
			byte* ptr = (byte*)(&d);
			textNodeBuffer[num++] = 148;
			for (int i = 0; i < 16; i++)
			{
				textNodeBuffer[num++] = ptr[i];
			}
			base.Advance(17);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001092C File Offset: 0x0000EB2C
		public override void WriteDateTimeText(DateTime dt)
		{
			this.WriteTextNodeWithInt64(XmlBinaryNodeType.DateTimeText, dt.ToBinary());
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00010940 File Offset: 0x0000EB40
		public override void WriteUniqueIdText(UniqueId value)
		{
			if (value.IsGuid)
			{
				int num;
				byte[] textNodeBuffer = this.GetTextNodeBuffer(17, out num);
				textNodeBuffer[num] = 172;
				value.TryGetGuid(textNodeBuffer, num + 1);
				base.Advance(17);
				return;
			}
			this.WriteText(value.ToString());
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00010988 File Offset: 0x0000EB88
		public override void WriteGuidText(Guid guid)
		{
			int num;
			byte[] textNodeBuffer = this.GetTextNodeBuffer(17, out num);
			textNodeBuffer[num] = 176;
			Buffer.BlockCopy(guid.ToByteArray(), 0, textNodeBuffer, num + 1, 16);
			base.Advance(17);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000109C3 File Offset: 0x0000EBC3
		public override void WriteTimeSpanText(TimeSpan value)
		{
			this.WriteTextNodeWithInt64(XmlBinaryNodeType.TimeSpanText, value.Ticks);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000109D7 File Offset: 0x0000EBD7
		public override void WriteStartListText()
		{
			this.inList = true;
			this.WriteNode(XmlBinaryNodeType.StartListText);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000A8EE File Offset: 0x00008AEE
		public override void WriteListSeparator()
		{
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000109EB File Offset: 0x0000EBEB
		public override void WriteEndListText()
		{
			this.inList = false;
			this.wroteAttributeValue = true;
			this.WriteNode(XmlBinaryNodeType.EndListText);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00010A06 File Offset: 0x0000EC06
		public void WriteArrayNode()
		{
			this.WriteNode(XmlBinaryNodeType.Array);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00010A0F File Offset: 0x0000EC0F
		private void WriteArrayInfo(XmlBinaryNodeType nodeType, int count)
		{
			this.WriteNode(nodeType);
			this.WriteMultiByteInt32(count);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00010A1F File Offset: 0x0000EC1F
		[SecurityCritical]
		public unsafe void UnsafeWriteArray(XmlBinaryNodeType nodeType, int count, byte* array, byte* arrayMax)
		{
			this.WriteArrayInfo(nodeType, count);
			this.UnsafeWriteArray(array, (int)((long)(arrayMax - array)));
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00010A38 File Offset: 0x0000EC38
		[SecurityCritical]
		private unsafe void UnsafeWriteArray(byte* array, int byteCount)
		{
			base.UnsafeWriteBytes(array, byteCount);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00010A44 File Offset: 0x0000EC44
		public void WriteDateTimeArray(DateTime[] array, int offset, int count)
		{
			this.WriteArrayInfo(XmlBinaryNodeType.DateTimeTextWithEndElement, count);
			for (int i = 0; i < count; i++)
			{
				this.WriteInt64(array[offset + i].ToBinary());
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00010A80 File Offset: 0x0000EC80
		public void WriteGuidArray(Guid[] array, int offset, int count)
		{
			this.WriteArrayInfo(XmlBinaryNodeType.GuidTextWithEndElement, count);
			for (int i = 0; i < count; i++)
			{
				byte[] byteBuffer = array[offset + i].ToByteArray();
				base.WriteBytes(byteBuffer, 0, 16);
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00010AC0 File Offset: 0x0000ECC0
		public void WriteTimeSpanArray(TimeSpan[] array, int offset, int count)
		{
			this.WriteArrayInfo(XmlBinaryNodeType.TimeSpanTextWithEndElement, count);
			for (int i = 0; i < count; i++)
			{
				this.WriteInt64(array[offset + i].Ticks);
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00010AFC File Offset: 0x0000ECFC
		public override void WriteQualifiedName(string prefix, XmlDictionaryString localName)
		{
			if (prefix.Length == 0)
			{
				this.WriteText(localName);
				return;
			}
			char c = prefix[0];
			int key;
			if (prefix.Length == 1 && c >= 'a' && c <= 'z' && this.TryGetKey(localName, out key))
			{
				this.WriteTextNode(XmlBinaryNodeType.QNameDictionaryText);
				base.WriteByte((byte)(c - 'a'));
				this.WriteDictionaryString(localName, key);
				return;
			}
			this.WriteText(prefix);
			this.WriteText(":");
			this.WriteText(localName);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00010B77 File Offset: 0x0000ED77
		protected override void FlushBuffer()
		{
			base.FlushBuffer();
			this.textNodeOffset = -1;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00010B86 File Offset: 0x0000ED86
		public override void Close()
		{
			base.Close();
			this.attributeValue.Clear();
		}

		// Token: 0x04000215 RID: 533
		private IXmlDictionary dictionary;

		// Token: 0x04000216 RID: 534
		private XmlBinaryWriterSession session;

		// Token: 0x04000217 RID: 535
		private bool inAttribute;

		// Token: 0x04000218 RID: 536
		private bool inList;

		// Token: 0x04000219 RID: 537
		private bool wroteAttributeValue;

		// Token: 0x0400021A RID: 538
		private XmlBinaryNodeWriter.AttributeValue attributeValue;

		// Token: 0x0400021B RID: 539
		private const int maxBytesPerChar = 3;

		// Token: 0x0400021C RID: 540
		private int textNodeOffset;

		// Token: 0x0200004F RID: 79
		private struct AttributeValue
		{
			// Token: 0x0600035C RID: 860 RVA: 0x00010B99 File Offset: 0x0000ED99
			public void Clear()
			{
				this.captureText = null;
				this.captureXText = null;
				this.captureStream = null;
			}

			// Token: 0x0600035D RID: 861 RVA: 0x00010BB0 File Offset: 0x0000EDB0
			public void WriteText(string s)
			{
				if (this.captureStream != null)
				{
					this.captureText = XmlConverter.Base64Encoding.GetString(this.captureStream.GetBuffer(), 0, (int)this.captureStream.Length);
					this.captureStream = null;
				}
				if (this.captureXText != null)
				{
					this.captureText = this.captureXText.Value;
					this.captureXText = null;
				}
				if (this.captureText == null || this.captureText.Length == 0)
				{
					this.captureText = s;
					return;
				}
				this.captureText += s;
			}

			// Token: 0x0600035E RID: 862 RVA: 0x00010C43 File Offset: 0x0000EE43
			public void WriteText(XmlDictionaryString s)
			{
				if (this.captureText != null || this.captureStream != null)
				{
					this.WriteText(s.Value);
					return;
				}
				this.captureXText = s;
			}

			// Token: 0x0600035F RID: 863 RVA: 0x00010C6C File Offset: 0x0000EE6C
			public void WriteBase64Text(byte[] trailBytes, int trailByteCount, byte[] buffer, int offset, int count)
			{
				if (this.captureText != null || this.captureXText != null)
				{
					if (trailByteCount > 0)
					{
						this.WriteText(XmlConverter.Base64Encoding.GetString(trailBytes, 0, trailByteCount));
					}
					this.WriteText(XmlConverter.Base64Encoding.GetString(buffer, offset, count));
					return;
				}
				if (this.captureStream == null)
				{
					this.captureStream = new MemoryStream();
				}
				if (trailByteCount > 0)
				{
					this.captureStream.Write(trailBytes, 0, trailByteCount);
				}
				this.captureStream.Write(buffer, offset, count);
			}

			// Token: 0x06000360 RID: 864 RVA: 0x00010CEC File Offset: 0x0000EEEC
			public void WriteTo(XmlBinaryNodeWriter writer)
			{
				if (this.captureText != null)
				{
					writer.WriteText(this.captureText);
					this.captureText = null;
					return;
				}
				if (this.captureXText != null)
				{
					writer.WriteText(this.captureXText);
					this.captureXText = null;
					return;
				}
				if (this.captureStream != null)
				{
					writer.WriteBase64Text(null, 0, this.captureStream.GetBuffer(), 0, (int)this.captureStream.Length);
					this.captureStream = null;
					return;
				}
				writer.WriteEmptyText();
			}

			// Token: 0x0400021D RID: 541
			private string captureText;

			// Token: 0x0400021E RID: 542
			private XmlDictionaryString captureXText;

			// Token: 0x0400021F RID: 543
			private MemoryStream captureStream;
		}
	}
}
