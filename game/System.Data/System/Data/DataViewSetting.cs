using System;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Represents the default settings for <see cref="P:System.Data.DataView.ApplyDefaultSort" />, <see cref="P:System.Data.DataView.DataViewManager" />, <see cref="P:System.Data.DataView.RowFilter" />, <see cref="P:System.Data.DataView.RowStateFilter" />, <see cref="P:System.Data.DataView.Sort" />, and <see cref="P:System.Data.DataView.Table" /> for DataViews created from the <see cref="T:System.Data.DataViewManager" />.</summary>
	// Token: 0x020000DC RID: 220
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class DataViewSetting
	{
		// Token: 0x06000DB0 RID: 3504 RVA: 0x000379FF File Offset: 0x00035BFF
		internal DataViewSetting()
		{
		}

		/// <summary>Gets or sets a value indicating whether to use the default sort.</summary>
		/// <returns>
		///   <see langword="true" /> if the default sort is used; otherwise <see langword="false" />.</returns>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x00037A25 File Offset: 0x00035C25
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x00037A2D File Offset: 0x00035C2D
		public bool ApplyDefaultSort
		{
			get
			{
				return this._applyDefaultSort;
			}
			set
			{
				if (this._applyDefaultSort != value)
				{
					this._applyDefaultSort = value;
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewManager" /> that contains this <see cref="T:System.Data.DataViewSetting" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataViewManager" /> object.</returns>
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x00037A3F File Offset: 0x00035C3F
		[Browsable(false)]
		public DataViewManager DataViewManager
		{
			get
			{
				return this._dataViewManager;
			}
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00037A47 File Offset: 0x00035C47
		internal void SetDataViewManager(DataViewManager dataViewManager)
		{
			if (this._dataViewManager != dataViewManager)
			{
				this._dataViewManager = dataViewManager;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> to which the <see cref="T:System.Data.DataViewSetting" /> properties apply.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> object.</returns>
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x00037A59 File Offset: 0x00035C59
		[Browsable(false)]
		public DataTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00037A61 File Offset: 0x00035C61
		internal void SetDataTable(DataTable table)
		{
			if (this._table != table)
			{
				this._table = table;
			}
		}

		/// <summary>Gets or sets the filter to apply in the <see cref="T:System.Data.DataView" />. See <see cref="P:System.Data.DataView.RowFilter" /> for a code sample using RowFilter.</summary>
		/// <returns>A string that contains the filter to apply.</returns>
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x00037A73 File Offset: 0x00035C73
		// (set) Token: 0x06000DB8 RID: 3512 RVA: 0x00037A7B File Offset: 0x00035C7B
		public string RowFilter
		{
			get
			{
				return this._rowFilter;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this._rowFilter != value)
				{
					this._rowFilter = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether to display Current, Deleted, Modified Current, ModifiedOriginal, New, Original, Unchanged, or no rows in the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A value that indicates which rows to display.</returns>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x00037A9C File Offset: 0x00035C9C
		// (set) Token: 0x06000DBA RID: 3514 RVA: 0x00037AA4 File Offset: 0x00035CA4
		public DataViewRowState RowStateFilter
		{
			get
			{
				return this._rowStateFilter;
			}
			set
			{
				if (this._rowStateFilter != value)
				{
					this._rowStateFilter = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating the sort to apply in the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>The sort to apply in the <see cref="T:System.Data.DataView" />.</returns>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x00037AB6 File Offset: 0x00035CB6
		// (set) Token: 0x06000DBC RID: 3516 RVA: 0x00037ABE File Offset: 0x00035CBE
		public string Sort
		{
			get
			{
				return this._sort;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this._sort != value)
				{
					this._sort = value;
				}
			}
		}

		// Token: 0x04000855 RID: 2133
		private DataViewManager _dataViewManager;

		// Token: 0x04000856 RID: 2134
		private DataTable _table;

		// Token: 0x04000857 RID: 2135
		private string _sort = string.Empty;

		// Token: 0x04000858 RID: 2136
		private string _rowFilter = string.Empty;

		// Token: 0x04000859 RID: 2137
		private DataViewRowState _rowStateFilter = DataViewRowState.CurrentRows;

		// Token: 0x0400085A RID: 2138
		private bool _applyDefaultSort;
	}
}
