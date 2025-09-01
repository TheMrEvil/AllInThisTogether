using System;

namespace System.Collections.Generic
{
	// Token: 0x02000062 RID: 98
	internal struct ArrayBuilder<T>
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x00010784 File Offset: 0x0000E984
		public ArrayBuilder(int capacity)
		{
			this = default(ArrayBuilder<T>);
			if (capacity > 0)
			{
				this._array = new T[capacity];
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0001079D File Offset: 0x0000E99D
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

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003BA RID: 954 RVA: 0x000107AD File Offset: 0x0000E9AD
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x1700008B RID: 139
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

		// Token: 0x060003BD RID: 957 RVA: 0x000107D2 File Offset: 0x0000E9D2
		public void Add(T item)
		{
			if (this._count == this.Capacity)
			{
				this.EnsureCapacity(this._count + 1);
			}
			this.UncheckedAdd(item);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000107F7 File Offset: 0x0000E9F7
		public T First()
		{
			return this._array[0];
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00010805 File Offset: 0x0000EA05
		public T Last()
		{
			return this._array[this._count - 1];
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001081C File Offset: 0x0000EA1C
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

		// Token: 0x060003C1 RID: 961 RVA: 0x0001086C File Offset: 0x0000EA6C
		public void UncheckedAdd(T item)
		{
			T[] array = this._array;
			int count = this._count;
			this._count = count + 1;
			array[count] = item;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00010898 File Offset: 0x0000EA98
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

		// Token: 0x040001E3 RID: 483
		private const int DefaultCapacity = 4;

		// Token: 0x040001E4 RID: 484
		private const int MaxCoreClrArrayLength = 2146435071;

		// Token: 0x040001E5 RID: 485
		private T[] _array;

		// Token: 0x040001E6 RID: 486
		private int _count;
	}
}
