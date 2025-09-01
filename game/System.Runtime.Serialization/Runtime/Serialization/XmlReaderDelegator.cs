using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace System.Runtime.Serialization
{
	// Token: 0x0200014E RID: 334
	internal class XmlReaderDelegator
	{
		// Token: 0x060010DA RID: 4314 RVA: 0x000432FE File Offset: 0x000414FE
		public XmlReaderDelegator(XmlReader reader)
		{
			XmlObjectSerializer.CheckNull(reader, "reader");
			this.reader = reader;
			this.dictionaryReader = (reader as XmlDictionaryReader);
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00043324 File Offset: 0x00041524
		internal XmlReader UnderlyingReader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x0004332C File Offset: 0x0004152C
		internal ExtensionDataReader UnderlyingExtensionDataReader
		{
			get
			{
				return this.reader as ExtensionDataReader;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x00043339 File Offset: 0x00041539
		internal int AttributeCount
		{
			get
			{
				if (!this.isEndOfEmptyElement)
				{
					return this.reader.AttributeCount;
				}
				return 0;
			}
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00043350 File Offset: 0x00041550
		internal string GetAttribute(string name)
		{
			if (!this.isEndOfEmptyElement)
			{
				return this.reader.GetAttribute(name);
			}
			return null;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00043368 File Offset: 0x00041568
		internal string GetAttribute(string name, string namespaceUri)
		{
			if (!this.isEndOfEmptyElement)
			{
				return this.reader.GetAttribute(name, namespaceUri);
			}
			return null;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00043381 File Offset: 0x00041581
		internal string GetAttribute(int i)
		{
			if (this.isEndOfEmptyElement)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("i", SR.GetString("Only Element nodes have attributes.")));
			}
			return this.reader.GetAttribute(i);
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x00003127 File Offset: 0x00001327
		internal bool IsEmptyElement
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x000433B1 File Offset: 0x000415B1
		internal bool IsNamespaceURI(string ns)
		{
			if (this.dictionaryReader == null)
			{
				return ns == this.reader.NamespaceURI;
			}
			return this.dictionaryReader.IsNamespaceUri(ns);
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x000433D9 File Offset: 0x000415D9
		internal bool IsLocalName(string localName)
		{
			if (this.dictionaryReader == null)
			{
				return localName == this.reader.LocalName;
			}
			return this.dictionaryReader.IsLocalName(localName);
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00043401 File Offset: 0x00041601
		internal bool IsNamespaceUri(XmlDictionaryString ns)
		{
			if (this.dictionaryReader == null)
			{
				return ns.Value == this.reader.NamespaceURI;
			}
			return this.dictionaryReader.IsNamespaceUri(ns);
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0004342E File Offset: 0x0004162E
		internal bool IsLocalName(XmlDictionaryString localName)
		{
			if (this.dictionaryReader == null)
			{
				return localName.Value == this.reader.LocalName;
			}
			return this.dictionaryReader.IsLocalName(localName);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0004345C File Offset: 0x0004165C
		internal int IndexOfLocalName(XmlDictionaryString[] localNames, XmlDictionaryString ns)
		{
			if (this.dictionaryReader != null)
			{
				return this.dictionaryReader.IndexOfLocalName(localNames, ns);
			}
			if (this.reader.NamespaceURI == ns.Value)
			{
				string localName = this.LocalName;
				for (int i = 0; i < localNames.Length; i++)
				{
					if (localName == localNames[i].Value)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x000434BF File Offset: 0x000416BF
		public bool IsStartElement()
		{
			return !this.isEndOfEmptyElement && this.reader.IsStartElement();
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x000434D6 File Offset: 0x000416D6
		internal bool IsStartElement(string localname, string ns)
		{
			return !this.isEndOfEmptyElement && this.reader.IsStartElement(localname, ns);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x000434F0 File Offset: 0x000416F0
		public bool IsStartElement(XmlDictionaryString localname, XmlDictionaryString ns)
		{
			if (this.dictionaryReader == null)
			{
				return !this.isEndOfEmptyElement && this.reader.IsStartElement(localname.Value, ns.Value);
			}
			return !this.isEndOfEmptyElement && this.dictionaryReader.IsStartElement(localname, ns);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0004353E File Offset: 0x0004173E
		internal bool MoveToAttribute(string name)
		{
			return !this.isEndOfEmptyElement && this.reader.MoveToAttribute(name);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00043556 File Offset: 0x00041756
		internal bool MoveToAttribute(string name, string ns)
		{
			return !this.isEndOfEmptyElement && this.reader.MoveToAttribute(name, ns);
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0004356F File Offset: 0x0004176F
		internal void MoveToAttribute(int i)
		{
			if (this.isEndOfEmptyElement)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("i", SR.GetString("Only Element nodes have attributes.")));
			}
			this.reader.MoveToAttribute(i);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0004359F File Offset: 0x0004179F
		internal bool MoveToElement()
		{
			return !this.isEndOfEmptyElement && this.reader.MoveToElement();
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x000435B6 File Offset: 0x000417B6
		internal bool MoveToFirstAttribute()
		{
			return !this.isEndOfEmptyElement && this.reader.MoveToFirstAttribute();
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x000435CD File Offset: 0x000417CD
		internal bool MoveToNextAttribute()
		{
			return !this.isEndOfEmptyElement && this.reader.MoveToNextAttribute();
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x000435E4 File Offset: 0x000417E4
		public XmlNodeType NodeType
		{
			get
			{
				if (!this.isEndOfEmptyElement)
				{
					return this.reader.NodeType;
				}
				return XmlNodeType.EndElement;
			}
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x000435FC File Offset: 0x000417FC
		internal bool Read()
		{
			this.reader.MoveToElement();
			if (!this.reader.IsEmptyElement)
			{
				return this.reader.Read();
			}
			if (this.isEndOfEmptyElement)
			{
				this.isEndOfEmptyElement = false;
				return this.reader.Read();
			}
			this.isEndOfEmptyElement = true;
			return true;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00043651 File Offset: 0x00041851
		internal XmlNodeType MoveToContent()
		{
			if (this.isEndOfEmptyElement)
			{
				return XmlNodeType.EndElement;
			}
			return this.reader.MoveToContent();
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00043669 File Offset: 0x00041869
		internal bool ReadAttributeValue()
		{
			return !this.isEndOfEmptyElement && this.reader.ReadAttributeValue();
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00043680 File Offset: 0x00041880
		public void ReadEndElement()
		{
			if (this.isEndOfEmptyElement)
			{
				this.Read();
				return;
			}
			this.reader.ReadEndElement();
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0004369D File Offset: 0x0004189D
		private Exception CreateInvalidPrimitiveTypeException(Type type)
		{
			return new InvalidDataContractException(SR.GetString(type.IsInterface ? "Interface type '{0}' cannot be created. Consider replacing with a non-interface serializable type." : "Type '{0}' is not a valid serializable type.", new object[]
			{
				DataContract.GetClrTypeFullName(type)
			}));
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000436CC File Offset: 0x000418CC
		public object ReadElementContentAsAnyType(Type valueType)
		{
			this.Read();
			object result = this.ReadContentAsAnyType(valueType);
			this.ReadEndElement();
			return result;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x000436E4 File Offset: 0x000418E4
		internal object ReadContentAsAnyType(Type valueType)
		{
			switch (Type.GetTypeCode(valueType))
			{
			case TypeCode.Boolean:
				return this.ReadContentAsBoolean();
			case TypeCode.Char:
				return this.ReadContentAsChar();
			case TypeCode.SByte:
				return this.ReadContentAsSignedByte();
			case TypeCode.Byte:
				return this.ReadContentAsUnsignedByte();
			case TypeCode.Int16:
				return this.ReadContentAsShort();
			case TypeCode.UInt16:
				return this.ReadContentAsUnsignedShort();
			case TypeCode.Int32:
				return this.ReadContentAsInt();
			case TypeCode.UInt32:
				return this.ReadContentAsUnsignedInt();
			case TypeCode.Int64:
				return this.ReadContentAsLong();
			case TypeCode.UInt64:
				return this.ReadContentAsUnsignedLong();
			case TypeCode.Single:
				return this.ReadContentAsSingle();
			case TypeCode.Double:
				return this.ReadContentAsDouble();
			case TypeCode.Decimal:
				return this.ReadContentAsDecimal();
			case TypeCode.DateTime:
				return this.ReadContentAsDateTime();
			case TypeCode.String:
				return this.ReadContentAsString();
			}
			if (valueType == Globals.TypeOfByteArray)
			{
				return this.ReadContentAsBase64();
			}
			if (valueType == Globals.TypeOfObject)
			{
				return new object();
			}
			if (valueType == Globals.TypeOfTimeSpan)
			{
				return this.ReadContentAsTimeSpan();
			}
			if (valueType == Globals.TypeOfGuid)
			{
				return this.ReadContentAsGuid();
			}
			if (valueType == Globals.TypeOfUri)
			{
				return this.ReadContentAsUri();
			}
			if (valueType == Globals.TypeOfXmlQualifiedName)
			{
				return this.ReadContentAsQName();
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateInvalidPrimitiveTypeException(valueType));
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0004388C File Offset: 0x00041A8C
		internal IDataNode ReadExtensionData(Type valueType)
		{
			switch (Type.GetTypeCode(valueType))
			{
			case TypeCode.Boolean:
				return new DataNode<bool>(this.ReadContentAsBoolean());
			case TypeCode.Char:
				return new DataNode<char>(this.ReadContentAsChar());
			case TypeCode.SByte:
				return new DataNode<sbyte>(this.ReadContentAsSignedByte());
			case TypeCode.Byte:
				return new DataNode<byte>(this.ReadContentAsUnsignedByte());
			case TypeCode.Int16:
				return new DataNode<short>(this.ReadContentAsShort());
			case TypeCode.UInt16:
				return new DataNode<ushort>(this.ReadContentAsUnsignedShort());
			case TypeCode.Int32:
				return new DataNode<int>(this.ReadContentAsInt());
			case TypeCode.UInt32:
				return new DataNode<uint>(this.ReadContentAsUnsignedInt());
			case TypeCode.Int64:
				return new DataNode<long>(this.ReadContentAsLong());
			case TypeCode.UInt64:
				return new DataNode<ulong>(this.ReadContentAsUnsignedLong());
			case TypeCode.Single:
				return new DataNode<float>(this.ReadContentAsSingle());
			case TypeCode.Double:
				return new DataNode<double>(this.ReadContentAsDouble());
			case TypeCode.Decimal:
				return new DataNode<decimal>(this.ReadContentAsDecimal());
			case TypeCode.DateTime:
				return new DataNode<DateTime>(this.ReadContentAsDateTime());
			case TypeCode.String:
				return new DataNode<string>(this.ReadContentAsString());
			}
			if (valueType == Globals.TypeOfByteArray)
			{
				return new DataNode<byte[]>(this.ReadContentAsBase64());
			}
			if (valueType == Globals.TypeOfObject)
			{
				return new DataNode<object>(new object());
			}
			if (valueType == Globals.TypeOfTimeSpan)
			{
				return new DataNode<TimeSpan>(this.ReadContentAsTimeSpan());
			}
			if (valueType == Globals.TypeOfGuid)
			{
				return new DataNode<Guid>(this.ReadContentAsGuid());
			}
			if (valueType == Globals.TypeOfUri)
			{
				return new DataNode<Uri>(this.ReadContentAsUri());
			}
			if (valueType == Globals.TypeOfXmlQualifiedName)
			{
				return new DataNode<XmlQualifiedName>(this.ReadContentAsQName());
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateInvalidPrimitiveTypeException(valueType));
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00043A4C File Offset: 0x00041C4C
		private void ThrowConversionException(string value, string type)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(XmlObjectSerializer.TryAddLineInfo(this, SR.GetString("The value '{0}' cannot be parsed as the type '{1}'.", new object[]
			{
				value,
				type
			}))));
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00043A76 File Offset: 0x00041C76
		private void ThrowNotAtElement()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Start element expected. Found {0}.", new object[]
			{
				"EndElement"
			})));
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00043A9A File Offset: 0x00041C9A
		internal virtual char ReadElementContentAsChar()
		{
			return this.ToChar(this.ReadElementContentAsInt());
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00043AA8 File Offset: 0x00041CA8
		internal virtual char ReadContentAsChar()
		{
			return this.ToChar(this.ReadContentAsInt());
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00043AB6 File Offset: 0x00041CB6
		private char ToChar(int value)
		{
			if (value < 0 || value > 65535)
			{
				this.ThrowConversionException(value.ToString(NumberFormatInfo.CurrentInfo), "Char");
			}
			return (char)value;
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00043ADD File Offset: 0x00041CDD
		public string ReadElementContentAsString()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowNotAtElement();
			}
			return this.reader.ReadElementContentAsString();
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00043AF8 File Offset: 0x00041CF8
		internal string ReadContentAsString()
		{
			if (!this.isEndOfEmptyElement)
			{
				return this.reader.ReadContentAsString();
			}
			return string.Empty;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00043B13 File Offset: 0x00041D13
		public bool ReadElementContentAsBoolean()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowNotAtElement();
			}
			return this.reader.ReadElementContentAsBoolean();
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00043B2E File Offset: 0x00041D2E
		internal bool ReadContentAsBoolean()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowConversionException(string.Empty, "Boolean");
			}
			return this.reader.ReadContentAsBoolean();
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00043B53 File Offset: 0x00041D53
		public float ReadElementContentAsFloat()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowNotAtElement();
			}
			return this.reader.ReadElementContentAsFloat();
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00043B6E File Offset: 0x00041D6E
		internal float ReadContentAsSingle()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowConversionException(string.Empty, "Float");
			}
			return this.reader.ReadContentAsFloat();
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00043B93 File Offset: 0x00041D93
		public double ReadElementContentAsDouble()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowNotAtElement();
			}
			return this.reader.ReadElementContentAsDouble();
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00043BAE File Offset: 0x00041DAE
		internal double ReadContentAsDouble()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowConversionException(string.Empty, "Double");
			}
			return this.reader.ReadContentAsDouble();
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00043BD3 File Offset: 0x00041DD3
		public decimal ReadElementContentAsDecimal()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowNotAtElement();
			}
			return this.reader.ReadElementContentAsDecimal();
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00043BEE File Offset: 0x00041DEE
		internal decimal ReadContentAsDecimal()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowConversionException(string.Empty, "Decimal");
			}
			return this.reader.ReadContentAsDecimal();
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00043C13 File Offset: 0x00041E13
		internal virtual byte[] ReadElementContentAsBase64()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowNotAtElement();
			}
			if (this.dictionaryReader == null)
			{
				return this.ReadContentAsBase64(this.reader.ReadElementContentAsString());
			}
			return this.dictionaryReader.ReadElementContentAsBase64();
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00043C48 File Offset: 0x00041E48
		internal virtual byte[] ReadContentAsBase64()
		{
			if (this.isEndOfEmptyElement)
			{
				return new byte[0];
			}
			if (this.dictionaryReader == null)
			{
				return this.ReadContentAsBase64(this.reader.ReadContentAsString());
			}
			return this.dictionaryReader.ReadContentAsBase64();
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00043C80 File Offset: 0x00041E80
		internal byte[] ReadContentAsBase64(string str)
		{
			if (str == null)
			{
				return null;
			}
			str = str.Trim();
			if (str.Length == 0)
			{
				return new byte[0];
			}
			byte[] result;
			try
			{
				result = Convert.FromBase64String(str);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(str, "byte[]", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(str, "byte[]", exception2));
			}
			return result;
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00043CF8 File Offset: 0x00041EF8
		internal virtual DateTime ReadElementContentAsDateTime()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowNotAtElement();
			}
			return this.reader.ReadElementContentAsDateTime();
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00043D13 File Offset: 0x00041F13
		internal virtual DateTime ReadContentAsDateTime()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowConversionException(string.Empty, "DateTime");
			}
			return this.reader.ReadContentAsDateTime();
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00043D38 File Offset: 0x00041F38
		public int ReadElementContentAsInt()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowNotAtElement();
			}
			return this.reader.ReadElementContentAsInt();
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00043D53 File Offset: 0x00041F53
		internal int ReadContentAsInt()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowConversionException(string.Empty, "Int32");
			}
			return this.reader.ReadContentAsInt();
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00043D78 File Offset: 0x00041F78
		public long ReadElementContentAsLong()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowNotAtElement();
			}
			return this.reader.ReadElementContentAsLong();
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00043D93 File Offset: 0x00041F93
		internal long ReadContentAsLong()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowConversionException(string.Empty, "Int64");
			}
			return this.reader.ReadContentAsLong();
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00043DB8 File Offset: 0x00041FB8
		public short ReadElementContentAsShort()
		{
			return this.ToShort(this.ReadElementContentAsInt());
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00043DC6 File Offset: 0x00041FC6
		internal short ReadContentAsShort()
		{
			return this.ToShort(this.ReadContentAsInt());
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00043DD4 File Offset: 0x00041FD4
		private short ToShort(int value)
		{
			if (value < -32768 || value > 32767)
			{
				this.ThrowConversionException(value.ToString(NumberFormatInfo.CurrentInfo), "Int16");
			}
			return (short)value;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00043DFF File Offset: 0x00041FFF
		public byte ReadElementContentAsUnsignedByte()
		{
			return this.ToByte(this.ReadElementContentAsInt());
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00043E0D File Offset: 0x0004200D
		internal byte ReadContentAsUnsignedByte()
		{
			return this.ToByte(this.ReadContentAsInt());
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00043E1B File Offset: 0x0004201B
		private byte ToByte(int value)
		{
			if (value < 0 || value > 255)
			{
				this.ThrowConversionException(value.ToString(NumberFormatInfo.CurrentInfo), "Byte");
			}
			return (byte)value;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00043E42 File Offset: 0x00042042
		public sbyte ReadElementContentAsSignedByte()
		{
			return this.ToSByte(this.ReadElementContentAsInt());
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00043E50 File Offset: 0x00042050
		internal sbyte ReadContentAsSignedByte()
		{
			return this.ToSByte(this.ReadContentAsInt());
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00043E5E File Offset: 0x0004205E
		private sbyte ToSByte(int value)
		{
			if (value < -128 || value > 127)
			{
				this.ThrowConversionException(value.ToString(NumberFormatInfo.CurrentInfo), "SByte");
			}
			return (sbyte)value;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00043E83 File Offset: 0x00042083
		public uint ReadElementContentAsUnsignedInt()
		{
			return this.ToUInt32(this.ReadElementContentAsLong());
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00043E91 File Offset: 0x00042091
		internal uint ReadContentAsUnsignedInt()
		{
			return this.ToUInt32(this.ReadContentAsLong());
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00043E9F File Offset: 0x0004209F
		private uint ToUInt32(long value)
		{
			if (value < 0L || value > (long)((ulong)-1))
			{
				this.ThrowConversionException(value.ToString(NumberFormatInfo.CurrentInfo), "UInt32");
			}
			return (uint)value;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00043EC4 File Offset: 0x000420C4
		internal virtual ulong ReadElementContentAsUnsignedLong()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowNotAtElement();
			}
			string text = this.reader.ReadElementContentAsString();
			if (text == null || text.Length == 0)
			{
				this.ThrowConversionException(string.Empty, "UInt64");
			}
			return XmlConverter.ToUInt64(text);
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00043F0C File Offset: 0x0004210C
		internal virtual ulong ReadContentAsUnsignedLong()
		{
			string text = this.reader.ReadContentAsString();
			if (text == null || text.Length == 0)
			{
				this.ThrowConversionException(string.Empty, "UInt64");
			}
			return XmlConverter.ToUInt64(text);
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00043F46 File Offset: 0x00042146
		public ushort ReadElementContentAsUnsignedShort()
		{
			return this.ToUInt16(this.ReadElementContentAsInt());
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00043F54 File Offset: 0x00042154
		internal ushort ReadContentAsUnsignedShort()
		{
			return this.ToUInt16(this.ReadContentAsInt());
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00043F62 File Offset: 0x00042162
		private ushort ToUInt16(int value)
		{
			if (value < 0 || value > 65535)
			{
				this.ThrowConversionException(value.ToString(NumberFormatInfo.CurrentInfo), "UInt16");
			}
			return (ushort)value;
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00043F89 File Offset: 0x00042189
		public TimeSpan ReadElementContentAsTimeSpan()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowNotAtElement();
			}
			return XmlConverter.ToTimeSpan(this.reader.ReadElementContentAsString());
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00043FA9 File Offset: 0x000421A9
		internal TimeSpan ReadContentAsTimeSpan()
		{
			return XmlConverter.ToTimeSpan(this.reader.ReadContentAsString());
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00043FBC File Offset: 0x000421BC
		public Guid ReadElementContentAsGuid()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowNotAtElement();
			}
			string text = this.reader.ReadElementContentAsString();
			Guid result;
			try
			{
				result = Guid.Parse(text);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "Guid", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "Guid", exception2));
			}
			catch (OverflowException exception3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "Guid", exception3));
			}
			return result;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00044050 File Offset: 0x00042250
		internal Guid ReadContentAsGuid()
		{
			string text = this.reader.ReadContentAsString();
			Guid result;
			try
			{
				result = Guid.Parse(text);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "Guid", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "Guid", exception2));
			}
			catch (OverflowException exception3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "Guid", exception3));
			}
			return result;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x000440D8 File Offset: 0x000422D8
		public Uri ReadElementContentAsUri()
		{
			if (this.isEndOfEmptyElement)
			{
				this.ThrowNotAtElement();
			}
			string text = this.ReadElementContentAsString();
			Uri result;
			try
			{
				result = new Uri(text, UriKind.RelativeOrAbsolute);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "Uri", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "Uri", exception2));
			}
			return result;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00044148 File Offset: 0x00042348
		internal Uri ReadContentAsUri()
		{
			string text = this.ReadContentAsString();
			Uri result;
			try
			{
				result = new Uri(text, UriKind.RelativeOrAbsolute);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "Uri", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "Uri", exception2));
			}
			return result;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x000441AC File Offset: 0x000423AC
		public XmlQualifiedName ReadElementContentAsQName()
		{
			this.Read();
			XmlQualifiedName result = this.ReadContentAsQName();
			this.ReadEndElement();
			return result;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x000441C1 File Offset: 0x000423C1
		internal virtual XmlQualifiedName ReadContentAsQName()
		{
			return this.ParseQualifiedName(this.ReadContentAsString());
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x000441D0 File Offset: 0x000423D0
		private XmlQualifiedName ParseQualifiedName(string str)
		{
			string empty;
			string ns;
			if (str == null || str.Length == 0)
			{
				ns = (empty = string.Empty);
			}
			else
			{
				string text;
				XmlObjectSerializerReadContext.ParseQualifiedName(str, this, out empty, out ns, out text);
			}
			return new XmlQualifiedName(empty, ns);
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00044206 File Offset: 0x00042406
		private void CheckExpectedArrayLength(XmlObjectSerializerReadContext context, int arrayLength)
		{
			context.IncrementItemCount(arrayLength);
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0004420F File Offset: 0x0004240F
		protected int GetArrayLengthQuota(XmlObjectSerializerReadContext context)
		{
			if (this.dictionaryReader.Quotas == null)
			{
				return context.RemainingItemCount;
			}
			return Math.Min(context.RemainingItemCount, this.dictionaryReader.Quotas.MaxArrayLength);
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00044240 File Offset: 0x00042440
		private void CheckActualArrayLength(int expectedLength, int actualLength, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
		{
			if (expectedLength != actualLength)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Array length '{0}' provided by Size attribute is not equal to the number of array elements '{1}' from namespace '{2}' found.", new object[]
				{
					expectedLength,
					itemName.Value,
					itemNamespace.Value
				})));
			}
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00044280 File Offset: 0x00042480
		internal bool TryReadBooleanArray(XmlObjectSerializerReadContext context, XmlDictionaryString itemName, XmlDictionaryString itemNamespace, int arrayLength, out bool[] array)
		{
			if (this.dictionaryReader == null)
			{
				array = null;
				return false;
			}
			if (arrayLength != -1)
			{
				this.CheckExpectedArrayLength(context, arrayLength);
				array = new bool[arrayLength];
				int num = 0;
				int num2;
				while ((num2 = this.dictionaryReader.ReadArray(itemName, itemNamespace, array, num, arrayLength - num)) > 0)
				{
					num += num2;
				}
				this.CheckActualArrayLength(arrayLength, num, itemName, itemNamespace);
			}
			else
			{
				array = BooleanArrayHelperWithDictionaryString.Instance.ReadArray(this.dictionaryReader, itemName, itemNamespace, this.GetArrayLengthQuota(context));
				context.IncrementItemCount(array.Length);
			}
			return true;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0004430C File Offset: 0x0004250C
		internal bool TryReadDateTimeArray(XmlObjectSerializerReadContext context, XmlDictionaryString itemName, XmlDictionaryString itemNamespace, int arrayLength, out DateTime[] array)
		{
			if (this.dictionaryReader == null)
			{
				array = null;
				return false;
			}
			if (arrayLength != -1)
			{
				this.CheckExpectedArrayLength(context, arrayLength);
				array = new DateTime[arrayLength];
				int num = 0;
				int num2;
				while ((num2 = this.dictionaryReader.ReadArray(itemName, itemNamespace, array, num, arrayLength - num)) > 0)
				{
					num += num2;
				}
				this.CheckActualArrayLength(arrayLength, num, itemName, itemNamespace);
			}
			else
			{
				array = DateTimeArrayHelperWithDictionaryString.Instance.ReadArray(this.dictionaryReader, itemName, itemNamespace, this.GetArrayLengthQuota(context));
				context.IncrementItemCount(array.Length);
			}
			return true;
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00044398 File Offset: 0x00042598
		internal bool TryReadDecimalArray(XmlObjectSerializerReadContext context, XmlDictionaryString itemName, XmlDictionaryString itemNamespace, int arrayLength, out decimal[] array)
		{
			if (this.dictionaryReader == null)
			{
				array = null;
				return false;
			}
			if (arrayLength != -1)
			{
				this.CheckExpectedArrayLength(context, arrayLength);
				array = new decimal[arrayLength];
				int num = 0;
				int num2;
				while ((num2 = this.dictionaryReader.ReadArray(itemName, itemNamespace, array, num, arrayLength - num)) > 0)
				{
					num += num2;
				}
				this.CheckActualArrayLength(arrayLength, num, itemName, itemNamespace);
			}
			else
			{
				array = DecimalArrayHelperWithDictionaryString.Instance.ReadArray(this.dictionaryReader, itemName, itemNamespace, this.GetArrayLengthQuota(context));
				context.IncrementItemCount(array.Length);
			}
			return true;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00044424 File Offset: 0x00042624
		internal bool TryReadInt32Array(XmlObjectSerializerReadContext context, XmlDictionaryString itemName, XmlDictionaryString itemNamespace, int arrayLength, out int[] array)
		{
			if (this.dictionaryReader == null)
			{
				array = null;
				return false;
			}
			if (arrayLength != -1)
			{
				this.CheckExpectedArrayLength(context, arrayLength);
				array = new int[arrayLength];
				int num = 0;
				int num2;
				while ((num2 = this.dictionaryReader.ReadArray(itemName, itemNamespace, array, num, arrayLength - num)) > 0)
				{
					num += num2;
				}
				this.CheckActualArrayLength(arrayLength, num, itemName, itemNamespace);
			}
			else
			{
				array = Int32ArrayHelperWithDictionaryString.Instance.ReadArray(this.dictionaryReader, itemName, itemNamespace, this.GetArrayLengthQuota(context));
				context.IncrementItemCount(array.Length);
			}
			return true;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x000444B0 File Offset: 0x000426B0
		internal bool TryReadInt64Array(XmlObjectSerializerReadContext context, XmlDictionaryString itemName, XmlDictionaryString itemNamespace, int arrayLength, out long[] array)
		{
			if (this.dictionaryReader == null)
			{
				array = null;
				return false;
			}
			if (arrayLength != -1)
			{
				this.CheckExpectedArrayLength(context, arrayLength);
				array = new long[arrayLength];
				int num = 0;
				int num2;
				while ((num2 = this.dictionaryReader.ReadArray(itemName, itemNamespace, array, num, arrayLength - num)) > 0)
				{
					num += num2;
				}
				this.CheckActualArrayLength(arrayLength, num, itemName, itemNamespace);
			}
			else
			{
				array = Int64ArrayHelperWithDictionaryString.Instance.ReadArray(this.dictionaryReader, itemName, itemNamespace, this.GetArrayLengthQuota(context));
				context.IncrementItemCount(array.Length);
			}
			return true;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0004453C File Offset: 0x0004273C
		internal bool TryReadSingleArray(XmlObjectSerializerReadContext context, XmlDictionaryString itemName, XmlDictionaryString itemNamespace, int arrayLength, out float[] array)
		{
			if (this.dictionaryReader == null)
			{
				array = null;
				return false;
			}
			if (arrayLength != -1)
			{
				this.CheckExpectedArrayLength(context, arrayLength);
				array = new float[arrayLength];
				int num = 0;
				int num2;
				while ((num2 = this.dictionaryReader.ReadArray(itemName, itemNamespace, array, num, arrayLength - num)) > 0)
				{
					num += num2;
				}
				this.CheckActualArrayLength(arrayLength, num, itemName, itemNamespace);
			}
			else
			{
				array = SingleArrayHelperWithDictionaryString.Instance.ReadArray(this.dictionaryReader, itemName, itemNamespace, this.GetArrayLengthQuota(context));
				context.IncrementItemCount(array.Length);
			}
			return true;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000445C8 File Offset: 0x000427C8
		internal bool TryReadDoubleArray(XmlObjectSerializerReadContext context, XmlDictionaryString itemName, XmlDictionaryString itemNamespace, int arrayLength, out double[] array)
		{
			if (this.dictionaryReader == null)
			{
				array = null;
				return false;
			}
			if (arrayLength != -1)
			{
				this.CheckExpectedArrayLength(context, arrayLength);
				array = new double[arrayLength];
				int num = 0;
				int num2;
				while ((num2 = this.dictionaryReader.ReadArray(itemName, itemNamespace, array, num, arrayLength - num)) > 0)
				{
					num += num2;
				}
				this.CheckActualArrayLength(arrayLength, num, itemName, itemNamespace);
			}
			else
			{
				array = DoubleArrayHelperWithDictionaryString.Instance.ReadArray(this.dictionaryReader, itemName, itemNamespace, this.GetArrayLengthQuota(context));
				context.IncrementItemCount(array.Length);
			}
			return true;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00044654 File Offset: 0x00042854
		internal IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			if (!(this.reader is IXmlNamespaceResolver))
			{
				return null;
			}
			return ((IXmlNamespaceResolver)this.reader).GetNamespacesInScope(scope);
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00044678 File Offset: 0x00042878
		internal bool HasLineInfo()
		{
			IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
			return xmlLineInfo != null && xmlLineInfo.HasLineInfo();
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x0004469C File Offset: 0x0004289C
		internal int LineNumber
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x000446C0 File Offset: 0x000428C0
		internal int LinePosition
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x000446E4 File Offset: 0x000428E4
		// (set) Token: 0x0600113A RID: 4410 RVA: 0x00044720 File Offset: 0x00042920
		internal bool Normalized
		{
			get
			{
				XmlTextReader xmlTextReader = this.reader as XmlTextReader;
				if (xmlTextReader == null)
				{
					IXmlTextParser xmlTextParser = this.reader as IXmlTextParser;
					return xmlTextParser != null && xmlTextParser.Normalized;
				}
				return xmlTextReader.Normalization;
			}
			set
			{
				XmlTextReader xmlTextReader = this.reader as XmlTextReader;
				if (xmlTextReader == null)
				{
					IXmlTextParser xmlTextParser = this.reader as IXmlTextParser;
					if (xmlTextParser != null)
					{
						xmlTextParser.Normalized = value;
						return;
					}
				}
				else
				{
					xmlTextReader.Normalization = value;
				}
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x0004475C File Offset: 0x0004295C
		// (set) Token: 0x0600113C RID: 4412 RVA: 0x00044798 File Offset: 0x00042998
		internal WhitespaceHandling WhitespaceHandling
		{
			get
			{
				XmlTextReader xmlTextReader = this.reader as XmlTextReader;
				if (xmlTextReader != null)
				{
					return xmlTextReader.WhitespaceHandling;
				}
				IXmlTextParser xmlTextParser = this.reader as IXmlTextParser;
				if (xmlTextParser != null)
				{
					return xmlTextParser.WhitespaceHandling;
				}
				return WhitespaceHandling.None;
			}
			set
			{
				XmlTextReader xmlTextReader = this.reader as XmlTextReader;
				if (xmlTextReader == null)
				{
					IXmlTextParser xmlTextParser = this.reader as IXmlTextParser;
					if (xmlTextParser != null)
					{
						xmlTextParser.WhitespaceHandling = value;
						return;
					}
				}
				else
				{
					xmlTextReader.WhitespaceHandling = value;
				}
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x000447D2 File Offset: 0x000429D2
		internal string Name
		{
			get
			{
				return this.reader.Name;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x000447DF File Offset: 0x000429DF
		public string LocalName
		{
			get
			{
				return this.reader.LocalName;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x000447EC File Offset: 0x000429EC
		internal string NamespaceURI
		{
			get
			{
				return this.reader.NamespaceURI;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x000447F9 File Offset: 0x000429F9
		internal string Value
		{
			get
			{
				return this.reader.Value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x00044806 File Offset: 0x00042A06
		internal Type ValueType
		{
			get
			{
				return this.reader.ValueType;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x00044813 File Offset: 0x00042A13
		internal int Depth
		{
			get
			{
				return this.reader.Depth;
			}
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00044820 File Offset: 0x00042A20
		internal string LookupNamespace(string prefix)
		{
			return this.reader.LookupNamespace(prefix);
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0004482E File Offset: 0x00042A2E
		internal bool EOF
		{
			get
			{
				return this.reader.EOF;
			}
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0004483B File Offset: 0x00042A3B
		internal void Skip()
		{
			this.reader.Skip();
			this.isEndOfEmptyElement = false;
		}

		// Token: 0x04000725 RID: 1829
		protected XmlReader reader;

		// Token: 0x04000726 RID: 1830
		protected XmlDictionaryReader dictionaryReader;

		// Token: 0x04000727 RID: 1831
		protected bool isEndOfEmptyElement;
	}
}
