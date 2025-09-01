using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data
{
	// Token: 0x0200013B RID: 315
	internal sealed class XMLDiffLoader
	{
		// Token: 0x060010E7 RID: 4327 RVA: 0x00047358 File Offset: 0x00045558
		internal void LoadDiffGram(DataSet ds, XmlReader dataTextReader)
		{
			XmlReader xmlReader = DataTextReader.CreateReader(dataTextReader);
			this._dataSet = ds;
			while (xmlReader.LocalName == "before")
			{
				if (!(xmlReader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1"))
				{
					break;
				}
				this.ProcessDiffs(ds, xmlReader);
				xmlReader.Read();
			}
			while (xmlReader.LocalName == "errors" && xmlReader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
			{
				this.ProcessErrors(ds, xmlReader);
				xmlReader.Read();
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x000473E0 File Offset: 0x000455E0
		private void CreateTablesHierarchy(DataTable dt)
		{
			foreach (object obj in dt.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (!this._tables.Contains(dataRelation.ChildTable))
				{
					this._tables.Add(dataRelation.ChildTable);
					this.CreateTablesHierarchy(dataRelation.ChildTable);
				}
			}
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00047464 File Offset: 0x00045664
		internal void LoadDiffGram(DataTable dt, XmlReader dataTextReader)
		{
			XmlReader xmlReader = DataTextReader.CreateReader(dataTextReader);
			this._dataTable = dt;
			this._tables = new ArrayList();
			this._tables.Add(dt);
			this.CreateTablesHierarchy(dt);
			while (xmlReader.LocalName == "before")
			{
				if (!(xmlReader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1"))
				{
					break;
				}
				this.ProcessDiffs(this._tables, xmlReader);
				xmlReader.Read();
			}
			while (xmlReader.LocalName == "errors" && xmlReader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
			{
				this.ProcessErrors(this._tables, xmlReader);
				xmlReader.Read();
			}
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00047514 File Offset: 0x00045714
		internal void ProcessDiffs(DataSet ds, XmlReader ssync)
		{
			int pos = -1;
			int i = ssync.Depth;
			ssync.Read();
			this.SkipWhitespaces(ssync);
			while (i < ssync.Depth)
			{
				DataTable dataTable = null;
				int depth = ssync.Depth;
				string attribute = ssync.GetAttribute("id", "urn:schemas-microsoft-com:xml-diffgram-v1");
				bool flag = ssync.GetAttribute("hasErrors", "urn:schemas-microsoft-com:xml-diffgram-v1") == "true";
				int num = this.ReadOldRowData(ds, ref dataTable, ref pos, ssync);
				if (num != -1)
				{
					if (dataTable == null)
					{
						throw ExceptionBuilder.DiffgramMissingSQL();
					}
					DataRow dataRow = (DataRow)dataTable.RowDiffId[attribute];
					if (dataRow != null)
					{
						dataRow._oldRecord = num;
						dataTable._recordManager[num] = dataRow;
					}
					else
					{
						dataRow = dataTable.NewEmptyRow();
						dataTable._recordManager[num] = dataRow;
						dataRow._oldRecord = num;
						dataRow._newRecord = num;
						dataTable.Rows.DiffInsertAt(dataRow, pos);
						dataRow.Delete();
						if (flag)
						{
							dataTable.RowDiffId[attribute] = dataRow;
						}
					}
				}
			}
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00047614 File Offset: 0x00045814
		internal void ProcessDiffs(ArrayList tableList, XmlReader ssync)
		{
			int pos = -1;
			int i = ssync.Depth;
			ssync.Read();
			while (i < ssync.Depth)
			{
				DataTable dataTable = null;
				int depth = ssync.Depth;
				string attribute = ssync.GetAttribute("id", "urn:schemas-microsoft-com:xml-diffgram-v1");
				bool flag = ssync.GetAttribute("hasErrors", "urn:schemas-microsoft-com:xml-diffgram-v1") == "true";
				int num = this.ReadOldRowData(this._dataSet, ref dataTable, ref pos, ssync);
				if (num != -1)
				{
					if (dataTable == null)
					{
						throw ExceptionBuilder.DiffgramMissingSQL();
					}
					DataRow dataRow = (DataRow)dataTable.RowDiffId[attribute];
					if (dataRow != null)
					{
						dataRow._oldRecord = num;
						dataTable._recordManager[num] = dataRow;
					}
					else
					{
						dataRow = dataTable.NewEmptyRow();
						dataTable._recordManager[num] = dataRow;
						dataRow._oldRecord = num;
						dataRow._newRecord = num;
						dataTable.Rows.DiffInsertAt(dataRow, pos);
						dataRow.Delete();
						if (flag)
						{
							dataTable.RowDiffId[attribute] = dataRow;
						}
					}
				}
			}
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00047714 File Offset: 0x00045914
		internal void ProcessErrors(DataSet ds, XmlReader ssync)
		{
			int i = ssync.Depth;
			ssync.Read();
			while (i < ssync.Depth)
			{
				DataTable table = ds.Tables.GetTable(XmlConvert.DecodeName(ssync.LocalName), ssync.NamespaceURI);
				if (table == null)
				{
					throw ExceptionBuilder.DiffgramMissingSQL();
				}
				string attribute = ssync.GetAttribute("id", "urn:schemas-microsoft-com:xml-diffgram-v1");
				DataRow dataRow = (DataRow)table.RowDiffId[attribute];
				string attribute2 = ssync.GetAttribute("Error", "urn:schemas-microsoft-com:xml-diffgram-v1");
				if (attribute2 != null)
				{
					dataRow.RowError = attribute2;
				}
				int j = ssync.Depth;
				ssync.Read();
				while (j < ssync.Depth)
				{
					if (XmlNodeType.Element == ssync.NodeType)
					{
						DataColumn column = table.Columns[XmlConvert.DecodeName(ssync.LocalName), ssync.NamespaceURI];
						string attribute3 = ssync.GetAttribute("Error", "urn:schemas-microsoft-com:xml-diffgram-v1");
						dataRow.SetColumnError(column, attribute3);
					}
					ssync.Read();
				}
				while (ssync.NodeType == XmlNodeType.EndElement && i < ssync.Depth)
				{
					ssync.Read();
				}
			}
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00047828 File Offset: 0x00045A28
		internal void ProcessErrors(ArrayList dt, XmlReader ssync)
		{
			int i = ssync.Depth;
			ssync.Read();
			while (i < ssync.Depth)
			{
				DataTable table = this.GetTable(XmlConvert.DecodeName(ssync.LocalName), ssync.NamespaceURI);
				if (table == null)
				{
					throw ExceptionBuilder.DiffgramMissingSQL();
				}
				string attribute = ssync.GetAttribute("id", "urn:schemas-microsoft-com:xml-diffgram-v1");
				DataRow dataRow = (DataRow)table.RowDiffId[attribute];
				if (dataRow == null)
				{
					for (int j = 0; j < dt.Count; j++)
					{
						dataRow = (DataRow)((DataTable)dt[j]).RowDiffId[attribute];
						if (dataRow != null)
						{
							table = dataRow.Table;
							break;
						}
					}
				}
				string attribute2 = ssync.GetAttribute("Error", "urn:schemas-microsoft-com:xml-diffgram-v1");
				if (attribute2 != null)
				{
					dataRow.RowError = attribute2;
				}
				int k = ssync.Depth;
				ssync.Read();
				while (k < ssync.Depth)
				{
					if (XmlNodeType.Element == ssync.NodeType)
					{
						DataColumn column = table.Columns[XmlConvert.DecodeName(ssync.LocalName), ssync.NamespaceURI];
						string attribute3 = ssync.GetAttribute("Error", "urn:schemas-microsoft-com:xml-diffgram-v1");
						dataRow.SetColumnError(column, attribute3);
					}
					ssync.Read();
				}
				while (ssync.NodeType == XmlNodeType.EndElement && i < ssync.Depth)
				{
					ssync.Read();
				}
			}
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0004797C File Offset: 0x00045B7C
		private DataTable GetTable(string tableName, string ns)
		{
			if (this._tables == null)
			{
				return this._dataSet.Tables.GetTable(tableName, ns);
			}
			if (this._tables.Count == 0)
			{
				return (DataTable)this._tables[0];
			}
			for (int i = 0; i < this._tables.Count; i++)
			{
				DataTable dataTable = (DataTable)this._tables[i];
				if (string.Equals(dataTable.TableName, tableName, StringComparison.Ordinal) && string.Equals(dataTable.Namespace, ns, StringComparison.Ordinal))
				{
					return dataTable;
				}
			}
			return null;
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00047A0C File Offset: 0x00045C0C
		private int ReadOldRowData(DataSet ds, ref DataTable table, ref int pos, XmlReader row)
		{
			if (ds != null)
			{
				table = ds.Tables.GetTable(XmlConvert.DecodeName(row.LocalName), row.NamespaceURI);
			}
			else
			{
				table = this.GetTable(XmlConvert.DecodeName(row.LocalName), row.NamespaceURI);
			}
			if (table == null)
			{
				row.Skip();
				return -1;
			}
			int depth = row.Depth;
			if (table == null)
			{
				throw ExceptionBuilder.DiffgramMissingTable(XmlConvert.DecodeName(row.LocalName));
			}
			string attribute = row.GetAttribute("rowOrder", "urn:schemas-microsoft-com:xml-msdata");
			if (!string.IsNullOrEmpty(attribute))
			{
				pos = (int)Convert.ChangeType(attribute, typeof(int), null);
			}
			int num = table.NewRecord();
			foreach (object obj in table.Columns)
			{
				((DataColumn)obj)[num] = DBNull.Value;
			}
			foreach (object obj2 in table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj2;
				if (dataColumn.ColumnMapping != MappingType.Element && dataColumn.ColumnMapping != MappingType.SimpleContent)
				{
					if (dataColumn.ColumnMapping == MappingType.Hidden)
					{
						attribute = row.GetAttribute("hidden" + dataColumn.EncodedColumnName, "urn:schemas-microsoft-com:xml-msdata");
					}
					else
					{
						attribute = row.GetAttribute(dataColumn.EncodedColumnName, dataColumn.Namespace);
					}
					if (attribute != null)
					{
						dataColumn[num] = dataColumn.ConvertXmlToObject(attribute);
					}
				}
			}
			row.Read();
			this.SkipWhitespaces(row);
			int depth2 = row.Depth;
			if (depth2 <= depth)
			{
				if (depth2 == depth && row.NodeType == XmlNodeType.EndElement)
				{
					row.Read();
					this.SkipWhitespaces(row);
				}
				return num;
			}
			if (table.XmlText != null)
			{
				DataColumn xmlText = table.XmlText;
				xmlText[num] = xmlText.ConvertXmlToObject(row.ReadString());
			}
			else
			{
				while (row.Depth > depth)
				{
					string text = XmlConvert.DecodeName(row.LocalName);
					string namespaceURI = row.NamespaceURI;
					DataColumn dataColumn2 = table.Columns[text, namespaceURI];
					if (dataColumn2 == null)
					{
						while (row.NodeType != XmlNodeType.EndElement && row.LocalName != text && row.NamespaceURI != namespaceURI)
						{
							row.Read();
						}
						row.Read();
					}
					else if (dataColumn2.IsCustomType)
					{
						bool flag = dataColumn2.DataType == typeof(object) || row.GetAttribute("InstanceType", "urn:schemas-microsoft-com:xml-msdata") != null || row.GetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance") != null;
						bool flag2 = false;
						if (dataColumn2.Table.DataSet != null && dataColumn2.Table.DataSet._udtIsWrapped)
						{
							row.Read();
							flag2 = true;
						}
						XmlRootAttribute xmlRootAttribute = null;
						if (!flag && !dataColumn2.ImplementsIXMLSerializable)
						{
							if (flag2)
							{
								xmlRootAttribute = new XmlRootAttribute(row.LocalName);
								xmlRootAttribute.Namespace = row.NamespaceURI;
							}
							else
							{
								xmlRootAttribute = new XmlRootAttribute(dataColumn2.EncodedColumnName);
								xmlRootAttribute.Namespace = dataColumn2.Namespace;
							}
						}
						dataColumn2[num] = dataColumn2.ConvertXmlToObject(row, xmlRootAttribute);
						if (flag2)
						{
							row.Read();
						}
					}
					else
					{
						int depth3 = row.Depth;
						row.Read();
						if (row.Depth > depth3)
						{
							if (row.NodeType == XmlNodeType.Text || row.NodeType == XmlNodeType.Whitespace || row.NodeType == XmlNodeType.SignificantWhitespace)
							{
								string s = row.ReadString();
								dataColumn2[num] = dataColumn2.ConvertXmlToObject(s);
								row.Read();
							}
						}
						else if (dataColumn2.DataType == typeof(string))
						{
							dataColumn2[num] = string.Empty;
						}
					}
				}
			}
			row.Read();
			this.SkipWhitespaces(row);
			return num;
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00047E38 File Offset: 0x00046038
		internal void SkipWhitespaces(XmlReader reader)
		{
			while (reader.NodeType == XmlNodeType.Whitespace || reader.NodeType == XmlNodeType.SignificantWhitespace)
			{
				reader.Read();
			}
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00003D93 File Offset: 0x00001F93
		public XMLDiffLoader()
		{
		}

		// Token: 0x04000A5E RID: 2654
		private ArrayList _tables;

		// Token: 0x04000A5F RID: 2655
		private DataSet _dataSet;

		// Token: 0x04000A60 RID: 2656
		private DataTable _dataTable;
	}
}
