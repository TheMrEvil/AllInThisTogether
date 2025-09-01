using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization.Configuration;

namespace System.Xml.Serialization
{
	/// <summary>Controls deserialization by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class. </summary>
	// Token: 0x020002EB RID: 747
	public abstract class XmlSerializationReader : XmlSerializationGeneratedCode
	{
		// Token: 0x06001D8B RID: 7563 RVA: 0x000AD4E8 File Offset: 0x000AB6E8
		static XmlSerializationReader()
		{
			XmlSerializerSection xmlSerializerSection = ConfigurationManager.GetSection(ConfigurationStrings.XmlSerializerSectionPath) as XmlSerializerSection;
			XmlSerializationReader.checkDeserializeAdvances = (xmlSerializerSection != null && xmlSerializerSection.CheckDeserializeAdvances);
		}

		/// <summary>Stores element and attribute names in a <see cref="T:System.Xml.NameTable" /> object. </summary>
		// Token: 0x06001D8C RID: 7564
		protected abstract void InitIDs();

		// Token: 0x06001D8D RID: 7565 RVA: 0x000AD518 File Offset: 0x000AB718
		internal void Init(XmlReader r, XmlDeserializationEvents events, string encodingStyle, TempAssembly tempAssembly)
		{
			this.events = events;
			if (XmlSerializationReader.checkDeserializeAdvances)
			{
				this.countingReader = new XmlCountingReader(r);
				this.r = this.countingReader;
			}
			else
			{
				this.r = r;
			}
			this.d = null;
			this.soap12 = (encodingStyle == "http://www.w3.org/2003/05/soap-encoding");
			base.Init(tempAssembly);
			this.schemaNsID = r.NameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.schemaNs2000ID = r.NameTable.Add("http://www.w3.org/2000/10/XMLSchema");
			this.schemaNs1999ID = r.NameTable.Add("http://www.w3.org/1999/XMLSchema");
			this.schemaNonXsdTypesNsID = r.NameTable.Add("http://microsoft.com/wsdl/types/");
			this.instanceNsID = r.NameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.instanceNs2000ID = r.NameTable.Add("http://www.w3.org/2000/10/XMLSchema-instance");
			this.instanceNs1999ID = r.NameTable.Add("http://www.w3.org/1999/XMLSchema-instance");
			this.soapNsID = r.NameTable.Add("http://schemas.xmlsoap.org/soap/encoding/");
			this.soap12NsID = r.NameTable.Add("http://www.w3.org/2003/05/soap-encoding");
			this.schemaID = r.NameTable.Add("schema");
			this.wsdlNsID = r.NameTable.Add("http://schemas.xmlsoap.org/wsdl/");
			this.wsdlArrayTypeID = r.NameTable.Add("arrayType");
			this.nullID = r.NameTable.Add("null");
			this.nilID = r.NameTable.Add("nil");
			this.typeID = r.NameTable.Add("type");
			this.arrayTypeID = r.NameTable.Add("arrayType");
			this.itemTypeID = r.NameTable.Add("itemType");
			this.arraySizeID = r.NameTable.Add("arraySize");
			this.arrayID = r.NameTable.Add("Array");
			this.urTypeID = r.NameTable.Add("anyType");
			this.InitIDs();
		}

		/// <summary>Gets or sets a value that determines whether XML strings are translated into valid .NET Framework type names.</summary>
		/// <returns>
		///     <see langword="true" /> if XML strings are decoded into valid .NET Framework type names; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x000AD732 File Offset: 0x000AB932
		// (set) Token: 0x06001D8F RID: 7567 RVA: 0x000AD73A File Offset: 0x000AB93A
		protected bool DecodeName
		{
			get
			{
				return this.decodeName;
			}
			set
			{
				this.decodeName = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlReader" /> object that is being used by <see cref="T:System.Xml.Serialization.XmlSerializationReader" />. </summary>
		/// <returns>The <see cref="T:System.Xml.XmlReader" /> that is being used by the <see cref="T:System.Xml.Serialization.XmlSerializationReader" />.</returns>
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x000AD743 File Offset: 0x000AB943
		protected XmlReader Reader
		{
			get
			{
				return this.r;
			}
		}

		/// <summary>Gets the current count of the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>The current count of an <see cref="T:System.Xml.XmlReader" />.</returns>
		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x000AD74B File Offset: 0x000AB94B
		protected int ReaderCount
		{
			get
			{
				if (!XmlSerializationReader.checkDeserializeAdvances)
				{
					return 0;
				}
				return this.countingReader.AdvanceCount;
			}
		}

		/// <summary>Gets the XML document object into which the XML document is being deserialized. </summary>
		/// <returns>An <see cref="T:System.Xml.XmlDocument" /> that represents the deserialized <see cref="T:System.Xml.XmlDocument" /> data.</returns>
		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001D92 RID: 7570 RVA: 0x000AD761 File Offset: 0x000AB961
		protected XmlDocument Document
		{
			get
			{
				if (this.d == null)
				{
					this.d = new XmlDocument(this.r.NameTable);
					this.d.SetBaseURI(this.r.BaseURI);
				}
				return this.d;
			}
		}

		/// <summary>Gets a dynamically generated assembly by name.</summary>
		/// <param name="assemblyFullName">The full name of the assembly.</param>
		/// <returns>A dynamically generated <see cref="T:System.Reflection.Assembly" />.</returns>
		// Token: 0x06001D93 RID: 7571 RVA: 0x000AD79D File Offset: 0x000AB99D
		protected static Assembly ResolveDynamicAssembly(string assemblyFullName)
		{
			return DynamicAssemblies.Get(assemblyFullName);
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x000AD7A8 File Offset: 0x000AB9A8
		private void InitPrimitiveIDs()
		{
			if (this.tokenID != null)
			{
				return;
			}
			this.r.NameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.r.NameTable.Add("http://microsoft.com/wsdl/types/");
			this.stringID = this.r.NameTable.Add("string");
			this.intID = this.r.NameTable.Add("int");
			this.booleanID = this.r.NameTable.Add("boolean");
			this.shortID = this.r.NameTable.Add("short");
			this.longID = this.r.NameTable.Add("long");
			this.floatID = this.r.NameTable.Add("float");
			this.doubleID = this.r.NameTable.Add("double");
			this.decimalID = this.r.NameTable.Add("decimal");
			this.dateTimeID = this.r.NameTable.Add("dateTime");
			this.qnameID = this.r.NameTable.Add("QName");
			this.dateID = this.r.NameTable.Add("date");
			this.timeID = this.r.NameTable.Add("time");
			this.hexBinaryID = this.r.NameTable.Add("hexBinary");
			this.base64BinaryID = this.r.NameTable.Add("base64Binary");
			this.unsignedByteID = this.r.NameTable.Add("unsignedByte");
			this.byteID = this.r.NameTable.Add("byte");
			this.unsignedShortID = this.r.NameTable.Add("unsignedShort");
			this.unsignedIntID = this.r.NameTable.Add("unsignedInt");
			this.unsignedLongID = this.r.NameTable.Add("unsignedLong");
			this.oldDecimalID = this.r.NameTable.Add("decimal");
			this.oldTimeInstantID = this.r.NameTable.Add("timeInstant");
			this.charID = this.r.NameTable.Add("char");
			this.guidID = this.r.NameTable.Add("guid");
			if (LocalAppContextSwitches.EnableTimeSpanSerialization)
			{
				this.timeSpanID = this.r.NameTable.Add("TimeSpan");
			}
			this.base64ID = this.r.NameTable.Add("base64");
			this.anyURIID = this.r.NameTable.Add("anyURI");
			this.durationID = this.r.NameTable.Add("duration");
			this.ENTITYID = this.r.NameTable.Add("ENTITY");
			this.ENTITIESID = this.r.NameTable.Add("ENTITIES");
			this.gDayID = this.r.NameTable.Add("gDay");
			this.gMonthID = this.r.NameTable.Add("gMonth");
			this.gMonthDayID = this.r.NameTable.Add("gMonthDay");
			this.gYearID = this.r.NameTable.Add("gYear");
			this.gYearMonthID = this.r.NameTable.Add("gYearMonth");
			this.IDID = this.r.NameTable.Add("ID");
			this.IDREFID = this.r.NameTable.Add("IDREF");
			this.IDREFSID = this.r.NameTable.Add("IDREFS");
			this.integerID = this.r.NameTable.Add("integer");
			this.languageID = this.r.NameTable.Add("language");
			this.NameID = this.r.NameTable.Add("Name");
			this.NCNameID = this.r.NameTable.Add("NCName");
			this.NMTOKENID = this.r.NameTable.Add("NMTOKEN");
			this.NMTOKENSID = this.r.NameTable.Add("NMTOKENS");
			this.negativeIntegerID = this.r.NameTable.Add("negativeInteger");
			this.nonNegativeIntegerID = this.r.NameTable.Add("nonNegativeInteger");
			this.nonPositiveIntegerID = this.r.NameTable.Add("nonPositiveInteger");
			this.normalizedStringID = this.r.NameTable.Add("normalizedString");
			this.NOTATIONID = this.r.NameTable.Add("NOTATION");
			this.positiveIntegerID = this.r.NameTable.Add("positiveInteger");
			this.tokenID = this.r.NameTable.Add("token");
		}

		/// <summary>Gets the value of the <see langword="xsi:type" /> attribute for the XML element at the current location of the <see cref="T:System.Xml.XmlReader" />. </summary>
		/// <returns>An XML qualified name that indicates the data type of an XML element.</returns>
		// Token: 0x06001D95 RID: 7573 RVA: 0x000ADD38 File Offset: 0x000ABF38
		protected XmlQualifiedName GetXsiType()
		{
			string attribute = this.r.GetAttribute(this.typeID, this.instanceNsID);
			if (attribute == null)
			{
				attribute = this.r.GetAttribute(this.typeID, this.instanceNs2000ID);
				if (attribute == null)
				{
					attribute = this.r.GetAttribute(this.typeID, this.instanceNs1999ID);
					if (attribute == null)
					{
						return null;
					}
				}
			}
			return this.ToXmlQualifiedName(attribute, false);
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x000ADDA0 File Offset: 0x000ABFA0
		private Type GetPrimitiveType(XmlQualifiedName typeName, bool throwOnUnknown)
		{
			this.InitPrimitiveIDs();
			if (typeName.Namespace == this.schemaNsID || typeName.Namespace == this.soapNsID || typeName.Namespace == this.soap12NsID)
			{
				if (typeName.Name == this.stringID || typeName.Name == this.anyURIID || typeName.Name == this.durationID || typeName.Name == this.ENTITYID || typeName.Name == this.ENTITIESID || typeName.Name == this.gDayID || typeName.Name == this.gMonthID || typeName.Name == this.gMonthDayID || typeName.Name == this.gYearID || typeName.Name == this.gYearMonthID || typeName.Name == this.IDID || typeName.Name == this.IDREFID || typeName.Name == this.IDREFSID || typeName.Name == this.integerID || typeName.Name == this.languageID || typeName.Name == this.NameID || typeName.Name == this.NCNameID || typeName.Name == this.NMTOKENID || typeName.Name == this.NMTOKENSID || typeName.Name == this.negativeIntegerID || typeName.Name == this.nonPositiveIntegerID || typeName.Name == this.nonNegativeIntegerID || typeName.Name == this.normalizedStringID || typeName.Name == this.NOTATIONID || typeName.Name == this.positiveIntegerID || typeName.Name == this.tokenID)
				{
					return typeof(string);
				}
				if (typeName.Name == this.intID)
				{
					return typeof(int);
				}
				if (typeName.Name == this.booleanID)
				{
					return typeof(bool);
				}
				if (typeName.Name == this.shortID)
				{
					return typeof(short);
				}
				if (typeName.Name == this.longID)
				{
					return typeof(long);
				}
				if (typeName.Name == this.floatID)
				{
					return typeof(float);
				}
				if (typeName.Name == this.doubleID)
				{
					return typeof(double);
				}
				if (typeName.Name == this.decimalID)
				{
					return typeof(decimal);
				}
				if (typeName.Name == this.dateTimeID)
				{
					return typeof(DateTime);
				}
				if (typeName.Name == this.qnameID)
				{
					return typeof(XmlQualifiedName);
				}
				if (typeName.Name == this.dateID)
				{
					return typeof(DateTime);
				}
				if (typeName.Name == this.timeID)
				{
					return typeof(DateTime);
				}
				if (typeName.Name == this.hexBinaryID)
				{
					return typeof(byte[]);
				}
				if (typeName.Name == this.base64BinaryID)
				{
					return typeof(byte[]);
				}
				if (typeName.Name == this.unsignedByteID)
				{
					return typeof(byte);
				}
				if (typeName.Name == this.byteID)
				{
					return typeof(sbyte);
				}
				if (typeName.Name == this.unsignedShortID)
				{
					return typeof(ushort);
				}
				if (typeName.Name == this.unsignedIntID)
				{
					return typeof(uint);
				}
				if (typeName.Name == this.unsignedLongID)
				{
					return typeof(ulong);
				}
				throw this.CreateUnknownTypeException(typeName);
			}
			else if (typeName.Namespace == this.schemaNs2000ID || typeName.Namespace == this.schemaNs1999ID)
			{
				if (typeName.Name == this.stringID || typeName.Name == this.anyURIID || typeName.Name == this.durationID || typeName.Name == this.ENTITYID || typeName.Name == this.ENTITIESID || typeName.Name == this.gDayID || typeName.Name == this.gMonthID || typeName.Name == this.gMonthDayID || typeName.Name == this.gYearID || typeName.Name == this.gYearMonthID || typeName.Name == this.IDID || typeName.Name == this.IDREFID || typeName.Name == this.IDREFSID || typeName.Name == this.integerID || typeName.Name == this.languageID || typeName.Name == this.NameID || typeName.Name == this.NCNameID || typeName.Name == this.NMTOKENID || typeName.Name == this.NMTOKENSID || typeName.Name == this.negativeIntegerID || typeName.Name == this.nonPositiveIntegerID || typeName.Name == this.nonNegativeIntegerID || typeName.Name == this.normalizedStringID || typeName.Name == this.NOTATIONID || typeName.Name == this.positiveIntegerID || typeName.Name == this.tokenID)
				{
					return typeof(string);
				}
				if (typeName.Name == this.intID)
				{
					return typeof(int);
				}
				if (typeName.Name == this.booleanID)
				{
					return typeof(bool);
				}
				if (typeName.Name == this.shortID)
				{
					return typeof(short);
				}
				if (typeName.Name == this.longID)
				{
					return typeof(long);
				}
				if (typeName.Name == this.floatID)
				{
					return typeof(float);
				}
				if (typeName.Name == this.doubleID)
				{
					return typeof(double);
				}
				if (typeName.Name == this.oldDecimalID)
				{
					return typeof(decimal);
				}
				if (typeName.Name == this.oldTimeInstantID)
				{
					return typeof(DateTime);
				}
				if (typeName.Name == this.qnameID)
				{
					return typeof(XmlQualifiedName);
				}
				if (typeName.Name == this.dateID)
				{
					return typeof(DateTime);
				}
				if (typeName.Name == this.timeID)
				{
					return typeof(DateTime);
				}
				if (typeName.Name == this.hexBinaryID)
				{
					return typeof(byte[]);
				}
				if (typeName.Name == this.byteID)
				{
					return typeof(sbyte);
				}
				if (typeName.Name == this.unsignedShortID)
				{
					return typeof(ushort);
				}
				if (typeName.Name == this.unsignedIntID)
				{
					return typeof(uint);
				}
				if (typeName.Name == this.unsignedLongID)
				{
					return typeof(ulong);
				}
				throw this.CreateUnknownTypeException(typeName);
			}
			else if (typeName.Namespace == this.schemaNonXsdTypesNsID)
			{
				if (typeName.Name == this.charID)
				{
					return typeof(char);
				}
				if (typeName.Name == this.guidID)
				{
					return typeof(Guid);
				}
				throw this.CreateUnknownTypeException(typeName);
			}
			else
			{
				if (throwOnUnknown)
				{
					throw this.CreateUnknownTypeException(typeName);
				}
				return null;
			}
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x000AE503 File Offset: 0x000AC703
		private bool IsPrimitiveNamespace(string ns)
		{
			return ns == this.schemaNsID || ns == this.schemaNonXsdTypesNsID || ns == this.soapNsID || ns == this.soap12NsID || ns == this.schemaNs2000ID || ns == this.schemaNs1999ID;
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x000AE53D File Offset: 0x000AC73D
		private string ReadStringValue()
		{
			if (this.r.IsEmptyElement)
			{
				this.r.Skip();
				return string.Empty;
			}
			this.r.ReadStartElement();
			string result = this.r.ReadString();
			this.ReadEndElement();
			return result;
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x000AE57C File Offset: 0x000AC77C
		private XmlQualifiedName ReadXmlQualifiedName()
		{
			bool flag = false;
			string value;
			if (this.r.IsEmptyElement)
			{
				value = string.Empty;
				flag = true;
			}
			else
			{
				this.r.ReadStartElement();
				value = this.r.ReadString();
			}
			XmlQualifiedName result = this.ToXmlQualifiedName(value);
			if (flag)
			{
				this.r.Skip();
				return result;
			}
			this.ReadEndElement();
			return result;
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x000AE5D8 File Offset: 0x000AC7D8
		private byte[] ReadByteArray(bool isBase64)
		{
			ArrayList arrayList = new ArrayList();
			int num = 1024;
			int num2 = -1;
			int num3 = 0;
			int num4 = 0;
			byte[] array = new byte[num];
			arrayList.Add(array);
			while (num2 != 0)
			{
				if (num3 == array.Length)
				{
					num = Math.Min(num * 2, 65536);
					array = new byte[num];
					num3 = 0;
					arrayList.Add(array);
				}
				if (isBase64)
				{
					num2 = this.r.ReadElementContentAsBase64(array, num3, array.Length - num3);
				}
				else
				{
					num2 = this.r.ReadElementContentAsBinHex(array, num3, array.Length - num3);
				}
				num3 += num2;
				num4 += num2;
			}
			byte[] array2 = new byte[num4];
			num3 = 0;
			foreach (object obj in arrayList)
			{
				byte[] array3 = (byte[])obj;
				num = Math.Min(array3.Length, num4);
				if (num > 0)
				{
					Buffer.BlockCopy(array3, 0, array2, num3, num);
					num3 += num;
					num4 -= num;
				}
			}
			arrayList.Clear();
			return array2;
		}

		/// <summary>Gets the value of the XML node at which the <see cref="T:System.Xml.XmlReader" /> is currently positioned. </summary>
		/// <param name="type">The <see cref="T:System.Xml.XmlQualifiedName" /> that represents the simple data type for the current location of the <see cref="T:System.Xml.XmlReader" />.</param>
		/// <returns>The value of the node as a .NET Framework value type, if the value is a simple XML Schema data type.</returns>
		// Token: 0x06001D9B RID: 7579 RVA: 0x000AE6F4 File Offset: 0x000AC8F4
		protected object ReadTypedPrimitive(XmlQualifiedName type)
		{
			return this.ReadTypedPrimitive(type, false);
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x000AE700 File Offset: 0x000AC900
		private object ReadTypedPrimitive(XmlQualifiedName type, bool elementCanBeType)
		{
			this.InitPrimitiveIDs();
			if (!this.IsPrimitiveNamespace(type.Namespace) || type.Name == this.urTypeID)
			{
				return this.ReadXmlNodes(elementCanBeType);
			}
			object result;
			if (type.Namespace == this.schemaNsID || type.Namespace == this.soapNsID || type.Namespace == this.soap12NsID)
			{
				if (type.Name == this.stringID || type.Name == this.normalizedStringID)
				{
					result = this.ReadStringValue();
				}
				else if (type.Name == this.anyURIID || type.Name == this.durationID || type.Name == this.ENTITYID || type.Name == this.ENTITIESID || type.Name == this.gDayID || type.Name == this.gMonthID || type.Name == this.gMonthDayID || type.Name == this.gYearID || type.Name == this.gYearMonthID || type.Name == this.IDID || type.Name == this.IDREFID || type.Name == this.IDREFSID || type.Name == this.integerID || type.Name == this.languageID || type.Name == this.NameID || type.Name == this.NCNameID || type.Name == this.NMTOKENID || type.Name == this.NMTOKENSID || type.Name == this.negativeIntegerID || type.Name == this.nonPositiveIntegerID || type.Name == this.nonNegativeIntegerID || type.Name == this.NOTATIONID || type.Name == this.positiveIntegerID || type.Name == this.tokenID)
				{
					result = this.CollapseWhitespace(this.ReadStringValue());
				}
				else if (type.Name == this.intID)
				{
					result = XmlConvert.ToInt32(this.ReadStringValue());
				}
				else if (type.Name == this.booleanID)
				{
					result = XmlConvert.ToBoolean(this.ReadStringValue());
				}
				else if (type.Name == this.shortID)
				{
					result = XmlConvert.ToInt16(this.ReadStringValue());
				}
				else if (type.Name == this.longID)
				{
					result = XmlConvert.ToInt64(this.ReadStringValue());
				}
				else if (type.Name == this.floatID)
				{
					result = XmlConvert.ToSingle(this.ReadStringValue());
				}
				else if (type.Name == this.doubleID)
				{
					result = XmlConvert.ToDouble(this.ReadStringValue());
				}
				else if (type.Name == this.decimalID)
				{
					result = XmlConvert.ToDecimal(this.ReadStringValue());
				}
				else if (type.Name == this.dateTimeID)
				{
					result = XmlSerializationReader.ToDateTime(this.ReadStringValue());
				}
				else if (type.Name == this.qnameID)
				{
					result = this.ReadXmlQualifiedName();
				}
				else if (type.Name == this.dateID)
				{
					result = XmlSerializationReader.ToDate(this.ReadStringValue());
				}
				else if (type.Name == this.timeID)
				{
					result = XmlSerializationReader.ToTime(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedByteID)
				{
					result = XmlConvert.ToByte(this.ReadStringValue());
				}
				else if (type.Name == this.byteID)
				{
					result = XmlConvert.ToSByte(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedShortID)
				{
					result = XmlConvert.ToUInt16(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedIntID)
				{
					result = XmlConvert.ToUInt32(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedLongID)
				{
					result = XmlConvert.ToUInt64(this.ReadStringValue());
				}
				else if (type.Name == this.hexBinaryID)
				{
					result = this.ToByteArrayHex(false);
				}
				else if (type.Name == this.base64BinaryID)
				{
					result = this.ToByteArrayBase64(false);
				}
				else if (type.Name == this.base64ID && (type.Namespace == this.soapNsID || type.Namespace == this.soap12NsID))
				{
					result = this.ToByteArrayBase64(false);
				}
				else
				{
					result = this.ReadXmlNodes(elementCanBeType);
				}
			}
			else if (type.Namespace == this.schemaNs2000ID || type.Namespace == this.schemaNs1999ID)
			{
				if (type.Name == this.stringID || type.Name == this.normalizedStringID)
				{
					result = this.ReadStringValue();
				}
				else if (type.Name == this.anyURIID || type.Name == this.anyURIID || type.Name == this.durationID || type.Name == this.ENTITYID || type.Name == this.ENTITIESID || type.Name == this.gDayID || type.Name == this.gMonthID || type.Name == this.gMonthDayID || type.Name == this.gYearID || type.Name == this.gYearMonthID || type.Name == this.IDID || type.Name == this.IDREFID || type.Name == this.IDREFSID || type.Name == this.integerID || type.Name == this.languageID || type.Name == this.NameID || type.Name == this.NCNameID || type.Name == this.NMTOKENID || type.Name == this.NMTOKENSID || type.Name == this.negativeIntegerID || type.Name == this.nonPositiveIntegerID || type.Name == this.nonNegativeIntegerID || type.Name == this.NOTATIONID || type.Name == this.positiveIntegerID || type.Name == this.tokenID)
				{
					result = this.CollapseWhitespace(this.ReadStringValue());
				}
				else if (type.Name == this.intID)
				{
					result = XmlConvert.ToInt32(this.ReadStringValue());
				}
				else if (type.Name == this.booleanID)
				{
					result = XmlConvert.ToBoolean(this.ReadStringValue());
				}
				else if (type.Name == this.shortID)
				{
					result = XmlConvert.ToInt16(this.ReadStringValue());
				}
				else if (type.Name == this.longID)
				{
					result = XmlConvert.ToInt64(this.ReadStringValue());
				}
				else if (type.Name == this.floatID)
				{
					result = XmlConvert.ToSingle(this.ReadStringValue());
				}
				else if (type.Name == this.doubleID)
				{
					result = XmlConvert.ToDouble(this.ReadStringValue());
				}
				else if (type.Name == this.oldDecimalID)
				{
					result = XmlConvert.ToDecimal(this.ReadStringValue());
				}
				else if (type.Name == this.oldTimeInstantID)
				{
					result = XmlSerializationReader.ToDateTime(this.ReadStringValue());
				}
				else if (type.Name == this.qnameID)
				{
					result = this.ReadXmlQualifiedName();
				}
				else if (type.Name == this.dateID)
				{
					result = XmlSerializationReader.ToDate(this.ReadStringValue());
				}
				else if (type.Name == this.timeID)
				{
					result = XmlSerializationReader.ToTime(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedByteID)
				{
					result = XmlConvert.ToByte(this.ReadStringValue());
				}
				else if (type.Name == this.byteID)
				{
					result = XmlConvert.ToSByte(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedShortID)
				{
					result = XmlConvert.ToUInt16(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedIntID)
				{
					result = XmlConvert.ToUInt32(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedLongID)
				{
					result = XmlConvert.ToUInt64(this.ReadStringValue());
				}
				else
				{
					result = this.ReadXmlNodes(elementCanBeType);
				}
			}
			else if (type.Namespace == this.schemaNonXsdTypesNsID)
			{
				if (type.Name == this.charID)
				{
					result = XmlSerializationReader.ToChar(this.ReadStringValue());
				}
				else if (type.Name == this.guidID)
				{
					result = new Guid(this.CollapseWhitespace(this.ReadStringValue()));
				}
				else if (type.Name == this.timeSpanID && LocalAppContextSwitches.EnableTimeSpanSerialization)
				{
					result = XmlConvert.ToTimeSpan(this.ReadStringValue());
				}
				else
				{
					result = this.ReadXmlNodes(elementCanBeType);
				}
			}
			else
			{
				result = this.ReadXmlNodes(elementCanBeType);
			}
			return result;
		}

		/// <summary>Reads an XML element that allows null values (<see langword="xsi:nil = 'true'" />) and returns a generic <see cref="T:System.Nullable`1" /> value. </summary>
		/// <param name="type">The <see cref="T:System.Xml.XmlQualifiedName" /> that represents the simple data type for the current location of the <see cref="T:System.Xml.XmlReader" />.</param>
		/// <returns>A generic <see cref="T:System.Nullable`1" /> that represents a null XML value.</returns>
		// Token: 0x06001D9D RID: 7581 RVA: 0x000AF084 File Offset: 0x000AD284
		protected object ReadTypedNull(XmlQualifiedName type)
		{
			this.InitPrimitiveIDs();
			if (!this.IsPrimitiveNamespace(type.Namespace) || type.Name == this.urTypeID)
			{
				return null;
			}
			object result;
			if (type.Namespace == this.schemaNsID || type.Namespace == this.soapNsID || type.Namespace == this.soap12NsID)
			{
				if (type.Name == this.stringID || type.Name == this.anyURIID || type.Name == this.durationID || type.Name == this.ENTITYID || type.Name == this.ENTITIESID || type.Name == this.gDayID || type.Name == this.gMonthID || type.Name == this.gMonthDayID || type.Name == this.gYearID || type.Name == this.gYearMonthID || type.Name == this.IDID || type.Name == this.IDREFID || type.Name == this.IDREFSID || type.Name == this.integerID || type.Name == this.languageID || type.Name == this.NameID || type.Name == this.NCNameID || type.Name == this.NMTOKENID || type.Name == this.NMTOKENSID || type.Name == this.negativeIntegerID || type.Name == this.nonPositiveIntegerID || type.Name == this.nonNegativeIntegerID || type.Name == this.normalizedStringID || type.Name == this.NOTATIONID || type.Name == this.positiveIntegerID || type.Name == this.tokenID)
				{
					result = null;
				}
				else if (type.Name == this.intID)
				{
					result = null;
				}
				else if (type.Name == this.booleanID)
				{
					result = null;
				}
				else if (type.Name == this.shortID)
				{
					result = null;
				}
				else if (type.Name == this.longID)
				{
					result = null;
				}
				else if (type.Name == this.floatID)
				{
					result = null;
				}
				else if (type.Name == this.doubleID)
				{
					result = null;
				}
				else if (type.Name == this.decimalID)
				{
					result = null;
				}
				else if (type.Name == this.dateTimeID)
				{
					result = null;
				}
				else if (type.Name == this.qnameID)
				{
					result = null;
				}
				else if (type.Name == this.dateID)
				{
					result = null;
				}
				else if (type.Name == this.timeID)
				{
					result = null;
				}
				else if (type.Name == this.unsignedByteID)
				{
					result = null;
				}
				else if (type.Name == this.byteID)
				{
					result = null;
				}
				else if (type.Name == this.unsignedShortID)
				{
					result = null;
				}
				else if (type.Name == this.unsignedIntID)
				{
					result = null;
				}
				else if (type.Name == this.unsignedLongID)
				{
					result = null;
				}
				else if (type.Name == this.hexBinaryID)
				{
					result = null;
				}
				else if (type.Name == this.base64BinaryID)
				{
					result = null;
				}
				else if (type.Name == this.base64ID && (type.Namespace == this.soapNsID || type.Namespace == this.soap12NsID))
				{
					result = null;
				}
				else
				{
					result = null;
				}
			}
			else if (type.Namespace == this.schemaNonXsdTypesNsID)
			{
				if (type.Name == this.charID)
				{
					result = null;
				}
				else if (type.Name == this.guidID)
				{
					result = null;
				}
				else if (type.Name == this.timeSpanID && LocalAppContextSwitches.EnableTimeSpanSerialization)
				{
					result = null;
				}
				else
				{
					result = null;
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		/// <summary>Determines whether an XML attribute name indicates an XML namespace. </summary>
		/// <param name="name">The name of an XML attribute.</param>
		/// <returns>
		///     <see langword="true " />if the XML attribute name indicates an XML namespace; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D9E RID: 7582 RVA: 0x000AF485 File Offset: 0x000AD685
		protected bool IsXmlnsAttribute(string name)
		{
			return name.StartsWith("xmlns", StringComparison.Ordinal) && (name.Length == 5 || name[5] == ':');
		}

		/// <summary>Sets the value of the XML attribute if it is of type <see langword="arrayType" /> from the Web Services Description Language (WSDL) namespace. </summary>
		/// <param name="attr">An <see cref="T:System.Xml.XmlAttribute" /> that may have the type <see langword="wsdl:array" />.</param>
		// Token: 0x06001D9F RID: 7583 RVA: 0x000AF4B0 File Offset: 0x000AD6B0
		protected void ParseWsdlArrayType(XmlAttribute attr)
		{
			if (attr.LocalName == this.wsdlArrayTypeID && attr.NamespaceURI == this.wsdlNsID)
			{
				int num = attr.Value.LastIndexOf(':');
				if (num < 0)
				{
					attr.Value = this.r.LookupNamespace("") + ":" + attr.Value;
					return;
				}
				attr.Value = this.r.LookupNamespace(attr.Value.Substring(0, num)) + ":" + attr.Value.Substring(num + 1);
			}
		}

		/// <summary>Gets or sets a value that should be <see langword="true" /> for a SOAP 1.1 return value.</summary>
		/// <returns>
		///     <see langword="true" />, if the value is a return value. </returns>
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x000AF548 File Offset: 0x000AD748
		// (set) Token: 0x06001DA1 RID: 7585 RVA: 0x000AF55D File Offset: 0x000AD75D
		protected bool IsReturnValue
		{
			get
			{
				return this.isReturnValue && !this.soap12;
			}
			set
			{
				this.isReturnValue = value;
			}
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read the current XML element if the element has a null attribute with the value true. </summary>
		/// <returns>
		///     <see langword="true" /> if the element has a null="true" attribute value and has been read; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001DA2 RID: 7586 RVA: 0x000AF568 File Offset: 0x000AD768
		protected bool ReadNull()
		{
			if (!this.GetNullAttr())
			{
				return false;
			}
			if (this.r.IsEmptyElement)
			{
				this.r.Skip();
				return true;
			}
			this.r.ReadStartElement();
			int num = 0;
			int readerCount = this.ReaderCount;
			while (this.r.NodeType != XmlNodeType.EndElement)
			{
				this.UnknownNode(null);
				this.CheckReaderCount(ref num, ref readerCount);
			}
			this.ReadEndElement();
			return true;
		}

		/// <summary>Determines whether the XML element where the <see cref="T:System.Xml.XmlReader" /> is currently positioned has a null attribute set to the value <see langword="true" />.</summary>
		/// <returns>
		///     <see langword="true" /> if <see cref="T:System.Xml.XmlReader" /> is currently positioned over a null attribute with the value <see langword="true" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001DA3 RID: 7587 RVA: 0x000AF5D8 File Offset: 0x000AD7D8
		protected bool GetNullAttr()
		{
			string attribute = this.r.GetAttribute(this.nilID, this.instanceNsID);
			if (attribute == null)
			{
				attribute = this.r.GetAttribute(this.nullID, this.instanceNsID);
			}
			if (attribute == null)
			{
				attribute = this.r.GetAttribute(this.nullID, this.instanceNs2000ID);
				if (attribute == null)
				{
					attribute = this.r.GetAttribute(this.nullID, this.instanceNs1999ID);
				}
			}
			return attribute != null && XmlConvert.ToBoolean(attribute);
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read a simple, text-only XML element that could be <see langword="null" />. </summary>
		/// <returns>The string value; otherwise, <see langword="null" />.</returns>
		// Token: 0x06001DA4 RID: 7588 RVA: 0x000AF65C File Offset: 0x000AD85C
		protected string ReadNullableString()
		{
			if (this.ReadNull())
			{
				return null;
			}
			return this.r.ReadElementString();
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read the fully qualified name of the element where it is currently positioned. </summary>
		/// <returns>A <see cref="T:System.Xml.XmlQualifiedName" /> that represents the fully qualified name of the current XML element; otherwise, <see langword="null" /> if a null="true" attribute value is present.</returns>
		// Token: 0x06001DA5 RID: 7589 RVA: 0x000AF673 File Offset: 0x000AD873
		protected XmlQualifiedName ReadNullableQualifiedName()
		{
			if (this.ReadNull())
			{
				return null;
			}
			return this.ReadElementQualifiedName();
		}

		/// <summary>Makes the <see cref="T:System.Xml.XmlReader" /> read the fully qualified name of the element where it is currently positioned. </summary>
		/// <returns>The fully qualified name of the current XML element.</returns>
		// Token: 0x06001DA6 RID: 7590 RVA: 0x000AF688 File Offset: 0x000AD888
		protected XmlQualifiedName ReadElementQualifiedName()
		{
			if (this.r.IsEmptyElement)
			{
				XmlQualifiedName result = new XmlQualifiedName(string.Empty, this.r.LookupNamespace(""));
				this.r.Skip();
				return result;
			}
			XmlQualifiedName result2 = this.ToXmlQualifiedName(this.CollapseWhitespace(this.r.ReadString()));
			this.r.ReadEndElement();
			return result2;
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read an XML document root element at its current position.</summary>
		/// <param name="wrapped">
		///       <see langword="true" /> if the method should read content only after reading the element's start element; otherwise, <see langword="false" />.</param>
		/// <returns>An <see cref="T:System.Xml.XmlDocument" /> that contains the root element that has been read.</returns>
		// Token: 0x06001DA7 RID: 7591 RVA: 0x000AF6EC File Offset: 0x000AD8EC
		protected XmlDocument ReadXmlDocument(bool wrapped)
		{
			XmlNode xmlNode = this.ReadXmlNode(wrapped);
			if (xmlNode == null)
			{
				return null;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.AppendChild(xmlDocument.ImportNode(xmlNode, true));
			return xmlDocument;
		}

		/// <summary>Removes all occurrences of white space characters from the beginning and end of the specified string.</summary>
		/// <param name="value">The string that will have its white space trimmed.</param>
		/// <returns>The trimmed string.</returns>
		// Token: 0x06001DA8 RID: 7592 RVA: 0x000AF71A File Offset: 0x000AD91A
		protected string CollapseWhitespace(string value)
		{
			if (value == null)
			{
				return null;
			}
			return value.Trim();
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read the XML node at its current position. </summary>
		/// <param name="wrapped">
		///       <see langword="true" /> to read content only after reading the element's start element; otherwise, <see langword="false" />.</param>
		/// <returns>An <see cref="T:System.Xml.XmlNode" /> that represents the XML node that has been read.</returns>
		// Token: 0x06001DA9 RID: 7593 RVA: 0x000AF728 File Offset: 0x000AD928
		protected XmlNode ReadXmlNode(bool wrapped)
		{
			XmlNode result = null;
			if (wrapped)
			{
				if (this.ReadNull())
				{
					return null;
				}
				this.r.ReadStartElement();
				this.r.MoveToContent();
				if (this.r.NodeType != XmlNodeType.EndElement)
				{
					result = this.Document.ReadNode(this.r);
				}
				int num = 0;
				int readerCount = this.ReaderCount;
				while (this.r.NodeType != XmlNodeType.EndElement)
				{
					this.UnknownNode(null);
					this.CheckReaderCount(ref num, ref readerCount);
				}
				this.r.ReadEndElement();
			}
			else
			{
				result = this.Document.ReadNode(this.r);
			}
			return result;
		}

		/// <summary>Produces a base-64 byte array from an input string. </summary>
		/// <param name="value">A string to translate into a base-64 byte array.</param>
		/// <returns>A base-64 byte array.</returns>
		// Token: 0x06001DAA RID: 7594 RVA: 0x000AF7C7 File Offset: 0x000AD9C7
		protected static byte[] ToByteArrayBase64(string value)
		{
			return XmlCustomFormatter.ToByteArrayBase64(value);
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read the string value at its current position and return it as a base-64 byte array.</summary>
		/// <param name="isNull">
		///       <see langword="true" /> to return <see langword="null" />; <see langword="false" /> to return a base-64 byte array.</param>
		/// <returns>A base-64 byte array; otherwise, <see langword="null" /> if the value of the <paramref name="isNull" /> parameter is <see langword="true" />.</returns>
		// Token: 0x06001DAB RID: 7595 RVA: 0x000AF7CF File Offset: 0x000AD9CF
		protected byte[] ToByteArrayBase64(bool isNull)
		{
			if (isNull)
			{
				return null;
			}
			return this.ReadByteArray(true);
		}

		/// <summary>Produces a hexadecimal byte array from an input string.</summary>
		/// <param name="value">A string to translate into a hexadecimal byte array.</param>
		/// <returns>A hexadecimal byte array.</returns>
		// Token: 0x06001DAC RID: 7596 RVA: 0x000AF7DD File Offset: 0x000AD9DD
		protected static byte[] ToByteArrayHex(string value)
		{
			return XmlCustomFormatter.ToByteArrayHex(value);
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read the string value at its current position and return it as a hexadecimal byte array.</summary>
		/// <param name="isNull">
		///       <see langword="true" /> to return <see langword="null" />; <see langword="false" /> to return a hexadecimal byte array.</param>
		/// <returns>A hexadecimal byte array; otherwise, <see langword="null" /> if the value of the <paramref name="isNull" /> parameter is true. </returns>
		// Token: 0x06001DAD RID: 7597 RVA: 0x000AF7E5 File Offset: 0x000AD9E5
		protected byte[] ToByteArrayHex(bool isNull)
		{
			if (isNull)
			{
				return null;
			}
			return this.ReadByteArray(false);
		}

		/// <summary>Gets the length of the SOAP-encoded array where the <see cref="T:System.Xml.XmlReader" /> is currently positioned.</summary>
		/// <param name="name">The local name that the array should have.</param>
		/// <param name="ns">The namespace that the array should have.</param>
		/// <returns>The length of the SOAP array.</returns>
		// Token: 0x06001DAE RID: 7598 RVA: 0x000AF7F4 File Offset: 0x000AD9F4
		protected int GetArrayLength(string name, string ns)
		{
			if (this.GetNullAttr())
			{
				return 0;
			}
			string attribute = this.r.GetAttribute(this.arrayTypeID, this.soapNsID);
			XmlSerializationReader.SoapArrayInfo soapArrayInfo = this.ParseArrayType(attribute);
			if (soapArrayInfo.dimensions != 1)
			{
				throw new InvalidOperationException(Res.GetString("SOAP-ENC:arrayType with multidimensional array found at {0}. Only single-dimensional arrays are supported. Consider using an array of arrays instead.", new object[]
				{
					this.CurrentTag()
				}));
			}
			XmlQualifiedName xmlQualifiedName = this.ToXmlQualifiedName(soapArrayInfo.qname, false);
			if (xmlQualifiedName.Name != name)
			{
				throw new InvalidOperationException(Res.GetString("The SOAP-ENC:arrayType references type is named '{0}'; a type named '{1}' was expected at {2}.", new object[]
				{
					xmlQualifiedName.Name,
					name,
					this.CurrentTag()
				}));
			}
			if (xmlQualifiedName.Namespace != ns)
			{
				throw new InvalidOperationException(Res.GetString("The SOAP-ENC:arrayType references type is from namespace '{0}'; the namespace '{1}' was expected at {2}.", new object[]
				{
					xmlQualifiedName.Namespace,
					ns,
					this.CurrentTag()
				}));
			}
			return soapArrayInfo.length;
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x000AF8DC File Offset: 0x000ADADC
		private XmlSerializationReader.SoapArrayInfo ParseArrayType(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(Res.GetString("SOAP-ENC:arrayType was missing at {0}.", new object[]
				{
					this.CurrentTag()
				}));
			}
			if (value.Length == 0)
			{
				throw new ArgumentException(Res.GetString("SOAP-ENC:arrayType was empty at {0}.", new object[]
				{
					this.CurrentTag()
				}), "value");
			}
			char[] array = value.ToCharArray();
			int num = array.Length;
			XmlSerializationReader.SoapArrayInfo result = default(XmlSerializationReader.SoapArrayInfo);
			int num2 = num - 1;
			if (array[num2] != ']')
			{
				throw new ArgumentException(Res.GetString("SOAP-ENC:arrayType must end with a ']' character."), "value");
			}
			num2--;
			while (num2 != -1 && array[num2] != '[')
			{
				if (array[num2] == ',')
				{
					throw new ArgumentException(Res.GetString("SOAP-ENC:arrayType with multidimensional array found at {0}. Only single-dimensional arrays are supported. Consider using an array of arrays instead.", new object[]
					{
						this.CurrentTag()
					}), "value");
				}
				num2--;
			}
			if (num2 == -1)
			{
				throw new ArgumentException(Res.GetString("SOAP-ENC:arrayType has mismatched brackets."), "value");
			}
			int num3 = num - num2 - 2;
			if (num3 > 0)
			{
				string text = new string(array, num2 + 1, num3);
				try
				{
					result.length = int.Parse(text, CultureInfo.InvariantCulture);
					goto IL_14F;
				}
				catch (Exception ex)
				{
					if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
					{
						throw;
					}
					throw new ArgumentException(Res.GetString("SOAP-ENC:arrayType could not handle '{1}' as the length of the array.", new object[]
					{
						text
					}), "value");
				}
			}
			result.length = -1;
			IL_14F:
			num2--;
			result.jaggedDimensions = 0;
			while (num2 != -1 && array[num2] == ']')
			{
				num2--;
				if (num2 < 0)
				{
					throw new ArgumentException(Res.GetString("SOAP-ENC:arrayType has mismatched brackets."), "value");
				}
				if (array[num2] == ',')
				{
					throw new ArgumentException(Res.GetString("SOAP-ENC:arrayType with multidimensional array found at {0}. Only single-dimensional arrays are supported. Consider using an array of arrays instead.", new object[]
					{
						this.CurrentTag()
					}), "value");
				}
				if (array[num2] != '[')
				{
					throw new ArgumentException(Res.GetString("SOAP-ENC:arrayType must end with a ']' character."), "value");
				}
				num2--;
				result.jaggedDimensions++;
			}
			result.dimensions = 1;
			result.qname = new string(array, 0, num2 + 1);
			return result;
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x000AFAF0 File Offset: 0x000ADCF0
		private XmlSerializationReader.SoapArrayInfo ParseSoap12ArrayType(string itemType, string arraySize)
		{
			XmlSerializationReader.SoapArrayInfo soapArrayInfo = default(XmlSerializationReader.SoapArrayInfo);
			if (itemType != null && itemType.Length > 0)
			{
				soapArrayInfo.qname = itemType;
			}
			else
			{
				soapArrayInfo.qname = "";
			}
			string[] array;
			if (arraySize != null && arraySize.Length > 0)
			{
				array = arraySize.Split(null);
			}
			else
			{
				array = new string[0];
			}
			soapArrayInfo.dimensions = 0;
			soapArrayInfo.length = -1;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Length > 0)
				{
					if (array[i] == "*")
					{
						soapArrayInfo.dimensions++;
					}
					else
					{
						try
						{
							soapArrayInfo.length = int.Parse(array[i], CultureInfo.InvariantCulture);
							soapArrayInfo.dimensions++;
						}
						catch (Exception ex)
						{
							if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
							{
								throw;
							}
							throw new ArgumentException(Res.GetString("SOAP-ENC:arrayType could not handle '{1}' as the length of the array.", new object[]
							{
								array[i]
							}), "value");
						}
					}
				}
			}
			if (soapArrayInfo.dimensions == 0)
			{
				soapArrayInfo.dimensions = 1;
			}
			return soapArrayInfo;
		}

		/// <summary>Produces a <see cref="T:System.DateTime" /> object from an input string. </summary>
		/// <param name="value">A string to translate into a <see cref="T:System.DateTime" /> object.</param>
		/// <returns>A <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x06001DB1 RID: 7601 RVA: 0x000AFC10 File Offset: 0x000ADE10
		protected static DateTime ToDateTime(string value)
		{
			return XmlCustomFormatter.ToDateTime(value);
		}

		/// <summary>Produces a <see cref="T:System.DateTime" /> object from an input string. </summary>
		/// <param name="value">A string to translate into a <see cref="T:System.DateTime" /> class object.</param>
		/// <returns>A <see cref="T:System.DateTime" />object.</returns>
		// Token: 0x06001DB2 RID: 7602 RVA: 0x000AFC18 File Offset: 0x000ADE18
		protected static DateTime ToDate(string value)
		{
			return XmlCustomFormatter.ToDate(value);
		}

		/// <summary>Produces a <see cref="T:System.DateTime" /> from a string that represents the time. </summary>
		/// <param name="value">A string to translate into a <see cref="T:System.DateTime" /> object.</param>
		/// <returns>A <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x06001DB3 RID: 7603 RVA: 0x000AFC20 File Offset: 0x000ADE20
		protected static DateTime ToTime(string value)
		{
			return XmlCustomFormatter.ToTime(value);
		}

		/// <summary>Produces a <see cref="T:System.Char" /> object from an input string. </summary>
		/// <param name="value">A string to translate into a <see cref="T:System.Char" /> object.</param>
		/// <returns>A <see cref="T:System.Char" /> object.</returns>
		// Token: 0x06001DB4 RID: 7604 RVA: 0x000AFC28 File Offset: 0x000ADE28
		protected static char ToChar(string value)
		{
			return XmlCustomFormatter.ToChar(value);
		}

		/// <summary>Produces a numeric enumeration value from a string that consists of delimited identifiers that represent constants from the enumerator list. </summary>
		/// <param name="value">A string that consists of delimited identifiers where each identifier represents a constant from the set enumerator list.</param>
		/// <param name="h">A <see cref="T:System.Collections.Hashtable" /> that consists of the identifiers as keys and the constants as integral numbers.</param>
		/// <param name="typeName">The name of the enumeration type.</param>
		/// <returns>A long value that consists of the enumeration value as a series of bitwise <see langword="OR" /> operations.</returns>
		// Token: 0x06001DB5 RID: 7605 RVA: 0x000AFC30 File Offset: 0x000ADE30
		protected static long ToEnum(string value, Hashtable h, string typeName)
		{
			return XmlCustomFormatter.ToEnum(value, h, typeName, true);
		}

		/// <summary>Decodes an XML name.</summary>
		/// <param name="value">An XML name to be decoded.</param>
		/// <returns>A decoded string.</returns>
		// Token: 0x06001DB6 RID: 7606 RVA: 0x000AFC3B File Offset: 0x000ADE3B
		protected static string ToXmlName(string value)
		{
			return XmlCustomFormatter.ToXmlName(value);
		}

		/// <summary>Decodes an XML name.</summary>
		/// <param name="value">An XML name to be decoded.</param>
		/// <returns>A decoded string.</returns>
		// Token: 0x06001DB7 RID: 7607 RVA: 0x000AFC43 File Offset: 0x000ADE43
		protected static string ToXmlNCName(string value)
		{
			return XmlCustomFormatter.ToXmlNCName(value);
		}

		/// <summary>Decodes an XML name.</summary>
		/// <param name="value">An XML name to be decoded.</param>
		/// <returns>A decoded string.</returns>
		// Token: 0x06001DB8 RID: 7608 RVA: 0x000AFC4B File Offset: 0x000ADE4B
		protected static string ToXmlNmToken(string value)
		{
			return XmlCustomFormatter.ToXmlNmToken(value);
		}

		/// <summary>Decodes an XML name.</summary>
		/// <param name="value">An XML name to be decoded.</param>
		/// <returns>A decoded string.</returns>
		// Token: 0x06001DB9 RID: 7609 RVA: 0x000AFC53 File Offset: 0x000ADE53
		protected static string ToXmlNmTokens(string value)
		{
			return XmlCustomFormatter.ToXmlNmTokens(value);
		}

		/// <summary>Obtains an <see cref="T:System.Xml.XmlQualifiedName" /> from a name that may contain a prefix. </summary>
		/// <param name="value">A name that may contain a prefix.</param>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> that represents a namespace-qualified XML name.</returns>
		// Token: 0x06001DBA RID: 7610 RVA: 0x000AFC5B File Offset: 0x000ADE5B
		protected XmlQualifiedName ToXmlQualifiedName(string value)
		{
			return this.ToXmlQualifiedName(value, this.DecodeName);
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x000AFC6C File Offset: 0x000ADE6C
		internal XmlQualifiedName ToXmlQualifiedName(string value, bool decodeName)
		{
			int num = (value == null) ? -1 : value.LastIndexOf(':');
			string text = (num < 0) ? null : value.Substring(0, num);
			string text2 = value.Substring(num + 1);
			if (decodeName)
			{
				text = XmlConvert.DecodeName(text);
				text2 = XmlConvert.DecodeName(text2);
			}
			if (text == null || text.Length == 0)
			{
				return new XmlQualifiedName(this.r.NameTable.Add(value), this.r.LookupNamespace(string.Empty));
			}
			string text3 = this.r.LookupNamespace(text);
			if (text3 == null)
			{
				throw new InvalidOperationException(Res.GetString("Namespace prefix '{0}' is not defined.", new object[]
				{
					text
				}));
			}
			return new XmlQualifiedName(this.r.NameTable.Add(text2), text3);
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownAttribute" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="o">An object that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is attempting to deserialize, subsequently accessible through the <see cref="P:System.Xml.Serialization.XmlAttributeEventArgs.ObjectBeingDeserialized" /> property.</param>
		/// <param name="attr">An <see cref="T:System.Xml.XmlAttribute" /> that represents the attribute in question.</param>
		// Token: 0x06001DBC RID: 7612 RVA: 0x000AFD26 File Offset: 0x000ADF26
		protected void UnknownAttribute(object o, XmlAttribute attr)
		{
			this.UnknownAttribute(o, attr, null);
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownAttribute" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="o">An object that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is attempting to deserialize, subsequently accessible through the <see cref="P:System.Xml.Serialization.XmlAttributeEventArgs.ObjectBeingDeserialized" /> property.</param>
		/// <param name="attr">A <see cref="T:System.Xml.XmlAttribute" /> that represents the attribute in question.</param>
		/// <param name="qnames">A comma-delimited list of XML qualified names.</param>
		// Token: 0x06001DBD RID: 7613 RVA: 0x000AFD34 File Offset: 0x000ADF34
		protected void UnknownAttribute(object o, XmlAttribute attr, string qnames)
		{
			if (this.events.OnUnknownAttribute != null)
			{
				int lineNumber;
				int linePosition;
				this.GetCurrentPosition(out lineNumber, out linePosition);
				XmlAttributeEventArgs e = new XmlAttributeEventArgs(attr, lineNumber, linePosition, o, qnames);
				this.events.OnUnknownAttribute(this.events.sender, e);
			}
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownElement" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="o">The <see cref="T:System.Object" /> that is being deserialized.</param>
		/// <param name="elem">The <see cref="T:System.Xml.XmlElement" /> for which an event is raised.</param>
		// Token: 0x06001DBE RID: 7614 RVA: 0x000AFD7F File Offset: 0x000ADF7F
		protected void UnknownElement(object o, XmlElement elem)
		{
			this.UnknownElement(o, elem, null);
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownElement" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="o">An object that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is attempting to deserialize, subsequently accessible through the <see cref="P:System.Xml.Serialization.XmlAttributeEventArgs.ObjectBeingDeserialized" /> property.</param>
		/// <param name="elem">The <see cref="T:System.Xml.XmlElement" /> for which an event is raised.</param>
		/// <param name="qnames">A comma-delimited list of XML qualified names.</param>
		// Token: 0x06001DBF RID: 7615 RVA: 0x000AFD8C File Offset: 0x000ADF8C
		protected void UnknownElement(object o, XmlElement elem, string qnames)
		{
			if (this.events.OnUnknownElement != null)
			{
				int lineNumber;
				int linePosition;
				this.GetCurrentPosition(out lineNumber, out linePosition);
				XmlElementEventArgs e = new XmlElementEventArgs(elem, lineNumber, linePosition, o, qnames);
				this.events.OnUnknownElement(this.events.sender, e);
			}
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownNode" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />. </summary>
		/// <param name="o">The object that is being deserialized.</param>
		// Token: 0x06001DC0 RID: 7616 RVA: 0x000AFDD7 File Offset: 0x000ADFD7
		protected void UnknownNode(object o)
		{
			this.UnknownNode(o, null);
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownNode" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="o">The object being deserialized.</param>
		/// <param name="qnames">A comma-delimited list of XML qualified names.</param>
		// Token: 0x06001DC1 RID: 7617 RVA: 0x000AFDE4 File Offset: 0x000ADFE4
		protected void UnknownNode(object o, string qnames)
		{
			if (this.r.NodeType == XmlNodeType.None || this.r.NodeType == XmlNodeType.Whitespace)
			{
				this.r.Read();
				return;
			}
			if (this.r.NodeType == XmlNodeType.EndElement)
			{
				return;
			}
			if (this.events.OnUnknownNode != null)
			{
				this.UnknownNode(this.Document.ReadNode(this.r), o, qnames);
				return;
			}
			if (this.r.NodeType == XmlNodeType.Attribute && this.events.OnUnknownAttribute == null)
			{
				return;
			}
			if (this.r.NodeType == XmlNodeType.Element && this.events.OnUnknownElement == null)
			{
				this.r.Skip();
				return;
			}
			this.UnknownNode(this.Document.ReadNode(this.r), o, qnames);
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x000AFEB0 File Offset: 0x000AE0B0
		private void UnknownNode(XmlNode unknownNode, object o, string qnames)
		{
			if (unknownNode == null)
			{
				return;
			}
			if (unknownNode.NodeType != XmlNodeType.None && unknownNode.NodeType != XmlNodeType.Whitespace && this.events.OnUnknownNode != null)
			{
				int lineNumber;
				int linePosition;
				this.GetCurrentPosition(out lineNumber, out linePosition);
				XmlNodeEventArgs e = new XmlNodeEventArgs(unknownNode, lineNumber, linePosition, o);
				this.events.OnUnknownNode(this.events.sender, e);
			}
			if (unknownNode.NodeType == XmlNodeType.Attribute)
			{
				this.UnknownAttribute(o, (XmlAttribute)unknownNode, qnames);
				return;
			}
			if (unknownNode.NodeType == XmlNodeType.Element)
			{
				this.UnknownElement(o, (XmlElement)unknownNode, qnames);
			}
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x000AFF40 File Offset: 0x000AE140
		private void GetCurrentPosition(out int lineNumber, out int linePosition)
		{
			if (this.Reader is IXmlLineInfo)
			{
				IXmlLineInfo xmlLineInfo = (IXmlLineInfo)this.Reader;
				lineNumber = xmlLineInfo.LineNumber;
				linePosition = xmlLineInfo.LinePosition;
				return;
			}
			lineNumber = (linePosition = -1);
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnreferencedObject" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="id">A unique string that is used to identify the unreferenced object, subsequently accessible through the <see cref="P:System.Xml.Serialization.UnreferencedObjectEventArgs.UnreferencedId" /> property.</param>
		/// <param name="o">An object that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is attempting to deserialize, subsequently accessible through the <see cref="P:System.Xml.Serialization.UnreferencedObjectEventArgs.UnreferencedObject" /> property.</param>
		// Token: 0x06001DC4 RID: 7620 RVA: 0x000AFF80 File Offset: 0x000AE180
		protected void UnreferencedObject(string id, object o)
		{
			if (this.events.OnUnreferencedObject != null)
			{
				UnreferencedObjectEventArgs e = new UnreferencedObjectEventArgs(o, id);
				this.events.OnUnreferencedObject(this.events.sender, e);
			}
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x000AFFC0 File Offset: 0x000AE1C0
		private string CurrentTag()
		{
			XmlNodeType nodeType = this.r.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
				return string.Concat(new string[]
				{
					"<",
					this.r.LocalName,
					" xmlns='",
					this.r.NamespaceURI,
					"'>"
				});
			case XmlNodeType.Attribute:
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
				break;
			case XmlNodeType.Text:
				return this.r.Value;
			case XmlNodeType.CDATA:
				return "CDATA";
			case XmlNodeType.ProcessingInstruction:
				return "<?";
			case XmlNodeType.Comment:
				return "<--";
			default:
				if (nodeType == XmlNodeType.EndElement)
				{
					return ">";
				}
				break;
			}
			return "(unknown)";
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that a type is unknown. </summary>
		/// <param name="type">An <see cref="T:System.Xml.XmlQualifiedName" /> that represents the name of the unknown type.</param>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		// Token: 0x06001DC6 RID: 7622 RVA: 0x000B0071 File Offset: 0x000AE271
		protected Exception CreateUnknownTypeException(XmlQualifiedName type)
		{
			return new InvalidOperationException(Res.GetString("The specified type was not recognized: name='{0}', namespace='{1}', at {2}.", new object[]
			{
				type.Name,
				type.Namespace,
				this.CurrentTag()
			}));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that a SOAP-encoded collection type cannot be modified and its values cannot be filled in. </summary>
		/// <param name="name">The fully qualified name of the .NET Framework type for which there is a mapping.</param>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		// Token: 0x06001DC7 RID: 7623 RVA: 0x000B00A3 File Offset: 0x000AE2A3
		protected Exception CreateReadOnlyCollectionException(string name)
		{
			return new InvalidOperationException(Res.GetString("Could not deserialize {0}. Parameterless constructor is required for collections and enumerators.", new object[]
			{
				name
			}));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that an object being deserialized should be abstract. </summary>
		/// <param name="name">The name of the abstract type.</param>
		/// <param name="ns">The .NET Framework namespace of the abstract type.</param>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		// Token: 0x06001DC8 RID: 7624 RVA: 0x000B00BE File Offset: 0x000AE2BE
		protected Exception CreateAbstractTypeException(string name, string ns)
		{
			return new InvalidOperationException(Res.GetString("The specified type is abstract: name='{0}', namespace='{1}', at {2}.", new object[]
			{
				name,
				ns,
				this.CurrentTag()
			}));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that an object being deserialized cannot be instantiated because there is no constructor available.</summary>
		/// <param name="typeName">The name of the type.</param>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		// Token: 0x06001DC9 RID: 7625 RVA: 0x000B00E6 File Offset: 0x000AE2E6
		protected Exception CreateInaccessibleConstructorException(string typeName)
		{
			return new InvalidOperationException(Res.GetString("{0} cannot be serialized because it does not have a parameterless constructor.", new object[]
			{
				typeName
			}));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that an object being deserialized cannot be instantiated because the constructor throws a security exception.</summary>
		/// <param name="typeName">The name of the type.</param>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		// Token: 0x06001DCA RID: 7626 RVA: 0x000B0101 File Offset: 0x000AE301
		protected Exception CreateCtorHasSecurityException(string typeName)
		{
			return new InvalidOperationException(Res.GetString("The type '{0}' cannot be serialized because its parameterless constructor is decorated with declarative security permission attributes. Consider using imperative asserts or demands in the constructor.", new object[]
			{
				typeName
			}));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that the current position of <see cref="T:System.Xml.XmlReader" /> represents an unknown XML node. </summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		// Token: 0x06001DCB RID: 7627 RVA: 0x000B011C File Offset: 0x000AE31C
		protected Exception CreateUnknownNodeException()
		{
			return new InvalidOperationException(Res.GetString("{0} was not expected.", new object[]
			{
				this.CurrentTag()
			}));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that an enumeration value is not valid. </summary>
		/// <param name="value">The enumeration value that is not valid.</param>
		/// <param name="enumType">The enumeration type.</param>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		// Token: 0x06001DCC RID: 7628 RVA: 0x000B013C File Offset: 0x000AE33C
		protected Exception CreateUnknownConstantException(string value, Type enumType)
		{
			return new InvalidOperationException(Res.GetString("Instance validation error: '{0}' is not a valid value for {1}.", new object[]
			{
				value,
				enumType.Name
			}));
		}

		/// <summary>Creates an <see cref="T:System.InvalidCastException" /> that indicates that an explicit reference conversion failed.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> that an object cannot be cast to. This type is incorporated into the exception message.</param>
		/// <param name="value">The object that cannot be cast. This object is incorporated into the exception message.</param>
		/// <returns>An <see cref="T:System.InvalidCastException" /> exception.</returns>
		// Token: 0x06001DCD RID: 7629 RVA: 0x000B0160 File Offset: 0x000AE360
		protected Exception CreateInvalidCastException(Type type, object value)
		{
			return this.CreateInvalidCastException(type, value, null);
		}

		/// <summary>Creates an <see cref="T:System.InvalidCastException" /> that indicates that an explicit reference conversion failed.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> that an object cannot be cast to. This type is incorporated into the exception message.</param>
		/// <param name="value">The object that cannot be cast. This object is incorporated into the exception message.</param>
		/// <param name="id">A string identifier.</param>
		/// <returns>An <see cref="T:System.InvalidCastException" /> exception.</returns>
		// Token: 0x06001DCE RID: 7630 RVA: 0x000B016C File Offset: 0x000AE36C
		protected Exception CreateInvalidCastException(Type type, object value, string id)
		{
			if (value == null)
			{
				return new InvalidCastException(Res.GetString("Cannot assign null value to an object of type {1}.", new object[]
				{
					type.FullName
				}));
			}
			if (id == null)
			{
				return new InvalidCastException(Res.GetString("Cannot assign object of type {0} to an object of type {1}.", new object[]
				{
					value.GetType().FullName,
					type.FullName
				}));
			}
			return new InvalidCastException(Res.GetString("Cannot assign object of type {0} to an object of type {1}. The error occurred while reading node with id='{2}'.", new object[]
			{
				value.GetType().FullName,
				type.FullName,
				id
			}));
		}

		/// <summary>Populates an object from its XML representation at the current location of the <see cref="T:System.Xml.XmlReader" />, with an option to read the inner element.</summary>
		/// <param name="xsdDerived">The local name of the derived XML Schema data type.</param>
		/// <param name="nsDerived">The namespace of the derived XML Schema data type.</param>
		/// <param name="xsdBase">The local name of the base XML Schema data type.</param>
		/// <param name="nsBase">The namespace of the base XML Schema data type.</param>
		/// <param name="clrDerived">The namespace of the derived .NET Framework type.</param>
		/// <param name="clrBase">The name of the base .NET Framework type.</param>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		// Token: 0x06001DCF RID: 7631 RVA: 0x000B01FB File Offset: 0x000AE3FB
		protected Exception CreateBadDerivationException(string xsdDerived, string nsDerived, string xsdBase, string nsBase, string clrDerived, string clrBase)
		{
			return new InvalidOperationException(Res.GetString("Type '{0}' from namespace '{1}' declared as derivation of type '{2}' from namespace '{3}, but corresponding CLR types are not compatible.  Cannot convert type '{4}' to '{5}'.", new object[]
			{
				xsdDerived,
				nsDerived,
				xsdBase,
				nsBase,
				clrDerived,
				clrBase
			}));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that a derived type that is mapped to an XML Schema data type cannot be located.</summary>
		/// <param name="name">The local name of the XML Schema data type that is mapped to the unavailable derived type.</param>
		/// <param name="ns">The namespace of the XML Schema data type that is mapped to the unavailable derived type.</param>
		/// <param name="clrType">The full name of the .NET Framework base type for which a derived type cannot be located.</param>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		// Token: 0x06001DD0 RID: 7632 RVA: 0x000B022D File Offset: 0x000AE42D
		protected Exception CreateMissingIXmlSerializableType(string name, string ns, string clrType)
		{
			return new InvalidOperationException(Res.GetString("Type '{0}' from namespace '{1}' does not have corresponding IXmlSerializable type. Please consider adding {2} to '{3}'.", new object[]
			{
				name,
				ns,
				typeof(XmlIncludeAttribute).Name,
				clrType
			}));
		}

		/// <summary>Ensures that a given array, or a copy, is large enough to contain a specified index. </summary>
		/// <param name="a">The <see cref="T:System.Array" /> that is being checked.</param>
		/// <param name="index">The required index.</param>
		/// <param name="elementType">The <see cref="T:System.Type" /> of the array's elements.</param>
		/// <returns>The existing <see cref="T:System.Array" />, if it is already large enough; otherwise, a new, larger array that contains the original array's elements.</returns>
		// Token: 0x06001DD1 RID: 7633 RVA: 0x000B0264 File Offset: 0x000AE464
		protected Array EnsureArrayIndex(Array a, int index, Type elementType)
		{
			if (a == null)
			{
				return Array.CreateInstance(elementType, 32);
			}
			if (index < a.Length)
			{
				return a;
			}
			Array array = Array.CreateInstance(elementType, a.Length * 2);
			Array.Copy(a, array, index);
			return array;
		}

		/// <summary>Ensures that a given array, or a copy, is no larger than a specified length. </summary>
		/// <param name="a">The array that is being checked.</param>
		/// <param name="length">The maximum length of the array.</param>
		/// <param name="elementType">The <see cref="T:System.Type" /> of the array's elements.</param>
		/// <param name="isNullable">
		///       <see langword="true" /> if <see langword="null" /> for the array, if present for the input array, can be returned; otherwise, a new, smaller array.</param>
		/// <returns>The existing <see cref="T:System.Array" />, if it is already small enough; otherwise, a new, smaller array that contains the original array's elements up to the size of<paramref name=" length" />.</returns>
		// Token: 0x06001DD2 RID: 7634 RVA: 0x000B02A0 File Offset: 0x000AE4A0
		protected Array ShrinkArray(Array a, int length, Type elementType, bool isNullable)
		{
			if (a == null)
			{
				if (isNullable)
				{
					return null;
				}
				return Array.CreateInstance(elementType, 0);
			}
			else
			{
				if (a.Length == length)
				{
					return a;
				}
				Array array = Array.CreateInstance(elementType, length);
				Array.Copy(a, array, length);
				return array;
			}
		}

		/// <summary>Produces the result of a call to the <see cref="M:System.Xml.XmlReader.ReadString" /> method appended to the input value. </summary>
		/// <param name="value">A string to prefix to the result of a call to the <see cref="M:System.Xml.XmlReader.ReadString" /> method.</param>
		/// <returns>The result of call to the <see cref="M:System.Xml.XmlReader.ReadString" /> method appended to the input value.</returns>
		// Token: 0x06001DD3 RID: 7635 RVA: 0x000B02DA File Offset: 0x000AE4DA
		protected string ReadString(string value)
		{
			return this.ReadString(value, false);
		}

		/// <summary>Returns the result of a call to the <see cref="M:System.Xml.XmlReader.ReadString" /> method of the <see cref="T:System.Xml.XmlReader" /> class, trimmed of white space if needed, and appended to the input value.</summary>
		/// <param name="value">A string that will be appended to.</param>
		/// <param name="trim">
		///       <see langword="true" /> if the result of the read operation should be trimmed; otherwise, <see langword="false" />.</param>
		/// <returns>The result of the read operation appended to the input value.</returns>
		// Token: 0x06001DD4 RID: 7636 RVA: 0x000B02E4 File Offset: 0x000AE4E4
		protected string ReadString(string value, bool trim)
		{
			string text = this.r.ReadString();
			if (text != null && trim)
			{
				text = text.Trim();
			}
			if (value == null || value.Length == 0)
			{
				return text;
			}
			return value + text;
		}

		/// <summary>Populates an object from its XML representation at the current location of the <see cref="T:System.Xml.XmlReader" />. </summary>
		/// <param name="serializable">An <see cref="T:System.Xml.Serialization.IXmlSerializable" /> that corresponds to the current position of the <see cref="T:System.Xml.XmlReader" />.</param>
		/// <returns>An object that implements the <see cref="T:System.Xml.Serialization.IXmlSerializable" /> interface with its members populated from the location of the <see cref="T:System.Xml.XmlReader" />.</returns>
		// Token: 0x06001DD5 RID: 7637 RVA: 0x000B0320 File Offset: 0x000AE520
		protected IXmlSerializable ReadSerializable(IXmlSerializable serializable)
		{
			return this.ReadSerializable(serializable, false);
		}

		/// <summary>This method supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="serializable">An IXmlSerializable object that corresponds to the current position of the XMLReader.</param>
		/// <param name="wrappedAny">Specifies whether the serializable object is wrapped.</param>
		/// <returns>An object that implements the IXmlSerializable interface with its members populated from the location of the XmlReader.</returns>
		// Token: 0x06001DD6 RID: 7638 RVA: 0x000B032C File Offset: 0x000AE52C
		protected IXmlSerializable ReadSerializable(IXmlSerializable serializable, bool wrappedAny)
		{
			string b = null;
			string b2 = null;
			if (wrappedAny)
			{
				b = this.r.LocalName;
				b2 = this.r.NamespaceURI;
				this.r.Read();
				this.r.MoveToContent();
			}
			serializable.ReadXml(this.r);
			if (wrappedAny)
			{
				while (this.r.NodeType == XmlNodeType.Whitespace)
				{
					this.r.Skip();
				}
				if (this.r.NodeType == XmlNodeType.None)
				{
					this.r.Skip();
				}
				if (this.r.NodeType == XmlNodeType.EndElement && this.r.LocalName == b && this.r.NamespaceURI == b2)
				{
					this.Reader.Read();
				}
			}
			return serializable;
		}

		/// <summary>Reads the value of the <see langword="href" /> attribute (<see langword="ref" /> attribute for SOAP 1.2) that is used to refer to an XML element in SOAP encoding. </summary>
		/// <param name="fixupReference">An output string into which the <see langword="href" /> attribute value is read.</param>
		/// <returns>
		///     <see langword="true" /> if the value was read; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001DD7 RID: 7639 RVA: 0x000B03F8 File Offset: 0x000AE5F8
		protected bool ReadReference(out string fixupReference)
		{
			string text = this.soap12 ? this.r.GetAttribute("ref", "http://www.w3.org/2003/05/soap-encoding") : this.r.GetAttribute("href");
			if (text == null)
			{
				fixupReference = null;
				return false;
			}
			if (!this.soap12)
			{
				if (!text.StartsWith("#", StringComparison.Ordinal))
				{
					throw new InvalidOperationException(Res.GetString("The referenced element with ID '{0}' is located outside the current document and cannot be retrieved.", new object[]
					{
						text
					}));
				}
				fixupReference = text.Substring(1);
			}
			else
			{
				fixupReference = text;
			}
			if (this.r.IsEmptyElement)
			{
				this.r.Skip();
			}
			else
			{
				this.r.ReadStartElement();
				this.ReadEndElement();
			}
			return true;
		}

		/// <summary>Stores an object that is being deserialized from a SOAP-encoded <see langword="multiRef" /> element for later access through the <see cref="M:System.Xml.Serialization.XmlSerializationReader.GetTarget(System.String)" /> method. </summary>
		/// <param name="id">The value of the <see langword="id" /> attribute of a <see langword="multiRef" /> element that identifies the element.</param>
		/// <param name="o">The object that is deserialized from the XML element.</param>
		// Token: 0x06001DD8 RID: 7640 RVA: 0x000B04A8 File Offset: 0x000AE6A8
		protected void AddTarget(string id, object o)
		{
			if (id == null)
			{
				if (this.targetsWithoutIds == null)
				{
					this.targetsWithoutIds = new ArrayList();
				}
				if (o != null)
				{
					this.targetsWithoutIds.Add(o);
					return;
				}
			}
			else
			{
				if (this.targets == null)
				{
					this.targets = new Hashtable();
				}
				if (!this.targets.Contains(id))
				{
					this.targets.Add(id, o);
				}
			}
		}

		/// <summary>Stores an object that contains a callback method instance that will be called, as necessary, to fill in the objects in a SOAP-encoded array. </summary>
		/// <param name="fixup">An <see cref="T:System.Xml.Serialization.XmlSerializationFixupCallback" /> delegate and the callback method's input data.</param>
		// Token: 0x06001DD9 RID: 7641 RVA: 0x000B050A File Offset: 0x000AE70A
		protected void AddFixup(XmlSerializationReader.Fixup fixup)
		{
			if (this.fixups == null)
			{
				this.fixups = new ArrayList();
			}
			this.fixups.Add(fixup);
		}

		/// <summary>Stores an object that contains a callback method that will be called, as necessary, to fill in .NET Framework collections or enumerations that map to SOAP-encoded arrays or SOAP-encoded, multi-referenced elements. </summary>
		/// <param name="fixup">A <see cref="T:System.Xml.Serialization.XmlSerializationCollectionFixupCallback" /> delegate and the callback method's input data.</param>
		// Token: 0x06001DDA RID: 7642 RVA: 0x000B052C File Offset: 0x000AE72C
		protected void AddFixup(XmlSerializationReader.CollectionFixup fixup)
		{
			if (this.collectionFixups == null)
			{
				this.collectionFixups = new ArrayList();
			}
			this.collectionFixups.Add(fixup);
		}

		/// <summary>Gets an object that is being deserialized from a SOAP-encoded <see langword="multiRef" /> element and that was stored earlier by <see cref="M:System.Xml.Serialization.XmlSerializationReader.AddTarget(System.String,System.Object)" />.  </summary>
		/// <param name="id">The value of the <see langword="id" /> attribute of a <see langword="multiRef" /> element that identifies the element.</param>
		/// <returns>An object to be deserialized from a SOAP-encoded <see langword="multiRef" /> element.</returns>
		// Token: 0x06001DDB RID: 7643 RVA: 0x000B0550 File Offset: 0x000AE750
		protected object GetTarget(string id)
		{
			object obj = (this.targets != null) ? this.targets[id] : null;
			if (obj == null)
			{
				throw new InvalidOperationException(Res.GetString("The referenced element with ID '{0}' was not found in the document.", new object[]
				{
					id
				}));
			}
			this.Referenced(obj);
			return obj;
		}

		/// <summary>Stores an object to be deserialized from a SOAP-encoded <see langword="multiRef" /> element.</summary>
		/// <param name="o">The object to be deserialized.</param>
		// Token: 0x06001DDC RID: 7644 RVA: 0x000B059A File Offset: 0x000AE79A
		protected void Referenced(object o)
		{
			if (o == null)
			{
				return;
			}
			if (this.referencedTargets == null)
			{
				this.referencedTargets = new Hashtable();
			}
			this.referencedTargets[o] = o;
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x000B05C0 File Offset: 0x000AE7C0
		private void HandleUnreferencedObjects()
		{
			if (this.targets != null)
			{
				foreach (object obj in this.targets)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (this.referencedTargets == null || !this.referencedTargets.Contains(dictionaryEntry.Value))
					{
						this.UnreferencedObject((string)dictionaryEntry.Key, dictionaryEntry.Value);
					}
				}
			}
			if (this.targetsWithoutIds != null)
			{
				foreach (object obj2 in this.targetsWithoutIds)
				{
					if (this.referencedTargets == null || !this.referencedTargets.Contains(obj2))
					{
						this.UnreferencedObject(null, obj2);
					}
				}
			}
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x000B06B4 File Offset: 0x000AE8B4
		private void DoFixups()
		{
			if (this.fixups == null)
			{
				return;
			}
			for (int i = 0; i < this.fixups.Count; i++)
			{
				XmlSerializationReader.Fixup fixup = (XmlSerializationReader.Fixup)this.fixups[i];
				fixup.Callback(fixup);
			}
			if (this.collectionFixups == null)
			{
				return;
			}
			for (int j = 0; j < this.collectionFixups.Count; j++)
			{
				XmlSerializationReader.CollectionFixup collectionFixup = (XmlSerializationReader.CollectionFixup)this.collectionFixups[j];
				collectionFixup.Callback(collectionFixup.Collection, collectionFixup.CollectionItems);
			}
		}

		/// <summary>Fills in the values of a SOAP-encoded array whose data type maps to a .NET Framework reference type.</summary>
		/// <param name="fixup">An object that contains the array whose values are filled in.</param>
		// Token: 0x06001DDF RID: 7647 RVA: 0x000B0748 File Offset: 0x000AE948
		protected void FixupArrayRefs(object fixup)
		{
			XmlSerializationReader.Fixup fixup2 = (XmlSerializationReader.Fixup)fixup;
			Array array = (Array)fixup2.Source;
			for (int i = 0; i < array.Length; i++)
			{
				string text = fixup2.Ids[i];
				if (text != null)
				{
					object target = this.GetTarget(text);
					try
					{
						array.SetValue(target, i);
					}
					catch (InvalidCastException)
					{
						throw new InvalidOperationException(Res.GetString("Invalid reference id='{0}'. Object of type {1} cannot be stored in an array of this type. Details: array index={2}.", new object[]
						{
							text,
							target.GetType().FullName,
							i.ToString(CultureInfo.InvariantCulture)
						}));
					}
				}
			}
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x000B07E4 File Offset: 0x000AE9E4
		private object ReadArray(string typeName, string typeNs)
		{
			Type type = null;
			XmlSerializationReader.SoapArrayInfo soapArrayInfo;
			if (this.soap12)
			{
				string attribute = this.r.GetAttribute(this.itemTypeID, this.soap12NsID);
				string attribute2 = this.r.GetAttribute(this.arraySizeID, this.soap12NsID);
				Type type2 = (Type)this.types[new XmlQualifiedName(typeName, typeNs)];
				if (attribute == null && attribute2 == null && (type2 == null || !type2.IsArray))
				{
					return null;
				}
				soapArrayInfo = this.ParseSoap12ArrayType(attribute, attribute2);
				if (type2 != null)
				{
					type = TypeScope.GetArrayElementType(type2, null);
				}
			}
			else
			{
				string attribute3 = this.r.GetAttribute(this.arrayTypeID, this.soapNsID);
				if (attribute3 == null)
				{
					return null;
				}
				soapArrayInfo = this.ParseArrayType(attribute3);
			}
			if (soapArrayInfo.dimensions != 1)
			{
				throw new InvalidOperationException(Res.GetString("SOAP-ENC:arrayType with multidimensional array found at {0}. Only single-dimensional arrays are supported. Consider using an array of arrays instead.", new object[]
				{
					this.CurrentTag()
				}));
			}
			Type type3 = null;
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(this.urTypeID, this.schemaNsID);
			XmlQualifiedName xmlQualifiedName2;
			if (soapArrayInfo.qname.Length > 0)
			{
				xmlQualifiedName2 = this.ToXmlQualifiedName(soapArrayInfo.qname, false);
				type3 = (Type)this.types[xmlQualifiedName2];
			}
			else
			{
				xmlQualifiedName2 = xmlQualifiedName;
			}
			if (this.soap12 && type3 == typeof(object))
			{
				type3 = null;
			}
			bool flag;
			if (type3 == null)
			{
				if (!this.soap12)
				{
					type3 = this.GetPrimitiveType(xmlQualifiedName2, true);
					flag = true;
				}
				else
				{
					if (xmlQualifiedName2 != xmlQualifiedName)
					{
						type3 = this.GetPrimitiveType(xmlQualifiedName2, false);
					}
					if (type3 != null)
					{
						flag = true;
					}
					else if (type == null)
					{
						type3 = typeof(object);
						flag = false;
					}
					else
					{
						type3 = type;
						XmlQualifiedName xmlQualifiedName3 = (XmlQualifiedName)this.typesReverse[type3];
						if (xmlQualifiedName3 == null)
						{
							xmlQualifiedName3 = XmlSerializationWriter.GetPrimitiveTypeNameInternal(type3);
							flag = true;
						}
						else
						{
							flag = type3.IsPrimitive;
						}
						if (xmlQualifiedName3 != null)
						{
							xmlQualifiedName2 = xmlQualifiedName3;
						}
					}
				}
			}
			else
			{
				flag = type3.IsPrimitive;
			}
			if (!this.soap12 && soapArrayInfo.jaggedDimensions > 0)
			{
				for (int i = 0; i < soapArrayInfo.jaggedDimensions; i++)
				{
					type3 = type3.MakeArrayType();
				}
			}
			if (this.r.IsEmptyElement)
			{
				this.r.Skip();
				return Array.CreateInstance(type3, 0);
			}
			this.r.ReadStartElement();
			this.r.MoveToContent();
			int num = 0;
			Array array = null;
			if (type3.IsValueType)
			{
				if (!flag && !type3.IsEnum)
				{
					throw new NotSupportedException(Res.GetString("Cannot serialize {0}. Arrays of structs are not supported with encoded SOAP.", new object[]
					{
						type3.FullName
					}));
				}
				int num2 = 0;
				int readerCount = this.ReaderCount;
				while (this.r.NodeType != XmlNodeType.EndElement)
				{
					array = this.EnsureArrayIndex(array, num, type3);
					array.SetValue(this.ReadReferencedElement(xmlQualifiedName2.Name, xmlQualifiedName2.Namespace), num);
					num++;
					this.r.MoveToContent();
					this.CheckReaderCount(ref num2, ref readerCount);
				}
				array = this.ShrinkArray(array, num, type3, false);
			}
			else
			{
				string[] array2 = null;
				int num3 = 0;
				int num4 = 0;
				int readerCount2 = this.ReaderCount;
				while (this.r.NodeType != XmlNodeType.EndElement)
				{
					array = this.EnsureArrayIndex(array, num, type3);
					array2 = (string[])this.EnsureArrayIndex(array2, num3, typeof(string));
					string name;
					string ns;
					if (this.r.NamespaceURI.Length != 0)
					{
						name = this.r.LocalName;
						if (this.r.NamespaceURI == this.soapNsID)
						{
							ns = "http://www.w3.org/2001/XMLSchema";
						}
						else
						{
							ns = this.r.NamespaceURI;
						}
					}
					else
					{
						name = xmlQualifiedName2.Name;
						ns = xmlQualifiedName2.Namespace;
					}
					array.SetValue(this.ReadReferencingElement(name, ns, out array2[num3]), num);
					num++;
					num3++;
					this.r.MoveToContent();
					this.CheckReaderCount(ref num4, ref readerCount2);
				}
				if (this.soap12 && type3 == typeof(object))
				{
					Type type4 = null;
					for (int j = 0; j < num; j++)
					{
						object value = array.GetValue(j);
						if (value != null)
						{
							Type type5 = value.GetType();
							if (type5.IsValueType)
							{
								type4 = null;
								break;
							}
							if (type4 == null || type5.IsAssignableFrom(type4))
							{
								type4 = type5;
							}
							else if (!type4.IsAssignableFrom(type5))
							{
								type4 = null;
								break;
							}
						}
					}
					if (type4 != null)
					{
						type3 = type4;
					}
				}
				array2 = (string[])this.ShrinkArray(array2, num3, typeof(string), false);
				array = this.ShrinkArray(array, num, type3, false);
				XmlSerializationReader.Fixup fixup = new XmlSerializationReader.Fixup(array, new XmlSerializationFixupCallback(this.FixupArrayRefs), array2);
				this.AddFixup(fixup);
			}
			this.ReadEndElement();
			return array;
		}

		/// <summary>Initializes callback methods that populate objects that map to SOAP-encoded XML data. </summary>
		// Token: 0x06001DE1 RID: 7649
		protected abstract void InitCallbacks();

		/// <summary>Deserializes objects from the SOAP-encoded <see langword="multiRef" /> elements in a SOAP message. </summary>
		// Token: 0x06001DE2 RID: 7650 RVA: 0x000B0CD4 File Offset: 0x000AEED4
		protected void ReadReferencedElements()
		{
			this.r.MoveToContent();
			int num = 0;
			int readerCount = this.ReaderCount;
			while (this.r.NodeType != XmlNodeType.EndElement && this.r.NodeType != XmlNodeType.None)
			{
				string text;
				this.ReadReferencingElement(null, null, true, out text);
				this.r.MoveToContent();
				this.CheckReaderCount(ref num, ref readerCount);
			}
			this.DoFixups();
			this.HandleUnreferencedObjects();
		}

		/// <summary>Deserializes an object from a SOAP-encoded <see langword="multiRef" /> XML element. </summary>
		/// <returns>The value of the referenced element in the document.</returns>
		// Token: 0x06001DE3 RID: 7651 RVA: 0x000B0D42 File Offset: 0x000AEF42
		protected object ReadReferencedElement()
		{
			return this.ReadReferencedElement(null, null);
		}

		/// <summary>Deserializes an object from a SOAP-encoded <see langword="multiRef" /> XML element. </summary>
		/// <param name="name">The local name of the element's XML Schema data type.</param>
		/// <param name="ns">The namespace of the element's XML Schema data type.</param>
		/// <returns>The value of the referenced element in the document.</returns>
		// Token: 0x06001DE4 RID: 7652 RVA: 0x000B0D4C File Offset: 0x000AEF4C
		protected object ReadReferencedElement(string name, string ns)
		{
			string text;
			return this.ReadReferencingElement(name, ns, out text);
		}

		/// <summary>Deserializes an object from an XML element in a SOAP message that contains a reference to a <see langword="multiRef" /> element. </summary>
		/// <param name="fixupReference">An output string into which the <see langword="href" /> attribute value is read.</param>
		/// <returns>The deserialized object.</returns>
		// Token: 0x06001DE5 RID: 7653 RVA: 0x000B0D63 File Offset: 0x000AEF63
		protected object ReadReferencingElement(out string fixupReference)
		{
			return this.ReadReferencingElement(null, null, out fixupReference);
		}

		/// <summary>Deserializes an object from an XML element in a SOAP message that contains a reference to a <see langword="multiRef" /> element. </summary>
		/// <param name="name">The local name of the element's XML Schema data type.</param>
		/// <param name="ns">The namespace of the element's XML Schema data type.</param>
		/// <param name="fixupReference">An output string into which the <see langword="href" /> attribute value is read.</param>
		/// <returns>The deserialized object.</returns>
		// Token: 0x06001DE6 RID: 7654 RVA: 0x000B0D6E File Offset: 0x000AEF6E
		protected object ReadReferencingElement(string name, string ns, out string fixupReference)
		{
			return this.ReadReferencingElement(name, ns, false, out fixupReference);
		}

		/// <summary>Deserializes an object from an XML element in a SOAP message that contains a reference to a <see langword="multiRef" /> element.</summary>
		/// <param name="name">The local name of the element's XML Schema data type.</param>
		/// <param name="ns">The namespace of the element's XML Schema data type.</param>
		/// <param name="elementCanBeType">
		///       <see langword="true" /> if the element name is also the XML Schema data type name; otherwise, <see langword="false" />.</param>
		/// <param name="fixupReference">An output string into which the value of the <see langword="href" /> attribute is read.</param>
		/// <returns>The deserialized object.</returns>
		// Token: 0x06001DE7 RID: 7655 RVA: 0x000B0D7C File Offset: 0x000AEF7C
		protected object ReadReferencingElement(string name, string ns, bool elementCanBeType, out string fixupReference)
		{
			if (this.callbacks == null)
			{
				this.callbacks = new Hashtable();
				this.types = new Hashtable();
				XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(this.urTypeID, this.r.NameTable.Add("http://www.w3.org/2001/XMLSchema"));
				this.types.Add(xmlQualifiedName, typeof(object));
				this.typesReverse = new Hashtable();
				this.typesReverse.Add(typeof(object), xmlQualifiedName);
				this.InitCallbacks();
			}
			this.r.MoveToContent();
			if (this.ReadReference(out fixupReference))
			{
				return null;
			}
			if (this.ReadNull())
			{
				return null;
			}
			string id = this.soap12 ? this.r.GetAttribute("id", "http://www.w3.org/2003/05/soap-encoding") : this.r.GetAttribute("id", null);
			object obj;
			if ((obj = this.ReadArray(name, ns)) == null)
			{
				XmlQualifiedName xmlQualifiedName2 = this.GetXsiType();
				if (xmlQualifiedName2 == null)
				{
					if (name == null)
					{
						xmlQualifiedName2 = new XmlQualifiedName(this.r.NameTable.Add(this.r.LocalName), this.r.NameTable.Add(this.r.NamespaceURI));
					}
					else
					{
						xmlQualifiedName2 = new XmlQualifiedName(this.r.NameTable.Add(name), this.r.NameTable.Add(ns));
					}
				}
				XmlSerializationReadCallback xmlSerializationReadCallback = (XmlSerializationReadCallback)this.callbacks[xmlQualifiedName2];
				if (xmlSerializationReadCallback != null)
				{
					obj = xmlSerializationReadCallback();
				}
				else
				{
					obj = this.ReadTypedPrimitive(xmlQualifiedName2, elementCanBeType);
				}
			}
			this.AddTarget(id, obj);
			return obj;
		}

		/// <summary>Stores an implementation of the <see cref="T:System.Xml.Serialization.XmlSerializationReadCallback" /> delegate and its input data for a later invocation. </summary>
		/// <param name="name">The name of the .NET Framework type that is being deserialized.</param>
		/// <param name="ns">The namespace of the .NET Framework type that is being deserialized.</param>
		/// <param name="type">The <see cref="T:System.Type" /> to be deserialized.</param>
		/// <param name="read">An <see cref="T:System.Xml.Serialization.XmlSerializationReadCallback" /> delegate.</param>
		// Token: 0x06001DE8 RID: 7656 RVA: 0x000B0F18 File Offset: 0x000AF118
		protected void AddReadCallback(string name, string ns, Type type, XmlSerializationReadCallback read)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(this.r.NameTable.Add(name), this.r.NameTable.Add(ns));
			this.callbacks[xmlQualifiedName] = read;
			this.types[xmlQualifiedName] = type;
			this.typesReverse[type] = xmlQualifiedName;
		}

		/// <summary>Makes the <see cref="T:System.Xml.XmlReader" /> read an XML end tag.</summary>
		// Token: 0x06001DE9 RID: 7657 RVA: 0x000B0F78 File Offset: 0x000AF178
		protected void ReadEndElement()
		{
			while (this.r.NodeType == XmlNodeType.Whitespace)
			{
				this.r.Skip();
			}
			if (this.r.NodeType == XmlNodeType.None)
			{
				this.r.Skip();
				return;
			}
			this.r.ReadEndElement();
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x000B0FC8 File Offset: 0x000AF1C8
		private object ReadXmlNodes(bool elementCanBeType)
		{
			ArrayList arrayList = new ArrayList();
			string localName = this.Reader.LocalName;
			string namespaceURI = this.Reader.NamespaceURI;
			string name = this.Reader.Name;
			string text = null;
			string text2 = null;
			int num = 0;
			int num2 = -1;
			int num3 = -1;
			XmlNode xmlNode;
			if (this.Reader.NodeType == XmlNodeType.Attribute)
			{
				XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, namespaceURI);
				xmlAttribute.Value = this.Reader.Value;
				xmlNode = xmlAttribute;
			}
			else
			{
				xmlNode = this.Document.CreateElement(name, namespaceURI);
			}
			this.GetCurrentPosition(out num2, out num3);
			XmlElement xmlElement = xmlNode as XmlElement;
			while (this.Reader.MoveToNextAttribute())
			{
				if (this.IsXmlnsAttribute(this.Reader.Name) || (this.Reader.Name == "id" && (!this.soap12 || this.Reader.NamespaceURI == "http://www.w3.org/2003/05/soap-encoding")))
				{
					num++;
				}
				if (this.Reader.LocalName == this.typeID && (this.Reader.NamespaceURI == this.instanceNsID || this.Reader.NamespaceURI == this.instanceNs2000ID || this.Reader.NamespaceURI == this.instanceNs1999ID))
				{
					string value = this.Reader.Value;
					int num4 = value.LastIndexOf(':');
					text = ((num4 >= 0) ? value.Substring(num4 + 1) : value);
					text2 = this.Reader.LookupNamespace((num4 >= 0) ? value.Substring(0, num4) : "");
				}
				XmlAttribute xmlAttribute2 = (XmlAttribute)this.Document.ReadNode(this.r);
				arrayList.Add(xmlAttribute2);
				if (xmlElement != null)
				{
					xmlElement.SetAttributeNode(xmlAttribute2);
				}
			}
			if (elementCanBeType && text == null)
			{
				text = localName;
				text2 = namespaceURI;
				XmlAttribute xmlAttribute3 = this.Document.CreateAttribute(this.typeID, this.instanceNsID);
				xmlAttribute3.Value = name;
				arrayList.Add(xmlAttribute3);
			}
			if (text == "anyType" && (text2 == this.schemaNsID || text2 == this.schemaNs1999ID || text2 == this.schemaNs2000ID))
			{
				num++;
			}
			this.Reader.MoveToElement();
			if (this.Reader.IsEmptyElement)
			{
				this.Reader.Skip();
			}
			else
			{
				this.Reader.ReadStartElement();
				this.Reader.MoveToContent();
				int num5 = 0;
				int readerCount = this.ReaderCount;
				while (this.Reader.NodeType != XmlNodeType.EndElement)
				{
					XmlNode xmlNode2 = this.Document.ReadNode(this.r);
					arrayList.Add(xmlNode2);
					if (xmlElement != null)
					{
						xmlElement.AppendChild(xmlNode2);
					}
					this.Reader.MoveToContent();
					this.CheckReaderCount(ref num5, ref readerCount);
				}
				this.ReadEndElement();
			}
			if (arrayList.Count <= num)
			{
				return new object();
			}
			object result = (XmlNode[])arrayList.ToArray(typeof(XmlNode));
			this.UnknownNode(xmlNode, null, null);
			return result;
		}

		/// <summary>Checks whether the deserializer has advanced.</summary>
		/// <param name="whileIterations">The current <see langword="count" /> in a while loop.</param>
		/// <param name="readerCount">The current <see cref="P:System.Xml.Serialization.XmlSerializationReader.ReaderCount" />. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.Serialization.XmlSerializationReader.ReaderCount" /> has not advanced. </exception>
		// Token: 0x06001DEB RID: 7659 RVA: 0x000B12CD File Offset: 0x000AF4CD
		protected void CheckReaderCount(ref int whileIterations, ref int readerCount)
		{
			if (XmlSerializationReader.checkDeserializeAdvances)
			{
				whileIterations++;
				if ((whileIterations & 128) == 128)
				{
					if (readerCount == this.ReaderCount)
					{
						throw new InvalidOperationException(Res.GetString("Internal error: deserialization failed to advance over underlying stream."));
					}
					readerCount = this.ReaderCount;
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializationReader" /> class.</summary>
		// Token: 0x06001DEC RID: 7660 RVA: 0x000B130D File Offset: 0x000AF50D
		protected XmlSerializationReader()
		{
		}

		// Token: 0x04001A66 RID: 6758
		private XmlReader r;

		// Token: 0x04001A67 RID: 6759
		private XmlCountingReader countingReader;

		// Token: 0x04001A68 RID: 6760
		private XmlDocument d;

		// Token: 0x04001A69 RID: 6761
		private Hashtable callbacks;

		// Token: 0x04001A6A RID: 6762
		private Hashtable types;

		// Token: 0x04001A6B RID: 6763
		private Hashtable typesReverse;

		// Token: 0x04001A6C RID: 6764
		private XmlDeserializationEvents events;

		// Token: 0x04001A6D RID: 6765
		private Hashtable targets;

		// Token: 0x04001A6E RID: 6766
		private Hashtable referencedTargets;

		// Token: 0x04001A6F RID: 6767
		private ArrayList targetsWithoutIds;

		// Token: 0x04001A70 RID: 6768
		private ArrayList fixups;

		// Token: 0x04001A71 RID: 6769
		private ArrayList collectionFixups;

		// Token: 0x04001A72 RID: 6770
		private bool soap12;

		// Token: 0x04001A73 RID: 6771
		private bool isReturnValue;

		// Token: 0x04001A74 RID: 6772
		private bool decodeName = true;

		// Token: 0x04001A75 RID: 6773
		private string schemaNsID;

		// Token: 0x04001A76 RID: 6774
		private string schemaNs1999ID;

		// Token: 0x04001A77 RID: 6775
		private string schemaNs2000ID;

		// Token: 0x04001A78 RID: 6776
		private string schemaNonXsdTypesNsID;

		// Token: 0x04001A79 RID: 6777
		private string instanceNsID;

		// Token: 0x04001A7A RID: 6778
		private string instanceNs2000ID;

		// Token: 0x04001A7B RID: 6779
		private string instanceNs1999ID;

		// Token: 0x04001A7C RID: 6780
		private string soapNsID;

		// Token: 0x04001A7D RID: 6781
		private string soap12NsID;

		// Token: 0x04001A7E RID: 6782
		private string schemaID;

		// Token: 0x04001A7F RID: 6783
		private string wsdlNsID;

		// Token: 0x04001A80 RID: 6784
		private string wsdlArrayTypeID;

		// Token: 0x04001A81 RID: 6785
		private string nullID;

		// Token: 0x04001A82 RID: 6786
		private string nilID;

		// Token: 0x04001A83 RID: 6787
		private string typeID;

		// Token: 0x04001A84 RID: 6788
		private string arrayTypeID;

		// Token: 0x04001A85 RID: 6789
		private string itemTypeID;

		// Token: 0x04001A86 RID: 6790
		private string arraySizeID;

		// Token: 0x04001A87 RID: 6791
		private string arrayID;

		// Token: 0x04001A88 RID: 6792
		private string urTypeID;

		// Token: 0x04001A89 RID: 6793
		private string stringID;

		// Token: 0x04001A8A RID: 6794
		private string intID;

		// Token: 0x04001A8B RID: 6795
		private string booleanID;

		// Token: 0x04001A8C RID: 6796
		private string shortID;

		// Token: 0x04001A8D RID: 6797
		private string longID;

		// Token: 0x04001A8E RID: 6798
		private string floatID;

		// Token: 0x04001A8F RID: 6799
		private string doubleID;

		// Token: 0x04001A90 RID: 6800
		private string decimalID;

		// Token: 0x04001A91 RID: 6801
		private string dateTimeID;

		// Token: 0x04001A92 RID: 6802
		private string qnameID;

		// Token: 0x04001A93 RID: 6803
		private string dateID;

		// Token: 0x04001A94 RID: 6804
		private string timeID;

		// Token: 0x04001A95 RID: 6805
		private string hexBinaryID;

		// Token: 0x04001A96 RID: 6806
		private string base64BinaryID;

		// Token: 0x04001A97 RID: 6807
		private string base64ID;

		// Token: 0x04001A98 RID: 6808
		private string unsignedByteID;

		// Token: 0x04001A99 RID: 6809
		private string byteID;

		// Token: 0x04001A9A RID: 6810
		private string unsignedShortID;

		// Token: 0x04001A9B RID: 6811
		private string unsignedIntID;

		// Token: 0x04001A9C RID: 6812
		private string unsignedLongID;

		// Token: 0x04001A9D RID: 6813
		private string oldDecimalID;

		// Token: 0x04001A9E RID: 6814
		private string oldTimeInstantID;

		// Token: 0x04001A9F RID: 6815
		private string anyURIID;

		// Token: 0x04001AA0 RID: 6816
		private string durationID;

		// Token: 0x04001AA1 RID: 6817
		private string ENTITYID;

		// Token: 0x04001AA2 RID: 6818
		private string ENTITIESID;

		// Token: 0x04001AA3 RID: 6819
		private string gDayID;

		// Token: 0x04001AA4 RID: 6820
		private string gMonthID;

		// Token: 0x04001AA5 RID: 6821
		private string gMonthDayID;

		// Token: 0x04001AA6 RID: 6822
		private string gYearID;

		// Token: 0x04001AA7 RID: 6823
		private string gYearMonthID;

		// Token: 0x04001AA8 RID: 6824
		private string IDID;

		// Token: 0x04001AA9 RID: 6825
		private string IDREFID;

		// Token: 0x04001AAA RID: 6826
		private string IDREFSID;

		// Token: 0x04001AAB RID: 6827
		private string integerID;

		// Token: 0x04001AAC RID: 6828
		private string languageID;

		// Token: 0x04001AAD RID: 6829
		private string NameID;

		// Token: 0x04001AAE RID: 6830
		private string NCNameID;

		// Token: 0x04001AAF RID: 6831
		private string NMTOKENID;

		// Token: 0x04001AB0 RID: 6832
		private string NMTOKENSID;

		// Token: 0x04001AB1 RID: 6833
		private string negativeIntegerID;

		// Token: 0x04001AB2 RID: 6834
		private string nonPositiveIntegerID;

		// Token: 0x04001AB3 RID: 6835
		private string nonNegativeIntegerID;

		// Token: 0x04001AB4 RID: 6836
		private string normalizedStringID;

		// Token: 0x04001AB5 RID: 6837
		private string NOTATIONID;

		// Token: 0x04001AB6 RID: 6838
		private string positiveIntegerID;

		// Token: 0x04001AB7 RID: 6839
		private string tokenID;

		// Token: 0x04001AB8 RID: 6840
		private string charID;

		// Token: 0x04001AB9 RID: 6841
		private string guidID;

		// Token: 0x04001ABA RID: 6842
		private string timeSpanID;

		// Token: 0x04001ABB RID: 6843
		private static bool checkDeserializeAdvances;

		// Token: 0x020002EC RID: 748
		private struct SoapArrayInfo
		{
			// Token: 0x04001ABC RID: 6844
			public string qname;

			// Token: 0x04001ABD RID: 6845
			public int dimensions;

			// Token: 0x04001ABE RID: 6846
			public int length;

			// Token: 0x04001ABF RID: 6847
			public int jaggedDimensions;
		}

		/// <summary>Holds an <see cref="T:System.Xml.Serialization.XmlSerializationFixupCallback" /> delegate instance, plus the method's inputs; also serves as the parameter for the method.</summary>
		// Token: 0x020002ED RID: 749
		protected class Fixup
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializationReader.Fixup" /> class.</summary>
			/// <param name="o">The object that contains other objects whose values get filled in by the callback implementation.</param>
			/// <param name="callback">A method that instantiates the <see cref="T:System.Xml.Serialization.XmlSerializationFixupCallback" /> delegate.</param>
			/// <param name="count">The size of the string array obtained through the <see cref="P:System.Xml.Serialization.XmlSerializationReader.Fixup.Ids" /> property.</param>
			// Token: 0x06001DED RID: 7661 RVA: 0x000B131C File Offset: 0x000AF51C
			public Fixup(object o, XmlSerializationFixupCallback callback, int count) : this(o, callback, new string[count])
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializationReader.Fixup" /> class.</summary>
			/// <param name="o">The object that contains other objects whose values get filled in by the callback implementation.</param>
			/// <param name="callback">A method that instantiates the <see cref="T:System.Xml.Serialization.XmlSerializationFixupCallback" /> delegate.</param>
			/// <param name="ids">The string array obtained through the <see cref="P:System.Xml.Serialization.XmlSerializationReader.Fixup.Ids" /> property.</param>
			// Token: 0x06001DEE RID: 7662 RVA: 0x000B132C File Offset: 0x000AF52C
			public Fixup(object o, XmlSerializationFixupCallback callback, string[] ids)
			{
				this.callback = callback;
				this.Source = o;
				this.ids = ids;
			}

			/// <summary>Gets the callback method that creates an instance of the <see cref="T:System.Xml.Serialization.XmlSerializationFixupCallback" /> delegate.</summary>
			/// <returns>The callback method that creates an instance of the <see cref="T:System.Xml.Serialization.XmlSerializationFixupCallback" /> delegate.</returns>
			// Token: 0x170005E1 RID: 1505
			// (get) Token: 0x06001DEF RID: 7663 RVA: 0x000B1349 File Offset: 0x000AF549
			public XmlSerializationFixupCallback Callback
			{
				get
				{
					return this.callback;
				}
			}

			/// <summary>Gets or sets the object that contains other objects whose values get filled in by the callback implementation.</summary>
			/// <returns>The source containing objects with values to fill.</returns>
			// Token: 0x170005E2 RID: 1506
			// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x000B1351 File Offset: 0x000AF551
			// (set) Token: 0x06001DF1 RID: 7665 RVA: 0x000B1359 File Offset: 0x000AF559
			public object Source
			{
				get
				{
					return this.source;
				}
				set
				{
					this.source = value;
				}
			}

			/// <summary>Gets or sets an array of keys for the objects that belong to the <see cref="P:System.Xml.Serialization.XmlSerializationReader.Fixup.Source" /> property whose values get filled in by the callback implementation.</summary>
			/// <returns>The array of keys.</returns>
			// Token: 0x170005E3 RID: 1507
			// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x000B1362 File Offset: 0x000AF562
			public string[] Ids
			{
				get
				{
					return this.ids;
				}
			}

			// Token: 0x04001AC0 RID: 6848
			private XmlSerializationFixupCallback callback;

			// Token: 0x04001AC1 RID: 6849
			private object source;

			// Token: 0x04001AC2 RID: 6850
			private string[] ids;
		}

		/// <summary>Holds an <see cref="T:System.Xml.Serialization.XmlSerializationCollectionFixupCallback" /> delegate instance, plus the method's inputs; also supplies the method's parameters. </summary>
		// Token: 0x020002EE RID: 750
		protected class CollectionFixup
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializationReader.CollectionFixup" /> class with parameters for a callback method. </summary>
			/// <param name="collection">A collection into which the callback method copies the collection items array.</param>
			/// <param name="callback">A method that instantiates the <see cref="T:System.Xml.Serialization.XmlSerializationCollectionFixupCallback" /> delegate.</param>
			/// <param name="collectionItems">An array into which the callback method copies a collection.</param>
			// Token: 0x06001DF3 RID: 7667 RVA: 0x000B136A File Offset: 0x000AF56A
			public CollectionFixup(object collection, XmlSerializationCollectionFixupCallback callback, object collectionItems)
			{
				this.callback = callback;
				this.collection = collection;
				this.collectionItems = collectionItems;
			}

			/// <summary>Gets the callback method that instantiates the <see cref="T:System.Xml.Serialization.XmlSerializationCollectionFixupCallback" /> delegate. </summary>
			/// <returns>The <see cref="T:System.Xml.Serialization.XmlSerializationCollectionFixupCallback" /> delegate that points to the callback method.</returns>
			// Token: 0x170005E4 RID: 1508
			// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x000B1387 File Offset: 0x000AF587
			public XmlSerializationCollectionFixupCallback Callback
			{
				get
				{
					return this.callback;
				}
			}

			/// <summary>Gets the <paramref name="object collection" /> for the callback method. </summary>
			/// <returns>The collection that is used for the fixup.</returns>
			// Token: 0x170005E5 RID: 1509
			// (get) Token: 0x06001DF5 RID: 7669 RVA: 0x000B138F File Offset: 0x000AF58F
			public object Collection
			{
				get
				{
					return this.collection;
				}
			}

			/// <summary>Gets the array into which the callback method copies a collection. </summary>
			/// <returns>The array into which the callback method copies a collection.</returns>
			// Token: 0x170005E6 RID: 1510
			// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x000B1397 File Offset: 0x000AF597
			public object CollectionItems
			{
				get
				{
					return this.collectionItems;
				}
			}

			// Token: 0x04001AC3 RID: 6851
			private XmlSerializationCollectionFixupCallback callback;

			// Token: 0x04001AC4 RID: 6852
			private object collection;

			// Token: 0x04001AC5 RID: 6853
			private object collectionItems;
		}
	}
}
