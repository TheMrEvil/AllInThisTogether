using System;

namespace IKVM.Reflection
{
	// Token: 0x02000016 RID: 22
	[Flags]
	public enum FieldAttributes
	{
		// Token: 0x0400005E RID: 94
		PrivateScope = 0,
		// Token: 0x0400005F RID: 95
		Private = 1,
		// Token: 0x04000060 RID: 96
		FamANDAssem = 2,
		// Token: 0x04000061 RID: 97
		Assembly = 3,
		// Token: 0x04000062 RID: 98
		Family = 4,
		// Token: 0x04000063 RID: 99
		FamORAssem = 5,
		// Token: 0x04000064 RID: 100
		Public = 6,
		// Token: 0x04000065 RID: 101
		FieldAccessMask = 7,
		// Token: 0x04000066 RID: 102
		Static = 16,
		// Token: 0x04000067 RID: 103
		InitOnly = 32,
		// Token: 0x04000068 RID: 104
		Literal = 64,
		// Token: 0x04000069 RID: 105
		NotSerialized = 128,
		// Token: 0x0400006A RID: 106
		HasFieldRVA = 256,
		// Token: 0x0400006B RID: 107
		SpecialName = 512,
		// Token: 0x0400006C RID: 108
		RTSpecialName = 1024,
		// Token: 0x0400006D RID: 109
		HasFieldMarshal = 4096,
		// Token: 0x0400006E RID: 110
		PinvokeImpl = 8192,
		// Token: 0x0400006F RID: 111
		HasDefault = 32768,
		// Token: 0x04000070 RID: 112
		ReservedMask = 38144
	}
}
