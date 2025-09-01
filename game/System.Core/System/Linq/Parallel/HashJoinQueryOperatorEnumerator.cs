using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200012B RID: 299
	internal class HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, THashKey, TOutput> : QueryOperatorEnumerator<TOutput, TLeftKey>
	{
		// Token: 0x06000924 RID: 2340 RVA: 0x00020318 File Offset: 0x0001E518
		internal HashJoinQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TLeftInput, THashKey>, TLeftKey> leftSource, QueryOperatorEnumerator<Pair<TRightInput, THashKey>, int> rightSource, Func<TLeftInput, TRightInput, TOutput> singleResultSelector, Func<TLeftInput, IEnumerable<TRightInput>, TOutput> groupResultSelector, IEqualityComparer<THashKey> keyComparer, CancellationToken cancellationToken)
		{
			this._leftSource = leftSource;
			this._rightSource = rightSource;
			this._singleResultSelector = singleResultSelector;
			this._groupResultSelector = groupResultSelector;
			this._keyComparer = keyComparer;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00020350 File Offset: 0x0001E550
		internal override bool MoveNext(ref TOutput currentElement, ref TLeftKey currentKey)
		{
			HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, THashKey, TOutput>.Mutables mutables = this._mutables;
			if (mutables == null)
			{
				mutables = (this._mutables = new HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, THashKey, TOutput>.Mutables());
				mutables._rightHashLookup = new HashLookup<THashKey, Pair<TRightInput, ListChunk<TRightInput>>>(this._keyComparer);
				Pair<TRightInput, THashKey> pair = default(Pair<TRightInput, THashKey>);
				int num = 0;
				int num2 = 0;
				while (this._rightSource.MoveNext(ref pair, ref num))
				{
					if ((num2++ & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					TRightInput first = pair.First;
					THashKey second = pair.Second;
					if (second != null)
					{
						Pair<TRightInput, ListChunk<TRightInput>> value = default(Pair<TRightInput, ListChunk<TRightInput>>);
						if (!mutables._rightHashLookup.TryGetValue(second, ref value))
						{
							value = new Pair<TRightInput, ListChunk<TRightInput>>(first, null);
							if (this._groupResultSelector != null)
							{
								value.Second = new ListChunk<TRightInput>(2);
								value.Second.Add(first);
							}
							mutables._rightHashLookup.Add(second, value);
						}
						else
						{
							if (value.Second == null)
							{
								value.Second = new ListChunk<TRightInput>(2);
								mutables._rightHashLookup[second] = value;
							}
							value.Second.Add(first);
						}
					}
				}
			}
			ListChunk<TRightInput> currentRightMatches = mutables._currentRightMatches;
			if (currentRightMatches != null && mutables._currentRightMatchesIndex == currentRightMatches.Count)
			{
				ListChunk<TRightInput> listChunk = mutables._currentRightMatches = currentRightMatches.Next;
				mutables._currentRightMatchesIndex = 0;
			}
			if (mutables._currentRightMatches == null)
			{
				Pair<TLeftInput, THashKey> pair2 = default(Pair<TLeftInput, THashKey>);
				TLeftKey tleftKey = default(TLeftKey);
				while (this._leftSource.MoveNext(ref pair2, ref tleftKey))
				{
					HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, THashKey, TOutput>.Mutables mutables2 = mutables;
					int outputLoopCount = mutables2._outputLoopCount;
					mutables2._outputLoopCount = outputLoopCount + 1;
					if ((outputLoopCount & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					Pair<TRightInput, ListChunk<TRightInput>> pair3 = default(Pair<TRightInput, ListChunk<TRightInput>>);
					TLeftInput first2 = pair2.First;
					THashKey second2 = pair2.Second;
					if (second2 != null && mutables._rightHashLookup.TryGetValue(second2, ref pair3) && this._singleResultSelector != null)
					{
						mutables._currentRightMatches = pair3.Second;
						mutables._currentRightMatchesIndex = 0;
						currentElement = this._singleResultSelector(first2, pair3.First);
						currentKey = tleftKey;
						if (pair3.Second != null)
						{
							mutables._currentLeft = first2;
							mutables._currentLeftKey = tleftKey;
						}
						return true;
					}
					if (this._groupResultSelector != null)
					{
						IEnumerable<TRightInput> enumerable = pair3.Second;
						if (enumerable == null)
						{
							enumerable = ParallelEnumerable.Empty<TRightInput>();
						}
						currentElement = this._groupResultSelector(first2, enumerable);
						currentKey = tleftKey;
						return true;
					}
				}
				return false;
			}
			currentElement = this._singleResultSelector(mutables._currentLeft, mutables._currentRightMatches._chunk[mutables._currentRightMatchesIndex]);
			currentKey = mutables._currentLeftKey;
			mutables._currentRightMatchesIndex++;
			return true;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0002060D File Offset: 0x0001E80D
		protected override void Dispose(bool disposing)
		{
			this._leftSource.Dispose();
			this._rightSource.Dispose();
		}

		// Token: 0x0400069D RID: 1693
		private readonly QueryOperatorEnumerator<Pair<TLeftInput, THashKey>, TLeftKey> _leftSource;

		// Token: 0x0400069E RID: 1694
		private readonly QueryOperatorEnumerator<Pair<TRightInput, THashKey>, int> _rightSource;

		// Token: 0x0400069F RID: 1695
		private readonly Func<TLeftInput, TRightInput, TOutput> _singleResultSelector;

		// Token: 0x040006A0 RID: 1696
		private readonly Func<TLeftInput, IEnumerable<TRightInput>, TOutput> _groupResultSelector;

		// Token: 0x040006A1 RID: 1697
		private readonly IEqualityComparer<THashKey> _keyComparer;

		// Token: 0x040006A2 RID: 1698
		private readonly CancellationToken _cancellationToken;

		// Token: 0x040006A3 RID: 1699
		private HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, THashKey, TOutput>.Mutables _mutables;

		// Token: 0x0200012C RID: 300
		private class Mutables
		{
			// Token: 0x06000927 RID: 2343 RVA: 0x00002162 File Offset: 0x00000362
			public Mutables()
			{
			}

			// Token: 0x040006A4 RID: 1700
			internal TLeftInput _currentLeft;

			// Token: 0x040006A5 RID: 1701
			internal TLeftKey _currentLeftKey;

			// Token: 0x040006A6 RID: 1702
			internal HashLookup<THashKey, Pair<TRightInput, ListChunk<TRightInput>>> _rightHashLookup;

			// Token: 0x040006A7 RID: 1703
			internal ListChunk<TRightInput> _currentRightMatches;

			// Token: 0x040006A8 RID: 1704
			internal int _currentRightMatchesIndex;

			// Token: 0x040006A9 RID: 1705
			internal int _outputLoopCount;
		}
	}
}
