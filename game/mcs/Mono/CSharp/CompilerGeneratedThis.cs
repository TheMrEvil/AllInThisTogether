using System;

namespace Mono.CSharp
{
	// Token: 0x020001EC RID: 492
	internal sealed class CompilerGeneratedThis : This
	{
		// Token: 0x060019C7 RID: 6599 RVA: 0x0007F655 File Offset: 0x0007D855
		public CompilerGeneratedThis(TypeSpec type, Location loc) : base(loc)
		{
			this.type = type;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0007F668 File Offset: 0x0007D868
		protected override Expression DoResolve(ResolveContext rc)
		{
			this.eclass = ExprClass.Variable;
			Block currentBlock = rc.CurrentBlock;
			if (currentBlock != null)
			{
				ToplevelBlock topBlock = currentBlock.ParametersBlock.TopBlock;
				if (topBlock.ThisVariable != null)
				{
					this.variable_info = topBlock.ThisVariable.VariableInfo;
				}
			}
			return this;
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0007568C File Offset: 0x0007388C
		public override Expression DoResolveLValue(ResolveContext rc, Expression right_side)
		{
			return this.DoResolve(rc);
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x000055E7 File Offset: 0x000037E7
		public override HoistedVariable GetHoistedVariable(AnonymousExpression ae)
		{
			return null;
		}
	}
}
