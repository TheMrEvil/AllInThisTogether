using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000190 RID: 400
	internal sealed class AnyAllSearchOperator<TInput> : UnaryQueryOperator<TInput, bool>
	{
		// Token: 0x06000AB6 RID: 2742 RVA: 0x00025888 File Offset: 0x00023A88
		internal AnyAllSearchOperator(IEnumerable<TInput> child, bool qualification, Func<TInput, bool> predicate) : base(child)
		{
			this._qualification = qualification;
			this._predicate = predicate;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000258A0 File Offset: 0x00023AA0
		internal bool Aggregate()
		{
			using (IEnumerator<bool> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == this._qualification)
					{
						return this._qualification;
					}
				}
			}
			return !this._qualification;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00025904 File Offset: 0x00023B04
		internal override QueryResults<bool> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TInput, bool>.UnaryQueryOperatorResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002591C File Offset: 0x00023B1C
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<bool> recipient, bool preferStriping, QuerySettings settings)
		{
			Shared<bool> resultFoundFlag = new Shared<bool>(false);
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<bool, int> partitionedStream = new PartitionedStream<bool, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new AnyAllSearchOperator<TInput>.AnyAllSearchOperatorEnumerator<TKey>(inputStream[i], this._qualification, this._predicate, i, resultFoundFlag, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x000080E3 File Offset: 0x000062E3
		[ExcludeFromCodeCoverage]
		internal override IEnumerable<bool> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400075C RID: 1884
		private readonly Func<TInput, bool> _predicate;

		// Token: 0x0400075D RID: 1885
		private readonly bool _qualification;

		// Token: 0x02000191 RID: 401
		private class AnyAllSearchOperatorEnumerator<TKey> : QueryOperatorEnumerator<bool, int>
		{
			// Token: 0x06000ABC RID: 2748 RVA: 0x00025984 File Offset: 0x00023B84
			internal AnyAllSearchOperatorEnumerator(QueryOperatorEnumerator<TInput, TKey> source, bool qualification, Func<TInput, bool> predicate, int partitionIndex, Shared<bool> resultFoundFlag, CancellationToken cancellationToken)
			{
				this._source = source;
				this._qualification = qualification;
				this._predicate = predicate;
				this._partitionIndex = partitionIndex;
				this._resultFoundFlag = resultFoundFlag;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000ABD RID: 2749 RVA: 0x000259BC File Offset: 0x00023BBC
			internal override bool MoveNext(ref bool currentElement, ref int currentKey)
			{
				if (this._resultFoundFlag.Value)
				{
					return false;
				}
				TInput arg = default(TInput);
				TKey tkey = default(TKey);
				if (this._source.MoveNext(ref arg, ref tkey))
				{
					currentElement = !this._qualification;
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
						if (this._predicate(arg) == this._qualification)
						{
							goto Block_5;
						}
						if (!this._source.MoveNext(ref arg, ref tkey))
						{
							return true;
						}
					}
					return false;
					Block_5:
					this._resultFoundFlag.Value = true;
					currentElement = this._qualification;
					return true;
				}
				return false;
			}

			// Token: 0x06000ABE RID: 2750 RVA: 0x00025A70 File Offset: 0x00023C70
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400075E RID: 1886
			private readonly QueryOperatorEnumerator<TInput, TKey> _source;

			// Token: 0x0400075F RID: 1887
			private readonly Func<TInput, bool> _predicate;

			// Token: 0x04000760 RID: 1888
			private readonly bool _qualification;

			// Token: 0x04000761 RID: 1889
			private readonly int _partitionIndex;

			// Token: 0x04000762 RID: 1890
			private readonly Shared<bool> _resultFoundFlag;

			// Token: 0x04000763 RID: 1891
			private readonly CancellationToken _cancellationToken;
		}
	}
}
