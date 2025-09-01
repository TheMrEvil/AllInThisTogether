using System;
using System.Diagnostics;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200003B RID: 59
	[DebuggerDisplay("Item count = {m_Count}")]
	internal struct TextProcessingStack<T>
	{
		// Token: 0x06000164 RID: 356 RVA: 0x0001A8D3 File Offset: 0x00018AD3
		public TextProcessingStack(T[] stack)
		{
			this.itemStack = stack;
			this.m_Capacity = stack.Length;
			this.index = 0;
			this.m_RolloverSize = 0;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0001A907 File Offset: 0x00018B07
		public TextProcessingStack(int capacity)
		{
			this.itemStack = new T[capacity];
			this.m_Capacity = capacity;
			this.index = 0;
			this.m_RolloverSize = 0;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0001A93E File Offset: 0x00018B3E
		public TextProcessingStack(int capacity, int rolloverSize)
		{
			this.itemStack = new T[capacity];
			this.m_Capacity = capacity;
			this.index = 0;
			this.m_RolloverSize = rolloverSize;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0001A978 File Offset: 0x00018B78
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0001A990 File Offset: 0x00018B90
		public T current
		{
			get
			{
				bool flag = this.index > 0;
				T result;
				if (flag)
				{
					result = this.itemStack[this.index - 1];
				}
				else
				{
					result = this.itemStack[0];
				}
				return result;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0001A9D4 File Offset: 0x00018BD4
		// (set) Token: 0x0600016A RID: 362 RVA: 0x0001A9EC File Offset: 0x00018BEC
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

		// Token: 0x0600016B RID: 363 RVA: 0x0001A9F8 File Offset: 0x00018BF8
		internal static void SetDefault(TextProcessingStack<T>[] stack, T item)
		{
			for (int i = 0; i < stack.Length; i++)
			{
				stack[i].SetDefault(item);
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0001AA26 File Offset: 0x00018C26
		public void Clear()
		{
			this.index = 0;
			this.m_Count = 0;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0001AA38 File Offset: 0x00018C38
		public void SetDefault(T item)
		{
			bool flag = this.itemStack == null;
			if (flag)
			{
				this.m_Capacity = 4;
				this.itemStack = new T[this.m_Capacity];
				this.m_DefaultItem = default(T);
			}
			this.itemStack[0] = item;
			this.index = 1;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0001AA90 File Offset: 0x00018C90
		public void Add(T item)
		{
			bool flag = this.index < this.itemStack.Length;
			if (flag)
			{
				this.itemStack[this.index] = item;
				this.index++;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0001AAD4 File Offset: 0x00018CD4
		public T Remove()
		{
			this.index--;
			bool flag = this.index <= 0;
			T result;
			if (flag)
			{
				this.index = 1;
				result = this.itemStack[0];
			}
			else
			{
				result = this.itemStack[this.index - 1];
			}
			return result;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0001AB30 File Offset: 0x00018D30
		public void Push(T item)
		{
			bool flag = this.index == this.m_Capacity;
			if (flag)
			{
				this.m_Capacity *= 2;
				bool flag2 = this.m_Capacity == 0;
				if (flag2)
				{
					this.m_Capacity = 4;
				}
				Array.Resize<T>(ref this.itemStack, this.m_Capacity);
			}
			this.itemStack[this.index] = item;
			bool flag3 = this.m_RolloverSize == 0;
			if (flag3)
			{
				this.index++;
				this.m_Count++;
			}
			else
			{
				this.index = (this.index + 1) % this.m_RolloverSize;
				this.m_Count = ((this.m_Count < this.m_RolloverSize) ? (this.m_Count + 1) : this.m_RolloverSize);
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0001AC00 File Offset: 0x00018E00
		public T Pop()
		{
			bool flag = this.index == 0 && this.m_RolloverSize == 0;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				bool flag2 = this.m_RolloverSize == 0;
				if (flag2)
				{
					this.index--;
				}
				else
				{
					this.index = (this.index - 1) % this.m_RolloverSize;
					this.index = ((this.index < 0) ? (this.index + this.m_RolloverSize) : this.index);
				}
				T t = this.itemStack[this.index];
				this.itemStack[this.index] = this.m_DefaultItem;
				this.m_Count = ((this.m_Count > 0) ? (this.m_Count - 1) : 0);
				result = t;
			}
			return result;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0001ACD8 File Offset: 0x00018ED8
		public T Peek()
		{
			bool flag = this.index == 0;
			T result;
			if (flag)
			{
				result = this.m_DefaultItem;
			}
			else
			{
				result = this.itemStack[this.index - 1];
			}
			return result;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0001AD14 File Offset: 0x00018F14
		public T CurrentItem()
		{
			bool flag = this.index > 0;
			T result;
			if (flag)
			{
				result = this.itemStack[this.index - 1];
			}
			else
			{
				result = this.itemStack[0];
			}
			return result;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0001AD58 File Offset: 0x00018F58
		public T PreviousItem()
		{
			bool flag = this.index > 1;
			T result;
			if (flag)
			{
				result = this.itemStack[this.index - 2];
			}
			else
			{
				result = this.itemStack[0];
			}
			return result;
		}

		// Token: 0x04000318 RID: 792
		public T[] itemStack;

		// Token: 0x04000319 RID: 793
		public int index;

		// Token: 0x0400031A RID: 794
		private T m_DefaultItem;

		// Token: 0x0400031B RID: 795
		private int m_Capacity;

		// Token: 0x0400031C RID: 796
		private int m_RolloverSize;

		// Token: 0x0400031D RID: 797
		private int m_Count;

		// Token: 0x0400031E RID: 798
		private const int k_DefaultCapacity = 4;
	}
}
