using System;
using System.Linq.Expressions;

namespace Mono.CSharp
{
	// Token: 0x02000197 RID: 407
	internal class DelegateInvocation : ExpressionStatement
	{
		// Token: 0x060015E4 RID: 5604 RVA: 0x00068F57 File Offset: 0x00067157
		public DelegateInvocation(Expression instance_expr, Arguments args, bool conditionalAccessReceiver, Location loc)
		{
			this.InstanceExpr = instance_expr;
			this.arguments = args;
			this.conditionalAccessReceiver = conditionalAccessReceiver;
			this.loc = loc;
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00068F7C File Offset: 0x0006717C
		public override bool ContainsEmitWithAwait()
		{
			return this.InstanceExpr.ContainsEmitWithAwait() || (this.arguments != null && this.arguments.ContainsEmitWithAwait());
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00068FA4 File Offset: 0x000671A4
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Arguments args = Arguments.CreateForExpressionTree(ec, this.arguments, new Expression[]
			{
				this.InstanceExpr.CreateExpressionTree(ec)
			});
			return base.CreateExpressionFactoryCall(ec, "Invoke", args);
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00068FE0 File Offset: 0x000671E0
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.InstanceExpr.FlowAnalysis(fc);
			if (this.arguments != null)
			{
				this.arguments.FlowAnalysis(fc, null);
			}
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00069004 File Offset: 0x00067204
		protected override Expression DoResolve(ResolveContext ec)
		{
			TypeSpec type = this.InstanceExpr.Type;
			if (type == null)
			{
				return null;
			}
			this.method = Delegate.GetInvokeMethod(type);
			OverloadResolver overloadResolver = new OverloadResolver(new MemberSpec[]
			{
				this.method
			}, OverloadResolver.Restrictions.DelegateInvoke, this.loc);
			if (overloadResolver.ResolveMember<MethodSpec>(ec, ref this.arguments) == null && !overloadResolver.BestCandidateIsDynamic)
			{
				return null;
			}
			this.type = this.method.ReturnType;
			if (this.conditionalAccessReceiver)
			{
				this.type = Expression.LiftMemberType(ec, this.type);
			}
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x0006909C File Offset: 0x0006729C
		public override void Emit(EmitContext ec)
		{
			if (this.conditionalAccessReceiver)
			{
				ec.ConditionalAccess = new ConditionalAccessContext(this.type, ec.DefineLabel());
			}
			CallEmitter callEmitter = default(CallEmitter);
			callEmitter.InstanceExpression = this.InstanceExpr;
			callEmitter.Emit(ec, this.method, this.arguments, this.loc);
			if (this.conditionalAccessReceiver)
			{
				ec.CloseConditionalAccess((this.type.IsNullableType && this.type != this.method.ReturnType) ? this.type : null);
			}
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00069130 File Offset: 0x00067330
		public override void EmitStatement(EmitContext ec)
		{
			if (this.conditionalAccessReceiver)
			{
				ec.ConditionalAccess = new ConditionalAccessContext(this.type, ec.DefineLabel())
				{
					Statement = true
				};
			}
			CallEmitter callEmitter = default(CallEmitter);
			callEmitter.InstanceExpression = this.InstanceExpr;
			callEmitter.EmitStatement(ec, this.method, this.arguments, this.loc);
			if (this.conditionalAccessReceiver)
			{
				ec.CloseConditionalAccess(null);
			}
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x000691A1 File Offset: 0x000673A1
		public override Expression MakeExpression(BuilderContext ctx)
		{
			return Invocation.MakeExpression(ctx, this.InstanceExpr, this.method, this.arguments);
		}

		// Token: 0x04000925 RID: 2341
		private readonly Expression InstanceExpr;

		// Token: 0x04000926 RID: 2342
		private readonly bool conditionalAccessReceiver;

		// Token: 0x04000927 RID: 2343
		private Arguments arguments;

		// Token: 0x04000928 RID: 2344
		private MethodSpec method;
	}
}
