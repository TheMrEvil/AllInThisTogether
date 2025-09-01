using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000AAE RID: 2734
	internal sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class
	{
		// Token: 0x060061D1 RID: 25041 RVA: 0x0000259F File Offset: 0x0000079F
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x060061D2 RID: 25042 RVA: 0x0014703C File Offset: 0x0014523C
		public bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x060061D3 RID: 25043 RVA: 0x0014704C File Offset: 0x0014524C
		public int GetHashCode(T obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}

		// Token: 0x060061D4 RID: 25044 RVA: 0x00147059 File Offset: 0x00145259
		// Note: this type is marked as 'beforefieldinit'.
		static ReferenceEqualityComparer()
		{
		}

		// Token: 0x04003A0E RID: 14862
		internal static readonly ReferenceEqualityComparer<T> Instance = new ReferenceEqualityComparer<T>();
	}
}
