using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020000FA RID: 250
	internal class RangeEnumerable : ParallelQuery<int>, IParallelPartitionable<int>
	{
		// Token: 0x06000880 RID: 2176 RVA: 0x0001D18B File Offset: 0x0001B38B
		internal RangeEnumerable(int from, int count) : base(QuerySettings.Empty)
		{
			this._from = from;
			this._count = count;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001D1A8 File Offset: 0x0001B3A8
		public QueryOperatorEnumerator<int, int>[] GetPartitions(int partitionCount)
		{
			int num = this._count / partitionCount;
			int num2 = this._count % partitionCount;
			int num3 = 0;
			QueryOperatorEnumerator<int, int>[] array = new QueryOperatorEnumerator<int, int>[partitionCount];
			for (int i = 0; i < partitionCount; i++)
			{
				int num4 = (i < num2) ? (num + 1) : num;
				array[i] = new RangeEnumerable.RangeEnumerator(this._from + num3, num4, num3);
				num3 += num4;
			}
			return array;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001D207 File Offset: 0x0001B407
		public override IEnumerator<int> GetEnumerator()
		{
			return new RangeEnumerable.RangeEnumerator(this._from, this._count, 0).AsClassicEnumerator();
		}

		// Token: 0x040005E3 RID: 1507
		private int _from;

		// Token: 0x040005E4 RID: 1508
		private int _count;

		// Token: 0x020000FB RID: 251
		private class RangeEnumerator : QueryOperatorEnumerator<int, int>
		{
			// Token: 0x06000883 RID: 2179 RVA: 0x0001D220 File Offset: 0x0001B420
			internal RangeEnumerator(int from, int count, int initialIndex)
			{
				this._from = from;
				this._count = count;
				this._initialIndex = initialIndex;
			}

			// Token: 0x06000884 RID: 2180 RVA: 0x0001D240 File Offset: 0x0001B440
			internal override bool MoveNext(ref int currentElement, ref int currentKey)
			{
				if (this._currentCount == null)
				{
					this._currentCount = new Shared<int>(-1);
				}
				int num = this._currentCount.Value + 1;
				if (num < this._count)
				{
					this._currentCount.Value = num;
					currentElement = num + this._from;
					currentKey = num + this._initialIndex;
					return true;
				}
				return false;
			}

			// Token: 0x06000885 RID: 2181 RVA: 0x0001D29B File Offset: 0x0001B49B
			internal override void Reset()
			{
				this._currentCount = null;
			}

			// Token: 0x040005E5 RID: 1509
			private readonly int _from;

			// Token: 0x040005E6 RID: 1510
			private readonly int _count;

			// Token: 0x040005E7 RID: 1511
			private readonly int _initialIndex;

			// Token: 0x040005E8 RID: 1512
			private Shared<int> _currentCount;
		}
	}
}
