using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x0200007C RID: 124
	internal sealed class XPathNodePointer : IXmlDataVirtualNode
	{
		// Token: 0x06000540 RID: 1344 RVA: 0x00012620 File Offset: 0x00010820
		static XPathNodePointer()
		{
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[0] = -1;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[1] = 1;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[2] = 2;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[3] = 4;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[4] = 4;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[5] = -1;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[6] = -1;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[7] = 7;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[8] = 8;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[9] = 0;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[10] = -1;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[11] = 0;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[12] = -1;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[13] = 6;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[14] = 5;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[15] = -1;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[16] = -1;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[17] = -1;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x000126D4 File Offset: 0x000108D4
		private XPathNodeType DecideXPNodeTypeForTextNodes(XmlNode node)
		{
			XPathNodeType result = XPathNodeType.Whitespace;
			while (node != null)
			{
				XmlNodeType nodeType = node.NodeType;
				if (nodeType - XmlNodeType.Text <= 1)
				{
					return XPathNodeType.Text;
				}
				if (nodeType != XmlNodeType.Whitespace)
				{
					if (nodeType != XmlNodeType.SignificantWhitespace)
					{
						return result;
					}
					result = XPathNodeType.SignificantWhitespace;
				}
				node = this._doc.SafeNextSibling(node);
			}
			return result;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00012718 File Offset: 0x00010918
		private XPathNodeType ConvertNodeType(XmlNode node)
		{
			if (XmlDataDocument.IsTextNode(node.NodeType))
			{
				return this.DecideXPNodeTypeForTextNodes(node);
			}
			int num = XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[(int)node.NodeType];
			if (num != 2)
			{
				return (XPathNodeType)num;
			}
			if (node.NamespaceURI == "http://www.w3.org/2000/xmlns/")
			{
				return XPathNodeType.Namespace;
			}
			return XPathNodeType.Attribute;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00012764 File Offset: 0x00010964
		private bool IsNamespaceNode(XmlNodeType nt, string ns)
		{
			return nt == XmlNodeType.Attribute && ns == "http://www.w3.org/2000/xmlns/";
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00012777 File Offset: 0x00010977
		internal XPathNodePointer(DataDocumentXPathNavigator owner, XmlDataDocument doc, XmlNode node) : this(owner, doc, node, null, false, null)
		{
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00012785 File Offset: 0x00010985
		internal XPathNodePointer(DataDocumentXPathNavigator owner, XPathNodePointer pointer) : this(owner, pointer._doc, pointer._node, pointer._column, pointer._fOnValue, pointer._parentOfNS)
		{
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000127AC File Offset: 0x000109AC
		private XPathNodePointer(DataDocumentXPathNavigator owner, XmlDataDocument doc, XmlNode node, DataColumn c, bool bOnValue, XmlBoundElement parentOfNS)
		{
			this._owner = new WeakReference(owner);
			this._doc = doc;
			this._node = node;
			this._column = c;
			this._fOnValue = bOnValue;
			this._parentOfNS = parentOfNS;
			this._doc.AddPointer(this);
			this._bNeedFoliate = false;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00012804 File Offset: 0x00010A04
		internal XPathNodePointer Clone(DataDocumentXPathNavigator owner)
		{
			this.RealFoliate();
			return new XPathNodePointer(owner, this);
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00012813 File Offset: 0x00010A13
		internal bool IsEmptyElement
		{
			get
			{
				return this._node != null && this._column == null && this._node.NodeType == XmlNodeType.Element && ((XmlElement)this._node).IsEmpty;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00012848 File Offset: 0x00010A48
		internal XPathNodeType NodeType
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return XPathNodeType.All;
				}
				if (this._column == null)
				{
					return this.ConvertNodeType(this._node);
				}
				if (this._fOnValue)
				{
					return XPathNodeType.Text;
				}
				if (this._column.ColumnMapping != MappingType.Attribute)
				{
					return XPathNodeType.Element;
				}
				if (this._column.Namespace == "http://www.w3.org/2000/xmlns/")
				{
					return XPathNodeType.Namespace;
				}
				return XPathNodeType.Attribute;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x000128B0 File Offset: 0x00010AB0
		internal string LocalName
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return string.Empty;
				}
				if (this._column == null)
				{
					XmlNodeType nodeType = this._node.NodeType;
					if (this.IsNamespaceNode(nodeType, this._node.NamespaceURI) && this._node.LocalName == "xmlns")
					{
						return string.Empty;
					}
					if (nodeType == XmlNodeType.Element || nodeType == XmlNodeType.Attribute || nodeType == XmlNodeType.ProcessingInstruction)
					{
						return this._node.LocalName;
					}
					return string.Empty;
				}
				else
				{
					if (this._fOnValue)
					{
						return string.Empty;
					}
					return this._doc.NameTable.Add(this._column.EncodedColumnName);
				}
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00012960 File Offset: 0x00010B60
		internal string Name
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return string.Empty;
				}
				if (this._column == null)
				{
					XmlNodeType nodeType = this._node.NodeType;
					if (this.IsNamespaceNode(nodeType, this._node.NamespaceURI))
					{
						if (this._node.LocalName == "xmlns")
						{
							return string.Empty;
						}
						return this._node.LocalName;
					}
					else
					{
						if (nodeType == XmlNodeType.Element || nodeType == XmlNodeType.Attribute || nodeType == XmlNodeType.ProcessingInstruction)
						{
							return this._node.Name;
						}
						return string.Empty;
					}
				}
				else
				{
					if (this._fOnValue)
					{
						return string.Empty;
					}
					return this._doc.NameTable.Add(this._column.EncodedColumnName);
				}
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00012A1C File Offset: 0x00010C1C
		internal string NamespaceURI
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return string.Empty;
				}
				if (this._column == null)
				{
					XPathNodeType xpathNodeType = this.ConvertNodeType(this._node);
					if (xpathNodeType == XPathNodeType.Element || xpathNodeType == XPathNodeType.Root || xpathNodeType == XPathNodeType.Attribute)
					{
						return this._node.NamespaceURI;
					}
					return string.Empty;
				}
				else
				{
					if (this._fOnValue)
					{
						return string.Empty;
					}
					if (this._column.Namespace == "http://www.w3.org/2000/xmlns/")
					{
						return string.Empty;
					}
					return this._doc.NameTable.Add(this._column.Namespace);
				}
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00012AB8 File Offset: 0x00010CB8
		internal string Prefix
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return string.Empty;
				}
				if (this._column != null)
				{
					return string.Empty;
				}
				if (this.IsNamespaceNode(this._node.NodeType, this._node.NamespaceURI))
				{
					return string.Empty;
				}
				return this._node.Prefix;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00012B18 File Offset: 0x00010D18
		internal string Value
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return null;
				}
				if (this._column == null)
				{
					string text = this._node.Value;
					if (XmlDataDocument.IsTextNode(this._node.NodeType))
					{
						if (this._node.ParentNode == null)
						{
							return text;
						}
						XmlNode xmlNode = this._doc.SafeNextSibling(this._node);
						while (xmlNode != null && XmlDataDocument.IsTextNode(xmlNode.NodeType))
						{
							text += xmlNode.Value;
							xmlNode = this._doc.SafeNextSibling(xmlNode);
						}
					}
					return text;
				}
				if (this._column.ColumnMapping != MappingType.Attribute && !this._fOnValue)
				{
					return null;
				}
				DataRow row = this.Row;
				DataRowVersion version = (row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current;
				object value = row[this._column, version];
				if (!Convert.IsDBNull(value))
				{
					return this._column.ConvertObjectToXml(value);
				}
				return null;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00012C04 File Offset: 0x00010E04
		internal string InnerText
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return string.Empty;
				}
				if (this._column == null)
				{
					if (this._node.NodeType != XmlNodeType.Document)
					{
						return this._node.InnerText;
					}
					XmlElement documentElement = ((XmlDocument)this._node).DocumentElement;
					if (documentElement != null)
					{
						return documentElement.InnerText;
					}
					return string.Empty;
				}
				else
				{
					DataRow row = this.Row;
					DataRowVersion version = (row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current;
					object value = row[this._column, version];
					if (!Convert.IsDBNull(value))
					{
						return this._column.ConvertObjectToXml(value);
					}
					return string.Empty;
				}
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00012CAC File Offset: 0x00010EAC
		internal string BaseURI
		{
			get
			{
				this.RealFoliate();
				if (this._node != null)
				{
					return this._node.BaseURI;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00012CD0 File Offset: 0x00010ED0
		internal string XmlLang
		{
			get
			{
				this.RealFoliate();
				XmlNode xmlNode = this._node;
				while (xmlNode != null)
				{
					XmlBoundElement xmlBoundElement = xmlNode as XmlBoundElement;
					if (xmlBoundElement != null)
					{
						if (xmlBoundElement.ElementState == ElementState.Defoliated)
						{
							DataRow row = xmlBoundElement.Row;
							using (IEnumerator enumerator = row.Table.Columns.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									DataColumn dataColumn = (DataColumn)obj;
									if (dataColumn.Prefix == "xml" && dataColumn.EncodedColumnName == "lang")
									{
										object obj2 = row[dataColumn];
										if (obj2 == DBNull.Value)
										{
											break;
										}
										return (string)obj2;
									}
								}
								goto IL_D4;
							}
						}
						if (xmlBoundElement.HasAttribute("xml:lang"))
						{
							return xmlBoundElement.GetAttribute("xml:lang");
						}
					}
					IL_D4:
					if (xmlNode.NodeType == XmlNodeType.Attribute)
					{
						xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					}
					else
					{
						xmlNode = xmlNode.ParentNode;
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00012DF0 File Offset: 0x00010FF0
		private XmlBoundElement GetRowElement()
		{
			XmlBoundElement result;
			if (this._column != null)
			{
				result = (this._node as XmlBoundElement);
				return result;
			}
			this._doc.Mapper.GetRegion(this._node, out result);
			return result;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00012E30 File Offset: 0x00011030
		private DataRow Row
		{
			get
			{
				XmlBoundElement rowElement = this.GetRowElement();
				if (rowElement == null)
				{
					return null;
				}
				return rowElement.Row;
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00012E50 File Offset: 0x00011050
		internal bool MoveTo(XPathNodePointer pointer)
		{
			if (this._doc != pointer._doc)
			{
				return false;
			}
			this._node = pointer._node;
			this._column = pointer._column;
			this._fOnValue = pointer._fOnValue;
			this._bNeedFoliate = pointer._bNeedFoliate;
			return true;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00012E9E File Offset: 0x0001109E
		private void MoveTo(XmlNode node)
		{
			this._node = node;
			this._column = null;
			this._fOnValue = false;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00012EB5 File Offset: 0x000110B5
		private void MoveTo(XmlNode node, DataColumn column, bool fOnValue)
		{
			this._node = node;
			this._column = column;
			this._fOnValue = fOnValue;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00012ECC File Offset: 0x000110CC
		private bool IsFoliated(XmlNode node)
		{
			return node == null || !(node is XmlBoundElement) || ((XmlBoundElement)node).IsFoliated;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00012EE8 File Offset: 0x000110E8
		private int ColumnCount(DataRow row, bool fAttribute)
		{
			DataColumn dataColumn = null;
			int num = 0;
			while ((dataColumn = this.NextColumn(row, dataColumn, fAttribute)) != null)
			{
				if (dataColumn.Namespace != "http://www.w3.org/2000/xmlns/")
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00012F20 File Offset: 0x00011120
		internal int AttributeCount
		{
			get
			{
				this.RealFoliate();
				if (this._node == null || this._column != null || this._node.NodeType != XmlNodeType.Element)
				{
					return 0;
				}
				if (!this.IsFoliated(this._node))
				{
					return this.ColumnCount(this.Row, true);
				}
				int num = 0;
				using (IEnumerator enumerator = this._node.Attributes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((XmlAttribute)enumerator.Current).NamespaceURI != "http://www.w3.org/2000/xmlns/")
						{
							num++;
						}
					}
				}
				return num;
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00012FD4 File Offset: 0x000111D4
		internal DataColumn NextColumn(DataRow row, DataColumn col, bool fAttribute)
		{
			if (row.RowState == DataRowState.Deleted)
			{
				return null;
			}
			DataColumnCollection columns = row.Table.Columns;
			int i = (col != null) ? (col.Ordinal + 1) : 0;
			int count = columns.Count;
			DataRowVersion version = (row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current;
			while (i < count)
			{
				DataColumn dataColumn = columns[i];
				if (!this._doc.IsNotMapped(dataColumn) && dataColumn.ColumnMapping == MappingType.Attribute == fAttribute && !Convert.IsDBNull(row[dataColumn, version]))
				{
					return dataColumn;
				}
				i++;
			}
			return null;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00013068 File Offset: 0x00011268
		internal DataColumn PreviousColumn(DataRow row, DataColumn col, bool fAttribute)
		{
			if (row.RowState == DataRowState.Deleted)
			{
				return null;
			}
			DataColumnCollection columns = row.Table.Columns;
			int i = (col != null) ? (col.Ordinal - 1) : (columns.Count - 1);
			int count = columns.Count;
			DataRowVersion version = (row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current;
			while (i >= 0)
			{
				DataColumn dataColumn = columns[i];
				if (!this._doc.IsNotMapped(dataColumn) && dataColumn.ColumnMapping == MappingType.Attribute == fAttribute && !Convert.IsDBNull(row[dataColumn, version]))
				{
					return dataColumn;
				}
				i--;
			}
			return null;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00013100 File Offset: 0x00011300
		internal bool MoveToAttribute(string localName, string namespaceURI)
		{
			this.RealFoliate();
			if (namespaceURI == "http://www.w3.org/2000/xmlns/")
			{
				return false;
			}
			if (this._node != null && (this._column == null || this._column.ColumnMapping == MappingType.Attribute) && this._node.NodeType == XmlNodeType.Element)
			{
				if (!this.IsFoliated(this._node))
				{
					DataColumn dataColumn = null;
					while ((dataColumn = this.NextColumn(this.Row, dataColumn, true)) != null)
					{
						if (dataColumn.EncodedColumnName == localName && dataColumn.Namespace == namespaceURI)
						{
							this.MoveTo(this._node, dataColumn, false);
							return true;
						}
					}
				}
				else
				{
					XmlNode namedItem = this._node.Attributes.GetNamedItem(localName, namespaceURI);
					if (namedItem != null)
					{
						this.MoveTo(namedItem, null, false);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000131C8 File Offset: 0x000113C8
		internal bool MoveToNextAttribute(bool bFirst)
		{
			this.RealFoliate();
			if (this._node != null)
			{
				if (bFirst && (this._column != null || this._node.NodeType != XmlNodeType.Element))
				{
					return false;
				}
				if (!bFirst)
				{
					if (this._column != null && this._column.ColumnMapping != MappingType.Attribute)
					{
						return false;
					}
					if (this._column == null && this._node.NodeType != XmlNodeType.Attribute)
					{
						return false;
					}
				}
				if (!this.IsFoliated(this._node))
				{
					DataColumn dataColumn = this._column;
					while ((dataColumn = this.NextColumn(this.Row, dataColumn, true)) != null)
					{
						if (dataColumn.Namespace != "http://www.w3.org/2000/xmlns/")
						{
							this.MoveTo(this._node, dataColumn, false);
							return true;
						}
					}
					return false;
				}
				if (bFirst)
				{
					using (IEnumerator enumerator = this._node.Attributes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							XmlAttribute xmlAttribute = (XmlAttribute)obj;
							if (xmlAttribute.NamespaceURI != "http://www.w3.org/2000/xmlns/")
							{
								this.MoveTo(xmlAttribute, null, false);
								return true;
							}
						}
						return false;
					}
				}
				XmlNamedNodeMap attributes = ((XmlAttribute)this._node).OwnerElement.Attributes;
				bool flag = false;
				foreach (object obj2 in attributes)
				{
					XmlAttribute xmlAttribute2 = (XmlAttribute)obj2;
					if (flag && xmlAttribute2.NamespaceURI != "http://www.w3.org/2000/xmlns/")
					{
						this.MoveTo(xmlAttribute2, null, false);
						return true;
					}
					if (xmlAttribute2 == this._node)
					{
						flag = true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00013384 File Offset: 0x00011584
		private bool IsValidChild(XmlNode parent, XmlNode child)
		{
			int num = XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[(int)child.NodeType];
			if (num == -1)
			{
				return false;
			}
			int num2 = XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[(int)parent.NodeType];
			if (num2 != 0)
			{
				return num2 == 1 && (num == 1 || num == 4 || num == 8 || num == 6 || num == 5 || num == 7);
			}
			return num == 1 || num == 8 || num == 7;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000133E8 File Offset: 0x000115E8
		private bool IsValidChild(XmlNode parent, DataColumn c)
		{
			int num = XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[(int)parent.NodeType];
			if (num != 0)
			{
				return num == 1 && (c.ColumnMapping == MappingType.Element || c.ColumnMapping == MappingType.SimpleContent);
			}
			return c.ColumnMapping == MappingType.Element;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001342C File Offset: 0x0001162C
		internal bool MoveToNextSibling()
		{
			this.RealFoliate();
			if (this._node != null)
			{
				if (this._column != null)
				{
					if (this._fOnValue)
					{
						return false;
					}
					DataRow row = this.Row;
					for (DataColumn dataColumn = this.NextColumn(row, this._column, false); dataColumn != null; dataColumn = this.NextColumn(row, dataColumn, false))
					{
						if (this.IsValidChild(this._node, dataColumn))
						{
							this.MoveTo(this._node, dataColumn, this._doc.IsTextOnly(dataColumn));
							return true;
						}
					}
					XmlNode xmlNode = this._doc.SafeFirstChild(this._node);
					if (xmlNode != null)
					{
						this.MoveTo(xmlNode);
						return true;
					}
				}
				else
				{
					XmlNode xmlNode2 = this._node;
					XmlNode parentNode = this._node.ParentNode;
					if (parentNode == null)
					{
						return false;
					}
					bool flag = XmlDataDocument.IsTextNode(this._node.NodeType);
					do
					{
						xmlNode2 = this._doc.SafeNextSibling(xmlNode2);
					}
					while ((xmlNode2 != null && flag && XmlDataDocument.IsTextNode(xmlNode2.NodeType)) || (xmlNode2 != null && !this.IsValidChild(parentNode, xmlNode2)));
					if (xmlNode2 != null)
					{
						this.MoveTo(xmlNode2);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00013534 File Offset: 0x00011734
		internal bool MoveToPreviousSibling()
		{
			this.RealFoliate();
			if (this._node != null)
			{
				if (this._column != null)
				{
					if (this._fOnValue)
					{
						return false;
					}
					DataRow row = this.Row;
					for (DataColumn dataColumn = this.PreviousColumn(row, this._column, false); dataColumn != null; dataColumn = this.PreviousColumn(row, dataColumn, false))
					{
						if (this.IsValidChild(this._node, dataColumn))
						{
							this.MoveTo(this._node, dataColumn, this._doc.IsTextOnly(dataColumn));
							return true;
						}
					}
				}
				else
				{
					XmlNode xmlNode = this._node;
					XmlNode parentNode = this._node.ParentNode;
					if (parentNode == null)
					{
						return false;
					}
					bool flag = XmlDataDocument.IsTextNode(this._node.NodeType);
					do
					{
						xmlNode = this._doc.SafePreviousSibling(xmlNode);
					}
					while ((xmlNode != null && flag && XmlDataDocument.IsTextNode(xmlNode.NodeType)) || (xmlNode != null && !this.IsValidChild(parentNode, xmlNode)));
					if (xmlNode != null)
					{
						this.MoveTo(xmlNode);
						return true;
					}
					if (!this.IsFoliated(parentNode) && parentNode is XmlBoundElement)
					{
						DataRow row2 = ((XmlBoundElement)parentNode).Row;
						if (row2 != null)
						{
							DataColumn dataColumn2 = this.PreviousColumn(row2, null, false);
							if (dataColumn2 != null)
							{
								this.MoveTo(parentNode, dataColumn2, this._doc.IsTextOnly(dataColumn2));
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001366C File Offset: 0x0001186C
		internal bool MoveToFirst()
		{
			this.RealFoliate();
			if (this._node != null)
			{
				DataRow dataRow = null;
				XmlNode xmlNode;
				if (this._column != null)
				{
					dataRow = this.Row;
					xmlNode = this._node;
				}
				else
				{
					xmlNode = this._node.ParentNode;
					if (xmlNode == null)
					{
						return false;
					}
					if (!this.IsFoliated(xmlNode) && xmlNode is XmlBoundElement)
					{
						dataRow = ((XmlBoundElement)xmlNode).Row;
					}
				}
				if (dataRow != null)
				{
					for (DataColumn dataColumn = this.NextColumn(dataRow, null, false); dataColumn != null; dataColumn = this.NextColumn(dataRow, dataColumn, false))
					{
						if (this.IsValidChild(this._node, dataColumn))
						{
							this.MoveTo(this._node, dataColumn, this._doc.IsTextOnly(dataColumn));
							return true;
						}
					}
				}
				for (XmlNode xmlNode2 = this._doc.SafeFirstChild(xmlNode); xmlNode2 != null; xmlNode2 = this._doc.SafeNextSibling(xmlNode2))
				{
					if (this.IsValidChild(xmlNode, xmlNode2))
					{
						this.MoveTo(xmlNode2);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00013750 File Offset: 0x00011950
		internal bool HasChildren
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return false;
				}
				if (this._column != null)
				{
					return this._column.ColumnMapping != MappingType.Attribute && this._column.ColumnMapping != MappingType.Hidden && !this._fOnValue;
				}
				if (!this.IsFoliated(this._node))
				{
					DataRow row = this.Row;
					for (DataColumn dataColumn = this.NextColumn(row, null, false); dataColumn != null; dataColumn = this.NextColumn(row, dataColumn, false))
					{
						if (this.IsValidChild(this._node, dataColumn))
						{
							return true;
						}
					}
				}
				for (XmlNode xmlNode = this._doc.SafeFirstChild(this._node); xmlNode != null; xmlNode = this._doc.SafeNextSibling(xmlNode))
				{
					if (this.IsValidChild(this._node, xmlNode))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00013814 File Offset: 0x00011A14
		internal bool MoveToFirstChild()
		{
			this.RealFoliate();
			if (this._node == null)
			{
				return false;
			}
			if (this._column == null)
			{
				if (!this.IsFoliated(this._node))
				{
					DataRow row = this.Row;
					for (DataColumn dataColumn = this.NextColumn(row, null, false); dataColumn != null; dataColumn = this.NextColumn(row, dataColumn, false))
					{
						if (this.IsValidChild(this._node, dataColumn))
						{
							this.MoveTo(this._node, dataColumn, this._doc.IsTextOnly(dataColumn));
							return true;
						}
					}
				}
				for (XmlNode xmlNode = this._doc.SafeFirstChild(this._node); xmlNode != null; xmlNode = this._doc.SafeNextSibling(xmlNode))
				{
					if (this.IsValidChild(this._node, xmlNode))
					{
						this.MoveTo(xmlNode);
						return true;
					}
				}
				return false;
			}
			if (this._column.ColumnMapping == MappingType.Attribute || this._column.ColumnMapping == MappingType.Hidden)
			{
				return false;
			}
			if (this._fOnValue)
			{
				return false;
			}
			this._fOnValue = true;
			return true;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00013900 File Offset: 0x00011B00
		internal bool MoveToParent()
		{
			this.RealFoliate();
			if (this.NodeType == XPathNodeType.Namespace)
			{
				this.MoveTo(this._parentOfNS);
				return true;
			}
			if (this._node != null)
			{
				if (this._column != null)
				{
					if (this._fOnValue && !this._doc.IsTextOnly(this._column))
					{
						this.MoveTo(this._node, this._column, false);
						return true;
					}
					this.MoveTo(this._node, null, false);
					return true;
				}
				else
				{
					XmlNode xmlNode;
					if (this._node.NodeType == XmlNodeType.Attribute)
					{
						xmlNode = ((XmlAttribute)this._node).OwnerElement;
					}
					else
					{
						xmlNode = this._node.ParentNode;
					}
					if (xmlNode != null)
					{
						this.MoveTo(xmlNode);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000139BC File Offset: 0x00011BBC
		private XmlNode GetParent(XmlNode node)
		{
			XPathNodeType xpathNodeType = this.ConvertNodeType(node);
			if (xpathNodeType == XPathNodeType.Namespace)
			{
				return this._parentOfNS;
			}
			if (xpathNodeType == XPathNodeType.Attribute)
			{
				return ((XmlAttribute)node).OwnerElement;
			}
			return node.ParentNode;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000139F4 File Offset: 0x00011BF4
		internal void MoveToRoot()
		{
			XmlNode node = this._node;
			for (XmlNode xmlNode = this._node; xmlNode != null; xmlNode = this.GetParent(xmlNode))
			{
				node = xmlNode;
			}
			this._node = node;
			this._column = null;
			this._fOnValue = false;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00013A34 File Offset: 0x00011C34
		internal bool IsSamePosition(XPathNodePointer pointer)
		{
			this.RealFoliate();
			pointer.RealFoliate();
			if (this._column == null && pointer._column == null)
			{
				return pointer._node == this._node && pointer._parentOfNS == this._parentOfNS;
			}
			return pointer._doc == this._doc && pointer._node == this._node && pointer._column == this._column && pointer._fOnValue == this._fOnValue && pointer._parentOfNS == this._parentOfNS;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00013AC4 File Offset: 0x00011CC4
		private XmlNodeOrder CompareNamespacePosition(XPathNodePointer other)
		{
			XPathNodePointer xpathNodePointer = this.Clone((DataDocumentXPathNavigator)this._owner.Target);
			other.Clone((DataDocumentXPathNavigator)other._owner.Target);
			while (xpathNodePointer.MoveToNextNamespace(XPathNamespaceScope.All))
			{
				if (xpathNodePointer.IsSamePosition(other))
				{
					return XmlNodeOrder.Before;
				}
			}
			return XmlNodeOrder.After;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00013B18 File Offset: 0x00011D18
		private static XmlNode GetRoot(XmlNode node, ref int depth)
		{
			depth = 0;
			XmlNode xmlNode = node;
			XmlNode xmlNode2 = (xmlNode.NodeType == XmlNodeType.Attribute) ? ((XmlAttribute)xmlNode).OwnerElement : xmlNode.ParentNode;
			while (xmlNode2 != null)
			{
				xmlNode = xmlNode2;
				xmlNode2 = xmlNode.ParentNode;
				depth++;
			}
			return xmlNode;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00013B5C File Offset: 0x00011D5C
		internal XmlNodeOrder ComparePosition(XPathNodePointer other)
		{
			this.RealFoliate();
			other.RealFoliate();
			if (this.IsSamePosition(other))
			{
				return XmlNodeOrder.Same;
			}
			XmlNode xmlNode;
			XmlNode xmlNode2;
			if (this.NodeType == XPathNodeType.Namespace && other.NodeType == XPathNodeType.Namespace)
			{
				if (this._parentOfNS == other._parentOfNS)
				{
					return this.CompareNamespacePosition(other);
				}
				xmlNode = this._parentOfNS;
				xmlNode2 = other._parentOfNS;
			}
			else if (this.NodeType == XPathNodeType.Namespace)
			{
				if (this._parentOfNS == other._node)
				{
					if (other._column == null)
					{
						return XmlNodeOrder.After;
					}
					return XmlNodeOrder.Before;
				}
				else
				{
					xmlNode = this._parentOfNS;
					xmlNode2 = other._node;
				}
			}
			else if (other.NodeType == XPathNodeType.Namespace)
			{
				if (this._node == other._parentOfNS)
				{
					if (this._column == null)
					{
						return XmlNodeOrder.Before;
					}
					return XmlNodeOrder.After;
				}
				else
				{
					xmlNode = this._node;
					xmlNode2 = other._parentOfNS;
				}
			}
			else if (this._node == other._node)
			{
				if (this._column == other._column)
				{
					if (this._fOnValue)
					{
						return XmlNodeOrder.After;
					}
					return XmlNodeOrder.Before;
				}
				else
				{
					if (this._column == null)
					{
						return XmlNodeOrder.Before;
					}
					if (other._column == null)
					{
						return XmlNodeOrder.After;
					}
					if (this._column.Ordinal < other._column.Ordinal)
					{
						return XmlNodeOrder.Before;
					}
					return XmlNodeOrder.After;
				}
			}
			else
			{
				xmlNode = this._node;
				xmlNode2 = other._node;
			}
			if (xmlNode == null || xmlNode2 == null)
			{
				return XmlNodeOrder.Unknown;
			}
			int num = -1;
			int num2 = -1;
			XmlNode root = XPathNodePointer.GetRoot(xmlNode, ref num);
			XmlNode root2 = XPathNodePointer.GetRoot(xmlNode2, ref num2);
			if (root != root2)
			{
				return XmlNodeOrder.Unknown;
			}
			if (num > num2)
			{
				while (xmlNode != null && num > num2)
				{
					xmlNode = ((xmlNode.NodeType == XmlNodeType.Attribute) ? ((XmlAttribute)xmlNode).OwnerElement : xmlNode.ParentNode);
					num--;
				}
				if (xmlNode == xmlNode2)
				{
					return XmlNodeOrder.After;
				}
			}
			else if (num2 > num)
			{
				while (xmlNode2 != null && num2 > num)
				{
					xmlNode2 = ((xmlNode2.NodeType == XmlNodeType.Attribute) ? ((XmlAttribute)xmlNode2).OwnerElement : xmlNode2.ParentNode);
					num2--;
				}
				if (xmlNode == xmlNode2)
				{
					return XmlNodeOrder.Before;
				}
			}
			XmlNode xmlNode3 = this.GetParent(xmlNode);
			XmlNode xmlNode4 = this.GetParent(xmlNode2);
			while (xmlNode3 != null && xmlNode4 != null)
			{
				if (xmlNode3 == xmlNode4)
				{
					while (xmlNode != null)
					{
						XmlNode nextSibling = xmlNode.NextSibling;
						if (nextSibling == xmlNode2)
						{
							return XmlNodeOrder.Before;
						}
						xmlNode = nextSibling;
					}
					return XmlNodeOrder.After;
				}
				xmlNode = xmlNode3;
				xmlNode2 = xmlNode4;
				xmlNode3 = xmlNode.ParentNode;
				xmlNode4 = xmlNode2.ParentNode;
			}
			return XmlNodeOrder.Unknown;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00013D70 File Offset: 0x00011F70
		internal XmlNode Node
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return null;
				}
				XmlBoundElement rowElement = this.GetRowElement();
				if (rowElement != null)
				{
					bool isFoliationEnabled = this._doc.IsFoliationEnabled;
					this._doc.IsFoliationEnabled = true;
					this._doc.Foliate(rowElement, ElementState.StrongFoliation);
					this._doc.IsFoliationEnabled = isFoliationEnabled;
				}
				this.RealFoliate();
				return this._node;
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00013DD4 File Offset: 0x00011FD4
		bool IXmlDataVirtualNode.IsOnNode(XmlNode nodeToCheck)
		{
			this.RealFoliate();
			return nodeToCheck == this._node;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00013DE5 File Offset: 0x00011FE5
		bool IXmlDataVirtualNode.IsOnColumn(DataColumn col)
		{
			this.RealFoliate();
			return col == this._column;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00013DF6 File Offset: 0x00011FF6
		void IXmlDataVirtualNode.OnFoliated(XmlNode foliatedNode)
		{
			if (this._node == foliatedNode)
			{
				if (this._column == null)
				{
					return;
				}
				this._bNeedFoliate = true;
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00013E14 File Offset: 0x00012014
		private void RealFoliate()
		{
			if (!this._bNeedFoliate)
			{
				return;
			}
			this._bNeedFoliate = false;
			XmlNode xmlNode;
			if (this._doc.IsTextOnly(this._column))
			{
				xmlNode = this._node.FirstChild;
			}
			else
			{
				if (this._column.ColumnMapping == MappingType.Attribute)
				{
					xmlNode = this._node.Attributes.GetNamedItem(this._column.EncodedColumnName, this._column.Namespace);
				}
				else
				{
					xmlNode = this._node.FirstChild;
					while (xmlNode != null && (!(xmlNode.LocalName == this._column.EncodedColumnName) || !(xmlNode.NamespaceURI == this._column.Namespace)))
					{
						xmlNode = xmlNode.NextSibling;
					}
				}
				if (xmlNode != null && this._fOnValue)
				{
					xmlNode = xmlNode.FirstChild;
				}
			}
			if (xmlNode == null)
			{
				throw new InvalidOperationException("Invalid foliation.");
			}
			this._node = xmlNode;
			this._column = null;
			this._fOnValue = false;
			this._bNeedFoliate = false;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00013F14 File Offset: 0x00012114
		private string GetNamespace(XmlBoundElement be, string name)
		{
			if (be == null)
			{
				return null;
			}
			if (be.IsFoliated)
			{
				XmlAttribute attributeNode = be.GetAttributeNode(name, "http://www.w3.org/2000/xmlns/");
				if (attributeNode != null)
				{
					return attributeNode.Value;
				}
				return null;
			}
			else
			{
				DataRow row = be.Row;
				if (row == null)
				{
					return null;
				}
				for (DataColumn dataColumn = this.PreviousColumn(row, null, true); dataColumn != null; dataColumn = this.PreviousColumn(row, dataColumn, true))
				{
					if (dataColumn.Namespace == "http://www.w3.org/2000/xmlns/")
					{
						DataRowVersion version = (row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current;
						return dataColumn.ConvertObjectToXml(row[dataColumn, version]);
					}
				}
				return null;
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00013FA8 File Offset: 0x000121A8
		internal string GetNamespace(string name)
		{
			if (name == "xml")
			{
				return "http://www.w3.org/XML/1998/namespace";
			}
			if (name == "xmlns")
			{
				return "http://www.w3.org/2000/xmlns/";
			}
			if (name != null && name.Length == 0)
			{
				name = "xmlns";
			}
			this.RealFoliate();
			XmlNode xmlNode = this._node;
			XmlNodeType nodeType = xmlNode.NodeType;
			while (xmlNode != null)
			{
				while (xmlNode != null && (nodeType = xmlNode.NodeType) != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.Attribute)
					{
						xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					}
					else
					{
						xmlNode = xmlNode.ParentNode;
					}
				}
				if (xmlNode != null)
				{
					string @namespace = this.GetNamespace((XmlBoundElement)xmlNode, name);
					if (@namespace != null)
					{
						return @namespace;
					}
					xmlNode = xmlNode.ParentNode;
				}
			}
			return string.Empty;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00014054 File Offset: 0x00012254
		internal bool MoveToNamespace(string name)
		{
			this._parentOfNS = (this._node as XmlBoundElement);
			if (this._parentOfNS == null)
			{
				return false;
			}
			string text = name;
			if (text == "xmlns")
			{
				text = "xmlns:xmlns";
			}
			if (text == null || text.Length == 0)
			{
			}
			this.RealFoliate();
			XmlNode xmlNode = this._node;
			XmlNodeType nodeType = xmlNode.NodeType;
			while (xmlNode != null)
			{
				XmlBoundElement xmlBoundElement = xmlNode as XmlBoundElement;
				if (xmlBoundElement != null)
				{
					if (xmlBoundElement.IsFoliated)
					{
						XmlAttribute attributeNode = xmlBoundElement.GetAttributeNode(name, "http://www.w3.org/2000/xmlns/");
						if (attributeNode != null)
						{
							this.MoveTo(attributeNode);
							return true;
						}
					}
					else
					{
						DataRow row = xmlBoundElement.Row;
						if (row == null)
						{
							return false;
						}
						for (DataColumn dataColumn = this.PreviousColumn(row, null, true); dataColumn != null; dataColumn = this.PreviousColumn(row, dataColumn, true))
						{
							if (dataColumn.Namespace == "http://www.w3.org/2000/xmlns/" && dataColumn.ColumnName == name)
							{
								this.MoveTo(xmlBoundElement, dataColumn, false);
								return true;
							}
						}
					}
				}
				do
				{
					xmlNode = xmlNode.ParentNode;
				}
				while (xmlNode != null && xmlNode.NodeType != XmlNodeType.Element);
			}
			this._parentOfNS = null;
			return false;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00014168 File Offset: 0x00012368
		private bool MoveToNextNamespace(XmlBoundElement be, DataColumn col, XmlAttribute curAttr)
		{
			if (be != null)
			{
				if (be.IsFoliated)
				{
					XmlAttributeCollection attributes = be.Attributes;
					bool flag = false;
					if (curAttr == null)
					{
						flag = true;
					}
					int i = attributes.Count;
					while (i > 0)
					{
						i--;
						XmlAttribute xmlAttribute = attributes[i];
						if (flag && xmlAttribute.NamespaceURI == "http://www.w3.org/2000/xmlns/" && !this.DuplicateNS(be, xmlAttribute.LocalName))
						{
							this.MoveTo(xmlAttribute);
							return true;
						}
						if (xmlAttribute == curAttr)
						{
							flag = true;
						}
					}
				}
				else
				{
					DataRow row = be.Row;
					if (row == null)
					{
						return false;
					}
					for (DataColumn dataColumn = this.PreviousColumn(row, col, true); dataColumn != null; dataColumn = this.PreviousColumn(row, dataColumn, true))
					{
						if (dataColumn.Namespace == "http://www.w3.org/2000/xmlns/" && !this.DuplicateNS(be, dataColumn.ColumnName))
						{
							this.MoveTo(be, dataColumn, false);
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00014240 File Offset: 0x00012440
		internal bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			this.RealFoliate();
			this._parentOfNS = (this._node as XmlBoundElement);
			if (this._parentOfNS == null)
			{
				return false;
			}
			XmlNode xmlNode = this._node;
			while (xmlNode != null)
			{
				XmlBoundElement be = xmlNode as XmlBoundElement;
				if (this.MoveToNextNamespace(be, null, null))
				{
					return true;
				}
				if (namespaceScope == XPathNamespaceScope.Local)
				{
					IL_72:
					this._parentOfNS = null;
					return false;
				}
				do
				{
					xmlNode = xmlNode.ParentNode;
				}
				while (xmlNode != null && xmlNode.NodeType != XmlNodeType.Element);
			}
			if (namespaceScope == XPathNamespaceScope.All)
			{
				this.MoveTo(this._doc._attrXml, null, false);
				return true;
			}
			goto IL_72;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000142C8 File Offset: 0x000124C8
		private bool DuplicateNS(XmlBoundElement endElem, string lname)
		{
			if (this._parentOfNS == null || endElem == null)
			{
				return false;
			}
			XmlBoundElement xmlBoundElement = this._parentOfNS;
			while (xmlBoundElement != null && xmlBoundElement != endElem)
			{
				if (this.GetNamespace(xmlBoundElement, lname) != null)
				{
					return true;
				}
				XmlNode xmlNode = xmlBoundElement;
				do
				{
					xmlNode = xmlNode.ParentNode;
				}
				while (xmlNode != null && xmlNode.NodeType != XmlNodeType.Element);
				xmlBoundElement = (xmlNode as XmlBoundElement);
			}
			return false;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00014320 File Offset: 0x00012520
		internal bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
		{
			this.RealFoliate();
			XmlNode xmlNode = this._node;
			if (this._column != null)
			{
				if (namespaceScope == XPathNamespaceScope.Local && this._parentOfNS != this._node)
				{
					return false;
				}
				XmlBoundElement xmlBoundElement = this._node as XmlBoundElement;
				DataRow row = xmlBoundElement.Row;
				for (DataColumn dataColumn = this.PreviousColumn(row, this._column, true); dataColumn != null; dataColumn = this.PreviousColumn(row, dataColumn, true))
				{
					if (dataColumn.Namespace == "http://www.w3.org/2000/xmlns/")
					{
						this.MoveTo(xmlBoundElement, dataColumn, false);
						return true;
					}
				}
				if (namespaceScope == XPathNamespaceScope.Local)
				{
					return false;
				}
				do
				{
					xmlNode = xmlNode.ParentNode;
					if (xmlNode == null)
					{
						break;
					}
				}
				while (xmlNode.NodeType != XmlNodeType.Element);
			}
			else if (this._node.NodeType == XmlNodeType.Attribute)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)this._node;
				xmlNode = xmlAttribute.OwnerElement;
				if (xmlNode == null)
				{
					return false;
				}
				if (namespaceScope == XPathNamespaceScope.Local && this._parentOfNS != xmlNode)
				{
					return false;
				}
				if (this.MoveToNextNamespace((XmlBoundElement)xmlNode, null, xmlAttribute))
				{
					return true;
				}
				if (namespaceScope == XPathNamespaceScope.Local)
				{
					return false;
				}
				do
				{
					xmlNode = xmlNode.ParentNode;
					if (xmlNode == null)
					{
						break;
					}
				}
				while (xmlNode.NodeType != XmlNodeType.Element);
			}
			while (xmlNode != null)
			{
				XmlBoundElement be = xmlNode as XmlBoundElement;
				if (this.MoveToNextNamespace(be, null, null))
				{
					return true;
				}
				do
				{
					xmlNode = xmlNode.ParentNode;
				}
				while (xmlNode != null && xmlNode.NodeType == XmlNodeType.Element);
			}
			if (namespaceScope == XPathNamespaceScope.All)
			{
				this.MoveTo(this._doc._attrXml, null, false);
				return true;
			}
			return false;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00014475 File Offset: 0x00012675
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			if (this._column != null)
			{
				DataRowState rowState = (this._node as XmlBoundElement).Row.RowState;
			}
			DataColumn column = this._column;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0001449E File Offset: 0x0001269E
		internal XmlDataDocument Document
		{
			get
			{
				return this._doc;
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000144A6 File Offset: 0x000126A6
		bool IXmlDataVirtualNode.IsInUse()
		{
			return this._owner.IsAlive;
		}

		// Token: 0x04000641 RID: 1601
		private readonly WeakReference _owner;

		// Token: 0x04000642 RID: 1602
		private readonly XmlDataDocument _doc;

		// Token: 0x04000643 RID: 1603
		private XmlNode _node;

		// Token: 0x04000644 RID: 1604
		private DataColumn _column;

		// Token: 0x04000645 RID: 1605
		private bool _fOnValue;

		// Token: 0x04000646 RID: 1606
		internal XmlBoundElement _parentOfNS;

		// Token: 0x04000647 RID: 1607
		internal static readonly int[] s_xmlNodeType_To_XpathNodeType_Map = new int[20];

		// Token: 0x04000648 RID: 1608
		internal const string StrReservedXmlns = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04000649 RID: 1609
		internal const string StrReservedXml = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x0400064A RID: 1610
		internal const string StrXmlNS = "xmlns";

		// Token: 0x0400064B RID: 1611
		private bool _bNeedFoliate;
	}
}
