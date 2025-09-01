using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200039F RID: 927
	[Obsolete("GPUFence has been deprecated. Use GraphicsFence instead (UnityUpgradable) -> GraphicsFence", false)]
	public struct GPUFence
	{
		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001F54 RID: 8020 RVA: 0x0003321C File Offset: 0x0003141C
		public bool passed
		{
			get
			{
				return true;
			}
		}
	}
}
