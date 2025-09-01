using System;

namespace Unity.Burst.Intrinsics
{
	// Token: 0x0200001B RID: 27
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[BurstRuntime.PreserveAttribute]
	internal sealed class BurstTargetCpuAttribute : Attribute
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00005705 File Offset: 0x00003905
		public BurstTargetCpuAttribute(BurstTargetCpu TargetCpu)
		{
			this.TargetCpu = TargetCpu;
		}

		// Token: 0x04000146 RID: 326
		public readonly BurstTargetCpu TargetCpu;
	}
}
