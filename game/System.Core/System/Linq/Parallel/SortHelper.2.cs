using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001FD RID: 509
	internal class SortHelper<TInputOutput, TKey> : SortHelper<TInputOutput>, IDisposable
	{
		// Token: 0x06000C69 RID: 3177 RVA: 0x0002B3C0 File Offset: 0x000295C0
		private SortHelper(QueryOperatorEnumerator<TInputOutput, TKey> source, int partitionCount, int partitionIndex, QueryTaskGroupState groupState, int[][] sharedIndices, OrdinalIndexState indexState, IComparer<TKey> keyComparer, GrowingArray<TKey>[] sharedkeys, TInputOutput[][] sharedValues, Barrier[][] sharedBarriers)
		{
			this._source = source;
			this._partitionCount = partitionCount;
			this._partitionIndex = partitionIndex;
			this._groupState = groupState;
			this._sharedIndices = sharedIndices;
			this._indexState = indexState;
			this._keyComparer = keyComparer;
			this._sharedKeys = sharedkeys;
			this._sharedValues = sharedValues;
			this._sharedBarriers = sharedBarriers;
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002B420 File Offset: 0x00029620
		internal static SortHelper<TInputOutput, TKey>[] GenerateSortHelpers(PartitionedStream<TInputOutput, TKey> partitions, QueryTaskGroupState groupState)
		{
			int partitionCount = partitions.PartitionCount;
			SortHelper<TInputOutput, TKey>[] array = new SortHelper<TInputOutput, TKey>[partitionCount];
			int i = 1;
			int num = 0;
			while (i < partitionCount)
			{
				num++;
				i <<= 1;
			}
			int[][] sharedIndices = new int[partitionCount][];
			GrowingArray<TKey>[] sharedkeys = new GrowingArray<TKey>[partitionCount];
			TInputOutput[][] sharedValues = new TInputOutput[partitionCount][];
			Barrier[][] array2 = JaggedArray<Barrier>.Allocate(num, partitionCount);
			if (partitionCount > 1)
			{
				int num2 = 1;
				for (int j = 0; j < array2.Length; j++)
				{
					for (int k = 0; k < array2[j].Length; k++)
					{
						if (k % num2 == 0)
						{
							array2[j][k] = new Barrier(2);
						}
					}
					num2 *= 2;
				}
			}
			for (int l = 0; l < partitionCount; l++)
			{
				array[l] = new SortHelper<TInputOutput, TKey>(partitions[l], partitionCount, l, groupState, sharedIndices, partitions.OrdinalIndexState, partitions.KeyComparer, sharedkeys, sharedValues, array2);
			}
			return array;
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002B4F4 File Offset: 0x000296F4
		public void Dispose()
		{
			if (this._partitionIndex == 0)
			{
				for (int i = 0; i < this._sharedBarriers.Length; i++)
				{
					for (int j = 0; j < this._sharedBarriers[i].Length; j++)
					{
						Barrier barrier = this._sharedBarriers[i][j];
						if (barrier != null)
						{
							barrier.Dispose();
						}
					}
				}
			}
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0002B548 File Offset: 0x00029748
		internal override TInputOutput[] Sort()
		{
			GrowingArray<TKey> keys = null;
			List<TInputOutput> values = null;
			this.BuildKeysFromSource(ref keys, ref values);
			this.QuickSortIndicesInPlace(keys, values, this._indexState);
			if (this._partitionCount > 1)
			{
				this.MergeSortCooperatively();
			}
			return this._sharedValues[this._partitionIndex];
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0002B590 File Offset: 0x00029790
		private void BuildKeysFromSource(ref GrowingArray<TKey> keys, ref List<TInputOutput> values)
		{
			values = new List<TInputOutput>();
			CancellationToken mergedCancellationToken = this._groupState.CancellationState.MergedCancellationToken;
			try
			{
				TInputOutput item = default(TInputOutput);
				TKey element = default(TKey);
				bool flag = this._source.MoveNext(ref item, ref element);
				if (keys == null)
				{
					keys = new GrowingArray<TKey>();
				}
				if (flag)
				{
					int num = 0;
					do
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(mergedCancellationToken);
						}
						keys.Add(element);
						values.Add(item);
					}
					while (this._source.MoveNext(ref item, ref element));
				}
			}
			finally
			{
				this._source.Dispose();
			}
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0002B634 File Offset: 0x00029834
		private void QuickSortIndicesInPlace(GrowingArray<TKey> keys, List<TInputOutput> values, OrdinalIndexState ordinalIndexState)
		{
			int[] array = new int[values.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = i;
			}
			if (array.Length > 1 && ordinalIndexState.IsWorseThan(OrdinalIndexState.Increasing))
			{
				this.QuickSort(0, array.Length - 1, keys.InternalArray, array, this._groupState.CancellationState.MergedCancellationToken);
			}
			if (this._partitionCount == 1)
			{
				TInputOutput[] array2 = new TInputOutput[values.Count];
				for (int j = 0; j < array.Length; j++)
				{
					array2[j] = values[array[j]];
				}
				this._sharedValues[this._partitionIndex] = array2;
				return;
			}
			this._sharedIndices[this._partitionIndex] = array;
			this._sharedKeys[this._partitionIndex] = keys;
			this._sharedValues[this._partitionIndex] = new TInputOutput[values.Count];
			values.CopyTo(this._sharedValues[this._partitionIndex]);
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0002B71C File Offset: 0x0002991C
		private void MergeSortCooperatively()
		{
			CancellationToken mergedCancellationToken = this._groupState.CancellationState.MergedCancellationToken;
			int num = this._sharedBarriers.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = i == num - 1;
				int num2 = this.ComputePartnerIndex(i);
				if (num2 < this._partitionCount)
				{
					int[] array = this._sharedIndices[this._partitionIndex];
					GrowingArray<TKey> growingArray = this._sharedKeys[this._partitionIndex];
					TKey[] internalArray = growingArray.InternalArray;
					TInputOutput[] array2 = this._sharedValues[this._partitionIndex];
					this._sharedBarriers[i][Math.Min(this._partitionIndex, num2)].SignalAndWait(mergedCancellationToken);
					if (this._partitionIndex >= num2)
					{
						this._sharedBarriers[i][num2].SignalAndWait(mergedCancellationToken);
						int[] array3 = this._sharedIndices[this._partitionIndex];
						TKey[] internalArray2 = this._sharedKeys[this._partitionIndex].InternalArray;
						TInputOutput[] array4 = this._sharedValues[this._partitionIndex];
						int[] array5 = this._sharedIndices[num2];
						GrowingArray<TKey> growingArray2 = this._sharedKeys[num2];
						TInputOutput[] array6 = this._sharedValues[num2];
						int num3 = array4.Length;
						int num4 = array2.Length;
						int num5 = num3 + num4;
						int num6 = (num5 + 1) / 2;
						int j = num5 - 1;
						int num7 = num3 - 1;
						int num8 = num4 - 1;
						while (j >= num6)
						{
							if ((j & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(mergedCancellationToken);
							}
							if (num7 >= 0 && (num8 < 0 || this._keyComparer.Compare(internalArray2[array3[num7]], internalArray[array[num8]]) > 0))
							{
								if (flag)
								{
									array6[j] = array4[array3[num7]];
								}
								else
								{
									array5[j] = array3[num7];
								}
								num7--;
							}
							else
							{
								if (flag)
								{
									array6[j] = array2[array[num8]];
								}
								else
								{
									array5[j] = num3 + array[num8];
								}
								num8--;
							}
							j--;
						}
						if (!flag && array2.Length != 0)
						{
							growingArray2.CopyFrom(internalArray, array2.Length);
							Array.Copy(array2, 0, array6, num3, array2.Length);
						}
						this._sharedBarriers[i][num2].SignalAndWait(mergedCancellationToken);
						return;
					}
					int[] array7 = this._sharedIndices[num2];
					TKey[] internalArray3 = this._sharedKeys[num2].InternalArray;
					TInputOutput[] array8 = this._sharedValues[num2];
					this._sharedIndices[num2] = array;
					this._sharedKeys[num2] = growingArray;
					this._sharedValues[num2] = array2;
					int num9 = array2.Length;
					int num10 = array8.Length;
					int num11 = num9 + num10;
					int[] array9 = null;
					TInputOutput[] array10 = new TInputOutput[num11];
					if (!flag)
					{
						array9 = new int[num11];
					}
					this._sharedIndices[this._partitionIndex] = array9;
					this._sharedKeys[this._partitionIndex] = growingArray;
					this._sharedValues[this._partitionIndex] = array10;
					this._sharedBarriers[i][this._partitionIndex].SignalAndWait(mergedCancellationToken);
					int num12 = (num11 + 1) / 2;
					int k = 0;
					int num13 = 0;
					int num14 = 0;
					while (k < num12)
					{
						if ((k & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(mergedCancellationToken);
						}
						if (num13 < num9 && (num14 >= num10 || this._keyComparer.Compare(internalArray[array[num13]], internalArray3[array7[num14]]) <= 0))
						{
							if (flag)
							{
								array10[k] = array2[array[num13]];
							}
							else
							{
								array9[k] = array[num13];
							}
							num13++;
						}
						else
						{
							if (flag)
							{
								array10[k] = array8[array7[num14]];
							}
							else
							{
								array9[k] = num9 + array7[num14];
							}
							num14++;
						}
						k++;
					}
					if (!flag && num9 > 0)
					{
						Array.Copy(array2, 0, array10, 0, num9);
					}
					this._sharedBarriers[i][this._partitionIndex].SignalAndWait(mergedCancellationToken);
				}
			}
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0002BAE0 File Offset: 0x00029CE0
		private int ComputePartnerIndex(int phase)
		{
			int num = 1 << phase;
			return this._partitionIndex + ((this._partitionIndex % (num * 2) == 0) ? num : (-num));
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0002BB0C File Offset: 0x00029D0C
		private void QuickSort(int left, int right, TKey[] keys, int[] indices, CancellationToken cancelToken)
		{
			if (right - left > 63)
			{
				CancellationState.ThrowIfCanceled(cancelToken);
			}
			do
			{
				int num = left;
				int num2 = right;
				int num3 = indices[num + (num2 - num >> 1)];
				TKey y = keys[num3];
				for (;;)
				{
					if (this._keyComparer.Compare(keys[indices[num]], y) >= 0)
					{
						while (this._keyComparer.Compare(keys[indices[num2]], y) > 0)
						{
							num2--;
						}
						if (num > num2)
						{
							break;
						}
						if (num < num2)
						{
							int num4 = indices[num];
							indices[num] = indices[num2];
							indices[num2] = num4;
						}
						num++;
						num2--;
						if (num > num2)
						{
							break;
						}
					}
					else
					{
						num++;
					}
				}
				if (num2 - left <= right - num)
				{
					if (left < num2)
					{
						this.QuickSort(left, num2, keys, indices, cancelToken);
					}
					left = num;
				}
				else
				{
					if (num < right)
					{
						this.QuickSort(num, right, keys, indices, cancelToken);
					}
					right = num2;
				}
			}
			while (left < right);
		}

		// Token: 0x040008C3 RID: 2243
		private QueryOperatorEnumerator<TInputOutput, TKey> _source;

		// Token: 0x040008C4 RID: 2244
		private int _partitionCount;

		// Token: 0x040008C5 RID: 2245
		private int _partitionIndex;

		// Token: 0x040008C6 RID: 2246
		private QueryTaskGroupState _groupState;

		// Token: 0x040008C7 RID: 2247
		private int[][] _sharedIndices;

		// Token: 0x040008C8 RID: 2248
		private GrowingArray<TKey>[] _sharedKeys;

		// Token: 0x040008C9 RID: 2249
		private TInputOutput[][] _sharedValues;

		// Token: 0x040008CA RID: 2250
		private Barrier[][] _sharedBarriers;

		// Token: 0x040008CB RID: 2251
		private OrdinalIndexState _indexState;

		// Token: 0x040008CC RID: 2252
		private IComparer<TKey> _keyComparer;
	}
}
