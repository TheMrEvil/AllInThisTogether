using System;

namespace Mono.CSharp
{
	// Token: 0x02000196 RID: 406
	public class NewDelegate : DelegateCreation
	{
		// Token: 0x060015E2 RID: 5602 RVA: 0x00068E10 File Offset: 0x00067010
		public NewDelegate(TypeSpec type, Arguments Arguments, Location loc)
		{
			this.type = type;
			this.Arguments = Arguments;
			this.loc = loc;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00068E30 File Offset: 0x00067030
		protected override Expression DoResolve(ResolveContext ec)
		{
			if (this.Arguments == null || this.Arguments.Count != 1)
			{
				ec.Report.Error(149, this.loc, "Method name expected");
				return null;
			}
			Argument argument = this.Arguments[0];
			if (!argument.ResolveMethodGroup(ec))
			{
				return null;
			}
			Expression expression = argument.Expr;
			AnonymousMethodExpression anonymousMethodExpression = expression as AnonymousMethodExpression;
			if (anonymousMethodExpression == null || ec.Module.Compiler.Settings.Version == LanguageVersion.ISO_1)
			{
				this.method_group = (expression as MethodGroupExpr);
				if (this.method_group == null)
				{
					if (expression.Type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
					{
						expression = Convert.ImplicitConversionRequired(ec, expression, this.type, this.loc);
					}
					else if (!expression.Type.IsDelegate)
					{
						expression.Error_UnexpectedKind(ec, ResolveFlags.Type | ResolveFlags.MethodGroup, this.loc);
						return null;
					}
					this.method_group = new MethodGroupExpr(Delegate.GetInvokeMethod(expression.Type), expression.Type, this.loc);
					this.method_group.InstanceExpression = expression;
				}
				return base.DoResolve(ec);
			}
			expression = anonymousMethodExpression.Compatible(ec, this.type);
			if (expression == null)
			{
				return null;
			}
			return expression.Resolve(ec);
		}

		// Token: 0x04000924 RID: 2340
		public Arguments Arguments;
	}
}
