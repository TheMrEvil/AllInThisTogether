using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000115 RID: 277
	internal class PartitionedDataSource<T> : PartitionedStream<T, int>
	{
		// Token: 0x060008DA RID: 2266 RVA: 0x0001ED03 File Offset: 0x0001CF03
		internal PartitionedDataSource(IEnumerable<T> source, int partitionCount, bool useStriping) : base(partitionCount, Util.GetDefaultComparer<int>(), (source is IList<!0>) ? OrdinalIndexState.Indexable : OrdinalIndexState.Correct)
		{
			this.InitializePartitions(source, partitionCount, useStriping);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001ED28 File Offset: 0x0001CF28
		private void InitializePartitions(IEnumerable<T> source, int partitionCount, bool useStriping)
		{
			ParallelEnumerableWrapper<T> parallelEnumerableWrapper = source as ParallelEnumerableWrapper<T>;
			if (parallelEnumerableWrapper != null)
			{
				source = parallelEnumerableWrapper.WrappedEnumerable;
			}
			IList<T> list = source as IList<!0>;
			if (list != null)
			{
				QueryOperatorEnumerator<T, int>[] array = new QueryOperatorEnumerator<T, int>[partitionCount];
				T[] array2 = source as T[];
				int num = -1;
				if (useStriping)
				{
					num = Scheduling.GetDefaultChunkSize<T>();
					if (num < 1)
					{
						num = 1;
					}
				}
				for (int i = 0; i < partitionCount; i++)
				{
					if (array2 != null)
					{
						if (useStriping)
						{
							array[i] = new PartitionedDataSource<T>.ArrayIndexRangeEnumerator(array2, partitionCount, i, num);
						}
						else
						{
							array[i] = new PartitionedDataSource<T>.ArrayContiguousIndexRangeEnumerator(array2, partitionCount, i);
						}
					}
					else if (useStriping)
					{
						array[i] = new PartitionedDataSource<T>.ListIndexRangeEnumerator(list, partitionCount, i, num);
					}
					else
					{
						array[i] = new PartitionedDataSource<T>.ListContiguousIndexRangeEnumerator(list, partitionCount, i);
					}
				}
				this._partitions = array;
				return;
			}
			this._partitions = PartitionedDataSource<T>.MakePartitions(source.GetEnumerator(), partitionCount);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001EDE8 File Offset: 0x0001CFE8
		private static QueryOperatorEnumerator<T, int>[] MakePartitions(IEnumerator<T> source, int partitionCount)
		{
			QueryOperatorEnumerator<T, int>[] array = new QueryOperatorEnumerator<T, int>[partitionCount];
			object sourceSyncLock = new object();
			Shared<int> currentIndex = new Shared<int>(0);
			Shared<int> degreeOfParallelism = new Shared<int>(partitionCount);
			Shared<bool> exceptionTracker = new Shared<bool>(false);
			for (int i = 0; i < partitionCount; i++)
			{
				array[i] = new PartitionedDataSource<T>.ContiguousChunkLazyEnumerator(source, exceptionTracker, sourceSyncLock, currentIndex, degreeOfParallelism);
			}
			return array;
		}

		// Token: 0x02000116 RID: 278
		internal sealed class ArrayIndexRangeEnumerator : QueryOperatorEnumerator<T, int>
		{
			// Token: 0x060008DD RID: 2269 RVA: 0x0001EE38 File Offset: 0x0001D038
			internal ArrayIndexRangeEnumerator(T[] data, int partitionCount, int partitionIndex, int maxChunkSize)
			{
				this._data = data;
				this._elementCount = data.Length;
				this._partitionCount = partitionCount;
				this._partitionIndex = partitionIndex;
				this._maxChunkSize = maxChunkSize;
				int num = maxChunkSize * partitionCount;
				this._sectionCount = this._elementCount / num + ((this._elementCount % num == 0) ? 0 : 1);
			}

			// Token: 0x060008DE RID: 2270 RVA: 0x0001EE94 File Offset: 0x0001D094
			internal override bool MoveNext(ref T currentElement, ref int currentKey)
			{
				PartitionedDataSource<T>.ArrayIndexRangeEnumerator.Mutables mutables = this._mutables;
				if (mutables == null)
				{
					mutables = (this._mutables = new PartitionedDataSource<T>.ArrayIndexRangeEnumerator.Mutables());
				}
				PartitionedDataSource<T>.ArrayIndexRangeEnumerator.Mutables mutables2 = mutables;
				int num = mutables2._currentPositionInChunk + 1;
				mutables2._currentPositionInChunk = num;
				if (num < mutables._currentChunkSize || this.MoveNextSlowPath())
				{
					currentKey = mutables._currentChunkOffset + mutables._currentPositionInChunk;
					currentElement = this._data[currentKey];
					return true;
				}
				return false;
			}

			// Token: 0x060008DF RID: 2271 RVA: 0x0001EF00 File Offset: 0x0001D100
			private bool MoveNextSlowPath()
			{
				PartitionedDataSource<T>.ArrayIndexRangeEnumerator.Mutables mutables = this._mutables;
				PartitionedDataSource<T>.ArrayIndexRangeEnumerator.Mutables mutables2 = mutables;
				int num = mutables2._currentSection + 1;
				mutables2._currentSection = num;
				int num2 = num;
				int num3 = this._sectionCount - num2;
				if (num3 <= 0)
				{
					return false;
				}
				int num4 = num2 * this._partitionCount * this._maxChunkSize;
				mutables._currentPositionInChunk = 0;
				if (num3 > 1)
				{
					mutables._currentChunkSize = this._maxChunkSize;
					mutables._currentChunkOffset = num4 + this._partitionIndex * this._maxChunkSize;
				}
				else
				{
					int num5 = this._elementCount - num4;
					int num6 = num5 / this._partitionCount;
					int num7 = num5 % this._partitionCount;
					mutables._currentChunkSize = num6;
					if (this._partitionIndex < num7)
					{
						mutables._currentChunkSize++;
					}
					if (mutables._currentChunkSize == 0)
					{
						return false;
					}
					mutables._currentChunkOffset = num4 + this._partitionIndex * num6 + ((this._partitionIndex < num7) ? this._partitionIndex : num7);
				}
				return true;
			}

			// Token: 0x04000646 RID: 1606
			private readonly T[] _data;

			// Token: 0x04000647 RID: 1607
			private readonly int _elementCount;

			// Token: 0x04000648 RID: 1608
			private readonly int _partitionCount;

			// Token: 0x04000649 RID: 1609
			private readonly int _partitionIndex;

			// Token: 0x0400064A RID: 1610
			private readonly int _maxChunkSize;

			// Token: 0x0400064B RID: 1611
			private readonly int _sectionCount;

			// Token: 0x0400064C RID: 1612
			private PartitionedDataSource<T>.ArrayIndexRangeEnumerator.Mutables _mutables;

			// Token: 0x02000117 RID: 279
			private class Mutables
			{
				// Token: 0x060008E0 RID: 2272 RVA: 0x0001EFE2 File Offset: 0x0001D1E2
				internal Mutables()
				{
					this._currentSection = -1;
				}

				// Token: 0x0400064D RID: 1613
				internal int _currentSection;

				// Token: 0x0400064E RID: 1614
				internal int _currentChunkSize;

				// Token: 0x0400064F RID: 1615
				internal int _currentPositionInChunk;

				// Token: 0x04000650 RID: 1616
				internal int _currentChunkOffset;
			}
		}

		// Token: 0x02000118 RID: 280
		internal sealed class ArrayContiguousIndexRangeEnumerator : QueryOperatorEnumerator<T, int>
		{
			// Token: 0x060008E1 RID: 2273 RVA: 0x0001EFF4 File Offset: 0x0001D1F4
			internal ArrayContiguousIndexRangeEnumerator(T[] data, int partitionCount, int partitionIndex)
			{
				this._data = data;
				int num = data.Length / partitionCount;
				int num2 = data.Length % partitionCount;
				int num3 = partitionIndex * num + ((partitionIndex < num2) ? partitionIndex : num2);
				this._startIndex = num3 - 1;
				this._maximumIndex = num3 + num + ((partitionIndex < num2) ? 1 : 0);
			}

			// Token: 0x060008E2 RID: 2274 RVA: 0x0001F044 File Offset: 0x0001D244
			internal override bool MoveNext(ref T currentElement, ref int currentKey)
			{
				if (this._currentIndex == null)
				{
					this._currentIndex = new Shared<int>(this._startIndex);
				}
				Shared<int> currentIndex = this._currentIndex;
				int num = currentIndex.Value + 1;
				currentIndex.Value = num;
				int num2 = num;
				if (num2 < this._maximumIndex)
				{
					currentKey = num2;
					currentElement = this._data[num2];
					return true;
				}
				return false;
			}

			// Token: 0x04000651 RID: 1617
			private readonly T[] _data;

			// Token: 0x04000652 RID: 1618
			private readonly int _startIndex;

			// Token: 0x04000653 RID: 1619
			private readonly int _maximumIndex;

			// Token: 0x04000654 RID: 1620
			private Shared<int> _currentIndex;
		}

		// Token: 0x02000119 RID: 281
		internal sealed class ListIndexRangeEnumerator : QueryOperatorEnumerator<T, int>
		{
			// Token: 0x060008E3 RID: 2275 RVA: 0x0001F0A4 File Offset: 0x0001D2A4
			internal ListIndexRangeEnumerator(IList<T> data, int partitionCount, int partitionIndex, int maxChunkSize)
			{
				this._data = data;
				this._elementCount = data.Count;
				this._partitionCount = partitionCount;
				this._partitionIndex = partitionIndex;
				this._maxChunkSize = maxChunkSize;
				int num = maxChunkSize * partitionCount;
				this._sectionCount = this._elementCount / num + ((this._elementCount % num == 0) ? 0 : 1);
			}

			// Token: 0x060008E4 RID: 2276 RVA: 0x0001F104 File Offset: 0x0001D304
			internal override bool MoveNext(ref T currentElement, ref int currentKey)
			{
				PartitionedDataSource<T>.ListIndexRangeEnumerator.Mutables mutables = this._mutables;
				if (mutables == null)
				{
					mutables = (this._mutables = new PartitionedDataSource<T>.ListIndexRangeEnumerator.Mutables());
				}
				PartitionedDataSource<T>.ListIndexRangeEnumerator.Mutables mutables2 = mutables;
				int num = mutables2._currentPositionInChunk + 1;
				mutables2._currentPositionInChunk = num;
				if (num < mutables._currentChunkSize || this.MoveNextSlowPath())
				{
					currentKey = mutables._currentChunkOffset + mutables._currentPositionInChunk;
					currentElement = this._data[currentKey];
					return true;
				}
				return false;
			}

			// Token: 0x060008E5 RID: 2277 RVA: 0x0001F170 File Offset: 0x0001D370
			private bool MoveNextSlowPath()
			{
				PartitionedDataSource<T>.ListIndexRangeEnumerator.Mutables mutables = this._mutables;
				PartitionedDataSource<T>.ListIndexRangeEnumerator.Mutables mutables2 = mutables;
				int num = mutables2._currentSection + 1;
				mutables2._currentSection = num;
				int num2 = num;
				int num3 = this._sectionCount - num2;
				if (num3 <= 0)
				{
					return false;
				}
				int num4 = num2 * this._partitionCount * this._maxChunkSize;
				mutables._currentPositionInChunk = 0;
				if (num3 > 1)
				{
					mutables._currentChunkSize = this._maxChunkSize;
					mutables._currentChunkOffset = num4 + this._partitionIndex * this._maxChunkSize;
				}
				else
				{
					int num5 = this._elementCount - num4;
					int num6 = num5 / this._partitionCount;
					int num7 = num5 % this._partitionCount;
					mutables._currentChunkSize = num6;
					if (this._partitionIndex < num7)
					{
						mutables._currentChunkSize++;
					}
					if (mutables._currentChunkSize == 0)
					{
						return false;
					}
					mutables._currentChunkOffset = num4 + this._partitionIndex * num6 + ((this._partitionIndex < num7) ? this._partitionIndex : num7);
				}
				return true;
			}

			// Token: 0x04000655 RID: 1621
			private readonly IList<T> _data;

			// Token: 0x04000656 RID: 1622
			private readonly int _elementCount;

			// Token: 0x04000657 RID: 1623
			private readonly int _partitionCount;

			// Token: 0x04000658 RID: 1624
			private readonly int _partitionIndex;

			// Token: 0x04000659 RID: 1625
			private readonly int _maxChunkSize;

			// Token: 0x0400065A RID: 1626
			private readonly int _sectionCount;

			// Token: 0x0400065B RID: 1627
			private PartitionedDataSource<T>.ListIndexRangeEnumerator.Mutables _mutables;

			// Token: 0x0200011A RID: 282
			private class Mutables
			{
				// Token: 0x060008E6 RID: 2278 RVA: 0x0001F252 File Offset: 0x0001D452
				internal Mutables()
				{
					this._currentSection = -1;
				}

				// Token: 0x0400065C RID: 1628
				internal int _currentSection;

				// Token: 0x0400065D RID: 1629
				internal int _currentChunkSize;

				// Token: 0x0400065E RID: 1630
				internal int _currentPositionInChunk;

				// Token: 0x0400065F RID: 1631
				internal int _currentChunkOffset;
			}
		}

		// Token: 0x0200011B RID: 283
		internal sealed class ListContiguousIndexRangeEnumerator : QueryOperatorEnumerator<T, int>
		{
			// Token: 0x060008E7 RID: 2279 RVA: 0x0001F264 File Offset: 0x0001D464
			internal ListContiguousIndexRangeEnumerator(IList<T> data, int partitionCount, int partitionIndex)
			{
				this._data = data;
				int num = data.Count / partitionCount;
				int num2 = data.Count % partitionCount;
				int num3 = partitionIndex * num + ((partitionIndex < num2) ? partitionIndex : num2);
				this._startIndex = num3 - 1;
				this._maximumIndex = num3 + num + ((partitionIndex < num2) ? 1 : 0);
			}

			// Token: 0x060008E8 RID: 2280 RVA: 0x0001F2B8 File Offset: 0x0001D4B8
			internal override bool MoveNext(ref T currentElement, ref int currentKey)
			{
				if (this._currentIndex == null)
				{
					this._currentIndex = new Shared<int>(this._startIndex);
				}
				Shared<int> currentIndex = this._currentIndex;
				int num = currentIndex.Value + 1;
				currentIndex.Value = num;
				int num2 = num;
				if (num2 < this._maximumIndex)
				{
					currentKey = num2;
					currentElement = this._data[num2];
					return true;
				}
				return false;
			}

			// Token: 0x04000660 RID: 1632
			private readonly IList<T> _data;

			// Token: 0x04000661 RID: 1633
			private readonly int _startIndex;

			// Token: 0x04000662 RID: 1634
			private readonly int _maximumIndex;

			// Token: 0x04000663 RID: 1635
			private Shared<int> _currentIndex;
		}

		// Token: 0x0200011C RID: 284
		private class ContiguousChunkLazyEnumerator : QueryOperatorEnumerator<T, int>
		{
			// Token: 0x060008E9 RID: 2281 RVA: 0x0001F316 File Offset: 0x0001D516
			internal ContiguousChunkLazyEnumerator(IEnumerator<T> source, Shared<bool> exceptionTracker, object sourceSyncLock, Shared<int> currentIndex, Shared<int> degreeOfParallelism)
			{
				this._source = source;
				this._sourceSyncLock = sourceSyncLock;
				this._currentIndex = currentIndex;
				this._activeEnumeratorsCount = degreeOfParallelism;
				this._exceptionTracker = exceptionTracker;
			}

			// Token: 0x060008EA RID: 2282 RVA: 0x0001F344 File Offset: 0x0001D544
			internal override bool MoveNext(ref T currentElement, ref int currentKey)
			{
				PartitionedDataSource<T>.ContiguousChunkLazyEnumerator.Mutables mutables = this._mutables;
				if (mutables == null)
				{
					mutables = (this._mutables = new PartitionedDataSource<T>.ContiguousChunkLazyEnumerator.Mutables());
				}
				T[] chunkBuffer;
				int num2;
				for (;;)
				{
					chunkBuffer = mutables._chunkBuffer;
					PartitionedDataSource<T>.ContiguousChunkLazyEnumerator.Mutables mutables2 = mutables;
					int num = mutables2._currentChunkIndex + 1;
					mutables2._currentChunkIndex = num;
					num2 = num;
					if (num2 < mutables._currentChunkSize)
					{
						break;
					}
					object sourceSyncLock = this._sourceSyncLock;
					lock (sourceSyncLock)
					{
						int num3 = 0;
						if (this._exceptionTracker.Value)
						{
							return false;
						}
						try
						{
							while (num3 < mutables._nextChunkMaxSize && this._source.MoveNext())
							{
								chunkBuffer[num3] = this._source.Current;
								num3++;
							}
						}
						catch
						{
							this._exceptionTracker.Value = true;
							throw;
						}
						mutables._currentChunkSize = num3;
						if (num3 == 0)
						{
							return false;
						}
						mutables._chunkBaseIndex = this._currentIndex.Value;
						checked
						{
							this._currentIndex.Value += num3;
						}
					}
					if (mutables._nextChunkMaxSize < chunkBuffer.Length)
					{
						PartitionedDataSource<T>.ContiguousChunkLazyEnumerator.Mutables mutables3 = mutables;
						num = mutables3._chunkCounter;
						mutables3._chunkCounter = num + 1;
						if ((num & 7) == 7)
						{
							mutables._nextChunkMaxSize *= 2;
							if (mutables._nextChunkMaxSize > chunkBuffer.Length)
							{
								mutables._nextChunkMaxSize = chunkBuffer.Length;
							}
						}
					}
					mutables._currentChunkIndex = -1;
				}
				currentElement = chunkBuffer[num2];
				currentKey = mutables._chunkBaseIndex + num2;
				return true;
			}

			// Token: 0x060008EB RID: 2283 RVA: 0x0001F4D0 File Offset: 0x0001D6D0
			protected override void Dispose(bool disposing)
			{
				if (Interlocked.Decrement(ref this._activeEnumeratorsCount.Value) == 0)
				{
					this._source.Dispose();
				}
			}

			// Token: 0x04000664 RID: 1636
			private const int chunksPerChunkSize = 7;

			// Token: 0x04000665 RID: 1637
			private readonly IEnumerator<T> _source;

			// Token: 0x04000666 RID: 1638
			private readonly object _sourceSyncLock;

			// Token: 0x04000667 RID: 1639
			private readonly Shared<int> _currentIndex;

			// Token: 0x04000668 RID: 1640
			private readonly Shared<int> _activeEnumeratorsCount;

			// Token: 0x04000669 RID: 1641
			private readonly Shared<bool> _exceptionTracker;

			// Token: 0x0400066A RID: 1642
			private PartitionedDataSource<T>.ContiguousChunkLazyEnumerator.Mutables _mutables;

			// Token: 0x0200011D RID: 285
			private class Mutables
			{
				// Token: 0x060008EC RID: 2284 RVA: 0x0001F4EF File Offset: 0x0001D6EF
				internal Mutables()
				{
					this._nextChunkMaxSize = 1;
					this._chunkBuffer = new T[Scheduling.GetDefaultChunkSize<T>()];
					this._currentChunkSize = 0;
					this._currentChunkIndex = -1;
					this._chunkBaseIndex = 0;
					this._chunkCounter = 0;
				}

				// Token: 0x0400066B RID: 1643
				internal readonly T[] _chunkBuffer;

				// Token: 0x0400066C RID: 1644
				internal int _nextChunkMaxSize;

				// Token: 0x0400066D RID: 1645
				internal int _currentChunkSize;

				// Token: 0x0400066E RID: 1646
				internal int _currentChunkIndex;

				// Token: 0x0400066F RID: 1647
				internal int _chunkBaseIndex;

				// Token: 0x04000670 RID: 1648
				internal int _chunkCounter;
			}
		}
	}
}
