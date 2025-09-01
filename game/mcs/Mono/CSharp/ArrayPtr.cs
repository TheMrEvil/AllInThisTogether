using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000209 RID: 521
	internal class ArrayPtr : FixedBufferPtr
	{
		// Token: 0x06001AE1 RID: 6881 RVA: 0x0008290F File Offset: 0x00080B0F
		public ArrayPtr(Expression array, TypeSpec array_type, Location l) : base(array, array_type, l)
		{
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0008291A File Offset: 0x00080B1A
		public override void Emit(EmitContext ec)
		{
			base.Emit(ec);
			ec.EmitInt(0);
			ec.Emit(OpCodes.Ldelema, ((PointerContainer)this.type).Element);
		}
	}
}
