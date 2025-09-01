using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020002C7 RID: 711
	internal sealed class ReferenceEquality<T> : IEqualityComparer<T> where T : class
	{
		// Token: 0x06002240 RID: 8768 RVA: 0x00002CCC File Offset: 0x00000ECC
		private ReferenceEquality()
		{
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x000A7E68 File Offset: 0x000A6068
		public bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x000A7E78 File Offset: 0x000A6078
		public int GetHashCode(T obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x000A7E85 File Offset: 0x000A6085
		// Note: this type is marked as 'beforefieldinit'.
		static ReferenceEquality()
		{
		}

		// Token: 0x04000CA0 RID: 3232
		public static readonly IEqualityComparer<T> Default = new ReferenceEquality<T>();
	}
}
