using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200003B RID: 59
	public struct FastMemoryDesc
	{
		// Token: 0x04000165 RID: 357
		public bool inFastMemory;

		// Token: 0x04000166 RID: 358
		public FastMemoryFlags flags;

		// Token: 0x04000167 RID: 359
		public float residencyFraction;
	}
}
