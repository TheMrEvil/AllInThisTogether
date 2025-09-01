using System;
using System.Reflection.Emit;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002C4 RID: 708
	internal interface ILocalCache
	{
		// Token: 0x0600159D RID: 5533
		LocalBuilder GetLocal(Type type);

		// Token: 0x0600159E RID: 5534
		void FreeLocal(LocalBuilder local);
	}
}
