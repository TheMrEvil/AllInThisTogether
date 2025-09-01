using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000358 RID: 856
	internal sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class
	{
		// Token: 0x06001A0F RID: 6671 RVA: 0x00002162 File Offset: 0x00000362
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x000573B7 File Offset: 0x000555B7
		public bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x000573C7 File Offset: 0x000555C7
		public int GetHashCode(T obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x000573D4 File Offset: 0x000555D4
		// Note: this type is marked as 'beforefieldinit'.
		static ReferenceEqualityComparer()
		{
		}

		// Token: 0x04000C7F RID: 3199
		internal static readonly ReferenceEqualityComparer<T> Instance = new ReferenceEqualityComparer<T>();
	}
}
