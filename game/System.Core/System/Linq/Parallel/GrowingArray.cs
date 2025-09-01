using System;

namespace System.Linq.Parallel
{
	// Token: 0x020001EE RID: 494
	internal class GrowingArray<T>
	{
		// Token: 0x06000C24 RID: 3108 RVA: 0x0002A9B0 File Offset: 0x00028BB0
		internal GrowingArray()
		{
			this._array = new T[1024];
			this._count = 0;
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0002A9CF File Offset: 0x00028BCF
		internal T[] InternalArray
		{
			get
			{
				return this._array;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0002A9D7 File Offset: 0x00028BD7
		internal int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0002A9E0 File Offset: 0x00028BE0
		internal void Add(T element)
		{
			if (this._count >= this._array.Length)
			{
				this.GrowArray(2 * this._array.Length);
			}
			T[] array = this._array;
			int count = this._count;
			this._count = count + 1;
			array[count] = element;
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0002AA2C File Offset: 0x00028C2C
		private void GrowArray(int newSize)
		{
			T[] array = new T[newSize];
			this._array.CopyTo(array, 0);
			this._array = array;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0002AA54 File Offset: 0x00028C54
		internal void CopyFrom(T[] otherArray, int otherCount)
		{
			if (this._count + otherCount > this._array.Length)
			{
				this.GrowArray(this._count + otherCount);
			}
			Array.Copy(otherArray, 0, this._array, this._count, otherCount);
			this._count += otherCount;
		}

		// Token: 0x04000895 RID: 2197
		private T[] _array;

		// Token: 0x04000896 RID: 2198
		private int _count;

		// Token: 0x04000897 RID: 2199
		private const int DEFAULT_ARRAY_SIZE = 1024;
	}
}
