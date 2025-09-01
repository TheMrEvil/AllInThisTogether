using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200047C RID: 1148
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class XmlQuerySequence<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
	{
		// Token: 0x06002CCE RID: 11470 RVA: 0x00108737 File Offset: 0x00106937
		public static XmlQuerySequence<T> CreateOrReuse(XmlQuerySequence<T> seq)
		{
			if (seq != null)
			{
				seq.Clear();
				return seq;
			}
			return new XmlQuerySequence<T>();
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x00108749 File Offset: 0x00106949
		public static XmlQuerySequence<T> CreateOrReuse(XmlQuerySequence<T> seq, T item)
		{
			if (seq != null)
			{
				seq.Clear();
				seq.Add(item);
				return seq;
			}
			return new XmlQuerySequence<T>(item);
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x00108763 File Offset: 0x00106963
		public XmlQuerySequence()
		{
			this.items = new T[16];
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x00108778 File Offset: 0x00106978
		public XmlQuerySequence(int capacity)
		{
			this.items = new T[capacity];
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x0010878C File Offset: 0x0010698C
		public XmlQuerySequence(T[] array, int size)
		{
			this.items = array;
			this.size = size;
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x001087A2 File Offset: 0x001069A2
		public XmlQuerySequence(T value)
		{
			this.items = new T[1];
			this.items[0] = value;
			this.size = 1;
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000D27EB File Offset: 0x000D09EB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new IListEnumerator<T>(this);
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x000D27EB File Offset: 0x000D09EB
		public IEnumerator<T> GetEnumerator()
		{
			return new IListEnumerator<T>(this);
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x001087CA File Offset: 0x001069CA
		public int Count
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06002CD7 RID: 11479 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002CD8 RID: 11480 RVA: 0x00002068 File Offset: 0x00000268
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x001087D2 File Offset: 0x001069D2
		void ICollection.CopyTo(Array array, int index)
		{
			if (this.size == 0)
			{
				return;
			}
			Array.Copy(this.items, 0, array, index, this.size);
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002CDA RID: 11482 RVA: 0x0001222F File Offset: 0x0001042F
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x00005BD6 File Offset: 0x00003DD6
		void ICollection<!0>.Add(T value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x00005BD6 File Offset: 0x00003DD6
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x001087F1 File Offset: 0x001069F1
		public bool Contains(T value)
		{
			return this.IndexOf(value) != -1;
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x00108800 File Offset: 0x00106A00
		public void CopyTo(T[] array, int index)
		{
			for (int i = 0; i < this.Count; i++)
			{
				array[index + i] = this[i];
			}
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x00005BD6 File Offset: 0x00003DD6
		bool ICollection<!0>.Remove(T value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x0001222F File Offset: 0x0001042F
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x0001222F File Offset: 0x0001042F
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700086C RID: 2156
		object IList.this[int index]
		{
			get
			{
				if (index >= this.size)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.items[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x00005BD6 File Offset: 0x00003DD6
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x00005BD6 File Offset: 0x00003DD6
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x00108855 File Offset: 0x00106A55
		bool IList.Contains(object value)
		{
			return this.Contains((T)((object)value));
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x00108863 File Offset: 0x00106A63
		int IList.IndexOf(object value)
		{
			return this.IndexOf((T)((object)value));
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x00005BD6 File Offset: 0x00003DD6
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x00005BD6 File Offset: 0x00003DD6
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x00005BD6 File Offset: 0x00003DD6
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700086D RID: 2157
		public T this[int index]
		{
			get
			{
				if (index >= this.size)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.items[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x00108894 File Offset: 0x00106A94
		public int IndexOf(T value)
		{
			int num = Array.IndexOf<T>(this.items, value);
			if (num >= this.size)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x00005BD6 File Offset: 0x00003DD6
		void IList<!0>.Insert(int index, T value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x00005BD6 File Offset: 0x00003DD6
		void IList<!0>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x001088BA File Offset: 0x00106ABA
		public void Clear()
		{
			this.size = 0;
			this.OnItemsChanged();
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x001088CC File Offset: 0x00106ACC
		public void Add(T value)
		{
			this.EnsureCache();
			T[] array = this.items;
			int num = this.size;
			this.size = num + 1;
			array[num] = value;
			this.OnItemsChanged();
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x00108902 File Offset: 0x00106B02
		public void SortByKeys(Array keys)
		{
			if (this.size <= 1)
			{
				return;
			}
			Array.Sort(keys, this.items, 0, this.size);
			this.OnItemsChanged();
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x00108928 File Offset: 0x00106B28
		private void EnsureCache()
		{
			if (this.size >= this.items.Length)
			{
				T[] array = new T[this.size * 2];
				this.CopyTo(array, 0);
				this.items = array;
			}
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x0000B528 File Offset: 0x00009728
		protected virtual void OnItemsChanged()
		{
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x00108962 File Offset: 0x00106B62
		// Note: this type is marked as 'beforefieldinit'.
		static XmlQuerySequence()
		{
		}

		// Token: 0x0400230C RID: 8972
		public static readonly XmlQuerySequence<T> Empty = new XmlQuerySequence<T>();

		// Token: 0x0400230D RID: 8973
		private static readonly Type XPathItemType = typeof(XPathItem);

		// Token: 0x0400230E RID: 8974
		private T[] items;

		// Token: 0x0400230F RID: 8975
		private int size;

		// Token: 0x04002310 RID: 8976
		private const int DefaultCacheSize = 16;
	}
}
