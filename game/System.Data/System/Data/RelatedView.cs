using System;

namespace System.Data
{
	// Token: 0x02000120 RID: 288
	internal sealed class RelatedView : DataView, IFilter
	{
		// Token: 0x06001006 RID: 4102 RVA: 0x0004234C File Offset: 0x0004054C
		public RelatedView(DataColumn[] columns, object[] values) : base(columns[0].Table, false)
		{
			if (values == null)
			{
				throw ExceptionBuilder.ArgumentNull("values");
			}
			this._parentRowView = null;
			this._parentKey = null;
			this._childKey = new DataKey(columns, true);
			this._filterValues = values;
			base.ResetRowViewCache();
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x000423A3 File Offset: 0x000405A3
		public RelatedView(DataRowView parentRowView, DataKey parentKey, DataColumn[] childKeyColumns) : base(childKeyColumns[0].Table, false)
		{
			this._filterValues = null;
			this._parentRowView = parentRowView;
			this._parentKey = new DataKey?(parentKey);
			this._childKey = new DataKey(childKeyColumns, true);
			base.ResetRowViewCache();
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x000423E4 File Offset: 0x000405E4
		private object[] GetParentValues()
		{
			if (this._filterValues != null)
			{
				return this._filterValues;
			}
			if (!this._parentRowView.HasRecord())
			{
				return null;
			}
			return this._parentKey.Value.GetKeyValues(this._parentRowView.GetRecord());
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00042430 File Offset: 0x00040630
		public bool Invoke(DataRow row, DataRowVersion version)
		{
			object[] parentValues = this.GetParentValues();
			if (parentValues == null)
			{
				return false;
			}
			object[] keyValues = row.GetKeyValues(this._childKey, version);
			bool flag = true;
			if (keyValues.Length != parentValues.Length)
			{
				flag = false;
			}
			else
			{
				for (int i = 0; i < keyValues.Length; i++)
				{
					if (!keyValues[i].Equals(parentValues[i]))
					{
						flag = false;
						break;
					}
				}
			}
			IFilter filter = base.GetFilter();
			if (filter != null)
			{
				flag &= filter.Invoke(row, version);
			}
			return flag;
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00005696 File Offset: 0x00003896
		internal override IFilter GetFilter()
		{
			return this;
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x000424A0 File Offset: 0x000406A0
		public override DataRowView AddNew()
		{
			DataRowView dataRowView = base.AddNew();
			dataRowView.Row.SetKeyValues(this._childKey, this.GetParentValues());
			return dataRowView;
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x000424BF File Offset: 0x000406BF
		internal override void SetIndex(string newSort, DataViewRowState newRowStates, IFilter newRowFilter)
		{
			base.SetIndex2(newSort, newRowStates, newRowFilter, false);
			base.Reset();
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x000424D4 File Offset: 0x000406D4
		public override bool Equals(DataView dv)
		{
			RelatedView relatedView = dv as RelatedView;
			if (relatedView == null)
			{
				return false;
			}
			if (!base.Equals(dv))
			{
				return false;
			}
			object[] columnsReference;
			if (this._filterValues != null)
			{
				columnsReference = this._childKey.ColumnsReference;
				object[] value = columnsReference;
				columnsReference = relatedView._childKey.ColumnsReference;
				return this.CompareArray(value, columnsReference) && this.CompareArray(this._filterValues, relatedView._filterValues);
			}
			if (relatedView._filterValues != null)
			{
				return false;
			}
			columnsReference = this._childKey.ColumnsReference;
			object[] value2 = columnsReference;
			columnsReference = relatedView._childKey.ColumnsReference;
			if (this.CompareArray(value2, columnsReference))
			{
				columnsReference = this._parentKey.Value.ColumnsReference;
				object[] value3 = columnsReference;
				columnsReference = this._parentKey.Value.ColumnsReference;
				if (this.CompareArray(value3, columnsReference))
				{
					return this._parentRowView.Equals(relatedView._parentRowView);
				}
			}
			return false;
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x000425A8 File Offset: 0x000407A8
		private bool CompareArray(object[] value1, object[] value2)
		{
			if (value1 == null || value2 == null)
			{
				return value1 == value2;
			}
			if (value1.Length != value2.Length)
			{
				return false;
			}
			for (int i = 0; i < value1.Length; i++)
			{
				if (value1[i] != value2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040009D8 RID: 2520
		private readonly DataKey? _parentKey;

		// Token: 0x040009D9 RID: 2521
		private readonly DataKey _childKey;

		// Token: 0x040009DA RID: 2522
		private readonly DataRowView _parentRowView;

		// Token: 0x040009DB RID: 2523
		private readonly object[] _filterValues;
	}
}
