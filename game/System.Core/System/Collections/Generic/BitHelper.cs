using System;

namespace System.Collections.Generic
{
	// Token: 0x0200035B RID: 859
	internal sealed class BitHelper
	{
		// Token: 0x06001A20 RID: 6688 RVA: 0x000575C6 File Offset: 0x000557C6
		internal unsafe BitHelper(int* bitArrayPtr, int length)
		{
			this._arrayPtr = bitArrayPtr;
			this._length = length;
			this._useStackAlloc = true;
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x000575E3 File Offset: 0x000557E3
		internal BitHelper(int[] bitArray, int length)
		{
			this._array = bitArray;
			this._length = length;
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x000575FC File Offset: 0x000557FC
		internal unsafe void MarkBit(int bitPosition)
		{
			int num = bitPosition / 32;
			if (num < this._length && num >= 0)
			{
				int num2 = 1 << bitPosition % 32;
				if (this._useStackAlloc)
				{
					this._arrayPtr[num] |= num2;
					return;
				}
				this._array[num] |= num2;
			}
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00057650 File Offset: 0x00055850
		internal unsafe bool IsMarked(int bitPosition)
		{
			int num = bitPosition / 32;
			if (num >= this._length || num < 0)
			{
				return false;
			}
			int num2 = 1 << bitPosition % 32;
			if (this._useStackAlloc)
			{
				return (this._arrayPtr[num] & num2) != 0;
			}
			return (this._array[num] & num2) != 0;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x000576A2 File Offset: 0x000558A2
		internal static int ToIntArrayLength(int n)
		{
			if (n <= 0)
			{
				return 0;
			}
			return (n - 1) / 32 + 1;
		}

		// Token: 0x04000C85 RID: 3205
		private const byte MarkedBitFlag = 1;

		// Token: 0x04000C86 RID: 3206
		private const byte IntSize = 32;

		// Token: 0x04000C87 RID: 3207
		private readonly int _length;

		// Token: 0x04000C88 RID: 3208
		private unsafe readonly int* _arrayPtr;

		// Token: 0x04000C89 RID: 3209
		private readonly int[] _array;

		// Token: 0x04000C8A RID: 3210
		private readonly bool _useStackAlloc;
	}
}
