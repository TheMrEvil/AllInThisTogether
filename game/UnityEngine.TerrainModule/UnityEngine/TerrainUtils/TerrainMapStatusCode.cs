using System;

namespace UnityEngine.TerrainUtils
{
	// Token: 0x02000019 RID: 25
	internal enum TerrainMapStatusCode
	{
		// Token: 0x04000066 RID: 102
		OK,
		// Token: 0x04000067 RID: 103
		Overlapping,
		// Token: 0x04000068 RID: 104
		SizeMismatch = 4,
		// Token: 0x04000069 RID: 105
		EdgeAlignmentMismatch = 8
	}
}
