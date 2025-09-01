using System;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies flags that are passed to the flags parameter of the <see cref="Overload:System.Drawing.Bitmap.LockBits" /> method. The <see cref="Overload:System.Drawing.Bitmap.LockBits" /> method locks a portion of an image so that you can read or write the pixel data.</summary>
	// Token: 0x0200010C RID: 268
	public enum ImageLockMode
	{
		/// <summary>Specifies that a portion of the image is locked for reading.</summary>
		// Token: 0x040009DD RID: 2525
		ReadOnly = 1,
		/// <summary>Specifies that a portion of the image is locked for writing.</summary>
		// Token: 0x040009DE RID: 2526
		WriteOnly,
		/// <summary>Specifies that a portion of the image is locked for reading or writing.</summary>
		// Token: 0x040009DF RID: 2527
		ReadWrite,
		/// <summary>Specifies that the buffer used for reading or writing pixel data is allocated by the user. If this flag is set, the <paramref name="flags" /> parameter of the <see cref="Overload:System.Drawing.Bitmap.LockBits" /> method serves as an input parameter (and possibly as an output parameter). If this flag is cleared, then the <paramref name="flags" /> parameter serves only as an output parameter.</summary>
		// Token: 0x040009E0 RID: 2528
		UserInputBuffer
	}
}
