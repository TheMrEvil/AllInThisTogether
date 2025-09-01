using System;

namespace System.Collections.Generic
{
	// Token: 0x020004C5 RID: 1221
	internal sealed class BitHelper
	{
		// Token: 0x06002792 RID: 10130 RVA: 0x000890C1 File Offset: 0x000872C1
		internal unsafe BitHelper(int* bitArrayPtr, int length)
		{
			this._arrayPtr = bitArrayPtr;
			this._length = length;
			this._useStackAlloc = true;
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000890DE File Offset: 0x000872DE
		internal BitHelper(int[] bitArray, int length)
		{
			this._array = bitArray;
			this._length = length;
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x000890F4 File Offset: 0x000872F4
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

		// Token: 0x06002795 RID: 10133 RVA: 0x00089148 File Offset: 0x00087348
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

		// Token: 0x06002796 RID: 10134 RVA: 0x0008919A File Offset: 0x0008739A
		internal static int ToIntArrayLength(int n)
		{
			if (n <= 0)
			{
				return 0;
			}
			return (n - 1) / 32 + 1;
		}

		// Token: 0x04001552 RID: 5458
		private const byte MarkedBitFlag = 1;

		// Token: 0x04001553 RID: 5459
		private const byte IntSize = 32;

		// Token: 0x04001554 RID: 5460
		private readonly int _length;

		// Token: 0x04001555 RID: 5461
		private unsafe readonly int* _arrayPtr;

		// Token: 0x04001556 RID: 5462
		private readonly int[] _array;

		// Token: 0x04001557 RID: 5463
		private readonly bool _useStackAlloc;
	}
}
