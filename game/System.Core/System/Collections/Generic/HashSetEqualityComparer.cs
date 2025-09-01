using System;

namespace System.Collections.Generic
{
	// Token: 0x02000360 RID: 864
	[Serializable]
	internal sealed class HashSetEqualityComparer<T> : IEqualityComparer<HashSet<T>>
	{
		// Token: 0x06001A63 RID: 6755 RVA: 0x00058F13 File Offset: 0x00057113
		public HashSetEqualityComparer()
		{
			this._comparer = EqualityComparer<T>.Default;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00058F26 File Offset: 0x00057126
		public bool Equals(HashSet<T> x, HashSet<T> y)
		{
			return HashSet<T>.HashSetEquals(x, y, this._comparer);
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00058F38 File Offset: 0x00057138
		public int GetHashCode(HashSet<T> obj)
		{
			int num = 0;
			if (obj != null)
			{
				foreach (T obj2 in obj)
				{
					num ^= (this._comparer.GetHashCode(obj2) & int.MaxValue);
				}
			}
			return num;
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00058F9C File Offset: 0x0005719C
		public override bool Equals(object obj)
		{
			HashSetEqualityComparer<T> hashSetEqualityComparer = obj as HashSetEqualityComparer<T>;
			return hashSetEqualityComparer != null && this._comparer == hashSetEqualityComparer._comparer;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00058FC3 File Offset: 0x000571C3
		public override int GetHashCode()
		{
			return this._comparer.GetHashCode();
		}

		// Token: 0x04000CA3 RID: 3235
		private readonly IEqualityComparer<T> _comparer;
	}
}
