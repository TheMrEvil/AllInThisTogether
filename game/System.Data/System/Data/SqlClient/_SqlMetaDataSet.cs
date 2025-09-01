using System;
using System.Collections.ObjectModel;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x02000265 RID: 613
	internal sealed class _SqlMetaDataSet
	{
		// Token: 0x06001CD1 RID: 7377 RVA: 0x00089398 File Offset: 0x00087598
		internal _SqlMetaDataSet(int count)
		{
			this._metaDataArray = new _SqlMetaData[count];
			for (int i = 0; i < this._metaDataArray.Length; i++)
			{
				this._metaDataArray[i] = new _SqlMetaData(i);
			}
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x000893D8 File Offset: 0x000875D8
		private _SqlMetaDataSet(_SqlMetaDataSet original)
		{
			this.id = original.id;
			this.indexMap = original.indexMap;
			this.visibleColumns = original.visibleColumns;
			this.dbColumnSchema = original.dbColumnSchema;
			if (original._metaDataArray == null)
			{
				this._metaDataArray = null;
				return;
			}
			this._metaDataArray = new _SqlMetaData[original._metaDataArray.Length];
			for (int i = 0; i < this._metaDataArray.Length; i++)
			{
				this._metaDataArray[i] = (_SqlMetaData)original._metaDataArray[i].Clone();
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x0008946B File Offset: 0x0008766B
		internal int Length
		{
			get
			{
				return this._metaDataArray.Length;
			}
		}

		// Token: 0x1700053E RID: 1342
		internal _SqlMetaData this[int index]
		{
			get
			{
				return this._metaDataArray[index];
			}
			set
			{
				this._metaDataArray[index] = value;
			}
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0008948A File Offset: 0x0008768A
		public object Clone()
		{
			return new _SqlMetaDataSet(this);
		}

		// Token: 0x040013EC RID: 5100
		internal ushort id;

		// Token: 0x040013ED RID: 5101
		internal int[] indexMap;

		// Token: 0x040013EE RID: 5102
		internal int visibleColumns;

		// Token: 0x040013EF RID: 5103
		internal DataTable schemaTable;

		// Token: 0x040013F0 RID: 5104
		private readonly _SqlMetaData[] _metaDataArray;

		// Token: 0x040013F1 RID: 5105
		internal ReadOnlyCollection<DbColumn> dbColumnSchema;
	}
}
