using System;

namespace System.Drawing.Imaging
{
	/// <summary>Used to specify the parameter value passed to a JPEG or TIFF image encoder when using the <see cref="M:System.Drawing.Image.Save(System.String,System.Drawing.Imaging.ImageCodecInfo,System.Drawing.Imaging.EncoderParameters)" /> or <see cref="M:System.Drawing.Image.SaveAdd(System.Drawing.Imaging.EncoderParameters)" /> methods.</summary>
	// Token: 0x02000105 RID: 261
	public enum EncoderValue
	{
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x0400098E RID: 2446
		ColorTypeCMYK,
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x0400098F RID: 2447
		ColorTypeYCCK,
		/// <summary>Specifies the LZW compression scheme. Can be passed to the TIFF encoder as a parameter that belongs to the Compression category.</summary>
		// Token: 0x04000990 RID: 2448
		CompressionLZW,
		/// <summary>Specifies the CCITT3 compression scheme. Can be passed to the TIFF encoder as a parameter that belongs to the compression category.</summary>
		// Token: 0x04000991 RID: 2449
		CompressionCCITT3,
		/// <summary>Specifies the CCITT4 compression scheme. Can be passed to the TIFF encoder as a parameter that belongs to the compression category.</summary>
		// Token: 0x04000992 RID: 2450
		CompressionCCITT4,
		/// <summary>Specifies the RLE compression scheme. Can be passed to the TIFF encoder as a parameter that belongs to the compression category.</summary>
		// Token: 0x04000993 RID: 2451
		CompressionRle,
		/// <summary>Specifies no compression. Can be passed to the TIFF encoder as a parameter that belongs to the compression category.</summary>
		// Token: 0x04000994 RID: 2452
		CompressionNone,
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x04000995 RID: 2453
		ScanMethodInterlaced,
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x04000996 RID: 2454
		ScanMethodNonInterlaced,
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x04000997 RID: 2455
		VersionGif87,
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x04000998 RID: 2456
		VersionGif89,
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x04000999 RID: 2457
		RenderProgressive,
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x0400099A RID: 2458
		RenderNonProgressive,
		/// <summary>Specifies that the image is to be rotated clockwise 90 degrees about its center. Can be passed to the JPEG encoder as a parameter that belongs to the transformation category.</summary>
		// Token: 0x0400099B RID: 2459
		TransformRotate90,
		/// <summary>Specifies that the image is to be rotated 180 degrees about its center. Can be passed to the JPEG encoder as a parameter that belongs to the transformation category.</summary>
		// Token: 0x0400099C RID: 2460
		TransformRotate180,
		/// <summary>Specifies that the image is to be rotated clockwise 270 degrees about its center. Can be passed to the JPEG encoder as a parameter that belongs to the transformation category.</summary>
		// Token: 0x0400099D RID: 2461
		TransformRotate270,
		/// <summary>Specifies that the image is to be flipped horizontally (about the vertical axis). Can be passed to the JPEG encoder as a parameter that belongs to the transformation category.</summary>
		// Token: 0x0400099E RID: 2462
		TransformFlipHorizontal,
		/// <summary>Specifies that the image is to be flipped vertically (about the horizontal axis). Can be passed to the JPEG encoder as a parameter that belongs to the transformation category.</summary>
		// Token: 0x0400099F RID: 2463
		TransformFlipVertical,
		/// <summary>Specifies that the image has more than one frame (page). Can be passed to the TIFF encoder as a parameter that belongs to the save flag category.</summary>
		// Token: 0x040009A0 RID: 2464
		MultiFrame,
		/// <summary>Specifies the last frame in a multiple-frame image. Can be passed to the TIFF encoder as a parameter that belongs to the save flag category.</summary>
		// Token: 0x040009A1 RID: 2465
		LastFrame,
		/// <summary>Specifies that a multiple-frame file or stream should be closed. Can be passed to the TIFF encoder as a parameter that belongs to the save flag category.</summary>
		// Token: 0x040009A2 RID: 2466
		Flush,
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x040009A3 RID: 2467
		FrameDimensionTime,
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x040009A4 RID: 2468
		FrameDimensionResolution,
		/// <summary>Specifies that a frame is to be added to the page dimension of an image. Can be passed to the TIFF encoder as a parameter that belongs to the save flag category.</summary>
		// Token: 0x040009A5 RID: 2469
		FrameDimensionPage
	}
}
