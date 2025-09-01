using System;
using System.Globalization;

namespace System.Data.Common
{
	// Token: 0x02000379 RID: 889
	internal sealed class DbSchemaRow
	{
		// Token: 0x060029C1 RID: 10689 RVA: 0x000B7678 File Offset: 0x000B5878
		internal static DbSchemaRow[] GetSortedSchemaRows(DataTable dataTable, bool returnProviderSpecificTypes)
		{
			DataColumn dataColumn = dataTable.Columns["SchemaMapping Unsorted Index"];
			if (dataColumn == null)
			{
				dataColumn = new DataColumn("SchemaMapping Unsorted Index", typeof(int));
				dataTable.Columns.Add(dataColumn);
			}
			int count = dataTable.Rows.Count;
			for (int i = 0; i < count; i++)
			{
				dataTable.Rows[i][dataColumn] = i;
			}
			DbSchemaTable schemaTable = new DbSchemaTable(dataTable, returnProviderSpecificTypes);
			DataRow[] array = dataTable.Select(null, "ColumnOrdinal ASC", DataViewRowState.CurrentRows);
			DbSchemaRow[] array2 = new DbSchemaRow[array.Length];
			for (int j = 0; j < array.Length; j++)
			{
				array2[j] = new DbSchemaRow(schemaTable, array[j]);
			}
			return array2;
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x000B7734 File Offset: 0x000B5934
		internal DbSchemaRow(DbSchemaTable schemaTable, DataRow dataRow)
		{
			this._schemaTable = schemaTable;
			this._dataRow = dataRow;
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060029C3 RID: 10691 RVA: 0x000B774A File Offset: 0x000B594A
		internal DataRow DataRow
		{
			get
			{
				return this._dataRow;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060029C4 RID: 10692 RVA: 0x000B7754 File Offset: 0x000B5954
		internal string ColumnName
		{
			get
			{
				object value = this._dataRow[this._schemaTable.ColumnName, DataRowVersion.Default];
				if (!Convert.IsDBNull(value))
				{
					return Convert.ToString(value, CultureInfo.InvariantCulture);
				}
				return string.Empty;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060029C5 RID: 10693 RVA: 0x000B7798 File Offset: 0x000B5998
		internal int Size
		{
			get
			{
				object value = this._dataRow[this._schemaTable.Size, DataRowVersion.Default];
				if (!Convert.IsDBNull(value))
				{
					return Convert.ToInt32(value, CultureInfo.InvariantCulture);
				}
				return 0;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x060029C6 RID: 10694 RVA: 0x000B77D8 File Offset: 0x000B59D8
		internal string BaseColumnName
		{
			get
			{
				if (this._schemaTable.BaseColumnName != null)
				{
					object value = this._dataRow[this._schemaTable.BaseColumnName, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToString(value, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060029C7 RID: 10695 RVA: 0x000B7828 File Offset: 0x000B5A28
		internal string BaseServerName
		{
			get
			{
				if (this._schemaTable.BaseServerName != null)
				{
					object value = this._dataRow[this._schemaTable.BaseServerName, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToString(value, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x000B7878 File Offset: 0x000B5A78
		internal string BaseCatalogName
		{
			get
			{
				if (this._schemaTable.BaseCatalogName != null)
				{
					object value = this._dataRow[this._schemaTable.BaseCatalogName, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToString(value, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060029C9 RID: 10697 RVA: 0x000B78C8 File Offset: 0x000B5AC8
		internal string BaseSchemaName
		{
			get
			{
				if (this._schemaTable.BaseSchemaName != null)
				{
					object value = this._dataRow[this._schemaTable.BaseSchemaName, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToString(value, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060029CA RID: 10698 RVA: 0x000B7918 File Offset: 0x000B5B18
		internal string BaseTableName
		{
			get
			{
				if (this._schemaTable.BaseTableName != null)
				{
					object value = this._dataRow[this._schemaTable.BaseTableName, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToString(value, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060029CB RID: 10699 RVA: 0x000B7968 File Offset: 0x000B5B68
		internal bool IsAutoIncrement
		{
			get
			{
				if (this._schemaTable.IsAutoIncrement != null)
				{
					object value = this._dataRow[this._schemaTable.IsAutoIncrement, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060029CC RID: 10700 RVA: 0x000B79B4 File Offset: 0x000B5BB4
		internal bool IsUnique
		{
			get
			{
				if (this._schemaTable.IsUnique != null)
				{
					object value = this._dataRow[this._schemaTable.IsUnique, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060029CD RID: 10701 RVA: 0x000B7A00 File Offset: 0x000B5C00
		internal bool IsRowVersion
		{
			get
			{
				if (this._schemaTable.IsRowVersion != null)
				{
					object value = this._dataRow[this._schemaTable.IsRowVersion, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060029CE RID: 10702 RVA: 0x000B7A4C File Offset: 0x000B5C4C
		internal bool IsKey
		{
			get
			{
				if (this._schemaTable.IsKey != null)
				{
					object value = this._dataRow[this._schemaTable.IsKey, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060029CF RID: 10703 RVA: 0x000B7A98 File Offset: 0x000B5C98
		internal bool IsExpression
		{
			get
			{
				if (this._schemaTable.IsExpression != null)
				{
					object value = this._dataRow[this._schemaTable.IsExpression, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060029D0 RID: 10704 RVA: 0x000B7AE4 File Offset: 0x000B5CE4
		internal bool IsHidden
		{
			get
			{
				if (this._schemaTable.IsHidden != null)
				{
					object value = this._dataRow[this._schemaTable.IsHidden, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060029D1 RID: 10705 RVA: 0x000B7B30 File Offset: 0x000B5D30
		internal bool IsLong
		{
			get
			{
				if (this._schemaTable.IsLong != null)
				{
					object value = this._dataRow[this._schemaTable.IsLong, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060029D2 RID: 10706 RVA: 0x000B7B7C File Offset: 0x000B5D7C
		internal bool IsReadOnly
		{
			get
			{
				if (this._schemaTable.IsReadOnly != null)
				{
					object value = this._dataRow[this._schemaTable.IsReadOnly, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060029D3 RID: 10707 RVA: 0x000B7BC8 File Offset: 0x000B5DC8
		internal Type DataType
		{
			get
			{
				if (this._schemaTable.DataType != null)
				{
					object obj = this._dataRow[this._schemaTable.DataType, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return (Type)obj;
					}
				}
				return null;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x060029D4 RID: 10708 RVA: 0x000B7C10 File Offset: 0x000B5E10
		internal bool AllowDBNull
		{
			get
			{
				if (this._schemaTable.AllowDBNull != null)
				{
					object value = this._dataRow[this._schemaTable.AllowDBNull, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					}
				}
				return true;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x060029D5 RID: 10709 RVA: 0x000B7C5B File Offset: 0x000B5E5B
		internal int UnsortedIndex
		{
			get
			{
				return (int)this._dataRow[this._schemaTable.UnsortedIndex, DataRowVersion.Default];
			}
		}

		// Token: 0x04001AA3 RID: 6819
		internal const string SchemaMappingUnsortedIndex = "SchemaMapping Unsorted Index";

		// Token: 0x04001AA4 RID: 6820
		private DbSchemaTable _schemaTable;

		// Token: 0x04001AA5 RID: 6821
		private DataRow _dataRow;
	}
}
