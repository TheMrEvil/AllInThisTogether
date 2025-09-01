using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000D3 RID: 211
	internal sealed class DataTableReaderListener
	{
		// Token: 0x06000CD3 RID: 3283 RVA: 0x00034E50 File Offset: 0x00033050
		internal DataTableReaderListener(DataTableReader reader)
		{
			if (reader == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTableReader");
			}
			if (this._currentDataTable != null)
			{
				this.UnSubscribeEvents();
			}
			this._readerWeak = new WeakReference(reader);
			this._currentDataTable = reader.CurrentDataTable;
			if (this._currentDataTable != null)
			{
				this.SubscribeEvents();
			}
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00034EA5 File Offset: 0x000330A5
		internal void CleanUp()
		{
			this.UnSubscribeEvents();
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00034EAD File Offset: 0x000330AD
		internal void UpdataTable(DataTable datatable)
		{
			if (datatable == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTable");
			}
			this.UnSubscribeEvents();
			this._currentDataTable = datatable;
			this.SubscribeEvents();
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00034ED0 File Offset: 0x000330D0
		private void SubscribeEvents()
		{
			if (this._currentDataTable == null)
			{
				return;
			}
			if (this._isSubscribed)
			{
				return;
			}
			this._currentDataTable.Columns.ColumnPropertyChanged += this.SchemaChanged;
			this._currentDataTable.Columns.CollectionChanged += this.SchemaChanged;
			this._currentDataTable.RowChanged += this.DataChanged;
			this._currentDataTable.RowDeleted += this.DataChanged;
			this._currentDataTable.TableCleared += this.DataTableCleared;
			this._isSubscribed = true;
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00034F74 File Offset: 0x00033174
		private void UnSubscribeEvents()
		{
			if (this._currentDataTable == null)
			{
				return;
			}
			if (!this._isSubscribed)
			{
				return;
			}
			this._currentDataTable.Columns.ColumnPropertyChanged -= this.SchemaChanged;
			this._currentDataTable.Columns.CollectionChanged -= this.SchemaChanged;
			this._currentDataTable.RowChanged -= this.DataChanged;
			this._currentDataTable.RowDeleted -= this.DataChanged;
			this._currentDataTable.TableCleared -= this.DataTableCleared;
			this._isSubscribed = false;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00035018 File Offset: 0x00033218
		private void DataTableCleared(object sender, DataTableClearEventArgs e)
		{
			DataTableReader dataTableReader = (DataTableReader)this._readerWeak.Target;
			if (dataTableReader != null)
			{
				dataTableReader.DataTableCleared();
				return;
			}
			this.UnSubscribeEvents();
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00035048 File Offset: 0x00033248
		private void SchemaChanged(object sender, CollectionChangeEventArgs e)
		{
			DataTableReader dataTableReader = (DataTableReader)this._readerWeak.Target;
			if (dataTableReader != null)
			{
				dataTableReader.SchemaChanged();
				return;
			}
			this.UnSubscribeEvents();
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00035078 File Offset: 0x00033278
		private void DataChanged(object sender, DataRowChangeEventArgs args)
		{
			DataTableReader dataTableReader = (DataTableReader)this._readerWeak.Target;
			if (dataTableReader != null)
			{
				dataTableReader.DataChanged(args);
				return;
			}
			this.UnSubscribeEvents();
		}

		// Token: 0x0400081B RID: 2075
		private DataTable _currentDataTable;

		// Token: 0x0400081C RID: 2076
		private bool _isSubscribed;

		// Token: 0x0400081D RID: 2077
		private WeakReference _readerWeak;
	}
}
