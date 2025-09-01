using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AC2 RID: 2754
	[Serializable]
	internal class NullableComparer<T> : Comparer<T?> where T : struct, IComparable<T>
	{
		// Token: 0x0600627E RID: 25214 RVA: 0x00149C9C File Offset: 0x00147E9C
		public override int Compare(T? x, T? y)
		{
			if (x != null)
			{
				if (y != null)
				{
					return x.value.CompareTo(y.value);
				}
				return 1;
			}
			else
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x0600627F RID: 25215 RVA: 0x00149CD7 File Offset: 0x00147ED7
		public override bool Equals(object obj)
		{
			return obj is NullableComparer<T>;
		}

		// Token: 0x06006280 RID: 25216 RVA: 0x00149C82 File Offset: 0x00147E82
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x06006281 RID: 25217 RVA: 0x00149CE2 File Offset: 0x00147EE2
		public NullableComparer()
		{
		}
	}
}
