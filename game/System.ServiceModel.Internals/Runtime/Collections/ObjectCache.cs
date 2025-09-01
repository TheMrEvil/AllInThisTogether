using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Runtime.Collections
{
	// Token: 0x02000052 RID: 82
	internal class ObjectCache<TKey, TValue> where TValue : class
	{
		// Token: 0x060002FB RID: 763 RVA: 0x0001006C File Offset: 0x0000E26C
		public ObjectCache(ObjectCacheSettings settings) : this(settings, null)
		{
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00010078 File Offset: 0x0000E278
		public ObjectCache(ObjectCacheSettings settings, IEqualityComparer<TKey> comparer)
		{
			this.settings = settings.Clone();
			this.cacheItems = new Dictionary<TKey, ObjectCache<TKey, TValue>.Item>(comparer);
			this.idleTimeoutEnabled = (settings.IdleTimeout != TimeSpan.MaxValue);
			this.leaseTimeoutEnabled = (settings.LeaseTimeout != TimeSpan.MaxValue);
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002FD RID: 765 RVA: 0x000100CF File Offset: 0x0000E2CF
		private object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002FE RID: 766 RVA: 0x000100D2 File Offset: 0x0000E2D2
		// (set) Token: 0x060002FF RID: 767 RVA: 0x000100DA File Offset: 0x0000E2DA
		public Action<TValue> DisposeItemCallback
		{
			[CompilerGenerated]
			get
			{
				return this.<DisposeItemCallback>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DisposeItemCallback>k__BackingField = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000300 RID: 768 RVA: 0x000100E3 File Offset: 0x0000E2E3
		public int Count
		{
			get
			{
				return this.cacheItems.Count;
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000100F0 File Offset: 0x0000E2F0
		public ObjectCacheItem<TValue> Add(TKey key, TValue value)
		{
			object thisLock = this.ThisLock;
			ObjectCacheItem<TValue> result;
			lock (thisLock)
			{
				if (this.Count >= this.settings.CacheLimit || this.cacheItems.ContainsKey(key))
				{
					result = new ObjectCache<TKey, TValue>.Item(key, value, this.DisposeItemCallback);
				}
				else
				{
					result = this.InternalAdd(key, value);
				}
			}
			return result;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00010168 File Offset: 0x0000E368
		public ObjectCacheItem<TValue> Take(TKey key)
		{
			return this.Take(key, null);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00010174 File Offset: 0x0000E374
		public ObjectCacheItem<TValue> Take(TKey key, Func<TValue> initializerDelegate)
		{
			ObjectCache<TKey, TValue>.Item item = null;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.cacheItems.TryGetValue(key, out item))
				{
					item.InternalAddReference();
				}
				else
				{
					if (initializerDelegate == null)
					{
						return null;
					}
					TValue value = initializerDelegate();
					if (this.Count >= this.settings.CacheLimit)
					{
						return new ObjectCache<TKey, TValue>.Item(key, value, this.DisposeItemCallback);
					}
					item = this.InternalAdd(key, value);
				}
			}
			return item;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00010208 File Offset: 0x0000E408
		private ObjectCache<TKey, TValue>.Item InternalAdd(TKey key, TValue value)
		{
			ObjectCache<TKey, TValue>.Item item = new ObjectCache<TKey, TValue>.Item(key, value, this);
			if (this.leaseTimeoutEnabled)
			{
				item.CreationTime = DateTime.UtcNow;
			}
			this.cacheItems.Add(key, item);
			this.StartTimerIfNecessary();
			return item;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00010248 File Offset: 0x0000E448
		private bool Return(TKey key, ObjectCache<TKey, TValue>.Item cacheItem)
		{
			bool result = false;
			if (this.disposed)
			{
				result = true;
			}
			else
			{
				cacheItem.InternalReleaseReference();
				DateTime utcNow = DateTime.UtcNow;
				if (this.idleTimeoutEnabled)
				{
					cacheItem.LastUsage = utcNow;
				}
				if (this.ShouldPurgeItem(cacheItem, utcNow))
				{
					this.cacheItems.Remove(key);
					cacheItem.LockedDispose();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000102A0 File Offset: 0x0000E4A0
		private void StartTimerIfNecessary()
		{
			if (this.idleTimeoutEnabled && this.Count > 1)
			{
				if (this.idleTimer == null)
				{
					if (ObjectCache<TKey, TValue>.onIdle == null)
					{
						ObjectCache<TKey, TValue>.onIdle = new Action<object>(ObjectCache<TKey, TValue>.OnIdle);
					}
					this.idleTimer = new IOThreadTimer(ObjectCache<TKey, TValue>.onIdle, this, false);
				}
				this.idleTimer.Set(this.settings.IdleTimeout);
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00010306 File Offset: 0x0000E506
		private static void OnIdle(object state)
		{
			((ObjectCache<TKey, TValue>)state).PurgeCache(true);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00010314 File Offset: 0x0000E514
		private static void Add<T>(ref List<T> list, T item)
		{
			if (list == null)
			{
				list = new List<T>();
			}
			list.Add(item);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0001032C File Offset: 0x0000E52C
		private bool ShouldPurgeItem(ObjectCache<TKey, TValue>.Item cacheItem, DateTime now)
		{
			return cacheItem.ReferenceCount <= 0 && ((this.idleTimeoutEnabled && now >= cacheItem.LastUsage + this.settings.IdleTimeout) || (this.leaseTimeoutEnabled && now - cacheItem.CreationTime >= this.settings.LeaseTimeout));
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00010398 File Offset: 0x0000E598
		private void GatherExpiredItems(ref List<KeyValuePair<TKey, ObjectCache<TKey, TValue>.Item>> expiredItems, bool calledFromTimer)
		{
			if (this.Count == 0)
			{
				return;
			}
			if (!this.leaseTimeoutEnabled && !this.idleTimeoutEnabled)
			{
				return;
			}
			DateTime utcNow = DateTime.UtcNow;
			bool flag = false;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				foreach (KeyValuePair<TKey, ObjectCache<TKey, TValue>.Item> item in this.cacheItems)
				{
					if (this.ShouldPurgeItem(item.Value, utcNow))
					{
						item.Value.LockedDispose();
						ObjectCache<TKey, TValue>.Add<KeyValuePair<TKey, ObjectCache<TKey, TValue>.Item>>(ref expiredItems, item);
					}
				}
				if (expiredItems != null)
				{
					for (int i = 0; i < expiredItems.Count; i++)
					{
						this.cacheItems.Remove(expiredItems[i].Key);
					}
				}
				flag = (calledFromTimer && this.Count > 0);
			}
			if (flag)
			{
				this.idleTimer.Set(this.settings.IdleTimeout);
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000104B8 File Offset: 0x0000E6B8
		private void PurgeCache(bool calledFromTimer)
		{
			List<KeyValuePair<TKey, ObjectCache<TKey, TValue>.Item>> list = null;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				this.GatherExpiredItems(ref list, calledFromTimer);
			}
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					list[i].Value.LocalDispose();
				}
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00010528 File Offset: 0x0000E728
		public void Dispose()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				foreach (ObjectCache<TKey, TValue>.Item item in this.cacheItems.Values)
				{
					if (item != null)
					{
						item.Dispose();
					}
				}
				this.cacheItems.Clear();
				this.settings.CacheLimit = 0;
				this.disposed = true;
				if (this.idleTimer != null)
				{
					this.idleTimer.Cancel();
					this.idleTimer = null;
				}
			}
		}

		// Token: 0x040001F3 RID: 499
		private const int timerThreshold = 1;

		// Token: 0x040001F4 RID: 500
		private ObjectCacheSettings settings;

		// Token: 0x040001F5 RID: 501
		private Dictionary<TKey, ObjectCache<TKey, TValue>.Item> cacheItems;

		// Token: 0x040001F6 RID: 502
		private bool idleTimeoutEnabled;

		// Token: 0x040001F7 RID: 503
		private bool leaseTimeoutEnabled;

		// Token: 0x040001F8 RID: 504
		private IOThreadTimer idleTimer;

		// Token: 0x040001F9 RID: 505
		private static Action<object> onIdle;

		// Token: 0x040001FA RID: 506
		private bool disposed;

		// Token: 0x040001FB RID: 507
		[CompilerGenerated]
		private Action<TValue> <DisposeItemCallback>k__BackingField;

		// Token: 0x02000097 RID: 151
		private class Item : ObjectCacheItem<TValue>
		{
			// Token: 0x0600042F RID: 1071 RVA: 0x00013336 File Offset: 0x00011536
			public Item(TKey key, TValue value, Action<TValue> disposeItemCallback) : this(key, value)
			{
				this.disposeItemCallback = disposeItemCallback;
			}

			// Token: 0x06000430 RID: 1072 RVA: 0x00013347 File Offset: 0x00011547
			public Item(TKey key, TValue value, ObjectCache<TKey, TValue> parent) : this(key, value)
			{
				this.parent = parent;
			}

			// Token: 0x06000431 RID: 1073 RVA: 0x00013358 File Offset: 0x00011558
			private Item(TKey key, TValue value)
			{
				this.key = key;
				this.value = value;
				this.referenceCount = 1;
			}

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x06000432 RID: 1074 RVA: 0x00013375 File Offset: 0x00011575
			public int ReferenceCount
			{
				get
				{
					return this.referenceCount;
				}
			}

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x06000433 RID: 1075 RVA: 0x0001337D File Offset: 0x0001157D
			public override TValue Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x06000434 RID: 1076 RVA: 0x00013385 File Offset: 0x00011585
			// (set) Token: 0x06000435 RID: 1077 RVA: 0x0001338D File Offset: 0x0001158D
			public DateTime CreationTime
			{
				[CompilerGenerated]
				get
				{
					return this.<CreationTime>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<CreationTime>k__BackingField = value;
				}
			}

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x06000436 RID: 1078 RVA: 0x00013396 File Offset: 0x00011596
			// (set) Token: 0x06000437 RID: 1079 RVA: 0x0001339E File Offset: 0x0001159E
			public DateTime LastUsage
			{
				[CompilerGenerated]
				get
				{
					return this.<LastUsage>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<LastUsage>k__BackingField = value;
				}
			}

			// Token: 0x06000438 RID: 1080 RVA: 0x000133A8 File Offset: 0x000115A8
			public override bool TryAddReference()
			{
				bool result;
				if (this.parent == null || this.referenceCount == -1)
				{
					result = false;
				}
				else
				{
					bool flag = false;
					object thisLock = this.parent.ThisLock;
					lock (thisLock)
					{
						if (this.referenceCount == -1)
						{
							result = false;
						}
						else if (this.referenceCount == 0 && this.parent.ShouldPurgeItem(this, DateTime.UtcNow))
						{
							this.LockedDispose();
							flag = true;
							result = false;
							this.parent.cacheItems.Remove(this.key);
						}
						else
						{
							this.referenceCount++;
							result = true;
						}
					}
					if (flag)
					{
						this.LocalDispose();
					}
				}
				return result;
			}

			// Token: 0x06000439 RID: 1081 RVA: 0x00013468 File Offset: 0x00011668
			public override void ReleaseReference()
			{
				bool flag;
				if (this.parent == null)
				{
					this.referenceCount = -1;
					flag = true;
				}
				else
				{
					object thisLock = this.parent.ThisLock;
					lock (thisLock)
					{
						if (this.referenceCount > 1)
						{
							this.InternalReleaseReference();
							flag = false;
						}
						else
						{
							flag = this.parent.Return(this.key, this);
						}
					}
				}
				if (flag)
				{
					this.LocalDispose();
				}
			}

			// Token: 0x0600043A RID: 1082 RVA: 0x000134EC File Offset: 0x000116EC
			internal void InternalAddReference()
			{
				this.referenceCount++;
			}

			// Token: 0x0600043B RID: 1083 RVA: 0x000134FC File Offset: 0x000116FC
			internal void InternalReleaseReference()
			{
				this.referenceCount--;
			}

			// Token: 0x0600043C RID: 1084 RVA: 0x0001350C File Offset: 0x0001170C
			public void LockedDispose()
			{
				this.referenceCount = -1;
			}

			// Token: 0x0600043D RID: 1085 RVA: 0x00013518 File Offset: 0x00011718
			public void Dispose()
			{
				if (this.Value != null)
				{
					Action<TValue> action = this.disposeItemCallback;
					if (this.parent != null)
					{
						action = this.parent.DisposeItemCallback;
					}
					if (action != null)
					{
						action(this.Value);
					}
					else if (this.Value is IDisposable)
					{
						((IDisposable)((object)this.Value)).Dispose();
					}
				}
				this.value = default(TValue);
				this.referenceCount = -1;
			}

			// Token: 0x0600043E RID: 1086 RVA: 0x00013598 File Offset: 0x00011798
			public void LocalDispose()
			{
				this.Dispose();
			}

			// Token: 0x040002FF RID: 767
			private readonly ObjectCache<TKey, TValue> parent;

			// Token: 0x04000300 RID: 768
			private readonly TKey key;

			// Token: 0x04000301 RID: 769
			private readonly Action<TValue> disposeItemCallback;

			// Token: 0x04000302 RID: 770
			private TValue value;

			// Token: 0x04000303 RID: 771
			private int referenceCount;

			// Token: 0x04000304 RID: 772
			[CompilerGenerated]
			private DateTime <CreationTime>k__BackingField;

			// Token: 0x04000305 RID: 773
			[CompilerGenerated]
			private DateTime <LastUsage>k__BackingField;
		}
	}
}
