using System;

namespace UnityEngine.Yoga
{
	// Token: 0x02000005 RID: 5
	internal class MeasureOutput
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002050 File Offset: 0x00000250
		public static YogaSize Make(float width, float height)
		{
			return new YogaSize
			{
				width = width,
				height = height
			};
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000207B File Offset: 0x0000027B
		public MeasureOutput()
		{
		}
	}
}
