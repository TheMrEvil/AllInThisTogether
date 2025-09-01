using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000065 RID: 101
	public static class HaltonSeq
	{
		// Token: 0x06000202 RID: 514 RVA: 0x0001092C File Offset: 0x0000EB2C
		public static float Get(int index, int radix)
		{
			float num = 0f;
			float num2 = 1f / (float)radix;
			while (index > 0)
			{
				num += (float)(index % radix) * num2;
				index /= radix;
				num2 /= (float)radix;
			}
			return num;
		}
	}
}
