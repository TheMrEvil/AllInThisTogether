using System;

namespace Mono.CSharp
{
	// Token: 0x020001B2 RID: 434
	public abstract class CompositeExpression : Expression
	{
		// Token: 0x060016D7 RID: 5847 RVA: 0x0006D3C0 File Offset: 0x0006B5C0
		protected CompositeExpression(Expression expr)
		{
			this.expr = expr;
			this.loc = expr.Location;
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x0006D3DB File Offset: 0x0006B5DB
		public override bool ContainsEmitWithAwait()
		{
			return this.expr.ContainsEmitWithAwait();
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x0006D3E8 File Offset: 0x0006B5E8
		public override Expression CreateExpressionTree(ResolveContext rc)
		{
			return this.expr.CreateExpressionTree(rc);
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x0006D3F6 File Offset: 0x0006B5F6
		public Expression Child
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x0006D400 File Offset: 0x0006B600
		protected override Expression DoResolve(ResolveContext rc)
		{
			this.expr = this.expr.Resolve(rc);
			if (this.expr == null)
			{
				return null;
			}
			this.type = this.expr.Type;
			this.eclass = this.expr.eclass;
			return this;
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x0006D44C File Offset: 0x0006B64C
		public override void Emit(EmitContext ec)
		{
			this.expr.Emit(ec);
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x0006D45A File Offset: 0x0006B65A
		public override bool IsNull
		{
			get
			{
				return this.expr.IsNull;
			}
		}

		// Token: 0x0400095D RID: 2397
		protected Expression expr;
	}
}
