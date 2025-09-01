using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;

namespace System.Data
{
	// Token: 0x0200011F RID: 287
	internal sealed class RecordManager
	{
		// Token: 0x06000FF2 RID: 4082 RVA: 0x00041E88 File Offset: 0x00040088
		internal RecordManager(DataTable table)
		{
			if (table == null)
			{
				throw ExceptionBuilder.ArgumentNull("table");
			}
			this._table = table;
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00041EB8 File Offset: 0x000400B8
		private void GrowRecordCapacity()
		{
			this.RecordCapacity = ((RecordManager.NewCapacity(this._recordCapacity) < this.NormalizedMinimumCapacity(this._minimumCapacity)) ? this.NormalizedMinimumCapacity(this._minimumCapacity) : RecordManager.NewCapacity(this._recordCapacity));
			DataRow[] array = this._table.NewRowArray(this._recordCapacity);
			if (this._rows != null)
			{
				Array.Copy(this._rows, 0, array, 0, Math.Min(this._lastFreeRecord, this._rows.Length));
			}
			this._rows = array;
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x00041F3F File Offset: 0x0004013F
		internal int LastFreeRecord
		{
			get
			{
				return this._lastFreeRecord;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x00041F47 File Offset: 0x00040147
		// (set) Token: 0x06000FF6 RID: 4086 RVA: 0x00041F4F File Offset: 0x0004014F
		internal int MinimumCapacity
		{
			get
			{
				return this._minimumCapacity;
			}
			set
			{
				if (this._minimumCapacity != value)
				{
					if (value < 0)
					{
						throw ExceptionBuilder.NegativeMinimumCapacity();
					}
					this._minimumCapacity = value;
				}
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x00041F6B File Offset: 0x0004016B
		// (set) Token: 0x06000FF8 RID: 4088 RVA: 0x00041F74 File Offset: 0x00040174
		internal int RecordCapacity
		{
			get
			{
				return this._recordCapacity;
			}
			set
			{
				if (this._recordCapacity != value)
				{
					for (int i = 0; i < this._table.Columns.Count; i++)
					{
						this._table.Columns[i].SetCapacity(value);
					}
					this._recordCapacity = value;
				}
			}
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00041FC3 File Offset: 0x000401C3
		internal static int NewCapacity(int capacity)
		{
			if (capacity >= 128)
			{
				return capacity + capacity;
			}
			return 128;
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00041FD6 File Offset: 0x000401D6
		private int NormalizedMinimumCapacity(int capacity)
		{
			if (capacity >= 1014)
			{
				return (capacity + 10 >> 10) + 1 << 10;
			}
			if (capacity >= 246)
			{
				return 1024;
			}
			if (capacity < 54)
			{
				return 64;
			}
			return 256;
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00042008 File Offset: 0x00040208
		internal int NewRecordBase()
		{
			int result;
			if (this._freeRecordList.Count != 0)
			{
				result = this._freeRecordList[this._freeRecordList.Count - 1];
				this._freeRecordList.RemoveAt(this._freeRecordList.Count - 1);
			}
			else
			{
				if (this._lastFreeRecord >= this._recordCapacity)
				{
					this.GrowRecordCapacity();
				}
				result = this._lastFreeRecord;
				this._lastFreeRecord++;
			}
			return result;
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00042080 File Offset: 0x00040280
		internal void FreeRecord(ref int record)
		{
			if (-1 != record)
			{
				this[record] = null;
				int count = this._table._columnCollection.Count;
				for (int i = 0; i < count; i++)
				{
					this._table._columnCollection[i].FreeRecord(record);
				}
				if (this._lastFreeRecord == record + 1)
				{
					this._lastFreeRecord--;
				}
				else if (record < this._lastFreeRecord)
				{
					this._freeRecordList.Add(record);
				}
				record = -1;
			}
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00042108 File Offset: 0x00040308
		internal void Clear(bool clearAll)
		{
			if (clearAll)
			{
				for (int i = 0; i < this._recordCapacity; i++)
				{
					this._rows[i] = null;
				}
				int count = this._table._columnCollection.Count;
				for (int j = 0; j < count; j++)
				{
					DataColumn dataColumn = this._table._columnCollection[j];
					for (int k = 0; k < this._recordCapacity; k++)
					{
						dataColumn.FreeRecord(k);
					}
				}
				this._lastFreeRecord = 0;
				this._freeRecordList.Clear();
				return;
			}
			this._freeRecordList.Capacity = this._freeRecordList.Count + this._table.Rows.Count;
			for (int l = 0; l < this._recordCapacity; l++)
			{
				if (this._rows[l] != null && this._rows[l].rowID != -1L)
				{
					int num = l;
					this.FreeRecord(ref num);
				}
			}
		}

		// Token: 0x170002C5 RID: 709
		internal DataRow this[int record]
		{
			get
			{
				return this._rows[record];
			}
			set
			{
				this._rows[record] = value;
			}
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0004220C File Offset: 0x0004040C
		internal void SetKeyValues(int record, DataKey key, object[] keyValues)
		{
			for (int i = 0; i < keyValues.Length; i++)
			{
				key.ColumnsReference[i][record] = keyValues[i];
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00042239 File Offset: 0x00040439
		internal int ImportRecord(DataTable src, int record)
		{
			return this.CopyRecord(src, record, -1);
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00042244 File Offset: 0x00040444
		internal int CopyRecord(DataTable src, int record, int copy)
		{
			if (record == -1)
			{
				return copy;
			}
			int num = -1;
			try
			{
				num = ((copy == -1) ? this._table.NewUninitializedRecord() : copy);
				int count = this._table.Columns.Count;
				for (int i = 0; i < count; i++)
				{
					DataColumn dataColumn = this._table.Columns[i];
					DataColumn dataColumn2 = src.Columns[dataColumn.ColumnName];
					if (dataColumn2 != null)
					{
						object obj = dataColumn2[record];
						ICloneable cloneable = obj as ICloneable;
						if (cloneable != null)
						{
							dataColumn[num] = cloneable.Clone();
						}
						else
						{
							dataColumn[num] = obj;
						}
					}
					else if (-1 == copy)
					{
						dataColumn.Init(num);
					}
				}
			}
			catch (Exception e) when (ADP.IsCatchableOrSecurityExceptionType(e))
			{
				if (-1 == copy)
				{
					this.FreeRecord(ref num);
				}
				throw;
			}
			return num;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00042328 File Offset: 0x00040528
		internal void SetRowCache(DataRow[] newRows)
		{
			this._rows = newRows;
			this._lastFreeRecord = this._rows.Length;
			this._recordCapacity = this._lastFreeRecord;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00007EED File Offset: 0x000060ED
		[Conditional("DEBUG")]
		internal void VerifyRecord(int record)
		{
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00007EED File Offset: 0x000060ED
		[Conditional("DEBUG")]
		internal void VerifyRecord(int record, DataRow row)
		{
		}

		// Token: 0x040009D2 RID: 2514
		private readonly DataTable _table;

		// Token: 0x040009D3 RID: 2515
		private int _lastFreeRecord;

		// Token: 0x040009D4 RID: 2516
		private int _minimumCapacity = 50;

		// Token: 0x040009D5 RID: 2517
		private int _recordCapacity;

		// Token: 0x040009D6 RID: 2518
		private readonly List<int> _freeRecordList = new List<int>();

		// Token: 0x040009D7 RID: 2519
		private DataRow[] _rows;
	}
}
