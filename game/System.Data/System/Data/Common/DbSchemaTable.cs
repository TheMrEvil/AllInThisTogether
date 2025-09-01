using System;

namespace System.Data.Common
{
	// Token: 0x0200037A RID: 890
	internal sealed class DbSchemaTable
	{
		// Token: 0x060029D6 RID: 10710 RVA: 0x000B7C7D File Offset: 0x000B5E7D
		internal DbSchemaTable(DataTable dataTable, bool returnProviderSpecificTypes)
		{
			this._dataTable = dataTable;
			this._columns = dataTable.Columns;
			this._returnProviderSpecificTypes = returnProviderSpecificTypes;
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060029D7 RID: 10711 RVA: 0x000B7CB1 File Offset: 0x000B5EB1
		internal DataColumn ColumnName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.ColumnName);
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060029D8 RID: 10712 RVA: 0x000B7CBA File Offset: 0x000B5EBA
		internal DataColumn Size
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.ColumnSize);
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060029D9 RID: 10713 RVA: 0x000B7CC3 File Offset: 0x000B5EC3
		internal DataColumn BaseServerName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseServerName);
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x060029DA RID: 10714 RVA: 0x000B7CCC File Offset: 0x000B5ECC
		internal DataColumn BaseColumnName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseColumnName);
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x060029DB RID: 10715 RVA: 0x000B7CD5 File Offset: 0x000B5ED5
		internal DataColumn BaseTableName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseTableName);
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x060029DC RID: 10716 RVA: 0x000B7CDE File Offset: 0x000B5EDE
		internal DataColumn BaseCatalogName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseCatalogName);
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x060029DD RID: 10717 RVA: 0x000B7CE7 File Offset: 0x000B5EE7
		internal DataColumn BaseSchemaName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseSchemaName);
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060029DE RID: 10718 RVA: 0x000B7CF0 File Offset: 0x000B5EF0
		internal DataColumn IsAutoIncrement
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsAutoIncrement);
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060029DF RID: 10719 RVA: 0x000B7CF9 File Offset: 0x000B5EF9
		internal DataColumn IsUnique
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsUnique);
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x060029E0 RID: 10720 RVA: 0x000B7D03 File Offset: 0x000B5F03
		internal DataColumn IsKey
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsKey);
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060029E1 RID: 10721 RVA: 0x000B7D0D File Offset: 0x000B5F0D
		internal DataColumn IsRowVersion
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsRowVersion);
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060029E2 RID: 10722 RVA: 0x000B7D17 File Offset: 0x000B5F17
		internal DataColumn AllowDBNull
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.AllowDBNull);
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x060029E3 RID: 10723 RVA: 0x000B7D21 File Offset: 0x000B5F21
		internal DataColumn IsExpression
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsExpression);
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x060029E4 RID: 10724 RVA: 0x000B7D2B File Offset: 0x000B5F2B
		internal DataColumn IsHidden
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsHidden);
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060029E5 RID: 10725 RVA: 0x000B7D35 File Offset: 0x000B5F35
		internal DataColumn IsLong
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsLong);
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060029E6 RID: 10726 RVA: 0x000B7D3F File Offset: 0x000B5F3F
		internal DataColumn IsReadOnly
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsReadOnly);
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060029E7 RID: 10727 RVA: 0x000B7D49 File Offset: 0x000B5F49
		internal DataColumn UnsortedIndex
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.SchemaMappingUnsortedIndex);
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060029E8 RID: 10728 RVA: 0x000B7D53 File Offset: 0x000B5F53
		internal DataColumn DataType
		{
			get
			{
				if (this._returnProviderSpecificTypes)
				{
					return this.CachedDataColumn(DbSchemaTable.ColumnEnum.ProviderSpecificDataType, DbSchemaTable.ColumnEnum.DataType);
				}
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.DataType);
			}
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x000B7D70 File Offset: 0x000B5F70
		private DataColumn CachedDataColumn(DbSchemaTable.ColumnEnum column)
		{
			return this.CachedDataColumn(column, column);
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x000B7D7C File Offset: 0x000B5F7C
		private DataColumn CachedDataColumn(DbSchemaTable.ColumnEnum column, DbSchemaTable.ColumnEnum column2)
		{
			DataColumn dataColumn = this._columnCache[(int)column];
			if (dataColumn == null)
			{
				int num = this._columns.IndexOf(DbSchemaTable.s_DBCOLUMN_NAME[(int)column]);
				if (-1 == num && column != column2)
				{
					num = this._columns.IndexOf(DbSchemaTable.s_DBCOLUMN_NAME[(int)column2]);
				}
				if (-1 != num)
				{
					dataColumn = this._columns[num];
					this._columnCache[(int)column] = dataColumn;
				}
			}
			return dataColumn;
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000B7DE0 File Offset: 0x000B5FE0
		// Note: this type is marked as 'beforefieldinit'.
		static DbSchemaTable()
		{
		}

		// Token: 0x04001AA6 RID: 6822
		private static readonly string[] s_DBCOLUMN_NAME = new string[]
		{
			SchemaTableColumn.ColumnName,
			SchemaTableColumn.ColumnOrdinal,
			SchemaTableColumn.ColumnSize,
			SchemaTableOptionalColumn.BaseServerName,
			SchemaTableOptionalColumn.BaseCatalogName,
			SchemaTableColumn.BaseColumnName,
			SchemaTableColumn.BaseSchemaName,
			SchemaTableColumn.BaseTableName,
			SchemaTableOptionalColumn.IsAutoIncrement,
			SchemaTableColumn.IsUnique,
			SchemaTableColumn.IsKey,
			SchemaTableOptionalColumn.IsRowVersion,
			SchemaTableColumn.DataType,
			SchemaTableOptionalColumn.ProviderSpecificDataType,
			SchemaTableColumn.AllowDBNull,
			SchemaTableColumn.ProviderType,
			SchemaTableColumn.IsExpression,
			SchemaTableOptionalColumn.IsHidden,
			SchemaTableColumn.IsLong,
			SchemaTableOptionalColumn.IsReadOnly,
			"SchemaMapping Unsorted Index"
		};

		// Token: 0x04001AA7 RID: 6823
		internal DataTable _dataTable;

		// Token: 0x04001AA8 RID: 6824
		private DataColumnCollection _columns;

		// Token: 0x04001AA9 RID: 6825
		private DataColumn[] _columnCache = new DataColumn[DbSchemaTable.s_DBCOLUMN_NAME.Length];

		// Token: 0x04001AAA RID: 6826
		private bool _returnProviderSpecificTypes;

		// Token: 0x0200037B RID: 891
		private enum ColumnEnum
		{
			// Token: 0x04001AAC RID: 6828
			ColumnName,
			// Token: 0x04001AAD RID: 6829
			ColumnOrdinal,
			// Token: 0x04001AAE RID: 6830
			ColumnSize,
			// Token: 0x04001AAF RID: 6831
			BaseServerName,
			// Token: 0x04001AB0 RID: 6832
			BaseCatalogName,
			// Token: 0x04001AB1 RID: 6833
			BaseColumnName,
			// Token: 0x04001AB2 RID: 6834
			BaseSchemaName,
			// Token: 0x04001AB3 RID: 6835
			BaseTableName,
			// Token: 0x04001AB4 RID: 6836
			IsAutoIncrement,
			// Token: 0x04001AB5 RID: 6837
			IsUnique,
			// Token: 0x04001AB6 RID: 6838
			IsKey,
			// Token: 0x04001AB7 RID: 6839
			IsRowVersion,
			// Token: 0x04001AB8 RID: 6840
			DataType,
			// Token: 0x04001AB9 RID: 6841
			ProviderSpecificDataType,
			// Token: 0x04001ABA RID: 6842
			AllowDBNull,
			// Token: 0x04001ABB RID: 6843
			ProviderType,
			// Token: 0x04001ABC RID: 6844
			IsExpression,
			// Token: 0x04001ABD RID: 6845
			IsHidden,
			// Token: 0x04001ABE RID: 6846
			IsLong,
			// Token: 0x04001ABF RID: 6847
			IsReadOnly,
			// Token: 0x04001AC0 RID: 6848
			SchemaMappingUnsortedIndex
		}
	}
}
