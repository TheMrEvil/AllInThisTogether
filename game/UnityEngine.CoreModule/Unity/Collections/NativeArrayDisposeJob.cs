using System;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x02000096 RID: 150
	internal struct NativeArrayDisposeJob : IJob
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x000050F5 File Offset: 0x000032F5
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x04000232 RID: 562
		internal NativeArrayDispose Data;
	}
}
