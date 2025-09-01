using System;
using System.Diagnostics;

namespace TMPro
{
	// Token: 0x0200006D RID: 109
	[DebuggerDisplay("Item count = {m_Count}")]
	public struct TMP_TextProcessingStack<T>
	{
		// Token: 0x06000588 RID: 1416 RVA: 0x0003600E File Offset: 0x0003420E
		public TMP_TextProcessingStack(T[] stack)
		{
			this.itemStack = stack;
			this.m_Capacity = stack.Length;
			this.index = 0;
			this.m_RolloverSize = 0;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00036041 File Offset: 0x00034241
		public TMP_TextProcessingStack(int capacity)
		{
			this.itemStack = new T[capacity];
			this.m_Capacity = capacity;
			this.index = 0;
			this.m_RolloverSize = 0;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00036077 File Offset: 0x00034277
		public TMP_TextProcessingStack(int capacity, int rolloverSize)
		{
			this.itemStack = new T[capacity];
			this.m_Capacity = capacity;
			this.index = 0;
			this.m_RolloverSize = rolloverSize;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x000360AD File Offset: 0x000342AD
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x000360B5 File Offset: 0x000342B5
		public T current
		{
			get
			{
				if (this.index > 0)
				{
					return this.itemStack[this.index - 1];
				}
				return this.itemStack[0];
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x000360E0 File Offset: 0x000342E0
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x000360E8 File Offset: 0x000342E8
		public int rolloverSize
		{
			get
			{
				return this.m_RolloverSize;
			}
			set
			{
				this.m_RolloverSize = value;
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000360F4 File Offset: 0x000342F4
		internal static void SetDefault(TMP_TextProcessingStack<T>[] stack, T item)
		{
			for (int i = 0; i < stack.Length; i++)
			{
				stack[i].SetDefault(item);
			}
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0003611C File Offset: 0x0003431C
		public void Clear()
		{
			this.index = 0;
			this.m_Count = 0;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0003612C File Offset: 0x0003432C
		public void SetDefault(T item)
		{
			if (this.itemStack == null)
			{
				this.m_Capacity = 4;
				this.itemStack = new T[this.m_Capacity];
				this.m_DefaultItem = default(T);
			}
			this.itemStack[0] = item;
			this.index = 1;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00036179 File Offset: 0x00034379
		public void Add(T item)
		{
			if (this.index < this.itemStack.Length)
			{
				this.itemStack[this.index] = item;
				this.index++;
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000361AB File Offset: 0x000343AB
		public T Remove()
		{
			this.index--;
			if (this.index <= 0)
			{
				this.index = 1;
				return this.itemStack[0];
			}
			return this.itemStack[this.index - 1];
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000361EC File Offset: 0x000343EC
		public void Push(T item)
		{
			if (this.index == this.m_Capacity)
			{
				this.m_Capacity *= 2;
				if (this.m_Capacity == 0)
				{
					this.m_Capacity = 4;
				}
				Array.Resize<T>(ref this.itemStack, this.m_Capacity);
			}
			this.itemStack[this.index] = item;
			if (this.m_RolloverSize == 0)
			{
				this.index++;
				this.m_Count++;
				return;
			}
			this.index = (this.index + 1) % this.m_RolloverSize;
			this.m_Count = ((this.m_Count < this.m_RolloverSize) ? (this.m_Count + 1) : this.m_RolloverSize);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x000362A8 File Offset: 0x000344A8
		public T Pop()
		{
			if (this.index == 0 && this.m_RolloverSize == 0)
			{
				return default(T);
			}
			if (this.m_RolloverSize == 0)
			{
				this.index--;
			}
			else
			{
				this.index = (this.index - 1) % this.m_RolloverSize;
				this.index = ((this.index < 0) ? (this.index + this.m_RolloverSize) : this.index);
			}
			T result = this.itemStack[this.index];
			this.itemStack[this.index] = this.m_DefaultItem;
			this.m_Count = ((this.m_Count > 0) ? (this.m_Count - 1) : 0);
			return result;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00036362 File Offset: 0x00034562
		public T Peek()
		{
			if (this.index == 0)
			{
				return this.m_DefaultItem;
			}
			return this.itemStack[this.index - 1];
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00036386 File Offset: 0x00034586
		public T CurrentItem()
		{
			if (this.index > 0)
			{
				return this.itemStack[this.index - 1];
			}
			return this.itemStack[0];
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000363B1 File Offset: 0x000345B1
		public T PreviousItem()
		{
			if (this.index > 1)
			{
				return this.itemStack[this.index - 2];
			}
			return this.itemStack[0];
		}

		// Token: 0x0400054A RID: 1354
		public T[] itemStack;

		// Token: 0x0400054B RID: 1355
		public int index;

		// Token: 0x0400054C RID: 1356
		private T m_DefaultItem;

		// Token: 0x0400054D RID: 1357
		private int m_Capacity;

		// Token: 0x0400054E RID: 1358
		private int m_RolloverSize;

		// Token: 0x0400054F RID: 1359
		private int m_Count;

		// Token: 0x04000550 RID: 1360
		private const int k_DefaultCapacity = 4;
	}
}
