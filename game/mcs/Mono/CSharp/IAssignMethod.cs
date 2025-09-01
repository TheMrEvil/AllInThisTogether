using System;

namespace Mono.CSharp
{
	// Token: 0x02000110 RID: 272
	public interface IAssignMethod
	{
		// Token: 0x06000D8A RID: 3466
		void Emit(EmitContext ec, bool leave_copy);

		// Token: 0x06000D8B RID: 3467
		void EmitAssign(EmitContext ec, Expression source, bool leave_copy, bool isCompound);
	}
}
