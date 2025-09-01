using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x020000F6 RID: 246
	internal sealed class LookupNode : ExpressionNode
	{
		// Token: 0x06000E9D RID: 3741 RVA: 0x0003D01A File Offset: 0x0003B21A
		internal LookupNode(DataTable table, string columnName, string relationName) : base(table)
		{
			this._relationName = relationName;
			this._columnName = columnName;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0003D034 File Offset: 0x0003B234
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			this._column = null;
			this._relation = null;
			if (table == null)
			{
				throw ExprException.ExpressionUnbound(this.ToString());
			}
			DataRelationCollection parentRelations = table.ParentRelations;
			if (this._relationName == null)
			{
				if (parentRelations.Count > 1)
				{
					throw ExprException.UnresolvedRelation(table.TableName, this.ToString());
				}
				this._relation = parentRelations[0];
			}
			else
			{
				this._relation = parentRelations[this._relationName];
			}
			if (this._relation == null)
			{
				throw ExprException.BindFailure(this._relationName);
			}
			DataTable parentTable = this._relation.ParentTable;
			this._column = parentTable.Columns[this._columnName];
			if (this._column == null)
			{
				throw ExprException.UnboundName(this._columnName);
			}
			int i;
			for (i = 0; i < list.Count; i++)
			{
				DataColumn dataColumn = list[i];
				if (this._column == dataColumn)
				{
					break;
				}
			}
			if (i >= list.Count)
			{
				list.Add(this._column);
			}
			AggregateNode.Bind(this._relation, list);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0003D13E File Offset: 0x0003B33E
		internal override object Eval()
		{
			throw ExprException.EvalNoContext();
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0003D148 File Offset: 0x0003B348
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			if (this._column == null || this._relation == null)
			{
				throw ExprException.ExpressionUnbound(this.ToString());
			}
			DataRow parentRow = row.GetParentRow(this._relation, version);
			if (parentRow == null)
			{
				return DBNull.Value;
			}
			return parentRow[this._column, parentRow.HasVersion(version) ? version : DataRowVersion.Current];
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00029AEE File Offset: 0x00027CEE
		internal override object Eval(int[] recordNos)
		{
			throw ExprException.ComputeNotAggregate(this.ToString());
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00006D64 File Offset: 0x00004F64
		internal override bool IsConstant()
		{
			return false;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00006D64 File Offset: 0x00004F64
		internal override bool IsTableConstant()
		{
			return false;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00006D64 File Offset: 0x00004F64
		internal override bool HasLocalAggregate()
		{
			return false;
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00006D64 File Offset: 0x00004F64
		internal override bool HasRemoteAggregate()
		{
			return false;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0003D1A5 File Offset: 0x0003B3A5
		internal override bool DependsOn(DataColumn column)
		{
			return this._column == column;
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00005696 File Offset: 0x00003896
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x0400090F RID: 2319
		private readonly string _relationName;

		// Token: 0x04000910 RID: 2320
		private readonly string _columnName;

		// Token: 0x04000911 RID: 2321
		private DataColumn _column;

		// Token: 0x04000912 RID: 2322
		private DataRelation _relation;
	}
}
