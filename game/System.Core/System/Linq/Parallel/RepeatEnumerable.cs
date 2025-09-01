using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020000FC RID: 252
	internal class RepeatEnumerable<TResult> : ParallelQuery<TResult>, IParallelPartitionable<TResult>
	{
		// Token: 0x06000886 RID: 2182 RVA: 0x0001D2A4 File Offset: 0x0001B4A4
		internal RepeatEnumerable(TResult element, int count) : base(QuerySettings.Empty)
		{
			this._element = element;
			this._count = count;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001D2C0 File Offset: 0x0001B4C0
		public QueryOperatorEnumerator<TResult, int>[] GetPartitions(int partitionCount)
		{
			int num = (this._count + partitionCount - 1) / partitionCount;
			QueryOperatorEnumerator<TResult, int>[] array = new QueryOperatorEnumerator<TResult, int>[partitionCount];
			int i = 0;
			int num2 = 0;
			while (i < partitionCount)
			{
				if (num2 + num > this._count)
				{
					array[i] = new RepeatEnumerable<TResult>.RepeatEnumerator(this._element, (num2 < this._count) ? (this._count - num2) : 0, num2);
				}
				else
				{
					array[i] = new RepeatEnumerable<TResult>.RepeatEnumerator(this._element, num, num2);
				}
				i++;
				num2 += num;
			}
			return array;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001D334 File Offset: 0x0001B534
		public override IEnumerator<TResult> GetEnumerator()
		{
			return new RepeatEnumerable<TResult>.RepeatEnumerator(this._element, this._count, 0).AsClassicEnumerator();
		}

		// Token: 0x040005E9 RID: 1513
		private TResult _element;

		// Token: 0x040005EA RID: 1514
		private int _count;

		// Token: 0x020000FD RID: 253
		private class RepeatEnumerator : QueryOperatorEnumerator<TResult, int>
		{
			// Token: 0x06000889 RID: 2185 RVA: 0x0001D34D File Offset: 0x0001B54D
			internal RepeatEnumerator(TResult element, int count, int indexOffset)
			{
				this._element = element;
				this._count = count;
				this._indexOffset = indexOffset;
			}

			// Token: 0x0600088A RID: 2186 RVA: 0x0001D36C File Offset: 0x0001B56C
			internal override bool MoveNext(ref TResult currentElement, ref int currentKey)
			{
				if (this._currentIndex == null)
				{
					this._currentIndex = new Shared<int>(-1);
				}
				if (this._currentIndex.Value < this._count - 1)
				{
					this._currentIndex.Value++;
					currentElement = this._element;
					currentKey = this._currentIndex.Value + this._indexOffset;
					return true;
				}
				return false;
			}

			// Token: 0x0600088B RID: 2187 RVA: 0x0001D3D8 File Offset: 0x0001B5D8
			internal override void Reset()
			{
				this._currentIndex = null;
			}

			// Token: 0x040005EB RID: 1515
			private readonly TResult _element;

			// Token: 0x040005EC RID: 1516
			private readonly int _count;

			// Token: 0x040005ED RID: 1517
			private readonly int _indexOffset;

			// Token: 0x040005EE RID: 1518
			private Shared<int> _currentIndex;
		}
	}
}
