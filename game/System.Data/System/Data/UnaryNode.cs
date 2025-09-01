using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data
{
	// Token: 0x020000F9 RID: 249
	internal sealed class UnaryNode : ExpressionNode
	{
		// Token: 0x06000EBD RID: 3773 RVA: 0x0003D687 File Offset: 0x0003B887
		internal UnaryNode(DataTable table, int op, ExpressionNode right) : base(table)
		{
			this._op = op;
			this._right = right;
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0003D69E File Offset: 0x0003B89E
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			this._right.Bind(table, list);
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0002988C File Offset: 0x00027A8C
		internal override object Eval()
		{
			return this.Eval(null, DataRowVersion.Default);
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0003D6B4 File Offset: 0x0003B8B4
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			return this.EvalUnaryOp(this._op, this._right.Eval(row, version));
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0003D6CF File Offset: 0x0003B8CF
		internal override object Eval(int[] recordNos)
		{
			return this._right.Eval(recordNos);
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0003D6E0 File Offset: 0x0003B8E0
		private object EvalUnaryOp(int op, object vl)
		{
			object value = DBNull.Value;
			if (DataExpression.IsUnknown(vl))
			{
				return DBNull.Value;
			}
			switch (op)
			{
			case 0:
				return vl;
			case 1:
			{
				StorageType storageType = DataStorage.GetStorageType(vl.GetType());
				if (ExpressionNode.IsNumericSql(storageType))
				{
					switch (storageType)
					{
					case StorageType.Byte:
						return (int)(-(int)((byte)vl));
					case StorageType.Int16:
						return (int)(-(int)((short)vl));
					case StorageType.UInt16:
					case StorageType.UInt32:
					case StorageType.UInt64:
						break;
					case StorageType.Int32:
						return -(int)vl;
					case StorageType.Int64:
						return -(long)vl;
					case StorageType.Single:
						return -(float)vl;
					case StorageType.Double:
						return -(double)vl;
					case StorageType.Decimal:
						return -(decimal)vl;
					default:
						switch (storageType)
						{
						case StorageType.SqlDecimal:
							return -(SqlDecimal)vl;
						case StorageType.SqlDouble:
							return -(SqlDouble)vl;
						case StorageType.SqlInt16:
							return -(SqlInt16)vl;
						case StorageType.SqlInt32:
							return -(SqlInt32)vl;
						case StorageType.SqlInt64:
							return -(SqlInt64)vl;
						case StorageType.SqlMoney:
							return -(SqlMoney)vl;
						case StorageType.SqlSingle:
							return -(SqlSingle)vl;
						}
						break;
					}
					return DBNull.Value;
				}
				throw ExprException.TypeMismatch(this.ToString());
			}
			case 2:
			{
				StorageType storageType = DataStorage.GetStorageType(vl.GetType());
				if (ExpressionNode.IsNumericSql(storageType))
				{
					return vl;
				}
				throw ExprException.TypeMismatch(this.ToString());
			}
			case 3:
				if (vl is SqlBoolean)
				{
					if (((SqlBoolean)vl).IsFalse)
					{
						return SqlBoolean.True;
					}
					if (((SqlBoolean)vl).IsTrue)
					{
						return SqlBoolean.False;
					}
					throw ExprException.UnsupportedOperator(op);
				}
				else
				{
					if (DataExpression.ToBoolean(vl))
					{
						return false;
					}
					return true;
				}
				break;
			default:
				throw ExprException.UnsupportedOperator(op);
			}
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0003D931 File Offset: 0x0003BB31
		internal override bool IsConstant()
		{
			return this._right.IsConstant();
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0003D93E File Offset: 0x0003BB3E
		internal override bool IsTableConstant()
		{
			return this._right.IsTableConstant();
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0003D94B File Offset: 0x0003BB4B
		internal override bool HasLocalAggregate()
		{
			return this._right.HasLocalAggregate();
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0003D958 File Offset: 0x0003BB58
		internal override bool HasRemoteAggregate()
		{
			return this._right.HasRemoteAggregate();
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0003D965 File Offset: 0x0003BB65
		internal override bool DependsOn(DataColumn column)
		{
			return this._right.DependsOn(column);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0003D974 File Offset: 0x0003BB74
		internal override ExpressionNode Optimize()
		{
			this._right = this._right.Optimize();
			if (this.IsConstant())
			{
				object constant = this.Eval();
				return new ConstNode(base.table, ValueType.Object, constant, false);
			}
			return this;
		}

		// Token: 0x0400095A RID: 2394
		internal readonly int _op;

		// Token: 0x0400095B RID: 2395
		internal ExpressionNode _right;
	}
}
