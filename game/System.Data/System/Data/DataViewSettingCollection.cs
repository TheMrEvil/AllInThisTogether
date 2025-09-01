using System;
using System.Collections;
using System.ComponentModel;
using Unity;

namespace System.Data
{
	/// <summary>Contains a read-only collection of <see cref="T:System.Data.DataViewSetting" /> objects for each <see cref="T:System.Data.DataTable" /> in a <see cref="T:System.Data.DataSet" />.</summary>
	// Token: 0x020000DD RID: 221
	public class DataViewSettingCollection : ICollection, IEnumerable
	{
		// Token: 0x06000DBD RID: 3517 RVA: 0x00037ADF File Offset: 0x00035CDF
		internal DataViewSettingCollection(DataViewManager dataViewManager)
		{
			this._list = new Hashtable();
			base..ctor();
			if (dataViewManager == null)
			{
				throw ExceptionBuilder.ArgumentNull("dataViewManager");
			}
			this._dataViewManager = dataViewManager;
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewSetting" /> objects of the specified <see cref="T:System.Data.DataTable" /> from the collection.</summary>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> to find.</param>
		/// <returns>A collection of <see cref="T:System.Data.DataViewSetting" /> objects.</returns>
		// Token: 0x1700026A RID: 618
		public virtual DataViewSetting this[DataTable table]
		{
			get
			{
				if (table == null)
				{
					throw ExceptionBuilder.ArgumentNull("table");
				}
				DataViewSetting dataViewSetting = (DataViewSetting)this._list[table];
				if (dataViewSetting == null)
				{
					dataViewSetting = new DataViewSetting();
					this[table] = dataViewSetting;
				}
				return dataViewSetting;
			}
			set
			{
				if (table == null)
				{
					throw ExceptionBuilder.ArgumentNull("table");
				}
				value.SetDataViewManager(this._dataViewManager);
				value.SetDataTable(table);
				this._list[table] = value;
			}
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00037B78 File Offset: 0x00035D78
		private DataTable GetTable(string tableName)
		{
			DataTable result = null;
			DataSet dataSet = this._dataViewManager.DataSet;
			if (dataSet != null)
			{
				result = dataSet.Tables[tableName];
			}
			return result;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00037BA4 File Offset: 0x00035DA4
		private DataTable GetTable(int index)
		{
			DataTable result = null;
			DataSet dataSet = this._dataViewManager.DataSet;
			if (dataSet != null)
			{
				result = dataSet.Tables[index];
			}
			return result;
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewSetting" /> of the <see cref="T:System.Data.DataTable" /> specified by its name.</summary>
		/// <param name="tableName">The name of the <see cref="T:System.Data.DataTable" /> to find.</param>
		/// <returns>A collection of <see cref="T:System.Data.DataViewSetting" /> objects.</returns>
		// Token: 0x1700026B RID: 619
		public virtual DataViewSetting this[string tableName]
		{
			get
			{
				DataTable table = this.GetTable(tableName);
				if (table != null)
				{
					return this[table];
				}
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewSetting" /> objects of the <see cref="T:System.Data.DataTable" /> specified by its index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.DataTable" /> to find.</param>
		/// <returns>A collection of <see cref="T:System.Data.DataViewSetting" /> objects.</returns>
		// Token: 0x1700026C RID: 620
		public virtual DataViewSetting this[int index]
		{
			get
			{
				DataTable table = this.GetTable(index);
				if (table != null)
				{
					return this[table];
				}
				return null;
			}
			set
			{
				DataTable table = this.GetTable(index);
				if (table != null)
				{
					this[table] = value;
				}
			}
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance starting at the specified index.</summary>
		/// <param name="ar">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to start inserting.</param>
		// Token: 0x06000DC5 RID: 3525 RVA: 0x00037C38 File Offset: 0x00035E38
		public void CopyTo(Array ar, int index)
		{
			foreach (object value in this)
			{
				ar.SetValue(value, index++);
			}
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance starting at the specified index.</summary>
		/// <param name="ar">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to start inserting.</param>
		// Token: 0x06000DC6 RID: 3526 RVA: 0x00037C68 File Offset: 0x00035E68
		public void CopyTo(DataViewSetting[] ar, int index)
		{
			foreach (object value in this)
			{
				ar.SetValue(value, index++);
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Data.DataViewSetting" /> objects in the <see cref="T:System.Data.DataViewSettingCollection" />.</summary>
		/// <returns>The number of <see cref="T:System.Data.DataViewSetting" /> objects in the collection.</returns>
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00037C98 File Offset: 0x00035E98
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				DataSet dataSet = this._dataViewManager.DataSet;
				if (dataSet != null)
				{
					return dataSet.Tables.Count;
				}
				return 0;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> for the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object.</returns>
		// Token: 0x06000DC8 RID: 3528 RVA: 0x00037CC1 File Offset: 0x00035EC1
		public IEnumerator GetEnumerator()
		{
			return new DataViewSettingCollection.DataViewSettingsEnumerator(this._dataViewManager);
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.DataViewSettingCollection" /> is read-only.</summary>
		/// <returns>Always returns <see langword="true" /> to indicate the collection is read-only.</returns>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00006D61 File Offset: 0x00004F61
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Data.DataViewSettingCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>This property is always <see langword="false" />, unless overridden by a derived class.</returns>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00006D64 File Offset: 0x00004F64
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Data.DataViewSettingCollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Data.DataViewSettingCollection" />.</returns>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00005696 File Offset: 0x00003896
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00037CCE File Offset: 0x00035ECE
		internal void Remove(DataTable table)
		{
			this._list.Remove(table);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x000108A6 File Offset: 0x0000EAA6
		internal DataViewSettingCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400085B RID: 2139
		private readonly DataViewManager _dataViewManager;

		// Token: 0x0400085C RID: 2140
		private readonly Hashtable _list;

		// Token: 0x020000DE RID: 222
		private sealed class DataViewSettingsEnumerator : IEnumerator
		{
			// Token: 0x06000DCE RID: 3534 RVA: 0x00037CDC File Offset: 0x00035EDC
			public DataViewSettingsEnumerator(DataViewManager dvm)
			{
				if (dvm.DataSet != null)
				{
					this._dataViewSettings = dvm.DataViewSettings;
					this._tableEnumerator = dvm.DataSet.Tables.GetEnumerator();
					return;
				}
				this._dataViewSettings = null;
				this._tableEnumerator = Array.Empty<DataTable>().GetEnumerator();
			}

			// Token: 0x06000DCF RID: 3535 RVA: 0x00037D31 File Offset: 0x00035F31
			public bool MoveNext()
			{
				return this._tableEnumerator.MoveNext();
			}

			// Token: 0x06000DD0 RID: 3536 RVA: 0x00037D3E File Offset: 0x00035F3E
			public void Reset()
			{
				this._tableEnumerator.Reset();
			}

			// Token: 0x17000271 RID: 625
			// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00037D4B File Offset: 0x00035F4B
			public object Current
			{
				get
				{
					return this._dataViewSettings[(DataTable)this._tableEnumerator.Current];
				}
			}

			// Token: 0x0400085D RID: 2141
			private DataViewSettingCollection _dataViewSettings;

			// Token: 0x0400085E RID: 2142
			private IEnumerator _tableEnumerator;
		}
	}
}
