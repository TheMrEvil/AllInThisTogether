using System;
using System.Globalization;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x02000152 RID: 338
	internal class XmlWriterDelegator
	{
		// Token: 0x060011B2 RID: 4530 RVA: 0x00045269 File Offset: 0x00043469
		public XmlWriterDelegator(XmlWriter writer)
		{
			XmlObjectSerializer.CheckNull(writer, "writer");
			this.writer = writer;
			this.dictionaryWriter = (writer as XmlDictionaryWriter);
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x0004528F File Offset: 0x0004348F
		internal XmlWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00045297 File Offset: 0x00043497
		internal void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x000452A4 File Offset: 0x000434A4
		internal string LookupPrefix(string ns)
		{
			return this.writer.LookupPrefix(ns);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x000452B2 File Offset: 0x000434B2
		private void WriteEndAttribute()
		{
			this.writer.WriteEndAttribute();
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x000452BF File Offset: 0x000434BF
		public void WriteEndElement()
		{
			this.writer.WriteEndElement();
			this.depth--;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x000452DA File Offset: 0x000434DA
		internal void WriteRaw(char[] buffer, int index, int count)
		{
			this.writer.WriteRaw(buffer, index, count);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x000452EA File Offset: 0x000434EA
		internal void WriteRaw(string data)
		{
			this.writer.WriteRaw(data);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x000452F8 File Offset: 0x000434F8
		internal void WriteXmlnsAttribute(XmlDictionaryString ns)
		{
			if (this.dictionaryWriter != null)
			{
				if (ns != null)
				{
					this.dictionaryWriter.WriteXmlnsAttribute(null, ns);
					return;
				}
			}
			else
			{
				this.WriteXmlnsAttribute(ns.Value);
			}
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00045320 File Offset: 0x00043520
		internal void WriteXmlnsAttribute(string ns)
		{
			if (ns != null)
			{
				if (ns.Length == 0)
				{
					this.writer.WriteAttributeString("xmlns", string.Empty, null, ns);
					return;
				}
				if (this.dictionaryWriter != null)
				{
					this.dictionaryWriter.WriteXmlnsAttribute(null, ns);
					return;
				}
				if (this.writer.LookupPrefix(ns) == null)
				{
					string localName = string.Format(CultureInfo.InvariantCulture, "d{0}p{1}", this.depth, this.prefixes);
					this.prefixes++;
					this.writer.WriteAttributeString("xmlns", localName, null, ns);
				}
			}
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x000453C0 File Offset: 0x000435C0
		internal void WriteXmlnsAttribute(string prefix, XmlDictionaryString ns)
		{
			if (this.dictionaryWriter != null)
			{
				this.dictionaryWriter.WriteXmlnsAttribute(prefix, ns);
				return;
			}
			this.writer.WriteAttributeString("xmlns", prefix, null, ns.Value);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x000453F0 File Offset: 0x000435F0
		private void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.writer.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00045400 File Offset: 0x00043600
		private void WriteStartAttribute(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			if (this.dictionaryWriter != null)
			{
				this.dictionaryWriter.WriteStartAttribute(prefix, localName, namespaceUri);
				return;
			}
			this.writer.WriteStartAttribute(prefix, (localName == null) ? null : localName.Value, (namespaceUri == null) ? null : namespaceUri.Value);
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0004543D File Offset: 0x0004363D
		internal void WriteAttributeString(string prefix, string localName, string ns, string value)
		{
			this.WriteStartAttribute(prefix, localName, ns);
			this.WriteAttributeStringValue(value);
			this.WriteEndAttribute();
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00045456 File Offset: 0x00043656
		internal void WriteAttributeString(string prefix, XmlDictionaryString attrName, XmlDictionaryString attrNs, string value)
		{
			this.WriteStartAttribute(prefix, attrName, attrNs);
			this.WriteAttributeStringValue(value);
			this.WriteEndAttribute();
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0004546F File Offset: 0x0004366F
		private void WriteAttributeStringValue(string value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0004547D File Offset: 0x0004367D
		internal void WriteAttributeString(string prefix, XmlDictionaryString attrName, XmlDictionaryString attrNs, XmlDictionaryString value)
		{
			this.WriteStartAttribute(prefix, attrName, attrNs);
			this.WriteAttributeStringValue(value);
			this.WriteEndAttribute();
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00045496 File Offset: 0x00043696
		private void WriteAttributeStringValue(XmlDictionaryString value)
		{
			if (this.dictionaryWriter == null)
			{
				this.writer.WriteString(value.Value);
				return;
			}
			this.dictionaryWriter.WriteString(value);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x000454BE File Offset: 0x000436BE
		internal void WriteAttributeInt(string prefix, XmlDictionaryString attrName, XmlDictionaryString attrNs, int value)
		{
			this.WriteStartAttribute(prefix, attrName, attrNs);
			this.WriteAttributeIntValue(value);
			this.WriteEndAttribute();
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x000454D7 File Offset: 0x000436D7
		private void WriteAttributeIntValue(int value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x000454E5 File Offset: 0x000436E5
		internal void WriteAttributeBool(string prefix, XmlDictionaryString attrName, XmlDictionaryString attrNs, bool value)
		{
			this.WriteStartAttribute(prefix, attrName, attrNs);
			this.WriteAttributeBoolValue(value);
			this.WriteEndAttribute();
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x000454FE File Offset: 0x000436FE
		private void WriteAttributeBoolValue(bool value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0004550C File Offset: 0x0004370C
		internal void WriteAttributeQualifiedName(string attrPrefix, XmlDictionaryString attrName, XmlDictionaryString attrNs, string name, string ns)
		{
			this.WriteXmlnsAttribute(ns);
			this.WriteStartAttribute(attrPrefix, attrName, attrNs);
			this.WriteAttributeQualifiedNameValue(name, ns);
			this.WriteEndAttribute();
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0004552F File Offset: 0x0004372F
		private void WriteAttributeQualifiedNameValue(string name, string ns)
		{
			this.writer.WriteQualifiedName(name, ns);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0004553E File Offset: 0x0004373E
		internal void WriteAttributeQualifiedName(string attrPrefix, XmlDictionaryString attrName, XmlDictionaryString attrNs, XmlDictionaryString name, XmlDictionaryString ns)
		{
			this.WriteXmlnsAttribute(ns);
			this.WriteStartAttribute(attrPrefix, attrName, attrNs);
			this.WriteAttributeQualifiedNameValue(name, ns);
			this.WriteEndAttribute();
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00045561 File Offset: 0x00043761
		private void WriteAttributeQualifiedNameValue(XmlDictionaryString name, XmlDictionaryString ns)
		{
			if (this.dictionaryWriter == null)
			{
				this.writer.WriteQualifiedName(name.Value, ns.Value);
				return;
			}
			this.dictionaryWriter.WriteQualifiedName(name, ns);
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00045590 File Offset: 0x00043790
		internal void WriteStartElement(string localName, string ns)
		{
			this.WriteStartElement(null, localName, ns);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0004559B File Offset: 0x0004379B
		internal virtual void WriteStartElement(string prefix, string localName, string ns)
		{
			this.writer.WriteStartElement(prefix, localName, ns);
			this.depth++;
			this.prefixes = 1;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x000455C0 File Offset: 0x000437C0
		public void WriteStartElement(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.WriteStartElement(null, localName, namespaceUri);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x000455CC File Offset: 0x000437CC
		internal void WriteStartElement(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			if (this.dictionaryWriter != null)
			{
				this.dictionaryWriter.WriteStartElement(prefix, localName, namespaceUri);
			}
			else
			{
				this.writer.WriteStartElement(prefix, (localName == null) ? null : localName.Value, (namespaceUri == null) ? null : namespaceUri.Value);
			}
			this.depth++;
			this.prefixes = 1;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0004562A File Offset: 0x0004382A
		internal void WriteStartElementPrimitive(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			if (this.dictionaryWriter != null)
			{
				this.dictionaryWriter.WriteStartElement(null, localName, namespaceUri);
				return;
			}
			this.writer.WriteStartElement(null, (localName == null) ? null : localName.Value, (namespaceUri == null) ? null : namespaceUri.Value);
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00045667 File Offset: 0x00043867
		internal void WriteEndElementPrimitive()
		{
			this.writer.WriteEndElement();
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x00045674 File Offset: 0x00043874
		internal WriteState WriteState
		{
			get
			{
				return this.writer.WriteState;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x00045681 File Offset: 0x00043881
		internal string XmlLang
		{
			get
			{
				return this.writer.XmlLang;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x0004568E File Offset: 0x0004388E
		internal XmlSpace XmlSpace
		{
			get
			{
				return this.writer.XmlSpace;
			}
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0004569B File Offset: 0x0004389B
		public void WriteNamespaceDecl(XmlDictionaryString ns)
		{
			this.WriteXmlnsAttribute(ns);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x000456A4 File Offset: 0x000438A4
		private Exception CreateInvalidPrimitiveTypeException(Type type)
		{
			return new InvalidDataContractException(SR.GetString("Type '{0}' is not a valid serializable type.", new object[]
			{
				DataContract.GetClrTypeFullName(type)
			}));
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x000456C4 File Offset: 0x000438C4
		internal void WriteAnyType(object value)
		{
			this.WriteAnyType(value, value.GetType());
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x000456D4 File Offset: 0x000438D4
		internal void WriteAnyType(object value, Type valueType)
		{
			bool flag = true;
			switch (Type.GetTypeCode(valueType))
			{
			case TypeCode.Boolean:
				this.WriteBoolean((bool)value);
				goto IL_1F5;
			case TypeCode.Char:
				this.WriteChar((char)value);
				goto IL_1F5;
			case TypeCode.SByte:
				this.WriteSignedByte((sbyte)value);
				goto IL_1F5;
			case TypeCode.Byte:
				this.WriteUnsignedByte((byte)value);
				goto IL_1F5;
			case TypeCode.Int16:
				this.WriteShort((short)value);
				goto IL_1F5;
			case TypeCode.UInt16:
				this.WriteUnsignedShort((ushort)value);
				goto IL_1F5;
			case TypeCode.Int32:
				this.WriteInt((int)value);
				goto IL_1F5;
			case TypeCode.UInt32:
				this.WriteUnsignedInt((uint)value);
				goto IL_1F5;
			case TypeCode.Int64:
				this.WriteLong((long)value);
				goto IL_1F5;
			case TypeCode.UInt64:
				this.WriteUnsignedLong((ulong)value);
				goto IL_1F5;
			case TypeCode.Single:
				this.WriteFloat((float)value);
				goto IL_1F5;
			case TypeCode.Double:
				this.WriteDouble((double)value);
				goto IL_1F5;
			case TypeCode.Decimal:
				this.WriteDecimal((decimal)value);
				goto IL_1F5;
			case TypeCode.DateTime:
				this.WriteDateTime((DateTime)value);
				goto IL_1F5;
			case TypeCode.String:
				this.WriteString((string)value);
				goto IL_1F5;
			}
			if (valueType == Globals.TypeOfByteArray)
			{
				this.WriteBase64((byte[])value);
			}
			else if (!(valueType == Globals.TypeOfObject))
			{
				if (valueType == Globals.TypeOfTimeSpan)
				{
					this.WriteTimeSpan((TimeSpan)value);
				}
				else if (valueType == Globals.TypeOfGuid)
				{
					this.WriteGuid((Guid)value);
				}
				else if (valueType == Globals.TypeOfUri)
				{
					this.WriteUri((Uri)value);
				}
				else if (valueType == Globals.TypeOfXmlQualifiedName)
				{
					this.WriteQName((XmlQualifiedName)value);
				}
				else
				{
					flag = false;
				}
			}
			IL_1F5:
			if (!flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateInvalidPrimitiveTypeException(valueType));
			}
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x000458E8 File Offset: 0x00043AE8
		internal void WriteExtensionData(IDataNode dataNode)
		{
			bool flag = true;
			Type dataType = dataNode.DataType;
			switch (Type.GetTypeCode(dataType))
			{
			case TypeCode.Boolean:
				this.WriteBoolean(((DataNode<bool>)dataNode).GetValue());
				goto IL_27C;
			case TypeCode.Char:
				this.WriteChar(((DataNode<char>)dataNode).GetValue());
				goto IL_27C;
			case TypeCode.SByte:
				this.WriteSignedByte(((DataNode<sbyte>)dataNode).GetValue());
				goto IL_27C;
			case TypeCode.Byte:
				this.WriteUnsignedByte(((DataNode<byte>)dataNode).GetValue());
				goto IL_27C;
			case TypeCode.Int16:
				this.WriteShort(((DataNode<short>)dataNode).GetValue());
				goto IL_27C;
			case TypeCode.UInt16:
				this.WriteUnsignedShort(((DataNode<ushort>)dataNode).GetValue());
				goto IL_27C;
			case TypeCode.Int32:
				this.WriteInt(((DataNode<int>)dataNode).GetValue());
				goto IL_27C;
			case TypeCode.UInt32:
				this.WriteUnsignedInt(((DataNode<uint>)dataNode).GetValue());
				goto IL_27C;
			case TypeCode.Int64:
				this.WriteLong(((DataNode<long>)dataNode).GetValue());
				goto IL_27C;
			case TypeCode.UInt64:
				this.WriteUnsignedLong(((DataNode<ulong>)dataNode).GetValue());
				goto IL_27C;
			case TypeCode.Single:
				this.WriteFloat(((DataNode<float>)dataNode).GetValue());
				goto IL_27C;
			case TypeCode.Double:
				this.WriteDouble(((DataNode<double>)dataNode).GetValue());
				goto IL_27C;
			case TypeCode.Decimal:
				this.WriteDecimal(((DataNode<decimal>)dataNode).GetValue());
				goto IL_27C;
			case TypeCode.DateTime:
				this.WriteDateTime(((DataNode<DateTime>)dataNode).GetValue());
				goto IL_27C;
			case TypeCode.String:
				this.WriteString(((DataNode<string>)dataNode).GetValue());
				goto IL_27C;
			}
			if (dataType == Globals.TypeOfByteArray)
			{
				this.WriteBase64(((DataNode<byte[]>)dataNode).GetValue());
			}
			else if (dataType == Globals.TypeOfObject)
			{
				object value = dataNode.Value;
				if (value != null)
				{
					this.WriteAnyType(value);
				}
			}
			else if (dataType == Globals.TypeOfTimeSpan)
			{
				this.WriteTimeSpan(((DataNode<TimeSpan>)dataNode).GetValue());
			}
			else if (dataType == Globals.TypeOfGuid)
			{
				this.WriteGuid(((DataNode<Guid>)dataNode).GetValue());
			}
			else if (dataType == Globals.TypeOfUri)
			{
				this.WriteUri(((DataNode<Uri>)dataNode).GetValue());
			}
			else if (dataType == Globals.TypeOfXmlQualifiedName)
			{
				this.WriteQName(((DataNode<XmlQualifiedName>)dataNode).GetValue());
			}
			else
			{
				flag = false;
			}
			IL_27C:
			if (!flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateInvalidPrimitiveTypeException(dataType));
			}
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0004546F File Offset: 0x0004366F
		internal void WriteString(string value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x000454FE File Offset: 0x000436FE
		internal virtual void WriteBoolean(bool value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00045B81 File Offset: 0x00043D81
		public void WriteBoolean(bool value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			this.WriteStartElementPrimitive(name, ns);
			this.WriteBoolean(value);
			this.WriteEndElementPrimitive();
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00045B98 File Offset: 0x00043D98
		internal virtual void WriteDateTime(DateTime value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00045BA6 File Offset: 0x00043DA6
		public void WriteDateTime(DateTime value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			this.WriteStartElementPrimitive(name, ns);
			this.WriteDateTime(value);
			this.WriteEndElementPrimitive();
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00045BBD File Offset: 0x00043DBD
		internal virtual void WriteDecimal(decimal value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00045BCB File Offset: 0x00043DCB
		public void WriteDecimal(decimal value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			this.WriteStartElementPrimitive(name, ns);
			this.WriteDecimal(value);
			this.WriteEndElementPrimitive();
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00045BE2 File Offset: 0x00043DE2
		internal virtual void WriteDouble(double value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x00045BF0 File Offset: 0x00043DF0
		public void WriteDouble(double value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			this.WriteStartElementPrimitive(name, ns);
			this.WriteDouble(value);
			this.WriteEndElementPrimitive();
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x000454D7 File Offset: 0x000436D7
		internal virtual void WriteInt(int value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00045C07 File Offset: 0x00043E07
		public void WriteInt(int value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			this.WriteStartElementPrimitive(name, ns);
			this.WriteInt(value);
			this.WriteEndElementPrimitive();
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00045C1E File Offset: 0x00043E1E
		internal virtual void WriteLong(long value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00045C2C File Offset: 0x00043E2C
		public void WriteLong(long value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			this.WriteStartElementPrimitive(name, ns);
			this.WriteLong(value);
			this.WriteEndElementPrimitive();
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x00045C43 File Offset: 0x00043E43
		internal virtual void WriteFloat(float value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x00045C51 File Offset: 0x00043E51
		public void WriteFloat(float value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			this.WriteStartElementPrimitive(name, ns);
			this.WriteFloat(value);
			this.WriteEndElementPrimitive();
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00045C68 File Offset: 0x00043E68
		internal virtual void WriteBase64(byte[] bytes)
		{
			if (bytes == null)
			{
				return;
			}
			this.writer.WriteBase64(bytes, 0, bytes.Length);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x000454D7 File Offset: 0x000436D7
		internal virtual void WriteShort(short value)
		{
			this.writer.WriteValue((int)value);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00045C7E File Offset: 0x00043E7E
		public void WriteShort(short value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			this.WriteStartElementPrimitive(name, ns);
			this.WriteShort(value);
			this.WriteEndElementPrimitive();
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x000454D7 File Offset: 0x000436D7
		internal virtual void WriteUnsignedByte(byte value)
		{
			this.writer.WriteValue((int)value);
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00045C95 File Offset: 0x00043E95
		public void WriteUnsignedByte(byte value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			this.WriteStartElementPrimitive(name, ns);
			this.WriteUnsignedByte(value);
			this.WriteEndElementPrimitive();
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x000454D7 File Offset: 0x000436D7
		internal virtual void WriteSignedByte(sbyte value)
		{
			this.writer.WriteValue((int)value);
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00045CAC File Offset: 0x00043EAC
		public void WriteSignedByte(sbyte value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			this.WriteStartElementPrimitive(name, ns);
			this.WriteSignedByte(value);
			this.WriteEndElementPrimitive();
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x00045CC3 File Offset: 0x00043EC3
		internal virtual void WriteUnsignedInt(uint value)
		{
			this.writer.WriteValue((long)((ulong)value));
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00045CD2 File Offset: 0x00043ED2
		public void WriteUnsignedInt(uint value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			this.WriteStartElementPrimitive(name, ns);
			this.WriteUnsignedInt(value);
			this.WriteEndElementPrimitive();
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00045CE9 File Offset: 0x00043EE9
		internal virtual void WriteUnsignedLong(ulong value)
		{
			this.writer.WriteRaw(XmlConvert.ToString(value));
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00045CFC File Offset: 0x00043EFC
		public void WriteUnsignedLong(ulong value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			this.WriteStartElementPrimitive(name, ns);
			this.WriteUnsignedLong(value);
			this.WriteEndElementPrimitive();
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x000454D7 File Offset: 0x000436D7
		internal virtual void WriteUnsignedShort(ushort value)
		{
			this.writer.WriteValue((int)value);
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00045D13 File Offset: 0x00043F13
		public void WriteUnsignedShort(ushort value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			this.WriteStartElementPrimitive(name, ns);
			this.WriteUnsignedShort(value);
			this.WriteEndElementPrimitive();
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x000454D7 File Offset: 0x000436D7
		internal virtual void WriteChar(char value)
		{
			this.writer.WriteValue((int)value);
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00045D2A File Offset: 0x00043F2A
		public void WriteChar(char value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			this.WriteStartElementPrimitive(name, ns);
			this.WriteChar(value);
			this.WriteEndElementPrimitive();
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00045D41 File Offset: 0x00043F41
		internal void WriteTimeSpan(TimeSpan value)
		{
			this.writer.WriteRaw(XmlConvert.ToString(value));
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00045D54 File Offset: 0x00043F54
		public void WriteTimeSpan(TimeSpan value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			this.WriteStartElementPrimitive(name, ns);
			this.WriteTimeSpan(value);
			this.WriteEndElementPrimitive();
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00045D6B File Offset: 0x00043F6B
		internal void WriteGuid(Guid value)
		{
			this.writer.WriteRaw(value.ToString());
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00045D85 File Offset: 0x00043F85
		public void WriteGuid(Guid value, XmlDictionaryString name, XmlDictionaryString ns)
		{
			this.WriteStartElementPrimitive(name, ns);
			this.WriteGuid(value);
			this.WriteEndElementPrimitive();
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00045D9C File Offset: 0x00043F9C
		internal void WriteUri(Uri value)
		{
			this.writer.WriteString(value.GetComponents(UriComponents.SerializationInfoString, UriFormat.UriEscaped));
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00045DB5 File Offset: 0x00043FB5
		internal virtual void WriteQName(XmlQualifiedName value)
		{
			if (value != XmlQualifiedName.Empty)
			{
				this.WriteXmlnsAttribute(value.Namespace);
				this.WriteQualifiedName(value.Name, value.Namespace);
			}
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0004552F File Offset: 0x0004372F
		internal void WriteQualifiedName(string localName, string ns)
		{
			this.writer.WriteQualifiedName(localName, ns);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00045561 File Offset: 0x00043761
		internal void WriteQualifiedName(XmlDictionaryString localName, XmlDictionaryString ns)
		{
			if (this.dictionaryWriter == null)
			{
				this.writer.WriteQualifiedName(localName.Value, ns.Value);
				return;
			}
			this.dictionaryWriter.WriteQualifiedName(localName, ns);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00045DE4 File Offset: 0x00043FE4
		public void WriteBooleanArray(bool[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
		{
			if (this.dictionaryWriter == null)
			{
				for (int i = 0; i < value.Length; i++)
				{
					this.WriteBoolean(value[i], itemName, itemNamespace);
				}
				return;
			}
			this.dictionaryWriter.WriteArray(null, itemName, itemNamespace, value, 0, value.Length);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00045E28 File Offset: 0x00044028
		public void WriteDateTimeArray(DateTime[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
		{
			if (this.dictionaryWriter == null)
			{
				for (int i = 0; i < value.Length; i++)
				{
					this.WriteDateTime(value[i], itemName, itemNamespace);
				}
				return;
			}
			this.dictionaryWriter.WriteArray(null, itemName, itemNamespace, value, 0, value.Length);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00045E70 File Offset: 0x00044070
		public void WriteDecimalArray(decimal[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
		{
			if (this.dictionaryWriter == null)
			{
				for (int i = 0; i < value.Length; i++)
				{
					this.WriteDecimal(value[i], itemName, itemNamespace);
				}
				return;
			}
			this.dictionaryWriter.WriteArray(null, itemName, itemNamespace, value, 0, value.Length);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00045EB8 File Offset: 0x000440B8
		public void WriteInt32Array(int[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
		{
			if (this.dictionaryWriter == null)
			{
				for (int i = 0; i < value.Length; i++)
				{
					this.WriteInt(value[i], itemName, itemNamespace);
				}
				return;
			}
			this.dictionaryWriter.WriteArray(null, itemName, itemNamespace, value, 0, value.Length);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00045EFC File Offset: 0x000440FC
		public void WriteInt64Array(long[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
		{
			if (this.dictionaryWriter == null)
			{
				for (int i = 0; i < value.Length; i++)
				{
					this.WriteLong(value[i], itemName, itemNamespace);
				}
				return;
			}
			this.dictionaryWriter.WriteArray(null, itemName, itemNamespace, value, 0, value.Length);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00045F40 File Offset: 0x00044140
		public void WriteSingleArray(float[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
		{
			if (this.dictionaryWriter == null)
			{
				for (int i = 0; i < value.Length; i++)
				{
					this.WriteFloat(value[i], itemName, itemNamespace);
				}
				return;
			}
			this.dictionaryWriter.WriteArray(null, itemName, itemNamespace, value, 0, value.Length);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00045F84 File Offset: 0x00044184
		public void WriteDoubleArray(double[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
		{
			if (this.dictionaryWriter == null)
			{
				for (int i = 0; i < value.Length; i++)
				{
					this.WriteDouble(value[i], itemName, itemNamespace);
				}
				return;
			}
			this.dictionaryWriter.WriteArray(null, itemName, itemNamespace, value, 0, value.Length);
		}

		// Token: 0x04000732 RID: 1842
		protected XmlWriter writer;

		// Token: 0x04000733 RID: 1843
		protected XmlDictionaryWriter dictionaryWriter;

		// Token: 0x04000734 RID: 1844
		internal int depth;

		// Token: 0x04000735 RID: 1845
		private int prefixes;

		// Token: 0x04000736 RID: 1846
		private const int CharChunkSize = 76;

		// Token: 0x04000737 RID: 1847
		private const int ByteChunkSize = 57;
	}
}
