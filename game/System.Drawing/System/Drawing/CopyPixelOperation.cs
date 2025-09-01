using System;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Determines how the source color in a copy pixel operation is combined with the destination color to result in a final color.</summary>
	// Token: 0x0200006A RID: 106
	[ComVisible(true)]
	public enum CopyPixelOperation
	{
		/// <summary>The destination area is filled by using the color associated with index 0 in the physical palette. (This color is black for the default physical palette.)</summary>
		// Token: 0x04000464 RID: 1124
		Blackness = 66,
		/// <summary>Windows that are layered on top of your window are included in the resulting image. By default, the image contains only your window. Note that this generally cannot be used for printing device contexts.</summary>
		// Token: 0x04000465 RID: 1125
		CaptureBlt = 1073741824,
		/// <summary>The destination area is inverted.</summary>
		// Token: 0x04000466 RID: 1126
		DestinationInvert = 5570569,
		/// <summary>The colors of the source area are merged with the colors of the selected brush of the destination device context using the Boolean <see langword="AND" /> operator.</summary>
		// Token: 0x04000467 RID: 1127
		MergeCopy = 12583114,
		/// <summary>The colors of the inverted source area are merged with the colors of the destination area by using the Boolean <see langword="OR" /> operator.</summary>
		// Token: 0x04000468 RID: 1128
		MergePaint = 12255782,
		/// <summary>The bitmap is not mirrored.</summary>
		// Token: 0x04000469 RID: 1129
		NoMirrorBitmap = -2147483648,
		/// <summary>The inverted source area is copied to the destination.</summary>
		// Token: 0x0400046A RID: 1130
		NotSourceCopy = 3342344,
		/// <summary>The source and destination colors are combined using the Boolean <see langword="OR" /> operator, and then resultant color is then inverted.</summary>
		// Token: 0x0400046B RID: 1131
		NotSourceErase = 1114278,
		/// <summary>The brush currently selected in the destination device context is copied to the destination bitmap.</summary>
		// Token: 0x0400046C RID: 1132
		PatCopy = 15728673,
		/// <summary>The colors of the brush currently selected in the destination device context are combined with the colors of the destination are using the Boolean <see langword="XOR" /> operator.</summary>
		// Token: 0x0400046D RID: 1133
		PatInvert = 5898313,
		/// <summary>The colors of the brush currently selected in the destination device context are combined with the colors of the inverted source area using the Boolean <see langword="OR" /> operator. The result of this operation is combined with the colors of the destination area using the Boolean <see langword="OR" /> operator.</summary>
		// Token: 0x0400046E RID: 1134
		PatPaint = 16452105,
		/// <summary>The colors of the source and destination areas are combined using the Boolean <see langword="AND" /> operator.</summary>
		// Token: 0x0400046F RID: 1135
		SourceAnd = 8913094,
		/// <summary>The source area is copied directly to the destination area.</summary>
		// Token: 0x04000470 RID: 1136
		SourceCopy = 13369376,
		/// <summary>The inverted colors of the destination area are combined with the colors of the source area using the Boolean <see langword="AND" /> operator.</summary>
		// Token: 0x04000471 RID: 1137
		SourceErase = 4457256,
		/// <summary>The colors of the source and destination areas are combined using the Boolean <see langword="XOR" /> operator.</summary>
		// Token: 0x04000472 RID: 1138
		SourceInvert = 6684742,
		/// <summary>The colors of the source and destination areas are combined using the Boolean <see langword="OR" /> operator.</summary>
		// Token: 0x04000473 RID: 1139
		SourcePaint = 15597702,
		/// <summary>The destination area is filled by using the color associated with index 1 in the physical palette. (This color is white for the default physical palette.)</summary>
		// Token: 0x04000474 RID: 1140
		Whiteness = 16711778
	}
}
