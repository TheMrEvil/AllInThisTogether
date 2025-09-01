using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200020D RID: 525
	internal sealed class OpAssignMethodConversionBinaryExpression : MethodBinaryExpression
	{
		// Token: 0x06000CE8 RID: 3304 RVA: 0x0002D157 File Offset: 0x0002B357
		internal OpAssignMethodConversionBinaryExpression(ExpressionType nodeType, Expression left, Expression right, Type type, MethodInfo method, LambdaExpression conversion) : base(nodeType, left, right, type, method)
		{
			this._conversion = conversion;
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0002D16E File Offset: 0x0002B36E
		internal override LambdaExpression GetConversion()
		{
			return this._conversion;
		}

		// Token: 0x04000917 RID: 2327
		private readonly LambdaExpression _conversion;
	}
}
