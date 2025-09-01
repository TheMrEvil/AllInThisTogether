using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200025F RID: 607
	internal interface IParameterProvider
	{
		// Token: 0x060011BD RID: 4541
		ParameterExpression GetParameter(int index);

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060011BE RID: 4542
		int ParameterCount { get; }
	}
}
