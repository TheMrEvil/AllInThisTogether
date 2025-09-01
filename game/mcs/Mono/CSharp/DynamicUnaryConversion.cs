using System;

namespace Mono.CSharp
{
	// Token: 0x0200016D RID: 365
	internal class DynamicUnaryConversion : DynamicExpressionStatement, IDynamicBinder
	{
		// Token: 0x060011AC RID: 4524 RVA: 0x00048F40 File Offset: 0x00047140
		public DynamicUnaryConversion(string name, Arguments args, Location loc) : base(null, args, loc)
		{
			this.name = name;
			this.binder = this;
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00048F59 File Offset: 0x00047159
		public static DynamicUnaryConversion CreateIsTrue(ResolveContext rc, Arguments args, Location loc)
		{
			return new DynamicUnaryConversion("IsTrue", args, loc)
			{
				type = rc.BuiltinTypes.Bool
			};
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00048F78 File Offset: 0x00047178
		public static DynamicUnaryConversion CreateIsFalse(ResolveContext rc, Arguments args, Location loc)
		{
			return new DynamicUnaryConversion("IsFalse", args, loc)
			{
				type = rc.BuiltinTypes.Bool
			};
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00048F98 File Offset: 0x00047198
		public Expression CreateCallSiteBinder(ResolveContext ec, Arguments args)
		{
			Arguments arguments = new Arguments(4);
			MemberAccess expr = new MemberAccess(new MemberAccess(new QualifiedAliasMember(QualifiedAliasMember.GlobalAlias, "System", this.loc), "Linq", this.loc), "Expressions", this.loc);
			CSharpBinderFlags flags = ec.HasSet(ResolveContext.Options.CheckedScope) ? CSharpBinderFlags.CheckedContext : CSharpBinderFlags.None;
			arguments.Add(new Argument(new DynamicExpressionStatement.BinderFlags(flags, this)));
			arguments.Add(new Argument(new MemberAccess(new MemberAccess(expr, "ExpressionType", this.loc), this.name, this.loc)));
			arguments.Add(new Argument(new TypeOf(ec.CurrentType, this.loc)));
			arguments.Add(new Argument(new ImplicitlyTypedArrayCreation(args.CreateDynamicBinderArguments(ec), this.loc)));
			return new Invocation(base.GetBinder("UnaryOperation", this.loc), arguments);
		}

		// Token: 0x0400078D RID: 1933
		private readonly string name;
	}
}
