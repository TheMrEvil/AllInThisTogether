using System;
using System.Runtime.InteropServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000330 RID: 816
	[StructLayout(LayoutKind.Auto)]
	internal struct RangeWorker
	{
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x0007BD2F File Offset: 0x00079F2F
		internal bool IsInitialized
		{
			get
			{
				return this._indexRanges != null;
			}
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x0007BD3A File Offset: 0x00079F3A
		internal RangeWorker(IndexRange[] ranges, int nInitialRange, long nStep, bool use32BitCurrentIndex)
		{
			this._indexRanges = ranges;
			this._use32BitCurrentIndex = use32BitCurrentIndex;
			this._nCurrentIndexRange = nInitialRange;
			this._nStep = nStep;
			this._nIncrementValue = nStep;
			this._nMaxIncrementValue = 16L * nStep;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x0007BD6C File Offset: 0x00079F6C
		internal unsafe bool FindNewWork(out long nFromInclusiveLocal, out long nToExclusiveLocal)
		{
			int num = this._indexRanges.Length;
			IndexRange indexRange;
			long num2;
			for (;;)
			{
				indexRange = this._indexRanges[this._nCurrentIndexRange];
				if (indexRange._bRangeFinished == 0)
				{
					if (this._indexRanges[this._nCurrentIndexRange]._nSharedCurrentIndexOffset == null)
					{
						Interlocked.CompareExchange<Box<long>>(ref this._indexRanges[this._nCurrentIndexRange]._nSharedCurrentIndexOffset, new Box<long>(0L), null);
					}
					if (IntPtr.Size == 4 && this._use32BitCurrentIndex)
					{
						fixed (long* ptr = &this._indexRanges[this._nCurrentIndexRange]._nSharedCurrentIndexOffset.Value)
						{
							num2 = (long)Interlocked.Add(ref *(int*)ptr, (int)this._nIncrementValue) - this._nIncrementValue;
						}
					}
					else
					{
						num2 = Interlocked.Add(ref this._indexRanges[this._nCurrentIndexRange]._nSharedCurrentIndexOffset.Value, this._nIncrementValue) - this._nIncrementValue;
					}
					if (indexRange._nToExclusive - indexRange._nFromInclusive > num2)
					{
						break;
					}
					Interlocked.Exchange(ref this._indexRanges[this._nCurrentIndexRange]._bRangeFinished, 1);
				}
				this._nCurrentIndexRange = (this._nCurrentIndexRange + 1) % this._indexRanges.Length;
				num--;
				if (num <= 0)
				{
					goto Block_9;
				}
			}
			nFromInclusiveLocal = indexRange._nFromInclusive + num2;
			nToExclusiveLocal = nFromInclusiveLocal + this._nIncrementValue;
			if (nToExclusiveLocal > indexRange._nToExclusive || nToExclusiveLocal < indexRange._nFromInclusive)
			{
				nToExclusiveLocal = indexRange._nToExclusive;
			}
			if (this._nIncrementValue < this._nMaxIncrementValue)
			{
				this._nIncrementValue *= 2L;
				if (this._nIncrementValue > this._nMaxIncrementValue)
				{
					this._nIncrementValue = this._nMaxIncrementValue;
				}
			}
			return true;
			Block_9:
			nFromInclusiveLocal = 0L;
			nToExclusiveLocal = 0L;
			return false;
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x0007BF1C File Offset: 0x0007A11C
		internal bool FindNewWork32(out int nFromInclusiveLocal32, out int nToExclusiveLocal32)
		{
			long num;
			long num2;
			bool result = this.FindNewWork(out num, out num2);
			nFromInclusiveLocal32 = (int)num;
			nToExclusiveLocal32 = (int)num2;
			return result;
		}

		// Token: 0x04001C46 RID: 7238
		internal readonly IndexRange[] _indexRanges;

		// Token: 0x04001C47 RID: 7239
		internal int _nCurrentIndexRange;

		// Token: 0x04001C48 RID: 7240
		internal long _nStep;

		// Token: 0x04001C49 RID: 7241
		internal long _nIncrementValue;

		// Token: 0x04001C4A RID: 7242
		internal readonly long _nMaxIncrementValue;

		// Token: 0x04001C4B RID: 7243
		internal readonly bool _use32BitCurrentIndex;
	}
}
