using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.Rendering
{
	// Token: 0x02000061 RID: 97
	public class ObservableList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000306 RID: 774 RVA: 0x0000EC78 File Offset: 0x0000CE78
		// (remove) Token: 0x06000307 RID: 775 RVA: 0x0000ECB0 File Offset: 0x0000CEB0
		public event ListChangedEventHandler<T> ItemAdded
		{
			[CompilerGenerated]
			add
			{
				ListChangedEventHandler<T> listChangedEventHandler = this.ItemAdded;
				ListChangedEventHandler<T> listChangedEventHandler2;
				do
				{
					listChangedEventHandler2 = listChangedEventHandler;
					ListChangedEventHandler<T> value2 = (ListChangedEventHandler<T>)Delegate.Combine(listChangedEventHandler2, value);
					listChangedEventHandler = Interlocked.CompareExchange<ListChangedEventHandler<T>>(ref this.ItemAdded, value2, listChangedEventHandler2);
				}
				while (listChangedEventHandler != listChangedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ListChangedEventHandler<T> listChangedEventHandler = this.ItemAdded;
				ListChangedEventHandler<T> listChangedEventHandler2;
				do
				{
					listChangedEventHandler2 = listChangedEventHandler;
					ListChangedEventHandler<T> value2 = (ListChangedEventHandler<T>)Delegate.Remove(listChangedEventHandler2, value);
					listChangedEventHandler = Interlocked.CompareExchange<ListChangedEventHandler<T>>(ref this.ItemAdded, value2, listChangedEventHandler2);
				}
				while (listChangedEventHandler != listChangedEventHandler2);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000308 RID: 776 RVA: 0x0000ECE8 File Offset: 0x0000CEE8
		// (remove) Token: 0x06000309 RID: 777 RVA: 0x0000ED20 File Offset: 0x0000CF20
		public event ListChangedEventHandler<T> ItemRemoved
		{
			[CompilerGenerated]
			add
			{
				ListChangedEventHandler<T> listChangedEventHandler = this.ItemRemoved;
				ListChangedEventHandler<T> listChangedEventHandler2;
				do
				{
					listChangedEventHandler2 = listChangedEventHandler;
					ListChangedEventHandler<T> value2 = (ListChangedEventHandler<T>)Delegate.Combine(listChangedEventHandler2, value);
					listChangedEventHandler = Interlocked.CompareExchange<ListChangedEventHandler<T>>(ref this.ItemRemoved, value2, listChangedEventHandler2);
				}
				while (listChangedEventHandler != listChangedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ListChangedEventHandler<T> listChangedEventHandler = this.ItemRemoved;
				ListChangedEventHandler<T> listChangedEventHandler2;
				do
				{
					listChangedEventHandler2 = listChangedEventHandler;
					ListChangedEventHandler<T> value2 = (ListChangedEventHandler<T>)Delegate.Remove(listChangedEventHandler2, value);
					listChangedEventHandler = Interlocked.CompareExchange<ListChangedEventHandler<T>>(ref this.ItemRemoved, value2, listChangedEventHandler2);
				}
				while (listChangedEventHandler != listChangedEventHandler2);
			}
		}

		// Token: 0x17000050 RID: 80
		public T this[int index]
		{
			get
			{
				return this.m_List[index];
			}
			set
			{
				this.OnEvent(this.ItemRemoved, index, this.m_List[index]);
				this.m_List[index] = value;
				this.OnEvent(this.ItemAdded, index, value);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000ED99 File Offset: 0x0000CF99
		public int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000EDA6 File Offset: 0x0000CFA6
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000EDA9 File Offset: 0x0000CFA9
		public ObservableList() : this(0)
		{
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000EDB2 File Offset: 0x0000CFB2
		public ObservableList(int capacity)
		{
			this.m_List = new List<T>(capacity);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000EDC6 File Offset: 0x0000CFC6
		public ObservableList(IEnumerable<T> collection)
		{
			this.m_List = new List<T>(collection);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000EDDA File Offset: 0x0000CFDA
		private void OnEvent(ListChangedEventHandler<T> e, int index, T item)
		{
			if (e != null)
			{
				e(this, new ListChangedEventArgs<T>(index, item));
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000EDED File Offset: 0x0000CFED
		public bool Contains(T item)
		{
			return this.m_List.Contains(item);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000EDFB File Offset: 0x0000CFFB
		public int IndexOf(T item)
		{
			return this.m_List.IndexOf(item);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000EE09 File Offset: 0x0000D009
		public void Add(T item)
		{
			this.m_List.Add(item);
			this.OnEvent(this.ItemAdded, this.m_List.IndexOf(item), item);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000EE30 File Offset: 0x0000D030
		public void Add(params T[] items)
		{
			foreach (T item in items)
			{
				this.Add(item);
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000EE5C File Offset: 0x0000D05C
		public void Insert(int index, T item)
		{
			this.m_List.Insert(index, item);
			this.OnEvent(this.ItemAdded, index, item);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000EE7C File Offset: 0x0000D07C
		public bool Remove(T item)
		{
			int index = this.m_List.IndexOf(item);
			bool flag = this.m_List.Remove(item);
			if (flag)
			{
				this.OnEvent(this.ItemRemoved, index, item);
			}
			return flag;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000EEB4 File Offset: 0x0000D0B4
		public int Remove(params T[] items)
		{
			if (items == null)
			{
				return 0;
			}
			int num = 0;
			foreach (T item in items)
			{
				num += (this.Remove(item) ? 1 : 0);
			}
			return num;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000EEF4 File Offset: 0x0000D0F4
		public void RemoveAt(int index)
		{
			T item = this.m_List[index];
			this.m_List.RemoveAt(index);
			this.OnEvent(this.ItemRemoved, index, item);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000EF28 File Offset: 0x0000D128
		public void Clear()
		{
			while (this.Count > 0)
			{
				this.RemoveAt(this.Count - 1);
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000EF43 File Offset: 0x0000D143
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.m_List.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000EF52 File Offset: 0x0000D152
		public IEnumerator<T> GetEnumerator()
		{
			return this.m_List.GetEnumerator();
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000EF5F File Offset: 0x0000D15F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000206 RID: 518
		private IList<T> m_List;

		// Token: 0x04000207 RID: 519
		[CompilerGenerated]
		private ListChangedEventHandler<T> ItemAdded;

		// Token: 0x04000208 RID: 520
		[CompilerGenerated]
		private ListChangedEventHandler<T> ItemRemoved;
	}
}
