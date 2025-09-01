using System;

namespace System.Threading
{
	// Token: 0x020002B2 RID: 690
	internal class SparselyPopulatedArrayFragment<T> where T : class
	{
		// Token: 0x06001E49 RID: 7753 RVA: 0x00070426 File Offset: 0x0006E626
		internal SparselyPopulatedArrayFragment(int size) : this(size, null)
		{
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x00070430 File Offset: 0x0006E630
		internal SparselyPopulatedArrayFragment(int size, SparselyPopulatedArrayFragment<T> prev)
		{
			this._elements = new T[size];
			this._freeCount = size;
			this._prev = prev;
		}

		// Token: 0x17000393 RID: 915
		internal T this[int index]
		{
			get
			{
				return Volatile.Read<T>(ref this._elements[index]);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001E4C RID: 7756 RVA: 0x00070469 File Offset: 0x0006E669
		internal int Length
		{
			get
			{
				return this._elements.Length;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x00070473 File Offset: 0x0006E673
		internal SparselyPopulatedArrayFragment<T> Prev
		{
			get
			{
				return this._prev;
			}
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x00070480 File Offset: 0x0006E680
		internal T SafeAtomicRemove(int index, T expectedElement)
		{
			T t = Interlocked.CompareExchange<T>(ref this._elements[index], default(T), expectedElement);
			if (t != null)
			{
				this._freeCount++;
			}
			return t;
		}

		// Token: 0x04001A9A RID: 6810
		internal readonly T[] _elements;

		// Token: 0x04001A9B RID: 6811
		internal volatile int _freeCount;

		// Token: 0x04001A9C RID: 6812
		internal volatile SparselyPopulatedArrayFragment<T> _next;

		// Token: 0x04001A9D RID: 6813
		internal volatile SparselyPopulatedArrayFragment<T> _prev;
	}
}
