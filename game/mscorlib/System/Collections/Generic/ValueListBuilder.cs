using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000AA5 RID: 2725
	[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
	internal ref struct ValueListBuilder<T>
	{
		// Token: 0x06006193 RID: 24979 RVA: 0x001463A3 File Offset: 0x001445A3
		public ValueListBuilder(Span<T> initialSpan)
		{
			this._span = initialSpan;
			this._arrayFromPool = null;
			this._pos = 0;
		}

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x06006194 RID: 24980 RVA: 0x001463BA File Offset: 0x001445BA
		public int Length
		{
			get
			{
				return this._pos;
			}
		}

		// Token: 0x1700115A RID: 4442
		public T this[int index]
		{
			get
			{
				return this._span[index];
			}
		}

		// Token: 0x06006196 RID: 24982 RVA: 0x001463D0 File Offset: 0x001445D0
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

		// Token: 0x06006197 RID: 24983 RVA: 0x00146413 File Offset: 0x00144613
		public ReadOnlySpan<T> AsSpan()
		{
			return this._span.Slice(0, this._pos);
		}

		// Token: 0x06006198 RID: 24984 RVA: 0x0014642C File Offset: 0x0014462C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			if (this._arrayFromPool != null)
			{
				ArrayPool<T>.Shared.Return(this._arrayFromPool, false);
				this._arrayFromPool = null;
			}
		}

		// Token: 0x06006199 RID: 24985 RVA: 0x00146450 File Offset: 0x00144650
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

		// Token: 0x040039F2 RID: 14834
		private Span<T> _span;

		// Token: 0x040039F3 RID: 14835
		private T[] _arrayFromPool;

		// Token: 0x040039F4 RID: 14836
		private int _pos;
	}
}
