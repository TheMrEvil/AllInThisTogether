using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200001F RID: 31
	[ExcludeFromObjectFactory]
	[ExcludeFromPreset]
	[Serializable]
	public class TextColorGradient : ScriptableObject
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00007BA0 File Offset: 0x00005DA0
		public TextColorGradient()
		{
			this.colorMode = ColorGradientMode.FourCornersGradient;
			this.topLeft = TextColorGradient.k_DefaultColor;
			this.topRight = TextColorGradient.k_DefaultColor;
			this.bottomLeft = TextColorGradient.k_DefaultColor;
			this.bottomRight = TextColorGradient.k_DefaultColor;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00007BEF File Offset: 0x00005DEF
		public TextColorGradient(Color color)
		{
			this.colorMode = ColorGradientMode.FourCornersGradient;
			this.topLeft = color;
			this.topRight = color;
			this.bottomLeft = color;
			this.bottomRight = color;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00007C23 File Offset: 0x00005E23
		public TextColorGradient(Color color0, Color color1, Color color2, Color color3)
		{
			this.colorMode = ColorGradientMode.FourCornersGradient;
			this.topLeft = color0;
			this.topRight = color1;
			this.bottomLeft = color2;
			this.bottomRight = color3;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00007C58 File Offset: 0x00005E58
		// Note: this type is marked as 'beforefieldinit'.
		static TextColorGradient()
		{
		}

		// Token: 0x040000BD RID: 189
		public ColorGradientMode colorMode = ColorGradientMode.FourCornersGradient;

		// Token: 0x040000BE RID: 190
		public Color topLeft;

		// Token: 0x040000BF RID: 191
		public Color topRight;

		// Token: 0x040000C0 RID: 192
		public Color bottomLeft;

		// Token: 0x040000C1 RID: 193
		public Color bottomRight;

		// Token: 0x040000C2 RID: 194
		private const ColorGradientMode k_DefaultColorMode = ColorGradientMode.FourCornersGradient;

		// Token: 0x040000C3 RID: 195
		private static readonly Color k_DefaultColor = Color.white;
	}
}
