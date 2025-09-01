using System;
using System.Runtime.CompilerServices;

namespace Unity.Burst.CompilerServices
{
	// Token: 0x02000025 RID: 37
	public static class Constant
	{
		// Token: 0x06000135 RID: 309 RVA: 0x000079A6 File Offset: 0x00005BA6
		public static bool IsConstantExpression<[IsUnmanaged] T>(T t) where T : struct, ValueType
		{
			return false;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000079A9 File Offset: 0x00005BA9
		public unsafe static bool IsConstantExpression(void* t)
		{
			return false;
		}
	}
}
