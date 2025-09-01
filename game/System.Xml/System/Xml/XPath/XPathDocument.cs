using System;
using System.Collections.Generic;
using System.IO;
using MS.Internal.Xml.Cache;

namespace System.Xml.XPath
{
	/// <summary>Provides a fast, read-only, in-memory representation of an XML document by using the XPath data model.</summary>
	// Token: 0x0200024F RID: 591
	public class XPathDocument : IXPathNavigable
	{
		// Token: 0x060015C8 RID: 5576 RVA: 0x00084E91 File Offset: 0x00083091
		internal XPathDocument()
		{
			this.nameTable = new NameTable();
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x00084EA4 File Offset: 0x000830A4
		internal XPathDocument(XmlNameTable nameTable)
		{
			if (nameTable == null)
			{
				throw new ArgumentNullException("nameTable");
			}
			this.nameTable = nameTable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathDocument" /> class from the XML data that is contained in the specified <see cref="T:System.Xml.XmlReader" /> object.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that contains the XML data. </param>
		/// <exception cref="T:System.Xml.XmlException">An error was encountered in the XML data. The <see cref="T:System.Xml.XPath.XPathDocument" /> remains empty. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object passed as a parameter is <see langword="null" />.</exception>
		// Token: 0x060015CA RID: 5578 RVA: 0x00084EC1 File Offset: 0x000830C1
		public XPathDocument(XmlReader reader) : this(reader, XmlSpace.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathDocument" /> class from the XML data that is contained in the specified <see cref="T:System.Xml.XmlReader" /> object with the specified white space handling.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that contains the XML data.</param>
		/// <param name="space">An <see cref="T:System.Xml.XmlSpace" /> object.</param>
		/// <exception cref="T:System.Xml.XmlException">An error was encountered in the XML data. The <see cref="T:System.Xml.XPath.XPathDocument" /> remains empty. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object parameter or <see cref="T:System.Xml.XmlSpace" /> object parameter is <see langword="null" />.</exception>
		// Token: 0x060015CB RID: 5579 RVA: 0x00084ECB File Offset: 0x000830CB
		public XPathDocument(XmlReader reader, XmlSpace space)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.LoadFromReader(reader, space);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathDocument" /> class from the XML data that is contained in the specified <see cref="T:System.IO.TextReader" /> object.</summary>
		/// <param name="textReader">The <see cref="T:System.IO.TextReader" /> object that contains the XML data.</param>
		/// <exception cref="T:System.Xml.XmlException">An error was encountered in the XML data. The <see cref="T:System.Xml.XPath.XPathDocument" /> remains empty. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.IO.TextReader" /> object passed as a parameter is <see langword="null" />.</exception>
		// Token: 0x060015CC RID: 5580 RVA: 0x00084EEC File Offset: 0x000830EC
		public XPathDocument(TextReader textReader)
		{
			XmlTextReaderImpl xmlTextReaderImpl = this.SetupReader(new XmlTextReaderImpl(string.Empty, textReader));
			try
			{
				this.LoadFromReader(xmlTextReaderImpl, XmlSpace.Default);
			}
			finally
			{
				xmlTextReaderImpl.Close();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathDocument" /> class from the XML data in the specified <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> object that contains the XML data.</param>
		/// <exception cref="T:System.Xml.XmlException">An error was encountered in the XML data. The <see cref="T:System.Xml.XPath.XPathDocument" /> remains empty. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.IO.Stream" /> object passed as a parameter is <see langword="null" />.</exception>
		// Token: 0x060015CD RID: 5581 RVA: 0x00084F34 File Offset: 0x00083134
		public XPathDocument(Stream stream)
		{
			XmlTextReaderImpl xmlTextReaderImpl = this.SetupReader(new XmlTextReaderImpl(string.Empty, stream));
			try
			{
				this.LoadFromReader(xmlTextReaderImpl, XmlSpace.Default);
			}
			finally
			{
				xmlTextReaderImpl.Close();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathDocument" /> class from the XML data in the specified file.</summary>
		/// <param name="uri">The path of the file that contains the XML data.</param>
		/// <exception cref="T:System.Xml.XmlException">An error was encountered in the XML data. The <see cref="T:System.Xml.XPath.XPathDocument" /> remains empty. </exception>
		/// <exception cref="T:System.ArgumentNullException">The file path parameter is <see langword="null" />.</exception>
		// Token: 0x060015CE RID: 5582 RVA: 0x00084F7C File Offset: 0x0008317C
		public XPathDocument(string uri) : this(uri, XmlSpace.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathDocument" /> class from the XML data in the file specified with the white space handling specified.</summary>
		/// <param name="uri">The path of the file that contains the XML data.</param>
		/// <param name="space">An <see cref="T:System.Xml.XmlSpace" /> object.</param>
		/// <exception cref="T:System.Xml.XmlException">An error was encountered in the XML data. The <see cref="T:System.Xml.XPath.XPathDocument" /> remains empty. </exception>
		/// <exception cref="T:System.ArgumentNullException">The file path parameter or <see cref="T:System.Xml.XmlSpace" /> object parameter is <see langword="null" />.</exception>
		// Token: 0x060015CF RID: 5583 RVA: 0x00084F88 File Offset: 0x00083188
		public XPathDocument(string uri, XmlSpace space)
		{
			XmlTextReaderImpl xmlTextReaderImpl = this.SetupReader(new XmlTextReaderImpl(uri));
			try
			{
				this.LoadFromReader(xmlTextReaderImpl, space);
			}
			finally
			{
				xmlTextReaderImpl.Close();
			}
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x00084FCC File Offset: 0x000831CC
		internal XmlRawWriter LoadFromWriter(XPathDocument.LoadFlags flags, string baseUri)
		{
			return new XPathDocumentBuilder(this, null, baseUri, flags);
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00084FD8 File Offset: 0x000831D8
		internal void LoadFromReader(XmlReader reader, XmlSpace space)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			if (xmlLineInfo == null || !xmlLineInfo.HasLineInfo())
			{
				xmlLineInfo = null;
			}
			this.hasLineInfo = (xmlLineInfo != null);
			this.nameTable = reader.NameTable;
			XPathDocumentBuilder xpathDocumentBuilder = new XPathDocumentBuilder(this, xmlLineInfo, reader.BaseURI, XPathDocument.LoadFlags.None);
			try
			{
				bool flag = reader.ReadState == ReadState.Initial;
				int depth = reader.Depth;
				string text = this.nameTable.Get("http://www.w3.org/2000/xmlns/");
				if (!flag || reader.Read())
				{
					while (flag || reader.Depth >= depth)
					{
						switch (reader.NodeType)
						{
						case XmlNodeType.Element:
						{
							bool isEmptyElement = reader.IsEmptyElement;
							xpathDocumentBuilder.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI, reader.BaseURI);
							while (reader.MoveToNextAttribute())
							{
								string namespaceURI = reader.NamespaceURI;
								if (namespaceURI == text)
								{
									if (reader.Prefix.Length == 0)
									{
										xpathDocumentBuilder.WriteNamespaceDeclaration(string.Empty, reader.Value);
									}
									else
									{
										xpathDocumentBuilder.WriteNamespaceDeclaration(reader.LocalName, reader.Value);
									}
								}
								else
								{
									xpathDocumentBuilder.WriteStartAttribute(reader.Prefix, reader.LocalName, namespaceURI);
									xpathDocumentBuilder.WriteString(reader.Value, TextBlockType.Text);
									xpathDocumentBuilder.WriteEndAttribute();
								}
							}
							if (isEmptyElement)
							{
								xpathDocumentBuilder.WriteEndElement(true);
							}
							break;
						}
						case XmlNodeType.Text:
						case XmlNodeType.CDATA:
							xpathDocumentBuilder.WriteString(reader.Value, TextBlockType.Text);
							break;
						case XmlNodeType.EntityReference:
							reader.ResolveEntity();
							break;
						case XmlNodeType.ProcessingInstruction:
							xpathDocumentBuilder.WriteProcessingInstruction(reader.LocalName, reader.Value, reader.BaseURI);
							break;
						case XmlNodeType.Comment:
							xpathDocumentBuilder.WriteComment(reader.Value);
							break;
						case XmlNodeType.DocumentType:
						{
							IDtdInfo dtdInfo = reader.DtdInfo;
							if (dtdInfo != null)
							{
								xpathDocumentBuilder.CreateIdTables(dtdInfo);
							}
							break;
						}
						case XmlNodeType.Whitespace:
							goto IL_1C6;
						case XmlNodeType.SignificantWhitespace:
							if (reader.XmlSpace != XmlSpace.Preserve)
							{
								goto IL_1C6;
							}
							xpathDocumentBuilder.WriteString(reader.Value, TextBlockType.SignificantWhitespace);
							break;
						case XmlNodeType.EndElement:
							xpathDocumentBuilder.WriteEndElement(false);
							break;
						}
						IL_228:
						if (!reader.Read())
						{
							break;
						}
						continue;
						IL_1C6:
						if (space == XmlSpace.Preserve && (!flag || reader.Depth != 0))
						{
							xpathDocumentBuilder.WriteString(reader.Value, TextBlockType.Whitespace);
							goto IL_228;
						}
						goto IL_228;
					}
				}
			}
			finally
			{
				xpathDocumentBuilder.Close();
			}
		}

		/// <summary>Initializes a read-only <see cref="T:System.Xml.XPath.XPathNavigator" /> object for navigating through nodes in this <see cref="T:System.Xml.XPath.XPathDocument" />.</summary>
		/// <returns>A read-only <see cref="T:System.Xml.XPath.XPathNavigator" /> object.</returns>
		// Token: 0x060015D2 RID: 5586 RVA: 0x00085240 File Offset: 0x00083440
		public XPathNavigator CreateNavigator()
		{
			return new XPathDocumentNavigator(this.pageRoot, this.idxRoot, null, 0);
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x00085255 File Offset: 0x00083455
		internal XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x0008525D File Offset: 0x0008345D
		internal bool HasLineInfo
		{
			get
			{
				return this.hasLineInfo;
			}
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x00085265 File Offset: 0x00083465
		internal int GetCollapsedTextNode(out XPathNode[] pageText)
		{
			pageText = this.pageText;
			return this.idxText;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00085275 File Offset: 0x00083475
		internal void SetCollapsedTextNode(XPathNode[] pageText, int idxText)
		{
			this.pageText = pageText;
			this.idxText = idxText;
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00085285 File Offset: 0x00083485
		internal int GetRootNode(out XPathNode[] pageRoot)
		{
			pageRoot = this.pageRoot;
			return this.idxRoot;
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00085295 File Offset: 0x00083495
		internal void SetRootNode(XPathNode[] pageRoot, int idxRoot)
		{
			this.pageRoot = pageRoot;
			this.idxRoot = idxRoot;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x000852A5 File Offset: 0x000834A5
		internal int GetXmlNamespaceNode(out XPathNode[] pageXmlNmsp)
		{
			pageXmlNmsp = this.pageXmlNmsp;
			return this.idxXmlNmsp;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x000852B5 File Offset: 0x000834B5
		internal void SetXmlNamespaceNode(XPathNode[] pageXmlNmsp, int idxXmlNmsp)
		{
			this.pageXmlNmsp = pageXmlNmsp;
			this.idxXmlNmsp = idxXmlNmsp;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x000852C5 File Offset: 0x000834C5
		internal void AddNamespace(XPathNode[] pageElem, int idxElem, XPathNode[] pageNmsp, int idxNmsp)
		{
			if (this.mapNmsp == null)
			{
				this.mapNmsp = new Dictionary<XPathNodeRef, XPathNodeRef>();
			}
			this.mapNmsp.Add(new XPathNodeRef(pageElem, idxElem), new XPathNodeRef(pageNmsp, idxNmsp));
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x000852F4 File Offset: 0x000834F4
		internal int LookupNamespaces(XPathNode[] pageElem, int idxElem, out XPathNode[] pageNmsp)
		{
			XPathNodeRef key = new XPathNodeRef(pageElem, idxElem);
			if (this.mapNmsp == null || !this.mapNmsp.ContainsKey(key))
			{
				pageNmsp = null;
				return 0;
			}
			key = this.mapNmsp[key];
			pageNmsp = key.Page;
			return key.Index;
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00085342 File Offset: 0x00083542
		internal void AddIdElement(string id, XPathNode[] pageElem, int idxElem)
		{
			if (this.idValueMap == null)
			{
				this.idValueMap = new Dictionary<string, XPathNodeRef>();
			}
			if (!this.idValueMap.ContainsKey(id))
			{
				this.idValueMap.Add(id, new XPathNodeRef(pageElem, idxElem));
			}
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00085378 File Offset: 0x00083578
		internal int LookupIdElement(string id, out XPathNode[] pageElem)
		{
			if (this.idValueMap == null || !this.idValueMap.ContainsKey(id))
			{
				pageElem = null;
				return 0;
			}
			XPathNodeRef xpathNodeRef = this.idValueMap[id];
			pageElem = xpathNodeRef.Page;
			return xpathNodeRef.Index;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x000853BD File Offset: 0x000835BD
		private XmlTextReaderImpl SetupReader(XmlTextReaderImpl reader)
		{
			reader.EntityHandling = EntityHandling.ExpandEntities;
			reader.XmlValidatingReaderCompatibilityMode = true;
			return reader;
		}

		// Token: 0x040017DA RID: 6106
		private XPathNode[] pageText;

		// Token: 0x040017DB RID: 6107
		private XPathNode[] pageRoot;

		// Token: 0x040017DC RID: 6108
		private XPathNode[] pageXmlNmsp;

		// Token: 0x040017DD RID: 6109
		private int idxText;

		// Token: 0x040017DE RID: 6110
		private int idxRoot;

		// Token: 0x040017DF RID: 6111
		private int idxXmlNmsp;

		// Token: 0x040017E0 RID: 6112
		private XmlNameTable nameTable;

		// Token: 0x040017E1 RID: 6113
		private bool hasLineInfo;

		// Token: 0x040017E2 RID: 6114
		private Dictionary<XPathNodeRef, XPathNodeRef> mapNmsp;

		// Token: 0x040017E3 RID: 6115
		private Dictionary<string, XPathNodeRef> idValueMap;

		// Token: 0x02000250 RID: 592
		internal enum LoadFlags
		{
			// Token: 0x040017E5 RID: 6117
			None,
			// Token: 0x040017E6 RID: 6118
			AtomizeNames,
			// Token: 0x040017E7 RID: 6119
			Fragment
		}
	}
}
