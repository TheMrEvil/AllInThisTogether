using System;

namespace System.Reflection
{
	// Token: 0x020008E2 RID: 2274
	[Flags]
	[Serializable]
	internal enum MdSigCallingConvention : byte
	{
		// Token: 0x04002FB8 RID: 12216
		CallConvMask = 15,
		// Token: 0x04002FB9 RID: 12217
		Default = 0,
		// Token: 0x04002FBA RID: 12218
		C = 1,
		// Token: 0x04002FBB RID: 12219
		StdCall = 2,
		// Token: 0x04002FBC RID: 12220
		ThisCall = 3,
		// Token: 0x04002FBD RID: 12221
		FastCall = 4,
		// Token: 0x04002FBE RID: 12222
		Vararg = 5,
		// Token: 0x04002FBF RID: 12223
		Field = 6,
		// Token: 0x04002FC0 RID: 12224
		LocalSig = 7,
		// Token: 0x04002FC1 RID: 12225
		Property = 8,
		// Token: 0x04002FC2 RID: 12226
		Unmgd = 9,
		// Token: 0x04002FC3 RID: 12227
		GenericInst = 10,
		// Token: 0x04002FC4 RID: 12228
		Generic = 16,
		// Token: 0x04002FC5 RID: 12229
		HasThis = 32,
		// Token: 0x04002FC6 RID: 12230
		ExplicitThis = 64
	}
}
