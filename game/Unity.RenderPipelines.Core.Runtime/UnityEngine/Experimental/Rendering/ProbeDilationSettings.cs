using System;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	internal struct ProbeDilationSettings
	{
		// Token: 0x04000091 RID: 145
		public bool enableDilation;

		// Token: 0x04000092 RID: 146
		public float dilationDistance;

		// Token: 0x04000093 RID: 147
		public float dilationValidityThreshold;

		// Token: 0x04000094 RID: 148
		public int dilationIterations;

		// Token: 0x04000095 RID: 149
		public bool squaredDistWeighting;
	}
}
