using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200020B RID: 523
	internal class ByRefAssignBinaryExpression : AssignBinaryExpression
	{
		// Token: 0x06000CE2 RID: 3298 RVA: 0x0002D124 File Offset: 0x0002B324
		internal ByRefAssignBinaryExpression(Expression left, Expression right) : base(left, right)
		{
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00007E1D File Offset: 0x0000601D
		internal override bool IsByRef
		{
			get
			{
				return true;
			}
		}
	}
}
