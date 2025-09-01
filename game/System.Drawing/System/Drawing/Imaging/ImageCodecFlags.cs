using System;

namespace System.Drawing.Imaging
{
	/// <summary>Provides attributes of an image encoder/decoder (codec).</summary>
	// Token: 0x02000108 RID: 264
	[Flags]
	public enum ImageCodecFlags
	{
		/// <summary>The codec supports encoding (saving).</summary>
		// Token: 0x040009AC RID: 2476
		Encoder = 1,
		/// <summary>The codec supports decoding (reading).</summary>
		// Token: 0x040009AD RID: 2477
		Decoder = 2,
		/// <summary>The codec supports raster images (bitmaps).</summary>
		// Token: 0x040009AE RID: 2478
		SupportBitmap = 4,
		/// <summary>The codec supports vector images (metafiles).</summary>
		// Token: 0x040009AF RID: 2479
		SupportVector = 8,
		/// <summary>The encoder requires a seekable output stream.</summary>
		// Token: 0x040009B0 RID: 2480
		SeekableEncode = 16,
		/// <summary>The decoder has blocking behavior during the decoding process.</summary>
		// Token: 0x040009B1 RID: 2481
		BlockingDecode = 32,
		/// <summary>The codec is built into GDI+.</summary>
		// Token: 0x040009B2 RID: 2482
		Builtin = 65536,
		/// <summary>Not used.</summary>
		// Token: 0x040009B3 RID: 2483
		System = 131072,
		/// <summary>Not used.</summary>
		// Token: 0x040009B4 RID: 2484
		User = 262144
	}
}
