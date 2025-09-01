using System;

namespace FIMSpace.FTools
{
	// Token: 0x02000058 RID: 88
	public class FMuscle_Float : FMuscle_Motor
	{
		// Token: 0x06000301 RID: 769 RVA: 0x00016E28 File Offset: 0x00015028
		protected override float GetDiff(float current, float desired)
		{
			return desired - current;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00016E2D File Offset: 0x0001502D
		public FMuscle_Float()
		{
		}
	}
}
