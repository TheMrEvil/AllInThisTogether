using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200020A RID: 522
	internal class AssignBinaryExpression : BinaryExpression
	{
		// Token: 0x06000CDD RID: 3293 RVA: 0x0002D0F5 File Offset: 0x0002B2F5
		internal AssignBinaryExpression(Expression left, Expression right) : base(left, right)
		{
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0002D0FF File Offset: 0x0002B2FF
		public static AssignBinaryExpression Make(Expression left, Expression right, bool byRef)
		{
			if (byRef)
			{
				return new ByRefAssignBinaryExpression(left, right);
			}
			return new AssignBinaryExpression(left, right);
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x000023D1 File Offset: 0x000005D1
		internal virtual bool IsByRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0002D113 File Offset: 0x0002B313
		public sealed override Type Type
		{
			get
			{
				return base.Left.Type;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0002D120 File Offset: 0x0002B320
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Assign;
			}
		}
	}
}
