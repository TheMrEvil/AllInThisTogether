using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	[Obsolete("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public enum ProceduralLoadingBehavior
	{
		// Token: 0x0400000D RID: 13
		DoNothing,
		// Token: 0x0400000E RID: 14
		Generate,
		// Token: 0x0400000F RID: 15
		BakeAndKeep,
		// Token: 0x04000010 RID: 16
		BakeAndDiscard,
		// Token: 0x04000011 RID: 17
		Cache,
		// Token: 0x04000012 RID: 18
		DoNothingAndCache
	}
}
