using System;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) <see langword="any" /> element.</summary>
	// Token: 0x02000595 RID: 1429
	public class XmlSchemaAny : XmlSchemaParticle
	{
		/// <summary>Gets or sets the namespaces containing the elements that can be used.</summary>
		/// <returns>Namespaces for elements that are available for use. The default is <see langword="##any" />.Optional.</returns>
		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060039B4 RID: 14772 RVA: 0x0014BBE4 File Offset: 0x00149DE4
		// (set) Token: 0x060039B5 RID: 14773 RVA: 0x0014BBEC File Offset: 0x00149DEC
		[XmlAttribute("namespace")]
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

		/// <summary>Gets or sets information about how an application or XML processor should handle the validation of XML documents for the elements specified by the <see langword="any" /> element.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaContentProcessing" /> values. If no <see langword="processContents" /> attribute is specified, the default is <see langword="Strict" />.</returns>
		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060039B6 RID: 14774 RVA: 0x0014BBF5 File Offset: 0x00149DF5
		// (set) Token: 0x060039B7 RID: 14775 RVA: 0x0014BBFD File Offset: 0x00149DFD
		[DefaultValue(XmlSchemaContentProcessing.None)]
		[XmlAttribute("processContents")]
		public XmlSchemaContentProcessing ProcessContents
		{
			get
			{
				return this.processContents;
			}
			set
			{
				this.processContents = value;
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x060039B8 RID: 14776 RVA: 0x0014BC06 File Offset: 0x00149E06
		[XmlIgnore]
		internal NamespaceList NamespaceList
		{
			get
			{
				return this.namespaceList;
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x060039B9 RID: 14777 RVA: 0x0014BC0E File Offset: 0x00149E0E
		[XmlIgnore]
		internal string ResolvedNamespace
		{
			get
			{
				if (this.ns == null || this.ns.Length == 0)
				{
					return "##any";
				}
				return this.ns;
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x060039BA RID: 14778 RVA: 0x0014BC31 File Offset: 0x00149E31
		[XmlIgnore]
		internal XmlSchemaContentProcessing ProcessContentsCorrect
		{
			get
			{
				if (this.processContents != XmlSchemaContentProcessing.None)
				{
					return this.processContents;
				}
				return XmlSchemaContentProcessing.Strict;
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x060039BB RID: 14779 RVA: 0x0014BC44 File Offset: 0x00149E44
		internal override string NameString
		{
			get
			{
				switch (this.namespaceList.Type)
				{
				case NamespaceList.ListType.Any:
					return "##any:*";
				case NamespaceList.ListType.Other:
					return "##other:*";
				case NamespaceList.ListType.Set:
				{
					StringBuilder stringBuilder = new StringBuilder();
					int num = 1;
					foreach (object obj in this.namespaceList.Enumerate)
					{
						string str = (string)obj;
						stringBuilder.Append(str + ":*");
						if (num < this.namespaceList.Enumerate.Count)
						{
							stringBuilder.Append(" ");
						}
						num++;
					}
					return stringBuilder.ToString();
				}
				default:
					return string.Empty;
				}
			}
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x0014BD18 File Offset: 0x00149F18
		internal void BuildNamespaceList(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceList(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x0014BD40 File Offset: 0x00149F40
		internal void BuildNamespaceListV1Compat(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceListV1Compat(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x0014BD68 File Offset: 0x00149F68
		internal bool Allows(XmlQualifiedName qname)
		{
			return this.namespaceList.Allows(qname.Namespace);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaAny" /> class.</summary>
		// Token: 0x060039BF RID: 14783 RVA: 0x0014BD7B File Offset: 0x00149F7B
		public XmlSchemaAny()
		{
		}

		// Token: 0x04002ACE RID: 10958
		private string ns;

		// Token: 0x04002ACF RID: 10959
		private XmlSchemaContentProcessing processContents;

		// Token: 0x04002AD0 RID: 10960
		private NamespaceList namespaceList;
	}
}
