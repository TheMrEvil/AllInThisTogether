using System;

namespace Mono.CSharp
{
	// Token: 0x02000168 RID: 360
	internal class DynamicConstructorBinder : DynamicExpressionStatement, IDynamicBinder
	{
		// Token: 0x06001195 RID: 4501 RVA: 0x000488F4 File Offset: 0x00046AF4
		public DynamicConstructorBinder(TypeSpec type, Arguments args, Location loc) : base(null, args, loc)
		{
			this.type = type;
			this.binder = this;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00048910 File Offset: 0x00046B10
		public Expression CreateCallSiteBinder(ResolveContext ec, Arguments args)
		{
			Arguments arguments = new Arguments(3);
			arguments.Add(new Argument(new DynamicExpressionStatement.BinderFlags(CSharpBinderFlags.None, this)));
			arguments.Add(new Argument(new TypeOf(ec.CurrentType, this.loc)));
			arguments.Add(new Argument(new ImplicitlyTypedArrayCreation(args.CreateDynamicBinderArguments(ec), this.loc)));
			return new Invocation(base.GetBinder("InvokeConstructor", this.loc), arguments);
		}
	}
}
