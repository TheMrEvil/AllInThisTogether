using System;
using System.Collections;
using System.Collections.Generic;

namespace cakeslice
{
	// Token: 0x02000002 RID: 2
	public class LinkedSet<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public LinkedSet()
		{
			this.list = new LinkedList<T>();
			this.dictionary = new Dictionary<T, LinkedListNode<T>>();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000206E File Offset: 0x0000026E
		public LinkedSet(IEqualityComparer<T> comparer)
		{
			this.list = new LinkedList<T>();
			this.dictionary = new Dictionary<T, LinkedListNode<T>>(comparer);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002090 File Offset: 0x00000290
		public bool Add(T t)
		{
			if (this.dictionary.ContainsKey(t))
			{
				return false;
			}
			LinkedListNode<T> value = this.list.AddLast(t);
			this.dictionary.Add(t, value);
			return true;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020C8 File Offset: 0x000002C8
		public bool Remove(T t)
		{
			LinkedListNode<T> node;
			if (this.dictionary.TryGetValue(t, out node))
			{
				this.dictionary.Remove(t);
				this.list.Remove(node);
				return true;
			}
			return false;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002101 File Offset: 0x00000301
		public void Clear()
		{
			this.list.Clear();
			this.dictionary.Clear();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002119 File Offset: 0x00000319
		public bool Contains(T t)
		{
			return this.dictionary.ContainsKey(t);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002127 File Offset: 0x00000327
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002134 File Offset: 0x00000334
		public IEnumerator<T> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002146 File Offset: 0x00000346
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x04000001 RID: 1
		private LinkedList<T> list;

		// Token: 0x04000002 RID: 2
		private Dictionary<T, LinkedListNode<T>> dictionary;
	}
}
