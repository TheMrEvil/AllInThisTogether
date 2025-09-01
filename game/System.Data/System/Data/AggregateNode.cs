using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x020000E4 RID: 228
	internal sealed class AggregateNode : ExpressionNode
	{
		// Token: 0x06000DE0 RID: 3552 RVA: 0x00037EB2 File Offset: 0x000360B2
		internal AggregateNode(DataTable table, FunctionId aggregateType, string columnName) : this(table, aggregateType, columnName, true, null)
		{
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00037EC0 File Offset: 0x000360C0
		internal AggregateNode(DataTable table, FunctionId aggregateType, string columnName, bool local, string relationName) : base(table)
		{
			this._aggregate = (Aggregate)aggregateType;
			if (aggregateType == FunctionId.Sum)
			{
				this._type = AggregateType.Sum;
			}
			else if (aggregateType == FunctionId.Avg)
			{
				this._type = AggregateType.Mean;
			}
			else if (aggregateType == FunctionId.Min)
			{
				this._type = AggregateType.Min;
			}
			else if (aggregateType == FunctionId.Max)
			{
				this._type = AggregateType.Max;
			}
			else if (aggregateType == FunctionId.Count)
			{
				this._type = AggregateType.Count;
			}
			else if (aggregateType == FunctionId.Var)
			{
				this._type = AggregateType.Var;
			}
			else
			{
				if (aggregateType != FunctionId.StDev)
				{
					throw ExprException.UndefinedFunction(Function.s_functionName[(int)aggregateType]);
				}
				this._type = AggregateType.StDev;
			}
			this._local = local;
			this._relationName = relationName;
			this._columnName = columnName;
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00037F64 File Offset: 0x00036164
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			if (table == null)
			{
				throw ExprException.AggregateUnbound(this.ToString());
			}
			if (this._local)
			{
				this._relation = null;
			}
			else
			{
				DataRelationCollection childRelations = table.ChildRelations;
				if (this._relationName == null)
				{
					if (childRelations.Count > 1)
					{
						throw ExprException.UnresolvedRelation(table.TableName, this.ToString());
					}
					if (childRelations.Count != 1)
					{
						throw ExprException.AggregateUnbound(this.ToString());
					}
					this._relation = childRelations[0];
				}
				else
				{
					this._relation = childRelations[this._relationName];
				}
			}
			this._childTable = ((this._relation == null) ? table : this._relation.ChildTable);
			this._column = this._childTable.Columns[this._columnName];
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

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00038088 File Offset: 0x00036288
		internal static void Bind(DataRelation relation, List<DataColumn> list)
		{
			if (relation != null)
			{
				foreach (DataColumn item in relation.ChildColumnsReference)
				{
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
				foreach (DataColumn item2 in relation.ParentColumnsReference)
				{
					if (!list.Contains(item2))
					{
						list.Add(item2);
					}
				}
			}
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0002988C File Offset: 0x00027A8C
		internal override object Eval()
		{
			return this.Eval(null, DataRowVersion.Default);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x000380EC File Offset: 0x000362EC
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			if (this._childTable == null)
			{
				throw ExprException.AggregateUnbound(this.ToString());
			}
			DataRow[] array;
			if (this._local)
			{
				array = new DataRow[this._childTable.Rows.Count];
				this._childTable.Rows.CopyTo(array, 0);
			}
			else
			{
				if (row == null)
				{
					throw ExprException.EvalNoContext();
				}
				if (this._relation == null)
				{
					throw ExprException.AggregateUnbound(this.ToString());
				}
				array = row.GetChildRows(this._relation, version);
			}
			if (version == DataRowVersion.Proposed)
			{
				version = DataRowVersion.Default;
			}
			List<int> list = new List<int>();
			int i = 0;
			while (i < array.Length)
			{
				if (array[i].RowState == DataRowState.Deleted)
				{
					if (DataRowAction.Rollback == array[i]._action)
					{
						version = DataRowVersion.Original;
						goto IL_BF;
					}
				}
				else if (DataRowAction.Rollback != array[i]._action || array[i].RowState != DataRowState.Added)
				{
					goto IL_BF;
				}
				IL_E1:
				i++;
				continue;
				IL_BF:
				if (version != DataRowVersion.Original || array[i]._oldRecord != -1)
				{
					list.Add(array[i].GetRecordFromVersion(version));
					goto IL_E1;
				}
				goto IL_E1;
			}
			int[] records = list.ToArray();
			return this._column.GetAggregateValue(records, this._type);
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x000381FD File Offset: 0x000363FD
		internal override object Eval(int[] records)
		{
			if (this._childTable == null)
			{
				throw ExprException.AggregateUnbound(this.ToString());
			}
			if (!this._local)
			{
				throw ExprException.ComputeNotAggregate(this.ToString());
			}
			return this._column.GetAggregateValue(records, this._type);
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00006D64 File Offset: 0x00004F64
		internal override bool IsConstant()
		{
			return false;
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00038239 File Offset: 0x00036439
		internal override bool IsTableConstant()
		{
			return this._local;
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00038239 File Offset: 0x00036439
		internal override bool HasLocalAggregate()
		{
			return this._local;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00038241 File Offset: 0x00036441
		internal override bool HasRemoteAggregate()
		{
			return !this._local;
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0003824C File Offset: 0x0003644C
		internal override bool DependsOn(DataColumn column)
		{
			return this._column == column || (this._column.Computed && this._column.DataExpression.DependsOn(column));
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00005696 File Offset: 0x00003896
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x0400088A RID: 2186
		private readonly AggregateType _type;

		// Token: 0x0400088B RID: 2187
		private readonly Aggregate _aggregate;

		// Token: 0x0400088C RID: 2188
		private readonly bool _local;

		// Token: 0x0400088D RID: 2189
		private readonly string _relationName;

		// Token: 0x0400088E RID: 2190
		private readonly string _columnName;

		// Token: 0x0400088F RID: 2191
		private DataTable _childTable;

		// Token: 0x04000890 RID: 2192
		private DataColumn _column;

		// Token: 0x04000891 RID: 2193
		private DataRelation _relation;
	}
}
