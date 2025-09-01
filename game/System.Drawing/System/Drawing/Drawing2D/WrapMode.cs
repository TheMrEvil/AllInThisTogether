using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies how a texture or gradient is tiled when it is smaller than the area being filled.</summary>
	// Token: 0x02000153 RID: 339
	public enum WrapMode
	{
		/// <summary>Tiles the gradient or texture.</summary>
		// Token: 0x04000B6E RID: 2926
		Tile,
		/// <summary>Reverses the texture or gradient horizontally and then tiles the texture or gradient.</summary>
		// Token: 0x04000B6F RID: 2927
		TileFlipX,
		/// <summary>Reverses the texture or gradient vertically and then tiles the texture or gradient.</summary>
		// Token: 0x04000B70 RID: 2928
		TileFlipY,
		/// <summary>Reverses the texture or gradient horizontally and vertically and then tiles the texture or gradient.</summary>
		// Token: 0x04000B71 RID: 2929
		TileFlipXY,
		/// <summary>The texture or gradient is not tiled.</summary>
		// Token: 0x04000B72 RID: 2930
		Clamp
	}
}
