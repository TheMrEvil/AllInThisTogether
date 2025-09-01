using System;

namespace System.Reflection
{
	// Token: 0x020008FE RID: 2302
	[Flags]
	internal enum PInfo
	{
		// Token: 0x04003073 RID: 12403
		Attributes = 1,
		// Token: 0x04003074 RID: 12404
		GetMethod = 2,
		// Token: 0x04003075 RID: 12405
		SetMethod = 4,
		// Token: 0x04003076 RID: 12406
		ReflectedType = 8,
		// Token: 0x04003077 RID: 12407
		DeclaringType = 16,
		// Token: 0x04003078 RID: 12408
		Name = 32
	}
}
