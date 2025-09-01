using System;
using System.Linq.Expressions;

namespace Mono.CSharp
{
	// Token: 0x020001A6 RID: 422
	public abstract class TypeCast : Expression
	{
		// Token: 0x0600167E RID: 5758 RVA: 0x0006C1FC File Offset: 0x0006A3FC
		protected TypeCast(Expression child, TypeSpec return_type)
		{
			this.eclass = child.eclass;
			this.loc = child.Location;
			this.type = return_type;
			this.child = child;
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x0006C22A File Offset: 0x0006A42A
		public Expression Child
		{
			get
			{
				return this.child;
			}
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0006C232 File Offset: 0x0006A432
		public override bool ContainsEmitWithAwait()
		{
			return this.child.ContainsEmitWithAwait();
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x0006C240 File Offset: 0x0006A440
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Arguments arguments = new Arguments(2);
			arguments.Add(new Argument(this.child.CreateExpressionTree(ec)));
			arguments.Add(new Argument(new TypeOf(this.type, this.loc)));
			if (this.type.IsPointer || this.child.Type.IsPointer)
			{
				base.Error_PointerInsideExpressionTree(ec);
			}
			return base.CreateExpressionFactoryCall(ec, ec.HasSet(ResolveContext.Options.CheckedScope) ? "ConvertChecked" : "Convert", arguments);
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00005936 File Offset: 0x00003B36
		protected override Expression DoResolve(ResolveContext ec)
		{
			return this;
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x0006C2CA File Offset: 0x0006A4CA
		public override void Emit(EmitContext ec)
		{
			this.child.Emit(ec);
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x0006C2D8 File Offset: 0x0006A4D8
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.child.FlowAnalysis(fc);
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0006C2E8 File Offset: 0x0006A4E8
		public override Expression MakeExpression(BuilderContext ctx)
		{
			if (!ctx.HasSet(BuilderContext.Options.CheckedScope))
			{
				return Expression.Convert(this.child.MakeExpression(ctx), this.type.GetMetaInfo());
			}
			return Expression.ConvertChecked(this.child.MakeExpression(ctx), this.type.GetMetaInfo());
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x0000AF70 File Offset: 0x00009170
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x0006C337 File Offset: 0x0006A537
		public override bool IsNull
		{
			get
			{
				return this.child.IsNull;
			}
		}

		// Token: 0x04000953 RID: 2387
		protected readonly Expression child;
	}
}
