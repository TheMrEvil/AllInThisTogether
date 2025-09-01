using System;

namespace Mono.CSharp
{
	// Token: 0x02000166 RID: 358
	internal class DynamicEventCompoundAssign : ExpressionStatement
	{
		// Token: 0x0600118D RID: 4493 RVA: 0x00048714 File Offset: 0x00046914
		public DynamicEventCompoundAssign(string name, Arguments args, ExpressionStatement assignment, ExpressionStatement invoke, Location loc)
		{
			this.condition = new DynamicEventCompoundAssign.IsEvent(name, args, loc);
			this.invoke = invoke;
			this.assign = assignment;
			this.loc = loc;
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00048742 File Offset: 0x00046942
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			return this.condition.CreateExpressionTree(ec);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00048750 File Offset: 0x00046950
		protected override Expression DoResolve(ResolveContext rc)
		{
			this.type = rc.BuiltinTypes.Dynamic;
			this.eclass = ExprClass.Value;
			this.condition = this.condition.Resolve(rc);
			return this;
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00048780 File Offset: 0x00046980
		public override void Emit(EmitContext ec)
		{
			ResolveContext rc = new ResolveContext(ec.MemberContext);
			new Conditional(new BooleanExpression(this.condition), this.invoke, this.assign, this.loc).Resolve(rc).Emit(ec);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x000487C8 File Offset: 0x000469C8
		public override void EmitStatement(EmitContext ec)
		{
			If @if = new If(this.condition, new StatementExpression(this.invoke), new StatementExpression(this.assign), this.loc);
			using (ec.With(BuilderContext.Options.OmitDebugInfo, true))
			{
				@if.Emit(ec);
			}
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00048830 File Offset: 0x00046A30
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.invoke.FlowAnalysis(fc);
		}

		// Token: 0x04000785 RID: 1925
		private Expression condition;

		// Token: 0x04000786 RID: 1926
		private ExpressionStatement invoke;

		// Token: 0x04000787 RID: 1927
		private ExpressionStatement assign;

		// Token: 0x02000391 RID: 913
		private class IsEvent : DynamicExpressionStatement, IDynamicBinder
		{
			// Token: 0x060026C8 RID: 9928 RVA: 0x000B6FFC File Offset: 0x000B51FC
			public IsEvent(string name, Arguments args, Location loc) : base(null, args, loc)
			{
				this.name = name;
				this.binder = this;
			}

			// Token: 0x060026C9 RID: 9929 RVA: 0x000B7018 File Offset: 0x000B5218
			public Expression CreateCallSiteBinder(ResolveContext ec, Arguments args)
			{
				this.type = ec.BuiltinTypes.Bool;
				Arguments arguments = new Arguments(3);
				arguments.Add(new Argument(new DynamicExpressionStatement.BinderFlags(CSharpBinderFlags.None, this)));
				arguments.Add(new Argument(new StringLiteral(ec.BuiltinTypes, this.name, this.loc)));
				arguments.Add(new Argument(new TypeOf(ec.CurrentType, this.loc)));
				return new Invocation(base.GetBinder("IsEvent", this.loc), arguments);
			}

			// Token: 0x04000F8D RID: 3981
			private string name;
		}
	}
}
