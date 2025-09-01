using System;

namespace IKVM.Reflection
{
	// Token: 0x02000024 RID: 36
	[Flags]
	public enum ImplMapFlags
	{
		// Token: 0x04000102 RID: 258
		NoMangle = 1,
		// Token: 0x04000103 RID: 259
		CharSetMask = 6,
		// Token: 0x04000104 RID: 260
		CharSetNotSpec = 0,
		// Token: 0x04000105 RID: 261
		CharSetAnsi = 2,
		// Token: 0x04000106 RID: 262
		CharSetUnicode = 4,
		// Token: 0x04000107 RID: 263
		CharSetAuto = 6,
		// Token: 0x04000108 RID: 264
		SupportsLastError = 64,
		// Token: 0x04000109 RID: 265
		CallConvMask = 1792,
		// Token: 0x0400010A RID: 266
		CallConvWinapi = 256,
		// Token: 0x0400010B RID: 267
		CallConvCdecl = 512,
		// Token: 0x0400010C RID: 268
		CallConvStdcall = 768,
		// Token: 0x0400010D RID: 269
		CallConvThiscall = 1024,
		// Token: 0x0400010E RID: 270
		CallConvFastcall = 1280,
		// Token: 0x0400010F RID: 271
		BestFitOn = 16,
		// Token: 0x04000110 RID: 272
		BestFitOff = 32,
		// Token: 0x04000111 RID: 273
		CharMapErrorOn = 4096,
		// Token: 0x04000112 RID: 274
		CharMapErrorOff = 8192
	}
}
