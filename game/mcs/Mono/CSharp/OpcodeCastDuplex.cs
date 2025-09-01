using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001AF RID: 431
	internal class OpcodeCastDuplex : OpcodeCast
	{
		// Token: 0x060016C3 RID: 5827 RVA: 0x0006D1D9 File Offset: 0x0006B3D9
		public OpcodeCastDuplex(Expression child, TypeSpec returnType, OpCode first, OpCode second) : base(child, returnType, first)
		{
			this.second = second;
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0006D1EC File Offset: 0x0006B3EC
		public override void Emit(EmitContext ec)
		{
			base.Emit(ec);
			ec.Emit(this.second);
		}

		// Token: 0x04000959 RID: 2393
		private readonly OpCode second;
	}
}
