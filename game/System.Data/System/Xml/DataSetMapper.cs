using System;
using System.Collections;
using System.Data;

namespace System.Xml
{
	// Token: 0x02000077 RID: 119
	internal sealed class DataSetMapper
	{
		// Token: 0x06000520 RID: 1312 RVA: 0x00011DEF File Offset: 0x0000FFEF
		internal DataSetMapper()
		{
			this._tableSchemaMap = new Hashtable();
			this._columnSchemaMap = new Hashtable();
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00011E10 File Offset: 0x00010010
		internal void SetupMapping(XmlDataDocument xd, DataSet ds)
		{
			if (this.IsMapped())
			{
				this._tableSchemaMap = new Hashtable();
				this._columnSchemaMap = new Hashtable();
			}
			this._doc = xd;
			this._dataSet = ds;
			foreach (object obj in this._dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				this.AddTableSchema(dataTable);
				foreach (object obj2 in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj2;
					if (!DataSetMapper.IsNotMapped(dataColumn))
					{
						this.AddColumnSchema(dataColumn);
					}
				}
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00011EF0 File Offset: 0x000100F0
		internal bool IsMapped()
		{
			return this._dataSet != null;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00011EFC File Offset: 0x000100FC
		internal DataTable SearchMatchingTableSchema(string localName, string namespaceURI)
		{
			object identity = DataSetMapper.GetIdentity(localName, namespaceURI);
			return (DataTable)this._tableSchemaMap[identity];
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00011F24 File Offset: 0x00010124
		internal DataTable SearchMatchingTableSchema(XmlBoundElement rowElem, XmlBoundElement elem)
		{
			DataTable dataTable = this.SearchMatchingTableSchema(elem.LocalName, elem.NamespaceURI);
			if (dataTable == null)
			{
				return null;
			}
			if (rowElem == null)
			{
				return dataTable;
			}
			if (this.GetColumnSchemaForNode(rowElem, elem) == null)
			{
				return dataTable;
			}
			using (IEnumerator enumerator = elem.Attributes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((XmlAttribute)enumerator.Current).NamespaceURI != "http://www.w3.org/2000/xmlns/")
					{
						return dataTable;
					}
				}
			}
			for (XmlNode xmlNode = elem.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					return dataTable;
				}
			}
			return null;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00011FD4 File Offset: 0x000101D4
		internal DataColumn GetColumnSchemaForNode(XmlBoundElement rowElem, XmlNode node)
		{
			object identity = DataSetMapper.GetIdentity(rowElem.LocalName, rowElem.NamespaceURI);
			object identity2 = DataSetMapper.GetIdentity(node.LocalName, node.NamespaceURI);
			Hashtable hashtable = (Hashtable)this._columnSchemaMap[identity];
			if (hashtable == null)
			{
				return null;
			}
			DataColumn dataColumn = (DataColumn)hashtable[identity2];
			if (dataColumn == null)
			{
				return null;
			}
			MappingType columnMapping = dataColumn.ColumnMapping;
			if (node.NodeType == XmlNodeType.Attribute && columnMapping == MappingType.Attribute)
			{
				return dataColumn;
			}
			if (node.NodeType == XmlNodeType.Element && columnMapping == MappingType.Element)
			{
				return dataColumn;
			}
			return null;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00012058 File Offset: 0x00010258
		internal DataTable GetTableSchemaForElement(XmlElement elem)
		{
			XmlBoundElement xmlBoundElement = elem as XmlBoundElement;
			if (xmlBoundElement == null)
			{
				return null;
			}
			return this.GetTableSchemaForElement(xmlBoundElement);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00012078 File Offset: 0x00010278
		internal DataTable GetTableSchemaForElement(XmlBoundElement be)
		{
			DataRow row = be.Row;
			if (row == null)
			{
				return null;
			}
			return row.Table;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001208B File Offset: 0x0001028B
		internal static bool IsNotMapped(DataColumn c)
		{
			return c.ColumnMapping == MappingType.Hidden;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00012096 File Offset: 0x00010296
		internal DataRow GetRowFromElement(XmlElement e)
		{
			XmlBoundElement xmlBoundElement = e as XmlBoundElement;
			if (xmlBoundElement == null)
			{
				return null;
			}
			return xmlBoundElement.Row;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000120A9 File Offset: 0x000102A9
		internal DataRow GetRowFromElement(XmlBoundElement be)
		{
			return be.Row;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000120B4 File Offset: 0x000102B4
		internal bool GetRegion(XmlNode node, out XmlBoundElement rowElem)
		{
			while (node != null)
			{
				XmlBoundElement xmlBoundElement = node as XmlBoundElement;
				if (xmlBoundElement != null && this.GetRowFromElement(xmlBoundElement) != null)
				{
					rowElem = xmlBoundElement;
					return true;
				}
				if (node.NodeType == XmlNodeType.Attribute)
				{
					node = ((XmlAttribute)node).OwnerElement;
				}
				else
				{
					node = node.ParentNode;
				}
			}
			rowElem = null;
			return false;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00012104 File Offset: 0x00010304
		internal bool IsRegionRadical(XmlBoundElement rowElem)
		{
			if (rowElem.ElementState == ElementState.Defoliated)
			{
				return true;
			}
			DataColumnCollection columns = this.GetTableSchemaForElement(rowElem).Columns;
			int num = 0;
			int count = rowElem.Attributes.Count;
			for (int i = 0; i < count; i++)
			{
				XmlAttribute xmlAttribute = rowElem.Attributes[i];
				if (!xmlAttribute.Specified)
				{
					return false;
				}
				DataColumn columnSchemaForNode = this.GetColumnSchemaForNode(rowElem, xmlAttribute);
				if (columnSchemaForNode == null)
				{
					return false;
				}
				if (!this.IsNextColumn(columns, ref num, columnSchemaForNode))
				{
					return false;
				}
				XmlNode firstChild = xmlAttribute.FirstChild;
				if (firstChild == null || firstChild.NodeType != XmlNodeType.Text || firstChild.NextSibling != null)
				{
					return false;
				}
			}
			num = 0;
			for (XmlNode xmlNode = rowElem.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.NodeType != XmlNodeType.Element)
				{
					return false;
				}
				XmlElement xmlElement = xmlNode as XmlElement;
				if (this.GetRowFromElement(xmlElement) != null)
				{
					IL_135:
					while (xmlNode != null)
					{
						if (xmlNode.NodeType != XmlNodeType.Element)
						{
							return false;
						}
						if (this.GetRowFromElement((XmlElement)xmlNode) == null)
						{
							return false;
						}
						xmlNode = xmlNode.NextSibling;
					}
					return true;
				}
				DataColumn columnSchemaForNode2 = this.GetColumnSchemaForNode(rowElem, xmlElement);
				if (columnSchemaForNode2 == null)
				{
					return false;
				}
				if (!this.IsNextColumn(columns, ref num, columnSchemaForNode2))
				{
					return false;
				}
				if (xmlElement.HasAttributes)
				{
					return false;
				}
				XmlNode firstChild2 = xmlElement.FirstChild;
				if (firstChild2 == null || firstChild2.NodeType != XmlNodeType.Text || firstChild2.NextSibling != null)
				{
					return false;
				}
			}
			goto IL_135;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001224C File Offset: 0x0001044C
		private void AddTableSchema(DataTable table)
		{
			object identity = DataSetMapper.GetIdentity(table.EncodedTableName, table.Namespace);
			this._tableSchemaMap[identity] = table;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00012278 File Offset: 0x00010478
		private void AddColumnSchema(DataColumn col)
		{
			DataTable table = col.Table;
			object identity = DataSetMapper.GetIdentity(table.EncodedTableName, table.Namespace);
			object identity2 = DataSetMapper.GetIdentity(col.EncodedColumnName, col.Namespace);
			Hashtable hashtable = (Hashtable)this._columnSchemaMap[identity];
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				this._columnSchemaMap[identity] = hashtable;
			}
			hashtable[identity2] = col;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000122E0 File Offset: 0x000104E0
		private static object GetIdentity(string localName, string namespaceURI)
		{
			return localName + ":" + namespaceURI;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000122EE File Offset: 0x000104EE
		private bool IsNextColumn(DataColumnCollection columns, ref int iColumn, DataColumn col)
		{
			while (iColumn < columns.Count)
			{
				if (columns[iColumn] == col)
				{
					iColumn++;
					return true;
				}
				iColumn++;
			}
			return false;
		}

		// Token: 0x04000638 RID: 1592
		private Hashtable _tableSchemaMap;

		// Token: 0x04000639 RID: 1593
		private Hashtable _columnSchemaMap;

		// Token: 0x0400063A RID: 1594
		private XmlDataDocument _doc;

		// Token: 0x0400063B RID: 1595
		private DataSet _dataSet;

		// Token: 0x0400063C RID: 1596
		internal const string strReservedXmlns = "http://www.w3.org/2000/xmlns/";
	}
}
