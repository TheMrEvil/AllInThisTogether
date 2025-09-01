using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data
{
	// Token: 0x0200014C RID: 332
	internal sealed class NewDiffgramGen
	{
		// Token: 0x0600119F RID: 4511 RVA: 0x000532AC File Offset: 0x000514AC
		internal NewDiffgramGen(DataSet ds)
		{
			this._ds = ds;
			this._dt = null;
			this._doc = new XmlDocument();
			for (int i = 0; i < ds.Tables.Count; i++)
			{
				this._tables.Add(ds.Tables[i]);
			}
			this.DoAssignments(this._tables);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00053320 File Offset: 0x00051520
		internal NewDiffgramGen(DataTable dt, bool writeHierarchy)
		{
			this._ds = null;
			this._dt = dt;
			this._doc = new XmlDocument();
			this._tables.Add(dt);
			if (writeHierarchy)
			{
				this._writeHierarchy = true;
				this.CreateTableHierarchy(dt);
			}
			this.DoAssignments(this._tables);
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00053384 File Offset: 0x00051584
		private void CreateTableHierarchy(DataTable dt)
		{
			foreach (object obj in dt.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (!this._tables.Contains(dataRelation.ChildTable))
				{
					this._tables.Add(dataRelation.ChildTable);
					this.CreateTableHierarchy(dataRelation.ChildTable);
				}
			}
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00053408 File Offset: 0x00051608
		private void DoAssignments(ArrayList tables)
		{
			int num = 0;
			for (int i = 0; i < tables.Count; i++)
			{
				num += ((DataTable)tables[i]).Rows.Count;
			}
			this._rowsOrder = new Hashtable(num);
			for (int j = 0; j < tables.Count; j++)
			{
				DataRowCollection rows = ((DataTable)tables[j]).Rows;
				num = rows.Count;
				for (int k = 0; k < num; k++)
				{
					this._rowsOrder[rows[k]] = k;
				}
			}
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x000534A4 File Offset: 0x000516A4
		private bool EmptyData()
		{
			for (int i = 0; i < this._tables.Count; i++)
			{
				if (((DataTable)this._tables[i]).Rows.Count > 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x000534E8 File Offset: 0x000516E8
		internal void Save(XmlWriter xmlw)
		{
			this.Save(xmlw, null);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x000534F4 File Offset: 0x000516F4
		internal void Save(XmlWriter xmlw, DataTable table)
		{
			this._xmlw = DataTextWriter.CreateWriter(xmlw);
			this._xmlw.WriteStartElement("diffgr", "diffgram", "urn:schemas-microsoft-com:xml-diffgram-v1");
			this._xmlw.WriteAttributeString("xmlns", "msdata", null, "urn:schemas-microsoft-com:xml-msdata");
			if (!this.EmptyData())
			{
				if (table != null)
				{
					new XmlDataTreeWriter(table, this._writeHierarchy).SaveDiffgramData(this._xmlw, this._rowsOrder);
				}
				else
				{
					new XmlDataTreeWriter(this._ds).SaveDiffgramData(this._xmlw, this._rowsOrder);
				}
				if (table == null)
				{
					for (int i = 0; i < this._ds.Tables.Count; i++)
					{
						this.GenerateTable(this._ds.Tables[i]);
					}
				}
				else
				{
					for (int j = 0; j < this._tables.Count; j++)
					{
						this.GenerateTable((DataTable)this._tables[j]);
					}
				}
				if (this._fBefore)
				{
					this._xmlw.WriteEndElement();
				}
				if (table == null)
				{
					for (int k = 0; k < this._ds.Tables.Count; k++)
					{
						this.GenerateTableErrors(this._ds.Tables[k]);
					}
				}
				else
				{
					for (int l = 0; l < this._tables.Count; l++)
					{
						this.GenerateTableErrors((DataTable)this._tables[l]);
					}
				}
				if (this._fErrors)
				{
					this._xmlw.WriteEndElement();
				}
			}
			this._xmlw.WriteEndElement();
			this._xmlw.Flush();
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00053690 File Offset: 0x00051890
		private void GenerateTable(DataTable table)
		{
			int count = table.Rows.Count;
			if (count <= 0)
			{
				return;
			}
			for (int i = 0; i < count; i++)
			{
				this.GenerateRow(table.Rows[i]);
			}
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000536CC File Offset: 0x000518CC
		private void GenerateTableErrors(DataTable table)
		{
			int count = table.Rows.Count;
			int count2 = table.Columns.Count;
			if (count <= 0)
			{
				return;
			}
			for (int i = 0; i < count; i++)
			{
				bool flag = false;
				DataRow dataRow = table.Rows[i];
				string prefix = (table.Namespace.Length != 0) ? table.Prefix : string.Empty;
				if (dataRow.HasErrors && dataRow.RowError.Length > 0)
				{
					if (!this._fErrors)
					{
						this._xmlw.WriteStartElement("diffgr", "errors", "urn:schemas-microsoft-com:xml-diffgram-v1");
						this._fErrors = true;
					}
					this._xmlw.WriteStartElement(prefix, dataRow.Table.EncodedTableName, dataRow.Table.Namespace);
					this._xmlw.WriteAttributeString("diffgr", "id", "urn:schemas-microsoft-com:xml-diffgram-v1", dataRow.Table.TableName + dataRow.rowID.ToString(CultureInfo.InvariantCulture));
					this._xmlw.WriteAttributeString("diffgr", "Error", "urn:schemas-microsoft-com:xml-diffgram-v1", dataRow.RowError);
					flag = true;
				}
				if (count2 > 0)
				{
					for (int j = 0; j < count2; j++)
					{
						DataColumn dataColumn = table.Columns[j];
						string columnError = dataRow.GetColumnError(dataColumn);
						string prefix2 = (dataColumn.Namespace.Length != 0) ? dataColumn.Prefix : string.Empty;
						if (columnError != null && columnError.Length != 0)
						{
							if (!flag)
							{
								if (!this._fErrors)
								{
									this._xmlw.WriteStartElement("diffgr", "errors", "urn:schemas-microsoft-com:xml-diffgram-v1");
									this._fErrors = true;
								}
								this._xmlw.WriteStartElement(prefix, dataRow.Table.EncodedTableName, dataRow.Table.Namespace);
								this._xmlw.WriteAttributeString("diffgr", "id", "urn:schemas-microsoft-com:xml-diffgram-v1", dataRow.Table.TableName + dataRow.rowID.ToString(CultureInfo.InvariantCulture));
								flag = true;
							}
							this._xmlw.WriteStartElement(prefix2, dataColumn.EncodedColumnName, dataColumn.Namespace);
							this._xmlw.WriteAttributeString("diffgr", "Error", "urn:schemas-microsoft-com:xml-diffgram-v1", columnError);
							this._xmlw.WriteEndElement();
						}
					}
					if (flag)
					{
						this._xmlw.WriteEndElement();
					}
				}
			}
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00053954 File Offset: 0x00051B54
		private void GenerateRow(DataRow row)
		{
			DataRowState rowState = row.RowState;
			if (rowState == DataRowState.Unchanged || rowState == DataRowState.Added)
			{
				return;
			}
			if (!this._fBefore)
			{
				this._xmlw.WriteStartElement("diffgr", "before", "urn:schemas-microsoft-com:xml-diffgram-v1");
				this._fBefore = true;
			}
			DataTable table = row.Table;
			int count = table.Columns.Count;
			string value = table.TableName + row.rowID.ToString(CultureInfo.InvariantCulture);
			string text = null;
			if (rowState == DataRowState.Deleted && row.Table.NestedParentRelations.Length != 0)
			{
				DataRow nestedParentRow = row.GetNestedParentRow(DataRowVersion.Original);
				if (nestedParentRow != null)
				{
					text = nestedParentRow.Table.TableName + nestedParentRow.rowID.ToString(CultureInfo.InvariantCulture);
				}
			}
			string prefix = (table.Namespace.Length != 0) ? table.Prefix : string.Empty;
			if (table.XmlText != null)
			{
				object obj = row[table.XmlText, DataRowVersion.Original];
			}
			else
			{
				DBNull value2 = DBNull.Value;
			}
			this._xmlw.WriteStartElement(prefix, row.Table.EncodedTableName, row.Table.Namespace);
			this._xmlw.WriteAttributeString("diffgr", "id", "urn:schemas-microsoft-com:xml-diffgram-v1", value);
			if (rowState == DataRowState.Deleted && XmlDataTreeWriter.RowHasErrors(row))
			{
				this._xmlw.WriteAttributeString("diffgr", "hasErrors", "urn:schemas-microsoft-com:xml-diffgram-v1", "true");
			}
			if (text != null)
			{
				this._xmlw.WriteAttributeString("diffgr", "parentId", "urn:schemas-microsoft-com:xml-diffgram-v1", text);
			}
			this._xmlw.WriteAttributeString("msdata", "rowOrder", "urn:schemas-microsoft-com:xml-msdata", this._rowsOrder[row].ToString());
			for (int i = 0; i < count; i++)
			{
				if (row.Table.Columns[i].ColumnMapping == MappingType.Attribute || row.Table.Columns[i].ColumnMapping == MappingType.Hidden)
				{
					this.GenerateColumn(row, row.Table.Columns[i], DataRowVersion.Original);
				}
			}
			for (int j = 0; j < count; j++)
			{
				if (row.Table.Columns[j].ColumnMapping == MappingType.Element || row.Table.Columns[j].ColumnMapping == MappingType.SimpleContent)
				{
					this.GenerateColumn(row, row.Table.Columns[j], DataRowVersion.Original);
				}
			}
			this._xmlw.WriteEndElement();
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00053BDC File Offset: 0x00051DDC
		private void GenerateColumn(DataRow row, DataColumn col, DataRowVersion version)
		{
			string columnValueAsString = col.GetColumnValueAsString(row, version);
			if (columnValueAsString == null)
			{
				if (col.ColumnMapping == MappingType.SimpleContent)
				{
					this._xmlw.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				}
				return;
			}
			string prefix = (col.Namespace.Length != 0) ? col.Prefix : string.Empty;
			switch (col.ColumnMapping)
			{
			case MappingType.Element:
			{
				bool flag = true;
				object obj = row[col, version];
				if (!col.IsCustomType || !col.IsValueCustomTypeInstance(obj) || typeof(IXmlSerializable).IsAssignableFrom(obj.GetType()))
				{
					this._xmlw.WriteStartElement(prefix, col.EncodedColumnName, col.Namespace);
					flag = false;
				}
				Type type = obj.GetType();
				if (!col.IsCustomType)
				{
					if ((type == typeof(char) || type == typeof(string)) && XmlDataTreeWriter.PreserveSpace(columnValueAsString))
					{
						this._xmlw.WriteAttributeString("xml", "space", "http://www.w3.org/XML/1998/namespace", "preserve");
					}
					this._xmlw.WriteString(columnValueAsString);
				}
				else if (obj != DBNull.Value && (!col.ImplementsINullable || !DataStorage.IsObjectSqlNull(obj)))
				{
					if (col.IsValueCustomTypeInstance(obj))
					{
						if (!flag && obj.GetType() != col.DataType)
						{
							this._xmlw.WriteAttributeString("msdata", "InstanceType", "urn:schemas-microsoft-com:xml-msdata", DataStorage.GetQualifiedName(type));
						}
						if (!flag)
						{
							col.ConvertObjectToXml(obj, this._xmlw, null);
						}
						else
						{
							if (obj.GetType() != col.DataType)
							{
								throw ExceptionBuilder.PolymorphismNotSupported(type.AssemblyQualifiedName);
							}
							XmlRootAttribute xmlRootAttribute = new XmlRootAttribute(col.EncodedColumnName);
							xmlRootAttribute.Namespace = col.Namespace;
							col.ConvertObjectToXml(obj, this._xmlw, xmlRootAttribute);
						}
					}
					else
					{
						if (type == typeof(Type) || type == typeof(Guid) || type == typeof(char) || DataStorage.IsSqlType(type))
						{
							this._xmlw.WriteAttributeString("msdata", "InstanceType", "urn:schemas-microsoft-com:xml-msdata", type.FullName);
						}
						else if (obj is Type)
						{
							this._xmlw.WriteAttributeString("msdata", "InstanceType", "urn:schemas-microsoft-com:xml-msdata", "Type");
						}
						else
						{
							string value = "xs:" + XmlTreeGen.XmlDataTypeName(type);
							this._xmlw.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", value);
							this._xmlw.WriteAttributeString("xmlns:xs", "http://www.w3.org/2001/XMLSchema");
						}
						if (!DataStorage.IsSqlType(type))
						{
							this._xmlw.WriteString(col.ConvertObjectToXml(obj));
						}
						else
						{
							col.ConvertObjectToXml(obj, this._xmlw, null);
						}
					}
				}
				if (!flag)
				{
					this._xmlw.WriteEndElement();
				}
				return;
			}
			case MappingType.Attribute:
				this._xmlw.WriteAttributeString(prefix, col.EncodedColumnName, col.Namespace, columnValueAsString);
				return;
			case MappingType.SimpleContent:
				this._xmlw.WriteString(columnValueAsString);
				return;
			case MappingType.Hidden:
				this._xmlw.WriteAttributeString("msdata", "hidden" + col.EncodedColumnName, "urn:schemas-microsoft-com:xml-msdata", columnValueAsString);
				return;
			default:
				return;
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00053F36 File Offset: 0x00052136
		internal static string QualifiedName(string prefix, string name)
		{
			if (prefix != null)
			{
				return prefix + ":" + name;
			}
			return name;
		}

		// Token: 0x04000B7F RID: 2943
		internal XmlDocument _doc;

		// Token: 0x04000B80 RID: 2944
		internal DataSet _ds;

		// Token: 0x04000B81 RID: 2945
		internal DataTable _dt;

		// Token: 0x04000B82 RID: 2946
		internal XmlWriter _xmlw;

		// Token: 0x04000B83 RID: 2947
		private bool _fBefore;

		// Token: 0x04000B84 RID: 2948
		private bool _fErrors;

		// Token: 0x04000B85 RID: 2949
		internal Hashtable _rowsOrder;

		// Token: 0x04000B86 RID: 2950
		private ArrayList _tables = new ArrayList();

		// Token: 0x04000B87 RID: 2951
		private bool _writeHierarchy;
	}
}
