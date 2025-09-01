using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001C1 RID: 449
	internal sealed class SingleQueryOperator<TSource> : UnaryQueryOperator<TSource, TSource>
	{
		// Token: 0x06000B6F RID: 2927 RVA: 0x0002817F File Offset: 0x0002637F
		internal SingleQueryOperator(IEnumerable<TSource> child, Func<TSource, bool> predicate) : base(child)
		{
			this._predicate = predicate;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00026180 File Offset: 0x00024380
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults(base.Child.Open(settings, false), this, settings, preferStriping);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00028190 File Offset: 0x00026390
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TSource, int> partitionedStream = new PartitionedStream<TSource, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Shuffled);
			Shared<int> totalElementCount = new Shared<int>(0);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new SingleQueryOperator<TSource>.SingleQueryOperatorEnumerator<TKey>(inputStream[i], this._predicate, totalElementCount);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x000080E3 File Offset: 0x000062E3
		[ExcludeFromCodeCoverage]
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040007F5 RID: 2037
		private readonly Func<TSource, bool> _predicate;

		// Token: 0x020001C2 RID: 450
		private class SingleQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TSource, int>
		{
			// Token: 0x06000B74 RID: 2932 RVA: 0x000281E5 File Offset: 0x000263E5
			internal SingleQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, Func<TSource, bool> predicate, Shared<int> totalElementCount)
			{
				this._source = source;
				this._predicate = predicate;
				this._totalElementCount = totalElementCount;
			}

			// Token: 0x06000B75 RID: 2933 RVA: 0x00028204 File Offset: 0x00026404
			internal override bool MoveNext(ref TSource currentElement, ref int currentKey)
			{
				if (!this._alreadySearched)
				{
					bool flag = false;
					TSource tsource = default(TSource);
					TKey tkey = default(TKey);
					while (this._source.MoveNext(ref tsource, ref tkey))
					{
						if (this._predicate == null || this._predicate(tsource))
						{
							Interlocked.Increment(ref this._totalElementCount.Value);
							currentElement = tsource;
							currentKey = 0;
							if (flag)
							{
								this._yieldExtra = true;
								break;
							}
							flag = true;
						}
						if (Volatile.Read(ref this._totalElementCount.Value) > 1)
						{
							break;
						}
					}
					this._alreadySearched = true;
					return flag;
				}
				if (this._yieldExtra)
				{
					this._yieldExtra = false;
					currentElement = default(TSource);
					currentKey = 0;
					return true;
				}
				return false;
			}

			// Token: 0x06000B76 RID: 2934 RVA: 0x000282B5 File Offset: 0x000264B5
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040007F6 RID: 2038
			private QueryOperatorEnumerator<TSource, TKey> _source;

			// Token: 0x040007F7 RID: 2039
			private Func<TSource, bool> _predicate;

			// Token: 0x040007F8 RID: 2040
			private bool _alreadySearched;

			// Token: 0x040007F9 RID: 2041
			private bool _yieldExtra;

			// Token: 0x040007FA RID: 2042
			private Shared<int> _totalElementCount;
		}
	}
}
