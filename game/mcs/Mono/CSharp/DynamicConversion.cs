using System;

namespace Mono.CSharp
{
	// Token: 0x02000167 RID: 359
	internal class DynamicConversion : DynamicExpressionStatement, IDynamicBinder
	{
		// Token: 0x06001193 RID: 4499 RVA: 0x0004883E File Offset: 0x00046A3E
		public DynamicConversion(TypeSpec targetType, CSharpBinderFlags flags, Arguments args, Location loc) : base(null, args, loc)
		{
			this.type = targetType;
			this.flags = flags;
			this.binder = this;
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00048860 File Offset: 0x00046A60
		public Expression CreateCallSiteBinder(ResolveContext ec, Arguments args)
		{
			Arguments arguments = new Arguments(3);
			this.flags |= (ec.HasSet(ResolveContext.Options.CheckedScope) ? CSharpBinderFlags.CheckedContext : CSharpBinderFlags.None);
			arguments.Add(new Argument(new DynamicExpressionStatement.BinderFlags(this.flags, this)));
			arguments.Add(new Argument(new TypeOf(this.type, this.loc)));
			arguments.Add(new Argument(new TypeOf(ec.CurrentType, this.loc)));
			return new Invocation(base.GetBinder("Convert", this.loc), arguments);
		}
	}
}
