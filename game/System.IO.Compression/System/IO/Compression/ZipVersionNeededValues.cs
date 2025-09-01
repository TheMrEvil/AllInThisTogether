using System;

namespace System.IO.Compression
{
	// Token: 0x0200003B RID: 59
	internal enum ZipVersionNeededValues : ushort
	{
		// Token: 0x0400020D RID: 525
		Default = 10,
		// Token: 0x0400020E RID: 526
		ExplicitDirectory = 20,
		// Token: 0x0400020F RID: 527
		Deflate = 20,
		// Token: 0x04000210 RID: 528
		Deflate64,
		// Token: 0x04000211 RID: 529
		Zip64 = 45
	}
}
