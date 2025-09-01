using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[Obsolete("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public enum ProceduralProcessorUsage
	{
		// Token: 0x04000002 RID: 2
		Unsupported,
		// Token: 0x04000003 RID: 3
		One,
		// Token: 0x04000004 RID: 4
		Half,
		// Token: 0x04000005 RID: 5
		All
	}
}
