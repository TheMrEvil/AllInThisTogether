using System;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200009A RID: 154
	internal class XmlUTF8TextReader : XmlBaseReader, IXmlLineInfo, IXmlTextReaderInitializer
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x00020FA8 File Offset: 0x0001F1A8
		public XmlUTF8TextReader()
		{
			this.prefix = new PrefixHandle(base.BufferReader);
			this.localName = new StringHandle(base.BufferReader);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00020FD4 File Offset: 0x0001F1D4
		public void SetInput(byte[] buffer, int offset, int count, Encoding encoding, XmlDictionaryReaderQuotas quotas, OnXmlDictionaryReaderClose onClose)
		{
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("buffer"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (offset > buffer.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					buffer.Length
				})));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > buffer.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					buffer.Length - offset
				})));
			}
			this.MoveToInitial(quotas, onClose);
			ArraySegment<byte> arraySegment = EncodingStreamWrapper.ProcessBuffer(buffer, offset, count, encoding);
			base.BufferReader.SetBuffer(arraySegment.Array, arraySegment.Offset, arraySegment.Count, null, null);
			this.buffered = true;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x000210D4 File Offset: 0x0001F2D4
		public void SetInput(Stream stream, Encoding encoding, XmlDictionaryReaderQuotas quotas, OnXmlDictionaryReaderClose onClose)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			this.MoveToInitial(quotas, onClose);
			stream = new EncodingStreamWrapper(stream, encoding);
			base.BufferReader.SetBuffer(stream, null, null);
			this.buffered = false;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0002110B File Offset: 0x0001F30B
		private void MoveToInitial(XmlDictionaryReaderQuotas quotas, OnXmlDictionaryReaderClose onClose)
		{
			base.MoveToInitial(quotas);
			this.maxBytesPerRead = quotas.MaxBytesPerRead;
			this.onClose = onClose;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00021128 File Offset: 0x0001F328
		public override void Close()
		{
			this.rowOffsets = null;
			base.Close();
			OnXmlDictionaryReaderClose onXmlDictionaryReaderClose = this.onClose;
			this.onClose = null;
			if (onXmlDictionaryReaderClose != null)
			{
				try
				{
					onXmlDictionaryReaderClose(this);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
				}
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0002117C File Offset: 0x0001F37C
		private void SkipWhitespace()
		{
			while (!base.BufferReader.EndOfFile && (XmlUTF8TextReader.charType[(int)base.BufferReader.GetByte()] & 4) != 0)
			{
				base.BufferReader.SkipByte();
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x000211B0 File Offset: 0x0001F3B0
		private void ReadDeclaration()
		{
			if (!this.buffered)
			{
				this.BufferElement();
			}
			int num;
			byte[] buffer = base.BufferReader.GetBuffer(5, out num);
			if (buffer[num] != 63 || buffer[num + 1] != 120 || buffer[num + 2] != 109 || buffer[num + 3] != 108 || (XmlUTF8TextReader.charType[(int)buffer[num + 4]] & 4) == 0)
			{
				XmlExceptionHelper.ThrowProcessingInstructionNotSupported(this);
			}
			if (base.Node.ReadState != ReadState.Initial)
			{
				XmlExceptionHelper.ThrowDeclarationNotFirst(this);
			}
			base.BufferReader.Advance(5);
			int offset = num + 1;
			int length = 3;
			int offset2 = base.BufferReader.Offset;
			this.SkipWhitespace();
			this.ReadAttributes();
			int i;
			for (i = base.BufferReader.Offset - offset2; i > 0; i--)
			{
				byte @byte = base.BufferReader.GetByte(offset2 + i - 1);
				if ((XmlUTF8TextReader.charType[(int)@byte] & 4) == 0)
				{
					break;
				}
			}
			buffer = base.BufferReader.GetBuffer(2, out num);
			if (buffer[num] != 63 || buffer[num + 1] != 62)
			{
				XmlExceptionHelper.ThrowTokenExpected(this, "?>", Encoding.UTF8.GetString(buffer, num, 2));
			}
			base.BufferReader.Advance(2);
			XmlBaseReader.XmlDeclarationNode xmlDeclarationNode = base.MoveToDeclaration();
			xmlDeclarationNode.LocalName.SetValue(offset, length);
			xmlDeclarationNode.Value.SetValue(ValueHandleType.UTF8, offset2, i);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x000212F4 File Offset: 0x0001F4F4
		private void VerifyNCName(string s)
		{
			try
			{
				XmlConvert.VerifyNCName(s);
			}
			catch (XmlException exception)
			{
				XmlExceptionHelper.ThrowXmlException(this, exception);
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00021324 File Offset: 0x0001F524
		private void ReadQualifiedName(PrefixHandle prefix, StringHandle localName)
		{
			int i;
			int num;
			byte[] buffer = base.BufferReader.GetBuffer(out i, out num);
			int num2 = 0;
			int num3 = 0;
			int num4 = i;
			int num5;
			if (i < num)
			{
				num5 = (int)buffer[i];
				num3 = num5;
				if ((XmlUTF8TextReader.charType[num5] & 1) == 0)
				{
					num2 |= 128;
				}
				num2 |= num5;
				for (i++; i < num; i++)
				{
					num5 = (int)buffer[i];
					if ((XmlUTF8TextReader.charType[num5] & 2) == 0)
					{
						break;
					}
					num2 |= num5;
				}
			}
			else
			{
				num2 |= 128;
				num5 = 0;
			}
			if (num5 == 58)
			{
				int num6 = i - num4;
				if (num6 == 1 && num3 >= 97 && num3 <= 122)
				{
					prefix.SetValue(PrefixHandle.GetAlphaPrefix(num3 - 97));
				}
				else
				{
					prefix.SetValue(num4, num6);
				}
				i++;
				int num7 = i;
				if (i < num)
				{
					num5 = (int)buffer[i];
					if ((XmlUTF8TextReader.charType[num5] & 1) == 0)
					{
						num2 |= 128;
					}
					num2 |= num5;
					for (i++; i < num; i++)
					{
						num5 = (int)buffer[i];
						if ((XmlUTF8TextReader.charType[num5] & 2) == 0)
						{
							break;
						}
						num2 |= num5;
					}
				}
				else
				{
					num2 |= 128;
				}
				localName.SetValue(num7, i - num7);
				if (num2 >= 128)
				{
					this.VerifyNCName(prefix.GetString());
					this.VerifyNCName(localName.GetString());
				}
			}
			else
			{
				prefix.SetValue(PrefixHandleType.Empty);
				localName.SetValue(num4, i - num4);
				if (num2 >= 128)
				{
					this.VerifyNCName(localName.GetString());
				}
			}
			base.BufferReader.Advance(i - num4);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000214A0 File Offset: 0x0001F6A0
		private int ReadAttributeText(byte[] buffer, int offset, int offsetMax)
		{
			byte[] array = XmlUTF8TextReader.charType;
			int num = offset;
			while (offset < offsetMax && (array[(int)buffer[offset]] & 16) != 0)
			{
				offset++;
			}
			return offset - num;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x000214D0 File Offset: 0x0001F6D0
		private void ReadAttributes()
		{
			int num = 0;
			if (this.buffered)
			{
				num = base.BufferReader.Offset;
			}
			for (;;)
			{
				this.ReadQualifiedName(this.prefix, this.localName);
				if (base.BufferReader.GetByte() != 61)
				{
					this.SkipWhitespace();
					if (base.BufferReader.GetByte() != 61)
					{
						XmlExceptionHelper.ThrowTokenExpected(this, "=", (char)base.BufferReader.GetByte());
					}
				}
				base.BufferReader.SkipByte();
				byte @byte = base.BufferReader.GetByte();
				if (@byte != 34 && @byte != 39)
				{
					this.SkipWhitespace();
					@byte = base.BufferReader.GetByte();
					if (@byte != 34 && @byte != 39)
					{
						XmlExceptionHelper.ThrowTokenExpected(this, "\"", (char)base.BufferReader.GetByte());
					}
				}
				base.BufferReader.SkipByte();
				bool flag = false;
				int offset = base.BufferReader.Offset;
				byte byte2;
				for (;;)
				{
					int offset2;
					int offsetMax;
					byte[] buffer = base.BufferReader.GetBuffer(out offset2, out offsetMax);
					int count = this.ReadAttributeText(buffer, offset2, offsetMax);
					base.BufferReader.Advance(count);
					byte2 = base.BufferReader.GetByte();
					if (byte2 == @byte)
					{
						break;
					}
					if (byte2 == 38)
					{
						this.ReadCharRef();
						flag = true;
					}
					else if (byte2 == 39 || byte2 == 34)
					{
						base.BufferReader.SkipByte();
					}
					else if (byte2 == 10 || byte2 == 13 || byte2 == 9)
					{
						base.BufferReader.SkipByte();
						flag = true;
					}
					else if (byte2 == 239)
					{
						this.ReadNonFFFE();
					}
					else
					{
						char c = (char)@byte;
						XmlExceptionHelper.ThrowTokenExpected(this, c.ToString(), (char)byte2);
					}
				}
				int length = base.BufferReader.Offset - offset;
				XmlBaseReader.XmlAttributeNode xmlAttributeNode;
				if (this.prefix.IsXmlns)
				{
					XmlBaseReader.Namespace @namespace = base.AddNamespace();
					this.localName.ToPrefixHandle(@namespace.Prefix);
					@namespace.Uri.SetValue(offset, length, flag);
					xmlAttributeNode = base.AddXmlnsAttribute(@namespace);
				}
				else if (this.prefix.IsEmpty && this.localName.IsXmlns)
				{
					XmlBaseReader.Namespace namespace2 = base.AddNamespace();
					namespace2.Prefix.SetValue(PrefixHandleType.Empty);
					namespace2.Uri.SetValue(offset, length, flag);
					xmlAttributeNode = base.AddXmlnsAttribute(namespace2);
				}
				else if (this.prefix.IsXml)
				{
					xmlAttributeNode = base.AddXmlAttribute();
					xmlAttributeNode.Prefix.SetValue(this.prefix);
					xmlAttributeNode.LocalName.SetValue(this.localName);
					xmlAttributeNode.Value.SetValue(flag ? ValueHandleType.EscapedUTF8 : ValueHandleType.UTF8, offset, length);
					base.FixXmlAttribute(xmlAttributeNode);
				}
				else
				{
					xmlAttributeNode = base.AddAttribute();
					xmlAttributeNode.Prefix.SetValue(this.prefix);
					xmlAttributeNode.LocalName.SetValue(this.localName);
					xmlAttributeNode.Value.SetValue(flag ? ValueHandleType.EscapedUTF8 : ValueHandleType.UTF8, offset, length);
				}
				xmlAttributeNode.QuoteChar = (char)@byte;
				base.BufferReader.SkipByte();
				byte2 = base.BufferReader.GetByte();
				bool flag2 = false;
				while ((XmlUTF8TextReader.charType[(int)byte2] & 4) != 0)
				{
					flag2 = true;
					base.BufferReader.SkipByte();
					byte2 = base.BufferReader.GetByte();
				}
				if (byte2 == 62 || byte2 == 47 || byte2 == 63)
				{
					break;
				}
				if (!flag2)
				{
					XmlExceptionHelper.ThrowXmlException(this, new XmlException(System.Runtime.Serialization.SR.GetString("Whitespace must appear between attributes.")));
				}
			}
			if (this.buffered && base.BufferReader.Offset - num > this.maxBytesPerRead)
			{
				XmlExceptionHelper.ThrowMaxBytesPerReadExceeded(this, this.maxBytesPerRead);
			}
			base.ProcessAttributes();
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00021848 File Offset: 0x0001FA48
		private void ReadNonFFFE()
		{
			int num;
			byte[] buffer = base.BufferReader.GetBuffer(3, out num);
			if (buffer[num + 1] == 191 && (buffer[num + 2] == 190 || buffer[num + 2] == 191))
			{
				XmlExceptionHelper.ThrowXmlException(this, new XmlException(System.Runtime.Serialization.SR.GetString("Characters with hexadecimal values 0xFFFE and 0xFFFF are not valid.")));
			}
			base.BufferReader.Advance(3);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x000218A9 File Offset: 0x0001FAA9
		private bool IsNextCharacterNonFFFE(byte[] buffer, int offset)
		{
			return buffer[offset + 1] != 191 || (buffer[offset + 2] != 190 && buffer[offset + 2] != 191);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x000218D4 File Offset: 0x0001FAD4
		private void BufferElement()
		{
			int offset = base.BufferReader.Offset;
			bool flag = false;
			byte b = 0;
			while (!flag)
			{
				int num;
				int num2;
				byte[] buffer = base.BufferReader.GetBuffer(128, out num, out num2);
				if (num + 128 != num2)
				{
					break;
				}
				int num3 = num;
				while (num3 < num2 && !flag)
				{
					byte b2 = buffer[num3];
					if (b == 0)
					{
						if (b2 == 39 || b2 == 34)
						{
							b = b2;
						}
						if (b2 == 62)
						{
							flag = true;
						}
					}
					else if (b2 == b)
					{
						b = 0;
					}
					num3++;
				}
				base.BufferReader.Advance(128);
			}
			base.BufferReader.Offset = offset;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00021974 File Offset: 0x0001FB74
		private new void ReadStartElement()
		{
			if (!this.buffered)
			{
				this.BufferElement();
			}
			XmlBaseReader.XmlElementNode xmlElementNode = base.EnterScope();
			xmlElementNode.NameOffset = base.BufferReader.Offset;
			this.ReadQualifiedName(xmlElementNode.Prefix, xmlElementNode.LocalName);
			xmlElementNode.NameLength = base.BufferReader.Offset - xmlElementNode.NameOffset;
			byte @byte = base.BufferReader.GetByte();
			while ((XmlUTF8TextReader.charType[(int)@byte] & 4) != 0)
			{
				base.BufferReader.SkipByte();
				@byte = base.BufferReader.GetByte();
			}
			if (@byte != 62 && @byte != 47)
			{
				this.ReadAttributes();
				@byte = base.BufferReader.GetByte();
			}
			xmlElementNode.Namespace = base.LookupNamespace(xmlElementNode.Prefix);
			bool flag = false;
			if (@byte == 47)
			{
				flag = true;
				base.BufferReader.SkipByte();
			}
			xmlElementNode.IsEmptyElement = flag;
			xmlElementNode.ExitScope = flag;
			if (base.BufferReader.GetByte() != 62)
			{
				XmlExceptionHelper.ThrowTokenExpected(this, ">", (char)base.BufferReader.GetByte());
			}
			base.BufferReader.SkipByte();
			xmlElementNode.BufferOffset = base.BufferReader.Offset;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00021A94 File Offset: 0x0001FC94
		private new void ReadEndElement()
		{
			base.BufferReader.SkipByte();
			XmlBaseReader.XmlElementNode elementNode = base.ElementNode;
			int nameOffset = elementNode.NameOffset;
			int nameLength = elementNode.NameLength;
			int num;
			byte[] buffer = base.BufferReader.GetBuffer(nameLength, out num);
			for (int i = 0; i < nameLength; i++)
			{
				if (buffer[num + i] != buffer[nameOffset + i])
				{
					this.ReadQualifiedName(this.prefix, this.localName);
					XmlExceptionHelper.ThrowTagMismatch(this, elementNode.Prefix.GetString(), elementNode.LocalName.GetString(), this.prefix.GetString(), this.localName.GetString());
				}
			}
			base.BufferReader.Advance(nameLength);
			if (base.BufferReader.GetByte() != 62)
			{
				this.SkipWhitespace();
				if (base.BufferReader.GetByte() != 62)
				{
					XmlExceptionHelper.ThrowTokenExpected(this, ">", (char)base.BufferReader.GetByte());
				}
			}
			base.BufferReader.SkipByte();
			base.MoveToEndElement();
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00021B8C File Offset: 0x0001FD8C
		private void ReadComment()
		{
			base.BufferReader.SkipByte();
			if (base.BufferReader.GetByte() != 45)
			{
				XmlExceptionHelper.ThrowTokenExpected(this, "--", (char)base.BufferReader.GetByte());
			}
			base.BufferReader.SkipByte();
			int offset = base.BufferReader.Offset;
			for (;;)
			{
				byte @byte = base.BufferReader.GetByte();
				if (@byte != 45)
				{
					if ((XmlUTF8TextReader.charType[(int)@byte] & 64) == 0)
					{
						if (@byte == 239)
						{
							this.ReadNonFFFE();
						}
						else
						{
							XmlExceptionHelper.ThrowInvalidXml(this, @byte);
						}
					}
					else
					{
						base.BufferReader.SkipByte();
					}
				}
				else
				{
					int num;
					byte[] buffer = base.BufferReader.GetBuffer(3, out num);
					if (buffer[num] == 45 && buffer[num + 1] == 45)
					{
						if (buffer[num + 2] == 62)
						{
							break;
						}
						XmlExceptionHelper.ThrowXmlException(this, new XmlException(System.Runtime.Serialization.SR.GetString("XML comments cannot contain '--' or end with '-'.")));
					}
					base.BufferReader.SkipByte();
				}
			}
			int length = base.BufferReader.Offset - offset;
			base.MoveToComment().Value.SetValue(ValueHandleType.UTF8, offset, length);
			base.BufferReader.Advance(3);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00021CA4 File Offset: 0x0001FEA4
		private void ReadCData()
		{
			int num;
			byte[] buffer = base.BufferReader.GetBuffer(7, out num);
			if (buffer[num] != 91 || buffer[num + 1] != 67 || buffer[num + 2] != 68 || buffer[num + 3] != 65 || buffer[num + 4] != 84 || buffer[num + 5] != 65 || buffer[num + 6] != 91)
			{
				XmlExceptionHelper.ThrowTokenExpected(this, "[CDATA[", Encoding.UTF8.GetString(buffer, num, 7));
			}
			base.BufferReader.Advance(7);
			int offset = base.BufferReader.Offset;
			for (;;)
			{
				byte @byte = base.BufferReader.GetByte();
				if (@byte != 93)
				{
					if (@byte == 239)
					{
						this.ReadNonFFFE();
					}
					else
					{
						base.BufferReader.SkipByte();
					}
				}
				else
				{
					buffer = base.BufferReader.GetBuffer(3, out num);
					if (buffer[num] == 93 && buffer[num + 1] == 93 && buffer[num + 2] == 62)
					{
						break;
					}
					base.BufferReader.SkipByte();
				}
			}
			int length = base.BufferReader.Offset - offset;
			base.MoveToCData().Value.SetValue(ValueHandleType.UTF8, offset, length);
			base.BufferReader.Advance(3);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00021DC4 File Offset: 0x0001FFC4
		private int ReadCharRef()
		{
			int offset = base.BufferReader.Offset;
			base.BufferReader.SkipByte();
			while (base.BufferReader.GetByte() != 59)
			{
				base.BufferReader.SkipByte();
			}
			base.BufferReader.SkipByte();
			int num = base.BufferReader.Offset - offset;
			base.BufferReader.Offset = offset;
			int charEntity = base.BufferReader.GetCharEntity(offset, num);
			base.BufferReader.Advance(num);
			return charEntity;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00021E44 File Offset: 0x00020044
		private void ReadWhitespace()
		{
			int offset;
			int num;
			if (this.buffered)
			{
				int offsetMax;
				byte[] buffer = base.BufferReader.GetBuffer(out offset, out offsetMax);
				num = this.ReadWhitespace(buffer, offset, offsetMax);
			}
			else
			{
				int offsetMax;
				byte[] buffer = base.BufferReader.GetBuffer(2048, out offset, out offsetMax);
				num = this.ReadWhitespace(buffer, offset, offsetMax);
				num = this.BreakText(buffer, offset, num);
			}
			base.BufferReader.Advance(num);
			base.MoveToWhitespaceText().Value.SetValue(ValueHandleType.UTF8, offset, num);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00021EC0 File Offset: 0x000200C0
		private int ReadWhitespace(byte[] buffer, int offset, int offsetMax)
		{
			byte[] array = XmlUTF8TextReader.charType;
			int num = offset;
			while (offset < offsetMax && (array[(int)buffer[offset]] & 32) != 0)
			{
				offset++;
			}
			return offset - num;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00021EF0 File Offset: 0x000200F0
		private int ReadText(byte[] buffer, int offset, int offsetMax)
		{
			byte[] array = XmlUTF8TextReader.charType;
			int num = offset;
			while (offset < offsetMax && (array[(int)buffer[offset]] & 8) != 0)
			{
				offset++;
			}
			return offset - num;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00021F1C File Offset: 0x0002011C
		private int ReadTextAndWatchForInvalidCharacters(byte[] buffer, int offset, int offsetMax)
		{
			byte[] array = XmlUTF8TextReader.charType;
			int num = offset;
			while (offset < offsetMax && ((array[(int)buffer[offset]] & 8) != 0 || buffer[offset] == 239))
			{
				if (buffer[offset] != 239)
				{
					offset++;
				}
				else if (offset + 2 < offsetMax)
				{
					if (this.IsNextCharacterNonFFFE(buffer, offset))
					{
						offset += 3;
					}
					else
					{
						XmlExceptionHelper.ThrowXmlException(this, new XmlException(System.Runtime.Serialization.SR.GetString("Characters with hexadecimal values 0xFFFE and 0xFFFF are not valid.")));
					}
				}
				else
				{
					if (base.BufferReader.Offset < offset)
					{
						break;
					}
					int num2;
					base.BufferReader.GetBuffer(3, out num2);
				}
			}
			return offset - num;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00021FAC File Offset: 0x000201AC
		private int BreakText(byte[] buffer, int offset, int length)
		{
			if (length > 0 && (buffer[offset + length - 1] & 128) == 128)
			{
				int num = length;
				do
				{
					length--;
				}
				while (length > 0 && (buffer[offset + length] & 192) != 192);
				if (length == 0)
				{
					return num;
				}
				byte b = (byte)(buffer[offset + length] << 2);
				int num2 = 2;
				while ((b & 128) == 128)
				{
					b = (byte)(b << 1);
					num2++;
					if (num2 > 4)
					{
						return num;
					}
				}
				if (length + num2 == num)
				{
					return num;
				}
				if (length == 0)
				{
					return num;
				}
			}
			return length;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0002202C File Offset: 0x0002022C
		private void ReadText(bool hasLeadingByteOf0xEF)
		{
			int num;
			int num2;
			byte[] buffer;
			int num3;
			if (this.buffered)
			{
				buffer = base.BufferReader.GetBuffer(out num, out num2);
				if (hasLeadingByteOf0xEF)
				{
					num3 = this.ReadTextAndWatchForInvalidCharacters(buffer, num, num2);
				}
				else
				{
					num3 = this.ReadText(buffer, num, num2);
				}
			}
			else
			{
				buffer = base.BufferReader.GetBuffer(2048, out num, out num2);
				if (hasLeadingByteOf0xEF)
				{
					num3 = this.ReadTextAndWatchForInvalidCharacters(buffer, num, num2);
				}
				else
				{
					num3 = this.ReadText(buffer, num, num2);
				}
				num3 = this.BreakText(buffer, num, num3);
			}
			base.BufferReader.Advance(num3);
			if (num < num2 - 1 - num3 && buffer[num + num3] == 60 && buffer[num + num3 + 1] != 33)
			{
				base.MoveToAtomicText().Value.SetValue(ValueHandleType.UTF8, num, num3);
				return;
			}
			base.MoveToComplexText().Value.SetValue(ValueHandleType.UTF8, num, num3);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000220F8 File Offset: 0x000202F8
		private void ReadEscapedText()
		{
			int num = this.ReadCharRef();
			if (num < 256 && (XmlUTF8TextReader.charType[num] & 4) != 0)
			{
				base.MoveToWhitespaceText().Value.SetCharValue(num);
				return;
			}
			base.MoveToComplexText().Value.SetCharValue(num);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00022144 File Offset: 0x00020344
		public override bool Read()
		{
			if (base.Node.ReadState == ReadState.Closed)
			{
				return false;
			}
			if (base.Node.CanMoveToElement)
			{
				this.MoveToElement();
			}
			base.SignNode();
			if (base.Node.ExitScope)
			{
				base.ExitScope();
			}
			if (!this.buffered)
			{
				base.BufferReader.SetWindow(base.ElementNode.BufferOffset, this.maxBytesPerRead);
			}
			if (base.BufferReader.EndOfFile)
			{
				base.MoveToEndOfFile();
				return false;
			}
			byte @byte = base.BufferReader.GetByte();
			if (@byte == 60)
			{
				base.BufferReader.SkipByte();
				@byte = base.BufferReader.GetByte();
				if (@byte == 47)
				{
					this.ReadEndElement();
				}
				else if (@byte == 33)
				{
					base.BufferReader.SkipByte();
					@byte = base.BufferReader.GetByte();
					if (@byte == 45)
					{
						this.ReadComment();
					}
					else
					{
						if (base.OutsideRootElement)
						{
							XmlExceptionHelper.ThrowXmlException(this, new XmlException(System.Runtime.Serialization.SR.GetString("CData elements not valid at top level of an XML document.")));
						}
						this.ReadCData();
					}
				}
				else if (@byte == 63)
				{
					this.ReadDeclaration();
				}
				else
				{
					this.ReadStartElement();
				}
			}
			else if ((XmlUTF8TextReader.charType[(int)@byte] & 32) != 0)
			{
				this.ReadWhitespace();
			}
			else if (base.OutsideRootElement && @byte != 13)
			{
				XmlExceptionHelper.ThrowInvalidRootData(this);
			}
			else if ((XmlUTF8TextReader.charType[(int)@byte] & 8) != 0)
			{
				this.ReadText(false);
			}
			else if (@byte == 38)
			{
				this.ReadEscapedText();
			}
			else if (@byte == 13)
			{
				base.BufferReader.SkipByte();
				if (!base.BufferReader.EndOfFile && base.BufferReader.GetByte() == 10)
				{
					this.ReadWhitespace();
				}
				else
				{
					base.MoveToComplexText().Value.SetCharValue(10);
				}
			}
			else if (@byte == 93)
			{
				int num;
				byte[] buffer = base.BufferReader.GetBuffer(3, out num);
				if (buffer[num] == 93 && buffer[num + 1] == 93 && buffer[num + 2] == 62)
				{
					XmlExceptionHelper.ThrowXmlException(this, new XmlException(System.Runtime.Serialization.SR.GetString("']]>' not valid in text node content.")));
				}
				base.BufferReader.SkipByte();
				base.MoveToComplexText().Value.SetCharValue(93);
			}
			else if (@byte == 239)
			{
				this.ReadText(true);
			}
			else
			{
				XmlExceptionHelper.ThrowInvalidXml(this, @byte);
			}
			return true;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0002238C File Offset: 0x0002058C
		protected override XmlSigningNodeWriter CreateSigningNodeWriter()
		{
			return new XmlSigningNodeWriter(true);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x000066E8 File Offset: 0x000048E8
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x00022394 File Offset: 0x00020594
		public int LineNumber
		{
			get
			{
				int result;
				int num;
				this.GetPosition(out result, out num);
				return result;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x000223AC File Offset: 0x000205AC
		public int LinePosition
		{
			get
			{
				int num;
				int result;
				this.GetPosition(out num, out result);
				return result;
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000223C4 File Offset: 0x000205C4
		private void GetPosition(out int row, out int column)
		{
			if (this.rowOffsets == null)
			{
				this.rowOffsets = base.BufferReader.GetRows();
			}
			int offset = base.BufferReader.Offset;
			int num = 0;
			while (num < this.rowOffsets.Length - 1 && this.rowOffsets[num + 1] < offset)
			{
				num++;
			}
			row = num + 1;
			column = offset - this.rowOffsets[num] + 1;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0002242B File Offset: 0x0002062B
		// Note: this type is marked as 'beforefieldinit'.
		static XmlUTF8TextReader()
		{
		}

		// Token: 0x0400039C RID: 924
		private const int MaxTextChunk = 2048;

		// Token: 0x0400039D RID: 925
		private PrefixHandle prefix;

		// Token: 0x0400039E RID: 926
		private StringHandle localName;

		// Token: 0x0400039F RID: 927
		private int[] rowOffsets;

		// Token: 0x040003A0 RID: 928
		private OnXmlDictionaryReaderClose onClose;

		// Token: 0x040003A1 RID: 929
		private bool buffered;

		// Token: 0x040003A2 RID: 930
		private int maxBytesPerRead;

		// Token: 0x040003A3 RID: 931
		private static byte[] charType = new byte[]
		{
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			108,
			108,
			0,
			0,
			68,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			124,
			88,
			72,
			88,
			88,
			88,
			64,
			72,
			88,
			88,
			88,
			88,
			88,
			90,
			90,
			88,
			90,
			90,
			90,
			90,
			90,
			90,
			90,
			90,
			90,
			90,
			88,
			88,
			64,
			88,
			88,
			88,
			88,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			88,
			88,
			80,
			88,
			91,
			88,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			88,
			88,
			88,
			88,
			88,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			3,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91,
			91
		};

		// Token: 0x0200009B RID: 155
		private static class CharType
		{
			// Token: 0x040003A4 RID: 932
			public const byte None = 0;

			// Token: 0x040003A5 RID: 933
			public const byte FirstName = 1;

			// Token: 0x040003A6 RID: 934
			public const byte Name = 2;

			// Token: 0x040003A7 RID: 935
			public const byte Whitespace = 4;

			// Token: 0x040003A8 RID: 936
			public const byte Text = 8;

			// Token: 0x040003A9 RID: 937
			public const byte AttributeText = 16;

			// Token: 0x040003AA RID: 938
			public const byte SpecialWhitespace = 32;

			// Token: 0x040003AB RID: 939
			public const byte Comment = 64;
		}
	}
}
