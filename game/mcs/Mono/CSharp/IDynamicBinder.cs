using System;

namespace Mono.CSharp
{
	// Token: 0x02000163 RID: 355
	internal interface IDynamicBinder
	{
		// Token: 0x0600117F RID: 4479
		Expression CreateCallSiteBinder(ResolveContext ec, Arguments args);
	}
}
