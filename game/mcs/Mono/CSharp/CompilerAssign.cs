using System;

namespace Mono.CSharp
{
	// Token: 0x02000115 RID: 277
	internal class CompilerAssign : Assign
	{
		// Token: 0x06000DAF RID: 3503 RVA: 0x000328BE File Offset: 0x00030ABE
		public CompilerAssign(Expression target, Expression source, Location loc) : base(target, source, loc)
		{
			if (target.Type != null)
			{
				this.type = target.Type;
				this.eclass = ExprClass.Value;
			}
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x000328E4 File Offset: 0x00030AE4
		protected override Expression DoResolve(ResolveContext ec)
		{
			Expression result = base.DoResolve(ec);
			VariableReference variableReference = this.target as VariableReference;
			if (variableReference != null && variableReference.VariableInfo != null)
			{
				variableReference.VariableInfo.IsEverAssigned = false;
			}
			return result;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0003291B File Offset: 0x00030B1B
		public void UpdateSource(Expression source)
		{
			this.source = source;
		}
	}
}
