using System;
using UnityEngine;

namespace DamageNumbersPro
{
	// Token: 0x02000016 RID: 22
	[Serializable]
	public struct TextSettings
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00006994 File Offset: 0x00004B94
		public TextSettings(float customDefault)
		{
			this.horizontal = customDefault;
			this.customColor = false;
			this.color = new Color(1f, 1f, 0f, 1f);
			this.size = 0f;
			this.vertical = 0f;
			this.characterSpacing = 0f;
			this.alpha = 1f;
			this.mark = false;
			this.markColor = new Color(0f, 0f, 0f, 0.5f);
			this.bold = false;
			this.italic = false;
			this.underline = false;
			this.strike = false;
		}

		// Token: 0x040000F3 RID: 243
		[Header("Basics:")]
		[Tooltip("Makes the text bold.")]
		public bool bold;

		// Token: 0x040000F4 RID: 244
		[Tooltip("Makes the text italic.")]
		public bool italic;

		// Token: 0x040000F5 RID: 245
		[Tooltip("Adds an underline to the text.")]
		public bool underline;

		// Token: 0x040000F6 RID: 246
		[Tooltip("Strikes through the text with a line.")]
		public bool strike;

		// Token: 0x040000F7 RID: 247
		[Header("Alpha:")]
		[Range(0f, 1f)]
		[Tooltip("Changes the alpha of the text.\nWon't work if Custom Color is used.")]
		public float alpha;

		// Token: 0x040000F8 RID: 248
		[Header("Color:")]
		[Tooltip("Changes the color of the text.\nOverrides the alpha option above.")]
		public bool customColor;

		// Token: 0x040000F9 RID: 249
		public Color color;

		// Token: 0x040000FA RID: 250
		[Header("Mark:")]
		[Tooltip("Highlights the text with a custom color.")]
		public bool mark;

		// Token: 0x040000FB RID: 251
		public Color markColor;

		// Token: 0x040000FC RID: 252
		[Header("Offset:")]
		[Tooltip("Horizontally moves the text.\nCan be used to offset the prefix or suffix.")]
		public float horizontal;

		// Token: 0x040000FD RID: 253
		[Tooltip("Vertically moves the text.\nCan be used to offset the prefix or suffix.")]
		public float vertical;

		// Token: 0x040000FE RID: 254
		[Header("Extra:")]
		[Tooltip("Changes the character spacing.")]
		public float characterSpacing;

		// Token: 0x040000FF RID: 255
		[Tooltip("Changes the font size.")]
		public float size;
	}
}
