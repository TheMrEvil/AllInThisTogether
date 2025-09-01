using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002D0 RID: 720
	internal sealed class SpilledExpressionBlock : BlockN
	{
		// Token: 0x060015F8 RID: 5624 RVA: 0x0004A237 File Offset: 0x00048437
		internal SpilledExpressionBlock(IReadOnlyList<Expression> expressions) : base(expressions)
		{
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			throw ContractUtils.Unreachable;
		}
	}
}
