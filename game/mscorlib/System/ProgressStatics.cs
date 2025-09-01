using System;
using System.Threading;

namespace System
{
	// Token: 0x02000171 RID: 369
	internal static class ProgressStatics
	{
		// Token: 0x06000E94 RID: 3732 RVA: 0x0003BB53 File Offset: 0x00039D53
		// Note: this type is marked as 'beforefieldinit'.
		static ProgressStatics()
		{
		}

		// Token: 0x040012C3 RID: 4803
		internal static readonly SynchronizationContext DefaultContext = new SynchronizationContext();
	}
}
