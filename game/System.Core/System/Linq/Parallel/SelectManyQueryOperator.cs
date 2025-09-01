using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001B9 RID: 441
	internal sealed class SelectManyQueryOperator<TLeftInput, TRightInput, TOutput> : UnaryQueryOperator<TLeftInput, TOutput>
	{
		// Token: 0x06000B52 RID: 2898 RVA: 0x000279DC File Offset: 0x00025BDC
		internal SelectManyQueryOperator(IEnumerable<TLeftInput> leftChild, Func<TLeftInput, IEnumerable<TRightInput>> rightChildSelector, Func<TLeftInput, int, IEnumerable<TRightInput>> indexedRightChildSelector, Func<TLeftInput, TRightInput, TOutput> resultSelector) : base(leftChild)
		{
			this._rightChildSelector = rightChildSelector;
			this._indexedRightChildSelector = indexedRightChildSelector;
			this._resultSelector = resultSelector;
			this._outputOrdered = (base.Child.OutputOrdered || indexedRightChildSelector != null);
			this.InitOrderIndex();
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00027A1C File Offset: 0x00025C1C
		private void InitOrderIndex()
		{
			OrdinalIndexState ordinalIndexState = base.Child.OrdinalIndexState;
			if (this._indexedRightChildSelector != null)
			{
				this._prematureMerge = ordinalIndexState.IsWorseThan(OrdinalIndexState.Correct);
				this._limitsParallelism = (this._prematureMerge && ordinalIndexState != OrdinalIndexState.Shuffled);
			}
			else if (base.OutputOrdered)
			{
				this._prematureMerge = ordinalIndexState.IsWorseThan(OrdinalIndexState.Increasing);
			}
			base.SetOrdinalIndexState(OrdinalIndexState.Increasing);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00027A80 File Offset: 0x00025C80
		internal override void WrapPartitionedStream<TLeftKey>(PartitionedStream<TLeftInput, TLeftKey> inputStream, IPartitionedStreamRecipient<TOutput> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			if (this._indexedRightChildSelector != null)
			{
				PartitionedStream<TLeftInput, int> inputStream2;
				if (this._prematureMerge)
				{
					inputStream2 = QueryOperator<TLeftInput>.ExecuteAndCollectResults<TLeftKey>(inputStream, partitionCount, base.OutputOrdered, preferStriping, settings).GetPartitionedStream();
				}
				else
				{
					inputStream2 = (PartitionedStream<TLeftInput, int>)inputStream;
				}
				this.WrapPartitionedStreamIndexed(inputStream2, recipient, settings);
				return;
			}
			if (this._prematureMerge)
			{
				PartitionedStream<TLeftInput, int> partitionedStream = QueryOperator<TLeftInput>.ExecuteAndCollectResults<TLeftKey>(inputStream, partitionCount, base.OutputOrdered, preferStriping, settings).GetPartitionedStream();
				this.WrapPartitionedStreamNotIndexed<int>(partitionedStream, recipient, settings);
				return;
			}
			this.WrapPartitionedStreamNotIndexed<TLeftKey>(inputStream, recipient, settings);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00027B04 File Offset: 0x00025D04
		private void WrapPartitionedStreamNotIndexed<TLeftKey>(PartitionedStream<TLeftInput, TLeftKey> inputStream, IPartitionedStreamRecipient<TOutput> recipient, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PairComparer<TLeftKey, int> keyComparer = new PairComparer<TLeftKey, int>(inputStream.KeyComparer, Util.GetDefaultComparer<int>());
			PartitionedStream<TOutput, Pair<TLeftKey, int>> partitionedStream = new PartitionedStream<TOutput, Pair<TLeftKey, int>>(partitionCount, keyComparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.SelectManyQueryOperatorEnumerator<TLeftKey>(inputStream[i], this, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<Pair<TLeftKey, int>>(partitionedStream);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00027B6C File Offset: 0x00025D6C
		private void WrapPartitionedStreamIndexed(PartitionedStream<TLeftInput, int> inputStream, IPartitionedStreamRecipient<TOutput> recipient, QuerySettings settings)
		{
			PairComparer<int, int> keyComparer = new PairComparer<int, int>(inputStream.KeyComparer, Util.GetDefaultComparer<int>());
			PartitionedStream<TOutput, Pair<int, int>> partitionedStream = new PartitionedStream<TOutput, Pair<int, int>>(inputStream.PartitionCount, keyComparer, this.OrdinalIndexState);
			for (int i = 0; i < inputStream.PartitionCount; i++)
			{
				partitionedStream[i] = new SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.IndexedSelectManyQueryOperatorEnumerator(inputStream[i], this, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<Pair<int, int>>(partitionedStream);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00027BD5 File Offset: 0x00025DD5
		internal override QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TLeftInput, TOutput>.UnaryQueryOperatorResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00027BEC File Offset: 0x00025DEC
		internal override IEnumerable<TOutput> AsSequentialQuery(CancellationToken token)
		{
			if (this._rightChildSelector != null)
			{
				if (this._resultSelector != null)
				{
					return CancellableEnumerable.Wrap<TLeftInput>(base.Child.AsSequentialQuery(token), token).SelectMany(this._rightChildSelector, this._resultSelector);
				}
				return (IEnumerable<!2>)CancellableEnumerable.Wrap<TLeftInput>(base.Child.AsSequentialQuery(token), token).SelectMany(this._rightChildSelector);
			}
			else
			{
				if (this._resultSelector != null)
				{
					return CancellableEnumerable.Wrap<TLeftInput>(base.Child.AsSequentialQuery(token), token).SelectMany(this._indexedRightChildSelector, this._resultSelector);
				}
				return (IEnumerable<!2>)CancellableEnumerable.Wrap<TLeftInput>(base.Child.AsSequentialQuery(token), token).SelectMany(this._indexedRightChildSelector);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x00027C9E File Offset: 0x00025E9E
		internal override bool LimitsParallelism
		{
			get
			{
				return this._limitsParallelism;
			}
		}

		// Token: 0x040007D7 RID: 2007
		private readonly Func<TLeftInput, IEnumerable<TRightInput>> _rightChildSelector;

		// Token: 0x040007D8 RID: 2008
		private readonly Func<TLeftInput, int, IEnumerable<TRightInput>> _indexedRightChildSelector;

		// Token: 0x040007D9 RID: 2009
		private readonly Func<TLeftInput, TRightInput, TOutput> _resultSelector;

		// Token: 0x040007DA RID: 2010
		private bool _prematureMerge;

		// Token: 0x040007DB RID: 2011
		private bool _limitsParallelism;

		// Token: 0x020001BA RID: 442
		private class IndexedSelectManyQueryOperatorEnumerator : QueryOperatorEnumerator<TOutput, Pair<int, int>>
		{
			// Token: 0x06000B5A RID: 2906 RVA: 0x00027CA6 File Offset: 0x00025EA6
			internal IndexedSelectManyQueryOperatorEnumerator(QueryOperatorEnumerator<TLeftInput, int> leftSource, SelectManyQueryOperator<TLeftInput, TRightInput, TOutput> selectManyOperator, CancellationToken cancellationToken)
			{
				this._leftSource = leftSource;
				this._selectManyOperator = selectManyOperator;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000B5B RID: 2907 RVA: 0x00027CC4 File Offset: 0x00025EC4
			internal override bool MoveNext(ref TOutput currentElement, ref Pair<int, int> currentKey)
			{
				for (;;)
				{
					if (this._currentRightSource == null)
					{
						this._mutables = new SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.IndexedSelectManyQueryOperatorEnumerator.Mutables();
						SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.IndexedSelectManyQueryOperatorEnumerator.Mutables mutables = this._mutables;
						int lhsCount = mutables._lhsCount;
						mutables._lhsCount = lhsCount + 1;
						if ((lhsCount & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						if (!this._leftSource.MoveNext(ref this._mutables._currentLeftElement, ref this._mutables._currentLeftSourceIndex))
						{
							break;
						}
						IEnumerable<TRightInput> enumerable = this._selectManyOperator._indexedRightChildSelector(this._mutables._currentLeftElement, this._mutables._currentLeftSourceIndex);
						this._currentRightSource = enumerable.GetEnumerator();
						if (this._selectManyOperator._resultSelector == null)
						{
							this._currentRightSourceAsOutput = (IEnumerator<!2>)this._currentRightSource;
						}
					}
					if (this._currentRightSource.MoveNext())
					{
						goto Block_4;
					}
					this._currentRightSource.Dispose();
					this._currentRightSource = null;
					this._currentRightSourceAsOutput = null;
				}
				return false;
				Block_4:
				this._mutables._currentRightSourceIndex++;
				if (this._selectManyOperator._resultSelector != null)
				{
					currentElement = this._selectManyOperator._resultSelector(this._mutables._currentLeftElement, this._currentRightSource.Current);
				}
				else
				{
					currentElement = this._currentRightSourceAsOutput.Current;
				}
				currentKey = new Pair<int, int>(this._mutables._currentLeftSourceIndex, this._mutables._currentRightSourceIndex);
				return true;
			}

			// Token: 0x06000B5C RID: 2908 RVA: 0x00027E32 File Offset: 0x00026032
			protected override void Dispose(bool disposing)
			{
				this._leftSource.Dispose();
				if (this._currentRightSource != null)
				{
					this._currentRightSource.Dispose();
				}
			}

			// Token: 0x040007DC RID: 2012
			private readonly QueryOperatorEnumerator<TLeftInput, int> _leftSource;

			// Token: 0x040007DD RID: 2013
			private readonly SelectManyQueryOperator<TLeftInput, TRightInput, TOutput> _selectManyOperator;

			// Token: 0x040007DE RID: 2014
			private IEnumerator<TRightInput> _currentRightSource;

			// Token: 0x040007DF RID: 2015
			private IEnumerator<TOutput> _currentRightSourceAsOutput;

			// Token: 0x040007E0 RID: 2016
			private SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.IndexedSelectManyQueryOperatorEnumerator.Mutables _mutables;

			// Token: 0x040007E1 RID: 2017
			private readonly CancellationToken _cancellationToken;

			// Token: 0x020001BB RID: 443
			private class Mutables
			{
				// Token: 0x06000B5D RID: 2909 RVA: 0x00027E52 File Offset: 0x00026052
				public Mutables()
				{
				}

				// Token: 0x040007E2 RID: 2018
				internal int _currentRightSourceIndex = -1;

				// Token: 0x040007E3 RID: 2019
				internal TLeftInput _currentLeftElement;

				// Token: 0x040007E4 RID: 2020
				internal int _currentLeftSourceIndex;

				// Token: 0x040007E5 RID: 2021
				internal int _lhsCount;
			}
		}

		// Token: 0x020001BC RID: 444
		private class SelectManyQueryOperatorEnumerator<TLeftKey> : QueryOperatorEnumerator<TOutput, Pair<TLeftKey, int>>
		{
			// Token: 0x06000B5E RID: 2910 RVA: 0x00027E61 File Offset: 0x00026061
			internal SelectManyQueryOperatorEnumerator(QueryOperatorEnumerator<TLeftInput, TLeftKey> leftSource, SelectManyQueryOperator<TLeftInput, TRightInput, TOutput> selectManyOperator, CancellationToken cancellationToken)
			{
				this._leftSource = leftSource;
				this._selectManyOperator = selectManyOperator;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000B5F RID: 2911 RVA: 0x00027E80 File Offset: 0x00026080
			internal override bool MoveNext(ref TOutput currentElement, ref Pair<TLeftKey, int> currentKey)
			{
				for (;;)
				{
					if (this._currentRightSource == null)
					{
						this._mutables = new SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.SelectManyQueryOperatorEnumerator<TLeftKey>.Mutables();
						SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.SelectManyQueryOperatorEnumerator<TLeftKey>.Mutables mutables = this._mutables;
						int lhsCount = mutables._lhsCount;
						mutables._lhsCount = lhsCount + 1;
						if ((lhsCount & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						if (!this._leftSource.MoveNext(ref this._mutables._currentLeftElement, ref this._mutables._currentLeftKey))
						{
							break;
						}
						IEnumerable<TRightInput> enumerable = this._selectManyOperator._rightChildSelector(this._mutables._currentLeftElement);
						this._currentRightSource = enumerable.GetEnumerator();
						if (this._selectManyOperator._resultSelector == null)
						{
							this._currentRightSourceAsOutput = (IEnumerator<!2>)this._currentRightSource;
						}
					}
					if (this._currentRightSource.MoveNext())
					{
						goto Block_4;
					}
					this._currentRightSource.Dispose();
					this._currentRightSource = null;
					this._currentRightSourceAsOutput = null;
				}
				return false;
				Block_4:
				this._mutables._currentRightSourceIndex++;
				if (this._selectManyOperator._resultSelector != null)
				{
					currentElement = this._selectManyOperator._resultSelector(this._mutables._currentLeftElement, this._currentRightSource.Current);
				}
				else
				{
					currentElement = this._currentRightSourceAsOutput.Current;
				}
				currentKey = new Pair<TLeftKey, int>(this._mutables._currentLeftKey, this._mutables._currentRightSourceIndex);
				return true;
			}

			// Token: 0x06000B60 RID: 2912 RVA: 0x00027FE3 File Offset: 0x000261E3
			protected override void Dispose(bool disposing)
			{
				this._leftSource.Dispose();
				if (this._currentRightSource != null)
				{
					this._currentRightSource.Dispose();
				}
			}

			// Token: 0x040007E6 RID: 2022
			private readonly QueryOperatorEnumerator<TLeftInput, TLeftKey> _leftSource;

			// Token: 0x040007E7 RID: 2023
			private readonly SelectManyQueryOperator<TLeftInput, TRightInput, TOutput> _selectManyOperator;

			// Token: 0x040007E8 RID: 2024
			private IEnumerator<TRightInput> _currentRightSource;

			// Token: 0x040007E9 RID: 2025
			private IEnumerator<TOutput> _currentRightSourceAsOutput;

			// Token: 0x040007EA RID: 2026
			private SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.SelectManyQueryOperatorEnumerator<TLeftKey>.Mutables _mutables;

			// Token: 0x040007EB RID: 2027
			private readonly CancellationToken _cancellationToken;

			// Token: 0x020001BD RID: 445
			private class Mutables
			{
				// Token: 0x06000B61 RID: 2913 RVA: 0x00028003 File Offset: 0x00026203
				public Mutables()
				{
				}

				// Token: 0x040007EC RID: 2028
				internal int _currentRightSourceIndex = -1;

				// Token: 0x040007ED RID: 2029
				internal TLeftInput _currentLeftElement;

				// Token: 0x040007EE RID: 2030
				internal TLeftKey _currentLeftKey;

				// Token: 0x040007EF RID: 2031
				internal int _lhsCount;
			}
		}
	}
}
