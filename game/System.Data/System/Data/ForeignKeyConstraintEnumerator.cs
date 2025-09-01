using System;

namespace System.Data
{
	// Token: 0x020000AD RID: 173
	internal class ForeignKeyConstraintEnumerator : ConstraintEnumerator
	{
		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002C98E File Offset: 0x0002AB8E
		public ForeignKeyConstraintEnumerator(DataSet dataSet) : base(dataSet)
		{
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002C997 File Offset: 0x0002AB97
		protected override bool IsValidCandidate(Constraint constraint)
		{
			return constraint is ForeignKeyConstraint;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002C9A2 File Offset: 0x0002ABA2
		public ForeignKeyConstraint GetForeignKeyConstraint()
		{
			return (ForeignKeyConstraint)base.CurrentObject;
		}
	}
}
