using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000360 RID: 864
	[Serializable]
	internal struct ScalableImage
	{
		// Token: 0x06001BF6 RID: 7158 RVA: 0x00082E34 File Offset: 0x00081034
		public override string ToString()
		{
			return string.Format("{0}: {1}, {2}: {3}", new object[]
			{
				"normalImage",
				this.normalImage,
				"highResolutionImage",
				this.highResolutionImage
			});
		}

		// Token: 0x04000DEE RID: 3566
		public Texture2D normalImage;

		// Token: 0x04000DEF RID: 3567
		public Texture2D highResolutionImage;
	}
}
