using System;

namespace IKVM.Reflection
{
	// Token: 0x0200001C RID: 28
	[Flags]
	public enum ParameterAttributes
	{
		// Token: 0x040000B4 RID: 180
		None = 0,
		// Token: 0x040000B5 RID: 181
		In = 1,
		// Token: 0x040000B6 RID: 182
		Out = 2,
		// Token: 0x040000B7 RID: 183
		Lcid = 4,
		// Token: 0x040000B8 RID: 184
		Retval = 8,
		// Token: 0x040000B9 RID: 185
		Optional = 16,
		// Token: 0x040000BA RID: 186
		HasDefault = 4096,
		// Token: 0x040000BB RID: 187
		HasFieldMarshal = 8192,
		// Token: 0x040000BC RID: 188
		Reserved3 = 16384,
		// Token: 0x040000BD RID: 189
		Reserved4 = 32768,
		// Token: 0x040000BE RID: 190
		ReservedMask = 61440
	}
}
