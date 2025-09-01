using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000039 RID: 57
	[Preserve]
	[Serializable]
	public class SGSR
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00009A98 File Offset: 0x00007C98
		public SGSR()
		{
		}

		// Token: 0x0400012B RID: 299
		[Tooltip("Fallback AA for when SGSR is not supported")]
		public PostProcessLayer.Antialiasing fallBackAA;

		// Token: 0x02000080 RID: 128
		public enum SGSR_Quality
		{
			// Token: 0x0400031F RID: 799
			Off,
			// Token: 0x04000320 RID: 800
			Native,
			// Token: 0x04000321 RID: 801
			Quality,
			// Token: 0x04000322 RID: 802
			Balanced,
			// Token: 0x04000323 RID: 803
			Performance,
			// Token: 0x04000324 RID: 804
			UltraPerformance
		}
	}
}
