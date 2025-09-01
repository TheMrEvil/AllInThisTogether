using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x020000FA RID: 250
	internal sealed class ZeroOpNode : ExpressionNode
	{
		// Token: 0x06000EC9 RID: 3785 RVA: 0x0003D9B1 File Offset: 0x0003BBB1
		internal ZeroOpNode(int op) : base(null)
		{
			this._op = op;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00007EED File Offset: 0x000060ED
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0003D9C4 File Offset: 0x0003BBC4
		internal override object Eval()
		{
			switch (this._op)
			{
			case 32:
				return DBNull.Value;
			case 33:
				return true;
			case 34:
				return false;
			default:
				return DBNull.Value;
			}
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0003AAFE File Offset: 0x00038CFE
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			return this.Eval();
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0003AAFE File Offset: 0x00038CFE
		internal override object Eval(int[] recordNos)
		{
			return this.Eval();
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00006D61 File Offset: 0x00004F61
		internal override bool IsConstant()
		{
			return true;
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x00006D61 File Offset: 0x00004F61
		internal override bool IsTableConstant()
		{
			return true;
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x00006D64 File Offset: 0x00004F64
		internal override bool HasLocalAggregate()
		{
			return false;
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x00006D64 File Offset: 0x00004F64
		internal override bool HasRemoteAggregate()
		{
			return false;
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00005696 File Offset: 0x00003896
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x0400095C RID: 2396
		internal readonly int _op;

		// Token: 0x0400095D RID: 2397
		internal const int zop_True = 1;

		// Token: 0x0400095E RID: 2398
		internal const int zop_False = 0;

		// Token: 0x0400095F RID: 2399
		internal const int zop_Null = -1;
	}
}
