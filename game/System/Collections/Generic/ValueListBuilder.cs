using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020004C3 RID: 1219
	internal ref struct ValueListBuilder<T>
	{
		// Token: 0x06002782 RID: 10114 RVA: 0x00088F0A File Offset: 0x0008710A
		public ValueListBuilder(Span<T> initialSpan)
		{
			this._span = initialSpan;
			this._arrayFromPool = null;
			this._pos = 0;
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06002783 RID: 10115 RVA: 0x00088F21 File Offset: 0x00087121
		public int Length
		{
			get
			{
				return this._pos;
			}
		}

		// Token: 0x17000811 RID: 2065
		public T this[int index]
		{
			get
			{
				return this._span[index];
			}
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x00088F38 File Offset: 0x00087138
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(T item)
		{
			int pos = this._pos;
			if (pos >= this._span.Length)
			{
				this.Grow();
			}
			*this._span[pos] = item;
			this._pos = pos + 1;
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x00088F7B File Offset: 0x0008717B
		public ReadOnlySpan<T> AsSpan()
		{
			return this._span.Slice(0, this._pos);
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x00088F94 File Offset: 0x00087194
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			if (this._arrayFromPool != null)
			{
				ArrayPool<T>.Shared.Return(this._arrayFromPool, false);
				this._arrayFromPool = null;
			}
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x00088FB8 File Offset: 0x000871B8
		private void Grow()
		{
			T[] array = ArrayPool<T>.Shared.Rent(this._span.Length * 2);
			this._span.TryCopyTo(array);
			T[] arrayFromPool = this._arrayFromPool;
			this._span = (this._arrayFromPool = array);
			if (arrayFromPool != null)
			{
				ArrayPool<T>.Shared.Return(arrayFromPool, false);
			}
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x0008901A File Offset: 0x0008721A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe T Pop()
		{
			this._pos--;
			return *this._span[this._pos];
		}

		// Token: 0x0400154D RID: 5453
		private Span<T> _span;

		// Token: 0x0400154E RID: 5454
		private T[] _arrayFromPool;

		// Token: 0x0400154F RID: 5455
		private int _pos;
	}
}
