using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200032D RID: 813
	internal class GradientRemap : LinkedPoolItem<GradientRemap>
	{
		// Token: 0x06001A6B RID: 6763 RVA: 0x00072848 File Offset: 0x00070A48
		public void Reset()
		{
			this.origIndex = 0;
			this.destIndex = 0;
			this.location = default(RectInt);
			this.atlas = TextureId.invalid;
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00072870 File Offset: 0x00070A70
		public GradientRemap()
		{
		}

		// Token: 0x04000C22 RID: 3106
		public int origIndex;

		// Token: 0x04000C23 RID: 3107
		public int destIndex;

		// Token: 0x04000C24 RID: 3108
		public RectInt location;

		// Token: 0x04000C25 RID: 3109
		public GradientRemap next;

		// Token: 0x04000C26 RID: 3110
		public TextureId atlas;
	}
}
