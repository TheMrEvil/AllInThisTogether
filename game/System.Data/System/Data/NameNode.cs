using System;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data
{
	// Token: 0x020000F7 RID: 247
	internal sealed class NameNode : ExpressionNode
	{
		// Token: 0x06000EA8 RID: 3752 RVA: 0x0003D1B3 File Offset: 0x0003B3B3
		internal NameNode(DataTable table, char[] text, int start, int pos) : base(table)
		{
			this._name = NameNode.ParseName(text, start, pos);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0003D1CB File Offset: 0x0003B3CB
		internal NameNode(DataTable table, string name) : base(table)
		{
			this._name = name;
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x0003D1DB File Offset: 0x0003B3DB
		internal override bool IsSqlColumn
		{
			get
			{
				return this._column.IsSqlType;
			}
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0003D1E8 File Offset: 0x0003B3E8
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			if (table == null)
			{
				throw ExprException.UnboundName(this._name);
			}
			try
			{
				this._column = table.Columns[this._name];
			}
			catch (Exception e)
			{
				this._found = false;
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				throw ExprException.UnboundName(this._name);
			}
			if (this._column == null)
			{
				throw ExprException.UnboundName(this._name);
			}
			this._name = this._column.ColumnName;
			this._found = true;
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
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0003D13E File Offset: 0x0003B33E
		internal override object Eval()
		{
			throw ExprException.EvalNoContext();
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003D2B4 File Offset: 0x0003B4B4
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			if (!this._found)
			{
				throw ExprException.UnboundName(this._name);
			}
			if (row != null)
			{
				return this._column[row.GetRecordFromVersion(version)];
			}
			if (this.IsTableConstant())
			{
				return this._column.DataExpression.Evaluate();
			}
			throw ExprException.UnboundName(this._name);
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00029AEE File Offset: 0x00027CEE
		internal override object Eval(int[] records)
		{
			throw ExprException.ComputeNotAggregate(this.ToString());
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00006D64 File Offset: 0x00004F64
		internal override bool IsConstant()
		{
			return false;
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003D30F File Offset: 0x0003B50F
		internal override bool IsTableConstant()
		{
			return this._column != null && this._column.Computed && this._column.DataExpression.IsTableAggregate();
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0003D338 File Offset: 0x0003B538
		internal override bool HasLocalAggregate()
		{
			return this._column != null && this._column.Computed && this._column.DataExpression.HasLocalAggregate();
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0003D361 File Offset: 0x0003B561
		internal override bool HasRemoteAggregate()
		{
			return this._column != null && this._column.Computed && this._column.DataExpression.HasRemoteAggregate();
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0003D38A File Offset: 0x0003B58A
		internal override bool DependsOn(DataColumn column)
		{
			return this._column == column || (this._column.Computed && this._column.DataExpression.DependsOn(column));
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00005696 File Offset: 0x00003896
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0003D3B8 File Offset: 0x0003B5B8
		internal static string ParseName(char[] text, int start, int pos)
		{
			char c = '\0';
			string text2 = string.Empty;
			int num = start;
			int num2 = pos;
			checked
			{
				if (text[start] == '`')
				{
					start++;
					pos--;
					c = '\\';
					text2 = "`";
				}
				else if (text[start] == '[')
				{
					start++;
					pos--;
					c = '\\';
					text2 = "]\\";
				}
			}
			if (c != '\0')
			{
				int num3 = start;
				for (int i = start; i < pos; i++)
				{
					if (text[i] == c && i + 1 < pos && text2.IndexOf(text[i + 1]) >= 0)
					{
						i++;
					}
					text[num3] = text[i];
					num3++;
				}
				pos = num3;
			}
			if (pos == start)
			{
				throw ExprException.InvalidName(new string(text, num, num2 - num));
			}
			return new string(text, start, pos - start);
		}

		// Token: 0x04000913 RID: 2323
		internal char _open;

		// Token: 0x04000914 RID: 2324
		internal char _close;

		// Token: 0x04000915 RID: 2325
		internal string _name;

		// Token: 0x04000916 RID: 2326
		internal bool _found;

		// Token: 0x04000917 RID: 2327
		internal bool _type;

		// Token: 0x04000918 RID: 2328
		internal DataColumn _column;
	}
}
