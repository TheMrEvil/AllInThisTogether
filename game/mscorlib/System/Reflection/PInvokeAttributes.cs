using System;

namespace System.Reflection
{
	// Token: 0x020008E3 RID: 2275
	[Flags]
	[Serializable]
	internal enum PInvokeAttributes
	{
		// Token: 0x04002FC8 RID: 12232
		NoMangle = 1,
		// Token: 0x04002FC9 RID: 12233
		CharSetMask = 6,
		// Token: 0x04002FCA RID: 12234
		CharSetNotSpec = 0,
		// Token: 0x04002FCB RID: 12235
		CharSetAnsi = 2,
		// Token: 0x04002FCC RID: 12236
		CharSetUnicode = 4,
		// Token: 0x04002FCD RID: 12237
		CharSetAuto = 6,
		// Token: 0x04002FCE RID: 12238
		BestFitUseAssem = 0,
		// Token: 0x04002FCF RID: 12239
		BestFitEnabled = 16,
		// Token: 0x04002FD0 RID: 12240
		BestFitDisabled = 32,
		// Token: 0x04002FD1 RID: 12241
		BestFitMask = 48,
		// Token: 0x04002FD2 RID: 12242
		ThrowOnUnmappableCharUseAssem = 0,
		// Token: 0x04002FD3 RID: 12243
		ThrowOnUnmappableCharEnabled = 4096,
		// Token: 0x04002FD4 RID: 12244
		ThrowOnUnmappableCharDisabled = 8192,
		// Token: 0x04002FD5 RID: 12245
		ThrowOnUnmappableCharMask = 12288,
		// Token: 0x04002FD6 RID: 12246
		SupportsLastError = 64,
		// Token: 0x04002FD7 RID: 12247
		CallConvMask = 1792,
		// Token: 0x04002FD8 RID: 12248
		CallConvWinapi = 256,
		// Token: 0x04002FD9 RID: 12249
		CallConvCdecl = 512,
		// Token: 0x04002FDA RID: 12250
		CallConvStdcall = 768,
		// Token: 0x04002FDB RID: 12251
		CallConvThiscall = 1024,
		// Token: 0x04002FDC RID: 12252
		CallConvFastcall = 1280,
		// Token: 0x04002FDD RID: 12253
		MaxValue = 65535
	}
}
