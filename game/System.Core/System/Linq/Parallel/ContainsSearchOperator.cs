using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000192 RID: 402
	internal sealed class ContainsSearchOperator<TInput> : UnaryQueryOperator<TInput, bool>
	{
		// Token: 0x06000ABF RID: 2751 RVA: 0x00025A7D File Offset: 0x00023C7D
		internal ContainsSearchOperator(IEnumerable<TInput> child, TInput searchValue, IEqualityComparer<TInput> comparer) : base(child)
		{
			this._searchValue = searchValue;
			if (comparer == null)
			{
				this._comparer = EqualityComparer<TInput>.Default;
				return;
			}
			this._comparer = comparer;
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00025AA4 File Offset: 0x00023CA4
		internal bool Aggregate()
		{
			using (IEnumerator<bool> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00025904 File Offset: 0x00023B04
		internal override QueryResults<bool> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TInput, bool>.UnaryQueryOperatorResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00025AF4 File Offset: 0x00023CF4
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<bool> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<bool, int> partitionedStream = new PartitionedStream<bool, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
			Shared<bool> resultFoundFlag = new Shared<bool>(false);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new ContainsSearchOperator<TInput>.ContainsSearchOperatorEnumerator<TKey>(inputStream[i], this._searchValue, this._comparer, i, resultFoundFlag, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x000080E3 File Offset: 0x000062E3
		[ExcludeFromCodeCoverage]
		internal override IEnumerable<bool> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000764 RID: 1892
		private readonly TInput _searchValue;

		// Token: 0x04000765 RID: 1893
		private readonly IEqualityComparer<TInput> _comparer;

		// Token: 0x02000193 RID: 403
		private class ContainsSearchOperatorEnumerator<TKey> : QueryOperatorEnumerator<bool, int>
		{
			// Token: 0x06000AC5 RID: 2757 RVA: 0x00025B5C File Offset: 0x00023D5C
			internal ContainsSearchOperatorEnumerator(QueryOperatorEnumerator<TInput, TKey> source, TInput searchValue, IEqualityComparer<TInput> comparer, int partitionIndex, Shared<bool> resultFoundFlag, CancellationToken cancellationToken)
			{
				this._source = source;
				this._searchValue = searchValue;
				this._comparer = comparer;
				this._partitionIndex = partitionIndex;
				this._resultFoundFlag = resultFoundFlag;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000AC6 RID: 2758 RVA: 0x00025B94 File Offset: 0x00023D94
			internal override bool MoveNext(ref bool currentElement, ref int currentKey)
			{
				if (this._resultFoundFlag.Value)
				{
					return false;
				}
				TInput x = default(TInput);
				TKey tkey = default(TKey);
				if (this._source.MoveNext(ref x, ref tkey))
				{
					currentElement = false;
					currentKey = this._partitionIndex;
					int num = 0;
					for (;;)
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						if (this._resultFoundFlag.Value)
						{
							break;
						}
						if (this._comparer.Equals(x, this._searchValue))
						{
							goto Block_5;
						}
						if (!this._source.MoveNext(ref x, ref tkey))
						{
							return true;
						}
					}
					return false;
					Block_5:
					this._resultFoundFlag.Value = true;
					currentElement = true;
					return true;
				}
				return false;
			}

			// Token: 0x06000AC7 RID: 2759 RVA: 0x00025C3B File Offset: 0x00023E3B
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000766 RID: 1894
			private readonly QueryOperatorEnumerator<TInput, TKey> _source;

			// Token: 0x04000767 RID: 1895
			private readonly TInput _searchValue;

			// Token: 0x04000768 RID: 1896
			private readonly IEqualityComparer<TInput> _comparer;

			// Token: 0x04000769 RID: 1897
			private readonly int _partitionIndex;

			// Token: 0x0400076A RID: 1898
			private readonly Shared<bool> _resultFoundFlag;

			// Token: 0x0400076B RID: 1899
			private CancellationToken _cancellationToken;
		}
	}
}
