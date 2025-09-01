using System;

namespace System.Data
{
	// Token: 0x020000AF RID: 175
	internal sealed class ParentForeignKeyConstraintEnumerator : ForeignKeyConstraintEnumerator
	{
		// Token: 0x06000AAB RID: 2731 RVA: 0x0002C9DE File Offset: 0x0002ABDE
		public ParentForeignKeyConstraintEnumerator(DataSet dataSet, DataTable inTable) : base(dataSet)
		{
			this._table = inTable;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002C9EE File Offset: 0x0002ABEE
		protected override bool IsValidCandidate(Constraint constraint)
		{
			return constraint is ForeignKeyConstraint && ((ForeignKeyConstraint)constraint).RelatedTable == this._table;
		}

		// Token: 0x0400078C RID: 1932
		private readonly DataTable _table;
	}
}
