using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	[Obsolete("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public enum ProceduralPropertyType
	{
		// Token: 0x04000014 RID: 20
		Boolean,
		// Token: 0x04000015 RID: 21
		Float,
		// Token: 0x04000016 RID: 22
		Vector2,
		// Token: 0x04000017 RID: 23
		Vector3,
		// Token: 0x04000018 RID: 24
		Vector4,
		// Token: 0x04000019 RID: 25
		Color3,
		// Token: 0x0400001A RID: 26
		Color4,
		// Token: 0x0400001B RID: 27
		Enum,
		// Token: 0x0400001C RID: 28
		Texture,
		// Token: 0x0400001D RID: 29
		String
	}
}
