using System;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization;
using System.Security;

namespace System.Xml
{
	// Token: 0x0200004A RID: 74
	internal class XmlBinaryReader : XmlBaseReader, IXmlBinaryReaderInitializer
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x0000D65C File Offset: 0x0000B85C
		public XmlBinaryReader()
		{
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000D664 File Offset: 0x0000B864
		public void SetInput(byte[] buffer, int offset, int count, IXmlDictionary dictionary, XmlDictionaryReaderQuotas quotas, XmlBinaryReaderSession session, OnXmlDictionaryReaderClose onClose)
		{
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
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
			this.MoveToInitial(quotas, session, onClose);
			base.BufferReader.SetBuffer(buffer, offset, count, dictionary, session);
			this.buffered = true;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000D746 File Offset: 0x0000B946
		public void SetInput(Stream stream, IXmlDictionary dictionary, XmlDictionaryReaderQuotas quotas, XmlBinaryReaderSession session, OnXmlDictionaryReaderClose onClose)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			this.MoveToInitial(quotas, session, onClose);
			base.BufferReader.SetBuffer(stream, dictionary, session);
			this.buffered = false;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000D777 File Offset: 0x0000B977
		private void MoveToInitial(XmlDictionaryReaderQuotas quotas, XmlBinaryReaderSession session, OnXmlDictionaryReaderClose onClose)
		{
			base.MoveToInitial(quotas);
			this.maxBytesPerRead = quotas.MaxBytesPerRead;
			this.arrayState = XmlBinaryReader.ArrayState.None;
			this.onClose = onClose;
			this.isTextWithEndElement = false;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000D7A4 File Offset: 0x0000B9A4
		public override void Close()
		{
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

		// Token: 0x060002C7 RID: 711 RVA: 0x0000D7F4 File Offset: 0x0000B9F4
		public override string ReadElementContentAsString()
		{
			if (base.Node.NodeType != XmlNodeType.Element)
			{
				this.MoveToStartElement();
			}
			if (!this.CanOptimizeReadElementContent())
			{
				return base.ReadElementContentAsString();
			}
			XmlBinaryNodeType nodeType = this.GetNodeType();
			string text;
			if (nodeType != XmlBinaryNodeType.Chars8TextWithEndElement)
			{
				if (nodeType != XmlBinaryNodeType.DictionaryTextWithEndElement)
				{
					text = base.ReadElementContentAsString();
				}
				else
				{
					this.SkipNodeType();
					text = base.BufferReader.GetDictionaryString(this.ReadDictionaryKey()).Value;
					this.ReadTextWithEndElement();
				}
			}
			else
			{
				this.SkipNodeType();
				text = base.BufferReader.ReadUTF8String(this.ReadUInt8());
				this.ReadTextWithEndElement();
			}
			if (text.Length > this.Quotas.MaxStringContentLength)
			{
				XmlExceptionHelper.ThrowMaxStringContentLengthExceeded(this, this.Quotas.MaxStringContentLength);
			}
			return text;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000D8B0 File Offset: 0x0000BAB0
		public override bool ReadElementContentAsBoolean()
		{
			if (base.Node.NodeType != XmlNodeType.Element)
			{
				this.MoveToStartElement();
			}
			if (!this.CanOptimizeReadElementContent())
			{
				return base.ReadElementContentAsBoolean();
			}
			XmlBinaryNodeType nodeType = this.GetNodeType();
			bool result;
			if (nodeType != XmlBinaryNodeType.FalseTextWithEndElement)
			{
				if (nodeType != XmlBinaryNodeType.TrueTextWithEndElement)
				{
					if (nodeType != XmlBinaryNodeType.BoolTextWithEndElement)
					{
						result = base.ReadElementContentAsBoolean();
					}
					else
					{
						this.SkipNodeType();
						result = (base.BufferReader.ReadUInt8() != 0);
						this.ReadTextWithEndElement();
					}
				}
				else
				{
					this.SkipNodeType();
					result = true;
					this.ReadTextWithEndElement();
				}
			}
			else
			{
				this.SkipNodeType();
				result = false;
				this.ReadTextWithEndElement();
			}
			return result;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000D948 File Offset: 0x0000BB48
		public override int ReadElementContentAsInt()
		{
			if (base.Node.NodeType != XmlNodeType.Element)
			{
				this.MoveToStartElement();
			}
			if (!this.CanOptimizeReadElementContent())
			{
				return base.ReadElementContentAsInt();
			}
			XmlBinaryNodeType nodeType = this.GetNodeType();
			int result;
			if (nodeType != XmlBinaryNodeType.ZeroTextWithEndElement)
			{
				if (nodeType != XmlBinaryNodeType.OneTextWithEndElement)
				{
					switch (nodeType)
					{
					case XmlBinaryNodeType.Int8TextWithEndElement:
						this.SkipNodeType();
						result = base.BufferReader.ReadInt8();
						this.ReadTextWithEndElement();
						return result;
					case XmlBinaryNodeType.Int16TextWithEndElement:
						this.SkipNodeType();
						result = base.BufferReader.ReadInt16();
						this.ReadTextWithEndElement();
						return result;
					case XmlBinaryNodeType.Int32TextWithEndElement:
						this.SkipNodeType();
						result = base.BufferReader.ReadInt32();
						this.ReadTextWithEndElement();
						return result;
					}
					result = base.ReadElementContentAsInt();
				}
				else
				{
					this.SkipNodeType();
					result = 1;
					this.ReadTextWithEndElement();
				}
			}
			else
			{
				this.SkipNodeType();
				result = 0;
				this.ReadTextWithEndElement();
			}
			return result;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000DA27 File Offset: 0x0000BC27
		private bool CanOptimizeReadElementContent()
		{
			return this.arrayState == XmlBinaryReader.ArrayState.None && !base.Signing;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000DA3C File Offset: 0x0000BC3C
		public override float ReadElementContentAsFloat()
		{
			if (base.Node.NodeType != XmlNodeType.Element)
			{
				this.MoveToStartElement();
			}
			if (this.CanOptimizeReadElementContent() && this.GetNodeType() == XmlBinaryNodeType.FloatTextWithEndElement)
			{
				this.SkipNodeType();
				float result = base.BufferReader.ReadSingle();
				this.ReadTextWithEndElement();
				return result;
			}
			return base.ReadElementContentAsFloat();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000DA90 File Offset: 0x0000BC90
		public override double ReadElementContentAsDouble()
		{
			if (base.Node.NodeType != XmlNodeType.Element)
			{
				this.MoveToStartElement();
			}
			if (this.CanOptimizeReadElementContent() && this.GetNodeType() == XmlBinaryNodeType.DoubleTextWithEndElement)
			{
				this.SkipNodeType();
				double result = base.BufferReader.ReadDouble();
				this.ReadTextWithEndElement();
				return result;
			}
			return base.ReadElementContentAsDouble();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
		public override decimal ReadElementContentAsDecimal()
		{
			if (base.Node.NodeType != XmlNodeType.Element)
			{
				this.MoveToStartElement();
			}
			if (this.CanOptimizeReadElementContent() && this.GetNodeType() == XmlBinaryNodeType.DecimalTextWithEndElement)
			{
				this.SkipNodeType();
				decimal result = base.BufferReader.ReadDecimal();
				this.ReadTextWithEndElement();
				return result;
			}
			return base.ReadElementContentAsDecimal();
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000DB38 File Offset: 0x0000BD38
		public override DateTime ReadElementContentAsDateTime()
		{
			if (base.Node.NodeType != XmlNodeType.Element)
			{
				this.MoveToStartElement();
			}
			if (this.CanOptimizeReadElementContent() && this.GetNodeType() == XmlBinaryNodeType.DateTimeTextWithEndElement)
			{
				this.SkipNodeType();
				DateTime result = base.BufferReader.ReadDateTime();
				this.ReadTextWithEndElement();
				return result;
			}
			return base.ReadElementContentAsDateTime();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000DB8C File Offset: 0x0000BD8C
		public override TimeSpan ReadElementContentAsTimeSpan()
		{
			if (base.Node.NodeType != XmlNodeType.Element)
			{
				this.MoveToStartElement();
			}
			if (this.CanOptimizeReadElementContent() && this.GetNodeType() == XmlBinaryNodeType.TimeSpanTextWithEndElement)
			{
				this.SkipNodeType();
				TimeSpan result = base.BufferReader.ReadTimeSpan();
				this.ReadTextWithEndElement();
				return result;
			}
			return base.ReadElementContentAsTimeSpan();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
		public override Guid ReadElementContentAsGuid()
		{
			if (base.Node.NodeType != XmlNodeType.Element)
			{
				this.MoveToStartElement();
			}
			if (this.CanOptimizeReadElementContent() && this.GetNodeType() == XmlBinaryNodeType.GuidTextWithEndElement)
			{
				this.SkipNodeType();
				Guid result = base.BufferReader.ReadGuid();
				this.ReadTextWithEndElement();
				return result;
			}
			return base.ReadElementContentAsGuid();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000DC34 File Offset: 0x0000BE34
		public override UniqueId ReadElementContentAsUniqueId()
		{
			if (base.Node.NodeType != XmlNodeType.Element)
			{
				this.MoveToStartElement();
			}
			if (this.CanOptimizeReadElementContent() && this.GetNodeType() == XmlBinaryNodeType.UniqueIdTextWithEndElement)
			{
				this.SkipNodeType();
				UniqueId result = base.BufferReader.ReadUniqueId();
				this.ReadTextWithEndElement();
				return result;
			}
			return base.ReadElementContentAsUniqueId();
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000DC88 File Offset: 0x0000BE88
		public override bool TryGetBase64ContentLength(out int length)
		{
			length = 0;
			if (!this.buffered)
			{
				return false;
			}
			if (this.arrayState != XmlBinaryReader.ArrayState.None)
			{
				return false;
			}
			int num;
			if (!base.Node.Value.TryGetByteArrayLength(out num))
			{
				return false;
			}
			int offset = base.BufferReader.Offset;
			bool result;
			try
			{
				bool flag = false;
				while (!flag && !base.BufferReader.EndOfFile)
				{
					XmlBinaryNodeType nodeType = this.GetNodeType();
					this.SkipNodeType();
					int num2;
					if (nodeType != XmlBinaryNodeType.EndElement)
					{
						switch (nodeType)
						{
						case XmlBinaryNodeType.Bytes8Text:
							num2 = base.BufferReader.ReadUInt8();
							break;
						case XmlBinaryNodeType.Bytes8TextWithEndElement:
							num2 = base.BufferReader.ReadUInt8();
							flag = true;
							break;
						case XmlBinaryNodeType.Bytes16Text:
							num2 = base.BufferReader.ReadUInt16();
							break;
						case XmlBinaryNodeType.Bytes16TextWithEndElement:
							num2 = base.BufferReader.ReadUInt16();
							flag = true;
							break;
						case XmlBinaryNodeType.Bytes32Text:
							num2 = base.BufferReader.ReadUInt31();
							break;
						case XmlBinaryNodeType.Bytes32TextWithEndElement:
							num2 = base.BufferReader.ReadUInt31();
							flag = true;
							break;
						default:
							return false;
						}
					}
					else
					{
						num2 = 0;
						flag = true;
					}
					base.BufferReader.Advance(num2);
					if (num > 2147483647 - num2)
					{
						return false;
					}
					num += num2;
				}
				length = num;
				result = true;
			}
			finally
			{
				base.BufferReader.Offset = offset;
			}
			return result;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000DDD4 File Offset: 0x0000BFD4
		private void ReadTextWithEndElement()
		{
			base.ExitScope();
			this.ReadNode();
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000DDE3 File Offset: 0x0000BFE3
		private XmlBaseReader.XmlAtomicTextNode MoveToAtomicTextWithEndElement()
		{
			this.isTextWithEndElement = true;
			return base.MoveToAtomicText();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000DDF4 File Offset: 0x0000BFF4
		public override bool Read()
		{
			if (base.Node.ReadState == ReadState.Closed)
			{
				return false;
			}
			base.SignNode();
			if (this.isTextWithEndElement)
			{
				this.isTextWithEndElement = false;
				base.MoveToEndElement();
				return true;
			}
			if (this.arrayState == XmlBinaryReader.ArrayState.Content)
			{
				if (this.arrayCount != 0)
				{
					this.MoveToArrayElement();
					return true;
				}
				this.arrayState = XmlBinaryReader.ArrayState.None;
			}
			if (base.Node.ExitScope)
			{
				base.ExitScope();
			}
			return this.ReadNode();
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000DE68 File Offset: 0x0000C068
		private bool ReadNode()
		{
			if (!this.buffered)
			{
				base.BufferReader.SetWindow(base.ElementNode.BufferOffset, this.maxBytesPerRead);
			}
			if (base.BufferReader.EndOfFile)
			{
				base.MoveToEndOfFile();
				return false;
			}
			XmlBinaryNodeType nodeType;
			if (this.arrayState == XmlBinaryReader.ArrayState.None)
			{
				nodeType = this.GetNodeType();
				this.SkipNodeType();
			}
			else
			{
				nodeType = this.arrayNodeType;
				this.arrayCount--;
				this.arrayState = XmlBinaryReader.ArrayState.Content;
			}
			switch (nodeType)
			{
			case XmlBinaryNodeType.EndElement:
				base.MoveToEndElement();
				return true;
			case XmlBinaryNodeType.Comment:
				this.ReadName(base.MoveToComment().Value);
				return true;
			case XmlBinaryNodeType.Array:
				this.ReadArray();
				return true;
			case XmlBinaryNodeType.MinElement:
			{
				XmlBaseReader.XmlElementNode xmlElementNode = base.EnterScope();
				xmlElementNode.Prefix.SetValue(PrefixHandleType.Empty);
				this.ReadName(xmlElementNode.LocalName);
				this.ReadAttributes();
				xmlElementNode.Namespace = base.LookupNamespace(PrefixHandleType.Empty);
				xmlElementNode.BufferOffset = base.BufferReader.Offset;
				return true;
			}
			case XmlBinaryNodeType.Element:
			{
				XmlBaseReader.XmlElementNode xmlElementNode = base.EnterScope();
				this.ReadName(xmlElementNode.Prefix);
				this.ReadName(xmlElementNode.LocalName);
				this.ReadAttributes();
				xmlElementNode.Namespace = base.LookupNamespace(xmlElementNode.Prefix);
				xmlElementNode.BufferOffset = base.BufferReader.Offset;
				return true;
			}
			case XmlBinaryNodeType.ShortDictionaryElement:
			{
				XmlBaseReader.XmlElementNode xmlElementNode = base.EnterScope();
				xmlElementNode.Prefix.SetValue(PrefixHandleType.Empty);
				this.ReadDictionaryName(xmlElementNode.LocalName);
				this.ReadAttributes();
				xmlElementNode.Namespace = base.LookupNamespace(PrefixHandleType.Empty);
				xmlElementNode.BufferOffset = base.BufferReader.Offset;
				return true;
			}
			case XmlBinaryNodeType.DictionaryElement:
			{
				XmlBaseReader.XmlElementNode xmlElementNode = base.EnterScope();
				this.ReadName(xmlElementNode.Prefix);
				this.ReadDictionaryName(xmlElementNode.LocalName);
				this.ReadAttributes();
				xmlElementNode.Namespace = base.LookupNamespace(xmlElementNode.Prefix);
				xmlElementNode.BufferOffset = base.BufferReader.Offset;
				return true;
			}
			case XmlBinaryNodeType.PrefixDictionaryElementA:
			case XmlBinaryNodeType.PrefixDictionaryElementB:
			case XmlBinaryNodeType.PrefixDictionaryElementC:
			case XmlBinaryNodeType.PrefixDictionaryElementD:
			case XmlBinaryNodeType.PrefixDictionaryElementE:
			case XmlBinaryNodeType.PrefixDictionaryElementF:
			case XmlBinaryNodeType.PrefixDictionaryElementG:
			case XmlBinaryNodeType.PrefixDictionaryElementH:
			case XmlBinaryNodeType.PrefixDictionaryElementI:
			case XmlBinaryNodeType.PrefixDictionaryElementJ:
			case XmlBinaryNodeType.PrefixDictionaryElementK:
			case XmlBinaryNodeType.PrefixDictionaryElementL:
			case XmlBinaryNodeType.PrefixDictionaryElementM:
			case XmlBinaryNodeType.PrefixDictionaryElementN:
			case XmlBinaryNodeType.PrefixDictionaryElementO:
			case XmlBinaryNodeType.PrefixDictionaryElementP:
			case XmlBinaryNodeType.PrefixDictionaryElementQ:
			case XmlBinaryNodeType.PrefixDictionaryElementR:
			case XmlBinaryNodeType.PrefixDictionaryElementS:
			case XmlBinaryNodeType.PrefixDictionaryElementT:
			case XmlBinaryNodeType.PrefixDictionaryElementU:
			case XmlBinaryNodeType.PrefixDictionaryElementV:
			case XmlBinaryNodeType.PrefixDictionaryElementW:
			case XmlBinaryNodeType.PrefixDictionaryElementX:
			case XmlBinaryNodeType.PrefixDictionaryElementY:
			case XmlBinaryNodeType.PrefixDictionaryElementZ:
			{
				XmlBaseReader.XmlElementNode xmlElementNode = base.EnterScope();
				PrefixHandleType alphaPrefix = PrefixHandle.GetAlphaPrefix(nodeType - XmlBinaryNodeType.PrefixDictionaryElementA);
				xmlElementNode.Prefix.SetValue(alphaPrefix);
				this.ReadDictionaryName(xmlElementNode.LocalName);
				this.ReadAttributes();
				xmlElementNode.Namespace = base.LookupNamespace(alphaPrefix);
				xmlElementNode.BufferOffset = base.BufferReader.Offset;
				return true;
			}
			case XmlBinaryNodeType.PrefixElementA:
			case XmlBinaryNodeType.PrefixElementB:
			case XmlBinaryNodeType.PrefixElementC:
			case XmlBinaryNodeType.PrefixElementD:
			case XmlBinaryNodeType.PrefixElementE:
			case XmlBinaryNodeType.PrefixElementF:
			case XmlBinaryNodeType.PrefixElementG:
			case XmlBinaryNodeType.PrefixElementH:
			case XmlBinaryNodeType.PrefixElementI:
			case XmlBinaryNodeType.PrefixElementJ:
			case XmlBinaryNodeType.PrefixElementK:
			case XmlBinaryNodeType.PrefixElementL:
			case XmlBinaryNodeType.PrefixElementM:
			case XmlBinaryNodeType.PrefixElementN:
			case XmlBinaryNodeType.PrefixElementO:
			case XmlBinaryNodeType.PrefixElementP:
			case XmlBinaryNodeType.PrefixElementQ:
			case XmlBinaryNodeType.PrefixElementR:
			case XmlBinaryNodeType.PrefixElementS:
			case XmlBinaryNodeType.PrefixElementT:
			case XmlBinaryNodeType.PrefixElementU:
			case XmlBinaryNodeType.PrefixElementV:
			case XmlBinaryNodeType.PrefixElementW:
			case XmlBinaryNodeType.PrefixElementX:
			case XmlBinaryNodeType.PrefixElementY:
			case XmlBinaryNodeType.PrefixElementZ:
			{
				XmlBaseReader.XmlElementNode xmlElementNode = base.EnterScope();
				PrefixHandleType alphaPrefix = PrefixHandle.GetAlphaPrefix(nodeType - XmlBinaryNodeType.PrefixElementA);
				xmlElementNode.Prefix.SetValue(alphaPrefix);
				this.ReadName(xmlElementNode.LocalName);
				this.ReadAttributes();
				xmlElementNode.Namespace = base.LookupNamespace(alphaPrefix);
				xmlElementNode.BufferOffset = base.BufferReader.Offset;
				return true;
			}
			case XmlBinaryNodeType.ZeroTextWithEndElement:
				this.MoveToAtomicTextWithEndElement().Value.SetValue(ValueHandleType.Zero);
				if (base.OutsideRootElement)
				{
					this.VerifyWhitespace();
				}
				return true;
			case XmlBinaryNodeType.OneTextWithEndElement:
				this.MoveToAtomicTextWithEndElement().Value.SetValue(ValueHandleType.One);
				if (base.OutsideRootElement)
				{
					this.VerifyWhitespace();
				}
				return true;
			case XmlBinaryNodeType.FalseTextWithEndElement:
				this.MoveToAtomicTextWithEndElement().Value.SetValue(ValueHandleType.False);
				if (base.OutsideRootElement)
				{
					this.VerifyWhitespace();
				}
				return true;
			case XmlBinaryNodeType.TrueTextWithEndElement:
				this.MoveToAtomicTextWithEndElement().Value.SetValue(ValueHandleType.True);
				if (base.OutsideRootElement)
				{
					this.VerifyWhitespace();
				}
				return true;
			case XmlBinaryNodeType.Int8TextWithEndElement:
				this.ReadText(this.MoveToAtomicTextWithEndElement(), ValueHandleType.Int8, 1);
				return true;
			case XmlBinaryNodeType.Int16TextWithEndElement:
				this.ReadText(this.MoveToAtomicTextWithEndElement(), ValueHandleType.Int16, 2);
				return true;
			case XmlBinaryNodeType.Int32TextWithEndElement:
				this.ReadText(this.MoveToAtomicTextWithEndElement(), ValueHandleType.Int32, 4);
				return true;
			case XmlBinaryNodeType.Int64TextWithEndElement:
				this.ReadText(this.MoveToAtomicTextWithEndElement(), ValueHandleType.Int64, 8);
				return true;
			case XmlBinaryNodeType.FloatTextWithEndElement:
				this.ReadText(this.MoveToAtomicTextWithEndElement(), ValueHandleType.Single, 4);
				return true;
			case XmlBinaryNodeType.DoubleTextWithEndElement:
				this.ReadText(this.MoveToAtomicTextWithEndElement(), ValueHandleType.Double, 8);
				return true;
			case XmlBinaryNodeType.DecimalTextWithEndElement:
				this.ReadText(this.MoveToAtomicTextWithEndElement(), ValueHandleType.Decimal, 16);
				return true;
			case XmlBinaryNodeType.DateTimeTextWithEndElement:
				this.ReadText(this.MoveToAtomicTextWithEndElement(), ValueHandleType.DateTime, 8);
				return true;
			case XmlBinaryNodeType.Chars8Text:
				if (this.buffered)
				{
					this.ReadText(base.MoveToComplexText(), ValueHandleType.UTF8, this.ReadUInt8());
				}
				else
				{
					this.ReadPartialUTF8Text(false, this.ReadUInt8());
				}
				return true;
			case XmlBinaryNodeType.Chars8TextWithEndElement:
				if (this.buffered)
				{
					this.ReadText(this.MoveToAtomicTextWithEndElement(), ValueHandleType.UTF8, this.ReadUInt8());
				}
				else
				{
					this.ReadPartialUTF8Text(true, this.ReadUInt8());
				}
				return true;
			case XmlBinaryNodeType.Chars16Text:
				if (this.buffered)
				{
					this.ReadText(base.MoveToComplexText(), ValueHandleType.UTF8, this.ReadUInt16());
				}
				else
				{
					this.ReadPartialUTF8Text(false, this.ReadUInt16());
				}
				return true;
			case XmlBinaryNodeType.Chars16TextWithEndElement:
				if (this.buffered)
				{
					this.ReadText(this.MoveToAtomicTextWithEndElement(), ValueHandleType.UTF8, this.ReadUInt16());
				}
				else
				{
					this.ReadPartialUTF8Text(true, this.ReadUInt16());
				}
				return true;
			case XmlBinaryNodeType.Chars32Text:
				if (this.buffered)
				{
					this.ReadText(base.MoveToComplexText(), ValueHandleType.UTF8, this.ReadUInt31());
				}
				else
				{
					this.ReadPartialUTF8Text(false, this.ReadUInt31());
				}
				return true;
			case XmlBinaryNodeType.Chars32TextWithEndElement:
				if (this.buffered)
				{
					this.ReadText(this.MoveToAtomicTextWithEndElement(), ValueHandleType.UTF8, this.ReadUInt31());
				}
				else
				{
					this.ReadPartialUTF8Text(true, this.ReadUInt31());
				}
				return true;
			case XmlBinaryNodeType.Bytes8Text:
				if (this.buffered)
				{
					this.ReadBinaryText(base.MoveToComplexText(), this.ReadUInt8());
				}
				else
				{
					this.ReadPartialBinaryText(false, this.ReadUInt8());
				}
				return true;
			case XmlBinaryNodeType.Bytes8TextWithEndElement:
				if (this.buffered)
				{
					this.ReadBinaryText(this.MoveToAtomicTextWithEndElement(), this.ReadUInt8());
				}
				else
				{
					this.ReadPartialBinaryText(true, this.ReadUInt8());
				}
				return true;
			case XmlBinaryNodeType.Bytes16Text:
				if (this.buffered)
				{
					this.ReadBinaryText(base.MoveToComplexText(), this.ReadUInt16());
				}
				else
				{
					this.ReadPartialBinaryText(false, this.ReadUInt16());
				}
				return true;
			case XmlBinaryNodeType.Bytes16TextWithEndElement:
				if (this.buffered)
				{
					this.ReadBinaryText(this.MoveToAtomicTextWithEndElement(), this.ReadUInt16());
				}
				else
				{
					this.ReadPartialBinaryText(true, this.ReadUInt16());
				}
				return true;
			case XmlBinaryNodeType.Bytes32Text:
				if (this.buffered)
				{
					this.ReadBinaryText(base.MoveToComplexText(), this.ReadUInt31());
				}
				else
				{
					this.ReadPartialBinaryText(false, this.ReadUInt31());
				}
				return true;
			case XmlBinaryNodeType.Bytes32TextWithEndElement:
				if (this.buffered)
				{
					this.ReadBinaryText(this.MoveToAtomicTextWithEndElement(), this.ReadUInt31());
				}
				else
				{
					this.ReadPartialBinaryText(true, this.ReadUInt31());
				}
				return true;
			case XmlBinaryNodeType.EmptyTextWithEndElement:
				this.MoveToAtomicTextWithEndElement().Value.SetValue(ValueHandleType.Empty);
				if (base.OutsideRootElement)
				{
					this.VerifyWhitespace();
				}
				return true;
			case XmlBinaryNodeType.DictionaryTextWithEndElement:
				this.MoveToAtomicTextWithEndElement().Value.SetDictionaryValue(this.ReadDictionaryKey());
				return true;
			case XmlBinaryNodeType.UniqueIdTextWithEndElement:
				this.ReadText(this.MoveToAtomicTextWithEndElement(), ValueHandleType.UniqueId, 16);
				return true;
			case XmlBinaryNodeType.TimeSpanTextWithEndElement:
				this.ReadText(this.MoveToAtomicTextWithEndElement(), ValueHandleType.TimeSpan, 8);
				return true;
			case XmlBinaryNodeType.GuidTextWithEndElement:
				this.ReadText(this.MoveToAtomicTextWithEndElement(), ValueHandleType.Guid, 16);
				return true;
			case XmlBinaryNodeType.UInt64TextWithEndElement:
				this.ReadText(this.MoveToAtomicTextWithEndElement(), ValueHandleType.UInt64, 8);
				return true;
			case XmlBinaryNodeType.BoolTextWithEndElement:
				this.MoveToAtomicTextWithEndElement().Value.SetValue((this.ReadUInt8() != 0) ? ValueHandleType.True : ValueHandleType.False);
				if (base.OutsideRootElement)
				{
					this.VerifyWhitespace();
				}
				return true;
			case XmlBinaryNodeType.UnicodeChars8Text:
				this.ReadUnicodeText(false, this.ReadUInt8());
				return true;
			case XmlBinaryNodeType.UnicodeChars8TextWithEndElement:
				this.ReadUnicodeText(true, this.ReadUInt8());
				return true;
			case XmlBinaryNodeType.UnicodeChars16Text:
				this.ReadUnicodeText(false, this.ReadUInt16());
				return true;
			case XmlBinaryNodeType.UnicodeChars16TextWithEndElement:
				this.ReadUnicodeText(true, this.ReadUInt16());
				return true;
			case XmlBinaryNodeType.UnicodeChars32Text:
				this.ReadUnicodeText(false, this.ReadUInt31());
				return true;
			case XmlBinaryNodeType.UnicodeChars32TextWithEndElement:
				this.ReadUnicodeText(true, this.ReadUInt31());
				return true;
			case XmlBinaryNodeType.QNameDictionaryTextWithEndElement:
				base.BufferReader.ReadQName(this.MoveToAtomicTextWithEndElement().Value);
				return true;
			}
			base.BufferReader.ReadValue(nodeType, base.MoveToComplexText().Value);
			return true;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000E80F File Offset: 0x0000CA0F
		private void VerifyWhitespace()
		{
			if (!base.Node.Value.IsWhitespace())
			{
				XmlExceptionHelper.ThrowInvalidBinaryFormat(this);
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000E82C File Offset: 0x0000CA2C
		private void ReadAttributes()
		{
			XmlBinaryNodeType nodeType = this.GetNodeType();
			if (nodeType < XmlBinaryNodeType.MinAttribute || nodeType > XmlBinaryNodeType.PrefixAttributeZ)
			{
				return;
			}
			this.ReadAttributes2();
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000E850 File Offset: 0x0000CA50
		private void ReadAttributes2()
		{
			int num = 0;
			if (this.buffered)
			{
				num = base.BufferReader.Offset;
			}
			for (;;)
			{
				XmlBinaryNodeType nodeType = this.GetNodeType();
				switch (nodeType)
				{
				case XmlBinaryNodeType.MinAttribute:
				{
					this.SkipNodeType();
					XmlBaseReader.XmlAttributeNode xmlAttributeNode = base.AddAttribute();
					xmlAttributeNode.Prefix.SetValue(PrefixHandleType.Empty);
					this.ReadName(xmlAttributeNode.LocalName);
					this.ReadAttributeText(xmlAttributeNode.AttributeText);
					continue;
				}
				case XmlBinaryNodeType.Attribute:
				{
					this.SkipNodeType();
					XmlBaseReader.XmlAttributeNode xmlAttributeNode = base.AddAttribute();
					this.ReadName(xmlAttributeNode.Prefix);
					this.ReadName(xmlAttributeNode.LocalName);
					this.ReadAttributeText(xmlAttributeNode.AttributeText);
					base.FixXmlAttribute(xmlAttributeNode);
					continue;
				}
				case XmlBinaryNodeType.ShortDictionaryAttribute:
				{
					this.SkipNodeType();
					XmlBaseReader.XmlAttributeNode xmlAttributeNode = base.AddAttribute();
					xmlAttributeNode.Prefix.SetValue(PrefixHandleType.Empty);
					this.ReadDictionaryName(xmlAttributeNode.LocalName);
					this.ReadAttributeText(xmlAttributeNode.AttributeText);
					continue;
				}
				case XmlBinaryNodeType.DictionaryAttribute:
				{
					this.SkipNodeType();
					XmlBaseReader.XmlAttributeNode xmlAttributeNode = base.AddAttribute();
					this.ReadName(xmlAttributeNode.Prefix);
					this.ReadDictionaryName(xmlAttributeNode.LocalName);
					this.ReadAttributeText(xmlAttributeNode.AttributeText);
					continue;
				}
				case XmlBinaryNodeType.ShortXmlnsAttribute:
				{
					this.SkipNodeType();
					XmlBaseReader.Namespace @namespace = base.AddNamespace();
					@namespace.Prefix.SetValue(PrefixHandleType.Empty);
					this.ReadName(@namespace.Uri);
					XmlBaseReader.XmlAttributeNode xmlAttributeNode = base.AddXmlnsAttribute(@namespace);
					continue;
				}
				case XmlBinaryNodeType.XmlnsAttribute:
				{
					this.SkipNodeType();
					XmlBaseReader.Namespace @namespace = base.AddNamespace();
					this.ReadName(@namespace.Prefix);
					this.ReadName(@namespace.Uri);
					XmlBaseReader.XmlAttributeNode xmlAttributeNode = base.AddXmlnsAttribute(@namespace);
					continue;
				}
				case XmlBinaryNodeType.ShortDictionaryXmlnsAttribute:
				{
					this.SkipNodeType();
					XmlBaseReader.Namespace @namespace = base.AddNamespace();
					@namespace.Prefix.SetValue(PrefixHandleType.Empty);
					this.ReadDictionaryName(@namespace.Uri);
					XmlBaseReader.XmlAttributeNode xmlAttributeNode = base.AddXmlnsAttribute(@namespace);
					continue;
				}
				case XmlBinaryNodeType.DictionaryXmlnsAttribute:
				{
					this.SkipNodeType();
					XmlBaseReader.Namespace @namespace = base.AddNamespace();
					this.ReadName(@namespace.Prefix);
					this.ReadDictionaryName(@namespace.Uri);
					XmlBaseReader.XmlAttributeNode xmlAttributeNode = base.AddXmlnsAttribute(@namespace);
					continue;
				}
				case XmlBinaryNodeType.PrefixDictionaryAttributeA:
				case XmlBinaryNodeType.PrefixDictionaryAttributeB:
				case XmlBinaryNodeType.PrefixDictionaryAttributeC:
				case XmlBinaryNodeType.PrefixDictionaryAttributeD:
				case XmlBinaryNodeType.PrefixDictionaryAttributeE:
				case XmlBinaryNodeType.PrefixDictionaryAttributeF:
				case XmlBinaryNodeType.PrefixDictionaryAttributeG:
				case XmlBinaryNodeType.PrefixDictionaryAttributeH:
				case XmlBinaryNodeType.PrefixDictionaryAttributeI:
				case XmlBinaryNodeType.PrefixDictionaryAttributeJ:
				case XmlBinaryNodeType.PrefixDictionaryAttributeK:
				case XmlBinaryNodeType.PrefixDictionaryAttributeL:
				case XmlBinaryNodeType.PrefixDictionaryAttributeM:
				case XmlBinaryNodeType.PrefixDictionaryAttributeN:
				case XmlBinaryNodeType.PrefixDictionaryAttributeO:
				case XmlBinaryNodeType.PrefixDictionaryAttributeP:
				case XmlBinaryNodeType.PrefixDictionaryAttributeQ:
				case XmlBinaryNodeType.PrefixDictionaryAttributeR:
				case XmlBinaryNodeType.PrefixDictionaryAttributeS:
				case XmlBinaryNodeType.PrefixDictionaryAttributeT:
				case XmlBinaryNodeType.PrefixDictionaryAttributeU:
				case XmlBinaryNodeType.PrefixDictionaryAttributeV:
				case XmlBinaryNodeType.PrefixDictionaryAttributeW:
				case XmlBinaryNodeType.PrefixDictionaryAttributeX:
				case XmlBinaryNodeType.PrefixDictionaryAttributeY:
				case XmlBinaryNodeType.PrefixDictionaryAttributeZ:
				{
					this.SkipNodeType();
					XmlBaseReader.XmlAttributeNode xmlAttributeNode = base.AddAttribute();
					PrefixHandleType alphaPrefix = PrefixHandle.GetAlphaPrefix(nodeType - XmlBinaryNodeType.PrefixDictionaryAttributeA);
					xmlAttributeNode.Prefix.SetValue(alphaPrefix);
					this.ReadDictionaryName(xmlAttributeNode.LocalName);
					this.ReadAttributeText(xmlAttributeNode.AttributeText);
					continue;
				}
				case XmlBinaryNodeType.PrefixAttributeA:
				case XmlBinaryNodeType.PrefixAttributeB:
				case XmlBinaryNodeType.PrefixAttributeC:
				case XmlBinaryNodeType.PrefixAttributeD:
				case XmlBinaryNodeType.PrefixAttributeE:
				case XmlBinaryNodeType.PrefixAttributeF:
				case XmlBinaryNodeType.PrefixAttributeG:
				case XmlBinaryNodeType.PrefixAttributeH:
				case XmlBinaryNodeType.PrefixAttributeI:
				case XmlBinaryNodeType.PrefixAttributeJ:
				case XmlBinaryNodeType.PrefixAttributeK:
				case XmlBinaryNodeType.PrefixAttributeL:
				case XmlBinaryNodeType.PrefixAttributeM:
				case XmlBinaryNodeType.PrefixAttributeN:
				case XmlBinaryNodeType.PrefixAttributeO:
				case XmlBinaryNodeType.PrefixAttributeP:
				case XmlBinaryNodeType.PrefixAttributeQ:
				case XmlBinaryNodeType.PrefixAttributeR:
				case XmlBinaryNodeType.PrefixAttributeS:
				case XmlBinaryNodeType.PrefixAttributeT:
				case XmlBinaryNodeType.PrefixAttributeU:
				case XmlBinaryNodeType.PrefixAttributeV:
				case XmlBinaryNodeType.PrefixAttributeW:
				case XmlBinaryNodeType.PrefixAttributeX:
				case XmlBinaryNodeType.PrefixAttributeY:
				case XmlBinaryNodeType.PrefixAttributeZ:
				{
					this.SkipNodeType();
					XmlBaseReader.XmlAttributeNode xmlAttributeNode = base.AddAttribute();
					PrefixHandleType alphaPrefix = PrefixHandle.GetAlphaPrefix(nodeType - XmlBinaryNodeType.PrefixAttributeA);
					xmlAttributeNode.Prefix.SetValue(alphaPrefix);
					this.ReadName(xmlAttributeNode.LocalName);
					this.ReadAttributeText(xmlAttributeNode.AttributeText);
					continue;
				}
				}
				break;
			}
			if (this.buffered && base.BufferReader.Offset - num > this.maxBytesPerRead)
			{
				XmlExceptionHelper.ThrowMaxBytesPerReadExceeded(this, this.maxBytesPerRead);
			}
			base.ProcessAttributes();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000EBD4 File Offset: 0x0000CDD4
		private void ReadText(XmlBaseReader.XmlTextNode textNode, ValueHandleType type, int length)
		{
			int offset = base.BufferReader.ReadBytes(length);
			textNode.Value.SetValue(type, offset, length);
			if (base.OutsideRootElement)
			{
				this.VerifyWhitespace();
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000EC0A File Offset: 0x0000CE0A
		private void ReadBinaryText(XmlBaseReader.XmlTextNode textNode, int length)
		{
			this.ReadText(textNode, ValueHandleType.Base64, length);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000EC18 File Offset: 0x0000CE18
		private void ReadPartialUTF8Text(bool withEndElement, int length)
		{
			int num = Math.Max(this.maxBytesPerRead - 5, 0);
			if (length > num)
			{
				int num2 = Math.Max(num - 5, 0);
				int num3 = base.BufferReader.ReadBytes(num2);
				int i;
				for (i = num3 + num2 - 1; i >= num3; i--)
				{
					byte @byte = base.BufferReader.GetByte(i);
					if ((@byte & 128) == 0 || (@byte & 192) == 192)
					{
						break;
					}
				}
				int num4 = num3 + num2 - i;
				base.BufferReader.Offset = base.BufferReader.Offset - num4;
				num2 -= num4;
				base.MoveToComplexText().Value.SetValue(ValueHandleType.UTF8, num3, num2);
				if (base.OutsideRootElement)
				{
					this.VerifyWhitespace();
				}
				XmlBinaryNodeType nodeType = withEndElement ? XmlBinaryNodeType.Chars32TextWithEndElement : XmlBinaryNodeType.Chars32Text;
				this.InsertNode(nodeType, length - num2);
				return;
			}
			if (withEndElement)
			{
				this.ReadText(this.MoveToAtomicTextWithEndElement(), ValueHandleType.UTF8, length);
				return;
			}
			this.ReadText(base.MoveToComplexText(), ValueHandleType.UTF8, length);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000ED0C File Offset: 0x0000CF0C
		private void ReadUnicodeText(bool withEndElement, int length)
		{
			if ((length & 1) != 0)
			{
				XmlExceptionHelper.ThrowInvalidBinaryFormat(this);
			}
			if (!this.buffered)
			{
				this.ReadPartialUnicodeText(withEndElement, length);
				return;
			}
			if (withEndElement)
			{
				this.ReadText(this.MoveToAtomicTextWithEndElement(), ValueHandleType.Unicode, length);
				return;
			}
			this.ReadText(base.MoveToComplexText(), ValueHandleType.Unicode, length);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000ED4C File Offset: 0x0000CF4C
		private void ReadPartialUnicodeText(bool withEndElement, int length)
		{
			int num = Math.Max(this.maxBytesPerRead - 5, 0);
			if (length > num)
			{
				int num2 = Math.Max(num - 5, 0);
				if ((num2 & 1) != 0)
				{
					num2--;
				}
				int num3 = base.BufferReader.ReadBytes(num2);
				int num4 = 0;
				char c = (char)base.BufferReader.GetInt16(num3 + num2 - 2);
				if (c >= '\ud800' && c < '\udc00')
				{
					num4 = 2;
				}
				base.BufferReader.Offset = base.BufferReader.Offset - num4;
				num2 -= num4;
				base.MoveToComplexText().Value.SetValue(ValueHandleType.Unicode, num3, num2);
				if (base.OutsideRootElement)
				{
					this.VerifyWhitespace();
				}
				XmlBinaryNodeType nodeType = withEndElement ? XmlBinaryNodeType.UnicodeChars32TextWithEndElement : XmlBinaryNodeType.UnicodeChars32Text;
				this.InsertNode(nodeType, length - num2);
				return;
			}
			if (withEndElement)
			{
				this.ReadText(this.MoveToAtomicTextWithEndElement(), ValueHandleType.Unicode, length);
				return;
			}
			this.ReadText(base.MoveToComplexText(), ValueHandleType.Unicode, length);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000EE34 File Offset: 0x0000D034
		private void ReadPartialBinaryText(bool withEndElement, int length)
		{
			int num = Math.Max(this.maxBytesPerRead - 5, 0);
			if (length > num)
			{
				int num2 = num;
				if (num2 > 3)
				{
					num2 -= num2 % 3;
				}
				this.ReadText(base.MoveToComplexText(), ValueHandleType.Base64, num2);
				XmlBinaryNodeType nodeType = withEndElement ? XmlBinaryNodeType.Bytes32TextWithEndElement : XmlBinaryNodeType.Bytes32Text;
				this.InsertNode(nodeType, length - num2);
				return;
			}
			if (withEndElement)
			{
				this.ReadText(this.MoveToAtomicTextWithEndElement(), ValueHandleType.Base64, length);
				return;
			}
			this.ReadText(base.MoveToComplexText(), ValueHandleType.Base64, length);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000EEAC File Offset: 0x0000D0AC
		private void InsertNode(XmlBinaryNodeType nodeType, int length)
		{
			byte[] array = new byte[5];
			array[0] = (byte)nodeType;
			array[1] = (byte)length;
			length >>= 8;
			array[2] = (byte)length;
			length >>= 8;
			array[3] = (byte)length;
			length >>= 8;
			array[4] = (byte)length;
			base.BufferReader.InsertBytes(array, 0, array.Length);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
		private void ReadAttributeText(XmlBaseReader.XmlAttributeTextNode textNode)
		{
			XmlBinaryNodeType nodeType = this.GetNodeType();
			this.SkipNodeType();
			base.BufferReader.ReadValue(nodeType, textNode.Value);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000EF24 File Offset: 0x0000D124
		private void ReadName(ValueHandle value)
		{
			int num = this.ReadMultiByteUInt31();
			int offset = base.BufferReader.ReadBytes(num);
			value.SetValue(ValueHandleType.UTF8, offset, num);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000EF50 File Offset: 0x0000D150
		private void ReadName(StringHandle handle)
		{
			int num = this.ReadMultiByteUInt31();
			int offset = base.BufferReader.ReadBytes(num);
			handle.SetValue(offset, num);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000EF7C File Offset: 0x0000D17C
		private void ReadName(PrefixHandle prefix)
		{
			int num = this.ReadMultiByteUInt31();
			int offset = base.BufferReader.ReadBytes(num);
			prefix.SetValue(offset, num);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000EFA8 File Offset: 0x0000D1A8
		private void ReadDictionaryName(StringHandle s)
		{
			int value = this.ReadDictionaryKey();
			s.SetValue(value);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000EFC3 File Offset: 0x0000D1C3
		private XmlBinaryNodeType GetNodeType()
		{
			return base.BufferReader.GetNodeType();
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000EFD0 File Offset: 0x0000D1D0
		private void SkipNodeType()
		{
			base.BufferReader.SkipNodeType();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000EFDD File Offset: 0x0000D1DD
		private int ReadDictionaryKey()
		{
			return base.BufferReader.ReadDictionaryKey();
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000EFEA File Offset: 0x0000D1EA
		private int ReadMultiByteUInt31()
		{
			return base.BufferReader.ReadMultiByteUInt31();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000EFF7 File Offset: 0x0000D1F7
		private int ReadUInt8()
		{
			return base.BufferReader.ReadUInt8();
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000F004 File Offset: 0x0000D204
		private int ReadUInt16()
		{
			return base.BufferReader.ReadUInt16();
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000F011 File Offset: 0x0000D211
		private int ReadUInt31()
		{
			return base.BufferReader.ReadUInt31();
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000F020 File Offset: 0x0000D220
		private bool IsValidArrayType(XmlBinaryNodeType nodeType)
		{
			if (nodeType <= XmlBinaryNodeType.TimeSpanTextWithEndElement)
			{
				switch (nodeType)
				{
				case XmlBinaryNodeType.Int16TextWithEndElement:
				case XmlBinaryNodeType.Int32TextWithEndElement:
				case XmlBinaryNodeType.Int64TextWithEndElement:
				case XmlBinaryNodeType.FloatTextWithEndElement:
				case XmlBinaryNodeType.DoubleTextWithEndElement:
				case XmlBinaryNodeType.DecimalTextWithEndElement:
				case XmlBinaryNodeType.DateTimeTextWithEndElement:
					break;
				case XmlBinaryNodeType.Int32Text:
				case XmlBinaryNodeType.Int64Text:
				case XmlBinaryNodeType.FloatText:
				case XmlBinaryNodeType.DoubleText:
				case XmlBinaryNodeType.DecimalText:
				case XmlBinaryNodeType.DateTimeText:
					return false;
				default:
					if (nodeType != XmlBinaryNodeType.TimeSpanTextWithEndElement)
					{
						return false;
					}
					break;
				}
			}
			else if (nodeType != XmlBinaryNodeType.GuidTextWithEndElement && nodeType != XmlBinaryNodeType.BoolTextWithEndElement)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000F094 File Offset: 0x0000D294
		private void ReadArray()
		{
			if (this.GetNodeType() == XmlBinaryNodeType.Array)
			{
				XmlExceptionHelper.ThrowInvalidBinaryFormat(this);
			}
			this.ReadNode();
			if (base.Node.NodeType != XmlNodeType.Element)
			{
				XmlExceptionHelper.ThrowInvalidBinaryFormat(this);
			}
			if (this.GetNodeType() == XmlBinaryNodeType.Array)
			{
				XmlExceptionHelper.ThrowInvalidBinaryFormat(this);
			}
			this.ReadNode();
			if (base.Node.NodeType != XmlNodeType.EndElement)
			{
				XmlExceptionHelper.ThrowInvalidBinaryFormat(this);
			}
			this.arrayState = XmlBinaryReader.ArrayState.Element;
			this.arrayNodeType = this.GetNodeType();
			if (!this.IsValidArrayType(this.arrayNodeType))
			{
				XmlExceptionHelper.ThrowInvalidBinaryFormat(this);
			}
			this.SkipNodeType();
			this.arrayCount = this.ReadMultiByteUInt31();
			if (this.arrayCount == 0)
			{
				XmlExceptionHelper.ThrowInvalidBinaryFormat(this);
			}
			this.MoveToArrayElement();
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000F143 File Offset: 0x0000D343
		private void MoveToArrayElement()
		{
			this.arrayState = XmlBinaryReader.ArrayState.Element;
			base.MoveToNode(base.ElementNode);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000F158 File Offset: 0x0000D358
		private void SkipArrayElements(int count)
		{
			this.arrayCount -= count;
			if (this.arrayCount == 0)
			{
				this.arrayState = XmlBinaryReader.ArrayState.None;
				base.ExitScope();
				this.ReadNode();
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000F184 File Offset: 0x0000D384
		public override bool IsStartArray(out Type type)
		{
			type = null;
			if (this.arrayState != XmlBinaryReader.ArrayState.Element)
			{
				return false;
			}
			XmlBinaryNodeType xmlBinaryNodeType = this.arrayNodeType;
			switch (xmlBinaryNodeType)
			{
			case XmlBinaryNodeType.Int16TextWithEndElement:
				type = typeof(short);
				return true;
			case XmlBinaryNodeType.Int32Text:
			case XmlBinaryNodeType.Int64Text:
			case XmlBinaryNodeType.FloatText:
			case XmlBinaryNodeType.DoubleText:
			case XmlBinaryNodeType.DecimalText:
			case XmlBinaryNodeType.DateTimeText:
				break;
			case XmlBinaryNodeType.Int32TextWithEndElement:
				type = typeof(int);
				return true;
			case XmlBinaryNodeType.Int64TextWithEndElement:
				type = typeof(long);
				return true;
			case XmlBinaryNodeType.FloatTextWithEndElement:
				type = typeof(float);
				return true;
			case XmlBinaryNodeType.DoubleTextWithEndElement:
				type = typeof(double);
				return true;
			case XmlBinaryNodeType.DecimalTextWithEndElement:
				type = typeof(decimal);
				return true;
			case XmlBinaryNodeType.DateTimeTextWithEndElement:
				type = typeof(DateTime);
				return true;
			default:
				switch (xmlBinaryNodeType)
				{
				case XmlBinaryNodeType.UniqueIdTextWithEndElement:
					type = typeof(UniqueId);
					return true;
				case XmlBinaryNodeType.TimeSpanText:
				case XmlBinaryNodeType.GuidText:
					break;
				case XmlBinaryNodeType.TimeSpanTextWithEndElement:
					type = typeof(TimeSpan);
					return true;
				case XmlBinaryNodeType.GuidTextWithEndElement:
					type = typeof(Guid);
					return true;
				default:
					if (xmlBinaryNodeType == XmlBinaryNodeType.BoolTextWithEndElement)
					{
						type = typeof(bool);
						return true;
					}
					break;
				}
				break;
			}
			return false;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000F2B4 File Offset: 0x0000D4B4
		public override bool TryGetArrayLength(out int count)
		{
			count = 0;
			if (!this.buffered)
			{
				return false;
			}
			if (this.arrayState != XmlBinaryReader.ArrayState.Element)
			{
				return false;
			}
			count = this.arrayCount;
			return true;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000F2D7 File Offset: 0x0000D4D7
		private bool IsStartArray(string localName, string namespaceUri, XmlBinaryNodeType nodeType)
		{
			return this.IsStartElement(localName, namespaceUri) && this.arrayState == XmlBinaryReader.ArrayState.Element && this.arrayNodeType == nodeType && !base.Signing;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000F300 File Offset: 0x0000D500
		private bool IsStartArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, XmlBinaryNodeType nodeType)
		{
			return this.IsStartElement(localName, namespaceUri) && this.arrayState == XmlBinaryReader.ArrayState.Element && this.arrayNodeType == nodeType && !base.Signing;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000F32C File Offset: 0x0000D52C
		private void CheckArray(Array array, int offset, int count)
		{
			if (array == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("array"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (offset > array.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					array.Length
				})));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > array.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					array.Length - offset
				})));
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000F3FC File Offset: 0x0000D5FC
		[SecuritySafeCritical]
		private unsafe int ReadArray(bool[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = Math.Min(count, this.arrayCount);
			fixed (bool* ptr = &array[offset])
			{
				bool* ptr2 = ptr;
				base.BufferReader.UnsafeReadArray((byte*)ptr2, (byte*)(ptr2 + num));
			}
			this.SkipArrayElements(num);
			return num;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000F445 File Offset: 0x0000D645
		public override int ReadArray(string localName, string namespaceUri, bool[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.BoolTextWithEndElement))
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000F46F File Offset: 0x0000D66F
		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, bool[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.BoolTextWithEndElement))
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000F49C File Offset: 0x0000D69C
		[SecuritySafeCritical]
		private unsafe int ReadArray(short[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = Math.Min(count, this.arrayCount);
			fixed (short* ptr = &array[offset])
			{
				short* ptr2 = ptr;
				base.BufferReader.UnsafeReadArray((byte*)ptr2, (byte*)(ptr2 + num));
			}
			this.SkipArrayElements(num);
			return num;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000F4E8 File Offset: 0x0000D6E8
		public override int ReadArray(string localName, string namespaceUri, short[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.Int16TextWithEndElement) && BitConverter.IsLittleEndian)
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000F519 File Offset: 0x0000D719
		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, short[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.Int16TextWithEndElement) && BitConverter.IsLittleEndian)
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000F54C File Offset: 0x0000D74C
		[SecuritySafeCritical]
		private unsafe int ReadArray(int[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = Math.Min(count, this.arrayCount);
			fixed (int* ptr = &array[offset])
			{
				int* ptr2 = ptr;
				base.BufferReader.UnsafeReadArray((byte*)ptr2, (byte*)(ptr2 + num));
			}
			this.SkipArrayElements(num);
			return num;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000F598 File Offset: 0x0000D798
		public override int ReadArray(string localName, string namespaceUri, int[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.Int32TextWithEndElement) && BitConverter.IsLittleEndian)
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000F5C9 File Offset: 0x0000D7C9
		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, int[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.Int32TextWithEndElement) && BitConverter.IsLittleEndian)
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000F5FC File Offset: 0x0000D7FC
		[SecuritySafeCritical]
		private unsafe int ReadArray(long[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = Math.Min(count, this.arrayCount);
			fixed (long* ptr = &array[offset])
			{
				long* ptr2 = ptr;
				base.BufferReader.UnsafeReadArray((byte*)ptr2, (byte*)(ptr2 + num));
			}
			this.SkipArrayElements(num);
			return num;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000F648 File Offset: 0x0000D848
		public override int ReadArray(string localName, string namespaceUri, long[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.Int64TextWithEndElement) && BitConverter.IsLittleEndian)
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000F679 File Offset: 0x0000D879
		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, long[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.Int64TextWithEndElement) && BitConverter.IsLittleEndian)
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000F6AC File Offset: 0x0000D8AC
		[SecuritySafeCritical]
		private unsafe int ReadArray(float[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = Math.Min(count, this.arrayCount);
			fixed (float* ptr = &array[offset])
			{
				float* ptr2 = ptr;
				base.BufferReader.UnsafeReadArray((byte*)ptr2, (byte*)(ptr2 + num));
			}
			this.SkipArrayElements(num);
			return num;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
		public override int ReadArray(string localName, string namespaceUri, float[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.FloatTextWithEndElement))
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000F722 File Offset: 0x0000D922
		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, float[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.FloatTextWithEndElement))
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000F74C File Offset: 0x0000D94C
		[SecuritySafeCritical]
		private unsafe int ReadArray(double[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = Math.Min(count, this.arrayCount);
			fixed (double* ptr = &array[offset])
			{
				double* ptr2 = ptr;
				base.BufferReader.UnsafeReadArray((byte*)ptr2, (byte*)(ptr2 + num));
			}
			this.SkipArrayElements(num);
			return num;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000F798 File Offset: 0x0000D998
		public override int ReadArray(string localName, string namespaceUri, double[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.DoubleTextWithEndElement))
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000F7C2 File Offset: 0x0000D9C2
		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, double[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.DoubleTextWithEndElement))
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000F7EC File Offset: 0x0000D9EC
		[SecuritySafeCritical]
		private unsafe int ReadArray(decimal[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = Math.Min(count, this.arrayCount);
			fixed (decimal* ptr = &array[offset])
			{
				decimal* ptr2 = ptr;
				base.BufferReader.UnsafeReadArray((byte*)ptr2, (byte*)(ptr2 + (IntPtr)num * 16 / (IntPtr)sizeof(decimal)));
			}
			this.SkipArrayElements(num);
			return num;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000F839 File Offset: 0x0000DA39
		public override int ReadArray(string localName, string namespaceUri, decimal[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.DecimalTextWithEndElement))
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000F863 File Offset: 0x0000DA63
		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, decimal[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.DecimalTextWithEndElement))
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000F890 File Offset: 0x0000DA90
		private int ReadArray(DateTime[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = Math.Min(count, this.arrayCount);
			for (int i = 0; i < num; i++)
			{
				array[offset + i] = base.BufferReader.ReadDateTime();
			}
			this.SkipArrayElements(num);
			return num;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000F8DB File Offset: 0x0000DADB
		public override int ReadArray(string localName, string namespaceUri, DateTime[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.DateTimeTextWithEndElement))
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000F905 File Offset: 0x0000DB05
		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, DateTime[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.DateTimeTextWithEndElement))
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000F930 File Offset: 0x0000DB30
		private int ReadArray(Guid[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = Math.Min(count, this.arrayCount);
			for (int i = 0; i < num; i++)
			{
				array[offset + i] = base.BufferReader.ReadGuid();
			}
			this.SkipArrayElements(num);
			return num;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000F97B File Offset: 0x0000DB7B
		public override int ReadArray(string localName, string namespaceUri, Guid[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.GuidTextWithEndElement))
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000F9A5 File Offset: 0x0000DBA5
		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, Guid[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.GuidTextWithEndElement))
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000F9D0 File Offset: 0x0000DBD0
		private int ReadArray(TimeSpan[] array, int offset, int count)
		{
			this.CheckArray(array, offset, count);
			int num = Math.Min(count, this.arrayCount);
			for (int i = 0; i < num; i++)
			{
				array[offset + i] = base.BufferReader.ReadTimeSpan();
			}
			this.SkipArrayElements(num);
			return num;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000FA1B File Offset: 0x0000DC1B
		public override int ReadArray(string localName, string namespaceUri, TimeSpan[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.TimeSpanTextWithEndElement))
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000FA45 File Offset: 0x0000DC45
		public override int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, TimeSpan[] array, int offset, int count)
		{
			if (this.IsStartArray(localName, namespaceUri, XmlBinaryNodeType.TimeSpanTextWithEndElement))
			{
				return this.ReadArray(array, offset, count);
			}
			return base.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000FA6F File Offset: 0x0000DC6F
		protected override XmlSigningNodeWriter CreateSigningNodeWriter()
		{
			return new XmlSigningNodeWriter(false);
		}

		// Token: 0x04000207 RID: 519
		private bool isTextWithEndElement;

		// Token: 0x04000208 RID: 520
		private bool buffered;

		// Token: 0x04000209 RID: 521
		private XmlBinaryReader.ArrayState arrayState;

		// Token: 0x0400020A RID: 522
		private int arrayCount;

		// Token: 0x0400020B RID: 523
		private int maxBytesPerRead;

		// Token: 0x0400020C RID: 524
		private XmlBinaryNodeType arrayNodeType;

		// Token: 0x0400020D RID: 525
		private OnXmlDictionaryReaderClose onClose;

		// Token: 0x0200004B RID: 75
		private enum ArrayState
		{
			// Token: 0x0400020F RID: 527
			None,
			// Token: 0x04000210 RID: 528
			Element,
			// Token: 0x04000211 RID: 529
			Content
		}
	}
}
