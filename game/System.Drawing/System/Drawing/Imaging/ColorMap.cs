using System;

namespace System.Drawing.Imaging
{
	/// <summary>Defines a map for converting colors. Several methods of the <see cref="T:System.Drawing.Imaging.ImageAttributes" /> class adjust image colors by using a color-remap table, which is an array of <see cref="T:System.Drawing.Imaging.ColorMap" /> structures. Not inheritable.</summary>
	// Token: 0x020000F8 RID: 248
	public sealed class ColorMap
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ColorMap" /> class.</summary>
		// Token: 0x06000C0B RID: 3083 RVA: 0x0001BC38 File Offset: 0x00019E38
		public ColorMap()
		{
			this._oldColor = default(Color);
			this._newColor = default(Color);
		}

		/// <summary>Gets or sets the existing <see cref="T:System.Drawing.Color" /> structure to be converted.</summary>
		/// <returns>The existing <see cref="T:System.Drawing.Color" /> structure to be converted.</returns>
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0001BC58 File Offset: 0x00019E58
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x0001BC60 File Offset: 0x00019E60
		public Color OldColor
		{
			get
			{
				return this._oldColor;
			}
			set
			{
				this._oldColor = value;
			}
		}

		/// <summary>Gets or sets the new <see cref="T:System.Drawing.Color" /> structure to which to convert.</summary>
		/// <returns>The new <see cref="T:System.Drawing.Color" /> structure to which to convert.</returns>
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0001BC69 File Offset: 0x00019E69
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x0001BC71 File Offset: 0x00019E71
		public Color NewColor
		{
			get
			{
				return this._newColor;
			}
			set
			{
				this._newColor = value;
			}
		}

		// Token: 0x04000848 RID: 2120
		private Color _oldColor;

		// Token: 0x04000849 RID: 2121
		private Color _newColor;
	}
}
