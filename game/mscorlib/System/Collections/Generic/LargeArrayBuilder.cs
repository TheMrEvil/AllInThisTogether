using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Collections.Generic
{
	// Token: 0x02000AA9 RID: 2729
	internal struct LargeArrayBuilder<T>
	{
		// Token: 0x060061AF RID: 25007 RVA: 0x00146858 File Offset: 0x00144A58
		public LargeArrayBuilder(bool initialize)
		{
			this = new LargeArrayBuilder<T>(int.MaxValue);
		}

		// Token: 0x060061B0 RID: 25008 RVA: 0x00146868 File Offset: 0x00144A68
		public LargeArrayBuilder(int maxCapacity)
		{
			this = default(LargeArrayBuilder<T>);
			this._first = (this._current = Array.Empty<T>());
			this._maxCapacity = maxCapacity;
		}

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x060061B1 RID: 25009 RVA: 0x00146897 File Offset: 0x00144A97
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x060061B2 RID: 25010 RVA: 0x001468A0 File Offset: 0x00144AA0
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

		// Token: 0x060061B3 RID: 25011 RVA: 0x001468EC File Offset: 0x00144AEC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddWithBufferAllocation(T item)
		{
			this.AllocateBuffer();
			T[] current = this._current;
			int index = this._index;
			this._index = index + 1;
			current[index] = item;
		}

		// Token: 0x060061B4 RID: 25012 RVA: 0x0014691C File Offset: 0x00144B1C
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

		// Token: 0x060061B5 RID: 25013 RVA: 0x001469A8 File Offset: 0x00144BA8
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

		// Token: 0x060061B6 RID: 25014 RVA: 0x001469F8 File Offset: 0x00144BF8
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

		// Token: 0x060061B7 RID: 25015 RVA: 0x00146A38 File Offset: 0x00144C38
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

		// Token: 0x060061B8 RID: 25016 RVA: 0x00146AD4 File Offset: 0x00144CD4
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

		// Token: 0x060061B9 RID: 25017 RVA: 0x00146B03 File Offset: 0x00144D03
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SlowAdd(T item)
		{
			this.Add(item);
		}

		// Token: 0x060061BA RID: 25018 RVA: 0x00146B0C File Offset: 0x00144D0C
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

		// Token: 0x060061BB RID: 25019 RVA: 0x00146B40 File Offset: 0x00144D40
		public bool TryMove(out T[] array)
		{
			array = this._first;
			return this._count == this._first.Length;
		}

		// Token: 0x060061BC RID: 25020 RVA: 0x00146B5C File Offset: 0x00144D5C
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

		// Token: 0x060061BD RID: 25021 RVA: 0x00146C10 File Offset: 0x00144E10
		[CompilerGenerated]
		internal static int <CopyTo>g__CopyToCore|17_0(T[] sourceBuffer, int sourceIndex, ref LargeArrayBuilder<T>.<>c__DisplayClass17_0 A_2)
		{
			int num = Math.Min(sourceBuffer.Length - sourceIndex, A_2.count);
			Array.Copy(sourceBuffer, sourceIndex, A_2.array, A_2.arrayIndex, num);
			A_2.arrayIndex += num;
			A_2.count -= num;
			return num;
		}

		// Token: 0x040039FB RID: 14843
		private const int StartingCapacity = 4;

		// Token: 0x040039FC RID: 14844
		private const int ResizeLimit = 8;

		// Token: 0x040039FD RID: 14845
		private readonly int _maxCapacity;

		// Token: 0x040039FE RID: 14846
		private T[] _first;

		// Token: 0x040039FF RID: 14847
		private ArrayBuilder<T[]> _buffers;

		// Token: 0x04003A00 RID: 14848
		private T[] _current;

		// Token: 0x04003A01 RID: 14849
		private int _index;

		// Token: 0x04003A02 RID: 14850
		private int _count;

		// Token: 0x02000AAA RID: 2730
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass17_0
		{
			// Token: 0x04003A03 RID: 14851
			public int count;

			// Token: 0x04003A04 RID: 14852
			public T[] array;

			// Token: 0x04003A05 RID: 14853
			public int arrayIndex;
		}
	}
}
