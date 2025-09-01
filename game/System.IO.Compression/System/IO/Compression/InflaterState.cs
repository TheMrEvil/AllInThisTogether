using System;

namespace System.IO.Compression
{
	// Token: 0x02000021 RID: 33
	internal enum InflaterState
	{
		// Token: 0x04000139 RID: 313
		ReadingHeader,
		// Token: 0x0400013A RID: 314
		ReadingBFinal = 2,
		// Token: 0x0400013B RID: 315
		ReadingBType,
		// Token: 0x0400013C RID: 316
		ReadingNumLitCodes,
		// Token: 0x0400013D RID: 317
		ReadingNumDistCodes,
		// Token: 0x0400013E RID: 318
		ReadingNumCodeLengthCodes,
		// Token: 0x0400013F RID: 319
		ReadingCodeLengthCodes,
		// Token: 0x04000140 RID: 320
		ReadingTreeCodesBefore,
		// Token: 0x04000141 RID: 321
		ReadingTreeCodesAfter,
		// Token: 0x04000142 RID: 322
		DecodeTop,
		// Token: 0x04000143 RID: 323
		HaveInitialLength,
		// Token: 0x04000144 RID: 324
		HaveFullLength,
		// Token: 0x04000145 RID: 325
		HaveDistCode,
		// Token: 0x04000146 RID: 326
		UncompressedAligning = 15,
		// Token: 0x04000147 RID: 327
		UncompressedByte1,
		// Token: 0x04000148 RID: 328
		UncompressedByte2,
		// Token: 0x04000149 RID: 329
		UncompressedByte3,
		// Token: 0x0400014A RID: 330
		UncompressedByte4,
		// Token: 0x0400014B RID: 331
		DecodingUncompressed,
		// Token: 0x0400014C RID: 332
		StartReadingFooter,
		// Token: 0x0400014D RID: 333
		ReadingFooter,
		// Token: 0x0400014E RID: 334
		VerifyingFooter,
		// Token: 0x0400014F RID: 335
		Done
	}
}
