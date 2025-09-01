using System;

namespace IKVM.Reflection
{
	// Token: 0x02000019 RID: 25
	[Flags]
	public enum MemberTypes
	{
		// Token: 0x04000080 RID: 128
		Constructor = 1,
		// Token: 0x04000081 RID: 129
		Event = 2,
		// Token: 0x04000082 RID: 130
		Field = 4,
		// Token: 0x04000083 RID: 131
		Method = 8,
		// Token: 0x04000084 RID: 132
		Property = 16,
		// Token: 0x04000085 RID: 133
		TypeInfo = 32,
		// Token: 0x04000086 RID: 134
		Custom = 64,
		// Token: 0x04000087 RID: 135
		NestedType = 128,
		// Token: 0x04000088 RID: 136
		All = 191
	}
}
