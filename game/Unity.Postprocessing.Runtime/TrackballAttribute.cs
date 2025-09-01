using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200000B RID: 11
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class TrackballAttribute : Attribute
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002223 File Offset: 0x00000423
		public TrackballAttribute(TrackballAttribute.Mode mode)
		{
			this.mode = mode;
		}

		// Token: 0x04000021 RID: 33
		public readonly TrackballAttribute.Mode mode;

		// Token: 0x02000073 RID: 115
		public enum Mode
		{
			// Token: 0x040002D1 RID: 721
			None,
			// Token: 0x040002D2 RID: 722
			Lift,
			// Token: 0x040002D3 RID: 723
			Gamma,
			// Token: 0x040002D4 RID: 724
			Gain
		}
	}
}
