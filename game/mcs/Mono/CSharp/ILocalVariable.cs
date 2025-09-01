using System;

namespace Mono.CSharp
{
	// Token: 0x020002AE RID: 686
	public interface ILocalVariable
	{
		// Token: 0x060020E3 RID: 8419
		void Emit(EmitContext ec);

		// Token: 0x060020E4 RID: 8420
		void EmitAssign(EmitContext ec);

		// Token: 0x060020E5 RID: 8421
		void EmitAddressOf(EmitContext ec);
	}
}
