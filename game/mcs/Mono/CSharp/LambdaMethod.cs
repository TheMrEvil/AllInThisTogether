using System;

namespace Mono.CSharp
{
	// Token: 0x02000184 RID: 388
	internal class LambdaMethod : AnonymousMethodBody
	{
		// Token: 0x06001268 RID: 4712 RVA: 0x0004D1A2 File Offset: 0x0004B3A2
		public LambdaMethod(ParametersCompiled parameters, ParametersBlock block, TypeSpec return_type, TypeSpec delegate_type, Location loc) : base(parameters, block, return_type, delegate_type, loc)
		{
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x0004D192 File Offset: 0x0004B392
		public override string ContainerType
		{
			get
			{
				return "lambda expression";
			}
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0000AF70 File Offset: 0x00009170
		protected override void CloneTo(CloneContext clonectx, Expression target)
		{
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0004D1B4 File Offset: 0x0004B3B4
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			BlockContext ec2 = new BlockContext(ec.MemberContext, base.Block, this.ReturnType);
			Expression expr = this.parameters.CreateExpressionTree(ec2, this.loc);
			Expression expression = base.Block.CreateExpressionTree(ec);
			if (expression == null)
			{
				return null;
			}
			Arguments arguments = new Arguments(2);
			arguments.Add(new Argument(expression));
			arguments.Add(new Argument(expr));
			return base.CreateExpressionFactoryCall(ec, "Lambda", new TypeArguments(new FullNamedExpression[]
			{
				new TypeExpression(this.type, this.loc)
			}), arguments);
		}
	}
}
