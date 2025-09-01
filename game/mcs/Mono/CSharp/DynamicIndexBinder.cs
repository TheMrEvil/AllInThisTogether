using System;

namespace Mono.CSharp
{
	// Token: 0x02000169 RID: 361
	internal class DynamicIndexBinder : DynamicMemberAssignable
	{
		// Token: 0x06001197 RID: 4503 RVA: 0x00048986 File Offset: 0x00046B86
		public DynamicIndexBinder(Arguments args, Location loc) : base(args, loc)
		{
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00048990 File Offset: 0x00046B90
		public DynamicIndexBinder(CSharpBinderFlags flags, Arguments args, Location loc) : this(args, loc)
		{
			this.flags = flags;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x000489A1 File Offset: 0x00046BA1
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.can_be_mutator = true;
			return base.DoResolve(ec);
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x000489B4 File Offset: 0x00046BB4
		protected override Expression CreateCallSiteBinder(ResolveContext ec, Arguments args, bool isSet)
		{
			Arguments arguments = new Arguments(3);
			arguments.Add(new Argument(new DynamicExpressionStatement.BinderFlags(this.flags, this)));
			arguments.Add(new Argument(new TypeOf(ec.CurrentType, this.loc)));
			arguments.Add(new Argument(new ImplicitlyTypedArrayCreation(args.CreateDynamicBinderArguments(ec), this.loc)));
			isSet |= ((this.flags & CSharpBinderFlags.ValueFromCompoundAssignment) > CSharpBinderFlags.None);
			return new Invocation(base.GetBinder(isSet ? "SetIndex" : "GetIndex", this.loc), arguments);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00048A4C File Offset: 0x00046C4C
		protected override Arguments CreateSetterArguments(ResolveContext rc, Expression rhs)
		{
			if (!this.can_be_mutator)
			{
				return base.CreateSetterArguments(rc, rhs);
			}
			Arguments arguments = new Arguments(base.Arguments.Count + 1);
			for (int i = 0; i < base.Arguments.Count; i++)
			{
				Expression expression = base.Arguments[i].Expr;
				if (expression is Constant || expression is VariableReference || expression is This)
				{
					arguments.Add(base.Arguments[i]);
				}
				else
				{
					LocalVariable localVariable = LocalVariable.CreateCompilerGenerated(expression.Type, rc.CurrentBlock, this.loc);
					expression = new SimpleAssign(localVariable.CreateReferenceExpression(rc, expression.Location), expression).Resolve(rc);
					base.Arguments[i].Expr = localVariable.CreateReferenceExpression(rc, expression.Location).Resolve(rc);
					arguments.Add(base.Arguments[i].Clone(expression));
				}
			}
			arguments.Add(new Argument(rhs));
			return arguments;
		}

		// Token: 0x04000788 RID: 1928
		private bool can_be_mutator;
	}
}
