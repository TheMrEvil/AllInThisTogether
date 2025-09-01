using System;

namespace Unity.Burst.CompilerServices
{
	// Token: 0x02000026 RID: 38
	public static class Hint
	{
		// Token: 0x06000137 RID: 311 RVA: 0x000079AC File Offset: 0x00005BAC
		public static bool Likely(bool condition)
		{
			return condition;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000079AF File Offset: 0x00005BAF
		public static bool Unlikely(bool condition)
		{
			return condition;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000079B2 File Offset: 0x00005BB2
		public static void Assume(bool condition)
		{
		}
	}
}
