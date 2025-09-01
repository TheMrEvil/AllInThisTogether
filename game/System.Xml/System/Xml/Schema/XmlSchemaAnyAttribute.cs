using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) <see langword="anyAttribute" /> element.</summary>
	// Token: 0x02000596 RID: 1430
	public class XmlSchemaAnyAttribute : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the namespaces containing the attributes that can be used.</summary>
		/// <returns>Namespaces for attributes that are available for use. The default is <see langword="##any" />.Optional.</returns>
		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x060039C0 RID: 14784 RVA: 0x0014BD83 File Offset: 0x00149F83
		// (set) Token: 0x060039C1 RID: 14785 RVA: 0x0014BD8B File Offset: 0x00149F8B
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

		/// <summary>Gets or sets information about how an application or XML processor should handle the validation of XML documents for the attributes specified by the <see langword="anyAttribute" /> element.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaContentProcessing" /> values. If no <see langword="processContents" /> attribute is specified, the default is <see langword="Strict" />.</returns>
		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x060039C2 RID: 14786 RVA: 0x0014BD94 File Offset: 0x00149F94
		// (set) Token: 0x060039C3 RID: 14787 RVA: 0x0014BD9C File Offset: 0x00149F9C
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

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x060039C4 RID: 14788 RVA: 0x0014BDA5 File Offset: 0x00149FA5
		[XmlIgnore]
		internal NamespaceList NamespaceList
		{
			get
			{
				return this.namespaceList;
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x060039C5 RID: 14789 RVA: 0x0014BDAD File Offset: 0x00149FAD
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

		// Token: 0x060039C6 RID: 14790 RVA: 0x0014BDBF File Offset: 0x00149FBF
		internal void BuildNamespaceList(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceList(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x0014BDE7 File Offset: 0x00149FE7
		internal void BuildNamespaceListV1Compat(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceListV1Compat(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x0014BE0F File Offset: 0x0014A00F
		internal bool Allows(XmlQualifiedName qname)
		{
			return this.namespaceList.Allows(qname.Namespace);
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x0014BE22 File Offset: 0x0014A022
		internal static bool IsSubset(XmlSchemaAnyAttribute sub, XmlSchemaAnyAttribute super)
		{
			return NamespaceList.IsSubset(sub.NamespaceList, super.NamespaceList);
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x0014BE38 File Offset: 0x0014A038
		internal static XmlSchemaAnyAttribute Intersection(XmlSchemaAnyAttribute o1, XmlSchemaAnyAttribute o2, bool v1Compat)
		{
			NamespaceList namespaceList = NamespaceList.Intersection(o1.NamespaceList, o2.NamespaceList, v1Compat);
			if (namespaceList != null)
			{
				return new XmlSchemaAnyAttribute
				{
					namespaceList = namespaceList,
					ProcessContents = o1.ProcessContents,
					Annotation = o1.Annotation
				};
			}
			return null;
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x0014BE84 File Offset: 0x0014A084
		internal static XmlSchemaAnyAttribute Union(XmlSchemaAnyAttribute o1, XmlSchemaAnyAttribute o2, bool v1Compat)
		{
			NamespaceList namespaceList = NamespaceList.Union(o1.NamespaceList, o2.NamespaceList, v1Compat);
			if (namespaceList != null)
			{
				return new XmlSchemaAnyAttribute
				{
					namespaceList = namespaceList,
					processContents = o1.processContents,
					Annotation = o1.Annotation
				};
			}
			return null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> class.</summary>
		// Token: 0x060039CC RID: 14796 RVA: 0x0014BECD File Offset: 0x0014A0CD
		public XmlSchemaAnyAttribute()
		{
		}

		// Token: 0x04002AD1 RID: 10961
		private string ns;

		// Token: 0x04002AD2 RID: 10962
		private XmlSchemaContentProcessing processContents;

		// Token: 0x04002AD3 RID: 10963
		private NamespaceList namespaceList;
	}
}
