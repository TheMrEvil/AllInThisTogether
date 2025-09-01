using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000031 RID: 49
	[ExcludeFromObjectFactory]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[ExcludeFromPreset]
	[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
	public sealed class GUITexture
	{
		// Token: 0x0600036D RID: 877 RVA: 0x0000C1F2 File Offset: 0x0000A3F2
		private static void FeatureRemoved()
		{
			throw new Exception("GUITexture has been removed from Unity. Use UI.Image instead.");
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000C200 File Offset: 0x0000A400
		// (set) Token: 0x0600036F RID: 879 RVA: 0x0000C22C File Offset: 0x0000A42C
		[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
		public Color color
		{
			get
			{
				GUITexture.FeatureRemoved();
				return new Color(0f, 0f, 0f);
			}
			set
			{
				GUITexture.FeatureRemoved();
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000C238 File Offset: 0x0000A438
		// (set) Token: 0x06000371 RID: 881 RVA: 0x0000C22C File Offset: 0x0000A42C
		[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
		public Texture texture
		{
			get
			{
				GUITexture.FeatureRemoved();
				return null;
			}
			set
			{
				GUITexture.FeatureRemoved();
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000C254 File Offset: 0x0000A454
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0000C22C File Offset: 0x0000A42C
		[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
		public Rect pixelInset
		{
			get
			{
				GUITexture.FeatureRemoved();
				return default(Rect);
			}
			set
			{
				GUITexture.FeatureRemoved();
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000C278 File Offset: 0x0000A478
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000C22C File Offset: 0x0000A42C
		[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
		public RectOffset border
		{
			get
			{
				GUITexture.FeatureRemoved();
				return null;
			}
			set
			{
				GUITexture.FeatureRemoved();
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x000073B2 File Offset: 0x000055B2
		public GUITexture()
		{
		}
	}
}
