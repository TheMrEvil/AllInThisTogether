using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000066 RID: 102
	public static class JobHandleUnsafeUtility
	{
		// Token: 0x0600018C RID: 396 RVA: 0x00003744 File Offset: 0x00001944
		public unsafe static JobHandle CombineDependencies(JobHandle* jobs, int count)
		{
			return JobHandle.CombineDependenciesInternalPtr((void*)jobs, count);
		}
	}
}
