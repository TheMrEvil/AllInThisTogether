using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001B0 RID: 432
	public sealed class ClassCast : TypeCast
	{
		// Token: 0x060016C5 RID: 5829 RVA: 0x0006C344 File Offset: 0x0006A544
		public ClassCast(Expression child, TypeSpec return_type) : base(child, return_type)
		{
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x0006D201 File Offset: 0x0006B401
		public ClassCast(Expression child, TypeSpec return_type, bool forced) : base(child, return_type)
		{
			this.forced = forced;
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0006D214 File Offset: 0x0006B414
		public override void Emit(EmitContext ec)
		{
			base.Emit(ec);
			bool flag = TypeManager.IsGenericParameter(this.child.Type);
			if (flag)
			{
				ec.Emit(OpCodes.Box, this.child.Type);
			}
			if (this.type.IsGenericParameter)
			{
				ec.Emit(OpCodes.Unbox_Any, this.type);
				return;
			}
			if (flag && !this.forced)
			{
				return;
			}
			ec.Emit(OpCodes.Castclass, this.type);
		}

		// Token: 0x0400095A RID: 2394
		private readonly bool forced;
	}
}
