using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000025 RID: 37
	[ExcludeFromPreset]
	[Serializable]
	public class TMP_ColorGradient : ScriptableObject
	{
		// Token: 0x06000135 RID: 309 RVA: 0x0001765C File Offset: 0x0001585C
		public TMP_ColorGradient()
		{
			this.colorMode = ColorMode.FourCornersGradient;
			this.topLeft = TMP_ColorGradient.k_DefaultColor;
			this.topRight = TMP_ColorGradient.k_DefaultColor;
			this.bottomLeft = TMP_ColorGradient.k_DefaultColor;
			this.bottomRight = TMP_ColorGradient.k_DefaultColor;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000176A9 File Offset: 0x000158A9
		public TMP_ColorGradient(Color color)
		{
			this.colorMode = ColorMode.FourCornersGradient;
			this.topLeft = color;
			this.topRight = color;
			this.bottomLeft = color;
			this.bottomRight = color;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000176DB File Offset: 0x000158DB
		public TMP_ColorGradient(Color color0, Color color1, Color color2, Color color3)
		{
			this.colorMode = ColorMode.FourCornersGradient;
			this.topLeft = color0;
			this.topRight = color1;
			this.bottomLeft = color2;
			this.bottomRight = color3;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0001770E File Offset: 0x0001590E
		// Note: this type is marked as 'beforefieldinit'.
		static TMP_ColorGradient()
		{
		}

		// Token: 0x04000146 RID: 326
		public ColorMode colorMode = ColorMode.FourCornersGradient;

		// Token: 0x04000147 RID: 327
		public Color topLeft;

		// Token: 0x04000148 RID: 328
		public Color topRight;

		// Token: 0x04000149 RID: 329
		public Color bottomLeft;

		// Token: 0x0400014A RID: 330
		public Color bottomRight;

		// Token: 0x0400014B RID: 331
		private const ColorMode k_DefaultColorMode = ColorMode.FourCornersGradient;

		// Token: 0x0400014C RID: 332
		private static readonly Color k_DefaultColor = Color.white;
	}
}
