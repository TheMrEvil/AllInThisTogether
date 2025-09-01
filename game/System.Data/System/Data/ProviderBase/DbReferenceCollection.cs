using System;
using System.Threading;

namespace System.Data.ProviderBase
{
	// Token: 0x02000352 RID: 850
	internal abstract class DbReferenceCollection
	{
		// Token: 0x060026EE RID: 9966 RVA: 0x000AC769 File Offset: 0x000AA969
		protected DbReferenceCollection()
		{
			this._items = new DbReferenceCollection.CollectionEntry[20];
			this._itemLock = new object();
			this._optimisticCount = 0;
			this._lastItemIndex = 0;
		}

		// Token: 0x060026EF RID: 9967
		public abstract void Add(object value, int tag);

		// Token: 0x060026F0 RID: 9968 RVA: 0x000AC798 File Offset: 0x000AA998
		protected void AddItem(object value, int tag)
		{
			bool flag = false;
			object itemLock = this._itemLock;
			lock (itemLock)
			{
				for (int i = 0; i <= this._lastItemIndex; i++)
				{
					if (this._items[i].Tag == 0)
					{
						this._items[i].NewTarget(tag, value);
						flag = true;
						break;
					}
				}
				if (!flag && this._lastItemIndex + 1 < this._items.Length)
				{
					this._lastItemIndex++;
					this._items[this._lastItemIndex].NewTarget(tag, value);
					flag = true;
				}
				if (!flag)
				{
					for (int j = 0; j <= this._lastItemIndex; j++)
					{
						if (!this._items[j].HasTarget)
						{
							this._items[j].NewTarget(tag, value);
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					Array.Resize<DbReferenceCollection.CollectionEntry>(ref this._items, this._items.Length * 2);
					this._lastItemIndex++;
					this._items[this._lastItemIndex].NewTarget(tag, value);
				}
				this._optimisticCount++;
			}
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x000AC8E8 File Offset: 0x000AAAE8
		internal T FindItem<T>(int tag, Func<T, bool> filterMethod) where T : class
		{
			bool flag = false;
			try
			{
				this.TryEnterItemLock(ref flag);
				if (flag && this._optimisticCount > 0)
				{
					for (int i = 0; i <= this._lastItemIndex; i++)
					{
						if (this._items[i].Tag == tag)
						{
							object target = this._items[i].Target;
							if (target != null)
							{
								T t = target as T;
								if (t != null && filterMethod(t))
								{
									return t;
								}
							}
						}
					}
				}
			}
			finally
			{
				this.ExitItemLockIfNeeded(flag);
			}
			return default(T);
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x000AC990 File Offset: 0x000AAB90
		public void Notify(int message)
		{
			bool flag = false;
			try
			{
				this.TryEnterItemLock(ref flag);
				if (flag)
				{
					try
					{
						this._isNotifying = true;
						if (this._optimisticCount > 0)
						{
							for (int i = 0; i <= this._lastItemIndex; i++)
							{
								object target = this._items[i].Target;
								if (target != null)
								{
									this.NotifyItem(message, this._items[i].Tag, target);
									this._items[i].RemoveTarget();
								}
							}
							this._optimisticCount = 0;
						}
						if (this._items.Length > 100)
						{
							this._lastItemIndex = 0;
							this._items = new DbReferenceCollection.CollectionEntry[20];
						}
					}
					finally
					{
						this._isNotifying = false;
					}
				}
			}
			finally
			{
				this.ExitItemLockIfNeeded(flag);
			}
		}

		// Token: 0x060026F3 RID: 9971
		protected abstract void NotifyItem(int message, int tag, object value);

		// Token: 0x060026F4 RID: 9972
		public abstract void Remove(object value);

		// Token: 0x060026F5 RID: 9973 RVA: 0x000ACA68 File Offset: 0x000AAC68
		protected void RemoveItem(object value)
		{
			bool flag = false;
			try
			{
				this.TryEnterItemLock(ref flag);
				if (flag && this._optimisticCount > 0)
				{
					for (int i = 0; i <= this._lastItemIndex; i++)
					{
						if (value == this._items[i].Target)
						{
							this._items[i].RemoveTarget();
							this._optimisticCount--;
							break;
						}
					}
				}
			}
			finally
			{
				this.ExitItemLockIfNeeded(flag);
			}
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x000ACAEC File Offset: 0x000AACEC
		private void TryEnterItemLock(ref bool lockObtained)
		{
			lockObtained = false;
			while (!this._isNotifying && !lockObtained)
			{
				Monitor.TryEnter(this._itemLock, 100, ref lockObtained);
			}
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x000ACB0F File Offset: 0x000AAD0F
		private void ExitItemLockIfNeeded(bool lockObtained)
		{
			if (lockObtained)
			{
				Monitor.Exit(this._itemLock);
			}
		}

		// Token: 0x0400196F RID: 6511
		private const int LockPollTime = 100;

		// Token: 0x04001970 RID: 6512
		private const int DefaultCollectionSize = 20;

		// Token: 0x04001971 RID: 6513
		private DbReferenceCollection.CollectionEntry[] _items;

		// Token: 0x04001972 RID: 6514
		private readonly object _itemLock;

		// Token: 0x04001973 RID: 6515
		private int _optimisticCount;

		// Token: 0x04001974 RID: 6516
		private int _lastItemIndex;

		// Token: 0x04001975 RID: 6517
		private volatile bool _isNotifying;

		// Token: 0x02000353 RID: 851
		private struct CollectionEntry
		{
			// Token: 0x060026F8 RID: 9976 RVA: 0x000ACB1F File Offset: 0x000AAD1F
			public void NewTarget(int tag, object target)
			{
				if (this._weak == null)
				{
					this._weak = new WeakReference(target, false);
				}
				else
				{
					this._weak.Target = target;
				}
				this._tag = tag;
			}

			// Token: 0x060026F9 RID: 9977 RVA: 0x000ACB4B File Offset: 0x000AAD4B
			public void RemoveTarget()
			{
				this._tag = 0;
			}

			// Token: 0x17000699 RID: 1689
			// (get) Token: 0x060026FA RID: 9978 RVA: 0x000ACB54 File Offset: 0x000AAD54
			public bool HasTarget
			{
				get
				{
					return this._tag != 0 && this._weak.IsAlive;
				}
			}

			// Token: 0x1700069A RID: 1690
			// (get) Token: 0x060026FB RID: 9979 RVA: 0x000ACB6B File Offset: 0x000AAD6B
			public int Tag
			{
				get
				{
					return this._tag;
				}
			}

			// Token: 0x1700069B RID: 1691
			// (get) Token: 0x060026FC RID: 9980 RVA: 0x000ACB73 File Offset: 0x000AAD73
			public object Target
			{
				get
				{
					if (this._tag != 0)
					{
						return this._weak.Target;
					}
					return null;
				}
			}

			// Token: 0x04001976 RID: 6518
			private int _tag;

			// Token: 0x04001977 RID: 6519
			private WeakReference _weak;
		}
	}
}
