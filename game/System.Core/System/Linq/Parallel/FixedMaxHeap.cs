using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001ED RID: 493
	internal class FixedMaxHeap<TElement>
	{
		// Token: 0x06000C18 RID: 3096 RVA: 0x0002A71D File Offset: 0x0002891D
		internal FixedMaxHeap(int maximumSize) : this(maximumSize, Util.GetDefaultComparer<TElement>())
		{
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002A72B File Offset: 0x0002892B
		internal FixedMaxHeap(int maximumSize, IComparer<TElement> comparer)
		{
			this._elements = new TElement[maximumSize];
			this._comparer = comparer;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0002A746 File Offset: 0x00028946
		internal int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0002A74E File Offset: 0x0002894E
		internal int Size
		{
			get
			{
				return this._elements.Length;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0002A758 File Offset: 0x00028958
		internal TElement MaxValue
		{
			get
			{
				if (this._count == 0)
				{
					throw new InvalidOperationException("Sequence contains no elements");
				}
				return this._elements[0];
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0002A779 File Offset: 0x00028979
		internal void Clear()
		{
			this._count = 0;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0002A784 File Offset: 0x00028984
		internal bool Insert(TElement e)
		{
			if (this._count < this._elements.Length)
			{
				this._elements[this._count] = e;
				this._count++;
				this.HeapifyLastLeaf();
				return true;
			}
			if (this._comparer.Compare(e, this._elements[0]) < 0)
			{
				this._elements[0] = e;
				this.HeapifyRoot();
				return true;
			}
			return false;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0002A7FA File Offset: 0x000289FA
		internal void ReplaceMax(TElement newValue)
		{
			this._elements[0] = newValue;
			this.HeapifyRoot();
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002A80F File Offset: 0x00028A0F
		internal void RemoveMax()
		{
			this._count--;
			if (this._count > 0)
			{
				this._elements[0] = this._elements[this._count];
				this.HeapifyRoot();
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0002A84C File Offset: 0x00028A4C
		private void Swap(int i, int j)
		{
			TElement telement = this._elements[i];
			this._elements[i] = this._elements[j];
			this._elements[j] = telement;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0002A88C File Offset: 0x00028A8C
		private void HeapifyRoot()
		{
			int i = 0;
			int count = this._count;
			while (i < count)
			{
				int num = (i + 1) * 2 - 1;
				int num2 = num + 1;
				if (num < count && this._comparer.Compare(this._elements[i], this._elements[num]) < 0)
				{
					if (num2 < count && this._comparer.Compare(this._elements[num], this._elements[num2]) < 0)
					{
						this.Swap(i, num2);
						i = num2;
					}
					else
					{
						this.Swap(i, num);
						i = num;
					}
				}
				else
				{
					if (num2 >= count || this._comparer.Compare(this._elements[i], this._elements[num2]) >= 0)
					{
						break;
					}
					this.Swap(i, num2);
					i = num2;
				}
			}
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0002A95C File Offset: 0x00028B5C
		private void HeapifyLastLeaf()
		{
			int num;
			for (int i = this._count - 1; i > 0; i = num)
			{
				num = (i + 1) / 2 - 1;
				if (this._comparer.Compare(this._elements[i], this._elements[num]) <= 0)
				{
					break;
				}
				this.Swap(i, num);
			}
		}

		// Token: 0x04000892 RID: 2194
		private TElement[] _elements;

		// Token: 0x04000893 RID: 2195
		private int _count;

		// Token: 0x04000894 RID: 2196
		private IComparer<TElement> _comparer;
	}
}
