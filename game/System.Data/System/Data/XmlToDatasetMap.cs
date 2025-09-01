using System;
using System.Collections;
using System.Xml;

namespace System.Data
{
	// Token: 0x02000144 RID: 324
	internal sealed class XmlToDatasetMap
	{
		// Token: 0x06001152 RID: 4434 RVA: 0x0004E3F8 File Offset: 0x0004C5F8
		public XmlToDatasetMap(DataSet dataSet, XmlNameTable nameTable)
		{
			this.BuildIdentityMap(dataSet, nameTable);
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0004E408 File Offset: 0x0004C608
		public XmlToDatasetMap(XmlNameTable nameTable, DataSet dataSet)
		{
			this.BuildIdentityMap(nameTable, dataSet);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x0004E418 File Offset: 0x0004C618
		public XmlToDatasetMap(DataTable dataTable, XmlNameTable nameTable)
		{
			this.BuildIdentityMap(dataTable, nameTable);
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0004E428 File Offset: 0x0004C628
		public XmlToDatasetMap(XmlNameTable nameTable, DataTable dataTable)
		{
			this.BuildIdentityMap(nameTable, dataTable);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0004E438 File Offset: 0x0004C638
		internal static bool IsMappedColumn(DataColumn c)
		{
			return c.ColumnMapping != MappingType.Hidden;
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x0004E448 File Offset: 0x0004C648
		private XmlToDatasetMap.TableSchemaInfo AddTableSchema(DataTable table, XmlNameTable nameTable)
		{
			string text = nameTable.Get(table.EncodedTableName);
			string namespaceURI = nameTable.Get(table.Namespace);
			if (text == null)
			{
				return null;
			}
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = new XmlToDatasetMap.TableSchemaInfo(table);
			this._tableSchemaMap[new XmlToDatasetMap.XmlNodeIdentety(text, namespaceURI)] = tableSchemaInfo;
			return tableSchemaInfo;
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0004E490 File Offset: 0x0004C690
		private XmlToDatasetMap.TableSchemaInfo AddTableSchema(XmlNameTable nameTable, DataTable table)
		{
			string encodedTableName = table.EncodedTableName;
			string text = nameTable.Get(encodedTableName);
			if (text == null)
			{
				text = nameTable.Add(encodedTableName);
			}
			table._encodedTableName = text;
			string text2 = nameTable.Get(table.Namespace);
			if (text2 == null)
			{
				text2 = nameTable.Add(table.Namespace);
			}
			else if (table._tableNamespace != null)
			{
				table._tableNamespace = text2;
			}
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = new XmlToDatasetMap.TableSchemaInfo(table);
			this._tableSchemaMap[new XmlToDatasetMap.XmlNodeIdentety(text, text2)] = tableSchemaInfo;
			return tableSchemaInfo;
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0004E508 File Offset: 0x0004C708
		private bool AddColumnSchema(DataColumn col, XmlNameTable nameTable, XmlToDatasetMap.XmlNodeIdHashtable columns)
		{
			string text = nameTable.Get(col.EncodedColumnName);
			string namespaceURI = nameTable.Get(col.Namespace);
			if (text == null)
			{
				return false;
			}
			XmlToDatasetMap.XmlNodeIdentety key = new XmlToDatasetMap.XmlNodeIdentety(text, namespaceURI);
			columns[key] = col;
			if (col.ColumnName.StartsWith("xml", StringComparison.OrdinalIgnoreCase))
			{
				this.HandleSpecialColumn(col, nameTable, columns);
			}
			return true;
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0004E564 File Offset: 0x0004C764
		private bool AddColumnSchema(XmlNameTable nameTable, DataColumn col, XmlToDatasetMap.XmlNodeIdHashtable columns)
		{
			string array = XmlConvert.EncodeLocalName(col.ColumnName);
			string text = nameTable.Get(array);
			if (text == null)
			{
				text = nameTable.Add(array);
			}
			col._encodedColumnName = text;
			string text2 = nameTable.Get(col.Namespace);
			if (text2 == null)
			{
				text2 = nameTable.Add(col.Namespace);
			}
			else if (col._columnUri != null)
			{
				col._columnUri = text2;
			}
			XmlToDatasetMap.XmlNodeIdentety key = new XmlToDatasetMap.XmlNodeIdentety(text, text2);
			columns[key] = col;
			if (col.ColumnName.StartsWith("xml", StringComparison.OrdinalIgnoreCase))
			{
				this.HandleSpecialColumn(col, nameTable, columns);
			}
			return true;
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0004E5F4 File Offset: 0x0004C7F4
		private void BuildIdentityMap(DataSet dataSet, XmlNameTable nameTable)
		{
			this._tableSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(dataSet.Tables.Count);
			foreach (object obj in dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = this.AddTableSchema(dataTable, nameTable);
				if (tableSchemaInfo != null)
				{
					foreach (object obj2 in dataTable.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj2;
						if (XmlToDatasetMap.IsMappedColumn(dataColumn))
						{
							this.AddColumnSchema(dataColumn, nameTable, tableSchemaInfo.ColumnsSchemaMap);
						}
					}
				}
			}
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0004E6CC File Offset: 0x0004C8CC
		private void BuildIdentityMap(XmlNameTable nameTable, DataSet dataSet)
		{
			this._tableSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(dataSet.Tables.Count);
			string text = nameTable.Get(dataSet.Namespace);
			if (text == null)
			{
				text = nameTable.Add(dataSet.Namespace);
			}
			dataSet._namespaceURI = text;
			foreach (object obj in dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = this.AddTableSchema(nameTable, dataTable);
				if (tableSchemaInfo != null)
				{
					foreach (object obj2 in dataTable.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj2;
						if (XmlToDatasetMap.IsMappedColumn(dataColumn))
						{
							this.AddColumnSchema(nameTable, dataColumn, tableSchemaInfo.ColumnsSchemaMap);
						}
					}
					foreach (object obj3 in dataTable.ChildRelations)
					{
						DataRelation dataRelation = (DataRelation)obj3;
						if (dataRelation.Nested)
						{
							string array = XmlConvert.EncodeLocalName(dataRelation.ChildTable.TableName);
							string text2 = nameTable.Get(array);
							if (text2 == null)
							{
								text2 = nameTable.Add(array);
							}
							string text3 = nameTable.Get(dataRelation.ChildTable.Namespace);
							if (text3 == null)
							{
								text3 = nameTable.Add(dataRelation.ChildTable.Namespace);
							}
							XmlToDatasetMap.XmlNodeIdentety key = new XmlToDatasetMap.XmlNodeIdentety(text2, text3);
							tableSchemaInfo.ColumnsSchemaMap[key] = dataRelation.ChildTable;
						}
					}
				}
			}
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0004E8C4 File Offset: 0x0004CAC4
		private void BuildIdentityMap(DataTable dataTable, XmlNameTable nameTable)
		{
			this._tableSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(1);
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = this.AddTableSchema(dataTable, nameTable);
			if (tableSchemaInfo != null)
			{
				foreach (object obj in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					if (XmlToDatasetMap.IsMappedColumn(dataColumn))
					{
						this.AddColumnSchema(dataColumn, nameTable, tableSchemaInfo.ColumnsSchemaMap);
					}
				}
			}
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0004E948 File Offset: 0x0004CB48
		private void BuildIdentityMap(XmlNameTable nameTable, DataTable dataTable)
		{
			ArrayList selfAndDescendants = this.GetSelfAndDescendants(dataTable);
			this._tableSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(selfAndDescendants.Count);
			foreach (object obj in selfAndDescendants)
			{
				DataTable dataTable2 = (DataTable)obj;
				XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = this.AddTableSchema(nameTable, dataTable2);
				if (tableSchemaInfo != null)
				{
					foreach (object obj2 in dataTable2.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj2;
						if (XmlToDatasetMap.IsMappedColumn(dataColumn))
						{
							this.AddColumnSchema(nameTable, dataColumn, tableSchemaInfo.ColumnsSchemaMap);
						}
					}
					foreach (object obj3 in dataTable2.ChildRelations)
					{
						DataRelation dataRelation = (DataRelation)obj3;
						if (dataRelation.Nested)
						{
							string array = XmlConvert.EncodeLocalName(dataRelation.ChildTable.TableName);
							string text = nameTable.Get(array);
							if (text == null)
							{
								text = nameTable.Add(array);
							}
							string text2 = nameTable.Get(dataRelation.ChildTable.Namespace);
							if (text2 == null)
							{
								text2 = nameTable.Add(dataRelation.ChildTable.Namespace);
							}
							XmlToDatasetMap.XmlNodeIdentety key = new XmlToDatasetMap.XmlNodeIdentety(text, text2);
							tableSchemaInfo.ColumnsSchemaMap[key] = dataRelation.ChildTable;
						}
					}
				}
			}
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0004EB1C File Offset: 0x0004CD1C
		private ArrayList GetSelfAndDescendants(DataTable dt)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(dt);
			for (int i = 0; i < arrayList.Count; i++)
			{
				foreach (object obj in ((DataTable)arrayList[i]).ChildRelations)
				{
					DataRelation dataRelation = (DataRelation)obj;
					if (!arrayList.Contains(dataRelation.ChildTable))
					{
						arrayList.Add(dataRelation.ChildTable);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0004EBB8 File Offset: 0x0004CDB8
		public object GetColumnSchema(XmlNode node, bool fIgnoreNamespace)
		{
			XmlNode xmlNode = (node.NodeType == XmlNodeType.Attribute) ? ((XmlAttribute)node).OwnerElement : node.ParentNode;
			while (xmlNode != null && xmlNode.NodeType == XmlNodeType.Element)
			{
				XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = (XmlToDatasetMap.TableSchemaInfo)(fIgnoreNamespace ? this._tableSchemaMap[xmlNode.LocalName] : this._tableSchemaMap[xmlNode]);
				xmlNode = xmlNode.ParentNode;
				if (tableSchemaInfo != null)
				{
					if (fIgnoreNamespace)
					{
						return tableSchemaInfo.ColumnsSchemaMap[node.LocalName];
					}
					return tableSchemaInfo.ColumnsSchemaMap[node];
				}
			}
			return null;
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0004EC48 File Offset: 0x0004CE48
		public object GetColumnSchema(DataTable table, XmlReader dataReader, bool fIgnoreNamespace)
		{
			if (this._lastTableSchemaInfo == null || this._lastTableSchemaInfo.TableSchema != table)
			{
				this._lastTableSchemaInfo = (XmlToDatasetMap.TableSchemaInfo)(fIgnoreNamespace ? this._tableSchemaMap[table.EncodedTableName] : this._tableSchemaMap[table]);
			}
			if (fIgnoreNamespace)
			{
				return this._lastTableSchemaInfo.ColumnsSchemaMap[dataReader.LocalName];
			}
			return this._lastTableSchemaInfo.ColumnsSchemaMap[dataReader];
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0004ECC4 File Offset: 0x0004CEC4
		public object GetSchemaForNode(XmlNode node, bool fIgnoreNamespace)
		{
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = null;
			if (node.NodeType == XmlNodeType.Element)
			{
				tableSchemaInfo = (XmlToDatasetMap.TableSchemaInfo)(fIgnoreNamespace ? this._tableSchemaMap[node.LocalName] : this._tableSchemaMap[node]);
			}
			if (tableSchemaInfo != null)
			{
				return tableSchemaInfo.TableSchema;
			}
			return this.GetColumnSchema(node, fIgnoreNamespace);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0004ED18 File Offset: 0x0004CF18
		public DataTable GetTableForNode(XmlReader node, bool fIgnoreNamespace)
		{
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = (XmlToDatasetMap.TableSchemaInfo)(fIgnoreNamespace ? this._tableSchemaMap[node.LocalName] : this._tableSchemaMap[node]);
			if (tableSchemaInfo != null)
			{
				this._lastTableSchemaInfo = tableSchemaInfo;
				return this._lastTableSchemaInfo.TableSchema;
			}
			return null;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0004ED64 File Offset: 0x0004CF64
		private void HandleSpecialColumn(DataColumn col, XmlNameTable nameTable, XmlToDatasetMap.XmlNodeIdHashtable columns)
		{
			string text;
			if ('x' == col.ColumnName[0])
			{
				text = "_x0078_";
			}
			else
			{
				text = "_x0058_";
			}
			text += col.ColumnName.Substring(1);
			if (nameTable.Get(text) == null)
			{
				nameTable.Add(text);
			}
			string namespaceURI = nameTable.Get(col.Namespace);
			XmlToDatasetMap.XmlNodeIdentety key = new XmlToDatasetMap.XmlNodeIdentety(text, namespaceURI);
			columns[key] = col;
		}

		// Token: 0x04000B58 RID: 2904
		private XmlToDatasetMap.XmlNodeIdHashtable _tableSchemaMap;

		// Token: 0x04000B59 RID: 2905
		private XmlToDatasetMap.TableSchemaInfo _lastTableSchemaInfo;

		// Token: 0x02000145 RID: 325
		private sealed class XmlNodeIdentety
		{
			// Token: 0x06001165 RID: 4453 RVA: 0x0004EDD0 File Offset: 0x0004CFD0
			public XmlNodeIdentety(string localName, string namespaceURI)
			{
				this.LocalName = localName;
				this.NamespaceURI = namespaceURI;
			}

			// Token: 0x06001166 RID: 4454 RVA: 0x0004EDE6 File Offset: 0x0004CFE6
			public override int GetHashCode()
			{
				return this.LocalName.GetHashCode();
			}

			// Token: 0x06001167 RID: 4455 RVA: 0x0004EDF4 File Offset: 0x0004CFF4
			public override bool Equals(object obj)
			{
				XmlToDatasetMap.XmlNodeIdentety xmlNodeIdentety = (XmlToDatasetMap.XmlNodeIdentety)obj;
				return string.Equals(this.LocalName, xmlNodeIdentety.LocalName, StringComparison.OrdinalIgnoreCase) && string.Equals(this.NamespaceURI, xmlNodeIdentety.NamespaceURI, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x04000B5A RID: 2906
			public string LocalName;

			// Token: 0x04000B5B RID: 2907
			public string NamespaceURI;
		}

		// Token: 0x02000146 RID: 326
		internal sealed class XmlNodeIdHashtable : Hashtable
		{
			// Token: 0x06001168 RID: 4456 RVA: 0x0004EE30 File Offset: 0x0004D030
			public XmlNodeIdHashtable(int capacity) : base(capacity)
			{
			}

			// Token: 0x170002E6 RID: 742
			public object this[XmlNode node]
			{
				get
				{
					this._id.LocalName = node.LocalName;
					this._id.NamespaceURI = node.NamespaceURI;
					return this[this._id];
				}
			}

			// Token: 0x170002E7 RID: 743
			public object this[XmlReader dataReader]
			{
				get
				{
					this._id.LocalName = dataReader.LocalName;
					this._id.NamespaceURI = dataReader.NamespaceURI;
					return this[this._id];
				}
			}

			// Token: 0x170002E8 RID: 744
			public object this[DataTable table]
			{
				get
				{
					this._id.LocalName = table.EncodedTableName;
					this._id.NamespaceURI = table.Namespace;
					return this[this._id];
				}
			}

			// Token: 0x170002E9 RID: 745
			public object this[string name]
			{
				get
				{
					this._id.LocalName = name;
					this._id.NamespaceURI = string.Empty;
					return this[this._id];
				}
			}

			// Token: 0x04000B5C RID: 2908
			private XmlToDatasetMap.XmlNodeIdentety _id = new XmlToDatasetMap.XmlNodeIdentety(string.Empty, string.Empty);
		}

		// Token: 0x02000147 RID: 327
		private sealed class TableSchemaInfo
		{
			// Token: 0x0600116D RID: 4461 RVA: 0x0004EF08 File Offset: 0x0004D108
			public TableSchemaInfo(DataTable tableSchema)
			{
				this.TableSchema = tableSchema;
				this.ColumnsSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(tableSchema.Columns.Count);
			}

			// Token: 0x04000B5D RID: 2909
			public DataTable TableSchema;

			// Token: 0x04000B5E RID: 2910
			public XmlToDatasetMap.XmlNodeIdHashtable ColumnsSchemaMap;
		}
	}
}
