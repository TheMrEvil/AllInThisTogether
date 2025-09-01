using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x0200004C RID: 76
	public class DynamicArray<T> where T : new()
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000DA4E File Offset: 0x0000BC4E
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000DA56 File Offset: 0x0000BC56
		public int size
		{
			[CompilerGenerated]
			get
			{
				return this.<size>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<size>k__BackingField = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000DA5F File Offset: 0x0000BC5F
		public int capacity
		{
			get
			{
				return this.m_Array.Length;
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000DA69 File Offset: 0x0000BC69
		public DynamicArray()
		{
			this.m_Array = new T[32];
			this.size = 0;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000DA85 File Offset: 0x0000BC85
		public DynamicArray(int size)
		{
			this.m_Array = new T[size];
			this.size = size;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000DAA0 File Offset: 0x0000BCA0
		public void Clear()
		{
			this.size = 0;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000DAA9 File Offset: 0x0000BCA9
		public bool Contains(T item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000DAB8 File Offset: 0x0000BCB8
		public int Add(in T value)
		{
			int size = this.size;
			if (size >= this.m_Array.Length)
			{
				T[] array = new T[this.m_Array.Length * 2];
				Array.Copy(this.m_Array, array, this.m_Array.Length);
				this.m_Array = array;
			}
			this.m_Array[size] = value;
			int size2 = this.size;
			this.size = size2 + 1;
			return size;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000DB28 File Offset: 0x0000BD28
		public unsafe void AddRange(DynamicArray<T> array)
		{
			this.Reserve(this.size + array.size, true);
			for (int i = 0; i < array.size; i++)
			{
				T[] array2 = this.m_Array;
				int size = this.size;
				this.size = size + 1;
				array2[size] = *array[i];
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000DB84 File Offset: 0x0000BD84
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num != -1)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.size)
			{
				throw new IndexOutOfRangeException();
			}
			if (index != this.size - 1)
			{
				Array.Copy(this.m_Array, index + 1, this.m_Array, index, this.size - index - 1);
			}
			int size = this.size;
			this.size = size - 1;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000DC04 File Offset: 0x0000BE04
		public void RemoveRange(int index, int count)
		{
			if (index < 0 || index >= this.size || count < 0 || index + count > this.size)
			{
				throw new ArgumentOutOfRangeException();
			}
			Array.Copy(this.m_Array, index + count, this.m_Array, index, this.size - index - count);
			this.size -= count;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000DC60 File Offset: 0x0000BE60
		public int FindIndex(int startIndex, int count, Predicate<T> match)
		{
			for (int i = startIndex; i < this.size; i++)
			{
				if (match(this.m_Array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000DC98 File Offset: 0x0000BE98
		public int IndexOf(T item, int index, int count)
		{
			int num = index;
			while (num < this.size && count > 0)
			{
				if (this.m_Array[num].Equals(item))
				{
					return num;
				}
				num++;
				count--;
			}
			return -1;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000DCE4 File Offset: 0x0000BEE4
		public int IndexOf(T item, int index)
		{
			for (int i = index; i < this.size; i++)
			{
				if (this.m_Array[i].Equals(item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000DD26 File Offset: 0x0000BF26
		public int IndexOf(T item)
		{
			return this.IndexOf(item, 0);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000DD30 File Offset: 0x0000BF30
		public void Resize(int newSize, bool keepContent = false)
		{
			this.Reserve(newSize, keepContent);
			this.size = newSize;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000DD44 File Offset: 0x0000BF44
		public void Reserve(int newCapacity, bool keepContent = false)
		{
			if (newCapacity > this.m_Array.Length)
			{
				if (keepContent)
				{
					T[] array = new T[newCapacity];
					Array.Copy(this.m_Array, array, this.m_Array.Length);
					this.m_Array = array;
					return;
				}
				this.m_Array = new T[newCapacity];
			}
		}

		// Token: 0x17000042 RID: 66
		public T this[int index]
		{
			get
			{
				return ref this.m_Array[index];
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000DD9C File Offset: 0x0000BF9C
		public static implicit operator T[](DynamicArray<T> array)
		{
			return array.m_Array;
		}

		// Token: 0x040001B7 RID: 439
		private T[] m_Array;

		// Token: 0x040001B8 RID: 440
		[CompilerGenerated]
		private int <size>k__BackingField;
	}
}
