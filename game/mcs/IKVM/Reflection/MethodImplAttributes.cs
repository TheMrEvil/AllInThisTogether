using System;

namespace IKVM.Reflection
{
	// Token: 0x0200001B RID: 27
	[Flags]
	public enum MethodImplAttributes
	{
		// Token: 0x040000A3 RID: 163
		CodeTypeMask = 3,
		// Token: 0x040000A4 RID: 164
		IL = 0,
		// Token: 0x040000A5 RID: 165
		Native = 1,
		// Token: 0x040000A6 RID: 166
		OPTIL = 2,
		// Token: 0x040000A7 RID: 167
		Runtime = 3,
		// Token: 0x040000A8 RID: 168
		ManagedMask = 4,
		// Token: 0x040000A9 RID: 169
		Unmanaged = 4,
		// Token: 0x040000AA RID: 170
		Managed = 0,
		// Token: 0x040000AB RID: 171
		ForwardRef = 16,
		// Token: 0x040000AC RID: 172
		PreserveSig = 128,
		// Token: 0x040000AD RID: 173
		InternalCall = 4096,
		// Token: 0x040000AE RID: 174
		Synchronized = 32,
		// Token: 0x040000AF RID: 175
		NoInlining = 8,
		// Token: 0x040000B0 RID: 176
		NoOptimization = 64,
		// Token: 0x040000B1 RID: 177
		AggressiveInlining = 256,
		// Token: 0x040000B2 RID: 178
		MaxMethodImplVal = 65535
	}
}
