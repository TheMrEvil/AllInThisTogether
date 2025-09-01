using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200020F RID: 527
	internal class MethodBinaryExpression : SimpleBinaryExpression
	{
		// Token: 0x06000CED RID: 3309 RVA: 0x0002D19F File Offset: 0x0002B39F
		internal MethodBinaryExpression(ExpressionType nodeType, Expression left, Expression right, Type type, MethodInfo method) : base(nodeType, left, right, type)
		{
			this._method = method;
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0002D1B4 File Offset: 0x0002B3B4
		internal override MethodInfo GetMethod()
		{
			return this._method;
		}

		// Token: 0x0400091A RID: 2330
		private readonly MethodInfo _method;
	}
}
