using System;

namespace System.Collections.Generic
{
	// Token: 0x020004EE RID: 1262
	internal sealed class SortedSetEqualityComparer<T> : IEqualityComparer<SortedSet<T>>
	{
		// Token: 0x06002951 RID: 10577 RVA: 0x0008E767 File Offset: 0x0008C967
		public SortedSetEqualityComparer(IEqualityComparer<T> memberEqualityComparer) : this(null, memberEqualityComparer)
		{
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x0008E771 File Offset: 0x0008C971
		private SortedSetEqualityComparer(IComparer<T> comparer, IEqualityComparer<T> memberEqualityComparer)
		{
			this._comparer = (comparer ?? Comparer<T>.Default);
			this._memberEqualityComparer = (memberEqualityComparer ?? EqualityComparer<T>.Default);
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x0008E799 File Offset: 0x0008C999
		public bool Equals(SortedSet<T> x, SortedSet<T> y)
		{
			return SortedSet<T>.SortedSetEquals(x, y, this._comparer);
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x0008E7A8 File Offset: 0x0008C9A8
		public int GetHashCode(SortedSet<T> obj)
		{
			int num = 0;
			if (obj != null)
			{
				foreach (T obj2 in obj)
				{
					num ^= (this._memberEqualityComparer.GetHashCode(obj2) & int.MaxValue);
				}
			}
			return num;
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x0008E80C File Offset: 0x0008CA0C
		public override bool Equals(object obj)
		{
			SortedSetEqualityComparer<T> sortedSetEqualityComparer = obj as SortedSetEqualityComparer<T>;
			return sortedSetEqualityComparer != null && this._comparer == sortedSetEqualityComparer._comparer;
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x0008E833 File Offset: 0x0008CA33
		public override int GetHashCode()
		{
			return this._comparer.GetHashCode() ^ this._memberEqualityComparer.GetHashCode();
		}

		// Token: 0x040015DF RID: 5599
		private readonly IComparer<T> _comparer;

		// Token: 0x040015E0 RID: 5600
		private readonly IEqualityComparer<T> _memberEqualityComparer;
	}
}
