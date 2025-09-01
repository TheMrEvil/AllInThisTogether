using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Data
{
	// Token: 0x02000128 RID: 296
	internal sealed class Index
	{
		// Token: 0x0600102F RID: 4143 RVA: 0x00043F18 File Offset: 0x00042118
		public Index(DataTable table, IndexField[] indexFields, DataViewRowState recordStates, IFilter rowFilter) : this(table, indexFields, null, recordStates, rowFilter)
		{
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00043F26 File Offset: 0x00042126
		public Index(DataTable table, Comparison<DataRow> comparison, DataViewRowState recordStates, IFilter rowFilter) : this(table, Index.GetAllFields(table.Columns), comparison, recordStates, rowFilter)
		{
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00043F40 File Offset: 0x00042140
		private static IndexField[] GetAllFields(DataColumnCollection columns)
		{
			IndexField[] array = new IndexField[columns.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new IndexField(columns[i], false);
			}
			return array;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00043F7C File Offset: 0x0004217C
		private Index(DataTable table, IndexField[] indexFields, Comparison<DataRow> comparison, DataViewRowState recordStates, IFilter rowFilter)
		{
			DataCommonEventSource.Log.Trace<int, int, DataViewRowState>("<ds.Index.Index|API> {0}, table={1}, recordStates={2}", this.ObjectID, (table != null) ? table.ObjectID : 0, recordStates);
			if ((recordStates & ~(DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedCurrent | DataViewRowState.ModifiedOriginal)) != DataViewRowState.None)
			{
				throw ExceptionBuilder.RecordStateRange();
			}
			this._table = table;
			this._listeners = new Listeners<DataViewListener>(this.ObjectID, (DataViewListener listener) => listener != null);
			this._indexFields = indexFields;
			this._recordStates = recordStates;
			this._comparison = comparison;
			DataColumnCollection columns = table.Columns;
			this._isSharable = (rowFilter == null && comparison == null);
			if (rowFilter != null)
			{
				this._rowFilter = new WeakReference(rowFilter);
				DataExpression dataExpression = rowFilter as DataExpression;
				if (dataExpression != null)
				{
					this._hasRemoteAggregate = dataExpression.HasRemoteAggregate();
				}
			}
			this.InitRecords(rowFilter);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00044064 File Offset: 0x00042264
		public bool Equal(IndexField[] indexDesc, DataViewRowState recordStates, IFilter rowFilter)
		{
			if (!this._isSharable || this._indexFields.Length != indexDesc.Length || this._recordStates != recordStates || rowFilter != null)
			{
				return false;
			}
			for (int i = 0; i < this._indexFields.Length; i++)
			{
				if (this._indexFields[i].Column != indexDesc[i].Column || this._indexFields[i].IsDescending != indexDesc[i].IsDescending)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x000440E8 File Offset: 0x000422E8
		internal bool HasRemoteAggregate
		{
			get
			{
				return this._hasRemoteAggregate;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x000440F0 File Offset: 0x000422F0
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x000440F8 File Offset: 0x000422F8
		public DataViewRowState RecordStates
		{
			get
			{
				return this._recordStates;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x00044100 File Offset: 0x00042300
		public IFilter RowFilter
		{
			get
			{
				return (IFilter)((this._rowFilter != null) ? this._rowFilter.Target : null);
			}
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0004411D File Offset: 0x0004231D
		public int GetRecord(int recordIndex)
		{
			return this._records[recordIndex];
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x0004412B File Offset: 0x0004232B
		public bool HasDuplicates
		{
			get
			{
				return this._records.HasDuplicates;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x00044138 File Offset: 0x00042338
		public int RecordCount
		{
			get
			{
				return this._recordCount;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x00044140 File Offset: 0x00042340
		public bool IsSharable
		{
			get
			{
				return this._isSharable;
			}
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00044148 File Offset: 0x00042348
		private bool AcceptRecord(int record)
		{
			return this.AcceptRecord(record, this.RowFilter);
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x00044158 File Offset: 0x00042358
		private bool AcceptRecord(int record, IFilter filter)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.Index.AcceptRecord|API> {0}, record={1}", this.ObjectID, record);
			if (filter == null)
			{
				return true;
			}
			DataRow dataRow = this._table._recordManager[record];
			if (dataRow == null)
			{
				return true;
			}
			DataRowVersion version = DataRowVersion.Default;
			if (dataRow._oldRecord == record)
			{
				version = DataRowVersion.Original;
			}
			else if (dataRow._newRecord == record)
			{
				version = DataRowVersion.Current;
			}
			else if (dataRow._tempRecord == record)
			{
				version = DataRowVersion.Proposed;
			}
			return filter.Invoke(dataRow, version);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x000441D6 File Offset: 0x000423D6
		internal void ListChangedAdd(DataViewListener listener)
		{
			this._listeners.Add(listener);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x000441E4 File Offset: 0x000423E4
		internal void ListChangedRemove(DataViewListener listener)
		{
			this._listeners.Remove(listener);
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x000441F2 File Offset: 0x000423F2
		public int RefCount
		{
			get
			{
				return this._refCount;
			}
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x000441FC File Offset: 0x000423FC
		public void AddRef()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.AddRef|API> {0}", this.ObjectID);
			this._table._indexesLock.EnterWriteLock();
			try
			{
				if (this._refCount == 0)
				{
					this._table.ShadowIndexCopy();
					this._table._indexes.Add(this);
				}
				this._refCount++;
			}
			finally
			{
				this._table._indexesLock.ExitWriteLock();
			}
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00044284 File Offset: 0x00042484
		public int RemoveRef()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.RemoveRef|API> {0}", this.ObjectID);
			this._table._indexesLock.EnterWriteLock();
			int result;
			try
			{
				int num = this._refCount - 1;
				this._refCount = num;
				result = num;
				if (this._refCount <= 0)
				{
					this._table.ShadowIndexCopy();
					this._table._indexes.Remove(this);
				}
			}
			finally
			{
				this._table._indexesLock.ExitWriteLock();
			}
			return result;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00044314 File Offset: 0x00042514
		private void ApplyChangeAction(int record, int action, int changeRecord)
		{
			if (action != 0)
			{
				if (action > 0)
				{
					if (this.AcceptRecord(record))
					{
						this.InsertRecord(record, true);
						return;
					}
				}
				else
				{
					if (this._comparison != null && -1 != record)
					{
						this.DeleteRecord(this.GetIndex(record, changeRecord));
						return;
					}
					this.DeleteRecord(this.GetIndex(record));
				}
			}
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00044363 File Offset: 0x00042563
		public bool CheckUnique()
		{
			return !this.HasDuplicates;
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00044370 File Offset: 0x00042570
		private int CompareRecords(int record1, int record2)
		{
			if (this._comparison != null)
			{
				return this.CompareDataRows(record1, record2);
			}
			if (this._indexFields.Length != 0)
			{
				int i = 0;
				while (i < this._indexFields.Length)
				{
					int num = this._indexFields[i].Column.Compare(record1, record2);
					if (num != 0)
					{
						if (!this._indexFields[i].IsDescending)
						{
							return num;
						}
						return -num;
					}
					else
					{
						i++;
					}
				}
				return 0;
			}
			return this._table.Rows.IndexOf(this._table._recordManager[record1]).CompareTo(this._table.Rows.IndexOf(this._table._recordManager[record2]));
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0004442A File Offset: 0x0004262A
		private int CompareDataRows(int record1, int record2)
		{
			return this._comparison(this._table._recordManager[record1], this._table._recordManager[record2]);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0004445C File Offset: 0x0004265C
		private int CompareDuplicateRecords(int record1, int record2)
		{
			if (this._table._recordManager[record1] == null)
			{
				if (this._table._recordManager[record2] != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (this._table._recordManager[record2] == null)
				{
					return 1;
				}
				int num = this._table._recordManager[record1].rowID.CompareTo(this._table._recordManager[record2].rowID);
				if (num == 0 && record1 != record2)
				{
					num = ((int)this._table._recordManager[record1].GetRecordState(record1)).CompareTo((int)this._table._recordManager[record2].GetRecordState(record2));
				}
				return num;
			}
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0004451C File Offset: 0x0004271C
		private int CompareRecordToKey(int record1, object[] vals)
		{
			int i = 0;
			while (i < this._indexFields.Length)
			{
				int num = this._indexFields[i].Column.CompareValueTo(record1, vals[i]);
				if (num != 0)
				{
					if (!this._indexFields[i].IsDescending)
					{
						return num;
					}
					return -num;
				}
				else
				{
					i++;
				}
			}
			return 0;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00044573 File Offset: 0x00042773
		public void DeleteRecordFromIndex(int recordIndex)
		{
			this.DeleteRecord(recordIndex, false);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0004457D File Offset: 0x0004277D
		private void DeleteRecord(int recordIndex)
		{
			this.DeleteRecord(recordIndex, true);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00044588 File Offset: 0x00042788
		private void DeleteRecord(int recordIndex, bool fireEvent)
		{
			DataCommonEventSource.Log.Trace<int, int, bool>("<ds.Index.DeleteRecord|INFO> {0}, recordIndex={1}, fireEvent={2}", this.ObjectID, recordIndex, fireEvent);
			if (recordIndex >= 0)
			{
				this._recordCount--;
				int record = this._records.DeleteByIndex(recordIndex);
				this.MaintainDataView(ListChangedType.ItemDeleted, record, !fireEvent);
				if (fireEvent)
				{
					this.OnListChanged(ListChangedType.ItemDeleted, recordIndex);
				}
			}
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x000445E2 File Offset: 0x000427E2
		public RBTree<int>.RBTreeEnumerator GetEnumerator(int startIndex)
		{
			return new RBTree<int>.RBTreeEnumerator(this._records, startIndex);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x000445F0 File Offset: 0x000427F0
		public int GetIndex(int record)
		{
			return this._records.GetIndexByKey(record);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00044600 File Offset: 0x00042800
		private int GetIndex(int record, int changeRecord)
		{
			DataRow dataRow = this._table._recordManager[record];
			int newRecord = dataRow._newRecord;
			int oldRecord = dataRow._oldRecord;
			int indexByKey;
			try
			{
				if (changeRecord != 1)
				{
					if (changeRecord == 2)
					{
						dataRow._oldRecord = record;
					}
				}
				else
				{
					dataRow._newRecord = record;
				}
				indexByKey = this._records.GetIndexByKey(record);
			}
			finally
			{
				if (changeRecord != 1)
				{
					if (changeRecord == 2)
					{
						dataRow._oldRecord = oldRecord;
					}
				}
				else
				{
					dataRow._newRecord = newRecord;
				}
			}
			return indexByKey;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00044684 File Offset: 0x00042884
		public object[] GetUniqueKeyValues()
		{
			if (this._indexFields == null || this._indexFields.Length == 0)
			{
				return Array.Empty<object>();
			}
			List<object[]> list = new List<object[]>();
			this.GetUniqueKeyValues(list, this._records.root);
			return list.ToArray();
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x000446C8 File Offset: 0x000428C8
		public int FindRecord(int record)
		{
			int num = this._records.Search(record);
			if (num != 0)
			{
				return this._records.GetIndexByNode(num);
			}
			return -1;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x000446F4 File Offset: 0x000428F4
		public int FindRecordByKey(object key)
		{
			int num = this.FindNodeByKey(key);
			if (num != 0)
			{
				return this._records.GetIndexByNode(num);
			}
			return -1;
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0004471C File Offset: 0x0004291C
		public int FindRecordByKey(object[] key)
		{
			int num = this.FindNodeByKeys(key);
			if (num != 0)
			{
				return this._records.GetIndexByNode(num);
			}
			return -1;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00044744 File Offset: 0x00042944
		private int FindNodeByKey(object originalKey)
		{
			if (this._indexFields.Length != 1)
			{
				throw ExceptionBuilder.IndexKeyLength(this._indexFields.Length, 1);
			}
			int num = this._records.root;
			if (num != 0)
			{
				DataColumn column = this._indexFields[0].Column;
				object value = column.ConvertValue(originalKey);
				num = this._records.root;
				if (this._indexFields[0].IsDescending)
				{
					while (num != 0)
					{
						int num2 = column.CompareValueTo(this._records.Key(num), value);
						if (num2 == 0)
						{
							break;
						}
						if (num2 < 0)
						{
							num = this._records.Left(num);
						}
						else
						{
							num = this._records.Right(num);
						}
					}
				}
				else
				{
					while (num != 0)
					{
						int num2 = column.CompareValueTo(this._records.Key(num), value);
						if (num2 == 0)
						{
							break;
						}
						if (num2 > 0)
						{
							num = this._records.Left(num);
						}
						else
						{
							num = this._records.Right(num);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00044830 File Offset: 0x00042A30
		private int FindNodeByKeys(object[] originalKey)
		{
			int num = (originalKey != null) ? originalKey.Length : 0;
			if (num == 0 || this._indexFields.Length != num)
			{
				throw ExceptionBuilder.IndexKeyLength(this._indexFields.Length, num);
			}
			int num2 = this._records.root;
			if (num2 != 0)
			{
				object[] array = new object[originalKey.Length];
				for (int i = 0; i < originalKey.Length; i++)
				{
					array[i] = this._indexFields[i].Column.ConvertValue(originalKey[i]);
				}
				num2 = this._records.root;
				while (num2 != 0)
				{
					num = this.CompareRecordToKey(this._records.Key(num2), array);
					if (num == 0)
					{
						break;
					}
					if (num > 0)
					{
						num2 = this._records.Left(num2);
					}
					else
					{
						num2 = this._records.Right(num2);
					}
				}
			}
			return num2;
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x000448F0 File Offset: 0x00042AF0
		private int FindNodeByKeyRecord(int record)
		{
			int num = this._records.root;
			if (num != 0)
			{
				num = this._records.root;
				while (num != 0)
				{
					int num2 = this.CompareRecords(this._records.Key(num), record);
					if (num2 == 0)
					{
						break;
					}
					if (num2 > 0)
					{
						num = this._records.Left(num);
					}
					else
					{
						num = this._records.Right(num);
					}
				}
			}
			return num;
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00044958 File Offset: 0x00042B58
		private Range GetRangeFromNode(int nodeId)
		{
			if (nodeId == 0)
			{
				return default(Range);
			}
			int indexByNode = this._records.GetIndexByNode(nodeId);
			if (this._records.Next(nodeId) == 0)
			{
				return new Range(indexByNode, indexByNode);
			}
			int num = this._records.SubTreeSize(this._records.Next(nodeId));
			return new Range(indexByNode, indexByNode + num - 1);
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x000449B8 File Offset: 0x00042BB8
		public Range FindRecords(object key)
		{
			int nodeId = this.FindNodeByKey(key);
			return this.GetRangeFromNode(nodeId);
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x000449D4 File Offset: 0x00042BD4
		public Range FindRecords(object[] key)
		{
			int nodeId = this.FindNodeByKeys(key);
			return this.GetRangeFromNode(nodeId);
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x000449F0 File Offset: 0x00042BF0
		internal void FireResetEvent()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.FireResetEvent|API> {0}", this.ObjectID);
			if (this.DoListChanged)
			{
				this.OnListChanged(DataView.s_resetEventArgs);
			}
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00044A1C File Offset: 0x00042C1C
		private int GetChangeAction(DataViewRowState oldState, DataViewRowState newState)
		{
			int num = ((this._recordStates & oldState) == DataViewRowState.None) ? 0 : 1;
			return (((this._recordStates & newState) == DataViewRowState.None) ? 0 : 1) - num;
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00044A48 File Offset: 0x00042C48
		private static int GetReplaceAction(DataViewRowState oldState)
		{
			if ((DataViewRowState.CurrentRows & oldState) != DataViewRowState.None)
			{
				return 1;
			}
			if ((DataViewRowState.OriginalRows & oldState) == DataViewRowState.None)
			{
				return 0;
			}
			return 2;
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00044A5B File Offset: 0x00042C5B
		public DataRow GetRow(int i)
		{
			return this._table._recordManager[this.GetRecord(i)];
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00044A74 File Offset: 0x00042C74
		public DataRow[] GetRows(object[] values)
		{
			return this.GetRows(this.FindRecords(values));
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00044A84 File Offset: 0x00042C84
		public DataRow[] GetRows(Range range)
		{
			DataRow[] array = this._table.NewRowArray(range.Count);
			if (array.Length != 0)
			{
				RBTree<int>.RBTreeEnumerator enumerator = this.GetEnumerator(range.Min);
				int num = 0;
				while (num < array.Length && enumerator.MoveNext())
				{
					array[num] = this._table._recordManager[enumerator.Current];
					num++;
				}
			}
			return array;
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00044AE8 File Offset: 0x00042CE8
		private void InitRecords(IFilter filter)
		{
			DataViewRowState recordStates = this._recordStates;
			bool append = this._indexFields.Length == 0;
			this._records = new Index.IndexTree(this);
			this._recordCount = 0;
			foreach (object obj in this._table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = -1;
				if (dataRow._oldRecord == dataRow._newRecord)
				{
					if ((recordStates & DataViewRowState.Unchanged) != DataViewRowState.None)
					{
						num = dataRow._oldRecord;
					}
				}
				else if (dataRow._oldRecord == -1)
				{
					if ((recordStates & DataViewRowState.Added) != DataViewRowState.None)
					{
						num = dataRow._newRecord;
					}
				}
				else if (dataRow._newRecord == -1)
				{
					if ((recordStates & DataViewRowState.Deleted) != DataViewRowState.None)
					{
						num = dataRow._oldRecord;
					}
				}
				else if ((recordStates & DataViewRowState.ModifiedCurrent) != DataViewRowState.None)
				{
					num = dataRow._newRecord;
				}
				else if ((recordStates & DataViewRowState.ModifiedOriginal) != DataViewRowState.None)
				{
					num = dataRow._oldRecord;
				}
				if (num != -1 && this.AcceptRecord(num, filter))
				{
					this._records.InsertAt(-1, num, append);
					this._recordCount++;
				}
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00044C0C File Offset: 0x00042E0C
		public int InsertRecordToIndex(int record)
		{
			int result = -1;
			if (this.AcceptRecord(record))
			{
				result = this.InsertRecord(record, false);
			}
			return result;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00044C30 File Offset: 0x00042E30
		private int InsertRecord(int record, bool fireEvent)
		{
			DataCommonEventSource.Log.Trace<int, int, bool>("<ds.Index.InsertRecord|INFO> {0}, record={1}, fireEvent={2}", this.ObjectID, record, fireEvent);
			bool append = false;
			if (this._indexFields.Length == 0 && this._table != null)
			{
				DataRow row = this._table._recordManager[record];
				append = (this._table.Rows.IndexOf(row) + 1 == this._table.Rows.Count);
			}
			int node = this._records.InsertAt(-1, record, append);
			this._recordCount++;
			this.MaintainDataView(ListChangedType.ItemAdded, record, !fireEvent);
			if (fireEvent)
			{
				if (this.DoListChanged)
				{
					this.OnListChanged(ListChangedType.ItemAdded, this._records.GetIndexByNode(node));
				}
				return 0;
			}
			return this._records.GetIndexByNode(node);
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00044CF4 File Offset: 0x00042EF4
		public bool IsKeyInIndex(object key)
		{
			int num = this.FindNodeByKey(key);
			return num != 0;
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00044D10 File Offset: 0x00042F10
		public bool IsKeyInIndex(object[] key)
		{
			int num = this.FindNodeByKeys(key);
			return num != 0;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00044D2C File Offset: 0x00042F2C
		public bool IsKeyRecordInIndex(int record)
		{
			int num = this.FindNodeByKeyRecord(record);
			return num != 0;
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x00044D45 File Offset: 0x00042F45
		private bool DoListChanged
		{
			get
			{
				return !this._suspendEvents && this._listeners.HasListeners && !this._table.AreIndexEventsSuspended;
			}
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00044D6C File Offset: 0x00042F6C
		private void OnListChanged(ListChangedType changedType, int newIndex, int oldIndex)
		{
			if (this.DoListChanged)
			{
				this.OnListChanged(new ListChangedEventArgs(changedType, newIndex, oldIndex));
			}
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00044D84 File Offset: 0x00042F84
		private void OnListChanged(ListChangedType changedType, int index)
		{
			if (this.DoListChanged)
			{
				this.OnListChanged(new ListChangedEventArgs(changedType, index));
			}
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00044D9C File Offset: 0x00042F9C
		private void OnListChanged(ListChangedEventArgs e)
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.OnListChanged|INFO> {0}", this.ObjectID);
			this._listeners.Notify<ListChangedEventArgs, bool, bool>(e, false, false, delegate(DataViewListener listener, ListChangedEventArgs args, bool arg2, bool arg3)
			{
				listener.IndexListChanged(args);
			});
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00044DEC File Offset: 0x00042FEC
		private void MaintainDataView(ListChangedType changedType, int record, bool trackAddRemove)
		{
			this._listeners.Notify<ListChangedType, DataRow, bool>(changedType, (0 <= record) ? this._table._recordManager[record] : null, trackAddRemove, delegate(DataViewListener listener, ListChangedType type, DataRow row, bool track)
			{
				listener.MaintainDataView(changedType, row, track);
			});
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00044E3C File Offset: 0x0004303C
		public void Reset()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.Reset|API> {0}", this.ObjectID);
			this.InitRecords(this.RowFilter);
			this.MaintainDataView(ListChangedType.Reset, -1, false);
			this.FireResetEvent();
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x00044E70 File Offset: 0x00043070
		public void RecordChanged(int record)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.Index.RecordChanged|API> {0}, record={1}", this.ObjectID, record);
			if (this.DoListChanged)
			{
				int index = this.GetIndex(record);
				if (index >= 0)
				{
					this.OnListChanged(ListChangedType.ItemChanged, index);
				}
			}
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00044EB0 File Offset: 0x000430B0
		public void RecordChanged(int oldIndex, int newIndex)
		{
			DataCommonEventSource.Log.Trace<int, int, int>("<ds.Index.RecordChanged|API> {0}, oldIndex={1}, newIndex={2}", this.ObjectID, oldIndex, newIndex);
			if (oldIndex > -1 || newIndex > -1)
			{
				if (oldIndex == newIndex)
				{
					this.OnListChanged(ListChangedType.ItemChanged, newIndex, oldIndex);
					return;
				}
				if (oldIndex == -1)
				{
					this.OnListChanged(ListChangedType.ItemAdded, newIndex, oldIndex);
					return;
				}
				if (newIndex == -1)
				{
					this.OnListChanged(ListChangedType.ItemDeleted, oldIndex);
					return;
				}
				this.OnListChanged(ListChangedType.ItemMoved, newIndex, oldIndex);
			}
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00044F10 File Offset: 0x00043110
		public void RecordStateChanged(int record, DataViewRowState oldState, DataViewRowState newState)
		{
			DataCommonEventSource.Log.Trace<int, int, DataViewRowState, DataViewRowState>("<ds.Index.RecordStateChanged|API> {0}, record={1}, oldState={2}, newState={3}", this.ObjectID, record, oldState, newState);
			int changeAction = this.GetChangeAction(oldState, newState);
			this.ApplyChangeAction(record, changeAction, Index.GetReplaceAction(oldState));
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x00044F4C File Offset: 0x0004314C
		public void RecordStateChanged(int oldRecord, DataViewRowState oldOldState, DataViewRowState oldNewState, int newRecord, DataViewRowState newOldState, DataViewRowState newNewState)
		{
			DataCommonEventSource.Log.Trace<int, int, DataViewRowState, DataViewRowState, int, DataViewRowState, DataViewRowState>("<ds.Index.RecordStateChanged|API> {0}, oldRecord={1}, oldOldState={2}, oldNewState={3}, newRecord={4}, newOldState={5}, newNewState={6}", this.ObjectID, oldRecord, oldOldState, oldNewState, newRecord, newOldState, newNewState);
			int changeAction = this.GetChangeAction(oldOldState, oldNewState);
			int changeAction2 = this.GetChangeAction(newOldState, newNewState);
			if (changeAction != -1 || changeAction2 != 1 || !this.AcceptRecord(newRecord))
			{
				this.ApplyChangeAction(oldRecord, changeAction, Index.GetReplaceAction(oldOldState));
				this.ApplyChangeAction(newRecord, changeAction2, Index.GetReplaceAction(newOldState));
				return;
			}
			int index;
			if (this._comparison != null && changeAction < 0)
			{
				index = this.GetIndex(oldRecord, Index.GetReplaceAction(oldOldState));
			}
			else
			{
				index = this.GetIndex(oldRecord);
			}
			if (this._comparison == null && index != -1 && this.CompareRecords(oldRecord, newRecord) == 0)
			{
				this._records.UpdateNodeKey(oldRecord, newRecord);
				int index2 = this.GetIndex(newRecord);
				this.OnListChanged(ListChangedType.ItemChanged, index2, index2);
				return;
			}
			this._suspendEvents = true;
			if (index != -1)
			{
				this._records.DeleteByIndex(index);
				this._recordCount--;
			}
			this._records.Insert(newRecord);
			this._recordCount++;
			this._suspendEvents = false;
			int index3 = this.GetIndex(newRecord);
			if (index == index3)
			{
				this.OnListChanged(ListChangedType.ItemChanged, index3, index);
				return;
			}
			if (index == -1)
			{
				this.MaintainDataView(ListChangedType.ItemAdded, newRecord, false);
				this.OnListChanged(ListChangedType.ItemAdded, this.GetIndex(newRecord));
				return;
			}
			this.OnListChanged(ListChangedType.ItemMoved, index3, index);
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x000450AC File Offset: 0x000432AC
		internal DataTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x000450B4 File Offset: 0x000432B4
		private void GetUniqueKeyValues(List<object[]> list, int curNodeId)
		{
			if (curNodeId != 0)
			{
				this.GetUniqueKeyValues(list, this._records.Left(curNodeId));
				int record = this._records.Key(curNodeId);
				object[] array = new object[this._indexFields.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._indexFields[i].Column[record];
				}
				list.Add(array);
				this.GetUniqueKeyValues(list, this._records.Right(curNodeId));
			}
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x00045134 File Offset: 0x00043334
		internal static int IndexOfReference<T>(List<T> list, T item) where T : class
		{
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] == item)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0004516C File Offset: 0x0004336C
		internal static bool ContainsReference<T>(List<T> list, T item) where T : class
		{
			return 0 <= Index.IndexOfReference<T>(list, item);
		}

		// Token: 0x040009F9 RID: 2553
		private const int DoNotReplaceCompareRecord = 0;

		// Token: 0x040009FA RID: 2554
		private const int ReplaceNewRecordForCompare = 1;

		// Token: 0x040009FB RID: 2555
		private const int ReplaceOldRecordForCompare = 2;

		// Token: 0x040009FC RID: 2556
		private readonly DataTable _table;

		// Token: 0x040009FD RID: 2557
		internal readonly IndexField[] _indexFields;

		// Token: 0x040009FE RID: 2558
		private readonly Comparison<DataRow> _comparison;

		// Token: 0x040009FF RID: 2559
		private readonly DataViewRowState _recordStates;

		// Token: 0x04000A00 RID: 2560
		private WeakReference _rowFilter;

		// Token: 0x04000A01 RID: 2561
		private Index.IndexTree _records;

		// Token: 0x04000A02 RID: 2562
		private int _recordCount;

		// Token: 0x04000A03 RID: 2563
		private int _refCount;

		// Token: 0x04000A04 RID: 2564
		private Listeners<DataViewListener> _listeners;

		// Token: 0x04000A05 RID: 2565
		private bool _suspendEvents;

		// Token: 0x04000A06 RID: 2566
		private readonly bool _isSharable;

		// Token: 0x04000A07 RID: 2567
		private readonly bool _hasRemoteAggregate;

		// Token: 0x04000A08 RID: 2568
		internal const int MaskBits = 2147483647;

		// Token: 0x04000A09 RID: 2569
		private static int s_objectTypeCount;

		// Token: 0x04000A0A RID: 2570
		private readonly int _objectID = Interlocked.Increment(ref Index.s_objectTypeCount);

		// Token: 0x02000129 RID: 297
		private sealed class IndexTree : RBTree<int>
		{
			// Token: 0x06001073 RID: 4211 RVA: 0x0004517B File Offset: 0x0004337B
			internal IndexTree(Index index) : base(TreeAccessMethod.KEY_SEARCH_AND_INDEX)
			{
				this._index = index;
			}

			// Token: 0x06001074 RID: 4212 RVA: 0x0004518B File Offset: 0x0004338B
			protected override int CompareNode(int record1, int record2)
			{
				return this._index.CompareRecords(record1, record2);
			}

			// Token: 0x06001075 RID: 4213 RVA: 0x0004519A File Offset: 0x0004339A
			protected override int CompareSateliteTreeNode(int record1, int record2)
			{
				return this._index.CompareDuplicateRecords(record1, record2);
			}

			// Token: 0x04000A0B RID: 2571
			private readonly Index _index;
		}

		// Token: 0x0200012A RID: 298
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001076 RID: 4214 RVA: 0x000451A9 File Offset: 0x000433A9
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001077 RID: 4215 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c()
			{
			}

			// Token: 0x06001078 RID: 4216 RVA: 0x000451B5 File Offset: 0x000433B5
			internal bool <.ctor>b__22_0(DataViewListener listener)
			{
				return listener != null;
			}

			// Token: 0x06001079 RID: 4217 RVA: 0x000451BB File Offset: 0x000433BB
			internal void <OnListChanged>b__85_0(DataViewListener listener, ListChangedEventArgs args, bool arg2, bool arg3)
			{
				listener.IndexListChanged(args);
			}

			// Token: 0x04000A0C RID: 2572
			public static readonly Index.<>c <>9 = new Index.<>c();

			// Token: 0x04000A0D RID: 2573
			public static Listeners<DataViewListener>.Func<DataViewListener, bool> <>9__22_0;

			// Token: 0x04000A0E RID: 2574
			public static Listeners<DataViewListener>.Action<DataViewListener, ListChangedEventArgs, bool, bool> <>9__85_0;
		}

		// Token: 0x0200012B RID: 299
		[CompilerGenerated]
		private sealed class <>c__DisplayClass86_0
		{
			// Token: 0x0600107A RID: 4218 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass86_0()
			{
			}

			// Token: 0x0600107B RID: 4219 RVA: 0x000451C4 File Offset: 0x000433C4
			internal void <MaintainDataView>b__0(DataViewListener listener, ListChangedType type, DataRow row, bool track)
			{
				listener.MaintainDataView(this.changedType, row, track);
			}

			// Token: 0x04000A0F RID: 2575
			public ListChangedType changedType;
		}
	}
}
