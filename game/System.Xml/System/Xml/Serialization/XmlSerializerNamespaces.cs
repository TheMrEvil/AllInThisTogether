using System;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Contains the XML namespaces and prefixes that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> uses to generate qualified names in an XML-document instance.</summary>
	// Token: 0x02000306 RID: 774
	public class XmlSerializerNamespaces
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> class.</summary>
		// Token: 0x06002038 RID: 8248 RVA: 0x0000216B File Offset: 0x0000036B
		public XmlSerializerNamespaces()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> class, using the specified instance of <see langword="XmlSerializerNamespaces" /> containing the collection of prefix and namespace pairs.</summary>
		/// <param name="namespaces">An instance of the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" />containing the namespace and prefix pairs. </param>
		// Token: 0x06002039 RID: 8249 RVA: 0x000D0906 File Offset: 0x000CEB06
		public XmlSerializerNamespaces(XmlSerializerNamespaces namespaces)
		{
			this.namespaces = (Hashtable)namespaces.Namespaces.Clone();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> class.</summary>
		/// <param name="namespaces">An array of <see cref="T:System.Xml.XmlQualifiedName" /> objects. </param>
		// Token: 0x0600203A RID: 8250 RVA: 0x000D0924 File Offset: 0x000CEB24
		public XmlSerializerNamespaces(XmlQualifiedName[] namespaces)
		{
			foreach (XmlQualifiedName xmlQualifiedName in namespaces)
			{
				this.Add(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
			}
		}

		/// <summary>Adds a prefix and namespace pair to an <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> object.</summary>
		/// <param name="prefix">The prefix associated with an XML namespace. </param>
		/// <param name="ns">An XML namespace. </param>
		// Token: 0x0600203B RID: 8251 RVA: 0x000D095B File Offset: 0x000CEB5B
		public void Add(string prefix, string ns)
		{
			if (prefix != null && prefix.Length > 0)
			{
				XmlConvert.VerifyNCName(prefix);
			}
			if (ns != null && ns.Length > 0)
			{
				XmlConvert.ToUri(ns);
			}
			this.AddInternal(prefix, ns);
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x000D098B File Offset: 0x000CEB8B
		internal void AddInternal(string prefix, string ns)
		{
			this.Namespaces[prefix] = ns;
		}

		/// <summary>Gets the array of prefix and namespace pairs in an <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> object.</summary>
		/// <returns>An array of <see cref="T:System.Xml.XmlQualifiedName" /> objects that are used as qualified names in an XML document.</returns>
		// Token: 0x0600203D RID: 8253 RVA: 0x000D099A File Offset: 0x000CEB9A
		public XmlQualifiedName[] ToArray()
		{
			if (this.NamespaceList == null)
			{
				return new XmlQualifiedName[0];
			}
			return (XmlQualifiedName[])this.NamespaceList.ToArray(typeof(XmlQualifiedName));
		}

		/// <summary>Gets the number of prefix and namespace pairs in the collection.</summary>
		/// <returns>The number of prefix and namespace pairs in the collection.</returns>
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x0600203E RID: 8254 RVA: 0x000D09C5 File Offset: 0x000CEBC5
		public int Count
		{
			get
			{
				return this.Namespaces.Count;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600203F RID: 8255 RVA: 0x000D09D4 File Offset: 0x000CEBD4
		internal ArrayList NamespaceList
		{
			get
			{
				if (this.namespaces == null || this.namespaces.Count == 0)
				{
					return null;
				}
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.Namespaces.Keys)
				{
					string text = (string)obj;
					arrayList.Add(new XmlQualifiedName(text, (string)this.Namespaces[text]));
				}
				return arrayList;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06002040 RID: 8256 RVA: 0x000D0A68 File Offset: 0x000CEC68
		// (set) Token: 0x06002041 RID: 8257 RVA: 0x000D0A83 File Offset: 0x000CEC83
		internal Hashtable Namespaces
		{
			get
			{
				if (this.namespaces == null)
				{
					this.namespaces = new Hashtable();
				}
				return this.namespaces;
			}
			set
			{
				this.namespaces = value;
			}
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x000D0A8C File Offset: 0x000CEC8C
		internal string LookupPrefix(string ns)
		{
			if (string.IsNullOrEmpty(ns))
			{
				return null;
			}
			if (this.namespaces == null || this.namespaces.Count == 0)
			{
				return null;
			}
			foreach (object obj in this.namespaces.Keys)
			{
				string text = (string)obj;
				if (!string.IsNullOrEmpty(text) && (string)this.namespaces[text] == ns)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x04001B20 RID: 6944
		private Hashtable namespaces;
	}
}
