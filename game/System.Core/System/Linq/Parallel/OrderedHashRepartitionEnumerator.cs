using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000112 RID: 274
	internal class OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey> : QueryOperatorEnumerator<Pair<TInputOutput, THashKey>, TOrderKey>
	{
		// Token: 0x060008D4 RID: 2260 RVA: 0x0001E874 File Offset: 0x0001CA74
		internal OrderedHashRepartitionEnumerator(QueryOperatorEnumerator<TInputOutput, TOrderKey> source, int partitionCount, int partitionIndex, Func<TInputOutput, THashKey> keySelector, OrderedHashRepartitionStream<TInputOutput, THashKey, TOrderKey> repartitionStream, CountdownEvent barrier, ListChunk<Pair<TInputOutput, THashKey>>[][] valueExchangeMatrix, ListChunk<TOrderKey>[][] keyExchangeMatrix, CancellationToken cancellationToken)
		{
			this._source = source;
			this._partitionCount = partitionCount;
			this._partitionIndex = partitionIndex;
			this._keySelector = keySelector;
			this._repartitionStream = repartitionStream;
			this._barrier = barrier;
			this._valueExchangeMatrix = valueExchangeMatrix;
			this._keyExchangeMatrix = keyExchangeMatrix;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001E8CC File Offset: 0x0001CACC
		internal override bool MoveNext(ref Pair<TInputOutput, THashKey> currentElement, ref TOrderKey currentKey)
		{
			if (this._partitionCount != 1)
			{
				OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey>.Mutables mutables = this._mutables;
				if (mutables == null)
				{
					mutables = (this._mutables = new OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey>.Mutables());
				}
				if (mutables._currentBufferIndex == -1)
				{
					this.EnumerateAndRedistributeElements();
				}
				while (mutables._currentBufferIndex < this._partitionCount)
				{
					if (mutables._currentBuffer != null)
					{
						OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey>.Mutables mutables2 = mutables;
						int num = mutables2._currentIndex + 1;
						mutables2._currentIndex = num;
						if (num < mutables._currentBuffer.Count)
						{
							currentElement = mutables._currentBuffer._chunk[mutables._currentIndex];
							currentKey = mutables._currentKeyBuffer._chunk[mutables._currentIndex];
							return true;
						}
						mutables._currentIndex = -1;
						mutables._currentBuffer = mutables._currentBuffer.Next;
						mutables._currentKeyBuffer = mutables._currentKeyBuffer.Next;
					}
					else
					{
						if (mutables._currentBufferIndex == this._partitionIndex)
						{
							this._barrier.Wait(this._cancellationToken);
							mutables._currentBufferIndex = -1;
						}
						mutables._currentBufferIndex++;
						mutables._currentIndex = -1;
						if (mutables._currentBufferIndex == this._partitionIndex)
						{
							mutables._currentBufferIndex++;
						}
						if (mutables._currentBufferIndex < this._partitionCount)
						{
							mutables._currentBuffer = this._valueExchangeMatrix[mutables._currentBufferIndex][this._partitionIndex];
							mutables._currentKeyBuffer = this._keyExchangeMatrix[mutables._currentBufferIndex][this._partitionIndex];
						}
					}
				}
				return false;
			}
			TInputOutput tinputOutput = default(TInputOutput);
			if (this._source.MoveNext(ref tinputOutput, ref currentKey))
			{
				currentElement = new Pair<TInputOutput, THashKey>(tinputOutput, (this._keySelector == null) ? default(THashKey) : this._keySelector(tinputOutput));
				return true;
			}
			return false;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001EA98 File Offset: 0x0001CC98
		private void EnumerateAndRedistributeElements()
		{
			OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey>.Mutables mutables = this._mutables;
			ListChunk<Pair<TInputOutput, THashKey>>[] array = new ListChunk<Pair<TInputOutput, THashKey>>[this._partitionCount];
			ListChunk<TOrderKey>[] array2 = new ListChunk<TOrderKey>[this._partitionCount];
			TInputOutput tinputOutput = default(TInputOutput);
			TOrderKey e = default(TOrderKey);
			int num = 0;
			while (this._source.MoveNext(ref tinputOutput, ref e))
			{
				if ((num++ & 63) == 0)
				{
					CancellationState.ThrowIfCanceled(this._cancellationToken);
				}
				THashKey thashKey = default(THashKey);
				int num2;
				if (this._keySelector != null)
				{
					thashKey = this._keySelector(tinputOutput);
					num2 = this._repartitionStream.GetHashCode(thashKey) % this._partitionCount;
				}
				else
				{
					num2 = this._repartitionStream.GetHashCode(tinputOutput) % this._partitionCount;
				}
				ListChunk<Pair<TInputOutput, THashKey>> listChunk = array[num2];
				ListChunk<TOrderKey> listChunk2 = array2[num2];
				if (listChunk == null)
				{
					listChunk = (array[num2] = new ListChunk<Pair<TInputOutput, THashKey>>(128));
					listChunk2 = (array2[num2] = new ListChunk<TOrderKey>(128));
				}
				listChunk.Add(new Pair<TInputOutput, THashKey>(tinputOutput, thashKey));
				listChunk2.Add(e);
			}
			for (int i = 0; i < this._partitionCount; i++)
			{
				this._valueExchangeMatrix[this._partitionIndex][i] = array[i];
				this._keyExchangeMatrix[this._partitionIndex][i] = array2[i];
			}
			this._barrier.Signal();
			mutables._currentBufferIndex = this._partitionIndex;
			mutables._currentBuffer = array[this._partitionIndex];
			mutables._currentKeyBuffer = array2[this._partitionIndex];
			mutables._currentIndex = -1;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001EC14 File Offset: 0x0001CE14
		protected override void Dispose(bool disposing)
		{
			if (this._barrier != null)
			{
				if (this._mutables == null || this._mutables._currentBufferIndex == -1)
				{
					this._barrier.Signal();
					this._barrier = null;
				}
				this._source.Dispose();
			}
		}

		// Token: 0x04000637 RID: 1591
		private const int ENUMERATION_NOT_STARTED = -1;

		// Token: 0x04000638 RID: 1592
		private readonly int _partitionCount;

		// Token: 0x04000639 RID: 1593
		private readonly int _partitionIndex;

		// Token: 0x0400063A RID: 1594
		private readonly Func<TInputOutput, THashKey> _keySelector;

		// Token: 0x0400063B RID: 1595
		private readonly HashRepartitionStream<TInputOutput, THashKey, TOrderKey> _repartitionStream;

		// Token: 0x0400063C RID: 1596
		private readonly ListChunk<Pair<TInputOutput, THashKey>>[][] _valueExchangeMatrix;

		// Token: 0x0400063D RID: 1597
		private readonly ListChunk<TOrderKey>[][] _keyExchangeMatrix;

		// Token: 0x0400063E RID: 1598
		private readonly QueryOperatorEnumerator<TInputOutput, TOrderKey> _source;

		// Token: 0x0400063F RID: 1599
		private CountdownEvent _barrier;

		// Token: 0x04000640 RID: 1600
		private readonly CancellationToken _cancellationToken;

		// Token: 0x04000641 RID: 1601
		private OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey>.Mutables _mutables;

		// Token: 0x02000113 RID: 275
		private class Mutables
		{
			// Token: 0x060008D8 RID: 2264 RVA: 0x0001EC52 File Offset: 0x0001CE52
			internal Mutables()
			{
				this._currentBufferIndex = -1;
			}

			// Token: 0x04000642 RID: 1602
			internal int _currentBufferIndex;

			// Token: 0x04000643 RID: 1603
			internal ListChunk<Pair<TInputOutput, THashKey>> _currentBuffer;

			// Token: 0x04000644 RID: 1604
			internal ListChunk<TOrderKey> _currentKeyBuffer;

			// Token: 0x04000645 RID: 1605
			internal int _currentIndex;
		}
	}
}
