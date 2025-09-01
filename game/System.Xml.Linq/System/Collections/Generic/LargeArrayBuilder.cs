using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Collections.Generic
{
	// Token: 0x02000065 RID: 101
	internal struct LargeArrayBuilder<T>
	{
		// Token: 0x060003CD RID: 973 RVA: 0x00010B2C File Offset: 0x0000ED2C
		public LargeArrayBuilder(bool initialize)
		{
			this = new LargeArrayBuilder<T>(int.MaxValue);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00010B3C File Offset: 0x0000ED3C
		public LargeArrayBuilder(int maxCapacity)
		{
			this = default(LargeArrayBuilder<T>);
			this._first = (this._current = Array.Empty<T>());
			this._maxCapacity = maxCapacity;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00010B6B File Offset: 0x0000ED6B
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00010B74 File Offset: 0x0000ED74
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

		// Token: 0x060003D1 RID: 977 RVA: 0x00010BC0 File Offset: 0x0000EDC0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddWithBufferAllocation(T item)
		{
			this.AllocateBuffer();
			T[] current = this._current;
			int index = this._index;
			this._index = index + 1;
			current[index] = item;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00010BF0 File Offset: 0x0000EDF0
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

		// Token: 0x060003D3 RID: 979 RVA: 0x00010C7C File Offset: 0x0000EE7C
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

		// Token: 0x060003D4 RID: 980 RVA: 0x00010CCC File Offset: 0x0000EECC
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

		// Token: 0x060003D5 RID: 981 RVA: 0x00010D0C File Offset: 0x0000EF0C
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

		// Token: 0x060003D6 RID: 982 RVA: 0x00010DA8 File Offset: 0x0000EFA8
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

		// Token: 0x060003D7 RID: 983 RVA: 0x00010DD7 File Offset: 0x0000EFD7
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SlowAdd(T item)
		{
			this.Add(item);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00010DE0 File Offset: 0x0000EFE0
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

		// Token: 0x060003D9 RID: 985 RVA: 0x00010E14 File Offset: 0x0000F014
		public bool TryMove(out T[] array)
		{
			array = this._first;
			return this._count == this._first.Length;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00010E30 File Offset: 0x0000F030
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

		// Token: 0x060003DB RID: 987 RVA: 0x00010EE4 File Offset: 0x0000F0E4
		[CompilerGenerated]
		internal static int <CopyTo>g__CopyToCore|17_0(T[] sourceBuffer, int sourceIndex, ref LargeArrayBuilder<T>.<>c__DisplayClass17_0 A_2)
		{
			int num = Math.Min(sourceBuffer.Length - sourceIndex, A_2.count);
			Array.Copy(sourceBuffer, sourceIndex, A_2.array, A_2.arrayIndex, num);
			A_2.arrayIndex += num;
			A_2.count -= num;
			return num;
		}

		// Token: 0x040001E9 RID: 489
		private const int StartingCapacity = 4;

		// Token: 0x040001EA RID: 490
		private const int ResizeLimit = 8;

		// Token: 0x040001EB RID: 491
		private readonly int _maxCapacity;

		// Token: 0x040001EC RID: 492
		private T[] _first;

		// Token: 0x040001ED RID: 493
		private ArrayBuilder<T[]> _buffers;

		// Token: 0x040001EE RID: 494
		private T[] _current;

		// Token: 0x040001EF RID: 495
		private int _index;

		// Token: 0x040001F0 RID: 496
		private int _count;

		// Token: 0x02000066 RID: 102
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass17_0
		{
			// Token: 0x040001F1 RID: 497
			public int count;

			// Token: 0x040001F2 RID: 498
			public T[] array;

			// Token: 0x040001F3 RID: 499
			public int arrayIndex;
		}
	}
}
