using System;

namespace Mono.CSharp
{
	// Token: 0x0200016B RID: 363
	internal class DynamicMemberBinder : DynamicMemberAssignable
	{
		// Token: 0x060011A0 RID: 4512 RVA: 0x00048D60 File Offset: 0x00046F60
		public DynamicMemberBinder(string name, Arguments args, Location loc) : base(args, loc)
		{
			this.name = name;
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00048D71 File Offset: 0x00046F71
		public DynamicMemberBinder(string name, CSharpBinderFlags flags, Arguments args, Location loc) : this(name, args, loc)
		{
			this.flags = flags;
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00048D84 File Offset: 0x00046F84
		protected override Expression CreateCallSiteBinder(ResolveContext ec, Arguments args, bool isSet)
		{
			Arguments arguments = new Arguments(4);
			arguments.Add(new Argument(new DynamicExpressionStatement.BinderFlags(this.flags, this)));
			arguments.Add(new Argument(new StringLiteral(ec.BuiltinTypes, this.name, this.loc)));
			arguments.Add(new Argument(new TypeOf(ec.CurrentType, this.loc)));
			arguments.Add(new Argument(new ImplicitlyTypedArrayCreation(args.CreateDynamicBinderArguments(ec), this.loc)));
			isSet |= ((this.flags & CSharpBinderFlags.ValueFromCompoundAssignment) > CSharpBinderFlags.None);
			return new Invocation(base.GetBinder(isSet ? "SetMember" : "GetMember", this.loc), arguments);
		}

		// Token: 0x0400078A RID: 1930
		private readonly string name;
	}
}
