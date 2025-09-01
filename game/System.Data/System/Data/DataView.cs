using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace System.Data
{
	/// <summary>Represents a databindable, customized view of a <see cref="T:System.Data.DataTable" /> for sorting, filtering, searching, editing, and navigation. The <see cref="T:System.Data.DataView" /> does not store data, but instead represents a connected view of its corresponding <see cref="T:System.Data.DataTable" />. Changes to the <see cref="T:System.Data.DataView" />'s data will affect the <see cref="T:System.Data.DataTable" />. Changes to the <see cref="T:System.Data.DataTable" />'s data will affect all <see cref="T:System.Data.DataView" />s associated with it.</summary>
	// Token: 0x020000D5 RID: 213
	[DefaultProperty("Table")]
	[DefaultEvent("PositionChanged")]
	public class DataView : MarshalByValueComponent, IBindingListView, IBindingList, IList, ICollection, IEnumerable, ITypedList, ISupportInitializeNotification, ISupportInitialize
	{
		// Token: 0x06000CDD RID: 3293 RVA: 0x000350BC File Offset: 0x000332BC
		internal DataView(DataTable table, bool locked)
		{
			GC.SuppressFinalize(this);
			DataCommonEventSource.Log.Trace<int, int, bool>("<ds.DataView.DataView|INFO> {0}, table={1}, locked={2}", this.ObjectID, (table != null) ? table.ObjectID : 0, locked);
			this._dvListener = new DataViewListener(this);
			this._locked = locked;
			this._table = table;
			this._dvListener.RegisterMetaDataEvents(this._table);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataView" /> class.</summary>
		// Token: 0x06000CDE RID: 3294 RVA: 0x00035188 File Offset: 0x00033388
		public DataView() : this(null)
		{
			this.SetIndex2("", DataViewRowState.CurrentRows, null, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataView" /> class with the specified <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> to add to the <see cref="T:System.Data.DataView" />.</param>
		// Token: 0x06000CDF RID: 3295 RVA: 0x000351A0 File Offset: 0x000333A0
		public DataView(DataTable table) : this(table, false)
		{
			this.SetIndex2("", DataViewRowState.CurrentRows, null, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataView" /> class with the specified <see cref="T:System.Data.DataTable" />, <see cref="P:System.Data.DataView.RowFilter" />, <see cref="P:System.Data.DataView.Sort" />, and <see cref="T:System.Data.DataViewRowState" />.</summary>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> to add to the <see cref="T:System.Data.DataView" />.</param>
		/// <param name="RowFilter">A <see cref="P:System.Data.DataView.RowFilter" /> to apply to the <see cref="T:System.Data.DataView" />.</param>
		/// <param name="Sort">A <see cref="P:System.Data.DataView.Sort" /> to apply to the <see cref="T:System.Data.DataView" />.</param>
		/// <param name="RowState">A <see cref="T:System.Data.DataViewRowState" /> to apply to the <see cref="T:System.Data.DataView" />.</param>
		// Token: 0x06000CE0 RID: 3296 RVA: 0x000351BC File Offset: 0x000333BC
		public DataView(DataTable table, string RowFilter, string Sort, DataViewRowState RowState)
		{
			GC.SuppressFinalize(this);
			DataCommonEventSource.Log.Trace<int, int, string, string, DataViewRowState>("<ds.DataView.DataView|API> {0}, table={1}, RowFilter='{2}', Sort='{3}', RowState={4}", this.ObjectID, (table != null) ? table.ObjectID : 0, RowFilter, Sort, RowState);
			if (table == null)
			{
				throw ExceptionBuilder.CanNotUse();
			}
			this._dvListener = new DataViewListener(this);
			this._locked = false;
			this._table = table;
			this._dvListener.RegisterMetaDataEvents(this._table);
			if ((RowState & ~(DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedCurrent | DataViewRowState.ModifiedOriginal)) != DataViewRowState.None)
			{
				throw ExceptionBuilder.RecordStateRange();
			}
			if ((RowState & DataViewRowState.ModifiedOriginal) != DataViewRowState.None && (RowState & DataViewRowState.ModifiedCurrent) != DataViewRowState.None)
			{
				throw ExceptionBuilder.SetRowStateFilter();
			}
			if (Sort == null)
			{
				Sort = string.Empty;
			}
			if (RowFilter == null)
			{
				RowFilter = string.Empty;
			}
			DataExpression newRowFilter = new DataExpression(table, RowFilter);
			this.SetIndex(Sort, RowState, newRowFilter);
		}

		/// <summary>Sets or gets a value that indicates whether deletes are allowed.</summary>
		/// <returns>
		///   <see langword="true" />, if deletes are allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x000352DB File Offset: 0x000334DB
		// (set) Token: 0x06000CE2 RID: 3298 RVA: 0x000352E3 File Offset: 0x000334E3
		[DefaultValue(true)]
		public bool AllowDelete
		{
			get
			{
				return this._allowDelete;
			}
			set
			{
				if (this._allowDelete != value)
				{
					this._allowDelete = value;
					this.OnListChanged(DataView.s_resetEventArgs);
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether to use the default sort. The default sort is (ascending) by all primary keys as specified by <see cref="P:System.Data.DataTable.PrimaryKey" />.</summary>
		/// <returns>
		///   <see langword="true" />, if the default sort is used; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00035300 File Offset: 0x00033500
		// (set) Token: 0x06000CE4 RID: 3300 RVA: 0x00035308 File Offset: 0x00033508
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		public bool ApplyDefaultSort
		{
			get
			{
				return this._applyDefaultSort;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, bool>("<ds.DataView.set_ApplyDefaultSort|API> {0}, {1}", this.ObjectID, value);
				if (this._applyDefaultSort != value)
				{
					this._comparison = null;
					this._applyDefaultSort = value;
					this.UpdateIndex(true);
					this.OnListChanged(DataView.s_resetEventArgs);
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether edits are allowed.</summary>
		/// <returns>
		///   <see langword="true" />, if edits are allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00035354 File Offset: 0x00033554
		// (set) Token: 0x06000CE6 RID: 3302 RVA: 0x0003535C File Offset: 0x0003355C
		[DefaultValue(true)]
		public bool AllowEdit
		{
			get
			{
				return this._allowEdit;
			}
			set
			{
				if (this._allowEdit != value)
				{
					this._allowEdit = value;
					this.OnListChanged(DataView.s_resetEventArgs);
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether the new rows can be added by using the <see cref="M:System.Data.DataView.AddNew" /> method.</summary>
		/// <returns>
		///   <see langword="true" />, if new rows can be added; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00035379 File Offset: 0x00033579
		// (set) Token: 0x06000CE8 RID: 3304 RVA: 0x00035381 File Offset: 0x00033581
		[DefaultValue(true)]
		public bool AllowNew
		{
			get
			{
				return this._allowNew;
			}
			set
			{
				if (this._allowNew != value)
				{
					this._allowNew = value;
					this.OnListChanged(DataView.s_resetEventArgs);
				}
			}
		}

		/// <summary>Gets the number of records in the <see cref="T:System.Data.DataView" /> after <see cref="P:System.Data.DataView.RowFilter" /> and <see cref="P:System.Data.DataView.RowStateFilter" /> have been applied.</summary>
		/// <returns>The number of records in the <see cref="T:System.Data.DataView" />.</returns>
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x0003539E File Offset: 0x0003359E
		[Browsable(false)]
		public int Count
		{
			get
			{
				return this._rowViewCache.Count;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x000353AB File Offset: 0x000335AB
		private int CountFromIndex
		{
			get
			{
				return ((this._index != null) ? this._index.RecordCount : 0) + ((this._addNewRow != null) ? 1 : 0);
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewManager" /> associated with this view.</summary>
		/// <returns>The <see langword="DataViewManager" /> that created this view. If this is the default <see cref="T:System.Data.DataView" /> for a <see cref="T:System.Data.DataTable" />, the <see langword="DataViewManager" /> property returns the default <see langword="DataViewManager" /> for the <see langword="DataSet" />. Otherwise, if the <see langword="DataView" /> was created without a <see langword="DataViewManager" />, this property is <see langword="null" />.</returns>
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x000353D0 File Offset: 0x000335D0
		[Browsable(false)]
		public DataViewManager DataViewManager
		{
			get
			{
				return this._dataViewManager;
			}
		}

		/// <summary>Gets a value that indicates whether the component is initialized.</summary>
		/// <returns>
		///   <see langword="true" /> to indicate the component has completed initialization; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x000353D8 File Offset: 0x000335D8
		[Browsable(false)]
		public bool IsInitialized
		{
			get
			{
				return !this._fInitInProgress;
			}
		}

		/// <summary>Gets a value that indicates whether the data source is currently open and projecting views of data on the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>
		///   <see langword="true" />, if the source is open; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x000353E3 File Offset: 0x000335E3
		[Browsable(false)]
		protected bool IsOpen
		{
			get
			{
				return this._open;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</returns>
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00006D64 File Offset: 0x00004F64
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the expression used to filter which rows are viewed in the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A string that specifies how rows are to be filtered.</returns>
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x000353EC File Offset: 0x000335EC
		// (set) Token: 0x06000CF0 RID: 3312 RVA: 0x00035414 File Offset: 0x00033614
		[DefaultValue("")]
		public virtual string RowFilter
		{
			get
			{
				DataExpression dataExpression = this._rowFilter as DataExpression;
				if (dataExpression != null)
				{
					return dataExpression.Expression;
				}
				return "";
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataView.set_RowFilter|API> {0}, '{1}'", this.ObjectID, value);
				if (this._fInitInProgress)
				{
					this._delayedRowFilter = value;
					return;
				}
				CultureInfo culture = (this._table != null) ? this._table.Locale : CultureInfo.CurrentCulture;
				if (this._rowFilter == null || string.Compare(this.RowFilter, value, false, culture) != 0)
				{
					DataExpression newRowFilter = new DataExpression(this._table, value);
					this.SetIndex(this._sort, this._recordStates, newRowFilter);
				}
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x000354A4 File Offset: 0x000336A4
		// (set) Token: 0x06000CF2 RID: 3314 RVA: 0x000354C8 File Offset: 0x000336C8
		internal Predicate<DataRow> RowPredicate
		{
			get
			{
				DataView.RowPredicateFilter rowPredicateFilter = this.GetFilter() as DataView.RowPredicateFilter;
				if (rowPredicateFilter == null)
				{
					return null;
				}
				return rowPredicateFilter._predicateFilter;
			}
			set
			{
				if (this.RowPredicate != value)
				{
					this.SetIndex(this.Sort, this.RowStateFilter, (value != null) ? new DataView.RowPredicateFilter(value) : null);
				}
			}
		}

		/// <summary>Gets or sets the row state filter used in the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataViewRowState" /> values.</returns>
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x000354F1 File Offset: 0x000336F1
		// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x000354FC File Offset: 0x000336FC
		[DefaultValue(DataViewRowState.CurrentRows)]
		public DataViewRowState RowStateFilter
		{
			get
			{
				return this._recordStates;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, DataViewRowState>("<ds.DataView.set_RowStateFilter|API> {0}, {1}", this.ObjectID, value);
				if (this._fInitInProgress)
				{
					this._delayedRecordStates = value;
					return;
				}
				if ((value & ~(DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedCurrent | DataViewRowState.ModifiedOriginal)) != DataViewRowState.None)
				{
					throw ExceptionBuilder.RecordStateRange();
				}
				if ((value & DataViewRowState.ModifiedOriginal) != DataViewRowState.None && (value & DataViewRowState.ModifiedCurrent) != DataViewRowState.None)
				{
					throw ExceptionBuilder.SetRowStateFilter();
				}
				if (this._recordStates != value)
				{
					this.SetIndex(this._sort, value, this._rowFilter);
				}
			}
		}

		/// <summary>Gets or sets the sort column or columns, and sort order for the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A string that contains the column name followed by "ASC" (ascending) or "DESC" (descending). Columns are sorted ascending by default. Multiple columns can be separated by commas.</returns>
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x0003556C File Offset: 0x0003376C
		// (set) Token: 0x06000CF6 RID: 3318 RVA: 0x000355C4 File Offset: 0x000337C4
		[DefaultValue("")]
		public string Sort
		{
			get
			{
				if (this._sort.Length == 0 && this._applyDefaultSort && this._table != null && this._table._primaryIndex.Length != 0)
				{
					return this._table.FormatSortString(this._table._primaryIndex);
				}
				return this._sort;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataView.set_Sort|API> {0}, '{1}'", this.ObjectID, value);
				if (this._fInitInProgress)
				{
					this._delayedSort = value;
					return;
				}
				CultureInfo culture = (this._table != null) ? this._table.Locale : CultureInfo.CurrentCulture;
				if (string.Compare(this._sort, value, false, culture) != 0 || this._comparison != null)
				{
					this.CheckSort(value);
					this._comparison = null;
					this.SetIndex(value, this._recordStates, this._rowFilter);
				}
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00035655 File Offset: 0x00033855
		// (set) Token: 0x06000CF8 RID: 3320 RVA: 0x0003565D File Offset: 0x0003385D
		internal Comparison<DataRow> SortComparison
		{
			get
			{
				return this._comparison;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataView.set_SortComparison|API> {0}", this.ObjectID);
				if (this._comparison != value)
				{
					this._comparison = value;
					this.SetIndex("", this._recordStates, this._rowFilter);
				}
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</returns>
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x00005696 File Offset: 0x00003896
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets or sets the source <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that provides the data for this view.</returns>
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0003569B File Offset: 0x0003389B
		// (set) Token: 0x06000CFB RID: 3323 RVA: 0x000356A4 File Offset: 0x000338A4
		[TypeConverter(typeof(DataTableTypeConverter))]
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.All)]
		public DataTable Table
		{
			get
			{
				return this._table;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, int>("<ds.DataView.set_Table|API> {0}, {1}", this.ObjectID, (value != null) ? value.ObjectID : 0);
				if (this._fInitInProgress && value != null)
				{
					this._delayedTable = value;
					return;
				}
				if (this._locked)
				{
					throw ExceptionBuilder.SetTable();
				}
				if (this._dataViewManager != null)
				{
					throw ExceptionBuilder.CanNotSetTable();
				}
				if (value != null && value.TableName.Length == 0)
				{
					throw ExceptionBuilder.CanNotBindTable();
				}
				if (this._table != value)
				{
					this._dvListener.UnregisterMetaDataEvents();
					this._table = value;
					if (this._table != null)
					{
						this._dvListener.RegisterMetaDataEvents(this._table);
					}
					this.SetIndex2("", DataViewRowState.CurrentRows, null, false);
					if (this._table != null)
					{
						this.OnListChanged(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, new DataTablePropertyDescriptor(this._table)));
					}
					this.OnListChanged(DataView.s_resetEventArgs);
				}
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <param name="recordIndex">An <see cref="T:System.Int32" /> value.</param>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</returns>
		// Token: 0x17000240 RID: 576
		object IList.this[int recordIndex]
		{
			get
			{
				return this[recordIndex];
			}
			set
			{
				throw ExceptionBuilder.SetIListObject();
			}
		}

		/// <summary>Gets a row of data from a specified table.</summary>
		/// <param name="recordIndex">The index of a record in the <see cref="T:System.Data.DataTable" />.</param>
		/// <returns>A <see cref="T:System.Data.DataRowView" /> of the row that you want.</returns>
		// Token: 0x17000241 RID: 577
		public DataRowView this[int recordIndex]
		{
			get
			{
				return this.GetRowView(this.GetRow(recordIndex));
			}
		}

		/// <summary>Adds a new row to the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A new <see cref="T:System.Data.DataRowView" /> object.</returns>
		// Token: 0x06000CFF RID: 3327 RVA: 0x000357A4 File Offset: 0x000339A4
		public virtual DataRowView AddNew()
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataView.AddNew|API> {0}", this.ObjectID);
			DataRowView result;
			try
			{
				this.CheckOpen();
				if (!this.AllowNew)
				{
					throw ExceptionBuilder.AddNewNotAllowNull();
				}
				if (this._addNewRow != null)
				{
					this._rowViewCache[this._addNewRow].EndEdit();
				}
				this._addNewRow = this._table.NewRow();
				DataRowView dataRowView = new DataRowView(this, this._addNewRow);
				this._rowViewCache.Add(this._addNewRow, dataRowView);
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, this.IndexOf(dataRowView)));
				result = dataRowView;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
			return result;
		}

		/// <summary>Starts the initialization of a <see cref="T:System.Data.DataView" /> that is used on a form or used by another component. The initialization occurs at runtime.</summary>
		// Token: 0x06000D00 RID: 3328 RVA: 0x00035860 File Offset: 0x00033A60
		public void BeginInit()
		{
			this._fInitInProgress = true;
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Data.DataView" /> that is used on a form or used by another component. The initialization occurs at runtime.</summary>
		// Token: 0x06000D01 RID: 3329 RVA: 0x0003586C File Offset: 0x00033A6C
		public void EndInit()
		{
			if (this._delayedTable != null && this._delayedTable.fInitInProgress)
			{
				this._delayedTable._delayedViews.Add(this);
				return;
			}
			this._fInitInProgress = false;
			this._fEndInitInProgress = true;
			if (this._delayedTable != null)
			{
				this.Table = this._delayedTable;
				this._delayedTable = null;
			}
			if (this._delayedSort != null)
			{
				this.Sort = this._delayedSort;
				this._delayedSort = null;
			}
			if (this._delayedRowFilter != null)
			{
				this.RowFilter = this._delayedRowFilter;
				this._delayedRowFilter = null;
			}
			if (this._delayedRecordStates != (DataViewRowState)(-1))
			{
				this.RowStateFilter = this._delayedRecordStates;
				this._delayedRecordStates = (DataViewRowState)(-1);
			}
			this._fEndInitInProgress = false;
			this.SetIndex(this.Sort, this.RowStateFilter, this._rowFilter);
			this.OnInitialized();
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00035940 File Offset: 0x00033B40
		private void CheckOpen()
		{
			if (!this.IsOpen)
			{
				throw ExceptionBuilder.NotOpen();
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00035950 File Offset: 0x00033B50
		private void CheckSort(string sort)
		{
			if (this._table == null)
			{
				throw ExceptionBuilder.CanNotUse();
			}
			if (sort.Length == 0)
			{
				return;
			}
			this._table.ParseSortString(sort);
		}

		/// <summary>Closes the <see cref="T:System.Data.DataView" />.</summary>
		// Token: 0x06000D04 RID: 3332 RVA: 0x00035976 File Offset: 0x00033B76
		protected void Close()
		{
			this._shouldOpen = false;
			this.UpdateIndex();
			this._dvListener.UnregisterMetaDataEvents();
		}

		/// <summary>Copies items into an array. Only for Web Forms Interfaces.</summary>
		/// <param name="array">array to copy into.</param>
		/// <param name="index">index to start at.</param>
		// Token: 0x06000D05 RID: 3333 RVA: 0x00035990 File Offset: 0x00033B90
		public void CopyTo(Array array, int index)
		{
			checked
			{
				if (this._index != null)
				{
					RBTree<int>.RBTreeEnumerator enumerator = this._index.GetEnumerator(0);
					while (enumerator.MoveNext())
					{
						int record = enumerator.Current;
						array.SetValue(this.GetRowView(record), index);
						index++;
					}
				}
				if (this._addNewRow != null)
				{
					array.SetValue(this._rowViewCache[this._addNewRow], index);
				}
			}
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x000359F8 File Offset: 0x00033BF8
		private void CopyTo(DataRowView[] array, int index)
		{
			checked
			{
				if (this._index != null)
				{
					RBTree<int>.RBTreeEnumerator enumerator = this._index.GetEnumerator(0);
					while (enumerator.MoveNext())
					{
						int record = enumerator.Current;
						array[index] = this.GetRowView(record);
						index++;
					}
				}
				if (this._addNewRow != null)
				{
					array[index] = this._rowViewCache[this._addNewRow];
				}
			}
		}

		/// <summary>Deletes a row at the specified index.</summary>
		/// <param name="index">The index of the row to delete.</param>
		// Token: 0x06000D07 RID: 3335 RVA: 0x00035A56 File Offset: 0x00033C56
		public void Delete(int index)
		{
			this.Delete(this.GetRow(index));
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00035A68 File Offset: 0x00033C68
		internal void Delete(DataRow row)
		{
			if (row != null)
			{
				long scopeId = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataView.Delete|API> {0}, row={1}", this.ObjectID, row._objectID);
				try
				{
					this.CheckOpen();
					if (row == this._addNewRow)
					{
						this.FinishAddNew(false);
					}
					else
					{
						if (!this.AllowDelete)
						{
							throw ExceptionBuilder.CanNotDelete();
						}
						row.Delete();
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(scopeId);
				}
			}
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Data.DataView" /> object.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000D09 RID: 3337 RVA: 0x00035AE0 File Offset: 0x00033CE0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
			base.Dispose(disposing);
		}

		/// <summary>Finds a row in the <see cref="T:System.Data.DataView" /> by the specified sort key value.</summary>
		/// <param name="key">The object to search for.</param>
		/// <returns>The index of the row in the <see cref="T:System.Data.DataView" /> that contains the sort key value specified; otherwise -1 if the sort key value does not exist.</returns>
		// Token: 0x06000D0A RID: 3338 RVA: 0x00035AF2 File Offset: 0x00033CF2
		public int Find(object key)
		{
			return this.FindByKey(key);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00035AFB File Offset: 0x00033CFB
		internal virtual int FindByKey(object key)
		{
			return this._index.FindRecordByKey(key);
		}

		/// <summary>Finds a row in the <see cref="T:System.Data.DataView" /> by the specified sort key values.</summary>
		/// <param name="key">An array of values, typed as <see cref="T:System.Object" />.</param>
		/// <returns>The index of the position of the first row in the <see cref="T:System.Data.DataView" /> that matches the sort key values specified; otherwise -1 if there are no matching sort key values.</returns>
		// Token: 0x06000D0C RID: 3340 RVA: 0x00035B09 File Offset: 0x00033D09
		public int Find(object[] key)
		{
			return this.FindByKey(key);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00035B12 File Offset: 0x00033D12
		internal virtual int FindByKey(object[] key)
		{
			return this._index.FindRecordByKey(key);
		}

		/// <summary>Returns an array of <see cref="T:System.Data.DataRowView" /> objects whose columns match the specified sort key value.</summary>
		/// <param name="key">The column value, typed as <see cref="T:System.Object" />, to search for.</param>
		/// <returns>An array of <see langword="DataRowView" /> objects whose columns match the specified sort key value; or, if no rows contain the specified sort key values, an empty <see langword="DataRowView" /> array.</returns>
		// Token: 0x06000D0E RID: 3342 RVA: 0x00035B20 File Offset: 0x00033D20
		public DataRowView[] FindRows(object key)
		{
			return this.FindRowsByKey(new object[]
			{
				key
			});
		}

		/// <summary>Returns an array of <see cref="T:System.Data.DataRowView" /> objects whose columns match the specified sort key value.</summary>
		/// <param name="key">An array of column values, typed as <see cref="T:System.Object" />, to search for.</param>
		/// <returns>An array of <see langword="DataRowView" /> objects whose columns match the specified sort key value; or, if no rows contain the specified sort key values, an empty <see langword="DataRowView" /> array.</returns>
		// Token: 0x06000D0F RID: 3343 RVA: 0x00035B32 File Offset: 0x00033D32
		public DataRowView[] FindRows(object[] key)
		{
			return this.FindRowsByKey(key);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00035B3C File Offset: 0x00033D3C
		internal virtual DataRowView[] FindRowsByKey(object[] key)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataView.FindRows|API> {0}", this.ObjectID);
			DataRowView[] dataRowViewFromRange;
			try
			{
				Range range = this._index.FindRecords(key);
				dataRowViewFromRange = this.GetDataRowViewFromRange(range);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
			return dataRowViewFromRange;
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00035B94 File Offset: 0x00033D94
		internal DataRowView[] GetDataRowViewFromRange(Range range)
		{
			if (range.IsNull)
			{
				return Array.Empty<DataRowView>();
			}
			DataRowView[] array = new DataRowView[range.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this[i + range.Min];
			}
			return array;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00035BE0 File Offset: 0x00033DE0
		internal void FinishAddNew(bool success)
		{
			DataCommonEventSource.Log.Trace<int, bool>("<ds.DataView.FinishAddNew|INFO> {0}, success={1}", this.ObjectID, success);
			DataRow addNewRow = this._addNewRow;
			if (success)
			{
				if (DataRowState.Detached == addNewRow.RowState)
				{
					this._table.Rows.Add(addNewRow);
				}
				else
				{
					addNewRow.EndEdit();
				}
			}
			if (addNewRow == this._addNewRow)
			{
				this._rowViewCache.Remove(this._addNewRow);
				this._addNewRow = null;
				if (!success)
				{
					addNewRow.CancelEdit();
				}
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, this.Count));
			}
		}

		/// <summary>Gets an enumerator for this <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for navigating through the list.</returns>
		// Token: 0x06000D13 RID: 3347 RVA: 0x00035C6C File Offset: 0x00033E6C
		public IEnumerator GetEnumerator()
		{
			DataRowView[] array = new DataRowView[this.Count];
			this.CopyTo(array, 0);
			return array.GetEnumerator();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</returns>
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00006D64 File Offset: 0x00004F64
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</returns>
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x00006D64 File Offset: 0x00004F64
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> value.</param>
		/// <returns>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</returns>
		// Token: 0x06000D16 RID: 3350 RVA: 0x00035C93 File Offset: 0x00033E93
		int IList.Add(object value)
		{
			if (value == null)
			{
				this.AddNew();
				return this.Count - 1;
			}
			throw ExceptionBuilder.AddExternalObject();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
		// Token: 0x06000D17 RID: 3351 RVA: 0x00035CAD File Offset: 0x00033EAD
		void IList.Clear()
		{
			throw ExceptionBuilder.CanNotClear();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> value.</param>
		/// <returns>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</returns>
		// Token: 0x06000D18 RID: 3352 RVA: 0x00035CB4 File Offset: 0x00033EB4
		bool IList.Contains(object value)
		{
			return 0 <= this.IndexOf(value as DataRowView);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> value.</param>
		/// <returns>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</returns>
		// Token: 0x06000D19 RID: 3353 RVA: 0x00035CC8 File Offset: 0x00033EC8
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as DataRowView);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00035CD8 File Offset: 0x00033ED8
		internal int IndexOf(DataRowView rowview)
		{
			if (rowview != null)
			{
				if (this._addNewRow == rowview.Row)
				{
					return this.Count - 1;
				}
				DataRowView dataRowView;
				if (this._index != null && DataRowState.Detached != rowview.Row.RowState && this._rowViewCache.TryGetValue(rowview.Row, out dataRowView) && dataRowView == rowview)
				{
					return this.IndexOfDataRowView(rowview);
				}
			}
			return -1;
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00035D37 File Offset: 0x00033F37
		private int IndexOfDataRowView(DataRowView rowview)
		{
			return this._index.GetIndex(rowview.Row.GetRecordFromVersion(rowview.Row.GetDefaultRowVersion(this.RowStateFilter) & (DataRowVersion)(-1025)));
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">An <see cref="T:System.Int32" /> value.</param>
		/// <param name="value">An <see cref="T:System.Object" /> value to be inserted.</param>
		// Token: 0x06000D1C RID: 3356 RVA: 0x00035D66 File Offset: 0x00033F66
		void IList.Insert(int index, object value)
		{
			throw ExceptionBuilder.InsertExternalObject();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> value.</param>
		// Token: 0x06000D1D RID: 3357 RVA: 0x00035D70 File Offset: 0x00033F70
		void IList.Remove(object value)
		{
			int num = this.IndexOf(value as DataRowView);
			if (0 <= num)
			{
				((IList)this).RemoveAt(num);
				return;
			}
			throw ExceptionBuilder.RemoveExternalObject();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
		/// <param name="index">An <see cref="T:System.Int32" /> value.</param>
		// Token: 0x06000D1E RID: 3358 RVA: 0x00035D9B File Offset: 0x00033F9B
		void IList.RemoveAt(int index)
		{
			this.Delete(index);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00035DA4 File Offset: 0x00033FA4
		internal Index GetFindIndex(string column, bool keepIndex)
		{
			if (this._findIndexes == null)
			{
				this._findIndexes = new Dictionary<string, Index>();
			}
			Index index;
			if (this._findIndexes.TryGetValue(column, out index))
			{
				if (!keepIndex)
				{
					this._findIndexes.Remove(column);
					index.RemoveRef();
					if (index.RefCount == 1)
					{
						index.RemoveRef();
					}
				}
			}
			else if (keepIndex)
			{
				index = this._table.GetIndex(column, this._recordStates, this.GetFilter());
				this._findIndexes[column] = index;
				index.AddRef();
			}
			return index;
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowNew" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowNew" />.</returns>
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x00035E2D File Offset: 0x0003402D
		bool IBindingList.AllowNew
		{
			get
			{
				return this.AllowNew;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
		/// <returns>The item added to the list.</returns>
		// Token: 0x06000D21 RID: 3361 RVA: 0x00035E35 File Offset: 0x00034035
		object IBindingList.AddNew()
		{
			return this.AddNew();
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowEdit" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowEdit" />.</returns>
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x00035E3D File Offset: 0x0003403D
		bool IBindingList.AllowEdit
		{
			get
			{
				return this.AllowEdit;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowRemove" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowRemove" />.</returns>
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x00035E45 File Offset: 0x00034045
		bool IBindingList.AllowRemove
		{
			get
			{
				return this.AllowDelete;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" />.</returns>
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x00006D61 File Offset: 0x00004F61
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" />.</returns>
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x00006D61 File Offset: 0x00004F61
		bool IBindingList.SupportsSearching
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" />.</returns>
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00006D61 File Offset: 0x00004F61
		bool IBindingList.SupportsSorting
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.IsSorted" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.IsSorted" />.</returns>
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x00035E4D File Offset: 0x0003404D
		bool IBindingList.IsSorted
		{
			get
			{
				return this.Sort.Length != 0;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortProperty" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortProperty" />.</returns>
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00035E5D File Offset: 0x0003405D
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				return this.GetSortProperty();
			}
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00035E65 File Offset: 0x00034065
		internal PropertyDescriptor GetSortProperty()
		{
			if (this._table != null && this._index != null && this._index._indexFields.Length == 1)
			{
				return new DataColumnPropertyDescriptor(this._index._indexFields[0].Column);
			}
			return null;
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortDirection" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortDirection" />.</returns>
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x00035EA4 File Offset: 0x000340A4
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				if (this._index._indexFields.Length != 1 || !this._index._indexFields[0].IsDescending)
				{
					return ListSortDirection.Ascending;
				}
				return ListSortDirection.Descending;
			}
		}

		/// <summary>Occurs when the list managed by the <see cref="T:System.Data.DataView" /> changes.</summary>
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000D2B RID: 3371 RVA: 0x00035ED1 File Offset: 0x000340D1
		// (remove) Token: 0x06000D2C RID: 3372 RVA: 0x00035EFF File Offset: 0x000340FF
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataView.add_ListChanged|API> {0}", this.ObjectID);
				this._onListChanged = (ListChangedEventHandler)Delegate.Combine(this._onListChanged, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataView.remove_ListChanged|API> {0}", this.ObjectID);
				this._onListChanged = (ListChangedEventHandler)Delegate.Remove(this._onListChanged, value);
			}
		}

		/// <summary>Occurs when initialization of the <see cref="T:System.Data.DataView" /> is completed.</summary>
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000D2D RID: 3373 RVA: 0x00035F30 File Offset: 0x00034130
		// (remove) Token: 0x06000D2E RID: 3374 RVA: 0x00035F68 File Offset: 0x00034168
		public event EventHandler Initialized
		{
			[CompilerGenerated]
			add
			{
				EventHandler eventHandler = this.Initialized;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.Initialized, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler eventHandler = this.Initialized;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.Initialized, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		/// <param name="property">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> object.</param>
		// Token: 0x06000D2F RID: 3375 RVA: 0x00035F9D File Offset: 0x0003419D
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
			this.GetFindIndex(property.Name, true);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />.</summary>
		/// <param name="property">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> object.</param>
		/// <param name="direction">A <see cref="T:System.ComponentModel.ListSortDirection" /> object.</param>
		// Token: 0x06000D30 RID: 3376 RVA: 0x00035FAD File Offset: 0x000341AD
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			this.Sort = this.CreateSortString(property, direction);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" />.</summary>
		/// <param name="property">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> object.</param>
		/// <param name="key">An <see cref="T:System.Object" /> value.</param>
		/// <returns>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" />.</returns>
		// Token: 0x06000D31 RID: 3377 RVA: 0x00035FC0 File Offset: 0x000341C0
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			if (property != null)
			{
				bool flag = false;
				Index index = null;
				try
				{
					if (this._findIndexes == null || !this._findIndexes.TryGetValue(property.Name, out index))
					{
						flag = true;
						index = this._table.GetIndex(property.Name, this._recordStates, this.GetFilter());
						index.AddRef();
					}
					Range range = index.FindRecords(key);
					if (!range.IsNull)
					{
						return this._index.GetIndex(index.GetRecord(range.Min));
					}
				}
				finally
				{
					if (flag && index != null)
					{
						index.RemoveRef();
						if (index.RefCount == 1)
						{
							index.RemoveRef();
						}
					}
				}
				return -1;
			}
			return -1;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		/// <param name="property">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> object.</param>
		// Token: 0x06000D32 RID: 3378 RVA: 0x0003607C File Offset: 0x0003427C
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
			this.GetFindIndex(property.Name, false);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveSort" />.</summary>
		// Token: 0x06000D33 RID: 3379 RVA: 0x0003608C File Offset: 0x0003428C
		void IBindingList.RemoveSort()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.DataView.RemoveSort|API> {0}", this.ObjectID);
			this.Sort = string.Empty;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingListView.ApplySort(System.ComponentModel.ListSortDescriptionCollection)" />.</summary>
		/// <param name="sorts">A <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> object.</param>
		// Token: 0x06000D34 RID: 3380 RVA: 0x000360B0 File Offset: 0x000342B0
		void IBindingListView.ApplySort(ListSortDescriptionCollection sorts)
		{
			if (sorts == null)
			{
				throw ExceptionBuilder.ArgumentNull("sorts");
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			foreach (object obj in ((IEnumerable)sorts))
			{
				ListSortDescription listSortDescription = (ListSortDescription)obj;
				if (listSortDescription == null)
				{
					throw ExceptionBuilder.ArgumentContainsNull("sorts");
				}
				PropertyDescriptor propertyDescriptor = listSortDescription.PropertyDescriptor;
				if (propertyDescriptor == null)
				{
					throw ExceptionBuilder.ArgumentNull("PropertyDescriptor");
				}
				if (!this._table.Columns.Contains(propertyDescriptor.Name))
				{
					throw ExceptionBuilder.ColumnToSortIsOutOfRange(propertyDescriptor.Name);
				}
				ListSortDirection sortDirection = listSortDescription.SortDirection;
				if (flag)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(this.CreateSortString(propertyDescriptor, sortDirection));
				if (!flag)
				{
					flag = true;
				}
			}
			this.Sort = stringBuilder.ToString();
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00036194 File Offset: 0x00034394
		private string CreateSortString(PropertyDescriptor property, ListSortDirection direction)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			stringBuilder.Append(property.Name);
			stringBuilder.Append(']');
			if (ListSortDirection.Descending == direction)
			{
				stringBuilder.Append(" DESC");
			}
			return stringBuilder.ToString();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingListView.RemoveFilter" />.</summary>
		// Token: 0x06000D36 RID: 3382 RVA: 0x000361DC File Offset: 0x000343DC
		void IBindingListView.RemoveFilter()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.DataView.RemoveFilter|API> {0}", this.ObjectID);
			this.RowFilter = string.Empty;
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.Filter" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.Filter" />.</returns>
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x000361FE File Offset: 0x000343FE
		// (set) Token: 0x06000D38 RID: 3384 RVA: 0x00036206 File Offset: 0x00034406
		string IBindingListView.Filter
		{
			get
			{
				return this.RowFilter;
			}
			set
			{
				this.RowFilter = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.SortDescriptions" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.SortDescriptions" />.</returns>
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0003620F File Offset: 0x0003440F
		ListSortDescriptionCollection IBindingListView.SortDescriptions
		{
			get
			{
				return this.GetSortDescriptions();
			}
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00036218 File Offset: 0x00034418
		internal ListSortDescriptionCollection GetSortDescriptions()
		{
			ListSortDescription[] array = Array.Empty<ListSortDescription>();
			if (this._table != null && this._index != null && this._index._indexFields.Length != 0)
			{
				array = new ListSortDescription[this._index._indexFields.Length];
				for (int i = 0; i < this._index._indexFields.Length; i++)
				{
					DataColumnPropertyDescriptor property = new DataColumnPropertyDescriptor(this._index._indexFields[i].Column);
					if (this._index._indexFields[i].IsDescending)
					{
						array[i] = new ListSortDescription(property, ListSortDirection.Descending);
					}
					else
					{
						array[i] = new ListSortDescription(property, ListSortDirection.Ascending);
					}
				}
			}
			return new ListSortDescriptionCollection(array);
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.SupportsAdvancedSorting" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.SupportsAdvancedSorting" />.</returns>
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x00006D61 File Offset: 0x00004F61
		bool IBindingListView.SupportsAdvancedSorting
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.SupportsFiltering" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.SupportsFiltering" />.</returns>
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00006D61 File Offset: 0x00004F61
		bool IBindingListView.SupportsFiltering
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ITypedList.GetListName(System.ComponentModel.PropertyDescriptor[])" />.</summary>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</param>
		/// <returns>For a description of this member, see <see cref="M:System.ComponentModel.ITypedList.GetListName(System.ComponentModel.PropertyDescriptor[])" />.</returns>
		// Token: 0x06000D3D RID: 3389 RVA: 0x000362CC File Offset: 0x000344CC
		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			if (this._table != null)
			{
				if (listAccessors == null || listAccessors.Length == 0)
				{
					return this._table.TableName;
				}
				DataSet dataSet = this._table.DataSet;
				if (dataSet != null)
				{
					DataTable dataTable = dataSet.FindTable(this._table, listAccessors, 0);
					if (dataTable != null)
					{
						return dataTable.TableName;
					}
				}
			}
			return string.Empty;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ITypedList.GetItemProperties(System.ComponentModel.PropertyDescriptor[])" />.</summary>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects to find in the collection as bindable. This can be <see langword="null" />.</param>
		// Token: 0x06000D3E RID: 3390 RVA: 0x00036324 File Offset: 0x00034524
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			if (this._table != null)
			{
				if (listAccessors == null || listAccessors.Length == 0)
				{
					return this._table.GetPropertyDescriptorCollection(null);
				}
				DataSet dataSet = this._table.DataSet;
				if (dataSet == null)
				{
					return new PropertyDescriptorCollection(null);
				}
				DataTable dataTable = dataSet.FindTable(this._table, listAccessors, 0);
				if (dataTable != null)
				{
					return dataTable.GetPropertyDescriptorCollection(null);
				}
			}
			return new PropertyDescriptorCollection(null);
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00036383 File Offset: 0x00034583
		internal virtual IFilter GetFilter()
		{
			return this._rowFilter;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0003638B File Offset: 0x0003458B
		private int GetRecord(int recordIndex)
		{
			if (this.Count <= recordIndex)
			{
				throw ExceptionBuilder.RowOutOfRange(recordIndex);
			}
			if (recordIndex != this._index.RecordCount)
			{
				return this._index.GetRecord(recordIndex);
			}
			return this._addNewRow.GetDefaultRecord();
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x000363C4 File Offset: 0x000345C4
		internal DataRow GetRow(int index)
		{
			int count = this.Count;
			if (count <= index)
			{
				throw ExceptionBuilder.GetElementIndex(index);
			}
			if (index == count - 1 && this._addNewRow != null)
			{
				return this._addNewRow;
			}
			return this._table._recordManager[this.GetRecord(index)];
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0003640F File Offset: 0x0003460F
		private DataRowView GetRowView(int record)
		{
			return this.GetRowView(this._table._recordManager[record]);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00036428 File Offset: 0x00034628
		private DataRowView GetRowView(DataRow dr)
		{
			return this._rowViewCache[dr];
		}

		/// <summary>Occurs after a <see cref="T:System.Data.DataView" /> has been changed successfully.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x06000D44 RID: 3396 RVA: 0x00036436 File Offset: 0x00034636
		protected virtual void IndexListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType != ListChangedType.Reset)
			{
				this.OnListChanged(e);
			}
			if (this._addNewRow != null && this._index.RecordCount == 0)
			{
				this.FinishAddNew(false);
			}
			if (e.ListChangedType == ListChangedType.Reset)
			{
				this.OnListChanged(e);
			}
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00036474 File Offset: 0x00034674
		internal void IndexListChangedInternal(ListChangedEventArgs e)
		{
			this._rowViewBuffer.Clear();
			if (ListChangedType.ItemAdded == e.ListChangedType && this._addNewMoved != null && this._addNewMoved.NewIndex != this._addNewMoved.OldIndex)
			{
				ListChangedEventArgs addNewMoved = this._addNewMoved;
				this._addNewMoved = null;
				this.IndexListChanged(this, addNewMoved);
			}
			this.IndexListChanged(this, e);
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x000364D4 File Offset: 0x000346D4
		internal void MaintainDataView(ListChangedType changedType, DataRow row, bool trackAddRemove)
		{
			DataRowView dataRowView = null;
			switch (changedType)
			{
			case ListChangedType.Reset:
				this.ResetRowViewCache();
				break;
			case ListChangedType.ItemAdded:
				if (trackAddRemove && this._rowViewBuffer.TryGetValue(row, out dataRowView))
				{
					this._rowViewBuffer.Remove(row);
				}
				if (row == this._addNewRow)
				{
					int newIndex = this.IndexOfDataRowView(this._rowViewCache[this._addNewRow]);
					this._addNewRow = null;
					this._addNewMoved = new ListChangedEventArgs(ListChangedType.ItemMoved, newIndex, this.Count - 1);
					return;
				}
				if (!this._rowViewCache.ContainsKey(row))
				{
					this._rowViewCache.Add(row, dataRowView ?? new DataRowView(this, row));
					return;
				}
				break;
			case ListChangedType.ItemDeleted:
				if (trackAddRemove)
				{
					this._rowViewCache.TryGetValue(row, out dataRowView);
					if (dataRowView != null)
					{
						this._rowViewBuffer.Add(row, dataRowView);
					}
				}
				this._rowViewCache.Remove(row);
				return;
			case ListChangedType.ItemMoved:
			case ListChangedType.ItemChanged:
			case ListChangedType.PropertyDescriptorAdded:
			case ListChangedType.PropertyDescriptorDeleted:
			case ListChangedType.PropertyDescriptorChanged:
				break;
			default:
				return;
			}
		}

		/// <summary>Raises the <see cref="E:System.Data.DataView.ListChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x06000D47 RID: 3399 RVA: 0x000365C8 File Offset: 0x000347C8
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			DataCommonEventSource.Log.Trace<int, ListChangedType>("<ds.DataView.OnListChanged|INFO> {0}, ListChangedType={1}", this.ObjectID, e.ListChangedType);
			try
			{
				DataColumn dataColumn = null;
				string text = null;
				switch (e.ListChangedType)
				{
				case ListChangedType.ItemMoved:
				case ListChangedType.ItemChanged:
					if (0 <= e.NewIndex)
					{
						DataRow row = this.GetRow(e.NewIndex);
						if (row.HasPropertyChanged)
						{
							dataColumn = row.LastChangedColumn;
							text = ((dataColumn != null) ? dataColumn.ColumnName : string.Empty);
						}
					}
					break;
				}
				if (this._onListChanged != null)
				{
					if (dataColumn != null && e.NewIndex == e.OldIndex)
					{
						ListChangedEventArgs e2 = new ListChangedEventArgs(e.ListChangedType, e.NewIndex, new DataColumnPropertyDescriptor(dataColumn));
						this._onListChanged(this, e2);
					}
					else
					{
						this._onListChanged(this, e);
					}
				}
				if (text != null)
				{
					this[e.NewIndex].RaisePropertyChangedEvent(text);
				}
			}
			catch (Exception e3) when (ADP.IsCatchableExceptionType(e3))
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e3);
			}
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x000366F4 File Offset: 0x000348F4
		private void OnInitialized()
		{
			EventHandler initialized = this.Initialized;
			if (initialized == null)
			{
				return;
			}
			initialized(this, EventArgs.Empty);
		}

		/// <summary>Opens a <see cref="T:System.Data.DataView" />.</summary>
		// Token: 0x06000D49 RID: 3401 RVA: 0x0003670C File Offset: 0x0003490C
		protected void Open()
		{
			this._shouldOpen = true;
			this.UpdateIndex();
			this._dvListener.RegisterMetaDataEvents(this._table);
		}

		/// <summary>Reserved for internal use only.</summary>
		// Token: 0x06000D4A RID: 3402 RVA: 0x0003672C File Offset: 0x0003492C
		protected void Reset()
		{
			if (this.IsOpen)
			{
				this._index.Reset();
			}
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00036744 File Offset: 0x00034944
		internal void ResetRowViewCache()
		{
			Dictionary<DataRow, DataRowView> dictionary = new Dictionary<DataRow, DataRowView>(this.CountFromIndex, DataView.DataRowReferenceComparer.s_default);
			if (this._index != null)
			{
				RBTree<int>.RBTreeEnumerator enumerator = this._index.GetEnumerator(0);
				while (enumerator.MoveNext())
				{
					int record = enumerator.Current;
					DataRow dataRow = this._table._recordManager[record];
					DataRowView value;
					if (!this._rowViewCache.TryGetValue(dataRow, out value))
					{
						value = new DataRowView(this, dataRow);
					}
					dictionary.Add(dataRow, value);
				}
			}
			if (this._addNewRow != null)
			{
				DataRowView value;
				this._rowViewCache.TryGetValue(this._addNewRow, out value);
				dictionary.Add(this._addNewRow, value);
			}
			this._rowViewCache = dictionary;
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x000367EC File Offset: 0x000349EC
		internal void SetDataViewManager(DataViewManager dataViewManager)
		{
			if (this._table == null)
			{
				throw ExceptionBuilder.CanNotUse();
			}
			if (this._dataViewManager != dataViewManager)
			{
				if (dataViewManager != null)
				{
					dataViewManager._nViews--;
				}
				this._dataViewManager = dataViewManager;
				if (dataViewManager != null)
				{
					dataViewManager._nViews++;
					DataViewSetting dataViewSetting = dataViewManager.DataViewSettings[this._table];
					try
					{
						this._applyDefaultSort = dataViewSetting.ApplyDefaultSort;
						DataExpression newRowFilter = new DataExpression(this._table, dataViewSetting.RowFilter);
						this.SetIndex(dataViewSetting.Sort, dataViewSetting.RowStateFilter, newRowFilter);
					}
					catch (Exception e) when (ADP.IsCatchableExceptionType(e))
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(e);
					}
					this._locked = true;
					return;
				}
				this.SetIndex("", DataViewRowState.CurrentRows, null);
			}
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x000368CC File Offset: 0x00034ACC
		internal virtual void SetIndex(string newSort, DataViewRowState newRowStates, IFilter newRowFilter)
		{
			this.SetIndex2(newSort, newRowStates, newRowFilter, true);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x000368D8 File Offset: 0x00034AD8
		internal void SetIndex2(string newSort, DataViewRowState newRowStates, IFilter newRowFilter, bool fireEvent)
		{
			DataCommonEventSource.Log.Trace<int, string, DataViewRowState>("<ds.DataView.SetIndex|INFO> {0}, newSort='{1}', newRowStates={2}", this.ObjectID, newSort, newRowStates);
			this._sort = newSort;
			this._recordStates = newRowStates;
			this._rowFilter = newRowFilter;
			if (this._fEndInitInProgress)
			{
				return;
			}
			if (fireEvent)
			{
				this.UpdateIndex(true);
			}
			else
			{
				this.UpdateIndex(true, false);
			}
			if (this._findIndexes != null)
			{
				Dictionary<string, Index> findIndexes = this._findIndexes;
				this._findIndexes = null;
				foreach (KeyValuePair<string, Index> keyValuePair in findIndexes)
				{
					keyValuePair.Value.RemoveRef();
				}
			}
		}

		/// <summary>Reserved for internal use only.</summary>
		// Token: 0x06000D4F RID: 3407 RVA: 0x0003698C File Offset: 0x00034B8C
		protected void UpdateIndex()
		{
			this.UpdateIndex(false);
		}

		/// <summary>Reserved for internal use only.</summary>
		/// <param name="force">Reserved for internal use only.</param>
		// Token: 0x06000D50 RID: 3408 RVA: 0x00036995 File Offset: 0x00034B95
		protected virtual void UpdateIndex(bool force)
		{
			this.UpdateIndex(force, true);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x000369A0 File Offset: 0x00034BA0
		internal void UpdateIndex(bool force, bool fireEvent)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataView.UpdateIndex|INFO> {0}, force={1}", this.ObjectID, force);
			try
			{
				if (this._open != this._shouldOpen || force)
				{
					this._open = this._shouldOpen;
					Index index = null;
					if (this._open && this._table != null)
					{
						if (this.SortComparison != null)
						{
							index = new Index(this._table, this.SortComparison, this._recordStates, this.GetFilter());
							index.AddRef();
						}
						else
						{
							index = this._table.GetIndex(this.Sort, this._recordStates, this.GetFilter());
						}
					}
					if (this._index != index)
					{
						if (this._index == null)
						{
							DataTable table = index.Table;
						}
						else
						{
							DataTable table2 = this._index.Table;
						}
						if (this._index != null)
						{
							this._dvListener.UnregisterListChangedEvent();
						}
						this._index = index;
						if (this._index != null)
						{
							this._dvListener.RegisterListChangedEvent(this._index);
						}
						this.ResetRowViewCache();
						if (fireEvent)
						{
							this.OnListChanged(DataView.s_resetEventArgs);
						}
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00036AD4 File Offset: 0x00034CD4
		internal void ChildRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataRelationPropertyDescriptor propDesc = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, propDesc) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : null)));
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00036B40 File Offset: 0x00034D40
		internal void ParentRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataRelationPropertyDescriptor propDesc = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, propDesc) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : null)));
		}

		/// <summary>Occurs after a <see cref="T:System.Data.DataColumnCollection" /> has been changed successfully.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x06000D54 RID: 3412 RVA: 0x00036BAC File Offset: 0x00034DAC
		protected virtual void ColumnCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataColumnPropertyDescriptor propDesc = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataColumnPropertyDescriptor((DataColumn)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, propDesc) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataColumnPropertyDescriptor((DataColumn)e.Element)) : null)));
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00036C16 File Offset: 0x00034E16
		internal void ColumnCollectionChangedInternal(object sender, CollectionChangeEventArgs e)
		{
			this.ColumnCollectionChanged(sender, e);
		}

		/// <summary>Creates and returns a new <see cref="T:System.Data.DataTable" /> based on rows in an existing <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A new <see cref="T:System.Data.DataTable" /> instance that contains the requested rows and columns.</returns>
		// Token: 0x06000D56 RID: 3414 RVA: 0x00036C20 File Offset: 0x00034E20
		public DataTable ToTable()
		{
			return this.ToTable(null, false, Array.Empty<string>());
		}

		/// <summary>Creates and returns a new <see cref="T:System.Data.DataTable" /> based on rows in an existing <see cref="T:System.Data.DataView" />.</summary>
		/// <param name="tableName">The name of the returned <see cref="T:System.Data.DataTable" />.</param>
		/// <returns>A new <see cref="T:System.Data.DataTable" /> instance that contains the requested rows and columns.</returns>
		// Token: 0x06000D57 RID: 3415 RVA: 0x00036C2F File Offset: 0x00034E2F
		public DataTable ToTable(string tableName)
		{
			return this.ToTable(tableName, false, Array.Empty<string>());
		}

		/// <summary>Creates and returns a new <see cref="T:System.Data.DataTable" /> based on rows in an existing <see cref="T:System.Data.DataView" />.</summary>
		/// <param name="distinct">If <see langword="true" />, the returned <see cref="T:System.Data.DataTable" /> contains rows that have distinct values for all its columns. The default value is <see langword="false" />.</param>
		/// <param name="columnNames">A string array that contains a list of the column names to be included in the returned <see cref="T:System.Data.DataTable" />. The <see cref="T:System.Data.DataTable" /> contains the specified columns in the order they appear within this array.</param>
		/// <returns>A new <see cref="T:System.Data.DataTable" /> instance that contains the requested rows and columns.</returns>
		// Token: 0x06000D58 RID: 3416 RVA: 0x00036C3E File Offset: 0x00034E3E
		public DataTable ToTable(bool distinct, params string[] columnNames)
		{
			return this.ToTable(null, distinct, columnNames);
		}

		/// <summary>Creates and returns a new <see cref="T:System.Data.DataTable" /> based on rows in an existing <see cref="T:System.Data.DataView" />.</summary>
		/// <param name="tableName">The name of the returned <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="distinct">If <see langword="true" />, the returned <see cref="T:System.Data.DataTable" /> contains rows that have distinct values for all its columns. The default value is <see langword="false" />.</param>
		/// <param name="columnNames">A string array that contains a list of the column names to be included in the returned <see cref="T:System.Data.DataTable" />. The <see langword="DataTable" /> contains the specified columns in the order they appear within this array.</param>
		/// <returns>A new <see cref="T:System.Data.DataTable" /> instance that contains the requested rows and columns.</returns>
		// Token: 0x06000D59 RID: 3417 RVA: 0x00036C4C File Offset: 0x00034E4C
		public DataTable ToTable(string tableName, bool distinct, params string[] columnNames)
		{
			DataCommonEventSource.Log.Trace<int, string, bool>("<ds.DataView.ToTable|API> {0}, TableName='{1}', distinct={2}", this.ObjectID, tableName, distinct);
			if (columnNames == null)
			{
				throw ExceptionBuilder.ArgumentNull("columnNames");
			}
			DataTable dataTable = new DataTable();
			dataTable.Locale = this._table.Locale;
			dataTable.CaseSensitive = this._table.CaseSensitive;
			dataTable.TableName = ((tableName != null) ? tableName : this._table.TableName);
			dataTable.Namespace = this._table.Namespace;
			dataTable.Prefix = this._table.Prefix;
			if (columnNames.Length == 0)
			{
				columnNames = new string[this.Table.Columns.Count];
				for (int i = 0; i < columnNames.Length; i++)
				{
					columnNames[i] = this.Table.Columns[i].ColumnName;
				}
			}
			int[] array = new int[columnNames.Length];
			List<object[]> list = new List<object[]>();
			for (int j = 0; j < columnNames.Length; j++)
			{
				DataColumn dataColumn = this.Table.Columns[columnNames[j]];
				if (dataColumn == null)
				{
					throw ExceptionBuilder.ColumnNotInTheUnderlyingTable(columnNames[j], this.Table.TableName);
				}
				dataTable.Columns.Add(dataColumn.Clone());
				array[j] = this.Table.Columns.IndexOf(dataColumn);
			}
			foreach (object obj in this)
			{
				DataRowView dataRowView = (DataRowView)obj;
				object[] array2 = new object[columnNames.Length];
				for (int k = 0; k < array.Length; k++)
				{
					array2[k] = dataRowView[array[k]];
				}
				if (!distinct || !this.RowExist(list, array2))
				{
					dataTable.Rows.Add(array2);
					list.Add(array2);
				}
			}
			return dataTable;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00036E34 File Offset: 0x00035034
		private bool RowExist(List<object[]> arraylist, object[] objectArray)
		{
			for (int i = 0; i < arraylist.Count; i++)
			{
				object[] array = arraylist[i];
				bool flag = true;
				for (int j = 0; j < objectArray.Length; j++)
				{
					flag &= array[j].Equals(objectArray[j]);
				}
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Data.DataView" /> instances are considered equal.</summary>
		/// <param name="view">The <see cref="T:System.Data.DataView" /> to be compared.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.Data.DataView" /> instances are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000D5B RID: 3419 RVA: 0x00036E80 File Offset: 0x00035080
		public virtual bool Equals(DataView view)
		{
			return view != null && this.Table == view.Table && this.Count == view.Count && string.Equals(this.RowFilter, view.RowFilter, StringComparison.OrdinalIgnoreCase) && string.Equals(this.Sort, view.Sort, StringComparison.OrdinalIgnoreCase) && this.SortComparison == view.SortComparison && this.RowPredicate == view.RowPredicate && this.RowStateFilter == view.RowStateFilter && this.DataViewManager == view.DataViewManager && this.AllowDelete == view.AllowDelete && this.AllowNew == view.AllowNew && this.AllowEdit == view.AllowEdit;
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x00036F42 File Offset: 0x00035142
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00036F4A File Offset: 0x0003514A
		// Note: this type is marked as 'beforefieldinit'.
		static DataView()
		{
		}

		// Token: 0x0400081E RID: 2078
		private DataViewManager _dataViewManager;

		// Token: 0x0400081F RID: 2079
		private DataTable _table;

		// Token: 0x04000820 RID: 2080
		private bool _locked;

		// Token: 0x04000821 RID: 2081
		private Index _index;

		// Token: 0x04000822 RID: 2082
		private Dictionary<string, Index> _findIndexes;

		// Token: 0x04000823 RID: 2083
		private string _sort = string.Empty;

		// Token: 0x04000824 RID: 2084
		private Comparison<DataRow> _comparison;

		// Token: 0x04000825 RID: 2085
		private IFilter _rowFilter;

		// Token: 0x04000826 RID: 2086
		private DataViewRowState _recordStates = DataViewRowState.CurrentRows;

		// Token: 0x04000827 RID: 2087
		private bool _shouldOpen = true;

		// Token: 0x04000828 RID: 2088
		private bool _open;

		// Token: 0x04000829 RID: 2089
		private bool _allowNew = true;

		// Token: 0x0400082A RID: 2090
		private bool _allowEdit = true;

		// Token: 0x0400082B RID: 2091
		private bool _allowDelete = true;

		// Token: 0x0400082C RID: 2092
		private bool _applyDefaultSort;

		// Token: 0x0400082D RID: 2093
		internal DataRow _addNewRow;

		// Token: 0x0400082E RID: 2094
		private ListChangedEventArgs _addNewMoved;

		// Token: 0x0400082F RID: 2095
		private ListChangedEventHandler _onListChanged;

		// Token: 0x04000830 RID: 2096
		internal static ListChangedEventArgs s_resetEventArgs = new ListChangedEventArgs(ListChangedType.Reset, -1);

		// Token: 0x04000831 RID: 2097
		private DataTable _delayedTable;

		// Token: 0x04000832 RID: 2098
		private string _delayedRowFilter;

		// Token: 0x04000833 RID: 2099
		private string _delayedSort;

		// Token: 0x04000834 RID: 2100
		private DataViewRowState _delayedRecordStates = (DataViewRowState)(-1);

		// Token: 0x04000835 RID: 2101
		private bool _fInitInProgress;

		// Token: 0x04000836 RID: 2102
		private bool _fEndInitInProgress;

		// Token: 0x04000837 RID: 2103
		private Dictionary<DataRow, DataRowView> _rowViewCache = new Dictionary<DataRow, DataRowView>(DataView.DataRowReferenceComparer.s_default);

		// Token: 0x04000838 RID: 2104
		private readonly Dictionary<DataRow, DataRowView> _rowViewBuffer = new Dictionary<DataRow, DataRowView>(DataView.DataRowReferenceComparer.s_default);

		// Token: 0x04000839 RID: 2105
		private DataViewListener _dvListener;

		// Token: 0x0400083A RID: 2106
		private static int s_objectTypeCount;

		// Token: 0x0400083B RID: 2107
		private readonly int _objectID = Interlocked.Increment(ref DataView.s_objectTypeCount);

		// Token: 0x0400083C RID: 2108
		[CompilerGenerated]
		private EventHandler Initialized;

		// Token: 0x020000D6 RID: 214
		private sealed class DataRowReferenceComparer : IEqualityComparer<DataRow>
		{
			// Token: 0x06000D5E RID: 3422 RVA: 0x00003D93 File Offset: 0x00001F93
			private DataRowReferenceComparer()
			{
			}

			// Token: 0x06000D5F RID: 3423 RVA: 0x00036F58 File Offset: 0x00035158
			public bool Equals(DataRow x, DataRow y)
			{
				return x == y;
			}

			// Token: 0x06000D60 RID: 3424 RVA: 0x00036F5E File Offset: 0x0003515E
			public int GetHashCode(DataRow obj)
			{
				return obj._objectID;
			}

			// Token: 0x06000D61 RID: 3425 RVA: 0x00036F66 File Offset: 0x00035166
			// Note: this type is marked as 'beforefieldinit'.
			static DataRowReferenceComparer()
			{
			}

			// Token: 0x0400083D RID: 2109
			internal static readonly DataView.DataRowReferenceComparer s_default = new DataView.DataRowReferenceComparer();
		}

		// Token: 0x020000D7 RID: 215
		private sealed class RowPredicateFilter : IFilter
		{
			// Token: 0x06000D62 RID: 3426 RVA: 0x00036F72 File Offset: 0x00035172
			internal RowPredicateFilter(Predicate<DataRow> predicate)
			{
				this._predicateFilter = predicate;
			}

			// Token: 0x06000D63 RID: 3427 RVA: 0x00036F81 File Offset: 0x00035181
			bool IFilter.Invoke(DataRow row, DataRowVersion version)
			{
				return this._predicateFilter(row);
			}

			// Token: 0x0400083E RID: 2110
			internal readonly Predicate<DataRow> _predicateFilter;
		}
	}
}
