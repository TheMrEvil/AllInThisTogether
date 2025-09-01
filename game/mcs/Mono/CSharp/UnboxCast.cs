using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001AC RID: 428
	public class UnboxCast : TypeCast
	{
		// Token: 0x060016B7 RID: 5815 RVA: 0x0006C344 File Offset: 0x0006A544
		public UnboxCast(Expression expr, TypeSpec return_type) : base(expr, return_type)
		{
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x00005936 File Offset: 0x00003B36
		protected override Expression DoResolve(ResolveContext ec)
		{
			return this;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0006C895 File Offset: 0x0006AA95
		public override void Emit(EmitContext ec)
		{
			base.Emit(ec);
			ec.Emit(OpCodes.Unbox_Any, this.type);
		}
	}
}
