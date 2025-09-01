using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml
{
	/// <summary>Resolves, adds, and removes namespaces to a collection and provides scope management for these namespaces. </summary>
	// Token: 0x0200023E RID: 574
	public class XmlNamespaceManager : IXmlNamespaceResolver, IEnumerable
	{
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x00084053 File Offset: 0x00082253
		internal static IXmlNamespaceResolver EmptyResolver
		{
			get
			{
				if (XmlNamespaceManager.s_EmptyResolver == null)
				{
					XmlNamespaceManager.s_EmptyResolver = new XmlNamespaceManager(new NameTable());
				}
				return XmlNamespaceManager.s_EmptyResolver;
			}
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0000216B File Offset: 0x0000036B
		internal XmlNamespaceManager()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlNamespaceManager" /> class with the specified <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="nameTable">The <see cref="T:System.Xml.XmlNameTable" /> to use. </param>
		/// <exception cref="T:System.NullReferenceException">
		///         <see langword="null" /> is passed to the constructor </exception>
		// Token: 0x06001579 RID: 5497 RVA: 0x00084078 File Offset: 0x00082278
		public XmlNamespaceManager(XmlNameTable nameTable)
		{
			this.nameTable = nameTable;
			this.xml = nameTable.Add("xml");
			this.xmlNs = nameTable.Add("xmlns");
			this.nsdecls = new XmlNamespaceManager.NamespaceDeclaration[8];
			string text = nameTable.Add(string.Empty);
			this.nsdecls[0].Set(text, text, -1, -1);
			this.nsdecls[1].Set(this.xmlNs, nameTable.Add("http://www.w3.org/2000/xmlns/"), -1, -1);
			this.nsdecls[2].Set(this.xml, nameTable.Add("http://www.w3.org/XML/1998/namespace"), 0, -1);
			this.lastDecl = 2;
			this.scopeId = 1;
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNameTable" /> associated with this object.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNameTable" /> used by this object.</returns>
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x00084137 File Offset: 0x00082337
		public virtual XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		/// <summary>Gets the namespace URI for the default namespace.</summary>
		/// <returns>Returns the namespace URI for the default namespace, or String.Empty if there is no default namespace.</returns>
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x00084140 File Offset: 0x00082340
		public virtual string DefaultNamespace
		{
			get
			{
				string text = this.LookupNamespace(string.Empty);
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
		}

		/// <summary>Pushes a namespace scope onto the stack.</summary>
		// Token: 0x0600157C RID: 5500 RVA: 0x00084163 File Offset: 0x00082363
		public virtual void PushScope()
		{
			this.scopeId++;
		}

		/// <summary>Pops a namespace scope off the stack.</summary>
		/// <returns>
		///     <see langword="true" /> if there are namespace scopes left on the stack; <see langword="false" /> if there are no more namespaces to pop.</returns>
		// Token: 0x0600157D RID: 5501 RVA: 0x00084174 File Offset: 0x00082374
		public virtual bool PopScope()
		{
			int num = this.lastDecl;
			if (this.scopeId == 1)
			{
				return false;
			}
			while (this.nsdecls[num].scopeId == this.scopeId)
			{
				if (this.useHashtable)
				{
					this.hashTable[this.nsdecls[num].prefix] = this.nsdecls[num].previousNsIndex;
				}
				num--;
			}
			this.lastDecl = num;
			this.scopeId--;
			return true;
		}

		/// <summary>Adds the given namespace to the collection.</summary>
		/// <param name="prefix">The prefix to associate with the namespace being added. Use String.Empty to add a default namespace.
		///       NoteIf the <see cref="T:System.Xml.XmlNamespaceManager" /> will be used for resolving namespaces in an XML Path Language (XPath) expression, a prefix must be specified. If an XPath expression does not include a prefix, it is assumed that the namespace Uniform Resource Identifier (URI) is the empty namespace. For more information about XPath expressions and the <see cref="T:System.Xml.XmlNamespaceManager" />, refer to the <see cref="M:System.Xml.XmlNode.SelectNodes(System.String)" /> and <see cref="M:System.Xml.XPath.XPathExpression.SetContext(System.Xml.XmlNamespaceManager)" /> methods.</param>
		/// <param name="uri">The namespace to add. </param>
		/// <exception cref="T:System.ArgumentException">The value for <paramref name="prefix" /> is "xml" or "xmlns". </exception>
		/// <exception cref="T:System.ArgumentNullException">The value for <paramref name="prefix" /> or <paramref name="uri" /> is <see langword="null" />. </exception>
		// Token: 0x0600157E RID: 5502 RVA: 0x000841FC File Offset: 0x000823FC
		public virtual void AddNamespace(string prefix, string uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			prefix = this.nameTable.Add(prefix);
			uri = this.nameTable.Add(uri);
			if (Ref.Equal(this.xml, prefix) && !uri.Equals("http://www.w3.org/XML/1998/namespace"))
			{
				throw new ArgumentException(Res.GetString("Prefix \"xml\" is reserved for use by XML and can be mapped only to namespace name \"http://www.w3.org/XML/1998/namespace\"."));
			}
			if (Ref.Equal(this.xmlNs, prefix))
			{
				throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
			}
			int num = this.LookupNamespaceDecl(prefix);
			int previousNsIndex = -1;
			if (num != -1)
			{
				if (this.nsdecls[num].scopeId == this.scopeId)
				{
					this.nsdecls[num].uri = uri;
					return;
				}
				previousNsIndex = num;
			}
			if (this.lastDecl == this.nsdecls.Length - 1)
			{
				XmlNamespaceManager.NamespaceDeclaration[] destinationArray = new XmlNamespaceManager.NamespaceDeclaration[this.nsdecls.Length * 2];
				Array.Copy(this.nsdecls, 0, destinationArray, 0, this.nsdecls.Length);
				this.nsdecls = destinationArray;
			}
			XmlNamespaceManager.NamespaceDeclaration[] array = this.nsdecls;
			int num2 = this.lastDecl + 1;
			this.lastDecl = num2;
			array[num2].Set(prefix, uri, this.scopeId, previousNsIndex);
			if (this.useHashtable)
			{
				this.hashTable[prefix] = this.lastDecl;
				return;
			}
			if (this.lastDecl >= 16)
			{
				this.hashTable = new Dictionary<string, int>(this.lastDecl);
				for (int i = 0; i <= this.lastDecl; i++)
				{
					this.hashTable[this.nsdecls[i].prefix] = i;
				}
				this.useHashtable = true;
			}
		}

		/// <summary>Removes the given namespace for the given prefix.</summary>
		/// <param name="prefix">The prefix for the namespace </param>
		/// <param name="uri">The namespace to remove for the given prefix. The namespace removed is from the current namespace scope. Namespaces outside the current scope are ignored. </param>
		/// <exception cref="T:System.ArgumentNullException">The value of <paramref name="prefix" /> or <paramref name="uri" /> is <see langword="null" />. </exception>
		// Token: 0x0600157F RID: 5503 RVA: 0x000843A0 File Offset: 0x000825A0
		public virtual void RemoveNamespace(string prefix, string uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			for (int num = this.LookupNamespaceDecl(prefix); num != -1; num = this.nsdecls[num].previousNsIndex)
			{
				if (string.Equals(this.nsdecls[num].uri, uri) && this.nsdecls[num].scopeId == this.scopeId)
				{
					this.nsdecls[num].uri = null;
				}
			}
		}

		/// <summary>Returns an enumerator to use to iterate through the namespaces in the <see cref="T:System.Xml.XmlNamespaceManager" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> containing the prefixes stored by the <see cref="T:System.Xml.XmlNamespaceManager" />.</returns>
		// Token: 0x06001580 RID: 5504 RVA: 0x00084430 File Offset: 0x00082630
		public virtual IEnumerator GetEnumerator()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(this.lastDecl + 1);
			for (int i = 0; i <= this.lastDecl; i++)
			{
				if (this.nsdecls[i].uri != null)
				{
					dictionary[this.nsdecls[i].prefix] = this.nsdecls[i].prefix;
				}
			}
			return dictionary.Keys.GetEnumerator();
		}

		/// <summary>Gets a collection of namespace names keyed by prefix which can be used to enumerate the namespaces currently in scope.</summary>
		/// <param name="scope">An enumeration value that specifies the type of namespace nodes to return.</param>
		/// <returns>A collection of namespace and prefix pairs currently in scope.</returns>
		// Token: 0x06001581 RID: 5505 RVA: 0x000844A8 File Offset: 0x000826A8
		public virtual IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			int i = 0;
			switch (scope)
			{
			case XmlNamespaceScope.All:
				i = 2;
				break;
			case XmlNamespaceScope.ExcludeXml:
				i = 3;
				break;
			case XmlNamespaceScope.Local:
				i = this.lastDecl;
				while (this.nsdecls[i].scopeId == this.scopeId)
				{
					i--;
				}
				i++;
				break;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>(this.lastDecl - i + 1);
			while (i <= this.lastDecl)
			{
				string prefix = this.nsdecls[i].prefix;
				string uri = this.nsdecls[i].uri;
				if (uri != null)
				{
					if (uri.Length > 0 || prefix.Length > 0 || scope == XmlNamespaceScope.Local)
					{
						dictionary[prefix] = uri;
					}
					else
					{
						dictionary.Remove(prefix);
					}
				}
				i++;
			}
			return dictionary;
		}

		/// <summary>Gets the namespace URI for the specified prefix.</summary>
		/// <param name="prefix">The prefix whose namespace URI you want to resolve. To match the default namespace, pass String.Empty. </param>
		/// <returns>Returns the namespace URI for <paramref name="prefix" /> or <see langword="null" /> if there is no mapped namespace. The returned string is atomized.For more information on atomized strings, see the <see cref="T:System.Xml.XmlNameTable" /> class.</returns>
		// Token: 0x06001582 RID: 5506 RVA: 0x0008456C File Offset: 0x0008276C
		public virtual string LookupNamespace(string prefix)
		{
			int num = this.LookupNamespaceDecl(prefix);
			if (num != -1)
			{
				return this.nsdecls[num].uri;
			}
			return null;
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x00084598 File Offset: 0x00082798
		private int LookupNamespaceDecl(string prefix)
		{
			if (!this.useHashtable)
			{
				for (int i = this.lastDecl; i >= 0; i--)
				{
					if (this.nsdecls[i].prefix == prefix && this.nsdecls[i].uri != null)
					{
						return i;
					}
				}
				for (int j = this.lastDecl; j >= 0; j--)
				{
					if (string.Equals(this.nsdecls[j].prefix, prefix) && this.nsdecls[j].uri != null)
					{
						return j;
					}
				}
				return -1;
			}
			int previousNsIndex;
			if (this.hashTable.TryGetValue(prefix, out previousNsIndex))
			{
				while (previousNsIndex != -1 && this.nsdecls[previousNsIndex].uri == null)
				{
					previousNsIndex = this.nsdecls[previousNsIndex].previousNsIndex;
				}
				return previousNsIndex;
			}
			return -1;
		}

		/// <summary>Finds the prefix declared for the given namespace URI.</summary>
		/// <param name="uri">The namespace to resolve for the prefix. </param>
		/// <returns>The matching prefix. If there is no mapped prefix, the method returns String.Empty. If a null value is supplied, then <see langword="null" /> is returned.</returns>
		// Token: 0x06001584 RID: 5508 RVA: 0x00084668 File Offset: 0x00082868
		public virtual string LookupPrefix(string uri)
		{
			for (int i = this.lastDecl; i >= 0; i--)
			{
				if (string.Equals(this.nsdecls[i].uri, uri))
				{
					string prefix = this.nsdecls[i].prefix;
					if (string.Equals(this.LookupNamespace(prefix), uri))
					{
						return prefix;
					}
				}
			}
			return null;
		}

		/// <summary>Gets a value indicating whether the supplied prefix has a namespace defined for the current pushed scope.</summary>
		/// <param name="prefix">The prefix of the namespace you want to find. </param>
		/// <returns>
		///     <see langword="true" /> if there is a namespace defined; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001585 RID: 5509 RVA: 0x000846C4 File Offset: 0x000828C4
		public virtual bool HasNamespace(string prefix)
		{
			int num = this.lastDecl;
			while (this.nsdecls[num].scopeId == this.scopeId)
			{
				if (string.Equals(this.nsdecls[num].prefix, prefix) && this.nsdecls[num].uri != null)
				{
					return prefix.Length > 0 || this.nsdecls[num].uri.Length > 0;
				}
				num--;
			}
			return false;
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0008474C File Offset: 0x0008294C
		internal bool GetNamespaceDeclaration(int idx, out string prefix, out string uri)
		{
			idx = this.lastDecl - idx;
			if (idx < 0)
			{
				string text;
				uri = (text = null);
				prefix = text;
				return false;
			}
			prefix = this.nsdecls[idx].prefix;
			uri = this.nsdecls[idx].uri;
			return true;
		}

		// Token: 0x040012DF RID: 4831
		private static volatile IXmlNamespaceResolver s_EmptyResolver;

		// Token: 0x040012E0 RID: 4832
		private XmlNamespaceManager.NamespaceDeclaration[] nsdecls;

		// Token: 0x040012E1 RID: 4833
		private int lastDecl;

		// Token: 0x040012E2 RID: 4834
		private XmlNameTable nameTable;

		// Token: 0x040012E3 RID: 4835
		private int scopeId;

		// Token: 0x040012E4 RID: 4836
		private Dictionary<string, int> hashTable;

		// Token: 0x040012E5 RID: 4837
		private bool useHashtable;

		// Token: 0x040012E6 RID: 4838
		private string xml;

		// Token: 0x040012E7 RID: 4839
		private string xmlNs;

		// Token: 0x040012E8 RID: 4840
		private const int MinDeclsCountForHashtable = 16;

		// Token: 0x0200023F RID: 575
		private struct NamespaceDeclaration
		{
			// Token: 0x06001587 RID: 5511 RVA: 0x00084798 File Offset: 0x00082998
			public void Set(string prefix, string uri, int scopeId, int previousNsIndex)
			{
				this.prefix = prefix;
				this.uri = uri;
				this.scopeId = scopeId;
				this.previousNsIndex = previousNsIndex;
			}

			// Token: 0x040012E9 RID: 4841
			public string prefix;

			// Token: 0x040012EA RID: 4842
			public string uri;

			// Token: 0x040012EB RID: 4843
			public int scopeId;

			// Token: 0x040012EC RID: 4844
			public int previousNsIndex;
		}
	}
}
