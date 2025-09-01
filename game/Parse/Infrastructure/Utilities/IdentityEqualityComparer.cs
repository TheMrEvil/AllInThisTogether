using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Parse.Infrastructure.Utilities
{
	// Token: 0x02000054 RID: 84
	public class IdentityEqualityComparer<T> : IEqualityComparer<T>
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x0000D125 File Offset: 0x0000B325
		public bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000D135 File Offset: 0x0000B335
		public int GetHashCode(T obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000D142 File Offset: 0x0000B342
		public IdentityEqualityComparer()
		{
		}
	}
}
