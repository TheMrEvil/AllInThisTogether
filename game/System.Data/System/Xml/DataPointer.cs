using System;
using System.Data;
using System.Diagnostics;

namespace System.Xml
{
	// Token: 0x02000076 RID: 118
	internal sealed class DataPointer : IXmlDataVirtualNode
	{
		// Token: 0x060004F6 RID: 1270 RVA: 0x00011189 File Offset: 0x0000F389
		internal DataPointer(XmlDataDocument doc, XmlNode node)
		{
			this._doc = doc;
			this._node = node;
			this._column = null;
			this._fOnValue = false;
			this._bNeedFoliate = false;
			this._isInUse = true;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000111BC File Offset: 0x0000F3BC
		internal DataPointer(DataPointer pointer)
		{
			this._doc = pointer._doc;
			this._node = pointer._node;
			this._column = pointer._column;
			this._fOnValue = pointer._fOnValue;
			this._bNeedFoliate = false;
			this._isInUse = true;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001120D File Offset: 0x0000F40D
		internal void AddPointer()
		{
			this._doc.AddPointer(this);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001121C File Offset: 0x0000F41C
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

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0001125C File Offset: 0x0000F45C
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

		// Token: 0x060004FB RID: 1275 RVA: 0x0001127B File Offset: 0x0000F47B
		private static bool IsFoliated(XmlNode node)
		{
			return node == null || !(node is XmlBoundElement) || ((XmlBoundElement)node).IsFoliated;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00011295 File Offset: 0x0000F495
		internal void MoveTo(DataPointer pointer)
		{
			this._doc = pointer._doc;
			this._node = pointer._node;
			this._column = pointer._column;
			this._fOnValue = pointer._fOnValue;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000112C7 File Offset: 0x0000F4C7
		private void MoveTo(XmlNode node)
		{
			this._node = node;
			this._column = null;
			this._fOnValue = false;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000112DE File Offset: 0x0000F4DE
		private void MoveTo(XmlNode node, DataColumn column, bool fOnValue)
		{
			this._node = node;
			this._column = column;
			this._fOnValue = fOnValue;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000112F8 File Offset: 0x0000F4F8
		private DataColumn NextColumn(DataRow row, DataColumn col, bool fAttribute, bool fNulls)
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
				if (!this._doc.IsNotMapped(dataColumn) && dataColumn.ColumnMapping == MappingType.Attribute == fAttribute && (fNulls || !Convert.IsDBNull(row[dataColumn, version])))
				{
					return dataColumn;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00011390 File Offset: 0x0000F590
		private DataColumn NthColumn(DataRow row, bool fAttribute, int iColumn, bool fNulls)
		{
			DataColumn dataColumn = null;
			checked
			{
				while ((dataColumn = this.NextColumn(row, dataColumn, fAttribute, fNulls)) != null)
				{
					if (iColumn == 0)
					{
						return dataColumn;
					}
					iColumn--;
				}
				return null;
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000113BC File Offset: 0x0000F5BC
		private int ColumnCount(DataRow row, bool fAttribute, bool fNulls)
		{
			DataColumn col = null;
			int num = 0;
			while ((col = this.NextColumn(row, col, fAttribute, fNulls)) != null)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000113E4 File Offset: 0x0000F5E4
		internal bool MoveToFirstChild()
		{
			this.RealFoliate();
			if (this._node == null)
			{
				return false;
			}
			if (this._column != null)
			{
				if (this._fOnValue)
				{
					return false;
				}
				this._fOnValue = true;
				return true;
			}
			else
			{
				if (!DataPointer.IsFoliated(this._node))
				{
					DataColumn dataColumn = this.NextColumn(this.Row, null, false, false);
					if (dataColumn != null)
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
				return false;
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00011478 File Offset: 0x0000F678
		internal bool MoveToNextSibling()
		{
			this.RealFoliate();
			if (this._node != null)
			{
				if (this._column != null)
				{
					if (this._fOnValue && !this._doc.IsTextOnly(this._column))
					{
						return false;
					}
					DataColumn dataColumn = this.NextColumn(this.Row, this._column, false, false);
					if (dataColumn != null)
					{
						this.MoveTo(this._node, dataColumn, false);
						return true;
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
					XmlNode xmlNode2 = this._doc.SafeNextSibling(this._node);
					if (xmlNode2 != null)
					{
						this.MoveTo(xmlNode2);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00011520 File Offset: 0x0000F720
		internal bool MoveToParent()
		{
			this.RealFoliate();
			if (this._node != null)
			{
				if (this._column != null)
				{
					if (this._fOnValue && !this._doc.IsTextOnly(this._column))
					{
						this.MoveTo(this._node, this._column, false);
						return true;
					}
					if (this._column.ColumnMapping != MappingType.Attribute)
					{
						this.MoveTo(this._node, null, false);
						return true;
					}
				}
				else
				{
					XmlNode parentNode = this._node.ParentNode;
					if (parentNode != null)
					{
						this.MoveTo(parentNode);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x000115AC File Offset: 0x0000F7AC
		internal bool MoveToOwnerElement()
		{
			this.RealFoliate();
			if (this._node != null)
			{
				if (this._column != null)
				{
					if (this._fOnValue || this._doc.IsTextOnly(this._column) || this._column.ColumnMapping != MappingType.Attribute)
					{
						return false;
					}
					this.MoveTo(this._node, null, false);
					return true;
				}
				else if (this._node.NodeType == XmlNodeType.Attribute)
				{
					XmlNode ownerElement = ((XmlAttribute)this._node).OwnerElement;
					if (ownerElement != null)
					{
						this.MoveTo(ownerElement, null, false);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00011638 File Offset: 0x0000F838
		internal int AttributeCount
		{
			get
			{
				this.RealFoliate();
				if (this._node == null || this._column != null || this._node.NodeType != XmlNodeType.Element)
				{
					return 0;
				}
				if (!DataPointer.IsFoliated(this._node))
				{
					return this.ColumnCount(this.Row, true, false);
				}
				return this._node.Attributes.Count;
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00011698 File Offset: 0x0000F898
		internal bool MoveToAttribute(int i)
		{
			this.RealFoliate();
			if (i < 0)
			{
				return false;
			}
			if (this._node != null && (this._column == null || this._column.ColumnMapping == MappingType.Attribute) && this._node.NodeType == XmlNodeType.Element)
			{
				if (!DataPointer.IsFoliated(this._node))
				{
					DataColumn dataColumn = this.NthColumn(this.Row, true, i, false);
					if (dataColumn != null)
					{
						this.MoveTo(this._node, dataColumn, false);
						return true;
					}
				}
				else
				{
					XmlNode xmlNode = this._node.Attributes.Item(i);
					if (xmlNode != null)
					{
						this.MoveTo(xmlNode, null, false);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00011730 File Offset: 0x0000F930
		internal XmlNodeType NodeType
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return XmlNodeType.None;
				}
				if (this._column == null)
				{
					return this._node.NodeType;
				}
				if (this._fOnValue)
				{
					return XmlNodeType.Text;
				}
				if (this._column.ColumnMapping == MappingType.Attribute)
				{
					return XmlNodeType.Attribute;
				}
				return XmlNodeType.Element;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0001177C File Offset: 0x0000F97C
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
					string localName = this._node.LocalName;
					if (this.IsLocalNameEmpty(this._node.NodeType))
					{
						return string.Empty;
					}
					return localName;
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

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x000117F8 File Offset: 0x0000F9F8
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
					return this._node.NamespaceURI;
				}
				if (this._fOnValue)
				{
					return string.Empty;
				}
				return this._doc.NameTable.Add(this._column.Namespace);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00011858 File Offset: 0x0000FA58
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
					string name = this._node.Name;
					if (this.IsLocalNameEmpty(this._node.NodeType))
					{
						return string.Empty;
					}
					return name;
				}
				else
				{
					string prefix = this.Prefix;
					string localName = this.LocalName;
					if (prefix == null || prefix.Length <= 0)
					{
						return localName;
					}
					if (localName != null && localName.Length > 0)
					{
						return this._doc.NameTable.Add(prefix + ":" + localName);
					}
					return prefix;
				}
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000118F0 File Offset: 0x0000FAF0
		private bool IsLocalNameEmpty(XmlNodeType nt)
		{
			switch (nt)
			{
			case XmlNodeType.None:
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.Comment:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
			case XmlNodeType.EndElement:
			case XmlNodeType.EndEntity:
				return true;
			case XmlNodeType.Element:
			case XmlNodeType.Attribute:
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.DocumentType:
			case XmlNodeType.Notation:
			case XmlNodeType.XmlDeclaration:
				return false;
			default:
				return true;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00011952 File Offset: 0x0000FB52
		internal string Prefix
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
					return this._node.Prefix;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00011984 File Offset: 0x0000FB84
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
					return this._node.Value;
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

		// Token: 0x0600050F RID: 1295 RVA: 0x00011A0C File Offset: 0x0000FC0C
		bool IXmlDataVirtualNode.IsOnNode(XmlNode nodeToCheck)
		{
			this.RealFoliate();
			return nodeToCheck == this._node;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00011A1D File Offset: 0x0000FC1D
		bool IXmlDataVirtualNode.IsOnColumn(DataColumn col)
		{
			this.RealFoliate();
			return col == this._column;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00011A2E File Offset: 0x0000FC2E
		internal XmlNode GetNode()
		{
			return this._node;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00011A36 File Offset: 0x0000FC36
		internal bool IsEmptyElement
		{
			get
			{
				this.RealFoliate();
				return this._node != null && this._column == null && this._node.NodeType == XmlNodeType.Element && ((XmlElement)this._node).IsEmpty;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00011A6E File Offset: 0x0000FC6E
		internal bool IsDefault
		{
			get
			{
				this.RealFoliate();
				return this._node != null && this._column == null && this._node.NodeType == XmlNodeType.Attribute && !((XmlAttribute)this._node).Specified;
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00011AA9 File Offset: 0x0000FCA9
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

		// Token: 0x06000515 RID: 1301 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		internal void RealFoliate()
		{
			if (!this._bNeedFoliate)
			{
				return;
			}
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

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00011BBC File Offset: 0x0000FDBC
		internal string PublicId
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType == XmlNodeType.Entity)
				{
					return ((XmlEntity)this._node).PublicId;
				}
				if (nodeType == XmlNodeType.DocumentType)
				{
					return ((XmlDocumentType)this._node).PublicId;
				}
				if (nodeType != XmlNodeType.Notation)
				{
					return null;
				}
				return ((XmlNotation)this._node).PublicId;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00011C14 File Offset: 0x0000FE14
		internal string SystemId
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType == XmlNodeType.Entity)
				{
					return ((XmlEntity)this._node).SystemId;
				}
				if (nodeType == XmlNodeType.DocumentType)
				{
					return ((XmlDocumentType)this._node).SystemId;
				}
				if (nodeType != XmlNodeType.Notation)
				{
					return null;
				}
				return ((XmlNotation)this._node).SystemId;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00011C6C File Offset: 0x0000FE6C
		internal string InternalSubset
		{
			get
			{
				if (this.NodeType == XmlNodeType.DocumentType)
				{
					return ((XmlDocumentType)this._node).InternalSubset;
				}
				return null;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00011C8C File Offset: 0x0000FE8C
		internal XmlDeclaration Declaration
		{
			get
			{
				XmlNode xmlNode = this._doc.SafeFirstChild(this._doc);
				if (xmlNode != null && xmlNode.NodeType == XmlNodeType.XmlDeclaration)
				{
					return (XmlDeclaration)xmlNode;
				}
				return null;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00011CC0 File Offset: 0x0000FEC0
		internal string Encoding
		{
			get
			{
				if (this.NodeType == XmlNodeType.XmlDeclaration)
				{
					return ((XmlDeclaration)this._node).Encoding;
				}
				if (this.NodeType == XmlNodeType.Document)
				{
					XmlDeclaration declaration = this.Declaration;
					if (declaration != null)
					{
						return declaration.Encoding;
					}
				}
				return null;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00011D04 File Offset: 0x0000FF04
		internal string Standalone
		{
			get
			{
				if (this.NodeType == XmlNodeType.XmlDeclaration)
				{
					return ((XmlDeclaration)this._node).Standalone;
				}
				if (this.NodeType == XmlNodeType.Document)
				{
					XmlDeclaration declaration = this.Declaration;
					if (declaration != null)
					{
						return declaration.Standalone;
					}
				}
				return null;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00011D48 File Offset: 0x0000FF48
		internal string Version
		{
			get
			{
				if (this.NodeType == XmlNodeType.XmlDeclaration)
				{
					return ((XmlDeclaration)this._node).Version;
				}
				if (this.NodeType == XmlNodeType.Document)
				{
					XmlDeclaration declaration = this.Declaration;
					if (declaration != null)
					{
						return declaration.Version;
					}
				}
				return null;
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00011D8C File Offset: 0x0000FF8C
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			if (this._column != null)
			{
				XmlBoundElement xmlBoundElement = this._node as XmlBoundElement;
				DataRow row = xmlBoundElement.Row;
				ElementState elementState = xmlBoundElement.ElementState;
				DataRowState rowState = row.RowState;
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00011DC2 File Offset: 0x0000FFC2
		bool IXmlDataVirtualNode.IsInUse()
		{
			return this._isInUse;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00011DCA File Offset: 0x0000FFCA
		internal void SetNoLongerUse()
		{
			this._node = null;
			this._column = null;
			this._fOnValue = false;
			this._bNeedFoliate = false;
			this._isInUse = false;
		}

		// Token: 0x04000632 RID: 1586
		private XmlDataDocument _doc;

		// Token: 0x04000633 RID: 1587
		private XmlNode _node;

		// Token: 0x04000634 RID: 1588
		private DataColumn _column;

		// Token: 0x04000635 RID: 1589
		private bool _fOnValue;

		// Token: 0x04000636 RID: 1590
		private bool _bNeedFoliate;

		// Token: 0x04000637 RID: 1591
		private bool _isInUse;
	}
}
