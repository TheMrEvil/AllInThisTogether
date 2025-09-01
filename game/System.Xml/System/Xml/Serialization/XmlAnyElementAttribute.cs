using System;

namespace System.Xml.Serialization
{
	/// <summary>Specifies that the member (a field that returns an array of <see cref="T:System.Xml.XmlElement" /> or <see cref="T:System.Xml.XmlNode" /> objects) contains objects that represent any XML element that has no corresponding member in the object being serialized or deserialized.</summary>
	// Token: 0x020002C4 RID: 708
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true)]
	public class XmlAnyElementAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> class.</summary>
		// Token: 0x06001ADF RID: 6879 RVA: 0x0009B44E File Offset: 0x0009964E
		public XmlAnyElementAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> class and specifies the XML element name generated in the XML document.</summary>
		/// <param name="name">The name of the XML element that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates. </param>
		// Token: 0x06001AE0 RID: 6880 RVA: 0x0009B45D File Offset: 0x0009965D
		public XmlAnyElementAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> class and specifies the XML element name generated in the XML document and its XML namespace.</summary>
		/// <param name="name">The name of the XML element that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates. </param>
		/// <param name="ns">The XML namespace of the XML element. </param>
		// Token: 0x06001AE1 RID: 6881 RVA: 0x0009B473 File Offset: 0x00099673
		public XmlAnyElementAttribute(string name, string ns)
		{
			this.name = name;
			this.ns = ns;
			this.nsSpecified = true;
		}

		/// <summary>Gets or sets the XML element name.</summary>
		/// <returns>The name of the XML element.</returns>
		/// <exception cref="T:System.InvalidOperationException">The element name of an array member does not match the element name specified by the <see cref="P:System.Xml.Serialization.XmlAnyElementAttribute.Name" /> property. </exception>
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x0009B497 File Offset: 0x00099697
		// (set) Token: 0x06001AE3 RID: 6883 RVA: 0x0009B4AD File Offset: 0x000996AD
		public string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the XML namespace generated in the XML document.</summary>
		/// <returns>An XML namespace.</returns>
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x0009B4B6 File Offset: 0x000996B6
		// (set) Token: 0x06001AE5 RID: 6885 RVA: 0x0009B4BE File Offset: 0x000996BE
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
				this.nsSpecified = true;
			}
		}

		/// <summary>Gets or sets the explicit order in which the elements are serialized or deserialized.</summary>
		/// <returns>The order of the code generation.</returns>
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x0009B4CE File Offset: 0x000996CE
		// (set) Token: 0x06001AE7 RID: 6887 RVA: 0x0009B4D6 File Offset: 0x000996D6
		public int Order
		{
			get
			{
				return this.order;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(Res.GetString("Negative values are prohibited."), "Order");
				}
				this.order = value;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x0009B4F8 File Offset: 0x000996F8
		internal bool NamespaceSpecified
		{
			get
			{
				return this.nsSpecified;
			}
		}

		// Token: 0x040019B4 RID: 6580
		private string name;

		// Token: 0x040019B5 RID: 6581
		private string ns;

		// Token: 0x040019B6 RID: 6582
		private int order = -1;

		// Token: 0x040019B7 RID: 6583
		private bool nsSpecified;
	}
}
