using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AC3 RID: 2755
	[Serializable]
	internal class ObjectComparer<T> : Comparer<T>
	{
		// Token: 0x06006282 RID: 25218 RVA: 0x00149CEA File Offset: 0x00147EEA
		public override int Compare(T x, T y)
		{
			return Comparer.Default.Compare(x, y);
		}

		// Token: 0x06006283 RID: 25219 RVA: 0x00149D02 File Offset: 0x00147F02
		public override bool Equals(object obj)
		{
			return obj is ObjectComparer<T>;
		}

		// Token: 0x06006284 RID: 25220 RVA: 0x00149C82 File Offset: 0x00147E82
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x06006285 RID: 25221 RVA: 0x00149C94 File Offset: 0x00147E94
		public ObjectComparer()
		{
		}
	}
}
