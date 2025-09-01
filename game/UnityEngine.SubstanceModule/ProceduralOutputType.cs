using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	[Obsolete("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public enum ProceduralOutputType
	{
		// Token: 0x0400001F RID: 31
		Unknown,
		// Token: 0x04000020 RID: 32
		Diffuse,
		// Token: 0x04000021 RID: 33
		Normal,
		// Token: 0x04000022 RID: 34
		Height,
		// Token: 0x04000023 RID: 35
		Emissive,
		// Token: 0x04000024 RID: 36
		Specular,
		// Token: 0x04000025 RID: 37
		Opacity,
		// Token: 0x04000026 RID: 38
		Smoothness,
		// Token: 0x04000027 RID: 39
		AmbientOcclusion,
		// Token: 0x04000028 RID: 40
		DetailMask,
		// Token: 0x04000029 RID: 41
		Metallic,
		// Token: 0x0400002A RID: 42
		Roughness
	}
}
