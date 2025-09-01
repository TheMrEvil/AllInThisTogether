using System;

namespace System.Data
{
	// Token: 0x020000AE RID: 174
	internal sealed class ChildForeignKeyConstraintEnumerator : ForeignKeyConstraintEnumerator
	{
		// Token: 0x06000AA9 RID: 2729 RVA: 0x0002C9AF File Offset: 0x0002ABAF
		public ChildForeignKeyConstraintEnumerator(DataSet dataSet, DataTable inTable) : base(dataSet)
		{
			this._table = inTable;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0002C9BF File Offset: 0x0002ABBF
		protected override bool IsValidCandidate(Constraint constraint)
		{
			return constraint is ForeignKeyConstraint && ((ForeignKeyConstraint)constraint).Table == this._table;
		}

		// Token: 0x0400078B RID: 1931
		private readonly DataTable _table;
	}
}
