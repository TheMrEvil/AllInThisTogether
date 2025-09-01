using System;

namespace UnityEngine.Experimental.AI
{
	// Token: 0x02000020 RID: 32
	[Flags]
	public enum PathQueryStatus
	{
		// Token: 0x04000064 RID: 100
		Failure = -2147483648,
		// Token: 0x04000065 RID: 101
		Success = 1073741824,
		// Token: 0x04000066 RID: 102
		InProgress = 536870912,
		// Token: 0x04000067 RID: 103
		StatusDetailMask = 16777215,
		// Token: 0x04000068 RID: 104
		WrongMagic = 1,
		// Token: 0x04000069 RID: 105
		WrongVersion = 2,
		// Token: 0x0400006A RID: 106
		OutOfMemory = 4,
		// Token: 0x0400006B RID: 107
		InvalidParam = 8,
		// Token: 0x0400006C RID: 108
		BufferTooSmall = 16,
		// Token: 0x0400006D RID: 109
		OutOfNodes = 32,
		// Token: 0x0400006E RID: 110
		PartialResult = 64
	}
}
