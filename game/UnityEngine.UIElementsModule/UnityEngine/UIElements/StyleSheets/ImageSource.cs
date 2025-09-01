using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000362 RID: 866
	internal struct ImageSource
	{
		// Token: 0x06001BF7 RID: 7159 RVA: 0x00082E78 File Offset: 0x00081078
		public bool IsNull()
		{
			return this.texture == null && this.sprite == null && this.vectorImage == null && this.renderTexture == null;
		}

		// Token: 0x04000DF2 RID: 3570
		public Texture2D texture;

		// Token: 0x04000DF3 RID: 3571
		public Sprite sprite;

		// Token: 0x04000DF4 RID: 3572
		public VectorImage vectorImage;

		// Token: 0x04000DF5 RID: 3573
		public RenderTexture renderTexture;
	}
}
