using System;

namespace Mono.CSharp
{
	// Token: 0x0200010A RID: 266
	internal class HoistedLocalVariable : HoistedVariable
	{
		// Token: 0x06000D4A RID: 3402 RVA: 0x000305AC File Offset: 0x0002E7AC
		public HoistedLocalVariable(AnonymousMethodStorey storey, LocalVariable local, string name) : base(storey, name, local.Type)
		{
		}
	}
}
