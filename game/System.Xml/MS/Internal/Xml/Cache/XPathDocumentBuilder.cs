using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000667 RID: 1639
	internal sealed class XPathDocumentBuilder : XmlRawWriter
	{
		// Token: 0x06004254 RID: 16980 RVA: 0x0016AAA6 File Offset: 0x00168CA6
		public XPathDocumentBuilder(XPathDocument doc, IXmlLineInfo lineInfo, string baseUri, XPathDocument.LoadFlags flags)
		{
			this._nodePageFact.Init(256);
			this._nmspPageFact.Init(16);
			this._stkNmsp = new Stack<XPathNodeRef>();
			this.Initialize(doc, lineInfo, baseUri, flags);
		}

		// Token: 0x06004255 RID: 16981 RVA: 0x0016AAE4 File Offset: 0x00168CE4
		public void Initialize(XPathDocument doc, IXmlLineInfo lineInfo, string baseUri, XPathDocument.LoadFlags flags)
		{
			this._doc = doc;
			this._nameTable = doc.NameTable;
			this._atomizeNames = ((flags & XPathDocument.LoadFlags.AtomizeNames) > XPathDocument.LoadFlags.None);
			this._idxParent = (this._idxSibling = 0);
			this._elemNameIndex = new XPathNodeRef[64];
			this._textBldr.Initialize(lineInfo);
			this._lineInfo = lineInfo;
			this._lineNumBase = 0;
			this._linePosBase = 0;
			this._infoTable = new XPathNodeInfoTable();
			XPathNode[] pageText;
			int idxText = this.NewNode(out pageText, XPathNodeType.Text, string.Empty, string.Empty, string.Empty, string.Empty);
			this._doc.SetCollapsedTextNode(pageText, idxText);
			this._idxNmsp = this.NewNamespaceNode(out this._pageNmsp, this._nameTable.Add("xml"), this._nameTable.Add("http://www.w3.org/XML/1998/namespace"), null, 0);
			this._doc.SetXmlNamespaceNode(this._pageNmsp, this._idxNmsp);
			if ((flags & XPathDocument.LoadFlags.Fragment) == XPathDocument.LoadFlags.None)
			{
				this._idxParent = this.NewNode(out this._pageParent, XPathNodeType.Root, string.Empty, string.Empty, string.Empty, baseUri);
				this._doc.SetRootNode(this._pageParent, this._idxParent);
				return;
			}
			this._doc.SetRootNode(this._nodePageFact.NextNodePage, this._nodePageFact.NextNodeIndex);
		}

		// Token: 0x06004256 RID: 16982 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x0016AC32 File Offset: 0x00168E32
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.WriteStartElement(prefix, localName, ns, string.Empty);
		}

		// Token: 0x06004258 RID: 16984 RVA: 0x0016AC44 File Offset: 0x00168E44
		public void WriteStartElement(string prefix, string localName, string ns, string baseUri)
		{
			if (this._atomizeNames)
			{
				prefix = this._nameTable.Add(prefix);
				localName = this._nameTable.Add(localName);
				ns = this._nameTable.Add(ns);
			}
			this.AddSibling(XPathNodeType.Element, localName, ns, prefix, baseUri);
			this._pageParent = this._pageSibling;
			this._idxParent = this._idxSibling;
			this._idxSibling = 0;
			int num = this._pageParent[this._idxParent].LocalNameHashCode & 63;
			this._elemNameIndex[num] = this.LinkSimilarElements(this._elemNameIndex[num].Page, this._elemNameIndex[num].Index, this._pageParent, this._idxParent);
			if (this._elemIdMap != null)
			{
				this._idAttrName = (XmlQualifiedName)this._elemIdMap[new XmlQualifiedName(localName, prefix)];
			}
		}

		// Token: 0x06004259 RID: 16985 RVA: 0x0016AD2D File Offset: 0x00168F2D
		public override void WriteEndElement()
		{
			this.WriteEndElement(true);
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x0016AD36 File Offset: 0x00168F36
		public override void WriteFullEndElement()
		{
			this.WriteEndElement(false);
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x0016AD2D File Offset: 0x00168F2D
		internal override void WriteEndElement(string prefix, string localName, string namespaceName)
		{
			this.WriteEndElement(true);
		}

		// Token: 0x0600425C RID: 16988 RVA: 0x0016AD36 File Offset: 0x00168F36
		internal override void WriteFullEndElement(string prefix, string localName, string namespaceName)
		{
			this.WriteEndElement(false);
		}

		// Token: 0x0600425D RID: 16989 RVA: 0x0016AD40 File Offset: 0x00168F40
		public void WriteEndElement(bool allowShortcutTag)
		{
			if (!this._pageParent[this._idxParent].HasContentChild)
			{
				TextBlockType textType = this._textBldr.TextType;
				if (textType == TextBlockType.Text)
				{
					if (this._lineInfo != null)
					{
						if (this._textBldr.LineNumber != this._pageParent[this._idxParent].LineNumber)
						{
							goto IL_CD;
						}
						int num = this._textBldr.LinePosition - this._pageParent[this._idxParent].LinePosition;
						if (num < 0 || num > 255)
						{
							goto IL_CD;
						}
						this._pageParent[this._idxParent].SetCollapsedLineInfoOffset(num);
					}
					this._pageParent[this._idxParent].SetCollapsedValue(this._textBldr.ReadText());
					goto IL_12D;
				}
				if (textType - TextBlockType.SignificantWhitespace > 1)
				{
					this._pageParent[this._idxParent].SetEmptyValue(allowShortcutTag);
					goto IL_12D;
				}
				IL_CD:
				this.CachedTextNode();
				this._pageParent[this._idxParent].SetValue(this._pageSibling[this._idxSibling].Value);
			}
			else if (this._textBldr.HasText)
			{
				this.CachedTextNode();
			}
			IL_12D:
			if (this._pageParent[this._idxParent].HasNamespaceDecls)
			{
				this._doc.AddNamespace(this._pageParent, this._idxParent, this._pageNmsp, this._idxNmsp);
				XPathNodeRef xpathNodeRef = this._stkNmsp.Pop();
				this._pageNmsp = xpathNodeRef.Page;
				this._idxNmsp = xpathNodeRef.Index;
			}
			this._pageSibling = this._pageParent;
			this._idxSibling = this._idxParent;
			this._idxParent = this._pageParent[this._idxParent].GetParent(out this._pageParent);
		}

		// Token: 0x0600425E RID: 16990 RVA: 0x0016AF18 File Offset: 0x00169118
		public override void WriteStartAttribute(string prefix, string localName, string namespaceName)
		{
			if (this._atomizeNames)
			{
				prefix = this._nameTable.Add(prefix);
				localName = this._nameTable.Add(localName);
				namespaceName = this._nameTable.Add(namespaceName);
			}
			this.AddSibling(XPathNodeType.Attribute, localName, namespaceName, prefix, string.Empty);
		}

		// Token: 0x0600425F RID: 16991 RVA: 0x0016AF68 File Offset: 0x00169168
		public override void WriteEndAttribute()
		{
			this._pageSibling[this._idxSibling].SetValue(this._textBldr.ReadText());
			if (this._idAttrName != null && this._pageSibling[this._idxSibling].LocalName == this._idAttrName.Name && this._pageSibling[this._idxSibling].Prefix == this._idAttrName.Namespace)
			{
				this._doc.AddIdElement(this._pageSibling[this._idxSibling].Value, this._pageParent, this._idxParent);
			}
		}

		// Token: 0x06004260 RID: 16992 RVA: 0x0016B021 File Offset: 0x00169221
		public override void WriteCData(string text)
		{
			this.WriteString(text, TextBlockType.Text);
		}

		// Token: 0x06004261 RID: 16993 RVA: 0x0016B02B File Offset: 0x0016922B
		public override void WriteComment(string text)
		{
			this.AddSibling(XPathNodeType.Comment, string.Empty, string.Empty, string.Empty, string.Empty);
			this._pageSibling[this._idxSibling].SetValue(text);
		}

		// Token: 0x06004262 RID: 16994 RVA: 0x0016B05F File Offset: 0x0016925F
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.WriteProcessingInstruction(name, text, string.Empty);
		}

		// Token: 0x06004263 RID: 16995 RVA: 0x0016B070 File Offset: 0x00169270
		public void WriteProcessingInstruction(string name, string text, string baseUri)
		{
			if (this._atomizeNames)
			{
				name = this._nameTable.Add(name);
			}
			this.AddSibling(XPathNodeType.ProcessingInstruction, name, string.Empty, string.Empty, baseUri);
			this._pageSibling[this._idxSibling].SetValue(text);
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x0016B0BD File Offset: 0x001692BD
		public override void WriteWhitespace(string ws)
		{
			this.WriteString(ws, TextBlockType.Whitespace);
		}

		// Token: 0x06004265 RID: 16997 RVA: 0x0016B021 File Offset: 0x00169221
		public override void WriteString(string text)
		{
			this.WriteString(text, TextBlockType.Text);
		}

		// Token: 0x06004266 RID: 16998 RVA: 0x0016B0C7 File Offset: 0x001692C7
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count), TextBlockType.Text);
		}

		// Token: 0x06004267 RID: 16999 RVA: 0x0016B021 File Offset: 0x00169221
		public override void WriteRaw(string data)
		{
			this.WriteString(data, TextBlockType.Text);
		}

		// Token: 0x06004268 RID: 17000 RVA: 0x0016B0C7 File Offset: 0x001692C7
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count), TextBlockType.Text);
		}

		// Token: 0x06004269 RID: 17001 RVA: 0x0016B0D8 File Offset: 0x001692D8
		public void WriteString(string text, TextBlockType textType)
		{
			this._textBldr.WriteTextBlock(text, textType);
		}

		// Token: 0x0600426A RID: 17002 RVA: 0x0000349C File Offset: 0x0000169C
		public override void WriteEntityRef(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600426B RID: 17003 RVA: 0x0016B0E7 File Offset: 0x001692E7
		public override void WriteCharEntity(char ch)
		{
			this.WriteString(new string(ch, 1), TextBlockType.Text);
		}

		// Token: 0x0600426C RID: 17004 RVA: 0x0016B0F8 File Offset: 0x001692F8
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			char[] value = new char[]
			{
				highChar,
				lowChar
			};
			this.WriteString(new string(value), TextBlockType.Text);
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x0016B124 File Offset: 0x00169324
		public override void Close()
		{
			if (this._textBldr.HasText)
			{
				this.CachedTextNode();
			}
			XPathNode[] array;
			if (this._doc.GetRootNode(out array) == this._nodePageFact.NextNodeIndex && array == this._nodePageFact.NextNodePage)
			{
				this.AddSibling(XPathNodeType.Text, string.Empty, string.Empty, string.Empty, string.Empty);
				this._pageSibling[this._idxSibling].SetValue(string.Empty);
			}
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x0000B528 File Offset: 0x00009728
		public override void Flush()
		{
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x06004271 RID: 17009 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void StartElementContent()
		{
		}

		// Token: 0x06004272 RID: 17010 RVA: 0x0016B1A4 File Offset: 0x001693A4
		internal override void WriteNamespaceDeclaration(string prefix, string namespaceName)
		{
			if (this._atomizeNames)
			{
				prefix = this._nameTable.Add(prefix);
			}
			namespaceName = this._nameTable.Add(namespaceName);
			XPathNode[] pageNmsp = this._pageNmsp;
			int num = this._idxNmsp;
			while (num != 0 && pageNmsp[num].LocalName != prefix)
			{
				num = pageNmsp[num].GetSibling(out pageNmsp);
			}
			XPathNode[] array;
			int num2 = this.NewNamespaceNode(out array, prefix, namespaceName, this._pageParent, this._idxParent);
			if (num != 0)
			{
				XPathNode[] pageNmsp2 = this._pageNmsp;
				int num3 = this._idxNmsp;
				XPathNode[] array2 = array;
				int num4 = num2;
				while (num3 != num || pageNmsp2 != pageNmsp)
				{
					XPathNode[] array3;
					int num5 = pageNmsp2[num3].GetParent(out array3);
					num5 = this.NewNamespaceNode(out array3, pageNmsp2[num3].LocalName, pageNmsp2[num3].Value, array3, num5);
					array2[num4].SetSibling(this._infoTable, array3, num5);
					array2 = array3;
					num4 = num5;
					num3 = pageNmsp2[num3].GetSibling(out pageNmsp2);
				}
				num = pageNmsp[num].GetSibling(out pageNmsp);
				if (num != 0)
				{
					array2[num4].SetSibling(this._infoTable, pageNmsp, num);
				}
			}
			else if (this._idxParent != 0)
			{
				array[num2].SetSibling(this._infoTable, this._pageNmsp, this._idxNmsp);
			}
			else
			{
				this._doc.SetRootNode(array, num2);
			}
			if (this._idxParent != 0)
			{
				if (!this._pageParent[this._idxParent].HasNamespaceDecls)
				{
					this._stkNmsp.Push(new XPathNodeRef(this._pageNmsp, this._idxNmsp));
					this._pageParent[this._idxParent].HasNamespaceDecls = true;
				}
				this._pageNmsp = array;
				this._idxNmsp = num2;
			}
		}

		// Token: 0x06004273 RID: 17011 RVA: 0x0016B37C File Offset: 0x0016957C
		public void CreateIdTables(IDtdInfo dtdInfo)
		{
			foreach (IDtdAttributeListInfo dtdAttributeListInfo in dtdInfo.GetAttributeLists())
			{
				IDtdAttributeInfo dtdAttributeInfo = dtdAttributeListInfo.LookupIdAttribute();
				if (dtdAttributeInfo != null)
				{
					if (this._elemIdMap == null)
					{
						this._elemIdMap = new Hashtable();
					}
					this._elemIdMap.Add(new XmlQualifiedName(dtdAttributeListInfo.LocalName, dtdAttributeListInfo.Prefix), new XmlQualifiedName(dtdAttributeInfo.LocalName, dtdAttributeInfo.Prefix));
				}
			}
		}

		// Token: 0x06004274 RID: 17012 RVA: 0x0016B40C File Offset: 0x0016960C
		private XPathNodeRef LinkSimilarElements(XPathNode[] pagePrev, int idxPrev, XPathNode[] pageNext, int idxNext)
		{
			if (pagePrev != null)
			{
				pagePrev[idxPrev].SetSimilarElement(this._infoTable, pageNext, idxNext);
			}
			return new XPathNodeRef(pageNext, idxNext);
		}

		// Token: 0x06004275 RID: 17013 RVA: 0x0016B430 File Offset: 0x00169630
		private int NewNamespaceNode(out XPathNode[] page, string prefix, string namespaceUri, XPathNode[] pageElem, int idxElem)
		{
			XPathNode[] array;
			int num;
			this._nmspPageFact.AllocateSlot(out array, out num);
			int lineNumOffset;
			int linePosOffset;
			this.ComputeLineInfo(false, out lineNumOffset, out linePosOffset);
			XPathNodeInfoAtom info = this._infoTable.Create(prefix, string.Empty, string.Empty, string.Empty, pageElem, array, null, this._doc, this._lineNumBase, this._linePosBase);
			array[num].Create(info, XPathNodeType.Namespace, idxElem);
			array[num].SetValue(namespaceUri);
			array[num].SetLineInfoOffsets(lineNumOffset, linePosOffset);
			page = array;
			return num;
		}

		// Token: 0x06004276 RID: 17014 RVA: 0x0016B4BC File Offset: 0x001696BC
		private int NewNode(out XPathNode[] page, XPathNodeType xptyp, string localName, string namespaceUri, string prefix, string baseUri)
		{
			XPathNode[] array;
			int num;
			this._nodePageFact.AllocateSlot(out array, out num);
			int lineNumOffset;
			int linePosOffset;
			this.ComputeLineInfo(XPathNavigator.IsText(xptyp), out lineNumOffset, out linePosOffset);
			XPathNodeInfoAtom info = this._infoTable.Create(localName, namespaceUri, prefix, baseUri, this._pageParent, array, array, this._doc, this._lineNumBase, this._linePosBase);
			array[num].Create(info, xptyp, this._idxParent);
			array[num].SetLineInfoOffsets(lineNumOffset, linePosOffset);
			page = array;
			return num;
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x0016B540 File Offset: 0x00169740
		private void ComputeLineInfo(bool isTextNode, out int lineNumOffset, out int linePosOffset)
		{
			if (this._lineInfo == null)
			{
				lineNumOffset = 0;
				linePosOffset = 0;
				return;
			}
			int lineNumber;
			int linePosition;
			if (isTextNode)
			{
				lineNumber = this._textBldr.LineNumber;
				linePosition = this._textBldr.LinePosition;
			}
			else
			{
				lineNumber = this._lineInfo.LineNumber;
				linePosition = this._lineInfo.LinePosition;
			}
			lineNumOffset = lineNumber - this._lineNumBase;
			if (lineNumOffset < 0 || lineNumOffset > 16383)
			{
				this._lineNumBase = lineNumber;
				lineNumOffset = 0;
			}
			linePosOffset = linePosition - this._linePosBase;
			if (linePosOffset < 0 || linePosOffset > 65535)
			{
				this._linePosBase = linePosition;
				linePosOffset = 0;
			}
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x0016B5D8 File Offset: 0x001697D8
		private void AddSibling(XPathNodeType xptyp, string localName, string namespaceUri, string prefix, string baseUri)
		{
			if (this._textBldr.HasText)
			{
				this.CachedTextNode();
			}
			XPathNode[] pageSibling;
			int idxSibling = this.NewNode(out pageSibling, xptyp, localName, namespaceUri, prefix, baseUri);
			if (this._idxParent != 0)
			{
				this._pageParent[this._idxParent].SetParentProperties(xptyp);
				if (this._idxSibling != 0)
				{
					this._pageSibling[this._idxSibling].SetSibling(this._infoTable, pageSibling, idxSibling);
				}
			}
			this._pageSibling = pageSibling;
			this._idxSibling = idxSibling;
		}

		// Token: 0x06004279 RID: 17017 RVA: 0x0016B65C File Offset: 0x0016985C
		private void CachedTextNode()
		{
			TextBlockType textType = this._textBldr.TextType;
			string value = this._textBldr.ReadText();
			this.AddSibling((XPathNodeType)textType, string.Empty, string.Empty, string.Empty, string.Empty);
			this._pageSibling[this._idxSibling].SetValue(value);
		}

		// Token: 0x04002EF6 RID: 12022
		private XPathDocumentBuilder.NodePageFactory _nodePageFact;

		// Token: 0x04002EF7 RID: 12023
		private XPathDocumentBuilder.NodePageFactory _nmspPageFact;

		// Token: 0x04002EF8 RID: 12024
		private XPathDocumentBuilder.TextBlockBuilder _textBldr;

		// Token: 0x04002EF9 RID: 12025
		private Stack<XPathNodeRef> _stkNmsp;

		// Token: 0x04002EFA RID: 12026
		private XPathNodeInfoTable _infoTable;

		// Token: 0x04002EFB RID: 12027
		private XPathDocument _doc;

		// Token: 0x04002EFC RID: 12028
		private IXmlLineInfo _lineInfo;

		// Token: 0x04002EFD RID: 12029
		private XmlNameTable _nameTable;

		// Token: 0x04002EFE RID: 12030
		private bool _atomizeNames;

		// Token: 0x04002EFF RID: 12031
		private XPathNode[] _pageNmsp;

		// Token: 0x04002F00 RID: 12032
		private int _idxNmsp;

		// Token: 0x04002F01 RID: 12033
		private XPathNode[] _pageParent;

		// Token: 0x04002F02 RID: 12034
		private int _idxParent;

		// Token: 0x04002F03 RID: 12035
		private XPathNode[] _pageSibling;

		// Token: 0x04002F04 RID: 12036
		private int _idxSibling;

		// Token: 0x04002F05 RID: 12037
		private int _lineNumBase;

		// Token: 0x04002F06 RID: 12038
		private int _linePosBase;

		// Token: 0x04002F07 RID: 12039
		private XmlQualifiedName _idAttrName;

		// Token: 0x04002F08 RID: 12040
		private Hashtable _elemIdMap;

		// Token: 0x04002F09 RID: 12041
		private XPathNodeRef[] _elemNameIndex;

		// Token: 0x04002F0A RID: 12042
		private const int ElementIndexSize = 64;

		// Token: 0x02000668 RID: 1640
		private struct NodePageFactory
		{
			// Token: 0x0600427A RID: 17018 RVA: 0x0016B6B3 File Offset: 0x001698B3
			public void Init(int initialPageSize)
			{
				this._pageSize = initialPageSize;
				this._page = new XPathNode[this._pageSize];
				this._pageInfo = new XPathNodePageInfo(null, 1);
				this._page[0].Create(this._pageInfo);
			}

			// Token: 0x17000CAD RID: 3245
			// (get) Token: 0x0600427B RID: 17019 RVA: 0x0016B6F1 File Offset: 0x001698F1
			public XPathNode[] NextNodePage
			{
				get
				{
					return this._page;
				}
			}

			// Token: 0x17000CAE RID: 3246
			// (get) Token: 0x0600427C RID: 17020 RVA: 0x0016B6F9 File Offset: 0x001698F9
			public int NextNodeIndex
			{
				get
				{
					return this._pageInfo.NodeCount;
				}
			}

			// Token: 0x0600427D RID: 17021 RVA: 0x0016B708 File Offset: 0x00169908
			public void AllocateSlot(out XPathNode[] page, out int idx)
			{
				page = this._page;
				idx = this._pageInfo.NodeCount;
				XPathNodePageInfo pageInfo = this._pageInfo;
				int num = pageInfo.NodeCount + 1;
				pageInfo.NodeCount = num;
				if (num >= this._page.Length)
				{
					if (this._pageSize < 65536)
					{
						this._pageSize *= 2;
					}
					this._page = new XPathNode[this._pageSize];
					this._pageInfo.NextPage = this._page;
					this._pageInfo = new XPathNodePageInfo(page, this._pageInfo.PageNumber + 1);
					this._page[0].Create(this._pageInfo);
				}
			}

			// Token: 0x04002F0B RID: 12043
			private XPathNode[] _page;

			// Token: 0x04002F0C RID: 12044
			private XPathNodePageInfo _pageInfo;

			// Token: 0x04002F0D RID: 12045
			private int _pageSize;
		}

		// Token: 0x02000669 RID: 1641
		private struct TextBlockBuilder
		{
			// Token: 0x0600427E RID: 17022 RVA: 0x0016B7B8 File Offset: 0x001699B8
			public void Initialize(IXmlLineInfo lineInfo)
			{
				this._lineInfo = lineInfo;
				this._textType = TextBlockType.None;
			}

			// Token: 0x17000CAF RID: 3247
			// (get) Token: 0x0600427F RID: 17023 RVA: 0x0016B7C8 File Offset: 0x001699C8
			public TextBlockType TextType
			{
				get
				{
					return this._textType;
				}
			}

			// Token: 0x17000CB0 RID: 3248
			// (get) Token: 0x06004280 RID: 17024 RVA: 0x0016B7D0 File Offset: 0x001699D0
			public bool HasText
			{
				get
				{
					return this._textType > TextBlockType.None;
				}
			}

			// Token: 0x17000CB1 RID: 3249
			// (get) Token: 0x06004281 RID: 17025 RVA: 0x0016B7DB File Offset: 0x001699DB
			public int LineNumber
			{
				get
				{
					return this._lineNum;
				}
			}

			// Token: 0x17000CB2 RID: 3250
			// (get) Token: 0x06004282 RID: 17026 RVA: 0x0016B7E3 File Offset: 0x001699E3
			public int LinePosition
			{
				get
				{
					return this._linePos;
				}
			}

			// Token: 0x06004283 RID: 17027 RVA: 0x0016B7EC File Offset: 0x001699EC
			public void WriteTextBlock(string text, TextBlockType textType)
			{
				if (text.Length != 0)
				{
					if (this._textType == TextBlockType.None)
					{
						this._text = text;
						this._textType = textType;
						if (this._lineInfo != null)
						{
							this._lineNum = this._lineInfo.LineNumber;
							this._linePos = this._lineInfo.LinePosition;
							return;
						}
					}
					else
					{
						this._text += text;
						if (textType < this._textType)
						{
							this._textType = textType;
						}
					}
				}
			}

			// Token: 0x06004284 RID: 17028 RVA: 0x0016B864 File Offset: 0x00169A64
			public string ReadText()
			{
				if (this._textType == TextBlockType.None)
				{
					return string.Empty;
				}
				this._textType = TextBlockType.None;
				return this._text;
			}

			// Token: 0x04002F0E RID: 12046
			private IXmlLineInfo _lineInfo;

			// Token: 0x04002F0F RID: 12047
			private TextBlockType _textType;

			// Token: 0x04002F10 RID: 12048
			private string _text;

			// Token: 0x04002F11 RID: 12049
			private int _lineNum;

			// Token: 0x04002F12 RID: 12050
			private int _linePos;
		}
	}
}
