using System;

namespace Mono.CSharp
{
	// Token: 0x02000175 RID: 373
	internal class OptionalAssign : SimpleAssign
	{
		// Token: 0x060011E9 RID: 4585 RVA: 0x0004A4B7 File Offset: 0x000486B7
		public OptionalAssign(Expression s, Location loc) : base(null, s, loc)
		{
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x0004A4C2 File Offset: 0x000486C2
		public override Location StartLocation
		{
			get
			{
				return Location.Null;
			}
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0004A4CC File Offset: 0x000486CC
		protected override Expression DoResolve(ResolveContext ec)
		{
			Expression expression = this.source.Clone(new CloneContext());
			expression = expression.Resolve(ec);
			if (expression == null)
			{
				return null;
			}
			if (ec.Module.Evaluator.DescribeTypeExpressions && !(ec.CurrentAnonymousMethod is AsyncInitializer))
			{
				ReportPrinter printer = ec.Report.SetPrinter(new SessionReportPrinter());
				Expression expression2;
				try
				{
					expression2 = this.source.Clone(new CloneContext());
					expression2 = expression2.Resolve(ec, ResolveFlags.Type);
					if (ec.Report.Errors > 0)
					{
						expression2 = null;
					}
				}
				finally
				{
					ec.Report.SetPrinter(printer);
				}
				if (expression2 is TypeExpr)
				{
					Arguments arguments = new Arguments(1);
					arguments.Add(new Argument(new TypeOf((TypeExpr)expression, base.Location)));
					return new Invocation(new SimpleName("Describe", base.Location), arguments).Resolve(ec);
				}
			}
			if (expression.Type.Kind == MemberKind.Void || expression is DynamicInvocation || expression is Assign)
			{
				return expression;
			}
			this.source = expression;
			Method method = (Method)ec.MemberContext.CurrentMemberDefinition;
			if (method.ParameterInfo.IsEmpty)
			{
				this.eclass = ExprClass.Value;
				this.type = InternalType.FakeInternalType;
				return this;
			}
			this.target = new SimpleName(method.ParameterInfo[0].Name, base.Location);
			return base.DoResolve(ec);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0004A648 File Offset: 0x00048848
		public override void EmitStatement(EmitContext ec)
		{
			if (this.target == null)
			{
				this.source.Emit(ec);
				return;
			}
			base.EmitStatement(ec);
		}
	}
}
