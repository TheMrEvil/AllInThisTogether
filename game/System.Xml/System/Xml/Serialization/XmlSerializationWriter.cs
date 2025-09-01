using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	/// <summary>Represents an abstract class used for controlling serialization by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class.</summary>
	// Token: 0x020002F8 RID: 760
	public abstract class XmlSerializationWriter : XmlSerializationGeneratedCode
	{
		// Token: 0x06001EC8 RID: 7880 RVA: 0x000C0DF3 File Offset: 0x000BEFF3
		internal void Init(XmlWriter w, XmlSerializerNamespaces namespaces, string encodingStyle, string idBase, TempAssembly tempAssembly)
		{
			this.w = w;
			this.namespaces = namespaces;
			this.soap12 = (encodingStyle == "http://www.w3.org/2003/05/soap-encoding");
			this.idBase = idBase;
			base.Init(tempAssembly);
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="M:System.Xml.XmlConvert.EncodeName(System.String)" /> method is used to write valid XML.</summary>
		/// <returns>
		///     <see langword="true" /> if the <see cref="M:System.Xml.Serialization.XmlSerializationWriter.FromXmlQualifiedName(System.Xml.XmlQualifiedName)" /> method returns an encoded name; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x000C0E24 File Offset: 0x000BF024
		// (set) Token: 0x06001ECA RID: 7882 RVA: 0x000C0E2C File Offset: 0x000BF02C
		protected bool EscapeName
		{
			get
			{
				return this.escapeName;
			}
			set
			{
				this.escapeName = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.XmlWriter" /> that is being used by the <see cref="T:System.Xml.Serialization.XmlSerializationWriter" />.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlWriter" /> used by the class instance.</returns>
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001ECB RID: 7883 RVA: 0x000C0E35 File Offset: 0x000BF035
		// (set) Token: 0x06001ECC RID: 7884 RVA: 0x000C0E3D File Offset: 0x000BF03D
		protected XmlWriter Writer
		{
			get
			{
				return this.w;
			}
			set
			{
				this.w = value;
			}
		}

		/// <summary>Gets or sets a list of XML qualified name objects that contain the namespaces and prefixes used to produce qualified names in XML documents.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> that contains the namespaces and prefix pairs.</returns>
		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001ECD RID: 7885 RVA: 0x000C0E46 File Offset: 0x000BF046
		// (set) Token: 0x06001ECE RID: 7886 RVA: 0x000C0E60 File Offset: 0x000BF060
		protected ArrayList Namespaces
		{
			get
			{
				if (this.namespaces != null)
				{
					return this.namespaces.NamespaceList;
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.namespaces = null;
					return;
				}
				XmlQualifiedName[] array = (XmlQualifiedName[])value.ToArray(typeof(XmlQualifiedName));
				this.namespaces = new XmlSerializerNamespaces(array);
			}
		}

		/// <summary>Processes a base-64 byte array.</summary>
		/// <param name="value">A base-64 <see cref="T:System.Byte" /> array.</param>
		/// <returns>The same byte array that was passed in as an argument.</returns>
		// Token: 0x06001ECF RID: 7887 RVA: 0x00002068 File Offset: 0x00000268
		protected static byte[] FromByteArrayBase64(byte[] value)
		{
			return value;
		}

		/// <summary>Gets a dynamically generated assembly by name.</summary>
		/// <param name="assemblyFullName">The full name of the assembly.</param>
		/// <returns>A dynamically generated assembly.</returns>
		// Token: 0x06001ED0 RID: 7888 RVA: 0x000AD79D File Offset: 0x000AB99D
		protected static Assembly ResolveDynamicAssembly(string assemblyFullName)
		{
			return DynamicAssemblies.Get(assemblyFullName);
		}

		/// <summary>Produces a string from an input hexadecimal byte array.</summary>
		/// <param name="value">A hexadecimal byte array to translate to a string.</param>
		/// <returns>The byte array value converted to a string.</returns>
		// Token: 0x06001ED1 RID: 7889 RVA: 0x000C0E9A File Offset: 0x000BF09A
		protected static string FromByteArrayHex(byte[] value)
		{
			return XmlCustomFormatter.FromByteArrayHex(value);
		}

		/// <summary>Produces a string from an input <see cref="T:System.DateTime" />.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> to translate to a string.</param>
		/// <returns>A string representation of the <see cref="T:System.DateTime" /> that shows the date and time.</returns>
		// Token: 0x06001ED2 RID: 7890 RVA: 0x000C0EA2 File Offset: 0x000BF0A2
		protected static string FromDateTime(DateTime value)
		{
			return XmlCustomFormatter.FromDateTime(value);
		}

		/// <summary>Produces a string from a <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> to translate to a string.</param>
		/// <returns>A string representation of the <see cref="T:System.DateTime" /> that shows the date but no time.</returns>
		// Token: 0x06001ED3 RID: 7891 RVA: 0x000C0EAA File Offset: 0x000BF0AA
		protected static string FromDate(DateTime value)
		{
			return XmlCustomFormatter.FromDate(value);
		}

		/// <summary>Produces a string from a <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> that is translated to a string.</param>
		/// <returns>A string representation of the <see cref="T:System.DateTime" /> object that shows the time but no date.</returns>
		// Token: 0x06001ED4 RID: 7892 RVA: 0x000C0EB2 File Offset: 0x000BF0B2
		protected static string FromTime(DateTime value)
		{
			return XmlCustomFormatter.FromTime(value);
		}

		/// <summary>Produces a string from an input <see cref="T:System.Char" />.</summary>
		/// <param name="value">A <see cref="T:System.Char" /> to translate to a string.</param>
		/// <returns>The <see cref="T:System.Char" /> value converted to a string.</returns>
		// Token: 0x06001ED5 RID: 7893 RVA: 0x000C0EBA File Offset: 0x000BF0BA
		protected static string FromChar(char value)
		{
			return XmlCustomFormatter.FromChar(value);
		}

		/// <summary>Produces a string that consists of delimited identifiers that represent the enumeration members that have been set.</summary>
		/// <param name="value">The enumeration value as a series of bitwise <see langword="OR" /> operations.</param>
		/// <param name="values">The enumeration's name values.</param>
		/// <param name="ids">The enumeration's constant values.</param>
		/// <returns>A string that consists of delimited identifiers, where each represents a member from the set enumerator list.</returns>
		// Token: 0x06001ED6 RID: 7894 RVA: 0x000C0EC2 File Offset: 0x000BF0C2
		protected static string FromEnum(long value, string[] values, long[] ids)
		{
			return XmlCustomFormatter.FromEnum(value, values, ids, null);
		}

		/// <summary>Takes a numeric enumeration value and the names and constants from the enumerator list for the enumeration and returns a string that consists of delimited identifiers that represent the enumeration members that have been set.</summary>
		/// <param name="value">The enumeration value as a series of bitwise <see langword="OR" /> operations.</param>
		/// <param name="values">The values of the enumeration.</param>
		/// <param name="ids">The constants of the enumeration.</param>
		/// <param name="typeName">The name of the type </param>
		/// <returns>A string that consists of delimited identifiers, where each item is one of the values set by the bitwise operation.</returns>
		// Token: 0x06001ED7 RID: 7895 RVA: 0x000C0ECD File Offset: 0x000BF0CD
		protected static string FromEnum(long value, string[] values, long[] ids, string typeName)
		{
			return XmlCustomFormatter.FromEnum(value, values, ids, typeName);
		}

		/// <summary>Encodes a valid XML name by replacing characters that are not valid with escape sequences.</summary>
		/// <param name="name">A string to be used as an XML name.</param>
		/// <returns>An encoded string.</returns>
		// Token: 0x06001ED8 RID: 7896 RVA: 0x000C0ED8 File Offset: 0x000BF0D8
		protected static string FromXmlName(string name)
		{
			return XmlCustomFormatter.FromXmlName(name);
		}

		/// <summary>Encodes a valid XML local name by replacing characters that are not valid with escape sequences.</summary>
		/// <param name="ncName">A string to be used as a local (unqualified) XML name.</param>
		/// <returns>An encoded string.</returns>
		// Token: 0x06001ED9 RID: 7897 RVA: 0x000C0EE0 File Offset: 0x000BF0E0
		protected static string FromXmlNCName(string ncName)
		{
			return XmlCustomFormatter.FromXmlNCName(ncName);
		}

		/// <summary>Encodes an XML name.</summary>
		/// <param name="nmToken">An XML name to be encoded.</param>
		/// <returns>An encoded string.</returns>
		// Token: 0x06001EDA RID: 7898 RVA: 0x000C0EE8 File Offset: 0x000BF0E8
		protected static string FromXmlNmToken(string nmToken)
		{
			return XmlCustomFormatter.FromXmlNmToken(nmToken);
		}

		/// <summary>Encodes a space-delimited sequence of XML names into a single XML name.</summary>
		/// <param name="nmTokens">A space-delimited sequence of XML names to be encoded.</param>
		/// <returns>An encoded string.</returns>
		// Token: 0x06001EDB RID: 7899 RVA: 0x000C0EF0 File Offset: 0x000BF0F0
		protected static string FromXmlNmTokens(string nmTokens)
		{
			return XmlCustomFormatter.FromXmlNmTokens(nmTokens);
		}

		/// <summary>Writes an <see langword="xsi:type" /> attribute for an XML element that is being serialized into a document.</summary>
		/// <param name="name">The local name of an XML Schema data type.</param>
		/// <param name="ns">The namespace of an XML Schema data type.</param>
		// Token: 0x06001EDC RID: 7900 RVA: 0x000C0EF8 File Offset: 0x000BF0F8
		protected void WriteXsiType(string name, string ns)
		{
			this.WriteAttribute("type", "http://www.w3.org/2001/XMLSchema-instance", this.GetQualifiedName(name, ns));
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x000C0F12 File Offset: 0x000BF112
		private XmlQualifiedName GetPrimitiveTypeName(Type type)
		{
			return this.GetPrimitiveTypeName(type, true);
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x000C0F1C File Offset: 0x000BF11C
		private XmlQualifiedName GetPrimitiveTypeName(Type type, bool throwIfUnknown)
		{
			XmlQualifiedName primitiveTypeNameInternal = XmlSerializationWriter.GetPrimitiveTypeNameInternal(type);
			if (throwIfUnknown && primitiveTypeNameInternal == null)
			{
				throw this.CreateUnknownTypeException(type);
			}
			return primitiveTypeNameInternal;
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x000C0F48 File Offset: 0x000BF148
		internal static XmlQualifiedName GetPrimitiveTypeNameInternal(Type type)
		{
			string ns = "http://www.w3.org/2001/XMLSchema";
			string name;
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				name = "boolean";
				goto IL_196;
			case TypeCode.Char:
				name = "char";
				ns = "http://microsoft.com/wsdl/types/";
				goto IL_196;
			case TypeCode.SByte:
				name = "byte";
				goto IL_196;
			case TypeCode.Byte:
				name = "unsignedByte";
				goto IL_196;
			case TypeCode.Int16:
				name = "short";
				goto IL_196;
			case TypeCode.UInt16:
				name = "unsignedShort";
				goto IL_196;
			case TypeCode.Int32:
				name = "int";
				goto IL_196;
			case TypeCode.UInt32:
				name = "unsignedInt";
				goto IL_196;
			case TypeCode.Int64:
				name = "long";
				goto IL_196;
			case TypeCode.UInt64:
				name = "unsignedLong";
				goto IL_196;
			case TypeCode.Single:
				name = "float";
				goto IL_196;
			case TypeCode.Double:
				name = "double";
				goto IL_196;
			case TypeCode.Decimal:
				name = "decimal";
				goto IL_196;
			case TypeCode.DateTime:
				name = "dateTime";
				goto IL_196;
			case TypeCode.String:
				name = "string";
				goto IL_196;
			}
			if (type == typeof(XmlQualifiedName))
			{
				name = "QName";
			}
			else if (type == typeof(byte[]))
			{
				name = "base64Binary";
			}
			else if (type == typeof(TimeSpan) && LocalAppContextSwitches.EnableTimeSpanSerialization)
			{
				name = "TimeSpan";
			}
			else if (type == typeof(Guid))
			{
				name = "guid";
				ns = "http://microsoft.com/wsdl/types/";
			}
			else
			{
				if (!(type == typeof(XmlNode[])))
				{
					return null;
				}
				name = "anyType";
			}
			IL_196:
			return new XmlQualifiedName(name, ns);
		}

		/// <summary>Writes an XML element whose text body is a value of a simple XML Schema data type.</summary>
		/// <param name="name">The local name of the element to write.</param>
		/// <param name="ns">The namespace of the element to write.</param>
		/// <param name="o">The object to be serialized in the element body.</param>
		/// <param name="xsiType">
		///       <see langword="true" /> if the XML element explicitly specifies the text value's type using the <see langword="xsi:type" /> attribute; otherwise, <see langword="false" />.</param>
		// Token: 0x06001EE0 RID: 7904 RVA: 0x000C10F4 File Offset: 0x000BF2F4
		protected void WriteTypedPrimitive(string name, string ns, object o, bool xsiType)
		{
			string ns2 = "http://www.w3.org/2001/XMLSchema";
			bool flag = true;
			bool flag2 = false;
			Type type = o.GetType();
			bool flag3 = false;
			string text;
			string text2;
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				text = XmlConvert.ToString((bool)o);
				text2 = "boolean";
				goto IL_322;
			case TypeCode.Char:
				text = XmlSerializationWriter.FromChar((char)o);
				text2 = "char";
				ns2 = "http://microsoft.com/wsdl/types/";
				goto IL_322;
			case TypeCode.SByte:
				text = XmlConvert.ToString((sbyte)o);
				text2 = "byte";
				goto IL_322;
			case TypeCode.Byte:
				text = XmlConvert.ToString((byte)o);
				text2 = "unsignedByte";
				goto IL_322;
			case TypeCode.Int16:
				text = XmlConvert.ToString((short)o);
				text2 = "short";
				goto IL_322;
			case TypeCode.UInt16:
				text = XmlConvert.ToString((ushort)o);
				text2 = "unsignedShort";
				goto IL_322;
			case TypeCode.Int32:
				text = XmlConvert.ToString((int)o);
				text2 = "int";
				goto IL_322;
			case TypeCode.UInt32:
				text = XmlConvert.ToString((uint)o);
				text2 = "unsignedInt";
				goto IL_322;
			case TypeCode.Int64:
				text = XmlConvert.ToString((long)o);
				text2 = "long";
				goto IL_322;
			case TypeCode.UInt64:
				text = XmlConvert.ToString((ulong)o);
				text2 = "unsignedLong";
				goto IL_322;
			case TypeCode.Single:
				text = XmlConvert.ToString((float)o);
				text2 = "float";
				goto IL_322;
			case TypeCode.Double:
				text = XmlConvert.ToString((double)o);
				text2 = "double";
				goto IL_322;
			case TypeCode.Decimal:
				text = XmlConvert.ToString((decimal)o);
				text2 = "decimal";
				goto IL_322;
			case TypeCode.DateTime:
				text = XmlSerializationWriter.FromDateTime((DateTime)o);
				text2 = "dateTime";
				goto IL_322;
			case TypeCode.String:
				text = (string)o;
				text2 = "string";
				flag = false;
				goto IL_322;
			}
			if (type == typeof(XmlQualifiedName))
			{
				text2 = "QName";
				flag3 = true;
				if (name == null)
				{
					this.w.WriteStartElement(text2, ns2);
				}
				else
				{
					this.w.WriteStartElement(name, ns);
				}
				text = this.FromXmlQualifiedName((XmlQualifiedName)o, false);
			}
			else if (type == typeof(byte[]))
			{
				text = string.Empty;
				flag2 = true;
				text2 = "base64Binary";
			}
			else if (type == typeof(Guid))
			{
				text = XmlConvert.ToString((Guid)o);
				text2 = "guid";
				ns2 = "http://microsoft.com/wsdl/types/";
			}
			else if (type == typeof(TimeSpan) && LocalAppContextSwitches.EnableTimeSpanSerialization)
			{
				text = XmlConvert.ToString((TimeSpan)o);
				text2 = "TimeSpan";
			}
			else
			{
				if (typeof(XmlNode[]).IsAssignableFrom(type))
				{
					if (name == null)
					{
						this.w.WriteStartElement("anyType", "http://www.w3.org/2001/XMLSchema");
					}
					else
					{
						this.w.WriteStartElement(name, ns);
					}
					XmlNode[] array = (XmlNode[])o;
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != null)
						{
							array[i].WriteTo(this.w);
						}
					}
					this.w.WriteEndElement();
					return;
				}
				throw this.CreateUnknownTypeException(type);
			}
			IL_322:
			if (!flag3)
			{
				if (name == null)
				{
					this.w.WriteStartElement(text2, ns2);
				}
				else
				{
					this.w.WriteStartElement(name, ns);
				}
			}
			if (xsiType)
			{
				this.WriteXsiType(text2, ns2);
			}
			if (text == null)
			{
				this.w.WriteAttributeString("nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
			}
			else if (flag2)
			{
				XmlCustomFormatter.WriteArrayBase64(this.w, (byte[])o, 0, ((byte[])o).Length);
			}
			else if (flag)
			{
				this.w.WriteRaw(text);
			}
			else
			{
				this.w.WriteString(text);
			}
			this.w.WriteEndElement();
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x000C14BC File Offset: 0x000BF6BC
		private string GetQualifiedName(string name, string ns)
		{
			if (ns == null || ns.Length == 0)
			{
				return name;
			}
			string text = this.w.LookupPrefix(ns);
			if (text == null)
			{
				if (ns == "http://www.w3.org/XML/1998/namespace")
				{
					text = "xml";
				}
				else
				{
					text = this.NextPrefix();
					this.WriteAttribute("xmlns", text, null, ns);
				}
			}
			else if (text.Length == 0)
			{
				return name;
			}
			return text + ":" + name;
		}

		/// <summary>Returns an XML qualified name, with invalid characters replaced by escape sequences.</summary>
		/// <param name="xmlQualifiedName">An <see cref="T:System.Xml.XmlQualifiedName" /> that represents the XML to be written.</param>
		/// <returns>An XML qualified name, with invalid characters replaced by escape sequences.</returns>
		// Token: 0x06001EE2 RID: 7906 RVA: 0x000C1528 File Offset: 0x000BF728
		protected string FromXmlQualifiedName(XmlQualifiedName xmlQualifiedName)
		{
			return this.FromXmlQualifiedName(xmlQualifiedName, true);
		}

		/// <summary>Produces a string that can be written as an XML qualified name, with invalid characters replaced by escape sequences.</summary>
		/// <param name="xmlQualifiedName">An <see cref="T:System.Xml.XmlQualifiedName" /> that represents the XML to be written.</param>
		/// <param name="ignoreEmpty">
		///       <see langword="true" /> to ignore empty spaces in the string; otherwise, <see langword="false" />.</param>
		/// <returns>An XML qualified name, with invalid characters replaced by escape sequences.</returns>
		// Token: 0x06001EE3 RID: 7907 RVA: 0x000C1532 File Offset: 0x000BF732
		protected string FromXmlQualifiedName(XmlQualifiedName xmlQualifiedName, bool ignoreEmpty)
		{
			if (xmlQualifiedName == null)
			{
				return null;
			}
			if (xmlQualifiedName.IsEmpty && ignoreEmpty)
			{
				return null;
			}
			return this.GetQualifiedName(this.EscapeName ? XmlConvert.EncodeLocalName(xmlQualifiedName.Name) : xmlQualifiedName.Name, xmlQualifiedName.Namespace);
		}

		/// <summary>Writes an opening element tag, including any attributes.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		// Token: 0x06001EE4 RID: 7908 RVA: 0x000C1572 File Offset: 0x000BF772
		protected void WriteStartElement(string name)
		{
			this.WriteStartElement(name, null, null, false, null);
		}

		/// <summary>Writes an opening element tag, including any attributes.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		// Token: 0x06001EE5 RID: 7909 RVA: 0x000C157F File Offset: 0x000BF77F
		protected void WriteStartElement(string name, string ns)
		{
			this.WriteStartElement(name, ns, null, false, null);
		}

		/// <summary>Writes an opening element tag, including any attributes.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="writePrefixed">
		///       <see langword="true" /> to write the element name with a prefix if none is available for the specified namespace; otherwise, <see langword="false" />.</param>
		// Token: 0x06001EE6 RID: 7910 RVA: 0x000C158C File Offset: 0x000BF78C
		protected void WriteStartElement(string name, string ns, bool writePrefixed)
		{
			this.WriteStartElement(name, ns, null, writePrefixed, null);
		}

		/// <summary>Writes an opening element tag, including any attributes.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized as an XML element.</param>
		// Token: 0x06001EE7 RID: 7911 RVA: 0x000C1599 File Offset: 0x000BF799
		protected void WriteStartElement(string name, string ns, object o)
		{
			this.WriteStartElement(name, ns, o, false, null);
		}

		/// <summary>Writes an opening element tag, including any attributes.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized as an XML element.</param>
		/// <param name="writePrefixed">
		///       <see langword="true" /> to write the element name with a prefix if none is available for the specified namespace; otherwise, <see langword="false" />.</param>
		// Token: 0x06001EE8 RID: 7912 RVA: 0x000C15A6 File Offset: 0x000BF7A6
		protected void WriteStartElement(string name, string ns, object o, bool writePrefixed)
		{
			this.WriteStartElement(name, ns, o, writePrefixed, null);
		}

		/// <summary>Writes an opening element tag, including any attributes.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized as an XML element.</param>
		/// <param name="writePrefixed">
		///       <see langword="true" /> to write the element name with a prefix if none is available for the specified namespace; otherwise, <see langword="false" />.</param>
		/// <param name="xmlns">An instance of the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> class that contains prefix and namespace pairs to be used in the generated XML.</param>
		// Token: 0x06001EE9 RID: 7913 RVA: 0x000C15B4 File Offset: 0x000BF7B4
		protected void WriteStartElement(string name, string ns, object o, bool writePrefixed, XmlSerializerNamespaces xmlns)
		{
			if (o != null && this.objectsInUse != null)
			{
				if (this.objectsInUse.ContainsKey(o))
				{
					throw new InvalidOperationException(Res.GetString("A circular reference was detected while serializing an object of type {0}.", new object[]
					{
						o.GetType().FullName
					}));
				}
				this.objectsInUse.Add(o, o);
			}
			string text = null;
			bool flag = false;
			if (this.namespaces != null)
			{
				foreach (object obj in this.namespaces.Namespaces.Keys)
				{
					string text2 = (string)obj;
					string text3 = (string)this.namespaces.Namespaces[text2];
					if (text2.Length > 0 && text3 == ns)
					{
						text = text2;
					}
					if (text2.Length == 0)
					{
						if (text3 == null || text3.Length == 0)
						{
							flag = true;
						}
						if (ns != text3)
						{
							writePrefixed = true;
						}
					}
				}
				this.usedPrefixes = this.ListUsedPrefixes(this.namespaces.Namespaces, this.aliasBase);
			}
			if (writePrefixed && text == null && ns != null && ns.Length > 0)
			{
				text = this.w.LookupPrefix(ns);
				if (text == null || text.Length == 0)
				{
					text = this.NextPrefix();
				}
			}
			if (text == null && xmlns != null)
			{
				text = xmlns.LookupPrefix(ns);
			}
			if (flag && text == null && ns != null && ns.Length != 0)
			{
				text = this.NextPrefix();
			}
			this.w.WriteStartElement(text, name, ns);
			if (this.namespaces != null)
			{
				foreach (object obj2 in this.namespaces.Namespaces.Keys)
				{
					string text4 = (string)obj2;
					string text5 = (string)this.namespaces.Namespaces[text4];
					if (text4.Length != 0 || (text5 != null && text5.Length != 0))
					{
						if (text5 == null || text5.Length == 0)
						{
							if (text4.Length > 0)
							{
								throw new InvalidOperationException(Res.GetString("Invalid namespace attribute: xmlns:{0}=\"\".", new object[]
								{
									text4
								}));
							}
							this.WriteAttribute("xmlns", text4, null, text5);
						}
						else if (this.w.LookupPrefix(text5) == null)
						{
							if (text == null && text4.Length == 0)
							{
								break;
							}
							this.WriteAttribute("xmlns", text4, null, text5);
						}
					}
				}
			}
			this.WriteNamespaceDeclarations(xmlns);
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x000C184C File Offset: 0x000BFA4C
		private Hashtable ListUsedPrefixes(Hashtable nsList, string prefix)
		{
			Hashtable hashtable = new Hashtable();
			int length = prefix.Length;
			foreach (object obj in this.namespaces.Namespaces.Keys)
			{
				string text = (string)obj;
				if (text.Length > length)
				{
					string text2 = text;
					int length2 = text2.Length;
					if (text2.Length > length && text2.Length <= length + "2147483647".Length && text2.StartsWith(prefix, StringComparison.Ordinal))
					{
						bool flag = true;
						for (int i = length; i < text2.Length; i++)
						{
							if (!char.IsDigit(text2, i))
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							long num = long.Parse(text2.Substring(length), CultureInfo.InvariantCulture);
							if (num <= 2147483647L)
							{
								int num2 = (int)num;
								if (!hashtable.ContainsKey(num2))
								{
									hashtable.Add(num2, num2);
								}
							}
						}
					}
				}
			}
			if (hashtable.Count > 0)
			{
				return hashtable;
			}
			return null;
		}

		/// <summary>Writes an XML element with an <see langword="xsi:nil='true'" /> attribute.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		// Token: 0x06001EEB RID: 7915 RVA: 0x000C1984 File Offset: 0x000BFB84
		protected void WriteNullTagEncoded(string name)
		{
			this.WriteNullTagEncoded(name, null);
		}

		/// <summary>Writes an XML element with an <see langword="xsi:nil='true'" /> attribute.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		// Token: 0x06001EEC RID: 7916 RVA: 0x000C198E File Offset: 0x000BFB8E
		protected void WriteNullTagEncoded(string name, string ns)
		{
			if (name == null || name.Length == 0)
			{
				return;
			}
			this.WriteStartElement(name, ns, null, true);
			this.w.WriteAttributeString("nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
			this.w.WriteEndElement();
		}

		/// <summary>Writes an XML element with an <see langword="xsi:nil='true'" /> attribute.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		// Token: 0x06001EED RID: 7917 RVA: 0x000C19CB File Offset: 0x000BFBCB
		protected void WriteNullTagLiteral(string name)
		{
			this.WriteNullTagLiteral(name, null);
		}

		/// <summary>Writes an XML element with an <see langword="xsi:nil='true'" /> attribute.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		// Token: 0x06001EEE RID: 7918 RVA: 0x000C19D5 File Offset: 0x000BFBD5
		protected void WriteNullTagLiteral(string name, string ns)
		{
			if (name == null || name.Length == 0)
			{
				return;
			}
			this.WriteStartElement(name, ns, null, false);
			this.w.WriteAttributeString("nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
			this.w.WriteEndElement();
		}

		/// <summary>Writes an XML element whose body is empty.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		// Token: 0x06001EEF RID: 7919 RVA: 0x000C1A12 File Offset: 0x000BFC12
		protected void WriteEmptyTag(string name)
		{
			this.WriteEmptyTag(name, null);
		}

		/// <summary>Writes an XML element whose body is empty.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		// Token: 0x06001EF0 RID: 7920 RVA: 0x000C1A1C File Offset: 0x000BFC1C
		protected void WriteEmptyTag(string name, string ns)
		{
			if (name == null || name.Length == 0)
			{
				return;
			}
			this.WriteStartElement(name, ns, null, false);
			this.w.WriteEndElement();
		}

		/// <summary>Writes a <see langword="&lt;closing&gt;" /> element tag.</summary>
		// Token: 0x06001EF1 RID: 7921 RVA: 0x000C1A3F File Offset: 0x000BFC3F
		protected void WriteEndElement()
		{
			this.w.WriteEndElement();
		}

		/// <summary>Writes a <see langword="&lt;closing&gt;" /> element tag.</summary>
		/// <param name="o">The object being serialized.</param>
		// Token: 0x06001EF2 RID: 7922 RVA: 0x000C1A4C File Offset: 0x000BFC4C
		protected void WriteEndElement(object o)
		{
			this.w.WriteEndElement();
			if (o != null && this.objectsInUse != null)
			{
				this.objectsInUse.Remove(o);
			}
		}

		/// <summary>Writes an object that uses custom XML formatting as an XML element.</summary>
		/// <param name="serializable">An object that implements the <see cref="T:System.Xml.Serialization.IXmlSerializable" /> interface that uses custom XML formatting.</param>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="isNullable">
		///       <see langword="true" /> to write an <see langword="xsi:nil='true'" /> attribute if the <see cref="T:System.Xml.Serialization.IXmlSerializable" /> class object is <see langword="null" />; otherwise, <see langword="false" />.</param>
		// Token: 0x06001EF3 RID: 7923 RVA: 0x000C1A70 File Offset: 0x000BFC70
		protected void WriteSerializable(IXmlSerializable serializable, string name, string ns, bool isNullable)
		{
			this.WriteSerializable(serializable, name, ns, isNullable, true);
		}

		/// <summary>Instructs <see cref="T:System.Xml.XmlNode" /> to write an object that uses custom XML formatting as an XML element.</summary>
		/// <param name="serializable">An object that implements the <see cref="T:System.Xml.Serialization.IXmlSerializable" /> interface that uses custom XML formatting.</param>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="isNullable">
		///       <see langword="true" /> to write an <see langword="xsi:nil='true'" /> attribute if the <see cref="T:System.Xml.Serialization.IXmlSerializable" /> object is <see langword="null" />; otherwise, <see langword="false" />.</param>
		/// <param name="wrapped">
		///       <see langword="true" /> to ignore writing the opening element tag; otherwise, <see langword="false" /> to write the opening element tag.</param>
		// Token: 0x06001EF4 RID: 7924 RVA: 0x000C1A7E File Offset: 0x000BFC7E
		protected void WriteSerializable(IXmlSerializable serializable, string name, string ns, bool isNullable, bool wrapped)
		{
			if (serializable == null)
			{
				if (isNullable)
				{
					this.WriteNullTagLiteral(name, ns);
				}
				return;
			}
			if (wrapped)
			{
				this.w.WriteStartElement(name, ns);
			}
			serializable.WriteXml(this.w);
			if (wrapped)
			{
				this.w.WriteEndElement();
			}
		}

		/// <summary>Writes an XML element that contains a string as the body. <see cref="T:System.Xml.XmlWriter" /> inserts an <see langword="xsi:nil='true'" /> attribute if the string's value is <see langword="null" />.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The string to write in the body of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the <see langword="xsi:type" /> attribute.</param>
		// Token: 0x06001EF5 RID: 7925 RVA: 0x000C1ABC File Offset: 0x000BFCBC
		protected void WriteNullableStringEncoded(string name, string ns, string value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				this.WriteNullTagEncoded(name, ns);
				return;
			}
			this.WriteElementString(name, ns, value, xsiType);
		}

		/// <summary>Writes an XML element that contains a string as the body. <see cref="T:System.Xml.XmlWriter" /> inserts an <see langword="xsi:nil='true'" /> attribute if the string's value is <see langword="null" />.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The string to write in the body of the XML element.</param>
		// Token: 0x06001EF6 RID: 7926 RVA: 0x000C1AD5 File Offset: 0x000BFCD5
		protected void WriteNullableStringLiteral(string name, string ns, string value)
		{
			if (value == null)
			{
				this.WriteNullTagLiteral(name, ns);
				return;
			}
			this.WriteElementString(name, ns, value, null);
		}

		/// <summary>Writes an XML element that contains a string as the body. <see cref="T:System.Xml.XmlWriter" /> inserts an <see langword="xsi:nil='true'" /> attribute if the string's value is <see langword="null" />.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The string to write in the body of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the <see langword="xsi:type" /> attribute.</param>
		// Token: 0x06001EF7 RID: 7927 RVA: 0x000C1AED File Offset: 0x000BFCED
		protected void WriteNullableStringEncodedRaw(string name, string ns, string value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				this.WriteNullTagEncoded(name, ns);
				return;
			}
			this.WriteElementStringRaw(name, ns, value, xsiType);
		}

		/// <summary>Writes a byte array as the body of an XML element. <see cref="T:System.Xml.XmlWriter" /> inserts an <see langword="xsi:nil='true'" /> attribute if the string's value is <see langword="null" />.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The byte array to write in the body of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the <see langword="xsi:type" /> attribute.</param>
		// Token: 0x06001EF8 RID: 7928 RVA: 0x000C1B06 File Offset: 0x000BFD06
		protected void WriteNullableStringEncodedRaw(string name, string ns, byte[] value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				this.WriteNullTagEncoded(name, ns);
				return;
			}
			this.WriteElementStringRaw(name, ns, value, xsiType);
		}

		/// <summary>Writes an XML element that contains a string as the body. <see cref="T:System.Xml.XmlWriter" /> inserts a <see langword="xsi:nil='true'" /> attribute if the string's value is <see langword="null" />.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The string to write in the body of the XML element.</param>
		// Token: 0x06001EF9 RID: 7929 RVA: 0x000C1B1F File Offset: 0x000BFD1F
		protected void WriteNullableStringLiteralRaw(string name, string ns, string value)
		{
			if (value == null)
			{
				this.WriteNullTagLiteral(name, ns);
				return;
			}
			this.WriteElementStringRaw(name, ns, value, null);
		}

		/// <summary>Writes a byte array as the body of an XML element. <see cref="T:System.Xml.XmlWriter" /> inserts an <see langword="xsi:nil='true'" /> attribute if the string's value is <see langword="null" />.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The byte array to write in the body of the XML element.</param>
		// Token: 0x06001EFA RID: 7930 RVA: 0x000C1B37 File Offset: 0x000BFD37
		protected void WriteNullableStringLiteralRaw(string name, string ns, byte[] value)
		{
			if (value == null)
			{
				this.WriteNullTagLiteral(name, ns);
				return;
			}
			this.WriteElementStringRaw(name, ns, value, null);
		}

		/// <summary>Writes an XML element whose body contains a valid XML qualified name. <see cref="T:System.Xml.XmlWriter" /> inserts an <see langword="xsi:nil='true'" /> attribute if the string's value is <see langword="null" />.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The XML qualified name to write in the body of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the <see langword="xsi:type" /> attribute.</param>
		// Token: 0x06001EFB RID: 7931 RVA: 0x000C1B4F File Offset: 0x000BFD4F
		protected void WriteNullableQualifiedNameEncoded(string name, string ns, XmlQualifiedName value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				this.WriteNullTagEncoded(name, ns);
				return;
			}
			this.WriteElementQualifiedName(name, ns, value, xsiType);
		}

		/// <summary>Writes an XML element whose body contains a valid XML qualified name. <see cref="T:System.Xml.XmlWriter" /> inserts an <see langword="xsi:nil='true'" /> attribute if the string's value is <see langword="null" />.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The XML qualified name to write in the body of the XML element.</param>
		// Token: 0x06001EFC RID: 7932 RVA: 0x000C1B6E File Offset: 0x000BFD6E
		protected void WriteNullableQualifiedNameLiteral(string name, string ns, XmlQualifiedName value)
		{
			if (value == null)
			{
				this.WriteNullTagLiteral(name, ns);
				return;
			}
			this.WriteElementQualifiedName(name, ns, value, null);
		}

		/// <summary>Writes an XML node object within the body of a named XML element.</summary>
		/// <param name="node">The XML node to write, possibly a child XML element.</param>
		/// <param name="name">The local name of the parent XML element to write.</param>
		/// <param name="ns">The namespace of the parent XML element to write.</param>
		/// <param name="isNullable">
		///       <see langword="true" /> to write an <see langword="xsi:nil='true'" /> attribute if the object to serialize is <see langword="null" />; otherwise, <see langword="false" />.</param>
		/// <param name="any">
		///       <see langword="true" /> to indicate that the node, if an XML element, adheres to an XML Schema <see langword="any" /> element declaration; otherwise, <see langword="false" />.</param>
		// Token: 0x06001EFD RID: 7933 RVA: 0x000C1B8C File Offset: 0x000BFD8C
		protected void WriteElementEncoded(XmlNode node, string name, string ns, bool isNullable, bool any)
		{
			if (node == null)
			{
				if (isNullable)
				{
					this.WriteNullTagEncoded(name, ns);
				}
				return;
			}
			this.WriteElement(node, name, ns, isNullable, any);
		}

		/// <summary>Instructs an <see cref="T:System.Xml.XmlWriter" /> object to write an <see cref="T:System.Xml.XmlNode" /> object within the body of a named XML element.</summary>
		/// <param name="node">The XML node to write, possibly a child XML element.</param>
		/// <param name="name">The local name of the parent XML element to write.</param>
		/// <param name="ns">The namespace of the parent XML element to write.</param>
		/// <param name="isNullable">
		///       <see langword="true" /> to write an <see langword="xsi:nil='true'" /> attribute if the object to serialize is <see langword="null" />; otherwise, <see langword="false" />.</param>
		/// <param name="any">
		///       <see langword="true" /> to indicate that the node, if an XML element, adheres to an XML Schema <see langword="any" /> element declaration; otherwise, <see langword="false" />.</param>
		// Token: 0x06001EFE RID: 7934 RVA: 0x000C1BAB File Offset: 0x000BFDAB
		protected void WriteElementLiteral(XmlNode node, string name, string ns, bool isNullable, bool any)
		{
			if (node == null)
			{
				if (isNullable)
				{
					this.WriteNullTagLiteral(name, ns);
				}
				return;
			}
			this.WriteElement(node, name, ns, isNullable, any);
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x000C1BCC File Offset: 0x000BFDCC
		private void WriteElement(XmlNode node, string name, string ns, bool isNullable, bool any)
		{
			if (typeof(XmlAttribute).IsAssignableFrom(node.GetType()))
			{
				throw new InvalidOperationException(Res.GetString("Cannot write a node of type XmlAttribute as an element value. Use XmlAnyAttributeAttribute with an array of XmlNode or XmlAttribute to write the node as an attribute."));
			}
			if (node is XmlDocument)
			{
				node = ((XmlDocument)node).DocumentElement;
				if (node == null)
				{
					if (isNullable)
					{
						this.WriteNullTagEncoded(name, ns);
					}
					return;
				}
			}
			if (any)
			{
				if (node is XmlElement && name != null && name.Length > 0 && (node.LocalName != name || node.NamespaceURI != ns))
				{
					throw new InvalidOperationException(Res.GetString("This element was named '{0}' from namespace '{1}' but should have been named '{2}' from namespace '{3}'.", new object[]
					{
						node.LocalName,
						node.NamespaceURI,
						name,
						ns
					}));
				}
			}
			else
			{
				this.w.WriteStartElement(name, ns);
			}
			node.WriteTo(this.w);
			if (!any)
			{
				this.w.WriteEndElement();
			}
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that a type being serialized is not being used in a valid manner or is unexpectedly encountered.</summary>
		/// <param name="o">The object whose type cannot be serialized.</param>
		/// <returns>The newly created exception.</returns>
		// Token: 0x06001F00 RID: 7936 RVA: 0x000C1CB1 File Offset: 0x000BFEB1
		protected Exception CreateUnknownTypeException(object o)
		{
			return this.CreateUnknownTypeException(o.GetType());
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that a type being serialized is not being used in a valid manner or is unexpectedly encountered.</summary>
		/// <param name="type">The type that cannot be serialized.</param>
		/// <returns>The newly created exception.</returns>
		// Token: 0x06001F01 RID: 7937 RVA: 0x000C1CC0 File Offset: 0x000BFEC0
		protected Exception CreateUnknownTypeException(Type type)
		{
			if (typeof(IXmlSerializable).IsAssignableFrom(type))
			{
				return new InvalidOperationException(Res.GetString("The type {0} may not be used in this context. To use {0} as a parameter, return type, or member of a class or struct, the parameter, return type, or member must be declared as type {0} (it cannot be object). Objects of type {0} may not be used in un-typed collections, such as ArrayLists.", new object[]
				{
					type.FullName
				}));
			}
			if (!new TypeScope().GetTypeDesc(type).IsStructLike)
			{
				return new InvalidOperationException(Res.GetString("The type {0} may not be used in this context.", new object[]
				{
					type.FullName
				}));
			}
			return new InvalidOperationException(Res.GetString("The type {0} was not expected. Use the XmlInclude or SoapInclude attribute to specify types that are not known statically.", new object[]
			{
				type.FullName
			}));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that a value for an XML element does not match an enumeration type.</summary>
		/// <param name="value">The value that is not valid.</param>
		/// <param name="elementName">The name of the XML element with an invalid value.</param>
		/// <param name="enumValue">The valid value.</param>
		/// <returns>The newly created exception.</returns>
		// Token: 0x06001F02 RID: 7938 RVA: 0x000C1D4D File Offset: 0x000BFF4D
		protected Exception CreateMismatchChoiceException(string value, string elementName, string enumValue)
		{
			return new InvalidOperationException(Res.GetString("Value of {0} mismatches the type of {1}; you need to set it to {2}.", new object[]
			{
				elementName,
				value,
				enumValue
			}));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that an XML element that should adhere to the XML Schema <see langword="any" /> element declaration cannot be processed.</summary>
		/// <param name="name">The XML element that cannot be processed.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <returns>The newly created exception.</returns>
		// Token: 0x06001F03 RID: 7939 RVA: 0x000C1D70 File Offset: 0x000BFF70
		protected Exception CreateUnknownAnyElementException(string name, string ns)
		{
			return new InvalidOperationException(Res.GetString("The XML element '{0}' from namespace '{1}' was not expected. The XML element name and namespace must match those provided via XmlAnyElementAttribute(s).", new object[]
			{
				name,
				ns
			}));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates a failure while writing an array where an XML Schema <see langword="choice" /> element declaration is applied.</summary>
		/// <param name="type">The type being serialized.</param>
		/// <param name="identifier">A name for the <see langword="choice" /> element declaration.</param>
		/// <returns>The newly created exception.</returns>
		// Token: 0x06001F04 RID: 7940 RVA: 0x000C1D8F File Offset: 0x000BFF8F
		protected Exception CreateInvalidChoiceIdentifierValueException(string type, string identifier)
		{
			return new InvalidOperationException(Res.GetString("Invalid or missing value of the choice identifier '{1}' of type '{0}[]'.", new object[]
			{
				type,
				identifier
			}));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates an unexpected name for an element that adheres to an XML Schema <see langword="choice" /> element declaration.</summary>
		/// <param name="value">The name that is not valid.</param>
		/// <param name="identifier">The <see langword="choice" /> element declaration that the name belongs to.</param>
		/// <param name="name">The expected local name of an element.</param>
		/// <param name="ns">The expected namespace of an element.</param>
		/// <returns>The newly created exception.</returns>
		// Token: 0x06001F05 RID: 7941 RVA: 0x000C1DAE File Offset: 0x000BFFAE
		protected Exception CreateChoiceIdentifierValueException(string value, string identifier, string name, string ns)
		{
			return new InvalidOperationException(Res.GetString("Value '{0}' of the choice identifier '{1}' does not match element '{2}' from namespace '{3}'.", new object[]
			{
				value,
				identifier,
				name,
				ns
			}));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> for an invalid enumeration value.</summary>
		/// <param name="value">An object that represents the invalid enumeration.</param>
		/// <param name="typeName">The XML type name.</param>
		/// <returns>The newly created exception.</returns>
		// Token: 0x06001F06 RID: 7942 RVA: 0x000C1DD6 File Offset: 0x000BFFD6
		protected Exception CreateInvalidEnumValueException(object value, string typeName)
		{
			return new InvalidOperationException(Res.GetString("Instance validation error: '{0}' is not a valid value for {1}.", new object[]
			{
				value,
				typeName
			}));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> which has been invalidly applied to a member; only members that are of type <see cref="T:System.Xml.XmlNode" />, or derived from <see cref="T:System.Xml.XmlNode" />, are valid.</summary>
		/// <param name="o">The object that represents the invalid member.</param>
		/// <returns>The newly created exception.</returns>
		// Token: 0x06001F07 RID: 7943 RVA: 0x000C1DF5 File Offset: 0x000BFFF5
		protected Exception CreateInvalidAnyTypeException(object o)
		{
			return this.CreateInvalidAnyTypeException(o.GetType());
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> which has been invalidly applied to a member; only members that are of type <see cref="T:System.Xml.XmlNode" />, or derived from <see cref="T:System.Xml.XmlNode" />, are valid.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> that is invalid.</param>
		/// <returns>The newly created exception.</returns>
		// Token: 0x06001F08 RID: 7944 RVA: 0x000C1E03 File Offset: 0x000C0003
		protected Exception CreateInvalidAnyTypeException(Type type)
		{
			return new InvalidOperationException(Res.GetString("Cannot serialize member of type {0}: XmlAnyElement can only be used with classes of type XmlNode or a type deriving from XmlNode.", new object[]
			{
				type.FullName
			}));
		}

		/// <summary>Writes a SOAP message XML element that contains a reference to a <see langword="multiRef " />element for a given object.</summary>
		/// <param name="n">The local name of the referencing element being written.</param>
		/// <param name="ns">The namespace of the referencing element being written.</param>
		/// <param name="o">The object being serialized.</param>
		// Token: 0x06001F09 RID: 7945 RVA: 0x000C1E23 File Offset: 0x000C0023
		protected void WriteReferencingElement(string n, string ns, object o)
		{
			this.WriteReferencingElement(n, ns, o, false);
		}

		/// <summary>Writes a SOAP message XML element that contains a reference to a <see langword="multiRef" /> element for a given object.</summary>
		/// <param name="n">The local name of the referencing element being written.</param>
		/// <param name="ns">The namespace of the referencing element being written.</param>
		/// <param name="o">The object being serialized.</param>
		/// <param name="isNullable">
		///       <see langword="true" /> to write an <see langword="xsi:nil='true'" /> attribute if the object to serialize is <see langword="null" />; otherwise, <see langword="false" />.</param>
		// Token: 0x06001F0A RID: 7946 RVA: 0x000C1E30 File Offset: 0x000C0030
		protected void WriteReferencingElement(string n, string ns, object o, bool isNullable)
		{
			if (o == null)
			{
				if (isNullable)
				{
					this.WriteNullTagEncoded(n, ns);
				}
				return;
			}
			this.WriteStartElement(n, ns, null, true);
			if (this.soap12)
			{
				this.w.WriteAttributeString("ref", "http://www.w3.org/2003/05/soap-encoding", this.GetId(o, true));
			}
			else
			{
				this.w.WriteAttributeString("href", "#" + this.GetId(o, true));
			}
			this.w.WriteEndElement();
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x000C1EAB File Offset: 0x000C00AB
		private bool IsIdDefined(object o)
		{
			return this.references != null && this.references.Contains(o);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x000C1EC4 File Offset: 0x000C00C4
		private string GetId(object o, bool addToReferencesList)
		{
			if (this.references == null)
			{
				this.references = new Hashtable();
				this.referencesToWrite = new ArrayList();
			}
			string text = (string)this.references[o];
			if (text == null)
			{
				string str = this.idBase;
				string str2 = "id";
				int num = this.nextId + 1;
				this.nextId = num;
				text = str + str2 + num.ToString(CultureInfo.InvariantCulture);
				this.references.Add(o, text);
				if (addToReferencesList)
				{
					this.referencesToWrite.Add(o);
				}
			}
			return text;
		}

		/// <summary>Writes an <see langword="id" /> attribute that appears in a SOAP-encoded <see langword="multiRef" /> element.</summary>
		/// <param name="o">The object being serialized.</param>
		// Token: 0x06001F0D RID: 7949 RVA: 0x000C1F4F File Offset: 0x000C014F
		protected void WriteId(object o)
		{
			this.WriteId(o, true);
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x000C1F59 File Offset: 0x000C0159
		private void WriteId(object o, bool addToReferencesList)
		{
			if (this.soap12)
			{
				this.w.WriteAttributeString("id", "http://www.w3.org/2003/05/soap-encoding", this.GetId(o, addToReferencesList));
				return;
			}
			this.w.WriteAttributeString("id", this.GetId(o, addToReferencesList));
		}

		/// <summary>Writes the specified <see cref="T:System.Xml.XmlNode" /> as an XML attribute.</summary>
		/// <param name="node">The XML node to write.</param>
		// Token: 0x06001F0F RID: 7951 RVA: 0x000C1F99 File Offset: 0x000C0199
		protected void WriteXmlAttribute(XmlNode node)
		{
			this.WriteXmlAttribute(node, null);
		}

		/// <summary>Writes the specified <see cref="T:System.Xml.XmlNode" /> object as an XML attribute.</summary>
		/// <param name="node">The XML node to write.</param>
		/// <param name="container">An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> object (or <see langword="null" />) used to generate a qualified name value for an <see langword="arrayType" /> attribute from the Web Services Description Language (WSDL) namespace ("http://schemas.xmlsoap.org/wsdl/").</param>
		// Token: 0x06001F10 RID: 7952 RVA: 0x000C1FA4 File Offset: 0x000C01A4
		protected void WriteXmlAttribute(XmlNode node, object container)
		{
			XmlAttribute xmlAttribute = node as XmlAttribute;
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(Res.GetString("The node must be either type XmlAttribute or a derived type."));
			}
			if (xmlAttribute.Value != null)
			{
				if (xmlAttribute.NamespaceURI == "http://schemas.xmlsoap.org/wsdl/" && xmlAttribute.LocalName == "arrayType")
				{
					string str;
					XmlQualifiedName xmlQualifiedName = TypeScope.ParseWsdlArrayType(xmlAttribute.Value, out str, (container is XmlSchemaObject) ? ((XmlSchemaObject)container) : null);
					string value = this.FromXmlQualifiedName(xmlQualifiedName, true) + str;
					this.WriteAttribute("arrayType", "http://schemas.xmlsoap.org/wsdl/", value);
					return;
				}
				this.WriteAttribute(xmlAttribute.Name, xmlAttribute.NamespaceURI, xmlAttribute.Value);
			}
		}

		/// <summary>Writes an XML attribute.</summary>
		/// <param name="localName">The local name of the XML attribute.</param>
		/// <param name="ns">The namespace of the XML attribute.</param>
		/// <param name="value">The value of the XML attribute as a string.</param>
		// Token: 0x06001F11 RID: 7953 RVA: 0x000C2050 File Offset: 0x000C0250
		protected void WriteAttribute(string localName, string ns, string value)
		{
			if (value == null)
			{
				return;
			}
			if (!(localName == "xmlns") && !localName.StartsWith("xmlns:", StringComparison.Ordinal))
			{
				int num = localName.IndexOf(':');
				if (num < 0)
				{
					if (ns == "http://www.w3.org/XML/1998/namespace")
					{
						string text = this.w.LookupPrefix(ns);
						if (text == null || text.Length == 0)
						{
							text = "xml";
						}
						this.w.WriteAttributeString(text, localName, ns, value);
						return;
					}
					this.w.WriteAttributeString(localName, ns, value);
					return;
				}
				else
				{
					string prefix = localName.Substring(0, num);
					this.w.WriteAttributeString(prefix, localName.Substring(num + 1), ns, value);
				}
			}
		}

		/// <summary>Instructs an <see cref="T:System.Xml.XmlWriter" /> object to write an XML attribute.</summary>
		/// <param name="localName">The local name of the XML attribute.</param>
		/// <param name="ns">The namespace of the XML attribute.</param>
		/// <param name="value">The value of the XML attribute as a byte array.</param>
		// Token: 0x06001F12 RID: 7954 RVA: 0x000C20F8 File Offset: 0x000C02F8
		protected void WriteAttribute(string localName, string ns, byte[] value)
		{
			if (value == null)
			{
				return;
			}
			if (!(localName == "xmlns") && !localName.StartsWith("xmlns:", StringComparison.Ordinal))
			{
				int num = localName.IndexOf(':');
				if (num < 0)
				{
					if (ns == "http://www.w3.org/XML/1998/namespace")
					{
						string text = this.w.LookupPrefix(ns);
						if (text == null || text.Length == 0)
						{
						}
						this.w.WriteStartAttribute("xml", localName, ns);
					}
					else
					{
						this.w.WriteStartAttribute(null, localName, ns);
					}
				}
				else
				{
					string prefix = localName.Substring(0, num);
					prefix = this.w.LookupPrefix(ns);
					this.w.WriteStartAttribute(prefix, localName.Substring(num + 1), ns);
				}
				XmlCustomFormatter.WriteArrayBase64(this.w, value, 0, value.Length);
				this.w.WriteEndAttribute();
			}
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlWriter" /> to write an XML attribute that has no namespace specified for its name.</summary>
		/// <param name="localName">The local name of the XML attribute.</param>
		/// <param name="value">The value of the XML attribute as a string.</param>
		// Token: 0x06001F13 RID: 7955 RVA: 0x000C21CD File Offset: 0x000C03CD
		protected void WriteAttribute(string localName, string value)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteAttributeString(localName, null, value);
		}

		/// <summary>Instructs an <see cref="T:System.Xml.XmlWriter" /> object to write an XML attribute that has no namespace specified for its name.</summary>
		/// <param name="localName">The local name of the XML attribute.</param>
		/// <param name="value">The value of the XML attribute as a byte array.</param>
		// Token: 0x06001F14 RID: 7956 RVA: 0x000C21E1 File Offset: 0x000C03E1
		protected void WriteAttribute(string localName, byte[] value)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteStartAttribute(null, localName, null);
			XmlCustomFormatter.WriteArrayBase64(this.w, value, 0, value.Length);
			this.w.WriteEndAttribute();
		}

		/// <summary>Writes an XML attribute where the namespace prefix is provided manually.</summary>
		/// <param name="prefix">The namespace prefix to write.</param>
		/// <param name="localName">The local name of the XML attribute.</param>
		/// <param name="ns">The namespace represented by the prefix.</param>
		/// <param name="value">The value of the XML attribute as a string.</param>
		// Token: 0x06001F15 RID: 7957 RVA: 0x000C2210 File Offset: 0x000C0410
		protected void WriteAttribute(string prefix, string localName, string ns, string value)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteAttributeString(prefix, localName, null, value);
		}

		/// <summary>Writes a specified string value.</summary>
		/// <param name="value">The value of the string to write.</param>
		// Token: 0x06001F16 RID: 7958 RVA: 0x000C2227 File Offset: 0x000C0427
		protected void WriteValue(string value)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteString(value);
		}

		/// <summary>Writes a base-64 byte array.</summary>
		/// <param name="value">The byte array to write.</param>
		// Token: 0x06001F17 RID: 7959 RVA: 0x000C2239 File Offset: 0x000C0439
		protected void WriteValue(byte[] value)
		{
			if (value == null)
			{
				return;
			}
			XmlCustomFormatter.WriteArrayBase64(this.w, value, 0, value.Length);
		}

		/// <summary>Writes the XML declaration if the writer is positioned at the start of an XML document.</summary>
		// Token: 0x06001F18 RID: 7960 RVA: 0x000C224F File Offset: 0x000C044F
		protected void WriteStartDocument()
		{
			if (this.w.WriteState == WriteState.Start)
			{
				this.w.WriteStartDocument();
			}
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element to be written without namespace qualification.</param>
		/// <param name="value">The text value of the XML element.</param>
		// Token: 0x06001F19 RID: 7961 RVA: 0x000C2269 File Offset: 0x000C0469
		protected void WriteElementString(string localName, string value)
		{
			this.WriteElementString(localName, null, value, null);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		// Token: 0x06001F1A RID: 7962 RVA: 0x000C2275 File Offset: 0x000C0475
		protected void WriteElementString(string localName, string ns, string value)
		{
			this.WriteElementString(localName, ns, value, null);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the <see langword="xsi:type" /> attribute.</param>
		// Token: 0x06001F1B RID: 7963 RVA: 0x000C2281 File Offset: 0x000C0481
		protected void WriteElementString(string localName, string value, XmlQualifiedName xsiType)
		{
			this.WriteElementString(localName, null, value, xsiType);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the <see langword="xsi:type" /> attribute.</param>
		// Token: 0x06001F1C RID: 7964 RVA: 0x000C2290 File Offset: 0x000C0490
		protected void WriteElementString(string localName, string ns, string value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				return;
			}
			if (xsiType == null)
			{
				this.w.WriteElementString(localName, ns, value);
				return;
			}
			this.w.WriteStartElement(localName, ns);
			this.WriteXsiType(xsiType.Name, xsiType.Namespace);
			this.w.WriteString(value);
			this.w.WriteEndElement();
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		// Token: 0x06001F1D RID: 7965 RVA: 0x000C22F2 File Offset: 0x000C04F2
		protected void WriteElementStringRaw(string localName, string value)
		{
			this.WriteElementStringRaw(localName, null, value, null);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		// Token: 0x06001F1E RID: 7966 RVA: 0x000C22FE File Offset: 0x000C04FE
		protected void WriteElementStringRaw(string localName, byte[] value)
		{
			this.WriteElementStringRaw(localName, null, value, null);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		// Token: 0x06001F1F RID: 7967 RVA: 0x000C230A File Offset: 0x000C050A
		protected void WriteElementStringRaw(string localName, string ns, string value)
		{
			this.WriteElementStringRaw(localName, ns, value, null);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		// Token: 0x06001F20 RID: 7968 RVA: 0x000C2316 File Offset: 0x000C0516
		protected void WriteElementStringRaw(string localName, string ns, byte[] value)
		{
			this.WriteElementStringRaw(localName, ns, value, null);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the <see langword="xsi:type" /> attribute.</param>
		// Token: 0x06001F21 RID: 7969 RVA: 0x000C2322 File Offset: 0x000C0522
		protected void WriteElementStringRaw(string localName, string value, XmlQualifiedName xsiType)
		{
			this.WriteElementStringRaw(localName, null, value, xsiType);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the <see langword="xsi:type" /> attribute.</param>
		// Token: 0x06001F22 RID: 7970 RVA: 0x000C232E File Offset: 0x000C052E
		protected void WriteElementStringRaw(string localName, byte[] value, XmlQualifiedName xsiType)
		{
			this.WriteElementStringRaw(localName, null, value, xsiType);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the <see langword="xsi:type" /> attribute.</param>
		// Token: 0x06001F23 RID: 7971 RVA: 0x000C233C File Offset: 0x000C053C
		protected void WriteElementStringRaw(string localName, string ns, string value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteStartElement(localName, ns);
			if (xsiType != null)
			{
				this.WriteXsiType(xsiType.Name, xsiType.Namespace);
			}
			this.w.WriteRaw(value);
			this.w.WriteEndElement();
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the <see langword="xsi:type" /> attribute.</param>
		// Token: 0x06001F24 RID: 7972 RVA: 0x000C2390 File Offset: 0x000C0590
		protected void WriteElementStringRaw(string localName, string ns, byte[] value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteStartElement(localName, ns);
			if (xsiType != null)
			{
				this.WriteXsiType(xsiType.Name, xsiType.Namespace);
			}
			XmlCustomFormatter.WriteArrayBase64(this.w, value, 0, value.Length);
			this.w.WriteEndElement();
		}

		/// <summary>Writes a SOAP 1.2 RPC result element with a specified qualified name in its body.</summary>
		/// <param name="name">The local name of the result body.</param>
		/// <param name="ns">The namespace of the result body.</param>
		// Token: 0x06001F25 RID: 7973 RVA: 0x000C23E7 File Offset: 0x000C05E7
		protected void WriteRpcResult(string name, string ns)
		{
			if (!this.soap12)
			{
				return;
			}
			this.WriteElementQualifiedName("result", "http://www.w3.org/2003/05/soap-rpc", new XmlQualifiedName(name, ns), null);
		}

		/// <summary>Writes an XML element with a specified qualified name in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The name to write, using its prefix if namespace-qualified, in the element text.</param>
		// Token: 0x06001F26 RID: 7974 RVA: 0x000C240A File Offset: 0x000C060A
		protected void WriteElementQualifiedName(string localName, XmlQualifiedName value)
		{
			this.WriteElementQualifiedName(localName, null, value, null);
		}

		/// <summary>Writes an XML element with a specified qualified name in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The name to write, using its prefix if namespace-qualified, in the element text.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the <see langword="xsi:type" /> attribute.</param>
		// Token: 0x06001F27 RID: 7975 RVA: 0x000C2416 File Offset: 0x000C0616
		protected void WriteElementQualifiedName(string localName, XmlQualifiedName value, XmlQualifiedName xsiType)
		{
			this.WriteElementQualifiedName(localName, null, value, xsiType);
		}

		/// <summary>Writes an XML element with a specified qualified name in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The name to write, using its prefix if namespace-qualified, in the element text.</param>
		// Token: 0x06001F28 RID: 7976 RVA: 0x000C2422 File Offset: 0x000C0622
		protected void WriteElementQualifiedName(string localName, string ns, XmlQualifiedName value)
		{
			this.WriteElementQualifiedName(localName, ns, value, null);
		}

		/// <summary>Writes an XML element with a specified qualified name in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The name to write, using its prefix if namespace-qualified, in the element text.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the <see langword="xsi:type" /> attribute.</param>
		// Token: 0x06001F29 RID: 7977 RVA: 0x000C2430 File Offset: 0x000C0630
		protected void WriteElementQualifiedName(string localName, string ns, XmlQualifiedName value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				return;
			}
			if (value.Namespace == null || value.Namespace.Length == 0)
			{
				this.WriteStartElement(localName, ns, null, true);
				this.WriteAttribute("xmlns", "");
			}
			else
			{
				this.w.WriteStartElement(localName, ns);
			}
			if (xsiType != null)
			{
				this.WriteXsiType(xsiType.Name, xsiType.Namespace);
			}
			this.w.WriteString(this.FromXmlQualifiedName(value, false));
			this.w.WriteEndElement();
		}

		/// <summary>Stores an implementation of the <see cref="T:System.Xml.Serialization.XmlSerializationWriteCallback" /> delegate and the type it applies to, for a later invocation.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of objects that are serialized.</param>
		/// <param name="typeName">The name of the type of objects that are serialized.</param>
		/// <param name="typeNs">The namespace of the type of objects that are serialized.</param>
		/// <param name="callback">An instance of the <see cref="T:System.Xml.Serialization.XmlSerializationWriteCallback" /> delegate.</param>
		// Token: 0x06001F2A RID: 7978 RVA: 0x000C24C4 File Offset: 0x000C06C4
		protected void AddWriteCallback(Type type, string typeName, string typeNs, XmlSerializationWriteCallback callback)
		{
			XmlSerializationWriter.TypeEntry typeEntry = new XmlSerializationWriter.TypeEntry();
			typeEntry.typeName = typeName;
			typeEntry.typeNs = typeNs;
			typeEntry.type = type;
			typeEntry.callback = callback;
			this.typeEntries[type] = typeEntry;
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x000C2504 File Offset: 0x000C0704
		private void WriteArray(string name, string ns, object o, Type type)
		{
			Type arrayElementType = TypeScope.GetArrayElementType(type, null);
			StringBuilder stringBuilder = new StringBuilder();
			if (!this.soap12)
			{
				while ((arrayElementType.IsArray || typeof(IEnumerable).IsAssignableFrom(arrayElementType)) && this.GetPrimitiveTypeName(arrayElementType, false) == null)
				{
					arrayElementType = TypeScope.GetArrayElementType(arrayElementType, null);
					stringBuilder.Append("[]");
				}
			}
			string text;
			string ns2;
			if (arrayElementType == typeof(object))
			{
				text = "anyType";
				ns2 = "http://www.w3.org/2001/XMLSchema";
			}
			else
			{
				XmlSerializationWriter.TypeEntry typeEntry = this.GetTypeEntry(arrayElementType);
				if (typeEntry != null)
				{
					text = typeEntry.typeName;
					ns2 = typeEntry.typeNs;
				}
				else if (this.soap12)
				{
					XmlQualifiedName primitiveTypeName = this.GetPrimitiveTypeName(arrayElementType, false);
					if (primitiveTypeName != null)
					{
						text = primitiveTypeName.Name;
						ns2 = primitiveTypeName.Namespace;
					}
					else
					{
						Type baseType = arrayElementType.BaseType;
						while (baseType != null)
						{
							typeEntry = this.GetTypeEntry(baseType);
							if (typeEntry != null)
							{
								break;
							}
							baseType = baseType.BaseType;
						}
						if (typeEntry != null)
						{
							text = typeEntry.typeName;
							ns2 = typeEntry.typeNs;
						}
						else
						{
							text = "anyType";
							ns2 = "http://www.w3.org/2001/XMLSchema";
						}
					}
				}
				else
				{
					XmlQualifiedName primitiveTypeName2 = this.GetPrimitiveTypeName(arrayElementType);
					text = primitiveTypeName2.Name;
					ns2 = primitiveTypeName2.Namespace;
				}
			}
			if (stringBuilder.Length > 0)
			{
				text += stringBuilder.ToString();
			}
			if (this.soap12 && name != null && name.Length > 0)
			{
				this.WriteStartElement(name, ns, null, false);
			}
			else
			{
				this.WriteStartElement("Array", "http://schemas.xmlsoap.org/soap/encoding/", null, true);
			}
			this.WriteId(o, false);
			if (type.IsArray)
			{
				Array array = (Array)o;
				int length = array.Length;
				if (this.soap12)
				{
					this.w.WriteAttributeString("itemType", "http://www.w3.org/2003/05/soap-encoding", this.GetQualifiedName(text, ns2));
					this.w.WriteAttributeString("arraySize", "http://www.w3.org/2003/05/soap-encoding", length.ToString(CultureInfo.InvariantCulture));
				}
				else
				{
					this.w.WriteAttributeString("arrayType", "http://schemas.xmlsoap.org/soap/encoding/", this.GetQualifiedName(text, ns2) + "[" + length.ToString(CultureInfo.InvariantCulture) + "]");
				}
				for (int i = 0; i < length; i++)
				{
					this.WritePotentiallyReferencingElement("Item", "", array.GetValue(i), arrayElementType, false, true);
				}
			}
			else
			{
				int num = typeof(ICollection).IsAssignableFrom(type) ? ((ICollection)o).Count : -1;
				if (this.soap12)
				{
					this.w.WriteAttributeString("itemType", "http://www.w3.org/2003/05/soap-encoding", this.GetQualifiedName(text, ns2));
					if (num >= 0)
					{
						this.w.WriteAttributeString("arraySize", "http://www.w3.org/2003/05/soap-encoding", num.ToString(CultureInfo.InvariantCulture));
					}
				}
				else
				{
					string str = (num >= 0) ? ("[" + num.ToString() + "]") : "[]";
					this.w.WriteAttributeString("arrayType", "http://schemas.xmlsoap.org/soap/encoding/", this.GetQualifiedName(text, ns2) + str);
				}
				IEnumerator enumerator = ((IEnumerable)o).GetEnumerator();
				if (enumerator != null)
				{
					while (enumerator.MoveNext())
					{
						object o2 = enumerator.Current;
						this.WritePotentiallyReferencingElement("Item", "", o2, arrayElementType, false, true);
					}
				}
			}
			this.w.WriteEndElement();
		}

		/// <summary>Writes a SOAP message XML element that can contain a reference to a <see langword="&lt;multiRef&gt;" /> XML element for a given object.</summary>
		/// <param name="n">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized either in the current XML element or a <see langword="multiRef" /> element that is referenced by the current element.</param>
		// Token: 0x06001F2C RID: 7980 RVA: 0x000C2856 File Offset: 0x000C0A56
		protected void WritePotentiallyReferencingElement(string n, string ns, object o)
		{
			this.WritePotentiallyReferencingElement(n, ns, o, null, false, false);
		}

		/// <summary>Writes a SOAP message XML element that can contain a reference to a <see langword="&lt;multiRef&gt;" /> XML element for a given object.</summary>
		/// <param name="n">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized either in the current XML element or a <see langword="multiRef" /> element that referenced by the current element.</param>
		/// <param name="ambientType">The type stored in the object's type mapping (as opposed to the object's type found directly through the <see langword="typeof" /> operation).</param>
		// Token: 0x06001F2D RID: 7981 RVA: 0x000C2864 File Offset: 0x000C0A64
		protected void WritePotentiallyReferencingElement(string n, string ns, object o, Type ambientType)
		{
			this.WritePotentiallyReferencingElement(n, ns, o, ambientType, false, false);
		}

		/// <summary>Writes a SOAP message XML element that can contain a reference to a <see langword="&lt;multiRef&gt;" /> XML element for a given object.</summary>
		/// <param name="n">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized either in the current XML element or a <see langword="multiRef" /> element that is referenced by the current element.</param>
		/// <param name="ambientType">The type stored in the object's type mapping (as opposed to the object's type found directly through the <see langword="typeof" /> operation).</param>
		/// <param name="suppressReference">
		///       <see langword="true" /> to serialize the object directly into the XML element rather than make the element reference another element that contains the data; otherwise, <see langword="false" />.</param>
		// Token: 0x06001F2E RID: 7982 RVA: 0x000C2873 File Offset: 0x000C0A73
		protected void WritePotentiallyReferencingElement(string n, string ns, object o, Type ambientType, bool suppressReference)
		{
			this.WritePotentiallyReferencingElement(n, ns, o, ambientType, suppressReference, false);
		}

		/// <summary>Writes a SOAP message XML element that can contain a reference to a <see langword="multiRef" /> XML element for a given object.</summary>
		/// <param name="n">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized either in the current XML element or a <see langword="multiRef" /> element that referenced by the current element.</param>
		/// <param name="ambientType">The type stored in the object's type mapping (as opposed to the object's type found directly through the <see langword="typeof" /> operation).</param>
		/// <param name="suppressReference">
		///       <see langword="true" /> to serialize the object directly into the XML element rather than make the element reference another element that contains the data; otherwise, <see langword="false" />.</param>
		/// <param name="isNullable">
		///       <see langword="true" /> to write an <see langword="xsi:nil='true'" /> attribute if the object to serialize is <see langword="null" />; otherwise, <see langword="false" />.</param>
		// Token: 0x06001F2F RID: 7983 RVA: 0x000C2884 File Offset: 0x000C0A84
		protected void WritePotentiallyReferencingElement(string n, string ns, object o, Type ambientType, bool suppressReference, bool isNullable)
		{
			if (o == null)
			{
				if (isNullable)
				{
					this.WriteNullTagEncoded(n, ns);
				}
				return;
			}
			Type type = o.GetType();
			if (Convert.GetTypeCode(o) == TypeCode.Object && !(o is Guid) && type != typeof(XmlQualifiedName) && !(o is XmlNode[]) && type != typeof(byte[]))
			{
				if ((suppressReference || this.soap12) && !this.IsIdDefined(o))
				{
					this.WriteReferencedElement(n, ns, o, ambientType);
					return;
				}
				if (n == null)
				{
					XmlSerializationWriter.TypeEntry typeEntry = this.GetTypeEntry(type);
					this.WriteReferencingElement(typeEntry.typeName, typeEntry.typeNs, o, isNullable);
					return;
				}
				this.WriteReferencingElement(n, ns, o, isNullable);
				return;
			}
			else
			{
				bool flag = type != ambientType && !type.IsEnum;
				XmlSerializationWriter.TypeEntry typeEntry2 = this.GetTypeEntry(type);
				if (typeEntry2 != null)
				{
					if (n == null)
					{
						this.WriteStartElement(typeEntry2.typeName, typeEntry2.typeNs, null, true);
					}
					else
					{
						this.WriteStartElement(n, ns, null, true);
					}
					if (flag)
					{
						this.WriteXsiType(typeEntry2.typeName, typeEntry2.typeNs);
					}
					typeEntry2.callback(o);
					this.w.WriteEndElement();
					return;
				}
				this.WriteTypedPrimitive(n, ns, o, flag);
				return;
			}
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x000C29B2 File Offset: 0x000C0BB2
		private void WriteReferencedElement(object o, Type ambientType)
		{
			this.WriteReferencedElement(null, null, o, ambientType);
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x000C29C0 File Offset: 0x000C0BC0
		private void WriteReferencedElement(string name, string ns, object o, Type ambientType)
		{
			if (name == null)
			{
				name = string.Empty;
			}
			Type type = o.GetType();
			if (type.IsArray || typeof(IEnumerable).IsAssignableFrom(type))
			{
				this.WriteArray(name, ns, o, type);
				return;
			}
			XmlSerializationWriter.TypeEntry typeEntry = this.GetTypeEntry(type);
			if (typeEntry == null)
			{
				throw this.CreateUnknownTypeException(type);
			}
			this.WriteStartElement((name.Length == 0) ? typeEntry.typeName : name, (ns == null) ? typeEntry.typeNs : ns, null, true);
			this.WriteId(o, false);
			if (ambientType != type)
			{
				this.WriteXsiType(typeEntry.typeName, typeEntry.typeNs);
			}
			typeEntry.callback(o);
			this.w.WriteEndElement();
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x000C2A76 File Offset: 0x000C0C76
		private XmlSerializationWriter.TypeEntry GetTypeEntry(Type t)
		{
			if (this.typeEntries == null)
			{
				this.typeEntries = new Hashtable();
				this.InitCallbacks();
			}
			return (XmlSerializationWriter.TypeEntry)this.typeEntries[t];
		}

		/// <summary>Initializes an instances of the <see cref="T:System.Xml.Serialization.XmlSerializationWriteCallback" /> delegate to serialize SOAP-encoded XML data.</summary>
		// Token: 0x06001F33 RID: 7987
		protected abstract void InitCallbacks();

		/// <summary>Serializes objects into SOAP-encoded <see langword="multiRef" /> XML elements in a SOAP message.</summary>
		// Token: 0x06001F34 RID: 7988 RVA: 0x000C2AA4 File Offset: 0x000C0CA4
		protected void WriteReferencedElements()
		{
			if (this.referencesToWrite == null)
			{
				return;
			}
			for (int i = 0; i < this.referencesToWrite.Count; i++)
			{
				this.WriteReferencedElement(this.referencesToWrite[i], null);
			}
		}

		/// <summary>Initializes object references only while serializing a SOAP-encoded SOAP message.</summary>
		// Token: 0x06001F35 RID: 7989 RVA: 0x000C2AE3 File Offset: 0x000C0CE3
		protected void TopLevelElement()
		{
			this.objectsInUse = new Hashtable();
		}

		/// <summary>Writes the namespace declaration attributes.</summary>
		/// <param name="xmlns">The XML namespaces to declare.</param>
		// Token: 0x06001F36 RID: 7990 RVA: 0x000C2AF0 File Offset: 0x000C0CF0
		protected void WriteNamespaceDeclarations(XmlSerializerNamespaces xmlns)
		{
			if (xmlns != null)
			{
				foreach (object obj in xmlns.Namespaces)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string text = (string)dictionaryEntry.Key;
					string text2 = (string)dictionaryEntry.Value;
					if (this.namespaces != null)
					{
						string text3 = this.namespaces.Namespaces[text] as string;
						if (text3 != null && text3 != text2)
						{
							throw new InvalidOperationException(Res.GetString("Illegal namespace declaration xmlns:{0}='{1}'. Namespace alias '{0}' already defined in the current scope.", new object[]
							{
								text,
								text2
							}));
						}
					}
					string text4 = (text2 == null || text2.Length == 0) ? null : this.Writer.LookupPrefix(text2);
					if (text4 == null || text4 != text)
					{
						this.WriteAttribute("xmlns", text, null, text2);
					}
				}
			}
			this.namespaces = null;
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x000C2BF8 File Offset: 0x000C0DF8
		private string NextPrefix()
		{
			int num;
			if (this.usedPrefixes == null)
			{
				string str = this.aliasBase;
				num = this.tempNamespacePrefix + 1;
				this.tempNamespacePrefix = num;
				return str + num.ToString();
			}
			Hashtable hashtable;
			do
			{
				hashtable = this.usedPrefixes;
				num = this.tempNamespacePrefix + 1;
				this.tempNamespacePrefix = num;
			}
			while (hashtable.ContainsKey(num));
			return this.aliasBase + this.tempNamespacePrefix.ToString();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializationWriter" /> class.</summary>
		// Token: 0x06001F38 RID: 7992 RVA: 0x000C2C69 File Offset: 0x000C0E69
		protected XmlSerializationWriter()
		{
		}

		// Token: 0x04001AF1 RID: 6897
		private XmlWriter w;

		// Token: 0x04001AF2 RID: 6898
		private XmlSerializerNamespaces namespaces;

		// Token: 0x04001AF3 RID: 6899
		private int tempNamespacePrefix;

		// Token: 0x04001AF4 RID: 6900
		private Hashtable usedPrefixes;

		// Token: 0x04001AF5 RID: 6901
		private Hashtable references;

		// Token: 0x04001AF6 RID: 6902
		private string idBase;

		// Token: 0x04001AF7 RID: 6903
		private int nextId;

		// Token: 0x04001AF8 RID: 6904
		private Hashtable typeEntries;

		// Token: 0x04001AF9 RID: 6905
		private ArrayList referencesToWrite;

		// Token: 0x04001AFA RID: 6906
		private Hashtable objectsInUse;

		// Token: 0x04001AFB RID: 6907
		private string aliasBase = "q";

		// Token: 0x04001AFC RID: 6908
		private bool soap12;

		// Token: 0x04001AFD RID: 6909
		private bool escapeName = true;

		// Token: 0x020002F9 RID: 761
		internal class TypeEntry
		{
			// Token: 0x06001F39 RID: 7993 RVA: 0x0000216B File Offset: 0x0000036B
			public TypeEntry()
			{
			}

			// Token: 0x04001AFE RID: 6910
			internal XmlSerializationWriteCallback callback;

			// Token: 0x04001AFF RID: 6911
			internal string typeNs;

			// Token: 0x04001B00 RID: 6912
			internal string typeName;

			// Token: 0x04001B01 RID: 6913
			internal Type type;
		}
	}
}
