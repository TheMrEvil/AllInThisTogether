using System;
using UnityEngine;

namespace FIMSpace.FTools
{
	// Token: 0x02000059 RID: 89
	public class FMuscle_Angle : FMuscle_Motor
	{
		// Token: 0x06000303 RID: 771 RVA: 0x00016E35 File Offset: 0x00015035
		protected override float GetDiff(float current, float desired)
		{
			return Mathf.DeltaAngle(current, desired);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00016E3E File Offset: 0x0001503E
		public FMuscle_Angle()
		{
		}
	}
}
