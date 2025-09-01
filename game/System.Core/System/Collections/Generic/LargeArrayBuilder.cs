using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Collections.Generic
{
	// Token: 0x02000356 RID: 854
	internal struct LargeArrayBuilder<T>
	{
		// Token: 0x06001A00 RID: 6656 RVA: 0x00056FB0 File Offset: 0x000551B0
		public LargeArrayBuilder(bool initialize)
		{
			this = new LargeArrayBuilder<T>(int.MaxValue);
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x00056FC0 File Offset: 0x000551C0
		public LargeArrayBuilder(int maxCapacity)
		{
			this = default(LargeArrayBuilder<T>);
			this._first = (this._current = Array.Empty<T>());
			this._maxCapacity = maxCapacity;
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x00056FEF File Offset: 0x000551EF
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00056FF8 File Offset: 0x000551F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(T item)
		{
			int index = this._index;
			T[] current = this._current;
			if (index >= current.Length)
			{
				this.AddWithBufferAllocation(item);
			}
			else
			{
				current[index] = item;
				this._index = index + 1;
			}
			this._count++;
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x00057044 File Offset: 0x00055244
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddWithBufferAllocation(T item)
		{
			this.AllocateBuffer();
			T[] current = this._current;
			int index = this._index;
			this._index = index + 1;
			current[index] = item;
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x00057074 File Offset: 0x00055274
		public void AddRange(IEnumerable<T> items)
		{
			using (IEnumerator<T> enumerator = items.GetEnumerator())
			{
				T[] current = this._current;
				int num = this._index;
				while (enumerator.MoveNext())
				{
					T t = enumerator.Current;
					if (num >= current.Length)
					{
						this.AddWithBufferAllocation(t, ref current, ref num);
					}
					else
					{
						current[num] = t;
					}
					num++;
				}
				this._count += num - this._index;
				this._index = num;
			}
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00057100 File Offset: 0x00055300
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddWithBufferAllocation(T item, ref T[] destination, ref int index)
		{
			this._count += index - this._index;
			this._index = index;
			this.AllocateBuffer();
			destination = this._current;
			index = this._index;
			this._current[index] = item;
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x00057150 File Offset: 0x00055350
		public void CopyTo(T[] array, int arrayIndex, int count)
		{
			int num = 0;
			while (count > 0)
			{
				T[] buffer = this.GetBuffer(num);
				int num2 = Math.Min(count, buffer.Length);
				Array.Copy(buffer, 0, array, arrayIndex, num2);
				count -= num2;
				arrayIndex += num2;
				num++;
			}
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x00057190 File Offset: 0x00055390
		public CopyPosition CopyTo(CopyPosition position, T[] array, int arrayIndex, int count)
		{
			LargeArrayBuilder<T>.<>c__DisplayClass17_0 CS$<>8__locals1;
			CS$<>8__locals1.count = count;
			CS$<>8__locals1.array = array;
			CS$<>8__locals1.arrayIndex = arrayIndex;
			int num = position.Row;
			int column = position.Column;
			T[] buffer = this.GetBuffer(num);
			int num2 = LargeArrayBuilder<T>.<CopyTo>g__CopyToCore|17_0(buffer, column, ref CS$<>8__locals1);
			if (CS$<>8__locals1.count == 0)
			{
				return new CopyPosition(num, column + num2).Normalize(buffer.Length);
			}
			do
			{
				buffer = this.GetBuffer(++num);
				num2 = LargeArrayBuilder<T>.<CopyTo>g__CopyToCore|17_0(buffer, 0, ref CS$<>8__locals1);
			}
			while (CS$<>8__locals1.count > 0);
			return new CopyPosition(num, num2).Normalize(buffer.Length);
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x0005722C File Offset: 0x0005542C
		public T[] GetBuffer(int index)
		{
			if (index == 0)
			{
				return this._first;
			}
			if (index > this._buffers.Count)
			{
				return this._current;
			}
			return this._buffers[index - 1];
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0005725B File Offset: 0x0005545B
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SlowAdd(T item)
		{
			this.Add(item);
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x00057264 File Offset: 0x00055464
		public T[] ToArray()
		{
			T[] array;
			if (this.TryMove(out array))
			{
				return array;
			}
			array = new T[this._count];
			this.CopyTo(array, 0, this._count);
			return array;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00057298 File Offset: 0x00055498
		public bool TryMove(out T[] array)
		{
			array = this._first;
			return this._count == this._first.Length;
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x000572B4 File Offset: 0x000554B4
		private void AllocateBuffer()
		{
			if (this._count < 8)
			{
				int num = Math.Min((this._count == 0) ? 4 : (this._count * 2), this._maxCapacity);
				this._current = new T[num];
				Array.Copy(this._first, 0, this._current, 0, this._count);
				this._first = this._current;
				return;
			}
			int num2;
			if (this._count == 8)
			{
				num2 = 8;
			}
			else
			{
				this._buffers.Add(this._current);
				num2 = Math.Min(this._count, this._maxCapacity - this._count);
			}
			this._current = new T[num2];
			this._index = 0;
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x00057368 File Offset: 0x00055568
		[CompilerGenerated]
		internal static int <CopyTo>g__CopyToCore|17_0(T[] sourceBuffer, int sourceIndex, ref LargeArrayBuilder<T>.<>c__DisplayClass17_0 A_2)
		{
			int num = Math.Min(sourceBuffer.Length - sourceIndex, A_2.count);
			Array.Copy(sourceBuffer, sourceIndex, A_2.array, A_2.arrayIndex, num);
			A_2.arrayIndex += num;
			A_2.count -= num;
			return num;
		}

		// Token: 0x04000C74 RID: 3188
		private const int StartingCapacity = 4;

		// Token: 0x04000C75 RID: 3189
		private const int ResizeLimit = 8;

		// Token: 0x04000C76 RID: 3190
		private readonly int _maxCapacity;

		// Token: 0x04000C77 RID: 3191
		private T[] _first;

		// Token: 0x04000C78 RID: 3192
		private ArrayBuilder<T[]> _buffers;

		// Token: 0x04000C79 RID: 3193
		private T[] _current;

		// Token: 0x04000C7A RID: 3194
		private int _index;

		// Token: 0x04000C7B RID: 3195
		private int _count;

		// Token: 0x02000357 RID: 855
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass17_0
		{
			// Token: 0x04000C7C RID: 3196
			public int count;

			// Token: 0x04000C7D RID: 3197
			public T[] array;

			// Token: 0x04000C7E RID: 3198
			public int arrayIndex;
		}
	}
}
