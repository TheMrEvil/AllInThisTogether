using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D
{
	// Token: 0x02000002 RID: 2
	[MovedFrom("UnityEngine.Experimental.U2D")]
	public struct SpriteShapeParameters
	{
		// Token: 0x04000001 RID: 1
		public Matrix4x4 transform;

		// Token: 0x04000002 RID: 2
		public Texture2D fillTexture;

		// Token: 0x04000003 RID: 3
		public uint fillScale;

		// Token: 0x04000004 RID: 4
		public uint splineDetail;

		// Token: 0x04000005 RID: 5
		public float angleThreshold;

		// Token: 0x04000006 RID: 6
		public float borderPivot;

		// Token: 0x04000007 RID: 7
		public float bevelCutoff;

		// Token: 0x04000008 RID: 8
		public float bevelSize;

		// Token: 0x04000009 RID: 9
		public bool carpet;

		// Token: 0x0400000A RID: 10
		public bool smartSprite;

		// Token: 0x0400000B RID: 11
		public bool adaptiveUV;

		// Token: 0x0400000C RID: 12
		public bool spriteBorders;

		// Token: 0x0400000D RID: 13
		public bool stretchUV;
	}
}
