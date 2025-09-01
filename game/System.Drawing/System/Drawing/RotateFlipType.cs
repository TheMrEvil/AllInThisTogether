using System;

namespace System.Drawing
{
	/// <summary>Specifies how much an image is rotated and the axis used to flip the image.</summary>
	// Token: 0x02000039 RID: 57
	public enum RotateFlipType
	{
		/// <summary>Specifies no clockwise rotation and no flipping.</summary>
		// Token: 0x04000345 RID: 837
		RotateNoneFlipNone,
		/// <summary>Specifies a 90-degree clockwise rotation without flipping.</summary>
		// Token: 0x04000346 RID: 838
		Rotate90FlipNone,
		/// <summary>Specifies a 180-degree clockwise rotation without flipping.</summary>
		// Token: 0x04000347 RID: 839
		Rotate180FlipNone,
		/// <summary>Specifies a 270-degree clockwise rotation without flipping.</summary>
		// Token: 0x04000348 RID: 840
		Rotate270FlipNone,
		/// <summary>Specifies no clockwise rotation followed by a horizontal flip.</summary>
		// Token: 0x04000349 RID: 841
		RotateNoneFlipX,
		/// <summary>Specifies a 90-degree clockwise rotation followed by a horizontal flip.</summary>
		// Token: 0x0400034A RID: 842
		Rotate90FlipX,
		/// <summary>Specifies a 180-degree clockwise rotation followed by a horizontal flip.</summary>
		// Token: 0x0400034B RID: 843
		Rotate180FlipX,
		/// <summary>Specifies a 270-degree clockwise rotation followed by a horizontal flip.</summary>
		// Token: 0x0400034C RID: 844
		Rotate270FlipX,
		/// <summary>Specifies no clockwise rotation followed by a vertical flip.</summary>
		// Token: 0x0400034D RID: 845
		RotateNoneFlipY = 6,
		/// <summary>Specifies a 90-degree clockwise rotation followed by a vertical flip.</summary>
		// Token: 0x0400034E RID: 846
		Rotate90FlipY,
		/// <summary>Specifies a 180-degree clockwise rotation followed by a vertical flip.</summary>
		// Token: 0x0400034F RID: 847
		Rotate180FlipY = 4,
		/// <summary>Specifies a 270-degree clockwise rotation followed by a vertical flip.</summary>
		// Token: 0x04000350 RID: 848
		Rotate270FlipY,
		/// <summary>Specifies no clockwise rotation followed by a horizontal and vertical flip.</summary>
		// Token: 0x04000351 RID: 849
		RotateNoneFlipXY = 2,
		/// <summary>Specifies a 90-degree clockwise rotation followed by a horizontal and vertical flip.</summary>
		// Token: 0x04000352 RID: 850
		Rotate90FlipXY,
		/// <summary>Specifies a 180-degree clockwise rotation followed by a horizontal and vertical flip.</summary>
		// Token: 0x04000353 RID: 851
		Rotate180FlipXY = 0,
		/// <summary>Specifies a 270-degree clockwise rotation followed by a horizontal and vertical flip.</summary>
		// Token: 0x04000354 RID: 852
		Rotate270FlipXY
	}
}
