using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200020C RID: 524
	internal sealed class CoalesceConversionBinaryExpression : BinaryExpression
	{
		// Token: 0x06000CE4 RID: 3300 RVA: 0x0002D12E File Offset: 0x0002B32E
		internal CoalesceConversionBinaryExpression(Expression left, Expression right, LambdaExpression conversion) : base(left, right)
		{
			this._conversion = conversion;
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0002D13F File Offset: 0x0002B33F
		internal override LambdaExpression GetConversion()
		{
			return this._conversion;
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0002D147 File Offset: 0x0002B347
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Coalesce;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0002D14A File Offset: 0x0002B34A
		public sealed override Type Type
		{
			get
			{
				return base.Right.Type;
			}
		}

		// Token: 0x04000916 RID: 2326
		private readonly LambdaExpression _conversion;
	}
}
