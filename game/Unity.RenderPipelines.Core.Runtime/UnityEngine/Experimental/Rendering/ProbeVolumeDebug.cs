using System;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200000F RID: 15
	internal class ProbeVolumeDebug
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00006150 File Offset: 0x00004350
		public ProbeVolumeDebug()
		{
		}

		// Token: 0x0400006E RID: 110
		public bool drawProbes;

		// Token: 0x0400006F RID: 111
		public bool drawBricks;

		// Token: 0x04000070 RID: 112
		public bool drawCells;

		// Token: 0x04000071 RID: 113
		public bool realtimeSubdivision;

		// Token: 0x04000072 RID: 114
		public int subdivisionCellUpdatePerFrame = 4;

		// Token: 0x04000073 RID: 115
		public float subdivisionDelayInSeconds = 1f;

		// Token: 0x04000074 RID: 116
		public DebugProbeShadingMode probeShading;

		// Token: 0x04000075 RID: 117
		public float probeSize = 1f;

		// Token: 0x04000076 RID: 118
		public float subdivisionViewCullingDistance = 500f;

		// Token: 0x04000077 RID: 119
		public float probeCullingDistance = 200f;

		// Token: 0x04000078 RID: 120
		public int maxSubdivToVisualize = 7;

		// Token: 0x04000079 RID: 121
		public float exposureCompensation;
	}
}
