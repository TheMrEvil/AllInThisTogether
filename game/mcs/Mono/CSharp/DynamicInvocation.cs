using System;

namespace Mono.CSharp
{
	// Token: 0x0200016A RID: 362
	internal class DynamicInvocation : DynamicExpressionStatement, IDynamicBinder
	{
		// Token: 0x0600119C RID: 4508 RVA: 0x00048B52 File Offset: 0x00046D52
		public DynamicInvocation(ATypeNameExpression member, Arguments args, Location loc) : base(null, args, loc)
		{
			this.binder = this;
			this.member = member;
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00048B6B File Offset: 0x00046D6B
		public static DynamicInvocation CreateSpecialNameInvoke(ATypeNameExpression member, Arguments args, Location loc)
		{
			return new DynamicInvocation(member, args, loc)
			{
				flags = CSharpBinderFlags.InvokeSpecialName
			};
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00048B7C File Offset: 0x00046D7C
		public Expression CreateCallSiteBinder(ResolveContext ec, Arguments args)
		{
			Arguments arguments = new Arguments((this.member != null) ? 5 : 3);
			bool flag = this.member is MemberAccess;
			CSharpBinderFlags flags;
			if (!flag && this.member is SimpleName)
			{
				flags = CSharpBinderFlags.InvokeSimpleName;
				flag = true;
			}
			else
			{
				flags = CSharpBinderFlags.None;
			}
			arguments.Add(new Argument(new DynamicExpressionStatement.BinderFlags(flags, this)));
			if (flag)
			{
				arguments.Add(new Argument(new StringLiteral(ec.BuiltinTypes, this.member.Name, this.member.Location)));
			}
			if (this.member != null && this.member.HasTypeArguments)
			{
				TypeArguments typeArguments = this.member.TypeArguments;
				if (typeArguments.Resolve(ec, false))
				{
					ArrayInitializer arrayInitializer = new ArrayInitializer(typeArguments.Count, this.loc);
					foreach (TypeSpec type in typeArguments.Arguments)
					{
						arrayInitializer.Add(new TypeOf(type, this.loc));
					}
					arguments.Add(new Argument(new ImplicitlyTypedArrayCreation(arrayInitializer, this.loc)));
				}
			}
			else if (flag)
			{
				arguments.Add(new Argument(new NullLiteral(this.loc)));
			}
			arguments.Add(new Argument(new TypeOf(ec.CurrentType, this.loc)));
			Expression expr;
			if (args == null)
			{
				expr = new ArrayCreation(new MemberAccess(DynamicExpressionStatement.GetBinderNamespace(this.loc), "CSharpArgumentInfo", this.loc), new ArrayInitializer(0, this.loc), this.loc);
			}
			else
			{
				expr = new ImplicitlyTypedArrayCreation(args.CreateDynamicBinderArguments(ec), this.loc);
			}
			arguments.Add(new Argument(expr));
			return new Invocation(base.GetBinder(flag ? "InvokeMember" : "Invoke", this.loc), arguments);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00048D45 File Offset: 0x00046F45
		public override void EmitStatement(EmitContext ec)
		{
			this.flags |= CSharpBinderFlags.ResultDiscarded;
			base.EmitStatement(ec);
		}

		// Token: 0x04000789 RID: 1929
		private readonly ATypeNameExpression member;
	}
}
