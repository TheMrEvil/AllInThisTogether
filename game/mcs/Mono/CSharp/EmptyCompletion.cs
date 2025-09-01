using System;

namespace Mono.CSharp
{
	// Token: 0x0200013C RID: 316
	public class EmptyCompletion : CompletingExpression
	{
		// Token: 0x06000FE1 RID: 4065 RVA: 0x0000AF70 File Offset: 0x00009170
		protected override void CloneTo(CloneContext clonectx, Expression target)
		{
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00041446 File Offset: 0x0003F646
		protected override Expression DoResolve(ResolveContext rc)
		{
			throw new CompletionResult("", new string[0]);
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x00041458 File Offset: 0x0003F658
		public EmptyCompletion()
		{
		}
	}
}
