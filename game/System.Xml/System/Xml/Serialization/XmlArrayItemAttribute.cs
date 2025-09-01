using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	/// <summary>Represents an attribute that specifies the derived types that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> can place in a serialized array.</summary>
	// Token: 0x020002C7 RID: 711
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true)]
	public class XmlArrayItemAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> class.</summary>
		// Token: 0x06001AFE RID: 6910 RVA: 0x000021EA File Offset: 0x000003EA
		public XmlArrayItemAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> class and specifies the name of the XML element generated in the XML document.</summary>
		/// <param name="elementName">The name of the XML element. </param>
		// Token: 0x06001AFF RID: 6911 RVA: 0x0009B621 File Offset: 0x00099821
		public XmlArrayItemAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> class and specifies the <see cref="T:System.Type" /> that can be inserted into the serialized array.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to serialize. </param>
		// Token: 0x06001B00 RID: 6912 RVA: 0x0009B630 File Offset: 0x00099830
		public XmlArrayItemAttribute(Type type)
		{
			this.type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> class and specifies the name of the XML element generated in the XML document and the <see cref="T:System.Type" /> that can be inserted into the generated XML document.</summary>
		/// <param name="elementName">The name of the XML element. </param>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to serialize. </param>
		// Token: 0x06001B01 RID: 6913 RVA: 0x0009B63F File Offset: 0x0009983F
		public XmlArrayItemAttribute(string elementName, Type type)
		{
			this.elementName = elementName;
			this.type = type;
		}

		/// <summary>Gets or sets the type allowed in an array.</summary>
		/// <returns>A <see cref="T:System.Type" /> that is allowed in the array.</returns>
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x0009B655 File Offset: 0x00099855
		// (set) Token: 0x06001B03 RID: 6915 RVA: 0x0009B65D File Offset: 0x0009985D
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		/// <summary>Gets or sets the name of the generated XML element.</summary>
		/// <returns>The name of the generated XML element. The default is the member identifier.</returns>
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x0009B666 File Offset: 0x00099866
		// (set) Token: 0x06001B05 RID: 6917 RVA: 0x0009B67C File Offset: 0x0009987C
		public string ElementName
		{
			get
			{
				if (this.elementName != null)
				{
					return this.elementName;
				}
				return string.Empty;
			}
			set
			{
				this.elementName = value;
			}
		}

		/// <summary>Gets or sets the namespace of the generated XML element.</summary>
		/// <returns>The namespace of the generated XML element.</returns>
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x0009B685 File Offset: 0x00099885
		// (set) Token: 0x06001B07 RID: 6919 RVA: 0x0009B68D File Offset: 0x0009988D
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		/// <summary>Gets or sets the level in a hierarchy of XML elements that the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> affects.</summary>
		/// <returns>The zero-based index of a set of indexes in an array of arrays.</returns>
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x0009B696 File Offset: 0x00099896
		// (set) Token: 0x06001B09 RID: 6921 RVA: 0x0009B69E File Offset: 0x0009989E
		public int NestingLevel
		{
			get
			{
				return this.nestingLevel;
			}
			set
			{
				this.nestingLevel = value;
			}
		}

		/// <summary>Gets or sets the XML data type of the generated XML element.</summary>
		/// <returns>An XML schema definition (XSD) data type, as defined by the World Wide Web Consortium (www.w3.org) document "XML Schema Part 2: DataTypes".</returns>
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x0009B6A7 File Offset: 0x000998A7
		// (set) Token: 0x06001B0B RID: 6923 RVA: 0x0009B6BD File Offset: 0x000998BD
		public string DataType
		{
			get
			{
				if (this.dataType != null)
				{
					return this.dataType;
				}
				return string.Empty;
			}
			set
			{
				this.dataType = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must serialize a member as an empty XML tag with the <see langword="xsi:nil" /> attribute set to <see langword="true" />.</summary>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates the <see langword="xsi:nil" /> attribute; otherwise, <see langword="false" />, and no instance is generated. The default is <see langword="true" />.</returns>
		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x0009B6C6 File Offset: 0x000998C6
		// (set) Token: 0x06001B0D RID: 6925 RVA: 0x0009B6CE File Offset: 0x000998CE
		public bool IsNullable
		{
			get
			{
				return this.nullable;
			}
			set
			{
				this.nullable = value;
				this.nullableSpecified = true;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x0009B6DE File Offset: 0x000998DE
		internal bool IsNullableSpecified
		{
			get
			{
				return this.nullableSpecified;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the name of the generated XML element is qualified.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaForm" /> values. The default is <see langword="XmlSchemaForm.None" />.</returns>
		/// <exception cref="T:System.Exception">The <see cref="P:System.Xml.Serialization.XmlArrayItemAttribute.Form" /> property is set to <see langword="XmlSchemaForm.Unqualified" /> and a <see cref="P:System.Xml.Serialization.XmlArrayItemAttribute.Namespace" /> value is specified. </exception>
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x0009B6E6 File Offset: 0x000998E6
		// (set) Token: 0x06001B10 RID: 6928 RVA: 0x0009B6EE File Offset: 0x000998EE
		public XmlSchemaForm Form
		{
			get
			{
				return this.form;
			}
			set
			{
				this.form = value;
			}
		}

		// Token: 0x040019BD RID: 6589
		private string elementName;

		// Token: 0x040019BE RID: 6590
		private Type type;

		// Token: 0x040019BF RID: 6591
		private string ns;

		// Token: 0x040019C0 RID: 6592
		private string dataType;

		// Token: 0x040019C1 RID: 6593
		private bool nullable;

		// Token: 0x040019C2 RID: 6594
		private bool nullableSpecified;

		// Token: 0x040019C3 RID: 6595
		private XmlSchemaForm form;

		// Token: 0x040019C4 RID: 6596
		private int nestingLevel;
	}
}
