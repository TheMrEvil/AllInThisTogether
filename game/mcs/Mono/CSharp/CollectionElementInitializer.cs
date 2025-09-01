using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x0200020D RID: 525
	internal class CollectionElementInitializer : Invocation
	{
		// Token: 0x06001AF6 RID: 6902 RVA: 0x00082EF3 File Offset: 0x000810F3
		public CollectionElementInitializer(Expression argument) : base(null, new Arguments(1))
		{
			this.arguments.Add(new CollectionElementInitializer.ElementInitializerArgument(argument));
			this.loc = argument.Location;
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x00082F20 File Offset: 0x00081120
		public CollectionElementInitializer(List<Expression> arguments, Location loc) : base(null, new Arguments(arguments.Count))
		{
			foreach (Expression e in arguments)
			{
				this.arguments.Add(new CollectionElementInitializer.ElementInitializerArgument(e));
			}
			this.loc = loc;
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x00082F94 File Offset: 0x00081194
		public CollectionElementInitializer(Location loc) : base(null, null)
		{
			this.loc = loc;
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x00082FA8 File Offset: 0x000811A8
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Arguments arguments = new Arguments(2);
			arguments.Add(new Argument(this.mg.CreateExpressionTree(ec)));
			ArrayInitializer arrayInitializer = new ArrayInitializer(this.arguments.Count, this.loc);
			foreach (Argument argument in this.arguments)
			{
				arrayInitializer.Add(argument.CreateExpressionTree(ec));
			}
			arguments.Add(new Argument(new ArrayCreation(Expression.CreateExpressionTypeExpression(ec, this.loc), arrayInitializer, this.loc)));
			return base.CreateExpressionFactoryCall(ec, "ElementInit", arguments);
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x00083068 File Offset: 0x00081268
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			CollectionElementInitializer collectionElementInitializer = (CollectionElementInitializer)t;
			if (this.arguments != null)
			{
				collectionElementInitializer.arguments = this.arguments.Clone(clonectx);
			}
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x00083096 File Offset: 0x00081296
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.expr = new CollectionElementInitializer.AddMemberAccess(ec.CurrentInitializerVariable, this.loc);
			return base.DoResolve(ec);
		}

		// Token: 0x020003BC RID: 956
		public class ElementInitializerArgument : Argument
		{
			// Token: 0x06002732 RID: 10034 RVA: 0x000BBC13 File Offset: 0x000B9E13
			public ElementInitializerArgument(Expression e) : base(e)
			{
			}
		}

		// Token: 0x020003BD RID: 957
		private sealed class AddMemberAccess : MemberAccess
		{
			// Token: 0x06002733 RID: 10035 RVA: 0x000BBC1C File Offset: 0x000B9E1C
			public AddMemberAccess(Expression expr, Location loc) : base(expr, "Add", loc)
			{
			}

			// Token: 0x06002734 RID: 10036 RVA: 0x000BBC2B File Offset: 0x000B9E2B
			public override void Error_TypeDoesNotContainDefinition(ResolveContext ec, TypeSpec type, string name)
			{
				if (TypeManager.HasElementType(type))
				{
					return;
				}
				base.Error_TypeDoesNotContainDefinition(ec, type, name);
			}
		}
	}
}
