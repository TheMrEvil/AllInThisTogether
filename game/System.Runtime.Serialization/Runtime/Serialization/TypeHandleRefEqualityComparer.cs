using System;
using System.Collections.Generic;

namespace System.Runtime.Serialization
{
	// Token: 0x020000C1 RID: 193
	internal class TypeHandleRefEqualityComparer : IEqualityComparer<TypeHandleRef>
	{
		// Token: 0x06000B33 RID: 2867 RVA: 0x00030248 File Offset: 0x0002E448
		public bool Equals(TypeHandleRef x, TypeHandleRef y)
		{
			return x.Value.Equals(y.Value);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0003026C File Offset: 0x0002E46C
		public int GetHashCode(TypeHandleRef obj)
		{
			return obj.Value.GetHashCode();
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0000222F File Offset: 0x0000042F
		public TypeHandleRefEqualityComparer()
		{
		}
	}
}
