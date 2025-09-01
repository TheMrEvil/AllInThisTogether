using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000AB8 RID: 2744
	[DebuggerDisplay("Count = {Count}")]
	internal class LowLevelList<T>
	{
		// Token: 0x0600621C RID: 25116 RVA: 0x001480C8 File Offset: 0x001462C8
		public LowLevelList()
		{
			this._items = LowLevelList<T>.s_emptyArray;
		}

		// Token: 0x0600621D RID: 25117 RVA: 0x001480DB File Offset: 0x001462DB
		public LowLevelList(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			if (capacity == 0)
			{
				this._items = LowLevelList<T>.s_emptyArray;
				return;
			}
			this._items = new T[capacity];
		}

		// Token: 0x0600621E RID: 25118 RVA: 0x00148110 File Offset: 0x00146310
		public LowLevelList(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			ICollection<T> collection2 = collection as ICollection<!0>;
			if (collection2 == null)
			{
				this._size = 0;
				this._items = LowLevelList<T>.s_emptyArray;
				foreach (!0 item in collection)
				{
					this.Add(item);
				}
				return;
			}
			int count = collection2.Count;
			if (count == 0)
			{
				this._items = LowLevelList<T>.s_emptyArray;
				return;
			}
			this._items = new T[count];
			collection2.CopyTo(this._items, 0);
			this._size = count;
		}

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x0600621F RID: 25119 RVA: 0x001481C0 File Offset: 0x001463C0
		// (set) Token: 0x06006220 RID: 25120 RVA: 0x001481CC File Offset: 0x001463CC
		public int Capacity
		{
			get
			{
				return this._items.Length;
			}
			set
			{
				if (value < this._size)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						T[] array = new T[value];
						Array.Copy(this._items, 0, array, 0, this._size);
						this._items = array;
						return;
					}
					this._items = LowLevelList<T>.s_emptyArray;
				}
			}
		}

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x06006221 RID: 25121 RVA: 0x0014822A File Offset: 0x0014642A
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17001175 RID: 4469
		public T this[int index]
		{
			get
			{
				if (index >= this._size)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this._items[index];
			}
			set
			{
				if (index >= this._size)
				{
					throw new ArgumentOutOfRangeException();
				}
				this._items[index] = value;
				this._version++;
			}
		}

		// Token: 0x06006224 RID: 25124 RVA: 0x0014827C File Offset: 0x0014647C
		public void Add(T item)
		{
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			T[] items = this._items;
			int size = this._size;
			this._size = size + 1;
			items[size] = item;
			this._version++;
		}

		// Token: 0x06006225 RID: 25125 RVA: 0x001482D4 File Offset: 0x001464D4
		private void EnsureCapacity(int min)
		{
			if (this._items.Length < min)
			{
				int num = (this._items.Length == 0) ? 4 : (this._items.Length * 2);
				if (num < min)
				{
					num = min;
				}
				this.Capacity = num;
			}
		}

		// Token: 0x06006226 RID: 25126 RVA: 0x00148310 File Offset: 0x00146510
		public void AddRange(IEnumerable<T> collection)
		{
			this.InsertRange(this._size, collection);
		}

		// Token: 0x06006227 RID: 25127 RVA: 0x0014831F File Offset: 0x0014651F
		public void Clear()
		{
			if (this._size > 0)
			{
				Array.Clear(this._items, 0, this._size);
				this._size = 0;
			}
			this._version++;
		}

		// Token: 0x06006228 RID: 25128 RVA: 0x00148354 File Offset: 0x00146554
		public bool Contains(T item)
		{
			if (item == null)
			{
				for (int i = 0; i < this._size; i++)
				{
					if (this._items[i] == null)
					{
						return true;
					}
				}
				return false;
			}
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x06006229 RID: 25129 RVA: 0x0014839E File Offset: 0x0014659E
		public void CopyTo(int index, T[] array, int arrayIndex, int count)
		{
			if (this._size - index < count)
			{
				throw new ArgumentException();
			}
			Array.Copy(this._items, index, array, arrayIndex, count);
		}

		// Token: 0x0600622A RID: 25130 RVA: 0x001483C2 File Offset: 0x001465C2
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		// Token: 0x0600622B RID: 25131 RVA: 0x001483D8 File Offset: 0x001465D8
		public int IndexOf(T item)
		{
			return Array.IndexOf<T>(this._items, item, 0, this._size);
		}

		// Token: 0x0600622C RID: 25132 RVA: 0x001483ED File Offset: 0x001465ED
		public int IndexOf(T item, int index)
		{
			if (index > this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return Array.IndexOf<T>(this._items, item, index, this._size - index);
		}

		// Token: 0x0600622D RID: 25133 RVA: 0x00148418 File Offset: 0x00146618
		public void Insert(int index, T item)
		{
			if (index > this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + 1, this._size - index);
			}
			this._items[index] = item;
			this._size++;
			this._version++;
		}

		// Token: 0x0600622E RID: 25134 RVA: 0x001484A8 File Offset: 0x001466A8
		public void InsertRange(int index, IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (index > this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			ICollection<T> collection2 = collection as ICollection<!0>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				if (count > 0)
				{
					this.EnsureCapacity(this._size + count);
					if (index < this._size)
					{
						Array.Copy(this._items, index, this._items, index + count, this._size - index);
					}
					if (this == collection2)
					{
						Array.Copy(this._items, 0, this._items, index, index);
						Array.Copy(this._items, index + count, this._items, index * 2, this._size - index);
					}
					else
					{
						T[] array = new T[count];
						collection2.CopyTo(array, 0);
						Array.Copy(array, 0, this._items, index, count);
					}
					this._size += count;
				}
			}
			else
			{
				foreach (!0 item in collection)
				{
					this.Insert(index++, item);
				}
			}
			this._version++;
		}

		// Token: 0x0600622F RID: 25135 RVA: 0x001485DC File Offset: 0x001467DC
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x06006230 RID: 25136 RVA: 0x00148600 File Offset: 0x00146800
		public int RemoveAll(Predicate<T> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			int num = 0;
			while (num < this._size && !match(this._items[num]))
			{
				num++;
			}
			if (num >= this._size)
			{
				return 0;
			}
			int i = num + 1;
			while (i < this._size)
			{
				while (i < this._size && match(this._items[i]))
				{
					i++;
				}
				if (i < this._size)
				{
					this._items[num++] = this._items[i++];
				}
			}
			Array.Clear(this._items, num, this._size - num);
			int result = this._size - num;
			this._size = num;
			this._version++;
			return result;
		}

		// Token: 0x06006231 RID: 25137 RVA: 0x001486D8 File Offset: 0x001468D8
		public void RemoveAt(int index)
		{
			if (index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._size - index);
			}
			this._items[this._size] = default(T);
			this._version++;
		}

		// Token: 0x06006232 RID: 25138 RVA: 0x00148758 File Offset: 0x00146958
		public T[] ToArray()
		{
			T[] array = new T[this._size];
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		// Token: 0x06006233 RID: 25139 RVA: 0x00148786 File Offset: 0x00146986
		// Note: this type is marked as 'beforefieldinit'.
		static LowLevelList()
		{
		}

		// Token: 0x04003A28 RID: 14888
		private const int _defaultCapacity = 4;

		// Token: 0x04003A29 RID: 14889
		protected T[] _items;

		// Token: 0x04003A2A RID: 14890
		protected int _size;

		// Token: 0x04003A2B RID: 14891
		protected int _version;

		// Token: 0x04003A2C RID: 14892
		private static readonly T[] s_emptyArray = new T[0];
	}
}
