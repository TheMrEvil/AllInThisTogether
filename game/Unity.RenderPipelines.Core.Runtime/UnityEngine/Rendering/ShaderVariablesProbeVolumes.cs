using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000078 RID: 120
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "C:\\BuildAgent\\work\\efc64307713e2651\\Library\\PackageCache\\com.unity.render-pipelines.core@12.1.15\\Runtime\\Lighting\\ProbeVolume\\ShaderVariablesProbeVolumes.cs", needAccessors = false, generateCBuffer = true, constantRegister = 5)]
	internal struct ShaderVariablesProbeVolumes
	{
		// Token: 0x0400025A RID: 602
		public Vector3 _PoolDim;

		// Token: 0x0400025B RID: 603
		public float _ViewBias;

		// Token: 0x0400025C RID: 604
		public Vector3 _MinCellPosition;

		// Token: 0x0400025D RID: 605
		public float _PVSamplingNoise;

		// Token: 0x0400025E RID: 606
		public Vector3 _CellIndicesDim;

		// Token: 0x0400025F RID: 607
		public float _CellInMeters;

		// Token: 0x04000260 RID: 608
		public float _CellInMinBricks;

		// Token: 0x04000261 RID: 609
		public float _MinBrickSize;

		// Token: 0x04000262 RID: 610
		public int _IndexChunkSize;

		// Token: 0x04000263 RID: 611
		public float _NormalBias;
	}
}
