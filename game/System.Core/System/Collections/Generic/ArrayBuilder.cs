using System;

namespace System.Collections.Generic
{
	// Token: 0x02000353 RID: 851
	internal struct ArrayBuilder<T>
	{
		// Token: 0x060019EA RID: 6634 RVA: 0x00056BC6 File Offset: 0x00054DC6
		public ArrayBuilder(int capacity)
		{
			this = default(ArrayBuilder<T>);
			if (capacity > 0)
			{
				this._array = new T[capacity];
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x00056BDF File Offset: 0x00054DDF
		public int Capacity
		{
			get
			{
				T[] array = this._array;
				if (array == null)
				{
					return 0;
				}
				return array.Length;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x00056BEF File Offset: 0x00054DEF
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17000471 RID: 1137
		public T this[int index]
		{
			get
			{
				return this._array[index];
			}
			set
			{
				this._array[index] = value;
			}
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x00056C14 File Offset: 0x00054E14
		public void Add(T item)
		{
			if (this._count == this.Capacity)
			{
				this.EnsureCapacity(this._count + 1);
			}
			this.UncheckedAdd(item);
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x00056C39 File Offset: 0x00054E39
		public T First()
		{
			return this._array[0];
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x00056C47 File Offset: 0x00054E47
		public T Last()
		{
			return this._array[this._count - 1];
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00056C5C File Offset: 0x00054E5C
		public T[] ToArray()
		{
			if (this._count == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = this._array;
			if (this._count < array.Length)
			{
				array = new T[this._count];
				Array.Copy(this._array, 0, array, 0, this._count);
			}
			return array;
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00056CAC File Offset: 0x00054EAC
		public void UncheckedAdd(T item)
		{
			T[] array = this._array;
			int count = this._count;
			this._count = count + 1;
			array[count] = item;
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x00056CD8 File Offset: 0x00054ED8
		private void EnsureCapacity(int minimum)
		{
			int capacity = this.Capacity;
			int num = (capacity == 0) ? 4 : (2 * capacity);
			if (num > 2146435071)
			{
				num = Math.Max(capacity + 1, 2146435071);
			}
			num = Math.Max(num, minimum);
			T[] array = new T[num];
			if (this._count > 0)
			{
				Array.Copy(this._array, 0, array, 0, this._count);
			}
			this._array = array;
		}

		// Token: 0x04000C6E RID: 3182
		private const int DefaultCapacity = 4;

		// Token: 0x04000C6F RID: 3183
		private const int MaxCoreClrArrayLength = 2146435071;

		// Token: 0x04000C70 RID: 3184
		private T[] _array;

		// Token: 0x04000C71 RID: 3185
		private int _count;
	}
}
