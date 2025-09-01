using System;
using System.Collections.Generic;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002A4 RID: 676
	internal sealed class AnalyzedTree
	{
		// Token: 0x0600141C RID: 5148 RVA: 0x0003DCE6 File Offset: 0x0003BEE6
		public AnalyzedTree()
		{
		}

		// Token: 0x04000A8A RID: 2698
		internal readonly Dictionary<object, CompilerScope> Scopes = new Dictionary<object, CompilerScope>();

		// Token: 0x04000A8B RID: 2699
		internal readonly Dictionary<LambdaExpression, BoundConstants> Constants = new Dictionary<LambdaExpression, BoundConstants>();
	}
}
