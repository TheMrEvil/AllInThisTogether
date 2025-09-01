using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AC1 RID: 2753
	[Serializable]
	internal class GenericComparer<T> : Comparer<T> where T : IComparable<T>
	{
		// Token: 0x0600627A RID: 25210 RVA: 0x00149C49 File Offset: 0x00147E49
		public override int Compare(T x, T y)
		{
			if (x != null)
			{
				if (y != null)
				{
					return x.CompareTo(y);
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

		// Token: 0x0600627B RID: 25211 RVA: 0x00149C77 File Offset: 0x00147E77
		public override bool Equals(object obj)
		{
			return obj is GenericComparer<T>;
		}

		// Token: 0x0600627C RID: 25212 RVA: 0x00149C82 File Offset: 0x00147E82
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x0600627D RID: 25213 RVA: 0x00149C94 File Offset: 0x00147E94
		public GenericComparer()
		{
		}
	}
}
