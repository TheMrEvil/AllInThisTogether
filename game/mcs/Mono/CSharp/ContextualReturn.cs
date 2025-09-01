using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000185 RID: 389
	public class ContextualReturn : Return
	{
		// Token: 0x0600126C RID: 4716 RVA: 0x0004D249 File Offset: 0x0004B449
		public ContextualReturn(Expression expr) : base(expr, expr.StartLocation)
		{
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0004D258 File Offset: 0x0004B458
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			return base.Expr.CreateExpressionTree(ec);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0004D268 File Offset: 0x0004B468
		protected override void DoEmit(EmitContext ec)
		{
			if (this.statement == null)
			{
				base.DoEmit(ec);
				return;
			}
			this.statement.EmitStatement(ec);
			if (this.unwind_protect)
			{
				ec.Emit(OpCodes.Leave, ec.CreateReturnLabel());
				return;
			}
			ec.Emit(OpCodes.Ret);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0004D2B8 File Offset: 0x0004B4B8
		protected override bool DoResolve(BlockContext ec)
		{
			if (ec.ReturnType.Kind != MemberKind.Void)
			{
				return base.DoResolve(ec);
			}
			base.Expr = base.Expr.Resolve(ec);
			if (base.Expr == null)
			{
				return false;
			}
			this.statement = (base.Expr as ExpressionStatement);
			if (this.statement == null)
			{
				base.Expr.Error_InvalidExpressionStatement(ec);
			}
			return true;
		}

		// Token: 0x040007BE RID: 1982
		private ExpressionStatement statement;
	}
}
