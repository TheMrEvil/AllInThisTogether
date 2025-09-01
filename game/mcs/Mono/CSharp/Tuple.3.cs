using System;

namespace Mono.CSharp
{
	// Token: 0x020002CA RID: 714
	internal static class Tuple
	{
		// Token: 0x06002254 RID: 8788 RVA: 0x000A8025 File Offset: 0x000A6225
		public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new Tuple<T1, T2>(item1, item2);
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000A802E File Offset: 0x000A622E
		public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
		{
			return new Tuple<T1, T2, T3>(item1, item2, item3);
		}
	}
}
