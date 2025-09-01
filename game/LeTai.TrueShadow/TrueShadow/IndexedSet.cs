using System;
using System.Collections;
using System.Collections.Generic;

namespace LeTai.TrueShadow
{
	// Token: 0x02000019 RID: 25
	internal class IndexedSet<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000102 RID: 258 RVA: 0x000064FF File Offset: 0x000046FF
		public void Add(T item)
		{
			this.dict.Add(item, this.list.Count);
			this.list.Add(item);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006524 File Offset: 0x00004724
		public bool AddUnique(T item)
		{
			if (this.dict.ContainsKey(item))
			{
				return false;
			}
			this.dict.Add(item, this.list.Count);
			this.list.Add(item);
			return true;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000655C File Offset: 0x0000475C
		public bool Remove(T item)
		{
			int index;
			if (!this.dict.TryGetValue(item, out index))
			{
				return false;
			}
			this.RemoveAt(index);
			return true;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006584 File Offset: 0x00004784
		public void Remove(Predicate<T> match)
		{
			int i = 0;
			while (i < this.list.Count)
			{
				T t = this.list[i];
				if (match(t))
				{
					this.Remove(t);
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000065C7 File Offset: 0x000047C7
		public void Clear()
		{
			this.list.Clear();
			this.dict.Clear();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000065DF File Offset: 0x000047DF
		public bool Contains(T item)
		{
			return this.dict.ContainsKey(item);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000065ED File Offset: 0x000047ED
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.list.CopyTo(array, arrayIndex);
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000065FC File Offset: 0x000047FC
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00006609 File Offset: 0x00004809
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000660C File Offset: 0x0000480C
		public int IndexOf(T item)
		{
			int result;
			if (this.dict.TryGetValue(item, out result))
			{
				return result;
			}
			return -1;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000662C File Offset: 0x0000482C
		public void Insert(int index, T item)
		{
			throw new NotSupportedException("Random Insertion is semantically invalid, since this structure does not guarantee ordering.");
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006638 File Offset: 0x00004838
		public void RemoveAt(int index)
		{
			T key = this.list[index];
			this.dict.Remove(key);
			if (index == this.list.Count - 1)
			{
				this.list.RemoveAt(index);
				return;
			}
			int index2 = this.list.Count - 1;
			T t = this.list[index2];
			this.list[index] = t;
			this.dict[t] = index;
			this.list.RemoveAt(index2);
		}

		// Token: 0x17000031 RID: 49
		public T this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				T key = this.list[index];
				this.dict.Remove(key);
				this.list[index] = value;
				this.dict.Add(key, index);
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006710 File Offset: 0x00004910
		public void Sort(Comparison<T> sortLayoutFunction)
		{
			this.list.Sort(sortLayoutFunction);
			for (int i = 0; i < this.list.Count; i++)
			{
				T key = this.list[i];
				this.dict[key] = i;
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006759 File Offset: 0x00004959
		public IEnumerator<T> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000676B File Offset: 0x0000496B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006773 File Offset: 0x00004973
		public IndexedSet()
		{
		}

		// Token: 0x040000AF RID: 175
		private readonly List<T> list = new List<T>();

		// Token: 0x040000B0 RID: 176
		private readonly Dictionary<T, int> dict = new Dictionary<T, int>();
	}
}
