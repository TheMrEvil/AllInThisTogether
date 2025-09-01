using System;

namespace FIMSpace.Generating
{
	// Token: 0x02000062 RID: 98
	[Serializable]
	public struct MinMaxF
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x00019173 File Offset: 0x00017373
		public MinMaxF(float min, float max)
		{
			this.Min = min;
			this.Max = max;
		}

		// Token: 0x04000306 RID: 774
		public float Min;

		// Token: 0x04000307 RID: 775
		public float Max;
	}
}
