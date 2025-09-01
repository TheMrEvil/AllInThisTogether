using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[Obsolete("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public enum ProceduralCacheSize
	{
		// Token: 0x04000007 RID: 7
		Tiny,
		// Token: 0x04000008 RID: 8
		Medium,
		// Token: 0x04000009 RID: 9
		Heavy,
		// Token: 0x0400000A RID: 10
		NoLimit,
		// Token: 0x0400000B RID: 11
		None
	}
}
