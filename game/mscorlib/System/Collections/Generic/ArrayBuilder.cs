using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AA6 RID: 2726
	internal struct ArrayBuilder<T>
	{
		// Token: 0x0600619A RID: 24986 RVA: 0x001464B2 File Offset: 0x001446B2
		public ArrayBuilder(int capacity)
		{
			this = default(ArrayBuilder<T>);
			if (capacity > 0)
			{
				this._array = new T[capacity];
			}
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x0600619B RID: 24987 RVA: 0x001464CB File Offset: 0x001446CB
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

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x0600619C RID: 24988 RVA: 0x001464DB File Offset: 0x001446DB
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x1700115D RID: 4445
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

		// Token: 0x0600619F RID: 24991 RVA: 0x00146500 File Offset: 0x00144700
		public void Add(T item)
		{
			if (this._count == this.Capacity)
			{
				this.EnsureCapacity(this._count + 1);
			}
			this.UncheckedAdd(item);
		}

		// Token: 0x060061A0 RID: 24992 RVA: 0x00146525 File Offset: 0x00144725
		public T First()
		{
			return this._array[0];
		}

		// Token: 0x060061A1 RID: 24993 RVA: 0x00146533 File Offset: 0x00144733
		public T Last()
		{
			return this._array[this._count - 1];
		}

		// Token: 0x060061A2 RID: 24994 RVA: 0x00146548 File Offset: 0x00144748
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

		// Token: 0x060061A3 RID: 24995 RVA: 0x00146598 File Offset: 0x00144798
		public void UncheckedAdd(T item)
		{
			T[] array = this._array;
			int count = this._count;
			this._count = count + 1;
			array[count] = item;
		}

		// Token: 0x060061A4 RID: 24996 RVA: 0x001465C4 File Offset: 0x001447C4
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

		// Token: 0x040039F5 RID: 14837
		private const int DefaultCapacity = 4;

		// Token: 0x040039F6 RID: 14838
		private const int MaxCoreClrArrayLength = 2146435071;

		// Token: 0x040039F7 RID: 14839
		private T[] _array;

		// Token: 0x040039F8 RID: 14840
		private int _count;
	}
}
