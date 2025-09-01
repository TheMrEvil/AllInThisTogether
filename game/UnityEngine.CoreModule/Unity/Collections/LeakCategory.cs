using System;
using UnityEngine.Scripting;

namespace Unity.Collections
{
	// Token: 0x0200008D RID: 141
	[UsedByNativeCode]
	internal enum LeakCategory
	{
		// Token: 0x04000213 RID: 531
		Invalid,
		// Token: 0x04000214 RID: 532
		Malloc,
		// Token: 0x04000215 RID: 533
		TempJob,
		// Token: 0x04000216 RID: 534
		Persistent,
		// Token: 0x04000217 RID: 535
		LightProbesQuery,
		// Token: 0x04000218 RID: 536
		NativeTest,
		// Token: 0x04000219 RID: 537
		MeshDataArray,
		// Token: 0x0400021A RID: 538
		TransformAccessArray,
		// Token: 0x0400021B RID: 539
		NavMeshQuery
	}
}
