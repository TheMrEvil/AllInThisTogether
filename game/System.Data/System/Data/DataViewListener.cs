using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000D8 RID: 216
	internal sealed class DataViewListener
	{
		// Token: 0x06000D64 RID: 3428 RVA: 0x00036F8F File Offset: 0x0003518F
		internal DataViewListener(DataView dv)
		{
			this._objectID = dv.ObjectID;
			this._dvWeak = new WeakReference(dv);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00036FB0 File Offset: 0x000351B0
		private void ChildRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.ChildRelationCollectionChanged(sender, e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00036FE4 File Offset: 0x000351E4
		private void ParentRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.ParentRelationCollectionChanged(sender, e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00037018 File Offset: 0x00035218
		private void ColumnCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.ColumnCollectionChangedInternal(sender, e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0003704C File Offset: 0x0003524C
		internal void MaintainDataView(ListChangedType changedType, DataRow row, bool trackAddRemove)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.MaintainDataView(changedType, row, trackAddRemove);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00037080 File Offset: 0x00035280
		internal void IndexListChanged(ListChangedEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.IndexListChangedInternal(e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x000370B0 File Offset: 0x000352B0
		internal void RegisterMetaDataEvents(DataTable table)
		{
			this._table = table;
			if (table != null)
			{
				this.RegisterListener(table);
				CollectionChangeEventHandler value = new CollectionChangeEventHandler(this.ColumnCollectionChanged);
				table.Columns.ColumnPropertyChanged += value;
				table.Columns.CollectionChanged += value;
				CollectionChangeEventHandler value2 = new CollectionChangeEventHandler(this.ChildRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ChildRelations).RelationPropertyChanged += value2;
				table.ChildRelations.CollectionChanged += value2;
				CollectionChangeEventHandler value3 = new CollectionChangeEventHandler(this.ParentRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ParentRelations).RelationPropertyChanged += value3;
				table.ParentRelations.CollectionChanged += value3;
			}
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0003714A File Offset: 0x0003534A
		internal void UnregisterMetaDataEvents()
		{
			this.UnregisterMetaDataEvents(true);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00037154 File Offset: 0x00035354
		private void UnregisterMetaDataEvents(bool updateListeners)
		{
			DataTable table = this._table;
			this._table = null;
			if (table != null)
			{
				CollectionChangeEventHandler value = new CollectionChangeEventHandler(this.ColumnCollectionChanged);
				table.Columns.ColumnPropertyChanged -= value;
				table.Columns.CollectionChanged -= value;
				CollectionChangeEventHandler value2 = new CollectionChangeEventHandler(this.ChildRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ChildRelations).RelationPropertyChanged -= value2;
				table.ChildRelations.CollectionChanged -= value2;
				CollectionChangeEventHandler value3 = new CollectionChangeEventHandler(this.ParentRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ParentRelations).RelationPropertyChanged -= value3;
				table.ParentRelations.CollectionChanged -= value3;
				if (updateListeners)
				{
					List<DataViewListener> listeners = table.GetListeners();
					List<DataViewListener> obj = listeners;
					lock (obj)
					{
						listeners.Remove(this);
					}
				}
			}
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00037230 File Offset: 0x00035430
		internal void RegisterListChangedEvent(Index index)
		{
			this._index = index;
			if (index != null)
			{
				lock (index)
				{
					index.AddRef();
					index.ListChangedAdd(this);
				}
			}
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0003727C File Offset: 0x0003547C
		internal void UnregisterListChangedEvent()
		{
			Index index = this._index;
			this._index = null;
			if (index != null)
			{
				Index obj = index;
				lock (obj)
				{
					index.ListChangedRemove(this);
					if (index.RemoveRef() <= 1)
					{
						index.RemoveRef();
					}
				}
			}
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x000372DC File Offset: 0x000354DC
		private void CleanUp(bool updateListeners)
		{
			this.UnregisterMetaDataEvents(updateListeners);
			this.UnregisterListChangedEvent();
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x000372EC File Offset: 0x000354EC
		private void RegisterListener(DataTable table)
		{
			List<DataViewListener> listeners = table.GetListeners();
			List<DataViewListener> obj = listeners;
			lock (obj)
			{
				int num = listeners.Count - 1;
				while (0 <= num)
				{
					DataViewListener dataViewListener = listeners[num];
					if (!dataViewListener._dvWeak.IsAlive)
					{
						listeners.RemoveAt(num);
						dataViewListener.CleanUp(false);
					}
					num--;
				}
				listeners.Add(this);
			}
		}

		// Token: 0x0400083F RID: 2111
		private readonly WeakReference _dvWeak;

		// Token: 0x04000840 RID: 2112
		private DataTable _table;

		// Token: 0x04000841 RID: 2113
		private Index _index;

		// Token: 0x04000842 RID: 2114
		internal readonly int _objectID;
	}
}
