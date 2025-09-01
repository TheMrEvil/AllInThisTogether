using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Xsl
{
	// Token: 0x02000327 RID: 807
	internal abstract class ListBase<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
	{
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06002125 RID: 8485
		public abstract int Count { get; }

		// Token: 0x1700066F RID: 1647
		public abstract T this[int index]
		{
			get;
			set;
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x000D270C File Offset: 0x000D090C
		public virtual bool Contains(T value)
		{
			return this.IndexOf(value) != -1;
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x000D271C File Offset: 0x000D091C
		public virtual int IndexOf(T value)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (value.Equals(this[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x000D2758 File Offset: 0x000D0958
		public virtual void CopyTo(T[] array, int index)
		{
			for (int i = 0; i < this.Count; i++)
			{
				array[index + i] = this[i];
			}
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x000D2786 File Offset: 0x000D0986
		public virtual IListEnumerator<T> GetEnumerator()
		{
			return new IListEnumerator<T>(this);
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x0600212C RID: 8492 RVA: 0x0001222F File Offset: 0x0001042F
		public virtual bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x0600212D RID: 8493 RVA: 0x0001222F File Offset: 0x0001042F
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x000D278E File Offset: 0x000D098E
		public virtual void Add(T value)
		{
			this.Insert(this.Count, value);
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public virtual void Insert(int index, T value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x000D27A0 File Offset: 0x000D09A0
		public virtual bool Remove(T value)
		{
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public virtual void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x000D27C4 File Offset: 0x000D09C4
		public virtual void Clear()
		{
			for (int i = this.Count - 1; i >= 0; i--)
			{
				this.RemoveAt(i);
			}
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x000D27EB File Offset: 0x000D09EB
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return new IListEnumerator<T>(this);
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x000D27EB File Offset: 0x000D09EB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new IListEnumerator<T>(this);
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x000D27F8 File Offset: 0x000D09F8
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06002136 RID: 8502 RVA: 0x00002068 File Offset: 0x00000268
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x000D2800 File Offset: 0x000D0A00
		void ICollection.CopyTo(Array array, int index)
		{
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index);
			}
		}

		// Token: 0x17000674 RID: 1652
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (!ListBase<T>.IsCompatibleType(value.GetType()))
				{
					throw new ArgumentException(Res.GetString("Type is incompatible."), "value");
				}
				this[index] = (T)((object)value);
			}
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x000D2870 File Offset: 0x000D0A70
		int IList.Add(object value)
		{
			if (!ListBase<T>.IsCompatibleType(value.GetType()))
			{
				throw new ArgumentException(Res.GetString("Type is incompatible."), "value");
			}
			this.Add((T)((object)value));
			return this.Count - 1;
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x000D28A8 File Offset: 0x000D0AA8
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x000D28B0 File Offset: 0x000D0AB0
		bool IList.Contains(object value)
		{
			return ListBase<T>.IsCompatibleType(value.GetType()) && this.Contains((T)((object)value));
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x000D28CD File Offset: 0x000D0ACD
		int IList.IndexOf(object value)
		{
			if (!ListBase<T>.IsCompatibleType(value.GetType()))
			{
				return -1;
			}
			return this.IndexOf((T)((object)value));
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x000D28EA File Offset: 0x000D0AEA
		void IList.Insert(int index, object value)
		{
			if (!ListBase<T>.IsCompatibleType(value.GetType()))
			{
				throw new ArgumentException(Res.GetString("Type is incompatible."), "value");
			}
			this.Insert(index, (T)((object)value));
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x000D291B File Offset: 0x000D0B1B
		void IList.Remove(object value)
		{
			if (ListBase<T>.IsCompatibleType(value.GetType()))
			{
				this.Remove((T)((object)value));
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x000D2937 File Offset: 0x000D0B37
		private static bool IsCompatibleType(object value)
		{
			return (value == null && !typeof(T).IsValueType) || value is T;
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x0000216B File Offset: 0x0000036B
		protected ListBase()
		{
		}
	}
}
