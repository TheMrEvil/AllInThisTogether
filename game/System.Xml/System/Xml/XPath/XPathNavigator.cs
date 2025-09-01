using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Schema;
using MS.Internal.Xml.XPath;

namespace System.Xml.XPath
{
	/// <summary>Provides a cursor model for navigating and editing XML data.</summary>
	// Token: 0x02000259 RID: 601
	[DebuggerDisplay("{debuggerDisplayProxy}")]
	public abstract class XPathNavigator : XPathItem, ICloneable, IXPathNavigable, IXmlNamespaceResolver
	{
		/// <summary>Gets the text value of the current node.</summary>
		/// <returns>A <see langword="string" /> that contains the text value of the current node.</returns>
		// Token: 0x06001605 RID: 5637 RVA: 0x00085626 File Offset: 0x00083826
		public override string ToString()
		{
			return this.Value;
		}

		/// <summary>Gets a value indicating if the current node represents an XPath node.</summary>
		/// <returns>Always returns <see langword="true" />.</returns>
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x0001222F File Offset: 0x0001042F
		public sealed override bool IsNode
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaType" /> information for the current node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" /> object; default is <see langword="null" />.</returns>
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x00085630 File Offset: 0x00083830
		public override XmlSchemaType XmlType
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo == null || schemaInfo.Validity != XmlSchemaValidity.Valid)
				{
					return null;
				}
				XmlSchemaType memberType = schemaInfo.MemberType;
				if (memberType != null)
				{
					return memberType;
				}
				return schemaInfo.SchemaType;
			}
		}

		/// <summary>Sets the value of the current node.</summary>
		/// <param name="value">The new value of the node.</param>
		/// <exception cref="T:System.ArgumentNullException">The value parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on the root node, a namespace node, or the specified value is invalid.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06001608 RID: 5640 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public virtual void SetValue(string value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the current node as a boxed object of the most appropriate .NET Framework type.</summary>
		/// <returns>The current node as a boxed object of the most appropriate .NET Framework type.</returns>
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x00085664 File Offset: 0x00083864
		public override object TypedValue
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ChangeType(this.Value, datatype.ValueType, this);
							}
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ChangeType(datatype.ParseValue(this.Value, this.NameTable, this), datatype.ValueType, this);
							}
						}
					}
				}
				return this.Value;
			}
		}

		/// <summary>Sets the typed value of the current node.</summary>
		/// <param name="typedValue">The new typed value of the node.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support the type of the object specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value specified cannot be <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element or attribute node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x0600160A RID: 5642 RVA: 0x000856FC File Offset: 0x000838FC
		public virtual void SetTypedValue(object typedValue)
		{
			if (typedValue == null)
			{
				throw new ArgumentNullException("typedValue");
			}
			XPathNodeType nodeType = this.NodeType;
			if (nodeType - XPathNodeType.Element > 1)
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
			}
			string text = null;
			IXmlSchemaInfo schemaInfo = this.SchemaInfo;
			if (schemaInfo != null)
			{
				XmlSchemaType schemaType = schemaInfo.SchemaType;
				if (schemaType != null)
				{
					text = schemaType.ValueConverter.ToString(typedValue, this);
					XmlSchemaDatatype datatype = schemaType.Datatype;
					if (datatype != null)
					{
						datatype.ParseValue(text, this.NameTable, this);
					}
				}
			}
			if (text == null)
			{
				text = XmlUntypedConverter.Untyped.ToString(typedValue, this);
			}
			this.SetValue(text);
		}

		/// <summary>Gets the .NET Framework <see cref="T:System.Type" /> of the current node.</summary>
		/// <returns>The .NET Framework <see cref="T:System.Type" /> of the current node. The default value is <see cref="T:System.String" />.</returns>
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x0008578C File Offset: 0x0008398C
		public override Type ValueType
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return datatype.ValueType;
							}
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return datatype.ValueType;
							}
						}
					}
				}
				return typeof(string);
			}
		}

		/// <summary>Gets the current node's value as a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The current node's value as a <see cref="T:System.Boolean" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Boolean" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Boolean" /> is not valid.</exception>
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x000857F8 File Offset: 0x000839F8
		public override bool ValueAsBoolean
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToBoolean(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToBoolean(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToBoolean(this.Value);
			}
		}

		/// <summary>Gets the current node's value as a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The current node's value as a <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.DateTime" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.DateTime" /> is not valid.</exception>
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x00085884 File Offset: 0x00083A84
		public override DateTime ValueAsDateTime
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToDateTime(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToDateTime(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToDateTime(this.Value);
			}
		}

		/// <summary>Gets the current node's value as a <see cref="T:System.Double" />.</summary>
		/// <returns>The current node's value as a <see cref="T:System.Double" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Double" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Double" /> is not valid.</exception>
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x00085910 File Offset: 0x00083B10
		public override double ValueAsDouble
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToDouble(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToDouble(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToDouble(this.Value);
			}
		}

		/// <summary>Gets the current node's value as an <see cref="T:System.Int32" />.</summary>
		/// <returns>The current node's value as an <see cref="T:System.Int32" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Int32" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int32" /> is not valid.</exception>
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x0008599C File Offset: 0x00083B9C
		public override int ValueAsInt
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToInt32(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToInt32(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToInt32(this.Value);
			}
		}

		/// <summary>Gets the current node's value as an <see cref="T:System.Int64" />.</summary>
		/// <returns>The current node's value as an <see cref="T:System.Int64" />.</returns>
		/// <exception cref="T:System.FormatException">The current node's string value cannot be converted to a <see cref="T:System.Int64" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int64" /> is not valid.</exception>
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x00085A28 File Offset: 0x00083C28
		public override long ValueAsLong
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToInt64(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToInt64(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToInt64(this.Value);
			}
		}

		/// <summary>Gets the current node's value as the <see cref="T:System.Type" /> specified, using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes.</summary>
		/// <param name="returnType">The <see cref="T:System.Type" /> to return the current node's value as.</param>
		/// <param name="nsResolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes.</param>
		/// <returns>The value of the current node as the <see cref="T:System.Type" /> requested.</returns>
		/// <exception cref="T:System.FormatException">The current node's value is not in the correct format for the target type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		// Token: 0x06001611 RID: 5649 RVA: 0x00085AB4 File Offset: 0x00083CB4
		public override object ValueAs(Type returnType, IXmlNamespaceResolver nsResolver)
		{
			if (nsResolver == null)
			{
				nsResolver = this;
			}
			IXmlSchemaInfo schemaInfo = this.SchemaInfo;
			if (schemaInfo != null)
			{
				if (schemaInfo.Validity == XmlSchemaValidity.Valid)
				{
					XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
					if (xmlSchemaType == null)
					{
						xmlSchemaType = schemaInfo.SchemaType;
					}
					if (xmlSchemaType != null)
					{
						return xmlSchemaType.ValueConverter.ChangeType(this.Value, returnType, nsResolver);
					}
				}
				else
				{
					XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
					if (xmlSchemaType != null)
					{
						XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
						if (datatype != null)
						{
							return xmlSchemaType.ValueConverter.ChangeType(datatype.ParseValue(this.Value, this.NameTable, nsResolver), returnType, nsResolver);
						}
					}
				}
			}
			return XmlUntypedConverter.Untyped.ChangeType(this.Value, returnType, nsResolver);
		}

		/// <summary>Creates a new copy of the <see cref="T:System.Xml.XPath.XPathNavigator" /> object.</summary>
		/// <returns>A new copy of the <see cref="T:System.Xml.XPath.XPathNavigator" /> object.</returns>
		// Token: 0x06001612 RID: 5650 RVA: 0x00085B49 File Offset: 0x00083D49
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>Returns a copy of the <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> copy of this <see cref="T:System.Xml.XPath.XPathNavigator" />.</returns>
		// Token: 0x06001613 RID: 5651 RVA: 0x00085B49 File Offset: 0x00083D49
		public virtual XPathNavigator CreateNavigator()
		{
			return this.Clone();
		}

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Xml.XmlNameTable" /> of the <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNameTable" /> object enabling you to get the atomized version of a <see cref="T:System.String" /> within the XML document.</returns>
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001614 RID: 5652
		public abstract XmlNameTable NameTable { get; }

		/// <summary>Gets the namespace URI for the specified prefix.</summary>
		/// <param name="prefix">The prefix whose namespace URI you want to resolve. To match the default namespace, pass <see cref="F:System.String.Empty" />.</param>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace URI assigned to the namespace prefix specified; <see langword="null" /> if no namespace URI is assigned to the prefix specified. The <see cref="T:System.String" /> returned is atomized.</returns>
		// Token: 0x06001615 RID: 5653 RVA: 0x00085B54 File Offset: 0x00083D54
		public virtual string LookupNamespace(string prefix)
		{
			if (prefix == null)
			{
				return null;
			}
			if (this.NodeType != XPathNodeType.Element)
			{
				XPathNavigator xpathNavigator = this.Clone();
				if (xpathNavigator.MoveToParent())
				{
					return xpathNavigator.LookupNamespace(prefix);
				}
			}
			else if (this.MoveToNamespace(prefix))
			{
				string value = this.Value;
				this.MoveToParent();
				return value;
			}
			if (prefix.Length == 0)
			{
				return string.Empty;
			}
			if (prefix == "xml")
			{
				return "http://www.w3.org/XML/1998/namespace";
			}
			if (prefix == "xmlns")
			{
				return "http://www.w3.org/2000/xmlns/";
			}
			return null;
		}

		/// <summary>Gets the prefix declared for the specified namespace URI.</summary>
		/// <param name="namespaceURI">The namespace URI to resolve for the prefix.</param>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace prefix assigned to the namespace URI specified; otherwise, <see cref="F:System.String.Empty" /> if no prefix is assigned to the namespace URI specified. The <see cref="T:System.String" /> returned is atomized.</returns>
		// Token: 0x06001616 RID: 5654 RVA: 0x00085BD4 File Offset: 0x00083DD4
		public virtual string LookupPrefix(string namespaceURI)
		{
			if (namespaceURI == null)
			{
				return null;
			}
			XPathNavigator xpathNavigator = this.Clone();
			if (this.NodeType != XPathNodeType.Element)
			{
				if (xpathNavigator.MoveToParent())
				{
					return xpathNavigator.LookupPrefix(namespaceURI);
				}
			}
			else if (xpathNavigator.MoveToFirstNamespace(XPathNamespaceScope.All))
			{
				while (!(namespaceURI == xpathNavigator.Value))
				{
					if (!xpathNavigator.MoveToNextNamespace(XPathNamespaceScope.All))
					{
						goto IL_4C;
					}
				}
				return xpathNavigator.LocalName;
			}
			IL_4C:
			if (namespaceURI == this.LookupNamespace(string.Empty))
			{
				return string.Empty;
			}
			if (namespaceURI == "http://www.w3.org/XML/1998/namespace")
			{
				return "xml";
			}
			if (namespaceURI == "http://www.w3.org/2000/xmlns/")
			{
				return "xmlns";
			}
			return null;
		}

		/// <summary>Returns the in-scope namespaces of the current node.</summary>
		/// <param name="scope">An <see cref="T:System.Xml.XmlNamespaceScope" /> value specifying the namespaces to return.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IDictionary`2" /> collection of namespace names keyed by prefix.</returns>
		// Token: 0x06001617 RID: 5655 RVA: 0x00085C70 File Offset: 0x00083E70
		public virtual IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			XPathNodeType nodeType = this.NodeType;
			if ((nodeType != XPathNodeType.Element && scope != XmlNamespaceScope.Local) || nodeType == XPathNodeType.Attribute || nodeType == XPathNodeType.Namespace)
			{
				XPathNavigator xpathNavigator = this.Clone();
				if (xpathNavigator.MoveToParent())
				{
					return xpathNavigator.GetNamespacesInScope(scope);
				}
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (scope == XmlNamespaceScope.All)
			{
				dictionary["xml"] = "http://www.w3.org/XML/1998/namespace";
			}
			if (this.MoveToFirstNamespace((XPathNamespaceScope)scope))
			{
				do
				{
					string localName = this.LocalName;
					string value = this.Value;
					if (localName.Length != 0 || value.Length != 0 || scope == XmlNamespaceScope.Local)
					{
						dictionary[localName] = value;
					}
				}
				while (this.MoveToNextNamespace((XPathNamespaceScope)scope));
				this.MoveToParent();
			}
			return dictionary;
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEqualityComparer" /> used for equality comparison of <see cref="T:System.Xml.XPath.XPathNavigator" /> objects.</summary>
		/// <returns>An <see cref="T:System.Collections.IEqualityComparer" /> used for equality comparison of <see cref="T:System.Xml.XPath.XPathNavigator" /> objects.</returns>
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x00085D0B File Offset: 0x00083F0B
		public static IEqualityComparer NavigatorComparer
		{
			get
			{
				return XPathNavigator.comparer;
			}
		}

		/// <summary>When overridden in a derived class, creates a new <see cref="T:System.Xml.XPath.XPathNavigator" /> positioned at the same node as this <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <returns>A new <see cref="T:System.Xml.XPath.XPathNavigator" /> positioned at the same node as this <see cref="T:System.Xml.XPath.XPathNavigator" />.</returns>
		// Token: 0x06001619 RID: 5657
		public abstract XPathNavigator Clone();

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Xml.XPath.XPathNodeType" /> of the current node.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XPath.XPathNodeType" /> values representing the current node.</returns>
		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600161A RID: 5658
		public abstract XPathNodeType NodeType { get; }

		/// <summary>When overridden in a derived class, gets the <see cref="P:System.Xml.XPath.XPathNavigator.Name" /> of the current node without any namespace prefix.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the local name of the current node, or <see cref="F:System.String.Empty" /> if the current node does not have a name (for example, text or comment nodes).</returns>
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x0600161B RID: 5659
		public abstract string LocalName { get; }

		/// <summary>When overridden in a derived class, gets the qualified name of the current node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the qualified <see cref="P:System.Xml.XPath.XPathNavigator.Name" /> of the current node, or <see cref="F:System.String.Empty" /> if the current node does not have a name (for example, text or comment nodes).</returns>
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x0600161C RID: 5660
		public abstract string Name { get; }

		/// <summary>When overridden in a derived class, gets the namespace URI of the current node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace URI of the current node, or <see cref="F:System.String.Empty" /> if the current node has no namespace URI.</returns>
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600161D RID: 5661
		public abstract string NamespaceURI { get; }

		/// <summary>When overridden in a derived class, gets the namespace prefix associated with the current node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace prefix associated with the current node.</returns>
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600161E RID: 5662
		public abstract string Prefix { get; }

		/// <summary>When overridden in a derived class, gets the base URI for the current node.</summary>
		/// <returns>The location from which the node was loaded, or <see cref="F:System.String.Empty" /> if there is no value.</returns>
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x0600161F RID: 5663
		public abstract string BaseURI { get; }

		/// <summary>When overridden in a derived class, gets a value indicating whether the current node is an empty element without an end element tag.</summary>
		/// <returns>
		///     <see langword="true" /> if the current node is an empty element; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001620 RID: 5664
		public abstract bool IsEmptyElement { get; }

		/// <summary>Gets the xml:lang scope for the current node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value of the xml:lang scope, or <see cref="F:System.String.Empty" /> if the current node has no xml:lang scope value to return.</returns>
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x00085D14 File Offset: 0x00083F14
		public virtual string XmlLang
		{
			get
			{
				XPathNavigator xpathNavigator = this.Clone();
				while (!xpathNavigator.MoveToAttribute("lang", "http://www.w3.org/XML/1998/namespace"))
				{
					if (!xpathNavigator.MoveToParent())
					{
						return string.Empty;
					}
				}
				return xpathNavigator.Value;
			}
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlReader" /> object that contains the current node and its child nodes.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> object that contains the current node and its child nodes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node or the root node.</exception>
		// Token: 0x06001622 RID: 5666 RVA: 0x00085D50 File Offset: 0x00083F50
		public virtual XmlReader ReadSubtree()
		{
			XPathNodeType nodeType = this.NodeType;
			if (nodeType > XPathNodeType.Element)
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
			}
			return this.CreateReader();
		}

		/// <summary>Streams the current node and its child nodes to the <see cref="T:System.Xml.XmlWriter" /> object specified.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> object to stream to.</param>
		// Token: 0x06001623 RID: 5667 RVA: 0x00085D7E File Offset: 0x00083F7E
		public virtual void WriteSubtree(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteNode(this, true);
		}

		/// <summary>Used by <see cref="T:System.Xml.XPath.XPathNavigator" /> implementations which provide a "virtualized" XML view over a store, to provide access to underlying objects.</summary>
		/// <returns>The default is <see langword="null" />.</returns>
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public virtual object UnderlyingObject
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a value indicating whether the current node has any attributes.</summary>
		/// <returns>Returns <see langword="true" /> if the current node has attributes; returns <see langword="false" /> if the current node has no attributes, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node.</returns>
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x00085D96 File Offset: 0x00083F96
		public virtual bool HasAttributes
		{
			get
			{
				if (!this.MoveToFirstAttribute())
				{
					return false;
				}
				this.MoveToParent();
				return true;
			}
		}

		/// <summary>Gets the value of the attribute with the specified local name and namespace URI.</summary>
		/// <param name="localName">The local name of the attribute. <paramref name="localName" /> is case-sensitive.</param>
		/// <param name="namespaceURI">The namespace URI of the attribute.</param>
		/// <returns>A <see cref="T:System.String" /> that contains the value of the specified attribute; <see cref="F:System.String.Empty" /> if a matching attribute is not found, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node.</returns>
		// Token: 0x06001626 RID: 5670 RVA: 0x00085DAA File Offset: 0x00083FAA
		public virtual string GetAttribute(string localName, string namespaceURI)
		{
			if (!this.MoveToAttribute(localName, namespaceURI))
			{
				return "";
			}
			string value = this.Value;
			this.MoveToParent();
			return value;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the attribute with the matching local name and namespace URI.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="namespaceURI">The namespace URI of the attribute; <see langword="null" /> for an empty namespace.</param>
		/// <returns>Returns <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the attribute; otherwise, <see langword="false" />. If <see langword="false" />, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06001627 RID: 5671 RVA: 0x00085DC9 File Offset: 0x00083FC9
		public virtual bool MoveToAttribute(string localName, string namespaceURI)
		{
			if (this.MoveToFirstAttribute())
			{
				while (!(localName == this.LocalName) || !(namespaceURI == this.NamespaceURI))
				{
					if (!this.MoveToNextAttribute())
					{
						this.MoveToParent();
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the first attribute of the current node.</summary>
		/// <returns>Returns <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the first attribute of the current node; otherwise, <see langword="false" />. If <see langword="false" />, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06001628 RID: 5672
		public abstract bool MoveToFirstAttribute();

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next attribute.</summary>
		/// <returns>Returns <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next attribute; <see langword="false" /> if there are no more attributes. If <see langword="false" />, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06001629 RID: 5673
		public abstract bool MoveToNextAttribute();

		/// <summary>Returns the value of the namespace node corresponding to the specified local name.</summary>
		/// <param name="name">The local name of the namespace node.</param>
		/// <returns>A <see cref="T:System.String" /> that contains the value of the namespace node; <see cref="F:System.String.Empty" /> if a matching namespace node is not found, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node.</returns>
		// Token: 0x0600162A RID: 5674 RVA: 0x00085E04 File Offset: 0x00084004
		public virtual string GetNamespace(string name)
		{
			if (this.MoveToNamespace(name))
			{
				string value = this.Value;
				this.MoveToParent();
				return value;
			}
			if (name == "xml")
			{
				return "http://www.w3.org/XML/1998/namespace";
			}
			if (name == "xmlns")
			{
				return "http://www.w3.org/2000/xmlns/";
			}
			return string.Empty;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the namespace node with the specified namespace prefix.</summary>
		/// <param name="name">The namespace prefix of the namespace node.</param>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the specified namespace; <see langword="false" /> if a matching namespace node was not found, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node. If <see langword="false" />, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x0600162B RID: 5675 RVA: 0x00085E53 File Offset: 0x00084053
		public virtual bool MoveToNamespace(string name)
		{
			if (this.MoveToFirstNamespace(XPathNamespaceScope.All))
			{
				while (!(name == this.LocalName))
				{
					if (!this.MoveToNextNamespace(XPathNamespaceScope.All))
					{
						this.MoveToParent();
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the first namespace node that matches the <see cref="T:System.Xml.XPath.XPathNamespaceScope" /> specified.</summary>
		/// <param name="namespaceScope">An <see cref="T:System.Xml.XPath.XPathNamespaceScope" /> value describing the namespace scope. </param>
		/// <returns>Returns <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the first namespace node; otherwise, <see langword="false" />. If <see langword="false" />, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x0600162C RID: 5676
		public abstract bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope);

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next namespace node matching the <see cref="T:System.Xml.XPath.XPathNamespaceScope" /> specified.</summary>
		/// <param name="namespaceScope">An <see cref="T:System.Xml.XPath.XPathNamespaceScope" /> value describing the namespace scope. </param>
		/// <returns>Returns <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next namespace node; otherwise, <see langword="false" />. If <see langword="false" />, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x0600162D RID: 5677
		public abstract bool MoveToNextNamespace(XPathNamespaceScope namespaceScope);

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to first namespace node of the current node.</summary>
		/// <returns>Returns <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the first namespace node; otherwise, <see langword="false" />. If <see langword="false" />, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x0600162E RID: 5678 RVA: 0x00085E7F File Offset: 0x0008407F
		public bool MoveToFirstNamespace()
		{
			return this.MoveToFirstNamespace(XPathNamespaceScope.All);
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next namespace node.</summary>
		/// <returns>Returns <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next namespace node; otherwise, <see langword="false" />. If <see langword="false" />, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x0600162F RID: 5679 RVA: 0x00085E88 File Offset: 0x00084088
		public bool MoveToNextNamespace()
		{
			return this.MoveToNextNamespace(XPathNamespaceScope.All);
		}

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next sibling node of the current node.</summary>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next sibling node; otherwise, <see langword="false" /> if there are no more siblings or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute node. If <see langword="false" />, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06001630 RID: 5680
		public abstract bool MoveToNext();

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the previous sibling node of the current node.</summary>
		/// <returns>Returns <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the previous sibling node; otherwise, <see langword="false" /> if there is no previous sibling node or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute node. If <see langword="false" />, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06001631 RID: 5681
		public abstract bool MoveToPrevious();

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the first sibling node of the current node.</summary>
		/// <returns>Returns <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the first sibling node of the current node; <see langword="false" /> if there is no first sibling, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute node. If the <see cref="T:System.Xml.XPath.XPathNavigator" /> is already positioned on the first sibling, <see cref="T:System.Xml.XPath.XPathNavigator" /> will return <see langword="true" /> and will not move its position.If <see cref="M:System.Xml.XPath.XPathNavigator.MoveToFirst" /> returns <see langword="false" /> because there is no first sibling, or if <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06001632 RID: 5682 RVA: 0x00085E94 File Offset: 0x00084094
		public virtual bool MoveToFirst()
		{
			XPathNodeType nodeType = this.NodeType;
			return nodeType - XPathNodeType.Attribute > 1 && this.MoveToParent() && this.MoveToFirstChild();
		}

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the first child node of the current node.</summary>
		/// <returns>Returns <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the first child node of the current node; otherwise, <see langword="false" />. If <see langword="false" />, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06001633 RID: 5683
		public abstract bool MoveToFirstChild();

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the parent node of the current node.</summary>
		/// <returns>Returns <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the parent node of the current node; otherwise, <see langword="false" />. If <see langword="false" />, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06001634 RID: 5684
		public abstract bool MoveToParent();

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the root node that the current node belongs to.</summary>
		// Token: 0x06001635 RID: 5685 RVA: 0x00085EC0 File Offset: 0x000840C0
		public virtual void MoveToRoot()
		{
			while (this.MoveToParent())
			{
			}
		}

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the same position as the specified <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <param name="other">The <see cref="T:System.Xml.XPath.XPathNavigator" /> positioned on the node that you want to move to. </param>
		/// <returns>Returns <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the same position as the specified <see cref="T:System.Xml.XPath.XPathNavigator" />; otherwise, <see langword="false" />. If <see langword="false" />, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06001636 RID: 5686
		public abstract bool MoveTo(XPathNavigator other);

		/// <summary>When overridden in a derived class, moves to the node that has an attribute of type ID whose value matches the specified <see cref="T:System.String" />.</summary>
		/// <param name="id">A <see cref="T:System.String" /> representing the ID value of the node to which you want to move.</param>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving; otherwise, <see langword="false" />. If <see langword="false" />, the position of the navigator is unchanged.</returns>
		// Token: 0x06001637 RID: 5687
		public abstract bool MoveToId(string id);

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the child node with the local name and namespace URI specified.</summary>
		/// <param name="localName">The local name of the child node to move to.</param>
		/// <param name="namespaceURI">The namespace URI of the child node to move to.</param>
		/// <returns>Returns <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the child node; otherwise, <see langword="false" />. If <see langword="false" />, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06001638 RID: 5688 RVA: 0x00085ECC File Offset: 0x000840CC
		public virtual bool MoveToChild(string localName, string namespaceURI)
		{
			if (this.MoveToFirstChild())
			{
				while (this.NodeType != XPathNodeType.Element || !(localName == this.LocalName) || !(namespaceURI == this.NamespaceURI))
				{
					if (!this.MoveToNext())
					{
						this.MoveToParent();
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the child node of the <see cref="T:System.Xml.XPath.XPathNodeType" /> specified.</summary>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the child node to move to.</param>
		/// <returns>Returns <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the child node; otherwise, <see langword="false" />. If <see langword="false" />, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x06001639 RID: 5689 RVA: 0x00085F18 File Offset: 0x00084118
		public virtual bool MoveToChild(XPathNodeType type)
		{
			if (this.MoveToFirstChild())
			{
				int contentKindMask = XPathNavigator.GetContentKindMask(type);
				while ((1 << (int)this.NodeType & contentKindMask) == 0)
				{
					if (!this.MoveToNext())
					{
						this.MoveToParent();
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the element with the local name and namespace URI specified in document order.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> moved successfully; otherwise <see langword="false" />.</returns>
		// Token: 0x0600163A RID: 5690 RVA: 0x00085F55 File Offset: 0x00084155
		public virtual bool MoveToFollowing(string localName, string namespaceURI)
		{
			return this.MoveToFollowing(localName, namespaceURI, null);
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the element with the local name and namespace URI specified, to the boundary specified, in document order.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <param name="end">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the element boundary which the current <see cref="T:System.Xml.XPath.XPathNavigator" /> will not move past while searching for the following element.</param>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> moved successfully; otherwise <see langword="false" />.</returns>
		// Token: 0x0600163B RID: 5691 RVA: 0x00085F60 File Offset: 0x00084160
		public virtual bool MoveToFollowing(string localName, string namespaceURI, XPathNavigator end)
		{
			XPathNavigator other = this.Clone();
			XPathNodeType nodeType;
			if (end != null)
			{
				nodeType = end.NodeType;
				if (nodeType - XPathNodeType.Attribute <= 1)
				{
					end = end.Clone();
					end.MoveToNonDescendant();
				}
			}
			nodeType = this.NodeType;
			if (nodeType - XPathNodeType.Attribute <= 1 && !this.MoveToParent())
			{
				return false;
			}
			for (;;)
			{
				if (!this.MoveToFirstChild())
				{
					while (!this.MoveToNext())
					{
						if (!this.MoveToParent())
						{
							goto Block_6;
						}
					}
				}
				if (end != null && this.IsSamePosition(end))
				{
					goto Block_8;
				}
				if (this.NodeType == XPathNodeType.Element && !(localName != this.LocalName) && !(namespaceURI != this.NamespaceURI))
				{
					return true;
				}
			}
			Block_6:
			this.MoveTo(other);
			return false;
			Block_8:
			this.MoveTo(other);
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the following element of the <see cref="T:System.Xml.XPath.XPathNodeType" /> specified in document order.</summary>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the element. The <see cref="T:System.Xml.XPath.XPathNodeType" /> cannot be <see cref="F:System.Xml.XPath.XPathNodeType.Attribute" /> or <see cref="F:System.Xml.XPath.XPathNodeType.Namespace" />.</param>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> moved successfully; otherwise <see langword="false" />.</returns>
		// Token: 0x0600163C RID: 5692 RVA: 0x00086008 File Offset: 0x00084208
		public virtual bool MoveToFollowing(XPathNodeType type)
		{
			return this.MoveToFollowing(type, null);
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the following element of the <see cref="T:System.Xml.XPath.XPathNodeType" /> specified, to the boundary specified, in document order.</summary>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the element. The <see cref="T:System.Xml.XPath.XPathNodeType" /> cannot be <see cref="F:System.Xml.XPath.XPathNodeType.Attribute" /> or <see cref="F:System.Xml.XPath.XPathNodeType.Namespace" />.</param>
		/// <param name="end">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the element boundary which the current <see cref="T:System.Xml.XPath.XPathNavigator" /> will not move past while searching for the following element.</param>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> moved successfully; otherwise <see langword="false" />.</returns>
		// Token: 0x0600163D RID: 5693 RVA: 0x00086014 File Offset: 0x00084214
		public virtual bool MoveToFollowing(XPathNodeType type, XPathNavigator end)
		{
			XPathNavigator other = this.Clone();
			int contentKindMask = XPathNavigator.GetContentKindMask(type);
			XPathNodeType nodeType;
			if (end != null)
			{
				nodeType = end.NodeType;
				if (nodeType - XPathNodeType.Attribute <= 1)
				{
					end = end.Clone();
					end.MoveToNonDescendant();
				}
			}
			nodeType = this.NodeType;
			if (nodeType - XPathNodeType.Attribute <= 1 && !this.MoveToParent())
			{
				return false;
			}
			for (;;)
			{
				if (!this.MoveToFirstChild())
				{
					while (!this.MoveToNext())
					{
						if (!this.MoveToParent())
						{
							goto Block_6;
						}
					}
				}
				if (end != null && this.IsSamePosition(end))
				{
					goto Block_8;
				}
				if ((1 << (int)this.NodeType & contentKindMask) != 0)
				{
					return true;
				}
			}
			Block_6:
			this.MoveTo(other);
			return false;
			Block_8:
			this.MoveTo(other);
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next sibling node with the local name and namespace URI specified.</summary>
		/// <param name="localName">The local name of the next sibling node to move to.</param>
		/// <param name="namespaceURI">The namespace URI of the next sibling node to move to.</param>
		/// <returns>Returns <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next sibling node; <see langword="false" /> if there are no more siblings, or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute node. If <see langword="false" />, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x0600163E RID: 5694 RVA: 0x000860B0 File Offset: 0x000842B0
		public virtual bool MoveToNext(string localName, string namespaceURI)
		{
			XPathNavigator other = this.Clone();
			while (this.MoveToNext())
			{
				if (this.NodeType == XPathNodeType.Element && localName == this.LocalName && namespaceURI == this.NamespaceURI)
				{
					return true;
				}
			}
			this.MoveTo(other);
			return false;
		}

		/// <summary>Moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> to the next sibling node of the current node that matches the <see cref="T:System.Xml.XPath.XPathNodeType" /> specified.</summary>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the sibling node to move to.</param>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is successful moving to the next sibling node; otherwise, <see langword="false" /> if there are no more siblings or if the <see cref="T:System.Xml.XPath.XPathNavigator" /> is currently positioned on an attribute node. If <see langword="false" />, the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> is unchanged.</returns>
		// Token: 0x0600163F RID: 5695 RVA: 0x00086100 File Offset: 0x00084300
		public virtual bool MoveToNext(XPathNodeType type)
		{
			XPathNavigator other = this.Clone();
			int contentKindMask = XPathNavigator.GetContentKindMask(type);
			while (this.MoveToNext())
			{
				if ((1 << (int)this.NodeType & contentKindMask) != 0)
				{
					return true;
				}
			}
			this.MoveTo(other);
			return false;
		}

		/// <summary>Gets a value indicating whether the current node has any child nodes.</summary>
		/// <returns>
		///     <see langword="true" /> if the current node has any child nodes; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x0008613F File Offset: 0x0008433F
		public virtual bool HasChildren
		{
			get
			{
				if (this.MoveToFirstChild())
				{
					this.MoveToParent();
					return true;
				}
				return false;
			}
		}

		/// <summary>When overridden in a derived class, determines whether the current <see cref="T:System.Xml.XPath.XPathNavigator" /> is at the same position as the specified <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <param name="other">The <see cref="T:System.Xml.XPath.XPathNavigator" /> to compare to this <see cref="T:System.Xml.XPath.XPathNavigator" />.</param>
		/// <returns>
		///     <see langword="true" /> if the two <see cref="T:System.Xml.XPath.XPathNavigator" /> objects have the same position; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001641 RID: 5697
		public abstract bool IsSamePosition(XPathNavigator other);

		/// <summary>Determines whether the specified <see cref="T:System.Xml.XPath.XPathNavigator" /> is a descendant of the current <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <param name="nav">The <see cref="T:System.Xml.XPath.XPathNavigator" /> to compare to this <see cref="T:System.Xml.XPath.XPathNavigator" />.</param>
		/// <returns>
		///     <see langword="true" /> if the specified <see cref="T:System.Xml.XPath.XPathNavigator" /> is a descendant of the current <see cref="T:System.Xml.XPath.XPathNavigator" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001642 RID: 5698 RVA: 0x00086153 File Offset: 0x00084353
		public virtual bool IsDescendant(XPathNavigator nav)
		{
			if (nav != null)
			{
				nav = nav.Clone();
				while (nav.MoveToParent())
				{
					if (nav.IsSamePosition(this))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Compares the position of the current <see cref="T:System.Xml.XPath.XPathNavigator" /> with the position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> specified.</summary>
		/// <param name="nav">The <see cref="T:System.Xml.XPath.XPathNavigator" /> to compare against.</param>
		/// <returns>An <see cref="T:System.Xml.XmlNodeOrder" /> value representing the comparative position of the two <see cref="T:System.Xml.XPath.XPathNavigator" /> objects.</returns>
		// Token: 0x06001643 RID: 5699 RVA: 0x00086178 File Offset: 0x00084378
		public virtual XmlNodeOrder ComparePosition(XPathNavigator nav)
		{
			if (nav == null)
			{
				return XmlNodeOrder.Unknown;
			}
			if (this.IsSamePosition(nav))
			{
				return XmlNodeOrder.Same;
			}
			XPathNavigator xpathNavigator = this.Clone();
			XPathNavigator xpathNavigator2 = nav.Clone();
			int i = XPathNavigator.GetDepth(xpathNavigator.Clone());
			int j = XPathNavigator.GetDepth(xpathNavigator2.Clone());
			if (i > j)
			{
				while (i > j)
				{
					xpathNavigator.MoveToParent();
					i--;
				}
				if (xpathNavigator.IsSamePosition(xpathNavigator2))
				{
					return XmlNodeOrder.After;
				}
			}
			if (j > i)
			{
				while (j > i)
				{
					xpathNavigator2.MoveToParent();
					j--;
				}
				if (xpathNavigator.IsSamePosition(xpathNavigator2))
				{
					return XmlNodeOrder.Before;
				}
			}
			XPathNavigator xpathNavigator3 = xpathNavigator.Clone();
			XPathNavigator xpathNavigator4 = xpathNavigator2.Clone();
			while (xpathNavigator3.MoveToParent() && xpathNavigator4.MoveToParent())
			{
				if (xpathNavigator3.IsSamePosition(xpathNavigator4))
				{
					xpathNavigator.GetType().ToString() != "Microsoft.VisualStudio.Modeling.StoreNavigator";
					return this.CompareSiblings(xpathNavigator, xpathNavigator2);
				}
				xpathNavigator.MoveToParent();
				xpathNavigator2.MoveToParent();
			}
			return XmlNodeOrder.Unknown;
		}

		/// <summary>Gets the schema information that has been assigned to the current node as a result of schema validation.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> object that contains the schema information for the current node.</returns>
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x0001E525 File Offset: 0x0001C725
		public virtual IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this as IXmlSchemaInfo;
			}
		}

		/// <summary>Verifies that the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> conforms to the XML Schema definition language (XSD) schema provided.</summary>
		/// <param name="schemas">The <see cref="T:System.Xml.Schema.XmlSchemaSet" /> containing the schemas used to validate the XML data contained in the <see cref="T:System.Xml.XPath.XPathNavigator" />.</param>
		/// <param name="validationEventHandler">The <see cref="T:System.Xml.Schema.ValidationEventHandler" /> that receives information about schema validation warnings and errors.</param>
		/// <returns>
		///     <see langword="true" /> if no schema validation errors occurred; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">A schema validation error occurred, and no <see cref="T:System.Xml.Schema.ValidationEventHandler" /> was specified to handle validation errors.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on a node that is not an element, attribute, or the root node or there is not type information to perform validation.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="M:System.Xml.XPath.XPathNavigator.CheckValidity(System.Xml.Schema.XmlSchemaSet,System.Xml.Schema.ValidationEventHandler)" /> method was called with an <see cref="T:System.Xml.Schema.XmlSchemaSet" /> parameter when the <see cref="T:System.Xml.XPath.XPathNavigator" /> was not positioned on the root node of the XML data.</exception>
		// Token: 0x06001645 RID: 5701 RVA: 0x00086258 File Offset: 0x00084458
		public virtual bool CheckValidity(XmlSchemaSet schemas, ValidationEventHandler validationEventHandler)
		{
			XmlSchemaType xmlSchemaType = null;
			XmlSchemaElement xmlSchemaElement = null;
			XmlSchemaAttribute xmlSchemaAttribute = null;
			switch (this.NodeType)
			{
			case XPathNodeType.Root:
				if (schemas == null)
				{
					throw new InvalidOperationException(Res.GetString("An XmlSchemaSet must be provided to validate the document."));
				}
				xmlSchemaType = null;
				break;
			case XPathNodeType.Element:
			{
				if (schemas == null)
				{
					throw new InvalidOperationException(Res.GetString("An XmlSchemaSet must be provided to validate the document."));
				}
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					xmlSchemaType = schemaInfo.SchemaType;
					xmlSchemaElement = schemaInfo.SchemaElement;
				}
				if (xmlSchemaType == null && xmlSchemaElement == null)
				{
					throw new InvalidOperationException(Res.GetString("Element should have prior schema information to call this method.", null));
				}
				break;
			}
			case XPathNodeType.Attribute:
			{
				if (schemas == null)
				{
					throw new InvalidOperationException(Res.GetString("An XmlSchemaSet must be provided to validate the document."));
				}
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					xmlSchemaType = schemaInfo.SchemaType;
					xmlSchemaAttribute = schemaInfo.SchemaAttribute;
				}
				if (xmlSchemaType == null && xmlSchemaAttribute == null)
				{
					throw new InvalidOperationException(Res.GetString("Element should have prior schema information to call this method.", null));
				}
				break;
			}
			default:
				throw new InvalidOperationException(Res.GetString("Validate and CheckValidity are only allowed on Root or Element nodes.", null));
			}
			XmlReader xmlReader = this.CreateReader();
			XPathNavigator.CheckValidityHelper checkValidityHelper = new XPathNavigator.CheckValidityHelper(validationEventHandler, xmlReader as XPathNavigatorReader);
			validationEventHandler = new ValidationEventHandler(checkValidityHelper.ValidationCallback);
			XmlReader validatingReader = this.GetValidatingReader(xmlReader, schemas, validationEventHandler, xmlSchemaType, xmlSchemaElement, xmlSchemaAttribute);
			while (validatingReader.Read())
			{
			}
			return checkValidityHelper.IsValid;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00086380 File Offset: 0x00084580
		private XmlReader GetValidatingReader(XmlReader reader, XmlSchemaSet schemas, ValidationEventHandler validationEvent, XmlSchemaType schemaType, XmlSchemaElement schemaElement, XmlSchemaAttribute schemaAttribute)
		{
			if (schemaAttribute != null)
			{
				return schemaAttribute.Validate(reader, null, schemas, validationEvent);
			}
			if (schemaElement != null)
			{
				return schemaElement.Validate(reader, null, schemas, validationEvent);
			}
			if (schemaType != null)
			{
				return schemaType.Validate(reader, null, schemas, validationEvent);
			}
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Auto;
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			xmlReaderSettings.Schemas = schemas;
			xmlReaderSettings.ValidationEventHandler += validationEvent;
			return XmlReader.Create(reader, xmlReaderSettings);
		}

		/// <summary>Compiles a string representing an XPath expression and returns an <see cref="T:System.Xml.XPath.XPathExpression" /> object.</summary>
		/// <param name="xpath">A string representing an XPath expression.</param>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathExpression" /> object representing the XPath expression.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="xpath" /> parameter contains an XPath expression that is not valid.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06001647 RID: 5703 RVA: 0x000863E6 File Offset: 0x000845E6
		public virtual XPathExpression Compile(string xpath)
		{
			return XPathExpression.Compile(xpath);
		}

		/// <summary>Selects a single node in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified XPath query.</summary>
		/// <param name="xpath">A <see cref="T:System.String" /> representing an XPath expression.</param>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object that contains the first matching node for the XPath query specified; otherwise, <see langword="null" /> if there are no query results.</returns>
		/// <exception cref="T:System.ArgumentException">An error was encountered in the XPath query or the return type of the XPath expression is not a node.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath query is not valid.</exception>
		// Token: 0x06001648 RID: 5704 RVA: 0x000863EE File Offset: 0x000845EE
		public virtual XPathNavigator SelectSingleNode(string xpath)
		{
			return this.SelectSingleNode(XPathExpression.Compile(xpath));
		}

		/// <summary>Selects a single node in the <see cref="T:System.Xml.XPath.XPathNavigator" /> object using the specified XPath query with the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes.</summary>
		/// <param name="xpath">A <see cref="T:System.String" /> representing an XPath expression.</param>
		/// <param name="resolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes in the XPath query.</param>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object that contains the first matching node for the XPath query specified; otherwise <see langword="null" /> if there are no query results.</returns>
		/// <exception cref="T:System.ArgumentException">An error was encountered in the XPath query or the return type of the XPath expression is not a node.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath query is not valid.</exception>
		// Token: 0x06001649 RID: 5705 RVA: 0x000863FC File Offset: 0x000845FC
		public virtual XPathNavigator SelectSingleNode(string xpath, IXmlNamespaceResolver resolver)
		{
			return this.SelectSingleNode(XPathExpression.Compile(xpath, resolver));
		}

		/// <summary>Selects a single node in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <see cref="T:System.Xml.XPath.XPathExpression" /> object.</summary>
		/// <param name="expression">An <see cref="T:System.Xml.XPath.XPathExpression" /> object containing the compiled XPath query.</param>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object that contains the first matching node for the XPath query specified; otherwise <see langword="null" /> if there are no query results.</returns>
		/// <exception cref="T:System.ArgumentException">An error was encountered in the XPath query or the return type of the XPath expression is not a node.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath query is not valid.</exception>
		// Token: 0x0600164A RID: 5706 RVA: 0x0008640C File Offset: 0x0008460C
		public virtual XPathNavigator SelectSingleNode(XPathExpression expression)
		{
			XPathNodeIterator xpathNodeIterator = this.Select(expression);
			if (xpathNodeIterator.MoveNext())
			{
				return xpathNodeIterator.Current;
			}
			return null;
		}

		/// <summary>Selects a node set, using the specified XPath expression.</summary>
		/// <param name="xpath">A <see cref="T:System.String" /> representing an XPath expression.</param>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> pointing to the selected node set.</returns>
		/// <exception cref="T:System.ArgumentException">The XPath expression contains an error or its return type is not a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x0600164B RID: 5707 RVA: 0x00086431 File Offset: 0x00084631
		public virtual XPathNodeIterator Select(string xpath)
		{
			return this.Select(XPathExpression.Compile(xpath));
		}

		/// <summary>Selects a node set using the specified XPath expression with the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes.</summary>
		/// <param name="xpath">A <see cref="T:System.String" /> representing an XPath expression.</param>
		/// <param name="resolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes.</param>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that points to the selected node set.</returns>
		/// <exception cref="T:System.ArgumentException">The XPath expression contains an error or its return type is not a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x0600164C RID: 5708 RVA: 0x0008643F File Offset: 0x0008463F
		public virtual XPathNodeIterator Select(string xpath, IXmlNamespaceResolver resolver)
		{
			return this.Select(XPathExpression.Compile(xpath, resolver));
		}

		/// <summary>Selects a node set using the specified <see cref="T:System.Xml.XPath.XPathExpression" />.</summary>
		/// <param name="expr">An <see cref="T:System.Xml.XPath.XPathExpression" /> object containing the compiled XPath query.</param>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that points to the selected node set.</returns>
		/// <exception cref="T:System.ArgumentException">The XPath expression contains an error or its return type is not a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x0600164D RID: 5709 RVA: 0x0008644E File Offset: 0x0008464E
		public virtual XPathNodeIterator Select(XPathExpression expr)
		{
			XPathNodeIterator xpathNodeIterator = this.Evaluate(expr) as XPathNodeIterator;
			if (xpathNodeIterator == null)
			{
				throw XPathException.Create("Expression must evaluate to a node-set.");
			}
			return xpathNodeIterator;
		}

		/// <summary>Evaluates the specified XPath expression and returns the typed result.</summary>
		/// <param name="xpath">A string representing an XPath expression that can be evaluated.</param>
		/// <returns>The result of the expression (Boolean, number, string, or node set). This maps to <see cref="T:System.Boolean" />, <see cref="T:System.Double" />, <see cref="T:System.String" />, or <see cref="T:System.Xml.XPath.XPathNodeIterator" /> objects respectively.</returns>
		/// <exception cref="T:System.ArgumentException">The return type of the XPath expression is a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x0600164E RID: 5710 RVA: 0x0008646A File Offset: 0x0008466A
		public virtual object Evaluate(string xpath)
		{
			return this.Evaluate(XPathExpression.Compile(xpath), null);
		}

		/// <summary>Evaluates the specified XPath expression and returns the typed result, using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes in the XPath expression.</summary>
		/// <param name="xpath">A string representing an XPath expression that can be evaluated.</param>
		/// <param name="resolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes in the XPath expression.</param>
		/// <returns>The result of the expression (Boolean, number, string, or node set). This maps to <see cref="T:System.Boolean" />, <see cref="T:System.Double" />, <see cref="T:System.String" />, or <see cref="T:System.Xml.XPath.XPathNodeIterator" /> objects respectively.</returns>
		/// <exception cref="T:System.ArgumentException">The return type of the XPath expression is a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x0600164F RID: 5711 RVA: 0x00086479 File Offset: 0x00084679
		public virtual object Evaluate(string xpath, IXmlNamespaceResolver resolver)
		{
			return this.Evaluate(XPathExpression.Compile(xpath, resolver));
		}

		/// <summary>Evaluates the <see cref="T:System.Xml.XPath.XPathExpression" /> and returns the typed result.</summary>
		/// <param name="expr">An <see cref="T:System.Xml.XPath.XPathExpression" /> that can be evaluated.</param>
		/// <returns>The result of the expression (Boolean, number, string, or node set). This maps to <see cref="T:System.Boolean" />, <see cref="T:System.Double" />, <see cref="T:System.String" />, or <see cref="T:System.Xml.XPath.XPathNodeIterator" /> objects respectively.</returns>
		/// <exception cref="T:System.ArgumentException">The return type of the XPath expression is a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06001650 RID: 5712 RVA: 0x00086488 File Offset: 0x00084688
		public virtual object Evaluate(XPathExpression expr)
		{
			return this.Evaluate(expr, null);
		}

		/// <summary>Uses the supplied context to evaluate the <see cref="T:System.Xml.XPath.XPathExpression" />, and returns the typed result.</summary>
		/// <param name="expr">An <see cref="T:System.Xml.XPath.XPathExpression" /> that can be evaluated.</param>
		/// <param name="context">An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that points to the selected node set that the evaluation is to be performed on.</param>
		/// <returns>The result of the expression (Boolean, number, string, or node set). This maps to <see cref="T:System.Boolean" />, <see cref="T:System.Double" />, <see cref="T:System.String" />, or <see cref="T:System.Xml.XPath.XPathNodeIterator" /> objects respectively.</returns>
		/// <exception cref="T:System.ArgumentException">The return type of the XPath expression is a node set.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06001651 RID: 5713 RVA: 0x00086494 File Offset: 0x00084694
		public virtual object Evaluate(XPathExpression expr, XPathNodeIterator context)
		{
			CompiledXpathExpr compiledXpathExpr = expr as CompiledXpathExpr;
			if (compiledXpathExpr == null)
			{
				throw XPathException.Create("This is an invalid object. Only objects returned from Compile() can be passed as input.");
			}
			Query query = Query.Clone(compiledXpathExpr.QueryTree);
			query.Reset();
			if (context == null)
			{
				context = new XPathSingletonIterator(this.Clone(), true);
			}
			object obj = query.Evaluate(context);
			if (obj is XPathNodeIterator)
			{
				return new XPathSelectionIterator(context.Current, query);
			}
			return obj;
		}

		/// <summary>Determines whether the current node matches the specified <see cref="T:System.Xml.XPath.XPathExpression" />.</summary>
		/// <param name="expr">An <see cref="T:System.Xml.XPath.XPathExpression" /> object containing the compiled XPath expression.</param>
		/// <returns>
		///     <see langword="true" /> if the current node matches the <see cref="T:System.Xml.XPath.XPathExpression" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The XPath expression cannot be evaluated.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06001652 RID: 5714 RVA: 0x000864F8 File Offset: 0x000846F8
		public virtual bool Matches(XPathExpression expr)
		{
			CompiledXpathExpr compiledXpathExpr = expr as CompiledXpathExpr;
			if (compiledXpathExpr == null)
			{
				throw XPathException.Create("This is an invalid object. Only objects returned from Compile() can be passed as input.");
			}
			Query query = Query.Clone(compiledXpathExpr.QueryTree);
			bool result;
			try
			{
				result = (query.MatchNode(this) != null);
			}
			catch (XPathException)
			{
				throw XPathException.Create("'{0}' is an invalid XSLT pattern.", compiledXpathExpr.Expression);
			}
			return result;
		}

		/// <summary>Determines whether the current node matches the specified XPath expression.</summary>
		/// <param name="xpath">The XPath expression.</param>
		/// <returns>
		///     <see langword="true" /> if the current node matches the specified XPath expression; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The XPath expression cannot be evaluated.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06001653 RID: 5715 RVA: 0x00086558 File Offset: 0x00084758
		public virtual bool Matches(string xpath)
		{
			return this.Matches(XPathNavigator.CompileMatchPattern(xpath));
		}

		/// <summary>Selects all the child nodes of the current node that have the matching <see cref="T:System.Xml.XPath.XPathNodeType" />.</summary>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the child nodes.</param>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes.</returns>
		// Token: 0x06001654 RID: 5716 RVA: 0x00086566 File Offset: 0x00084766
		public virtual XPathNodeIterator SelectChildren(XPathNodeType type)
		{
			return new XPathChildIterator(this.Clone(), type);
		}

		/// <summary>Selects all the child nodes of the current node that have the local name and namespace URI specified.</summary>
		/// <param name="name">The local name of the child nodes. </param>
		/// <param name="namespaceURI">The namespace URI of the child nodes. </param>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <see langword="null" /> cannot be passed as a parameter.</exception>
		// Token: 0x06001655 RID: 5717 RVA: 0x00086574 File Offset: 0x00084774
		public virtual XPathNodeIterator SelectChildren(string name, string namespaceURI)
		{
			return new XPathChildIterator(this.Clone(), name, namespaceURI);
		}

		/// <summary>Selects all the ancestor nodes of the current node that have a matching <see cref="T:System.Xml.XPath.XPathNodeType" />.</summary>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the ancestor nodes.</param>
		/// <param name="matchSelf">To include the context node in the selection, <see langword="true" />; otherwise, <see langword="false" />.</param>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes. The returned nodes are in reverse document order.</returns>
		// Token: 0x06001656 RID: 5718 RVA: 0x00086583 File Offset: 0x00084783
		public virtual XPathNodeIterator SelectAncestors(XPathNodeType type, bool matchSelf)
		{
			return new XPathAncestorIterator(this.Clone(), type, matchSelf);
		}

		/// <summary>Selects all the ancestor nodes of the current node that have the specified local name and namespace URI.</summary>
		/// <param name="name">The local name of the ancestor nodes.</param>
		/// <param name="namespaceURI">The namespace URI of the ancestor nodes.</param>
		/// <param name="matchSelf">To include the context node in the selection, <see langword="true" />; otherwise, <see langword="false" />. </param>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes. The returned nodes are in reverse document order.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <see langword="null" /> cannot be passed as a parameter.</exception>
		// Token: 0x06001657 RID: 5719 RVA: 0x00086592 File Offset: 0x00084792
		public virtual XPathNodeIterator SelectAncestors(string name, string namespaceURI, bool matchSelf)
		{
			return new XPathAncestorIterator(this.Clone(), name, namespaceURI, matchSelf);
		}

		/// <summary>Selects all the descendant nodes of the current node that have a matching <see cref="T:System.Xml.XPath.XPathNodeType" />.</summary>
		/// <param name="type">The <see cref="T:System.Xml.XPath.XPathNodeType" /> of the descendant nodes.</param>
		/// <param name="matchSelf">
		///       <see langword="true" /> to include the context node in the selection; otherwise, <see langword="false" />.</param>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes.</returns>
		// Token: 0x06001658 RID: 5720 RVA: 0x000865A2 File Offset: 0x000847A2
		public virtual XPathNodeIterator SelectDescendants(XPathNodeType type, bool matchSelf)
		{
			return new XPathDescendantIterator(this.Clone(), type, matchSelf);
		}

		/// <summary>Selects all the descendant nodes of the current node with the local name and namespace URI specified.</summary>
		/// <param name="name">The local name of the descendant nodes. </param>
		/// <param name="namespaceURI">The namespace URI of the descendant nodes. </param>
		/// <param name="matchSelf">
		///       <see langword="true" /> to include the context node in the selection; otherwise, <see langword="false" />.</param>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNodeIterator" /> that contains the selected nodes.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <see langword="null" /> cannot be passed as a parameter.</exception>
		// Token: 0x06001659 RID: 5721 RVA: 0x000865B1 File Offset: 0x000847B1
		public virtual XPathNodeIterator SelectDescendants(string name, string namespaceURI, bool matchSelf)
		{
			return new XPathDescendantIterator(this.Clone(), name, namespaceURI, matchSelf);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XPath.XPathNavigator" /> can edit the underlying XML data.</summary>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> can edit the underlying XML data; otherwise <see langword="false" />.</returns>
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public virtual bool CanEdit
		{
			get
			{
				return false;
			}
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlWriter" /> object used to create a new child node at the beginning of the list of child nodes of the current node.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object used to create a new child node at the beginning of the list of child nodes of the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on does not allow a new child node to be prepended.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x0600165B RID: 5723 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public virtual XmlWriter PrependChild()
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlWriter" /> object used to create one or more new child nodes at the end of the list of child nodes of the current node. </summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object used to create new child nodes at the end of the list of child nodes of the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on is not the root node or an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x0600165C RID: 5724 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public virtual XmlWriter AppendChild()
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlWriter" /> object used to create a new sibling node after the currently selected node.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object used to create a new sibling node after the currently selected node.</returns>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted after the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x0600165D RID: 5725 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public virtual XmlWriter InsertAfter()
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlWriter" /> object used to create a new sibling node before the currently selected node.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object used to create a new sibling node before the currently selected node.</returns>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted before the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x0600165E RID: 5726 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public virtual XmlWriter InsertBefore()
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlWriter" /> object used to create new attributes on the current element.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object used to create new attributes on the current element.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x0600165F RID: 5727 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public virtual XmlWriter CreateAttributes()
		{
			throw new NotSupportedException();
		}

		/// <summary>Replaces a range of sibling nodes from the current node to the node specified.</summary>
		/// <param name="lastSiblingToReplace">An <see cref="T:System.Xml.XPath.XPathNavigator" /> positioned on the last sibling node in the range to replace.</param>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> object used to specify the replacement range.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> specified is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.InvalidOperationException">The last node to replace specified is not a valid sibling node of the current node.</exception>
		// Token: 0x06001660 RID: 5728 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public virtual XmlWriter ReplaceRange(XPathNavigator lastSiblingToReplace)
		{
			throw new NotSupportedException();
		}

		/// <summary>Replaces the current node with the content of the string specified.</summary>
		/// <param name="newNode">The XML data string for the new node.</param>
		/// <exception cref="T:System.ArgumentNullException">The XML string parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element, text, processing instruction, or comment node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML string parameter is not well-formed.</exception>
		// Token: 0x06001661 RID: 5729 RVA: 0x000865C4 File Offset: 0x000847C4
		public virtual void ReplaceSelf(string newNode)
		{
			XmlReader newNode2 = this.CreateContextReader(newNode, false);
			this.ReplaceSelf(newNode2);
		}

		/// <summary>Replaces the current node with the contents of the <see cref="T:System.Xml.XmlReader" /> object specified.</summary>
		/// <param name="newNode">An <see cref="T:System.Xml.XmlReader" /> object positioned on the XML data for the new node.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XmlReader" /> object is in an error state or closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element, text, processing instruction, or comment node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML contents of the <see cref="T:System.Xml.XmlReader" /> object parameter is not well-formed.</exception>
		// Token: 0x06001662 RID: 5730 RVA: 0x000865E4 File Offset: 0x000847E4
		public virtual void ReplaceSelf(XmlReader newNode)
		{
			if (newNode == null)
			{
				throw new ArgumentNullException("newNode");
			}
			XPathNodeType nodeType = this.NodeType;
			if (nodeType == XPathNodeType.Root || nodeType == XPathNodeType.Attribute || nodeType == XPathNodeType.Namespace)
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
			}
			XmlWriter xmlWriter = this.ReplaceRange(this);
			this.BuildSubtree(newNode, xmlWriter);
			xmlWriter.Close();
		}

		/// <summary>Replaces the current node with the contents of the <see cref="T:System.Xml.XPath.XPathNavigator" /> object specified.</summary>
		/// <param name="newNode">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the new node.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element, text, processing instruction, or comment node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML contents of the <see cref="T:System.Xml.XPath.XPathNavigator" /> object parameter is not well-formed.</exception>
		// Token: 0x06001663 RID: 5731 RVA: 0x00086638 File Offset: 0x00084838
		public virtual void ReplaceSelf(XPathNavigator newNode)
		{
			if (newNode == null)
			{
				throw new ArgumentNullException("newNode");
			}
			XmlReader newNode2 = newNode.CreateReader();
			this.ReplaceSelf(newNode2);
		}

		/// <summary>Gets or sets the markup representing the opening and closing tags of the current node and its child nodes.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the markup representing the opening and closing tags of the current node and its child nodes.</returns>
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x00086664 File Offset: 0x00084864
		// (set) Token: 0x06001665 RID: 5733 RVA: 0x00086750 File Offset: 0x00084950
		public virtual string OuterXml
		{
			get
			{
				if (this.NodeType == XPathNodeType.Attribute)
				{
					return this.Name + "=\"" + this.Value + "\"";
				}
				if (this.NodeType != XPathNodeType.Namespace)
				{
					StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
					XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
					{
						Indent = true,
						OmitXmlDeclaration = true,
						ConformanceLevel = ConformanceLevel.Auto
					});
					try
					{
						xmlWriter.WriteNode(this, true);
					}
					finally
					{
						xmlWriter.Close();
					}
					return stringWriter.ToString();
				}
				if (this.LocalName.Length == 0)
				{
					return "xmlns=\"" + this.Value + "\"";
				}
				return string.Concat(new string[]
				{
					"xmlns:",
					this.LocalName,
					"=\"",
					this.Value,
					"\""
				});
			}
			set
			{
				this.ReplaceSelf(value);
			}
		}

		/// <summary>Gets or sets the markup representing the child nodes of the current node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the markup of the child nodes of the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XPath.XPathNavigator.InnerXml" /> property cannot be set.</exception>
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x0008675C File Offset: 0x0008495C
		// (set) Token: 0x06001667 RID: 5735 RVA: 0x000867F8 File Offset: 0x000849F8
		public virtual string InnerXml
		{
			get
			{
				XPathNodeType nodeType = this.NodeType;
				if (nodeType <= XPathNodeType.Element)
				{
					StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
					XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
					{
						Indent = true,
						OmitXmlDeclaration = true,
						ConformanceLevel = ConformanceLevel.Auto
					});
					try
					{
						if (this.MoveToFirstChild())
						{
							do
							{
								xmlWriter.WriteNode(this, true);
							}
							while (this.MoveToNext());
							this.MoveToParent();
						}
					}
					finally
					{
						xmlWriter.Close();
					}
					return stringWriter.ToString();
				}
				if (nodeType - XPathNodeType.Attribute > 1)
				{
					return string.Empty;
				}
				return this.Value;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				XPathNodeType nodeType = this.NodeType;
				if (nodeType > XPathNodeType.Element)
				{
					if (nodeType != XPathNodeType.Attribute)
					{
						throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
					}
					this.SetValue(value);
					return;
				}
				else
				{
					XPathNavigator xpathNavigator = this.CreateNavigator();
					while (xpathNavigator.MoveToFirstChild())
					{
						xpathNavigator.DeleteSelf();
					}
					if (value.Length != 0)
					{
						xpathNavigator.AppendChild(value);
						return;
					}
					return;
				}
			}
		}

		/// <summary>Creates a new child node at the end of the list of child nodes of the current node using the XML data string specified.</summary>
		/// <param name="newChild">The XML data string for the new child node.</param>
		/// <exception cref="T:System.ArgumentNullException">The XML data string parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on is not the root node or an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML data string parameter is not well-formed.</exception>
		// Token: 0x06001668 RID: 5736 RVA: 0x00086864 File Offset: 0x00084A64
		public virtual void AppendChild(string newChild)
		{
			XmlReader newChild2 = this.CreateContextReader(newChild, true);
			this.AppendChild(newChild2);
		}

		/// <summary>Creates a new child node at the end of the list of child nodes of the current node using the XML contents of the <see cref="T:System.Xml.XmlReader" /> object specified.</summary>
		/// <param name="newChild">An <see cref="T:System.Xml.XmlReader" /> object positioned on the XML data for the new child node.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XmlReader" /> object is in an error state or closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on is not the root node or an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML contents of the <see cref="T:System.Xml.XmlReader" /> object parameter is not well-formed.</exception>
		// Token: 0x06001669 RID: 5737 RVA: 0x00086884 File Offset: 0x00084A84
		public virtual void AppendChild(XmlReader newChild)
		{
			if (newChild == null)
			{
				throw new ArgumentNullException("newChild");
			}
			XmlWriter xmlWriter = this.AppendChild();
			this.BuildSubtree(newChild, xmlWriter);
			xmlWriter.Close();
		}

		/// <summary>Creates a new child node at the end of the list of child nodes of the current node using the nodes in the <see cref="T:System.Xml.XPath.XPathNavigator" /> specified.</summary>
		/// <param name="newChild">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the node to add as the new child node.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on is not the root node or an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x0600166A RID: 5738 RVA: 0x000868B4 File Offset: 0x00084AB4
		public virtual void AppendChild(XPathNavigator newChild)
		{
			if (newChild == null)
			{
				throw new ArgumentNullException("newChild");
			}
			if (!this.IsValidChildType(newChild.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
			}
			XmlReader newChild2 = newChild.CreateReader();
			this.AppendChild(newChild2);
		}

		/// <summary>Creates a new child node at the beginning of the list of child nodes of the current node using the XML string specified.</summary>
		/// <param name="newChild">The XML data string for the new child node.</param>
		/// <exception cref="T:System.ArgumentNullException">The XML string parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on does not allow a new child node to be prepended.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML string parameter is not well-formed.</exception>
		// Token: 0x0600166B RID: 5739 RVA: 0x000868FC File Offset: 0x00084AFC
		public virtual void PrependChild(string newChild)
		{
			XmlReader newChild2 = this.CreateContextReader(newChild, true);
			this.PrependChild(newChild2);
		}

		/// <summary>Creates a new child node at the beginning of the list of child nodes of the current node using the XML contents of the <see cref="T:System.Xml.XmlReader" /> object specified.</summary>
		/// <param name="newChild">An <see cref="T:System.Xml.XmlReader" /> object positioned on the XML data for the new child node.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XmlReader" /> object is in an error state or closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on does not allow a new child node to be prepended.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML contents of the <see cref="T:System.Xml.XmlReader" /> object parameter is not well-formed.</exception>
		// Token: 0x0600166C RID: 5740 RVA: 0x0008691C File Offset: 0x00084B1C
		public virtual void PrependChild(XmlReader newChild)
		{
			if (newChild == null)
			{
				throw new ArgumentNullException("newChild");
			}
			XmlWriter xmlWriter = this.PrependChild();
			this.BuildSubtree(newChild, xmlWriter);
			xmlWriter.Close();
		}

		/// <summary>Creates a new child node at the beginning of the list of child nodes of the current node using the nodes in the <see cref="T:System.Xml.XPath.XPathNavigator" /> object specified.</summary>
		/// <param name="newChild">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the node to add as the new child node.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on does not allow a new child node to be prepended.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x0600166D RID: 5741 RVA: 0x0008694C File Offset: 0x00084B4C
		public virtual void PrependChild(XPathNavigator newChild)
		{
			if (newChild == null)
			{
				throw new ArgumentNullException("newChild");
			}
			if (!this.IsValidChildType(newChild.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
			}
			XmlReader newChild2 = newChild.CreateReader();
			this.PrependChild(newChild2);
		}

		/// <summary>Creates a new sibling node before the currently selected node using the XML string specified.</summary>
		/// <param name="newSibling">The XML data string for the new sibling node.</param>
		/// <exception cref="T:System.ArgumentNullException">The XML string parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted before the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML string parameter is not well-formed.</exception>
		// Token: 0x0600166E RID: 5742 RVA: 0x00086994 File Offset: 0x00084B94
		public virtual void InsertBefore(string newSibling)
		{
			XmlReader newSibling2 = this.CreateContextReader(newSibling, false);
			this.InsertBefore(newSibling2);
		}

		/// <summary>Creates a new sibling node before the currently selected node using the XML contents of the <see cref="T:System.Xml.XmlReader" /> object specified.</summary>
		/// <param name="newSibling">An <see cref="T:System.Xml.XmlReader" /> object positioned on the XML data for the new sibling node.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XmlReader" /> object is in an error state or closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted before the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML contents of the <see cref="T:System.Xml.XmlReader" /> object parameter is not well-formed.</exception>
		// Token: 0x0600166F RID: 5743 RVA: 0x000869B4 File Offset: 0x00084BB4
		public virtual void InsertBefore(XmlReader newSibling)
		{
			if (newSibling == null)
			{
				throw new ArgumentNullException("newSibling");
			}
			XmlWriter xmlWriter = this.InsertBefore();
			this.BuildSubtree(newSibling, xmlWriter);
			xmlWriter.Close();
		}

		/// <summary>Creates a new sibling node before the currently selected node using the nodes in the <see cref="T:System.Xml.XPath.XPathNavigator" /> specified.</summary>
		/// <param name="newSibling">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the node to add as the new sibling node.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted before the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06001670 RID: 5744 RVA: 0x000869E4 File Offset: 0x00084BE4
		public virtual void InsertBefore(XPathNavigator newSibling)
		{
			if (newSibling == null)
			{
				throw new ArgumentNullException("newSibling");
			}
			if (!this.IsValidSiblingType(newSibling.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
			}
			XmlReader newSibling2 = newSibling.CreateReader();
			this.InsertBefore(newSibling2);
		}

		/// <summary>Creates a new sibling node after the currently selected node using the XML string specified.</summary>
		/// <param name="newSibling">The XML data string for the new sibling node.</param>
		/// <exception cref="T:System.ArgumentNullException">The XML string parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted after the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML string parameter is not well-formed.</exception>
		// Token: 0x06001671 RID: 5745 RVA: 0x00086A2C File Offset: 0x00084C2C
		public virtual void InsertAfter(string newSibling)
		{
			XmlReader newSibling2 = this.CreateContextReader(newSibling, false);
			this.InsertAfter(newSibling2);
		}

		/// <summary>Creates a new sibling node after the currently selected node using the XML contents of the <see cref="T:System.Xml.XmlReader" /> object specified.</summary>
		/// <param name="newSibling">An <see cref="T:System.Xml.XmlReader" /> object positioned on the XML data for the new sibling node.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XmlReader" /> object is in an error state or closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted after the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML contents of the <see cref="T:System.Xml.XmlReader" /> object parameter is not well-formed.</exception>
		// Token: 0x06001672 RID: 5746 RVA: 0x00086A4C File Offset: 0x00084C4C
		public virtual void InsertAfter(XmlReader newSibling)
		{
			if (newSibling == null)
			{
				throw new ArgumentNullException("newSibling");
			}
			XmlWriter xmlWriter = this.InsertAfter();
			this.BuildSubtree(newSibling, xmlWriter);
			xmlWriter.Close();
		}

		/// <summary>Creates a new sibling node after the currently selected node using the nodes in the <see cref="T:System.Xml.XPath.XPathNavigator" /> object specified.</summary>
		/// <param name="newSibling">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the node to add as the new sibling node.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> object parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted after the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06001673 RID: 5747 RVA: 0x00086A7C File Offset: 0x00084C7C
		public virtual void InsertAfter(XPathNavigator newSibling)
		{
			if (newSibling == null)
			{
				throw new ArgumentNullException("newSibling");
			}
			if (!this.IsValidSiblingType(newSibling.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
			}
			XmlReader newSibling2 = newSibling.CreateReader();
			this.InsertAfter(newSibling2);
		}

		/// <summary>Deletes a range of sibling nodes from the current node to the node specified.</summary>
		/// <param name="lastSiblingToDelete">An <see cref="T:System.Xml.XPath.XPathNavigator" /> positioned on the last sibling node in the range to delete.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> specified is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		/// <exception cref="T:System.InvalidOperationException">The last node to delete specified is not a valid sibling node of the current node.</exception>
		// Token: 0x06001674 RID: 5748 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public virtual void DeleteRange(XPathNavigator lastSiblingToDelete)
		{
			throw new NotSupportedException();
		}

		/// <summary>Deletes the current node and its child nodes.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on a node that cannot be deleted such as the root node or a namespace node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06001675 RID: 5749 RVA: 0x00086AC3 File Offset: 0x00084CC3
		public virtual void DeleteSelf()
		{
			this.DeleteRange(this);
		}

		/// <summary>Creates a new child element at the beginning of the list of child nodes of the current node using the namespace prefix, local name, and namespace URI specified with the value specified.</summary>
		/// <param name="prefix">The namespace prefix of the new child element (if any).</param>
		/// <param name="localName">The local name of the new child element (if any).</param>
		/// <param name="namespaceURI">The namespace URI of the new child element (if any). <see cref="F:System.String.Empty" /> and <see langword="null" /> are equivalent.</param>
		/// <param name="value">The value of the new child element. If <see cref="F:System.String.Empty" /> or <see langword="null" /> are passed, an empty element is created.</param>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on does not allow a new child node to be prepended.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06001676 RID: 5750 RVA: 0x00086ACC File Offset: 0x00084CCC
		public virtual void PrependChildElement(string prefix, string localName, string namespaceURI, string value)
		{
			XmlWriter xmlWriter = this.PrependChild();
			xmlWriter.WriteStartElement(prefix, localName, namespaceURI);
			if (value != null)
			{
				xmlWriter.WriteString(value);
			}
			xmlWriter.WriteEndElement();
			xmlWriter.Close();
		}

		/// <summary>Creates a new child element node at the end of the list of child nodes of the current node using the namespace prefix, local name and namespace URI specified with the value specified.</summary>
		/// <param name="prefix">The namespace prefix of the new child element node (if any).</param>
		/// <param name="localName">The local name of the new child element node (if any).</param>
		/// <param name="namespaceURI">The namespace URI of the new child element node (if any). <see cref="F:System.String.Empty" /> and <see langword="null" /> are equivalent.</param>
		/// <param name="value">The value of the new child element node. If <see cref="F:System.String.Empty" /> or <see langword="null" /> are passed, an empty element is created.</param>
		/// <exception cref="T:System.InvalidOperationException">The current node the <see cref="T:System.Xml.XPath.XPathNavigator" /> is positioned on is not the root node or an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06001677 RID: 5751 RVA: 0x00086B04 File Offset: 0x00084D04
		public virtual void AppendChildElement(string prefix, string localName, string namespaceURI, string value)
		{
			XmlWriter xmlWriter = this.AppendChild();
			xmlWriter.WriteStartElement(prefix, localName, namespaceURI);
			if (value != null)
			{
				xmlWriter.WriteString(value);
			}
			xmlWriter.WriteEndElement();
			xmlWriter.Close();
		}

		/// <summary>Creates a new sibling element before the current node using the namespace prefix, local name, and namespace URI specified, with the value specified.</summary>
		/// <param name="prefix">The namespace prefix of the new child element (if any).</param>
		/// <param name="localName">The local name of the new child element (if any).</param>
		/// <param name="namespaceURI">The namespace URI of the new child element (if any). <see cref="F:System.String.Empty" /> and <see langword="null" /> are equivalent.</param>
		/// <param name="value">The value of the new child element. If <see cref="F:System.String.Empty" /> or <see langword="null" /> are passed, an empty element is created.</param>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted before the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06001678 RID: 5752 RVA: 0x00086B3C File Offset: 0x00084D3C
		public virtual void InsertElementBefore(string prefix, string localName, string namespaceURI, string value)
		{
			XmlWriter xmlWriter = this.InsertBefore();
			xmlWriter.WriteStartElement(prefix, localName, namespaceURI);
			if (value != null)
			{
				xmlWriter.WriteString(value);
			}
			xmlWriter.WriteEndElement();
			xmlWriter.Close();
		}

		/// <summary>Creates a new sibling element after the current node using the namespace prefix, local name and namespace URI specified, with the value specified.</summary>
		/// <param name="prefix">The namespace prefix of the new child element (if any).</param>
		/// <param name="localName">The local name of the new child element (if any).</param>
		/// <param name="namespaceURI">The namespace URI of the new child element (if any). <see cref="F:System.String.Empty" /> and <see langword="null" /> are equivalent.</param>
		/// <param name="value">The value of the new child element. If <see cref="F:System.String.Empty" /> or <see langword="null" /> are passed, an empty element is created.</param>
		/// <exception cref="T:System.InvalidOperationException">The position of the <see cref="T:System.Xml.XPath.XPathNavigator" /> does not allow a new sibling node to be inserted after the current node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x06001679 RID: 5753 RVA: 0x00086B74 File Offset: 0x00084D74
		public virtual void InsertElementAfter(string prefix, string localName, string namespaceURI, string value)
		{
			XmlWriter xmlWriter = this.InsertAfter();
			xmlWriter.WriteStartElement(prefix, localName, namespaceURI);
			if (value != null)
			{
				xmlWriter.WriteString(value);
			}
			xmlWriter.WriteEndElement();
			xmlWriter.Close();
		}

		/// <summary>Creates an attribute node on the current element node using the namespace prefix, local name and namespace URI specified with the value specified.</summary>
		/// <param name="prefix">The namespace prefix of the new attribute node (if any).</param>
		/// <param name="localName">The local name of the new attribute node which cannot <see cref="F:System.String.Empty" /> or <see langword="null" />.</param>
		/// <param name="namespaceURI">The namespace URI for the new attribute node (if any).</param>
		/// <param name="value">The value of the new attribute node. If <see cref="F:System.String.Empty" /> or <see langword="null" /> are passed, an empty attribute node is created.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> is not positioned on an element node.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XPath.XPathNavigator" /> does not support editing.</exception>
		// Token: 0x0600167A RID: 5754 RVA: 0x00086BAC File Offset: 0x00084DAC
		public virtual void CreateAttribute(string prefix, string localName, string namespaceURI, string value)
		{
			XmlWriter xmlWriter = this.CreateAttributes();
			xmlWriter.WriteStartAttribute(prefix, localName, namespaceURI);
			if (value != null)
			{
				xmlWriter.WriteString(value);
			}
			xmlWriter.WriteEndAttribute();
			xmlWriter.Close();
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x00086BE4 File Offset: 0x00084DE4
		internal bool MoveToPrevious(string localName, string namespaceURI)
		{
			XPathNavigator other = this.Clone();
			localName = ((localName != null) ? this.NameTable.Get(localName) : null);
			while (this.MoveToPrevious())
			{
				if (this.NodeType == XPathNodeType.Element && localName == this.LocalName && namespaceURI == this.NamespaceURI)
				{
					return true;
				}
			}
			this.MoveTo(other);
			return false;
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00086C44 File Offset: 0x00084E44
		internal bool MoveToPrevious(XPathNodeType type)
		{
			XPathNavigator other = this.Clone();
			int contentKindMask = XPathNavigator.GetContentKindMask(type);
			while (this.MoveToPrevious())
			{
				if ((1 << (int)this.NodeType & contentKindMask) != 0)
				{
					return true;
				}
			}
			this.MoveTo(other);
			return false;
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00086C84 File Offset: 0x00084E84
		internal bool MoveToNonDescendant()
		{
			if (this.NodeType == XPathNodeType.Root)
			{
				return false;
			}
			if (this.MoveToNext())
			{
				return true;
			}
			XPathNavigator xpathNavigator = this.Clone();
			if (!this.MoveToParent())
			{
				return false;
			}
			XPathNodeType nodeType = xpathNavigator.NodeType;
			if (nodeType - XPathNodeType.Attribute <= 1 && this.MoveToFirstChild())
			{
				return true;
			}
			while (!this.MoveToNext())
			{
				if (!this.MoveToParent())
				{
					this.MoveTo(xpathNavigator);
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x00086CE8 File Offset: 0x00084EE8
		internal uint IndexInParent
		{
			get
			{
				XPathNavigator xpathNavigator = this.Clone();
				uint num = 0U;
				XPathNodeType nodeType = this.NodeType;
				if (nodeType != XPathNodeType.Attribute)
				{
					if (nodeType != XPathNodeType.Namespace)
					{
						while (xpathNavigator.MoveToNext())
						{
							num += 1U;
						}
					}
					else
					{
						while (xpathNavigator.MoveToNextNamespace())
						{
							num += 1U;
						}
					}
				}
				else
				{
					while (xpathNavigator.MoveToNextAttribute())
					{
						num += 1U;
					}
				}
				return num;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x00086D38 File Offset: 0x00084F38
		internal virtual string UniqueId
		{
			get
			{
				XPathNavigator xpathNavigator = this.Clone();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(XPathNavigator.NodeTypeLetter[(int)this.NodeType]);
				for (;;)
				{
					uint num = xpathNavigator.IndexInParent;
					if (!xpathNavigator.MoveToParent())
					{
						break;
					}
					if (num <= 31U)
					{
						stringBuilder.Append(XPathNavigator.UniqueIdTbl[(int)num]);
					}
					else
					{
						stringBuilder.Append('0');
						do
						{
							stringBuilder.Append(XPathNavigator.UniqueIdTbl[(int)(num & 31U)]);
							num >>= 5;
						}
						while (num != 0U);
						stringBuilder.Append('0');
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00086DBC File Offset: 0x00084FBC
		private static XPathExpression CompileMatchPattern(string xpath)
		{
			bool needContext;
			return new CompiledXpathExpr(new QueryBuilder().BuildPatternQuery(xpath, out needContext), xpath, needContext);
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00086DE0 File Offset: 0x00084FE0
		private static int GetDepth(XPathNavigator nav)
		{
			int num = 0;
			while (nav.MoveToParent())
			{
				num++;
			}
			return num;
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00086E00 File Offset: 0x00085000
		private XmlNodeOrder CompareSiblings(XPathNavigator n1, XPathNavigator n2)
		{
			int num = 0;
			XPathNodeType nodeType = n1.NodeType;
			if (nodeType != XPathNodeType.Attribute)
			{
				if (nodeType != XPathNodeType.Namespace)
				{
					num += 2;
				}
			}
			else
			{
				num++;
			}
			nodeType = n2.NodeType;
			if (nodeType != XPathNodeType.Attribute)
			{
				if (nodeType == XPathNodeType.Namespace)
				{
					if (num == 0)
					{
						while (n1.MoveToNextNamespace())
						{
							if (n1.IsSamePosition(n2))
							{
								return XmlNodeOrder.Before;
							}
						}
					}
				}
				else
				{
					num -= 2;
					if (num == 0)
					{
						while (n1.MoveToNext())
						{
							if (n1.IsSamePosition(n2))
							{
								return XmlNodeOrder.Before;
							}
						}
					}
				}
			}
			else
			{
				num--;
				if (num == 0)
				{
					while (n1.MoveToNextAttribute())
					{
						if (n1.IsSamePosition(n2))
						{
							return XmlNodeOrder.Before;
						}
					}
				}
			}
			if (num >= 0)
			{
				return XmlNodeOrder.After;
			}
			return XmlNodeOrder.Before;
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00086E94 File Offset: 0x00085094
		internal static XmlNamespaceManager GetNamespaces(IXmlNamespaceResolver resolver)
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(new NameTable());
			foreach (KeyValuePair<string, string> keyValuePair in resolver.GetNamespacesInScope(XmlNamespaceScope.All))
			{
				if (keyValuePair.Key != "xmlns")
				{
					xmlNamespaceManager.AddNamespace(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return xmlNamespaceManager;
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x00086F10 File Offset: 0x00085110
		internal static int GetContentKindMask(XPathNodeType type)
		{
			return XPathNavigator.ContentKindMasks[(int)type];
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00086F19 File Offset: 0x00085119
		internal static int GetKindMask(XPathNodeType type)
		{
			if (type == XPathNodeType.All)
			{
				return int.MaxValue;
			}
			if (type == XPathNodeType.Text)
			{
				return 112;
			}
			return 1 << (int)type;
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00086F33 File Offset: 0x00085133
		internal static bool IsText(XPathNodeType type)
		{
			return type - XPathNodeType.Text <= 2;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00086F40 File Offset: 0x00085140
		private bool IsValidChildType(XPathNodeType type)
		{
			XPathNodeType nodeType = this.NodeType;
			if (nodeType != XPathNodeType.Root)
			{
				if (nodeType == XPathNodeType.Element)
				{
					if (type == XPathNodeType.Element || type - XPathNodeType.Text <= 4)
					{
						return true;
					}
				}
			}
			else if (type == XPathNodeType.Element || type - XPathNodeType.SignificantWhitespace <= 3)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00086F78 File Offset: 0x00085178
		private bool IsValidSiblingType(XPathNodeType type)
		{
			XPathNodeType nodeType = this.NodeType;
			return (nodeType == XPathNodeType.Element || nodeType - XPathNodeType.Text <= 4) && (type == XPathNodeType.Element || type - XPathNodeType.Text <= 4);
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00086FA3 File Offset: 0x000851A3
		private XmlReader CreateReader()
		{
			return XPathNavigatorReader.Create(this);
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00086FAC File Offset: 0x000851AC
		private XmlReader CreateContextReader(string xml, bool fromCurrentNode)
		{
			if (xml == null)
			{
				throw new ArgumentNullException("xml");
			}
			XPathNavigator xpathNavigator = this.CreateNavigator();
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.NameTable);
			if (!fromCurrentNode)
			{
				xpathNavigator.MoveToParent();
			}
			if (xpathNavigator.MoveToFirstNamespace(XPathNamespaceScope.All))
			{
				do
				{
					xmlNamespaceManager.AddNamespace(xpathNavigator.LocalName, xpathNavigator.Value);
				}
				while (xpathNavigator.MoveToNextNamespace(XPathNamespaceScope.All));
			}
			XmlParserContext context = new XmlParserContext(this.NameTable, xmlNamespaceManager, null, XmlSpace.Default);
			return new XmlTextReader(xml, XmlNodeType.Element, context)
			{
				WhitespaceHandling = WhitespaceHandling.Significant
			};
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00087028 File Offset: 0x00085228
		internal void BuildSubtree(XmlReader reader, XmlWriter writer)
		{
			string text = "http://www.w3.org/2000/xmlns/";
			ReadState readState = reader.ReadState;
			if (readState != ReadState.Initial && readState != ReadState.Interactive)
			{
				throw new ArgumentException(Res.GetString("Operation is not valid due to the current state of the object."), "reader");
			}
			int num = 0;
			if (readState == ReadState.Initial)
			{
				if (!reader.Read())
				{
					return;
				}
				num++;
			}
			do
			{
				switch (reader.NodeType)
				{
				case XmlNodeType.Element:
				{
					writer.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
					bool isEmptyElement = reader.IsEmptyElement;
					while (reader.MoveToNextAttribute())
					{
						if (reader.NamespaceURI == text)
						{
							if (reader.Prefix.Length == 0)
							{
								writer.WriteAttributeString("", "xmlns", text, reader.Value);
							}
							else
							{
								writer.WriteAttributeString("xmlns", reader.LocalName, text, reader.Value);
							}
						}
						else
						{
							writer.WriteStartAttribute(reader.Prefix, reader.LocalName, reader.NamespaceURI);
							writer.WriteString(reader.Value);
							writer.WriteEndAttribute();
						}
					}
					reader.MoveToElement();
					if (isEmptyElement)
					{
						writer.WriteEndElement();
					}
					else
					{
						num++;
					}
					break;
				}
				case XmlNodeType.Attribute:
					if (reader.NamespaceURI == text)
					{
						if (reader.Prefix.Length == 0)
						{
							writer.WriteAttributeString("", "xmlns", text, reader.Value);
						}
						else
						{
							writer.WriteAttributeString("xmlns", reader.LocalName, text, reader.Value);
						}
					}
					else
					{
						writer.WriteStartAttribute(reader.Prefix, reader.LocalName, reader.NamespaceURI);
						writer.WriteString(reader.Value);
						writer.WriteEndAttribute();
					}
					break;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					writer.WriteString(reader.Value);
					break;
				case XmlNodeType.EntityReference:
					reader.ResolveEntity();
					break;
				case XmlNodeType.ProcessingInstruction:
					writer.WriteProcessingInstruction(reader.LocalName, reader.Value);
					break;
				case XmlNodeType.Comment:
					writer.WriteComment(reader.Value);
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					writer.WriteString(reader.Value);
					break;
				case XmlNodeType.EndElement:
					writer.WriteFullEndElement();
					num--;
					break;
				}
			}
			while (reader.Read() && num > 0);
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x00087266 File Offset: 0x00085466
		private object debuggerDisplayProxy
		{
			get
			{
				return new XPathNavigator.DebuggerDisplayProxy(this);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathNavigator" /> class.</summary>
		// Token: 0x0600168D RID: 5773 RVA: 0x00087273 File Offset: 0x00085473
		protected XPathNavigator()
		{
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0008727C File Offset: 0x0008547C
		// Note: this type is marked as 'beforefieldinit'.
		static XPathNavigator()
		{
		}

		// Token: 0x04001801 RID: 6145
		internal static readonly XPathNavigatorKeyComparer comparer = new XPathNavigatorKeyComparer();

		// Token: 0x04001802 RID: 6146
		internal static readonly char[] NodeTypeLetter = new char[]
		{
			'R',
			'E',
			'A',
			'N',
			'T',
			'S',
			'W',
			'P',
			'C',
			'X'
		};

		// Token: 0x04001803 RID: 6147
		internal static readonly char[] UniqueIdTbl = new char[]
		{
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'G',
			'H',
			'I',
			'J',
			'K',
			'L',
			'M',
			'N',
			'O',
			'P',
			'Q',
			'R',
			'S',
			'T',
			'U',
			'V',
			'W',
			'X',
			'Y',
			'Z',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6'
		};

		// Token: 0x04001804 RID: 6148
		internal const int AllMask = 2147483647;

		// Token: 0x04001805 RID: 6149
		internal const int NoAttrNmspMask = 2147483635;

		// Token: 0x04001806 RID: 6150
		internal const int TextMask = 112;

		// Token: 0x04001807 RID: 6151
		internal static readonly int[] ContentKindMasks = new int[]
		{
			1,
			2,
			0,
			0,
			112,
			32,
			64,
			128,
			256,
			2147483635
		};

		// Token: 0x0200025A RID: 602
		private class CheckValidityHelper
		{
			// Token: 0x0600168F RID: 5775 RVA: 0x000872D8 File Offset: 0x000854D8
			internal CheckValidityHelper(ValidationEventHandler nextEventHandler, XPathNavigatorReader reader)
			{
				this.isValid = true;
				this.nextEventHandler = nextEventHandler;
				this.reader = reader;
			}

			// Token: 0x06001690 RID: 5776 RVA: 0x000872F8 File Offset: 0x000854F8
			internal void ValidationCallback(object sender, ValidationEventArgs args)
			{
				if (args.Severity == XmlSeverityType.Error)
				{
					this.isValid = false;
				}
				XmlSchemaValidationException ex = args.Exception as XmlSchemaValidationException;
				if (ex != null && this.reader != null)
				{
					ex.SetSourceObject(this.reader.UnderlyingObject);
				}
				if (this.nextEventHandler != null)
				{
					this.nextEventHandler(sender, args);
					return;
				}
				if (ex != null && args.Severity == XmlSeverityType.Error)
				{
					throw ex;
				}
			}

			// Token: 0x17000409 RID: 1033
			// (get) Token: 0x06001691 RID: 5777 RVA: 0x0008735F File Offset: 0x0008555F
			internal bool IsValid
			{
				get
				{
					return this.isValid;
				}
			}

			// Token: 0x04001808 RID: 6152
			private bool isValid;

			// Token: 0x04001809 RID: 6153
			private ValidationEventHandler nextEventHandler;

			// Token: 0x0400180A RID: 6154
			private XPathNavigatorReader reader;
		}

		// Token: 0x0200025B RID: 603
		[DebuggerDisplay("{ToString()}")]
		internal struct DebuggerDisplayProxy
		{
			// Token: 0x06001692 RID: 5778 RVA: 0x00087367 File Offset: 0x00085567
			public DebuggerDisplayProxy(XPathNavigator nav)
			{
				this.nav = nav;
			}

			// Token: 0x06001693 RID: 5779 RVA: 0x00087370 File Offset: 0x00085570
			public override string ToString()
			{
				string text = this.nav.NodeType.ToString();
				switch (this.nav.NodeType)
				{
				case XPathNodeType.Element:
					text = text + ", Name=\"" + this.nav.Name + "\"";
					break;
				case XPathNodeType.Attribute:
				case XPathNodeType.Namespace:
				case XPathNodeType.ProcessingInstruction:
					text = text + ", Name=\"" + this.nav.Name + "\"";
					text = text + ", Value=\"" + XmlConvert.EscapeValueForDebuggerDisplay(this.nav.Value) + "\"";
					break;
				case XPathNodeType.Text:
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
				case XPathNodeType.Comment:
					text = text + ", Value=\"" + XmlConvert.EscapeValueForDebuggerDisplay(this.nav.Value) + "\"";
					break;
				}
				return text;
			}

			// Token: 0x0400180B RID: 6155
			private XPathNavigator nav;
		}
	}
}
