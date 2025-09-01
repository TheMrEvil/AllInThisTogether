using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x020001CB RID: 459
	public class UserOperatorCall : Expression
	{
		// Token: 0x0600181F RID: 6175 RVA: 0x00074199 File Offset: 0x00072399
		public UserOperatorCall(MethodSpec oper, Arguments args, Func<ResolveContext, Expression, Expression> expr_tree, Location loc)
		{
			this.oper = oper;
			this.arguments = args;
			this.expr_tree = expr_tree;
			this.type = oper.ReturnType;
			this.eclass = ExprClass.Value;
			this.loc = loc;
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x000741D1 File Offset: 0x000723D1
		public override bool ContainsEmitWithAwait()
		{
			return this.arguments.ContainsEmitWithAwait();
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x000741E0 File Offset: 0x000723E0
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			if (this.expr_tree != null)
			{
				return this.expr_tree(ec, new TypeOfMethod(this.oper, this.loc));
			}
			Arguments args = Arguments.CreateForExpressionTree(ec, this.arguments, new Expression[]
			{
				new NullLiteral(this.loc),
				new TypeOfMethod(this.oper, this.loc)
			});
			return base.CreateExpressionFactoryCall(ec, "Call", args);
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0000AF70 File Offset: 0x00009170
		protected override void CloneTo(CloneContext context, Expression target)
		{
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00005936 File Offset: 0x00003B36
		protected override Expression DoResolve(ResolveContext ec)
		{
			return this;
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x00074258 File Offset: 0x00072458
		public override void Emit(EmitContext ec)
		{
			default(CallEmitter).Emit(ec, this.oper, this.arguments, this.loc);
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00074287 File Offset: 0x00072487
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.arguments.FlowAnalysis(fc, null);
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00074296 File Offset: 0x00072496
		public override Expression MakeExpression(BuilderContext ctx)
		{
			return Expression.Call((MethodInfo)this.oper.GetMetaInfo(), Arguments.MakeExpression(this.arguments, ctx));
		}

		// Token: 0x04000992 RID: 2450
		protected readonly Arguments arguments;

		// Token: 0x04000993 RID: 2451
		protected readonly MethodSpec oper;

		// Token: 0x04000994 RID: 2452
		private readonly Func<ResolveContext, Expression, Expression> expr_tree;
	}
}
