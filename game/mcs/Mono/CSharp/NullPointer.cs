using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000152 RID: 338
	internal class NullPointer : NullConstant
	{
		// Token: 0x060010D5 RID: 4309 RVA: 0x00044AA4 File Offset: 0x00042CA4
		public NullPointer(TypeSpec type, Location loc) : base(type, loc)
		{
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00044AAE File Offset: 0x00042CAE
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			base.Error_PointerInsideExpressionTree(ec);
			return base.CreateExpressionTree(ec);
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00044ABE File Offset: 0x00042CBE
		public override void Emit(EmitContext ec)
		{
			ec.EmitInt(0);
			ec.Emit(OpCodes.Conv_U);
		}
	}
}
