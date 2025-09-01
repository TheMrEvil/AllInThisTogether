using System;

namespace System.Data
{
	// Token: 0x020000B7 RID: 183
	internal readonly struct DataKey
	{
		// Token: 0x06000B13 RID: 2835 RVA: 0x0002E150 File Offset: 0x0002C350
		internal DataKey(DataColumn[] columns, bool copyColumns)
		{
			if (columns == null)
			{
				throw ExceptionBuilder.ArgumentNull("columns");
			}
			if (columns.Length == 0)
			{
				throw ExceptionBuilder.KeyNoColumns();
			}
			if (columns.Length > 32)
			{
				throw ExceptionBuilder.KeyTooManyColumns(32);
			}
			for (int i = 0; i < columns.Length; i++)
			{
				if (columns[i] == null)
				{
					throw ExceptionBuilder.ArgumentNull("column");
				}
			}
			for (int j = 0; j < columns.Length; j++)
			{
				for (int k = 0; k < j; k++)
				{
					if (columns[j] == columns[k])
					{
						throw ExceptionBuilder.KeyDuplicateColumns(columns[j].ColumnName);
					}
				}
			}
			if (copyColumns)
			{
				this._columns = new DataColumn[columns.Length];
				for (int l = 0; l < columns.Length; l++)
				{
					this._columns[l] = columns[l];
				}
			}
			else
			{
				this._columns = columns;
			}
			this.CheckState();
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0002E20B File Offset: 0x0002C40B
		internal DataColumn[] ColumnsReference
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0002E213 File Offset: 0x0002C413
		internal bool HasValue
		{
			get
			{
				return this._columns != null;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0002E21E File Offset: 0x0002C41E
		internal DataTable Table
		{
			get
			{
				return this._columns[0].Table;
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0002E230 File Offset: 0x0002C430
		internal void CheckState()
		{
			DataTable table = this._columns[0].Table;
			if (table == null)
			{
				throw ExceptionBuilder.ColumnNotInAnyTable();
			}
			for (int i = 1; i < this._columns.Length; i++)
			{
				if (this._columns[i].Table == null)
				{
					throw ExceptionBuilder.ColumnNotInAnyTable();
				}
				if (this._columns[i].Table != table)
				{
					throw ExceptionBuilder.KeyTableMismatch();
				}
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002E292 File Offset: 0x0002C492
		internal bool ColumnsEqual(DataKey key)
		{
			return DataKey.ColumnsEqual(this._columns, key._columns);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0002E2A8 File Offset: 0x0002C4A8
		internal static bool ColumnsEqual(DataColumn[] column1, DataColumn[] column2)
		{
			if (column1 == column2)
			{
				return true;
			}
			if (column1 == null || column2 == null)
			{
				return false;
			}
			if (column1.Length != column2.Length)
			{
				return false;
			}
			for (int i = 0; i < column1.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < column2.Length; j++)
				{
					if (column1[i].Equals(column2[j]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0002E304 File Offset: 0x0002C504
		internal bool ContainsColumn(DataColumn column)
		{
			for (int i = 0; i < this._columns.Length; i++)
			{
				if (column == this._columns[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0002E332 File Offset: 0x0002C532
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002E344 File Offset: 0x0002C544
		public override bool Equals(object value)
		{
			return this.Equals((DataKey)value);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0002E354 File Offset: 0x0002C554
		internal bool Equals(DataKey value)
		{
			DataColumn[] columns = this._columns;
			DataColumn[] columns2 = value._columns;
			if (columns == columns2)
			{
				return true;
			}
			if (columns == null || columns2 == null)
			{
				return false;
			}
			if (columns.Length != columns2.Length)
			{
				return false;
			}
			for (int i = 0; i < columns.Length; i++)
			{
				if (!columns[i].Equals(columns2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0002E3A8 File Offset: 0x0002C5A8
		internal string[] GetColumnNames()
		{
			string[] array = new string[this._columns.Length];
			for (int i = 0; i < this._columns.Length; i++)
			{
				array[i] = this._columns[i].ColumnName;
			}
			return array;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0002E3E8 File Offset: 0x0002C5E8
		internal IndexField[] GetIndexDesc()
		{
			IndexField[] array = new IndexField[this._columns.Length];
			for (int i = 0; i < this._columns.Length; i++)
			{
				array[i] = new IndexField(this._columns[i], false);
			}
			return array;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0002E42C File Offset: 0x0002C62C
		internal object[] GetKeyValues(int record)
		{
			object[] array = new object[this._columns.Length];
			for (int i = 0; i < this._columns.Length; i++)
			{
				array[i] = this._columns[i][record];
			}
			return array;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0002E46C File Offset: 0x0002C66C
		internal Index GetSortIndex()
		{
			return this.GetSortIndex(DataViewRowState.CurrentRows);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0002E478 File Offset: 0x0002C678
		internal Index GetSortIndex(DataViewRowState recordStates)
		{
			IndexField[] indexDesc = this.GetIndexDesc();
			return this._columns[0].Table.GetIndex(indexDesc, recordStates, null);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0002E4A4 File Offset: 0x0002C6A4
		internal bool RecordsEqual(int record1, int record2)
		{
			for (int i = 0; i < this._columns.Length; i++)
			{
				if (this._columns[i].Compare(record1, record2) != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0002E4D8 File Offset: 0x0002C6D8
		internal DataColumn[] ToArray()
		{
			DataColumn[] array = new DataColumn[this._columns.Length];
			for (int i = 0; i < this._columns.Length; i++)
			{
				array[i] = this._columns[i];
			}
			return array;
		}

		// Token: 0x040007A4 RID: 1956
		private const int maxColumns = 32;

		// Token: 0x040007A5 RID: 1957
		private readonly DataColumn[] _columns;
	}
}
