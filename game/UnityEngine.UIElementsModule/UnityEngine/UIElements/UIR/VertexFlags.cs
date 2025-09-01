using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000347 RID: 839
	internal enum VertexFlags
	{
		// Token: 0x04000CE8 RID: 3304
		IsSolid,
		// Token: 0x04000CE9 RID: 3305
		IsText,
		// Token: 0x04000CEA RID: 3306
		IsTextured,
		// Token: 0x04000CEB RID: 3307
		IsDynamic,
		// Token: 0x04000CEC RID: 3308
		IsSvgGradients,
		// Token: 0x04000CED RID: 3309
		[Obsolete("Enum member VertexFlags.LastType has been deprecated. Use VertexFlags.IsGraphViewEdge instead.")]
		LastType = 10,
		// Token: 0x04000CEE RID: 3310
		IsGraphViewEdge = 10
	}
}
