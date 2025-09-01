using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data
{
	// Token: 0x02000141 RID: 321
	internal sealed class XmlDataLoader
	{
		// Token: 0x06001135 RID: 4405 RVA: 0x0004CA94 File Offset: 0x0004AC94
		internal XmlDataLoader(DataSet dataset, bool IsXdr, bool ignoreSchema)
		{
			this._dataSet = dataset;
			this._nodeToRowMap = new Hashtable();
			this._fIsXdr = IsXdr;
			this._ignoreSchema = ignoreSchema;
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0004CABC File Offset: 0x0004ACBC
		internal XmlDataLoader(DataSet dataset, bool IsXdr, XmlElement topNode, bool ignoreSchema)
		{
			this._dataSet = dataset;
			this._nodeToRowMap = new Hashtable();
			this._fIsXdr = IsXdr;
			this._childRowsStack = new Stack(50);
			this._topMostNode = topNode;
			this._ignoreSchema = ignoreSchema;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0004CAF9 File Offset: 0x0004ACF9
		internal XmlDataLoader(DataTable datatable, bool IsXdr, bool ignoreSchema)
		{
			this._dataSet = null;
			this._dataTable = datatable;
			this._isTableLevel = true;
			this._nodeToRowMap = new Hashtable();
			this._fIsXdr = IsXdr;
			this._ignoreSchema = ignoreSchema;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0004CB30 File Offset: 0x0004AD30
		internal XmlDataLoader(DataTable datatable, bool IsXdr, XmlElement topNode, bool ignoreSchema)
		{
			this._dataSet = null;
			this._dataTable = datatable;
			this._isTableLevel = true;
			this._nodeToRowMap = new Hashtable();
			this._fIsXdr = IsXdr;
			this._childRowsStack = new Stack(50);
			this._topMostNode = topNode;
			this._ignoreSchema = ignoreSchema;
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x0004CB86 File Offset: 0x0004AD86
		// (set) Token: 0x0600113A RID: 4410 RVA: 0x0004CB8E File Offset: 0x0004AD8E
		internal bool FromInference
		{
			get
			{
				return this._fromInference;
			}
			set
			{
				this._fromInference = value;
			}
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0004CB98 File Offset: 0x0004AD98
		private void AttachRows(DataRow parentRow, XmlNode parentElement)
		{
			if (parentElement == null)
			{
				return;
			}
			for (XmlNode xmlNode = parentElement.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					XmlElement e = (XmlElement)xmlNode;
					DataRow rowFromElement = this.GetRowFromElement(e);
					if (rowFromElement != null && rowFromElement.RowState == DataRowState.Detached)
					{
						if (parentRow != null)
						{
							rowFromElement.SetNestedParentRow(parentRow, false);
						}
						rowFromElement.Table.Rows.Add(rowFromElement);
					}
					else if (rowFromElement == null)
					{
						this.AttachRows(parentRow, xmlNode);
					}
					this.AttachRows(rowFromElement, xmlNode);
				}
			}
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0004CC14 File Offset: 0x0004AE14
		private int CountNonNSAttributes(XmlNode node)
		{
			int num = 0;
			for (int i = 0; i < node.Attributes.Count; i++)
			{
				XmlAttribute xmlAttribute = node.Attributes[i];
				if (!this.FExcludedNamespace(node.Attributes[i].NamespaceURI))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0004CC64 File Offset: 0x0004AE64
		private string GetValueForTextOnlyColums(XmlNode n)
		{
			string text = null;
			while (n != null && (n.NodeType == XmlNodeType.Whitespace || !this.IsTextLikeNode(n.NodeType)))
			{
				n = n.NextSibling;
			}
			if (n != null)
			{
				if (this.IsTextLikeNode(n.NodeType) && (n.NextSibling == null || !this.IsTextLikeNode(n.NodeType)))
				{
					text = n.Value;
					n = n.NextSibling;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (n != null && this.IsTextLikeNode(n.NodeType))
					{
						stringBuilder.Append(n.Value);
						n = n.NextSibling;
					}
					text = stringBuilder.ToString();
				}
			}
			if (text == null)
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0004CD10 File Offset: 0x0004AF10
		private string GetInitialTextFromNodes(ref XmlNode n)
		{
			string text = null;
			if (n != null)
			{
				while (n.NodeType == XmlNodeType.Whitespace)
				{
					n = n.NextSibling;
				}
				if (this.IsTextLikeNode(n.NodeType) && (n.NextSibling == null || !this.IsTextLikeNode(n.NodeType)))
				{
					text = n.Value;
					n = n.NextSibling;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (n != null && this.IsTextLikeNode(n.NodeType))
					{
						stringBuilder.Append(n.Value);
						n = n.NextSibling;
					}
					text = stringBuilder.ToString();
				}
			}
			if (text == null)
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0004CDBC File Offset: 0x0004AFBC
		private DataColumn GetTextOnlyColumn(DataRow row)
		{
			DataColumnCollection columns = row.Table.Columns;
			int count = columns.Count;
			for (int i = 0; i < count; i++)
			{
				DataColumn dataColumn = columns[i];
				if (this.IsTextOnly(dataColumn))
				{
					return dataColumn;
				}
			}
			return null;
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0004CDFC File Offset: 0x0004AFFC
		internal DataRow GetRowFromElement(XmlElement e)
		{
			return (DataRow)this._nodeToRowMap[e];
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0004CE10 File Offset: 0x0004B010
		internal bool FColumnElement(XmlElement e)
		{
			if (this._nodeToSchemaMap.GetColumnSchema(e, this.FIgnoreNamespace(e)) == null)
			{
				return false;
			}
			if (this.CountNonNSAttributes(e) > 0)
			{
				return false;
			}
			for (XmlNode xmlNode = e.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode is XmlElement)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0004CE5E File Offset: 0x0004B05E
		private bool FExcludedNamespace(string ns)
		{
			return ns.Equals("http://www.w3.org/2000/xmlns/") || (this._htableExcludedNS != null && this._htableExcludedNS.Contains(ns));
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0004CE88 File Offset: 0x0004B088
		private bool FIgnoreNamespace(XmlNode node)
		{
			if (!this._fIsXdr)
			{
				return false;
			}
			XmlNode xmlNode;
			if (node is XmlAttribute)
			{
				xmlNode = ((XmlAttribute)node).OwnerElement;
			}
			else
			{
				xmlNode = node;
			}
			return xmlNode.NamespaceURI.StartsWith("x-schema:#", StringComparison.Ordinal);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0004CECD File Offset: 0x0004B0CD
		private bool FIgnoreNamespace(XmlReader node)
		{
			return this._fIsXdr && node.NamespaceURI.StartsWith("x-schema:#", StringComparison.Ordinal);
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0004CEED File Offset: 0x0004B0ED
		internal bool IsTextLikeNode(XmlNodeType n)
		{
			if (n - XmlNodeType.Text > 1)
			{
				if (n == XmlNodeType.EntityReference)
				{
					throw ExceptionBuilder.FoundEntity();
				}
				if (n - XmlNodeType.Whitespace > 1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0004CF0B File Offset: 0x0004B10B
		internal bool IsTextOnly(DataColumn c)
		{
			return c.ColumnMapping == MappingType.SimpleContent;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0004CF1C File Offset: 0x0004B11C
		internal void LoadData(XmlDocument xdoc)
		{
			if (xdoc.DocumentElement == null)
			{
				return;
			}
			bool enforceConstraints;
			if (this._isTableLevel)
			{
				enforceConstraints = this._dataTable.EnforceConstraints;
				this._dataTable.EnforceConstraints = false;
			}
			else
			{
				enforceConstraints = this._dataSet.EnforceConstraints;
				this._dataSet.EnforceConstraints = false;
				this._dataSet._fInReadXml = true;
			}
			if (this._isTableLevel)
			{
				this._nodeToSchemaMap = new XmlToDatasetMap(this._dataTable, xdoc.NameTable);
			}
			else
			{
				this._nodeToSchemaMap = new XmlToDatasetMap(this._dataSet, xdoc.NameTable);
			}
			DataRow dataRow = null;
			if (this._isTableLevel || (this._dataSet != null && this._dataSet._fTopLevelTable))
			{
				XmlElement documentElement = xdoc.DocumentElement;
				DataTable dataTable = (DataTable)this._nodeToSchemaMap.GetSchemaForNode(documentElement, this.FIgnoreNamespace(documentElement));
				if (dataTable != null)
				{
					dataRow = dataTable.CreateEmptyRow();
					this._nodeToRowMap[documentElement] = dataRow;
					this.LoadRowData(dataRow, documentElement);
					dataTable.Rows.Add(dataRow);
				}
			}
			this.LoadRows(dataRow, xdoc.DocumentElement);
			this.AttachRows(dataRow, xdoc.DocumentElement);
			if (this._isTableLevel)
			{
				this._dataTable.EnforceConstraints = enforceConstraints;
				return;
			}
			this._dataSet._fInReadXml = false;
			this._dataSet.EnforceConstraints = enforceConstraints;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0004D064 File Offset: 0x0004B264
		private void LoadRowData(DataRow row, XmlElement rowElement)
		{
			DataTable table = row.Table;
			if (this.FromInference)
			{
				table.Prefix = rowElement.Prefix;
			}
			Hashtable hashtable = new Hashtable();
			row.BeginEdit();
			XmlNode xmlNode = rowElement.FirstChild;
			DataColumn textOnlyColumn = this.GetTextOnlyColumn(row);
			if (textOnlyColumn != null)
			{
				hashtable[textOnlyColumn] = textOnlyColumn;
				string valueForTextOnlyColums = this.GetValueForTextOnlyColums(xmlNode);
				if (XMLSchema.GetBooleanAttribute(rowElement, "nil", "http://www.w3.org/2001/XMLSchema-instance", false) && string.IsNullOrEmpty(valueForTextOnlyColums))
				{
					row[textOnlyColumn] = DBNull.Value;
				}
				else
				{
					this.SetRowValueFromXmlText(row, textOnlyColumn, valueForTextOnlyColums);
				}
			}
			while (xmlNode != null && xmlNode != rowElement)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					object obj = this._nodeToSchemaMap.GetSchemaForNode(xmlElement, this.FIgnoreNamespace(xmlElement));
					if (obj is DataTable && this.FColumnElement(xmlElement))
					{
						obj = this._nodeToSchemaMap.GetColumnSchema(xmlElement, this.FIgnoreNamespace(xmlElement));
					}
					if (obj == null || obj is DataColumn)
					{
						xmlNode = xmlElement.FirstChild;
						if (obj != null && obj is DataColumn)
						{
							DataColumn dataColumn = (DataColumn)obj;
							if (dataColumn.Table == row.Table && dataColumn.ColumnMapping != MappingType.Attribute && hashtable[dataColumn] == null)
							{
								hashtable[dataColumn] = dataColumn;
								string valueForTextOnlyColums2 = this.GetValueForTextOnlyColums(xmlNode);
								if (XMLSchema.GetBooleanAttribute(xmlElement, "nil", "http://www.w3.org/2001/XMLSchema-instance", false) && string.IsNullOrEmpty(valueForTextOnlyColums2))
								{
									row[dataColumn] = DBNull.Value;
								}
								else
								{
									this.SetRowValueFromXmlText(row, dataColumn, valueForTextOnlyColums2);
								}
							}
						}
						else if (obj == null && xmlNode != null)
						{
							continue;
						}
						if (xmlNode == null)
						{
							xmlNode = xmlElement;
						}
					}
				}
				while (xmlNode != rowElement && xmlNode.NextSibling == null)
				{
					xmlNode = xmlNode.ParentNode;
				}
				if (xmlNode != rowElement)
				{
					xmlNode = xmlNode.NextSibling;
				}
			}
			foreach (object obj2 in rowElement.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj2;
				object columnSchema = this._nodeToSchemaMap.GetColumnSchema(xmlAttribute, this.FIgnoreNamespace(xmlAttribute));
				if (columnSchema != null && columnSchema is DataColumn)
				{
					DataColumn dataColumn2 = (DataColumn)columnSchema;
					if (dataColumn2.ColumnMapping == MappingType.Attribute && hashtable[dataColumn2] == null)
					{
						hashtable[dataColumn2] = dataColumn2;
						xmlNode = xmlAttribute.FirstChild;
						this.SetRowValueFromXmlText(row, dataColumn2, this.GetInitialTextFromNodes(ref xmlNode));
					}
				}
			}
			foreach (object obj3 in row.Table.Columns)
			{
				DataColumn dataColumn3 = (DataColumn)obj3;
				if (hashtable[dataColumn3] == null && XmlToDatasetMap.IsMappedColumn(dataColumn3))
				{
					if (!dataColumn3.AutoIncrement)
					{
						if (dataColumn3.AllowDBNull)
						{
							row[dataColumn3] = DBNull.Value;
						}
						else
						{
							row[dataColumn3] = dataColumn3.DefaultValue;
						}
					}
					else
					{
						dataColumn3.Init(row._tempRecord);
					}
				}
			}
			row.EndEdit();
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0004D380 File Offset: 0x0004B580
		private void LoadRows(DataRow parentRow, XmlNode parentElement)
		{
			if (parentElement == null)
			{
				return;
			}
			if ((parentElement.LocalName == "schema" && parentElement.NamespaceURI == "http://www.w3.org/2001/XMLSchema") || (parentElement.LocalName == "sync" && parentElement.NamespaceURI == "urn:schemas-microsoft-com:xml-updategram") || (parentElement.LocalName == "Schema" && parentElement.NamespaceURI == "urn:schemas-microsoft-com:xml-data"))
			{
				return;
			}
			for (XmlNode xmlNode = parentElement.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode is XmlElement)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					object schemaForNode = this._nodeToSchemaMap.GetSchemaForNode(xmlElement, this.FIgnoreNamespace(xmlElement));
					if (schemaForNode != null && schemaForNode is DataTable)
					{
						DataRow dataRow = this.GetRowFromElement(xmlElement);
						if (dataRow == null)
						{
							if (parentRow != null && this.FColumnElement(xmlElement))
							{
								goto IL_F2;
							}
							dataRow = ((DataTable)schemaForNode).CreateEmptyRow();
							this._nodeToRowMap[xmlElement] = dataRow;
							this.LoadRowData(dataRow, xmlElement);
						}
						this.LoadRows(dataRow, xmlNode);
					}
					else
					{
						this.LoadRows(null, xmlNode);
					}
				}
				IL_F2:;
			}
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x0004D48C File Offset: 0x0004B68C
		private void SetRowValueFromXmlText(DataRow row, DataColumn col, string xmlText)
		{
			row[col] = col.ConvertXmlToObject(xmlText);
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0004D49C File Offset: 0x0004B69C
		private void InitNameTable()
		{
			XmlNameTable nameTable = this._dataReader.NameTable;
			this._XSD_XMLNS_NS = nameTable.Add("http://www.w3.org/2000/xmlns/");
			this._XDR_SCHEMA = nameTable.Add("Schema");
			this._XDRNS = nameTable.Add("urn:schemas-microsoft-com:xml-data");
			this._SQL_SYNC = nameTable.Add("sync");
			this._UPDGNS = nameTable.Add("urn:schemas-microsoft-com:xml-updategram");
			this._XSD_SCHEMA = nameTable.Add("schema");
			this._XSDNS = nameTable.Add("http://www.w3.org/2001/XMLSchema");
			this._DFFNS = nameTable.Add("urn:schemas-microsoft-com:xml-diffgram-v1");
			this._MSDNS = nameTable.Add("urn:schemas-microsoft-com:xml-msdata");
			this._DIFFID = nameTable.Add("id");
			this._HASCHANGES = nameTable.Add("hasChanges");
			this._ROWORDER = nameTable.Add("rowOrder");
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0004D584 File Offset: 0x0004B784
		internal void LoadData(XmlReader reader)
		{
			this._dataReader = DataTextReader.CreateReader(reader);
			int depth = this._dataReader.Depth;
			bool enforceConstraints = this._isTableLevel ? this._dataTable.EnforceConstraints : this._dataSet.EnforceConstraints;
			this.InitNameTable();
			if (this._nodeToSchemaMap == null)
			{
				this._nodeToSchemaMap = (this._isTableLevel ? new XmlToDatasetMap(this._dataReader.NameTable, this._dataTable) : new XmlToDatasetMap(this._dataReader.NameTable, this._dataSet));
			}
			if (this._isTableLevel)
			{
				this._dataTable.EnforceConstraints = false;
			}
			else
			{
				this._dataSet.EnforceConstraints = false;
				this._dataSet._fInReadXml = true;
			}
			if (this._topMostNode != null)
			{
				if (!this._isDiffgram && !this._isTableLevel)
				{
					DataTable dataTable = this._nodeToSchemaMap.GetSchemaForNode(this._topMostNode, this.FIgnoreNamespace(this._topMostNode)) as DataTable;
					if (dataTable != null)
					{
						this.LoadTopMostTable(dataTable);
					}
				}
				this._topMostNode = null;
			}
			while (!this._dataReader.EOF && this._dataReader.Depth >= depth)
			{
				if (reader.NodeType != XmlNodeType.Element)
				{
					this._dataReader.Read();
				}
				else
				{
					DataTable tableForNode = this._nodeToSchemaMap.GetTableForNode(this._dataReader, this.FIgnoreNamespace(this._dataReader));
					if (tableForNode == null)
					{
						if (!this.ProcessXsdSchema())
						{
							this._dataReader.Read();
						}
					}
					else
					{
						this.LoadTable(tableForNode, false);
					}
				}
			}
			if (this._isTableLevel)
			{
				this._dataTable.EnforceConstraints = enforceConstraints;
				return;
			}
			this._dataSet._fInReadXml = false;
			this._dataSet.EnforceConstraints = enforceConstraints;
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0004D734 File Offset: 0x0004B934
		private void LoadTopMostTable(DataTable table)
		{
			bool flag = this._isTableLevel || this._dataSet.DataSetName != table.TableName;
			bool flag2 = false;
			int num = this._dataReader.Depth - 1;
			int i = this._childRowsStack.Count;
			DataColumnCollection columns = table.Columns;
			object[] array = new object[columns.Count];
			DataColumn dataColumn;
			using (IEnumerator enumerator = this._topMostNode.Attributes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					dataColumn = (this._nodeToSchemaMap.GetColumnSchema(xmlAttribute, this.FIgnoreNamespace(xmlAttribute)) as DataColumn);
					if (dataColumn != null && dataColumn.ColumnMapping == MappingType.Attribute)
					{
						XmlNode firstChild = xmlAttribute.FirstChild;
						array[dataColumn.Ordinal] = dataColumn.ConvertXmlToObject(this.GetInitialTextFromNodes(ref firstChild));
						flag2 = true;
					}
				}
				goto IL_201;
			}
			IL_EA:
			XmlNodeType nodeType = this._dataReader.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
			{
				object columnSchema = this._nodeToSchemaMap.GetColumnSchema(table, this._dataReader, this.FIgnoreNamespace(this._dataReader));
				dataColumn = (columnSchema as DataColumn);
				if (dataColumn != null)
				{
					if (array[dataColumn.Ordinal] == null)
					{
						this.LoadColumn(dataColumn, array);
						flag2 = true;
						goto IL_201;
					}
					this._dataReader.Read();
					goto IL_201;
				}
				else
				{
					DataTable dataTable = columnSchema as DataTable;
					if (dataTable != null)
					{
						this.LoadTable(dataTable, true);
						flag2 = true;
						goto IL_201;
					}
					if (this.ProcessXsdSchema())
					{
						goto IL_201;
					}
					if (!flag2 && !flag)
					{
						return;
					}
					this._dataReader.Read();
					goto IL_201;
				}
				break;
			}
			case XmlNodeType.Attribute:
				goto IL_1F5;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				break;
			case XmlNodeType.EntityReference:
				throw ExceptionBuilder.FoundEntity();
			default:
				if (nodeType - XmlNodeType.Whitespace > 1)
				{
					goto IL_1F5;
				}
				break;
			}
			string s = this._dataReader.ReadString();
			dataColumn = table._xmlText;
			if (dataColumn != null && array[dataColumn.Ordinal] == null)
			{
				array[dataColumn.Ordinal] = dataColumn.ConvertXmlToObject(s);
				goto IL_201;
			}
			goto IL_201;
			IL_1F5:
			this._dataReader.Read();
			IL_201:
			if (num >= this._dataReader.Depth)
			{
				this._dataReader.Read();
				for (int j = array.Length - 1; j >= 0; j--)
				{
					if (array[j] == null)
					{
						dataColumn = columns[j];
						if (dataColumn.AllowDBNull && dataColumn.ColumnMapping != MappingType.Hidden && !dataColumn.AutoIncrement)
						{
							array[j] = DBNull.Value;
						}
					}
				}
				DataRow parentRow = table.Rows.AddWithColumnEvents(array);
				while (i < this._childRowsStack.Count)
				{
					DataRow dataRow = (DataRow)this._childRowsStack.Pop();
					bool flag3 = dataRow.RowState == DataRowState.Unchanged;
					dataRow.SetNestedParentRow(parentRow, false);
					if (flag3)
					{
						dataRow._oldRecord = dataRow._newRecord;
					}
				}
				return;
			}
			goto IL_EA;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0004DA14 File Offset: 0x0004BC14
		private void LoadTable(DataTable table, bool isNested)
		{
			int i = this._dataReader.Depth;
			int j = this._childRowsStack.Count;
			DataColumnCollection columns = table.Columns;
			object[] array = new object[columns.Count];
			int pos = -1;
			string key = string.Empty;
			string text = null;
			bool flag = false;
			for (int k = this._dataReader.AttributeCount - 1; k >= 0; k--)
			{
				this._dataReader.MoveToAttribute(k);
				DataColumn dataColumn = this._nodeToSchemaMap.GetColumnSchema(table, this._dataReader, this.FIgnoreNamespace(this._dataReader)) as DataColumn;
				if (dataColumn != null && dataColumn.ColumnMapping == MappingType.Attribute)
				{
					array[dataColumn.Ordinal] = dataColumn.ConvertXmlToObject(this._dataReader.Value);
				}
				if (this._isDiffgram)
				{
					if (this._dataReader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
					{
						string localName = this._dataReader.LocalName;
						if (!(localName == "id"))
						{
							if (!(localName == "hasChanges"))
							{
								if (localName == "hasErrors")
								{
									flag = (bool)Convert.ChangeType(this._dataReader.Value, typeof(bool), CultureInfo.InvariantCulture);
								}
							}
							else
							{
								text = this._dataReader.Value;
							}
						}
						else
						{
							key = this._dataReader.Value;
						}
					}
					else if (this._dataReader.NamespaceURI == "urn:schemas-microsoft-com:xml-msdata")
					{
						if (this._dataReader.LocalName == "rowOrder")
						{
							pos = (int)Convert.ChangeType(this._dataReader.Value, typeof(int), CultureInfo.InvariantCulture);
						}
						else if (this._dataReader.LocalName.StartsWith("hidden", StringComparison.Ordinal))
						{
							dataColumn = columns[XmlConvert.DecodeName(this._dataReader.LocalName.Substring(6))];
							if (dataColumn != null && dataColumn.ColumnMapping == MappingType.Hidden)
							{
								array[dataColumn.Ordinal] = dataColumn.ConvertXmlToObject(this._dataReader.Value);
							}
						}
					}
				}
			}
			if (this._dataReader.Read() && i < this._dataReader.Depth)
			{
				while (i < this._dataReader.Depth)
				{
					XmlNodeType nodeType = this._dataReader.NodeType;
					DataColumn dataColumn;
					switch (nodeType)
					{
					case XmlNodeType.Element:
					{
						object columnSchema = this._nodeToSchemaMap.GetColumnSchema(table, this._dataReader, this.FIgnoreNamespace(this._dataReader));
						dataColumn = (columnSchema as DataColumn);
						if (dataColumn != null)
						{
							if (array[dataColumn.Ordinal] == null)
							{
								this.LoadColumn(dataColumn, array);
								continue;
							}
							this._dataReader.Read();
							continue;
						}
						else
						{
							DataTable dataTable = columnSchema as DataTable;
							if (dataTable != null)
							{
								this.LoadTable(dataTable, true);
								continue;
							}
							if (this.ProcessXsdSchema())
							{
								continue;
							}
							DataTable tableForNode = this._nodeToSchemaMap.GetTableForNode(this._dataReader, this.FIgnoreNamespace(this._dataReader));
							if (tableForNode != null)
							{
								this.LoadTable(tableForNode, false);
								continue;
							}
							this._dataReader.Read();
							continue;
						}
						break;
					}
					case XmlNodeType.Attribute:
						goto IL_370;
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						break;
					case XmlNodeType.EntityReference:
						throw ExceptionBuilder.FoundEntity();
					default:
						if (nodeType - XmlNodeType.Whitespace > 1)
						{
							goto IL_370;
						}
						break;
					}
					string s = this._dataReader.ReadString();
					dataColumn = table._xmlText;
					if (dataColumn != null && array[dataColumn.Ordinal] == null)
					{
						array[dataColumn.Ordinal] = dataColumn.ConvertXmlToObject(s);
						continue;
					}
					continue;
					IL_370:
					this._dataReader.Read();
				}
				this._dataReader.Read();
			}
			DataRow dataRow;
			if (this._isDiffgram)
			{
				dataRow = table.NewRow(table.NewUninitializedRecord());
				dataRow.BeginEdit();
				for (int l = array.Length - 1; l >= 0; l--)
				{
					DataColumn dataColumn = columns[l];
					dataColumn[dataRow._tempRecord] = ((array[l] != null) ? array[l] : DBNull.Value);
				}
				dataRow.EndEdit();
				table.Rows.DiffInsertAt(dataRow, pos);
				if (text == null)
				{
					dataRow._oldRecord = dataRow._newRecord;
				}
				if (text == "modified" || flag)
				{
					table.RowDiffId[key] = dataRow;
				}
			}
			else
			{
				for (int m = array.Length - 1; m >= 0; m--)
				{
					if (array[m] == null)
					{
						DataColumn dataColumn = columns[m];
						if (dataColumn.AllowDBNull && dataColumn.ColumnMapping != MappingType.Hidden && !dataColumn.AutoIncrement)
						{
							array[m] = DBNull.Value;
						}
					}
				}
				dataRow = table.Rows.AddWithColumnEvents(array);
			}
			while (j < this._childRowsStack.Count)
			{
				DataRow dataRow2 = (DataRow)this._childRowsStack.Pop();
				bool flag2 = dataRow2.RowState == DataRowState.Unchanged;
				dataRow2.SetNestedParentRow(dataRow, false);
				if (flag2)
				{
					dataRow2._oldRecord = dataRow2._newRecord;
				}
			}
			if (isNested)
			{
				this._childRowsStack.Push(dataRow);
			}
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0004DF10 File Offset: 0x0004C110
		private void LoadColumn(DataColumn column, object[] foundColumns)
		{
			string text = string.Empty;
			string text2 = null;
			int i = this._dataReader.Depth;
			if (this._dataReader.AttributeCount > 0)
			{
				text2 = this._dataReader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			}
			if (column.IsCustomType)
			{
				object obj = null;
				string text3 = null;
				string text4 = null;
				XmlRootAttribute xmlRootAttribute = null;
				if (this._dataReader.AttributeCount > 0)
				{
					text3 = this._dataReader.GetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance");
					text4 = this._dataReader.GetAttribute("InstanceType", "urn:schemas-microsoft-com:xml-msdata");
				}
				bool flag = !column.ImplementsIXMLSerializable && (!(column.DataType == typeof(object)) && text4 == null) && text3 == null;
				if (text2 != null && XmlConvert.ToBoolean(text2))
				{
					if (!flag && text4 != null && text4.Length > 0)
					{
						obj = SqlUdtStorage.GetStaticNullForUdtType(DataStorage.GetType(text4));
					}
					if (obj == null)
					{
						obj = DBNull.Value;
					}
					if (!this._dataReader.IsEmptyElement)
					{
						while (this._dataReader.Read() && i < this._dataReader.Depth)
						{
						}
					}
					this._dataReader.Read();
				}
				else
				{
					bool flag2 = false;
					if (column.Table.DataSet != null && column.Table.DataSet._udtIsWrapped)
					{
						this._dataReader.Read();
						flag2 = true;
					}
					if (flag)
					{
						if (flag2)
						{
							xmlRootAttribute = new XmlRootAttribute(this._dataReader.LocalName);
							xmlRootAttribute.Namespace = this._dataReader.NamespaceURI;
						}
						else
						{
							xmlRootAttribute = new XmlRootAttribute(column.EncodedColumnName);
							xmlRootAttribute.Namespace = column.Namespace;
						}
					}
					obj = column.ConvertXmlToObject(this._dataReader, xmlRootAttribute);
					if (flag2)
					{
						this._dataReader.Read();
					}
				}
				foundColumns[column.Ordinal] = obj;
				return;
			}
			if (this._dataReader.Read() && i < this._dataReader.Depth)
			{
				while (i < this._dataReader.Depth)
				{
					XmlNodeType nodeType = this._dataReader.NodeType;
					switch (nodeType)
					{
					case XmlNodeType.Element:
					{
						if (this.ProcessXsdSchema())
						{
							continue;
						}
						object columnSchema = this._nodeToSchemaMap.GetColumnSchema(column.Table, this._dataReader, this.FIgnoreNamespace(this._dataReader));
						DataColumn dataColumn = columnSchema as DataColumn;
						if (dataColumn != null)
						{
							if (foundColumns[dataColumn.Ordinal] == null)
							{
								this.LoadColumn(dataColumn, foundColumns);
								continue;
							}
							this._dataReader.Read();
							continue;
						}
						else
						{
							DataTable dataTable = columnSchema as DataTable;
							if (dataTable != null)
							{
								this.LoadTable(dataTable, true);
								continue;
							}
							DataTable tableForNode = this._nodeToSchemaMap.GetTableForNode(this._dataReader, this.FIgnoreNamespace(this._dataReader));
							if (tableForNode != null)
							{
								this.LoadTable(tableForNode, false);
								continue;
							}
							this._dataReader.Read();
							continue;
						}
						break;
					}
					case XmlNodeType.Attribute:
						goto IL_369;
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						break;
					case XmlNodeType.EntityReference:
						throw ExceptionBuilder.FoundEntity();
					default:
						if (nodeType - XmlNodeType.Whitespace > 1)
						{
							goto IL_369;
						}
						break;
					}
					if (text.Length != 0)
					{
						this._dataReader.ReadString();
						continue;
					}
					text = this._dataReader.Value;
					StringBuilder stringBuilder = null;
					while (this._dataReader.Read() && i < this._dataReader.Depth && this.IsTextLikeNode(this._dataReader.NodeType))
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(text);
						}
						stringBuilder.Append(this._dataReader.Value);
					}
					if (stringBuilder != null)
					{
						text = stringBuilder.ToString();
						continue;
					}
					continue;
					IL_369:
					this._dataReader.Read();
				}
				this._dataReader.Read();
			}
			if (text.Length == 0 && text2 != null && XmlConvert.ToBoolean(text2))
			{
				foundColumns[column.Ordinal] = DBNull.Value;
				return;
			}
			foundColumns[column.Ordinal] = column.ConvertXmlToObject(text);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0004E2E0 File Offset: 0x0004C4E0
		private bool ProcessXsdSchema()
		{
			if (this._dataReader.LocalName == this._XSD_SCHEMA && this._dataReader.NamespaceURI == this._XSDNS)
			{
				if (this._ignoreSchema)
				{
					this._dataReader.Skip();
				}
				else if (this._isTableLevel)
				{
					this._dataTable.ReadXSDSchema(this._dataReader, false);
					this._nodeToSchemaMap = new XmlToDatasetMap(this._dataReader.NameTable, this._dataTable);
				}
				else
				{
					this._dataSet.ReadXSDSchema(this._dataReader, false);
					this._nodeToSchemaMap = new XmlToDatasetMap(this._dataReader.NameTable, this._dataSet);
				}
			}
			else
			{
				if ((this._dataReader.LocalName != this._XDR_SCHEMA || this._dataReader.NamespaceURI != this._XDRNS) && (this._dataReader.LocalName != this._SQL_SYNC || this._dataReader.NamespaceURI != this._UPDGNS))
				{
					return false;
				}
				this._dataReader.Skip();
			}
			return true;
		}

		// Token: 0x04000A7A RID: 2682
		private DataSet _dataSet;

		// Token: 0x04000A7B RID: 2683
		private XmlToDatasetMap _nodeToSchemaMap;

		// Token: 0x04000A7C RID: 2684
		private Hashtable _nodeToRowMap;

		// Token: 0x04000A7D RID: 2685
		private Stack _childRowsStack;

		// Token: 0x04000A7E RID: 2686
		private Hashtable _htableExcludedNS;

		// Token: 0x04000A7F RID: 2687
		private bool _fIsXdr;

		// Token: 0x04000A80 RID: 2688
		internal bool _isDiffgram;

		// Token: 0x04000A81 RID: 2689
		private XmlElement _topMostNode;

		// Token: 0x04000A82 RID: 2690
		private bool _ignoreSchema;

		// Token: 0x04000A83 RID: 2691
		private DataTable _dataTable;

		// Token: 0x04000A84 RID: 2692
		private bool _isTableLevel;

		// Token: 0x04000A85 RID: 2693
		private bool _fromInference;

		// Token: 0x04000A86 RID: 2694
		private XmlReader _dataReader;

		// Token: 0x04000A87 RID: 2695
		private object _XSD_XMLNS_NS;

		// Token: 0x04000A88 RID: 2696
		private object _XDR_SCHEMA;

		// Token: 0x04000A89 RID: 2697
		private object _XDRNS;

		// Token: 0x04000A8A RID: 2698
		private object _SQL_SYNC;

		// Token: 0x04000A8B RID: 2699
		private object _UPDGNS;

		// Token: 0x04000A8C RID: 2700
		private object _XSD_SCHEMA;

		// Token: 0x04000A8D RID: 2701
		private object _XSDNS;

		// Token: 0x04000A8E RID: 2702
		private object _DFFNS;

		// Token: 0x04000A8F RID: 2703
		private object _MSDNS;

		// Token: 0x04000A90 RID: 2704
		private object _DIFFID;

		// Token: 0x04000A91 RID: 2705
		private object _HASCHANGES;

		// Token: 0x04000A92 RID: 2706
		private object _ROWORDER;
	}
}
