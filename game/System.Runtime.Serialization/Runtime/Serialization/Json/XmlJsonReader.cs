using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x0200018D RID: 397
	internal class XmlJsonReader : XmlBaseReader, IXmlJsonReaderInitializer
	{
		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x00003127 File Offset: 0x00001327
		public override bool CanCanonicalize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x0004C860 File Offset: 0x0004AA60
		public override string Value
		{
			get
			{
				if (this.IsAttributeValue && !this.IsLocalName("type"))
				{
					return this.UnescapeJsonString(base.Value);
				}
				return base.Value;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x0004C88A File Offset: 0x0004AA8A
		private bool IsAttributeValue
		{
			get
			{
				return base.Node.NodeType == XmlNodeType.Attribute || base.Node is XmlBaseReader.XmlAttributeTextNode;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x0004C8AA File Offset: 0x0004AAAA
		private bool IsReadingCollection
		{
			get
			{
				return this.scopeDepth > 0 && this.scopes[this.scopeDepth] == JsonNodeType.Collection;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x0004C8C7 File Offset: 0x0004AAC7
		private bool IsReadingComplexText
		{
			get
			{
				return !base.Node.IsAtomicValue && base.Node.NodeType == XmlNodeType.Text;
			}
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0004C8E8 File Offset: 0x0004AAE8
		public override void Close()
		{
			base.Close();
			OnXmlDictionaryReaderClose onXmlDictionaryReaderClose = this.onReaderClose;
			this.onReaderClose = null;
			this.ResetState();
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

		// Token: 0x060013DE RID: 5086 RVA: 0x00003141 File Offset: 0x00001341
		public override void EndCanonicalization()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0004C93C File Offset: 0x0004AB3C
		public override string GetAttribute(int index)
		{
			return this.UnescapeJsonString(base.GetAttribute(index));
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0004C94B File Offset: 0x0004AB4B
		public override string GetAttribute(string localName, string namespaceUri)
		{
			if (localName != "type")
			{
				return this.UnescapeJsonString(base.GetAttribute(localName, namespaceUri));
			}
			return base.GetAttribute(localName, namespaceUri);
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0004C971 File Offset: 0x0004AB71
		public override string GetAttribute(string name)
		{
			if (name != "type")
			{
				return this.UnescapeJsonString(base.GetAttribute(name));
			}
			return base.GetAttribute(name);
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0004C995 File Offset: 0x0004AB95
		public override string GetAttribute(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			if (XmlDictionaryString.GetString(localName) != "type")
			{
				return this.UnescapeJsonString(base.GetAttribute(localName, namespaceUri));
			}
			return base.GetAttribute(localName, namespaceUri);
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0004C9C0 File Offset: 0x0004ABC0
		public override bool Read()
		{
			if (base.Node.CanMoveToElement)
			{
				this.MoveToElement();
			}
			if (base.Node.ReadState == ReadState.Closed)
			{
				return false;
			}
			if (base.Node.ExitScope)
			{
				base.ExitScope();
			}
			if (!this.buffered)
			{
				base.BufferReader.SetWindow(base.ElementNode.BufferOffset, this.maxBytesPerRead);
			}
			byte @byte;
			if (!this.IsReadingComplexText)
			{
				this.SkipWhitespaceInBufferReader();
				if (this.TryGetByte(out @byte) && (this.charactersToSkipOnNextRead[0] == @byte || this.charactersToSkipOnNextRead[1] == @byte))
				{
					base.BufferReader.SkipByte();
					this.charactersToSkipOnNextRead[0] = 0;
					this.charactersToSkipOnNextRead[1] = 0;
				}
				this.SkipWhitespaceInBufferReader();
				if (this.TryGetByte(out @byte) && @byte == 93 && this.IsReadingCollection)
				{
					base.BufferReader.SkipByte();
					this.SkipWhitespaceInBufferReader();
					this.ExitJsonScope();
				}
				if (base.BufferReader.EndOfFile)
				{
					if (this.scopeDepth > 0)
					{
						this.MoveToEndElement();
						return true;
					}
					base.MoveToEndOfFile();
					return false;
				}
			}
			@byte = base.BufferReader.GetByte();
			if (this.scopeDepth == 0)
			{
				this.ReadNonExistentElementName(StringHandleConstStringType.Root);
			}
			else if (this.IsReadingComplexText)
			{
				switch (this.complexTextMode)
				{
				case XmlJsonReader.JsonComplexTextMode.QuotedText:
					if (@byte == 92)
					{
						this.ReadEscapedCharacter(true);
					}
					else
					{
						this.ReadQuotedText(true);
					}
					break;
				case XmlJsonReader.JsonComplexTextMode.NumericalText:
					this.ReadNumericalText();
					break;
				case XmlJsonReader.JsonComplexTextMode.None:
					XmlExceptionHelper.ThrowXmlException(this, new XmlException(SR.GetString("Encountered an unexpected character '{0}' in JSON.", new object[]
					{
						(char)@byte
					})));
					break;
				}
			}
			else if (this.IsReadingCollection)
			{
				this.ReadNonExistentElementName(StringHandleConstStringType.Item);
			}
			else if (@byte == 93)
			{
				base.BufferReader.SkipByte();
				this.MoveToEndElement();
				this.ExitJsonScope();
			}
			else if (@byte == 123)
			{
				base.BufferReader.SkipByte();
				this.SkipWhitespaceInBufferReader();
				@byte = base.BufferReader.GetByte();
				if (@byte == 125)
				{
					base.BufferReader.SkipByte();
					this.SkipWhitespaceInBufferReader();
					if (this.TryGetByte(out @byte))
					{
						if (@byte == 44)
						{
							base.BufferReader.SkipByte();
						}
					}
					else
					{
						this.charactersToSkipOnNextRead[0] = 44;
					}
					this.MoveToEndElement();
				}
				else
				{
					this.EnterJsonScope(JsonNodeType.Object);
					this.ParseStartElement();
				}
			}
			else if (@byte == 125)
			{
				base.BufferReader.SkipByte();
				if (this.expectingFirstElementInNonPrimitiveChild)
				{
					this.SkipWhitespaceInBufferReader();
					@byte = base.BufferReader.GetByte();
					if (@byte == 44 || @byte == 125)
					{
						base.BufferReader.SkipByte();
					}
					else
					{
						XmlExceptionHelper.ThrowXmlException(this, new XmlException(SR.GetString("Encountered an unexpected character '{0}' in JSON.", new object[]
						{
							(char)@byte
						})));
					}
					this.expectingFirstElementInNonPrimitiveChild = false;
				}
				this.MoveToEndElement();
			}
			else if (@byte == 44)
			{
				base.BufferReader.SkipByte();
				this.MoveToEndElement();
			}
			else if (@byte == 34)
			{
				if (this.readServerTypeElement)
				{
					this.readServerTypeElement = false;
					this.EnterJsonScope(JsonNodeType.Object);
					this.ParseStartElement();
				}
				else if (base.Node.NodeType == XmlNodeType.Element)
				{
					if (this.expectingFirstElementInNonPrimitiveChild)
					{
						this.EnterJsonScope(JsonNodeType.Object);
						this.ParseStartElement();
					}
					else
					{
						base.BufferReader.SkipByte();
						this.ReadQuotedText(true);
					}
				}
				else if (base.Node.NodeType == XmlNodeType.EndElement)
				{
					this.EnterJsonScope(JsonNodeType.Element);
					this.ParseStartElement();
				}
				else
				{
					XmlExceptionHelper.ThrowXmlException(this, new XmlException(SR.GetString("Encountered an unexpected character '{0}' in JSON.", new object[]
					{
						'"'
					})));
				}
			}
			else if (@byte == 102)
			{
				int num;
				byte[] buffer = base.BufferReader.GetBuffer(5, out num);
				if (buffer[num + 1] != 97 || buffer[num + 2] != 108 || buffer[num + 3] != 115 || buffer[num + 4] != 101)
				{
					XmlExceptionHelper.ThrowTokenExpected(this, "false", Encoding.UTF8.GetString(buffer, num, 5));
				}
				base.BufferReader.Advance(5);
				if (this.TryGetByte(out @byte) && !XmlJsonReader.IsWhitespace(@byte) && @byte != 44 && @byte != 125 && @byte != 93)
				{
					string expected = "false";
					string @string = Encoding.UTF8.GetString(buffer, num, 4);
					char c = (char)@byte;
					XmlExceptionHelper.ThrowTokenExpected(this, expected, @string + c.ToString());
				}
				base.MoveToAtomicText().Value.SetValue(ValueHandleType.UTF8, num, 5);
			}
			else if (@byte == 116)
			{
				int num2;
				byte[] buffer2 = base.BufferReader.GetBuffer(4, out num2);
				if (buffer2[num2 + 1] != 114 || buffer2[num2 + 2] != 117 || buffer2[num2 + 3] != 101)
				{
					XmlExceptionHelper.ThrowTokenExpected(this, "true", Encoding.UTF8.GetString(buffer2, num2, 4));
				}
				base.BufferReader.Advance(4);
				if (this.TryGetByte(out @byte) && !XmlJsonReader.IsWhitespace(@byte) && @byte != 44 && @byte != 125 && @byte != 93)
				{
					string expected2 = "true";
					string string2 = Encoding.UTF8.GetString(buffer2, num2, 4);
					char c = (char)@byte;
					XmlExceptionHelper.ThrowTokenExpected(this, expected2, string2 + c.ToString());
				}
				base.MoveToAtomicText().Value.SetValue(ValueHandleType.UTF8, num2, 4);
			}
			else if (@byte == 110)
			{
				int num3;
				byte[] buffer3 = base.BufferReader.GetBuffer(4, out num3);
				if (buffer3[num3 + 1] != 117 || buffer3[num3 + 2] != 108 || buffer3[num3 + 3] != 108)
				{
					XmlExceptionHelper.ThrowTokenExpected(this, "null", Encoding.UTF8.GetString(buffer3, num3, 4));
				}
				base.BufferReader.Advance(4);
				this.SkipWhitespaceInBufferReader();
				if (this.TryGetByte(out @byte))
				{
					if (@byte == 44 || @byte == 125)
					{
						base.BufferReader.SkipByte();
					}
					else if (@byte != 93)
					{
						string expected3 = "null";
						string string3 = Encoding.UTF8.GetString(buffer3, num3, 4);
						char c = (char)@byte;
						XmlExceptionHelper.ThrowTokenExpected(this, expected3, string3 + c.ToString());
					}
				}
				else
				{
					this.charactersToSkipOnNextRead[0] = 44;
					this.charactersToSkipOnNextRead[1] = 125;
				}
				this.MoveToEndElement();
			}
			else if (@byte == 45 || (48 <= @byte && @byte <= 57) || @byte == 73 || @byte == 78)
			{
				this.ReadNumericalText();
			}
			else
			{
				XmlExceptionHelper.ThrowXmlException(this, new XmlException(SR.GetString("Encountered an unexpected character '{0}' in JSON.", new object[]
				{
					(char)@byte
				})));
			}
			return true;
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0004CFFC File Offset: 0x0004B1FC
		public override decimal ReadContentAsDecimal()
		{
			string text = this.ReadContentAsString();
			decimal result;
			try
			{
				result = decimal.Parse(text, NumberStyles.Float, NumberFormatInfo.InvariantInfo);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "decimal", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "decimal", exception2));
			}
			catch (OverflowException exception3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "decimal", exception3));
			}
			return result;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0004D088 File Offset: 0x0004B288
		public override int ReadContentAsInt()
		{
			return XmlJsonReader.ParseInt(this.ReadContentAsString(), NumberStyles.Float);
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0004D09C File Offset: 0x0004B29C
		public override long ReadContentAsLong()
		{
			string text = this.ReadContentAsString();
			long result;
			try
			{
				result = long.Parse(text, NumberStyles.Float, NumberFormatInfo.InvariantInfo);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "Int64", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "Int64", exception2));
			}
			catch (OverflowException exception3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "Int64", exception3));
			}
			return result;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0004D128 File Offset: 0x0004B328
		public override int ReadValueAsBase64(byte[] buffer, int offset, int count)
		{
			if (!this.IsAttributeValue)
			{
				return base.ReadValueAsBase64(buffer, offset, count);
			}
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("buffer"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", SR.GetString("The value of this argument must be non-negative.")));
			}
			if (offset > buffer.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					buffer.Length
				})));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > buffer.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					buffer.Length - offset
				})));
			}
			return 0;
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0004D200 File Offset: 0x0004B400
		public override int ReadValueChunk(char[] chars, int offset, int count)
		{
			if (!this.IsAttributeValue)
			{
				return base.ReadValueChunk(chars, offset, count);
			}
			if (chars == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("chars"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", SR.GetString("The value of this argument must be non-negative.")));
			}
			if (offset > chars.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					chars.Length
				})));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > chars.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					chars.Length - offset
				})));
			}
			string text = this.UnescapeJsonString(base.Node.ValueAsString);
			int num = Math.Min(count, text.Length);
			if (num > 0)
			{
				text.CopyTo(0, chars, offset, num);
				if (base.Node.QNameType == XmlBaseReader.QNameType.Xmlns)
				{
					base.Node.Namespace.Uri.SetValue(0, 0);
				}
				else
				{
					base.Node.Value.SetValue(ValueHandleType.UTF8, 0, 0);
				}
			}
			return num;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0004D340 File Offset: 0x0004B540
		public void SetInput(byte[] buffer, int offset, int count, Encoding encoding, XmlDictionaryReaderQuotas quotas, OnXmlDictionaryReaderClose onClose)
		{
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", SR.GetString("The value of this argument must be non-negative.")));
			}
			if (offset > buffer.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", SR.GetString("On JSON writer, offset exceeded buffer size {0}.", new object[]
				{
					buffer.Length
				})));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > buffer.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", SR.GetString("JSON size exceeded remaining buffer space, by {0} byte(s).", new object[]
				{
					buffer.Length - offset
				})));
			}
			this.MoveToInitial(quotas, onClose);
			ArraySegment<byte> arraySegment = JsonEncodingStreamWrapper.ProcessBuffer(buffer, offset, count, encoding);
			base.BufferReader.SetBuffer(arraySegment.Array, arraySegment.Offset, arraySegment.Count, null, null);
			this.buffered = true;
			this.ResetState();
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0004D441 File Offset: 0x0004B641
		public void SetInput(Stream stream, Encoding encoding, XmlDictionaryReaderQuotas quotas, OnXmlDictionaryReaderClose onClose)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			this.MoveToInitial(quotas, onClose);
			stream = new JsonEncodingStreamWrapper(stream, encoding, true);
			base.BufferReader.SetBuffer(stream, null, null);
			this.buffered = false;
			this.ResetState();
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x00003141 File Offset: 0x00001341
		public override void StartCanonicalization(Stream stream, bool includeComments, string[] inclusivePrefixes)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0004D480 File Offset: 0x0004B680
		internal static void CheckArray(Array array, int offset, int count)
		{
			if (array == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("array"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", SR.GetString("The value of this argument must be non-negative.")));
			}
			if (offset > array.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					array.Length
				})));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > array.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					array.Length - offset
				})));
			}
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0004D54E File Offset: 0x0004B74E
		protected override XmlSigningNodeWriter CreateSigningNodeWriter()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("Method {0} is not supported in JSON.", new object[]
			{
				"CreateSigningNodeWriter"
			})));
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0004D574 File Offset: 0x0004B774
		private static int BreakText(byte[] buffer, int offset, int length)
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

		// Token: 0x060013EF RID: 5103 RVA: 0x0004D5F4 File Offset: 0x0004B7F4
		private static int ComputeNumericalTextLength(byte[] buffer, int offset, int offsetMax)
		{
			int num = offset;
			while (offset < offsetMax)
			{
				byte b = buffer[offset];
				if (b == 44 || b == 125 || b == 93 || XmlJsonReader.IsWhitespace(b))
				{
					break;
				}
				offset++;
			}
			return offset - num;
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0004D62C File Offset: 0x0004B82C
		private static int ComputeQuotedTextLengthUntilEndQuote(byte[] buffer, int offset, int offsetMax, out bool escaped)
		{
			int num = offset;
			escaped = false;
			while (offset < offsetMax)
			{
				byte b = buffer[offset];
				if (b < 32)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("Encountered an invalid character '{0}'.", new object[]
					{
						(char)b
					})));
				}
				if (b == 92 || b == 239)
				{
					escaped = true;
					break;
				}
				if (b == 34)
				{
					break;
				}
				offset++;
			}
			return offset - num;
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0004D690 File Offset: 0x0004B890
		private static bool IsWhitespace(byte ch)
		{
			return ch == 32 || ch == 9 || ch == 10 || ch == 13;
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0004D6A8 File Offset: 0x0004B8A8
		private static char ParseChar(string value, NumberStyles style)
		{
			int value2 = XmlJsonReader.ParseInt(value, style);
			char result;
			try
			{
				result = Convert.ToChar(value2);
			}
			catch (OverflowException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "char", exception));
			}
			return result;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0004D6EC File Offset: 0x0004B8EC
		private static int ParseInt(string value, NumberStyles style)
		{
			int result;
			try
			{
				result = int.Parse(value, style, NumberFormatInfo.InvariantInfo);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "Int32", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "Int32", exception2));
			}
			catch (OverflowException exception3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "Int32", exception3));
			}
			return result;
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0004D76C File Offset: 0x0004B96C
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
					if (b2 == 92)
					{
						num3++;
						if (num3 >= num2)
						{
							break;
						}
					}
					else if (b == 0)
					{
						if (b2 == 39 || b2 == 34)
						{
							b = b2;
						}
						if (b2 == 58)
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

		// Token: 0x060013F5 RID: 5109 RVA: 0x0004D824 File Offset: 0x0004BA24
		private void EnterJsonScope(JsonNodeType currentNodeType)
		{
			this.scopeDepth++;
			if (this.scopes == null)
			{
				this.scopes = new JsonNodeType[4];
			}
			else if (this.scopes.Length == this.scopeDepth)
			{
				JsonNodeType[] destinationArray = new JsonNodeType[this.scopeDepth * 2];
				Array.Copy(this.scopes, destinationArray, this.scopeDepth);
				this.scopes = destinationArray;
			}
			this.scopes[this.scopeDepth] = currentNodeType;
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0004D89A File Offset: 0x0004BA9A
		private JsonNodeType ExitJsonScope()
		{
			JsonNodeType result = this.scopes[this.scopeDepth];
			this.scopes[this.scopeDepth] = JsonNodeType.None;
			this.scopeDepth--;
			return result;
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0004D8C5 File Offset: 0x0004BAC5
		private new void MoveToEndElement()
		{
			this.ExitJsonScope();
			base.MoveToEndElement();
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0004D8D4 File Offset: 0x0004BAD4
		private void MoveToInitial(XmlDictionaryReaderQuotas quotas, OnXmlDictionaryReaderClose onClose)
		{
			base.MoveToInitial(quotas);
			this.maxBytesPerRead = quotas.MaxBytesPerRead;
			this.onReaderClose = onClose;
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x0004D8F0 File Offset: 0x0004BAF0
		private void ParseAndSetLocalName()
		{
			XmlBaseReader.XmlElementNode xmlElementNode = base.EnterScope();
			xmlElementNode.NameOffset = base.BufferReader.Offset;
			do
			{
				if (base.BufferReader.GetByte() == 92)
				{
					this.ReadEscapedCharacter(false);
				}
				else
				{
					this.ReadQuotedText(false);
				}
			}
			while (this.complexTextMode == XmlJsonReader.JsonComplexTextMode.QuotedText);
			int num = base.BufferReader.Offset - 1;
			xmlElementNode.LocalName.SetValue(xmlElementNode.NameOffset, num - xmlElementNode.NameOffset);
			xmlElementNode.NameLength = num - xmlElementNode.NameOffset;
			xmlElementNode.Namespace.Uri.SetValue(xmlElementNode.NameOffset, 0);
			xmlElementNode.Prefix.SetValue(PrefixHandleType.Empty);
			xmlElementNode.IsEmptyElement = false;
			xmlElementNode.ExitScope = false;
			xmlElementNode.BufferOffset = num;
			int @byte = (int)base.BufferReader.GetByte(xmlElementNode.NameOffset);
			if ((XmlJsonReader.charType[@byte] & 1) == 0)
			{
				this.SetJsonNameWithMapping(xmlElementNode);
				return;
			}
			int i = 0;
			int num2 = xmlElementNode.NameOffset;
			while (i < xmlElementNode.NameLength)
			{
				@byte = (int)base.BufferReader.GetByte(num2);
				if ((XmlJsonReader.charType[@byte] & 2) == 0 || @byte >= 128)
				{
					this.SetJsonNameWithMapping(xmlElementNode);
					return;
				}
				i++;
				num2++;
			}
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0004DA18 File Offset: 0x0004BC18
		private void ParseStartElement()
		{
			if (!this.buffered)
			{
				this.BufferElement();
			}
			this.expectingFirstElementInNonPrimitiveChild = false;
			byte @byte = base.BufferReader.GetByte();
			if (@byte == 34)
			{
				base.BufferReader.SkipByte();
				this.ParseAndSetLocalName();
				this.SkipWhitespaceInBufferReader();
				this.SkipExpectedByteInBufferReader(58);
				this.SkipWhitespaceInBufferReader();
				if (base.BufferReader.GetByte() == 123)
				{
					base.BufferReader.SkipByte();
					this.expectingFirstElementInNonPrimitiveChild = true;
				}
				this.ReadAttributes();
				return;
			}
			XmlExceptionHelper.ThrowTokenExpected(this, "\"", (char)@byte);
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0004DAA4 File Offset: 0x0004BCA4
		private void ReadAttributes()
		{
			XmlBaseReader.XmlAttributeNode xmlAttributeNode = base.AddAttribute();
			xmlAttributeNode.LocalName.SetConstantValue(StringHandleConstStringType.Type);
			xmlAttributeNode.Namespace.Uri.SetValue(0, 0);
			xmlAttributeNode.Prefix.SetValue(PrefixHandleType.Empty);
			this.SkipWhitespaceInBufferReader();
			byte @byte = base.BufferReader.GetByte();
			if (@byte <= 102)
			{
				if (@byte != 34)
				{
					if (@byte == 91)
					{
						xmlAttributeNode.Value.SetConstantValue(ValueHandleConstStringType.Array);
						base.BufferReader.SkipByte();
						this.EnterJsonScope(JsonNodeType.Collection);
						return;
					}
					if (@byte != 102)
					{
						goto IL_132;
					}
				}
				else
				{
					if (!this.expectingFirstElementInNonPrimitiveChild)
					{
						xmlAttributeNode.Value.SetConstantValue(ValueHandleConstStringType.String);
						return;
					}
					xmlAttributeNode.Value.SetConstantValue(ValueHandleConstStringType.Object);
					this.ReadServerTypeAttribute(true);
					return;
				}
			}
			else if (@byte <= 116)
			{
				if (@byte == 110)
				{
					xmlAttributeNode.Value.SetConstantValue(ValueHandleConstStringType.Null);
					return;
				}
				if (@byte != 116)
				{
					goto IL_132;
				}
			}
			else
			{
				if (@byte == 123)
				{
					xmlAttributeNode.Value.SetConstantValue(ValueHandleConstStringType.Object);
					this.ReadServerTypeAttribute(false);
					return;
				}
				if (@byte != 125)
				{
					goto IL_132;
				}
				if (this.expectingFirstElementInNonPrimitiveChild)
				{
					xmlAttributeNode.Value.SetConstantValue(ValueHandleConstStringType.Object);
					return;
				}
				XmlExceptionHelper.ThrowXmlException(this, new XmlException(SR.GetString("Encountered an unexpected character '{0}' in JSON.", new object[]
				{
					(char)@byte
				})));
				return;
			}
			xmlAttributeNode.Value.SetConstantValue(ValueHandleConstStringType.Boolean);
			return;
			IL_132:
			if (@byte == 45 || (@byte <= 57 && @byte >= 48) || @byte == 78 || @byte == 73)
			{
				xmlAttributeNode.Value.SetConstantValue(ValueHandleConstStringType.Number);
				return;
			}
			XmlExceptionHelper.ThrowXmlException(this, new XmlException(SR.GetString("Encountered an unexpected character '{0}' in JSON.", new object[]
			{
				(char)@byte
			})));
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0004DC30 File Offset: 0x0004BE30
		private void ReadEscapedCharacter(bool moveToText)
		{
			base.BufferReader.SkipByte();
			char c = (char)base.BufferReader.GetByte();
			if (c == 'u')
			{
				base.BufferReader.SkipByte();
				int num;
				byte[] buffer = base.BufferReader.GetBuffer(5, out num);
				string @string = Encoding.UTF8.GetString(buffer, num, 4);
				base.BufferReader.Advance(4);
				int num2 = (int)XmlJsonReader.ParseChar(@string, NumberStyles.HexNumber);
				if (char.IsHighSurrogate((char)num2) && base.BufferReader.GetByte() == 92)
				{
					base.BufferReader.SkipByte();
					this.SkipExpectedByteInBufferReader(117);
					buffer = base.BufferReader.GetBuffer(5, out num);
					@string = Encoding.UTF8.GetString(buffer, num, 4);
					base.BufferReader.Advance(4);
					char c2 = XmlJsonReader.ParseChar(@string, NumberStyles.HexNumber);
					if (!char.IsLowSurrogate(c2))
					{
						XmlExceptionHelper.ThrowXmlException(this, new XmlException(SR.GetString("Low surrogate char '0x{0}' not valid. Low surrogate chars range from 0xDC00 to 0xDFFF.", new object[]
						{
							@string
						})));
					}
					num2 = new SurrogateChar(c2, (char)num2).Char;
				}
				if (buffer[num + 4] == 34)
				{
					base.BufferReader.SkipByte();
					if (moveToText)
					{
						base.MoveToAtomicText().Value.SetCharValue(num2);
					}
					this.complexTextMode = XmlJsonReader.JsonComplexTextMode.None;
					return;
				}
				if (moveToText)
				{
					base.MoveToComplexText().Value.SetCharValue(num2);
				}
				this.complexTextMode = XmlJsonReader.JsonComplexTextMode.QuotedText;
				return;
			}
			else
			{
				if (c <= 'b')
				{
					if (c <= '/')
					{
						if (c == '"' || c == '/')
						{
							goto IL_1CE;
						}
					}
					else
					{
						if (c == '\\')
						{
							goto IL_1CE;
						}
						if (c == 'b')
						{
							c = '\b';
							goto IL_1CE;
						}
					}
				}
				else if (c <= 'n')
				{
					if (c == 'f')
					{
						c = '\f';
						goto IL_1CE;
					}
					if (c == 'n')
					{
						c = '\n';
						goto IL_1CE;
					}
				}
				else
				{
					if (c == 'r')
					{
						c = '\r';
						goto IL_1CE;
					}
					if (c == 't')
					{
						c = '\t';
						goto IL_1CE;
					}
				}
				XmlExceptionHelper.ThrowXmlException(this, new XmlException(SR.GetString("Encountered an unexpected character '{0}' in JSON.", new object[]
				{
					c
				})));
				IL_1CE:
				base.BufferReader.SkipByte();
				if (base.BufferReader.GetByte() == 34)
				{
					base.BufferReader.SkipByte();
					if (moveToText)
					{
						base.MoveToAtomicText().Value.SetCharValue((int)c);
					}
					this.complexTextMode = XmlJsonReader.JsonComplexTextMode.None;
					return;
				}
				if (moveToText)
				{
					base.MoveToComplexText().Value.SetCharValue((int)c);
				}
				this.complexTextMode = XmlJsonReader.JsonComplexTextMode.QuotedText;
				return;
			}
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0004DE68 File Offset: 0x0004C068
		private void ReadNonExistentElementName(StringHandleConstStringType elementName)
		{
			this.EnterJsonScope(JsonNodeType.Object);
			XmlBaseReader.XmlElementNode xmlElementNode = base.EnterScope();
			xmlElementNode.LocalName.SetConstantValue(elementName);
			xmlElementNode.Namespace.Uri.SetValue(xmlElementNode.NameOffset, 0);
			xmlElementNode.Prefix.SetValue(PrefixHandleType.Empty);
			xmlElementNode.BufferOffset = base.BufferReader.Offset;
			xmlElementNode.IsEmptyElement = false;
			xmlElementNode.ExitScope = false;
			this.ReadAttributes();
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0004DED8 File Offset: 0x0004C0D8
		private int ReadNonFFFE()
		{
			int num;
			byte[] buffer = base.BufferReader.GetBuffer(3, out num);
			if (buffer[num + 1] == 191 && (buffer[num + 2] == 190 || buffer[num + 2] == 191))
			{
				XmlExceptionHelper.ThrowXmlException(this, new XmlException(SR.GetString("FFFE in JSON is invalid.")));
			}
			return 3;
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0004DF30 File Offset: 0x0004C130
		private void ReadNumericalText()
		{
			int num2;
			int num3;
			int num;
			if (this.buffered)
			{
				num = XmlJsonReader.ComputeNumericalTextLength(base.BufferReader.GetBuffer(out num2, out num3), num2, num3);
			}
			else
			{
				byte[] buffer = base.BufferReader.GetBuffer(2048, out num2, out num3);
				num = XmlJsonReader.ComputeNumericalTextLength(buffer, num2, num3);
				num = XmlJsonReader.BreakText(buffer, num2, num);
			}
			base.BufferReader.Advance(num);
			if (num2 <= num3 - num)
			{
				base.MoveToAtomicText().Value.SetValue(ValueHandleType.UTF8, num2, num);
				this.complexTextMode = XmlJsonReader.JsonComplexTextMode.None;
				return;
			}
			base.MoveToComplexText().Value.SetValue(ValueHandleType.UTF8, num2, num);
			this.complexTextMode = XmlJsonReader.JsonComplexTextMode.NumericalText;
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0004DFCC File Offset: 0x0004C1CC
		private void ReadQuotedText(bool moveToText)
		{
			int offset;
			bool flag;
			int num;
			bool flag2;
			if (this.buffered)
			{
				int num2;
				num = XmlJsonReader.ComputeQuotedTextLengthUntilEndQuote(base.BufferReader.GetBuffer(out offset, out num2), offset, num2, out flag);
				flag2 = (offset < num2 - num);
			}
			else
			{
				int num2;
				byte[] buffer = base.BufferReader.GetBuffer(2048, out offset, out num2);
				num = XmlJsonReader.ComputeQuotedTextLengthUntilEndQuote(buffer, offset, num2, out flag);
				flag2 = (offset < num2 - num);
				num = XmlJsonReader.BreakText(buffer, offset, num);
			}
			if (flag && base.BufferReader.GetByte() == 239)
			{
				offset = base.BufferReader.Offset;
				num = this.ReadNonFFFE();
			}
			base.BufferReader.Advance(num);
			if (!flag && flag2)
			{
				if (moveToText)
				{
					base.MoveToAtomicText().Value.SetValue(ValueHandleType.UTF8, offset, num);
				}
				this.SkipExpectedByteInBufferReader(34);
				this.complexTextMode = XmlJsonReader.JsonComplexTextMode.None;
				return;
			}
			if (num == 0 && flag)
			{
				this.ReadEscapedCharacter(moveToText);
				return;
			}
			if (moveToText)
			{
				base.MoveToComplexText().Value.SetValue(ValueHandleType.UTF8, offset, num);
			}
			this.complexTextMode = XmlJsonReader.JsonComplexTextMode.QuotedText;
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0004E0C8 File Offset: 0x0004C2C8
		private void ReadServerTypeAttribute(bool consumedObjectChar)
		{
			if (!consumedObjectChar)
			{
				this.SkipExpectedByteInBufferReader(123);
				this.SkipWhitespaceInBufferReader();
				byte @byte = base.BufferReader.GetByte();
				if (@byte != 34 && @byte != 125)
				{
					XmlExceptionHelper.ThrowTokenExpected(this, "\"", (char)@byte);
				}
			}
			else
			{
				this.SkipWhitespaceInBufferReader();
			}
			int num;
			int num2;
			byte[] buffer = base.BufferReader.GetBuffer(8, out num, out num2);
			if (num + 8 <= num2 && buffer[num] == 34 && buffer[num + 1] == 95 && buffer[num + 2] == 95 && buffer[num + 3] == 116 && buffer[num + 4] == 121 && buffer[num + 5] == 112 && buffer[num + 6] == 101 && buffer[num + 7] == 34)
			{
				XmlBaseReader.XmlAttributeNode xmlAttributeNode = base.AddAttribute();
				xmlAttributeNode.LocalName.SetValue(num + 1, 6);
				xmlAttributeNode.Namespace.Uri.SetValue(0, 0);
				xmlAttributeNode.Prefix.SetValue(PrefixHandleType.Empty);
				base.BufferReader.Advance(8);
				if (!this.buffered)
				{
					this.BufferElement();
				}
				this.SkipWhitespaceInBufferReader();
				this.SkipExpectedByteInBufferReader(58);
				this.SkipWhitespaceInBufferReader();
				this.SkipExpectedByteInBufferReader(34);
				buffer = base.BufferReader.GetBuffer(out num, out num2);
				do
				{
					if (base.BufferReader.GetByte() == 92)
					{
						this.ReadEscapedCharacter(false);
					}
					else
					{
						this.ReadQuotedText(false);
					}
				}
				while (this.complexTextMode == XmlJsonReader.JsonComplexTextMode.QuotedText);
				xmlAttributeNode.Value.SetValue(ValueHandleType.UTF8, num, base.BufferReader.Offset - 1 - num);
				this.SkipWhitespaceInBufferReader();
				if (base.BufferReader.GetByte() == 44)
				{
					base.BufferReader.SkipByte();
					this.readServerTypeElement = true;
				}
			}
			if (base.BufferReader.GetByte() == 125)
			{
				base.BufferReader.SkipByte();
				this.readServerTypeElement = false;
				this.expectingFirstElementInNonPrimitiveChild = false;
				return;
			}
			this.readServerTypeElement = true;
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0004E2A2 File Offset: 0x0004C4A2
		private void ResetState()
		{
			this.complexTextMode = XmlJsonReader.JsonComplexTextMode.None;
			this.expectingFirstElementInNonPrimitiveChild = false;
			this.charactersToSkipOnNextRead = new byte[2];
			this.scopeDepth = 0;
			if (this.scopes != null && this.scopes.Length > 25)
			{
				this.scopes = null;
			}
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0004E2E0 File Offset: 0x0004C4E0
		private void SetJsonNameWithMapping(XmlBaseReader.XmlElementNode elementNode)
		{
			XmlBaseReader.Namespace @namespace = base.AddNamespace();
			@namespace.Prefix.SetValue(PrefixHandleType.A);
			@namespace.Uri.SetConstantValue(StringHandleConstStringType.Item);
			base.AddXmlnsAttribute(@namespace);
			XmlBaseReader.XmlAttributeNode xmlAttributeNode = base.AddAttribute();
			xmlAttributeNode.LocalName.SetConstantValue(StringHandleConstStringType.Item);
			xmlAttributeNode.Namespace.Uri.SetValue(0, 0);
			xmlAttributeNode.Prefix.SetValue(PrefixHandleType.Empty);
			xmlAttributeNode.Value.SetValue(ValueHandleType.UTF8, elementNode.NameOffset, elementNode.NameLength);
			elementNode.NameLength = 0;
			elementNode.Prefix.SetValue(PrefixHandleType.A);
			elementNode.LocalName.SetConstantValue(StringHandleConstStringType.Item);
			elementNode.Namespace = @namespace;
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0004E384 File Offset: 0x0004C584
		private void SkipExpectedByteInBufferReader(byte characterToSkip)
		{
			if (base.BufferReader.GetByte() != characterToSkip)
			{
				char c = (char)characterToSkip;
				XmlExceptionHelper.ThrowTokenExpected(this, c.ToString(), (char)base.BufferReader.GetByte());
			}
			base.BufferReader.SkipByte();
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0004E3C4 File Offset: 0x0004C5C4
		private void SkipWhitespaceInBufferReader()
		{
			byte ch;
			while (this.TryGetByte(out ch) && XmlJsonReader.IsWhitespace(ch))
			{
				base.BufferReader.SkipByte();
			}
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0004E3F0 File Offset: 0x0004C5F0
		private bool TryGetByte(out byte ch)
		{
			int num;
			int num2;
			byte[] buffer = base.BufferReader.GetBuffer(1, out num, out num2);
			if (num < num2)
			{
				ch = buffer[num];
				return true;
			}
			ch = 0;
			return false;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0004E420 File Offset: 0x0004C620
		private string UnescapeJsonString(string val)
		{
			if (val == null)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int startIndex = 0;
			int num = 0;
			for (int i = 0; i < val.Length; i++)
			{
				if (val[i] == '\\')
				{
					i++;
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					stringBuilder.Append(val, startIndex, num);
					if (i >= val.Length)
					{
						XmlExceptionHelper.ThrowXmlException(this, new XmlException(SR.GetString("Encountered an unexpected character '{0}' in JSON.", new object[]
						{
							val[i]
						})));
					}
					char c = val[i];
					if (c <= '\\')
					{
						if (c <= '\'')
						{
							if (c != '"' && c != '\'')
							{
								goto IL_17D;
							}
						}
						else if (c != '/' && c != '\\')
						{
							goto IL_17D;
						}
						stringBuilder.Append(val[i]);
					}
					else if (c <= 'f')
					{
						if (c != 'b')
						{
							if (c == 'f')
							{
								stringBuilder.Append('\f');
							}
						}
						else
						{
							stringBuilder.Append('\b');
						}
					}
					else if (c != 'n')
					{
						switch (c)
						{
						case 'r':
							stringBuilder.Append('\r');
							break;
						case 't':
							stringBuilder.Append('\t');
							break;
						case 'u':
							if (i + 3 >= val.Length)
							{
								XmlExceptionHelper.ThrowXmlException(this, new XmlException(SR.GetString("Encountered an unexpected character '{0}' in JSON.", new object[]
								{
									val[i]
								})));
							}
							stringBuilder.Append(XmlJsonReader.ParseChar(val.Substring(i + 1, 4), NumberStyles.HexNumber));
							i += 4;
							break;
						}
					}
					else
					{
						stringBuilder.Append('\n');
					}
					IL_17D:
					startIndex = i + 1;
					num = 0;
				}
				else
				{
					num++;
				}
			}
			if (stringBuilder == null)
			{
				return val;
			}
			if (num > 0)
			{
				stringBuilder.Append(val, startIndex, num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0004E5DF File Offset: 0x0004C7DF
		public XmlJsonReader()
		{
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0004E5EE File Offset: 0x0004C7EE
		// Note: this type is marked as 'beforefieldinit'.
		static XmlJsonReader()
		{
		}

		// Token: 0x040009FD RID: 2557
		private const int MaxTextChunk = 2048;

		// Token: 0x040009FE RID: 2558
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
			2,
			2,
			0,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			0,
			0,
			0,
			0,
			3,
			0,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			0,
			0,
			0,
			0,
			0,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3
		};

		// Token: 0x040009FF RID: 2559
		private bool buffered;

		// Token: 0x04000A00 RID: 2560
		private byte[] charactersToSkipOnNextRead;

		// Token: 0x04000A01 RID: 2561
		private XmlJsonReader.JsonComplexTextMode complexTextMode = XmlJsonReader.JsonComplexTextMode.None;

		// Token: 0x04000A02 RID: 2562
		private bool expectingFirstElementInNonPrimitiveChild;

		// Token: 0x04000A03 RID: 2563
		private int maxBytesPerRead;

		// Token: 0x04000A04 RID: 2564
		private OnXmlDictionaryReaderClose onReaderClose;

		// Token: 0x04000A05 RID: 2565
		private bool readServerTypeElement;

		// Token: 0x04000A06 RID: 2566
		private int scopeDepth;

		// Token: 0x04000A07 RID: 2567
		private JsonNodeType[] scopes;

		// Token: 0x0200018E RID: 398
		private enum JsonComplexTextMode
		{
			// Token: 0x04000A09 RID: 2569
			QuotedText,
			// Token: 0x04000A0A RID: 2570
			NumericalText,
			// Token: 0x04000A0B RID: 2571
			None
		}

		// Token: 0x0200018F RID: 399
		private static class CharType
		{
			// Token: 0x04000A0C RID: 2572
			public const byte FirstName = 1;

			// Token: 0x04000A0D RID: 2573
			public const byte Name = 2;

			// Token: 0x04000A0E RID: 2574
			public const byte None = 0;
		}
	}
}
