using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace System.Xml.Linq
{
	/// <summary>Represents an XML attribute.</summary>
	// Token: 0x0200001A RID: 26
	public class XAttribute : XObject
	{
		/// <summary>Gets an empty collection of attributes.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XAttribute" /> containing an empty collection.</returns>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000050FB File Offset: 0x000032FB
		public static IEnumerable<XAttribute> EmptySequence
		{
			get
			{
				return Array.Empty<XAttribute>();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XAttribute" /> class from the specified name and value.</summary>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> of the attribute.</param>
		/// <param name="value">An <see cref="T:System.Object" /> containing the value of the attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> or <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060000CE RID: 206 RVA: 0x00005104 File Offset: 0x00003304
		public XAttribute(XName name, object value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			string stringValue = XContainer.GetStringValue(value);
			XAttribute.ValidateAttribute(name, stringValue);
			this.name = name;
			this.value = stringValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XAttribute" /> class from another <see cref="T:System.Xml.Linq.XAttribute" /> object.</summary>
		/// <param name="other">An <see cref="T:System.Xml.Linq.XAttribute" /> object to copy from.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="other" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060000CF RID: 207 RVA: 0x00005155 File Offset: 0x00003355
		public XAttribute(XAttribute other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.name = other.name;
			this.value = other.value;
		}

		/// <summary>Determines if this attribute is a namespace declaration.</summary>
		/// <returns>
		///   <see langword="true" /> if this attribute is a namespace declaration; otherwise <see langword="false" />.</returns>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005184 File Offset: 0x00003384
		public bool IsNamespaceDeclaration
		{
			get
			{
				string namespaceName = this.name.NamespaceName;
				if (namespaceName.Length == 0)
				{
					return this.name.LocalName == "xmlns";
				}
				return namespaceName == "http://www.w3.org/2000/xmlns/";
			}
		}

		/// <summary>Gets the expanded name of this attribute.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> containing the name of this attribute.</returns>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000051C3 File Offset: 0x000033C3
		public XName Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the next attribute of the parent element.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XAttribute" /> containing the next attribute of the parent element.</returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000051CB File Offset: 0x000033CB
		public XAttribute NextAttribute
		{
			get
			{
				if (this.parent == null || ((XElement)this.parent).lastAttr == this)
				{
					return null;
				}
				return this.next;
			}
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XAttribute" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.Attribute" />.</returns>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000051F0 File Offset: 0x000033F0
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Attribute;
			}
		}

		/// <summary>Gets the previous attribute of the parent element.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XAttribute" /> containing the previous attribute of the parent element.</returns>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000051F4 File Offset: 0x000033F4
		public XAttribute PreviousAttribute
		{
			get
			{
				if (this.parent == null)
				{
					return null;
				}
				XAttribute lastAttr = ((XElement)this.parent).lastAttr;
				while (lastAttr.next != this)
				{
					lastAttr = lastAttr.next;
				}
				if (lastAttr == ((XElement)this.parent).lastAttr)
				{
					return null;
				}
				return lastAttr;
			}
		}

		/// <summary>Gets or sets the value of this attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value of this attribute.</returns>
		/// <exception cref="T:System.ArgumentNullException">When setting, the <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00005244 File Offset: 0x00003444
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000524C File Offset: 0x0000344C
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				XAttribute.ValidateAttribute(this.name, value);
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Value);
				this.value = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		/// <summary>Removes this attribute from its parent element.</summary>
		/// <exception cref="T:System.InvalidOperationException">The parent element is <see langword="null" />.</exception>
		// Token: 0x060000D7 RID: 215 RVA: 0x0000528A File Offset: 0x0000348A
		public void Remove()
		{
			if (this.parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			((XElement)this.parent).RemoveAttribute(this);
		}

		/// <summary>Sets the value of this attribute.</summary>
		/// <param name="value">The value to assign to this attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> is an <see cref="T:System.Xml.Linq.XObject" />.</exception>
		// Token: 0x060000D8 RID: 216 RVA: 0x000052B0 File Offset: 0x000034B0
		public void SetValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.Value = XContainer.GetStringValue(value);
		}

		/// <summary>Converts the current <see cref="T:System.Xml.Linq.XAttribute" /> object to a string representation.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the XML text representation of an attribute and its value.</returns>
		// Token: 0x060000D9 RID: 217 RVA: 0x000052CC File Offset: 0x000034CC
		public override string ToString()
		{
			string result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
				{
					ConformanceLevel = ConformanceLevel.Fragment
				}))
				{
					xmlWriter.WriteAttributeString(this.GetPrefixOfNamespace(this.name.Namespace), this.name.LocalName, this.name.NamespaceName, this.value);
				}
				result = stringWriter.ToString().Trim();
			}
			return result;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.String" />.</param>
		/// <returns>A <see cref="T:System.String" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		// Token: 0x060000DA RID: 218 RVA: 0x00005370 File Offset: 0x00003570
		[CLSCompliant(false)]
		public static explicit operator string(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return attribute.value;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Boolean" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Boolean" />.</param>
		/// <returns>A <see cref="T:System.Boolean" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Boolean" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060000DB RID: 219 RVA: 0x0000537D File Offset: 0x0000357D
		[CLSCompliant(false)]
		public static explicit operator bool(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToBoolean(attribute.value.ToLowerInvariant());
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Boolean" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Boolean" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Boolean" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Boolean" /> value.</exception>
		// Token: 0x060000DC RID: 220 RVA: 0x000053A0 File Offset: 0x000035A0
		[CLSCompliant(false)]
		public static explicit operator bool?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new bool?(XmlConvert.ToBoolean(attribute.value.ToLowerInvariant()));
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to an <see cref="T:System.Int32" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Int32" />.</param>
		/// <returns>A <see cref="T:System.Int32" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Int32" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060000DD RID: 221 RVA: 0x000053CF File Offset: 0x000035CF
		[CLSCompliant(false)]
		public static explicit operator int(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToInt32(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int32" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int32" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int32" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		// Token: 0x060000DE RID: 222 RVA: 0x000053EC File Offset: 0x000035EC
		[CLSCompliant(false)]
		public static explicit operator int?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new int?(XmlConvert.ToInt32(attribute.value));
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.UInt32" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.UInt32" />.</param>
		/// <returns>A <see cref="T:System.UInt32" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.UInt32" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060000DF RID: 223 RVA: 0x00005416 File Offset: 0x00003616
		[CLSCompliant(false)]
		public static explicit operator uint(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToUInt32(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt32" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt32" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt32" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.UInt32" /> value.</exception>
		// Token: 0x060000E0 RID: 224 RVA: 0x00005434 File Offset: 0x00003634
		[CLSCompliant(false)]
		public static explicit operator uint?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new uint?(XmlConvert.ToUInt32(attribute.value));
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to an <see cref="T:System.Int64" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Int64" />.</param>
		/// <returns>A <see cref="T:System.Int64" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Int64" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060000E1 RID: 225 RVA: 0x0000545E File Offset: 0x0000365E
		[CLSCompliant(false)]
		public static explicit operator long(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToInt64(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int64" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int64" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int64" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Int64" /> value.</exception>
		// Token: 0x060000E2 RID: 226 RVA: 0x0000547C File Offset: 0x0000367C
		[CLSCompliant(false)]
		public static explicit operator long?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new long?(XmlConvert.ToInt64(attribute.value));
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.UInt64" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.UInt64" />.</param>
		/// <returns>A <see cref="T:System.UInt64" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.UInt64" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060000E3 RID: 227 RVA: 0x000054A6 File Offset: 0x000036A6
		[CLSCompliant(false)]
		public static explicit operator ulong(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToUInt64(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt64" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt64" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt64" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.UInt64" /> value.</exception>
		// Token: 0x060000E4 RID: 228 RVA: 0x000054C4 File Offset: 0x000036C4
		[CLSCompliant(false)]
		public static explicit operator ulong?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new ulong?(XmlConvert.ToUInt64(attribute.value));
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Single" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Single" />.</param>
		/// <returns>A <see cref="T:System.Single" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Single" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060000E5 RID: 229 RVA: 0x000054EE File Offset: 0x000036EE
		[CLSCompliant(false)]
		public static explicit operator float(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToSingle(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Single" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Single" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Single" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Single" /> value.</exception>
		// Token: 0x060000E6 RID: 230 RVA: 0x0000550C File Offset: 0x0000370C
		[CLSCompliant(false)]
		public static explicit operator float?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new float?(XmlConvert.ToSingle(attribute.value));
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Double" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Double" />.</param>
		/// <returns>A <see cref="T:System.Double" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Double" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060000E7 RID: 231 RVA: 0x00005536 File Offset: 0x00003736
		[CLSCompliant(false)]
		public static explicit operator double(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToDouble(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Double" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Double" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Double" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Double" /> value.</exception>
		// Token: 0x060000E8 RID: 232 RVA: 0x00005554 File Offset: 0x00003754
		[CLSCompliant(false)]
		public static explicit operator double?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new double?(XmlConvert.ToDouble(attribute.value));
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Decimal" />.</param>
		/// <returns>A <see cref="T:System.Decimal" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Decimal" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060000E9 RID: 233 RVA: 0x0000557E File Offset: 0x0000377E
		[CLSCompliant(false)]
		public static explicit operator decimal(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToDecimal(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Decimal" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Decimal" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Decimal" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Decimal" /> value.</exception>
		// Token: 0x060000EA RID: 234 RVA: 0x0000559C File Offset: 0x0000379C
		[CLSCompliant(false)]
		public static explicit operator decimal?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new decimal?(XmlConvert.ToDecimal(attribute.value));
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.DateTime" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.DateTime" />.</param>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.DateTime" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060000EB RID: 235 RVA: 0x000055C6 File Offset: 0x000037C6
		[CLSCompliant(false)]
		public static explicit operator DateTime(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return DateTime.Parse(attribute.value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTime" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTime" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTime" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.DateTime" /> value.</exception>
		// Token: 0x060000EC RID: 236 RVA: 0x000055EC File Offset: 0x000037EC
		[CLSCompliant(false)]
		public static explicit operator DateTime?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new DateTime?(DateTime.Parse(attribute.value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind));
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.DateTimeOffset" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.DateTimeOffset" />.</param>
		/// <returns>A <see cref="T:System.DateTimeOffset" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.DateTimeOffset" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060000ED RID: 237 RVA: 0x00005620 File Offset: 0x00003820
		[CLSCompliant(false)]
		public static explicit operator DateTimeOffset(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToDateTimeOffset(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTimeOffset" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTimeOffset" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTimeOffset" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.DateTimeOffset" /> value.</exception>
		// Token: 0x060000EE RID: 238 RVA: 0x0000563C File Offset: 0x0000383C
		[CLSCompliant(false)]
		public static explicit operator DateTimeOffset?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new DateTimeOffset?(XmlConvert.ToDateTimeOffset(attribute.value));
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.TimeSpan" />.</param>
		/// <returns>A <see cref="T:System.TimeSpan" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.TimeSpan" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060000EF RID: 239 RVA: 0x00005666 File Offset: 0x00003866
		[CLSCompliant(false)]
		public static explicit operator TimeSpan(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToTimeSpan(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.TimeSpan" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.TimeSpan" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.TimeSpan" /> value.</exception>
		// Token: 0x060000F0 RID: 240 RVA: 0x00005684 File Offset: 0x00003884
		[CLSCompliant(false)]
		public static explicit operator TimeSpan?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new TimeSpan?(XmlConvert.ToTimeSpan(attribute.value));
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Guid" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Guid" />.</param>
		/// <returns>A <see cref="T:System.Guid" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Guid" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060000F1 RID: 241 RVA: 0x000056AE File Offset: 0x000038AE
		[CLSCompliant(false)]
		public static explicit operator Guid(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToGuid(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Guid" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Guid" />.</param>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Guid" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Guid" /> value.</exception>
		// Token: 0x060000F2 RID: 242 RVA: 0x000056CC File Offset: 0x000038CC
		[CLSCompliant(false)]
		public static explicit operator Guid?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new Guid?(XmlConvert.ToGuid(attribute.value));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000056F6 File Offset: 0x000038F6
		internal int GetDeepHashCode()
		{
			return this.name.GetHashCode() ^ this.value.GetHashCode();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005710 File Offset: 0x00003910
		internal string GetPrefixOfNamespace(XNamespace ns)
		{
			string namespaceName = ns.NamespaceName;
			if (namespaceName.Length == 0)
			{
				return string.Empty;
			}
			if (this.parent != null)
			{
				return ((XElement)this.parent).GetPrefixOfNamespace(ns);
			}
			if (namespaceName == "http://www.w3.org/XML/1998/namespace")
			{
				return "xml";
			}
			if (namespaceName == "http://www.w3.org/2000/xmlns/")
			{
				return "xmlns";
			}
			return null;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000576C File Offset: 0x0000396C
		private static void ValidateAttribute(XName name, string value)
		{
			string namespaceName = name.NamespaceName;
			if (namespaceName == "http://www.w3.org/2000/xmlns/")
			{
				if (value.Length == 0)
				{
					throw new ArgumentException(SR.Format("The prefix '{0}' cannot be bound to the empty namespace name.", name.LocalName));
				}
				if (value == "http://www.w3.org/XML/1998/namespace")
				{
					if (name.LocalName != "xml")
					{
						throw new ArgumentException("The prefix 'xml' is bound to the namespace name 'http://www.w3.org/XML/1998/namespace'. Other prefixes must not be bound to this namespace name, and it must not be declared as the default namespace.");
					}
				}
				else
				{
					if (value == "http://www.w3.org/2000/xmlns/")
					{
						throw new ArgumentException("The prefix 'xmlns' is bound to the namespace name 'http://www.w3.org/2000/xmlns/'. It must not be declared. Other prefixes must not be bound to this namespace name, and it must not be declared as the default namespace.");
					}
					string localName = name.LocalName;
					if (localName == "xml")
					{
						throw new ArgumentException("The prefix 'xml' is bound to the namespace name 'http://www.w3.org/XML/1998/namespace'. Other prefixes must not be bound to this namespace name, and it must not be declared as the default namespace.");
					}
					if (localName == "xmlns")
					{
						throw new ArgumentException("The prefix 'xmlns' is bound to the namespace name 'http://www.w3.org/2000/xmlns/'. It must not be declared. Other prefixes must not be bound to this namespace name, and it must not be declared as the default namespace.");
					}
				}
			}
			else if (namespaceName.Length == 0 && name.LocalName == "xmlns")
			{
				if (value == "http://www.w3.org/XML/1998/namespace")
				{
					throw new ArgumentException("The prefix 'xml' is bound to the namespace name 'http://www.w3.org/XML/1998/namespace'. Other prefixes must not be bound to this namespace name, and it must not be declared as the default namespace.");
				}
				if (value == "http://www.w3.org/2000/xmlns/")
				{
					throw new ArgumentException("The prefix 'xmlns' is bound to the namespace name 'http://www.w3.org/2000/xmlns/'. It must not be declared. Other prefixes must not be bound to this namespace name, and it must not be declared as the default namespace.");
				}
			}
		}

		// Token: 0x04000087 RID: 135
		internal XAttribute next;

		// Token: 0x04000088 RID: 136
		internal XName name;

		// Token: 0x04000089 RID: 137
		internal string value;
	}
}
