using System;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000209 RID: 521
	internal sealed class LogicalBinaryExpression : BinaryExpression
	{
		// Token: 0x06000CDA RID: 3290 RVA: 0x0002D0D0 File Offset: 0x0002B2D0
		internal LogicalBinaryExpression(ExpressionType nodeType, Expression left, Expression right) : base(left, right)
		{
			this.NodeType = nodeType;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0002D0E1 File Offset: 0x0002B2E1
		public sealed override Type Type
		{
			get
			{
				return typeof(bool);
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0002D0ED File Offset: 0x0002B2ED
		public sealed override ExpressionType NodeType
		{
			[CompilerGenerated]
			get
			{
				return this.<NodeType>k__BackingField;
			}
		}

		// Token: 0x04000915 RID: 2325
		[CompilerGenerated]
		private readonly ExpressionType <NodeType>k__BackingField;
	}
}
