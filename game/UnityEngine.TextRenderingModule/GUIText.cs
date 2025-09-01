using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
	[ExcludeFromPreset]
	[ExcludeFromObjectFactory]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class GUIText
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static void FeatureRemoved()
		{
			throw new Exception("GUIText has been removed from Unity. Use UI.Text instead.");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002060 File Offset: 0x00000260
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002079 File Offset: 0x00000279
		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public bool text
		{
			get
			{
				GUIText.FeatureRemoved();
				return false;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002084 File Offset: 0x00000284
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002079 File Offset: 0x00000279
		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public Material material
		{
			get
			{
				GUIText.FeatureRemoved();
				return null;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020A0 File Offset: 0x000002A0
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002079 File Offset: 0x00000279
		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public Font font
		{
			get
			{
				GUIText.FeatureRemoved();
				return null;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020BC File Offset: 0x000002BC
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002079 File Offset: 0x00000279
		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public TextAlignment alignment
		{
			get
			{
				GUIText.FeatureRemoved();
				return TextAlignment.Left;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020D8 File Offset: 0x000002D8
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002079 File Offset: 0x00000279
		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public TextAnchor anchor
		{
			get
			{
				GUIText.FeatureRemoved();
				return TextAnchor.UpperLeft;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020F4 File Offset: 0x000002F4
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002079 File Offset: 0x00000279
		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public float lineSpacing
		{
			get
			{
				GUIText.FeatureRemoved();
				return 0f;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002114 File Offset: 0x00000314
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002079 File Offset: 0x00000279
		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public float tabSize
		{
			get
			{
				GUIText.FeatureRemoved();
				return 0f;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002134 File Offset: 0x00000334
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002079 File Offset: 0x00000279
		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public int fontSize
		{
			get
			{
				GUIText.FeatureRemoved();
				return 0;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002150 File Offset: 0x00000350
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002079 File Offset: 0x00000279
		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public FontStyle fontStyle
		{
			get
			{
				GUIText.FeatureRemoved();
				return FontStyle.Normal;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000216C File Offset: 0x0000036C
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002079 File Offset: 0x00000279
		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public bool richText
		{
			get
			{
				GUIText.FeatureRemoved();
				return false;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002188 File Offset: 0x00000388
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002079 File Offset: 0x00000279
		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public Color color
		{
			get
			{
				GUIText.FeatureRemoved();
				return new Color(0f, 0f, 0f);
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000021B4 File Offset: 0x000003B4
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002079 File Offset: 0x00000279
		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public Vector2 pixelOffset
		{
			get
			{
				GUIText.FeatureRemoved();
				return new Vector2(0f, 0f);
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000021DB File Offset: 0x000003DB
		public GUIText()
		{
		}
	}
}
