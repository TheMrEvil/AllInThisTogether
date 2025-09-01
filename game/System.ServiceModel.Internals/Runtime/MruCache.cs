using System;
using System.Collections.Generic;

namespace System.Runtime
{
	// Token: 0x02000024 RID: 36
	internal class MruCache<TKey, TValue> where TKey : class where TValue : class
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00005352 File Offset: 0x00003552
		public MruCache(int watermark) : this(watermark * 4 / 5, watermark)
		{
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005360 File Offset: 0x00003560
		public MruCache(int lowWatermark, int highWatermark) : this(lowWatermark, highWatermark, null)
		{
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000536B File Offset: 0x0000356B
		public MruCache(int lowWatermark, int highWatermark, IEqualityComparer<TKey> comparer)
		{
			this.lowWatermark = lowWatermark;
			this.highWatermark = highWatermark;
			this.mruList = new LinkedList<TKey>();
			if (comparer == null)
			{
				this.items = new Dictionary<TKey, MruCache<TKey, TValue>.CacheEntry>();
				return;
			}
			this.items = new Dictionary<TKey, MruCache<TKey, TValue>.CacheEntry>(comparer);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000053A7 File Offset: 0x000035A7
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000053B4 File Offset: 0x000035B4
		public void Add(TKey key, TValue value)
		{
			bool flag = false;
			try
			{
				if (this.items.Count == this.highWatermark)
				{
					int num = this.highWatermark - this.lowWatermark;
					for (int i = 0; i < num; i++)
					{
						TKey value2 = this.mruList.Last.Value;
						this.mruList.RemoveLast();
						TValue value3 = this.items[value2].value;
						this.items.Remove(value2);
						this.OnSingleItemRemoved(value3);
						this.OnItemAgedOutOfCache(value3);
					}
				}
				MruCache<TKey, TValue>.CacheEntry value4;
				value4.node = this.mruList.AddFirst(key);
				value4.value = value;
				this.items.Add(key, value4);
				this.mruEntry = value4;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.Clear();
				}
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000548C File Offset: 0x0000368C
		public void Clear()
		{
			this.mruList.Clear();
			this.items.Clear();
			this.mruEntry.value = default(TValue);
			this.mruEntry.node = null;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000054C4 File Offset: 0x000036C4
		public bool Remove(TKey key)
		{
			MruCache<TKey, TValue>.CacheEntry cacheEntry;
			if (this.items.TryGetValue(key, out cacheEntry))
			{
				this.items.Remove(key);
				this.OnSingleItemRemoved(cacheEntry.value);
				this.mruList.Remove(cacheEntry.node);
				if (this.mruEntry.node == cacheEntry.node)
				{
					this.mruEntry.value = default(TValue);
					this.mruEntry.node = null;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000553E File Offset: 0x0000373E
		protected virtual void OnSingleItemRemoved(TValue item)
		{
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005540 File Offset: 0x00003740
		protected virtual void OnItemAgedOutOfCache(TValue item)
		{
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005544 File Offset: 0x00003744
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (this.mruEntry.node != null && key != null && key.Equals(this.mruEntry.node.Value))
			{
				value = this.mruEntry.value;
				return true;
			}
			MruCache<TKey, TValue>.CacheEntry cacheEntry;
			bool flag = this.items.TryGetValue(key, out cacheEntry);
			value = cacheEntry.value;
			if (flag && this.mruList.Count > 1 && this.mruList.First != cacheEntry.node)
			{
				this.mruList.Remove(cacheEntry.node);
				this.mruList.AddFirst(cacheEntry.node);
				this.mruEntry = cacheEntry;
			}
			return flag;
		}

		// Token: 0x040000BC RID: 188
		private LinkedList<TKey> mruList;

		// Token: 0x040000BD RID: 189
		private Dictionary<TKey, MruCache<TKey, TValue>.CacheEntry> items;

		// Token: 0x040000BE RID: 190
		private int lowWatermark;

		// Token: 0x040000BF RID: 191
		private int highWatermark;

		// Token: 0x040000C0 RID: 192
		private MruCache<TKey, TValue>.CacheEntry mruEntry;

		// Token: 0x0200007F RID: 127
		private struct CacheEntry
		{
			// Token: 0x040002B5 RID: 693
			internal TValue value;

			// Token: 0x040002B6 RID: 694
			internal LinkedListNode<TKey> node;
		}
	}
}
