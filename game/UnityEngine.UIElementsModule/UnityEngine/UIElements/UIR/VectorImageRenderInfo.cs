using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200032A RID: 810
	internal class VectorImageRenderInfo : LinkedPoolItem<VectorImageRenderInfo>
	{
		// Token: 0x06001A64 RID: 6756 RVA: 0x000727AC File Offset: 0x000709AC
		public void Reset()
		{
			this.useCount = 0;
			this.firstGradientRemap = null;
			this.gradientSettingsAlloc = default(Alloc);
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x000727C9 File Offset: 0x000709C9
		public VectorImageRenderInfo()
		{
		}

		// Token: 0x04000C1C RID: 3100
		public int useCount;

		// Token: 0x04000C1D RID: 3101
		public GradientRemap firstGradientRemap;

		// Token: 0x04000C1E RID: 3102
		public Alloc gradientSettingsAlloc;
	}
}
