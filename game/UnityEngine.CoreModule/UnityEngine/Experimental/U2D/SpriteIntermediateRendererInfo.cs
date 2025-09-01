using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.U2D
{
	// Token: 0x02000454 RID: 1108
	[NativeHeader("Runtime/2D/Renderer/SpriteRendererGroup.h")]
	[RequiredByNativeCode]
	internal struct SpriteIntermediateRendererInfo
	{
		// Token: 0x04000E3D RID: 3645
		public int SpriteID;

		// Token: 0x04000E3E RID: 3646
		public int TextureID;

		// Token: 0x04000E3F RID: 3647
		public int MaterialID;

		// Token: 0x04000E40 RID: 3648
		public Color Color;

		// Token: 0x04000E41 RID: 3649
		public Matrix4x4 Transform;

		// Token: 0x04000E42 RID: 3650
		public Bounds Bounds;

		// Token: 0x04000E43 RID: 3651
		public int Layer;

		// Token: 0x04000E44 RID: 3652
		public int SortingLayer;

		// Token: 0x04000E45 RID: 3653
		public int SortingOrder;

		// Token: 0x04000E46 RID: 3654
		public ulong SceneCullingMask;

		// Token: 0x04000E47 RID: 3655
		public IntPtr IndexData;

		// Token: 0x04000E48 RID: 3656
		public IntPtr VertexData;

		// Token: 0x04000E49 RID: 3657
		public int IndexCount;

		// Token: 0x04000E4A RID: 3658
		public int VertexCount;

		// Token: 0x04000E4B RID: 3659
		public int ShaderChannelMask;
	}
}
