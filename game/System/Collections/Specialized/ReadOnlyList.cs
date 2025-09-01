using System;

namespace System.Collections.Specialized
{
	// Token: 0x020004B7 RID: 1207
	internal sealed class ReadOnlyList : IList, ICollection, IEnumerable
	{
		// Token: 0x06002705 RID: 9989 RVA: 0x00087AC2 File Offset: 0x00085CC2
		internal ReadOnlyList(IList list)
		{
			this._list = list;
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06002706 RID: 9990 RVA: 0x00087AD1 File Offset: 0x00085CD1
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06002707 RID: 9991 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06002708 RID: 9992 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06002709 RID: 9993 RVA: 0x00087ADE File Offset: 0x00085CDE
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		// Token: 0x170007F3 RID: 2035
		public object this[int index]
		{
			get
			{
				return this._list[index];
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x0600270C RID: 9996 RVA: 0x00087AF9 File Offset: 0x00085CF9
		public object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x0003577D File Offset: 0x0003397D
		public int Add(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x0003577D File Offset: 0x0003397D
		public void Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x00087B06 File Offset: 0x00085D06
		public bool Contains(object value)
		{
			return this._list.Contains(value);
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x00087B14 File Offset: 0x00085D14
		public void CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x00087B23 File Offset: 0x00085D23
		public IEnumerator GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x00087B30 File Offset: 0x00085D30
		public int IndexOf(object value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x0003577D File Offset: 0x0003397D
		public void Insert(int index, object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x0003577D File Offset: 0x0003397D
		public void Remove(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x0003577D File Offset: 0x0003397D
		public void RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x04001520 RID: 5408
		private readonly IList _list;
	}
}
